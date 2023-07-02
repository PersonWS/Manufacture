using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    public class ScrewMachineStepData
    {
        public int Id { get; set; }
        /// <summary>
        /// 工作模式，1拧紧，2拧松，2自由行程
        /// </summary>
        public int modeid { get; set; }
        /// <summary>
        /// step内项目序号
        /// </summary>
        public int itemid { get; set; }
        /// <summary>
        /// step内项目名称
        /// </summary>
        public string stepitem { get; set; }
        /// <summary>
        /// step号
        /// </summary>
        public int stepid { get; set; }
        /// <summary>
        /// 显示标志，大于0显示，0不显示
        /// </summary>
        public int isuse { get; set; }
        /// <summary>
        /// 任务0地址，其余任务地址=startaddress+任务号*150
        /// </summary>
        public ushort startaddress { get; set; }
        
    }
}