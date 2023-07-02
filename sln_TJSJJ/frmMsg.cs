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
    public partial class frmMsg : Form
    {
        public frmMsg()
        {
            InitializeComponent();
            
        }
        public void SetDataSource()
        {
            if (Corecurrent.GetCorecurrent().TJ_dtAlarm.Rows.Count == 0)
            {

            }
            else
            {
                //dataGridView1.DataSource = Corecurrent.GetCorecurrent().TJ_dtAlarm;
                dataGridView1.Refresh();
            }
        }

        private void frmMsg_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Corecurrent.GetCorecurrent().TJ_IsShowAlarm = false;
        }

        private void frmMsg_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Corecurrent.GetCorecurrent().TJ_IsShowAlarm = false;
            e.Cancel = true;
        }

        private void frmMsg_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Corecurrent.GetCorecurrent().TJ_dtAlarm;
        }


        private void timUI_Tick(object sender, EventArgs e)
        {
            //dataGridView1.Refresh();
            //dataGridView1.DataSource = Corecurrent.GetCorecurrent().TJ_dtAlarm;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //if (Corecurrent.GetCorecurrent().TJ_dtAlarm.Rows.Count == 0)
            //{
            //    return;
            //}
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
            e.RowBounds.Location.Y,
                dataGridView1.RowHeadersWidth - 4,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dataGridView1.RowHeadersDefaultCellStyle.Font,
            rectangle,
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void frmMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '\b':
                    if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length > 0)
                    {
                        Corecurrent.GetCorecurrent().SJJ_strSCANSN = "";
                    }
                    break;

                case '\x001b':
                    Corecurrent.GetCorecurrent().SJJ_strSCANSN = null;
                    base.DialogResult = DialogResult.Cancel;
                    base.Close();
                    break;

                default:
                    if (Corecurrent.GetCorecurrent().SJJ_bolSCANWC == true)
                    {
                        Corecurrent.GetCorecurrent().SJJ_strSCANSN = "";
                        Corecurrent.GetCorecurrent().SJJ_bolSCANWC = false;
                    }
                    if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length < 0x1c)
                    {
                        if (e.KeyChar == 13)
                        {
                            Corecurrent.GetCorecurrent().SJJ_bolSCANWC = true;
                        }
                        else
                        {
                            Corecurrent.GetCorecurrent().SJJ_bolSCANWC = false;
                            Corecurrent.GetCorecurrent().SJJ_strSCANSN = Corecurrent.GetCorecurrent().SJJ_strSCANSN + e.KeyChar.ToString();
                        }
                            
                    }
                    if ((Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length > 0) && !this.timUI.Enabled)
                    {
                        this.timUI.Enabled = true;
                    }
                    if (Corecurrent.GetCorecurrent().SJJ_strSCANSN.Length == 0x1c)
                    {

                    }
                    break;
            }
        }
    }
}
