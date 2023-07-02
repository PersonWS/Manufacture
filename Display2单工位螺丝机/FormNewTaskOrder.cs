using ScrewMachineManagementSystem.Controller;
using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormNewTaskOrder : Form
    {
        public FormNewTaskOrder()
        {
            InitializeComponent();
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
            string taskID = textBoxTaskID.Text;
            string productCode = textBoxProductCode.Text;

            int txtQty = (int)numericUpDownBoxQty.Value;
            if (string.IsNullOrEmpty(taskID))
            {
                utility.ShowMessage("请输入正确的任务单号！");
                textBoxTaskID.Focus();
                textBoxTaskID.SelectAll();
                return;
            }
            if(snCodeLenght_P==0)
            {
                utility.ShowMessage("SN码长度错误！{0}",snCodeLenght_P);
                return;
            }
            List<WorkTaskInfo> list = CONT_WorkTaskInfo.LoadList(" where taskid='" + taskID + "'");
            if (list.Count != 0)
            {
                if (utility.ShowMessageResponse(string.Format("此任务单已经生产使用 {0} 次,", list.Count) + "是否继续使用？") != DialogResult.Yes)
                {
                    textBoxTaskID.Focus();
                    textBoxTaskID.SelectAll();
                    return;
                }
            }
            
                var v = utility.ShowMessageResponse("确定保存执行新生产任务吗？", "系统提示", 1);
            if (v != DialogResult.Yes)
            {
                utility.ShowMessage("用户取消了新任务的确定操作！");
                DialogResult = DialogResult.No;
                return;
            }
            utility.structCurrentWorkTask.taskID = taskID;
            utility.structCurrentWorkTask.SNCodeLenght_P = snCodeLenght_P;
            utility.structCurrentWorkTask.SNCodeLenght_M = snCodeLenght_M;
            utility.structCurrentWorkTask.machinemodel = textBoxMachineModel.Text;
            utility.structCurrentWorkTask.NumberOfScrews = Convert.ToInt32(NumberOfScrews.Text);
            utility.structCurrentWorkTask.Customer = comboBoxCustomer.Text;
            utility.structCurrentWorkTask.productCode = textBoxProductCode.Text;
            utility.structCurrentWorkTask.Qty = Convert.ToInt32(txtQty);
            DialogResult = DialogResult.Yes;

            Close();
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

            textBoxTaskID.Focus();
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
            textBoxProductCode.Text = jr.stringValue;

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
            textBoxMachineModel.Text = jr.stringValue;

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


            textBoxTaskID.Focus();
            textBoxTaskID.SelectAll();
            //comboBoxProductCode.Focus();
            //comboBoxProductCode.Select();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ////手动 / 自动DB1.DBX0.4  信号为 FALSE  切换配方必须保证这个信号为FALSE，如果为true不允许操作配方切换
            //Model.ResultJsonInfo jr = S7NetPlus.ReadOneBool(1, 0, 4);
            //if(!jr.Successed)
            //{
            //    utility.ShowMessage(String.Format("读取切换配方手动/自动DB{0}.DBX{1}.{2}失败",1,0,4));
            //    return;
            //}
            //if(jr.booValue[0])
            //{
            //    utility.ShowMessage(String.Format("自动状态DB{0}.DBX{1}.{2}=True,禁止切换配方！", 1, 0, 4));
            //    return;
            //}

            
            //FormSwitchRecipes f =new FormSwitchRecipes();
            //if(f.ShowDialog()==DialogResult.OK)
            {
                refReshTPID();
            }
            
        }

        private void FormNewTaskOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            utility.FormNewTaskOpened = false;
        }
    }
}
