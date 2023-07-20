using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDAMaster;
using S7.Net;

namespace ScrewMachineManagementSystem.CenterControl
{
    /// <summary>
    /// 主业务类，主要实现业务流程的启动及流程的判断，外部设备IO信号的管理及接收外部设备信息
    /// </summary>
    public class BusinessMain
    {
        PLC_Monitor _plc_monitor;

        PLC_Connect _plcConnect;
        /// <summary>
        /// 需要采集的PLC点位
        /// </summary>
        List<PLC_Point> _gatherPLC_Point;

        /// <summary>
        /// 需要写入的PLC点位
        /// </summary>
        List<PLC_Point> _writePLC_Point;

        private bool _isBusinessStart = false;

        /// <summary>
        /// SN码，如果没获得或者  PLC重新开始申请SN则为空
        /// </summary>
        public string _SN_code = "";
        /// <summary>
        /// 过站信息校验是否通过， PLC重新开始申请SN则为false
        /// </summary>
        public bool _lastProcessCheck = false;
        /// <summary>
        /// 互锁信息校验是否通过， PLC重新开始申请SN则为false
        /// </summary>
        public bool _interlockCheck = false;
        /// <summary>
        /// 产品加工结果  ture 为OK  ，false为NG   PLC重新开始申请SN则为NULL
        /// </summary>
        public bool? _manufactureResult = null;

        /// <summary>
        /// 加工结果是否上传给MES， PLC重新开始申请SN则为false
        /// </summary>
        public bool _resultUpload = false;

        /// <summary>
        /// 过站信息校验，记录上一工序的名称
        /// </summary>
        public readonly string _lastProcessName = "123";

        /// <summary>
        /// 信息输出
        /// </summary>
        public event Action<string> MessageOutput;
        /// <summary>
        /// 向外部请求SN码
        /// </summary>
        public event Func<string> Need_SN_Request;
        /// <summary>
        /// 获取上一工序名称 传出的string为SN码
        /// </summary>
        public event Func<string, string> Need_lastProcessName_Request;
        /// <summary>
        /// 保存加工结果是否成功
        /// </summary>
        public event Func<bool> SaveInformationToMES_Result_Request;

        public static object obj = new object();
        /// <summary>
        /// 写入失败列表
        /// </summary>
        List<PLC_Point> _listWriteFailedPoint;


        public BusinessMain()
        {
            Initialize();
        }

        private void Initialize()
        {
            _listWriteFailedPoint = new List<PLC_Point>();
            this._gatherPLC_Point = BusinessNeedPlcPoint.GatherPLC_Point;
            this._writePLC_Point = BusinessNeedPlcPoint.WritePLC_Point;
        }

        public bool BusinessStart()
        {
            _plcConnect = new PLC_Connect(CpuType.S71200, ConfigurationKeys.PLC_IP, ConfigurationKeys.PLC_Rack, ConfigurationKeys.PLC_Slot);
            _plc_monitor = new PLC_Monitor(_plcConnect, 200);
            _plcConnect.MessageOutput -= MessageOutput;
            _plcConnect.MessageOutput += MessageOutput;

            _plcConnect.PointWriteFail -= WriteFailCallBack;
            _plcConnect.PointWriteFail += WriteFailCallBack;

            _plc_monitor.MessageOutput -= MessageOutput;
            _plc_monitor.MessageOutput += MessageOutput;

            _plc_monitor.pointValueChanged -= PointValueChanged;
            _plc_monitor.pointValueChanged += PointValueChanged;
            return _plc_monitor.Start(BusinessNeedPlcPoint.GatherPLC_Point);//启动监控


        }

