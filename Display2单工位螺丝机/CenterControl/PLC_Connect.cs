using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrewMachineManagementSystem;
using S7.Net;

using System.Threading;

namespace ScrewMachineManagementSystem.CenterControl
{

    public class PLC_Connect
    {
        S7.Net.Plc _plcEntity;

        public Plc PlcEntity { get => _plcEntity; }

        public bool IsConnected = false;

        /// <summary>
        /// 输出操作信息
        /// </summary>
        public event Action<string> MessageOutput;
        /// <summary>
        /// PLC成功进行了连接
        /// </summary>
        public event Action<PLC_Connect> PlcConnected;
        /// <summary>
        /// PLC连接断开
        /// </summary>
        public event Action<PLC_Connect> PlcDisConnected;

        public event PointValueChangedEventHnadler PointWriteFail;

        public PLC_Connect(CpuType type, string ip, short rack, short slot, int port = 102)
        {
            //创建PLC对象
            //_plcEntity = new Plc(CpuType.S71200, ConfigurationKeys.PLC_IP, port, ConfigurationKeys.PLC_Rack, ConfigurationKeys.PLC_Slot);
            _plcEntity = new Plc(type, ip, port, rack, slot);
        }


        public bool Connect()
        {
            try
            {
                PlcEntity.Open();
                IsConnected = true;
                if (PlcConnected != null)
                {
                    PlcConnected(this);
                }
                MessageOutPutMethod(string.Format("PLC:{0} 连接成功！", this.PlcEntity.IP));
                return true;
            }
            catch (Exception ex)
            {
                MessageOutPutMethod(string.Format("PLC:{0} 连接失败！ \r\n ex={1}", this.PlcEntity.IP, ex.ToString()));
                IsConnected = false;
                return false;
            }
        }


        public bool DisConnect()
        {
            try
            {
                PlcEntity.Close();
                IsConnected = false;
                if (PlcDisConnected != null)
                {
                    PlcDisConnected(this);
                }
                MessageOutPutMethod(string.Format("PLC:{0} 连接已断开", this.PlcEntity.IP));
                return true;
            }
            catch (Exception ex)
            {
                MessageOutPutMethod("DisConnect failed" + ex.ToString());
                return false;
            }
        }

        public bool WriteData(PLC_Point p)
        {
            try
            {
                switch (p.plcWriteType)
                {
                    case PLC_Point_Type.T_Bool:
                        _plcEntity.WriteBit(p.dataType, p.DataBlock, p.DataAdress, p.BitAdress, (bool)p.value);
                        return true;
                    case PLC_Point_Type.T_Byte:
                        break;
                    case PLC_Point_Type.T_Bytes:
                        break;
                    case PLC_Point_Type.T_Int:
                        break;
                    case PLC_Point_Type.T_Ints:
                        break;
                    case PLC_Point_Type.T_Word:
                        break;
                    case PLC_Point_Type.T_String:
                        _plcEntity.WriteBytes(p.dataType, p.DataBlock, p.DataAdress + 2, Encoding.ASCII.GetBytes((string)p.value));
                        return true;
                    default:
                        return false;
                }
                return false;
                // _plcEntity.Write
            }
            catch (Exception ex)
            {
                WriteFail(p);

                MessageOutPutMethod("PLC_Connect-- WriteData error ex=" + ex.ToString());
                return false;
            }
        }


        private void MessageOutPutMethod(string s)
        {
            if (MessageOutput != null)
            {
                MessageOutput(s);
            }
        }
        private void WriteFail(PLC_Point p)
        {
            if (PointWriteFail != null)
            {
                PointWriteFail(p);
            }
        }


    }

}
