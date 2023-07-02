using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormHostInfo : Form
    {
        public FormHostInfo()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msgButton = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("确定要修改本机信息吗？", "系统提示", msgButton);
            if (dr == DialogResult.No)
            {
                return;
            }
            //string hostname = textBoxHostName.Text;
            //if (string.IsNullOrEmpty(hostname))
            //{
            //    MessageBox.Show("请输入计算机名！", "系统提示");
            //    return;
            //}
            string devicename = textBoxDeviceName.Text;
            if (string.IsNullOrEmpty(devicename))
            {
                MessageBox.Show("请输入设备名！", "系统提示");
                return;
            }
            //IpTransferTool.SetComputerName(hostname);
            Model.SystemParam s = new Model.SystemParam();
            s.Name=devicename;
            s.Id = 1;
           // Controller.CONT_StationInfo.Update(s);
        }
    }

}
