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

        Color _color_ON = Color.Lime;
        Color _color_OFF = Color.DimGray;

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
            lab_lastProcessName.Text = BusinessMain._lastProcessName;
            if (_isMonitor)
            {
                ShowMessage("主服务已启动");
                return;
            }
            _businessMain = new BusinessMain();
            _businessMain.MessageOutput += MessageOutput;
            //订阅关键事件
            _businessMain.Need_SN_Request += Need_SN_Request;
            _businessMain.Need_lastProcessName_Request += Need_lastProcessName_Request;
            _businessMain.SaveInformationToMES_Result_Request += SaveInformationToMES_Result_Request;
            _isMonitor = true;
            ShowMessage("准备启动主服务...");
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


        //刷新PLC点位值，并进行显示
        private void ShowPLC_PointState()
        {
            try
            {
                while (_isMonitor)
                {
                    if (_businessMain.PLC_Connect.IsConnected)
                    {
                        lab_PLC_ConnectState.ForeColor = _color_ON;
                    }
                    else
                    { lab_PLC_ConnectState.ForeColor = _color_OFF; }
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
                                    { lab_snWrite.ForeColor = _color_ON; lab_sn.Text = (string)item.Value.value; }
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


        private bool _bool_need_SN_Request = false;
        private string _SN_Number = "";
        //外部申请的回调函数
        //向PLC输入SN
        private string Need_SN_Request()
        {
            _bool_need_SN_Request = false;
            this.Invoke(new Action(() => { lab_snWrite_apply.ForeColor = _color_ON; }));
            while (!_bool_need_SN_Request)
            {
                Thread.Sleep(500);
            }
            this.Invoke(new Action(() => { lab_snWrite_apply.ForeColor = _color_OFF; }));
            return _SN_Number;
        }

        private bool _bool_Need_lastProcessName_Request = false;
        private string _lastProcessName = "";
        /// 获取上一工序名称 传出的string为SN码
        private string Need_lastProcessName_Request(string SN)
        {
            _bool_Need_lastProcessName_Request = false;
            this.Invoke(new Action(() =>
            {
                lab_manufacturePermission_apply.ForeColor = _color_ON;
                lab_manufactureDeny_apply.ForeColor = _color_ON;
                lab_interlock_apply.ForeColor = _color_ON;
            }));
            while (!_bool_Need_lastProcessName_Request)
            {
                Thread.Sleep(500);
            }
            this.Invoke(new Action(() =>
            {
                lab_manufacturePermission_apply.ForeColor = _color_OFF;
                lab_manufactureDeny_apply.ForeColor = _color_OFF;
                lab_interlock_apply.ForeColor = _color_OFF;
            }));
            return _lastProcessName;
        }

        private bool _bool_SaveInformationToMES_Result_Request = false;
        private bool _bool_SaveInformationToMES_Result = false;
        /// <summary>
        /// 保存加工结果是否成功,传出1的string为SN码,传出2的string为加工结果
        /// </summary>
        private bool SaveInformationToMES_Result_Request(string SN, string manufactureResult)
        {
            _bool_SaveInformationToMES_Result_Request = false;
            this.Invoke(new Action(() =>
            {
                lab_manufactureResultRecept_apply.ForeColor = _color_ON;
            }));
            while (!_bool_SaveInformationToMES_Result_Request)
            {
                Thread.Sleep(500);
            }
            this.Invoke(new Action(() =>
            {
                lab_manufactureResultRecept_apply.ForeColor = _color_OFF;
            }));
            return _bool_SaveInformationToMES_Result;
        }




        private void MessageOutput(string s)
        {
            ShowMessage(s);
        }



        private void ShowMessage(string s)
        {
            this.Invoke(new Action(() =>
            {

                txt_showMessage.AppendText(string.Format("{0}：{1}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), s));

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

        private void btn_ForceManufacturePermission_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("强制【允许加工】会忽略产品的上一道工序执行情况，互锁功能会在本次失效，确认执行码？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (_businessMain.LastProcessNameCheck("unknown", true))
                { ShowMessage("强制【允许加工】成功"); }
                else
                { ShowMessage("强制【允许加工】失败"); }
            }
        }

        private void btn_forceResultOK_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("强制【结果收到】会忽略产品信息是否保存，直接确认信息已保存完成，确认执行码？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (_businessMain.ManufactureResultWriteToPLC("unknown", true, true))
                { ShowMessage("强制【结果收到】成功"); }
                else
                { ShowMessage("强制【结果收到】失败"); }
            }
        }

        private void btn_sn_set_Click(object sender, EventArgs e)
        {
            _SN_Number = txt_SN.Text;
            _bool_need_SN_Request = true;
        }

        private void btn_set_lastProcessName_Click(object sender, EventArgs e)
        {
            _lastProcessName = txt_lastProcessName.Text;
            _bool_Need_lastProcessName_Request = true;
        }

        private void btn_saveReult_Click(object sender, EventArgs e)
        {
            _bool_SaveInformationToMES_Result_Request = true;
            _bool_SaveInformationToMES_Result = true;
        }
    }
}
