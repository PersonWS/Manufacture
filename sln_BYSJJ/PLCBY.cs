using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
using System.Threading;
namespace sln_BYSJJ
{
    public class PLCBY
    {
        int intYNJG;
        public S7Client s7Client;
        public static PLCBY plcby;
        public PLCBY()
        {
            int Result;
            //snap7初始化
            s7Client = new S7Client();
            Result = s7Client.ConnectTo("10.217.12.70", 0, 1);
            //Result = s7Client.ConnectTo("192.168.0.107", 0, 1);
            //Result = s7Client.ConnectTo("127.0.0.1", 0, 1);
            if (Result==0)
            {
                Corecurrent.GetCorecurrent().BY_IsConnnectPlc = true;
            }
            Thread plc_Td = new Thread(PlcAlarmRe);
            plc_Td.IsBackground = true;
            plc_Td.Start();
        }
        public static PLCBY GetBYPlc()
        {
            return plcby;
        }

        void PlcAlarmRe()
        {
            while(true)
            {
                if(s7Client.Connected())
                {
                    Corecurrent.GetCorecurrent().BY_IsConnnectPlc = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().BY_IsConnnectPlc = false;
                }

                //---------------------工位状态start--------------------------
                s7Client.DBRead(2001, 38, 2, Corecurrent.GetCorecurrent().BY_BufferGWZT);
                //---------------------工位状态end----------------------------

                //---------------------报警start------------------------------
                s7Client.DBRead(2001, 32, 5, Corecurrent.GetCorecurrent().BY_BufferBJ);
                if(bolGetAlarm()==true)
                {
                    Corecurrent.GetCorecurrent().BY_IsAlarm = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().BY_IsAlarm = false;
                }
                //---------------------报警end--------------------------------

                //---------------------工位参数start--------------------------
                byte[] BufferBYGWCS = new byte[24];
                s7Client.DBRead(2001, 40, 24, BufferBYGWCS);
                //实际压力值
                Single RealBYSJYLZ = Snap7.S7.GetRealAt(BufferBYGWCS, 0);
                Corecurrent.GetCorecurrent().BY_strSJYLZ = System.Convert.ToString(RealBYSJYLZ);
                //伺服故障代码
                ushort UshortBYSFGZDM = Snap7.S7.GetWordAt(BufferBYGWCS, 4);
                Corecurrent.GetCorecurrent().BY_strSFGZDM = System.Convert.ToString(UshortBYSFGZDM);
                //伺服报警代码
                ushort UshortBYSFBJDM = Snap7.S7.GetWordAt(BufferBYGWCS, 6);
                Corecurrent.GetCorecurrent().BY_strSFBJDM = System.Convert.ToString(UshortBYSFBJDM);
                //伺服实际位置
                Single RealBYSFSJWZ = Snap7.S7.GetRealAt(BufferBYGWCS, 8);
                Corecurrent.GetCorecurrent().BY_strSFSJWZ = System.Convert.ToString(RealBYSFSJWZ);
                //伺服当前倍率
                int IntBYSFDQBL = Snap7.S7.GetDIntAt(BufferBYGWCS, 12);
                Corecurrent.GetCorecurrent().BY_strSFDQBL = System.Convert.ToString(IntBYSFDQBL);
                //RFID信号强度
                int IntYXHQD = Snap7.S7.GetDIntAt(BufferBYGWCS, 16);
                Corecurrent.GetCorecurrent().BY_strXHQD = System.Convert.ToString(IntYXHQD);
                //RFID通讯状态
                int IntBYTXZT = Snap7.S7.GetIntAt(BufferBYGWCS, 20);
                Corecurrent.GetCorecurrent().BY_strTXZT = System.Convert.ToString(IntBYTXZT);
                //当前配方显示
                int IntBYDQPF = Snap7.S7.GetIntAt(BufferBYGWCS, 22);
                Corecurrent.GetCorecurrent().BY_strDQPF = System.Convert.ToString(IntBYDQPF);
                //---------------------工位参数end----------------------------

                //主SN码
                byte[] BufferZSN = new byte[30];
                s7Client.DBRead(2001, 2, 30, BufferZSN);
                var byteArrZSN = new byte[28];
                Array.Copy(BufferZSN, 2, byteArrZSN, 0, 28);
                Corecurrent.GetCorecurrent().BY_strZSN = System.Text.Encoding.ASCII.GetString(byteArrZSN);

                byte[] Buffer= new byte[1];
                intYNJG = s7Client.DBRead(2001, 0, 1, Buffer);
                //是否加工label
                if (Snap7.S7.GetBitAt(Buffer, 0, 0) == true)
                {
                    Corecurrent.GetCorecurrent().BY_bolLblSFJG = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().BY_bolLblSFJG = false;
                }
                //结果OKlabel
                if (Snap7.S7.GetBitAt(Buffer, 0, 1) == true)
                {
                    Corecurrent.GetCorecurrent().BY_bolLblJGOK = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().BY_bolLblJGOK = false;
                }
                //结果NGlabel
                if (Snap7.S7.GetBitAt(Buffer, 0, 2) == true)
                {
                    Corecurrent.GetCorecurrent().BY_bolLblJGNG = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().BY_bolLblJGNG = false;
                }

               
                if (Snap7.S7.GetBitAt(Buffer,0,0)==true)
                {
                    Corecurrent.GetCorecurrent().BY_bolRecivePLC = true;
                    Corecurrent.GetCorecurrent().BY_NowProcedure = Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_PROCESSYN;  
                }
                else
                {
                    if (Corecurrent.GetCorecurrent().BY_bolGetJGResult == false)
                    {
                        Corecurrent.GetCorecurrent().BY_NowProcedure = Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_IDLE;
                    }    
                    if (Corecurrent.GetCorecurrent().BY_bolLblYXJG == true || Corecurrent.GetCorecurrent().BY_bolLblJZJG == true)
                    {
                        Corecurrent.GetCorecurrent().BY_bolJGY = false;
                        Corecurrent.GetCorecurrent().BY_bolJGN = false;
                        Corecurrent.GetCorecurrent().BY_bolJGW = false;
                        Corecurrent.GetCorecurrent().BY_IsWritePlc = true;
                    }
                    if(Corecurrent.GetCorecurrent().BY_bolRecivePLC == true)
                    {
                        Corecurrent.GetCorecurrent().mapMake.Remove("BY_WAITASKPROCESS");
                        Corecurrent.GetCorecurrent().mapMake.Remove("BY_ASKMES");
                        Corecurrent.GetCorecurrent().mapMake.Remove("BY_JGWAITRESULT");

                        Corecurrent.GetCorecurrent().mapMake.Remove("BY_MESJGJZ");
                        Corecurrent.GetCorecurrent().mapMake.Remove("BY_JGOK");
                        Corecurrent.GetCorecurrent().mapMake.Remove("BY_JGNG");
                        Corecurrent.GetCorecurrent().mapMake.Remove("BY_SENDPLCYXJG");

                        Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200100TRUE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200100FALSE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200101TRUE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200102TRUE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("BY_DB200202TRUE");
                        Corecurrent.GetCorecurrent().BY_bolRecivePLC = false;
                    }
                }
                if (Snap7.S7.GetBitAt(Buffer, 0, 1) == false
                 && Snap7.S7.GetBitAt(Buffer, 0, 2) == false)
                {
                    if(Corecurrent.GetCorecurrent().BY_bolGetJGResult == true)
                    {
                        Corecurrent.GetCorecurrent().BY_bolJGY = false;
                        Corecurrent.GetCorecurrent().BY_bolJGN = false;
                        Corecurrent.GetCorecurrent().BY_bolJGW = false;
                        Corecurrent.GetCorecurrent().BY_IsWritePlc = true;
                        Corecurrent.GetCorecurrent().BY_NowProcedure = Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_END;
                    }
                }
                else
                {
                    if (Snap7.S7.GetBitAt(Buffer, 0, 1) == true)
                    {
                        Corecurrent.GetCorecurrent().BY_bolResultOK = true;
                        Corecurrent.GetCorecurrent().BY_bolResultNG = false;
                        Corecurrent.GetCorecurrent().BY_bolGetJGResult = true;
                        Corecurrent.GetCorecurrent().BY_NowProcedure = Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_SENDMES;
                    }
                    if (Snap7.S7.GetBitAt(Buffer, 0, 2) == true)
                    {
                        Corecurrent.GetCorecurrent().BY_bolResultOK = false;
                        Corecurrent.GetCorecurrent().BY_bolResultNG = true;
                        Corecurrent.GetCorecurrent().BY_bolGetJGResult = true;
                        Corecurrent.GetCorecurrent().BY_NowProcedure = Corecurrent.BY_SysRunStatus.BY_Procedure.BY_PROCEDURE_SENDMES;
                    }
                }
                
                if (Corecurrent.GetCorecurrent().BY_IsWritePlc == true)
                {
                    byte[] BufferJGYN = new byte[1];
                    if(Corecurrent.GetCorecurrent().BY_bolJGY == true
                    && Corecurrent.GetCorecurrent().BY_bolJGN == false
                    && Corecurrent.GetCorecurrent().BY_bolJGW == false)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, true);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, false);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().BY_bolLblYXJG = true;
                        Corecurrent.GetCorecurrent().BY_bolLblJZJG = false;
                        Corecurrent.GetCorecurrent().BY_bolLblJSWC = false;
                        Corecurrent.GetCorecurrent().BY_IsWritePlc = false;
                    }
                    if(Corecurrent.GetCorecurrent().BY_bolJGY == false
                    && Corecurrent.GetCorecurrent().BY_bolJGN == true
                    && Corecurrent.GetCorecurrent().BY_bolJGW == false)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, true);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, false);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().BY_bolLblYXJG = false;
                        Corecurrent.GetCorecurrent().BY_bolLblJZJG = true;
                        Corecurrent.GetCorecurrent().BY_bolLblJSWC = false;
                        Corecurrent.GetCorecurrent().BY_IsWritePlc = false;
                    }
                    if(Corecurrent.GetCorecurrent().BY_bolJGY == false
                    && Corecurrent.GetCorecurrent().BY_bolJGN == false
                    && Corecurrent.GetCorecurrent().BY_bolJGW == true)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, true);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().BY_bolLblYXJG = false;
                        Corecurrent.GetCorecurrent().BY_bolLblJZJG = false;
                        Corecurrent.GetCorecurrent().BY_bolLblJSWC = true;
                        Corecurrent.GetCorecurrent().BY_IsWritePlc = false;
                    }
                    if(Corecurrent.GetCorecurrent().BY_bolJGY == false
                    && Corecurrent.GetCorecurrent().BY_bolJGN == false
                    && Corecurrent.GetCorecurrent().BY_bolJGW == false)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, false);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().BY_bolLblYXJG = false;
                        Corecurrent.GetCorecurrent().BY_bolLblJZJG = false;
                        Corecurrent.GetCorecurrent().BY_bolLblJSWC = false;
                        Corecurrent.GetCorecurrent().BY_IsWritePlc = false;
                    }
                }
            }
        }

        bool bolGetAlarm()
        {
            bool BolAlarm = false;
            //传输报警
            //RFID通讯异常
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.0"))
                {
                    SetRows("传输报警", "DB2001DBX32.0", "RFID通讯异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX32.0", "RFID通讯异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.0"))
                {
                    DelRows("DB2001DBX32.0");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX32.0");
                }
            }
            //RFID信号强度弱
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.1"))
                {
                    SetRows("传输报警", "DB2001DBX32.1", "RFID信号强度弱");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX32.1", "RFID信号强度弱");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.1"))
                {
                    DelRows("DB2001DBX32.1");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX32.1");
                }
            }
            //上层进料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.2"))
                {
                    SetRows("传输报警", "DB2001DBX32.2", "上层进料异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX32.2", "上层进料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.2"))
                {
                    DelRows("DB2001DBX32.2");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX32.2");
                }
            }
            //上层出料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.3"))
                {
                    SetRows("传输报警", "DB2001DBX32.3", "上层出料异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX32.3", "上层出料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.3"))
                {
                    DelRows("DB2001DBX32.3");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX32.3");
                }
            }
            //下层进料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.4"))
                {
                    SetRows("传输报警", "DB2001DBX32.4", "下层进料异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX32.4", "下层进料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.4"))
                {
                    DelRows("DB2001DBX32.4");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX32.4");
                }
            }
            //下层出料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 5))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.5"))
                {
                    SetRows("传输报警", "DB2001DBX32.5", "下层出料异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX32.5", "下层出料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.5"))
                {
                    DelRows("DB2001DBX32.5");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX32.5");
                }
            }
            //上层保护光电异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 6))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.6"))
                {
                    SetRows("传输报警", "DB2001DBX32.6", "上层保护光电异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX32.6", "上层保护光电异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.6"))
                {
                    DelRows("DB2001DBX32.6");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX32.6");
                }
            }
            //下层保护光电异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 0, 7))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.7"))
                {
                    SetRows("传输报警", "DB2001DBX32.7", "下层保护光电异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX32.7", "下层保护光电异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX32.7"))
                {
                    DelRows("DB2001DBX32.7");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX32.7");
                }
            }
            //升降抬起异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 1, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX33.0"))
                {
                    SetRows("传输报警", "DB2001DBX33.0", "升降抬起异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX33.0", "升降抬起异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX33.0"))
                {
                    DelRows("DB2001DBX33.0");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX33.0");
                }
            }
            //升降落下异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 1, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX33.1"))
                {
                    SetRows("传输报警", "DB2001DBX33.1", "升降落下异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX33.1", "升降落下异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX33.1"))
                {
                    DelRows("DB2001DBX33.1");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX33.1");
                }
            }
            //RFID无数据
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 1, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX33.2"))
                {
                    SetRows("传输报警", "DB2001DBX33.2", "NG标志");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX33.2", "NG标志");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX33.2"))
                {
                    DelRows("DB2001DBX33.2");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX33.2");
                }
            }
            

            //常规报警
            //面板急停报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 2, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX34.0"))
                {
                    SetRows("常规报警", "DB2001DBX34.0", "面板急停报警");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX34.0", "面板急停报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX34.0"))
                {
                    DelRows("DB2001DBX34.0");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX34.0");
                }
            }
            //安全门打开报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 2, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX34.1"))
                {
                    SetRows("常规报警", "DB2001DBX34.1", "安全门打开报警");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX34.1", "安全门打开报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX34.1"))
                {
                    DelRows("DB2001DBX34.1");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX34.1");
                }
            }
            //与贴屏对接心跳异常
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 2, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX34.2"))
                {
                    SetRows("常规报警", "DB2001DBX34.2", "与贴屏对接心跳异常");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX34.2", "与贴屏对接心跳异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX34.2"))
                {
                    DelRows("DB2001DBX34.2");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX34.2");
                }
            }
            //双工位螺丝机无配方
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 2, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX34.3"))
                {
                    SetRows("常规报警", "DB2001DBX34.3", "双工位螺丝机无配方");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX34.3", "双工位螺丝机无配方");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX34.3"))
                {
                    DelRows("DB2001DBX34.3");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX34.3");
                }
            }

            //伺服报警
            //伺服暂停
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.0"))
                {
                    SetRows("伺服报警", "DB2001DBX36.0", "伺服暂停");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX36.0", "伺服暂停");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.0"))
                {
                    DelRows("DB2001DBX36.0");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX36.0");
                }
            }
            //伺服未使能
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.1"))
                {
                    SetRows("伺服报警", "DB2001DBX36.1", "伺服未使能");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX36.1", "伺服未使能");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.1"))
                {
                    DelRows("DB2001DBX36.1");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX36.1");
                }
            }
            //伺服上极限
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.2"))
                {
                    SetRows("伺服报警", "DB2001DBX36.2", "伺服上极限");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX36.2", "伺服上极限");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.2"))
                {
                    DelRows("DB2001DBX36.2");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX36.2");
                }
            }
            //伺服下极限
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.3"))
                {
                    SetRows("伺服报警", "DB2001DBX36.3", "伺服下极限");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX36.3", "伺服下极限");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.3"))
                {
                    DelRows("DB2001DBX36.3");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX36.3");
                }
            }
            //伺服无倍率
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.4"))
                {
                    SetRows("伺服报警", "DB2001DBX36.4", "伺服无倍率");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX36.4", "伺服无倍率");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.4"))
                {
                    DelRows("DB2001DBX36.4");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX36.4");
                }
            }
            //伺服上软极限
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 5))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.5"))
                {
                    SetRows("伺服报警", "DB2001DBX36.5", "伺服上软极限");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX36.5", "伺服上软极限");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.5"))
                {
                    DelRows("DB2001DBX36.5");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX36.5");
                }
            }
            //伺服下软极限
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BY_BufferBJ, 4, 6))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.6"))
                {
                    SetRows("伺服报警", "DB2001DBX36.6", "伺服下软极限");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Add("DB2001DBX36.6", "伺服下软极限");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().BY_mapAlarm.Keys.Contains("DB2001DBX36.6"))
                {
                    DelRows("DB2001DBX36.6");
                    Corecurrent.GetCorecurrent().BY_mapAlarm.Remove("DB2001DBX36.6");
                }
            }
            return BolAlarm;
        }
        void SetRows(string strBJLB, string strDW, string strBJXX)
        {
            Corecurrent.GetCorecurrent().BY_bolTableRe = false;
            DataRow drAlarm = Corecurrent.GetCorecurrent().BY_dtAlarm.NewRow();
            drAlarm["时间"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            drAlarm["报警类别"] = strBJLB;
            drAlarm["点位"] = strDW;
            drAlarm["报警信息"] = strBJXX;
            Corecurrent.GetCorecurrent().BY_dtAlarm.Rows.Add(drAlarm);
            Corecurrent.GetCorecurrent().BY_dtAlarm.AcceptChanges();
            Corecurrent.GetCorecurrent().BY_bolTableRe = true;
        }

        void DelRows(string strDW)
        {
            Corecurrent.GetCorecurrent().BY_bolTableRe = false;
            foreach (DataRow drAlarm in Corecurrent.GetCorecurrent().BY_dtAlarm.Rows)
            {
                if (drAlarm["点位"].ToString() == strDW)
                {
                    drAlarm.Delete();
                }
            }
            Corecurrent.GetCorecurrent().BY_dtAlarm.AcceptChanges();
            Corecurrent.GetCorecurrent().BY_bolTableRe = true;
        }
    }
}
