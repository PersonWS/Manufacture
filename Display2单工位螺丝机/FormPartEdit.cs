using PDAMaster;
using ScrewMachineManagementSystem.Model;
using System;
using System.Threading;
using System.Windows.Forms;


namespace ScrewMachineManagementSystem
{
    public partial class FormPartEdit : Form
    {
        public FormPartEdit()
        {
            InitializeComponent();
        }
        int DBBlock = 51;
        int[] partAddress = new int[] { 778, 786, 794, 802, 810, 818, 826, 834, 842, 850 };
        RadioButton[] labelpart = new RadioButton[10];
        TextBox[] textsPart = new TextBox[10];

        void initControl()
        {
            labelpart[0] = radioButton1;
            labelpart[1] = radioButton2;
            labelpart[2] = radioButton3;
            labelpart[3] = radioButton4;
            labelpart[4] = radioButton5;
            labelpart[5] = radioButton6;
            labelpart[6] = radioButton7;
            labelpart[7] = radioButton8;
            labelpart[8] = radioButton9;
            labelpart[9] = radioButton10;
            textsPart[0] = textBox1;
            textsPart[1] = textBox2;
            textsPart[2] = textBox3;
            textsPart[3] = textBox4;
            textsPart[4] = textBox5;
            textsPart[5] = textBox6;
            textsPart[6] = textBox7;
            textsPart[7] = textBox8;
            textsPart[8] = textBox9;
            textsPart[9] = textBox10;

            for (int i = 0; i < labelpart.Length; i++)
            {
                labelpart[i].Text = "Part_" + i.ToString().PadLeft(2, '0');
                labelpart[i].CheckedChanged += RadioButton_CheckedChanged;
                labelpart[i].Tag = i;

            }
        }

        int selectedIndex = 0;
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (r.Checked)
            {
                selectedIndex = Convert.ToInt32(r.Tag);
                for (int i = 0; i < labelpart.Length; i++)
                {
                    textsPart[i].Enabled = false;
                }
                textsPart[selectedIndex].Enabled = true;
            }
        }

        string[] parts = new string[10] { null, null, null, null, null, null, null, null, null, null };

        /// <summary>
        /// 刷新产品识别码
        /// </summary>
        void RefreshTPID()
        {
            parts = new string[10];
            Model.ResultJsonInfo jr = S7NetPlus.ReadOneString(51, 778);
            parts[0] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 786);
            parts[1] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 794);
            parts[2] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 802);
            parts[3] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 810);
            parts[4] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 818);
            parts[5] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 826);
            parts[6] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 834);
            parts[7] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 842);
            parts[8] = jr.stringValue;
            jr = S7NetPlus.ReadOneString(51, 850);
            parts[9] = jr.stringValue;
            for (int i = 0; i < parts.Length; i++)
            {
                string s = "Part_" + (1 + i).ToString().PadLeft(2, '0');

                labelpart[i].Text = s;
                textsPart[i].Text = parts[i];
                textsPart[i].Tag = i;
                textsPart[i].TextChanged += textboxPartEdit_TextChanged;

            }
        }

        private void textboxPartEdit_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = labelpart[selectedIndex].Text + "正在修改，完成后请点击更新！";
        }

        private void FormPartEdit_Load(object sender, EventArgs e)
        {
            initControl();
            RefreshTPID();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int tpid = selectedIndex + ConfigurationKeys.TaskIDStartValue;
            Model.ResultJsonInfo jr = new ResultJsonInfo();


            //1 读取加工程序编号        DB15.DBX12.0  （1-10个编号）	int
            jr = S7NetPlus.WriteDataInt(15, 12, (short)tpid);
            if (!jr.Successed)
            {
                utility.ShowMessage(string.Format("写入配方编号失败，DB{0} DBX{1}.{2}", 15, 12, 0));
                return;
            }
            Thread.Sleep(200);

            //2
            jr = S7NetPlus.WriteDataBool(8, 260, true, 0);
            Thread.Sleep(500);

            jr = S7NetPlus.WriteDataBool(8, 260, false, 0);

            if (jr.Successed)
            {
                Thread.Sleep(200);
                jr = S7NetPlus.ReadOneInt(15, 12);
                if (!jr.Successed)
                {
                    utility.ShowMessage("配方编号读取失败，请重试");
                    return;
                }
                if (tpid != jr.intValue)
                {
                    utility.ShowMessage(string.Format("配方编号写入失败，DB{0} DBX{1}.{2}", 15, 12, 0));
                    return;
                }
                FormPartOneEdit f = new FormPartOneEdit(selectedIndex);
                f.ShowDialog();
            }
            else
            {
                utility.ShowMessage(string.Format("写入读取按钮失败，DB{0} DBX{1}.{2}", 8, 260, 0));
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult d = utility.ShowMessageResponse("确定要更新所有修改过的工件识别码？");
            if (d != DialogResult.Yes)
            {
                return;
            }
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                string s = textsPart[i].Text;
                if (string.IsNullOrEmpty(s))
                {
                    utility.ShowMessage("工件识别码不能为空！");
                    textsPart[i].Focus();
                    return;
                }
                if (part != s)
                {
                    Model.ResultJsonInfo jr = S7NetPlus.WriteDataString(DBBlock, partAddress[i], s);
                    if (!jr.Successed)
                    {
                        utility.ShowMessage(labelpart[i].Text + "数值写入失败，" + jr.Message);
                        return;
                    }
                    else
                    {
                        parts[i] = s;
                    }
                }
            }

            toolStripStatusLabel1.Text = "数值写入成功";
        }
    }
}
