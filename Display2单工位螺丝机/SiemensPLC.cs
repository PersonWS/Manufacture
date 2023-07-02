using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScrewMachineManagementSystem
{
    public class SiemensPLC
    {
        /*
        https://www.codenong.com/cs106385415/
        https://blog.csdn.net/guanxiaozhi/article/details/113546156
        https://www.freesion.com/article/5658375021/
        https://blog.csdn.net/qq_36669241/article/details/126104532
        */
        /// <summary>
        /// PLC类型：S7200=0;Logo0BA8=1;S7200Smart=2;S7300=10,S7400=20;S71200=30;S71500=40;
        /// </summary>
        public int PLCType { get => _plcType; set => _plcType = value; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAdress { get => _ipAddress; set => IpAdress = value; }

        /// <summary>
        /// 机架
        /// </summary>
        public short Crack { get => _crack; set => _crack = value; }

        /// <summary>
        /// 槽
        /// </summary>
        public short Slot { get => _slot; set => _slot = value; }

        int _plcType = 40;
        string _ipAddress = "192.168.1.12";
        short _crack = 0;
        short _slot = 1;
        public static Plc _plc;

        /// <summary>
        /// PLC连接状态
        /// </summary>
        /// <returns></returns>
        public bool ConnectState { get => _plc.IsConnected; }

        /// <summary>
        /// PLC连接
        /// </summary>
        public static Model.ResultJsonInfo ConnectPLC(Plc _plc)
        {
            Model.ResultJsonInfo result = new Model.ResultJsonInfo();
            try
            {
                _plc.Open();
                result.Successed = true;
                result.Message = ("PLC连接成功");
                return result;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = ("PLC连接失败：" + ex.Message.ToString());
                return result;
            }
        }

        /// <summary>
        /// 断开PLC连接
        /// </summary>
        public static Model.ResultJsonInfo DisconnectPLC(Plc plc)
        {
            Model.ResultJsonInfo result = new Model.ResultJsonInfo();
            try
            {
                plc.Close();
                result.Successed = true;
                result.Message = ("PLC断开成功");
                return result;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = ("PLC断开失败：" + ex.Message.ToString());
                return result;
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static Model.ResultJsonInfo Read(string varName, string varType)
        {
            Model.ResultJsonInfo result = new Model.ResultJsonInfo();
            try
            {
                switch (varType)
                {
                    case "布尔":
                    case "整数":
                        result.intValue = (int)_plc.Read(varName);
                        break;
                    case "实数":
                        result.doubleValue = ((uint)_plc.Read(varName)).ConvertToFloat();
                        break;
                    case "字符":
                        int dblength = varName.IndexOf(':') - 2;//DB号的长度
                        int db = Convert.ToInt32(varName.Substring(2, dblength));//DB号
                        int adrlength = varName.Length - varName.IndexOf(':') - 1;//startByteAdr的长度
                        int startByteAddress = Convert.ToInt32(varName.Substring(varName.IndexOf(':') + 1, adrlength));//startByteAdr

                        var count = (byte)_plc.Read(DataType.DataBlock, db, startByteAddress, VarType.Byte, 1);
                        result.stringValue = (string)_plc.Read(DataType.DataBlock, db, startByteAddress, VarType.S7String, count);
                        break;
                    default:
                        result.stringValue = null;
                        break;
                }
                result.Successed = true;
                result.Message = ("PLC读取成功");
                return result;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = ("PLC读取失败：" + ex.Message.ToString());
                return result;
            }
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="varValue"></param>
        /// <param name="type"></param>位、字节、单字、双字和字符
        public static Model.ResultJsonInfo Write(string varName, string varValue, string type)
        {
            Model.ResultJsonInfo result = new Model.ResultJsonInfo();
            try
            {
                switch (type)
                {
                    case "位":
                        _plc.Write(varName, (varValue == "1") ? true : false);
                        break;
                    case "字节":
                        _plc.Write(varName, Convert.ToByte(varValue));
                        break;
                    case "单字":
                        _plc.Write(varName, Convert.ToInt16(varValue));
                        break;
                    case "双字":
                        _plc.Write(varName, Convert.ToInt32(varValue));
                        break;
                    case "实数":
                        _plc.Write(varName, Convert.ToSingle(varValue));
                        break;
                    case "字符":

                        int dblength = varName.IndexOf(':') - 2;//DB号的长度
                        int db = Convert.ToInt32(varName.Substring(2, dblength));//DB号
                        int adrlength = varName.Length - varName.IndexOf(':') - 1;//startByteAdr的长度
                        int startByteAddress = Convert.ToInt32(varName.Substring(varName.IndexOf(':') + 1, adrlength));//startByteAdr

                        //var temp = System.Text.Encoding.ASCII.GetBytes(varValue);//编码
                        var temp = System.Text.Encoding.Unicode.GetBytes(varValue);//编码
                        var bytes = S7.Net.Types.S7String.ToByteArray(varValue, temp.Length);//字节数组

                        _plc.WriteBytes(DataType.DataBlock, db, startByteAddress, bytes);
                        break;
                    default:
                        break;
                }
                result.Successed = true;
                result.Message = ("PLC写入成功");
                return result;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = ("PLC写入失败：" + ex.Message.ToString());
                return result;
            }

        }

        public void getAlerm()
        {
            //读取数据选择从DB块中读取，db设置为1，起始地址为0，读取18个字节
            var bytes = SiemensPLC._plc.ReadBytes(DataType.DataBlock, 512, 0, 26);
            
            //取字节0中的第0位
            var db1Bool1 = bytes[0].SelectBit(0);
            Console.WriteLine("DB1.DBX0.0:" + db1Bool1);
            //取字节0中的第1位
            bool db1Bool2 = bytes[0].SelectBit(1); ;
            Console.WriteLine("DB1.DBX0.1:" + db1Bool2);
        }
        /*
            方法：public object Read(string variable)
            入参：读取数据地址
            出参：Object类型数据，可强制类型转换

                    var db1Bool1 = plc.Read("DB1.DBX0.0");
                    Console.WriteLine("DB1.DBX0.0:" + db1Bool1);

            bool db1Bool2 = (bool)plc.Read("DB1.DBX0.1");
                    Console.WriteLine("DB1.DBX0.1:" + db1Bool2);

            int IntVariable = (ushort)plc.Read("DB1.DBW2.0");
                    Console.WriteLine("DB1.DBW2.0:" + IntVariable);

            float RealVariable = ((uint)plc.Read("DB1.DBD4.0")).ConvertToFloat();
                    Console.WriteLine("DB1.DBD4.0:" + RealVariable);

            var dIntVariable = (uint)plc.Read("DB1.DBD8.0");
                    Console.WriteLine("DB1.DBD8.0: " + dIntVariable);

            var dWordVariable = (uint)plc.Read("DB1.DBD12.0");
                    Console.WriteLine("DB1.DBD12.0: " + Convert.ToString(dWordVariable, 16));

            var wordVariable = (ushort)plc.Read("DB1.DBW16.0");
                    Console.WriteLine("DB1.DBW16.0: " + Convert.ToString(wordVariable,16));

         */

        /*
        方法：public byte[] ReadBytes(DataType dataType, int db, int startByteAdr, int count)
        入参：
	        1、DataType数据类型，可选择从DB块或者Memory中读取；
	        2、db：1：DataBlock=1,Memory=0；
	        3、startByteAdr：起始地址，即DB块的起始偏移量；
	        4、count:读取大小，该大小由读取的DB块的最后一个数据的偏移量和大小决定，这里最后一个字节WordVariable偏移量为16，数据类型为word，2个字节，因此此次读取为16+2=18个字节。
        出参：Byte[],这里Byte[]的大小必然和count的大小是相同的，

                //读取数据选择从DB块中读取，db设置为1，起始地址为0，读取18个字节
                var bytes = plc.ReadBytes(DataType.DataBlock, 1, 0, 18);
                //取字节0中的第0位
                var db1Bool1 = bytes[0].SelectBit(0);
                Console.WriteLine("DB1.DBX0.0:" + db1Bool1);
        //取字节0中的第1位
        bool db1Bool2 = bytes[0].SelectBit(1); ;
        Console.WriteLine("DB1.DBX0.1:" + db1Bool2);
        //跳到字节2并连续取两个字节数据
        int IntVariable = S7.Net.Types.Int.FromByteArray(bytes.Skip(2).Take(2).ToArray());
                Console.WriteLine("DB1.DBW2.0:" + IntVariable);
        //...
        double RealVariable = S7.Net.Types.Real.FromByteArray(bytes.Skip(4).Take(4).ToArray());
                Console.WriteLine("DB1.DBD4.0:" + RealVariable);
        //...
        int dIntVariable = S7.Net.Types.DInt.FromByteArray(bytes.Skip(8).Take(4).ToArray());
                Console.WriteLine("DB1.DBD8.0: " + dIntVariable);
        //...
        uint dWordVariable = S7.Net.Types.DWord.FromByteArray(bytes.Skip(12).Take(4).ToArray());
                Console.WriteLine("DB1.DBD12.0: " + Convert.ToString(dWordVariable, 16));
        //...
        ushort wordVariable = S7.Net.Types.Word.FromByteArray(bytes.Skip(16).Take(2).ToArray());
                Console.WriteLine("DB1.DBW16.0: " + Convert.ToString(wordVariable, 16));

        */

        /*写入单个
        方法：public void Write(string variable, object value)
        入参：
	        1、string variable：写入地址
	        2、object value，写入数据

                plc.Write("DB1.DBX0.0", true);
        plc.Write("DB1.DBD12.0", 123457);
        */

        /*
         读写字符串–String与S7String
        有人问到用S7.net读写字符串，大概试验了一下，两种方法基本一致

        读
        读字符串分两步操作：
        1、获取字符串的长度；
        2、从指定地址开始，读取字符串长度；

        //String读取
        var count = (byte) plc.Read(dataType, dbNumber, address, VarType.Byte, 1);//获取字符串长度
        val = (string) plc.Read(dataType, dbNumber, address + 1, VarType.String, count);//获取对应长度的字符串
        1
        2
        3
        //S7String读取
        var reservedLength = (byte) plc.Read(dataType, dbNumber, address, VarType.Byte, 1);//获取字符串长度
        val = (string) plc.Read(dataType, dbNumber, address, VarType.S7String, reservedLength);//获取对应长度的字符串
        1
        2
        3
        写
        写入与读取是反向操作，同样的也是要先写入要写入数据的长度，然后在写入数据

        //Write写入
        plc.Write(dataType, dbNumber, address, val.Length);    //写入长度
        plc.Write(dataType, dbNumber, address + 1, val);       //写入字符串
        1
        2
        3
        //使用S7String方法构建，需要先构建S7String对应的字节数组，然后将数组写入。
        var temp = Encoding.ASCII.GetBytes(val);   //将val字符串转换为字符数组
        var bytes = S7.Net.Types.S7String.ToByteArray(val, temp.Length);
        plc.WriteBytes(dataType, dbNumber, address, bytes);

         */
    }
}



