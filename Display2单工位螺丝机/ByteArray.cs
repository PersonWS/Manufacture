using System;
using System.Windows;
using System.Collections.Generic;
/// <summary>
/// 字节集合类
/// </summary>
public class ByteArray
{
    /// <summary>
    /// 初始化一个List集合对象
    /// </summary>
    private List<byte> list = new List<byte>();

    /// <summary>
    /// 通过索引获取值
    /// </summary>
    /// <param name="index">索引</param>
    /// <returns>返回字节</returns>
    public byte this[int index]
    {
        get => list[index];
        set => list[index] = value;
    }

    /// <summary>
    /// 返回长度
    /// </summary>
    public int Length => list.Count;


    /// <summary>
    /// 通过属性返回字节数组
    /// </summary>
    public byte[] array
    {
        get { return list.ToArray(); }
    }

    /// <summary>
    /// 清空字节数组
    /// </summary>
    public void Clear()
    {
        list.Clear();
    }

    /// <summary>
    /// 添加一个字节
    /// </summary>
    /// <param name="item">字节</param>
    public void Add(byte item)
    {
        list.Add(item);
    }

    /// <summary>
    /// 添加一个字节数组
    /// </summary>
    /// <param name="items">字节数组</param>
    public void Add(byte[] items)
    {
        list.AddRange(items);
    }

    /// <summary>
    /// 添加二个字节
    /// </summary>
    /// <param name="item">字节</param>
    public void Add(byte item1, byte item2)
    {
        Add(new byte[] { item1, item2 });
    }

    /// <summary>
    /// 添加三个字节
    /// </summary>
    /// <param name="item">字节</param>
    public void Add(byte item1, byte item2, byte item3)
    {
        Add(new byte[] { item1, item2, item3 });
    }

    /// <summary>
    /// 添加四个字节
    /// </summary>
    /// <param name="item">字节</param>
    public void Add(byte item1, byte item2, byte item3, byte item4)
    {
        Add(new byte[] { item1, item2, item3, item4 });
    }

    /// <summary>
    /// 添加五个字节
    /// </summary>
    /// <param name="item">字节</param>
    public void Add(byte item1, byte item2, byte item3, byte item4, byte item5)
    {
        Add(new byte[] { item1, item2, item3, item4, item5 });
    }


    /// <summary>
    /// 添加一个ByteArray对象
    /// </summary>
    /// <param name="byteArray">ByteArray对象</param>
    public void Add(ByteArray byteArray)
    {
        list.AddRange(byteArray.array);
    }

    public void Insert(int index, byte item)
    {
        list.Insert(index, item);
    }


    public void Insert(int index, byte[] item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            list.Insert(index + i, item[i]);
        }
    }

    /// <summary>
    /// 添加一个ushort类型数值
    /// </summary>
    /// <param name="value">ushort类型数值</param>
    /// <param name="dataFormat">字节序</param>
    public void Add(ushort value, DataFormat dataFormat)
    {
        byte[] data = BitConverter.GetBytes(value);

        byte[] result = new byte[data.Length];

        switch (dataFormat)
        {
            case DataFormat.ABCD:
            case DataFormat.CDAB:
                result[0] = data[1];
                result[1] = data[0];
                break;
            case DataFormat.BADC:
            case DataFormat.DCBA:
                result[0] = data[0];
                result[1] = data[1];
                break;
            default:
                break;
        }
        Add(result);
    }
    /// <summary>
    /// 16进制格式反转
    /// </summary>
    public enum DataFormat
    {
        /// <summary>
        /// 低位在前，高位在后，不用反转
        /// </summary>
        ABCD,
        /// <summary>
        /// 高位在前，低位在后，需要反转
        /// </summary>
        CDAB,
            BADC,
            DCBA
    }
    /// <summary>
    /// 添加一个short类型数值
    /// </summary>
    /// <param name="value">short类型数值</param>
    /// <param name="dataFormat">字节序</param>
    public void Add(short value, DataFormat dataFormat)
    {
        byte[] data = BitConverter.GetBytes(value);

        byte[] result = new byte[data.Length];

        switch (dataFormat)
        {
            case DataFormat.ABCD:
            case DataFormat.CDAB:
                result[0] = data[1];
                result[1] = data[0];
                break;
            case DataFormat.BADC:
            case DataFormat.DCBA:
                result[0] = data[0];
                result[1] = data[1];
                break;
            default:
                break;
        }
        Add(result);
    }

    /// <summary>
    /// 添加一个ushort类型数值
    /// </summary>
    /// <param name="value">ushort类型数值</param>
    /// <param name="dataFormat">字节序</param>
    public void Insert(int index, ushort value, DataFormat dataFormat)
    {
        byte[] data = BitConverter.GetBytes(value);

        byte[] result = new byte[data.Length];

        switch (dataFormat)
        {
            case DataFormat.ABCD:
            case DataFormat.CDAB:
                result[0] = data[1];
                result[1] = data[0];
                break;
            case DataFormat.BADC:
            case DataFormat.DCBA:
                result[0] = data[0];
                result[1] = data[1];
                break;
            default:
                break;
        }
        Insert(index, result);
    }

    /// <summary>
    /// 添加一个short类型数值
    /// </summary>
    /// <param name="value">short类型数值</param>
    /// <param name="dataFormat">字节序</param>
    public void Insert(int index, short value, DataFormat dataFormat)
    {
        byte[] data = BitConverter.GetBytes(value);

        byte[] result = new byte[data.Length];

        switch (dataFormat)
        {
            case DataFormat.ABCD:
            case DataFormat.CDAB:
                result[0] = data[1];
                result[1] = data[0];
                break;
            case DataFormat.BADC:
            case DataFormat.DCBA:
                result[0] = data[0];
                result[1] = data[1];
                break;
            default:
                break;
        }
        Insert(index, result);
    }
    /*使用场景示例
      /// <summary>
        /// 根据起始地址及长度，确定读取协议帧
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="length">长度</param>
        /// <returns>协议帧数据</returns>
        public  virtual OperateResult<byte[]> BuildReadMessageFrame(string address, ushort length)
        {
            var result = MelsecHelper.MelsecAddressAnalysis(address, IsFx5U);
            if (!result.IsSuccess) return OperateResult.CreateFailResult<byte[]>(result);

            //创建ByteArray对象
            ByteArray sendCommand = new ByteArray();
            // 副头部
            sendCommand.Add(0x50, 0x00);
            // 网络编号
            sendCommand.Add(NetworkNo);
            // 可编程控制器编号
            sendCommand.Add(0xFF);
            // 请求目标模块I/O编号
            sendCommand.Add(0xFF, 0x03);
            // 请求目标模块站号
            sendCommand.Add(DstModuleNo);
            // 请求数据长度
            sendCommand.Add(0x0C, 0x00);
            // CPU监视定时器
            sendCommand.Add(0x0A, 0x00);
            // 指令
            sendCommand.Add(0x01, 0x04);
            // 子指令
            sendCommand.Add(result.Content1.AreaType, 0x00);
            // 起始软元件
            byte[] startAddress = BitConverter.GetBytes(result.Content2);
            sendCommand.Add(startAddress[0], startAddress[1], startAddress[2]);
            // 软元件代码
            sendCommand.Add(result.Content1.AreaBinaryCode);
            // 软元件点数
            sendCommand.Add((byte)(length % 256), (byte)(length / 256));

            return OperateResult.CreateSuccessResult(sendCommand.array);
        }
     */
}