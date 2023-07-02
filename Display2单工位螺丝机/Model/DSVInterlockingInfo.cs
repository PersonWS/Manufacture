using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    public class DSVInterlockingInfo
    {
        public int Id { get; set; }
        public string DB_Password { get; set; }
        public string DB_User { get; set; }
        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public string LineGroup { get; set; }
        public string SW_User { get; set; }
        public string stationId { get; set; }
        
        public bool Debug { get; set; }=false;
        public bool ShowWindow { get; set; }=true;
        public bool PassForNoDB { get; set; } = false;
        public int Function { get; set; } = 5;
        public DateTime UPD { get; set; } = DateTime.Now;
        public string OID { get; set; } = utility.loginUserID;

        public string CID { get; set; } = utility.stationId;
        /// <summary>
        /// 工作模式，0:离线false，1:MES互锁true
        /// </summary>
        public bool workMode { get; set; } = true;
        
    }
}
