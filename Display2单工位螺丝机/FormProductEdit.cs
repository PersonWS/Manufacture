using PDAMaster;
using ScrewMachineManagementSystem.Controller;
using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormProductEdit : Form
    {
        public FormProductEdit(string id)
        {
            InitializeComponent();
            _id = id;
        }
        string _id = string.Empty;
        int plc_umberOfScrews = 0;
        string plc_MachineModel = "";
        string[] parts = new string[10] { null, null, null, null, null, null, null, null, null, null };

        /// <summary>
        /// 刷新产品识别码
        /// </summary>
        void RefreshTPID()
        {

            comboBox1.Items.Clear();
            parts = new string[10];
            Model.ResultJsonInfo jr = S7NetPlus.ReadOneString(51, 778);
            parts[0] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 786);
            parts[1] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 794);
            parts[2] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 802);
            parts[3] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 810);
            parts[4] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 818);
            parts[5] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 826);
            parts[6] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 834);
            parts[7] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 842);
            parts[8] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 850);
            parts[9] = jr.stringValue;
            for (int i = ConfigurationKeys.TaskIDStartValue; i <= ConfigurationKeys.teachPendantTaskID_Max; i++)
            {
                string s = "Part_" + i.ToString().PadLeft(2, '0') + " : ";
                string s1 = parts[i - 1] == null ? "未定义" : parts[i - 1];
                comboBox1.Items.Add(s + s1);
            }
        }
        private void FormProductEdit_Load(object sender, EventArgs e)
        {
            RefreshTPID();
            if (string.IsNullOrEmpty(_id))
            {
                groupBox1.Text = "新增";
                comboBoxDefWorkStation.SelectedIndex = 1;
            }
            else
            {
                groupBox1.Text = "编辑";
                List<ProductInfo> l = CONT_ProductInfo.LoadList(" where Id='" + _id + "'");
                if (l.Count > 0)
                {
                    textBoxProductID.Text = l[0].ProductCode;

                    checkBox1.Checked = l[0].checkPrefixCode;
                    textBoxMachineModel.Text = l[0].MachineModel;
                    textBoxCustomer.Text = l[0].Customer;
                    textBoxProjectPhase.Text = l[0].ProjectPhase;
                    comboBox1.SelectedIndex = l[0].TPTaskID - ConfigurationKeys.TaskIDStartValue;
                    //textBoxNumberOfScrews.Text = l[0].NumberOfScrews.ToString();
                    //int defWorkStation = l[0].DefaultWorkStation;
                    //comboBoxDefWorkStation.SelectedIndex = defWorkStation;
                    //numericUpDownSNLenght.Value = l[0].SNCodeLenght;
                    textBoxPrefixCode.Text = l[0].PrefixCode;
                }
            }
            numericUpDown1.Value = ConfigurationKeys.teachPendantTaskID_Max;
            if (ConfigurationKeys.TaskIDStartValue == 0)
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    utility.ShowMessage("请选择示教器任务号");
                    return;
                }
                ProductInfo productInfo = new ProductInfo();
                productInfo.checkPrefixCode = checkBox1.Checked;
                productInfo.ProductCode = textBoxProductID.Text;
                productInfo.MachineModel = textBoxMachineModel.Text;
                productInfo.Customer = textBoxCustomer.Text;
                productInfo.ProjectPhase = textBoxProjectPhase.Text;
                productInfo.MKD = DateTime.Now;
                productInfo.TPTaskID = comboBox1.SelectedIndex + ConfigurationKeys.TaskIDStartValue;
                int NumberOfScrews = (short)numericUpDownNumberOfScrews.Value;
                int defaultStationID = comboBoxDefWorkStation.SelectedIndex;
                productInfo.SNCodeLenght = (int)numericUpDownSNLenght.Value;
                productInfo.PrefixCode = textBoxPrefixCode.Text;
                if (productInfo.PrefixCode == "" || productInfo.PrefixCode == null)
                {
                    utility.ShowMessage("请输入正确的产品码前缀！");
                }



                productInfo.DefaultWorkStation = defaultStationID;
                productInfo.NumberOfScrews = NumberOfScrews;

                if (plc_umberOfScrews != productInfo.NumberOfScrews || plc_MachineModel != productInfo.MachineModel)
                {
                    if (utility.ShowMessageResponse("当前任务号的配方信息与编辑的信息不符，是否继续保存？" + Environment.NewLine + "当前工序名称：" + plc_MachineModel + ",螺丝数量：" + plc_umberOfScrews) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                if (string.IsNullOrEmpty(_id))
                {
                    List<ProductInfo> l = CONT_ProductInfo.LoadList(" where Id='" + _id + "'");
                    if (l.Count > 0)
                    {
                        MessageBox.Show("此产品码已存在，不需要增加！");
                        return;
                    }
                    //new
                    if (CONT_ProductInfo.Save(productInfo) < 0)
                    {
                        utility.ShowMessage("保存新产品信息出错");
                        return;
                    }

                }
                else
                {
                    productInfo.Id = Convert.ToInt32(_id);
                    CONT_ProductInfo.Update(productInfo);
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
                Model.ResultJsonInfo jr = S7NetPlus.WriteDataInt(15, 12, (short)productInfo.TPTaskID);
                if (jr.Successed)
                {
                    //2 写入加工吹钉螺丝个数 DB14.DBX14.0  (int)（保留两工位不用）	int
                    //jr = S7NetPlus.WriteDataInt(14, 14, (short)productInfo.NumberOfScrews);
                    //3 写入加工程序名称 DB14.DBX0.0  （10个字符）	string
                    jr = S7NetPlus.WriteDataString(14, 0, productInfo.MachineModel);
                    if (jr.Successed)
                    {
                        //4   写入加工吸钉螺丝个数 DB14.DBX16.0(int)  int
                        jr = S7NetPlus.WriteDataInt(14, 16, (short)productInfo.NumberOfScrews);
                        //5	写入加工SN码位数 DB14.DBX18.0  	int
                        jr = S7NetPlus.WriteDataInt(14, 18, (short)productInfo.SNCodeLenght);
                        //6   写入加工产品识别码 DB14.DBX20.0  string
                        jr = S7NetPlus.WriteDataString(14, 20, productInfo.ProductCode);
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
            catch (Exception ex)
            {
                MessageBox.Show("保存产品信息出错！" + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            readPlc(comboBox1.SelectedIndex + ConfigurationKeys.TaskIDStartValue);
        }
        /// <summary>
        /// 读取PLC数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int tpid = comboBox1.SelectedIndex;
            if (tpid > -1)
            {
                tpid = tpid + ConfigurationKeys.TaskIDStartValue;
                readPlc(tpid + ConfigurationKeys.TaskIDStartValue);
                //读取数据库
                List<ProductInfo> l = CONT_ProductInfo.LoadList(" where tptaskid='" + tpid + "'");
                if (l.Count > 0)
                {
                    textBoxProductID.Text = l[0].ProductCode;
                    textBoxPrefixCode.Text = l[0].PrefixCode;
                    textBoxCustomer.Text = l[0].Customer;
                    textBoxProjectPhase.Text = l[0].ProjectPhase;
                    checkBox1.Checked = l[0].checkPrefixCode;

                    //textBoxMachineModel.Text = l[0].MachineModel;

                    //textBoxNumberOfScrews.Text = l[0].NumberOfScrews.ToString();
                    //int defWorkStation = l[0].DefaultWorkStation;
                    //comboBoxDefWorkStation.SelectedIndex = defWorkStation;
                    //numericUpDownSNLenght.Value = l[0].SNCodeLenght;

                }

            }
            else
            {
                utility.ShowMessage("请选择示教器任务号（配方号）");
                return;
            }

            //新增不读数据库,修改读数据库已保存的产品信息
            if (!string.IsNullOrEmpty(_id))
            {




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


            */
            Model.ResultJsonInfo jr = new ResultJsonInfo();
            //1
            jr = S7NetPlus.WriteDataBool(8, 260, true, 0);

            //2 读取加工程序编号        DB15.DBX12.0  （1-10个编号）	int
            jr = S7NetPlus.WriteDataInt(15, 12, (short)tpid);
            if (jr.Successed)
            {
                jr = S7NetPlus.ReadOneDInt(15, 12);
                if(!jr.Successed)
                {
                    utility.ShowMessage("配方编号读取失败，请重试");
                    return;
                }
                if (tpid != jr.intValue)
                {
                    utility.ShowMessage("配方编号没有写入，请重新操作");
                    return;
                }
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
                    plc_MachineModel = jr.stringValue;
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
                    plc_umberOfScrews = jr.intValue;
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
                //恢复260.0为false
                Thread.Sleep(100);
                jr = S7NetPlus.WriteDataBool(8, 260, false, 0);

            }
            else
            {
                jr = S7NetPlus.WriteDataBool(8, 260, false, 0);
                utility.ShowMessage("PLC写入加工文件程序编号错误," + jr.Message);
                return;
            }

        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = checkBox2.Checked;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (utility.ShowMessageResponse("确定要更新配方参数？将影响已经存储的配方数据！") != DialogResult.Yes)
            {
                return;
            }

            
            if (!File.Exists(utility._xmlSystemParamFilePath ))
            {
                //logger.Fatal("参数文件xmlparam.xml丢失！");
                utility.ShowMessage("参数文件systemParam.xml丢失！");
                Application.Exit();
            }
            string filename = utility._xmlSystemParamFilePath;
            int TaskCount = (int)numericUpDown1.Value;
            int StartValue = 0;
            if (radioButton2.Checked)
                StartValue = 1;
            bool b = PDAMaster.Configuration.ModifyXmlInformation(filename, "WorkStationCount", "DefaultValue", TaskCount.ToString());
            bool c = PDAMaster.Configuration.ModifyXmlInformation(filename, "TaskIDStartValue", "DefaultValue", StartValue.ToString());
            if (!b || !c)
            {
                utility.ShowMessage("更新参数失败");
            }

          
        }

        private void buttonRefreshTPID_Click(object sender, EventArgs e)
        {
            RefreshTPID();
        }
    }
}
