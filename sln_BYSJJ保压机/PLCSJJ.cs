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
    class PLCSJJ
    {
        public S7Client s7Client;
        public static PLCSJJ plcsjj;
        public PLCSJJ()
        {
            int Result;
            //snap7初始化
            s7Client = new S7Client();
            Result = s7Client.ConnectTo("10.217.17.59", 0, 1);
            //Result = s7Client.ConnectTo("127.0.0.1", 0, 1);
            if (Result == 0)
            {
                Corecurrent.GetCorecurrent().SJJ_IsConnnectPlc = true;
            }
            Thread plc_Td = new Thread(PlcAlarmRe);
            plc_Td.IsBackground = true;
            plc_Td.Start();
        }
        public static PLCSJJ GetPLCSJJ()
        {
            return plcsjj;
        }

        void PlcAlarmRe()
        {
            while (true)
            {
                if (s7Client.Connected())
                {
                    Corecurrent.GetCorecurrent().SJJ_IsConnnectPlc = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().SJJ_IsConnnectPlc = false;
                }

                //主SN码
                byte[] SJJ_BufferPLCSN = new byte[30];
                s7Client.DBRead(2001, 4, 30, SJJ_BufferPLCSN);
                var byteArrPLCSN = new byte[28];
                Array.Copy(SJJ_BufferPLCSN, 2, byteArrPLCSN, 0, 28);
                Corecurrent.GetCorecurrent().SJJ_strGKJSN = System.Text.Encoding.ASCII.GetString(byteArrPLCSN);

                byte[] Buffer = new byte[3];
                s7Client.DBRead(2001, 0, 3, Buffer);
                //首升降机请求发送
                if (Snap7.S7.GetBitAt(Buffer, 0, 0) == true)
                {
                    Corecurrent.GetCorecurrent().SJJ_bolLblQQFS = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().SJJ_bolLblQQFS = false;
                }

                //尾升降机写入完成
                if (Snap7.S7.GetBitAt(Buffer, 2, 0) == true)
                {
                    Corecurrent.GetCorecurrent().SJJ_bolLblPGXRWC = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().SJJ_bolLblPGXRWC = false;
                }
                //首升降机接收请求
                if (Snap7.S7.GetBitAt(Buffer, 0, 0) == true)
                {
                    Corecurrent.GetCorecurrent().SSJJ_bolSJS = true;
                    Corecurrent.GetCorecurrent().SSJJ_NowProcedure = Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_GETQQ;
                }
                else
                {
                    Corecurrent.GetCorecurrent().SSJJ_NowProcedure = Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_IDLE;
                    if (Corecurrent.GetCorecurrent().SSJJ_bolSJS == true)
                    {
                        Corecurrent.GetCorecurrent().mapMake.Remove("SSJJ_WAITASKPROCESS");
                        Corecurrent.GetCorecurrent().mapMake.Remove("SSJJ_GETQQ");

                        Corecurrent.GetCorecurrent().mapInfo.Remove("SSJJ_DB200100TRUE");
                        Corecurrent.GetCorecurrent().SJJ_IsWriteFlag = "SSJJ_SNWC_QK";
                        Corecurrent.GetCorecurrent().SJJ_IsWritePlc = true;
                        Corecurrent.GetCorecurrent().SSJJ_bolSJS = false;
                    }
                }
                //尾升降机接收请求
                if (Snap7.S7.GetBitAt(Buffer, 2, 0) == true)
                {
                    Corecurrent.GetCorecurrent().WSJJ_bolSJS = true;
                    Corecurrent.GetCorecurrent().WSJJ_NowProcedure = Corecurrent.WSJJ_SysRunStatus.WSJJ_Procedure.WSJJ_PROCEDURE_DB;
                }
                else
                {
                    Corecurrent.GetCorecurrent().WSJJ_NowProcedure = Corecurrent.WSJJ_SysRunStatus.WSJJ_Procedure.WSJJ_PROCEDURE_IDLE;
                    if (Corecurrent.GetCorecurrent().WSJJ_bolSJS == true)
                    {
                        Corecurrent.GetCorecurrent().mapMake.Remove("WSJJ_WAITASKPROCESS");
                        Corecurrent.GetCorecurrent().mapMake.Remove("WSJJ_GETQQ");

                        Corecurrent.GetCorecurrent().mapInfo.Remove("WSJJ_DB200100TRUE");
                        Corecurrent.GetCorecurrent().SJJ_IsWriteFlag = "WSJJ_DB_QK";
                        Corecurrent.GetCorecurrent().SJJ_IsWritePlc = true;
                        Corecurrent.GetCorecurrent().WSJJ_bolSJS = false;
                    }
                }

                if (Corecurrent.GetCorecurrent().SJJ_IsWritePlc == true)
                {
                    if(Corecurrent.GetCorecurrent().SJJ_IsWriteFlag == "SSJJ_SNWC_WR")
                    {
                        byte[] SJJ_BufferSN = new byte[28];
                        Snap7.S7.SetStringAt(SJJ_BufferSN, 0, 28, Corecurrent.GetCorecurrent().SJJ_strSCANSN);
                        s7Client.DBWrite(2002, 2, 28, SJJ_BufferSN);

                        byte[] SJJ_BufferXRWC = new byte[1];
                        Snap7.S7.SetBitAt(ref SJJ_BufferXRWC, 0, 0, true);
                        s7Client.DBWrite(2002, 0, 1, SJJ_BufferXRWC);

                        Corecurrent.GetCorecurrent().SJJ_bolLblGPXRWC = true;
                        Corecurrent.GetCorecurrent().SJJ_strPLCSN = Corecurrent.GetCorecurrent().SJJ_strSCANSN;
                    }
                    else if (Corecurrent.GetCorecurrent().SJJ_IsWriteFlag == "SSJJ_SNWC_QK")
                    {
                        byte[] SJJ_BufferXRWC = new byte[1];
                        Snap7.S7.SetBitAt(ref SJJ_BufferXRWC, 0, 0, false);
                        s7Client.DBWrite(2002, 0, 0, SJJ_BufferXRWC);

                        Corecurrent.GetCorecurrent().SJJ_bolLblGPXRWC = false;
                    }
                    else if (Corecurrent.GetCorecurrent().SJJ_IsWriteFlag == "WSJJ_DBWC_WR")
                    {
                        byte[] SJJ_BufferDBWC = new byte[1];
                        Snap7.S7.SetBitAt(ref SJJ_BufferDBWC, 0, 0, true);
                        Snap7.S7.SetBitAt(ref SJJ_BufferDBWC, 0, 1, false);
                        s7Client.DBWrite(2002, 32, 1, SJJ_BufferDBWC);
                        Corecurrent.GetCorecurrent().WSJJ_bolLblDBWC = true;
                        Corecurrent.GetCorecurrent().WSJJ_bolLblDBSB = false;
                    }
                    else if (Corecurrent.GetCorecurrent().SJJ_IsWriteFlag == "WSJJ_DBSB_WR")
                    {
                        byte[] SJJ_BufferDBSB = new byte[1];
                        Snap7.S7.SetBitAt(ref SJJ_BufferDBSB, 0, 0, false);
                        Snap7.S7.SetBitAt(ref SJJ_BufferDBSB, 0, 1, true);
                        s7Client.DBWrite(2002, 32, 1, SJJ_BufferDBSB);
                        Corecurrent.GetCorecurrent().WSJJ_bolLblDBWC = false;
                        Corecurrent.GetCorecurrent().WSJJ_bolLblDBSB = true;
                    }
                    else if (Corecurrent.GetCorecurrent().SJJ_IsWriteFlag == "WSJJ_DB_QK")
                    {
                        byte[] SJJ_BufferDBQK = new byte[1];
                        Snap7.S7.SetBitAt(ref SJJ_BufferDBQK, 0, 0, false);
                        Snap7.S7.SetBitAt(ref SJJ_BufferDBQK, 0, 1, false);
                        s7Client.DBWrite(2002, 32, 1, SJJ_BufferDBQK);
                        Corecurrent.GetCorecurrent().WSJJ_bolLblDBWC = false;
                        Corecurrent.GetCorecurrent().WSJJ_bolLblDBSB = false;
                    }
                        Corecurrent.GetCorecurrent().SJJ_IsWritePlc = false;
                }
            }
        }
    }
}
