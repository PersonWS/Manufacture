using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.CenterControl
{
    /// <summary>
    /// 业务需要的PLC的点位
    /// </summary>
    public static class BusinessNeedPlcPoint
    {
        /// <summary>
        /// 需要采集的PLC点位
        /// </summary>
        static List<PLC_Point> _gatherPLC_Point;

        /// <summary>
        /// 需要写入的PLC点位
        /// </summary>
        static List<PLC_Point> _writePLC_Point;


        static Dictionary<string, PLC_Point> _dic_PLC_Point;

        static Dictionary<string, PLC_Point> _dic_writePLC_Point;

        static BusinessNeedPlcPoint()
        {
            InitializeGatherPlcPoint();
            //InitializeWritePlcPoint();
        }

        //public static List<PLC_Point> GatherPLC_Point { get => _gatherPLC_Point; }
        //public static List<PLC_Point> WritePLC_Point { get => _writePLC_Point; }

        public static Dictionary<string, PLC_Point> Dic_gatherPLC_Point { get => _dic_PLC_Point; }

        public static Dictionary<string, PLC_Point> Dic_writePLC_Point { get => _dic_writePLC_Point; }

        /// <summary>
        /// 这里填写需要监控的PLC点位
        /// </summary>
        private static void InitializeGatherPlcPoint()
        {
            //_gatherPLC_Point = new List<PLC_Point>();
            _dic_PLC_Point = new Dictionary<string, PLC_Point>();
            PLC_Point p1 = new PLC_Point();
            p1.VarName = "SN码请求";
            p1.dataType = S7.Net.DataType.DataBlock;
            p1.DataBlock = 2001;
            p1.DataAdress = 0;
            p1.BitAdress = 0;
            p1.plcReadType = PLC_Point_Type.T_Byte;
            p1.plcRealType = PLC_Point_Type.T_Bool;
            p1.remark = "收到SN码请求时，可清除上次写入的所有数据，例如SN码，禁止加工，允许加工，互锁结果，结果收到等";
            p1.value = false;
            p1.plcOperateType = PLC_Point_Operate_Type.ReadOnly;
            //_gatherPLC_Point.Add(p1);
            _dic_PLC_Point.Add("SN码请求", p1);

            PLC_Point p2 = new PLC_Point();
            p2.VarName = "开始加工请求";
            p2.dataType = S7.Net.DataType.DataBlock;
            p2.DataBlock = 2001;
            p2.DataAdress = 0;
            p2.BitAdress = 1;
            p2.plcReadType = PLC_Point_Type.T_Byte;
            p2.plcRealType = PLC_Point_Type.T_Bool;
            p2.value = false;
            p2.plcOperateType = PLC_Point_Operate_Type.ReadOnly;
            //_gatherPLC_Point.Add(p2);
            _dic_PLC_Point.Add("开始加工请求", p2);


            PLC_Point p4 = new PLC_Point();
            p4.VarName = "结果NG";
            p4.dataType = S7.Net.DataType.DataBlock;
            p4.DataBlock = 2001;
            p4.DataAdress = 0;
            p4.BitAdress = 3;
            p4.plcReadType = PLC_Point_Type.T_Byte;
            p4.plcRealType = PLC_Point_Type.T_Bool;
            p4.value = false;
            p4.plcOperateType = PLC_Point_Operate_Type.ReadOnly;
            //_gatherPLC_Point.Add(p4);
            _dic_PLC_Point.Add("结果NG", p4);

            PLC_Point p3 = new PLC_Point();
            p3.VarName = "结果OK";
            p3.dataType = S7.Net.DataType.DataBlock;
            p3.DataBlock = 2001;
            p3.DataAdress = 0;
            p3.BitAdress = 2;
            p3.plcReadType = PLC_Point_Type.T_Byte;
            p3.plcRealType = PLC_Point_Type.T_Bool;
            p3.value = false;
            p2.plcOperateType = PLC_Point_Operate_Type.ReadOnly;
            //_gatherPLC_Point.Add(p3);
            _dic_PLC_Point.Add("结果OK", p3);

            //写入的PLC点位
            _writePLC_Point = new List<PLC_Point>();
            _dic_writePLC_Point = new Dictionary<string, PLC_Point>();
            PLC_Point p11 = new PLC_Point();
            p11.VarName = "写入SN码";
            p11.dataType = S7.Net.DataType.DataBlock;
            p11.DataBlock = 2002;
            p11.DataAdress = 1;
            p11.plcReadType = PLC_Point_Type.T_String;
            p11.plcWriteType = PLC_Point_Type.T_String;
            p11.plcOperateType = PLC_Point_Operate_Type.readAndWrite;
            //p1.plcAnalysisType = PLC_Point_Type.T_Bool;
            //_writePLC_Point.Add(p1);
            _dic_PLC_Point.Add("写入SN码", p11);

            PLC_Point p12 = new PLC_Point();
            p12.VarName = "允许加工请求";
            p12.dataType = S7.Net.DataType.DataBlock;
            p12.DataBlock = 2002;
            p12.DataAdress = 0;
            p12.BitAdress = 0;
            p12.plcWriteType = PLC_Point_Type.T_Bool;
            p12.plcRealType = PLC_Point_Type.T_Bool;
            p12.plcReadType = PLC_Point_Type.T_Byte;
            p12.plcOperateType = PLC_Point_Operate_Type.readAndWrite;
            //_writePLC_Point.Add(p2);
            _dic_PLC_Point.Add("允许加工请求", p12);

            PLC_Point p13 = new PLC_Point();
            p13.VarName = "禁止加工请求";
            p13.dataType = S7.Net.DataType.DataBlock;
            p13.DataBlock = 2002;
            p13.DataAdress = 0;
            p13.BitAdress = 1;
            p13.plcWriteType = PLC_Point_Type.T_Bool;
            p13.plcRealType = PLC_Point_Type.T_Bool;
            p13.plcReadType = PLC_Point_Type.T_Byte;
            p13.plcOperateType = PLC_Point_Operate_Type.readAndWrite;
            //_writePLC_Point.Add(p3);
            _dic_PLC_Point.Add("禁止加工请求", p13);

            PLC_Point p14 = new PLC_Point();
            p14.VarName = "互锁结果";
            p14.dataType = S7.Net.DataType.DataBlock;
            p14.DataBlock = 2002;
            p14.DataAdress = 0;
            p14.BitAdress = 3;
            p14.plcWriteType = PLC_Point_Type.T_Bool;
            p14.plcRealType = PLC_Point_Type.T_Bool;
            p14.plcReadType = PLC_Point_Type.T_Byte;
            p14.plcOperateType = PLC_Point_Operate_Type.readAndWrite;
            p14.remark = "0: 互锁失败     1：互锁成功";
            //_writePLC_Point.Add(p4);
            _dic_PLC_Point.Add("互锁结果", p14);

            PLC_Point p15 = new PLC_Point();
            p15.VarName = "加工结果收到";
            p15.dataType = S7.Net.DataType.DataBlock;
            p15.DataBlock = 2002;
            p15.DataAdress = 0;
            p15.BitAdress = 2;
            p15.plcWriteType = PLC_Point_Type.T_Bool;
            p15.plcRealType = PLC_Point_Type.T_Bool;
            p15.plcReadType = PLC_Point_Type.T_Byte;
            p15.plcOperateType = PLC_Point_Operate_Type.readAndWrite;
            //_writePLC_Point.Add(p5);
            _dic_PLC_Point.Add("加工结果收到", p15);
        }

        /*
        private static void InitializeWritePlcPoint()
        {
            _writePLC_Point = new List<PLC_Point>();
            _dic_writePLC_Point = new Dictionary<string, PLC_Point>();
            PLC_Point p1 = new PLC_Point();
            p1.VarName = "写入SN码";
            p1.dataType = S7.Net.DataType.DataBlock;
            p1.DataBlock = 2002;
            p1.DataAdress = 1;
            //p1.BitAdress = 0;
            p1.plcWriteType = PLC_Point_Type.T_String;
            //p1.plcAnalysisType = PLC_Point_Type.T_Bool;
            _writePLC_Point.Add(p1);
            _dic_writePLC_Point.Add("写入SN码", p1);

            PLC_Point p2 = new PLC_Point();
            p2.VarName = "允许加工请求";
            p2.dataType = S7.Net.DataType.DataBlock;
            p2.DataBlock = 2002;
            p2.DataAdress = 0;
            p2.BitAdress = 0;
            p2.plcWriteType = PLC_Point_Type.T_Bool;
            p2.plcRealType = PLC_Point_Type.T_Bool;
            _writePLC_Point.Add(p2);
            _dic_writePLC_Point.Add("允许加工请求", p2);

            PLC_Point p3 = new PLC_Point();
            p3.VarName = "禁止加工请求";
            p3.dataType = S7.Net.DataType.DataBlock;
            p3.DataBlock = 2002;
            p3.DataAdress = 0;
            p3.BitAdress = 1;
            p3.plcWriteType = PLC_Point_Type.T_Bool;
            p3.plcRealType = PLC_Point_Type.T_Bool;
            _writePLC_Point.Add(p3);
            _dic_writePLC_Point.Add("禁止加工请求", p3);

            PLC_Point p4 = new PLC_Point();
            p4.VarName = "互锁结果";
            p4.dataType = S7.Net.DataType.DataBlock;
            p4.DataBlock = 2002;
            p4.DataAdress = 0;
            p4.BitAdress = 3;
            p4.plcWriteType = PLC_Point_Type.T_Bool;
            p4.plcRealType = PLC_Point_Type.T_Bool;
            p4.remark = "0: 互锁失败     1：互锁成功";
            _writePLC_Point.Add(p4);
            _dic_writePLC_Point.Add("互锁结果", p4);

            PLC_Point p5 = new PLC_Point();
            p5.VarName = "加工结果收到";
            p5.dataType = S7.Net.DataType.DataBlock;
            p5.DataBlock = 2002;
            p5.DataAdress = 0;
            p5.BitAdress = 2;
            p5.plcWriteType = PLC_Point_Type.T_Bool;
            p5.plcRealType = PLC_Point_Type.T_Bool;
            _writePLC_Point.Add(p5);
            _dic_writePLC_Point.Add("加工结果收到", p5);

        }
        */

    }
}
