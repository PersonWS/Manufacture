using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
using System.Threading;
namespace sln_TP
{
    public class Plc
    {
        int intYNJG;
        public S7Client s7Client;
        public static Plc plc;
        public Plc()
        {
            int Result;
            //snap7初始化
            s7Client = new S7Client();
            Result = s7Client.ConnectTo("10.217.12.50", 0, 1);
            //Result = s7Client.ConnectTo("192.168.0.107", 0, 1);
            //Result = s7Client.ConnectTo("127.0.0.1", 0, 1);
            if (Result==0)
            {
                Corecurrent.GetCorecurrent().IsConnnectPlc = true;
            }
            Thread plc_Td = new Thread(PlcAlarmRe);
            plc_Td.IsBackground = true;
            plc_Td.Start();
        }
        public static Plc GetPlc()
        {
            return plc;
        }

        void PlcAlarmRe()
        {
            while(true)
            {
                if(s7Client.Connected())
                {
                    Corecurrent.GetCorecurrent().IsConnnectPlc = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().IsConnnectPlc = false;
                }

                //---------------------工位状态start--------------------------
                s7Client.DBRead(2001, 72, 3, Corecurrent.GetCorecurrent().BufferGWZT);
                //---------------------工位状态end----------------------------

                //---------------------报警start------------------------------
                s7Client.DBRead(2001, 62, 9, Corecurrent.GetCorecurrent().BufferBJ);
                if(bolGetAlarm()==true)
                {
                    Corecurrent.GetCorecurrent().IsAlarm = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().IsAlarm = false;
                }
                //---------------------报警end--------------------------------

                //---------------------工位参数start--------------------------
                byte[] BufferGWCS = new byte[42];
                s7Client.DBRead(2001, 76, 42, BufferGWCS);
                //贴屏中转X偏移位置
                Single RealTPZZX = Snap7.S7.GetRealAt(BufferGWCS, 0);
                Corecurrent.GetCorecurrent().strTPZZX = System.Convert.ToString(RealTPZZX);
                //贴屏中转Y偏移位置
                Single RealTPZZY = Snap7.S7.GetRealAt(BufferGWCS, 4);
                Corecurrent.GetCorecurrent().strTPZZY = System.Convert.ToString(RealTPZZY);
                //贴屏中转R偏移位置
                Single RealTPZZR = Snap7.S7.GetRealAt(BufferGWCS, 8);
                Corecurrent.GetCorecurrent().strTPZZR = System.Convert.ToString(RealTPZZR);
                //贴屏X偏移位置
                Single RealTPX = Snap7.S7.GetRealAt(BufferGWCS, 12);
                Corecurrent.GetCorecurrent().strTPX = System.Convert.ToString(RealTPX);
                //贴屏Y偏移位置
                Single RealTPY = Snap7.S7.GetRealAt(BufferGWCS, 16);
                Corecurrent.GetCorecurrent().strTPY = System.Convert.ToString(RealTPY);
                //贴屏R偏移位置
                Single RealTPR = Snap7.S7.GetRealAt(BufferGWCS, 20);
                Corecurrent.GetCorecurrent().strTPR = System.Convert.ToString(RealTPR);
                //伺服实际位置
                Single RealSFWZ = Snap7.S7.GetRealAt(BufferGWCS, 24);
                Corecurrent.GetCorecurrent().strSFWZ = System.Convert.ToString(RealSFWZ);
                //故障代码
                Single RealGZDM = Snap7.S7.GetRealAt(BufferGWCS, 28);
                Corecurrent.GetCorecurrent().strGZDM = System.Convert.ToString(RealGZDM);
                //报警代码
                ushort UshortBJDM = Snap7.S7.GetWordAt(BufferGWCS, 32);
                Corecurrent.GetCorecurrent().strBJDM = System.Convert.ToString(UshortBJDM);
                //RFID信号强度
                int IntBJDM = Snap7.S7.GetDIntAt(BufferGWCS, 34);
                Corecurrent.GetCorecurrent().strXHQD = System.Convert.ToString(IntBJDM);
                //RFID通讯状态
                int IntTXZT = Snap7.S7.GetIntAt(BufferGWCS, 38);
                Corecurrent.GetCorecurrent().strTXZT = System.Convert.ToString(IntTXZT);
                //当前配方
                int intDQPF = Snap7.S7.GetIntAt(BufferGWCS, 40);
                Corecurrent.GetCorecurrent().strDQPF = System.Convert.ToString(intDQPF);
                //---------------------工位参数end----------------------------

                //后壳SN码
                byte[] BufferHSN = new byte[30];
                s7Client.DBRead(2001, 2, 30, BufferHSN);
                var byteArrHSN = new byte[28];
                Array.Copy(BufferHSN, 2, byteArrHSN, 0, 28);
                Corecurrent.GetCorecurrent().strHSN = System.Text.Encoding.ASCII.GetString(byteArrHSN);

                //主SN码
                byte[] BufferZSN = new byte[30];
                s7Client.DBRead(2001, 32, 30, BufferZSN);
                var byteArrZSN = new byte[28];
                Array.Copy(BufferZSN, 2, byteArrZSN, 0, 28);
                Corecurrent.GetCorecurrent().strZSN = System.Text.Encoding.ASCII.GetString(byteArrZSN);

                byte[] Buffer= new byte[1];
                intYNJG = s7Client.DBRead(2001, 0, 1, Buffer);
                //是否加工label
                if (Snap7.S7.GetBitAt(Buffer, 0, 0) == true)
                {
                    Corecurrent.GetCorecurrent().bolLblSFJG = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().bolLblSFJG = false;
                }
                //结果OKlabel
                if (Snap7.S7.GetBitAt(Buffer, 0, 1) == true)
                {
                    Corecurrent.GetCorecurrent().bolLblJGOK = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().bolLblJGOK = false;
                }
                //结果NGlabel
                if (Snap7.S7.GetBitAt(Buffer, 0, 2) == true)
                {
                    Corecurrent.GetCorecurrent().bolLblJGNG = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().bolLblJGNG = false;
                }

               
                if (Snap7.S7.GetBitAt(Buffer,0,0)==true)
                {
                    Corecurrent.GetCorecurrent().bolRecivePLC = true;
                    Corecurrent.GetCorecurrent().NowProcedure = Corecurrent.SysRunStatus.Procedure.PROCEDURE_PROCESSYN;   
                }
                else
                {
                    if(Corecurrent.GetCorecurrent().bolGetJGResult ==false)
                    {
                        Corecurrent.GetCorecurrent().NowProcedure = Corecurrent.SysRunStatus.Procedure.PROCEDURE_IDLE;
                    }
                    
                    if (Corecurrent.GetCorecurrent().bolLblYXJG == true || Corecurrent.GetCorecurrent().bolLblJZJG == true)
                    {
                        Corecurrent.GetCorecurrent().bolJGY = false;
                        Corecurrent.GetCorecurrent().bolJGN = false;
                        Corecurrent.GetCorecurrent().bolJGW = false;
                        Corecurrent.GetCorecurrent().IsWritePlc = true;
                    }
                    if(Corecurrent.GetCorecurrent().bolRecivePLC == true)
                    {
                        Corecurrent.GetCorecurrent().mapMake.Remove("WAITASKPROCESS");
                        Corecurrent.GetCorecurrent().mapMake.Remove("ASKMES");
                        Corecurrent.GetCorecurrent().mapMake.Remove("JGWAITRESULT");

                        Corecurrent.GetCorecurrent().mapMake.Remove("MESJGJZ");
                        Corecurrent.GetCorecurrent().mapMake.Remove("JGOK");
                        Corecurrent.GetCorecurrent().mapMake.Remove("JGNG");
                        Corecurrent.GetCorecurrent().mapMake.Remove("ZSNK");
                        Corecurrent.GetCorecurrent().mapMake.Remove("ZSNCD");
                        Corecurrent.GetCorecurrent().mapMake.Remove("SENDPLCYXJG");

                        Corecurrent.GetCorecurrent().mapInfo.Remove("DB200100TRUE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("DB200100FALSE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("DB200101TRUE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("DB200102TRUE");
                        Corecurrent.GetCorecurrent().mapInfo.Remove("DB200202TRUE");
                        Corecurrent.GetCorecurrent().bolRecivePLC = false;
                        Corecurrent.GetCorecurrent().bolHSQQ = false;
                    }
                }
                if (Snap7.S7.GetBitAt(Buffer, 0, 1) == false
                 && Snap7.S7.GetBitAt(Buffer, 0, 2) == false)
                {
                    if(Corecurrent.GetCorecurrent().bolGetJGResult == true)
                    {
                        Corecurrent.GetCorecurrent().bolJGY = false;
                        Corecurrent.GetCorecurrent().bolJGN = false;
                        Corecurrent.GetCorecurrent().bolJGW = false;
                        Corecurrent.GetCorecurrent().IsWritePlc = true;
                        Corecurrent.GetCorecurrent().NowProcedure = Corecurrent.SysRunStatus.Procedure.PROCEDURE_END;
                    }
                }
                else
                {
                    if (Snap7.S7.GetBitAt(Buffer, 0, 1) == true)
                    {
                        Corecurrent.GetCorecurrent().bolResultOK = true;
                        Corecurrent.GetCorecurrent().bolResultNG = false;
                        Corecurrent.GetCorecurrent().bolGetJGResult = true;
                        Corecurrent.GetCorecurrent().NowProcedure = Corecurrent.SysRunStatus.Procedure.PROCEDURE_SENDMES;
                    }
                    if (Snap7.S7.GetBitAt(Buffer, 0, 2) == true)
                    {
                        Corecurrent.GetCorecurrent().bolResultOK = false;
                        Corecurrent.GetCorecurrent().bolResultNG = true;
                        Corecurrent.GetCorecurrent().bolGetJGResult = true;
                        Corecurrent.GetCorecurrent().NowProcedure = Corecurrent.SysRunStatus.Procedure.PROCEDURE_SENDMES;
                    }
                }
                
                if (Corecurrent.GetCorecurrent().IsWritePlc==true)
                {
                    byte[] BufferJGYN = new byte[1];
                    if(Corecurrent.GetCorecurrent().bolJGY == true
                    && Corecurrent.GetCorecurrent().bolJGN == false
                    && Corecurrent.GetCorecurrent().bolJGW == false)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, true);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, false);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().bolLblYXJG = true;
                        Corecurrent.GetCorecurrent().bolLblJZJG = false;
                        Corecurrent.GetCorecurrent().bolLblJSWC = false;
                        Corecurrent.GetCorecurrent().IsWritePlc = false;
                    }
                    if(Corecurrent.GetCorecurrent().bolJGY == false
                    && Corecurrent.GetCorecurrent().bolJGN == true
                    && Corecurrent.GetCorecurrent().bolJGW == false)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, true);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, false);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().bolLblYXJG = false;
                        Corecurrent.GetCorecurrent().bolLblJZJG = true;
                        Corecurrent.GetCorecurrent().bolLblJSWC = false;
                        Corecurrent.GetCorecurrent().IsWritePlc = false;
                    }
                    if(Corecurrent.GetCorecurrent().bolJGY == false
                    && Corecurrent.GetCorecurrent().bolJGN == false
                    && Corecurrent.GetCorecurrent().bolJGW == true)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, true);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().bolLblYXJG = false;
                        Corecurrent.GetCorecurrent().bolLblJZJG = false;
                        Corecurrent.GetCorecurrent().bolLblJSWC = true;
                        Corecurrent.GetCorecurrent().IsWritePlc = false;
                    }
                    if(Corecurrent.GetCorecurrent().bolJGY == false
                    && Corecurrent.GetCorecurrent().bolJGN == false
                    && Corecurrent.GetCorecurrent().bolJGW == false)
                    {
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 0, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 1, false);
                        Snap7.S7.SetBitAt(ref BufferJGYN, 0, 2, false);
                        s7Client.DBWrite(2002, 0, 1, BufferJGYN);
                        Corecurrent.GetCorecurrent().bolLblYXJG = false;
                        Corecurrent.GetCorecurrent().bolLblJZJG = false;
                        Corecurrent.GetCorecurrent().bolLblJSWC = false;
                        Corecurrent.GetCorecurrent().IsWritePlc = false;
                    }
                }
            }
        }

        bool bolGetAlarm()
        {
            bool BolAlarm = false;
            //传输报警
            //RFID通讯异常
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.0"))
                {
                    SetRows("传输报警", "DB2001DBX62.0", "RFID通讯异常");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX62.0", "RFID通讯异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.0"))
                {
                    DelRows("DB2001DBX62.0");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX62.0");
                }
            }
            //RFID信号强度弱
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.1"))
                {
                    SetRows("传输报警", "DB2001DBX62.1", "RFID信号强度弱");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX62.1", "RFID信号强度弱");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.1"))
                {
                    DelRows("DB2001DBX62.1");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX62.1");
                }
            }
            //上层进料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.2"))
                {
                    SetRows("传输报警", "DB2001DBX62.2", "上层进料异常");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX62.2", "上层进料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.2"))
                {
                    DelRows("DB2001DBX62.2");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX62.2");
                }
            }
            //上层出料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.3"))
                {
                    SetRows("传输报警", "DB2001DBX62.3", "上层出料异常");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX62.3", "上层出料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.3"))
                {
                    DelRows("DB2001DBX62.3");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX62.3");
                }
            }
            //下层进料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.4"))
                {
                    SetRows("传输报警", "DB2001DBX62.4", "下层进料异常");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX62.4", "下层进料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.4"))
                {
                    DelRows("DB2001DBX62.4");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX62.4");
                }
            }
            //下层出料异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 5))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.5"))
                {
                    SetRows("传输报警", "DB2001DBX62.5", "下层出料异常");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX62.5", "下层出料异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.5"))
                {
                    DelRows("DB2001DBX62.5");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX62.5");
                }
            }
            //上层保护光电异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 6))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.6"))
                {
                    SetRows("传输报警", "DB2001DBX62.6", "上层保护光电异常");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX62.6", "上层保护光电异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.6"))
                {
                    DelRows("DB2001DBX62.6");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX62.6");
                }
            }
            //下层保护光电异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 0, 7))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.7"))
                {
                    SetRows("传输报警", "DB2001DBX62.7", "下层保护光电异常");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX62.7", "下层保护光电异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX62.7"))
                {
                    DelRows("DB2001DBX62.7");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX62.7");
                }
            }
            //升降抬起异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 1, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX63.0"))
                {
                    SetRows("传输报警", "DB2001DBX63.0", "升降抬起异常");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX63.0", "升降抬起异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX63.0"))
                {
                    DelRows("DB2001DBX63.0");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX63.0");
                }
            }
            //升降落下异常
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 1, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX63.1"))
                {
                    SetRows("传输报警", "DB2001DBX63.1", "升降落下异常");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX63.1", "升降落下异常");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX63.1"))
                {
                    DelRows("DB2001DBX63.1");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX63.1");
                }
            }
            //NG标志
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 1, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX63.2"))
                {
                    SetRows("传输报警", "DB2001DBX63.2", "NG标志");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX63.2", "NG标志");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX63.2"))
                {
                    DelRows("DB2001DBX63.2");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX63.2");
                }
            }
            //中转台无SN码
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 1, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX63.3"))
                {
                    SetRows("传输报警", "DB2001DBX63.3", "中转台无SN码");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX63.3", "中转台无SN码");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX63.3"))
                {
                    DelRows("DB2001DBX63.3");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX63.3");
                }
            }

            //常规报警
            //面板急停报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.0"))
                {
                    SetRows("常规报警", "DB2001DBX64.0", "面板急停报警");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX64.0", "面板急停报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.0"))
                {
                    DelRows("DB2001DBX64.0");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX64.0");
                }
            }
            //安全门打开报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.1"))
                {
                    SetRows("常规报警", "DB2001DBX64.1", "安全门打开报警");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX64.1", "安全门打开报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.1"))
                {
                    DelRows("DB2001DBX64.1");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX64.1");
                }
            }
            //按钮盒急停报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.2"))
                {
                    SetRows("常规报警", "DB2001DBX64.2", "按钮盒急停报警");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX64.2", "按钮盒急停报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.2"))
                {
                    DelRows("DB2001DBX64.2");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX64.2");
                }
            }
            //搬运示教器急停报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.3"))
                {
                    SetRows("常规报警", "DB2001DBX64.3", "搬运示教器急停报警");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX64.3", "搬运示教器急停报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.3"))
                {
                    DelRows("DB2001DBX64.3");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX64.3");
                }
            }
            //贴屏示教器急停报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.4"))
                {
                    SetRows("常规报警", "DB2001DBX64.4", "贴屏示教器急停报警");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX64.4", "贴屏示教器急停报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.4"))
                {
                    DelRows("DB2001DBX64.4");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX64.4");
                }
            }
            //吸盘气源压力报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 5))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.5"))
                {
                    SetRows("常规报警", "DB2001DBX64.5", "吸盘气源压力报警");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX64.5", "吸盘气源压力报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.5"))
                {
                    DelRows("DB2001DBX64.5");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX64.5");
                }
            }
            //传输气源压力报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 6))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.6"))
                {
                    SetRows("常规报警", "DB2001DBX64.6", "传输气源压力报警");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX64.6", "传输气源压力报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.6"))
                {
                    DelRows("DB2001DBX64.6");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX64.6");
                }
            }
            //相机不在运行中
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 2, 7))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.7"))
                {
                    SetRows("常规报警", "DB2001DBX64.7", "相机不在运行中");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX64.7", "相机不在运行中");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX64.7"))
                {
                    DelRows("DB2001DBX64.7");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX64.7");
                }
            }
            //配方与相机程序不符
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 3, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX65.0"))
                {
                    SetRows("常规报警", "DB2001DBX65.0", "配方与相机程序不符");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX65.0", "配方与相机程序不符");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX65.0"))
                {
                    DelRows("DB2001DBX65.0");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX65.0");
                }
            }
            //无双单吸盘选择
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 3, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX65.1"))
                {
                    SetRows("常规报警", "DB2001DBX65.1", "无双单吸盘选择");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX65.1", "无双单吸盘选择");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX65.1"))
                {
                    DelRows("DB2001DBX65.1");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX65.1");
                }
            }
            //双工位螺丝机无配方
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 3, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX65.2"))
                {
                    SetRows("常规报警", "DB2001DBX65.2", "双工位螺丝机无配方");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX65.2", "双工位螺丝机无配方");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX65.2"))
                {
                    DelRows("DB2001DBX65.2");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX65.2");
                }
            }

            //伺服报警
            //伺服暂停
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 4, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX66.0"))
                {
                    SetRows("伺服报警", "DB2001DBX66.0", "伺服暂停");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX66.0", "伺服暂停");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX66.0"))
                {
                    DelRows("DB2001DBX66.0");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX66.0");
                }
            }
            //伺服未使能
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 4, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX66.1"))
                {
                    SetRows("伺服报警", "DB2001DBX66.1", "伺服未使能");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX66.1", "伺服未使能");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX66.1"))
                {
                    DelRows("DB2001DBX66.1");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX66.1");
                }
            }
            //伺服正转软极限
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 4, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX66.2"))
                {
                    SetRows("伺服报警", "DB2001DBX66.2", "伺服正转软极限");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX66.2", "伺服正转软极限");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX66.2"))
                {
                    DelRows("DB2001DBX66.2");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX66.2");
                }
            }
            //伺服反转软极限
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 4, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX66.3"))
                {
                    SetRows("伺服报警", "DB2001DBX66.3", "伺服反转软极限");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX66.3", "伺服反转软极限");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX66.3"))
                {
                    DelRows("DB2001DBX66.3");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX66.3");
                }
            }

            //搬运机器人报警
            //示教器未自动
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.0"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX68.0", "示教器未自动");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX68.0", "示教器未自动");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.0"))
                {
                    DelRows("DB2001DBX68.0");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX68.0");
                }
            }
            //机器人报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.1"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX68.1", "机器人报警");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX68.1", "机器人报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.1"))
                {
                    DelRows("DB2001DBX68.1");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX68.1");
                }
            }
            //机器人未使能
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.2"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX68.2", "机器人未使能");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX68.2", "机器人未使能");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.2"))
                {
                    DelRows("DB2001DBX68.2");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX68.2");
                }
            }
            //机器人暂停
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.3"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX68.3", "机器人暂停");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX68.3", "机器人暂停");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.3"))
                {
                    DelRows("DB2001DBX68.3");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX68.3");
                }
            }
            //搬运吸盘气压不足
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.4"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX68.4", "搬运吸盘气压不足");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX68.4", "搬运吸盘气压不足");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.4"))
                {
                    DelRows("DB2001DBX68.4");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX68.4");
                }
            }
            //翻转吸盘气压不足
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 5))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.5"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX68.5", "翻转吸盘气压不足");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX68.5", "翻转吸盘气压不足");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.5"))
                {
                    DelRows("DB2001DBX68.5");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX68.5");
                }
            }
            //吸盘有料，无法回原位
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 6))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.6"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX68.6", "吸盘有料，无法回原位");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX68.6", "吸盘有料，无法回原位");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.6"))
                {
                    DelRows("DB2001DBX68.6");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX68.6");
                }
            }
            //机器人在干涉区
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 6, 7))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.7"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX68.7", "机器人在干涉区");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX68.7", "机器人在干涉区");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX68.7"))
                {
                    DelRows("DB2001DBX68.7");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX68.7");
                }
            }
            //翻转平台有料
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 7, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX69.0"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX69.0", "翻转平台有料");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX69.0", "翻转平台有料");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX69.0"))
                {
                    DelRows("DB2001DBX69.0");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX69.0");
                }
            }
            //搬运在干涉区，翻转平台禁止动作
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 7, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX69.1"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX69.1", "搬运在干涉区，翻转平台禁止动作");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX69.1", "搬运在干涉区，翻转平台禁止动作");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX69.1"))
                {
                    DelRows("DB2001DBX69.1");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX69.1");
                }
            }
            //双工位无SN码
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 7, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX69.2"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX69.2", "双工位无SN码");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX69.2", "双工位无SN码");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX69.2"))
                {
                    DelRows("DB2001DBX69.2");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX69.2");
                }
            }
            //螺丝机停止
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 7, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX69.3"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX69.3", "螺丝机停止");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX69.3", "螺丝机停止");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX69.3"))
                {
                    DelRows("DB2001DBX69.3");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX69.3");
                }
            }

            //贴屏机器人报警
            //示教器未自动
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 0))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.0"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX70.0", "示教器未自动");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX70.0", "示教器未自动");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.0"))
                {
                    DelRows("DB2001DBX70.0");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX70.0");
                }
            }
            //机器人报警
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 1))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.1"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX70.1", "机器人报警");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX70.1", "机器人报警");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.1"))
                {
                    DelRows("DB2001DBX70.1");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX70.1");
                }
            }
            //机器人未使能
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 2))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.2"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX70.2", "机器人未使能");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX70.2", "机器人未使能");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.2"))
                {
                    DelRows("DB2001DBX70.2");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX70.2");
                }
            }
            //机器人暂停
            if (Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 3))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.3"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX70.3", "机器人暂停");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX70.3", "机器人暂停");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.3"))
                {
                    DelRows("DB2001DBX70.3");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX70.3");
                }
            }
            //贴屏吸盘气压不足
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 4))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.4"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX70.4", "贴屏吸盘气压不足");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX70.4", "贴屏吸盘气压不足");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.4"))
                {
                    DelRows("DB2001DBX70.4");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX70.4");
                }
            }
            //吸盘有料，无法回原位
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 5))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.5"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX70.5", "吸盘有料，无法回原位");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX70.5", "吸盘有料，无法回原位");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.5"))
                {
                    DelRows("DB2001DBX70.5");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX70.5");
                }
            }
            //机器人在干涉区
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 6))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.6"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX70.6", "机器人在干涉区");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX70.6", "机器人在干涉区");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.6"))
                {
                    DelRows("DB2001DBX70.6");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX70.6");
                }
            }
            //拍照NG
            if(Snap7.S7.GetBitAt(Corecurrent.GetCorecurrent().BufferBJ, 8, 7))
            {
                BolAlarm = true;
                if (!Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.7"))
                {
                    SetRows("搬运机器人报警", "DB2001DBX70.7", "拍照NG");
                    Corecurrent.GetCorecurrent().mapAlarm.Add("DB2001DBX70.7", "拍照NG");
                }
            }
            else
            {
                if (Corecurrent.GetCorecurrent().mapAlarm.Keys.Contains("DB2001DBX70.7"))
                {
                    DelRows("DB2001DBX70.7");
                    Corecurrent.GetCorecurrent().mapAlarm.Remove("DB2001DBX70.7");
                }
            }
            return BolAlarm;
        }
        void SetRows(string strBJLB, string strDW, string strBJXX)
        {
            Corecurrent.GetCorecurrent().bolTableRe = false;
            DataRow drAlarm = Corecurrent.GetCorecurrent().dtAlarm.NewRow();
            drAlarm["时间"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            drAlarm["报警类别"] = strBJLB;
            drAlarm["点位"] = strDW;
            drAlarm["报警信息"] = strBJXX;
            Corecurrent.GetCorecurrent().dtAlarm.Rows.Add(drAlarm);
            Corecurrent.GetCorecurrent().dtAlarm.AcceptChanges();
            Corecurrent.GetCorecurrent().bolTableRe = true;
        }

        void DelRows(string strDW)
        {
            Corecurrent.GetCorecurrent().bolTableRe = false;
            foreach (DataRow drAlarm in Corecurrent.GetCorecurrent().dtAlarm.Rows)
            {
                if (drAlarm["点位"].ToString() == strDW)
                {
                    drAlarm.Delete();
                }
            }
            Corecurrent.GetCorecurrent().dtAlarm.AcceptChanges();
            Corecurrent.GetCorecurrent().bolTableRe = true;
        }
    }
}
