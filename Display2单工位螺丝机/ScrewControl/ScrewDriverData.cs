using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem
{
    /// <summary>
    /// 电批应答数据解析类
    /// </summary>
    class ScrewDriverData_ACK
    {
        private bool isDataLegal;
        /// <summary>
        /// 原始数据
        /// </summary>
        byte[] dataOriginal;

        /// <summary>
        /// 01 报文头部，用于识别报文是否正确
        /// </summary>
        private readonly byte frameHeader = 0x02;
        /// <summary>
        /// 02 接下来的数据长度，不含尾帧
        /// </summary>
        public int dataLength;
        /// <summary>
        /// 03 模式， R / W : 读/写   A / T ：应答/传送  CHAR
        /// </summary>
        public ModeType modeType;

        /****************接下来为数据域****************/
        /// <summary>
        /// 05 MID号，占4字节  CHAR
        /// 0001：建立连接  0002：断开连接
        /// 0201：电批运行状态   Ready/Running/OK/NG/Err
        /// 0202：最终拧紧结果  0203：实时曲线数据     0204：Pset数据读取
        /// </summary>
        public MIDType MIDType;

        public string data;
        /// <summary>
        /// 解析后的数据，根据MID来区分
        /// </summary>
        public object analysisData;

        public bool IsDataLegal
        {
            get
            {
                if (dataOriginal == null || dataOriginal.Length < 11 || dataOriginal[0] != 0x02)
                {
                    return false;
                }
                else
                { return true; };
            }
        }

        public ScrewDriverData_ACK(byte[] data)
        {
            if (data != null)
            {
                data.CopyTo(this.dataOriginal = new byte[data.Length], 0);
            }
 ;
        }

        public ScrewDriverData_ACK ScrewDriverData_Analysis()
        {
            if (!IsDataLegal)
            {
                LogUtility.ErrorLog_custom("ScrewDriverData_Analysis 检测到数据异常   data:" + BitConverter.ToString(this.dataOriginal));
                return null;
            }
            dataLength = BitConverter.ToInt32(this.dataOriginal.Skip(1).Take(4).Reverse().ToArray(), 0); byte[] b = this.dataOriginal.Skip(1).Take(4).ToArray();
            if (dataLength != this.dataOriginal.Length - 6)//如果数据帧异常，则抛弃数据
            {
                LogUtility.ErrorLog_custom(string.Format("ScrewDriverData_Analysis 检测到数据长度异常,报文要求长度{0},实际长度：{1}   data:{2}", dataLength, this.dataOriginal.Length - 6, BitConverter.ToString(this.dataOriginal)));
                return null;
            }
            if (!Enum.TryParse<ModeType>(Encoding.ASCII.GetString(new byte[] { this.dataOriginal[5] }), out modeType))
            {
                LogUtility.ErrorLog_custom(string.Format("ScrewDriverData_Analysis 检测到数据应答模式错误,应答模式为：{0}", Encoding.ASCII.GetString(new byte[] { this.dataOriginal[5] })));
                return null;
            }
            if (!Enum.TryParse<MIDType>(Encoding.ASCII.GetString(this.dataOriginal.Skip(6).Take(4).ToArray()), out MIDType))
            {
                LogUtility.ErrorLog_custom(string.Format("ScrewDriverData_Analysis  MID类型错误,报文的MID值为：{0}", Encoding.ASCII.GetString(this.dataOriginal.Skip(6).Take(4).ToArray())));
                return null;
            }

            data = Encoding.ASCII.GetString(this.dataOriginal.Skip(10).Take(dataLength - 5).ToArray());

            switch (modeType)
            {
                case ModeType.R:
                    break;
                case ModeType.W:
                    break;
                case ModeType.A:
                    break;
                case ModeType.T:
                    switch (MIDType)
                    {
                        case MIDType.Connect:
                            break;
                        case MIDType.DisConnect:
                            break;
                        case MIDType.DownloadPestToScrew:
                            break;
                        case MIDType.PestSelect:
                            break;
                        case MIDType.ScrewRunState:
                            ScrewRunState ScrewRunState = new ScrewRunState(data);
                            analysisData = ScrewRunState;
                            break;
                        case MIDType.ScrewWorkResult:
                            ScrewWorkResult screwWorkResult = new ScrewWorkResult(data);
                            analysisData = screwWorkResult;
                            break;
                        case MIDType.ScrewWorkCurve:
                            break;
                        case MIDType.ScrewPset:
                            break;
                        case MIDType.EngineEnable:
                            break;
                        default:
                            break;
                    }

                    break;
                case ModeType.NULL:
                    break;
            }
            return this;


        }


    }



    public enum ModeType
    {
        R = 1,
        W = 2,
        A = 3,
        T = 4,
        NULL = 5
    }

    public enum MIDType
    {
        /// <summary>
        /// 创建连接
        /// </summary>
        Connect = 0001,
        /// <summary>
        /// 断开连接
        /// </summary>
        DisConnect = 0002,
        /// <summary>
        /// 下载数据到电批
        /// </summary>
        DownloadPestToScrew = 0102,
        PestSelect = 0103,
        /// <summary>
        /// 电批运行状态
        /// </summary>
        ScrewRunState = 0201,
        /// <summary>
        /// 电批拧紧结果
        /// </summary>
        ScrewWorkResult = 0202,
        /// <summary>
        /// 工作曲线
        /// </summary>
        ScrewWorkCurve = 0203,
        ScrewPset = 0204,
        /// <summary>
        /// 电机使能
        /// </summary>
        EngineEnable = 0301
    }


    public class ScrewRunState
    {
        string data;
        //001   Ready ：1= 准备运行    Running ：1= 正在运行    OK ：1= 拧紧合格   NG：1= 拧紧不合格
        /// <summary>
        ///  Ready ：1= 准备运行    Running ：1= 正在运行 
        /// </summary>
       public string runState;
        /// <summary>
        /// OK ：1= 拧紧合格   NG：1= 拧紧不合格
        /// </summary>
        public string workResult;

        //002  SysErr：0= 设备正常；1= 设备故障  SysErrID：
        //1= 欠压；2= 过压 ;3= 编码器断线; 4= 峰值过流；5= 编码器通讯异常 ; 6= 速度过速；7= 速度超差 ；8= 编码器电压异常；9=PCB 过温
        //10= 故障1；11=IPM 报警；12= 故障2；13= 故障3；14= 故障4；15= 故障5；16= 摩擦力矩异常
        /// <summary>
        /// SysErr：0= 设备正常；1= 设备故障  SysErrID：
        /// </summary>
        public string sysErr;
        /// <summary>
        /// 1= 欠压；2= 过压 ;3= 编码器断线; 4= 峰值过流；5= 编码器通讯异常 ; 6= 速度过速；7= 速度超差 ；8= 编码器电压异常；9=PCB 过温
        ///10= 故障1；11=IPM 报警；12= 故障2；13= 故障3；14= 故障4；15= 故障5；16= 摩擦力矩异常
        /// </summary>
        public string sysErrID;

        //003  运行故障状态 （0：正常；1：故障）1. 参数设置失败  2. 控制超时  3. 无意义，RSV

        public string faultState;

        public ScrewRunState(string s)
        { this.data = s; Analysis(); }

        public string GetStatus()
        {
            string str = workResult+"  "+ runState ;
            if (sysErr != "设备正常")
            {
                str += string.Format("sysErr:{0} ,sysErrID:{1}", sysErr, sysErrID);
            }
            return str;
        }

        private void Analysis()
        {
            if (data.Split(';').Length < 3)
            {
                LogUtility.ErrorLog_custom(string.Format("设备状态数据异常 ，异常数据：{0}", data));
                return;
            }
            string[] strA = data.Split(';');
            //运行状态：
            string[] strB = strA[0].Split(',');
            if (strB.Length == 4)
            {
                strB[0] = strB[0].Replace("001=", "");
                if (strB[0] == "1" && strB[1] == "0")
                {
                    runState = "准备运行";
                }
                else if (strB[0] == "0" && strB[1] == "1")
                { runState = "正在运行"; }
                else
                { LogUtility.ErrorLog_custom(string.Format("设备状态异常，获得编码为：{0},{1}", strB[0], strB[1])); }
                if (strB[2] == "1" && strB[3] == "0")
                {
                    workResult = "拧紧合格";
                }
                else if (strB[2] == "0" && strB[3] == "1")
                { workResult = "拧紧不合格"; }
                else
                { workResult = ""; }
            }
            string[] strC = strA[1].Split(',');
            if (strC.Length == 2)
            {
                strC[0] = strC[0].Replace("002=", "");
                if (strC[0] == "0")
                {
                    sysErr = "设备正常";
                }
                else
                {
                    sysErr = "设备故障";
                    sysErrID = strC[2];

                }
            }
            string[] strD = strA[2].Split(',');
            if (strD.Length == 3)
            {
                strD[0] = strD[0].Replace("003=", "");
                if (strD[0] == "0")
                {
                    faultState = "设备正常";
                }
                else
                {
                    faultState = "设备故障:";
                    if (strD[1] == "1")
                    {
                        faultState += "参数设置失败";
                    }
                    else if (strD[2] == "1")
                    { faultState += "控制超时";  }

                }
            }
            ///001=1,0,1,0;002=0,0;003=0,0,0;
        }
    }


    public class ScrewWorkResult
    {
        string dataOriginal;

        public ScrewWorkResult_StageResult workResult;

        public string workResultState;

        public string ngCode;

        public List<ScrewWorkResult_StageResult> StageResultList = new List<ScrewWorkResult_StageResult>();

        public ScrewWorkResult(string s)
        { this.dataOriginal = s; Analysis(); }

        private void Analysis()
        {
            string[] strA = dataOriginal.Split(';');
            if (strA[0].Contains("00010"))
            {
                workResult = new ScrewWorkResult_StageResult(null, strA[0],strA[1]);
            }
            //解析NG结果
            if (strA[2].Contains("00012"))
            {
                if (strA[2].Split('=').Length==2)
                {
                    switch (strA[2].Split('=')[1])
                    {
                        case "00":
                            ngCode = "";
                            break;

                        case "01":
                            ngCode = "最终扭矩过大";
                            break;
                        case "02":
                            ngCode = "最终扭矩过大";
                            break;
                        case "03":
                            ngCode = "最终角度过小";
                            break;
                        case "04":
                            ngCode = "最终角度过小";
                            break;
                        default:
                            ngCode = "";
                            break;
                    }
                }
            }
            //解析过程结果
            if (strA.Length>=13)
            {
                for (int i = 0; i < 5; i++)
                {
                    ScrewWorkResult_StageResult r = new ScrewWorkResult_StageResult((i+1).ToString(),strA[2 * i + 3], strA[2 * i + 4]);
                    StageResultList.Add(r);
                }
            }
            
        }
        ///00010=0.603,1275.977,1.648,1636.367;00011=1;00012=00;01010=0.136,-362.682,0.276;01011=1;01020=0.000,0.000,0.000;01021=1;01030=0.156,540.872,0.681;01031=1;01040=0.488,1373.380,0.535;01041=1;01050=0.603,87.663,0.123;01051=1;
    }

    public class ScrewWorkResult_StageResult
    {
        string dataOriginal1;

        string dataOriginal2;

        public string stage;

        public string Torque;

        public string MonitorAngle;

        public string Angle;

        public string Time;

        public string result;

        public ScrewWorkResult_StageResult(string stage, string s1,string s2)
        { this.stage = stage; this.dataOriginal1 = s1; this.dataOriginal2 = s2; Analysis(); }

        private void Analysis()
        {
            string[] strSpilt = dataOriginal1.Split('=');
            if (strSpilt.Length!=2)
            {
                return;
            }
            string[] strA = strSpilt[1].Split(',');
            if (strA.Length < 3)
            {
                return;
            }
            else if (strA.Length == 3)
            {
                Torque = strA[0];
                Angle = strA[1];
                Time = strA[2];
            }
            else if (strA.Length == 4)
            {
                Torque = strA[0];
                MonitorAngle = strA[1];
                Time = strA[2];
                Angle = strA[3];
            }

            string [] strB = dataOriginal2.Split('=');
            if (strB.Length==2)
            {
                switch (strB[1])
                {
                    case "1":
                        result = "OK";
                        break;
                    case "2":
                        result = "扭矩过大";
                        break;
                    case "3":
                        result = "扭矩过小";
                        break;
                    case "4":
                        result = "角度过大";
                        break;
                    case "5":
                        result = "角度过小";
                        break;
                    case "6":
                        result = "时间过长";
                        break;
                    case "7":
                        result = "时间过短";
                        break;
                    default:
                        result = strB[1];
                        break;
                }

            }

        }


    }


    }
