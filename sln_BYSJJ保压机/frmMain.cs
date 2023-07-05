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
using sln_BYSJJ.Model;
using sln_BYSJJ.Controller;

namespace sln_BYSJJ
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
            //等待保压PLC发送命令
            Corecurrent.GetCorecurrent().BY_NowProcedure = Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_IDLE;
            //等待首升降机PLC发送命令
            Corecurrent.GetCorecurrent().SSJJ_NowProcedure = Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_IDLE;
            //等待尾升降机PLC发送命令
            Corecurrent.GetCorecurrent().WSJJ_NowProcedure = Corecurrent.WSJJ_SysRunStatus.WSJJ_Procedure.WSJJ_PROCEDURE_IDLE;
            Corecurrent.GetCorecurrent().BY_dtAlarm = new DataTable();
            Corecurrent.GetCorecurrent().BY_dtAlarm.Columns.Add(new DataColumn("时间", typeof(string)));
            Corecurrent.GetCorecurrent().BY_dtAlarm.Columns.Add(new DataColumn("报警类别", typeof(string)));
            Corecurrent.GetCorecurrent().BY_dtAlarm.Columns.Add(new DataColumn("点位", typeof(string)));
            Corecurrent.GetCorecurrent().BY_dtAlarm.Columns.Add(new DataColumn("报警信息", typeof(string)));
            //PLC初始化
            PLCBY.plcby = new PLCBY();
            PLCSJJ.plcsjj = new PLCSJJ();
            //this.barCodes = new string[] { "\x0010CT~~CD,~CC^~CT~", "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR6,6~SD15^JUS^LRN^CI0^XZ", "^XA", "^MMT", "^PW240", "^LL0128", "^LS0", "^FT17,126^BQN,2,3", @"^FH\^FDLA,P01234567890000062^FS", @"^FT87,76^A0N,22,26^FH\^FDID:^FS", @"^FT87,117^A0N,21,21^FH\^FDS/N:^FS", @"^FT224,171^A0N,28,28^FH\^^FS", @"^FT130,116^A0N,19,24^FH\^FD0000623^FS", @"^FT17,50^A0N,20,21^FH\^FDModel^FS", "^PQ1,0,1,Y^XZ" };
            //this.barCodes = new string[] { "\x0010CT~~CD,~CC^~CT~", "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR6,6~SD15^JUS^LRN^CI0^XZ", "^XA", "^MMT", "^PW240", "^LL0128", "^LS0", "^FT17,142^BQN,2,3", @"^FH\^FDLA,P01234567890000062^FS", @"^FT87,92^A0N,22,26^FH\^FDID:^FS", @"^FT87,133^A0N,21,21^FH\^FDS/N:^FS", @"^FT224,187^A0N,28,28^FH\^^FS", @"^FT130,132^A0N,19,24^FH\^FD0000623^FS", @"^FT17,50^A0N,20,21^FH\^FDModel^FS", "^PQ1,0,1,Y^XZ" };
            //this.barCodes = new string[] { "\x0010CT~~CD,~CC^~CT~", "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR6,6~SD15^JUS^LRN^CI0^XZ", "^XA", "^MMT", "^PW240", "^LL0128", "^LS0", "^FT17,140^BQN,2,3", @"^FH\^FDLA,P01234567890000062^FS", @"^FT87,90^A0N,22,26^FH\^FDID:^FS", @"^FT87,131^A0N,21,21^FH\^FDS/N:^FS", @"^FT224,185^A0N,28,28^FH\^^FS", @"^FT130,130^A0N,19,24^FH\^FD0000623^FS", @"^FT17,50^A0N,20,21^FH\^FDModel^FS", "^PQ1,0,1,Y^XZ" };
            this.barCodes = new string[] { "\x0010CT~~CD,~CC^~CT~", "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR6,6~SD15^JUS^LRN^CI0^XZ", "^XA", "^MMT", "^PW240", "^LL0128", "^LS0", "^FT17,135^BQN,2,3", @"^FH\^FDLA,P01234567890000062^FS", @"^FT87,90^A0N,22,26^FH\^FDID:^FS", @"^FT87,125^A0N,21,21^FH\^FDS/N:^FS", @"^FT224,185^A0N,28,28^FH\^^FS", @"^FT130,125^A0N,19,24^FH\^FD0000623^FS", @"^FT17,50^A0N,20,21^FH\^FDModel^FS", "^PQ1,0,1,Y^XZ" };

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
            if (Corecurrent.GetCorecurrent().BY_IsConnnectPlc == false)
            {
                FillInfoLog("BYPLCNOCONN", "保压PLC 连接不成功，请检查IP地址、机架、插槽等是否正确");
            }
            else
            {
                FillInfoLog("BYPLCYESCONN", "保压PLC 连接成功!");
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
            //if (Corecurrent.GetCorecurrent().BY_IsAlarm == true)
            //{
            //    if (Corecurrent.GetCorecurrent().BY_IsShowAlarm == false)
            //    {
            //        fromMsg.Show();
            //        Corecurrent.GetCorecurrent().BY_IsShowAlarm = true;
            //    }
            //    else if (Corecurrent.GetCorecurrent().BY_IsShowAlarm == true)
            //    {
            //        fromMsg.Show();
            //        if (Corecurrent.GetCorecurrent().BY_dtAlarm.Rows.Count > 0)
            //        {
            //            if(Corecurrent.GetCorecurrent().BY_bolTableRe == true)
            //            {
            //                fromMsg.SetDataSource();
            //                Corecurrent.GetCorecurrent().BY_bolTableRe = false;
            //            }

            //        }     
            //    }
            //}
            //else if(Corecurrent.GetCorecurrent().BY_IsAlarm == false)
            //{
            //    if (Corecurrent.GetCorecurrent().BY_IsShowAlarm == true)
            //    {
            //        fromMsg.Hide();
            //        Corecurrent.GetCorecurrent().BY_IsShowAlarm = false;
            //    }
            //    else
            //    {
            //        //fromMsg.SetDataSource();
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
            //实际压力值
            labelBYSJYLZ.Text = Corecurrent.GetCorecurrent().BY_strSJYLZ;
            //伺服故障代码
            labelBYSFGZDM.Text = Corecurrent.GetCorecurrent().BY_strSFGZDM;
            //伺服报警代码
            labelBYSFBJDM.Text = Corecurrent.GetCorecurrent().BY_strSFBJDM;
            //伺服实际位置
            labelBYSFSJWZ.Text = Corecurrent.GetCorecurrent().BY_strSFSJWZ;
            //伺服当前倍率
            labelBYSFDQBL.Text = Corecurrent.GetCorecurrent().BY_strSFDQBL;
            //RFID信号强度
            if (Corecurrent.GetCorecurrent().BY_strXHQD == "1")
            {
                labelBYXHQD.Text = "弱";
            }
            else if (Corecurrent.GetCorecurrent().BY_strXHQD == "2")
            {
                labelBYXHQD.Text = "中等";
            }
            else if (Corecurrent.GetCorecurrent().BY_strXHQD == "3")
            {
                labelBYXHQD.Text = "强";
            }
            else
            {
                labelBYXHQD.Text = "未知代码:" + Corecurrent.GetCorecurrent().BY_strXHQD;
            }
            //RFID通讯状态
            if (Corecurrent.GetCorecurrent().BY_strTXZT == "0")
            {
                labelBYTXZT.Text = "异常";
            }
            else if (Corecurrent.GetCorecurrent().BY_strTXZT == "1")
            {
                labelBYTXZT.Text = "正常";
            }
            else
            {
                labelBYTXZT.Text = "未知代码:" + Corecurrent.GetCorecurrent().BY_strTXZT;
            }

            //当前配方
            if (Corecurrent.GetCorecurrent().BY_strDQPF == "1")
            {
                labelBYDQPF.Text = "C255";
            }
            else if (Corecurrent.GetCorecurrent().BY_strDQPF == "2")
            {
                labelBYDQPF.Text = "C235";
            }
            else if (Corecurrent.GetCorecurrent().BY_strDQPF == "3")
            {
                labelBYDQPF.Text = "C206仪表";
            }
            else if (Corecurrent.GetCorecurrent().BY_strDQPF == "4")
            {
                labelBYDQPF.Text = "C206中控";
            }
            else if (Corecurrent.GetCorecurrent().BY_strDQPF == "5")
            {
                labelBYDQPF.Text = "C255黑框";
            }
            else
            {
                labelBYDQPF.Text = "未知代码:" + Corecurrent.GetCorecurrent().BY_strDQPF;
            }
            //---------------------工位参数end----------------------------
            //保压主SN码
            if (Corecurrent.GetCorecurrent().BY_strZSN.Trim().Replace("\0", "").Length == 0)
            {
                lblBYZSN.Text = "-";
            }
            else
            {
                lblBYZSN.Text = Corecurrent.GetCorecurrent().BY_strZSN;
            }

            //升降机扫描SN码
            //if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Trim().Replace("\0", "").Length == 0)
            //{
            //    lblSJJSCAN.Text = "-";
            //}
            //else
            //{
            //    lblSJJSCAN.Text = Corecurrent.GetCorecurrent().SJJ_strSCANSN;
            //}

            //升降机SN接收
            //if (Corecurrent.GetCorecurrent().SJJ_strPLCSN.Trim().Replace("\0", "").Length == 0)
            //{
            //    lblSJJSNJS.Text = "-";
            //}
            //else
            //{
            //    lblSJJSNJS.Text = Corecurrent.GetCorecurrent().SJJ_strPLCSN;
            //}

            //升降机SN发送
            if (Corecurrent.GetCorecurrent().SJJ_strGKJSN.Trim().Replace("\0", "").Length == 0)
            {
                lblSJJSNFS.Text = "-";
            }
            else
            {
                lblSJJSNFS.Text = Corecurrent.GetCorecurrent().SJJ_strGKJSN;
            }

            //保压点位状态
            if (Corecurrent.GetCorecurrent().BY_bolLblSFJG == true)
            {
                lblBYSFJG.BackColor = Color.Green;
            }
            else
            {
                lblBYSFJG.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().BY_bolLblJGOK == true)
            {
                lblBYJGOK.BackColor = Color.Green;
            }
            else
            {
                lblBYJGOK.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().BY_bolLblJGNG == true)
            {
                lblBYJGNG.BackColor = Color.Green;
            }
            else
            {
                lblBYJGNG.BackColor = Color.White;
            }
            if (Corecurrent.GetCorecurrent().BY_bolLblYXJG == true)
            {
                lblBYYXJG.BackColor = Color.Green;
            }
            else
            {
                lblBYYXJG.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().BY_bolLblJZJG == true)
            {
                lblBYJZJG.BackColor = Color.Green;
            }
            else
            {
                lblBYJZJG.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().BY_bolLblJSWC == true)
            {
                lblBYJSWC.BackColor = Color.Green;
            }
            else
            {
                lblBYJSWC.BackColor = Color.White;
            }

            //升降机点位状态
            //if (Corecurrent.GetCorecurrent().SJJ_bolLblQQFS == true)
            //{
            //    lblSJJQQFS.BackColor = Color.Green;
            //}
            //else
            //{
            //    lblSJJQQFS.BackColor = Color.White;
            //}

            if (Corecurrent.GetCorecurrent().SJJ_bolLblPGXRWC == true)
            {
                lblSJJPGXRWC.BackColor = Color.Green;
            }
            else
            {
                lblSJJPGXRWC.BackColor = Color.White;
            }


            if (Corecurrent.GetCorecurrent().WSJJ_bolLblDBWC == true)
            {
                lblSJJDBWC.BackColor = Color.Green;
            }
            else
            {
                lblSJJDBWC.BackColor = Color.White;
            }

            if (Corecurrent.GetCorecurrent().WSJJ_bolLblDBSB == true)
            {
                lblSJJDBSB.BackColor = Color.Green;
            }
            else
            {
                lblSJJDBSB.BackColor = Color.White;
            }
            //if (Corecurrent.GetCorecurrent().SJJ_bolLblGPXRWC == true)
            //{
            //    lblSJJGPXRWC.BackColor = Color.Green;
            //}
            //else
            //{
            //    lblSJJGPXRWC.BackColor = Color.White;
            //}

            #region 保压流程
            //保压流程
            if (Corecurrent.GetCorecurrent().BY_NowProcedure == Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_IDLE)
            {
                FillMakeLog("BY_WAITASKPROCESS", "等待保压PLC指令");
                lblSysMsg_BY.Text = "等待保压PLC指令";
                lblMESSelResult.Text = "";
                lblMESSelResult.Visible = false;
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().BY_bolHSXC = false;
                Corecurrent.GetCorecurrent().BY_strHSQQJG = "";
                Corecurrent.GetCorecurrent().BY_bolHSQQ = false;
                Corecurrent.GetCorecurrent().BY_bolHSJG = false;
                Corecurrent.GetCorecurrent().BY_bolShowJg = false;
            }
            if (Corecurrent.GetCorecurrent().BY_NowProcedure == Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_PROCESSYN)
            {
                if (Corecurrent.GetCorecurrent().BY_strZSN.Trim().Replace("\0", "").Length == 0)
                {
                    FillMakeLog("ZSNK", "主SN码不能为空");
                    lblSysMsg_BY.Text = "主SN码不能为空";
                    lblMESSelResult.Text = "";
                    lblMESSelResult.Visible = false;
                    tabC.SelectedTab = tabp1;
                }
                else if (Corecurrent.GetCorecurrent().BY_strZSN.Trim().Replace("\0", "").Length != 28 && Corecurrent.GetCorecurrent().BY_strZSN.Trim().Replace("\0", "").Length != 19)
                {
                    //FillMakeLog("ZSNCD", "主SN码长度应该为28位");
                    //lblSysMsg_BY.Text = "主SN码长度应该为28位";
                    //lblMESSelResult.Text = "";
                    //lblMESSelResult.Visible = false;
                    //tabC.SelectedTab = tabp1;
                    FillMakeLog("ZSNCD", "主SN码长度应该为28位或19位");
                    lblSysMsg_BY.Text = "主SN码长度应该为28位或19位";
                    lblMESSelResult.Text = "";
                    lblMESSelResult.Visible = false;
                    tabC.SelectedTab = tabp1;
                }
                else
                {
                    if (utility.dSV.workMode == false)
                    {
                        //FillMakeLog("BY_ASKMES", "询问互锁是否加工？");
                        FillInfoLog("BY_DB200100TRUE", "接收到保压PLC DB2001 0.0是否加工请求");
                        //tabC.SelectedTab = tabp2;

                        Corecurrent.GetCorecurrent().BY_bolReciveMES = true;
                        Corecurrent.GetCorecurrent().BY_bolReciveMes_RY = true;
                        Corecurrent.GetCorecurrent().BY_bolReciveMes_RN = false;
                        Corecurrent.GetCorecurrent().BY_bolJGY = true;
                        Corecurrent.GetCorecurrent().BY_bolJGN = false;
                        Corecurrent.GetCorecurrent().BY_bolJGW = false;
                        Corecurrent.GetCorecurrent().BY_IsWritePlc = true;
                        lblSysMsg_BY.Text = "互锁反馈允许加工,已发送保压PLC允许加工,等待保压PLC断开是否加工...";
                        FillMakeLog("BY_SENDPLCYXJG", "互锁反馈允许加工,已发送保压PLC允许加工,等待保压PLC断开是否加工...");
                        //lblMESSelResult.Visible = true;
                    }
                    else if (utility.dSV.workMode == true)
                    {
                        if (Corecurrent.GetCorecurrent().BY_bolHSXC == false)
                        {
                            lblSysMsg_BY.Text = "请求已发送互锁，等待互锁反馈...";
                        }
                        Corecurrent.GetCorecurrent().BY_strHSQQJG = "QQ";
                        Corecurrent.GetCorecurrent().BY_bolHSXC = true;
                    }
                }
            }
            if (Corecurrent.GetCorecurrent().BY_NowProcedure == Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_ASKMES)
            {
                lblSysMsg_BY.Text = "问询MES是否可以加工,等待MES回答...";
                tabC.SelectedTab = tabp1;
            }
            if (Corecurrent.GetCorecurrent().BY_NowProcedure == Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_JGWAIT)
            {
                FillMakeLog("BY_JGWAITRESULT", "产品加工中,等待加工结果...");
                FillInfoLog("BY_DB200100FALSE", "断开保压PLC DB2001 0.0是否加工请求");
                lblSysMsg_BY.Text = "产品加工中,等待加工结果...";
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().BY_bolWaitJGResult = true;
            }
            if (Corecurrent.GetCorecurrent().BY_NowProcedure == Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_MESNG)
            {
                FillMakeLog("BY_MESJGJZ", "互锁通知禁止加工,是否等待PLC通知结果？如何处理？");
                FillInfoLog("BY_DB200100FALSE", "断开保压PLC DB2001 0.0是否加工请求");
                lblSysMsg_BY.Text = "互锁通知禁止加工,是否等待PLC通知结果？如何处理？";
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().BY_bolWaitJGResult = true;
            }
            if (Corecurrent.GetCorecurrent().BY_NowProcedure == Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_SENDMES)
            {
                if (Corecurrent.GetCorecurrent().BY_bolShowJg == false)
                {
                    Corecurrent.GetCorecurrent().BY_dtTime = DateTime.Now;
                    Corecurrent.GetCorecurrent().BY_bolShowJg = true;
                    if (Corecurrent.GetCorecurrent().BY_bolResultOK == true
                     && Corecurrent.GetCorecurrent().BY_bolResultNG == false)
                    {
                        fromJg.setJG("OK");
                    }
                    if (Corecurrent.GetCorecurrent().BY_bolResultOK == false
                     && Corecurrent.GetCorecurrent().BY_bolResultNG == true)
                    {
                        fromJg.setJG("NG");
                    }
                    fromJg.timUI.Enabled = true;
                    fromJg.Show();
                }

                if (utility.dSV.workMode == false)
                {
                    Corecurrent.GetCorecurrent().BY_strHSQQJG = "";
                    Corecurrent.GetCorecurrent().BY_bolHSXC = false;

                    if (Corecurrent.GetCorecurrent().BY_bolResultOK == true
                        && Corecurrent.GetCorecurrent().BY_bolResultNG == false)
                    {
                        FillMakeLog("BY_JGOK", "结果OK");
                        FillInfoLog("BY_DB200101TRUE", "接收到保压PLC DB2001 0.1加工结果OK");
                        lblBYResult.Text = "结果OK";
                        //updateHS(true);
                    }
                    if (Corecurrent.GetCorecurrent().BY_bolResultOK == false
                    && Corecurrent.GetCorecurrent().BY_bolResultNG == true)
                    {
                        FillMakeLog("BY_JGNG", "结果NG");
                        FillInfoLog("BY_DB200102TRUE", "接收到保压PLC DB2001 0.2加工结果NG");
                        lblBYResult.Text = "结果NG";
                        //updateHS(false);
                    }
                    Corecurrent.GetCorecurrent().BY_bolJGY = false;
                    Corecurrent.GetCorecurrent().BY_bolJGN = false;
                    Corecurrent.GetCorecurrent().BY_bolJGW = true;
                    Corecurrent.GetCorecurrent().BY_IsWritePlc = true;
                    FillMakeLog("BY_SENDPLCMES", "SN码及加工结果已发送互锁,已发送保压PLC接收完成,等待保压PLC清除结果...");
                    FillInfoLog("BY_DB200202TRUE", "发送保压PLC DB2002 0.2接收完成");
                    lblSysMsg_BY.Text = "SN码及加工结果已发送互锁,已发送保压PLC接收完成,等待保压PLC清除结果...";
                    tabC.SelectedTab = tabp1;
                }
                else if (utility.dSV.workMode == true)
                {
                    Corecurrent.GetCorecurrent().BY_strHSQQJG = "JG";
                    Corecurrent.GetCorecurrent().BY_bolHSXC = true;
                }
            }
            if (Corecurrent.GetCorecurrent().BY_NowProcedure == Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_END)
            {
                //需要写入PLC时候开关
                Corecurrent.GetCorecurrent().BY_IsWritePlc = false;
                //是否从PLC接收请求开关
                Corecurrent.GetCorecurrent().BY_bolRecivePLC = false;
                //是否从MES接收数据开关
                Corecurrent.GetCorecurrent().BY_bolReciveMES = false;
                //从MES接收允许加工
                Corecurrent.GetCorecurrent().BY_bolReciveMes_RY = false;
                //从MES接收禁止加工
                Corecurrent.GetCorecurrent().BY_bolReciveMes_RN = false;
                //人工触发加工状态允许加工
                Corecurrent.GetCorecurrent().BY_bolJGY = false;
                //人工触发加工状态禁止加工
                Corecurrent.GetCorecurrent().BY_bolJGN = false;
                //给定加工完成信号
                Corecurrent.GetCorecurrent().BY_bolJGW = false;
                //等待加工结果状态开关
                Corecurrent.GetCorecurrent().BY_bolWaitJGResult = false;
                //加工结果已经出来
                Corecurrent.GetCorecurrent().BY_bolGetJGResult = false;
                //加工结果OK
                Corecurrent.GetCorecurrent().BY_bolResultOK = false;
                //加工结果NG
                Corecurrent.GetCorecurrent().BY_bolResultNG = false;
                Corecurrent.GetCorecurrent().mapMake.Remove("BY_WAITASKPROCESS");
                Corecurrent.GetCorecurrent().mapMake.Remove("BY_ASKMES");
                Corecurrent.GetCorecurrent().mapMake.Remove("BY_JGWAITRESULT");

                Corecurrent.GetCorecurrent().mapMake.Remove("BY_MESJGJZ");
                Corecurrent.GetCorecurrent().mapMake.Remove("BY_JGOK");
                Corecurrent.GetCorecurrent().mapMake.Remove("BY_JGNG");
                Corecurrent.GetCorecurrent().mapMake.Remove("BY_SENDPLCYXJG");

                Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200100TRUE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200100FALSE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200101TRUE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200102TRUE");
                Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200202TRUE");
                lblSysMsg_BY.Text = "等待保压PLC指令";
                lblBYResult.Text = "-";
                lblMESSelResult.Text = "";
                lblMESSelResult.Visible = false;
                tabC.SelectedTab = tabp1;
                Corecurrent.GetCorecurrent().BY_NowProcedure = Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_IDLE;
            }
            #endregion

            #region 首升降机流程
            //首升降机流程
            //if (Corecurrent.GetCorecurrent().SSJJ_NowProcedure == Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_IDLE)
            //{
            //    FillMakeLog("SSJJ_WAITASKPROCESS", "等待升降机PLC指令");
            //    lblSysMsg_SJJ.Text = "等待升降机PLC指令";
            //}
            //if (Corecurrent.GetCorecurrent().SSJJ_NowProcedure == Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_GETQQ)
            //{
            //    FillMakeLog("SSJJ_GETQQ", "获得PLC请求发送,写入SN及完成,等待PLC断开请求发送");
            //    FillInfoLog("SSJJ_DB200100TRUE", "接收到升降机PLC DB2001 0.0请求发送");
            //    lblSysMsg_SJJ.Text = "获得PLC请求发送,写入SN及完成,等待PLC断开请求发送";
            //    Corecurrent.GetCorecurrent().SJJ_IsWriteFlag = "SSJJ_SNWC_WR";
            //    Corecurrent.GetCorecurrent().SJJ_IsWritePlc = true;
            //}
            #endregion

            #region 尾升降机流程
            //尾升降机流程
            if (Corecurrent.GetCorecurrent().WSJJ_NowProcedure == Corecurrent.WSJJ_SysRunStatus.WSJJ_Procedure.WSJJ_PROCEDURE_IDLE)
            {
                Corecurrent.GetCorecurrent().WSJJ_bolDBF = false;
                FillMakeLog("WSJJ_WAITASKPROCESS", "等待升降机PLC指令");
                lblSysMsg_SJJ.Text = "等待升降机PLC指令";
            }
            if (Corecurrent.GetCorecurrent().WSJJ_NowProcedure == Corecurrent.WSJJ_SysRunStatus.WSJJ_Procedure.WSJJ_PROCEDURE_DB)
            {
                if (Corecurrent.GetCorecurrent().WSJJ_bolDBF == false)
                {
                    Corecurrent.GetCorecurrent().WSJJ_bolDBF = true;
                    FillMakeLog("WSJJ_GETQQ", "获得PLC打标请求,已发打标指令");
                    FillInfoLog("WSJJ_DB200120TRUE", "接收到升降机PLC DB2001 2.0打标请求");
                    lblSysMsg_SJJ.Text = "获得PLC打标请求,已发打标指令";
                    bool bolTF = false;
                    bolTF = execFile("zd888t", Corecurrent.GetCorecurrent().SJJ_strGKJSN);
                    if (bolTF)
                    {
                        Corecurrent.GetCorecurrent().WSJJ_bolDBF = true;
                        lblSysMsg_SJJ.Text = "打标成功已发送指令,等待PLC清空打标请求";
                        Corecurrent.GetCorecurrent().SJJ_IsWriteFlag = "WSJJ_DBWC_WR";
                        Corecurrent.GetCorecurrent().SJJ_IsWritePlc = true;
                    }
                    else
                    {
                        Corecurrent.GetCorecurrent().WSJJ_bolDBF = false;
                        lblSysMsg_SJJ.Text = "打标失败已发送指令,等待PLC清空打标请求";
                        Corecurrent.GetCorecurrent().SJJ_IsWriteFlag = "WSJJ_DBSB_WR";
                        Corecurrent.GetCorecurrent().SJJ_IsWritePlc = true;
                    }
                }
            }
            #endregion
        }
        private void timHSCZ_Tick(object sender, EventArgs e)
        {
            if (Corecurrent.GetCorecurrent().BY_bolHSXC == true)
            {
                if (Corecurrent.GetCorecurrent().BY_strHSQQJG == "QQ")
                {
                    if (Corecurrent.GetCorecurrent().BY_bolHSQQ == false)
                    {
                        Corecurrent.GetCorecurrent().BY_bolHSQQ = true;
                        long s = SV_Interlocking.sv_lockInfo(Corecurrent.GetCorecurrent().BY_strZSN.Trim().ToUpper());
                        if (s == 0)
                        {
                            lblSysMsg_BY.Text = "互锁反馈允许加工,已发送保压PLC允许加工,等待保压PLC断开是否加工...";
                            lblMESSelResult.Text = "";
                            lblMESSelResult.Visible = false;
                            tabC.SelectedTab = tabp1;

                            Corecurrent.GetCorecurrent().BY_bolReciveMES = true;
                            Corecurrent.GetCorecurrent().BY_bolReciveMes_RY = true;
                            Corecurrent.GetCorecurrent().BY_bolReciveMes_RN = false;
                            Corecurrent.GetCorecurrent().BY_bolJGY = true;
                            Corecurrent.GetCorecurrent().BY_bolJGN = false;
                            Corecurrent.GetCorecurrent().BY_bolJGW = false;
                            Corecurrent.GetCorecurrent().BY_IsWritePlc = true;
                            FillMakeLog("BY_SENDPLCYXJG", "互锁反馈允许加工,已发送保压PLC允许加工,等待保压PLC断开是否加工...");
                        }
                        else if (s == -1)
                        {
                            lblSysMsg_BY.Text = "互锁反馈禁止加工,已发送PLC禁止加工,等待保压PLC断开是否加工...";
                            lblMESSelResult.Text = "";
                            lblMESSelResult.Visible = false;
                            tabC.SelectedTab = tabp1;

                            Corecurrent.GetCorecurrent().BY_bolReciveMES = true;
                            Corecurrent.GetCorecurrent().BY_bolReciveMes_RY = false;
                            Corecurrent.GetCorecurrent().BY_bolReciveMes_RN = true;
                            Corecurrent.GetCorecurrent().BY_bolJGY = false;
                            Corecurrent.GetCorecurrent().BY_bolJGN = true;
                            Corecurrent.GetCorecurrent().BY_bolJGW = false;
                            Corecurrent.GetCorecurrent().BY_IsWritePlc = true;
                            FillMakeLog("BY_SENDPLCJZJG", "互锁反馈禁止加工,已发送PLC禁止加工,等待保压PLC断开是否加工...");
                        }
                        else
                        {
                            MessageBox.Show("互锁验证失败！");
                        }
                    }
                }
                if (Corecurrent.GetCorecurrent().BY_strHSQQJG == "JG")
                {
                    if (Corecurrent.GetCorecurrent().BY_bolHSJG == false)
                    {
                        Corecurrent.GetCorecurrent().BY_bolHSJG = true;
                        if (Corecurrent.GetCorecurrent().BY_bolResultOK == true
                        && Corecurrent.GetCorecurrent().BY_bolResultNG == false)
                        {
                            FillMakeLog("BY_JGOK", "结果OK");
                            FillInfoLog("BY_DB200101TRUE", "接收到保压PLC DB2001 0.1加工结果OK");
                            lblBYResult.Text = "结果OK";
                            updateHS(true);
                        }
                        if (Corecurrent.GetCorecurrent().BY_bolResultOK == false
                        && Corecurrent.GetCorecurrent().BY_bolResultNG == true)
                        {
                            FillMakeLog("BY_JGNG", "结果NG");
                            FillInfoLog("BY_DB200102TRUE", "接收到保压PLC DB2001 0.2加工结果NG");
                            lblBYResult.Text = "结果NG";
                            updateHS(false);
                        }
                        Corecurrent.GetCorecurrent().BY_bolJGY = false;
                        Corecurrent.GetCorecurrent().BY_bolJGN = false;
                        Corecurrent.GetCorecurrent().BY_bolJGW = true;
                        Corecurrent.GetCorecurrent().BY_IsWritePlc = true;
                        FillMakeLog("BY_SENDPLCMES", "SN码及加工结果已发送互锁,已发送保压PLC接收完成,等待保压PLC清除结果...");
                        FillInfoLog("BY_DB200202TRUE", "发送保压PLC DB2002 0.2接收完成");
                        lblSysMsg_BY.Text = "SN码及加工结果已发送互锁,已发送保压PLC接收完成,等待保压PLC清除结果...";
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
                    UUT_SERIAL_NUMBER = Corecurrent.GetCorecurrent().BY_strZSN.Trim().ToUpper(),
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
                logtitle = "上传互锁失败," + exception.Message + (isok ? "OK,产品主SN码:" : "NG,产品主SN码:") + Corecurrent.GetCorecurrent().BY_strZSN.Trim().ToUpper();
                MessageBox.Show(logtitle);
            }
            finally
            {
                //this.writeLog(LogType.Error, logtitle);
                //this.FillInfoLog(logtitle);
                string result = isok ? "Passed" : "Failed";
                LogHelper.WriteLog(Corecurrent.GetCorecurrent().BY_strZSN.Trim().ToUpper() + "过站完成,结果：" + result);
            }
        }
        private void btnJGY_Click(object sender, EventArgs e)
        {
            //允许加工
            Corecurrent.GetCorecurrent().BY_bolReciveMES = true;
            Corecurrent.GetCorecurrent().BY_bolReciveMes_RY = true;
            Corecurrent.GetCorecurrent().BY_bolReciveMes_RN = false;
            Corecurrent.GetCorecurrent().BY_bolJGY = true;
            Corecurrent.GetCorecurrent().BY_bolJGN = false;
            Corecurrent.GetCorecurrent().BY_bolJGW = false;
            Corecurrent.GetCorecurrent().BY_IsWritePlc = true;
            lblMESSelResult.Text = "互锁反馈允许加工,已发送保压PLC允许加工,等待保压PLC断开是否加工...";
            FillMakeLog("BY_SENDPLCYXJG", "互锁反馈允许加工,已发送保压PLC允许加工,等待保压PLC断开是否加工...");
            lblMESSelResult.Visible = true;
        }

        private void btnJGN_Click(object sender, EventArgs e)
        {
            //禁止加工
            Corecurrent.GetCorecurrent().BY_bolReciveMES = true;
            Corecurrent.GetCorecurrent().BY_bolReciveMes_RY = false;
            Corecurrent.GetCorecurrent().BY_bolReciveMes_RN = true;
            Corecurrent.GetCorecurrent().BY_bolJGY = false;
            Corecurrent.GetCorecurrent().BY_bolJGN = true;
            Corecurrent.GetCorecurrent().BY_bolJGW = false;
            Corecurrent.GetCorecurrent().BY_IsWritePlc = true;
            lblMESSelResult.Text = "互锁反馈禁止加工,已发送PLC禁止加工,等待保压PLC断开是否加工...";
            FillMakeLog("BY_SENDPLCJZJG", "互锁反馈禁止加工,已发送PLC禁止加工,等待保压PLC断开是否加工...");
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
            int intCnt = 0;
            bool bolTF = false;
            while (bolTF == false)
            {
                intCnt = intCnt + 1;
                bolTF = execFile("zd888t", "P123456789012345678901234567");
            }
            labelSystem.Text = intCnt.ToString();
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
                array[12] = array[12].Replace("0000623", codes.Substring(codes.Length - 7, 7));
                //array[13] = array[13].Replace("Model", "DT" + codes.Substring(6, 4) + "/" + codes.Substring(10, 2));
                array[13] = array[13].Replace("Model", txtBQMC.Text);
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
            //switch (e.KeyChar)
            //{
            //    case '\b':
            //        if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length > 0)
            //        {
            //            Corecurrent.GetCorecurrent().SJJ_strSCANSN = "";
            //        }
            //        break;

            //    case '\x001b':
            //        Corecurrent.GetCorecurrent().SJJ_strSCANSN = null;
            //        base.DialogResult = DialogResult.Cancel;
            //        base.Close();
            //        break;

            //    default:
            //        if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length < 0x1c)
            //        {
            //            Corecurrent.GetCorecurrent().SJJ_strSCANSN = Corecurrent.GetCorecurrent().SJJ_strSCANSN + e.KeyChar.ToString();
            //        }
            //        if ((Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length > 0) && !this.timUI.Enabled)
            //        {
            //            this.timUI.Enabled = true;
            //        }
            //        if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length == 0x1c)
            //        {

            //        }
            //        break;
            //}
        }

        private void labelLockModel_Click(object sender, EventArgs e)
        {
            frmDlocking.Show();
        }


    }
}
