using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    public class WorkTaskDetail
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public int Id{ get; set; }
        /// <summary>
        /// 加工任务ID，WorkTask.TaskId
        /// </summary>
        public string taskID { get; set; }
        /// <summary>
        /// 产品码，8位
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 产品条码，P码
        /// </summary>
        public string productSN { get; set; }
        /// <summary>
        /// 产品条码,M码
        /// </summary>
        public string productSN_M { get; set; }
        /// <summary>
        /// 螺丝序号
        /// </summary>
        public int xh { get; set; }
        
        /// <summary>
        /// 扭力值
        /// </summary>
        public double TorisionValue { get; set; }
        /// <summary>
        /// 圈数
        /// </summary>
        public double CylinderNumber { get; set; }
        /// <summary>
        /// 完成状态,OK ,NG
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 浮高NG
        /// </summary>
        public int FloatingHigh { get; set; }
        /// <summary>
        /// 滑牙NG
        /// </summary>
        public int ScrewLoose { get; set; }
        /// <summary>
        /// 其他NG
        /// </summary>
        public string OtherErr { get; set; }
        public DateTime mkd { get; set; }=DateTime.Now;
        /// <summary>
        /// 上传状态，0未，1已
        /// </summary>
        public int uploadStatus { get; set; } = 0;
        /// <summary>
        /// 删除标记，1删除
        /// </summary>
        public int deleteflag { get; set; } = 0;
        /// <summary>
        /// 加工工位ID
        /// </summary>
        public int workStationId { get; set; }
        public DateTime upd { get; set; } = DateTime.Now;
        public string oid { get; set; } = utility.loginUserID;

        public DateTime datetimeid { get; set; } 
    }
    /// <summary>
    /// 扫码结构
    /// </summary>
    public class struckScanProductSN
    {
        /// <summary>
        /// 扫描的产品SN，正在加工的SN
        /// </summary>
        public string productSN { get; set; }="";
        /// <summary>
        /// 扫描的产品M码
        /// </summary>
        public string productSN_M { get; set; } = "";
        /// <summary>
        /// 选择的工位，0未选，1左，2右
        /// </summary>
        public Int16 stationId = 1;
    }
}
