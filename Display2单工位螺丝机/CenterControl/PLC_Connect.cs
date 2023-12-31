﻿using System;
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

        public bool IsConnecting = true;

        private bool _isWriting = false;

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

        private static readonly object _obj2 = new object();

        private CpuType _cpuType;
        public string _ip;
        private short _rack;
        private short _slot;
        private int _port;

        public PLC_Connect(CpuType type, string ip, short rack, short slot, int port = 102)
        {
            //创建PLC对象
            //_plcEntity = new Plc(CpuType.S71200, ConfigurationKeys.PLC_IP, port, ConfigurationKeys.PLC_Rack, ConfigurationKeys.PLC_Slot);
            this._cpuType = type;
            this._ip = ip;
            this._rack = rack;
            this._slot = slot;
            this._port = port;
        }


        public bool Connect()
        {
            lock (_obj2)
            {
                try
                {
                    IsConnecting = true;
                    _plcEntity = new Plc(_cpuType, _ip, _port, _rack, _slot); _plcEntity.ReadTimeout = 3; _plcEntity.WriteTimeout = 3;
                    MessageOutPutMethod(string.Format("PLC:{0} 开始尝试连接...", this.PlcEntity.IP), 0);
                    _plcEntity.Open();
                    IsConnected = true;
                    if (PlcConnected != null)
                    {
                        PlcConnected(this);
                    }
                    MessageOutPutMethod(string.Format("PLC:{0} 连接成功！", this.PlcEntity.IP), 0);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageOutPutMethod(string.Format("PLC:{0} 连接失败！ \r\n ex={1}", this.PlcEntity.IP, ex.ToString()), 0);
                    IsConnected = false;
                    return false;
                }
                finally
                { IsConnecting = false; }
            }

        }


        public bool DisConnect()
        {
            try
            {
                while (_isWriting)
                {
                    Thread.Sleep(50);
                }
                if (_plcEntity != null)
                {
                    _plcEntity.Close();
                    IsConnected = false;

                    MessageOutPutMethod(string.Format("PLC:{0} 连接已断开", this.PlcEntity.IP), 0);
                    if (PlcDisConnected != null)
                    {
                        PlcDisConnected(this);
                    }
                    //_plcEntity = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageOutPutMethod("DisConnect failed" + ex.ToString(),1);
                return false;
            }
        }

        public bool ReadData(ref PLC_Point item)
        {
            item.isValueChanged = false;
            item.isReadSucess = false;
            try
            {
                switch (item.plcReadType)
                {
                    case PLC_Point_Type.T_Bool://按照bool来读取

                        break;
                    case PLC_Point_Type.T_Byte://按照byte来读取
                        byte[] re = PlcEntity.ReadBytes(DataType.DataBlock, item.DataBlock, item.DataAdress, item.Length);
                        item.isReadSucess = true;
                        if (re != null && re.Length > 0)
                        {
                            //验证 是否为bool元素
                            if (item.plcRealType == PLC_Point_Type.T_Bool)
                            {
                                string binaryStr = ByteToBinaryString(re[0]);
                                bool value = binaryStr[7 - item.BitAdress].ToString() == "1" ? true : false;
                                if (item.value == null || value != (bool)item.value)
                                {
                                    item.value = value;
                                    item.isValueChanged = true;
                                }
                            }
                            else
                            {
                                if (re[0] != (byte)item.value)
                                {
                                    item.isValueChanged = true;
                                    item.value = re[0];
                                }
                            }
                        }
                        break;
                    case PLC_Point_Type.T_Int:
                        break;
                    case PLC_Point_Type.T_Word:
                        break;
                    case PLC_Point_Type.T_String://按照string来读取
                        byte[] sre1 = PlcEntity.ReadBytes(DataType.DataBlock, item.DataBlock, item.DataAdress, 2); //获取字符串长度
                        item.isReadSucess = true;
                        if (sre1 != null && sre1.Length > 0)
                        {
                            byte[] sre2 = PlcEntity.ReadBytes(DataType.DataBlock, item.DataBlock, item.DataAdress, sre1[1] + 2);
                            string str = Encoding.ASCII.GetString(sre2.Skip(2).Take(sre2.Length - 2).ToArray()).Replace("\0", "");
                            if (str != (string)item.value)
                            {
                                item.value = str;
                                item.isValueChanged = true;
                            }
                        }
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                //MessageOutput("plc_connect: ReadData" + e.ToString());
                return false;
                //if (true)
                //{

                //}
                //throw e;
            }

        }

        public bool WriteData(PLC_Point p)
        {
            lock (_obj2)
            {
                _isWriting = true;
                try
                {
                    if (!_plcEntity.IsConnected)
                    {
                        return false;
                    }
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
                            //生成byte[]
                            if (p.value != null && p.Length != 0)
                            {
                                List<byte> stringList = new List<byte>();
                                stringList.AddRange(new byte[] { p.maxLength, (byte)((string)p.value).Length });
                                stringList.AddRange(Encoding.ASCII.GetBytes((string)p.value));
                                _plcEntity.WriteBytes(p.dataType, p.DataBlock, p.DataAdress, stringList.ToArray());
                            }
                            else
                            {
                                _plcEntity.WriteBytes(p.dataType, p.DataBlock, p.DataAdress, new byte[] { 0, 0 });
                            }

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

                    MessageOutPutMethod("PLC_Connect-- WriteData error ex=" + ex.ToString(), 1);
                    return false;
                }
                finally
                {
                    _isWriting = false;
                }
            }

        }


        private void MessageOutPutMethod(string s, int level)
        {
            LogUtility.RecordLog(level, s);
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
        public string ByteToBinaryString(byte data)
        {
            string str = Convert.ToString(data, 2).PadLeft(8, '0');
            return str;
        }


    }

}
