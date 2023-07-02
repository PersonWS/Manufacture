using System;
using System.Linq;

namespace ScrewMachineManagementSystem
{
    /// <summary>
    /// 丹尼克尔电批控制器，PTC-ASF-0009N-0-H10-015
    /// </summary>
    public class DNKE_DKTCP
    {

        /// <summary>
        /// MID
        /// </summary>
        public enum DNKE_MidInfo
        {
            建立通信连接 = 1,
            断开连接 = 2,
            Pset选择 = 103,
            运行状态 = 201,
            最终拧紧结果 = 202,
            实时曲线数据 = 203,
            电机运行停止使能 = 301,

        }

        /// <summary>
        ///  数据包错误代码
        /// </summary>
        public enum DNKE_ComError
        {
            帧头错误 = 100,
            帧尾错误 = 200,
            数据长度校验错误 = 300,
            数据解析错误 = 400,
            无效MID = 500,
            无效PID = 600,
            无效数据类型 = 700,

        }
        /// <summary>
        /// 运行状态读取的返回值 MID=0201，code=001
        /// </summary>
        public enum MID0201_001
        {
            //以“，”间隔, 以“；”结束
            准备运行 = 1,//ready
            正在运行 = 2,//run
            拧紧合格 = 3,//OK ：
            拧紧不合格 = 4,//NG：
        }

