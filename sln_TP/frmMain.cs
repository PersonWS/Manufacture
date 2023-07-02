using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snap7;
using sv_Interlocking_Main;
using sln_TP.Model;
using sln_TP.Controller;
namespace sln_TP
{
    public partial class frmMain : Form
    {
        frmMsg fromMsg = new frmMsg();
        frmStation fromStation = new frmStation();
        frmAlarm fromAlarm = new frmAlarm();
        frmDSVInterlockingConfig frmDlocking = new frmDSVInterlockingConfig();
        frmJg fromJg = new frmJg();
        public frmMain()
        {
            InitializeComponent();
            //等待PLC发送命令
            Corecurrent.GetCorecurrent().NowProcedure = Corecurrent.SysRunStatus.Procedure.PROCEDURE_IDLE;
            Corecurrent.GetCorecurrent().dtAlarm = new DataTable();
            Corecurrent.GetCorecurrent().dtAlarm.Columns.Add(new DataColumn("时间", typeof(string)));
            Corecurrent.GetCorecurrent().dtAlarm.Columns.Add(new DataColumn("报警类别", typeof(string)));
            Corecurrent.GetCorecurrent().dtAlarm.Columns.Add(new DataColumn("点位", typeof(string)));
            Corecurrent.GetCorecurrent().dtAlarm.Columns.Add(new DataColumn("报警信息", typeof(string)));
            //PLC初始化
            Plc.plc = new Plc();
            //sv_Interlocking_Main.sv_Interlocking_Main_Class.SV_Interlocking_Main();
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
            if (Corecurrent.GetCorecurrent().IsConnnectPlc == false)
            {
                FillInfoLog("PLCNOCONN", "PLC 连接不成功，请检查IP地址、机架、插槽等是否正确");
            }
            else
            {
                FillInfoLog("PLCYESCONN", "PLC 连接成功!");
            }
            //---------------------报警弹窗start--------------------------
            //if(Corecurrent.GetCorecurrent().IsAlarm == true)
            //{
            //    if (Corecurrent.GetCorecurrent().IsShowAlarm == false)
            //    {
            //        fromMsg.Show();
            //        Corecurrent.GetCorecurrent().IsShowAlarm = true;
            //    }
            //    else if (Corecurrent.GetCorecurrent().IsShowAlarm == true)
            //    {
            //        fromMsg.Show();
            //        if (Corecurrent.GetCorecurrent().dtAlarm.Rows.Count > 0)
            //        {
            //            if(Corecurrent.GetCorecurrent().bolTableRe == true)
            //            {
            //                fromMsg.SetDataSource();
            //                Corecurrent.GetCorecurrent().bolTableRe = false;
            //            }
            //        }
            //    }
            //}
            //else if(Corecurrent.GetCorecurrent().IsAlarm == false)
            //{
            //    if (Corecurrent.GetCorecurrent().IsShowAlarm == true)
            //    {
            //        fromMsg.Hide();
            //        Corecurrent.GetCorecurrent().IsShowAlarm = false;
            //    }
            //    else
            //    {

            //    }
            //}
            //---------------------报警弹窗end----------------------------
            //---------------------工位参数start--------------------------
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
            //贴屏中转X偏移位置
            labelTPZZX.Text = Corecurrent.GetCorecurrent().strTPZZX;
            //贴屏中转Y偏移位置
            labelTPZZY.Text = Corecurrent.GetCorecurrent().strTPZZY;
            //贴屏中转R偏移位置
            labelTPZZR.Text = Corecurrent.GetCorecurrent().strTPZZR;
            //贴屏X偏移位置
            labelTPX.Text = Corecurrent.GetCorecurrent().strTPX;
            //贴屏Y偏移位置
            labelTPY.Text = Corecurrent.GetCorecurrent().strTPY;
            //贴屏R偏移位置
            labelTPR.Text = Corecurrent.GetCorecurrent().strTPR;
            //伺服实际位置
            labelSFWZ.Text = Corecurrent.GetCorecurrent().strSFWZ;
            //故障代码
            labelGZDM.Text = Corecurrent.GetCorecurrent().strGZDM;
            //报警代码
            labelBJDM.Text = Corecurrent.GetCorecurrent().strBJDM;
            //RFID信号强度
            if (Corecurrent.GetCorecurrent().strXHQD == "1")
            {
                labelXHQD.Text = "弱";
            }
            else if (Corecurrent.GetCorecurrent().strXHQD == "2")
            {
                labelXHQD.Text = "中等";
            }
            else if (Corecurrent.GetCorecurrent().strXHQD == "3")
            {
                labelXHQD.Text = "强";
            }
            else
            {
                labelXHQD.Text = "未知代码:" + Corecurrent.GetCorecurrent().strXHQD;
            }
            //RFID通讯状态
            if (Corecurrent.GetCorecurrent().strTXZT == "0")
            {
                labelTXZT.Text = "异常";
            }
            else if (Corecurrent.GetCorecurrent().strTXZT == "1")
            {
                labelTXZT.Text = "正常";
            }
            else
            {
                labelTXZT.Text = "未知代码:" + Corecurrent.GetCorecurrent().strTXZT;
            }

            //当前配方
            if (Corecurrent.GetCorecurrent().strDQPF == "1")
            {
                labelDQPF.Text = "C255";
            }
            else if (Corecurrent.GetCorecurrent().strDQPF == "2")
            {
                labelDQPF.Text = "C235";
            }
            else if (Corecurrent.GetCorecurrent().strDQPF == "3")
            {
                labelDQPF.Text = "C206仪表";
            }
            else if (Corecurrent.GetCorecurrent().strDQPF == "4")
            {
                labelDQPF.Text = "C206中控";
            }
            else if (Corecurrent.GetCorecurrent().strDQPF == "5")
            {
                labelDQPF.Text = "C255黑框";
            }
            else
            {
                labelDQPF.Text = "未知代码:" + Corecurrent.GetCorecurrent().strDQPF;
            }
            //---------------------工位参数end----------------------------
            //后壳SN码
            if (Corecurrent.GetCorecurrent().strHSN.Trim().Replace("\0", "").Length == 0)
            {
                lblHSN.Text = "-";
            }
            else
            {
                lblHSN.Text = Corecurrent.GetCorecurrent().strHSN.ToUpper();
            }
            //主SN码
            if (Corecurrent.GetCorecurrent().strZSN.Trim().Replace("\0", "").Length == 0)
            {
                lblZSN.Text = "-";
            }
            else
            {
                lblZSN.Text = Corecurrent.GetCorecurrent().strZSN.ToUpper();
            }
            if (Corecurrent.GetCorecurrent().bolLblSFJG == true)
            {
                lblSFJG.BackColor = Color.Green;
            }
            else
            {
                lblSFJG.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().bolLblJGOK == true)
            {
                lblJGOK.BackColor = Color.Green;
            }
            else
            {
                lblJGOK.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().bolLblJGNG == true)
            {
                lblJGNG.BackColor = Color.Green;
            }
            else
            {
                lblJGNG.BackColor = Color.White;
            }
            if (Corecurrent.GetCorecurrent().bolLblYXJG == true)
            {
                lblYXJG.BackColor = Color.Green;
            }
            else
            {
                lblYXJG.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().bolLblJZJG == true)
            {
                lblJZJG.BackColor = Color.Green;
            }
            else
            {
                lblJZJG.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().bolLblJSWC == true)
            {
                lblJSWC.BackColor = Color.Green;
            }
            else
            {
                lblJSWC.BackColor = Color.White;
            }
            if (Corecurrent.GetCorecurrent().NowProcedure == Corecurrent.SysRunStatus.Procedure.PROCEDURE_IDLE)
            {
                FillMakeLog("WAITASKPROCESS", "等待PLC指令");
                lblSysMsg.Text = "等待PLC指令";
                lblMESSelResult.Text = "";
                lblMESSelResult.Visible = false;
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().bolHSXC = false;
                Corecurrent.GetCorecurrent().strHSQQJG = "";
                Corecurrent.GetCorecurrent().bolHSQQ = false;
                Corecurrent.GetCorecurrent().bolHSJG = false;
                Corecurrent.GetCorecurrent().bolShowJg = false;
            }
            if (Corecurrent.GetCorecurrent().NowProcedure == Corecurrent.SysRunStatus.Procedure.PROCEDURE_PROCESSYN)
            {
                if (Corecurrent.GetCorecurrent().strZSN.Trim().Replace("\0", "").Length == 0)
                {
                    FillMakeLog("ZSNK", "主SN码不能为空");
                    lblSysMsg.Text = "主SN码不能为空";
                    lblMESSelResult.Text = "";
                    lblMESSelResult.Visible = false;
                    tabC.SelectedTab = tabp1;
                }
                else if (Corecurrent.GetCorecurrent().strZSN.Trim().Replace("\0", "").Length != 28 && Corecurrent.GetCorecurrent().strZSN.Trim().Replace("\0", "").Length != 19)
                {
                    //FillMakeLog("ZSNCD", "主SN码长度应该为28位");
                    //lblSysMsg.Text = "主SN码长度应该为28位";
                    //lblMESSelResult.Text = "";
                    //lblMESSelResult.Visible = false;
                    //tabC.SelectedTab = tabp1;
                    FillMakeLog("ZSNCD", "主SN码长度应该为28位或19位");
                    lblSysMsg.Text = "主SN码长度应该为28位或19位";
                    lblMESSelResult.Text = "";
                    lblMESSelResult.Visible = false;
                    tabC.SelectedTab = tabp1;
                }
                else
                {
                    if (utility.dSV.workMode == false)
                    {
                        //FillMakeLog("ASKMES", "询问互锁是否加工？");
                        FillInfoLog("DB200100TRUE", "接收到PLC DB2001 0.0是否加工请求");
                        //tabC.SelectedTab = tabp2;

                        Corecurrent.GetCorecurrent().bolReciveMes = true;
                        Corecurrent.GetCorecurrent().bolReciveMes_RY = true;
                        Corecurrent.GetCorecurrent().bolReciveMes_RN = false;
                        Corecurrent.GetCorecurrent().bolJGY = true;
                        Corecurrent.GetCorecurrent().bolJGN = false;
                        Corecurrent.GetCorecurrent().bolJGW = false;
                        Corecurrent.GetCorecurrent().IsWritePlc = true;
                        lblSysMsg.Text = "互锁反馈允许加工,已发送PLC允许加工,等待PLC断开是否加工...";
                        FillMakeLog("SENDPLCYXJG", "互锁反馈允许加工,已发送PLC允许加工,等待PLC断开是否加工...");
                        //lblMESSelResult.Visible = true;
                    }
                    else if (utility.dSV.workMode == true)
                    {
                        if (Corecurrent.GetCorecurrent().bolHSXC == false)
                        {
                            lblSysMsg.Text = "请求已发送互锁，等待互锁反馈...";
                        }
                        Corecurrent.GetCorecurrent().strHSQQJG = "QQ";
                        Corecurrent.GetCorecurrent().bolHSXC = true;
                    }
                }
            }
            if (Corecurrent.GetCorecurrent().NowProcedure == Corecurrent.SysRunStatus.Procedure.PROCEDURE_ASKMES)
            {
                lblSysMsg.Text = "问询互锁是否可以加工,等待互锁回答...";
                tabC.SelectedTab = tabp1;
            }
            if (Corecurrent.GetCorecurrent().NowProcedure == Corecurrent.SysRunStatus.Procedure.PROCEDURE_JGWAIT)
            {
                FillMakeLog("JGWAITRESULT", "产品加工中,等待加工结果...");
                FillInfoLog("DB200100FALSE", "断开PLC DB2001 0.0是否加工请求");
                lblSysMsg.Text = "产品加工中,等待加工结果...";
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().bolWaitJGResult = true;
            }
            if (Corecurrent.GetCorecurrent().NowProcedure == Corecurrent.SysRunStatus.Procedure.PROCEDURE_MESNG)
            {
                FillMakeLog("MESJGJZ", "互锁反馈禁止加工,是否等待PLC通知结果？如何处理？");
                FillInfoLog("DB200100FALSE", "断开PLC DB2001 0.0是否加工请求");
                lblSysMsg.Text = "互锁反馈禁止加工,是否等待PLC通知结果？如何处理？";
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().bolWaitJGResult = true;
            }
            if (Corecurrent.GetCorecurrent().NowProcedure == Corecurrent.SysRunStatus.Procedure.PROCEDURE_SENDMES)
            {
                if (Corecurrent.GetCorecurrent().bolShowJg == false)
                {
                    Corecurrent.GetCorecurrent().dtTime = DateTime.Now;
                    Corecurrent.GetCorecurrent().bolShowJg = true;
                    if (Corecurrent.GetCorecurrent().bolResultOK == true
                     && Corecurrent.GetCorecurrent().bolResultNG == false)
                    {
                        fromJg.setJG("OK");
                    }
                    if (Corecurrent.GetCorecurrent().bolResultOK == false
                     && Corecurrent.GetCorecurrent().bolResultNG == true)
                    {
                        fromJg.setJG("NG");
                    }
                    fromJg.timUI.Enabled = true;
                    fromJg.Show();
                }
                if (utility.dSV.workMode == false)
                {
                    Corecurrent.GetCorecurrent().strHSQQJG = "";
                    Corecurrent.GetCorecurrent().bolHSXC = false;

                    if (Corecurrent.GetCorecurrent().bolResultOK == true
                    && Corecurrent.GetCorecurrent().bolResultNG == false)
                    {
                        FillMakeLog("JGOK", "结果OK");
                        FillInfoLog("DB200101TRUE", "接收到PLC DB2001 0.1加工结果OK");
                        lblResult.Text = "结果OK";
                        //updateHS(true);
                    }
                    if (Corecurrent.GetCorecurrent().bolResultOK == false
                    && Corecurrent.GetCorecurrent().bolResultNG == true)
                    {
                        FillMakeLog("JGNG", "结果NG");
                        FillInfoLog("DB200102TRUE", "接收到PLC DB2001 0.2加工结果NG");
                        lblResult.Text = "结果NG";
                        //updateHS(false);
                    }

                    Corecurrent.GetCorecurrent().bolJGY = false;
                    Corecurrent.GetCorecurrent().bolJGN = false;
                    Corecurrent.GetCorecurrent().bolJGW = true;
                    Corecurrent.GetCorecurrent().IsWritePlc = true;
                    FillMakeLog("SENDPLCMES", "SN码及加工结果已发送互锁,已发送PLC接收完成,等待PLC清除结果...");
                    FillInfoLog("DB200202TRUE", "发送PLC DB2002 0.2接收完成");
                    lblSysMsg.Text = "SN码及加工结果已发送互锁,已发送PLC接收完成,等待PLC清除结果...";
                    tabC.SelectedTab = tabp1;
                }
                else if (utility.dSV.workMode == true)
                {
                    Corecurrent.GetCorecurrent().strHSQQJG = "JG";
                    Corecurrent.GetCorecurrent().bolHSXC = true;
                }
            }
            if (Corecurrent.GetCorecurrent().NowProcedure == Corecurrent.SysRunStatus.Procedure.PROCEDURE_END)
            {
                //需要写入PLC时候开关
                Corecurrent.GetCorecurrent().IsWritePlc = false;
                //是否从PLC接收请求开关
                Corecurrent.GetCorecurrent().bolRecivePLC = false;
                //是否从MES接收数据开关
                Corecurrent.GetCorecurrent().bolReciveMes = false;
                //从MES接收允许加工
                Corecurrent.GetCorecurrent().bolReciveMes_RY = false;
                //从MES接收禁止加工
                Corecurrent.GetCorecurrent().bolReciveMes_RN = false;
                //人工触发加工状态允许加工
                Corecurrent.GetCorecurrent().bolJGY = false;
                //人工触发加工状态禁止加工
                Corecurrent.GetCorecurrent().bolJGN = false;
                //给定加工完成信号
                Corecurrent.GetCorecurrent().bolJGW = false;
                //等待加工结果状态开关
                Corecurrent.GetCorecurrent().bolWaitJGResult = false;
                //加工结果已经出来
                Corecurrent.GetCorecurrent().bolGetJGResult = false;
                //加工结果OK
                Corecurrent.GetCorecurrent().bolResultOK = false;
                //加工结果NG
                Corecurrent.GetCorecurrent().bolResultNG = false;
                Corecurrent.GetCorecurrent().mapMake.Remove("WAITASKPROCESS");
                Corecurrent.GetCorecurrent().mapMake.Remove("ASKMES");
                Corecurrent.GetCorecurrent().mapMake.Remove("JGWAITRESULT");

                Corecurrent.GetCorecurrent().mapMake.Remove("MESJGJZ");
                Corecurrent.GetCorecurrent().mapMake.Remove("JGOK");
                Corecurrent.GetCorecurrent().mapMake.Remove("JGNG");
                Corecurrent.GetCorecurrent().mapMake.Remove("ZSNK");
                Corecurrent.GetCorecurrent().mapMake.Remove("ZSNCD");
                Corecurrent.GetCorecurrent().mapMake.Remove("SENDPLCYXJG");

                Corecurrent.GetCorecurrent().mapInfo.Remove("DB200100TRUE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("DB200100FALSE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("DB200101TRUE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("DB200102TRUE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("DB200202TRUE");
                lblSysMsg.Text = "等待PLC指令";
                lblResult.Text = "-";
                lblMESSelResult.Text = "";
                lblMESSelResult.Visible = false;
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().NowProcedure = Corecurrent.SysRunStatus.Procedure.PROCEDURE_IDLE;
            }
        }

        private void timHSCZ_Tick(object sender, EventArgs e)
        {
            if (Corecurrent.GetCorecurrent().bolHSXC == true)
            {
                if (Corecurrent.GetCorecurrent().strHSQQJG == "QQ")
                {
                    if (Corecurrent.GetCorecurrent().bolHSQQ == false)
                    {
                        Corecurrent.GetCorecurrent().bolHSQQ = true;
                        long s = SV_Interlocking.sv_lockInfo(Corecurrent.GetCorecurrent().strZSN.Trim().ToUpper());
                        LogHelper.WriteLog("res=" + s.ToString());
                        if (s == 0)
                        {
                            LogHelper.WriteLog("ok");
                            lblSysMsg.Text = "互锁反馈允许加工,已发送PLC允许加工,等待PLC断开是否加工...";
                            lblMESSelResult.Text = "";
                            lblMESSelResult.Visible = false;
                            tabC.SelectedTab = tabp1;

                            Corecurrent.GetCorecurrent().bolReciveMes = true;
                            Corecurrent.GetCorecurrent().bolReciveMes_RY = true;
                            Corecurrent.GetCorecurrent().bolReciveMes_RN = false;
                            Corecurrent.GetCorecurrent().bolJGY = true;
                            Corecurrent.GetCorecurrent().bolJGN = false;
                            Corecurrent.GetCorecurrent().bolJGW = false;
                            Corecurrent.GetCorecurrent().IsWritePlc = true;
                            FillMakeLog("SENDPLCYXJG", "互锁反馈允许加工,已发送PLC允许加工,等待PLC断开是否加工...");
                        }
                        else if (s == -1)
                        {
                            LogHelper.WriteLog("ng");
                            lblSysMsg.Text = "互锁反馈禁止加工,已发送PLC禁止加工,等待PLC断开是否加工...";
                            lblMESSelResult.Text = "";
                            lblMESSelResult.Visible = false;
                            tabC.SelectedTab = tabp1;

                            Corecurrent.GetCorecurrent().bolReciveMes = true;
                            Corecurrent.GetCorecurrent().bolReciveMes_RY = false;
                            Corecurrent.GetCorecurrent().bolReciveMes_RN = true;
                            Corecurrent.GetCorecurrent().bolJGY = false;
                            Corecurrent.GetCorecurrent().bolJGN = true;
                            Corecurrent.GetCorecurrent().bolJGW = false;
                            Corecurrent.GetCorecurrent().IsWritePlc = true;
                            FillMakeLog("SENDPLCJZJG", "互锁反馈禁止加工,已发送PLC禁止加工,等待PLC断开是否加工...");
                        }
                        else
                        {
                            MessageBox.Show("互锁验证失败！");
                        }
                    }
                }
                if (Corecurrent.GetCorecurrent().strHSQQJG == "JG")
                {
                    if (Corecurrent.GetCorecurrent().bolHSJG == false)
                    {
                        Corecurrent.GetCorecurrent().bolHSJG = true;
                        if (Corecurrent.GetCorecurrent().bolResultOK == true
                           && Corecurrent.GetCorecurrent().bolResultNG == false)
                        {
                            FillMakeLog("JGOK", "结果OK");
                            FillInfoLog("DB200101TRUE", "接收到PLC DB2001 0.1加工结果OK");
                            lblResult.Text = "结果OK";
                            updateHS(true);
                        }
                        if (Corecurrent.GetCorecurrent().bolResultOK == false
                        && Corecurrent.GetCorecurrent().bolResultNG == true)
                        {
                            FillMakeLog("JGNG", "结果NG");
                            FillInfoLog("DB200102TRUE", "接收到PLC DB2001 0.2加工结果NG");
                            lblResult.Text = "结果NG";
                            updateHS(false);
                        }

                        Corecurrent.GetCorecurrent().bolJGY = false;
                        Corecurrent.GetCorecurrent().bolJGN = false;
                        Corecurrent.GetCorecurrent().bolJGW = true;
                        Corecurrent.GetCorecurrent().IsWritePlc = true;
                        FillMakeLog("SENDPLCMES", "SN码及加工结果已发送互锁,已发送PLC接收完成,等待PLC清除结果...");
                        FillInfoLog("DB200202TRUE", "发送PLC DB2002 0.2接收完成");
                        lblSysMsg.Text = "SN码及加工结果已发送互锁,已发送PLC接收完成,等待PLC清除结果...";
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
                    BATCH_SERIAL_NUMBER = Corecurrent.GetCorecurrent().strHSN.Trim().ToUpper(),
                    UUT_SERIAL_NUMBER = Corecurrent.GetCorecurrent().strZSN.Trim().ToUpper(),
                    USER_LOGIN_NAME = utility.dSV.SW_User,
                    START_DATE_TIME = DateTime.Now,
                    UUT_STATUS = isok ? "Passed" : "Failed"
                };
                CONT_UUT_RESULT.InsertMesData(uut_result);
                //logtitle = "上传互锁成功" + (isok ? "OK,产品主SN码:" : "NG,产品主SN码:") + Corecurrent.GetCorecurrent().strZSN.Trim().ToUpper() + " 产品后壳SN码:" + Corecurrent.GetCorecurrent().strHSN.Trim().ToUpper();
                //MessageBox.Show(logtitle);
            }
            catch (Exception exception)
            {
                logtitle = "上传互锁失败," + exception.Message + (isok ? "OK,产品主SN码:" : "NG,产品主SN码:") + Corecurrent.GetCorecurrent().strZSN.Trim().ToUpper() + " 产品后壳SN码:" + Corecurrent.GetCorecurrent().strHSN.Trim().ToUpper();
                MessageBox.Show(logtitle);
            }
            finally
            {
                string result = isok ? "Passed" : "Failed";
                LogHelper.WriteLog(Corecurrent.GetCorecurrent().strZSN.Trim().ToUpper() + "过站完成,结果：" + result);

                //this.writeLog(LogType.Error, logtitle);
                //this.FillInfoLog(logtitle);
            }
        }
        private void btnJGY_Click(object sender, EventArgs e)
        {
            //允许加工
            Corecurrent.GetCorecurrent().bolReciveMes = true;
            Corecurrent.GetCorecurrent().bolReciveMes_RY = true;
            Corecurrent.GetCorecurrent().bolReciveMes_RN = false;
            Corecurrent.GetCorecurrent().bolJGY = true;
            Corecurrent.GetCorecurrent().bolJGN = false;
            Corecurrent.GetCorecurrent().bolJGW = false;
            Corecurrent.GetCorecurrent().IsWritePlc = true;
            lblMESSelResult.Text = "互锁反馈允许加工,已发送PLC允许加工,等待PLC断开是否加工...";
            FillMakeLog("SENDPLCYXJG", "互锁反馈允许加工,已发送PLC允许加工,等待PLC断开是否加工...");
            lblMESSelResult.Visible = true;
        }

        private void btnJGN_Click(object sender, EventArgs e)
        {
            //禁止加工
            Corecurrent.GetCorecurrent().bolReciveMes = true;
            Corecurrent.GetCorecurrent().bolReciveMes_RY = false;
            Corecurrent.GetCorecurrent().bolReciveMes_RN = true;
            Corecurrent.GetCorecurrent().bolJGY = false;
            Corecurrent.GetCorecurrent().bolJGN = true;
            Corecurrent.GetCorecurrent().bolJGW = false;
            Corecurrent.GetCorecurrent().IsWritePlc = true;
            lblMESSelResult.Text = "互锁反馈禁止加工,已发送PLC禁止加工,等待PLC断开是否加工...";
            FillMakeLog("SENDPLCJZJG", "互锁反馈禁止加工,已发送PLC禁止加工,等待PLC断开是否加工...");
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
        /// 互锁模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelLockModel_Click(object sender, EventArgs e)
        {
            frmDlocking.Show();
        }


    }
}
