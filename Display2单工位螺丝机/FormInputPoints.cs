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
    public partial class FormInputPoints : Form
    {
        public FormInputPoints()
        {
            InitializeComponent();
            S7NetPlus.readOutPoint = true;
        }

        int[] pointsInfo = new int[4];
        private void FormInputPoints_Load(object sender, EventArgs e)
        {
            //定义表头
            pointsInfo = S7NetPlus.inputPointsInfo;

            int width = this.Width / 3;    //设置列宽度 width

            listView1.Columns.Add("节点名称", width, HorizontalAlignment.Center);
            listView1.Columns.Add("节点地址", width, HorizontalAlignment.Center);
            listView1.Columns.Add("状态", width, HorizontalAlignment.Center);
            listView1.GridLines = true;
            refreshListView();
        }


        void refreshListView()
        {
            listView1.Items.Clear();
            this.listView1.BeginUpdate();   //与EndUpdate成对使用，挂起UI，绘制控件
            if (radioButton1.Checked)
            {
                pointsInfo = S7NetPlus.inputPointsInfo;
                foreach (S7NetPlus.inputDiagitList s in Enum.GetValues(typeof(S7NetPlus.inputDiagitList)))
                {
                    ListViewItem listViewItem = new ListViewItem();

                    listViewItem.Text = s.ToString();  //第一列
                    int index = Convert.ToInt32(s);
                    listViewItem.Tag = index;
                    int startaddr = pointsInfo[1] + index / 8;
                    int bitAddr = index % 8;
                    listViewItem.SubItems.Add(String.Format("DB{0}DBX{1}.{2}", pointsInfo[0], startaddr, bitAddr));
                    listViewItem.SubItems.Add("⚪");
                    listViewItem.UseItemStyleForSubItems = false;
                    this.listView1.Items.Add(listViewItem);
                }
            }
            else if (radioButton2.Checked)
            {
                pointsInfo = S7NetPlus.outPointsInfo;
                foreach (S7NetPlus.outPointsList s in Enum.GetValues(typeof(S7NetPlus.outPointsList)))
                {
                    ListViewItem listViewItem = new ListViewItem();

                    listViewItem.Text = s.ToString();  //第一列
                    int index = Convert.ToInt32(s);
                    listViewItem.Tag = index;
                    int startaddr = pointsInfo[1] + index / 8;
                    int bitAddr = index % 8;
                    listViewItem.SubItems.Add(String.Format("DB{0}DBX{1}.{2}", pointsInfo[0], startaddr, bitAddr));
                    listViewItem.SubItems.Add("⚪");
                    listViewItem.UseItemStyleForSubItems = false;
                    this.listView1.Items.Add(listViewItem);
                }
            }
            else if (radioButton3.Checked)
            {
                
                foreach (Model.AlarmPoints s in S7NetPlus.alarmPoints )
                {
                    ListViewItem listViewItem = new ListViewItem();

                    listViewItem.Text = s.AlarmInfo.ToString();  //第一列
                    listViewItem.Tag = s.Id;
                    listViewItem.SubItems.Add(String.Format("DB{0}DBX{1}.{2}", s.DisplayInfo, s.Address, s.Bits));
                    listViewItem.SubItems.Add("⚪");
                    listViewItem.UseItemStyleForSubItems = false;
                    this.listView1.Items.Add(listViewItem);
                }
            }

            this.listView1.EndUpdate();  //
            if (listView1.Items.Count > 0)
                timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (radioButton1.Checked)
            {
                for (int i = 0; i < S7NetPlus.inputDiagitStatus.Length; i++)
                {
                    if (S7NetPlus.inputDiagitStatus[i])
                        listView1.Items[i].SubItems[2].ForeColor = Color.Red;
                    else
                        listView1.Items[i].SubItems[2].ForeColor = Color.Gray;
                }
            }
            else if (radioButton2.Checked)
            {
                for (int i = 0; i < S7NetPlus.outDiagit.Length; i++)
                {
                    if (S7NetPlus.outDiagit[i])
                        listView1.Items[i].SubItems[2].ForeColor = Color.Red;
                    else
                        listView1.Items[i].SubItems[2].ForeColor = Color.Gray;
                }
            }
            else if (radioButton3.Checked)
            {
                for (int i = 0; i < S7NetPlus.alarmPoints.Count; i++)
                {
                    if (S7NetPlus.alarmPoints[i].realStatue)
                        listView1.Items[i].SubItems[2].ForeColor = Color.Red;
                    else
                        listView1.Items[i].SubItems[2].ForeColor = Color.Gray;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            refreshListView();
        }

        private void FormInputPoints_FormClosed(object sender, FormClosedEventArgs e)
        {
            S7NetPlus.readOutPoint = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            refreshListView();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            refreshListView();
        }
    }
}
