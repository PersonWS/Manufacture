using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.CenterControl
{
    public class PLC_Point
    {
        /// <summary>
        /// 数据类型--PLC 第三方库使用
        /// </summary>
       public S7.Net.DataType dataType;
        /// <summary>
        /// 读取时按照哪种格式读取
        /// </summary>
        public PLC_Point_Type plcReadType;
        /// <summary>
        /// 数据使用时按照哪种数据格式来使用
        /// </summary>
        public PLC_Point_Type plcRealType;

        /// <summary>
        /// 写入时按照哪种格式读取
        /// </summary>
        public PLC_Point_Type plcWriteType;

        /// <summary>
        /// 点位的操作类型  只读  只写  读写
        /// </summary>
        public PLC_Point_Operate_Type plcOperateType;

        /// <summary>
        /// DB块的地址
        /// </summary>
        public int DataBlock;
        /// <summary>
        /// 数据位起始地址
        /// </summary>
        public int DataAdress;
        /// <summary>
        /// 如果需要读写数字，则指定数组长度
        /// </summary>
        public int Length=1;
        /// <summary>
        /// 如果是位读，需要读第几位，255则读取所有的位
        /// </summary>
        public int BitAdress;
        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName;
        /// <summary>
        /// 当前值
        /// </summary>
        public object value;
        /// <summary>
        /// 附加说明
        /// </summary>
        public string remark;


        /*
        /// <summary>
        /// 按照业务获得实际的名称
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            string name = "";
            switch (dataType)
            {
                case S7.Net.DataType.Input:
                    break;
                case S7.Net.DataType.Output:
                    break;
                case S7.Net.DataType.Memory:
                    break;
                case S7.Net.DataType.DataBlock:
                    name += "DB" + DataBlock.ToString();
                    switch (plcAnalysisType)
                    {
                        case PLC_Point_Type.T_Bool:
                            break;
                        case PLC_Point_Type.T_Byte:
                            break;
                        case PLC_Point_Type.T_Int:
                            break;
                        case PLC_Point_Type.T_Word:
                            break;
                        case PLC_Point_Type.T_String:
                            break;
                        default:
                            break;
                    }
                    break;
                case S7.Net.DataType.Timer:
                    break;
                case S7.Net.DataType.Counter:
                    break;
                default:
                    break;
            }
            return name;
        }
        */

    }
    public enum PLC_Point_Type
    {
        T_Bool=1,
        T_Byte=11,
        T_Bytes = 12,
        T_Int =21,
        T_Ints = 22,
        T_Word =31,
        T_String=100
    }

    public enum PLC_Point_Operate_Type
    {
        ReadOnly = 1,
        writeOnly = 2,
        readAndWrite = 3,
    }
}
