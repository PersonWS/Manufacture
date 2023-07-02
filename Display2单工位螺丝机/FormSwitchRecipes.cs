using ScrewMachineManagementSystem.Controller;
using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormSwitchRecipes : Form
    {
        public FormSwitchRecipes()
        {
            InitializeComponent();
        }

        private void labelX_Click(object sender, EventArgs e)
        {
            Close();
        }
        string keyInputs = "";
        private void FormSwitchRecipes_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Back:
                    if (keyInputs.Length > 0)
                    {
                        keyInputs = keyInputs.Substring(0, keyInputs.Length - 1);
                        labelSN.Text = keyInputs;
                        labelScanedLenght.Text = keyInputs.Length.ToString();
                    }
                    break;
                case (char)Keys.Escape:
                    utility.struckScanProduct.productSN = null;
                    utility.struckScanProduct.stationId = 0;
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;
                default:
                    if (!AllLineStatue)
                    {
                        toolStripStatusLabel1.Text = "全线配方下载条件不满足";

                        utility.ShowMessage("全线配方下载条件不满足");
                        keyInputs = "";
                        labelSN.Text = "";
                        return;
                    }

                    keyInputs = keyInputs + ((char)e.KeyChar).ToString().ToUpper();
                    labelSN.Text = keyInputs;
                    labelScanedLenght.Text = keyInputs.Length.ToString();






                    break;

            }
        }




        private void labelSN_TextChanged(object sender, EventArgs e)
        {
            if (labelSN.Text == "")
            {
                labelSN.Text = "请扫描产品SN";
            }

            if (labelSN.Text == "请扫描产品SN")
            {
                labelSN.ForeColor = Color.LightGray;
            }
            else
            {
                labelSN.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 配方号
        /// </summary>
        int tpid = 0;
        /// <summary>
        /// 全线配方下载条件，T是，F否
        /// </summary>
        bool AllLineStatue = false;
        private void FormSwitchRecipes_Load(object sender, EventArgs e)
        {
            //全线配方下载条件满足    DB211.DBX1.2
            Model.ResultJsonInfo jr = S7NetPlus.ReadOneBool(211, 1, 2);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("读取全线配方下载条件失败DB{0}.DBX{1}.{2}失败", 211, 0, 6));
                return;
            }
            else
            {
                if (jr.booValue != null)
                    AllLineStatue = jr.booValue[0];
                if (AllLineStatue)
                {
                    labelAllLineStatue.Text = "是";
                }
                else
                {
                    labelAllLineStatue.Text = "否";
                }
                //切换配方允许信号 DB51.DB938.0 , 打开面页时保持为True，关闭面页时为FALSE。
                jr = S7NetPlus.WriteDataBool(51, 938, true, 0);
                if (!jr.Successed)
                {
                    utility.ShowMessage(String.Format("切换配方允许信号DB{0}.DBX{1}.{2}失败", 51, 938, 0));
                    return;
                }



            }
        }



        bool buttonVisable()
        {

            //按钮操作条件   DB1.DB0.4 , 手自动保持为false /  DB211.DBX1.2 全线配方下载条件满足  TRUE 时 / DB15.DBX12.0  不等于222
            bool DB1_0_4 = false;
            bool DB211_1_2 = false;
            int DB15_12_0 = 222;
            Model.ResultJsonInfo jr = S7NetPlus.ReadOneBool(1, 0, 4);
            if (!jr.Successed)
            {
                toolStripStatusLabel1.Text = String.Format("读取手自动状态DB{0}.DBX{1}.{2}失败", 1, 0, 4);
                DB1_0_4 = true;
            }
            else
                DB1_0_4 = jr.booValue[0];

            jr = S7NetPlus.ReadOneBool(211, 1, 2);
            if (!jr.Successed)
            {
                toolStripStatusLabel1.Text = (String.Format("读取全线配方下载条件{0}.DBX{1}.{2}失败", 211, 1, 2));
                DB211_1_2 = false;
            }
            else
                DB211_1_2 = jr.booValue[0];

            jr = S7NetPlus.ReadOneInt(15, 12, 0);
            if (!jr.Successed)
            {
                DB15_12_0 = 222;
                toolStripStatusLabel1.Text = (String.Format("读取配方信息{0}.DBX{1}.{2}失败", 15, 12, 0));

            }
            else
                DB15_12_0 = jr.intValue;



            if (!DB1_0_4 && DB211_1_2 && DB15_12_0 != 222)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        private void FormSwitchRecipes_FormClosing(object sender, FormClosingEventArgs e)
        {
            //切换配方允许信号 DB51.DB938.0 , 打开面页时保持为True，关闭面页时为FALSE。
            Model.ResultJsonInfo jr = S7NetPlus.WriteDataBool(51, 938, false, 0);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("切换配方允许信号DB{0}.DBX{1}.{2}失败", 51, 938, 0));
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
        private void buttonRead_Click(object sender, EventArgs e)
        {
            //按钮显示条件 DB51.DB938.0 , 打开面页时保持为True / DB211.DBX1.2   TRUE 时 / DB15.DBX12.0  不等于222
            if (!buttonVisable())
            {
                utility.ShowMessage("不满足配方读取条件！");
                return;
            }
            string str = "配方读取完成";
            //点击读取按钮 DB8.DBX260.0(bool)
            Model.ResultJsonInfo jr = S7NetPlus.WriteDataBool(8, 260, true, 0);
            Thread.Sleep(500);
             jr = S7NetPlus.WriteDataBool(8, 260, false, 0);

            if (!jr.Successed)
            {
                str = String.Format("配方读取失败，DB{0}.DBX{1}.{2}失败", 8, 260, 0);
                utility.ShowMessage(str);
                return;
            }
            else
            {
                readPlc();
                //refReshTPID();
            }
            toolStripStatusLabel1.Text = str;
        }

        /// <summary>
        /// 刷新当前任务配方,读取扫码切换的配方，读取DB14
        /// </summary>
        void refReshTPID()
        {

            //产品识别码更改为读取DB51.DBX522.0
            Model.ResultJsonInfo jr =new  Model.ResultJsonInfo();
            //配方号为读取DB51.DBX256.0
            jr = S7NetPlus.ReadOneInt(51, 256);
            int tpid = jr.intValue;
            if (textBoxTPID.Text != tpid.ToString())
            {
                utility.ShowMessage(String.Format("读取的配方号{0}与扫码确认的配方号不符！",tpid));
                return;
            }
            jr =S7NetPlus.ReadOneString(51, 522);
            textBoxProductCode.Text = jr.stringValue;

            //SN码长度更改为读取DB51.DBX262.0
            jr = S7NetPlus.ReadOneInt(51, 262);
          int  snCodeLenght = jr.intValue;
            textBoxSNLenght.Text = snCodeLenght.ToString();
            
            
            //机型为读取DB51.DBX0.0
            jr = S7NetPlus.ReadOneString(51, 0);
            textBoxMachineModel.Text = jr.stringValue;

            //螺丝数量更改为读取DB51.DBX258.0(吸钉）
            jr = S7NetPlus.ReadOneInt(51, 258);
            NumberOfScrews.Text = jr.intValue.ToString();



            //客户信息读取数据库
            List<ProductInfo> l = CONT_ProductInfo.LoadList(" where tptaskid='" + tpid + "'");
            if (l.Count > 0)
            {
                //textBoxProductID.Text = l[0].ProductCode;
                //textBoxPrefixCode.Text = l[0].PrefixCode;
                textBoxSNLenght.Text = l[0].Customer;
                //textBoxProjectPhase.Text = l[0].ProjectPhase;
                //checkBox1.Checked = l[0].checkPrefixCode;
            }


           
        }
        void readPlc()
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

                NumberOfScrews.Text= jr.intValue.ToString();
            }
            else
            {
                utility.ShowMessage("读吸钉螺丝个数 DB14.DBX16.0失败," + jr.Message);
            }
            //6   读取加工SN码位数 DB14.DBX18.0    int
            jr = S7NetPlus.ReadOneInt(14, 18, 0);
            textBoxSNLenght.Text = jr.intValue.ToString();
            //7   读取加工产品识别码 DB14.DBX20.0    string
            jr = S7NetPlus.ReadOneString(14, 20, 0);
            textBoxProductCode.Text = jr.stringValue;
            //恢复260.0为false
            Thread.Sleep(100);
            jr = S7NetPlus.WriteDataBool(8, 260, false, 0);



        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            DialogResult d = utility.ShowMessageResponse("确定要切换配方吗？");
            if(d!=DialogResult.Yes)
            {
                return;
            }
            //按钮显示条件 DB51.DB938.0 , 打开面页时保持为True / DB211.DBX0.6   TRUE 时 / DB15.DBX12.0  不等于222
            if (!buttonVisable())
            {
                utility.ShowMessage("不满足配方切换条件！");
                return;
            }
            string str = "配方切换完成";
            //点击下载到PLC 按钮   DB8.DBX261.1    BOOL
            Model.ResultJsonInfo jr = S7NetPlus.WriteDataBool(8, 261, true, 1);

            Thread.Sleep(200);
            jr = S7NetPlus.WriteDataBool(8, 261, false, 1);
            if (!jr.Successed)
            {
                str = String.Format("配方切换失败，DB{0}.DBX{1}.{2}失败", 8, 261, 1);
                utility.ShowMessage(str);
                return;
            }
            toolStripStatusLabel1.Text = str;
        }

        private void buttonScanOK_Click(object sender, EventArgs e)
        {
            //if (keyInputs.Length > 0 && keyInputs.Length == utility.structCurrentWorkTask.SNCodeLenght)  ////大于0表示扫码枪没有enter结尾
            //{
            //手动 / 自动DB1.DBX0.4  信号为 FALSE  切换配方必须保证这个信号为FALSE，如果为true不允许操作配方切换
            Model.ResultJsonInfo jr = S7NetPlus.ReadOneBool(1, 0, 4);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("读取切换配方手动/自动DB{0}.DBX{1}.{2}失败", 1, 0, 4));
                return;
            }
            if (jr.booValue[0])
            {
                utility.ShowMessage(String.Format("自动状态DB{0}.DBX{1}.{2}=True,禁止切换配方！", 1, 0, 4));
                return;
            }

            if (utility.structCurrentWorkTask.checkfixCode)
            {
                int idex = keyInputs.IndexOf(utility.structCurrentWorkTask.PrefixCode, 0);
                if (idex < 0)
                {
                    utility.ShowMessage("产品SN不符合产品码规则！", 0);
                    return;
                }
            }

            //if (utility.dSV.workMode)
            //{
            //    long s = SV_Interlocking.sv_lockInfo(keyInputs);
            //    if (s != 0)
            //    {
            //        utility.ShowMessage("互锁验证失败！SN:" + keyInputs);
            //        return;
            //    }

            //}

            if (keyInputs.ToUpper().Substring(0, 1) != PDAMaster.ConfigurationKeys.PLC_UpLoad1Code)
            {
                utility.ShowMessage("产品SN类型不符合P码！");
                keyInputs = "";
                labelSN.Text = "";
                return;
            }

            //扫sn码发送到 DB43.DBX258.0 ：P123451234567896985741589654
            jr = S7NetPlus.WriteDataString(43, 258, keyInputs);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("发送SN码失败DB{0}.DBX{1}.{2}失败", 43, 258, 0));
                return;
            }
            //读取加工程序编号 DB15.DBX12.0  （1 - 10个编号）（根据扫码信息自动转换到相应的产品编号)(读取到数值为222时表示为识别码错误并且提示）
            jr = S7NetPlus.ReadOneInt(15, 12, 0);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("读取配方ID失败DB{0}.DBX{1}.{2}失败", 15, 12, 0));
                return;
            }
            if (jr.intValue == 222)
            {
                utility.ShowMessage("识别码错误!");
                return;
            }
            tpid = jr.intValue;
            textBoxTPID.Text = tpid.ToString();
            /*
             读取加工程序编号 DB15.DBX12.0  （1-10个编号）（根据扫码信息自动转换到相应的产品编号)(读取到数值为222时表示为识别码错误并且提示）
            按钮显示条件   DB51.DB938.0 , 打开面页时保持为True /  DB211.DBX0.6   TRUE 时 / DB15.DBX12.0  不等于222
            点击读取按钮         DB8.DBX260.0   (bool)  
            点击下载到PLC 按钮   DB8.DBX261.1    BOOL
             * */
            // Close();
            //}
        }
    }
}
