using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// 参数模块
    /// </summary>
    public class ModelParam
    {
       
        /// <summary>
        /// 参数Hex地址 0x600
        /// </summary>
        private string pid;

        public string PID
        {
            get { return pid; }
            set { pid = value; }
        }

        /// <summary>
        /// 所对应的参数名称
        /// </summary>
        private string paramName;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName
        {
            get { return paramName; }
            set { paramName = value; }
        }

        /// <summary>
        /// 参数默认值
        /// </summary>
        private string defaultValue;

        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }

 

   
}