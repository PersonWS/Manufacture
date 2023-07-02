using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormIO2 : Form
    {
        public FormIO2()
        {
            InitializeComponent();
        }
        List<Model.IOInfo> iOInfos = Controller.CONT_IOInfo.LoadList("");
        private void FormIO2_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            List<Model.IOInfo2> iOInfo2s = Controller.CONT_IOInfo2.LoadList("");


            dataGridView1.DataSource = null;
            dataGridView1.DataSource = iOInfo2s;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            if (dataGridView1.RowCount > 0)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
                return;

            ResultJsonInfo jr = getExtendedIO(RegisterAddress.DistributedModel_TPID);
            if (jr.Successed)
            {
                foreach(DataGridViewRow dr in dataGridView1.Rows)
                {
                    if (dr.Cells[1].Value.ToString()== RegisterAddress.DistributedModel_TPID.Address.ToString())
                    {
                        int addrIndex =Convert.ToInt32( dr.Cells[2].Value);
                        if (ExtendedIO_560[15 - addrIndex] == '1')
                        {
                            dr.Cells[5].Value = true;
                            dr.Cells[5].Style.BackColor = Color.LimeGreen;
                        }
                        else
                        {
                            dr.Cells[5].Value = false;
                            dr.Cells[5].Style.BackColor = Color.Gray;
                        }
                        if (dr.Cells[6].Value.ToString() == "1")
                            dr.Cells[5].ReadOnly = false;
                        else
                            dr.Cells[5].ReadOnly = true;
                    }
                }
                dataGridView1.EndEdit();
            }

            jr = getExtendedIO(RegisterAddress.DistributedModel_630);
            if (jr.Successed)
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    if (dr.Cells[1].Value.ToString() == RegisterAddress.DistributedModel_630.Address.ToString())
                    {
                        int addrIndex = Convert.ToInt32(dr.Cells[2].Value);
                        if (ExtendedIO_560[15 - addrIndex] == '1')
                        {
                            dr.Cells[5].Value = true;
                            dr.Cells[5].Style.BackColor = Color.LimeGreen;
                        }
                        else
                        {
                            dr.Cells[5].Value = false;
                            dr.Cells[5].Style.BackColor = Color.Gray;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 扩展IO模块，地址560，输出1，地址560，16位
        /// FEDCDA9876543210
        /// </summary>
        char[] ExtendedIO_560 = Enumerable.Repeat('0', 16).ToArray();

        /// <summary>
        /// 解析扩展IO——560地址
        /// </summary>
        /// <returns>地址560的值</returns>
        ResultJsonInfo getExtendedIO(struct_Register address)
        {
            ResultJsonInfo jR = new ResultJsonInfo( );
            //jR = SerialCommunication.ReadConmmand(address, 1);
            //if (jR.Successed)
            //{
            //    string a2 = "";
            //    a2 = Convert.ToString(jR.readBuffer[0], 2).PadLeft(16, '0');
            //    ExtendedIO_560 = a2.ToCharArray();
            //    //输出ushort
            //    //string a162 = new string(ExtendedIO_560);
            //    //ushort new560 = Convert.ToUInt16(a162, 2);
            //}
            return jR;
        }


        /// <summary>
        /// char[]转ushort
        /// </summary>
        /// <param name="charArray"></param>
        /// <returns></returns>
        ushort charArray2Ushort(char[] charArray)
        {
            string a162 = new string(charArray);
            ushort new560 = Convert.ToUInt16(a162, 2);
            return new560;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
                //if(e.RowIndex>-1 && dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()=="1")  //输出
                //{
                //    timer1.Enabled = false;
                //    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[5];
                //    Boolean flag = Convert.ToBoolean(checkCell.Value);
                //    ResultJsonInfo jr = getExtendedIO(RegisterAddress.DistributedModel_TPID);
                //    if(flag)
                //    {
                //        ExtendedIO_560[15 - e.RowIndex] = '0';
                //    }
                //    else
                //        ExtendedIO_560[15 - e.RowIndex] = '1';

                //    ushort new560 = charArray2Ushort(ExtendedIO_560);
                //  Model.ResultJsonInfo  jR = SerialCommunication.sendConmmand(RegisterAddress.DistributedModel_TPID, new560);

                //    timer1.Enabled = true;
                //}
            
        }
    }
}
