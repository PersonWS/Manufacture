using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    //如果好用，请收藏地址，帮忙分享。
    public class JsonScrewMachineTaskData
    {
        /// <summary>
        /// 任务内项目
        /// </summary>
        public class TaskDataItem
        {
            /// <summary>
            /// 项目名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 十进制地址
            /// </summary>
            public int DecAddress { get; set; }
        }

        /// <summary>
        /// 任务内容
        /// </summary>
        public class TaskItem
        {
            /// <summary>
            /// 任务id，，1拧紧，2拧松，3自由行程
            /// </summary>
            public int Id { get; set; }
            
            /// <summary>
            /// 任务名称
            /// </summary>
            public string TaskName { get; set; }
            /// <summary>
            /// 任务内容list
            /// </summary>
            public List<TaskDataItem> TaskDataItem { get; set; }
        }

        public class TaskRoot
        {
            /// <summary>
            /// 每个任务的地址间隔
            /// </summary>
            public int TaskAddrInterval { get; set; }
            /// <summary>
            /// 任务0地址，下一个任务 taskIndex 的地址=DecAddress+TaskAddrInterval*taskIndex
            /// </summary>
            public string explain { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<TaskItem> taskItem { get; set; }
        }


    }

}
