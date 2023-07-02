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
    public partial class FormSystem : Form
    {
        public FormSystem()
        {
            InitializeComponent();
        }
        string title = "系统设置";
        private void FormSystem_Load(object sender, EventArgs e)
        {

        }

      

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
            
        }
       
        private void buttonProductInfo_Click(object sender, EventArgs e)
        {
            this.Text = title + "  [产品管理]";
           // FormProductInfo f = new FormProductInfo();
            FormPartEdit f = new FormPartEdit();
            utility.showForm(panelForm, f);

        }
        

        private void buttonUser_Click(object sender, EventArgs e)
        {
            FormUser f = new FormUser();
            utility.showForm(panelForm, f);
        }

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panelForm, 2, 0, 0, 0);
        }

        private void panelCmd_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panelCmd, 0, 0, 2, 0);
        }

       

        private void buttonMobusRTU_Click(object sender, EventArgs e)
        {
            FormModbusConfig f = new FormModbusConfig();
            utility.showForm(panelForm, f);
        }

       

        private void button7_Click(object sender, EventArgs e)
        {
            //this.Text = title + "  [IO信息-运动控制]";
            //FormIO f = new FormIO();
            //utility.showForm(panelForm, f);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //this.Text = title + "  [IO信息-扩展模块]";
            //FormIO2 f = new FormIO2();
            //utility.showForm(panelForm, f);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = title + "  [互锁模式]";
            FormDSVInterlockingConfig f = new FormDSVInterlockingConfig();
            utility.showForm(panelForm, f);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Text = title + "  [屏蔽设置]";
            FormShieldData f = new FormShieldData();
            utility.showForm(panelForm, f);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Text = title + "  [输入输出节点]";
            FormInputPoints f = new FormInputPoints();
            utility.showForm(panelForm, f);
        }

        private void FormSystem_FormClosing(object sender, FormClosingEventArgs e)
        {
            S7NetPlus.readOutPoint = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Text = title + "  [运动控制节点]";
            FormActionCardPoints f = new FormActionCardPoints();
            utility.showForm(panelForm, f);
        }

       
    }
}
