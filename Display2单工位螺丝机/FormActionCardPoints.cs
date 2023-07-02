using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormActionCardPoints : Form
    {
        public FormActionCardPoints()
        {
            InitializeComponent();
        }

        private void FormActionCardPoints_Load(object sender, EventArgs e)
        {
            int width = this.Width / 4;    //设置列宽度 width

            listView1.Columns.Add("节点名称", width , HorizontalAlignment.Center);
            listView1.Columns.Add("节点类型", width , HorizontalAlignment.Center);
            listView1.Columns.Add("节点地址", width , HorizontalAlignment.Center);
            listView1.Columns.Add("状态/值", width , HorizontalAlignment.Center);
            listView1.GridLines = true;
            refreshListView();
        }
        void refreshListView()
        {
            this.listView1.BeginUpdate();   //与EndUpdate成对使用，挂起UI，绘制控件

            for (int i = 0; i < S7NetPlus.ActionCardPoints.Length; i++)
            {
                ListViewItem listViewItem = new ListViewItem();
                string[] points = S7NetPlus.ActionCardPoints[i].Split(',');
                listViewItem.Text = points[0];  //第一列


                int db = Convert.ToInt32(points[2]);
                int startaddr = Convert.ToInt32(points[3]);
                int bitAddr = Convert.ToInt32(points[4]);
                listViewItem.SubItems.Add(points[1]);
                listViewItem.SubItems.Add(String.Format("DB{0}DBX{1}.{2}", db, startaddr, bitAddr));
                string s = "";
                switch (points[1])
                {
                    case "Bool":
                        s = "⚪";
                        break;
                    case "Int":
                        s = "0";
                        break;
                    case "Word":
                        s = "";
                        break;
                    case "Real":
                        s = "0.0";
                        break;
                }
                listViewItem.SubItems.Add(s);
                listViewItem.UseItemStyleForSubItems = false;
                this.listView1.Items.Add(listViewItem);
            }
            this.listView1.EndUpdate();  //
            if (listView1.Items.Count > 0)
                timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                string[] points = S7NetPlus.ActionCardPoints[i].Split(',');
                int db = Convert.ToInt32(points[2]);
                int startaddr = Convert.ToInt32(points[3]);
                int bitAddr = Convert.ToInt32(points[4]);
                Model.ResultJsonInfo jr = new Model.ResultJsonInfo();
                switch (points[1])
                {
                    case "Bool":
                        jr = S7NetPlus.ReadOneBool(db, startaddr, (byte)bitAddr);
                        if (jr.Successed)
                        {
                            if (jr.booValue[0])
                                listView1.Items[i].SubItems[3].ForeColor = Color.Red;
                            else
                                listView1.Items[i].SubItems[3].ForeColor = Color.Gray;
                        }
                        break;
                    case "Int":
                        jr = S7NetPlus.ReadOneInt(db, startaddr, (byte)bitAddr);
                        if (jr.Successed)
                        {
                            listView1.Items[i].SubItems[3].Text = jr.intValue.ToString();
                        }
                        else
                        {
                            listView1.Items[i].SubItems[3].Text = "读取失败";
                        }

                        break;
                    case "Word":
                        jr = S7NetPlus.ReadOneString(db, startaddr, (byte)bitAddr);
                        if (jr.Successed)
                        {
                            listView1.Items[i].SubItems[3].Text =jr.stringValue;
                        }
                        else
                        {
                            listView1.Items[i].SubItems[3].Text = "读取失败";
                        }
                        break;
                    case "Real":
                        jr = S7NetPlus.ReadOneReal(db, startaddr, (byte)bitAddr);
                        if (jr.Successed)
                        {
                            listView1.Items[i].SubItems[3].Text = jr.doubleValue.ToString();
                        }
                        else
                        {
                            listView1.Items[i].SubItems[3].Text = "读取失败";
                        }
                        break;
                }
                Thread.Sleep(50);
            }
        }
    }
}
