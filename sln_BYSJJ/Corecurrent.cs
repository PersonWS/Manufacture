using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
namespace sln_BYSJJ
{
    public class Corecurrent
    {
        #region 保压设备
        public struct BY_SysRunStatus
        {
            public enum BY_Procedure
            {
                BY_PROCEDURE_UNKNOWN,     //未知进程
                BY_PROCEDURE_IDLE,        //等待进程状态
                BY_PROCEDURE_PROCESSYN,   //是否加工询问
                BY_PROCEDURE_ASKMES,      //询问MES过程
                BY_PROCEDURE_JGWAIT,      //等待加工过程
                BY_PROCEDURE_MESNG,       //MES通知NG状态过程
                BY_PROCEDURE_SENDMES,     //结果给MES过程
                BY_PROCEDURE_END,         //流程结束初始化所有变量
            }
        }
        //保压设备当前流程
        public BY_SysRunStatus.BY_Procedure BY_NowProcedure;
        //连接到保压PLC状态
        public bool BY_IsConnnectPlc = false;

        //保压存在报警
        public bool BY_IsAlarm = false;
        //保压报警弹窗是否打开
        public bool BY_IsShowAlarm = false;
        //保压报警列表
        public DataTable BY_dtAlarm;
        //保压报警信息
        public byte[] BY_BufferBJ = new byte[5];
        //保压工位状态
        public byte[] BY_BufferGWZT = new byte[2];
        //保压主SN码
        public string BY_strZSN = "";
        //---------------------保压工位参数start--------------------------
        //实际压力值
        public string BY_strSJYLZ = "";
        //伺服故障代码
        public string BY_strSFGZDM = "";
        //伺服报警代码
        public string BY_strSFBJDM = "";
        //伺服实际位置
        public string BY_strSFSJWZ = "";
        //伺服当前倍率
        public string BY_strSFDQBL = "";
        //RFID信号强度
        public string BY_strXHQD = "";
        //RFID通讯状态
        public string BY_strTXZT = "";
        //当前配方显示
        public string BY_strDQPF = "";
        //---------------------保压工位参数end------------------------------
        //需要写入保压PLC时候开关
        public bool BY_IsWritePlc = false;
        //是否从保压PLC接收请求开关
        public bool BY_bolRecivePLC = false;
        //是否保压从MES接收数据开关
        public bool BY_bolReciveMES = false;
        //保压从MES接收允许加工
        public bool BY_bolReciveMes_RY = false;
        //保压从MES接收禁止加工
        public bool BY_bolReciveMes_RN = false;
        //人工触发加工状态允许加工_上位机写入保压PLC
        public bool BY_bolJGY = false;
        //人工触发加工状态禁止加工_上位机写入保压PLC
        public bool BY_bolJGN = false;
        //给定加工完成信号_上位机写入保压PLC
        public bool BY_bolJGW = false;
        //等待加工结果状态开关
        public bool BY_bolWaitJGResult = false;
        //加工结果已经出来
        public bool BY_bolGetJGResult = false;
        //加工结果OK
        public bool BY_bolResultOK = false;
        //加工结果NG
        public bool BY_bolResultNG = false;
        //是否加工label
        public bool BY_bolLblSFJG = false;
        //结果OKlabel
        public bool BY_bolLblJGOK = false;
        //结果NGlabel
        public bool BY_bolLblJGNG = false;
        //允许加工label
        public bool BY_bolLblYXJG = false;
        //禁止加工label
        public bool BY_bolLblJZJG = false;
        //接收完成label
        public bool BY_bolLblJSWC = false;
        // 刷新警告
        public bool BY_bolTableRe = false;
        // 互锁线程开启
        public bool BY_bolHSXC = false;
        // 互锁请求开启
        public bool BY_bolHSQQ = false;
        // 互锁结果开启
        public bool BY_bolHSJG = false;
        // 互锁已请求和结果
        public string BY_strHSQQJG = "";
        // 显示结果
        public bool BY_bolShowJg = false;
        // 显示结果开始计时时间
        public DateTime BY_dtTime = DateTime.Now;
        //保压报警列表词典
        public Dictionary<string, string> BY_mapAlarm = new Dictionary<string, string>();
        #endregion

        #region 升降机设备
        //首升降机
        public struct SSJJ_SysRunStatus
        {
            public enum SSJJ_Procedure
            {
                SSJJ_PROCEDURE_UNKNOWN,     //未知进程
                SSJJ_PROCEDURE_IDLE,        //等待进程状态
                SSJJ_PROCEDURE_GETQQ,       //接到请求
                SSJJ_PROCEDURE_ASKMES,      //询问MES过程
                SSJJ_PROCEDURE_JGWAIT,      //等待加工过程
                SSJJ_PROCEDURE_MESNG,       //MES通知NG状态过程
                SSJJ_PROCEDURE_SENDMES,     //结果给MES过程
                SSJJ_PROCEDURE_END,         //流程结束初始化所有变量
            }
        }

        //尾升降机
        public struct WSJJ_SysRunStatus
        {
            public enum WSJJ_Procedure
            {
                WSJJ_PROCEDURE_UNKNOWN,     //未知进程
                WSJJ_PROCEDURE_IDLE,        //等待进程状态
                WSJJ_PROCEDURE_GETQQ,       //接到请求
                WSJJ_PROCEDURE_DB,          //打标流程
                WSJJ_PROCEDURE_JGWAIT,      //等待加工过程
                WSJJ_PROCEDURE_MESNG,       //MES通知NG状态过程
                WSJJ_PROCEDURE_SENDMES,     //结果给MES过程
                WSJJ_PROCEDURE_END,         //流程结束初始化所有变量
            }
        }
        //首升降机设备当前流程
        public SSJJ_SysRunStatus.SSJJ_Procedure SSJJ_NowProcedure;
        //尾升降机设备当前流程
        public WSJJ_SysRunStatus.WSJJ_Procedure WSJJ_NowProcedure;
        //连接到升降机PLC状态
        public bool SJJ_IsConnnectPlc = false;
        //需要写入升降机PLC时候开关
        public bool SJJ_IsWritePlc = false;
        //首升降机接收
        public bool SSJJ_bolSJS = false;
        //尾升降机接收
        public bool WSJJ_bolSJS = false;
        //尾升降机打标已发
        public bool WSJJ_bolDBF = false;
        //升降机写入标志
        public string SJJ_IsWriteFlag = "";
        //升降机扫描SN码
        public string SJJ_strSCANSN = "";
        //升降机接收SN码
        public string SJJ_strPLCSN = "";
        //升降机发送SN码
        public string SJJ_strGKJSN = "";
        //升降机请求发送
        public bool SJJ_bolLblQQFS = false;
        
        //升降机PLC->工控机写入完成
        public bool SJJ_bolLblPGXRWC = false;
        //升降机工控机->PLC写入完成
        public bool SJJ_bolLblGPXRWC = false;
        //打标完成
        public bool WSJJ_bolLblDBWC = false;
        //打标失败
        public bool WSJJ_bolLblDBSB = false;
        #endregion



        #region 通用变量
        public Dictionary<string, string> mapInfo = new Dictionary<string, string>();
        public Dictionary<string, string> mapMake = new Dictionary<string, string>();
        public Queue<KeyValuePair<BY_SysRunStatus.BY_Procedure, string>> procedure_status_queue;
        public static Corecurrent corecurrent = new Corecurrent();
        public static Corecurrent GetCorecurrent()
        {
            //corecurrent.procedure_status_queue.Dequeue();
            return corecurrent;
        }
        #endregion









    }
}
