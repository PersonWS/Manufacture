using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormAlermQuery : Form
    {
        public FormAlermQuery()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
        }

        void initGrid()
        {
            int cols = dataGridView1.Columns.Count; //有一列datetimeid隐藏
            int w = (dataGridView1.Parent.Width - 41) / cols - 3;
            dataGridView1.Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            dataGridView1.Columns[0].Width = w-100;
            dataGridView1.Columns[1].Width = w+400;
            dataGridView1.Columns[2].Width = w-150;
            dataGridView1.Columns[3].Width = w-150;
            dataGridView1.Rows.Clear();
        }

        private void FormAlermQuery_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            initGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        List<Model.Log> list = new List<Model.Log>();
        private void button1_Click(object sender, EventArgs e)
        {
            string dt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");
            string dt2 = dateTimePicker1.Value.ToString("yyyy-MM-dd 23:59:59");
            list = Controller.CONT_Log.LoadList(" where logtype=3 and LogDatetime >= '" + dt1 + "' and LogDatetime<'" + dt2 + "'");
            dataGridView1.DataSource = list;
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

                        utility.ShowMessage("导出数据完成！" + Environment.NewLine + filepath);
                    }
                }
                else
                {
                    utility.ShowMessage("没有可以导出的数据，请更改查询条件！");
                }
            }
            catch (Exception ex)
            {
                utility.ShowMessage("导出数据错误！" + ex.Message);
            }
        }
    }
}
