using NFPublicLib.Instructions.BitLogic;
using PDAMaster;
using S7.Net;
using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ScrewMachineManagementSystem.CenterControl;

namespace ScrewMachineManagementSystem
{
    public partial class FormMain : Form
    {
        BusinessMain _businessMain;
        public FormMain()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            CheckForIllegalCrossThreadCalls = false;
            InitilizeScrewDataTable();
        }
        #region 定义2
        DataTable _dt_screwDataTable;

        Frm_GetSN _frm_GetSN;

        bool _is_frm_GetSN_Closed;

        bool _isMonitor = true;

        Color _color_ON = Color.Lime;
        Color _color_OFF = Color.DimGray;

        /// <summary>
        /// sn
        /// </summary>
        string _SN;
        /// <summary>
        /// 刷新事件显示的定时器
        /// </summary>
        System.Threading.Timer _timer_refreshTime;
        /// <summary>
        /// 是否正在进行初始化
        /// </summary>
        bool _is_LabelRefreshIng = false;

        private static readonly object _lock_obj = new object();
        /// <summary>
        /// 电批数据接收的线程
        /// </summary>
        Thread _thread_ScrewDataReceive;
        /// <summary>
        /// 电批连接守护线程
        /// </summary>
        Thread _thread_ScrewDefender;
        /// <summary>
        /// 是否启用守护线程
        /// </summary>
        bool _isScrewDefender = false;

        bool _isScrewConnecting = false;
        /// <summary>
        /// 电批的socket连接
        /// </summary>
        Socket _socketSender_screw;
        /// <summary>
        /// ping 失败允许的最大次数
        /// </summary>
        int _screw_maxPingCount = 2;

        int _screw_PingCount = 0;


        #endregion


        #region 变量定义
        /// <summary>
        /// 用于判断电批状态是否发生了改变
        /// </summary>
        ScrewRunState _screwRunState;
        /// <summary>
        /// 螺丝加工工作是否完成，配合DB43.300.7
        /// </summary>
        bool luosiIsOver = true;
        /// <summary>
        /// 电批开始工作，true，//4111/4104按下，开始读取电批数据
        /// </summary>
        private static bool isRun = false;

        /// <summary>
        /// 功能按钮
        /// </summary>
        string btnAction = "";

        /// <summary>
        /// 工位产品SN
        /// </summary>
        Label[] labelSN = new Label[2];
        /// <summary>
        /// 当前工件运行时间
        /// </summary>
        int[] currRunTimes = new int[2];

        /// <summary>
        /// 工位状态
        /// </summary>
        Label[] labelStatus = new Label[2];
        /// <summary>
        /// 工位状态
        /// </summary>
        Label[] labelRunTimes = new Label[2];
        /// <summary>
        /// 工位工作结果
        /// </summary>
        Label[] labelResult = new Label[2];
        /// <summary>
        /// 左右工位名称
        /// </summary>
        string[] stationName = new string[2] { "工位", "工位" };


        /// <summary>
        /// SN码扫描状态，T不能再次扫码
        /// </summary>

        /// <summary>
        /// 正在加工的数量
        /// </summary>
        int QtyMading = 0;
        int QtyOK = 0;
        int QtyNG = 0;
        /// <summary>
        /// 每个产品SN的已经加工的螺丝数量,
        /// OK/NG都+1
        /// </summary>
        int luosiNum_Worked = 0;
        /// <summary>
        /// 拧紧结果enum
        /// </summary>
        enum enumTighteningResults_020200010
        {
            无定义 = 0,
            OK拧紧合格 = 1,
            NG拧紧不合格 = 2,
        }

        /// <summary>
        /// 开始读电批信息数据，T=开始
        /// </summary>
        bool startReadScrewdriverInfo = false;

        /// <summary>
        /// 取得NGtitle
        /// 00= 无定义；
        ///01= 最终扭矩过大；
        ///02= 最终扭矩过小 ；
        ///03= 最终角度过大；
        ///04= 最终角度过小；
        ///n1= 第 n 步扭矩过大； 1 ＜＝ n ＜＝ 5
        ///n2= 第 n 步时间超限；1 ＜＝ n ＜＝ 5
        ///90= 总时间超限；
        /// </summary>
        /// <param name="ngcode"></param>
        /// <returns></returns>
        private static string getNGtitle(string ngcode)
        {
            string ngtitle = "";
            switch (ngcode)
            {
                case "00":
                    ///ngtitle = "无定义";
                    break;
                case "01":
                    ngtitle = "最终扭矩过大";
                    break;
                case "02":
                    ngtitle = "最终扭矩过小";
                    break;
                case "03":
                    ngtitle = "最终角度过大";
                    break;
                case "04":
                    ngtitle = "最终角度过小";
                    break;
                case "90":
                    ngtitle = "总时间超限";
                    break;
                default:
                    string ngcode_1 = ngcode.Substring(0, 1);
                    string ngcode_2 = ngcode.Substring(1, 1);
                    if (ngcode_2 == "1")
                        ngtitle = "第+" + ngcode_1 + "步扭矩过大";
                    else
                        ngtitle = "第+" + ngcode_1 + "时间超限";
                    break;
            }
            return ngtitle;
        }

        /// <summary>
        /// 电批运行状态
        /// </summary>
        struct runStatus0201001
        {
            public bool ready_0;
            public bool run_1;
            public bool ok_2;
            public bool ng_3;
        }
        /// <summary>
        /// 电批运行状态0201001
        /// </summary>
        runStatus0201001 runStatus = new runStatus0201001();
        /// <summary>
        /// 最终拧紧结果TighteningResults_0202
        /// </summary>
        struct TighteningResults_0202
        {
            /// <summary>
            /// 使用标志，T:可以使用，F:不可以使用/使用完毕
            /// </summary>
            public bool UseSign;

            /// <summary>
            /// 拧紧结果代码00011，0= 无定义；	1=OK，拧紧合格；	2=NG，拧紧不合格；
            /// </summary>
            public string code;
            /// <summary>
            /// 拧紧结果内容
            /// </summary>
            public string title;
            /// <summary>
            /// 最终扭矩，00010_0
            /// </summary>
            public double Finaltorque1;
            /// <summary>
            /// 监控角度,00010_1
            /// </summary>
            public double MonitorAngle2;
            /// <summary>
            /// 结束时间,00010_2
            /// </summary>
            public double FinalTime3;
            /// <summary>
            /// 结束角度,00010_3
            /// </summary>
            public double FinalAngle4;
            /// <summary>
            /// NG代码，00012
            /// </summary>
            public string NGCode;
            /// <summary>
            /// NG内容，00012
            /// </summary>
            public string NGTitle;
        }
        /// <summary>
        /// 拧紧结果数据
        /// </summary>
        private TighteningResults_0202 TighteningResults0202 = new TighteningResults_0202();
        /// <summary>
        /// 曲线数据
        /// </summary>
        private RealtimeCurveData CurveData = new RealtimeCurveData();
        /// <summary>
        /// 扭矩数组
        /// </summary>
        public List<double> CurvelistTorque = new List<double>();
        /// <summary>
        /// 角度数组
        /// </summary>
        public List<double> CurvelistAngle = new List<double>();
        struct RealtimeCurveData
        {
            /// <summary>
            /// 采样频率0101
            /// </summary>
            public string samplingFrequency;
            /// <summary>
            /// 对应的pset
            /// </summary>
            public string pset;
            /// <summary>
            /// 拧紧曲线是否结束
            /// </summary>
            public bool IsFinished;
            /// <summary>
            /// 是否是曲线起点
            /// </summary>
            public bool IsStartingPoint;

        }

        /// <summary>
        /// True时开始画曲线
        /// </summary>
        bool startDrawCurve = false;
        #endregion

