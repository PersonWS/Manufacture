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
    class PLCSJJ
    {
        public S7Client s7Client;
        public static PLCSJJ plcsjj;
        public PLCSJJ()
        {
            int Result;
            //snap7初始化
            s7Client = new S7Client();
            Result = s7Client.ConnectTo("10.217.12.10", 0, 1);
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
                //升降机请求发送
                if (Snap7.S7.GetBitAt(Buffer, 0, 0) == true)
                {
                    Corecurrent.GetCorecurrent().SJJ_bolLblQQFS = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().SJJ_bolLblQQFS = false;
                }

                //升降机写入完成
                if (Snap7.S7.GetBitAt(Buffer, 2, 0) == true)
                {
                    Corecurrent.GetCorecurrent().SJJ_bolLblPGXRWC = true;
                }
                else
                {
                    Corecurrent.GetCorecurrent().SJJ_bolLblPGXRWC = false;
                }

                if (Snap7.S7.GetBitAt(Buffer, 0, 0) == true)
                {
                    Corecurrent.GetCorecurrent().SJJ_bolSJS = true;
                    Corecurrent.GetCorecurrent().SSJJ_NowProcedure = Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_GETQQ;
                }
                else
                {
                    Corecurrent.GetCorecurrent().SSJJ_NowProcedure = Corecurrent.SSJJ_SysRunStatus.SSJJ_Procedure.SSJJ_PROCEDURE_IDLE;
                    if (Corecurrent.GetCorecurrent().SJJ_bolSJS == true)
                    {
                        Corecurrent.GetCorecurrent().mapMake.Remove("SJJ_WAITASKPROCESS");
                        Corecurrent.GetCorecurrent().mapMake.Remove("SJJ_GETQQ");

                        Corecurrent.GetCorecurrent().mapInfo.Remove("SJJ_DB200100TRUE");
                        Corecurrent.GetCorecurrent().SJJ_IsWriteFlag = "SNWC_QK";
                        Corecurrent.GetCorecurrent().SJJ_IsWritePlc = true;
                        Corecurrent.GetCorecurrent().SJJ_bolSJS = false;
                    }
                }


                if (Corecurrent.GetCorecurrent().SJJ_IsWritePlc == true)
                {
                    if(Corecurrent.GetCorecurrent().SJJ_IsWriteFlag == "SNWC_WR")
                    {
                        byte[] SJJ_BufferSN = new byte[30];
                        Snap7.S7.SetStringAt(SJJ_BufferSN, 0, 28, Corecurrent.GetCorecurrent().SJJ_strSCANSN);
                        s7Client.DBWrite(2002, 2, 30, SJJ_BufferSN);

                        byte[] SJJ_BufferXRWC = new byte[1];
                        Snap7.S7.SetBitAt(ref SJJ_BufferXRWC, 0, 0, true);
                        s7Client.DBWrite(2002, 0, 1, SJJ_BufferXRWC);

                        Corecurrent.GetCorecurrent().SJJ_bolLblGPXRWC = true;
                        Corecurrent.GetCorecurrent().SJJ_strPLCSN = Corecurrent.GetCorecurrent().SJJ_strSCANSN;
                    }
                    else if (Corecurrent.GetCorecurrent().SJJ_IsWriteFlag == "SNWC_QK")
                    {
                        byte[] SJJ_BufferSN = new byte[30];
                        Snap7.S7.SetStringAt(SJJ_BufferSN, 0, 0, "");
                        s7Client.DBWrite(2002, 2, 30, SJJ_BufferSN);

                        byte[] SJJ_BufferXRWC = new byte[1];
                        Snap7.S7.SetBitAt(ref SJJ_BufferXRWC, 0, 0, false);
                        s7Client.DBWrite(2002, 0, 1, SJJ_BufferXRWC);

                        Corecurrent.GetCorecurrent().SJJ_bolLblGPXRWC = false;
                        Corecurrent.GetCorecurrent().SJJ_strPLCSN = "";
                        Corecurrent.GetCorecurrent().SJJ_strSCANSN = "";
                    }
                    
                    Corecurrent.GetCorecurrent().SJJ_IsWritePlc = false;
                }
            }
        }
    }
}
