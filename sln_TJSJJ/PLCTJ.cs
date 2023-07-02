using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
using System.Threading;
namespace sln_TJSJJ
{
    public class PLCTJ
    {
        int intYNJG;
        public S7Client s7Client;
        public static PLCTJ plctj;
        public PLCTJ()
        {
            int Result;
            //snap7初始化
            s7Client = new S7Client();
            Result = s7Client.ConnectTo("10.217.12.30", 0, 1);
            //Result = s7Client.ConnectTo("192.168.1.134", 0, 1);
            //Result = s7Client.ConnectTo("127.0.0.1", 0, 1);
            if (Result==0)
            {
                Corecurrent.GetCorecurrent().TJ_IsConnnectPlc = true;
            }
            Thread plc_Td = new Thread(PlcAlarmRe);
            plc_Td.IsBackground = true;
            plc_Td.Start();
        }
        public static PLCTJ GetTJPlc()
        {
            return plctj;
        }

        void PlcAlarmRe()
        {
            while(true)
            {
                if(s7Client.Connected())
                {
                    Corecurrent.GetCorecurrent().TJ_IsConnnectPlc = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().TJ_IsConnnectPlc = false;
                }

                //---------------------工位状态start--------------------------
                s7Client.DBRead(2001, 38, 1, Corecurrent.GetCorecurrent().TJ_BufferGWZT);
                //---------------------工位状态end----------------------------

                //---------------------报警start------------------------------
                s7Client.DBRead(2001, 32, 6, Corecurrent.GetCorecurrent().TJ_BufferBJ);
                if(bolGetAlarm()==true)
                {
                    Corecurrent.GetCorecurrent().TJ_IsAlarm = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().TJ_IsAlarm = false;
                }
                //---------------------报警end--------------------------------

                //---------------------工位参数start--------------------------
                byte[] BufferTJGWCS = new byte[8];
                s7Client.DBRead(2001, 40, 8, BufferTJGWCS);
                ////实际压力值
                //Single RealTJSJYLZ = Snap7.S7.GetRealAt(BufferTJGWCS, 0);
                //Corecurrent.GetCorecurrent().TJ_strSJYLZ = System.Convert.ToString(RealTJSJYLZ);
                ////伺服故障代码
                //ushort UshortTJSFGZDM = Snap7.S7.GetWordAt(BufferTJGWCS, 4);
                //Corecurrent.GetCorecurrent().TJ_strSFGZDM = System.Convert.ToString(UshortTJSFGZDM);
                ////伺服报警代码
                //ushort UshortTJSFBJDM = Snap7.S7.GetWordAt(BufferTJGWCS, 6);
                //Corecurrent.GetCorecurrent().TJ_strSFBJDM = System.Convert.ToString(UshortTJSFBJDM);
                ////伺服实际位置
                //Single RealTJSFSJWZ = Snap7.S7.GetRealAt(BufferTJGWCS, 8);
                //Corecurrent.GetCorecurrent().TJ_strSFSJWZ = System.Convert.ToString(RealTJSFSJWZ);
                ////伺服当前倍率
                //int IntTJSFDQBL = Snap7.S7.GetDIntAt(BufferTJGWCS, 12);
                //Corecurrent.GetCorecurrent().TJ_strSFDQBL = System.Convert.ToString(IntTJSFDQBL);
                //RFID信号强度
                int IntTJXHQD = Snap7.S7.GetDIntAt(BufferTJGWCS, 0);
                Corecurrent.GetCorecurrent().TJ_strXHQD = System.Convert.ToString(IntTJXHQD);
                //RFID通讯状态
                int IntTJTXZT = Snap7.S7.GetIntAt(BufferTJGWCS, 4);
                Corecurrent.GetCorecurrent().TJ_strTXZT = System.Convert.ToString(IntTJTXZT);
                //当前配方显示
                int IntTJDQPF = Snap7.S7.GetIntAt(BufferTJGWCS, 6);
                Corecurrent.GetCorecurrent().TJ_strDQPF = System.Convert.ToString(IntTJDQPF);
                //---------------------工位参数end----------------------------

                //后壳SN码
                byte[] BufferHSN = new byte[30];
                s7Client.DBRead(2001, 2, 30, BufferHSN);
                var byteArrHSN = new byte[28];
                Array.Copy(BufferHSN, 2, byteArrHSN, 0, 28);
                Corecurrent.GetCorecurrent().TJ_strHSN = System.Text.Encoding.ASCII.GetString(byteArrHSN);

                byte[] Buffer= new byte[1];
                intYNJG = s7Client.DBRead(2001, 0, 1, Buffer);
                //是否加工label
                if (Snap7.S7.GetBitAt(Buffer, 0, 0) == true)
                {
                    Corecurrent.GetCorecurrent().TJ_bolLblSFJG = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().TJ_bolLblSFJG = false;
                }
                //结果OKlabel
                if (Snap7.S7.GetBitAt(Buffer, 0, 1) == true)
                {
                    Corecurrent.GetCorecurrent().TJ_bolLblJGOK = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().TJ_bolLblJGOK = false;
                }
                //结果NGlabel
                if (Snap7.S7.GetBitAt(Buffer, 0, 2) == true)
                {
                    Corecurrent.GetCorecurrent().TJ_bolLblJGNG = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().TJ_bolLblJGNG = false;
                }

               
                if (Snap7.S7.GetBitAt(Buffer,0,0)==true)
                {
                    Corecurrent.GetCorecurrent().TJ_bolRecivePLC = true;
                    Corecurrent.GetCorecurrent().TJ_NowProcedure = Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_PROCESSYN;  
                }
                else
                {
                    if (Corecurrent.GetCorecurrent().TJ_bolGetJGResult == false)
                    {
                        Corecurrent.GetCorecurrent().TJ_NowProcedure = Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_IDLE;
                    }  
                    if (Corecurrent.GetCorecurrent().TJ_bolLblYXJG == true || Corecurrent.GetCorecurrent().TJ_bolLblJZJG == true)
                    {
                        Corecurrent.GetCorecurrent().TJ_bolJGY = false;
                        Corecurrent.GetCorecurrent().TJ_bolJGN = false;
                        Corecurrent.GetCorecurrent().TJ_bolJGW = false;
                        Corecurrent.GetCorecurrent().TJ_IsWritePlc = true;
                    }
                    if(Corecurrent.GetCorecurrent().TJ_bolRecivePLC == true)
                    {
                        Corecurrent.GetCorecurrent().mapMake.Remove("TJ_WAITASKPROCESS");
                        Corecurrent.GetCorecurrent().mapMake.Remove("TJ_ASKMES");
                        Corecurrent.GetCorecurrent().mapMake.Remove("TJ_JGWAITRESULT");

                        Corecurrent.GetCorecurrent().mapMake.Remove("TJ_MESJGJZ");
                        Corecurrent.GetCorecurrent().mapMake.Remove("TJ_JGOK");
                        Corecurrent.GetCorecurrent().mapMake.Remove("TJ_JGNG");
                        Corecurrent.GetCorecurrent().mapMake.Remove("TJ_SENDPLCYXJG");

                        Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200100TRUE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200100FALSE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200101TRUE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200102TRUE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("TJ_DB200202TRUE");
                        Corecurrent.GetCorecurrent().TJ_bolRecivePLC = false;
                    }
                }
                if (Snap7.S7.GetBitAt(Buffer, 0, 1) == false
                 && Snap7.S7.GetBitAt(Buffer, 0, 2) == false)
                {
                    if(Corecurrent.GetCorecurrent().TJ_bolGetJGResult == true)
                    {
                        Corecurrent.GetCorecurrent().TJ_bolJGY = false;
                        Corecurrent.GetCorecurrent().TJ_bolJGN = false;
                        Corecurrent.GetCorecurrent().TJ_bolJGW = false;
                        Corecurrent.GetCorecurrent().TJ_IsWritePlc = true;
                        Corecurrent.GetCorecurrent().TJ_NowProcedure = Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_END;
                    }
                }
                else
                {
                    if (Snap7.S7.GetBitAt(Buffer, 0, 1) == true)
                    {
                        Corecurrent.GetCorecurrent().TJ_bolResultOK = true;
                        Corecurrent.GetCorecurrent().TJ_bolResultNG = false;
                        Corecurrent.GetCorecurrent().TJ_bolGetJGResult = true;
                        Corecurrent.GetCorecurrent().TJ_NowProcedure = Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_SENDMES;
                    }
                    if (Snap7.S7.GetBitAt(Buffer, 0, 2) == true)
                    {
                        Corecurrent.GetCorecurrent().TJ_bolResultOK = false;
                        Corecurrent.GetCorecurrent().TJ_bolResultNG = true;
                        Corecurrent.GetCorecurrent().TJ_bolGetJGResult = true;
                        Corecurrent.GetCorecurrent().TJ_NowProcedure = Corecurrent.TJ_SysRunStatus.TJ_Procedure.TJ_PROCEDURE_SENDMES;
                    }
                }
                
                if (Corecurrent.GetCorecurrent().TJ_IsWritePlc == true)
                {
                    byte[] BufferJGYN = new byte[1];
                    if(Corecurrent.GetCorecurrent().TJ_bolJGY == true
                    && Corecurrent.GetCorecurrent().TJ_bolJGN == false
                    && Corecurrent.GetCorecurrent().TJ_bolJGW == false)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, true);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, false);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().TJ_bolLblYXJG = true;
                        Corecurrent.GetCorecurrent().TJ_bolLblJZJG = false;
                        Corecurrent.GetCorecurrent().TJ_bolLblJSWC = false;
                        Corecurrent.GetCorecurrent().TJ_IsWritePlc = false;
                    }
                    if(Corecurrent.GetCorecurrent().TJ_bolJGY == false
                    && Corecurrent.GetCorecurrent().TJ_bolJGN == true
                    && Corecurrent.GetCorecurrent().TJ_bolJGW == false)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, true);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, false);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().TJ_bolLblYXJG = false;
                        Corecurrent.GetCorecurrent().TJ_bolLblJZJG = true;
                        Corecurrent.GetCorecurrent().TJ_bolLblJSWC = false;
                        Corecurrent.GetCorecurrent().TJ_IsWritePlc = false;
                    }
                    if(Corecurrent.GetCorecurrent().TJ_bolJGY == false
                    && Corecurrent.GetCorecurrent().TJ_bolJGN == false
                    && Corecurrent.GetCorecurrent().TJ_bolJGW == true)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, true);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().TJ_bolLblYXJG = false;
                        Corecurrent.GetCorecurrent().TJ_bolLblJZJG = false;
                        Corecurrent.GetCorecurrent().TJ_bolLblJSWC = true;
                        Corecurrent.GetCorecurrent().TJ_IsWritePlc = false;
                    }
                    if(Corecurrent.GetCorecurrent().TJ_bolJGY == false
                    && Corecurrent.GetCorecurrent().TJ_bolJGN == false
                    && Corecurrent.GetCorecurrent().TJ_bolJGW == false)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, false);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().TJ_bolLblYXJG = false;
                        Corecurrent.GetCorecurrent().TJ_bolLblJZJG = false;
                        Corecurrent.GetCorecurrent().TJ_bolLblJSWC = false;
                        Corecurrent.GetCorecurrent().TJ_IsWritePlc = false;
                    }
                }
            }
        }

        bool bolGetAlarm()
        {
            bool BolAlarm = false;
            //传输报警
            //RFID通讯异常
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.0"))
                {
                    SetRows("传输报警", "DB2001DBX32.0", "RFID通讯异常");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX32.0", "RFID通讯异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.0"))
                {
                    DelRows("DB2001DBX32.0");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX32.0");
                }
            }
            //RFID信号强度弱
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.1"))
                {
                    SetRows("传输报警", "DB2001DBX32.1", "RFID信号强度弱");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX32.1", "RFID信号强度弱");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.1"))
                {
                    DelRows("DB2001DBX32.1");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX32.1");
                }
            }
            //上层进料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.2"))
                {
                    SetRows("传输报警", "DB2001DBX32.2", "上层进料异常");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX32.2", "上层进料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.2"))
                {
                    DelRows("DB2001DBX32.2");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX32.2");
                }
            }
            //上层出料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.3"))
                {
                    SetRows("传输报警", "DB2001DBX32.3", "上层出料异常");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX32.3", "上层出料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.3"))
                {
                    DelRows("DB2001DBX32.3");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX32.3");
                }
            }
            //下层进料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.4"))
                {
                    SetRows("传输报警", "DB2001DBX32.4", "下层进料异常");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX32.4", "下层进料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.4"))
                {
                    DelRows("DB2001DBX32.4");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX32.4");
                }
            }
            //下层出料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 5))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.5"))
                {
                    SetRows("传输报警", "DB2001DBX32.5", "下层出料异常");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX32.5", "下层出料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.5"))
                {
                    DelRows("DB2001DBX32.5");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX32.5");
                }
            }
            //上层保护光电异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 6))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.6"))
                {
                    SetRows("传输报警", "DB2001DBX32.6", "上层保护光电异常");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX32.6", "上层保护光电异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.6"))
                {
                    DelRows("DB2001DBX32.6");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX32.6");
                }
            }
            //下层保护光电异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 0, 7))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.7"))
                {
                    SetRows("传输报警", "DB2001DBX32.7", "下层保护光电异常");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX32.7", "下层保护光电异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX32.7"))
                {
                    DelRows("DB2001DBX32.7");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX32.7");
                }
            }
            //升降抬起异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 1, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX33.0"))
                {
                    SetRows("传输报警", "DB2001DBX33.0", "升降抬起异常");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX33.0", "升降抬起异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX33.0"))
                {
                    DelRows("DB2001DBX33.0");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX33.0");
                }
            }
            //升降落下异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 1, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX33.1"))
                {
                    SetRows("传输报警", "DB2001DBX33.1", "升降落下异常");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX33.1", "升降落下异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX33.1"))
                {
                    DelRows("DB2001DBX33.1");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX33.1");
                }
            }
            //NG标志
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 1, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX33.2"))
                {
                    SetRows("传输报警", "DB2001DBX33.2", "NG标志");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX33.2", "NG标志");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX33.2"))
                {
                    DelRows("DB2001DBX33.2");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX33.2");
                }
            }
            

            //常规报警
            //面板急停报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.0"))
                {
                    SetRows("常规报警", "DB2001DBX34.0", "面板急停报警");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX34.0", "面板急停报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.0"))
                {
                    DelRows("DB2001DBX34.0");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX34.0");
                }
            }
            //安全门打开报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.1"))
                {
                    SetRows("常规报警", "DB2001DBX34.1", "安全门打开报警");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX34.1", "安全门打开报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.1"))
                {
                    DelRows("DB2001DBX34.1");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX34.1");
                }
            }
            //气源压力不足报警
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.2"))
                {
                    SetRows("常规报警", "DB2001DBX34.2", "气源压力不足报警");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX34.2", "气源压力不足报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.2"))
                {
                    DelRows("DB2001DBX34.2");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX34.2");
                }
            }
            //擦胶机换料提示
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.3"))
                {
                    SetRows("常规报警", "DB2001DBX34.3", "擦胶机换料提示");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX34.3", "擦胶机换料提示");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.3"))
                {
                    DelRows("DB2001DBX34.3");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX34.3");
                }
            }
            //擦胶机缺料报警
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.4"))
                {
                    SetRows("常规报警", "DB2001DBX34.4", "擦胶机缺料报警");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX34.4", "擦胶机缺料报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.4"))
                {
                    DelRows("DB2001DBX34.4");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX34.4");
                }
            }
            //工位暂停
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 5))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.5"))
                {
                    SetRows("常规报警", "DB2001DBX34.5", "工位暂停");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX34.5", "工位暂停");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.5"))
                {
                    DelRows("DB2001DBX34.5");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX34.5");
                }
            }
            //换胶提醒
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 6))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.6"))
                {
                    SetRows("常规报警", "DB2001DBX34.6", "换胶提醒");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX34.6", "换胶提醒");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.6"))
                {
                    DelRows("DB2001DBX34.6");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX34.6");
                }
            }
            //双工位螺丝机无配方
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 2, 7))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.7"))
                {
                    SetRows("常规报警", "DB2001DBX34.7", "双工位螺丝机无配方");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX34.7", "双工位螺丝机无配方");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX34.7"))
                {
                    DelRows("DB2001DBX34.7");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX34.7");
                }
            }

            //伺服报警
            //XYZ轴未复位完成
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.0"))
                {
                    SetRows("伺服报警", "DB2001DBX36.0", "XYZ轴未复位完成");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX36.0", "XYZ轴未复位完成");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.0"))
                {
                    DelRows("DB2001DBX36.0");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX36.0");
                }
            }
            //X轴未使能
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.1"))
                {
                    SetRows("伺服报警", "DB2001DBX36.1", "X轴未使能");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX36.1", "X轴未使能");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.1"))
                {
                    DelRows("DB2001DBX36.1");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX36.1");
                }
            }
            //Y轴未使能
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.2"))
                {
                    SetRows("伺服报警", "DB2001DBX36.2", "Y轴未使能");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX36.2", "Y轴未使能");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.2"))
                {
                    DelRows("DB2001DBX36.2");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX36.2");
                }
            }
            //Z轴未使能
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.3"))
                {
                    SetRows("伺服报警", "DB2001DBX36.3", "Z轴未使能");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX36.3", "Z轴未使能");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.3"))
                {
                    DelRows("DB2001DBX36.3");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX36.3");
                }
            }
            //X轴有报警
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.4"))
                {
                    SetRows("伺服报警", "DB2001DBX36.4", "X轴有报警");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX36.4", "X轴有报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.4"))
                {
                    DelRows("DB2001DBX36.4");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX36.4");
                }
            }
            //Y轴有报警
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 5))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.5"))
                {
                    SetRows("伺服报警", "DB2001DBX36.5", "Y轴有报警");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX36.5", "Y轴有报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.5"))
                {
                    DelRows("DB2001DBX36.5");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX36.5");
                }
            }
            //Z轴有报警
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 6))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.6"))
                {
                    SetRows("伺服报警", "DB2001DBX36.6", "Z轴有报警");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX36.6", "Z轴有报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.6"))
                {
                    DelRows("DB2001DBX36.6");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX36.6");
                }
            }
            //X轴正极限触发
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 4, 7))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.7"))
                {
                    SetRows("伺服报警", "DB2001DBX36.7", "X轴正极限触发");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX36.7", "X轴正极限触发");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX36.7"))
                {
                    DelRows("DB2001DBX36.7");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX36.7");
                }
            }
            //X轴负极限触发
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.0"))
                {
                    SetRows("伺服报警", "DB2001DBX37.0", "X轴负极限触发");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX37.0", "X轴负极限触发");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.0"))
                {
                    DelRows("DB2001DBX37.0");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX37.0");
                }
            }
            //Y轴正极限触发
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.1"))
                {
                    SetRows("伺服报警", "DB2001DBX37.1", "Y轴正极限触发");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX37.1", "Y轴正极限触发");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.1"))
                {
                    DelRows("DB2001DBX37.1");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX37.1");
                }
            }
            //Y轴负极限触发
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.2"))
                {
                    SetRows("伺服报警", "DB2001DBX37.2", "Y轴负极限触发");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX37.2", "Y轴负极限触发");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.2"))
                {
                    DelRows("DB2001DBX37.2");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX37.2");
                }
            }
            //Z轴正极限触发
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.3"))
                {
                    SetRows("伺服报警", "DB2001DBX37.3", "Z轴正极限触发");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX37.3", "Z轴正极限触发");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.3"))
                {
                    DelRows("DB2001DBX37.3");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX37.3");
                }
            }
            //Z轴负极限触发
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().TJ_BufferBJ, 5, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.4"))
                {
                    SetRows("伺服报警", "DB2001DBX37.4", "Z轴负极限触发");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Add("DB2001DBX37.4", "Z轴负极限触发");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().TJ_mapAlarm.Keys.Contains("DB2001DBX37.4"))
                {
                    DelRows("DB2001DBX37.4");
                    Corecurrent.GetCorecurrent().TJ_mapAlarm.Remove("DB2001DBX37.4");
                }
            }
            return BolAlarm;
        }
        void SetRows(string strBJLB, string strDW, string strBJXX)
        {
            Corecurrent.GetCorecurrent().TJ_bolTableRe = false;
            DataRow drAlarm = Corecurrent.GetCorecurrent().TJ_dtAlarm.NewRow();
            drAlarm["时间"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            drAlarm["报警类别"] = strBJLB;
            drAlarm["点位"] = strDW;
            drAlarm["报警信息"] = strBJXX;
            Corecurrent.GetCorecurrent().TJ_dtAlarm.Rows.Add(drAlarm);
            Corecurrent.GetCorecurrent().TJ_dtAlarm.AcceptChanges();
            Corecurrent.GetCorecurrent().TJ_bolTableRe = true;
        }

        void DelRows(string strDW)
        {
            //if(Corecurrent.GetCorecurrent().TJ_dtAlarm.Rows.Count==0)
            //{
            //    return;
            //}
            Corecurrent.GetCorecurrent().TJ_bolTableRe = false;
            foreach (DataRow drAlarm in Corecurrent.GetCorecurrent().TJ_dtAlarm.Rows)
            {
                if (drAlarm["点位"].ToString() == strDW)
                {
                    drAlarm.Delete();
                }
            }
            Corecurrent.GetCorecurrent().TJ_dtAlarm.AcceptChanges();
            Corecurrent.GetCorecurrent().TJ_bolTableRe = true;
        }
    }
}
