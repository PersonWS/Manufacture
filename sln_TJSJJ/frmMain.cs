using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using System.IO;
using Snap7;
using sln_TJSJJ.Model;
using sln_TJSJJ.Controller;
namespace sln_TJSJJ
{
    public partial class frmMain : Form
    {
        frmMsg fromMsg = new frmMsg();
        frmStation fromStation = new frmStation();
        frmAlarm fromAlarm = new frmAlarm();
        frmDSVInterlockingConfig frmDlocking = new frmDSVInterlockingConfig();
        frmJg fromJg = new frmJg();
        private string[] barCodes;

        public frmMain()
        {
            InitializeComponent();
            //等待涂胶PLC发送命令
            Corecurrent.GetCorecurrent().TJ_NowProcedure = Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_IDLE;
            //等待升降机PLC发送命令
            Corecurrent.GetCorecurrent().SSJJ_NowProcedure = Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_IDLE;
            Corecurrent.GetCorecurrent().TJ_dtAlarm = new DataTable();
            Corecurrent.GetCorecurrent().TJ_dtAlarm.Columns.Add(new DataColumn("时间", typeof(string)));
            Corecurrent.GetCorecurrent().TJ_dtAlarm.Columns.Add(new DataColumn("报警类别", typeof(string)));
            Corecurrent.GetCorecurrent().TJ_dtAlarm.Columns.Add(new DataColumn("点位", typeof(string)));
            Corecurrent.GetCorecurrent().TJ_dtAlarm.Columns.Add(new DataColumn("报警信息", typeof(string)));
            //PLC初始化
            PLCTJ.plctj = new PLCTJ();
            PLCSJJ.plcsjj = new PLCSJJ();
            this.barCodes = new string[] { "\x0010CT~~CD,~CC^~CT~", "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR6,6~SD15^JUS^LRN^CI0^XZ", "^XA", "^MMT", "^PW240", "^LL0128", "^LS0", "^FT17,126^BQN,2,3", @"^FH\^FDLA,P01234567890000062^FS", @"^FT87,76^A0N,22,26^FH\^FDID:^FS", @"^FT87,117^A0N,21,21^FH\^FDS/N:^FS", @"^FT224,171^A0N,28,28^FH\^^FS", @"^FT130,116^A0N,19,24^FH\^FD0000623^FS", @"^FT17,50^A0N,20,21^FH\^FDModel^FS", "^PQ1,0,1,Y^XZ" };

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxLineMode.SelectedIndex = 0;
        }
        void FillInfoLog(string strkey, string info)
        {
            if (!Corecurrent.GetCorecurrent().mapInfo.ContainsKey(strkey))
            {
                if (listBoxInfoLog.Items.Count > 500)
                {
                    listBoxInfoLog.Items.Clear();
                }
                listBoxInfoLog.Items.Add(DateTime.Now.ToString("yy-MM-dd HH:mm:ss,") + info);
                listBoxInfoLog.SelectedIndex = listBoxInfoLog.Items.Count - 1;
                listBoxInfoLog.TopIndex = listBoxInfoLog.Items.Count - 1;
                Corecurrent.GetCorecurrent().mapInfo.Add(strkey, info);
            }
        }

        void FillMakeLog(string strkey, string make)
        {
            if (!Corecurrent.GetCorecurrent().mapMake.ContainsKey(strkey))
            {
                if (listBoxMakeLog.Items.Count > 500)
                {
                    listBoxMakeLog.Items.Clear();
                }
                listBoxMakeLog.Items.Add(DateTime.Now.ToString("yy-MM-dd HH:mm:ss,") + make);
                listBoxMakeLog.SelectedIndex = listBoxMakeLog.Items.Count - 1;
                listBoxMakeLog.TopIndex = listBoxMakeLog.Items.Count - 1;
                Corecurrent.GetCorecurrent().mapMake.Add(strkey, make);
            }
        }
        private void timUI_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (Corecurrent.GetCorecurrent().TJ_IsConnnectPlc == false)
            {
                FillInfoLog("BYPLCNOCONN", "涂胶PLC 连接不成功，请检查IP地址、机架、插槽等是否正确");
            }
            else
            {
                FillInfoLog("BYPLCYESCONN", "涂胶PLC 连接成功!");
            }

