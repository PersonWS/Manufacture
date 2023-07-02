using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sln_TJSJJ
{
    public partial class frmJg : Form
    {
        public frmJg()
        {
            InitializeComponent();
        }
        public void  setJG(string strJg)
        {
            this.lblJg.Text = strJg;
        }
        private void frmJg_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void frmJg_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.timUI.Enabled = false;
            e.Cancel = true;
        }

        private void timUI_Tick(object sender, EventArgs e)
        {
            if((DateTime.Now - Corecurrent.GetCorecurrent().TJ_dtTime).Seconds<10)
            {
                this.lbltime.Text = (10-(DateTime.Now - Corecurrent.GetCorecurrent().TJ_dtTime).Seconds).ToString() + "秒后自动关闭";
            }
            else
            {
                this.Hide();
                this.timUI.Enabled = false;
            }
        }
    }
}
