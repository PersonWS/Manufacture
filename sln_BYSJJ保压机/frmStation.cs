using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snap7;
namespace sln_BYSJJ
{
    public partial class frmStation : Form
    {
        Color clrTrue = Color.Green;
        Color clrFalse = Color.White;
        public frmStation()
        {
            InitializeComponent();
        }

        private void frmStation_Load(object sender, EventArgs e)
        {

        }

        private void frmStation_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void frmStation_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void timUI_Tick(object sender, EventArgs e)
        {
            lbl_BY_SFHYW.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferGWZT, 0, 0) ? clrTrue : clrFalse;
            lbl_BY_SFYSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferGWZT, 0, 1) ? clrTrue : clrFalse;
            lbl_BY_YSCKD.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferGWZT, 0, 2) ? clrTrue : clrFalse;
            lbl_BY_WBJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferGWZT, 0, 3) ? clrTrue : clrFalse;
            lbl_BY_SFZT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferGWZT, 0, 4) ? clrTrue : clrFalse;
            lbl_BY_DWWC.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferGWZT, 0, 5) ? clrTrue : clrFalse;
            lbl_BY_XRWC.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferGWZT, 0, 6) ? clrTrue : clrFalse;
            lbl_BY_ZTMS.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferGWZT, 0, 7) ? clrTrue : clrFalse;
            lbl_BY_ZDMS.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferGWZT, 1, 0) ? clrTrue : clrFalse;
        }
    }
}