            if (Corecurrent.GetCorecurrent().SJJ_IsConnnectPlc == false)
            {
                FillInfoLog("SJJPLCNOCONN", "升降机PLC 连接不成功，请检查IP地址、机架、插槽等是否正确");
            }
            else
            {
                FillInfoLog("SJJPLCYESCONN", "升降机PLC 连接成功!");
            }
            //---------------------报警弹窗start--------------------------
            //if (Corecurrent.GetCorecurrent().TJ_IsAlarm == true)
            //{
            //    if (Corecurrent.GetCorecurrent().TJ_IsShowAlarm == false)
            //    {
            //        fromMsg.Show();
            //        Corecurrent.GetCorecurrent().TJ_IsShowAlarm = true;
            //    }
            //    else if (Corecurrent.GetCorecurrent().TJ_IsShowAlarm == true)
            //    {
            //        fromMsg.Show();
            //        if (Corecurrent.GetCorecurrent().TJ_dtAlarm.Rows.Count > 0)
            //        {
            //            if (Corecurrent.GetCorecurrent().TJ_bolTableRe == true)
            //            {
            //                fromMsg.SetDataSource();
            //                Corecurrent.GetCorecurrent().TJ_bolTableRe = false;
            //            }  
            //        }
            //    }
            //}
            //else if(Corecurrent.GetCorecurrent().TJ_IsAlarm == false)
            //{
            //    if (Corecurrent.GetCorecurrent().TJ_IsShowAlarm == true)
            //    {
            //        fromMsg.Hide();
            //        Corecurrent.GetCorecurrent().TJ_IsShowAlarm = false;
            //    }
            //    else
            //    {
            //        //fromMsg.SetDataSource();
            //    }
            //}
            //---------------------报警弹窗end----------------------------
            //---------------------工位参数start--------------------------
            ////实际压力值
            //labelTJSJYLZ.Text = Corecurrent.GetCorecurrent().TJ_strSJYLZ;
            ////伺服故障代码
            //labelTJSFGZDM.Text = Corecurrent.GetCorecurrent().TJ_strSFGZDM;
            ////伺服报警代码
            //labelTJSFBJDM.Text = Corecurrent.GetCorecurrent().TJ_strSFBJDM;
            ////伺服实际位置
            //labelTJSFSJWZ.Text = Corecurrent.GetCorecurrent().TJ_strSFSJWZ;
            ////伺服当前倍率
            //labelTJSFDQBL.Text = Corecurrent.GetCorecurrent().TJ_strSFDQBL;

            //工作模式
            if (utility.dSV.workMode == false)
            {
                comboBoxLineMode.SelectedIndex = 0;
            }
            else
            {
                comboBoxLineMode.SelectedIndex = 1;
            }
            //工位名称
            labelSTATIONID.Text = utility.dSV.stationId;
            //工号
            labelUSERID.Text = utility.loginUserID;
            //RFID信号强度
            if (Corecurrent.GetCorecurrent().TJ_strXHQD == "1")
            {
                labelTJXHQD.Text = "弱";
            }
            else if (Corecurrent.GetCorecurrent().TJ_strXHQD == "2")
            {
                labelTJXHQD.Text = "中等";
            }
            else if (Corecurrent.GetCorecurrent().TJ_strXHQD == "3")
            {
                labelTJXHQD.Text = "强";
            }
            else
            {
                labelTJXHQD.Text = "未知代码:" + Corecurrent.GetCorecurrent().TJ_strXHQD;
            }
            //RFID通讯状态
            if (Corecurrent.GetCorecurrent().TJ_strTXZT == "0")
            {
                labelTJTXZT.Text = "异常";
            }
            else if (Corecurrent.GetCorecurrent().TJ_strTXZT == "1")
            {
                labelTJTXZT.Text = "正常";
            }
            else
            {
                labelTJTXZT.Text = "未知代码:" + Corecurrent.GetCorecurrent().TJ_strTXZT;
            }

            //当前配方
            if (Corecurrent.GetCorecurrent().TJ_strDQPF == "1")
            {
                labelTJDQPF.Text = "C255";
            }
            else if (Corecurrent.GetCorecurrent().TJ_strDQPF == "2")
            {
                labelTJDQPF.Text = "C235";
            }
            else if (Corecurrent.GetCorecurrent().TJ_strDQPF == "3")
            {
                labelTJDQPF.Text = "C206仪表";
            }
            else if (Corecurrent.GetCorecurrent().TJ_strDQPF == "4")
            {
                labelTJDQPF.Text = "C206中控";
            }
            else if (Corecurrent.GetCorecurrent().TJ_strDQPF == "5")
            {
                labelTJDQPF.Text = "C255黑框";
            }
            else
            {
                labelTJDQPF.Text = "未知代码:" + Corecurrent.GetCorecurrent().TJ_strDQPF;
            }
            //---------------------工位参数end----------------------------
            //涂胶主SN码
            if (Corecurrent.GetCorecurrent().TJ_strHSN.Trim().Replace("\0", "").Length == 0)
            {
                lblTJHSN.Text = "-";
            }
            else
            {
                lblTJHSN.Text = Corecurrent.GetCorecurrent().TJ_strHSN;
            }

