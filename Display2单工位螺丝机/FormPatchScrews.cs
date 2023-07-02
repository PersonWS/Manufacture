using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormPatchScrews : Form
    {
        #region 无边框拖动效果
        [DllImport("user32.dll")]//拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void Start_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion
        public FormPatchScrews()
        {
            InitializeComponent();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormPatchScrews_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult d = utility.ShowMessageResponse("如果锁螺丝过程未结束，降影响数据结果！" + Environment.NewLine + "确定要关闭补螺丝过程吗？");
            if (d != DialogResult.Yes)
                return;

            Model.ResultJsonInfo jr = S7NetPlus.WriteDataBool(29, 44, false, 1);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("写入补螺丝状态置0失败，DB{0} DBX{1}.{2}", 29, 44, 1, jr.Message));
                return;
            }
        }

        private void FormPatchScrews_Load(object sender, EventArgs e)
        {
            textBoxSNCode.Text = "";
            
            textBoxSNCode.Focus();

        }

        private void FormPatchScrews_Shown(object sender, EventArgs e)
        {
            Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
            jr = S7NetPlus.WriteDataBool(29, 44, true, 1);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("写入补螺丝状态置1失败，DB{0} DBX{1}.{2}", 29, 44, 1, jr.Message));
                return;
            }
        }

        string[] snstatus = new string[] { "出错数据","再次出错","已删除","已完成"};
        private void label6_Click(object sender, EventArgs e)
        {
            string sn = textBoxSNCode.Text;
            if (string.IsNullOrEmpty(sn))
            {
                utility.ShowMessage("请输入扫描/正确的SN码");
                return;
            }
            string swhere = " where sn='" + sn + "'";
            if (!checkBox1.Checked)
            {
                swhere = swhere + " and flag<2 ";
            }

            List<Model.PatchScrews> patches = Controller.CONT_PatchScrews.LoadList(sn);
            if (patches.Count == 0)
            {
                utility.ShowMessage(string.Format("没有查询到匹配的数据！{0}", sn));
                return;
            }
            else
            {
                for (int i = 0; i < patches.Count; i++)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    string[] row0 = { (i+1).ToString(), patches[i].SN, patches[i].LineID.ToString(),snstatus[ patches[i].Flag], patches[i].Flag.ToString() };
                    dataGridView1.Rows.Add(row0);
                }
                if(patches.Count==1)
                {
                    //SN码的错误行号，发送到DB29.dbw88  （int） 写入
                    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
                    jr = S7NetPlus.ReadOneInt(29, 88);
                    if(!jr.Successed)
                    {
                        utility.ShowMessage(string.Format("SN码错误行号写入失败！SN:{0},行号:{1}", patches[0].SN, patches[0].LineID));
                        return;
                    }
                }
            }
        }

        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            label6.BackColor = Color.Blue;
            label6.ForeColor = Color.White;
        }

        private void label6_MouseUp(object sender, MouseEventArgs e)
        {
            label6.BackColor = SystemColors.Control;
            label6.ForeColor = Color.Black;
        }

        private void labelSend_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];

        }


    }
}
