using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    /// <summary>
    /// 调试执行
    /// </summary>
    public partial class FormScrewMachineActive : Form
    {
        public FormScrewMachineActive()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 动作模式button数组，0拧紧，1拧松，2自由
        /// </summary>
        Button[] btMode = new Button[3];
        Label[] lbBusy = new Label[3];
        Label[] lbMode = new Label[3];
        void initButton4()
        {
            btMode[0] = buttonTighten;
            btMode[1] = buttonUnscrew;
            btMode[2] = buttonFree;
            lbMode[2] = labelFree;
            lbMode[1] = labelun;
            lbMode[0] = labelTr;

            lbBusy[0] = labelErr;
            lbBusy[1] = labelOK;
            lbBusy[2] = labelBUSY;

            for (int i = 0; i < 3; i++)
            {
                btMode[i].Tag = i;
                btMode[i].MouseDown += btMode_MouseDown;
                btMode[i].MouseUp += btMode_MouseUp; ;
            }

        }

        private void btMode_MouseUp(object sender, MouseEventArgs e)
        {
            Button c = (Button)sender;
            int index = Convert.ToInt32(c.Tag);
            try
            {


                //switch (index)
                //{
                //    case 0:
                //        SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06,61500, 0x2BBB);  //停止紧
                //        break;
                //    case 1:
                //        SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06, 61500, 0x2DDD);  //停止松
                //        break;
                //    case 2:
                //        SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06, 61500, 0x2FFF);  //停止自由
                //        break;
                //}
            }
            catch (Exception ex)
            {

                if (ex.Source.Equals("System"))
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + ex.Message);
                    toolStripStatusLabel1.Text = ex.Message;

                }
            }
        }

        private void btMode_MouseDown(object sender, EventArgs e)
        {
            Button c = (Button)sender;
            int index = Convert.ToInt32(c.Tag);
            Model.ResultJsonInfo jRRInfo;
            //try
            //{
            //    switch (index)
            //    {
            //        case 0:
            //            jRRInfo = SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06,61500, 0x2AAA);  //拧紧
            //            break;
            //        case 1:
            //            jRRInfo = SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06,61500, 0x2CCC);  //拧松
            //            break;
            //        case 2:
            //            jRRInfo = SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06,61500, 0x2EEE);  //自由
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    switch (index)
            //    {
            //        case 0:
            //            jRRInfo = SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06, 61500, 0x2BBB);  //停止紧
            //            break;
            //        case 1:
            //            jRRInfo = SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06, 61500, 0x2DDD);  //停止松
            //            break;
            //        case 2:
            //            jRRInfo = SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06,61500, 0x2FFF);  //停止自由
            //            break;
            //    }
            //    if (ex.Source.Equals("System"))
            //    {


            //    }
            //    MessageBox.Show(ex.Message, "系统提示");
            //    toolStripStatusLabel1.Text = ex.Message;
            //}


        }


        private void FormScrewMachineActive_Load(object sender, EventArgs e)
        {
            List<Model.TPTaskInfo> l = Controller.CONT_TPTaskInfo.LoadList("");
            foreach (var v in l)
            {
                string tpid = v.TaskId.ToString("0#");
                string tpname = v.TaskName;
                comboBox1.Items.Add(tpid + ":" + tpname);
            }
            initButton4();
            try
            {
                SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06, 61506, 0x3BBB);  //设置PC模式
            }
            catch (Exception ex)
            {

                if (ex.Source.Equals("System"))
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + ex.Message);
                    toolStripStatusLabel1.Text = ex.Message;

                }
            }
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            timer1.Enabled = true;
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ushort selectedTaskid = (ushort)comboBox1.SelectedIndex;
            var jRRInfo = SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06, 3901, selectedTaskid);  //设置任务
            toolStripStatusLabel1.Text = jRRInfo.Message;
            if (!jRRInfo.Successed)
            {
                MessageBox.Show(jRRInfo.Message, "系統提示");
            }
        }

        void setIOout(ushort buffer)
        {
            for (int i = 0; i < 3; i++)
            {
                lbBusy[i].ForeColor = SystemColors.ControlDark;
            }
            switch (buffer)
            {
                case 0:
                    break;
                case 1:
                    lbBusy[2].ForeColor = Color.Lime;
                    break;
                case 2:
                    lbBusy[1].ForeColor = Color.Lime;
                    break;
                default:
                    lbBusy[0].ForeColor = Color.Lime;
                    break;
            }
        }

        void setIOin(ushort buffer)
        {
            for (int i = 0; i < 3; i++)
            {
                lbMode[i].ForeColor = SystemColors.ControlDark;
            }
            switch (buffer)
            {
                case 0:
                    break;
                case 1:
                    lbMode[2].ForeColor = Color.Lime;
                    break;
                case 2:
                    lbMode[1].ForeColor = Color.Lime;
                    break;
                default:
                    lbMode[0].ForeColor = Color.Lime;
                    break;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //setIOout(SerialCommunication._holdingregister[17]);
                //setIOin(SerialCommunication._holdingregister[15]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "系统提示");
                toolStripStatusLabel1.Text = ex.Message;
            }
        }

        private void FormScrewMachineActive_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06, 61506, 0x3AAA);  //设置PC模式
            }
            catch (Exception ex)
            {
                    toolStripStatusLabel1.Text ="更新电批控制模式失败！"+ ex.Message;
            }
        }

       
    }
}
