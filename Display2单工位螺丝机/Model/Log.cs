using System;

namespace ScrewMachineManagementSystem.Model
{
    public class Log
    {
       public DateTime LogDatetime { get; set; }=DateTime.Now;
        /// <summary>
        /// 日志类型
        /// </summary>
        public int LogType { get; set; }
        public string LogTitle { get; set; }
        public string UserID { get; set; } = utility.loginUserID;
        public string CID { get; set; } = utility.stationId;

    }
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 用户操作
        /// </summary>
        UserAction=0,
        /// <summary>
        /// 出错信息
        /// </summary>
        Error=1,
        /// <summary>
        /// 设备返回的响应
        /// </summary>
        DeviceResponse=2,
        // <summary>
        /// 报警信息
        /// </summary>
        AlarmMessages = 3
    }
}
