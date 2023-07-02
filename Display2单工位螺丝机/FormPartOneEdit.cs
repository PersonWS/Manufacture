using PDAMaster;
using ScrewMachineManagementSystem.Model;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormPartOneEdit : Form
    {
        public FormPartOneEdit(int cindex)
        {
            InitializeComponent();
            _cindex = cindex;
        }
        int _cindex = -1;
        private void FormPartOneEdit_Load(object sender, EventArgs e)
        {
            for (int i = ConfigurationKeys.TaskIDStartValue; i <= ConfigurationKeys.teachPendantTaskID_Max; i++)
            {
                string s = "Part_" + i.ToString().PadLeft(2, '0');

                comboBox1.Items.Add(s);
            }
            comboBox1.SelectedIndex = _cindex;
            int tpid = comboBox1.SelectedIndex;
            if (tpid > -1)
            {
                tpid = tpid + ConfigurationKeys.TaskIDStartValue;
                readPlc(tpid);


            }
            else
            {
                utility.ShowMessage("请选择示教器任务号（配方号）");
                return;
            }

        }

        /// <summary>
        /// 根据任务号，读取PLC配方
        /// </summary>
        void readPlc(int tpid)
        {
            /*
             读取加工文件信息	类型
               步骤	读取加工文件信息	类型
                1	点击读取按钮          DB8.DBX260.0   (bool)  	bool
                2	读取加工程序编号        DB15.DBX12.0  （1-10个编号）	int
                3	读取加工吹钉螺丝个数  DB14.DBX14.0  (int)（保留两工位不用）	int
                4	读取加工程序名称        DB14.DBX0.0  （10个字符）	string
                5	读取加工吸钉螺丝个数  DB14.DBX16.0  (int)	int
                6	读取加工SN码位数       DB14.DBX18.0  	int
                7	读取加工产品识别码   DB14.DBX20.0 	string
            8	相机拍照次数   DB14.DBX28.0 	string


            */
            Model.ResultJsonInfo jr = new ResultJsonInfo();
            //1
            // jr = S7NetPlus.WriteDataBool(8, 260, true, 0);

            //2 读取加工程序编号        DB15.DBX12.0  （1-10个编号）	int

            //jr = S7NetPlus.ReadOneInt(15, 12);
            //if (!jr.Successed)
            //{
            //    utility.ShowMessage("配方编号读取失败，请重试");
            //    return;
            //}

            //if (tpid != jr.intValue)
            //{
            //    utility.ShowMessage("配方编号没有写入，请重新操作");
            //    return;
            //}
            //3读取加工吹钉螺丝个数  DB14.DBX14.0  (int)（保留两工位不用）	int，NumberOfScrews
            //jr = S7NetPlus.ReadOneInt(14, 14, 0);
            //if (jr.Successed)
            //{
            //    plc_umberOfScrews = jr.intValue;
            //    textBoxNumberOfScrews.Text = jr.intValue.ToString();
            //}
            //else
            //{
            //    utility.ShowMessage("读加工螺丝个数 DB14.DBX258.0失败," + jr.Message);
            //}

            //4读加工程序名称/机型 14 0.0
            jr = S7NetPlus.ReadOneString(14, 0, 0);
            if (jr.Successed)
            {

                textBoxMachineModel.Text = jr.stringValue;
            }
            else
            {
                utility.ShowMessage("读加工程序名称 DB14.DBX0.0 ," + jr.Message);
            }

            //读吸钉螺丝个数 2/3工位用，NumberOfScrews
            jr = S7NetPlus.ReadOneInt(14, 16, 0);
            if (jr.Successed)
            {

                numericUpDownNumberOfScrews.Value = jr.intValue;
            }
            else
            {
                utility.ShowMessage("读吸钉螺丝个数 DB14.DBX16.0失败," + jr.Message);
            }
            //6   读取加工SN码位数 DB14.DBX18.0    int
            jr = S7NetPlus.ReadOneInt(14, 18, 0);
            numericUpDownSNLenght.Value = jr.intValue;
            //7   读取加工产品识别码 DB14.DBX20.0    string
            jr = S7NetPlus.ReadOneString(14, 20, 0);
            textBoxProductID.Text = jr.stringValue;
            //8拍照次数
            jr = S7NetPlus.ReadOneInt(14, 28, 0);
            numericUpDownNumberofphotos.Value = jr.intValue;
            //恢复260.0为false
            Thread.Sleep(100);
            jr = S7NetPlus.WriteDataBool(8, 260, false, 0);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tpid = comboBox1.SelectedIndex;
            if (tpid > -1)
            {
                tpid = tpid + ConfigurationKeys.TaskIDStartValue;
                //     readPlc(tpid + ConfigurationKeys.TaskIDStartValue);


            }
            else
            {
                utility.ShowMessage("请选择示教器任务号（配方号）");
                return;
            }



            //更新PLC
            /*写入加工文件信息
            步骤	写入加工文件信息	类型
            1	写入加工程序编号 DB15.DBX12.0  （1-10个编号）	int
            2	写入加工吹钉螺丝个数 DB14.DBX14.0  (int)（保留两工位不用）	int
            3	写入加工程序名称 DB14.DBX0.0  （10个字符）	string
            4	写入加工吸钉螺丝个数 DB14.DBX16.0  (int)	int
            5	写入加工SN码位数 DB14.DBX18.0  	int
            6	写入加工产品识别码 DB14.DBX20.0 	string
            7	点击写入按钮         DB8.DBX260.7    (bool)  	bool


            */
            Model.ResultJsonInfo jr = S7NetPlus.WriteDataInt(15, 12, (short)tpid);
            if (jr.Successed)
            {
                //2 写入加工吹钉螺丝个数 DB14.DBX14.0  (int)（保留两工位不用）	int
                //jr = S7NetPlus.WriteDataInt(14, 14, (short)productInfo.NumberOfScrews);
                //3 写入加工程序名称 DB14.DBX0.0  （10个字符）	string
                jr = S7NetPlus.WriteDataString(14, 0, textBoxMachineModel.Text);
                if (jr.Successed)
                {
                    //4   写入加工吸钉螺丝个数 DB14.DBX16.0(int)  int
                    jr = S7NetPlus.WriteDataInt(14, 16, (short)numericUpDownNumberOfScrews.Value);
                    //5	写入加工SN码位数 DB14.DBX18.0  	int
                    jr = S7NetPlus.WriteDataInt(14, 18, (short)numericUpDownSNLenght.Value);
                    //6   写入加工产品识别码 DB14.DBX20.0  string
                    jr = S7NetPlus.WriteDataString(14, 20, textBoxProductID.Text);
                    jr = S7NetPlus.WriteDataInt(14, 20, (short)numericUpDownNumberofphotos.Value);
                    jr = S7NetPlus.WriteDataBool(8, 260, true, 7);
                    Thread.Sleep(200);
                    jr = S7NetPlus.WriteDataBool(8, 260, false, 7);
                }
                else
                {
                    utility.ShowMessage("写入加工程序名称错误," + jr.Message);
                    return;
                }

            }
            else
            {
                utility.ShowMessage("写入加工文件程序编号错误," + jr.Message);
                return;
            }
            this.DialogResult = DialogResult.OK;
            MessageBox.Show("产品信息保存完成！");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
