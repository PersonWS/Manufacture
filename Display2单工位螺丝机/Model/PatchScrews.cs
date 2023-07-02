namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// 补螺丝数据
    /// </summary>
    public class PatchScrews
    {
        public int Id { get; set; }
        /// <summary>
        /// SN码
        /// </summary>
        public string SN { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int LineID { get; set; }
        public string OID { get; set; } = utility.loginUserID;
        /// <summary>
        /// 工作站
        /// </summary>
        public string StationID { get; set; } = utility.dSV.stationId;
        /// <summary>
        /// 生产任务ID
        /// </summary>
        public string TaskID { get; set; }
        public System.DateTime MKD { get; set; } = System.DateTime.Now;
        /// <summary>
        /// 状态，0新数据，1补螺丝失败，2已删除，3补螺丝完成
        /// </summary>
        public int Flag { get; set; } = 0;
        public System.DateTime UPD { get; set; } = System.DateTime.Now;
    }
}
