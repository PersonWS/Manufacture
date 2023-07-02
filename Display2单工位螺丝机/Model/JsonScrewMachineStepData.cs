using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{

    public class ScrewMachineActiveData
    {

        /// <summary>
        /// 步骤内容
        /// </summary>
        public class StepItems
        {
            /// <summary>
            /// 动作名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 十进制地址
            /// </summary>
            public int DecAddress { get; set; }
        }

        public class ActiveItem
        {
            /// <summary>
            /// 动作Id，1拧紧，2拧松，3自由行程
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 动作名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 步骤数量
            /// </summary>
            public int StepNums { get; set; }
            /// <summary>
            /// 步骤list
            /// </summary>
            public List<StepItems> stepItems { get; set; }
        }

        /// <summary>
        /// Model，动作步骤item
        /// </summary>
        public class ActiveObject
        {
            /// <summary>
            /// 解释，
            /// </summary>
            public string explain { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ActiveItem> Active { get; set; }
        }


    }


}
