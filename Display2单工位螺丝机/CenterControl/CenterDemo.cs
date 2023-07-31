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
            //DisableButton();
        }
        ~CenterDemo()
        {
            if (_businessMain!=null)
            {
                _businessMain.Need_SN_Request -= Need_SN_Request;
                _businessMain.Need_lastProcessName_Request -= Need_lastProcessName_Request;
                _businessMain.SaveInformationToMES_Result_Request -= SaveInformationToMES_Result_Request;
                _businessMain.Need_ClearScrewData += Need_ClearScrewData;
                _businessMain.MessageOutput -= MessageOutput;

            }
        }


            

        private void button1_Click(object sender, EventArgs e)
        {
            _businessMain = BusinessMain.GetInstance();
            lab_lastProcessName.Text = BusinessMain._lastProcessName;


            _businessMain.MessageOutput += MessageOutput;
            //订阅关键事件
            if (chk_subscribe.Checked)
            {
                _businessMain.Need_SN_Request -= Need_SN_Request;
                _businessMain.Need_SN_Request += Need_SN_Request;
                _businessMain.Need_lastProcessName_Request -= Need_lastProcessName_Request;
                _businessMain.Need_lastProcessName_Request += Need_lastProcessName_Request;
                _businessMain.SaveInformationToMES_Result_Request -= SaveInformationToMES_Result_Request;
                _businessMain.SaveInformationToMES_Result_Request += SaveInformationToMES_Result_Request;
            }

            _isMonitor = true;
            ShowMessage("准备启动主服务...");

            if (_businessMain.BusinessStart())
            {
                ShowMessage("主服务成功启动");

                _thread_showPlcInfo = new Thread(ShowPLC_PointState);
                _thread_showPlcInfo.IsBackground = true;
                _thread_showPlcInfo.Start();
                EnableButton();
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

        private bool _bool_Need_ClearScrewData = false;
        private bool Need_ClearScrewData()
        {
            ShowMessage("收到清理电批数据请求");
            _bool_Need_ClearScrewData = false;
            this.Invoke(new Action(() => { lab_ScrewClearOK_apply.ForeColor = _color_ON; }));
            while (!_bool_Need_ClearScrewData)
            {
                Thread.Sleep(500);
            }
            this.Invoke(new Action(() => { lab_ScrewClearOK_apply.ForeColor = _color_OFF; }));
            ShowMessage("电批数据清理完成");
            return true;
        }

        private bool _bool_need_SN_Request = false;
        private string _SN_Number = "";
        //外部申请的回调函数
        //向PLC输入SN
        private string Need_SN_Request()
        {
            ShowMessage("收到SN码写入请求，请输入SN码并确认");
            _bool_need_SN_Request = false;
            this.Invoke(new Action(() => { lab_snWrite_apply.ForeColor = _color_ON; }));
            while (!_bool_need_SN_Request)
            {
                Thread.Sleep(500);
            }
            this.Invoke(new Action(() => { lab_snWrite_apply.ForeColor = _color_OFF; }));
            ShowMessage("SN码输入完成");
            return _SN_Number;

        }

        private bool _bool_Need_lastProcessName_Request = false;
        private string _lastProcessName = "";
        /// 获取上一工序名称 传出的string为SN码
        private string Need_lastProcessName_Request(string SN)
        {
            ShowMessage("收到上一工序校验及互锁请求，请输入上一工序号并确认");
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
            ShowMessage("上一工序输入完成");
            return _lastProcessName;
        }

        private bool _bool_SaveInformationToMES_Result_Request = false;
        private bool _bool_SaveInformationToMES_Result = false;
        /// <summary>
        /// 保存加工结果是否成功,传出1的string为SN码,传出2的string为加工结果
        /// </summary>
        private bool SaveInformationToMES_Result_Request(string SN, string manufactureResult)
        {
            ShowMessage("收到加工完成，保存加工结果请求，请保存并确认");
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
            ShowMessage("保存加工信息完成");
            return _bool_SaveInformationToMES_Result;
        }




        private void MessageOutput(string s)
        {
            ShowMessage(s);
        }



        private void ShowMessage(string s)
        {
            try
            {
                this.Invoke(new Action(() =>
                {

                    txt_showMessage.AppendText(string.Format("{0}：{1}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), s));

                    txt_showMessage.ScrollToCaret();
                }));
            }
            catch (Exception)
            {
            }

        }

        private void btn_StopCenterControl_Click(object sender, EventArgs e)
        {
            _isMonitor = false;
            _businessMain.BusinessStop();
            Thread.Sleep(500);
            ShowMessage("主服务已停止");
            DisableButton();
            //刷新label显示
            foreach (var item in this.Controls)
            {
                if (item.GetType() == typeof(Label))
                {
                    if (((Label)item).Text == "●")
                    {
                        ((Label)item).ForeColor = Color.Red;
                    }
                }
                else if (item.GetType() == typeof(GroupBox))
                {
                    foreach (var item2 in ((GroupBox)item).Controls)
                    {
                        if (item2.GetType() == typeof(Label))
                        {
                            if (((Label)item2).Text == "●")
                            {
                                ((Label)item2).ForeColor = _color_OFF;
                            }
                        }
                    }
                }
            }

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("强制【写入SN】会忽略产品的上一道工序执行情况，确认执行码？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (_businessMain.WriteSN_ToPLC(txt_SN.Text))
                { _SN_Number = txt_SN.Text; ShowMessage("强制【写入SN】成功"); }
                else
                { ShowMessage("强制【写入SN】失败"); }
            }
        }

        private void DisableButton()
        {
            this.Invoke(new Action(() =>{
                btn_ForceManufacturePermission.Enabled = false;
                btn_forceResultOK.Enabled = false;
                btn_saveReult.Enabled = false;
                btn_set_lastProcessName.Enabled = false;
                btn_SNForceWrite.Enabled = false;
                btn_StartCenterControl.Enabled = false;
                btn_sn_set.Enabled = false;
                btn_StopCenterControl.Enabled = false;
                btn_ClearScrew.Enabled = false;
                btn_StartCenterControl.Enabled = true;
            }));

        }

        private void EnableButton()
        {
            this.Invoke(new Action(() => {
                btn_ForceManufacturePermission.Enabled = true;
                btn_forceResultOK.Enabled = true;
                btn_saveReult.Enabled = true;
                btn_set_lastProcessName.Enabled = true;
                btn_SNForceWrite.Enabled = true;
                btn_StartCenterControl.Enabled = true;
                btn_sn_set.Enabled = true;
                btn_StopCenterControl.Enabled = true;
                btn_ClearScrew.Enabled = true;
                btn_StartCenterControl.Enabled = false;
            }));

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            _bool_Need_ClearScrewData = true;
        }

        private void btn_clearScrewForce_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("强制【表格清空】会清理掉电批显示的数据，确认执行码？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (_businessMain.ClearScrewTableData())
                { ShowMessage("强制【表格清空】成功"); }
                else
                { ShowMessage("强制【表格清空】失败"); }
            }
        }

        private void btn_resetAllData_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("强制【清空所有数据】会使DB2002所有的点位数据置位为0，确认执行码？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (_businessMain.ClearPointData_Reset0())
                { ShowMessage("【重置DB2002所有状 态数据[置位为0]】成功"); }
                else
                { ShowMessage("【重置DB2002所有状 态数据[置位为0]】失败"); }
            }
        }

        private void btn_scanner_Click(object sender, EventArgs e)
        {
            Frm_GetSN f = new Frm_GetSN();
            f.ShowDialog();
        }
    }
}
