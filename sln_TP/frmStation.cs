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
namespace sln_TP
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
            lbl_BY_ZDMS.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 0, 0) ? clrTrue : clrFalse;
            lbl_BY_ZT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 0, 1) ? clrTrue : clrFalse;
            lbl_BY_TZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 0, 2) ? clrTrue : clrFalse;
            lbl_BY_SJQJT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 0, 3) ? clrTrue : clrFalse;
            lbl_BY_BJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 0, 4) ? clrTrue : clrFalse;
            lbl_BY_YSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 0, 5) ? clrTrue : clrFalse;
            lbl_BY_YHZCX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 0, 6) ? clrTrue : clrFalse;
            lbl_BY_ZLGSQ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 0, 7) ? clrTrue : clrFalse;

            lbl_BY_FLGSQ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 1, 0) ? clrTrue : clrFalse;
            lbl_BY_FZGSQ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 1, 1) ? clrTrue : clrFalse;
            lbl_TP_ZDMS.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 1, 2) ? clrTrue : clrFalse;
            lbl_TP_ZT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 1, 3) ? clrTrue : clrFalse;
            lbl_TP_TZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 1, 4) ? clrTrue : clrFalse;
            lbl_TP_SJQJT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 1, 5) ? clrTrue : clrFalse;
            lbl_TP_BJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 1, 6) ? clrTrue : clrFalse;
            lbl_TP_YSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 1, 7) ? clrTrue : clrFalse;

            lbl_TP_YHZCX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 2, 0) ? clrTrue : clrFalse;
            lbl_TP_ZLGSQ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 2, 1) ? clrTrue : clrFalse;
            lbl_FZSF_YW.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 2, 2) ? clrTrue : clrFalse;
            lbl_FZSF_YSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 2, 3) ? clrTrue : clrFalse;
            lbl_FZSF_BJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 2, 4) ? clrTrue : clrFalse;
            lbl_FZSF_SJQJT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferGWZT, 2, 5) ? clrTrue : clrFalse;
        }
    }
}
