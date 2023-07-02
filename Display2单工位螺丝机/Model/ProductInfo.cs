using System;

namespace ScrewMachineManagementSystem.Model
{
    public class ProductInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 产品识别码，可以包含或者不包含产品前缀PrefixCode
        /// </summary>
        public string ProductCode { get; set; } = "";
        /// <summary>
        /// 机器型号
        /// </summary>
        public string MachineModel { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 项目阶段
        /// </summary>
        public string ProjectPhase { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public decimal RunTime { get; set; }

        /// <summary>
        /// mkd
        /// </summary>
        public DateTime MKD { get; set; } = DateTime.Now;

        /// <summary>
        /// 示教器任务号
        /// </summary>
        public int TPTaskID { get; set; }
        /// <summary>
        /// 吸钉螺丝数量，吸钉
        /// </summary>
        public int NumberOfScrews { get; set; }

        public int DefaultWorkStation { get; set; } = 1;
        /// <summary>
        /// 产品SN码长度
        /// </summary>
        public int SNCodeLenght { get; set; }
        /// <summary>
        /// 产品码前缀
        /// </summary>
        public string PrefixCode { get; set; }

        /// <summary>
        /// 是否检验前缀
        /// </summary>
        public bool checkPrefixCode { get; set; }
    }



}
