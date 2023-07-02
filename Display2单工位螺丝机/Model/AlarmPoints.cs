using System;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// 报警节点信息
    /// </summary>
    public class AlarmPoints
    {

        public int Id { get; set; }
        /// <summary>
        /// 报警信息
        /// </summary>
        public string AlarmInfo { get; set; }
        /// <summary>
        /// PLC DB块
        /// </summary>
        public int DB { get; set; }
        /// <summary>
        /// 读取地址
        /// </summary>
        public int Address { get; set; }
        /// <summary>
        /// 信息位，0-7
        /// </summary>
        public int Bits { get; set; }
        /// <summary>
        /// 显示处理方式，0弹出，需要响应；1T显示F恢复
        /// </summary>
        public int ProcessMode { get; set; }
        /// <summary>
        /// 显示信息
        /// </summary>
        public string DisplayInfo { get; set; }

        public DateTime MKD { get; set; } = DateTime.Now;
        public string OID { get; set; } = utility.loginUserID;
        /// <summary>
        /// 实时状态，不保存
        /// </summary>
        public bool realStatue { get; set; } = false;
    }
}
