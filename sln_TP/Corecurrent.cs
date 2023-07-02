using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
using System.Configuration;
namespace sln_TP
{
    public class Corecurrent
    {
        public struct SysRunStatus
        {
            public enum Procedure
            {
                PROCEDURE_UNKNOWN,     //未知进程
                PROCEDURE_IDLE,        //等待进程状态
                PROCEDURE_PROCESSYN,   //是否加工询问
                PROCEDURE_ASKMES,      //询问MES过程
                PROCEDURE_JGWAIT,      //等待加工过程
                PROCEDURE_MESNG,       //MES通知NG状态过程
                PROCEDURE_SENDMES,     //结果给MES过程
                PROCEDURE_END,         //流程结束初始化所有变量
            }
        }

        //报警列表
        public DataTable dtAlarm;
        //工位状态
        public byte[] BufferGWZT = new byte[3];
        //报警信息
        public byte[] BufferBJ = new byte[9];
        //后壳SN码
        public string strHSN = "";
        //主SN码
        public string strZSN = "";
        //---------------------工位参数start--------------------------
        //贴屏中转X偏移位置
        public string strTPZZX = "";
        //贴屏中转Y偏移位置
        public string strTPZZY = "";
        //贴屏中转R偏移位置
        public string strTPZZR = "";
        //贴屏X偏移位置
        public string strTPX = "";
        //贴屏Y偏移位置
        public string strTPY = "";
        //贴屏R偏移位置
        public string strTPR = "";
        //伺服实际位置
        public string strSFWZ = "";
        //故障代码
        public string strGZDM = "";
        //报警代码
        public string strBJDM = "";
        //RFID信号强度
        public string strXHQD = "";
        //RFID通讯状态
        public string strTXZT = "";
        //当前配方
        public string strDQPF = "";
        //---------------------工位参数end----------------------------
        //存在报警
        public bool IsAlarm = false;
        //报警弹窗是否打开
        public bool IsShowAlarm = false;
        //连接到PLC状态
        public bool IsConnnectPlc = false;
        //需要写入PLC时候开关
        public bool IsWritePlc = false;
        //是否从PLC接收请求开关
        public bool bolRecivePLC = false;
        //是否从MES接收数据开关
        public bool bolReciveMes = false;
        //从MES接收允许加工
        public bool bolReciveMes_RY = false;
        //从MES接收禁止加工
        public bool bolReciveMes_RN = false;
        //人工触发加工状态允许加工_上位机写入PLC
        public bool bolJGY = false;
        //人工触发加工状态禁止加工_上位机写入PLC
        public bool bolJGN = false;
        //给定加工完成信号_上位机写入PLC
        public bool bolJGW = false;
        //等待加工结果状态开关
        public bool bolWaitJGResult = false;
        //加工结果已经出来
        public bool bolGetJGResult = false;
        //加工结果OK
        public bool bolResultOK = false;
        //加工结果NG
        public bool bolResultNG = false;
        //是否加工label
        public bool bolLblSFJG = false;
        //结果OKlabel
        public bool bolLblJGOK = false;
        //结果NGlabel
        public bool bolLblJGNG = false;

        //允许加工label
        public bool bolLblYXJG = false;
        //禁止加工label
        public bool bolLblJZJG = false;
        //接收完成label
        public bool bolLblJSWC = false;
        // 刷新警告
        public bool bolTableRe = false;
        // 互锁线程开启
        public bool bolHSXC = false;
        // 互锁请求开启
        public bool bolHSQQ = false;
        // 互锁结果开启
        public bool bolHSJG = false;
        // 互锁已请求和结果
        public string strHSQQJG = "";
        // 显示结果
        public bool bolShowJg = false;
        // 显示结果开始计时时间
        public DateTime dtTime = DateTime.Now;

        public SysRunStatus.Procedure NowProcedure;
        public Dictionary<string, string> mapAlarm = new Dictionary<string, string>();
        public Dictionary<string, string> mapInfo = new Dictionary<string, string>();
        public Dictionary<string, string> mapMake = new Dictionary<string, string>();
        public Queue<KeyValuePair<SysRunStatus.Procedure, string>> procedure_status_queue;
        public static Corecurrent corecurrent = new Corecurrent();
        public static Corecurrent GetCorecurrent()
        {
            //corecurrent.procedure_status_queue.Dequeue();
            return corecurrent;
        }
        /// <summary>
        /// 数据库路径   app.config connectionStrings
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string LoadAppConfigString(string id = "sqlDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
