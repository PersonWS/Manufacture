using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormReporter : Form
    {
        public FormReporter()
        {
            InitializeComponent();
        }

        private void FormReporter_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
        }
        List<Model.WorkTaskDetail> list = new List<Model.WorkTaskDetail>();
        private void button1_Click(object sender, EventArgs e)
        {
            string dt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dt2 = dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd");
            list = Controller.CONT_WorkTaskDetail.LoadList(" mkd >= '" + dt1 + "' and mkd<'" + dt2 + "'");
            dataGridView1.DataSource = list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            //if(e.Equals(dataGridView1.CurrentCell))
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((int)row.Cells[5].Value == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:  //当月
                    dateTimePicker1.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
                    //当月最后一天23时59分59秒：
                    dateTimePicker2.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
                    break;
                case 1:
                    dateTimePicker1.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1);//上个月1号
                    dateTimePicker2.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1);//上个月最后一天

                    break;
                case 2:
                    dateTimePicker1.Value = DateTime.Now.AddMonths(0 - ((DateTime.Now.Month - 1) % 3)).AddDays(1 - DateTime.Now.Day);//本季度第一天； 
                    dateTimePicker2.Value = DateTime.Parse(DateTime.Now.AddMonths(3 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1);//本季度的最后一天


                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (list.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "文本文件(*.txt|*.txt";
                    sfd.FilterIndex = 1;
                    sfd.RestoreDirectory = true;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string filepath = sfd.FileName.ToString();
                        //string filenameext = filepath.Substring(filepath.LastIndexOf("\\") + 1);
                        FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Flush();
                        sw.BaseStream.Seek(0, SeekOrigin.Begin);
                        string ssrows = "";
                        for (int icol = 0; icol < dataGridView1.Columns.Count; icol++)
                        {
                            if (ssrows == "")
                                ssrows = dataGridView1.Columns[icol].HeaderText;
                            else
                                ssrows = ssrows + "," + dataGridView1.Columns[icol].HeaderText;
                        }
                        sw.WriteLine(ssrows);
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            ssrows = "";
                            for (int icol = 0; icol < dataGridView1.Columns.Count; icol++)
                            {
                                if (ssrows == "")
                                {
                                    if (dataGridView1.Rows[i].Cells[icol].Value == null)
                                        ssrows = "";
                                    else
                                        ssrows = dataGridView1.Rows[i].Cells[icol].Value.ToString();
                                }
                                else
                                {
                                    if (dataGridView1.Rows[i].Cells[icol].Value == null)
                                        ssrows = ssrows + "," + "";
                                    else
                                        ssrows = ssrows + "," + dataGridView1.Rows[i].Cells[icol].Value.ToString();
                                }
                            }
                            sw.WriteLine(ssrows);
                        }
                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        utility.ShowMessage("导出数据完成！" +Environment.NewLine+ filepath);
                    }
                }
                else
                {
                    utility.ShowMessage("没有可以导出的数据，请更改查询条件！");
                }
            }
            catch (Exception ex)
            {
                utility.ShowMessage("导出数据错误！"+ex.Message);
            }
        }
    }
}
