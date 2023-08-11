using ScrewMachineManagementSystem.Controller;
using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

using System.Threading;

namespace ScrewMachineManagementSystem
{
    public partial class Frm_GetSN : Form
    {
        string _SN_Code = "";

        int _max_SN_Leng = 28;

        public event Action<string> SN_CodeGet;

        /// <summary>
        /// 0:点击OK 键，写入SN   1：点击取消键，取消写入SN
        /// </summary>
        public event Action<bool> FormClosing_OK_Cancel;

        List<byte> _byte = new List<byte>();
        List<char> _char = new List<char>();

        InputLanguage _currentLanguage;


        public Frm_GetSN()
        {
            InitializeComponent();
            _currentLanguage = InputLanguage.CurrentInputLanguage;
            this.TopMost = true;

            System.Threading.ThreadPool.QueueUserWorkItem(Initialize, null);
             
   
            //System.Threading.ThreadPool.QueueUserWorkItem( SetInputKeyboard ,null);

        }

        /// <summary>/// 输入法切ENG/// </summary>
        ///

            ~Frm_GetSN()
        {

        }



    private void Initialize(object obj)
        {
            try
            {
                System.Threading.Thread.Sleep(200);
                this.Invoke(new Action(() =>
                {
                    txt_SN_CheckCode.Text = LogUtility.ReadConfiguration("SN_CHECK_STRING");//记录用户校验码
                }));

            }
            catch (Exception)
            {

            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            var v = utility.ShowMessageResponse("确定要取消任务？" + Environment.NewLine + "没有任务单将不能显示曲线，不能保存数据！");
            if (v != DialogResult.Yes)
            {
                return;
            }
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Int32.TryParse(txt_SN_maxLength.Text, out _max_SN_Leng);
            _SN_Code = txt_SN_Scan.Text.Replace("\r","");
            //检查SN
            if (_SN_Code.Length != _max_SN_Leng)
            {
                MessageBox.Show(string.Format("扫描到的SN:{0} \r\n实际扫描的SN长度为：{1} ,要求的SN长度为：{2} .\r\n请检查SN或输入法设置是否正确.", _SN_Code, _SN_Code.Length,_max_SN_Leng), "SN码长度错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                txt_SN_Scan.Text = "";
                return;
            }
            if (!string.IsNullOrEmpty(txt_SN_CheckCode.Text))
            {
                if (string.IsNullOrEmpty(txt_SN_Scan.Text))
                {
                    if (DialogResult.Cancel == MessageBox.Show(string.Format("SN为空，确认输入吗？", _SN_Code), "SN码校验失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2))
                    {
                        return;
                    } 
                }
                else
                {
                    if (_SN_Code.Substring(0, txt_SN_CheckCode.Text.Length) != txt_SN_CheckCode.Text)
                    {
                        MessageBox.Show(string.Format("扫描到的SN:{0} ,SN首部校验失败", _SN_Code), "SN码校验失败", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        txt_SN_Scan.Text = "";
                        return;
                    }
                }

            }
            if (SN_CodeGet != null)
            {
                SN_CodeGet(_SN_Code);
            }
            if (FormClosing_OK_Cancel != null)
            {
                FormClosing_OK_Cancel(true);
            }
            this.Close();
        }

        private void FormNewTaskOrder_Load(object sender, EventArgs e)
        {

            //List<ProductInfo> list = CONT_ProductInfo.LoadList(" order by mkd desc");
            //foreach (var item in list)
            //{
            //    comboBoxProductCode.Items.Add(item.ProductCode);
            //}
            List<string> customers = CONT_WorkTaskInfo.LoadCustomerList();
            foreach (string customer in customers)
            {
                comboBoxCustomer.Items.Add(customer);
            }
            utility.FormNewTaskOpened = true;
            refReshTPID();

            txt_SN_Scan.Focus();
        }
        int snCodeLenght_P = 0;
        int snCodeLenght_M = 0;
        /// <summary>
        /// 刷新当前任务配方
        /// </summary>
        void refReshTPID()
        {
            ////产品识别码更改为读取DB51.DBX522.0
            //Model.ResultJsonInfo jr = S7NetPlus.ReadOneString(51, 522);
            //txt_SN_CheckCode.Text = jr.stringValue;

            ////SN码长度更改为读取DB51.DBX262.0
            //jr = S7NetPlus.ReadOneInt(51, 262);
            // snCodeLenght_P = jr.intValue;
            //textBoxSNLenght.Text = snCodeLenght_P.ToString();
            ////M码长度，DB51.dbx952.0
            //jr = S7NetPlus.ReadOneInt(51, 952);
            //snCodeLenght_M = jr.intValue;
            //textBoxMLenght.Text = snCodeLenght_M.ToString(); ;

            ////配方号为读取DB51.DBX256.0
            //jr = S7NetPlus.ReadOneInt(51, 256);
            //int tpid = jr.intValue;
            //textBoxTPID.Text = tpid.ToString();
            ////机型为读取DB51.DBX0.0
            //jr = S7NetPlus.ReadOneString(51, 0);
            //txt_SN_maxLength.Text = jr.stringValue;

            ////螺丝数量更改为读取DB51.DBX258.0(吸钉）
            //jr = S7NetPlus.ReadOneInt(51, 258);
            //NumberOfScrews.Text = jr.intValue.ToString();



            //客户信息读取数据库
            //List<ProductInfo> l = CONT_ProductInfo.LoadList(" where tptaskid='" + tpid + "'");
            //if (l.Count > 0)
            //{
            //    //textBoxProductID.Text = l[0].ProductCode;
            //    //textBoxPrefixCode.Text = l[0].PrefixCode;
            //    textBoxSNLenght.Text = l[0].Customer;
            //    //textBoxProjectPhase.Text = l[0].ProjectPhase;
            //    //checkBox1.Checked = l[0].checkPrefixCode;
            //}


            txt_SN_Scan.Focus();
            txt_SN_Scan.SelectAll();
            //comboBoxProductCode.Focus();
            //comboBoxProductCode.Select();
        }


        private void Frm_GetSN_FormClosing(object sender, FormClosingEventArgs e)
        {
            InputLanguage.CurrentInputLanguage = _currentLanguage;
        }

        private void txt_SN_Scan_KeyPress(object sender, KeyPressEventArgs e)
        {
            _byte.Add((byte)e.KeyChar);
            _char.Add(e.KeyChar);
            if (e.KeyChar == '\r')
            {
                buttonOK_Click(sender, e);
            }
        }

        //调用API
        [System.Runtime.InteropServices.DllImport(@"C:\Windows\System32\user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow(); //获得本窗体的句柄
        [System.Runtime.InteropServices.DllImport(@"C:\Windows\System32\user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体
        [System.Runtime.InteropServices.DllImport(@"C:\Windows\System32\user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern IntPtr SetFocus(HandleRef hWnd);

        public void PlayAround()
        {
            Process[] processList = Process.GetProcesses();

            foreach (Process theProcess in processList)
            {
                if (theProcess.ProcessName == "Display3单工位螺丝机")
                {
                    string mainWindowTitle = theProcess.MainWindowTitle;
                    SetFocus(new HandleRef(null, theProcess.MainWindowHandle));
                }

            }

        }
        private void SetInputKeyboard(object obj)
        {
            Thread.Sleep(300);
            this.Invoke(new Action(() =>
            {
                //这两个方法都失败了
                //if (this.Handle != GetForegroundWindow()) //持续使该窗体置为最前,屏蔽该行则单次置顶
                //{
                //    bool a = SetForegroundWindow(this.Handle);

                //}
                //PlayAround()
                this.ImeMode = ImeMode.Off;
                this.txt_SN_Scan.ImeMode = ImeMode.Off;
                InputLanguageCollection c = InputLanguage.InstalledInputLanguages;
                    foreach (InputLanguage item in InputLanguage.InstalledInputLanguages)
                    {
                        if (item.Culture.ToString().ToLower().Contains("en"))
                        {
                            InputLanguage.CurrentInputLanguage = item;
                        }
                        // InputLanguage.CurrentInputLanguage
                    }


            }));
        }

        private void txt_SN_CheckCode_TextChanged(object sender, EventArgs e)
        {
            LogUtility.WriteConfiguration("SN_CHECK_STRING", txt_SN_CheckCode.Text );
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show(string.Format("手动关闭窗体后，软件将不会向PLC写入SN，如果需要再次写入SN，需要使用【扫码】功能手动扫码!\r\n 确认要进行手动关闭操作码？"), "SN码扫码界面关闭提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                _SN_Code = null;
                if (FormClosing_OK_Cancel != null)
                {
                    FormClosing_OK_Cancel(false);
                }
                this.Close();
            }
        }

        private void btn_clearSN_Click(object sender, EventArgs e)
        {
            this.txt_SN_Scan.Text = "";
        }

        private void Frm_GetSN_Activated(object sender, EventArgs e)
        {
            this.ImeMode = ImeMode.Off;
            this.txt_SN_Scan.ImeMode = ImeMode.Off;
            InputLanguageCollection c = InputLanguage.InstalledInputLanguages;
            foreach (InputLanguage item in InputLanguage.InstalledInputLanguages)
            {
                if (item.Culture.ToString().ToLower().Contains("en"))
                {
                    InputLanguage.CurrentInputLanguage = item;
                }
                // InputLanguage.CurrentInputLanguage
            }
           
        }



        //在窗体加载的时候给变量赋值,即将当前窗体的句柄赋给变量

    }
}
