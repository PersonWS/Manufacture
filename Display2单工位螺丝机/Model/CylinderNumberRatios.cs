using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// 圈数系数使用范围
    /// </summary>
    public class CylinderNumberRatios
    {
        public int Id { get; set; }
        /// <summary>
        /// base地址,select ushort 溢出，暂时int
        /// </summary>
        public int baseAddress { get; set; }
        /// <summary>
        /// 圈数系数，读取值*ratio.默认kgf.m
        /// </summary>
        public double ratio { get; set; } = 0.01;
        /// <summary>
        /// 任务间隔，每任务地址=baseAddress * 任务ID * TaskInterval，0：单一地址
        /// </summary>
        public int TaskInterval { get; set; } = 150;
        /// <summary>
        /// step间隔，同一个任务内，每个step地址=baseAddress+ StepInterval * setpID
        /// </summary>
        public int StepInterval { get; set; } = 5;

    }
}
