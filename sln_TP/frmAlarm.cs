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
            lbl_CS_TXYC.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 0) ? clrTrue : clrFalse;
            //RFID信号强度弱
            lbl_CS_XHQDR.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 1) ? clrTrue : clrFalse;
            //上层进料异常
            lbl_CS_SCJL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 2) ? clrTrue : clrFalse;
            //上层出料异常
            lbl_CS_SCCL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 3) ? clrTrue : clrFalse;
            //下层进料异常
            lbl_CS_XCJL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 4) ? clrTrue : clrFalse;
            //下层出料异常
            lbl_CS_XCCL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 5) ? clrTrue : clrFalse;
            //上层保护光电异常
            lbl_CS_SCBH.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 6) ? clrTrue : clrFalse;
            //下层保护光电异常
            lbl_CS_XCBH.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 7) ? clrTrue : clrFalse;
            //升降抬起异常
            lbl_CS_SJTQ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 1, 0) ? clrTrue : clrFalse;
            //升降落下异常
            lbl_CS_SJLX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 1, 1) ? clrTrue : clrFalse;
            //NG标志
            lbl_CS_NGBZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 1, 2) ? clrTrue : clrFalse;
            //中转台无SN码
            lbl_CS_NOSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 1, 3) ? clrTrue : clrFalse;

            //常规报警
            //面板急停报警
            lbl_CG_MBJT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 0) ? clrTrue : clrFalse;
            //安全门打开报警
            lbl_CG_AQMDK.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 1) ? clrTrue : clrFalse;
            //按钮盒急停报警
            lbl_CG_ANHJT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 2) ? clrTrue : clrFalse;
            //搬运示教器急停报警
            lbl_CG_BYSJQJT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 3) ? clrTrue : clrFalse;
            //贴屏示教器急停报警
            lbl_CG_TPSJQJT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 4) ? clrTrue : clrFalse;
            //吸盘气源压力报警
            lbl_CG_XPQY.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 5) ? clrTrue : clrFalse;
            //传输气源压力报警
            lbl_CG_CSQY.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 6) ? clrTrue : clrFalse;
            //相机不在运行中
            lbl_CG_XJNORUN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 7) ? clrTrue : clrFalse;
            //配方与相机程序不符
            lbl_CG_PFXJBF.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 3, 0) ? clrTrue : clrFalse;
            //无双单吸盘选择
            lbl_CG_WSDXZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 3, 1) ? clrTrue : clrFalse;
            //双工位螺丝机无配方
            lbl_CG_SGNOPF.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 3, 2) ? clrTrue : clrFalse;

            //伺服报警
            //伺服暂停
            lbl_SF_ZT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 4, 0) ? clrTrue : clrFalse;
            //伺服未使能
            lbl_SF_WSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 4, 1) ? clrTrue : clrFalse;
            //伺服正转软极限
            lbl_SF_ZZRJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 4, 2) ? clrTrue : clrFalse;
            //伺服反转软极限
            lbl_SF_FZRJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 4, 3) ? clrTrue : clrFalse;

            //搬运机器人报警
            //示教器未自动
            lbl_BY_SJQWZD.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 0) ? clrTrue : clrFalse;
            //机器人报警
            lbl_BY_BJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 1) ? clrTrue : clrFalse;
            //机器人未使能
            lbl_BY_WSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 2) ? clrTrue : clrFalse;
            //机器人暂停
            lbl_BY_ZT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 3) ? clrTrue : clrFalse;
            //搬运吸盘气压不足
            lbl_BY_BYQYBZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 4) ? clrTrue : clrFalse;
            //翻转吸盘气压不足
            lbl_BY_FZQYBZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 5) ? clrTrue : clrFalse;
            //吸盘有料，无法回原位
            lbl_BY_WFHYW.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 6) ? clrTrue : clrFalse;
            //机器人在干涉区
            lbl_BY_GSQ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 7) ? clrTrue : clrFalse;
            //翻转平台有料
            lbl_BY_FZPTYL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 7, 0) ? clrTrue : clrFalse;
            //搬运在干涉区，翻转平台禁止动作
            lbl_BY_FZJZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 7, 1) ? clrTrue : clrFalse;
            //双工位无SN码
            lbl_BY_SGWSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 7, 2) ? clrTrue : clrFalse;
            //螺丝机停止
            lbl_BY_LSJTZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 7, 3) ? clrTrue : clrFalse;

            //贴屏机器人报警
            //示教器未自动
            lbl_TP_SJQWZD.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 0) ? clrTrue : clrFalse;
            //机器人报警
            lbl_TP_BJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 1) ? clrTrue : clrFalse;
            //机器人未使能
            lbl_TP_WSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 2) ? clrTrue : clrFalse;
            //机器人暂停
            lbl_TP_ZT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 3) ? clrTrue : clrFalse;
            //贴屏吸盘气压不足
            lbl_TP_QYBZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 4) ? clrTrue : clrFalse;
            //吸盘有料，无法回原位
            lbl_TP_WFHYW.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 5) ? clrTrue : clrFalse;
            //机器人在干涉区
            lbl_TP_GSQ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 6) ? clrTrue : clrFalse;
            //拍照NG
            lbl_TP_PZNG.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 7) ? clrTrue : clrFalse;
        }
    }
}
