using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormGetInfo : Form
    {
        public FormGetInfo(string info)
        {
            InitializeComponent();
            textBox1.Text = info;
        }

        private void FormGetInfo_Load(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.CloseForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.SelectAll();//全选文本框种的文本

            this.textBox1.Copy();//把选中的文本复制大剪切板
            utility.ShowMessage("请把剪切板中的信息发送给管理员。");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Clipboard.GetText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Model.SystemParam> system = Controller.CONT_SystemParam.LoadList(" where Name = 'SystemID'");
            system[0].ParamValue = textBox2.Text;
            Controller.CONT_SystemParam.Update(system[0]);
            utility.ShowMessage("系统更新完成，请重新运行。");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
