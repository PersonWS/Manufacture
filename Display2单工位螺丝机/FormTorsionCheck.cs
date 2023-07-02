using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormTorsionCheck : Form
    {
        public FormTorsionCheck()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //发 0xFA,读返回数据，高位在前
                if (!utility.modbusObjects[2].serialPort.IsOpen)
                    utility.modbusObjects[2].serialPort.Open();


                byte[] b = new byte[1];
                b[0] = 0xFA;
                utility.modbusObjects[2].serialPort.Write(b, 0, 1);
                Thread.Sleep(100);
                int length = utility.modbusObjects[2].serialPort.BytesToRead;
                byte[] data = new byte[4];
                if (length >= 2)
                    utility.modbusObjects[2].serialPort.Read(data, 0, 2);
                //utility.modbusObjects[2].serialPort.Close();
                string sss = "扭力数据，" + string.Join("-", data);
                double nl = Convert.ToDouble(BitConverter.ToInt32(data, 0)) / 1000;
                label1.Text = nl.ToString();
                labelMsg.Text = "";
                //  Fillinfo("扭力数据，" + nl.ToString()); ;
            }
            catch (Exception ex)
            {
                labelMsg.Text = ex.Message;
            }
        }

        private void FormTorsionCheck_Load(object sender, EventArgs e)
        {
            //读取电批第15任务
            Model.ResultJsonInfo jr = SerialCommunication.ReadConmmand(RegisterAddress.AddrTorsionBase, 1);
            if(!jr.Successed)
            {
                utility.ShowMessage("读取扭力基准错误");
                return;
            }
            double TorsionBase=Convert.ToDouble(jr.readBuffer[0]);
            labelTorsionBase.Text = (TorsionBase / 100).ToString();
            utility.modbusObjects[2].serialPort.Close();
            serialPort1.PortName = utility.modbusObjects[2].serialPort.PortName;
            serialPort1.BaudRate = utility.modbusObjects[2].serialPort.BaudRate;
            serialPort1.DataBits = utility.modbusObjects[2].serialPort.DataBits;
            serialPort1.StopBits = utility.modbusObjects[2].serialPort.StopBits;
            serialPort1.Parity = utility.modbusObjects[2].serialPort.Parity;
            serialPort1.Open();

            byte[] b = new byte[1];
            b[0] = 0xFA;
            serialPort1.Write(b, 0, 1);
        }
        private void FormTorsionCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                timer1.Enabled = false;
                Close();
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
               
                int length = serialPort1.BytesToRead;
                byte[] data = new byte[4];
                byte[] sdata = new byte[2];
                if (length >= 2)
                    serialPort1.Read(sdata, 0, 2);
                //utility.modbusObjects[2].serialPort.Close();
                data[0]= sdata[1];
                data[1]= sdata[0];
                string sss = "扭力数据，" + string.Join("-", data);
                double nl = Convert.ToDouble(BitConverter.ToInt32(data, 0)) / 100;
                label1.Text = nl.ToString();
                labelMsg.Text = "";
                //  Fillinfo("扭力数据，" + nl.ToString()); ;
            }
            catch (Exception ex)
            {
                labelMsg.Text = ex.Message;
            }
        }
    }
}
