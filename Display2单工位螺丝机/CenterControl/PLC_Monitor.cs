using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ScrewMachineManagementSystem;
using S7.Net;

namespace ScrewMachineManagementSystem.CenterControl
{
    public delegate void OperateErrorMessageEventHnadler(string msg);

    public delegate void PointValueChangedEventHnadler(PLC_Point point);
    /// <summary>
    /// 用于监视PLC变量的变化情况
    /// </summary>
    public class PLC_Monitor
    {
        /// <summary>
        /// 程序已创建的plc连接实例
        /// </summary>
        private PLC_Connect _plcConnectEntity;

        //创建PLC对象
        //S7NetPlus.S71200 = new Plc(CpuType.S71200, ConfigurationKeys.PLC_IP, ConfigurationKeys.PLC_Rack, ConfigurationKeys.PLC_Slot);

        /// <summary>
        /// polling  监视点位
        /// </summary>
        private Thread _thread_engine;
        /// <summary>
        /// 监视现场心跳
        /// </summary>
        long _heartBeat_engine = 0;

        /// <summary>
        /// defender 线程，通过心跳信息判断
        /// </summary>
        private Thread _thread_defend;
        /// <summary>
        /// defender 心跳检测记录
        /// </summary>
        long _heartbeat_defender = 0;

        bool _isMonitor = false;

        bool _isDefender = false;

        int _monitorInterval = 100;

        public PLC_Connect PlcConnectEntity { get => _plcConnectEntity; }

        public event PointValueChangedEventHnadler pointValueChanged;
        /// <summary>
        /// 信息输出事件
        /// </summary>
        public event Action<string> MessageOutput;

        public PLC_Monitor(PLC_Connect connect, int monitorInterval = 100)
        {
            this._monitorInterval = monitorInterval;
            ThreadPool.SetMaxThreads(10, 20);
            _plcConnectEntity = connect;
            
        }

        ~PLC_Monitor()
        {
            _isMonitor = false;
            _isDefender = false;
        }

        public bool Start()
        {
            if (!ConnectPlc())
            {
                return false;
            };
            _isMonitor = true;
            _isDefender = true;
            StartMonitorThread();
            StartDefenderThread();
            return true;
        }

        private bool ConnectPlc()
        {
            try
            {
                if (PlcConnectEntity != null && !PlcConnectEntity.PlcEntity.IsConnected)
                {
                   return PlcConnectEntity.Connect();
                }
                else if (PlcConnectEntity == null)
                {
                    MessageOutPutMethod("PLC_Monitor--Start _plcConnectEntity is null");
                    return false;
                }
                return true;
            }
            catch (Exception es)
            {
                MessageOutPutMethod("PLC_Monitor--Start  ConnectPlc error ,ex=" + es.ToString());
                return false;
            }
        }

        public bool Stop()
        {
            _isDefender = false;
            _isMonitor = false;
            return true;
        }
        private void StartMonitorThread()
        {
            if (_thread_engine != null && _thread_engine.ThreadState != ThreadState.Stopped)
            {
                try
                {
                    _thread_engine.Abort();
                    _thread_engine = null;
                }
                catch (Exception)
                {
                }
            }
            _thread_engine = new Thread(Monitor);
            _thread_engine.Name = "thread_engine";
            _thread_engine.IsBackground = true;
            _thread_engine.Start();
        }
        private void StartDefenderThread()
        {
            if (_thread_defend != null && _thread_defend.ThreadState != ThreadState.Stopped)
            {
                try
                {
                    _thread_defend.Abort();
                    _thread_defend = null;
                }
                catch (Exception)
                {
                }
            }

            _thread_defend = new Thread(Defender);
            _thread_defend.Name = "thread_defend";
            _thread_defend.IsBackground = true;
            _thread_defend.Start();
        }

        /// <summary>
        /// 监控变量的主线程
        /// </summary>
        private void Monitor()
        {
            while (_isMonitor)
            {
                _heartBeat_engine++;
                    for (int i = 0; i < BusinessNeedPlcPoint.Dic_gatherPLC_Point.Count; i++)
                    {
                        if (!PlcConnectEntity.IsConnected)
                        {
                            PlcConnectEntity.Connect();
                            continue;
                        }
                        try
                        {
                            PLC_Point item = BusinessNeedPlcPoint.Dic_gatherPLC_Point[BusinessNeedPlcPoint.Dic_gatherPLC_Point.Keys.ElementAt(i)];
                            switch (item.plcReadType)
                            {
                                case PLC_Point_Type.T_Bool://按照bool来读取

                                    break;
                                case PLC_Point_Type.T_Byte://按照byte来读取
                                    byte[] re = PlcConnectEntity.PlcEntity.ReadBytes(DataType.DataBlock, item.DataBlock, item.DataAdress, item.Length);
                                    if (re != null && re.Length > 0)
                                    {
                                        //验证 是否为bool元素
                                        if (item.plcRealType == PLC_Point_Type.T_Bool)
                                        {
                                            string binaryStr = ByteToBinaryString(re[0]);
                                            bool value = binaryStr[7-item.BitAdress].ToString() == "1" ? true : false;
                                            if (item.value==null||value != (bool)item.value)
                                            {
                                                item.value = value;
                                                ThreadPool.QueueUserWorkItem((obj) => { pointValueChanged(item); });
                                            }
                                        }
                                        else
                                        {
                                            if (re[0] != (byte)item.value)
                                            {
                                                if (pointValueChanged != null)
                                                {
                                                    item.value = re[0];
                                                    ThreadPool.QueueUserWorkItem((obj) => { pointValueChanged(item); });
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case PLC_Point_Type.T_Int:
                                    break;
                                case PLC_Point_Type.T_Word:
                                    break;
                                case PLC_Point_Type.T_String://按照string来读取
                                    byte[] sre1 = PlcConnectEntity.PlcEntity.ReadBytes(DataType.DataBlock, item.DataBlock, item.DataAdress, 1); //获取字符串长度
                                    if (sre1 != null && sre1.Length > 0)
                                    {
                                        byte[] sre2 = PlcConnectEntity.PlcEntity.ReadBytes(DataType.DataBlock, item.DataBlock, item.DataAdress+2, sre1[0]);
                                        string str = Encoding.ASCII.GetString(sre2).Replace("\0","");
                                        if (str != (string)item.value)
                                        {
                                            if (pointValueChanged != null)
                                            {
                                                item.value = str;
                                                pointValueChanged(item);
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (Exception e)
                        {

                        if (e.HResult == -2146233088)
                        {
                            PlcConnectEntity.DisConnect();
                        }
                        MessageOutPutMethod(e.ToString());
                    }
                      
                }
                Thread.Sleep(_monitorInterval);

            }

        }

        /// <summary>
        /// 守护线程
        /// </summary>
        private void Defender()
        {
            while (_isDefender)
            {
                Thread.Sleep(30000);
                if (_heartbeat_defender != _heartBeat_engine)
                {
                    _heartbeat_defender = _heartBeat_engine;
                }
                else
                {
                    if (_isMonitor)
                    {
                        MessageOutPutMethod("Defender 检测到_heartBeat_engine 停止，重新启动监视线程StartMonitorThread");
                        StartMonitorThread();
                    }
                }
            }
        }




        private void MessageOutPutMethod(string s)
        {
            if (MessageOutput != null)
            {
                MessageOutput(s);
            }
        }


        public string ByteToBinaryString(byte data)
        {
            string str = Convert.ToString(data, 2).PadLeft(8, '0');
            return str;
        }


    }
}
