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
        private static BusinessMain businessMain;
        PLC_Monitor _plc_monitor;

        public PLC_Monitor PLC_Monitor
        { get { return _plc_monitor; } }

        PLC_Connect _plcConnect;

        public PLC_Connect PLC_Connect
        { get { return _plcConnect; } }


        private bool _isBusinessStart = false;

        public bool IsBusinessStart { get { return _isBusinessStart; } }

        /// <summary>
        /// SN码，如果没获得或者  PLC重新开始申请SN则为空
        /// </summary>
        public string _SN_code = "";
        /// <summary>
        /// 允许加工还是禁止加工， PLC重新开始申请SN则为false
        /// </summary>
        public bool _isManufacturePermission = false;
        /// <summary>
        /// 互锁信息校验是否通过， PLC重新开始申请SN则为false
        /// </summary>
        public bool _interlockCheck = false;
        /// <summary>
        /// 产品加工结果  ture 为OK  ，false为NG   PLC重新开始申请SN则为NULL
        /// </summary>
        public bool _isManufactureResult = false;

        /// <summary>
        /// 加工结果是否上传给MES， PLC重新开始申请SN则为false
        /// </summary>
        public bool _isResultUpload = false;

        /// <summary>
        /// 过站信息校验，记录上一工序的名称
        /// </summary>
        public static readonly string _lastProcessName = "BYJ";

        /// <summary>
        /// 业务信息输出
        /// </summary>
        public event Action<string> MessageOutput_Business;

        /// <summary>
        /// 一般信息输出
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
        /// 保存加工结果是否成功,传出1的string为SN码,传出2的string为加工结果
        /// </summary>
        public event Func<string, string, bool> SaveInformationToMES_Result_Request;

        /// <summary>
        /// 清理电批数据请求
        /// </summary>
        public event Func<bool> Need_ClearScrewData;
        /// <summary>
        /// 业务启动成功
        /// </summary>
        public event Action BusinessStartedEvent;
        /// <summary>
        /// 业务停止
        /// </summary>
        public event Action BusinessStopedEvent;


        public static object obj = new object();
        /// <summary>
        /// 写入失败列表
        /// </summary>
        List<PLC_Point> _listWriteFailedPoint;
        /// <summary>
        /// 数据点写入失败时的最大复写次数
        /// </summary>
        private readonly int _maxWriteCount = 5;
        /// <summary>
        /// 写入失败后，下次写入的间隔  ms
        /// </summary>
        private readonly int _maxWriteInterval = 200;
        /// <summary>
        /// 同时注册的实例
        /// </summary>
        private static int _registerID = 0;
        /// <summary>
        /// 同时注册的实例
        /// </summary>
        public static int RegisterID { get { return _registerID; } }

        private BusinessMain()
        {
            Initialize();
        }
        public static BusinessMain GetInstance()
        {
            if (businessMain == null)
            {
                businessMain = new BusinessMain();
                _registerID = 0;
            }
            _registerID++;
            return businessMain;
        }

        public void Dispose()
        {
            _registerID--;
            if (_registerID <= 0)
            {
                if (_plc_monitor != null)
                {
                    _plc_monitor.Stop();//停止监控\
                    _plc_monitor = null;
                }
                if (_plcConnect != null)
                {
                    _plcConnect.DisConnect();
                    _plcConnect = null;
                }
                if (BusinessStopedEvent != null)
                {
                    BusinessStopedEvent();
                }
                _isBusinessStart = false;
                businessMain = null;
            }



        }


        private void Initialize()
        {
            _listWriteFailedPoint = new List<PLC_Point>();
            // _plcConnect = new PLC_Connect(CpuType.S71200, ConfigurationKeys.PLC_IP, ConfigurationKeys.PLC_Rack, ConfigurationKeys.PLC_Slot);
            _plcConnect = new PLC_Connect(CpuType.S71200, ConfigurationKeys.PLC_IP, ConfigurationKeys.PLC_Rack, ConfigurationKeys.PLC_Slot);
            _plc_monitor = new PLC_Monitor(_plcConnect, 200);
        }

        public bool BusinessStart()
        {
            if (_isBusinessStart)
            {
                MessageOutputMethod("Business already  Start ");
                return true;
            }
            ////检查实例
            //if (_plcConnect==null)
            //{
            //    _plcConnect = new PLC_Connect(CpuType.S71200, ConfigurationKeys.PLC_IP, ConfigurationKeys.PLC_Rack, ConfigurationKeys.PLC_Slot);
            //}
            //if (_plc_monitor==null)
            //{
            //    _plc_monitor = new PLC_Monitor(_plcConnect, 200);
            //}

            _plcConnect.MessageOutput -= MessageOutput;
            _plcConnect.MessageOutput += MessageOutput;

            _plcConnect.PointWriteFail -= WriteFailCallBack;
            _plcConnect.PointWriteFail += WriteFailCallBack;



            _plc_monitor.MessageOutput -= MessageOutput;
            _plc_monitor.MessageOutput += MessageOutput;

            _plc_monitor.pointValueChanged -= PointValueChanged;
            _plc_monitor.pointValueChanged += PointValueChanged;
            MessageOutputMethod("正在启动PLC点位监控服务...");
            bool a = _plc_monitor.Start();//启动监控
            if (a)
            {
                _isBusinessStart = true;
                if (BusinessStartedEvent != null)
                {
                    BusinessStartedEvent();
                }
            }
            return a;

        }

        private void PointValueChanged(PLC_Point point)
        {
            //lock (obj)
            //{
            try
            {
                switch (point.VarName)
                {
                    case "SN码请求"://SN码请求 为1 时，置位所有需要写入的数据
                        if ((bool)point.value == true)
                        {
                            MessageOutput_BusinessMethod(string.Format("PLC:{0} 发出SN码请求", PLC_Connect.PlcEntity.IP));
                            SN_CodeRequest(point);
                        }
                        else
                        { MessageOutput_BusinessMethod(string.Format("SN:{0} SN码请求信号 复位  0", _SN_code)); }
                        break;
                    case "开始加工请求"://开始加工请求  , 需要向外部请求该SN码的过站信息，是否为本工序上一站
                        if ((bool)point.value == true)
                        {
                            MessageOutput_BusinessMethod(string.Format("SN:{0} 发出  开始加工请求", _SN_code));
                            ManufactureRequest(point);
                        }
                        else
                        { MessageOutput_BusinessMethod(string.Format("SN:{0} 开始加工请求信号 复位  0", _SN_code)); }
                        break;
                    case "结果OK"://结果OK 为1 时，置位所有需要写入的数据
                        if ((bool)point.value == true)
                        {
                            MessageOutput_BusinessMethod(string.Format("SN:{0} 加工完成，加工结果：OK", _SN_code));
                            ManufactureResultOutput();
                        }
                        else
                        { MessageOutput_BusinessMethod(string.Format("SN:{0} 结果OK信号  复位  0", _SN_code)); }
                        break;
                    case "结果NG"://SN码请求 为1 时，置位所有需要写入的数据
                        if ((bool)point.value == true)
                        {
                            MessageOutput_BusinessMethod(string.Format("SN:{0} 加工完成，加工结果：NG", _SN_code));
                            ManufactureResultOutput();
                        }
                        else
                        { MessageOutput_BusinessMethod(string.Format("SN:{0} 结果NG信号  复位  0", _SN_code)); }
                        break;
                    case "表格清空"://表格清空 为1 时清螺丝机电批数据
                        if ((bool)point.value == true)
                        {
                            MessageOutput_BusinessMethod("表格清空信号=1  ，准备清空电批数据");
                            ClearScrewTableData();//置为1时清理电批数据
                            //MessageOutPutMethod("表格清空,电批表格数据已清空");
                        }
                        else//置为 0时将 表格已清空信号 复位为0
                        {
                            ResetScrewDataClearSignal();
                            MessageOutput_BusinessMethod("表格清空,电批表格数据已清空,信号复位");
                        }
                        break;
                    case "SN码":
                        _SN_code = (string)point.value;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageOutputMethod(string.Format("SN:{0}  pointName:{1}  执行时发生错误 ，ex={2}", _SN_code, point.VarName, ex.ToString()));
            }

            //}
        }

        /// <summary>
        /// 表格已清空 信号的写入与复位
        /// </summary>
        /// <param name="value"></param>
        public bool ClearScrewTableData()
        {
            MessageOutput_BusinessMethod("收到表格清空请求,清理上次的遗留状态，清理开始...");
            ClearPointData_Reset0();
            if (Need_ClearScrewData != null)
            {
                if (Need_ClearScrewData())
                {
                    BusinessNeedPlcPoint.Dic_gatherPLC_Point["表格已清空"].value = true;
                    if (WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["表格已清空"]))//写入成功 
                    {
                        MessageOutput_BusinessMethod("电批数据已清空");
                        return true;
                    }
                    else
                    {
                        MessageOutput_BusinessMethod("电批数据显示反馈结果设置失败，准备重新发起电批数据清理，表格清空  需为 1");
                        BusinessNeedPlcPoint.Dic_gatherPLC_Point["表格清空"].value = false;
                        return false;
                    }
                }
                else
                {
                    MessageOutput_BusinessMethod("电批数据显示清理失败，准备重新发起电批数据清理，表格清空  需为 1");
                    BusinessNeedPlcPoint.Dic_gatherPLC_Point["表格清空"].value = false;
                    return false;
                }
            }
            else
            {
                MessageOutput_BusinessMethod("未执行清理电批数据任务，请检查【Need_ClearScrewData】事件是否已订阅");
                return false;
            }
        }

        private void ResetScrewDataClearSignal()
        {
            BusinessNeedPlcPoint.Dic_gatherPLC_Point["表格已清空"].value = false;
            if (WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["表格已清空"]))//写入成功 
            {
                MessageOutput_BusinessMethod("[表格已清空] 信号复位为  0  成功");
            }
            else
            {
                MessageOutput_BusinessMethod("[表格已清空] 信号复位为  0  失败 ，准备重新写入");
                BusinessNeedPlcPoint.Dic_gatherPLC_Point["表格清空"].value = false;
            }


        }
        /// <summary>
        /// 加工结果输出
        /// </summary>
        private void ManufactureResultOutput()
        {
            if ((bool)BusinessNeedPlcPoint.Dic_gatherPLC_Point["结果NG"].value == true && (bool)BusinessNeedPlcPoint.Dic_gatherPLC_Point["结果OK"].value == true)
            {
                MessageOutput_BusinessMethod(string.Format("SN:{0} OK NG 同时输出", _SN_code));
            }
            else
            {
                if (string.IsNullOrEmpty(_SN_code))
                {
                    MessageOutput_BusinessMethod("SN码未输入时检测到 OK / NG 信号输出，不处理");
                    return;
                }
                if ((bool)BusinessNeedPlcPoint.Dic_gatherPLC_Point["允许加工请求"].value != true)
                {
                    MessageOutput_BusinessMethod("不允许加工时， OK / NG 信号输出，不处理");
                    return;
                }
                string result = (bool)BusinessNeedPlcPoint.Dic_gatherPLC_Point["结果NG"].value ? "NG" : "OK";

                if (SaveInformationToMES_Result_Request != null)
                {
                    bool a = SaveInformationToMES_Result_Request(_SN_code, result);

                    ManufactureResultWriteToPLC(_SN_code, a, false);

                }
                else
                { MessageOutput_BusinessMethod("向MES传递并保存信息失败，请检查【SaveInformationToMES_Result_Request】事件是否已订阅"); }

            }
        }
        /// <summary>
        /// 将加工结果记录完成后，向PLC写入记录完成信号
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="result"></param>
        /// <param name="isForceWrite">强制写入时不再关注加工记录是否记录完成</param>
        /// <returns></returns>
        public bool ManufactureResultWriteToPLC(string SN, bool result, bool isForceWrite = false)
        {
            //校验SN码
            if (BusinessNeedPlcPoint.Dic_gatherPLC_Point["SN码"].value != null && (string)BusinessNeedPlcPoint.Dic_gatherPLC_Point["SN码"].value != SN && !isForceWrite)
            {
                MessageOutput_BusinessMethod("SN码校验失败， PLC 加工结果收到 未写入PLC");
                return false;
            }
            if (result)
            {
                BusinessNeedPlcPoint.Dic_gatherPLC_Point["加工结果收到"].value = true;
                if (WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["加工结果收到"]))//写入成功 
                {
                    MessageOutput_BusinessMethod("PLC 加工结果收到 写入成功");
                    return true;
                }
                else
                {
                    MessageOutput_BusinessMethod("PLC 加工结果收到 写入失败");
                    return false;
                }

            }
            else
            { MessageOutput_BusinessMethod("加工结果记录失败，不向PLC写入记录完成信号"); return false; }

        }

        /// <summary>
        /// 当sn码请求置位为1时 ,清除历史数据
        /// </summary>
        public void SN_CodeRequest(PLC_Point point)
        {
            MessageOutput_BusinessMethod("SN码写入前,清理上次的遗留状态，清理开始...");
            ClearPointData_Reset0();


            if (Need_SN_Request != null)//向外部请求SN码
            {
                string snCode = Need_SN_Request();
                if (!string.IsNullOrEmpty(_SN_code))//如果
                {
                    MessageOutput_BusinessMethod(string.Format("已获得SN:{0} ,本次传入的SN:{1}不生效", _SN_code, snCode));
                    return;
                }
                //if (string.IsNullOrEmpty(snCode)) //取消SN空值检测
                //{
                //    MessageOutPutMethod("外部传入SN为空，准备重新发起SN申请，SN码请求  需为 1");
                //    point.value = false;
                //}
                else
                {
                    if (!WriteSN_ToPLC(snCode))
                    {
                        MessageOutput_BusinessMethod("SN写入失败，准备重新发起SN申请，SN码请求  需为 1");
                        point.value = false;
                    }

                }
            }
            else
            { MessageOutput_BusinessMethod("外部未传入SN，请检查【Need_SN_Request】事件是否已订阅"); }

        }

        /// <summary>
        /// 清理点位的数据，使其置位为0
        /// </summary>
        public bool ClearPointData_Reset0()
        {
            //首先清除上一次的所有指令
            BusinessNeedPlcPoint.Dic_gatherPLC_Point["表格已清空"].value = false;
           bool a=  WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["表格已清空"]);
            MessageOutput_BusinessMethod("表格已清空 已清除");

            BusinessNeedPlcPoint.Dic_gatherPLC_Point["SN码"].value = null;
            bool b = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["SN码"]);

            MessageOutput_BusinessMethod("PLC SN码已清除");

            BusinessNeedPlcPoint.Dic_gatherPLC_Point["允许加工请求"].value = false;
            bool c= WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["允许加工请求"]);

            MessageOutput_BusinessMethod("PLC 允许加工请求 已清除");

            BusinessNeedPlcPoint.Dic_gatherPLC_Point["禁止加工请求"].value = false;
            bool d = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["禁止加工请求"]);
            MessageOutput_BusinessMethod("PLC 禁止加工请求 已清除");

            BusinessNeedPlcPoint.Dic_gatherPLC_Point["互锁结果"].value = false;
            bool e = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["互锁结果"]);
            _interlockCheck = false;
            MessageOutput_BusinessMethod("PLC 互锁结果 已清除");

            BusinessNeedPlcPoint.Dic_gatherPLC_Point["加工结果收到"].value = false;
            bool f = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["加工结果收到"]);
            _SN_code = "";
            _isManufacturePermission = false;
            _isResultUpload = false;
            _isManufactureResult = false;
            MessageOutput_BusinessMethod("PLC 加工结果收到 已清除");

            if (a && b && c && d && e && f)
            {
                return true;
            }
            else
            { return false; }
        }

        /// <summary>
        /// 将SN码写入PLC
        /// </summary>
        /// <param name="snCode"></param>
        /// <returns></returns>
        public bool WriteSN_ToPLC(string snCode)
        {
            BusinessNeedPlcPoint.Dic_gatherPLC_Point["SN码"].value = snCode;
            if (WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["SN码"]))
            {
                MessageOutput_BusinessMethod(string.Format("SN写入成功，写入的SN;{0}", snCode));
                _SN_code = snCode;
                return true;
            }
            else
            {
                MessageOutput_BusinessMethod(string.Format("SN写入失败，需要写入的SN;{0}，请重新执行SN_CodeRequest", snCode));
                return false;
            }
        }

        /// <summary>
        /// 开始加工申请逻辑
        /// </summary>
        public void ManufactureRequest(PLC_Point point)
        {
            //检查SN码是否已写入
            if (string.IsNullOrEmpty(_SN_code))
            {
                MessageOutput_BusinessMethod("收到加工请求，但SN码未写入，不处理");
                point.value = false;
                System.Threading.Thread.Sleep(1000);
                return;
            }
            if (Need_lastProcessName_Request != null)
            {
                string lastProcessIn = Need_lastProcessName_Request(_SN_code);//通过SN号获得上一工序名称
                if (_isManufacturePermission)
                {
                    MessageOutput_BusinessMethod(string.Format("SN:{0} ,开始加工已申请，本次申请不生效", _SN_code));
                    return;
                }
                if (string.IsNullOrEmpty(lastProcessIn))
                {
                    MessageOutput_BusinessMethod("外部传入的上一工序为空，准备重新发起工序校验申请，是否加工  需为 1");
                    System.Threading.Thread.Sleep(1000);
                    point.value = false;
                    return;
                }
                if (!LastProcessNameCheck(lastProcessIn))
                {
                    MessageOutput_BusinessMethod("写入工序校验结果失败，准备重新发起工序校验申请，是否加工  需为 1");
                    System.Threading.Thread.Sleep(1000);
                    point.value = false;
                }
                else
                { _isManufacturePermission = true; }
            }
            else
            { MessageOutput_BusinessMethod(string.Format("SN:{0}收到加工请求，但外部未传入上一道工序名称，请检查【Need_lastProcessName_Request】事件是否已订阅", _SN_code)); }
        }
        /// <summary>
        /// 进行上道工序检查，并向PLC写入联锁结果
        /// </summary>
        /// <param name="lastProcessName"></param>
        /// <param name="isForceWrite">true：忽略上道工序检查，直接确认联锁并允许设备加工</param>
        /// <returns></returns>
        public bool LastProcessNameCheck(string lastProcessName, bool isForceWrite = false)
        {
            if (lastProcessName == _lastProcessName || isForceWrite)
            {
                MessageOutput_BusinessMethod(string.Format("SN:{0}  上一工序符合要求，准备开始写入加工指令", _SN_code));
                BusinessNeedPlcPoint.Dic_gatherPLC_Point["禁止加工请求"].value = false;
                bool a = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["禁止加工请求"]);
                BusinessNeedPlcPoint.Dic_gatherPLC_Point["允许加工请求"].value = true;
                bool b = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["允许加工请求"]);
                BusinessNeedPlcPoint.Dic_gatherPLC_Point["互锁结果"].value = false;
                bool c = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["互锁结果"]);
                if (a && b && c)
                {
                    MessageOutput_BusinessMethod(string.Format("SN:{0}  PLC 允许加工请求 写入成功", _SN_code));
                    //_manufacturePermission = true;
                    return true;
                }
                else
                {
                    MessageOutput_BusinessMethod("PLC 允许加工请求 写入失败");
                    return false;
                }
            }
            else
            {
                MessageOutput_BusinessMethod(string.Format("产品工序校验失败！  SN:{0}  需要的上道工序为{1} ,实际上道工序为：{2}", _SN_code, _lastProcessName, lastProcessName));
                BusinessNeedPlcPoint.Dic_gatherPLC_Point["禁止加工请求"].value = true;
                bool a = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["禁止加工请求"]);
                bool b = false;
                BusinessNeedPlcPoint.Dic_gatherPLC_Point["互锁结果"].value = true;
                bool c = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["互锁结果"]);
                if (a)
                {
                    BusinessNeedPlcPoint.Dic_gatherPLC_Point["允许加工请求"].value = false;
                    b = WriteData_RetryLimit5(BusinessNeedPlcPoint.Dic_gatherPLC_Point["允许加工请求"]);
                }
                if (b && c)
                {
                    MessageOutput_BusinessMethod(string.Format("SN:{0}  PLC 禁止加工请求 写入成功", _SN_code));
                    return true;

                }
                else
                {
                    MessageOutput_BusinessMethod(string.Format("SN:{0}  PLC 禁止加工请求 写入失败", _SN_code));
                }
                MessageOutput_BusinessMethod(string.Format("SN:{0}  上一工序不符合要求，传入的上一工序为：{0}，需要的上一工序为：{1}，请检查", _SN_code, lastProcessName, _lastProcessName));
                return false;

            }
        }


        /// <summary>
        /// 写入数据，如果写入失败则尝试再次写入
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool WriteData_RetryLimit5(PLC_Point p)
        {
            bool isWriteSuccess = false; int writecount = 0;
            while (!isWriteSuccess && writecount < _maxWriteCount)
            {
                isWriteSuccess = _plcConnect.WriteData(p);
            }
            System.Threading.Thread.Sleep(_maxWriteInterval);

            return isWriteSuccess;
        }


        private void WriteFailCallBack(PLC_Point p)
        {
            _listWriteFailedPoint.Add(p);
        }

        public void BusinessStop()
        {
            if (_plc_monitor != null)
            {
                _plc_monitor.Stop();//停止监控\
            }
            if (_plcConnect != null)
            {
                _plcConnect.DisConnect();
            }




        }


        private void MessageOutput_BusinessMethod(string s)
        {
            if (MessageOutput_Business != null)
            {
                MessageOutput_Business(s);
            }
        }

        private void MessageOutputMethod(string s)
        {
            if (MessageOutput != null)
            {
                MessageOutput(s);
            }
        }


    }
}
