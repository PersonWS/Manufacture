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
    public partial class frmAlarm : Form
    {
        Color clrTrue = Color.Green;
        Color clrFalse = Color.White;
        public frmAlarm()
        {
            InitializeComponent();
        }

        private void frmAlarm_Load(object sender, EventArgs e)
        {

        }

        private void frmAlarm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void frmAlarm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timUI.Enabled = false;
            this.Hide();
            e.Cancel = true;
        }

        private void timUI_Tick(object sender, EventArgs e)
        {
            //传输报警
            //RFID通讯异常
            lbl_BYCS_TXYC.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 0) ? clrTrue : clrFalse;
            //RFID信号强度弱
            lbl_BYCS_XHQDR.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 1) ? clrTrue : clrFalse;
            //上层进料异常
            lbl_BYCS_SCJL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 2) ? clrTrue : clrFalse;
            //上层出料异常
            lbl_BYCS_SCCL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 3) ? clrTrue : clrFalse;
            //下层进料异常
            lbl_BYCS_XCJL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 4) ? clrTrue : clrFalse;
            //下层出料异常
            lbl_BYCS_XCCL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 5) ? clrTrue : clrFalse;
            //上层保护光电异常
            lbl_BYCS_SCBH.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 6) ? clrTrue : clrFalse;
            //下层保护光电异常
            lbl_BYCS_XCBH.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 7) ? clrTrue : clrFalse;
            //升降抬起异常
            lbl_BYCS_SJTQ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 1, 0) ? clrTrue : clrFalse;
            //升降落下异常
            lbl_BYCS_SJLX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 1, 1) ? clrTrue : clrFalse;
            //RFID无数据
            lbl_BYCS_WSJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 1, 2) ? clrTrue : clrFalse;
            
            //常规报警
            //面板急停报警
            lbl_BYCG_MBJT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 2, 0) ? clrTrue : clrFalse;
            //安全门打开报警
            lbl_BYCG_AQMDK.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 2, 1) ? clrTrue : clrFalse;
            //与贴屏对接心跳异常
            lbl_BYCG_YTPDJXT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 2, 2) ? clrTrue : clrFalse;
            //双工位螺丝机无配方
            lbl_BYCG_SGNOPF.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 2, 3) ? clrTrue : clrFalse;

            //伺服报警
            //伺服暂停
            lbl_BYSF_ZT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 0) ? clrTrue : clrFalse;
            //伺服未使能
            lbl_BYSF_WSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 1) ? clrTrue : clrFalse;
            //伺服上极限
            lbl_BYSF_SJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 2) ? clrTrue : clrFalse;
            //伺服下极限
            lbl_BYSF_XJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 3) ? clrTrue : clrFalse;
            //伺服无倍率
            lbl_BYSF_WBL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 4) ? clrTrue : clrFalse;
            //伺服上软极限
            lbl_BYSF_SRJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 5) ? clrTrue : clrFalse;
            //伺服下软极限
            lbl_BYSF_XRJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 6) ? clrTrue : clrFalse;
        }
    }
}
