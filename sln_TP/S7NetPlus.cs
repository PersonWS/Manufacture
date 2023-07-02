using S7.Net;
using System;
using System.Collections.Generic;
using System.Text;
using thinger.DataConvertLib;

namespace sln_TP
{
    public class S7NetPlus
    {
        /// <summary>
        /// 转盘信号，转盘信号到位（DB1.DBX3.3）
        /// </summary>
        public static bool boolTurntableSignal = false;

        /// <summary>
        /// 屏蔽点信息，[DB,StartAddr,PointCount,bits],(DB,起点，字节数，连续位数)
        /// </summary>
        public static int[] ShieldDatasPointsInfo = new int[4] { 7, 0, 2, 9 };
        /// <summary>
        /// PLC屏蔽数据
        /// </summary>
        public static string[] ShieldDatas = new string[9]
        {
            "Y1供料器报警屏蔽;7;0;0",
            "Y2供料器报警屏蔽;7;0;1",
            "工装产品检测1屏蔽;7;0;2",
            "工装产品检测2屏蔽;7;0;3",
            "夹紧气缸1夹紧屏蔽;7;0;4",
            "夹紧气缸2夹紧屏蔽;7;0;5",
            "上位机屏蔽;7;0;6",
            "安全门屏蔽;7;0;7",
            "光幕屏蔽;7;1;0"
        };
        /// <summary>
        /// 是否读取输出节点信息
        /// </summary>
        public static bool readOutPoint = false;



        /// <summary>
        /// 输出点信息，[DB,StartAddr,ReadBitCount,PointCount]
        /// </summary>
        public static int[] outPointsInfo = new int[4] { 1, 6, 5, 34 };

        /// <summary>
        /// 34个输入消息，DB1.DBX0.0-4.5
        /// </summary>
        public static bool[] outDiagit = new bool[outPointsInfo[3]];
        /// <summary>
        /// 输出节点
        /// </summary>
        public enum outPointsList
        {
            三色灯黄色 = 0,
            三色灯绿色 = 1,
            三色灯红色 = 2,
            三色灯蜂鸣器 = 3,
            绿色半球指示灯 = 4,
            红色半球指示灯 = 5,
            运动控制卡任务选择1 = 6,
            运动控制卡任务选择2 = 7,
            运动控制卡任务选择3 = 8,
            运动控制卡任务选择4 = 9,
            运动控制卡任务选择5 = 10,
            Y1供料器有料输出 = 11,
            Y2供料器有料输出 = 12,
            运动控制卡启动 = 13,
            运动控制卡复位 = 14,
            运动控制卡紧急停止 = 15,
            运动控制卡停止 = 16,
            吸取螺丝电磁阀 = 17,
            下压气缸电磁阀 = 18,
            工装电磁铁 = 19,
            光栅报警 = 20,
            伺服复位 = 21,
            电批急停 = 22,
            电批测试启动 = 23,
            FFU = 24,
            照明 = 25,
            左启动指示灯 = 26,
            右启动指示灯 = 27,
            夹紧气缸夹紧 = 28,
            伺服报警 = 29,
            备用_5_1 = 30,
            备用_6_1 = 31,
            备用_7_1 = 32,
            备用_8_1 = 33,

        }


        /// <summary>
        /// 输入点信息，[DB,StartAddr,PointCount]
        /// </summary>
        public static int[] inputPointsInfo = new int[4] { 1, 0, 5, 38 };
        /// <summary>
        /// 38个输入消息状态，DB1.DBX0.0-4.5
        /// </summary>
        public static bool[] inputDiagitStatus = new bool[inputPointsInfo[3]];
        /// <summary>
        /// 38个输入消息
        /// </summary>
        public enum inputDiagitList
        {
            操作面板急停 = 0,
            回原位 = 1,
            启动按钮 = 2,
            停止按钮 = 3,
            手动or自动 = 4,
            按钮盒急停 = 5,
            左启动 = 6,
            右启动 = 7,
            备用 = 8,
            光幕 = 9,
            Y1供料器有料 = 10,
            Y2供料器有料 = 11,
            工装产品信号1 = 12,
            工装产品信号2 = 13,
            气夹气缸到位2 = 14,
            气夹气缸到位1 = 15,
            工具错误 = 16,
            工具拧紧OK = 17,
            工具拧紧NG = 18,
            工具准备好 = 19,
            工具运行 = 20,
            气源压力检测 = 21,
            光栅屏蔽对射开关 = 22,
            吸附信号 = 23,
            电批下压气缸下压信号 = 24,
            安全门 = 25,
            回原位中 = 26,
            回原位完成 = 27,
            X轴故障报警 = 28,
            Y轴故障报警 = 29,
            Z轴故障报警 = 30,
            运动控制卡完成释放夹紧气缸 = 31,
            备用_3 = 32,
            备用_4 = 33,
            备用_5 = 34,
            备用_6 = 35,
            备用_7 = 36,
            备用_8 = 37,
        }



