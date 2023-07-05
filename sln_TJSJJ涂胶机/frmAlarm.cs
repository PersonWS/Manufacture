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
namespace sln_TJSJJ
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
            timUI.Enabled = true;
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
            lbl_TJCS_TXYC.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 0) ? clrTrue : clrFalse;
            //RFID信号强度弱
            lbl_TJCS_XHQDR.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 1) ? clrTrue : clrFalse;
            //上层进料异常
            lbl_TJCS_SCJL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 2) ? clrTrue : clrFalse;
            //上层出料异常
            lbl_TJCS_SCCL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 3) ? clrTrue : clrFalse;
            //下层进料异常
            lbl_TJCS_XCJL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 4) ? clrTrue : clrFalse;
            //下层出料异常
            lbl_TJCS_XCCL.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 5) ? clrTrue : clrFalse;
            //上层保护光电异常
            lbl_TJCS_SCBH.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 6) ? clrTrue : clrFalse;
            //下层保护光电异常
            lbl_TJCS_XCBH.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 7) ? clrTrue : clrFalse;
            //升降抬起异常
            lbl_TJCS_SJTQ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 1, 0) ? clrTrue : clrFalse;
            //升降落下异常
            lbl_TJCS_SJLX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 1, 1) ? clrTrue : clrFalse;
            //NG标志
            lbl_TJCS_NGBZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 1, 2) ? clrTrue : clrFalse;
            
            //常规报警
            //面板急停报警
            lbl_TJCG_MBJT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 0) ? clrTrue : clrFalse;
            //安全门打开报警
            lbl_TJCG_AQMDK.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 1) ? clrTrue : clrFalse;
            //气源压力不足报警
            lbl_TJCG_QYYLBZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 2) ? clrTrue : clrFalse;
            //擦胶机换料提示
            lbl_TJCG_HLTX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 3) ? clrTrue : clrFalse;
            //擦胶机缺料报警
            lbl_TJCG_QLBJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 4) ? clrTrue : clrFalse;
            //工位暂停
            lbl_TJCG_GWZT.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 5) ? clrTrue : clrFalse;
            //换胶提醒
            lbl_TJCG_HJTX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 6) ? clrTrue : clrFalse;
            //双工位螺丝机无配方
            lbl_TJCG_SGNOPF.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 7) ? clrTrue : clrFalse;

            //伺服报警
            //XYZ轴未复位完成
            lbl_TJSF_XYZ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 0) ? clrTrue : clrFalse;
            //X轴未使能
            lbl_TJSF_XWSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 1) ? clrTrue : clrFalse;
            //Y轴未使能
            lbl_TJSF_YWSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 2) ? clrTrue : clrFalse;
            //Z轴未使能
            lbl_TJSF_ZWSN.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 3) ? clrTrue : clrFalse;
            //X轴有报警
            lbl_TJSF_XYBJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 4) ? clrTrue : clrFalse;
            //Y轴有报警
            lbl_TJSF_YYBJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 5) ? clrTrue : clrFalse;
            //Z轴有报警
            lbl_TJSF_ZYBJ.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 6) ? clrTrue : clrFalse;
            //X轴正极限触发
            lbl_TJSF_XZJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 7) ? clrTrue : clrFalse;
            //X轴负极限触发
            lbl_TJSF_XFJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 0) ? clrTrue : clrFalse;
            //Y轴正极限触发
            lbl_TJSF_YZJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 1) ? clrTrue : clrFalse;
            //Y轴负极限触发
            lbl_TJSF_YFJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 2) ? clrTrue : clrFalse;
            //Z轴正极限触发
            lbl_TJSF_ZZJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 3) ? clrTrue : clrFalse;
            //Z轴负极限触发
            lbl_TJSF_ZFJX.BackColor = Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 4) ? clrTrue : clrFalse;
        }
    }
}
