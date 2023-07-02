using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem.Model
{
    /// <summary>
    /// 消息类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultJsonInfo
    {
        
        public bool Successed { get; set; }=false;
        public string Message { get; set; }
        public double doubleValue { get; set; }
        public string stringValue { get; set; }
        public Int32 intValue { get; set; }
        public bool[] booValue { get; set; }
        public ushort[] readBuffer { get; set; }
        public Exception exception { get; set; }
        
    }

}