            //升降机扫描SN码
            if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Trim().Replace("\0", "").Length == 0)
            {
                lblSJJSCAN.Text = "-";
            }
            else
            {
                lblSJJSCAN.Text = Corecurrent.GetCorecurrent().SJJ_strSCANSN;
            }

            //升降机SN接收
            if (Corecurrent.GetCorecurrent().SJJ_strPLCSN.Trim().Replace("\0", "").Length == 0)
            {
                lblSJJSNJS.Text = "-";
            }
            else
            {
                lblSJJSNJS.Text = Corecurrent.GetCorecurrent().SJJ_strPLCSN;
            }

            //升降机SN发送
            //if (Corecurrent.GetCorecurrent().SJJ_strGKJSN.Trim().Replace("\0", "").Length == 0)
            //{
            //    lblSJJSNFS.Text = "-";
            //}
            //else
            //{
            //    lblSJJSNFS.Text = Corecurrent.GetCorecurrent().SJJ_strGKJSN;
            //}

            //涂胶点位状态
            if (Corecurrent.GetCorecurrent().TJ_bolLblSFJG == true)
            {
                lblTJSFJG.BackColor = Color.Green;
            }
            else
            {
                lblTJSFJG.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().TJ_bolLblJGOK == true)
            {
                lblTJJGOK.BackColor = Color.Green;
            }
            else
            {
                lblTJJGOK.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().TJ_bolLblJGNG == true)
            {
                lblTJJGNG.BackColor = Color.Green;
            }
            else
            {
                lblTJJGNG.BackColor = Color.White;
            }
            if (Corecurrent.GetCorecurrent().TJ_bolLblYXJG == true)
            {
                lblTJYXJG.BackColor = Color.Green;
            }
            else
            {
                lblTJYXJG.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().TJ_bolLblJZJG == true)
            {
                lblTJJZJG.BackColor = Color.Green;
            }
            else
            {
                lblTJJZJG.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().TJ_bolLblJSWC == true)
            {
                lblTJJSWC.BackColor = Color.Green;
            }
            else
            {
                lblTJJSWC.BackColor = Color.White;
            }

            //升降机点位状态
            if (Corecurrent.GetCorecurrent().SJJ_bolLblQQFS == true)
            {
                lblSJJQQFS.BackColor = Color.Green;
            }
            else
            {
                lblSJJQQFS.BackColor = Color.White;
            }

            //if (Corecurrent.GetCorecurrent().SJJ_bolLblPGXRWC == true)
            //{
            //    lblSJJPGXRWC.BackColor = Color.Green;
            //}
            //else
            //{
            //    lblSJJPGXRWC.BackColor = Color.White;
            //}

            if (Corecurrent.GetCorecurrent().SJJ_bolLblGPXRWC == true)
            {
                lblSJJGPXRWC.BackColor = Color.Green;
            }
            else
            {
                lblSJJGPXRWC.BackColor = Color.White;
            }

