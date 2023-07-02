using PDAMaster;
using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace ScrewMachineManagementSystem
{
    public class utility
    {


        /*
         * changeTime = string.IsNullOrWhiteSpace(attributeTime.ValueAsString) ? " " : attributeTime.ValueAsString;
           ？号后面是判断之后的输出语句，
           ：的左边是判断为真的时候的输出
             右边是判断为否的时候的输出
            IswParts parts = (HandShakeItem != null ? HandShakeItem.GetAllParts() : null);
         */
        /// <summary>
        /// 系统参数xml文件路径
        /// </summary>
        public static string _xmlSystemParamFilePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName) + "\\systemParam.xml";


        /// <summary>
        /// 登录用户信息
        /// </summary>
        public static string SystemName= "Display2单工位螺丝机";
        /// <summary>
        /// 参数列表
        /// </summary>
        public static List<ModelParam> paramModeList = new List<ModelParam>();
        /// <summary>
        /// 工作模式
        /// </summary>
        public static bool LoginModeEngineer = false;
        /// <summary>
        /// 是否有临时命令，T有则不轮寻
        /// </summary>
        public static bool isTempCommand = false;


        /// <summary>
        /// 设备总工位数,比实际数加一，0代表自选工位
        /// </summary>
        public static int workStationCounts = 3;

        /// <summary>
        /// 电批扭力系数
        /// </summary>
        public static decimal ScrewMachineTorisionRatio = 1;


        /// <summary>
        /// 圈数系数，100=1r(圈)
        /// </summary>
        public static double CylinderNumberRatio = 0.01;


        /// <summary>
        /// 串口对象，，4-模拟量采集
        /// </summary>
        public static List<Model.ModbusObject> modbusObjects;

        /// <summary>
        /// 登录状态，T已登录，F已注销
        /// </summary>
        public static bool isLogin;
        /// <summary>
        /// 静音标志,True静音，False不静音
        /// </summary>
        public static bool bool_Mute = false;
        /// <summary>
        /// 停止标志
        /// </summary>
        public static bool bool_Stop = false;
        /// <summary>
        /// 伺服复位标志
        /// </summary>
        public static bool bool_Servo = false;

        public static bool bool_Home_display = false;


        ///// <summary>
        ///// 是否提示回原点，T回原位成功，F提示请回原点
        ///// </summary>
        //public static bool bool_Home = false;
        ///// <summary>
        ///// 回原点画面是否显示
        ///// </summary>
        //public static bool bool_HomeFormOpened = false;
        ///// <summary>
        ///// T正在回原点中，F已回原点
        ///// </summary>
        //public static bool bool_Homeing = false;
        
        /// <summary>
        /// 心跳
        /// </summary>
        public static bool bool_Heart = false;

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public static string loginUserID;
        /// <summary>
        /// 本机信息
        /// </summary>
        public static string stationId;

        /// <summary>
        /// 当前产品SN,P码
        /// </summary>
        public static string currentProductSN;

        /// <summary>
        /// 当前产品SN,M码
        /// </summary>
        public static string currentProductSN_M;
        /// <summary>
        /// 产品吗长度，新任务扫码使用
        /// </summary>
        public static int ProductCodeLenght = 8;



        /// <summary>
        /// 校准扭力示教器任务号
        /// </summary>
        public static int teachPendantTaskID_TorqueForce = 15;
        /// <summary>
        /// 新任务form是否打开，T打开，F未打开
        /// </summary>
        public static bool FormNewTaskOpened = false;

        /// <summary>
        /// 扫码form是否打开，T打开，F未打开
        /// </summary>
        public static bool FormScanSNFormOpened = false;
        /// <summary>
        /// 扫码form是否被人为关闭，T人为未扫码关掉，F正常扫码后关掉
        /// </summary>
        public static bool FormScanSNFormisClosed= false;

        /// <summary>
        /// 清除扭力数据表格
        /// </summary>
        public static bool boolClearDataGridView=false;
        /// <summary>
        /// 是否有报警信号
        /// </summary>
        public static bool boolAlarmSingle = false;
        /// <summary>
        /// 互锁信息
        /// </summary>
        public static Model.DSVInterlockingInfo dSV = new Model.DSVInterlockingInfo();
        /// <summary>
        /// 当前任务的信息结果
        /// </summary>
        public static Model.structNewWorkTaskInfo structCurrentWorkTask = new Model.structNewWorkTaskInfo();
        /// <summary>
        /// 扫描的产品sn信息和工位,装卸工位刚扫描的数据，selectY
        /// </summary>
        public static Model.struckScanProductSN struckScanProduct = new Model.struckScanProductSN();

        /// <summary>
        /// 生产工位的sn信息
        /// </summary>
        public static Model.struckScanProductSN struckProduceProduct = new Model.struckScanProductSN();

     

        /// <summary>
        /// 数据库路径   app.config connectionStrings
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string LoadAppConfigString(string id = "sqlDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        /// <summary>
        /// 设置Panel边框，左上右下
        /// </summary>
        /// <param name="panel">panel对象</param>
        /// <param name="borderLeft">左边框</param>
        /// <param name="borderTop">上边框</param>
        /// <param name="borderRight">右边框</param>
        /// <param name="borderBottem">下边框</param>
        public static void setPanelBorder(PaintEventArgs ee, Panel panel, int borderLeft, int borderTop, int borderRight, int borderBottem)
        {
            ControlPaint.DrawBorder(ee.Graphics, panel.ClientRectangle,
                  Color.DimGray, borderLeft, ButtonBorderStyle.Solid, //左边
                  Color.DimGray, borderTop, ButtonBorderStyle.Solid, //上边
                  Color.DimGray, borderRight, ButtonBorderStyle.Solid, //右边
                  Color.DimGray, borderBottem, ButtonBorderStyle.Solid);//底边
        }

        /// <summary>
        /// 在panel内显示form
        /// </summary>
        /// <param name="f"></param>
        public static void showForm(Panel panelForm, Form f)
        {
            panelForm.Controls.Clear();
            f.ControlBox = false;
            f.MaximizeBox = false;
            f.MinimizeBox = false;
            // f.FormBorderStyle = FormBorderStyle.None;
            f.TopLevel = false;
            f.WindowState = FormWindowState.Maximized;
            f.Dock = panelForm.Dock;// DockStyle.Fill;
            f.Parent = panelForm;
            //panelForm.Controls.Add(f);
            f.Show();
        }




        public static bool IsNumberic(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public static bool IsDouble(string oText)
        {
            try
            {
                double var1 = Convert.ToDouble(oText);
                return true;
            }
            catch
            {

                return false;
            }
        }

        /// <summary>
        /// ShowMessage是否已经打开
        /// </summary>
        public static bool ShowMessageIsOpened = false;
        /// <summary>
        /// 显示消息,interval=0不自动关闭
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="interval">超时关闭，0不自动关闭</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static DialogResult ShowMessage(string msg, int interval = 0, string title = "系统提示")
        {
            MessageDialog messageDlg = new MessageDialog(msg, interval, title);
            return messageDlg.ShowDialog();
        }

        /// <summary>
        /// 响应式提示,确定返回Yes
        /// </summary>
        /// <param name="title"></param>
        public static DialogResult ShowMessageResponse(string msg, string title = "系统提示", int defaultButton = 0)
        {
            MessageResponseDialog messageResponseDlg = new MessageResponseDialog(msg, title, defaultButton);
            return messageResponseDlg.ShowDialog();
        }
        /// <summary>
        /// 日志对象
        /// </summary>

        public static void saveLog(string info, Model.LogType logType)
        {
            Model.Log log = new Model.Log();
            log.LogDatetime = DateTime.Now;
            log.LogType = (int)logType;
            log.LogTitle = (string)info;
            log.UserID = utility.loginUserID;
            Controller.CONT_Log.Save(log);
        }

        /// <summary>
        /// 添加到listview，再保存到log表
        /// </summary>
        /// <param name="l"></param>
        /// <param name="info"></param>
        /// <param name="logType"></param>
        public static void SaveInfoLog(ListBox l, string info, Model.LogType logType)
        {
            if (l.Items.Count > 500)
                l.Items.Clear();
            l.Items.Add(DateTime.Now.ToString("yy-MM-dd HH:mm:ss,") + info);
            saveLog(info, logType);
        }


        /// <summary>
        /// 取得所有xml参数
        /// </summary>
        public static void getParamList()
        {
            try
            {
                string _currPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                if (!File.Exists(_currPath + "\\systemParam.xml"))
                {
                    //logger.Fatal("参数文件xmlparam.xml丢失！");
                    utility.ShowMessage("参数文件systemParam.xml丢失！");
                    Application.Exit();
                }
                XmlTextReader xmReader = new XmlTextReader(_currPath + "\\systemParam.xml");
                //List<paramModel> modeList = new List<paramModel>();
                Model.ModelParam model = new Model.ModelParam();
                while (xmReader.Read())
                {
                    if (xmReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmReader.Name == "param")
                        {
                            model.PID = xmReader.GetAttribute(0);
                            model.ParamName = xmReader.GetAttribute(1);
                        }
                        if (xmReader.Name == "DefaultValue")
                        {
                            model.DefaultValue = xmReader.ReadElementString();
                        }
                        if (xmReader.Name == "Description")
                        {
                            model.Description = xmReader.ReadElementString();
                        }
                        if (xmReader.Name == "Remark")
                        {
                            model.Remark = xmReader.ReadElementString();
                        }
                    }
                    if (xmReader.NodeType == XmlNodeType.EndElement)
                    {
                        utility.paramModeList.Add(model);
                        model = new Model.ModelParam();
                    }
                }
                utility.paramModeList.RemoveAt(utility.paramModeList.Count - 1);
                xmReader.Close();
                xmReader.Dispose();
                foreach (Model.ModelParam pm in utility.paramModeList)
                {
                    switch (pm.ParamName)
                    {
                        case "WorkStationCount":
                            ConfigurationKeys.WorkStationCount = Convert.ToInt32(pm.DefaultValue);
                            break;
                        case "TaskCount":
                            ConfigurationKeys.teachPendantTaskID_Max = Convert.ToInt32(pm.DefaultValue);
                            break;
                        case "TaskIDStartValue":
                            ConfigurationKeys.TaskIDStartValue = Convert.ToInt32(pm.DefaultValue);
                            break;
                        case "PLC_IP":
                            ConfigurationKeys.PLC_IP = pm.DefaultValue;
                            break;
                        case "PLC_Port":
                            ConfigurationKeys.PLC_Port = Convert.ToInt32(pm.DefaultValue);
                            break;
                        case "PLC_Rack":
                            ConfigurationKeys.PLC_Rack = Convert.ToInt16(pm.DefaultValue);
                            break;
                        case "PLC_Slot":
                            ConfigurationKeys.PLC_Slot = Convert.ToInt16(pm.DefaultValue);
                            break;
                        case "ScrewMachineCount":
                            ConfigurationKeys.ScrewMachineCount = Convert.ToInt32(pm.DefaultValue);
                            break;
                        case "ScrewMachineIP1":
                            ConfigurationKeys.ScrewMachineIP1 = pm.DefaultValue;
                            break;
                        case "ScrewMachinePort1":
                            ConfigurationKeys.ScrewMachinePort1 = Convert.ToInt32(pm.DefaultValue);
                            break;
                        case "ScrewMachineFlag1":
                            ConfigurationKeys.ScrewMachineFlag1 = pm.DefaultValue == "1" ? true : false;
                            break;
                        case "ScrewMachineIP2":
                            ConfigurationKeys.ScrewMachineIP2 = pm.DefaultValue;
                            break;
                        case "ScrewMachinePort2":
                            ConfigurationKeys.ScrewMachinePort2 = Convert.ToInt32(pm.DefaultValue);
                            break;
                        case "ScrewMachineFlag2":
                            ConfigurationKeys.ScrewMachineFlag2 = pm.DefaultValue == "1" ? true : false;
                            break;
                        case "PLC_UpLoad1Code":
                            ConfigurationKeys.PLC_UpLoad1Code = pm.DefaultValue;
                            break;
                        case "MES_UpLoad1Code":
                            ConfigurationKeys.MES_UpLoad1Code = pm.DefaultValue;
                            break;
                        case "Checking_PLC_MES":
                            ConfigurationKeys.Checking_PLC_MES = Convert.ToInt32(pm.DefaultValue);
                            break;
                        //case "MCodeLength":
                        //    ConfigurationKeys.MCodeLength = Convert.ToInt32(pm.DefaultValue);
                        //    break;

                            

                    }
                }
            }
            catch (Exception ex)
            {
                utility.ShowMessage("读取参数文件错误!" + ex.Message);
                return;
            }
        }
    }
}