        private void PointValueChanged(PLC_Point point)
        {
            lock (obj)
            {
                try
                {
                    switch (point.VarName)
                    {
                        case "SN码请求"://SN码请求 为1 时，置位所有需要写入的数据
                            if ((int)point.value == 1)
                            {
                                SN_CodeRequest();
                            }
                            break;
                        case "开始加工请求"://开始加工请求  需要向外部请求该SN码的过站信息，是否为本工序上一站
                            if ((bool)point.value == true)
                            {
                                ManufactureRequest();
                            }
                            break;
                        case "结果OK"://SN码请求 为1 时，置位所有需要写入的数据
                            if ((bool)point.value == true)
                            {
                                if ((bool)BusinessNeedPlcPoint.Dic_gatherPLC_Point["结果NG"].value)
                                {
                                    MessageOutPutMethod(string.Format("")
                                }
                            }
                            break;
                        case "结果NG"://SN码请求 为1 时，置位所有需要写入的数据
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        /// <summary>
        /// 当sn码请求置位为1时 ,清除历史数据
        /// </summary>
        public void SN_CodeRequest()
        {
            //首先清除上一次的所有指令
            BusinessNeedPlcPoint.Dic_writePLC_Point["写入SN码"].value = "\0\0\0\0\0\0\0\0";
            _plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["写入SN码"]);
            _SN_code = "";
            MessageOutPutMethod("PLC SN码已清除");

            BusinessNeedPlcPoint.Dic_writePLC_Point["允许加工请求"].value = 0;
            _plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["允许加工请求"]);

            MessageOutPutMethod("PLC 允许加工请求 已清除");

            BusinessNeedPlcPoint.Dic_writePLC_Point["禁止加工请求"].value = 0;
            _plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["禁止加工请求"]);
            MessageOutPutMethod("PLC 禁止加工请求 已清除");

            BusinessNeedPlcPoint.Dic_writePLC_Point["互锁结果"].value = 0;
            _plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["互锁结果"]);
            _interlockCheck = false;
            MessageOutPutMethod("PLC 互锁结果 已清除");

            BusinessNeedPlcPoint.Dic_writePLC_Point["加工结果收到"].value = 0;
            _plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["加工结果收到"]);
            _manufactureResult = null;
            _lastProcessCheck = false;
            _resultUpload = false;
            MessageOutPutMethod("PLC 加工结果收到 已清除");
            if (Need_SN_Request != null)//向外部请求SN码
            {
                string snCode = Need_SN_Request();
                BusinessNeedPlcPoint.Dic_writePLC_Point["写入SN码"].value = snCode;
                if (_plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["写入SN码"]))
                {
                    MessageOutPutMethod(string.Format("SN写入成功，写入的SN;{0}", snCode));
                    _SN_code = snCode;
                }
                else
                {
                    MessageOutPutMethod(string.Format("SN写入失败，需要写入的SN;{0}，请重新执行SN_CodeRequest", snCode));
                }
            }
            else
            { MessageOutPutMethod("外部未传入SN，请检查【Need_SN_Request】事件是否已订阅"); }

        }

        /// <summary>
        /// 开始加工申请逻辑
        /// </summary>
        public void ManufactureRequest()
        {
            //检查SN码是否已写入
            if (string.IsNullOrEmpty(_SN_code))
            {
                MessageOutPutMethod("收到加工请求，但SN码未写入，不处理");
                return;
            }
            if (Need_lastProcessName_Request != null)
            {
                string lastProcessIn = Need_lastProcessName_Request(_SN_code);//通过SN号获得上一工序名称
                if (lastProcessIn == _lastProcessName)
                {
                    MessageOutPutMethod(string.Format("SN:{0}  上一工序符合要求，准备开始写入加工指令", _SN_code));
                    BusinessNeedPlcPoint.Dic_writePLC_Point["禁止加工请求"].value = 0;
                    bool a = _plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["禁止加工请求"]);
                    BusinessNeedPlcPoint.Dic_writePLC_Point["允许加工请求"].value = 1;
                    bool b = _plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["允许加工请求"]);
                    if (a && b)
                    {
                        MessageOutPutMethod(string.Format("SN:{0}  PLC 允许加工请求 写入成功", _SN_code));
                    }
                    else
                    {
                        MessageOutPutMethod("PLC 允许加工请求 写入失败");
                    }
                }
                else
                {
                    BusinessNeedPlcPoint.Dic_writePLC_Point["禁止加工请求"].value = 1;
                    bool a = _plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["禁止加工请求"]);
                    bool b = false;
                    if (a)
                    {
                        BusinessNeedPlcPoint.Dic_writePLC_Point["允许加工请求"].value = 0;
                        b = _plcConnect.WriteData(BusinessNeedPlcPoint.Dic_writePLC_Point["允许加工请求"]);
                    }
                    if (b)
                    {
                        MessageOutPutMethod(string.Format("SN:{0}  PLC 禁止加工请求 写入成功", _SN_code));
                    }
                    else
                    {
                        MessageOutPutMethod(string.Format("SN:{0}  PLC 禁止加工请求 写入失败", _SN_code));
                    }
                    MessageOutPutMethod(string.Format("SN:{0}  上一工序不符合要求，传入的上一工序为：{0}，需要的上一工序为：{1}，请检查", _SN_code, lastProcessIn, _lastProcessName));

                }
            }
            else
            { MessageOutPutMethod(string.Format("SN:{0}收到加工请求，但外部未传入上一道工序名称，请检查【Need_lastProcessName_Request】事件是否已订阅",_SN_code)); }
        }

        private void WriteFailCallBack(PLC_Point p)
        {
            _listWriteFailedPoint.Add(p);
        }

        public void BusinessStop()
        {
            _plc_monitor.Stop();//停止监控


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
