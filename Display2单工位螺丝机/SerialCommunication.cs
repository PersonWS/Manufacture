
using Newtonsoft.Json;
using PDAMaster;
using System;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    /// <summary>
    /// 串口class
    /// </summary>
    public class SerialCommunication
    {

        public static string esDriver_ip = "";
        public static int esDriver_port = 5000;

        /// <summary>
        /// 读取模式
        /// </summary>
        public enum ReadType
        {
            /// <summary>
            /// 功能码01,读线圈寄存器，位操作
            /// </summary>
            Read01 = 0x01,
            /// <summary>
            /// 功能码02，读离散输入寄存器，位操作
            /// </summary>
            Read02 = 0x02,
            /// <summary>
            /// 功能码03，读保持寄存器，字操作
            /// </summary>
            Read03 = 0x03,
            /// <summary>
            /// 功能码04，读输入寄存器，字操作
            /// </summary>
            Read04 = 0x04

        }


        /// <summary>
        /// 写入模式
        /// </summary>
        public enum WriteType
        {
            /// <summary>
            /// //功能码05,写单个线圈寄存器，位操作
            /// </summary>
            Write05 = 0x05,
            /// <summary>
            /// //功能码06，写单个保持寄存器，字操作
            /// </summary>
            Write06 = 0x06,
            /// <summary>
            /// //功能码0F，写多个线圈寄存器，位操作
            /// </summary>
            Write05s = 0x0F,
            /// <summary>
            /// //功能码10，写多个保持寄存器，字操作
            /// </summary>
            Write06s = 0x10
        }

        /// <summary>
        /// 遍历serialport
        /// </summary>
        public static void getSerialParams(ComboBox cbox)
        {
            cbox.Items.Clear();
            foreach (string vPortName in System.IO.Ports.SerialPort.GetPortNames())
            {
                cbox.Items.Add(vPortName);
            }
        }

        


      
 
        public static Model.ResultJsonInfo setSerial()
        {
            string portName = "";
            Model.ResultJsonInfo jRRInfo = new Model.ResultJsonInfo();
            try
            {
                //S7NetPlus.S71200_IP = ConfigurationKeys.PLC_IP;// 
                //S7NetPlus.S71200_Rack = (short)ConfigurationKeys.PLC_Rack;
                //S7NetPlus.S71200_Slot = (short)ConfigurationKeys.PLC_Slot;

                esDriver_ip = ConfigurationKeys.ScrewMachineIP1;
                esDriver_port = ConfigurationKeys.ScrewMachinePort1;

              
                
                    
              

                    //var v = utility.modbusObjects[2];
                    //portName = v.name;
                    //v.serialPort.PortName = v.comport;
                    //v.serialPort.BaudRate = v.baud;
                    //v.serialPort.DataBits = 8;
                    //v.serialPort.StopBits = System.IO.Ports.StopBits.One;
                    //v.serialPort.Parity = System.IO.Ports.Parity.None;
                    
                    //    if (!v.serialPort.IsOpen)   //有的Modbus设备共用一个端口
                    //        v.serialPort.Open();
                    
                    //if (jRRInfo.Message == null)
                    //    jRRInfo.Message = portName + "端口设置成功:" + v.comport;
                    //else
                    //    jRRInfo.Message = jRRInfo.Message + Environment.NewLine + portName + "端口设置成功:" + v.comport;
                

                jRRInfo.Successed = true;
                jRRInfo.Message = jRRInfo.Message + Environment.NewLine + "串行端口设置打开成功!";
                return jRRInfo;
            }
            catch (Exception ex)
            {
                jRRInfo.Message = "通讯端口错误,请配置端口，或者检查通讯线路!" + portName + "," + ex.Message;
                jRRInfo.exception = ex;
                return jRRInfo;
            }

        }
        

    }
}

