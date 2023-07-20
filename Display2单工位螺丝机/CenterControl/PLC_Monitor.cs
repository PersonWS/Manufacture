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
        /// <summary>
        /// 需要进行监视的点位
        /// </summary>
        private List<PLC_Point> _listForMonitorPoint;

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

        public bool Start(List<PLC_Point> monitorPoint)
        {
            if (!ConnectPlc())
            {
                return false;
            };
            _isMonitor = true;
            _isDefender = true;
            this._listForMonitorPoint = monitorPoint;
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
                    PlcConnectEntity.Connect();
                }
                else if (PlcConnectEntity == null)
                {
                    MessageOutPutMethod("PLC_Monitor--Start _plcConnectEntity is null");
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
            _thread_defend.Name = "thread_engine";
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
                try
                {
                    if (!PlcConnectEntity.PlcEntity.IsConnected)
                    {
                        PlcConnectEntity.Connect();
                        continue;
                    }
                    foreach (var i in BusinessNeedPlcPoint.Dic_gatherPLC_Point)
                    {
                        PLC_Point item = i.Value;
                        switch (item.plcReadType)
                        {
                            case PLC_Point_Type.T_Bool://按照bool来读取
                                
                                break;
                            case PLC_Point_Type.T_Byte://按照byte来读取
                                Task<byte[]> re = PlcConnectEntity.PlcEntity.ReadBytesAsync(DataType.DataBlock, item.DataBlock, item.DataAdress, item.Length);
                                if (re.Result != null && re.Result.Length > 0 && re.Result[0] != (byte)item.value)
                                {
                                    if (pointValueChanged != null)
                                    {
                                        item.value = re.Result[0];
                                        ThreadPool.QueueUserWorkItem((obj) => { pointValueChanged(item); });
                                    }
                                }
                                break;
                            case PLC_Point_Type.T_Int:
                                break;
                            case PLC_Point_Type.T_Word:
                                break;
                            case PLC_Point_Type.T_String://按照string来读取
                                Task<byte[]> sre1 = PlcConnectEntity.PlcEntity.ReadBytesAsync(DataType.DataBlock, item.DataBlock, item.DataAdress, 1); //获取字符串长度
                                if (sre1.Result != null && sre1.Result.Length > 0 && sre1.Result[0] != (byte)item.value)
                                {
                                    Task<byte[]> sre2 = PlcConnectEntity.PlcEntity.ReadBytesAsync(DataType.DataBlock, item.DataBlock, item.DataAdress, sre1.Result[0]);
                                    string str = Encoding.ASCII.GetString(sre2.Result);
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
                }
                catch (Exception)
                {

                }

            }

        }

        /// <summary>
        /// 守护线程
        /// </summary>
        private void Defender()
        {
            while (_isDefender)
            {
                Thread.Sleep(10000);
                if (_heartbeat_defender != _heartBeat_engine)
                {
                    _heartbeat_defender = _heartBeat_engine;
                }
                else
                {
                    if (_isMonitor)
                    {
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



    }
}
