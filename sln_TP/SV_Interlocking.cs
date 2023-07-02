using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sln_TP
{
    public class SV_Interlocking
    {
        public static long sv_lockInfo(string sn)
        {
            StringBuilder svinfo = new StringBuilder();
            svinfo.Append(utility.dSV.DB_Password + ",");
            svinfo.Append(utility.dSV.DB_User + ",");
            svinfo.Append(utility.dSV.DatabaseName + ",");
            svinfo.Append(utility.dSV.ServerName + ",");
            svinfo.Append(sn + ",");
            svinfo.Append(utility.dSV.stationId + ",");
            svinfo.Append(utility.dSV.LineGroup + ",");
            svinfo.Append(utility.dSV.SW_User + ",");
            svinfo.Append(utility.dSV.Debug.ToString() + ",");
            svinfo.Append(utility.dSV.ShowWindow.ToString() + ",");
            svinfo.Append(utility.dSV.PassForNoDB.ToString() + ",");
            svinfo.Append(utility.dSV.Function.ToString());
            int s = sv_Interlocking_Main.sv_Interlocking_Main_Class.SV_Interlocking_Main(svinfo.ToString());

            return s;
        }
        /// <summary>
        /// SV_InterlockingFuncCode互锁功能码
        /// </summary>
        public enum SV_InterlockingFuncCode
        {
            全部功能打开 = -1,
            全部功能关闭 = 0,
            只打开Fail防错 = 1,
            只打开Pass防错 = 2,
            打开Pass和Fail防错 = 3,
            只打开互锁 = 4,
            互锁和Fail防错 = 5,
            互锁和Pass防错 = 6,
            互锁和PassFail防错 = 7
        };
    }
}
