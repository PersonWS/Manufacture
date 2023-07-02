using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormSeletY : Form
    {
        public FormSeletY()
        {
            InitializeComponent();
        }
        private void FormSeletY_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2;//桌面的宽度的一半减去自身宽的的一半
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2 - 200;//桌面的高度的一半减去自身高度的一半

            utility.FormScanSNFormOpened = true;
            label1.Text = "当前任务产品识别码：" + utility.structCurrentWorkTask.productCode;
            labelPcodeLength.Text = String.Format("P码长度{0}", utility.structCurrentWorkTask.SNCodeLenght_P);
            labelMcodeLength.Text = String.Format("M码长度{0}", utility.structCurrentWorkTask.SNCodeLenght_M);
            timer1.Enabled = false;
            utility.struckScanProduct.productSN = "";
            utility.struckScanProduct.productSN_M = "";
            if (utility.LoginModeEngineer)
            {
                //labelNoSnMode.Visible = true;
                labelSelect1.Visible = true;
                //labelSelect2.Visible = true;
            }
        }



        private void labelSelect1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(utility.struckScanProduct.productSN))
            {
                labelSN.Text = "请扫描产品SN";
                utility.ShowMessage("请扫描产品SN码！");
                return;
            }
            if (utility.structCurrentWorkTask.SNCodeLenght_M != 0)
            {
                if (string.IsNullOrEmpty(utility.struckScanProduct.productSN_M))
                {
                    labelSN_M.Text = "请扫描产品M码";
                    utility.ShowMessage("请扫描产品M码！");
                    return;
                }
            }

            //utility.boolClearDataGridView = true;//清除螺丝扭力表格
            /*	
                3	允许生产后将SN码写入（DB43.DBX0.0)	String	W		
                4	写入后读取，相同写入成功（DB43.DBX0.0)	String	R		
                5	对比成功后在触发写入指令（DB43.DBX256.0) true	BOOL	W	非保持	
                6	加工产品ID不符（DB4.DBX517.3)	BOOL	R	报警信息	写入后出现报警信息不执行第7步骤，提示检查sn码是否正确，从新执行1-5.直到报警解除在执行7.
	                产品ID不在设定范围 （DB4.DBX517.4)	BOOL	R		
	                读取sn码位数错误 （DB4.DBX517.7)	BOOL	R		
                7	写入上位机启动信号（DB43.DBX256.2) true	BOOL	W	保持	
                8	检查转盘信号到位（DB1.DBX3.3）==false	BOOL	R		
                	写入上位机启动信号（DB43.DBX256.2) false	BOOL	W		
             */
            Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
            //允许生产后将SN码写入（DB43.DBX0.0)	String	W		
            jr = S7NetPlus.WriteDataString(43, 0, utility.struckScanProduct.productSN);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("SN码写入PLC出错，请检查！DB{0},DBX{1}.{2},", 43, 0, 0) + jr.Message);
                return;
            }
            //读取43.0.0，验证是否写入完成
            jr = S7NetPlus.ReadOneString(43, 0);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("读取写入的SN码错误，DB{0},DBX{1}.{2},", 43, 0, 0) + jr.Message);
                return;
            }
            if (utility.struckScanProduct.productSN != jr.stringValue)
            {
                utility.ShowMessage(String.Format("SN码未写入DB{0},DBX{1}.{2},", 43, 0, 0));
                return;
            }

            //对比成功后在触发写入指令（DB43.DBX256.0) true    BOOL W   非保持
            jr = S7NetPlus.WriteDataBool(43, 256, true);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("触发写入指令true出错，请检查！DB{0},DBX{1}.{2},", 43, 256, 0) + jr.Message);
                return;
            }
            Thread.Sleep(200);
            jr = S7NetPlus.WriteDataBool(43, 256, false);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("触发写入指令false出错，请检查！DB{0},DBX{1}.{2},", 43, 256, 0) + jr.Message);
                return;
            }
            foreach (int i in alarmIndex_CheckSN)
            {

                if (S7NetPlus.alarmPoints[i].realStatue)
                {

                    utility.ShowMessage(S7NetPlus.alarmPoints[i].AlarmInfo + ",请检查SN码是否正确" + Environment.NewLine + "SN码:" + utility.struckScanProduct.productSN);
                    return;
                }
            }
            
            ////加工产品ID不符（DB4.DBX517.3)	BOOL
            //jr = S7NetPlus.ReadOneBool(S7NetPlus.PLC_ProductID_Error_Bool);
            //if (!jr.Successed)
            //{
                
            //    utility.ShowMessage(jr.Message);
                
            //    return;
            //}
            //else
            //{
            //    if (jr.booValue[0])
            //    {
            //        utility.ShowMessage(("加工产品ID不符"));
            //        return;
            //    }
            //}

            
            //if (S7NetPlus.boolSNCode_Lenght_Error)
            //{
            //    utility.ShowMessage(("读取sn码位数错误"));
            //    return;
            //}
            
            //7 写入上位机启动信号（DB43.DBX256.2) true	BOOL	W	保持	
            jr = S7NetPlus.WriteDataBool(43, 256, true, 2);
            if (!jr.Successed)
            {
                utility.ShowMessage(String.Format("写入上位机启动信号出错，请检查！DB{0},DBX{1}.{2},", 43, 256, 2) + jr.Message);
                return;
            }
            labelMsg.Text = "上位机启动信号完成";
            timer1.Enabled = true;
           
        }

        /// <summary>
        /// 报警信息中检查SN码的ｉｎｄｅｘ
        /// 加工产品ID不符（DB4.DBX517.3) ：43，-26
        /// 产品ID不在设定范围 （DB4.DBX517.4)：44-27
        /// 读取sn码位数错误 （DB4.DBX517.7)：47-28
        /// </summary>
        int[] alarmIndex_CheckSN = new int[] { 26, 27, 28 };

        string keyInputs = "";
        private void FormSeletY_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Back:
                    if (keyInputs.Length > 0)
                    {
                        keyInputs = keyInputs.Substring(0, keyInputs.Length - 1);
                        labelSN.Text = keyInputs;
                        labelScanedLenght.Text = String.Format("扫码长度:{0}", keyInputs.Length);
                    }
                    break;
                case (char)Keys.Escape:
                    utility.struckScanProduct.productSN = "";
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;

                default:
                    keyInputs = keyInputs + ((char)e.KeyChar).ToString().ToUpper();
                    //keyInputs = labelSN.Text.ToUpper();  文本框测试用
                    int len = keyInputs.Length;
                    if (len > 0)
                    {
                        string sn1 = keyInputs.Substring(0, 1);
                        if (sn1 == PDAMaster.ConfigurationKeys.PLC_UpLoad1Code)
                        {
                            if (len < utility.structCurrentWorkTask.SNCodeLenght_P)
                            {
                                //keyInputs = keyInputs + ((char)e.KeyChar).ToString().ToUpper();
                                labelScanedLenght.Text = String.Format("扫码长度:{0}", keyInputs.Length);
                                labelMsg.Text = "正在扫码...";
                            }
                            else
                            {
                                e.Handled = true;
                            }

                            if (len == utility.structCurrentWorkTask.SNCodeLenght_P)
                            {
                                if (utility.structCurrentWorkTask.productCode != keyInputs.Substring(6, 6))
                                {
                                    utility.ShowMessage("产品SN不符合产品识别码规则！");
                                    keyInputs = "";
                                    return;

                                }
                                if (utility.dSV.workMode)
                                {
                                    long s = SV_Interlocking.sv_lockInfo(keyInputs);
                                    if (s != 0)
                                    {
                                        utility.ShowMessage("互锁验证失败！SN:" + keyInputs);
                                        return;
                                    }
                                }
                                utility.struckScanProduct.productSN = keyInputs;
                                labelSN.Text = keyInputs;
                                keyInputs = "";
                            }
                        }
                        else
                        {
                            if (sn1 == PDAMaster.ConfigurationKeys.MES_UpLoad1Code)
                            {
                                if (len < utility.structCurrentWorkTask.SNCodeLenght_M)
                                {
                                    // keyInputs = keyInputs + ((char)e.KeyChar).ToString().ToUpper();
                                    labelScanedLenght.Text = String.Format("扫码长度:{0}", keyInputs.Length);
                                    labelMsg.Text = "正在扫码：" + keyInputs;
                                }
                                else
                                {
                                    e.Handled = true;
                                }

                                if (len == utility.structCurrentWorkTask.SNCodeLenght_M)
                                {
                                    utility.struckScanProduct.productSN_M = keyInputs;
                                    labelSN_M.Text = keyInputs;
                                    keyInputs = "";
                                }
                            }
                            else
                            {
                                labelMsg.Text = "扫码错误！" + keyInputs;
                                keyInputs = "";
                            }
                        }

                        if (utility.struckScanProduct.productSN.Length == utility.structCurrentWorkTask.SNCodeLenght_P &&
                                     utility.struckScanProduct.productSN_M.Length == utility.structCurrentWorkTask.SNCodeLenght_M)
                        {
                            
                            labelSelect1_Click(null, null);
                        }
                    }
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

        private void FormSeletY_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F4) && (e.Alt == true))  //屏蔽ALT+F4
            {
                e.Handled = true;
            }
        }

        private void labelX_Click(object sender, EventArgs e)
        {
            utility.FormScanSNFormisClosed = true;//人为i关掉
            utility.struckScanProduct = new Model.struckScanProductSN();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormSeletY_FormClosing(object sender, FormClosingEventArgs e)
        {

            utility.FormScanSNFormOpened = false;
        }



        private void labelSelect1_MouseDown(object sender, MouseEventArgs e)
        {
            Label l = (Label)sender;
            l.BackColor = Color.Lime;
        }

        private void labelSelect1_MouseUp(object sender, MouseEventArgs e)
        {
            Label l = (Label)sender;
            l.BackColor = SystemColors.ButtonFace;
        }

        private void labelSN_M_TextChanged(object sender, EventArgs e)
        {
            if (labelSN_M.Text == "")
            {
                labelSN_M.Text = "请扫描产品SN";
            }

            if (labelSN_M.Text == "请扫描产品SN")
            {
                labelSN_M.ForeColor = Color.LightGray;
            }
            else
            {
                labelSN_M.ForeColor = Color.Black;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (utility.struckScanProduct.productSN == null || utility.struckScanProduct.productSN_M == null)
                return;
            if (!(utility.struckScanProduct.productSN.Length == utility.structCurrentWorkTask.SNCodeLenght_P &&
                                 utility.struckScanProduct.productSN_M.Length == utility.structCurrentWorkTask.SNCodeLenght_M))
            {
                return;
            }

            Thread.Sleep(100);
            Model.ResultJsonInfo jr = S7NetPlus.ReadOneBool(43, 300, 6);
            if (!jr.Successed)
            {
                labelMsg.Text = String.Format("读取上位机启动信号出错，请检查，或者退出！DB{0},DBX{1}.{2},", 43, 256, 2) + jr.Message;
            }
            else
            {
                if (jr.booValue[0])
                {
                    //jr = S7NetPlus.WriteDataBool(43, 256, true, 2);//20221129改为false 
                    jr = S7NetPlus.WriteDataBool(43, 256, false, 2);
                    if (!jr.Successed)
                    {
                        labelMsg.Text = String.Format("写入上位机启动信号false出错，请检查！DB{0},DBX{1}.{2},", 43, 256, 2) + jr.Message;
                    }

                    timer1.Enabled = false;
                    utility.struckScanProduct.stationId = 1;
                    jr = S7NetPlus.WriteDataBool(43, 300, true, 3);
                    Thread.Sleep(500);
                    jr = S7NetPlus.WriteDataBool(43, 300, false, 3);
                    DialogResult = DialogResult.OK;
                    utility.FormScanSNFormisClosed = false;//正常扫码关掉
                    Close();
                }
                else
                {
                    timer1.Enabled = false;
                    utility.struckScanProduct.stationId = 1;
                    jr = S7NetPlus.WriteDataBool(43, 300, true, 3);
                    Thread.Sleep(500);
                    jr = S7NetPlus.WriteDataBool(43, 300, false, 3);
                    DialogResult = DialogResult.OK;
                    utility.FormScanSNFormisClosed = false;//正常扫码关掉
                    Close();
                }

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            keyInputs = "";
            utility.struckScanProduct.productSN = null;
            utility.struckScanProduct.productSN_M = null;
            labelSN.Text = "";
            labelSN_M.Text = "";
        }
    }
}
