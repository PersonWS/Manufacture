using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// IO表
    /// </summary>
    public class IOInfo
    {
        /*
            1、功能码01：读1路或多路开关量线圈输出状态
            2、功能码02：读1路或多路开关量状态输入
            3、功能码03：读多路寄存器
            4、功能码05：写1路开关量输出
            5、功能码06：写单路寄存器
            6、功能码10：写多路寄存器
        */
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 寄存器地址
        /// </summary>
        public int Address { get; set; }
        /// <summary>
        /// 读写权限，0只读，1读写
        /// </summary>
        public int RWPermission { get; set; }
        /// <summary>
        /// 允许的功能码，解析 string[]
        /// </summary>
        public string FunCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 读功能码
        /// </summary>
        public ushort FunCodeRead { get; set; }
        /// <summary>
        /// 写功能码
        /// </summary>
        public ushort FunCodeWrite { get; set; }
        /// <summary>
        /// 列名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 有效值
        /// </summary>
        public int DValue { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>

        public bool EnableOrNot { get; set; }
        public bool AllowModify { get; set; }
    }
}
