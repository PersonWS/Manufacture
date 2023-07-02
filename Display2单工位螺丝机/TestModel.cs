using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem
{
    
    public  class TestModel
    {
        
        public String ID { get; set; }

        /// <summary>
        /// 单据ID，UseMode=0：投产计划单号CPDYYYYMM0001,1:生产计划单号MODYYYYMM0001
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 单据ID，UseMode=0：投产计划单号CPDYYYYMM0001,1:生产计划单号MODYYYYMM0001
        /// </summary>
        public String Name { get; set; }

        public DateTime Birthday { get; set; }

        public int Sex { get; set; }

    }
}