        /// <summary>
        /// 报警信息，DB4，DBX512.0-DBX517.7,4个字节，每个字节8位，共48个位
        /// </summary>
        //public static List<Model.AlarmPoints> alarmPoints = new List<Model.AlarmPoints>();

        /// <summary>
        /// PLC读取用的DB结构
        /// </summary>
        public struct PLC_DB_Address
        {
            public int db;
            public int address;
        }
        /// <summary>
        /// 报警信息节点
        /// </summary>
        public static List<PLC_DB_Address> plc_DB_Alarm = new List<PLC_DB_Address>();



        string[] AlartMessage = new string[26] {"读取运动板卡的详细报警",
                                                    "请检查主气源是否有压力",
                                                    "请检查面板急停是否没有复位",
                                                    "请检查操作板急停是否没有复位",
                                                    "请检查安全门是否关闭",
                                                    "请检查光幕是否被遮挡",
                                                    "回原位超过设定时间。",
                                                    "Y1供料器缺料，请及时补料",
                                                    "Y2供料器缺料，请及时补料",
                                                    "工装产品检测1无件",
                                                    "工装产品检测2无件",
                                                    "夹紧气缸1未夹紧到位",
                                                    "夹紧气缸2未夹紧到位",
                                                    "检查伺服电批控制器是否有报警",
                                                    "一级报警",
                                                    "二级报警",
                                                    "下压气缸故障,请按复位按钮",
                                                    "打螺丝延时过小,请按复位按钮",
                                                    "真空检测失败,请按复位按钮",
                                                    "吸取螺丝失败,请按复位按钮",
                                                    "分料超时,请按复位按钮",
                                                    "夹紧气缸故障FB运行前未复位,请按复位按钮",
                                                    "下压气缸不在原位,请按复位按钮",
                                                    "X轴故障报警,请按复位按钮",
                                                    "Y轴故障报警,请按复位按钮",
                                                    "Z轴故障报警,请按复位按钮"
                                                            };

        /// <summary>
        /// 运动控制卡节点信息，[DB,StartAddr,BitCount,PointCount]
        /// </summary>
        public static int[] ActionCardPointsInfo = new int[4] { 4, 512, 4, 26 };

        /// <summary>
        /// 运动控制卡节点信息
        /// </summary>
        public static string[] ActionCardPoints = new string[54]
        {
                    "X轴原位,Bool,2,0,0",
                    "Y轴原位,Bool,2,0,1",
                    "Z轴原位,Bool,2,0,2",
                    "Y1供料器有料,Bool,2,0,3",
                    "Y2供料器有料,Bool,2,0,4",
                    "备用,Bool,2,0,5",
                    "任务选择1,Bool,2,0,6",
                    "任务选择2,Bool,2,0,7",
                    "启动,Bool,2,1,0",
                    "回原位,Bool,2,1,1",
                    "急停,Bool,2,1,2",
                    "OK,Bool,2,1,3",
                    "光幕和门控报警,Bool,2,1,4",
                    "任务选择3,Bool,2,1,5",
                    "任务选择4,Bool,2,1,6",
                    "任务选择5,Bool,2,1,7",
                    "负压传感器,Bool,2,2,0",
                    "备用_1,Bool,2,2,1",
                    "备用_2,Bool,2,2,2",
                    "备用_3,Bool,2,2,3",
                    "备用_4,Bool,2,2,4",
                    "下压气缸原位,Bool,2,2,5",
                    "备用_5,Bool,2,2,6",
                    "停止,Bool,2,2,7",
                    "吸取螺丝信号,Bool,2,4,0",
                    "回原位成功,Bool,2,4,1",
                    "回原位中,Bool,2,4,2",
                    "下压气缸信号,Bool,2,4,3",
                    "Y5,Bool,2,4,4",
                    "认螺帽反转,Bool,2,4,5",
                    "运动控制卡完成信号,Bool,2,4,6",
                    "Y8,Bool,2,4,7",
                    "任务选择3,Bool,2,5,0",
                    "任务选择4,Bool,2,5,1",
                    "任务选择5,Bool,2,5,2",
                    "Y12,Bool,2,5,3",
                    "启动电批,Bool,2,5,4",
                    "Y14,Bool,2,5,5",
                    "回原位标志位,Bool,2,5,6",
                    "Y16,Bool,2,5,7",
                    "获取X轴当前坐标,Real,2,6,0",
                    "获取Y轴当前坐标,Real,2,1,0",
                    "获取Z轴当前坐标,Real,2,2,0",
                    "获取Y2轴当前坐标,Real,2,3,0",
                    "完成螺丝数量,Int,2,3,0",
                    "完成产品数量,Int,2,4,0",
                    "产品合格率,Int,2,4,0",
                    "螺丝孔加工完成情况查询,Word,2,4,0",
                    "设备报警状态查询,Word,2,4,0",
                    "左机加工完成情况查询,Word,2,4,0",
                    "右机加工完成情况查询,Word,2,5,0",
                    "报警异常号,Int,2,5,0",
                    "光标处运行标志,Int,2,5,0",
                    "当前程序所在行号,Int,2,5,0",
        };

