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
        BusinessMain bm;
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
            bm = new BusinessMain();
            bm.BusinessStart();
            _thread_showPlcInfo = new Thread(ShowPLC_PointState);
            _thread_showPlcInfo.IsBackground = true;
            _thread_showPlcInfo.Start();
        }



        private void ShowPLC_PointState()
        {
            foreach (KeyValuePair<string, PLC_Point> item in BusinessNeedPlcPoint.Dic_gatherPLC_Point)
            {
                this.Invoke((new Action(() =>
                {
                    switch (item.Key)
                    {
                        case "SN码请求":
                            if ((bool)item.Value.value)
                            { lab_snRequest.ForeColor = Color.Green; }
                            else
                            { lab_snRequest.ForeColor = Color.Black; }
                            break;
                        case "开始加工请求":
                            if ((bool)item.Value.value)
                            { lab_isManufacture.ForeColor = Color.Green; }
                            else
                            { lab_isManufacture.ForeColor = Color.Black; }
                            break;
                        case "结果NG":
                            if ((bool)item.Value.value)
                            { lab_ng.ForeColor = Color.Green; }
                            else
                            { lab_ng.ForeColor = Color.Black; }
                            break;
                        case "结果OK":
                            if ((bool)item.Value.value)
                            { lab_ok.ForeColor = Color.Green; }
                            else
                            { lab_ok.ForeColor = Color.Black; }
                            break;
                        default:
                            break;
                    }
                })));
            }





        }



        private void ShowMessage(string s)
        {
            this.Invoke(new Action(() =>
            {
                string.Format("{0} \r\n{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), s);
            }));
        }
    }
}
