using ScrewMachineManagementSystem.Controller;
using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class Frm_GetSN : Form
    {
        string _SN_Code = "";

        int _max_SN_Leng = 28;

        public event Action<string> SN_CodeGet;

        public event Action FormClosingByUser;

        public Frm_GetSN()
        {
            InitializeComponent();
            this.TopMost = true;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            var v = utility.ShowMessageResponse("确定要取消任务？"+Environment.NewLine+"没有任务单将不能显示曲线，不能保存数据！");
            if (v != DialogResult.Yes)
            {
                return;
            }
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Int32.TryParse(txt_SN_maxLength.Text, out _max_SN_Leng);
            _SN_Code = txt_SN_Scan.Text;
            //检查SN
            if (_SN_Code.Length> _max_SN_Leng)
            {
                MessageBox.Show(string.Format("扫描到的SN:{0} ,长度为：{1} ，超过最大允许长度!", _SN_Code, _SN_Code.Length), "SN码长度错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                txt_SN_Scan.Text="";
                return;
            }
            if (!string.IsNullOrEmpty(txt_SN_CheckCode.Text))
            {
                if (_SN_Code.Substring(0, txt_SN_CheckCode.Text.Length) != txt_SN_CheckCode.Text)
                {
                    MessageBox.Show(string.Format("扫描到的SN:{0} ,SN首部校验失败", _SN_Code), "SN码校验失败", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    txt_SN_Scan.Text = "";
                    return;
                }
                else
                {
                    if (SN_CodeGet != null)
                    {
                        SN_CodeGet(_SN_Code);
                    }
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(string.Format("SN长度为0，请重新扫描", _SN_Code, _SN_Code.Length), "SN码长度错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void FormNewTaskOrder_Load(object sender, EventArgs e)
        {

            //List<ProductInfo> list = CONT_ProductInfo.LoadList(" order by mkd desc");
            //foreach (var item in list)
            //{
            //    comboBoxProductCode.Items.Add(item.ProductCode);
            //}
            List<string> customers= CONT_WorkTaskInfo.LoadCustomerList();
            foreach(string customer in customers)
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
            //产品识别码更改为读取DB51.DBX522.0
            Model.ResultJsonInfo jr = S7NetPlus.ReadOneString(51, 522);
            txt_SN_CheckCode.Text = jr.stringValue;

            //SN码长度更改为读取DB51.DBX262.0
            jr = S7NetPlus.ReadOneInt(51, 262);
             snCodeLenght_P = jr.intValue;
            textBoxSNLenght.Text = snCodeLenght_P.ToString();
            //M码长度，DB51.dbx952.0
            jr = S7NetPlus.ReadOneInt(51, 952);
            snCodeLenght_M = jr.intValue;
            textBoxMLenght.Text = snCodeLenght_M.ToString(); ;
            
            //配方号为读取DB51.DBX256.0
            jr = S7NetPlus.ReadOneInt(51, 256);
            int tpid = jr.intValue;
            textBoxTPID.Text = tpid.ToString();
            //机型为读取DB51.DBX0.0
            jr = S7NetPlus.ReadOneString(51, 0);
            txt_SN_maxLength.Text = jr.stringValue;

            //螺丝数量更改为读取DB51.DBX258.0(吸钉）
            jr = S7NetPlus.ReadOneInt(51, 258);
            NumberOfScrews.Text = jr.intValue.ToString();
            
            

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
            if (FormClosingByUser != null)
            {
                FormClosingByUser();
            }
        }
    }
}