        /// <summary>
        ///  运行状态读取的返回值 MID0201_002_SysErr，code=002
        /// </summary>
        public enum MID0201_002_SysErr
        {
            //以“，”间隔, 以“；”结束
            设备故障 = 1,
            设备正常 = 0,
        }
        /// <summary>
        ///  运行状态读取的返回值 MID0201_002_SysErr，code=002
        /// </summary>
        public enum MID0201_002_SysErrID
        {
            //以“，”间隔, 以“；”结束\
            无信息 = 0,
            欠压 = 1,
            过压 = 2,
            编码器故障 = 3,
            软件过流 = 4,
            编码器通讯 = 5,
            速度异常 = 6,
            速度超差 = 7,
            故障1 = 8,
            故障2 = 9,
            故障3 = 10,
            IPM故障 = 11,
            故障4 = 12,
            故障5 = 13,
            故障6 = 14,
            IQ保护 = 15,
            摩擦保护 = 16,
        }
        /// <summary>
        ///  内部故障状态的返回值 MID0201_003_SysErr，code=003
        /// </summary>
        public enum MID0201_003_SysErr
        {
            //以“，”间隔, 以“；”结束
            故障 = 1,
            正常 = 0,
        }
        /// <summary>
        ///  内部故障状态的返回值 MID0201_003_SysErr，code=003
        /// </summary>
        public enum MID0201_003_SysErrID
        {
            //以“，”间隔, 以“；”结束\
            内部通信异常1 = 1,
            内部通信异常2 = 2,
            内部通信异常3 = 3,
        }
        /// <summary>
        /// DNKE连接状态
        /// </summary>
        public static bool DNKE_Connected = false;
        /// <summary>
        /// 建立连接,0001   
        /// </summary>
        public static byte[] Cmd_Connect = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x05, 0x52, 0x30, 0x30, 0x30, 0x31, 0x03 };
        /// <summary>
        /// 连接命令返回数组
        /// </summary>
        public static byte[] Cmd_Connect_Return = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x08, 0x41, 0x30, 0x30, 0x30, 0x31, 0x41, 0x43, 0x4B, 0x03 };
        /// <summary>
        /// 断开连接,0002
        /// </summary>
        public static byte[] Cmd_DisConnect = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x05, 0x52, 0x30, 0x30, 0x30, 0x32, 0x03 };
        /// <summary>
        /// 断开连接命令返回数组
        /// </summary>
        public static byte[] Cmd_DisConnect_Return = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x08, 0x41, 0x30, 0x30, 0x30, 0x31, 0x41, 0x43, 0x4B, 0x03 };

        /// <summary>
        /// 运行状态,0201 Ready/Busy/OK/NG/Err  
        /// </summary>
        public static byte[] Cmd_RunningState = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x05, 0x52, 0x30, 0x32, 0x30, 0x31, 0x03 };
        /// <summary>
        /// 最终拧紧结果,0202 
        /// </summary>
        public static byte[] Cmd_TighteningResults = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x05, 0x52, 0x30, 0x32, 0x30, 0x32, 0x03 };
        /// <summary>
        /// 实时曲线数据,0203
        /// </summary>
        public static byte[] Cmd_RealCurveData = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x05, 0x52, 0x30, 0x32, 0x30, 0x33, 0x03 };
        /// <summary>
        /// Pset 选择,0103,[0x3D,0x31,0x3B]表示"=1;",1是pset=1
        /// </summary>
        public static byte[] Cmd_SelectPset = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x0a, 0x57, 0x30, 0x31, 0x30, 0x33, 0x30, 0x31, 0x3D, 0x33, 0x3B, 0x03 };
        /// <summary>
        /// 电机运行停止使能 ON/OFF,0301
        /// </summary>
        public static byte[] Cmd_MotoeEnable = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x0a, 0x57, 0x30, 0x33, 0x30, 0x31, 0x30, 0x31, 0x3D, 0x31, 0x3B, 0x03 };
        public static byte[] Cmd_MotoDisable = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x0a, 0x57, 0x30, 0x33, 0x30, 0x31, 0x30, 0x31, 0x3d, 0x03, 0x3b, 0x03 };



        /*
         * string sssss = DNKE_DKTCP.toASCII("0123456");
                byte[] buffer = DNKE_DKTCP.toASCIIbyte("01234");
         */
        /// <summary>
        /// 数字转16进制ASCII码    
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string toASCII(string code)
        {
            char[] cs = code.ToCharArray();//先转字节数组
            string Hstr = null;
            for (int l = 0; l < cs.Length; l++)
            {
                Hstr += ((int)cs[l]).ToString("X");
            }
            //System.Console.WriteLine(Hstr);
            return Hstr;
        }
        /// <summary>
        /// 字符串转ASCII 的byte数组
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static byte[] toASCIIbyte(string code)
        {
            char[] cs = code.ToCharArray();//先转字节数组
            byte[] buffer = new byte[cs.Length];

            for (int l = 0; l < cs.Length; l++)
            {
                buffer[l] = Convert.ToByte(((int)cs[l]).ToString("X"));
            }
            //System.Console.WriteLine(Hstr);
            return buffer;
        }


        /// <summary>
        /// 16进制ASCII码转普通字符串,去掉前5位A000X  和最后一位03
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getAscBytetoStr(byte[] buffer)
        {
            //skip忽略前n位(从第N+1位开始)，Take截取M位
            byte[] e = buffer.Skip(6).Take(buffer.Length - 6 - 1).ToArray();



            string strCharacter = System.Text.Encoding.Default.GetString(e);
            return strCharacter;
        }
        /// <summary>
        /// 先将16进制ASCII码转字节数组
        /// </summary>
        /// <param name="sHex"></param>
        /// <param name="isExchange"></param>
        /// <returns></returns>
        public static byte[] Hex2Bytes(string sHex, bool isExchange)
        {
            if (sHex == null || sHex.Length == 0)
                return null;
            sHex = sHex.Length % 2 == 0 ? sHex : "0" + sHex;
            byte[] bRtns = new byte[sHex.Length / 2];
            for (int i = 0; i < bRtns.Length; i++)
            {
                if (isExchange)
                    bRtns[bRtns.Length - 1 - i] = Convert.ToByte(sHex.Substring(i * 2, 2), 16);
                else
                    bRtns[i] = Convert.ToByte(sHex.Substring(i * 2, 2), 16);
            }
            return bRtns;
        }
    }
}


