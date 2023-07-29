using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;

using System.Drawing.Printing;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows;
using System.Drawing.Imaging;
using System.Runtime.InteropServices.ComTypes;
using System.Globalization;
using static Dapper.SqlMapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem
{
    public partial class ManualCodeScanning : Form
    {

        [DllImport("Fnthex32.dll")]

        //public static extern int GETFONTHEX(
        //                      string BarcodeText,
        //                      string FontName,

        //                      int Orient,
        //                      int Height,
        //                      int Width,
        //                      int IsBold,
        //                      int IsItalic,
        //                      StringBuilder ReturnBarcodeCMD);

        public static extern int GETFONTHEX(string BarcodeText, string FontName, string FileName, int Orient, int Height, int Width, int IsBold, int IsItalic, StringBuilder ReturnBarcodeCMD);

        public ManualCodeScanning()
        {

            InitializeComponent();
            pictureBox2.Image = Generate1("5985959599", 30, 30);
        }
        private void FormSeletY_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2;//桌面的宽度的一半减去自身宽的的一半
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2 - 200;//桌面的高度的一半减去自身高度的一半

            // label1.Text = "当前任务产品识别码：" + utility.structCurrentWorkTask.productCode;
            // labelPcodeLength.Text = String.Format("P码长度{0}", utility.structCurrentWorkTask.SNCodeLenght_P);
            // labelMcodeLength.Text = String.Format("M码长度{0}", utility.structCurrentWorkTask.SNCodeLenght_M);
            timer1.Enabled = false;
            // Corecurrent.GetCorecurrent().TJ_scanPSN = "";
            // Corecurrent.GetCorecurrent().TJ_scanQKSN = "";

        }



        private void labelSelect1_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            this.Close();

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
            // Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
            // //允许生产后将SN码写入（DB43.DBX0.0)	String	W		
            // jr = S7NetPlus.WriteDataString(43, 0, Corecurrent.GetCorecurrent().TJ_scanPSN);
            // if (!jr.Successed)
            // {
            //     utility.ShowMessage(String.Format("SN码写入PLC出错，请检查！DB{0},DBX{1}.{2},", 43, 0, 0) + jr.Message);
            //     return;
            // }
            // //读取43.0.0，验证是否写入完成
            // jr = S7NetPlus.ReadOneString(43, 0);
            // if (!jr.Successed)
            // {
            //     utility.ShowMessage(String.Format("读取写入的SN码错误，DB{0},DBX{1}.{2},", 43, 0, 0) + jr.Message);
            //     return;
            // }
            // if (Corecurrent.GetCorecurrent().TJ_scanPSN != jr.stringValue)
            // {
            //     utility.ShowMessage(String.Format("SN码未写入DB{0},DBX{1}.{2},", 43, 0, 0));
            //     return;
            // }
            //
            // //对比成功后在触发写入指令（DB43.DBX256.0) true    BOOL W   非保持
            // jr = S7NetPlus.WriteDataBool(43, 256, true);
            // if (!jr.Successed)
            // {
            //     utility.ShowMessage(String.Format("触发写入指令true出错，请检查！DB{0},DBX{1}.{2},", 43, 256, 0) + jr.Message);
            //     return;
            // }
            // Thread.Sleep(200);
            // jr = S7NetPlus.WriteDataBool(43, 256, false);
            // if (!jr.Successed)
            // {
            //     utility.ShowMessage(String.Format("触发写入指令false出错，请检查！DB{0},DBX{1}.{2},", 43, 256, 0) + jr.Message);
            //     return;
            // }
            // foreach (int i in alarmIndex_CheckSN)
            // {
            //
            //     if (S7NetPlus.alarmPoints[i].realStatue)
            //     {
            //
            //         utility.ShowMessage(S7NetPlus.alarmPoints[i].AlarmInfo + ",请检查SN码是否正确" + Environment.NewLine + "SN码:" + Corecurrent.GetCorecurrent().TJ_scanPSN);
            //         return;
            //     }
            // }

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
            // jr = S7NetPlus.WriteDataBool(43, 256, true, 2);
            // if (!jr.Successed)
            // {
            //     utility.ShowMessage(String.Format("写入上位机启动信号出错，请检查！DB{0},DBX{1}.{2},", 43, 256, 2) + jr.Message);
            //     return;
            // }
            // labelMsg.Text = "上位机启动信号完成";
            // timer1.Enabled = true;

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
                // case (char)Keys.Back:
                //     if (keyInputs.Length > 0)
                //     {
                //         keyInputs = keyInputs.Substring(0, keyInputs.Length - 1);
                //         labelSN.Text = keyInputs;
                //         labelScanedLenght.Text = String.Format("扫码长度:{0}", keyInputs.Length);
                //     }
                //     break;
                case (char)Keys.Escape:
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;

                default:
                    keyInputs = keyInputs + ((char)e.KeyChar).ToString().ToUpper();

                    labelSN_M.Text = keyInputs;

                    if (keyInputs.Length == 28)
                    {
                        Writelog("2_" + keyInputs);
                        pictureBox2.Image = Generate1(keyInputs, 30, 30);
                        Writelog("打印二维码开始！" + keyInputs);
                        //Print(keyInputs);
                        //dd();
                    }
                    //keyInputs = labelSN.Text.ToUpper();  文本框测试用
                    int len = keyInputs.Length;
                    if (len > 0)
                    {
                        labelScanedLenght.Text = String.Format("扫码长度:{0}", keyInputs.Length);
                        labelMsg.Text = "正在扫码...";

                        if (len > 28)
                        {
                            MessageBox.Show("长度超限, 请重新扫码");
                            keyInputs = labelMsg.Text = "";

                            return;
                        }
                    }
                    break;

            }
        }

        #region 二维码









        public static void dd()
        {
            try
            {


                //string sBarCodeCMD = "";            //条码打印命令
                //StringBuilder sb1 = new StringBuilder(50240);

                //int i1;
                //i1 = GETFONTHEX("定向天线，DXX-1885-2025/25555-2635-90/65-14.5i/15.8i-", "Arial", 0, 30, 20, 0, 0, sb1);
                //sb1 = sb1.Replace("PrintTemplate", "ok01");
                //sBarCodeCMD = sb1.ToString() + "^XA^MD30^LH20,20^FO20,20^XGok01,1,1^FS^XZ"; Console.WriteLine(sBarCodeCMD);
                ////Open();
                ////PrintZPL(sBarCodeCMD);
                ////Close();



                ////string sBarCodeCMD;
                ////StringBuilder sb1 = new StringBuilder(10240);
                ////int i1;
                ////i1 = GETFONTHEX("需要用到的变量", "黑体", "PrintTemplate.prn", 0, 20, 20, 1, 0, sb1);
                ////sBarCodeCMD = sb1.ToString().Remove(0, 19).Replace("\n", "");
                //Writelog("123,打印开始");
            }
            catch (Exception ex)
            {

                Writelog(ex.Message);
            }


        }

        public void Print(string no)
        {
            try
            {
                Writelog("打印二维码方法进入开始！" + no);

                var bit = Generate1(no, 30, 30);

                //设置打印机属性
                PrintDocument pd = new PrintDocument();
                pd.DocumentName = "打印二维码"; // 设置文档名称
                                           //pd.PrinterSettings.PrinterName = pd.PrinterSettings.PrinterName; // // 设置打印机名称
                pd.PrinterSettings.PrinterName = "ZDesigner ZD888-203dpi";
                pd.OriginAtMargins = true;
                //处理打印事件
                pd.PrintPage += (sender, e) =>
                {
                    try
                    {
                        // 在此处绘制打印内容（可以使用Graphics对象绘制打印内容）          
                        e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Verdana", 20), new SolidBrush(Color.Black), 10, 10);
                        Writelog("打印汉字");
                        e.Graphics.DrawImage(bit, 20, 20);
                        Writelog("打印二维码13");
                    }
                    catch (Exception ex)
                    {
                        Writelog(ex.Message);
                    }
                };
                //页面设置
                //pd.DefaultPageSettings.Landscape = true; // 设置横向打印
                //pd.DefaultPageSettings.Margins = new Margins(1, 1, 1, 1); // 设置页边距
                // 执行打印操作
                pd.Print(); // 开始打印
                Writelog("打印完成");
            }
            catch (Exception e)
            {
                Writelog("打印机方法异常：" + e.Message);
            }


        }







        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap Generate1(string text, int width, int height)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {
                DisableECI = true,//设置内容编码
                CharacterSet = "UTF-8",  //设置二维码的宽度和高度
                Width = width,
                Height = height,
                Margin = 1//设置二维码的边距,单位不是固定像素
            };

            writer.Options = options;
            Bitmap map = writer.Write(text);
            return map;
        }

        #endregion







        private void labelSN_TextChanged(object sender, EventArgs e)
        {
            // if (labelSN.Text == "")
            // {
            //     labelSN.Text = "请扫描产品SN";
            // }
            //
            // labelSN.ForeColor = labelSN.Text == "请扫描产品SN" ? Color.LightGray : Color.Black;
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
            //utility.FormScanSNFormisClosed = true;//人为i关掉
            // utility.struckScanProduct = new Model.struckScanProductSN();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormSeletY_FormClosing(object sender, FormClosingEventArgs e)
        {
            //utility.FormScanSNFormOpened = false;
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

            labelSN_M.ForeColor = labelSN_M.Text == "请扫描产品SN" ? Color.LightGray : Color.Black;
        }
        private static void Writelog(string msg)
        {

            StreamWriter stream;
            //写⼊⽇志内容

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\SignInLog";

            //检查上传的物理路径是否存在，不存在则创建

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            //一天一个日志文件
            var name = DateTime.Now.ToString("yyyy-MM-dd");

            stream = new StreamWriter(path + "\\" + name + ".txt", true, Encoding.Default);

            stream.Write(DateTime.Now.ToString() + ":" + msg);

            stream.Write("\r\n");

            stream.Flush();
            stream.Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // if (Corecurrent.GetCorecurrent().TJ_scanPSN == null || Corecurrent.GetCorecurrent().TJ_scanQKSN == null)
            //     return;
            // if (Corecurrent.GetCorecurrent().TJ_scanPSN.Length != 28)
            //       // && Corecurrent.GetCorecurrent().TJ_scanQKSN.Length == utility.structCurrentWorkTask.SNCodeLenght_M))
            // {
            //     return;
            // }

            // Thread.Sleep(100);
            // Model.ResultJsonInfo jr = S7NetPlus.ReadOneBool(43, 300, 6);
            // if (!jr.Successed)
            // {
            //     labelMsg.Text = String.Format("读取上位机启动信号出错，请检查，或者退出！DB{0},DBX{1}.{2},", 43, 256, 2) + jr.Message;
            // }
            // else
            // {
            //     if (jr.booValue[0])
            //     {
            //         //jr = S7NetPlus.WriteDataBool(43, 256, true, 2);//20221129改为false 
            //         jr = S7NetPlus.WriteDataBool(43, 256, false, 2);
            //         if (!jr.Successed)
            //         {
            //             labelMsg.Text = String.Format("写入上位机启动信号false出错，请检查！DB{0},DBX{1}.{2},", 43, 256, 2) + jr.Message;
            //         }
            //
            //         timer1.Enabled = false;
            //         utility.struckScanProduct.stationId = 1;
            //         jr = S7NetPlus.WriteDataBool(43, 300, true, 3);
            //         Thread.Sleep(500);
            //         jr = S7NetPlus.WriteDataBool(43, 300, false, 3);
            //         DialogResult = DialogResult.OK;
            //         utility.FormScanSNFormisClosed = false;//正常扫码关掉
            //         Close();
            //     }
            //     else
            //     {
            //         timer1.Enabled = false;
            //         utility.struckScanProduct.stationId = 1;
            //         jr = S7NetPlus.WriteDataBool(43, 300, true, 3);
            //         Thread.Sleep(500);
            //         jr = S7NetPlus.WriteDataBool(43, 300, false, 3);
            //         DialogResult = DialogResult.OK;
            //         utility.FormScanSNFormisClosed = false;//正常扫码关掉
            //         Close();
            //     }
            //
            // }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            keyInputs = "";
            //labelSN.Text = "";
            labelSN_M.Text = "";
        }
    }
}
