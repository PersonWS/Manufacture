
using ScrewMachineManagementSystem.Controller;
using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormProductInfo : Form
    {
        public FormProductInfo()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panel1, 0, 0, 0, 1);
        }

        string _productID = string.Empty;
        string _productName = string.Empty;
        private void FormProductInfo_Load(object sender, EventArgs e)
        {



            int w = (panel1.Width - 80) / (dataGridView1.Columns.Count - 1);
            foreach (DataGridViewColumn v in dataGridView1.Columns)
            {
                if (v.Visible)
                    v.Width = w;
            }
            getProducts();


        }

        void getProducts()
        {
            string yyyymm = DateTime.Now.ToString("yyyy-MM");
            initdataview(yyyymm);
            comboBox1.Items.Add("所有产品");
            List<ProductInfo> l = CONT_ProductInfo.LoadListDistinct("select distinct substr(date(mkd), 1, 7) as productcode from ProductInfo ");

            foreach (var l1 in l)
            {
                string ss = l1.ProductCode;
                comboBox1.Items.Add(ss);
            }
            dataGridView1.DataSource = null;

            List<ProductInfo> l2 = CONT_ProductInfo.LoadList("");
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = l2;
        }
        void initdataview(string yyyymm)
        {

            dataGridView1.DataSource = null;

            List<ProductInfo> l = CONT_ProductInfo.LoadList(" where substr(date(mkd), 1, 7)='" + yyyymm + "'");
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = l;

        }





        private void labelEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_productID))
            {
                FormProductEdit f = new FormProductEdit(_productID);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    //string yyyymm = DateTime.Now.ToString("yyyy-MM");
                    //if (comboBox1.Text != string.Empty)
                    //    yyyymm = comboBox1.Text;
                    //initdataview(yyyymm);

                    comboBox1.SelectedIndex = 0;
                    dataGridView1.DataSource = null;

                    List<ProductInfo> l2 = CONT_ProductInfo.LoadList("");
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = l2;

                }

            }
        }

        private void labelAdd_Click(object sender, EventArgs e)
        {
            FormProductEdit f = new FormProductEdit("");
            if (f.ShowDialog() == DialogResult.OK)
            {
                string yyyymm = DateTime.Now.ToString("yyyy-MM");
                if (comboBox1.Text != string.Empty)
                    yyyymm = comboBox1.Text;
                initdataview(yyyymm);
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.DataSource = null;

                List<ProductInfo> l2 = CONT_ProductInfo.LoadList("");
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = l2;
            }
            else
            {
                string yyyymm = comboBox1.Text;
                initdataview(yyyymm);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                labelEdit.Enabled = true;
                labelDelete.Enabled = true;
                _productID = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                _productName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            else
            {
                labelEdit.Enabled = false;
                labelDelete.Enabled = false;
            }
        }

        private void labelDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_productID))
            {
                if (utility.ShowMessageResponse("确定要删除选中的产品信息吗？"+Environment.NewLine+"产品码："+ _productName) == DialogResult.Yes)
                {
                    Model.ProductInfo p = new ProductInfo();
                    p.Id = Convert.ToInt32(_productID);
                    CONT_ProductInfo.Delete(p);
                    getProducts();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            FormPartEdit form = new FormPartEdit();
            form.ShowDialog();
        }
    }
}

























































