using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    public class IOInfo2
    {
        /*
         * CREATE TABLE [IOInfo2](
  [Id] INTEGER PRIMARY KEY ASC AUTOINCREMENT, 
  [Address] INT, 
  [AddrIndex] INT, 
  [Titles] VARCHAR, 
  [Status] BOOL DEFAULT False);*/

        public int Id { get; set; }
        public int Address { get; set; }    
        public int AddrIndex { get; set; }
        public string Titles { get; set; }
        public bool Status { get; set; }=false;
        public int flag { get; set; } = 0;
        public string IOstatuses { get; set; }
    }
}
