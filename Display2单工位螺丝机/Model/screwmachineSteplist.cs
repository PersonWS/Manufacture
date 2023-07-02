using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    public class screwmachineSteplist
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int Dvalue { get; set; }
        public DateTime UPD { get; set; }=DateTime.Now;
    }
}
