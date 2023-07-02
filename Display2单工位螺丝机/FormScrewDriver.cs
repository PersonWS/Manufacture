using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormScrewDriver : Form
    {
        public FormScrewDriver()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panel1, 0, 0, 0, 1);
        }

        private void buttonCommSetup_Click(object sender, EventArgs e)
        {
            SerialCommunication.setSerial();
        }

        private void buttonTask_Click(object sender, EventArgs e)
        {
            if (!utility.modbusObjects[0].serialPort.IsOpen)
                utility.modbusObjects[0].serialPort.Open();
            FormScrewMachineTask f = new FormScrewMachineTask();
            utility.showForm(panel2, f);
        }

        /// <summary>
        /// 显示标题
        /// </summary>
        Label[] labelTitle = new Label[8];
        /// <summary>
        /// 电批8参数数值
        /// </summary>
        Label[] labelScrewDriver = new Label[8];

        void initlabelTitle()
        {
            labelTitle[0] = label1;
            labelTitle[1] = label2;
            labelTitle[2] = label3;
            labelTitle[3] = label4;
            labelTitle[4] = label5;
            labelTitle[5] = label6;
            labelTitle[6] = label7;
            labelTitle[7] = label8;


            labelScrewDriver[0] = labelComm;            //通讯
            labelScrewDriver[1] = labelTaskID;          //任务id
            labelScrewDriver[2] = labelCylinderNumber;  //圈数
            labelScrewDriver[3] = labelMaxTorsion;      //最大扭力
            labelScrewDriver[4] = labelTimes;           //耗时
            labelScrewDriver[5] = labelTighteningResults;//拧紧结果
            labelScrewDriver[6] = labelProcess;         //进程
            labelScrewDriver[7] = labelAlert;           //警报
        }

        

        private void FormScrewDriver_Load(object sender, EventArgs e)
        {
            initlabelTitle();
            int w = panelBottom.Width / 8;

            for (int i = 0; i < 8; i++)
            {
                labelTitle[i].Width = labelScrewDriver[i].Width = w;
                labelTitle[i].Left = labelScrewDriver[i].Left = i * w + 1;
            }
            timer1.Enabled = true;
        }

        private void buttonTestRun_Click(object sender, EventArgs e)
        {
            if (!utility.modbusObjects[0].serialPort.IsOpen)
                utility.modbusObjects[0].serialPort.Open();
            FormScrewMachineActive f = new FormScrewMachineActive();
            f.Show();
        }

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panelForm, 0, 1, 0, 0);
        }

        private void labelComm_TextChanged(object sender, EventArgs e)
        {
            if (labelComm.Text == "断开")
                labelComm.ForeColor = Color.Red;
            else
                labelComm.ForeColor = Color.Lime;
        }

        private void labelAlert_TextChanged(object sender, EventArgs e)
        {
            if (labelAlert.Text == "无")
                labelAlert.ForeColor = Color.Lime;
            else
                labelAlert.ForeColor = Color.Red;
        }
        private void labelTighteningResults_TextChanged(object sender, EventArgs e)
        {
            if (labelTighteningResults.Text == "OK")
                labelTighteningResults.ForeColor = Color.Lime;
            else
                labelTighteningResults.ForeColor = Color.Red;
        }

        private void FormScrewDriver_Resize(object sender, EventArgs e)
        {
            int w = panelBottom.Width / 8;
            for (int i = 0; i < 8; i++)
            {
                labelTitle[i].Width = labelScrewDriver[i].Width = w;
                labelTitle[i].Left = labelScrewDriver[i].Left = i * w + 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Year == 2022 && DateTime.Now.Month == 7 && DateTime.Now.Day == 26)
                utility.isTempCommand = true;
            if (utility.isTempCommand)
                return;
            try
            {
                // setSteplist();
                if (!utility.modbusObjects[0].serialPort.IsOpen)
                    utility.modbusObjects[0].serialPort.Open();

                //SerialCommunication._holdingregister = SerialCommunication.master[0].ReadHoldingRegisters((byte)utility.modbusObjects[0].slaveid, 60625, 32);
                //if(SerialCommunication._holdingregister[15]!=(ushort)0 || SerialCommunication._holdingregister[17] != (ushort)0)
                //{
                //    SerialCommunication._holdingregister[15] = SerialCommunication._holdingregister[15];
                //}
                //labelTaskID.Text = SerialCommunication._holdingregister[0].ToString().PadLeft(2, '0');
                //labelCylinderNumber.Text = (SerialCommunication._holdingregister[1] * 0.01).ToString();
                //labelMaxTorsion.Text = (SerialCommunication._holdingregister[3]*0.01).ToString();
                //labelTimes.Text = SerialCommunication._holdingregister[4].ToString();
                //ushort TighteningResults = SerialCommunication._holdingregister[5];
                string smsg = "";
                switch (TighteningResults)
                {
                    case 0x4AAA:
                        smsg = "NG";
                        break;
                    case 0x4BBB:
                        smsg = "OK";
                        break;
                    case 0x4CCC:
                        smsg = "未完成";
                        break;
                    default:
                        smsg = "未知";
                        break;
                }
                labelTighteningResults.Text = smsg;// registerBuffer[5].ToString();
                labelProcess.Text = SerialCommunication._holdingregister[11].ToString();
                /*
                 * //ALLErrorFlag,=1,浮高；
                 * =2，滑牙；
                 * =3，过流（断电重启）；
                 * =4，过压（检查供电电压是否偏高）；
                 * =5，欠压（检查供电电压是否偏低）；
                 * =6，飞车；
                 * =7，I2T过热（检查批头是否打滑，螺丝是否打滑）;
                 * =8，反转不到位；
                 * =9，位置偏差过大（检测电机线与encoder线接触是否良好）；

                */
                ushort ErrorFlag = SerialCommunication._holdingregister[13];

                switch (ErrorFlag)
                {
                    case 1:
                        smsg = "浮高";
                        break;
                    case 2:
                        smsg = "滑牙";
                        break;
                    case 3:
                        smsg = "过流(断电重启)";
                        break;
                    case 4:
                        smsg = "过压(电压是否偏高)";
                        break;
                    case 5:
                        smsg = "欠压(电压是否偏低)";
                        break;
                    case 6:
                        smsg = "飞车";
                        break;
                    case 7:
                        smsg = "I2T过热(是否打滑)";
                        break;
                    case 8:
                        smsg = "反转不到位";
                        break;
                    case 9:
                        smsg = "位置偏差过大";
                        break;
                    default:
                        smsg = "无";
                        break;
                }
                labelAlert.Text = smsg;
                labelComm.Text = "连接正常";


            }catch (Exception ex)
            {
                labelAlert.Text = ex.ToString();
            }


        }

        private void buttonRealChart_Click(object sender, EventArgs e)
        {
           
            FormRealChart f = new FormRealChart();
            utility.showForm(panel2, f);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormScrewDriver_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06, 61506, 0x3AAA);  //设置PC模式
            }
            catch (Exception ex)
            {
                utility.ShowMessage( "更新电批控制模式失败！" + ex.Message);
            }
        }
    }
}
