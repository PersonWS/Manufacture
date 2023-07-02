using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
namespace sln_TJSJJ
{
    public class Corecurrent
    {
        #region 涂胶设备
        public struct TJ_SysRunStatus
        {
            public enum TJ_Procedure
            {
                TJ_PROCEDURE_UNKNOWN,     //未知进程
                TJ_PROCEDURE_IDLE,        //等待进程状态
                TJ_PROCEDURE_PROCESSYN,   //是否加工询问
                TJ_PROCEDURE_ASKMES,      //询问MES过程
                TJ_PROCEDURE_JGWAIT,      //等待加工过程
                TJ_PROCEDURE_MESNG,       //MES通知NG状态过程
                TJ_PROCEDURE_SENDMES,     //结果给MES过程
                TJ_PROCEDURE_END,         //流程结束初始化所有变量
            }
        }
        //涂胶设备当前流程
        public TJ_SysRunStatus.TJ_Procedure TJ_NowProcedure;
        //连接到涂胶PLC状态
        public bool TJ_IsConnnectPlc = false;

        //涂胶存在报警
        public bool TJ_IsAlarm = false;
        //涂胶报警弹窗是否打开
        public bool TJ_IsShowAlarm = false;
        //涂胶报警列表
        public DataTable TJ_dtAlarm;
        //涂胶报警信息
        public byte[] TJ_BufferBJ = new byte[6];
        //涂胶工位状态
        public byte[] TJ_BufferGWZT = new byte[1];
        //涂胶后壳SN码
        public string TJ_strHSN = "";
        //---------------------涂胶工位参数start--------------------------
        //实际压力值
        public string TJ_strSJYLZ = "";
        //伺服故障代码
        public string TJ_strSFGZDM = "";
        //伺服报警代码
        public string TJ_strSFBJDM = "";
        //伺服实际位置
        public string TJ_strSFSJWZ = "";
        //伺服当前倍率
        public string TJ_strSFDQBL = "";
        //RFID信号强度
        public string TJ_strXHQD = "";
        //RFID通讯状态
        public string TJ_strTXZT = "";
        //当前配方显示
        public string TJ_strDQPF = "";
        //---------------------涂胶工位参数end------------------------------
        //需要写入涂胶PLC时候开关
        public bool TJ_IsWritePlc = false;
        //是否从涂胶PLC接收请求开关
        public bool TJ_bolRecivePLC = false;
        //是否涂胶从MES接收数据开关
        public bool TJ_bolReciveMES = false;
        //涂胶从MES接收允许加工
        public bool TJ_bolReciveMes_RY = false;
        //涂胶从MES接收禁止加工
        public bool TJ_bolReciveMes_RN = false;
        //人工触发加工状态允许加工_上位机写入涂胶PLC
        public bool TJ_bolJGY = false;
        //人工触发加工状态禁止加工_上位机写入涂胶PLC
        public bool TJ_bolJGN = false;
        //给定加工完成信号_上位机写入涂胶PLC
        public bool TJ_bolJGW = false;
        //等待加工结果状态开关
        public bool TJ_bolWaitJGResult = false;
        //加工结果已经出来
        public bool TJ_bolGetJGResult = false;
        //加工结果OK
        public bool TJ_bolResultOK = false;
        //加工结果NG
        public bool TJ_bolResultNG = false;
        //是否加工label
        public bool TJ_bolLblSFJG = false;
        //结果OKlabel
        public bool TJ_bolLblJGOK = false;
        //结果NGlabel
        public bool TJ_bolLblJGNG = false;
        //允许加工label
        public bool TJ_bolLblYXJG = false;
        //禁止加工label
        public bool TJ_bolLblJZJG = false;
        //接收完成label
        public bool TJ_bolLblJSWC = false;
        // 刷新警告
        public bool TJ_bolTableRe = false;
        // 互锁线程开启
        public bool TJ_bolHSXC = false;
        // 互锁请求开启
        public bool TJ_bolHSQQ = false;
        // 互锁结果开启
        public bool TJ_bolHSJG = false;
        // 互锁已请求和结果
        public string TJ_strHSQQJG = "";
        // 显示结果
        public bool TJ_bolShowJg = false;
        // 显示结果开始计时时间
        public DateTime TJ_dtTime = DateTime.Now;
        //涂胶报警列表词典
        public Dictionary<string, string> TJ_mapAlarm = new Dictionary<string, string>();
        #endregion

        #region 首升降机设备
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
        //首升降机设备当前流程
        public SSJJ_SysRunStatus.SSJJ_Procedure SSJJ_NowProcedure;
        //连接到升降机PLC状态
        public bool SJJ_IsConnnectPlc = false;
        //需要写入升降机PLC时候开关
        public bool SJJ_IsWritePlc = false;
        //首升降机接收
        public bool SJJ_bolSJS = false;
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
        //升降机扫码完成
        public bool SJJ_bolSCANWC = false;
        #endregion



        #region 通用变量
        public Dictionary<string, string> mapInfo = new Dictionary<string, string>();
        public Dictionary<string, string> mapMake = new Dictionary<string, string>();
        public Queue<KeyValuePair<TJ_SysRunStatus.TJ_Procedure, string>> procedure_status_queue;
        public static Corecurrent corecurrent = new Corecurrent();
        public static Corecurrent GetCorecurrent()
        {
            //corecurrent.procedure_status_queue.Dequeue();
            return corecurrent;
        }
        #endregion









    }
}