            #region 涂胶流程
            //涂胶流程
            if (Corecurrent.GetCorecurrent().TJ_NowProcedure == Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_IDLE)
            {
                FillMakeLog("TJ_WAITASKPROCESS", "等待涂胶PLC指令");
                lblSysMsg_TJ.Text = "等待涂胶PLC指令";
                lblMESSelResult.Text = "";
                lblMESSelResult.Visible = false;
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().TJ_bolHSXC = false;
                Corecurrent.GetCorecurrent().TJ_strHSQQJG = "";
                Corecurrent.GetCorecurrent().TJ_bolHSQQ = false;
                Corecurrent.GetCorecurrent().TJ_bolHSJG = false;
                Corecurrent.GetCorecurrent().TJ_bolShowJg = false;
            }
            if (Corecurrent.GetCorecurrent().TJ_NowProcedure == Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_PROCESSYN)
            {
                if (Corecurrent.GetCorecurrent().TJ_strHSN.Trim().Replace("\0", "").Length == 0)
                {
                    FillMakeLog("ZSNK", "后壳SN码不能为空");
                    lblSysMsg_TJ.Text = "后壳SN码不能为空";
                    lblMESSelResult.Text = "";
                    lblMESSelResult.Visible = false;
                    tabC.SelectedTab = tabp1;
                }
                else if (Corecurrent.GetCorecurrent().TJ_strHSN.Trim().Replace("\0", "").Length != 28 && Corecurrent.GetCorecurrent().TJ_strHSN.Trim().Replace("\0", "").Length != 19)
                {
                    //FillMakeLog("ZSNCD", "后壳SN码长度应该为28位");
                    //lblSysMsg_TJ.Text = "后壳SN码长度应该为28位";
                    //lblMESSelResult.Text = "";
                    //lblMESSelResult.Visible = false;
                    //tabC.SelectedTab = tabp1;
                    FillMakeLog("ZSNCD", "后壳SN码长度应该为28位或19位");
                    lblSysMsg_TJ.Text = "后壳SN码长度应该为28位或19位";
                    lblMESSelResult.Text = "";
                    lblMESSelResult.Visible = false;
                    tabC.SelectedTab = tabp1;
                }
                else
                {
                    if (utility.dSV.workMode == false)
                    {
                        //FillMakeLog("TJ_ASKMES", "询问互锁是否加工？");
                        FillInfoLog("TJ_DB200100TRUE", "接收到涂胶PLC DB2001 0.0是否加工请求");
                        //tabC.SelectedTab = tabp2;

                        Corecurrent.GetCorecurrent().TJ_bolReciveMES = true;
                        Corecurrent.GetCorecurrent().TJ_bolReciveMes_RY = true;
                        Corecurrent.GetCorecurrent().TJ_bolReciveMes_RN = false;
                        Corecurrent.GetCorecurrent().TJ_bolJGY = true;
                        Corecurrent.GetCorecurrent().TJ_bolJGN = false;
                        Corecurrent.GetCorecurrent().TJ_bolJGW = false;
                        Corecurrent.GetCorecurrent().TJ_IsWritePlc = true;
                        lblSysMsg_TJ.Text = "互锁反馈允许加工,已发送涂胶PLC允许加工,等待涂胶PLC断开是否加工...";
                        FillMakeLog("TJ_SENDPLCYXJG", "互锁反馈允许加工,已发送涂胶PLC允许加工,等待涂胶PLC断开是否加工...");
                        //lblMESSelResult.Visible = true;
                    }
                    else if (utility.dSV.workMode == true)
                    {
                        if (Corecurrent.GetCorecurrent().TJ_bolHSXC == false)
                        {
                            lblSysMsg_TJ.Text = "请求已发送互锁，等待互锁反馈...";
                        }
                        Corecurrent.GetCorecurrent().TJ_strHSQQJG = "QQ";
                        Corecurrent.GetCorecurrent().TJ_bolHSXC = true;
                    }
                }
            }
            if (Corecurrent.GetCorecurrent().TJ_NowProcedure == Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_ASKMES)
            {
                lblSysMsg_TJ.Text = "问询互锁是否可以加工,等待MES回答...";
                tabC.SelectedTab = tabp1;
            }
            if (Corecurrent.GetCorecurrent().TJ_NowProcedure == Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_JGWAIT)
            {
                FillMakeLog("TJ_JGWAITRESULT", "产品加工中,等待加工结果...");
                FillInfoLog("TJ_DB200100FALSE", "断开涂胶PLC DB2001 0.0是否加工请求");
                lblSysMsg_TJ.Text = "产品加工中,等待加工结果...";
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().TJ_bolWaitJGResult = true;
            }
            if (Corecurrent.GetCorecurrent().TJ_NowProcedure == Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_MESNG)
            {
                FillMakeLog("TJ_MESJGJZ", "互锁通知禁止加工,是否等待PLC通知结果？如何处理？");
                FillInfoLog("TJ_DB200100FALSE", "断开涂胶PLC DB2001 0.0是否加工请求");
                lblSysMsg_TJ.Text = "互锁通知禁止加工,是否等待PLC通知结果？如何处理？";
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().TJ_bolWaitJGResult = true;
            }
            if (Corecurrent.GetCorecurrent().TJ_NowProcedure == Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_SENDMES)
            {
                if (Corecurrent.GetCorecurrent().TJ_bolShowJg == false)
                {
                    Corecurrent.GetCorecurrent().TJ_dtTime = DateTime.Now;
                    Corecurrent.GetCorecurrent().TJ_bolShowJg = true;
                    if (Corecurrent.GetCorecurrent().TJ_bolResultOK == true
                     && Corecurrent.GetCorecurrent().TJ_bolResultNG == false)
                    {
                        fromJg.setJG("OK");
                    }
                    if (Corecurrent.GetCorecurrent().TJ_bolResultOK == false
                     && Corecurrent.GetCorecurrent().TJ_bolResultNG == true)
                    {
                        fromJg.setJG("NG");
                    }
                    fromJg.timUI.Enabled = true;
                    fromJg.Show();
                }
                if (utility.dSV.workMode == false)
                {
                    Corecurrent.GetCorecurrent().TJ_strHSQQJG = "";
                    Corecurrent.GetCorecurrent().TJ_bolHSXC = false;

                    if (Corecurrent.GetCorecurrent().TJ_bolResultOK == true
                    && Corecurrent.GetCorecurrent().TJ_bolResultNG == false)
                    {
                        FillMakeLog("TJ_JGOK", "结果OK");
                        FillInfoLog("TJ_DB200101TRUE", "接收到涂胶PLC DB2001 0.1加工结果OK");
                        lblTJResult.Text = "结果OK";
                    }
                    if (Corecurrent.GetCorecurrent().TJ_bolResultOK == false
                    && Corecurrent.GetCorecurrent().TJ_bolResultNG == true)
                    {
                        FillMakeLog("TJ_JGNG", "结果NG");
                        FillInfoLog("TJ_DB200102TRUE", "接收到涂胶PLC DB2001 0.2加工结果NG");
                        lblTJResult.Text = "结果NG";
                    }
                    Corecurrent.GetCorecurrent().TJ_bolJGY = false;
                    Corecurrent.GetCorecurrent().TJ_bolJGN = false;
                    Corecurrent.GetCorecurrent().TJ_bolJGW = true;
                    Corecurrent.GetCorecurrent().TJ_IsWritePlc = true;
                    FillMakeLog("TJ_SENDPLCMES", "SN码及加工结果已发送互锁,已发送涂胶PLC接收完成,等待涂胶PLC清除结果...");
                    FillInfoLog("TJ_DB200202TRUE", "发送涂胶PLC DB2002 0.2接收完成");
                    lblSysMsg_TJ.Text = "SN码及加工结果已发送互锁,已发送涂胶PLC接收完成,等待涂胶PLC清除结果...";
                    tabC.SelectedTab = tabp1;
                }
                else if (utility.dSV.workMode == true)
                {
                    Corecurrent.GetCorecurrent().TJ_strHSQQJG = "JG";
                    Corecurrent.GetCorecurrent().TJ_bolHSXC = true;
                }
            }
            if (Corecurrent.GetCorecurrent().TJ_NowProcedure == Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_END)
            {
                //需要写入PLC时候开关
                Corecurrent.GetCorecurrent().TJ_IsWritePlc = false;
                //是否从PLC接收请求开关
                Corecurrent.GetCorecurrent().TJ_bolRecivePLC = false;
                //是否从MES接收数据开关
                Corecurrent.GetCorecurrent().TJ_bolReciveMES = false;
                //从MES接收允许加工
                Corecurrent.GetCorecurrent().TJ_bolReciveMes_RY = false;
                //从MES接收禁止加工
                Corecurrent.GetCorecurrent().TJ_bolReciveMes_RN = false;
                //人工触发加工状态允许加工
                Corecurrent.GetCorecurrent().TJ_bolJGY = false;
                //人工触发加工状态禁止加工
                Corecurrent.GetCorecurrent().TJ_bolJGN = false;
                //给定加工完成信号
                Corecurrent.GetCorecurrent().TJ_bolJGW = false;
                //等待加工结果状态开关
                Corecurrent.GetCorecurrent().TJ_bolWaitJGResult = false;
                //加工结果已经出来
                Corecurrent.GetCorecurrent().TJ_bolGetJGResult = false;
                //加工结果OK
                Corecurrent.GetCorecurrent().TJ_bolResultOK = false;
                //加工结果NG
                Corecurrent.GetCorecurrent().TJ_bolResultNG = false;
                Corecurrent.GetCorecurrent().mapMake.Remove("TJ_WAITASKPROCESS");
                Corecurrent.GetCorecurrent().mapMake.Remove("TJ_ASKMES");
                Corecurrent.GetCorecurrent().mapMake.Remove("TJ_JGWAITRESULT");

                Corecurrent.GetCorecurrent().mapMake.Remove("TJ_MESJGJZ");
                Corecurrent.GetCorecurrent().mapMake.Remove("TJ_JGOK");
                Corecurrent.GetCorecurrent().mapMake.Remove("TJ_JGNG");
                Corecurrent.GetCorecurrent().mapMake.Remove("TJ_SENDPLCYXJG");

                Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200100TRUE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200100FALSE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200101TRUE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200102TRUE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200202TRUE");
                lblSysMsg_TJ.Text = "等待涂胶PLC指令";
                lblTJResult.Text = "-";
                lblMESSelResult.Text = "";
                lblMESSelResult.Visible = false;
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().TJ_NowProcedure = Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_IDLE;
            }
            #endregion

