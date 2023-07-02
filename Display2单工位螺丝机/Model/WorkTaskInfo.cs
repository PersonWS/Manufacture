using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// 生产任务总表
    /// </summary>
    public class WorkTaskInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 工单任务ID
        /// </summary>
        public string taskID { get; set; }
        /// <summary>
        /// 8位产品码
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 工单日期
        /// </summary>
        public DateTime taskDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 任务数量
        /// </summary>
        public int taskQty { get; set; }
        /// <summary>
        /// 示教器任务ID
        /// </summary>
        public int TPID { get; set; }
        /// <summary>
        /// 生产任务状态，0新任务，1已完成，2终止，3废弃
        /// </summary>
        public int Status { get; set; } = 0;
        public string UserID { get; set; } = utility.loginUserID;
        public string CID { get; set; } = utility.stationId;
        public DateTime mkd { get; set; } = DateTime.Now;
        /// <summary>
        /// 螺丝数量
        /// </summary>
        public int NumberOfScrews { get; set; }
        /// <summary>
        /// 上传状态，0未，1已
        /// </summary>
        public int uploadStatus { get; set; }
    }

    public struct structNewWorkTaskInfo
    {
        public string taskID;
        
        /// <summary>
        /// 新任务产品码
        /// </summary>
        public string productCode;
        /// <summary>
        /// 示教器任务号
        /// </summary>
        public int tpid;
        /// <summary>
        /// 新任务数量
        /// </summary>
        public int Qty;
        /// <summary>
        /// 任务客户
        /// </summary>
        public string Customer;
        /// <summary>
        /// 螺丝数量
        /// </summary>
        public int NumberOfScrews;
        /// <summary>
        /// 机型
        /// </summary>
        public string machinemodel;
        /// <summary>
        /// 项目阶段
        /// </summary>
        public string projectphase;
        /// <summary>
        /// 默认工位
        /// </summary>
        public int defWorkStationID;
        /// <summary>
        /// 产品SN_P码长度，符合长度代替回车
        /// </summary>
        public int SNCodeLenght_P;
        /// <summary>
        /// 产品SN_P码长度，符合长度代替回车
        /// </summary>
        public int SNCodeLenght_M;
        /// <summary>
        /// 产品码前缀
        /// </summary>
        public string PrefixCode;
        /// <summary>
        /// 是否校验前缀
        /// </summary>
        public bool checkfixCode;
    }

}
