using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sln_BYSJJ
{
    public partial class frmMsg : Form
    {
        public frmMsg()
        {
            InitializeComponent();
            
        }
        public void SetDataSource()
        {
            if (Corecurrent.GetCorecurrent().BY_dtAlarm.Rows.Count == 0)
            {

            }
            else
            {
                //dataGridView1.DataSource = Corecurrent.GetCorecurrent().BY_dtAlarm;
                dataGridView1.Refresh();
            }
        }

        private void frmMsg_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Corecurrent.GetCorecurrent().BY_IsShowAlarm = false;
        }

        private void frmMsg_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Corecurrent.GetCorecurrent().BY_IsShowAlarm = false;
            e.Cancel = true;
        }

        private void frmMsg_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Corecurrent.GetCorecurrent().BY_dtAlarm;
        }



        private void timUI_Tick(object sender, EventArgs e)
        {
            //dataGridView1.Refresh();
            //dataGridView1.DataSource = Corecurrent.GetCorecurrent().BY_dtAlarm;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
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
    }
}
