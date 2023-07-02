using System;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormShieldData : Form
    {
        public FormShieldData()
        {
            InitializeComponent();
        }
        
        private void FormShieldData_Load(object sender, EventArgs e)
        {
            init();
            Model.ResultJsonInfo jr = S7NetPlus.ReadMuliBools(S7NetPlus.ShieldDatasPointsInfo[0],
                S7NetPlus.ShieldDatasPointsInfo[1], S7NetPlus.ShieldDatasPointsInfo[2]);

            for (int i = 0; i < S7NetPlus.ShieldDatas.Length; i++)
            {
                string[] ss = S7NetPlus.ShieldDatas[i].Split(';');
                labellist[i].Text = ss[0];

                if (jr.Successed)
                {
                    if (jr.booValue[i])
                        ComboBoxlist[i].SelectedIndex = 1;
                    else
                        ComboBoxlist[i].SelectedIndex = 0;
                }
                else
                {
                    ComboBoxlist[i].SelectedIndex = 2;
                }
            }

        }
        Label[] labellist = new Label[9];
        ComboBox[] ComboBoxlist = new ComboBox[9];
        void init()
        {
            labellist[0] = label1;
            labellist[1] = label2;
            labellist[2] = label3;
            labellist[3] = label4;
            labellist[4] = label5;
            labellist[5] = label6;
            labellist[6] = label7;
            labellist[7] = label8;
            labellist[8] = label9;
            ComboBoxlist[0] = comboBox1;
            ComboBoxlist[1] = comboBox2;
            ComboBoxlist[2] = comboBox3;
            ComboBoxlist[3] = comboBox4;
            ComboBoxlist[4] = comboBox5;
            ComboBoxlist[5] = comboBox6;
            ComboBoxlist[6] = comboBox7;
            ComboBoxlist[7] = comboBox8;
            ComboBoxlist[8] = comboBox9;
            for (int i = 0; i < ComboBoxlist.Length; i++)
            {
                ComboBoxlist[i].Items.Clear();
                ComboBoxlist[i].Tag = i;
                ComboBoxlist[i].Items.Add("取消屏蔽");
                ComboBoxlist[i].Items.Add("开启屏蔽");
                ComboBoxlist[i].Items.Add("未读取");
                ComboBoxlist[i].SelectedIndexChanged += ComboBoxlist_SelectedIndexChanged;
            }


        }

        private void ComboBoxlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int index = Convert.ToInt32(c.Tag);
            try
            {

                bool b = index == 1 ? true : false;
                string[] ss = S7NetPlus.ShieldDatas[index].Split(';');
                int db = Convert.ToInt32(ss[1]);
                int startaddr = Convert.ToInt32(ss[2]);
                int bitaddr = Convert.ToInt32(ss[3]);
                S7NetPlus.WriteDataBool(db, startaddr, b, bitaddr);
            }
            catch (Exception ex)
            {
                utility.ShowMessage(labellist[index].Text + ComboBoxlist[index].Text + "失败！");
            }
        }


    }
}
