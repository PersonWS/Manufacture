using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormIO : Form
    {
        public FormIO()
        {
            InitializeComponent();

        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        List<Model.IOInfo> iOInfos = Controller.CONT_IOInfo.LoadList("");
        private void FormIO_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            initOver = false;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = iOInfos.FindAll(p => p.DeviceID == 2);
            initOver = true;
            if (dataGridView1.RowCount > 0)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;

            //comboBox1.DataSource = devices;
            //comboBox1.DisplayMember = "Name";
            //int deviceIndex=comboBox1.SelectedIndex;
            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource=iOInfos.FindAll(p=>p.DeviceID==deviceIndex+1);  //只取name字段，重新生成新的List集合
            //if (utility.LoginModeEngineer)
            //{
            //    foreach (DataGridViewColumn dc in dataGridView1.Columns)
            //    {
            //        if (dc.Name == "EnableOrNot" || dc.Name == "AllowModify")
            //        {
            //            dc.ReadOnly = false;
            //        }
            //    }
            //}
        }
        bool initOver = false;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //只取name字段，重新生成新的List集合
            //if (utility.LoginModeEngineer)
            //{
            //    foreach(DataGridViewRow  dr in dataGridView1.Rows)
            //    {
            //        dr.Cells[6].ReadOnly =! (bool)dr.Cells[7].Value;           
            //    }
            //}
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
            //    e.RowBounds.Location.Y,
            //    dataGridView1.RowHeadersWidth - 4,
            //    e.RowBounds.Height);
            //TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            //    dataGridView1.RowHeadersDefaultCellStyle.Font,
            //    rectangle,
            //    dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
            //    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int crow = e.RowIndex;
            int ccol = e.ColumnIndex;
            if (ccol == 7)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[7];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                List<Model.IOInfo> infos = Controller.CONT_IOInfo.LoadList(" where id=" + id);
                Model.IOInfo info = infos[0];
                if (flag)
                    info.EnableOrNot = true;
                else
                    info.EnableOrNot = false;

                Controller.CONT_IOInfo.Update(info);
            }
        }

        private void FormIO_FormClosing(object sender, FormClosingEventArgs e)
        {
            utility.initRegisterAddress();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;


            if (!utility.modbusObjects[1].serialPort.IsOpen)
                utility.modbusObjects[1].serialPort.Open();


            ////运动控制 输入4096-4119
            //bool[] buffers = SerialCommunication.master[1].ReadCoils(1, 4096, 25);
            //for (int index = 0; index < buffers.Length; index++)
            //{
            //    int addr = 4096 + index;
            //    foreach (DataGridViewRow dr in dataGridView1.Rows)
            //    {
            //        if (Convert.ToInt32(dr.Cells[3].Value) == addr && (bool)dr.Cells[8].Value != buffers[index])
            //        {
            //            dr.Cells[8].Value = buffers[index];

            //        }
            //    }
            //}

            //运动控制 /输出 4176-4191
            //buffers = SerialCommunication.master[1].ReadCoils(1, 4176, 16);
            //for (int index = 0; index < buffers.Length; index++)
            //{
            //    int addr = 4176 + index;
            //    foreach (DataGridViewRow dr in dataGridView1.Rows)
            //    {
            //        if (Convert.ToInt32(dr.Cells[3].Value) == addr)
            //        {
            //            dr.Cells[8].Value = buffers[index];

            //        }
            //    }
            //}

            dataGridView1.EndEdit();


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 8)
            {

                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[8];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                int addr = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                string types = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                //if (types.IndexOf("输出") > -1)
                //    SerialCommunication.master[1].WriteSingleCoil(1, (ushort)addr, !flag);

            }
        }
    }
}

