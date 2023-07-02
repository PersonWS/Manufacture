using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    public class Screwmachinetaskdata
    {
        public int Id { get; set; }
        /// <summary>
        /// 模式id，1拧紧，2拧松，2自由行程
        /// </summary>
        public int modeid { get; set; }
        /// <summary>
        /// 模式
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 任务项目id
        /// </summary>
        public int itemid { get; set; }
        /// <summary>
        /// 任务项目名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 取值范围
        /// </summary>
        public string scopes { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string units { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
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
