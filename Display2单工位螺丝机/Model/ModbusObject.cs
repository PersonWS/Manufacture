using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// modbus对象，1-电批，2-运动控制，3-IO输入输出，4-模拟量采集
    /// </summary>
    public class ModbusObject
    {
        /// <summary>
        /// 顺序号，不能修改
        /// 1-电批，2-示教器，3-IO输入输出，4-模拟量采集
        /// </summary>
        public int orderid { get; set; }
        /// <summary>
        /// modbus设备mcheng 
        /// </summary>
        public string name { get; set; }
        public string comport { get; set; }
        public int baud { get; set; }
        /// <summary>
        /// 从机地址，
        /// </summary>
        public int slaveid { get; set; }
        /// <summary>
        /// 启用，1，0禁用
        /// </summary>
        public int isuse { get; set; }
        /// <summary>
        /// 本机设备号存储地址
        /// </summary>
        public int DecAddress { get; set; }


        public string remark { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string MachineModel { get; set; }

        public SerialPort serialPort { get; set; }=new SerialPort();
    }
    public enum ModbusDevice
    {
        /// <summary>
        /// 电批
        /// </summary>
        ScrewDriver = 0,
        /// <summary>
        /// 运动控制器
        /// </summary>
        MotionController = 1,
        /// <summary>
        /// 分布式IO
        /// </summary>
        Distributed_IO = 2,
        /// <summary>
        /// 扭力检测仪
        /// </summary>
        TorsionMeter = 3,
    }
    
}