        //定义PLC类型
        public static Plc S71200;

        /// <summary>
        /// 读取bool状态数组
        /// </summary>
        /// <param name="DbNum">DB块</param>
        /// <param name="Start_Address">其实地址</param>
        /// <param name="Bits">读取的字节数，每个字节8个位</param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo ReadMuliBools(int DbNum, int Start_Address, int Bits)
        //{
        //    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
        //    bool[] res = new bool[1];
        //    try
        //    {
        //        jr.Successed = true;
        //        //读取数据选择从DB块中读取，db设置为1，起始地址为0，读取N个字节    0.0-5.7,N=6字节,每个字节8个位
        //        var bytes = S71200.ReadBytes(S7.Net.DataType.DataBlock, DbNum, Start_Address, Bits);
        //        jr.booValue = BitLib.GetBitArrayFromByteArray(bytes);
        //        return jr;
        //    }
        //    catch (Exception ex)
        //    {
        //        jr.Successed = false;
        //        jr.Message = ex.Message;
        //        return jr;
        //    }
        //}

        /// <summary>
        /// 上位机启动信号
        /// </summary>
        /// <param name="b">true可以启动，false禁止启动</param>
        /// <param name="isenabled">true启用，false禁用</param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo PLC_StartSignal(bool b, bool isenabled)
        //{
        //    //上位机启动信号 W   Bool DB8.DBX260.0
        //    Model.ResultJsonInfo jr = S7NetPlus.WriteDataBool(8, 260, b, 0);
        //    return jr;
        //}
        /// <summary>
        /// 读一个bool值
        /// </summary>
        /// <param name="DbNum">DB块</param>
        /// <param name="Start_Address">读取的字节</param>
        /// <param name="bitAddr">读start_address第几位</param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo ReadOneBool(int DbNum, int Start_Address, byte bitAddr = 0)
        //{

        //    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
        //    try
        //    {
        //        jr.Successed = true;
        //        jr.booValue = new bool[1];
        //        jr.booValue[0] = (bool)S71200.Read(S7.Net.DataType.DataBlock, DbNum, Start_Address, VarType.Bit, 1, bitAddr);
        //        return jr;
        //    }
        //    catch (Exception ex)
        //    {
        //        jr.Successed = false;
        //        jr.Message = ex.Message;
        //        return jr;
        //    }
        //}

        /// <summary>
        /// 读一个int 值
        /// </summary>
        /// <param name="DbNum"></param>
        /// <param name="Start_Address"></param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo ReadOneInt(int DbNum, int Start_Address, byte bitAddr = 0)
        //{

        //    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
        //    try
        //    {
        //        jr.Successed = true;
        //        jr.intValue = Convert.ToInt32(S71200.Read(S7.Net.DataType.DataBlock, DbNum, Start_Address, VarType.Int, 1, bitAddr));
        //        return jr;
        //    }
        //    catch (Exception ex)
        //    {
        //        jr.Successed = false;
        //        jr.Message = "读取失败," + ex.Message;
        //        return jr;
        //    }
        //}


        /// <summary>
        /// 读一个Dint 值
        /// </summary>
        /// <param name="DbNum"></param>
        /// <param name="Start_Address"></param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo ReadOneDInt(int DbNum, int Start_Address, byte bitAddr = 0)
        //{

        //    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
        //    try
        //    {
        //        jr.Successed = true;
        //        jr.intValue = Convert.ToInt32(S71200.Read(S7.Net.DataType.DataBlock, DbNum, Start_Address, VarType.DInt, 1, bitAddr));
        //        return jr;
        //    }
        //    catch (Exception ex)
        //    {
        //        jr.Successed = false;
        //        jr.Message = "读取失败," + ex.Message;
        //        return jr;
        //    }
        //}

