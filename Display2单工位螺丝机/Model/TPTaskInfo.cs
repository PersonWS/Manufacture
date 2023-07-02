using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// 电批任务信息
    /// </summary>
    public class TPTaskInfo
    {
        /// <summary>
        /// 任务id，0-15
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
    }
}