            #region 升降机流程
            //升降机流程
            if (Corecurrent.GetCorecurrent().SSJJ_NowProcedure == Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_IDLE)
            {
                FillMakeLog("SJJ_WAITASKPROCESS", "等待升降机PLC指令");
                lblSysMsg_SJJ.Text = "等待升降机PLC指令";
                Corecurrent.GetCorecurrent().SJJ_IsWriteFlag = "";
                Corecurrent.GetCorecurrent().SJJ_IsWritePlc = false;
            }
            if (Corecurrent.GetCorecurrent().SSJJ_NowProcedure == Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_GETQQ)
            {
                if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length == 0)
                {
                    lblSysMsg_SJJ.Text = "请扫码";
                }
                else if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length != 28)
                {
                    if (Corecurrent.GetCorecurrent().SJJ_bolSCANWC == false)
                    {
                        lblSysMsg_SJJ.Text = "扫码中....";
                    }
                    else
                    {
                        lblSysMsg_SJJ.Text = "扫码长度应该为28位";
                    }
                }
                else
                {
                    FillMakeLog("SJJ_GETQQ", "获得PLC请求发送,写入SN及完成,等待PLC断开请求发送");
                    FillInfoLog("SJJ_DB200100TRUE", "接收到升降机PLC DB2001 0.0请求发送");
                    lblSysMsg_SJJ.Text = "获得PLC请求发送,写入SN及完成,等待PLC断开请求发送";
                    Corecurrent.GetCorecurrent().SJJ_IsWriteFlag = "SNWC_WR";
                    Corecurrent.GetCorecurrent().SJJ_IsWritePlc = true;
                }
            }
            #endregion
        }
        private void timHSCZ_Tick(object sender, EventArgs e)
        {
            if (Corecurrent.GetCorecurrent().TJ_bolHSXC == true)
            {
                if (Corecurrent.GetCorecurrent().TJ_strHSQQJG == "QQ")
                {
                    if (Corecurrent.GetCorecurrent().TJ_bolHSQQ == false)
                    {
                        Corecurrent.GetCorecurrent().TJ_bolHSQQ = true;
                        long s = SV_Interlocking.sv_lockInfo(Corecurrent.GetCorecurrent().TJ_strHSN.Trim().ToUpper());
                        if (s == 0)
                        {
                            lblSysMsg_TJ.Text = "互锁反馈允许加工,已发送涂胶PLC允许加工,等待涂胶PLC断开是否加工...";
                            lblMESSelResult.Text = "";
                            lblMESSelResult.Visible = false;
                            tabC.SelectedTab = tabp1;

                            Corecurrent.GetCorecurrent().TJ_bolReciveMES = true;
                            Corecurrent.GetCorecurrent().TJ_bolReciveMes_RY = true;
                            Corecurrent.GetCorecurrent().TJ_bolReciveMes_RN = false;
                            Corecurrent.GetCorecurrent().TJ_bolJGY = true;
                            Corecurrent.GetCorecurrent().TJ_bolJGN = false;
                            Corecurrent.GetCorecurrent().TJ_bolJGW = false;
                            Corecurrent.GetCorecurrent().TJ_IsWritePlc = true;
                            FillMakeLog("TJ_SENDPLCYXJG", "互锁反馈允许加工,已发送涂胶PLC允许加工,等待涂胶PLC断开是否加工...");
                        }
                        else if (s == -1)
                        {
                            lblSysMsg_TJ.Text = "互锁反馈禁止加工,已发送PLC禁止加工,等待涂胶PLC断开是否加工...";
                            lblMESSelResult.Text = "";
                            lblMESSelResult.Visible = false;
                            tabC.SelectedTab = tabp1;

                            Corecurrent.GetCorecurrent().TJ_bolReciveMES = true;
                            Corecurrent.GetCorecurrent().TJ_bolReciveMes_RY = false;
                            Corecurrent.GetCorecurrent().TJ_bolReciveMes_RN = true;
                            Corecurrent.GetCorecurrent().TJ_bolJGY = false;
                            Corecurrent.GetCorecurrent().TJ_bolJGN = true;
                            Corecurrent.GetCorecurrent().TJ_bolJGW = false;
                            Corecurrent.GetCorecurrent().TJ_IsWritePlc = true;
                            FillMakeLog("TJ_SENDPLCJZJG", "互锁反馈禁止加工,已发送PLC禁止加工,等待涂胶PLC断开是否加工...");
                        }
                        else
                        {
                            MessageBox.Show("互锁验证失败！");
                        }
                    }
                }
                if (Corecurrent.GetCorecurrent().TJ_strHSQQJG == "JG")
                {
                    if (Corecurrent.GetCorecurrent().TJ_bolHSJG == false)
                    {
                        Corecurrent.GetCorecurrent().TJ_bolHSJG = true;
                        if (Corecurrent.GetCorecurrent().TJ_bolResultOK == true
                        && Corecurrent.GetCorecurrent().TJ_bolResultNG == false)
                        {
                            FillMakeLog("TJ_JGOK", "结果OK");
                            FillInfoLog("TJ_DB200101TRUE", "接收到涂胶PLC DB2001 0.1加工结果OK");
                            lblTJResult.Text = "结果OK";
                            updateHS(true);
                        }
                        if (Corecurrent.GetCorecurrent().TJ_bolResultOK == false
                        && Corecurrent.GetCorecurrent().TJ_bolResultNG == true)
                        {
                            FillMakeLog("TJ_JGNG", "结果NG");
                            FillInfoLog("TJ_DB200102TRUE", "接收到涂胶PLC DB2001 0.2加工结果NG");
                            lblTJResult.Text = "结果NG";
                            updateHS(false);
                        }
                        Corecurrent.GetCorecurrent().TJ_bolJGY = false;
                        Corecurrent.GetCorecurrent().TJ_bolJGN = false;
                        Corecurrent.GetCorecurrent().TJ_bolJGW = true;
                        Corecurrent.GetCorecurrent().TJ_IsWritePlc = true;
                        FillMakeLog("TJ_SENDPLCMES", "SN码及加工结果已发送互锁,已发送涂胶PLC接收完成,等待涂胶PLC清除结果...");
                        FillInfoLog("TJ_DB200202TRUE", "发送涂胶PLC DB2002 0.2接收完成");
                        lblSysMsg_TJ.Text = "SN码及加工结果已发送互锁,已发送涂胶PLC接收完成,等待涂胶PLC清除结果...";
                        tabC.SelectedTab = tabp1;
                    }
                }
            }
        }
        private void updateHS(bool isok)
        {
            string logtitle = "";
            try
            {
                UUT_RESULT uut_result = new UUT_RESULT
                {
                    STATION_ID = utility.dSV.stationId,
                    //BATCH_SERIAL_NUMBER = Corecurrent.GetCorecurrent().BY_strZSN.Trim().ToUpper(),
                    UUT_SERIAL_NUMBER = Corecurrent.GetCorecurrent().TJ_strHSN.Trim().ToUpper(),
                    USER_LOGIN_NAME = utility.dSV.SW_User,
                    START_DATE_TIME = DateTime.Now,
                    UUT_STATUS = isok ? "Passed" : "Failed"
                };
                CONT_UUT_RESULT.InsertMesData(uut_result);
                //logtitle = "上传mes成功" + (isok ? "OK,产品主SN码:" : "NG,产品主SN码:") + Corecurrent.GetCorecurrent().strZSN.Trim().ToUpper() + " 产品后壳SN码:" + Corecurrent.GetCorecurrent().strHSN.Trim().ToUpper();
                //MessageBox.Show(logtitle);
            }
            catch (Exception exception)
            {
                logtitle = "上传互锁失败," + exception.Message + (isok ? "OK,产品后壳SN码:" : "NG,产品后壳SN码:") + Corecurrent.GetCorecurrent().TJ_strHSN.Trim().ToUpper();
                MessageBox.Show(logtitle);
            }
            finally
            {
                string result = isok ? "Passed" : "Failed";
                LogHelper.WriteLog(Corecurrent.GetCorecurrent().TJ_strHSN.Trim().ToUpper() + "过站完成,结果：" + result);
                //this.writeLog(LogType.Error, logtitle);
                //this.FillInfoLog(logtitle);
            }
        }
        private void btnJGY_Click(object sender, EventArgs e)
        {
            //允许加工
            Corecurrent.GetCorecurrent().TJ_bolReciveMES = true;
            Corecurrent.GetCorecurrent().TJ_bolReciveMes_RY = true;
            Corecurrent.GetCorecurrent().TJ_bolReciveMes_RN = false;
            Corecurrent.GetCorecurrent().TJ_bolJGY = true;
            Corecurrent.GetCorecurrent().TJ_bolJGN = false;
            Corecurrent.GetCorecurrent().TJ_bolJGW = false;
            Corecurrent.GetCorecurrent().TJ_IsWritePlc = true;
            lblMESSelResult.Text = "互锁反馈允许加工,已发送涂胶PLC允许加工,等待涂胶PLC断开是否加工...";
            FillMakeLog("TJ_SENDPLCYXJG", "互锁反馈允许加工,已发送涂胶PLC允许加工,等待涂胶PLC断开是否加工...");
            lblMESSelResult.Visible = true;
        }

        private void btnJGN_Click(object sender, EventArgs e)
        {
            //禁止加工
            Corecurrent.GetCorecurrent().TJ_bolReciveMES = true;
            Corecurrent.GetCorecurrent().TJ_bolReciveMes_RY = false;
            Corecurrent.GetCorecurrent().TJ_bolReciveMes_RN = true;
            Corecurrent.GetCorecurrent().TJ_bolJGY = false;
            Corecurrent.GetCorecurrent().TJ_bolJGN = true;
            Corecurrent.GetCorecurrent().TJ_bolJGW = false;
            Corecurrent.GetCorecurrent().TJ_IsWritePlc = true;
            lblMESSelResult.Text = "互锁反馈禁止加工,已发送PLC禁止加工,等待涂胶PLC断开是否加工...";
            FillMakeLog("TJ_SENDPLCJZJG", "互锁反馈禁止加工,已发送PLC禁止加工,等待涂胶PLC断开是否加工...");
            lblMESSelResult.Visible = true;
        }

        /// <summary>
        /// 工位状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelStation_Click(object sender, EventArgs e)
        {
            fromStation.Show();
        }

        /// <summary>
        /// 报警点位状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelAlarm_Click(object sender, EventArgs e)
        {
            fromAlarm.timUI.Enabled = true;
            fromAlarm.Show();
        }



        /// <summary>
        /// 报警历史查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelAlermQuery_Click(object sender, EventArgs e)
        {
            bool bolTF = execFile("zd888t", "1234567890123456789012345678");
        }

        private void labelLockModel_Click(object sender, EventArgs e)
        {
            frmDlocking.Show();
        }


        private bool pingip(string ip = "10.217.12.78")
        {
            Ping ping = new Ping();
            return (ping.Send(ip, 120).Status == IPStatus.Success);
        }

        private bool checkPrinter(string ip, int port)
        {
            try
            {
                Connection connection = new TcpConnection(ip, port);
                connection.Open();
                ZebraPrinter instance = ZebraPrinterFactory.GetInstance(connection);
                ZebraPrinterLinkOs os = ZebraPrinterFactory.CreateLinkOsPrinter(instance);
                PrinterStatus status = (os != null) ? os.GetCurrentStatus() : instance.GetCurrentStatus();
                return status.isReadyToPrint;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool execFile(string printername, string codes)
        {
            try
            {
                int index = 0;
                string[] array = new string[15];
                this.barCodes.CopyTo(array, 0);
                array[8] = array[8].Replace("P01234567890000062", codes);
                array[12] = array[12].Replace("0000623", codes.Substring(12, 7));
                array[13] = array[13].Replace("Model", "DT" + codes.Substring(6, 4) + "/" + codes.Substring(10, 2));
                StreamWriter writer = new StreamWriter("2.prn", false, Encoding.UTF8);
                while (index < array.Length)
                {
                    writer.WriteLine(array[index]);
                    index++;
                }
                writer.Close();
                return ZebraPrintHelper.SendFileToPrinter(printername, "2.prn");
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '\b':
                    if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length > 0)
                    {
                        Corecurrent.GetCorecurrent().SJJ_strSCANSN = "";
                    }
                    break;

                case '\x001b':
                    Corecurrent.GetCorecurrent().SJJ_strSCANSN = null;
                    base.DialogResult = DialogResult.Cancel;
                    base.Close();
                    break;

                default:
                    if (Corecurrent.GetCorecurrent().SJJ_bolSCANWC == true)
                    {
                        Corecurrent.GetCorecurrent().SJJ_strSCANSN = "";
                        Corecurrent.GetCorecurrent().SJJ_bolSCANWC = false;
                    }
                    if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length < 0x1c)
                    {
                        if (e.KeyChar == 13)
                        {
                            Corecurrent.GetCorecurrent().SJJ_bolSCANWC = true;
                        }
                        else
                        {
                            Corecurrent.GetCorecurrent().SJJ_bolSCANWC = false;
                            Corecurrent.GetCorecurrent().SJJ_strSCANSN = Corecurrent.GetCorecurrent().SJJ_strSCANSN + e.KeyChar.ToString();
                        }

                    }
                    if ((Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length > 0) && !this.timUI.Enabled)
                    {
                        this.timUI.Enabled = true;
                    }
                    if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length == 0x1c)
                    {

                    }
                    break;
            }
        }


    }
}