        /// <summary>
        /// 读一个Real值
        /// </summary>
        /// <param name="DbNum"></param>
        /// <param name="Start_Address"></param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo ReadOneReal(int DbNum, int Start_Address, byte bitAddr = 0)
        //{

        //    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
        //    try
        //    {
        //        jr.Successed = true;
        //        jr.doubleValue = Convert.ToDouble(S71200.Read(S7.Net.DataType.DataBlock, DbNum, Start_Address, VarType.Real, 1, bitAddr));
        //        return jr;
        //    }
        //    catch (Exception ex)
        //    {
        //        jr.Successed = false;
        //        jr.Message = "读取失败," + ex.Message;
        //        return jr;
        //    }
        //}

        /// <summary>
        /// 读一个String值
        /// </summary>
        /// <param name="DbNum"></param>
        /// <param name="Start_Address"></param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo ReadOneString(int DbNum, int Start_Address, byte bitAddr = 0)
        //{

        //    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
        //    try
        //    {
        //        //获取字符串长度
        //        var reservedLength = Convert.ToByte(S71200.Read(S7.Net.DataType.DataBlock, DbNum, Start_Address, VarType.Byte, 1, bitAddr));
        //        string StrReplac = Convert.ToString(S71200.Read(S7.Net.DataType.DataBlock, DbNum, Start_Address, VarType.S7String, reservedLength, bitAddr));
        //        //去除字符串后面空白符号
        //        if (StrReplac != null)
        //        {
        //            int jindex = StrReplac.IndexOf('\0', 0);
        //            if (jindex > -1)
        //                jr.stringValue = StrReplac.Substring(0, jindex);
        //            else
        //                jr.stringValue = StrReplac.Replace("\0", "");
        //        }
        //        jr.Successed = true;

        //        return jr;
        //    }
        //    catch (Exception ex)
        //    {
        //        jr.Successed = false;
        //        jr.Message = "读取失败," + ex.Message;
        //        return jr;
        //    }
        //}

        /// <summary>
        /// 写bool
        /// </summary>
        /// <param name="DbNum"></param>
        /// <param name="Start_Address"></param>
        /// <param name="DataType"></param>
        /// <param name="value">写入值，true/false </param>
        /// <param name="bitAddr">字节的第几位</param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo WriteDataBool(int DbNum, int Start_Address, bool value, int bitAddr = 0)
        //{
        //    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
        //    try
        //    {
        //        jr.Successed = true;
        //        S71200.WriteBit(S7.Net.DataType.DataBlock, DbNum, Start_Address, bitAddr, value);
        //        return jr;
        //    }
        //    catch (Exception ex)
        //    {
        //        jr.Successed = false;
        //        jr.Message = String.Format("写入Bool失败,DB{0}DBX{1}.{2},", DbNum, Start_Address, bitAddr) + ex.Message;
        //        return jr;
        //    }
        //}

        /// <summary>
        /// 写iint
        /// </summary>
        /// <param name="DbNum"></param>
        /// <param name="Start_Address"></param>
        /// <param name="DataType"></param>
        /// <param name="value">写入值，int </param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo WriteDataInt(int DbNum, int Start_Address, Int16 value)
        //{
        //    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
        //    try
        //    {
        //        jr.Successed = true;
        //        S71200.Write(S7.Net.DataType.DataBlock, DbNum, Start_Address, value);
        //        return jr;
        //    }
        //    catch (Exception ex)
        //    {
        //        jr.Successed = false;
        //        jr.Message = String.Format("写入int失败,DB{0}DBD{1},", DbNum, Start_Address) + ex.Message;
        //        return jr;
        //    }
        //}

        /// <summary>
        /// 写iint
        /// </summary>
        /// <param name="DbNum"></param>
        /// <param name="Start_Address"></param>
        /// <param name="DataType"></param>
        /// <param name="value">写入值，int </param>
        /// <returns></returns>
        //public static Model.ResultJsonInfo WriteDataString(int DbNum, int Start_Address, string value)
        //{
        //    Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
        //    try
        //    {
        //        jr.Successed = true;
        //        string val = value;
        //        var temp = Encoding.Default.GetBytes(value);   //将val字符串转换为字符数组
        //        var bytes = S7.Net.Types.S7String.ToByteArray(val, temp.Length);
        //        S71200.WriteBytes(S7.Net.DataType.DataBlock, DbNum, Start_Address, bytes);
        //        return jr;
        //    }
        //    catch (Exception ex)
        //    {
        //        jr.Successed = false;
        //        jr.Message = String.Format("写入string失败,DB{0}DBD{1},", DbNum, Start_Address) + ex.Message;
        //        return jr;
        //    }
        //}

    }

}