        #region 控件初始化
        private void panelTop_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panelTop, 0, 0, 0, 2);
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            labelTime.Left = panelTop.Width - labelTime.Width;
        }
        #endregion
        private void FormMain_Load(object sender, EventArgs e)
        {
            LogHelper.WriteLog("系统启动");
            //labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");
            initWorkStation();
            string AddressIp = string.Empty;

            labelHostName.Text = utility.stationId;
            labelSystemName.Text = utility.SystemName;
            //initDatagridview();
            //chart1.Series[0].Points.AddXY(0, 0);
            //chart1.Series[1].Points.AddXY(0, 0);
            comboBoxLineMode.SelectedIndex = utility.dSV.workMode ? 1 : 0;
            _is_LabelRefreshIng = true;
            ThreadPool.QueueUserWorkItem(InitializeWhileFormOpen, null);

            SystemInit();
            //TcpConnect();
            //if (socketSender.Connected)     //联通成功，与电批建立连接
            //{
            //    socketSender.Send(DNKE_DKTCP.Cmd_Connect);
            //}
            //PlcConnect();

            //SystemInit();
            //转shown事件
        }

        private void InitializeWhileFormOpen(object obj)
        {
            lock (_lock_obj)//防止多次执行
            {
                TcpConnect(null);
                ScrewDefenderThreadStart();
                if (_socketSender_screw != null && _socketSender_screw.Connected)     //联通成功，与电批建立连接
                {
                    _socketSender_screw.Send(DNKE_DKTCP.Cmd_Connect);
                }
                // PlcConnect();



                _businessMain = CenterControl.BusinessMain.GetInstance();
                _businessMain.MessageOutput += BusinessMainMessageOutput;

                _businessMain.Need_SN_Request -= Need_SN_Request;
                _businessMain.Need_SN_Request += Need_SN_Request;
                _businessMain.Need_lastProcessName_Request -= Need_lastProcessName_Request;
                _businessMain.Need_lastProcessName_Request += Need_lastProcessName_Request;
                _businessMain.SaveInformationToMES_Result_Request -= SaveInformationToMES_Result_Request;
                _businessMain.SaveInformationToMES_Result_Request += SaveInformationToMES_Result_Request;
                _businessMain.Need_ClearScrewData -= Need_ClearScrewData;
                _businessMain.Need_ClearScrewData += Need_ClearScrewData;

                _businessMain.BusinessStartedEvent -= BusinessStarted;
                _businessMain.BusinessStartedEvent += BusinessStarted;
                _businessMain.BusinessStopedEvent -= BusinessStoped;
                _businessMain.BusinessStopedEvent += BusinessStoped;

                _businessMain.PLC_Connect.PlcConnected -= PLC_Connected;
                _businessMain.PLC_Connect.PlcConnected += PLC_Connected;
                _businessMain.PLC_Connect.PlcDisConnected -= PLC_DisConnected;
                _businessMain.PLC_Connect.PlcDisConnected += PLC_DisConnected;

                _businessMain.BusinessStart();
                _isMonitor = true;
                _is_LabelRefreshIng = false;
                ThreadPool.QueueUserWorkItem(ShowPLC_PointState, null);
            }


        }
        #region 连接及断开的事件
        private void BusinessStarted()
        {

        }

        private void BusinessStoped()
        {

        }

        private void PLC_Connected(PLC_Connect plc)
        {
            SetLabel_LED_Forecolor(this.lab_plcState, _color_ON);
        }

        private void PLC_DisConnected(PLC_Connect plc)
        {
            SetLabel_LED_Forecolor(this.lab_plcState, _color_OFF);
        }
        #endregion

        private void BusinessMainMessageOutput(string s)
        {
            FillInfoLog(s);
        }

        #region 业务处理事件
        private string Need_SN_Request()
        {
            SetLabelForecolor(lab_snWrite_apply, _color_ON);//设定申请显示
            this.Invoke(new Action(() =>
            {
                txt_plcSN.Text = "";
                txt_scannerSN.Text = "";
            }));//清理历史数据
            FillInfoLog("收到SN码写入请求，清空电批数据");
            Need_ClearScrewData();
            _is_frm_GetSN_Closed = false;
            //申请触发时，清空电批的数据
            if (_dt_screwDataTable != null)
            {
                _dt_screwDataTable.Rows.Clear();
            }
            _is_frm_GetSN_Closed = false;
            FillInfoLog("收到SN码写入请求，请输入SN码并确认");
            //....这里写获得SN号的代码
            if (_frm_GetSN == null)
            {
                _frm_GetSN = new Frm_GetSN();
                _frm_GetSN.SN_CodeGet += Frm_GetSN_SN_CodeGet;
                _frm_GetSN.FormClosingByUser += Frm_FormClosingByUser;

                DialogResult dr = _frm_GetSN.ShowDialog();
            }
            else //窗体已打开则中断程序
            {
                FillInfoLog("检测到SN扫码窗体已打开，本次申请无效...");
                Thread.ResetAbort();
            }

            //while (!_is_frm_GetSN_Closed)
            //{
            //    Thread.Sleep(500);
            //}
            FillInfoLog("SN码输入完成");
            SetLabelForecolor(lab_snWrite_apply, _color_OFF);
            return _SN;

        }
        /// <summary>
        /// 获得扫描到的SN
        /// </summary>
        /// <param name="s"></param>
        private void Frm_GetSN_SN_CodeGet(string s)
        {
            this._SN = s;
            txt_scannerSN.Text = s;
        }
        private void Frm_FormClosingByUser()
        {
            this._is_frm_GetSN_Closed = true;
            _frm_GetSN.SN_CodeGet -= Frm_GetSN_SN_CodeGet;
            _frm_GetSN.FormClosingByUser -= Frm_FormClosingByUser;
            _frm_GetSN = null;
        }

        /// 获取上一工序名称 传出的string为SN码
        private string Need_lastProcessName_Request(string SN)
        {
            SetLabelForecolor(lab_interlock_apply, _color_ON);
            SetLabelForecolor(lab_manufacturePermission_apply, _color_ON);
            SetLabelForecolor(lab_manufactureDeny_apply, _color_ON);
            string lastProcessName = "BYJ";
            FillInfoLog("收到上一工序校验及互锁请求，请输入上一工序号并确认");
            //....这里写获得上一工序的代码
            FillInfoLog("上一工序获取入完成");
            SetLabelForecolor(lab_interlock_apply, _color_OFF);
            SetLabelForecolor(lab_manufacturePermission_apply, _color_OFF);
            SetLabelForecolor(lab_manufactureDeny_apply, _color_OFF);
            return lastProcessName;
        }
        /// <summary>
        /// 保存加工结果是否成功,传出1的string为SN码,传出2的string为加工结果
        /// </summary>
        private bool SaveInformationToMES_Result_Request(string SN, string manufactureResult)
        {
            SetLabelForecolor(lab_manufactureResultRecept_apply, _color_ON);
            bool bool_SaveInformationToMES_Result_Request = false;
            FillInfoLog("收到加工完成，保存加工结果请求，请保存并确认");

            //....这里写保存数据的代码
            bool_SaveInformationToMES_Result_Request = true;
            FillInfoLog("保存加工信息完成");
            SetLabelForecolor(lab_manufactureResultRecept_apply, _color_OFF);
            return bool_SaveInformationToMES_Result_Request;
        }
        #endregion

        void initDatagridview()
        {
            //dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Visible = true;
            dataGridView1.Dock = DockStyle.Fill;
            int cols = dataGridView1.Columns.Count;
            int w = (dataGridView1.Parent.Width - 43) / (cols);
            for (int i = 0; i < cols; i++)
            {
                dataGridView1.Columns[i].Width = w;
            }

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Byte[] b = new byte[100];
            List<ScrewDriverData_ACK> ack = AnalysisScrewACK_Data(b);//解析数据
            ShowScrewData(ack);
            /*
            //停止才能重新登录
            if (!isRun && utility.LoginModeEngineer)
            {
                FormSystem f = new FormSystem();
                f.ShowDialog();
            }
            else
                utility.ShowMessage("请停止运行，并启用工程模式！");
                */
        }

        private void labelRefresh_Click(object sender, EventArgs e)
        {
            if (_is_LabelRefreshIng)
            {
                MessageBox.Show("初始化未完成，请不要重复点击", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if (DialogResult.OK == MessageBox.Show("初始化操作会断开电批，PLC的连接，并进行重连操作，该操作可能会导致正在执行的数据丢失！ \r\n 确定要对系统进行初始化吗？", "系统初始化", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly))
            {
                _is_LabelRefreshIng = true;

                ThreadPool.QueueUserWorkItem(this.DisposeConnectAndReConnect, null);

            }
            //var v = utility.ShowMessageResponse("确定要初始化系统吗？");
            //if (v != DialogResult.Yes)
            //{
            //    return;
            //}
            //SystemInit();



        }

        private void DisposeConnectAndReConnect(object obj)
        {
            FillInfoLog("系统即将开始初始化...");
            this.DisposeSource(null);
            FillInfoLog("重新建立连接...");
            InitializeWhileFormOpen(null);
        }

        void SystemInit()
        {
            //初始化软件各个状态恢复软件初始值同
            //需要在手动模式下显示系统初时置位DB28.DBX7.0 为 1 按钮松手为0 始化按钮
            //S7NetPlus.WriteDataBool(S7NetPlus.PLC_DSVLock_Bool, !utility.dSV.workMode);
            //isRun = false;
            //utility.struckScanProduct = new struckScanProductSN();
            //luosiIsOver = true;
            //timer1.Enabled = true;
            this._timer_refreshTime = new System.Threading.Timer(Timer_refreshTime);
            this._timer_refreshTime.Change(0, 1000);
        }
        private void FormMain_Activated(object sender, EventArgs e)
        {
            if (utility.LoginModeEngineer)
            {
                labelRunMode.Text = "工程模式";
            }
            else
            {
                labelRunMode.Text = "生产模式";
            }
            labelUserID.Text = utility.loginUserID;
            labelSystem.Visible = utility.LoginModeEngineer;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var v = utility.ShowMessageResponse("确定退出系统吗？");
            if (v == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                LogHelper.WriteLog("用户退出系统");
                this.Dispose();//可以阻止Application.Exit()这句代码执行后再次Form2_FormClosing（）方法，需要点两次关闭
                Application.Exit();
            }
        }

        int runtimes = 0;
        /// <summary>
        /// 显示时间，采集扭力数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");
            }));

            try
            {
                //comboBoxLineMode.SelectedIndex = utility.dSV.workMode ? 1 : 0;
                //if (utility.boolClearDataGridView)
                //{
                //    dataGridView1.Rows.Clear();
                //    for (int irow = 0; irow < utility.structCurrentWorkTask.NumberOfScrews; irow++)
                //    {
                //        int rowindex = this.dataGridView1.Rows.Add();
                //        dataGridView1.Rows[irow].Cells[0].Value = rowindex + 1;
                //    }
                //    utility.boolClearDataGridView = false;
                //}
                //if (runtimes > 3000000)
                //    runtimes = 0;

                //if (!utility.bool_Home)//提示请回原点
                //{
                //    utility.string_HomeTitleMsg = "请回原位...";
                //    if (!utility.bool_HomeFormOpened)
                //    {
                //        FormGoHome f = new FormGoHome();
                //        f.ShowDialog();
                //    }
                //    if (utility.bool_Homeing)
                //        utility.string_HomeTitleMsg = "正在回原位中...";
                //}
                //else
                //{
                //    utility.string_HomeTitleMsg = "回原位完成";
                //}


                if (utility.bool_Stop)
                {
                    if (stop())
                        utility.bool_Stop = false;
                }
                //按下复位，回原位按钮，允许按下启动
                if (S7NetPlus.inputDiagitStatus[1])
                {
                    utility.struckScanProduct = new struckScanProductSN();

                    dataGridView1.Rows.Clear();
                    timer3.Enabled = false;
                    S7NetPlus.WriteDataBool(8, 260, true, 0);//允许启动
                    setWorkStation();
                }

                int index19 = (int)S7NetPlus.inputDiagitList.工具准备好;

                //准备好
                if (S7NetPlus.inputDiagitStatus[index19])
                {
                    lab_screwState.LedColor = Color.Lime;
                }
                else
                {
                    lab_screwState.LedColor = Color.Gray;
                }


                runtimes++;
                //if (utility.bool_Heart)
                //{
                //    lab_plcState.LedColor = Color.Lime;
                //}
                //else
                //{
                //    lab_plcState.LedColor = Color.Gray;
                //}
                //扫码后，读取左右启动按键按下，延迟1秒，恢复Y26(560)[9/10]的
                //读取左启动按键，4111/4104 =1，delay 1秒
                //工具错误    bool    16
                //工具拧紧OK  bool    17
                //工具拧紧NG  bool    18
                //工具准备好   bool    19
                //工具运行    bool    20
                int index16 = (int)S7NetPlus.inputDiagitList.工具错误;
                //工具错误，报红
                if (S7NetPlus.inputDiagitStatus[index16])
                {
                    lab_screwState.LedColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                string msg = "timer1通讯故障，请检查，然后重新运行" + ex.Message;
                FillMakeLog(msg);
                utility.ShowMessage(msg);
            }
        }


        private void Timer_refreshTime(object state)
        {
            this.Invoke(new Action(() =>
            {
                labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");
            }));
        }


        /// <summary>
        /// 电批，slaveID=1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRTU_Tick(object sender, EventArgs e)
        {
            if (!isRun) return;  //未运行
            Application.DoEvents();

            if (S7NetPlus.boolworkIsOver)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    int rowindex = luosiNum_Worked - 1;
                    dataGridView1.Rows[rowindex].Cells[3].Value = "NG";
                    //更新表格最后一个螺丝为NG，
                    DateTime datetimeid = Convert.ToDateTime(this.dataGridView1.Rows[rowindex].Cells[8].Value);
                    Model.WorkTaskDetail wtd = new WorkTaskDetail();
                    wtd.taskID = utility.structCurrentWorkTask.taskID;
                    wtd.datetimeid = datetimeid;
                    //wtd.productSN = utility.struckProduceProduct.productSN;
                    //wtd.productSN_M = utility.struckProduceProduct.productSN_M;
                    wtd.Result = "NG";
                    wtd.OtherErr = "意外NG";
                    Controller.CONT_WorkTaskDetail.Update_DatetimeID(wtd);
                    timer3.Enabled = false;
                }
            }
            if (startDrawCurve)
            {
                //开始画曲线
                //已取完数据，可以开始画曲线
                startDrawCurve = false;
                FillMakeLog("开始画曲线");
                int curveCount = 0;
                if (CurvelistTorque.Count < CurvelistAngle.Count)
                    curveCount = CurvelistTorque.Count;
                else
                    curveCount = CurvelistAngle.Count;

                //chart1.Series[0].Points.SuspendUpdates();
                //chart1.Series[0].Points.Clear();
                //chart1.Series[1].Points.SuspendUpdates();
                //chart1.Series[1].Points.Clear();
                //chart1.ChartAreas[0].AxisX2.Maximum = CurvelistTorque.Max();
                //chart1.ChartAreas[0].AxisX2.Minimum = CurvelistTorque.Min();
                //for (int i = 0; i < curveCount; i++)
                //{
                //    string strTime = Convert.ToString(0.3 * i);
                //    chart1.Series[1].Points.AddXY(strTime, CurvelistTorque[i]);
                //    chart1.Series[0].Points.AddXY(strTime, CurvelistAngle[i]);
                //}
            }


        }


        /// <summary>
        /// 报警信息
        /// </summary>
        void checkMotionControlMachiningStatus()
        {
            try
            {
                for (int i = 0; i < S7NetPlus.alarmPoints.Count; i++)
                {
                    if (S7NetPlus.alarmPoints[i].realStatue)  //i点=true报警
                    {


                        string msg = S7NetPlus.alarmPoints[i].DisplayInfo;
                        //LogHelper.WriteLog(msg);
                        //FillMakeLog(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                FillInfoLog("checkMotionControlMachiningStatus," + ex.Message);
            }
        }
        /// <summary>
        /// PLC报警检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            /* 本次项目取消报警 ws 2023-7-14
            checkMotionControlMachiningStatus();
            */
        }
        /// <summary>
        /// 按钮动作的确认操作
        /// </summary>
        /// <param name="actions"></param>
        DialogResult ResponseButtonActionButton(string actions)
        {
            var v = utility.ShowMessageResponse("确定要执行‘" + actions + "’操作吗？");

            writeLog(LogType.UserAction, btnAction);
            if (v == DialogResult.No)
                utility.ShowMessage("用户取消了" + actions + "操作，继续当前任务！", 0);
            return v;
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="logtitle"></param>
        void writeLog(LogType logType, string logtitle)
        {
            Model.Log log = new Log();
            log.LogType = (int)logType;
            log.LogTitle = logtitle;
            log.UserID = utility.loginUserID;
            Controller.CONT_Log.Save(log);
        }

        /// <summary>
        /// 扫码唤醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelNewTask_Click(object sender, EventArgs e)
        {
            btnAction = ((Label)sender).Text;
            if (ResponseButtonActionButton(btnAction) == DialogResult.Yes)
            {
                utility.ShowMessage("执行新工单任务！确定后扫描新工单产品码！", 0);

                utility.structCurrentWorkTask = new structNewWorkTaskInfo();  //新工作单
            }
        }




        private void labelLogin_Click(object sender, EventArgs e)
        {
            //int dindex = dataGridView1.Rows.Add();
            //dataGridView1.Rows[dindex].Cells[0].Value = dindex;
            DialogResult dr = utility.ShowMessageResponse("确定要注销当前用户，重新登录吗？");
            if (dr == DialogResult.No)
            {
                return;
            }

            FormLogin loginForm = new FormLogin();
            loginForm.ShowDialog();
        }

        string pre_makeInfo = "";
        /// <summary>
        /// 生产日志
        /// </summary>
        /// <param name="info"></param>
        void FillMakeLog(string info)
        {
            //if (listBoxMakeLog.Items.Count > 500)
            //    listBoxMakeLog.Items.Clear();
            //if (pre_makeInfo != info)
            //{
            //    listBoxMakeLog.Items.Add(DateTime.Now.ToString("MM-dd HH:mm:ss,") + info);
            //    listBoxMakeLog.SelectedIndex = listBoxMakeLog.Items.Count - 1;
            //    listBoxMakeLog.TopIndex = listBoxMakeLog.Items.Count - 1;
            //    pre_makeInfo = info;
            //}
        }

        string pre_Info = "";
        void FillInfoLog(string info)
        {
            if (listBoxInfoLog.Items.Count > 500)
                listBoxInfoLog.Items.Clear();
            if (pre_Info != info)
            {
                listBoxInfoLog.Items.Add(DateTime.Now.ToString("MM-dd HH:mm:ss,") + info);
                listBoxInfoLog.SelectedIndex = listBoxInfoLog.Items.Count - 1;
                listBoxInfoLog.TopIndex = listBoxInfoLog.Items.Count - 1;
                pre_Info = info;
            }
        }


        Model.WorkTaskInfo workTaskInfo = new Model.WorkTaskInfo();
        private void label1StartTask_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(utility.structCurrentWorkTask.productCode))
            {
                var result = utility.ShowMessageResponse("确定结束前次任务，执行新生产任务吗？");
                if (result == DialogResult.No)
                {
                    utility.ShowMessage("用户取消了新任务操作");
                    return;
                }
            }
            utility.structCurrentWorkTask = new structNewWorkTaskInfo();
            //FormNewTaskOrder f = new FormNewTaskOrder();
            //if (f.ShowDialog() != DialogResult.Yes)
            //{
            //    utility.ShowMessage("用户取消了新任务的操作！", 0);
            //    return;
            //}
            //stop();
            string productCode = utility.structCurrentWorkTask.productCode;
            //保存新任务
            workTaskInfo = new Model.WorkTaskInfo();
            workTaskInfo.productCode = productCode;
            workTaskInfo.taskQty = utility.structCurrentWorkTask.Qty;
            workTaskInfo.TPID = utility.structCurrentWorkTask.tpid;
            workTaskInfo.Customer = utility.structCurrentWorkTask.Customer;
            workTaskInfo.NumberOfScrews = utility.structCurrentWorkTask.NumberOfScrews;
            workTaskInfo.taskID = utility.structCurrentWorkTask.taskID;
            Model.ResultJsonInfo jr = Controller.CONT_WorkTaskInfo.Save(workTaskInfo);
            if (!jr.Successed)
            {
                utility.ShowMessage("生成新生产任务失败！" + jr.Message, 0);
                return;
            }
            workTaskInfo.Id = jr.intValue;
            //labelProductID.Text = productCode;
            labelTaskOrderID.Text = workTaskInfo.taskID;
            //labelMachineModel.Text = utility.structCurrentWorkTask.machinemodel;
            //labelProjectPhase.Text = utility.structCurrentWorkTask.projectphase;
            //labelTaskQty.Text = workTaskInfo.taskQty.ToString();
            //labelNumbers.Text = utility.structCurrentWorkTask.NumberOfScrews.ToString();
            //labelProcessedQty.Text = "";
            //labelUnqualifiedQty.Text = "";
            //labelRTY.Text = "";
            QtyMading = 0;
            QtyNG = 0;
            QtyOK = 0;
            FillMakeLog("工单：" + jr.intValue);

            //}
            //utility.ShowMessage("请按“复位”按钮，确认设备已经回原位！");

            //label1ScanCode_Click(null, null);
        }

        /// <summary>
        /// 工位属性初始化
        /// </summary>
        void initWorkStation()
        {
            //labelSN[0] = labelProductSN1;
            //labelStatus[0] = labelStationStatus1;
            //labelRunTimes[0] = labelRunTimes1;
            //labelResult[0] = labelResult1;
        }
        /// <summary>
        /// 工位属性赋值
        /// </summary>
        void setWorkStation()
        {
            labelSN[0].Text = utility.struckProduceProduct.productSN;
            labelStatus[0].Text = "进行中...";
            labelSN[0].Text = "";
            labelStatus[0].Text = "待机";
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F4) && (e.Alt == true))  //屏蔽ALT+F4
            {
                e.Handled = true;
            }
            switch (e.KeyCode)
            {
                case Keys.F12:
                    if (e.Control && e.Alt)
                    {
                        Close();
                    }
                    break;
            }
        }


        /// <summary>
        /// 检查工件是否放置在指定工位
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        bool CheckStation(short stationId)
        {
            bool b = false;
            Model.ResultJsonInfo jr = new ResultJsonInfo();
            //1.1.4，1.1.5
            switch (stationId)
            {
                case 1:
                    jr = S7NetPlus.ReadOneBool(1, 1, 4);
                    break;
                case 2:
                    jr = S7NetPlus.ReadOneBool(1, 1, 5);
                    break;
            }
            ////检查工位是否放置工件

            if (jr.Successed) //要求响应操作
            {
                b = jr.booValue[0];
                if (!jr.booValue[0])
                {
                    utility.ShowMessage(stationName[utility.struckScanProduct.stationId - 1] + "没有检测到工件！");
                    b = false;
                }
                else
                {
                    FillInfoLog(stationName[utility.struckScanProduct.stationId - 1] + "检查完成");
                }
            }
            else
            {
                utility.ShowMessage(stationName[utility.struckScanProduct.stationId - 1] + "读取状态出错，" + jr.Message);
                b = false;
            }

            return b;
        }





        /// <summary>
        /// 扫码，扫码结束后，根据工位1/2，设置560[9/10]='1',重设置560，表示可以接受启动按钮(4111)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1ScanCode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(utility.structCurrentWorkTask.productCode))
            {
                utility.ShowMessage("请先扫描产品任务码！");
                return;
            }

            timer3.Enabled = false;
            FormSeletY f1 = new FormSeletY();
            if (f1.ShowDialog() == DialogResult.Cancel)
            {
                utility.ShowMessage("用户取消扫码和选择工位，操作取消！", 0);
                return;
            }

            //labelProductSN1.Text = utility.struckScanProduct.productSN;
            //timerRTU,OK/NG时=true
            //是否检查工位是否有工件
            int inputCheckIndex = (int)S7NetPlus.inputDiagitList.工装产品信号1;

            if (S7NetPlus.inputDiagitStatus[inputCheckIndex])
                CheckStation(utility.struckScanProduct.stationId);

            setWorkStation();
        }

        private void labelResetNumber_Click(object sender, EventArgs e)
        {
            //清除当前任务的已加工数据
            if (utility.ShowMessageResponse("确定重置删除当前任务的加工信息吗？") != DialogResult.Yes)
            {
                return;
            }
            WorkTaskDetail taskDetail = new WorkTaskDetail();
            taskDetail.taskID = utility.structCurrentWorkTask.taskID;
            if (Controller.CONT_WorkTaskDetail.Delete2(taskDetail) > 0)
            {

            }

            //labelProcessedQty.Text = "";
            //labelRTY.Text = "";
            //labelUnqualifiedQty.Text = "";
            utility.ShowMessage("当前任务的加工信息已经重置！");
        }


        /// <summary>
        /// 清除工位当前加工的数据
        /// </summary>
        void clearStationData()
        {
            //delete
            List<WorkTaskDetail> taskDetails = new List<WorkTaskDetail>();
            try
            {
                WorkTaskDetail taskDetail = new WorkTaskDetail();
                taskDetail.taskID = utility.structCurrentWorkTask.taskID;

                if (Controller.CONT_WorkTaskDetail.Delete2(taskDetail) > 0)
                {

                    dataGridView1.Rows.Clear();
                    utility.ShowMessage("数据清除完成");
                }
                else
                {
                    utility.ShowMessage("数据清除失败，请检查");
                }

            }
            catch (Exception ex)
            {
                utility.ShowMessage("数据清除失败," + ex.Message);
            }
        }

        private void labelClearData1_Click(object sender, EventArgs e)
        {
            try
            {
                //是否清除当前工位数据，OK或者NG都可以清除
                if (utility.struckScanProduct.stationId != 1 || dataGridView1.Rows.Count == 0)
                {
                    utility.ShowMessage("当前工位没有数据");
                    return;
                }
                if (utility.ShowMessageResponse("确定清除当前工位的加工数据吗？") != DialogResult.Yes)
                    return;
                //delete 
                clearStationData();
            }
            catch (Exception ex)
            {

            }
        }


        private void labelClearData2_Click(object sender, EventArgs e)
        {
            if (utility.struckScanProduct.stationId != 2 || dataGridView1.Rows.Count == 0)
            {
                utility.ShowMessage("当前工位没有数据");
                return;
            }
            if (utility.ShowMessageResponse("确定清除当前工位的加工数据吗？") != DialogResult.Yes)
                return;

            clearStationData();
        }

        bool stop()
        {
            timer3.Enabled = false;
            //上位机启动信号	W	Bool	DB8.DBX260.0
            Model.ResultJsonInfo jr = S7NetPlus.WriteDataBool(S7NetPlus.PC_StartSingle_Bool, false);
            if (!jr.Successed)
            {
                utility.ShowMessage(jr.Message);
                return false;
            }
            FillMakeLog("设备停止执行完毕");
            return true;
        }
        private void labelStop_Click(object sender, EventArgs e)
        {
            /*示教器发送停止，660_Y12
            */
            stop();
        }
        /// <summary>
        /// 运行时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                currRunTimes[utility.struckScanProduct.stationId - 1]++;
                string runtimes = string.Format("{0:N1}秒", currRunTimes[utility.struckScanProduct.stationId - 1] * 0.1);
                //labelRunTime.Text = runtimes;
                labelRunTimes[utility.struckScanProduct.stationId - 1].Text = runtimes;

            }
            catch (Exception ex)
            {
                FillMakeLog("timer3异常，" + ex.Message + ",工位：" + utility.struckScanProduct.stationId);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            FormReporter f = new FormReporter();
            f.ShowDialog();
        }

        private void label1StartTask_MouseUp(object sender, MouseEventArgs e)
        {
            Label l = (Label)sender;
            l.BackColor = Color.Transparent;
            if (l.Name == "labelRefresh")
            {
                //置位DB28.DBX7.0 为 1 按钮松手为0 始化按钮
                Model.ResultJsonInfo jr = S7NetPlus.WriteDataBool(S7NetPlus.PLC_Init_bool, false);
            }
        }
        private void label1StartTask_MouseDown(object sender, MouseEventArgs e)
        {
            Label l = (Label)sender;
            l.BackColor = Color.Lime;
            if (l.Name == "labelRefresh")
            {
                //置位DB28.DBX7.0 为 1 按钮松手为0 始化按钮
                Model.ResultJsonInfo jr = S7NetPlus.WriteDataBool(S7NetPlus.PLC_Init_bool, true);
            }
        }

        /// <summary>
        /// 电批连接
        /// </summary>
        void TcpConnect(object obj)
        {
            try
            {
                _isScrewConnecting = true;
                //Create socket
                _socketSender_screw = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(ConfigurationKeys.ScrewMachineIP1);
                IPEndPoint point = new IPEndPoint(ip, ConfigurationKeys.ScrewMachinePort1);
                //Get the IP address and port number of the remote server
                FillInfoLog("开始连接电批...");
                if (!LogUtility.Ping(ConfigurationKeys.ScrewMachineIP1))
                {
                    FillInfoLog("电批连接失败，请检查网络连接是否正常");
                    _isScrewConnecting = false;
                    return;
                }

                _socketSender_screw.Connect(point);
                SetLabel_LED_Forecolor(this.lab_screwState, _color_ON);
                FillInfoLog("电批连接成功");
                _socketSender_screw.Send(DNKE_DKTCP.Cmd_DisConnect);
                LogUtility.ErrorLog_custom("握手信号发送：" + BitConverter.ToString(DNKE_DKTCP.Cmd_Connect));
                //socketSender.Send(DNKE_DKTCP.Cmd_Connect);
                //Start a new thread and keep receiving messages sent by the server
                _thread_ScrewDataReceive = new Thread(ReciveMessages);
                _thread_ScrewDataReceive.Name = "_thread_ScrewDataReceive";
                _thread_ScrewDataReceive.IsBackground = true;
                _thread_ScrewDataReceive.Start();
                _isScrewConnecting = false;


            }
            catch (Exception ex)
            {
                lab_screwState.LedColor = Color.Gray;
                FillInfoLog("电批连接失败，" + ex.Message);
            }
            finally
            { _isScrewConnecting = false; }
        }

        private readonly ManualResetEvent TimeoutObject = new ManualResetEvent(false);
        private void ConnectCallBackMethod(IAsyncResult r)
        {
            TimeoutObject.Set();
        }

        #region 电批数据处理2
        void ReciveMessages2(Object sk)
        {
            //byte[] b=new byte[100];
            //List<ScrewDriverData_ACK> ack = AnalysisScrewACK_Data(b);//解析数据
            //ShowScrewData(ack);

            byte[] b2 = new byte[2048];

            int r1 = _socketSender_screw.Receive(b2);
            LogUtility.ErrorLog_custom("握手信号已接收：" + BitConverter.ToString(b2.Skip(0).Take(r1).ToArray()));
            //订阅运行状态
            LogUtility.ErrorLog_custom("订阅运行状态发送：" + BitConverter.ToString(DNKE_DKTCP.Cmd_RunningState));
            int s = _socketSender_screw.Send(DNKE_DKTCP.Cmd_RunningState);
            r1 = _socketSender_screw.Receive(b2);
            LogUtility.ErrorLog_custom("订阅运行状态已接收：" + BitConverter.ToString(b2.Skip(0).Take(r1).ToArray()));
            //订阅拧紧结果
            LogUtility.ErrorLog_custom("订阅拧紧结果发送：" + BitConverter.ToString(DNKE_DKTCP.Cmd_TighteningResults));
            r1 = _socketSender_screw.Send(DNKE_DKTCP.Cmd_TighteningResults);
            r1 = _socketSender_screw.Receive(b2);
            LogUtility.ErrorLog_custom("订阅拧紧结果已接收：" + BitConverter.ToString(b2.Skip(0).Take(r1).ToArray()));

            while (true)
            {
                string sflag = "";
                try
                {
                    // return;

                    byte[] buffer = new byte[2048];
                    int r = _socketSender_screw.Receive(buffer);
                    //DebugInfo("TCP接收:" + r);
                    if (r > 0)
                    {
                        byte[] b = new byte[r];
                        Array.Copy(buffer, b, r);
                        LogUtility.ErrorLog_custom(BitConverter.ToString(b));//记录报文
                        List<ScrewDriverData_ACK> ack = AnalysisScrewACK_Data(b);//解析数据
                        ShowScrewData(ack);
                    }
                }
                catch
                {

                }
            }

        }
        /// <summary>
        /// 解析电批数据，防止粘包
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private List<ScrewDriverData_ACK> AnalysisScrewACK_Data(byte[] array)
        {
            List<ScrewDriverData_ACK> ack = new List<ScrewDriverData_ACK>();
            //string s1 = "02-00-00-00-E3-54-30-32-30-32-30-30-30-31-30-3D-30-2E-36-30-36-2C-31-31-39-36-2E-33-33-36-2C-31-2E-36-33-38-2C-31-35-37-33-2E-33-34-32-3B-30-30-30-31-31-3D-31-3B-30-30-30-31-32-3D-30-30-3B-30-31-30-31-30-3D-30-2E-30-39-33-2C-2D-33-36-32-2E-36-38-32-2C-30-2E-32-37-36-3B-30-31-30-31-31-3D-31-3B-30-31-30-32-30-3D-30-2E-30-30-30-2C-30-2E-30-30-30-2C-30-2E-30-30-30-3B-30-31-30-32-31-3D-31-3B-30-31-30-33-30-3D-30-2E-31-34-30-2C-35-34-30-2E-38-37-32-2C-30-2E-36-38-31-3B-30-31-30-33-31-3D-31-3B-30-31-30-34-30-3D-30-2E-34-38-38-2C-31-33-30-39-2E-37-38-31-2C-30-2E-35-32-33-3B-30-31-30-34-31-3D-31-3B-30-31-30-35-30-3D-30-2E-36-30-36-2C-38-37-2E-36-36-33-2C-30-2E-31-32-36-3B-30-31-30-35-31-3D-31-3B-03";
            //string s1 = "02-00-00-00-E1-54-30-32-30-32-30-30-30-31-30-3D-30-2E-35-38-38-2C-31-33-35-31-2E-30-33-34-2C-31-2E-38-36-36-2C-31-33-35-33-2E-33-32-36-3B-30-30-30-31-31-3D-31-3B-30-30-30-31-32-3D-30-30-3B-30-31-30-31-30-3D-30-2E-30-39-33-2C-2D-33-36-32-2E-36-38-32-2C-30-2E-32-37-35-3B-30-31-30-31-31-3D-31-3B-30-31-30-32-30-3D-30-2E-30-30-30-2C-30-2E-30-30-30-2C-30-2E-30-30-30-3B-30-31-30-32-31-3D-31-3B-30-31-30-33-30-3D-30-2E-32-31-33-2C-39-30-30-2E-36-39-30-2C-30-2E-36-39-35-3B-30-31-30-33-31-3D-31-3B-30-31-30-34-30-3D-30-2E-31-30-39-2C-31-2E-37-31-39-2C-30-2E-30-32-35-3B-30-31-30-34-31-3D-31-3B-30-31-30-35-30-3D-30-2E-35-38-38-2C-38-31-35-2E-38-39-32-2C-30-2E-38-33-37-3B-30-31-30-35-31-3D-31-3B-03";
            // string s1 = "02-00-00-00-E2-54-30-32-30-32-30-30-30-31-30-3D-30-2E-31-34-39-2C-34-31-32-39-2E-33-30-37-2C-34-2E-30-30-32-2C-34-31-32-39-2E-33-30-37-3B-30-30-30-31-31-3D-32-3B-30-30-30-31-32-3D-35-32-3B-30-31-30-31-30-3D-30-2E-31-31-33-2C-2D-33-36-32-2E-36-38-32-2C-30-2E-32-37-35-3B-30-31-30-31-31-3D-31-3B-30-31-30-32-30-3D-30-2E-30-30-30-2C-30-2E-30-30-30-2C-30-2E-30-30-30-3B-30-31-30-32-31-3D-31-3B-30-31-30-33-30-3D-30-2E-32-31-35-2C-39-30-31-2E-32-36-33-2C-30-2E-36-39-35-3B-30-31-30-33-31-3D-31-3B-30-31-30-34-30-3D-30-2E-31-30-39-2C-32-2E-38-36-35-2C-30-2E-30-32-39-3B-30-31-30-34-31-3D-31-3B-30-31-30-35-30-3D-30-2E-31-34-39-2C-33-35-38-37-2E-38-36-32-2C-33-2E-30-30-30-3B-30-31-30-35-31-3D-36-3B-03";
            //string s1 = "02-00-00-00-E2-54-30-32-30-32-30-30-30-31-30-3D-30-2E-35-38-38-2C-31-34-33-38-2E-36-39-37-2C-31-2E-39-33-30-2C-31-34-34-30-2E-34-31-36-3B-30-30-30-31-31-3D-31-3B-30-30-30-31-32-3D-30-30-3B-30-31-30-31-30-3D-30-2E-30-39-30-2C-2D-33-36-32-2E-36-38-32-2C-30-2E-32-37-35-3B-30-31-30-31-31-3D-31-3B-30-31-30-32-30-3D-30-2E-30-30-30-2C-30-2E-30-30-30-2C-30-2E-30-30-30-3B-30-31-30-32-31-3D-31-3B-30-31-30-33-30-3D-30-2E-31-38-34-2C-39-30-30-2E-36-39-30-2C-30-2E-36-39-35-3B-30-31-30-33-31-3D-31-3B-30-31-30-34-30-3D-30-2E-31-30-39-2C-35-38-2E-34-34-32-2C-30-2E-31-31-31-3B-30-31-30-34-31-3D-31-3B-30-31-30-35-30-3D-30-2E-35-38-38-2C-38-34-36-2E-32-35-39-2C-30-2E-38-31-35-3B-30-31-30-35-31-3D-31-3B-03";



            try
            {
                string s1 = BitConverter.ToString(array);
                string[] s2 = s1.Split(new string[] { "03" }, StringSplitOptions.None);
                List<byte[]> byteArray = new List<byte[]>();
                //分解数据包
                for (int i = 0; i < s2.Length - 1; i++)
                {
                    string str = s2[i] += "03";
                    if (str[0] == '-')
                    {
                        str = str.Substring(1, str.Length - 1);
                    }
                    string[] split = str.Split('-');
                    byte[] by = new byte[split.Length];
                    for (int j = 0; j < split.Length; j++)
                    {
                        by[j] = (byte)Convert.ToInt32(split[j], 16);
                    }
                    byteArray.Add(by);
                }

                foreach (byte[] item in byteArray)
                {
                    ScrewDriverData_ACK a = new ScrewDriverData_ACK(item);
                    a.ScrewDriverData_Analysis();
                    ack.Add(a);

                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorLog(ex, "AnalysisScrewACK_Data");
            }

            return ack;
        }

        private void ShowScrewData(List<ScrewDriverData_ACK> ack)
        {
            foreach (var item in ack)
            {
                switch (item.MIDType)
                {
                    case MIDType.Connect:
                        break;
                    case MIDType.DisConnect:
                        break;
                    case MIDType.DownloadPestToScrew:
                        break;
                    case MIDType.PestSelect:
                        break;
                    case MIDType.ScrewRunState:
                        ScrewRunState state = (ScrewRunState)item.analysisData;
                        if (state == null)
                        {
                            break;
                        }
                        if ((_screwRunState == null) || (_screwRunState.runState != state.runState || _screwRunState.workResult != state.workResult || _screwRunState.sysErr != state.sysErr))//状态发生变化时输出
                        {
                            FillInfoLog(string.Format("电批状态：{0}", state.GetStatus()));
                            _screwRunState = state;
                        }
                        break;
                    case MIDType.ScrewWorkResult:
                        //准备展示数据
                        DataTable dt = GenerateScrewDataTabel((ScrewWorkResult)item.analysisData);
                        this.Invoke(new Action(() =>
                        {
                            lock (this)
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    this.dataGridView1.DataSource = dt.Copy();

                                    for (int i = 0; i < ((DataTable)this.dataGridView1.DataSource).Rows.Count; i++)
                                    {
                                        if (((DataTable)this.dataGridView1.DataSource).Rows[i]["扭力结果"].ToString() == "OK")
                                        {
                                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                                        }
                                    }
                                    initDatagridview();
                                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                                }

                            }

                        }));
                        break;
                    case MIDType.ScrewWorkCurve:
                        break;
                    case MIDType.ScrewPset:
                        break;
                    case MIDType.EngineEnable:
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 清空列表数据
        /// </summary>
        private bool Need_ClearScrewData()
        {
            SetLabelForecolor(lab_ScrewClearOK_apply, _color_ON);
            this.Invoke(new Action(() =>
            {
                if (this.dataGridView1.DataSource != null)
                {
                    lock (this.dataGridView1.DataSource)
                    {
                        ((DataTable)this.dataGridView1.DataSource).Rows.Clear();
                    }

                }
                if (this._dt_screwDataTable != null)
                {
                    lock (this._dt_screwDataTable)
                    {
                        _dt_screwDataTable.Rows.Clear();
                    }
                }

            }));
            SetLabelForecolor(lab_ScrewClearOK_apply, _color_OFF);
            return true;
        }
        /// <summary>
        /// 生成展示的datatable
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private System.Data.DataTable GenerateScrewDataTabel(ScrewWorkResult result)
        {
            //_dt_screwDataTable.Rows.Clear();
            //foreach (ScrewWorkResult_StageResult item in result.StageResultList)
            //{
            //    DataRow dr = _dt_screwDataTable.NewRow();
            //    dr["序号"] = item.stage;
            //    dr["角度"] = item.Angle;
            //    dr["扭力"] = (Math.Round(Convert.ToDouble(item.Torque) / 0.098, 3)).ToString();
            //    dr["扭力结果"] = item.result;
            //    dr["耗时(S)"] = item.Time;
            //    _dt_screwDataTable.Rows.Add(dr);
            //}

            try
            {
                DataRow dr2 = _dt_screwDataTable.NewRow();
                dr2["序号"] = _dt_screwDataTable.Rows.Count + 1;
                dr2["角度"] = result.workResult.MonitorAngle;
                string turbo = (Math.Round(Convert.ToDouble(result.workResult.Torque) / 0.098, 3)).ToString("0.000");
                dr2["扭力"] = turbo;
                dr2["扭力结果"] = result.workResultState;
                dr2["其他"] = result.ngCode;
                dr2["耗时(S)"] = result.workResult.Time;
                _dt_screwDataTable.Rows.Add(dr2);
            }
            catch (Exception ex)
            {
                LogUtility.ErrorLog(ex, "GenerateScrewDataTabel");
            }

            //Writelog(JsonConvert.SerializeObject(_dt_screwDataTable.Rows));

            return _dt_screwDataTable;
        }
        #endregion

        #region 电批数据接收处理
        /// <summary>
        /// 电批数据接收处理
        /// </summary>
        /// <param name="sk"></param>
        void ReciveMessages(Object sk)
        {
            byte[] b2 = new byte[2048];

            try
            {
                int r1 = _socketSender_screw.Receive(b2);
                LogUtility.ErrorLog_custom("握手信号已接收：" + BitConverter.ToString(b2.Skip(0).Take(r1).ToArray()));
                //订阅运行状态
                LogUtility.ErrorLog_custom("订阅运行状态发送：" + BitConverter.ToString(DNKE_DKTCP.Cmd_RunningState));
                int s = _socketSender_screw.Send(DNKE_DKTCP.Cmd_RunningState);
                r1 = _socketSender_screw.Receive(b2);
                LogUtility.ErrorLog_custom("订阅运行状态已接收：" + BitConverter.ToString(b2.Skip(0).Take(r1).ToArray()));
                //订阅拧紧结果
                LogUtility.ErrorLog_custom("订阅拧紧结果发送：" + BitConverter.ToString(DNKE_DKTCP.Cmd_TighteningResults));
                r1 = _socketSender_screw.Send(DNKE_DKTCP.Cmd_TighteningResults);
                r1 = _socketSender_screw.Receive(b2);
                LogUtility.ErrorLog_custom("订阅拧紧结果已接收：" + BitConverter.ToString(b2.Skip(0).Take(r1).ToArray()));
            }
            catch (Exception e)
            {
                FillInfoLog("电批数据异常，如果自动连接失败，请使用【初始化】功能重新连接");
                //TcpConnect();
            }
            while (true)
            {
                string sflag = "";
                try
                {
                    // return;

                    byte[] buffer = new byte[2048];
                    int r = _socketSender_screw.Receive(buffer);
                    //DebugInfo("TCP接收:" + r);
                    if (r > 0)
                    {
                        byte[] b = new byte[r];
                        Array.Copy(buffer, b, r);
                        LogUtility.ErrorLog_custom(BitConverter.ToString(b));
                        List<ScrewDriverData_ACK> ack = AnalysisScrewACK_Data(b);//解析数据
                        ShowScrewData(ack);
                        /*
                        if (b[0] == 2 && b[r - 1] == 3)
                        {
                            //02 00 00 00 00 54   30 32 30 31    30 30 31 3D    30 2C 30 2C 30 2C 30 3B       30 30 32 3D 31 2C 33 3B 03
                            //A0000T0201001=0,0,0,0;002=1,3;
                            //BitConverter.ToString(buffer, 0, r).Replace("-", "");
                            string tcpResults = DNKE_DKTCP.getAscBytetoStr(b);  //已去掉前6位A0000T 和 最后一位 3，0201001=0,0,0,0;002=1,3;
                            LogHelper.WriteLog(BitConverter.ToString(buffer, 0, r) + Environment.NewLine + tcpResults);
                            string RecMid = tcpResults.Substring(0, 4);
                            int midIndex = Convert.ToInt32(RecMid);   //enum索引
                            string midTitle = ((DNKE_DKTCP.DNKE_MidInfo)(midIndex)).ToString();   //
                            if (tcpResults.IndexOf("ACK") > -1)
                            {
                                DNKE_DKTCP.DNKE_Connected = true;
                                lbLed2.LedColor = Color.Lime;

                                FillInfoLog("电批" + midTitle + "成功:");
                            }
                            else
                            {
                                if (tcpResults.IndexOf("ERROR") > -1)
                                {
                                    DNKE_DKTCP.DNKE_Connected = false;
                                    lbLed2.LedColor = Color.Gray;
                                    string errorCode = tcpResults.Substring(tcpResults.Length - 7, 7);
                                    int errorIntcode = Convert.ToInt32(errorCode);
                                    string errorTitle = ((DNKE_DKTCP.DNKE_ComError)(errorIntcode)).ToString();
                                    utility.ShowMessage("电批错误：" + midTitle + "," + errorCode + "," + errorTitle + Environment.NewLine + tcpResults);
                                    //return;
                                }
                            }
                            Model.ResultJsonInfo jr = new ResultJsonInfo();
                            //0201001=0,0,0,0;002=1,3;
                            string result = tcpResults.Substring(4, tcpResults.Length - 4 - 1);   //去掉订阅符0201 和 最后一个分号;    (001=0,0,0,0;002=1,3)
                            if (result.IndexOf(';') > -1)
                            {
                                sflag = "1";
                                string[] list = result.Split(';'); //3B(;)做分隔符，001=0,0,0,0;002=1,3
                                double CylinderNumber = 0;
                                double TorisionValue = 0;
                                DateTime datetimeid = DateTime.Now;
                                switch (RecMid)
                                {
                                    case "0201":
                                        string msg0201001 = "";
                                        foreach (string str in list)
                                        {
                                            //每个索引的数据，3D(=)做分隔符
                                            sflag = "0201";
                                            string[] states0 = str.Split('=');  //001=0,0,0,0         002=1,3
                                            string stateCode = states0[0];  //0201返回值的code

                                            List<int> list0201 = states0[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();
                                            switch (stateCode)
                                            {
                                                case "001":  //0201-001 运行状态
                                                    if (states0.Length == 2)
                                                    {
                                                        if (list0201.Count != 4)
                                                        {
                                                            utility.ShowMessage("02010001运行状态返回长度错误！" + list[0]);
                                                            return;
                                                        }
                                                        runStatus.ready_0 = list0201[0] == 1 ? true : false;
                                                        runStatus.run_1 = list0201[1] == 1 ? true : false;
                                                        runStatus.ok_2 = list0201[2] == 1 ? true : false;
                                                        runStatus.ng_3 = list0201[3] == 1 ? true : false;
                                                        if (runStatus.ready_0)
                                                            labelStationStatus1.Text = "准备运行";
                                                        if (runStatus.run_1)
                                                            labelStationStatus1.Text = "正在运行";
                                                        if (runStatus.ok_2)
                                                            labelResult1.Text = "OK";
                                                        if (runStatus.ng_3)
                                                            labelResult1.Text = "NG";
                                                        FillInfoLog("系统状态信息:" + labelStationStatus1.Text + "," + labelResult1.Text);

                                                    }
                                                    break;
                                                case "002":
                                                    if (states0.Length == 2)
                                                    {
                                                        if (states0[1].IndexOf(",") > -1)
                                                        {
                                                            List<int> SysErrState = states0[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();
                                                            if (SysErrState.Count > 0)
                                                            {
                                                                if (SysErrState.IndexOf(1) > -1)
                                                                {

                                                                }
                                                                msg0201001 = ((DNKE_DKTCP.MID0201_002_SysErr)SysErrState[0]).ToString();
                                                                if (SysErrState[0] == 1) //电批故障
                                                                {
                                                                    if (msg0201001 == "")
                                                                        msg0201001 = ((DNKE_DKTCP.MID0201_002_SysErrID)SysErrState[1]).ToString();
                                                                    else
                                                                        msg0201001 = msg0201001 + "," + ((DNKE_DKTCP.MID0201_002_SysErrID)SysErrState[1]).ToString();
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            FillMakeLog(states0[1]);
                                                        }
                                                        FillInfoLog("系统状态信息:" + msg0201001);
                                                    }
                                                    break;
                                                case "003":

                                                    break;
                                            }


                                        }
                                        break;
                                    case "0202":
                                        sflag = "0202";
                                        foreach (string str in list)
                                        {

                                            string[] states0 = (str).Split('=');  //001=0,0,0,0         002=1,3
                                            string stateCode = states0[0];  //0201返回值的code

                                            List<string> list0202 = states0[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                            switch (stateCode)
                                            {
                                                case "00010":  //最终拧紧结果，用2C(,)分割
                                                    TighteningResults0202.Finaltorque1 = Convert.ToDouble(list0202[0]);
                                                    TighteningResults0202.MonitorAngle2 = Convert.ToDouble(list0202[1]);
                                                    TighteningResults0202.FinalTime3 = Convert.ToDouble(list0202[2]);
                                                    TighteningResults0202.FinalAngle4 = Convert.ToDouble(list0202[3]);
                                                    break;
                                                case "00011":  //最终拧紧状态
                                                    TighteningResults0202.code = states0[1];
                                                    TighteningResults0202.title = ((enumTighteningResults_020200010)(Convert.ToInt32(TighteningResults0202.code))).ToString();
                                                    //Thread.Sleep(50);
                                                    break;
                                                case "00012":  //NG代码
                                                    foreach (string v in list0202)
                                                    {
                                                        TighteningResults0202.NGTitle = "";
                                                        if (TighteningResults0202.NGTitle == "" || TighteningResults0202.NGTitle == null)
                                                        {
                                                            TighteningResults0202.NGCode = v;
                                                            TighteningResults0202.NGTitle = getNGtitle(v);
                                                        }
                                                        else
                                                        {
                                                            TighteningResults0202.NGCode = TighteningResults0202.NGCode + "," + v;
                                                            TighteningResults0202.NGTitle = TighteningResults0202.NGTitle + "," + getNGtitle(v);
                                                        }
                                                    }
                                                    switch (TighteningResults0202.code)
                                                    {
                                                        case "1":
                                                            CylinderNumber = TighteningResults0202.MonitorAngle2;
                                                            TorisionValue = TighteningResults0202.Finaltorque1 * 10.197;
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[1].Value = string.Format("{0:N2}", CylinderNumber);
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[2].Value = string.Format("{0:N2}", TorisionValue);
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[3].Value = "OK";
                                                            this.dataGridView1.Rows[luosiNum_Worked].DefaultCellStyle.BackColor = Color.LimeGreen;
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[7].Value = String.Format("{0:N2}秒", TighteningResults0202.FinalTime3);
                                                            datetimeid = DateTime.Now;
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[8].Value = datetimeid;
                                                            luosiNum_Worked++;
                                                            Model.WorkTaskDetail wtd = new Model.WorkTaskDetail();
                                                            wtd.taskID = utility.structCurrentWorkTask.taskID;
                                                            wtd.productCode = utility.structCurrentWorkTask.productCode;
                                                            wtd.productSN = utility.struckProduceProduct.productSN;


                                                            wtd.productSN_M = utility.struckProduceProduct.productSN_M;
                                                            wtd.xh = luosiNum_Worked;
                                                            wtd.CylinderNumber = CylinderNumber;
                                                            wtd.TorisionValue = TorisionValue;

                                                            wtd.workStationId = utility.struckProduceProduct.stationId;
                                                            wtd.mkd = datetimeid;
                                                            Controller.CONT_WorkTaskDetail.Save(wtd); //保存当前螺丝数据
                                                                                                      //上位机启动信号置0
                                                            jr = S7NetPlus.WriteDataBool(S7NetPlus.PC_StartSingle_Bool, false);
                                                            FillInfoLog("螺丝OK:" + luosiNum_Worked);

                                                            //if (utility.dSV.workMode)
                                                            //{
                                                            //    updateMes(true);
                                                            //}
                                                            if (S7NetPlus.boolworkIsOver)
                                                            {
                                                                luosiNum_Worked = utility.structCurrentWorkTask.NumberOfScrews;
                                                            }
                                                            //加工数量==总螺丝数量
                                                            if (luosiNum_Worked == utility.structCurrentWorkTask.NumberOfScrews)
                                                            {
                                                                luosiIsOver = true;

                                                                utility.struckProduceProduct.productSN = "";
                                                                isRun = false;
                                                                QtyMading++;
                                                                labelProcessedQty.Text = QtyMading.ToString();
                                                                QtyOK++;
                                                                labelRTY.Text = ((double)QtyOK / (double)utility.structCurrentWorkTask.Qty).ToString("#0.00");
                                                                luosiNum_Worked = 0;
                                                                isRun = false;
                                                                timer3.Enabled = false;
                                                                //label1ScanCode_Click(null, null);
                                                                utility.FormScanSNFormisClosed = false;//正常扫码关掉
                                                                //if (utility.dSV.workMode)
                                                                //{
                                                                //    updateMes(true);
                                                                //}
                                                            }

                                                            break;
                                                        case "2":
                                                            timer3.Enabled = false;
                                                            if (luosiNum_Worked > 0)
                                                                luosiNum_Worked--;

                                                            CylinderNumber = TighteningResults0202.MonitorAngle2;
                                                            TorisionValue = TighteningResults0202.Finaltorque1 * 10.197;
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[1].Value = string.Format("{0:N2}", CylinderNumber);
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[2].Value = string.Format("{0:N2}", TorisionValue);
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[3].Value = "NG";
                                                            this.dataGridView1.Rows[luosiNum_Worked].DefaultCellStyle.BackColor = Color.Red;
                                                            int FloatingHigh = 0;
                                                            int ScrewLoose = 0;
                                                            string otherErr = TighteningResults0202.NGTitle;
                                                            datetimeid = DateTime.Now;
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[6].Value = otherErr;
                                                            this.dataGridView1.Rows[luosiNum_Worked].Cells[8].Value = datetimeid;
                                                            Model.WorkTaskDetail wtd_ng = new Model.WorkTaskDetail();
                                                            wtd_ng.taskID = utility.structCurrentWorkTask.taskID;
                                                            wtd_ng.productCode = utility.structCurrentWorkTask.productCode;

                                                            wtd_ng.productSN = utility.struckProduceProduct.productSN;
                                                            wtd_ng.productSN_M = utility.struckProduceProduct.productSN_M;
                                                            wtd_ng.xh = luosiNum_Worked + 1;
                                                            wtd_ng.CylinderNumber = CylinderNumber;
                                                            wtd_ng.TorisionValue = TorisionValue;
                                                            wtd_ng.Result = "NG";
                                                            wtd_ng.FloatingHigh = FloatingHigh;
                                                            wtd_ng.ScrewLoose = ScrewLoose;
                                                            wtd_ng.OtherErr = otherErr;
                                                            wtd_ng.workStationId = utility.struckProduceProduct.stationId;
                                                            wtd_ng.mkd = datetimeid;
                                                            Controller.CONT_WorkTaskDetail.Save(wtd_ng); //保存当前螺丝数据
                                                            luosiIsOver = true;

                                                            isRun = false;
                                                            QtyNG++;
                                                            labelUnqualifiedQty.Text = QtyNG.ToString();
                                                            FillInfoLog("螺丝NG:" + luosiNum_Worked);
                                                            //上位机启动信号置0
                                                            jr = S7NetPlus.WriteDataBool(S7NetPlus.PC_StartSingle_Bool, false);
                                                            //if (utility.dSV.workMode)
                                                            //{
                                                            //    updateMes(false);
                                                            //}
                                                            utility.ShowMessage("NG!" + Environment.NewLine + otherErr, 3);
                                                            break;
                                                    }
                                                    break;
                                            }


                                        }
                                        break;
                                    case "0203":
                                        sflag = "0203";
                                        foreach (string str in list)
                                        {

                                            string[] states0 = (str).Split('=');  //001=0,0,0,0         002=1,3
                                            string stateCode = states0[0];  //0201返回值的code

                                            List<string> list0203 = states0[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                                            switch (stateCode)
                                            {
                                                case "0201":
                                                    CurveData.IsFinished = states0[1] == "1" ? true : false;
                                                    break;
                                                case "0202":
                                                    CurveData.IsStartingPoint = states0[1] == "1" ? true : false;
                                                    if (CurveData.IsStartingPoint)
                                                    {
                                                        CurvelistTorque.Clear();
                                                        CurvelistAngle.Clear();
                                                        startDrawCurve = false;//曲线数组初始化禁止画曲线
                                                    }
                                                    break;
                                                case "0301":
                                                    foreach (string v in list0203)
                                                    {
                                                        CurvelistTorque.Add(Convert.ToDouble(v) * 10.197);
                                                    }
                                                    break;
                                                case "0302":
                                                    foreach (string v in list0203)
                                                    {
                                                        CurvelistAngle.Add(Convert.ToDouble(v));
                                                        if (CurveData.IsFinished)
                                                        {
                                                            startDrawCurve = true;


                                                        }
                                                    }
                                                    break;
                                            }


                                        }
                                        break;
                                }
                            }
                            else
                            {
                                FillMakeLog(result);
                            }
                            //this.Invoke(new Action(() =>
                            //{
                            //    FillInfoLog(tcpResults);
                            //}));
                        }
                        */
                    }
                }
                catch (Exception ex)
                {
                    FillInfoLog("电批数据处理异常," + sflag + "," + ex.Message);
                }
            }
        }

        #endregion

        /// <summary>
        /// 上传mes
        /// </summary>
        /// <param name="isok">T：passed，F:Failed</param>
        /// <returns></returns>
        void updateMes(bool isok)
        {
            string msg = "";
            try
            {

                Model.UUT_RESULT uUT_RESULT = new Model.UUT_RESULT();
                uUT_RESULT.STATION_ID = utility.dSV.stationId;
                uUT_RESULT.UUT_SERIAL_NUMBER = utility.struckScanProduct.productSN;
                uUT_RESULT.USER_LOGIN_NAME = utility.dSV.SW_User;
                uUT_RESULT.START_DATE_TIME = DateTime.Now;

                uUT_RESULT.UUT_STATUS = isok ? "Passed" : "Failed";


                Controller.CONT_UUT_RESULT.InsertMesData(uUT_RESULT);

                workTaskInfo.uploadStatus = 1;
                Controller.CONT_WorkTaskInfo.Update(workTaskInfo);
                msg = "上传mes成功" + (isok ? "OK," : "NG,") + utility.struckScanProduct.productSN;

            }
            catch (Exception ex)
            {
                msg = "上传mes失败," + ex.Message + (isok ? "OK," : "NG,") + utility.struckScanProduct.productSN;
            }
            finally
            {
                string result = isok ? "Passed" : "Failed";
                LogHelper.WriteLog(utility.struckScanProduct.productSN + "过站完成,结果：" + result);
                writeLog(LogType.Error, msg);
                FillInfoLog(msg);
            }

        }



        private void FormMain_Shown(object sender, EventArgs e)
        {
            //utility.bool_Home_display = true;
            //if (utility.ShowMessage("系统已启动，请回'原位'") == DialogResult.OK)
            //{

            //}
            //if (!utility.LoginModeEngineer)
            //{

            label1StartTask_Click(null, null);
            //}
        }

        private void labelAlermQuery_Click(object sender, EventArgs e)
        {
            FormAlermQuery f = new FormAlermQuery();
            f.ShowDialog();
        }

        /// <summary>
        /// 连接PLC
        /// </summary>
        void PlcConnect()
        {
            try
            {
                //创建PLC对象
                S7NetPlus.S71200 = new Plc(CpuType.S71200, ConfigurationKeys.PLC_IP, ConfigurationKeys.PLC_Rack, ConfigurationKeys.PLC_Slot);

                //调用S7.NET中的方法连接PLC
                S7NetPlus.S71200.Open();

                //连接成功后使能操作按钮
                if (S7NetPlus.S71200.IsConnected)
                {
                    //Start a new thread and keep receiving messages sent by the server
                    Thread plc_Td = new Thread(PlcAlarmRe);
                    plc_Td.IsBackground = true;
                    plc_Td.Start();
                }
                else
                    FillInfoLog("PLC 连接不成功，请检查IP地址、机架、插槽等是否正确");
            }
            catch (Exception ex)
            {
                FillInfoLog("PLC 连接不成功，请检查IP地址、机架、插槽等是否正确" + ex.Message);
            }
        }

        //string plc_SNCode = "";
        //string plc_SNCode_M = "";
        /// <summary>
        /// 读取PLC状态
        /// </summary>
        /// <param name="sk"></param>
        void PlcAlarmRe(Object sk)
        {
            while (true) //读取PLC报警功能取消  2023-7-11 ws 改为false
            {
                try
                {
                    //报警消息，DB4，512，4bit
                    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
                    //foreach (S7NetPlus.PLC_DB_Address s in S7NetPlus.plc_DB_Alarm)
                    //{
                    //    jr = S7NetPlus.ReadMuliBools(s.db, s.address, 1);  //8读出所有的bit
                    //    if (jr.Successed)
                    //    {
                    //        //jr.booValue.Count(b => b == true) > 1;
                    //        for (int i = 0; i < jr.booValue.Length; i++)
                    //        {
                    //            int alarmindex = S7NetPlus.alarmPoints.FindIndex(a => a.DB == s.db && a.Address == s.address && a.Bits == i);
                    //            if (alarmindex > 0)
                    //                S7NetPlus.alarmPoints[alarmindex].realStatue = jr.booValue[i];
                    //        }
                    //        //FillInfoLog("读取报警消息，DB"+s.db.ToString()+"."+s.address.ToString());
                    //    }
                    //    else
                    //    {
                    //        FillInfoLog("读取报警消息出错，" + jr.Message);
                    //    }
                    //}

                    //读取DB4.DBX517.5   (拧紧不合格）true
                    //读取DB4.DBX517.6（拍照不合格）true
                    //读取DB26.DBX272.0(当前工位sn码）

                    /*
                    //双工位db12.dbw64原位
                    jr = S7NetPlus.ReadOneInt(S7NetPlus.HomeStaus_Bool);
                    labelHomeStaus.Text = jr.intValue == 1 ? "已回原位" : "请回原位";

                    S7NetPlus.boolWorkMode = false;
                    jr = S7NetPlus.ReadOneBool(S7NetPlus.PLC_WorkMode_Bool);
                    if (!jr.Successed)
                        FillInfoLog(jr.Message);
                    else
                        S7NetPlus.boolWorkMode = jr.booValue[0];
                    labelWorkMode.Text = S7NetPlus.boolWorkMode ? "自动" : "手动";
                    labelRefresh.Enabled = !S7NetPlus.boolWorkMode;
                    jr = S7NetPlus.ReadOneBool(S7NetPlus.ScanCode_Bool);

                    S7NetPlus.boolScanCode = jr.booValue[0];
                    labelStop.Text = S7NetPlus.boolScanCode ? "不扫码加工" : "扫码加工";

                    //弹扫码窗标志 DB43.300.2=1,弹窗
                    jr = S7NetPlus.ReadOneBool(S7NetPlus.OpenScanForm_Bool);
                    utility.FormScanSNFormOpened = jr.booValue[0];

                    //读取sn码位数错误 （DB4.DBX517.7)	BOOL
                    jr = S7NetPlus.ReadOneBool(S7NetPlus.PLC_SNCode_Lenght_Error_Bool);
                    S7NetPlus.boolSNCode_Lenght_Error = jr.booValue[0];

                    //300.7,取消/NG 信号
                    jr = S7NetPlus.ReadOneBool(S7NetPlus.CancelSingle_Bool);
                    if (!jr.Successed)
                    {
                        S7NetPlus.boolworkIsOver = true;
                        FillInfoLog(jr.Message);
                    }
                    else
                    {
                        S7NetPlus.boolworkIsOver = jr.booValue[0];
                    }
                    jr = S7NetPlus.ReadOneBool(S7NetPlus.PLC_AllowStartSingle_Bool);
                    if (!jr.Successed)
                    {
                        FillInfoLog(jr.Message);
                    }

                    S7NetPlus.boolAllowStartSingle = jr.booValue[0];

                    //sn位数错误
                    labelSNInspectionStatus.BackColor = S7NetPlus.boolSNCode_Lenght_Error ? Color.Red : SystemColors.ButtonFace;
                    if (S7NetPlus.boolAllowStartSingle)
                    {
                        ScrewingProcess();
                    }




                    // checkPatchsScrews();//判断是否是补螺丝状态
                    //jr = S7NetPlus.ReadOneBool(43, 256, 2);
                    //if (!jr.Successed)
                    //    FillInfoLog(String.Format("读取启动信号失败，DB{0},DBX{1}.{2}", 43, 256, 2));
                    //else
                    //{
                    //    S7NetPlus.boolStartSingle = jr.booValue[0];
                    //    labelStartSingle.BackColor = S7NetPlus.boolStartSingle ? Color.Lime : SystemColors.ButtonFace;
                    //}



                    ////输入消息
                    //jr = new ResultJsonInfo();
                    //jr = S7NetPlus.ReadMuliBools(S7NetPlus.inputPointsInfo[0], S7NetPlus.inputPointsInfo[1], S7NetPlus.inputPointsInfo[2]);
                    //if (jr.Successed)
                    //{
                    //    for (int i = 0; i < S7NetPlus.inputDiagitStatus.Length; i++)
                    //    {
                    //        S7NetPlus.inputDiagitStatus[i] = jr.booValue[i];
                    //    }
                    //}
                    //else
                    //{
                    //    FillInfoLog("读取报警消息出错，" + jr.Message);
                    //}

                    ////输出消息
                    //jr = new ResultJsonInfo();
                    //if (S7NetPlus.readOutPoint)
                    //{
                    //    jr = S7NetPlus.ReadMuliBools(S7NetPlus.outPointsInfo[0], S7NetPlus.outPointsInfo[1], S7NetPlus.outPointsInfo[2]);
                    //    if (jr.Successed)
                    //    {
                    //        for (int i = 0; i < S7NetPlus.outDiagit.Length; i++)
                    //        {
                    //            S7NetPlus.outDiagit[i] = jr.booValue[i];
                    //        }
                    //    }
                    //    else
                    //    {
                    //        FillInfoLog("读取报警消息出错，" + jr.Message);
                    //    }
                    //}


                    jr = new ResultJsonInfo();
                    //如果已经启动，DB2，4.2=1，则上位机启动信号置0 DB8，260.0=0
                    jr = S7NetPlus.ReadOneBool(2, 4, 2);
                    if (jr.Successed)
                    {
                        if (jr.booValue[0])
                            jr = S7NetPlus.WriteDataBool(S7NetPlus.PC_StartSingle_Bool, false);

                    }


                    ////回原位，DB2.DBX5.6
                    ////true,检查DB2DBX5.6 T回原点成功，F提示请回原点
                    //jr = new ResultJsonInfo();
                    //jr = S7NetPlus.ReadOneBool(2, 5, 6);
                    //if (jr.Successed)
                    //{
                    //    utility.bool_Home = jr.booValue[0];

                    //}
                    //else
                    //{
                    //    FillInfoLog("读取回原位标志出错，" + jr.Message);
                    //}
                    ////回原位中的状态，DB1.DBX3.2
                    ////true,回原点过程中
                    //jr = new ResultJsonInfo();
                    //jr = S7NetPlus.ReadOneBool(1, 3, 2);
                    //if (jr.Successed)
                    //{
                    //    utility.bool_Homeing = jr.booValue[0];

                    //}
                    //else
                    //{
                    //    FillInfoLog("读取回原位中出错，" + jr.Message);
                    //}
                    */
                    //读心跳M0.5,连续10s不变，认为断线
                    object obj_heart = S7NetPlus.S71200.Read("M0.5");
                    if (obj_heart == null)
                    {
                        utility.bool_Heart = false;
                    }
                    else
                    {
                        utility.bool_Heart = true;
                    }



                    UpLoadMes();
                }
                catch (Exception ex)
                {
                    FillInfoLog("连接访问PLC出错，" + ex.Message);
                }
            }
        }
        private R_TRIG r_TRIG = new R_TRIG();
        /// <summary>
        /// 产品作业结果上传互锁
        /// </summary>
        private void UpLoadMes()
        {
            //db43.dbw302打螺丝最终结果
            var jr = S7NetPlus.ReadOneInt(43, 302);
            //判断结果是否为1-OK或2-NG
            r_TRIG.CLK = jr.intValue != 0;//为1或2
            bool res = jr.intValue == 1;
            if (r_TRIG.Q)
            {
                S7NetPlus.WriteDataInt(43, 302, (short)0);
                LogHelper.WriteLog($"收到结果{jr.intValue}");
                if (utility.dSV.workMode)
                {

                    updateMes(res);

                    //上位机启动信号置0
                    S7NetPlus.WriteDataBool(S7NetPlus.PC_StartSingle_Bool, false);

                }

            }

        }


        /// <summary>
        /// 打螺丝工序
        /// 打螺丝工序	步骤	                    数据类型	R/W	    信号类型	备注
        /// 1	转盘信号到位（DB1.DBX3.3）        true 	 BOOL	R		
        /// 2	读取DB26.DBX272.0(当前工位sn码）           String	R		
        /// 3	读取允许启动信号（DB1.DBX9.5)     true	     BOOL	R		
        /// 4	开始读取丹尼克尔电批螺丝扭矩信息		            R		
        /// 5	整体工序合格信号DB31.DBX570.0 	    BOOL	    R		接收到任意信号表示工件完成
        ///  	整体工序不合格信号DB31.DBX570.1	BOOL	    R		
        /// </summary>
        void ScrewingProcess()
        {
            Model.ResultJsonInfo jr = new ResultJsonInfo();
            if (S7NetPlus.boolworkIsOver || luosiIsOver)
            {
                luosiIsOver = false;
                dataGridView1.Rows.Clear();
                for (int irow = 0; irow < utility.structCurrentWorkTask.NumberOfScrews; irow++)
                {
                    int rowindex = this.dataGridView1.Rows.Add();
                    dataGridView1.Rows[irow].Cells[0].Value = rowindex + 1;
                }
                luosiNum_Worked = 0;
                isRun = true;
                FillMakeLog("收到开始读电批数据的标志");
                currRunTimes[utility.struckScanProduct.stationId - 1] = 0;
                timer3.Enabled = true;
                timerRTU.Enabled = true;
                FillMakeLog("订阅电批消息");
                _socketSender_screw.Send(DNKE_DKTCP.Cmd_TighteningResults);
                Thread.Sleep(100);
                _socketSender_screw.Send(DNKE_DKTCP.Cmd_RunningState);
                Thread.Sleep(100);
                _socketSender_screw.Send(DNKE_DKTCP.Cmd_RealCurveData);
            }
        }
        /// <summary>
        /// 检查补螺丝
        /// DB29 44.1=T,补螺丝画面已打开，不检查DB31.DBX570.1,检查DB29 90.4
        /// 否则 检查DB31.DBX570.1
        /// </summary>
        void checkPatchsScrews()
        {
            /*
            Model.ResultJsonInfo jr = new ResultJsonInfo();

            jr = S7NetPlus.ReadOneBool(29, 44, 1);
            if (!jr.Successed)
                FillInfoLog(String.Format("读取打螺丝失败信号出错，DB{0},DBX{1}.{2}", 29, 44, 1));
            else
            {
                if (jr.booValue[0])
                {
                    //补螺丝时是否，再次出错
                    jr = S7NetPlus.ReadOneBool(29, 90, 4);
                    if (!jr.Successed)
                        FillInfoLog(String.Format("读取补螺丝失败信号出错，DB{0},DBX{1}.{2}", 29, 94, 4));
                    else
                    {
                        if (jr.booValue[0])
                        {

                        }
                    }
                }
                else
                {
                    //打螺丝失败信号 DB31.DBX570.1   Bool R   收到true信号后读取步骤2
                    //DB29.DBX0.0 String[40]  R SN码 储存到数据库中
                    //DB29.DBW42 INT R 错误行号
                    jr = S7NetPlus.ReadOneBool(31, 570, 1);
                    if (!jr.Successed)
                        FillInfoLog(String.Format("读取打螺丝失败信号出错，DB{0},DBX{1}.{2}", 31, 570, 1));
                    else
                    {
                        string sn_error = "";
                        jr = S7NetPlus.ReadOneString(29, 0, 0);
                        if (!jr.Successed)
                            FillInfoLog(String.Format("读取出错的sn码失败，DB{0},DBX{1}.{2},{3}", 29, 0, 0, jr.Message));
                        else
                        {
                            sn_error = jr.stringValue;
                            jr = S7NetPlus.ReadOneInt(29, 42, 0);
                            if (!jr.Successed)
                                FillInfoLog(String.Format("读取出错的sn码行号失败，DB{0},DBX{1}.{2},{3}", 29, 42, 0, jr.Message));
                            else
                            {
                                int LineID_Error = jr.intValue;
                                Model.PatchScrews patchScrews = new PatchScrews();
                                patchScrews.MKD = DateTime.Now;
                                patchScrews.UPD = DateTime.Now;
                                patchScrews.SN = sn_error;
                                patchScrews.LineID = LineID_Error;
                                patchScrews.Flag = 0;

                                Controller.CONT_PatchScrews.Save(patchScrews);
                                FillInfoLog(String.Format("出错的sn信息保存完成，SN码:{0},行号:{1}", sn_error, LineID_Error));
                            }
                        }
                    }
                }
            }
            */

        }








        private void timerScanSN_Tick(object sender, EventArgs e)
        {
            if (utility.dSV.workMode)
            {
                if (utility.FormScanSNFormOpened && !utility.FormScanSNFormisClosed)
                {
                    if (workTaskInfo.taskID != null)
                    {
                        timerScanSN.Enabled = false;
                        FormSeletY formSeletY = new FormSeletY();
                        formSeletY.ShowDialog();
                        timerScanSN.Enabled = true;
                    }
                }
            }
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void labelMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void labelTaskOrderID_TextChanged(object sender, EventArgs e)
        {
            if (labelTaskOrderID.Text == "未建立")
            {
                labelTaskOrderID.BackColor = Color.Red;
            }
            else
            {
                labelTaskOrderID.BackColor = Color.White;
            }
        }

        private void lab_centerControl_Click(object sender, EventArgs e)
        {
            //Byte[] b = new byte[100];
            //List<ScrewDriverData_ACK> ack = AnalysisScrewACK_Data(b);//解析数据
            //ShowScrewData(ack);
            CenterControl.CenterDemo demo = new CenterControl.CenterDemo();
            demo.Show();
        }

        private void InitilizeScrewDataTable()
        {
            _dt_screwDataTable = new DataTable();
            _dt_screwDataTable.Columns.Add("序号");
            _dt_screwDataTable.Columns.Add("角度");
            _dt_screwDataTable.Columns.Add("扭力");
            _dt_screwDataTable.Columns.Add("扭力结果");
            _dt_screwDataTable.Columns.Add("其他");
            _dt_screwDataTable.Columns.Add("耗时(S)");
        }

        //刷新PLC点位值，并进行显示
        private void ShowPLC_PointState(object obj)
        {
            try
            {
                while (_isMonitor)
                {
                    //if (_businessMain.PLC_Connect.IsConnected)
                    //{
                    //    lab_PLC_ConnectState.ForeColor = _color_ON;
                    //}
                    //else
                    //{ lab_PLC_ConnectState.ForeColor = _color_OFF; }
                    foreach (KeyValuePair<string, PLC_Point> item in BusinessNeedPlcPoint.Dic_gatherPLC_Point)
                    {
                        this.Invoke((new Action(() =>
                        {
                            switch (item.Key)
                            {
                                case "SN码请求":
                                    if ((bool)item.Value.value)
                                    { lab_snRequest.ForeColor = _color_ON; }
                                    else
                                    { lab_snRequest.ForeColor = _color_OFF; }
                                    break;
                                case "开始加工请求":
                                    if ((bool)item.Value.value)
                                    { lab_isManufacture.ForeColor = _color_ON; }
                                    else
                                    { lab_isManufacture.ForeColor = _color_OFF; }
                                    break;
                                case "结果NG":
                                    if ((bool)item.Value.value)
                                    { lab_ng.ForeColor = _color_ON; }
                                    else
                                    { lab_ng.ForeColor = _color_OFF; }
                                    break;
                                case "结果OK":
                                    if ((bool)item.Value.value)
                                    { lab_ok.ForeColor = _color_ON; }
                                    else
                                    { lab_ok.ForeColor = _color_OFF; }
                                    break;
                                case "SN码":
                                    if (item.Value.value != null && !string.IsNullOrEmpty((string)item.Value.value))
                                    { lab_snWrite.ForeColor = _color_ON; txt_plcSN.Text = (string)item.Value.value; }
                                    else
                                    { lab_snWrite.ForeColor = _color_OFF; }
                                    break;
                                case "允许加工请求":
                                    if (item.Value.value != null && (bool)item.Value.value)
                                    { lab_manufacturePermission.ForeColor = _color_ON; }
                                    else
                                    { lab_manufacturePermission.ForeColor = _color_OFF; }
                                    break;
                                case "禁止加工请求":
                                    if (item.Value.value != null && (bool)item.Value.value)
                                    { lab_manufactureDeny.ForeColor = _color_ON; }
                                    else
                                    { lab_manufactureDeny.ForeColor = _color_OFF; }
                                    break;
                                case "互锁结果":
                                    if (item.Value.value != null && (bool)item.Value.value)
                                    { lab_interlock.ForeColor = _color_ON; }
                                    else
                                    { lab_interlock.ForeColor = _color_OFF; }
                                    break;
                                case "加工结果收到":
                                    if (item.Value.value != null && (bool)item.Value.value)
                                    { lab_manufactureResultRecept.ForeColor = _color_ON; }
                                    else
                                    { lab_manufactureResultRecept.ForeColor = _color_OFF; }
                                    break;
                                case "表格清空":
                                    if (item.Value.value != null && (bool)item.Value.value)
                                    { lab_screwClear_plc.ForeColor = _color_ON; }
                                    else
                                    { lab_screwClear_plc.ForeColor = _color_OFF; }
                                    break;
                                case "表格已清空":
                                    if (item.Value.value != null && (bool)item.Value.value)
                                    { lab_ScrewClearOK.ForeColor = _color_ON; }
                                    else
                                    { lab_ScrewClearOK.ForeColor = _color_OFF; }
                                    break;

                                default:
                                    break;
                            }
                        })));
                    }
                    Thread.Sleep(500);

                }

            }
            catch
            { }


        }

        private void SetLabelForecolor(Label l, Color c)
        {
            this.Invoke(new Action(() =>
            {
                l.ForeColor = c;
            }));
        }

        private void SetLabel_LED_Forecolor(LBSoft.IndustrialCtrls.Leds.LBLed l, Color c)
        {
            this.Invoke(new Action(() =>
            {
                l.LedColor = c;
            }));
        }

        /// <summary>
        /// 断开连接的资源
        /// </summary>
        private void DisposeSource(object obj)
        {
            _isMonitor = false;
            try
            {
                FillInfoLog("强制中断电批连接...");
                ScrewDefenderThreadStop();
                ScrewDisConnect();


            }
            catch (Exception e)
            {

            }

            try
            {
                FillInfoLog("强制中断PLC连接...");
                if (_businessMain != null)
                {
                    _businessMain.BusinessStop();
                    _businessMain.Dispose();
                }
            }
            catch (Exception)
            {

            }
            finally
            { _businessMain = null; FillInfoLog("PLC连接已断开"); }
            Thread.Sleep(1000);
        }

        private void listBoxInfoLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                this.listBoxInfoLog.ItemHeight = 16;
                e.DrawBackground();
                e.DrawFocusRectangle();
                StringFormat strFmt = new System.Drawing.StringFormat();
                strFmt.Alignment = StringAlignment.Near; //文本垂直居中
                strFmt.LineAlignment = StringAlignment.Center; //文本水平居中
                if (e.Index == -1)
                {
                    return;
                }
                e.Graphics.DrawString(listBoxInfoLog.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, strFmt);
            }
            catch (Exception)
            {

            }

        }

        private void ScrewDefenderThreadStart()
        {
            if (_thread_ScrewDefender != null)
            {
                return;
            }
            _thread_ScrewDefender = new Thread(ScrewDefenderProcess);
            _thread_ScrewDefender.Name = "_thread_ScrewDefender";
            _thread_ScrewDefender.IsBackground = true;
            _isScrewDefender = true;
            _screw_PingCount = 0;
            _thread_ScrewDefender.Start();
            FillInfoLog("电批守护线程已启动");
        }
        /// <summary>
        /// 停止守护线程
        /// </summary>
        private void ScrewDefenderThreadStop()
        {
            //停止守护线程
            _isScrewDefender = false;
            try
            {
                Thread.Sleep(200);
                if (_thread_ScrewDefender != null)
                {
                    _thread_ScrewDefender.Abort();
                }
            }
            catch (Exception)
            {

            }
            finally
            { _thread_ScrewDefender = null; FillInfoLog("电批守护线程已停止"); }
        }
        private void ScrewDefenderProcess()
        {
            try
            {

                while (_isScrewDefender)
                {
                    if (_isScrewConnecting)
                    {
                        continue;
                    }
                    if (!LogUtility.Ping(ConfigurationKeys.ScrewMachineIP1) || _socketSender_screw == null|| !_socketSender_screw.Connected)
                    {
                        _screw_PingCount++;
                        if (_screw_PingCount >= _screw_maxPingCount)
                        {
                            FillInfoLog("电批Ping失败已达最大次数，准备重新进行电批连接..");
                            this.ScrewDisConnect();
                            ThreadPool.QueueUserWorkItem(TcpConnect, null);
                            _screw_PingCount = 0;
                        }
                    }
                    Thread.Sleep(500);
                }
            }
            catch (Exception e)
            {
                LogUtility.ErrorLog(e, "ScrewDefenderProcess");
            }
        }

        private void ScrewDisConnect()
        {
            //中断接收线程
            try
            {
                if (_thread_ScrewDataReceive != null)
                {
                    _thread_ScrewDataReceive.Abort();
                }
            }
            catch (Exception)
            {

                ;
            }
            finally { _thread_ScrewDataReceive = null; }

            //中断连接
            try
            {
                if (_socketSender_screw != null)
                {
                    _socketSender_screw.Disconnect(false);
                    _socketSender_screw.Dispose();

                }
            }
            catch (Exception)
            {

            }
            finally
            {
                SetLabel_LED_Forecolor(this.lab_screwState, _color_OFF);
                _socketSender_screw = null; FillInfoLog("电批连接已断开");
            }


        }



    }
}
