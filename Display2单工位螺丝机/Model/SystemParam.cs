using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class SystemParam
    {
        public int Id { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string ParamValue { get; set; }
        /// <summary>
        /// 参数数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 参数小数位数值，1.23的"23"
        /// </summary>
        public int DecimalDigit { get; set; }
        /// <summary>
        /// 参数描述
        /// </summary>
        public string Describe { get; set; }
    }
}
