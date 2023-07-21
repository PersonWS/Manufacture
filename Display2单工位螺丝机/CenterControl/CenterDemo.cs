using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ScrewMachineManagementSystem.CenterControl
{
    public partial class CenterDemo : Form
    {
        BusinessMain _businessMain;

        bool _isMonitor = false;

        
        /// <summary>
        /// 展示PLC点位信息
        /// </summary>
        Thread _thread_showPlcInfo;
        public CenterDemo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isMonitor)
            {
                ShowMessage("主服务已启动");
                return;
            }
            _businessMain = new BusinessMain();
            _businessMain.MessageOutput += MessageOutput;
            _isMonitor = true;
            ShowMessage("准备启动主服务");
            if (_businessMain.BusinessStart())
            {
                ShowMessage("主服务成功启动");

                _thread_showPlcInfo = new Thread(ShowPLC_PointState);
                _thread_showPlcInfo.IsBackground = true;
                _thread_showPlcInfo.Start();
               // ShowMessage("");
            }
            else
            {
                ShowMessage("主服务启动失败");
            }

        }



        private void ShowPLC_PointState()
        {
            try
            {
                while (_isMonitor)
                {
                    if (_businessMain.PLC_Connect.IsConnected)
                    {
                        lab_PLC_ConnectState.ForeColor = Color.Lime;
                    }
                    else
                    { lab_PLC_ConnectState.ForeColor = Color.DimGray;  }
                    foreach (KeyValuePair<string, PLC_Point> item in BusinessNeedPlcPoint.Dic_gatherPLC_Point)
                    {
                        this.Invoke((new Action(() =>
                        {
                            switch (item.Key)
                            {
                                case "SN码请求":
                                    if ((bool)item.Value.value)
                                    { lab_snRequest.ForeColor = Color.Lime; }
                                    else
                                    { lab_snRequest.ForeColor = Color.DimGray; }
                                    break;
                                case "开始加工请求":
                                    if ((bool)item.Value.value)
                                    { lab_isManufacture.ForeColor = Color.Lime; }
                                    else
                                    { lab_isManufacture.ForeColor = Color.DimGray; }
                                    break;
                                case "结果NG":
                                    if ((bool)item.Value.value)
                                    { lab_ng.ForeColor = Color.Lime; }
                                    else
                                    { lab_ng.ForeColor = Color.DimGray; }
                                    break;
                                case "结果OK":
                                    if ((bool)item.Value.value)
                                    { lab_ok.ForeColor = Color.Lime; }
                                    else
                                    { lab_ok.ForeColor = Color.DimGray; }
                                    break;
                                case"写入SN码":
                                    if (item.Value.value != null && !string.IsNullOrEmpty((string)item.Value.value))
                                    { lab_snWrite.ForeColor = Color.Lime; }
                                    else
                                    { lab_snWrite.ForeColor = Color.DimGray; }
                                    break;
                                case "允许加工请求":
                                    if (item.Value.value!=null&&(bool)item.Value.value)
                                    { lab_manufacturePermission.ForeColor = Color.Lime; }
                                    else
                                    { lab_manufacturePermission.ForeColor = Color.DimGray; }
                                    break;
                                case "禁止加工请求":
                                    if (item.Value.value != null && (bool)item.Value.value)
                                    { lab_manufactureDeny.ForeColor = Color.Lime; }
                                    else
                                    { lab_manufactureDeny.ForeColor = Color.DimGray; }
                                    break;
                                case "互锁结果":
                                    if (item.Value.value != null && (bool)item.Value.value)
                                    { lab_interlock.ForeColor = Color.Lime; }
                                    else
                                    { lab_interlock.ForeColor = Color.DimGray; }
                                    break;
                                case "加工结果收到":
                                    if (item.Value.value != null && (bool)item.Value.value)
                                    { lab_manufactureResultRecept.ForeColor = Color.Lime; }
                                    else
                                    { lab_manufactureResultRecept.ForeColor = Color.DimGray; }
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





        private void MessageOutput(string s)
        {
            ShowMessage(s);
        }



        private void ShowMessage(string s)
        {
            this.Invoke(new Action(() =>
            {

              txt_showMessage.Text+=  string.Format("{0} \r\n{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), s);

                txt_showMessage.ScrollToCaret();
            }));
        }

        private void btn_StopCenterControl_Click(object sender, EventArgs e)
        {

            _businessMain.BusinessStop();
            Thread.Sleep(500);
            ShowMessage("主服务已停止");
            _isMonitor = false;

        }

        private void btn_clearLog_Click(object sender, EventArgs e)
        {
            this.txt_showMessage.Text = "";
        }
    }
}
