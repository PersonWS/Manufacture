using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// 电批参数
    /// </summary>
  public  class Mscrewdriver
    {

        public string id { get; set; }

        /// <summary>
        /// 设备地址
        /// </summary>
        public string serialport { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int baudrate { get; set; }

        /// <summary>
        /// 标志，0禁用，1使用
        /// </summary>
        public int flag { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
