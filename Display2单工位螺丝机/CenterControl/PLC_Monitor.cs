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
            _isDefender = true;
            StartDefenderThread();
            _isMonitor = true;
            StartMonitorThread();
            if (!ConnectPlc())
            {
                return false;
            };


            return true;
        }

        private bool ConnectPlc()
        {
            try
            {
                if ((PlcConnectEntity != null))
                {
                    return PlcConnectEntity.Connect();

                }
                else if (PlcConnectEntity == null)
                {
                    MessageOutPutMethod("PLC_Monitor--Start _plcConnectEntity is null", 0);
                    return false;
                }
                return true;
            }
            catch (Exception es)
            {
                MessageOutPutMethod("PLC_Monitor--Start  ConnectPlc error ,ex=" + es.ToString(), 1);
                return false;
            }
        }

        public bool Stop()
        {
            _isDefender = false;
            _isMonitor = false;
            return true;
        }
        public void Dispose()
        {

            try
            {
                _isDefender = false;
                _isMonitor = false;
                if (_thread_defend != null)
                {
                    try
                    {
                        _thread_defend.Abort();
                    }
                    catch (Exception)
                    {
                    }
                }
                _plcConnectEntity.DisConnect();
            }
            catch (Exception)
            {
            }
            finally
            { _plcConnectEntity = null; }

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
            MessageOutPutMethod("PLC 守护线程已启动", 0);
        }

        /// <summary>
        /// 监控变量的主线程
        /// </summary>
        private void Monitor()
        {
            while (_isMonitor)
            {
                for (int i = 0; i < BusinessNeedPlcPoint.Dic_gatherPLC_Point.Count; i++)
                {
                    if (!PlcConnectEntity.IsConnected)
                    {
                       // PlcConnectEntity.Connect();
                        continue;
                    }
                    try
                    {
                        PLC_Point item = BusinessNeedPlcPoint.Dic_gatherPLC_Point[BusinessNeedPlcPoint.Dic_gatherPLC_Point.Keys.ElementAt(i)];

                        if (PlcConnectEntity.ReadData(ref item))
                        {
                            _heartBeat_engine++;
                            if (pointValueChanged != null && item.isValueChanged)
                            {
                                ThreadPool.QueueUserWorkItem((obj) => { pointValueChanged(item); });
                            }
                        } ;

                    }
                    catch (Exception e)
                    {

                        if (e.HResult == -2146233088)
                        {
                            PlcConnectEntity.DisConnect();
                        }
                        MessageOutPutMethod(e.ToString(), 1);
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
            Thread.Sleep(30000);
            while (_isDefender)
            {
                Thread.Sleep(5000);
                if (_heartbeat_defender != _heartBeat_engine)
                {
                    _heartbeat_defender = _heartBeat_engine;
                }
                else
                {
                    if (PlcConnectEntity.IsConnecting)
                    {
                        continue;
                    }
                    if (_isMonitor)
                    {
                        MessageOutPutMethod("PLC Defender 检测到 心跳信号 停止，重新尝试连接PLC", 1);
                        try
                        {
                            PlcConnectEntity.DisConnect();
                            PlcConnectEntity.Connect();
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        { StartMonitorThread(); }

                    }
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




    }
}
