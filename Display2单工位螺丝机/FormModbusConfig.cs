using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace ScrewMachineManagementSystem
{
    public partial class FormModbusConfig : Form
    {
        public FormModbusConfig()
        {
            InitializeComponent();
            //定义表头 
            listView1.View = View.Details;
            int w = listView1.Width / 5 - 1;
            
            this.listView1.Columns.Add("序号", w - 30, HorizontalAlignment.Left);
            this.listView1.Columns.Add("参数名称", w + 10, HorizontalAlignment.Left);
            this.listView1.Columns.Add("参数解释", w + 10, HorizontalAlignment.Left);
            this.listView1.Columns.Add("参数值", w + 10, HorizontalAlignment.Left);
            this.listView1.Columns.Add("备注", w + 10, HorizontalAlignment.Left);
        }
        /*
         * https://www.jb51.net/article/204021.htm
         * datagridview
         */

        //https://blog.csdn.net/Hat_man_/article/details/108246877
        [DllImport("user32")]
        public static extern int GetScrollPos(int hwnd, int nBar);
        private TextBox txtInput;
        private void initListview()
        {
            listView1.Items.Clear();
            this.listView1.BeginUpdate();   //与EndUpdate成对使用，挂起UI，绘制控件 
            for (int i = 0; i < utility.paramModeList.Count; i++)   //添加n行数据 
            {
                //index++;
                ListViewItem listViewItem = new ListViewItem(new string[]{
                   
                    utility.paramModeList[i].PID.ToString(),
                    utility.paramModeList[i].ParamName.ToString(),
                    utility.paramModeList[i].Description.ToString(),
                    utility.paramModeList[i].DefaultValue.ToString(),

                    utility.paramModeList[i].Remark.ToString()
                });
                this.listView1.Items.Add(listViewItem);
            }
            this.listView1.EndUpdate();  //
        }



        private void FormModbusConfig_Load(object sender, EventArgs e)
        {
            labelTitle.Text = this.Text;
            try
            {
                initListview();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示");
                LogHelper.WriteLog("读取modbusRTU配置文件错误，" + ex.Message, ex);
            }
        }






        private void labelExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 选中的行
        /// </summary>
        ListViewItem selectedItem;
        /// <summary>
        /// 选中的列
        /// </summary>
        int selectedColumn = 0;
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                selectedItem = this.listView1.GetItemAt(e.X, e.Y);
                //找到文本框
                Rectangle rect = selectedItem.GetBounds(ItemBoundsPortion.Entire);
                int StartX = rect.Left; //获取文本框位置的X坐标
                int ColumnIndex = 0;    //文本框的索引

                //获取列的索引
                //得到滑块的位置
                int pos = GetScrollPos(this.listView1.Handle.ToInt32(), 0);
                foreach (ColumnHeader Column in listView1.Columns)
                {
                    if (e.X + pos >= StartX + Column.Width)
                    {
                        StartX += Column.Width;
                        ColumnIndex += 1;
                    }
                }

                if (ColumnIndex < 3)//前3列，不修改。如果双击为第一列则不可以进入修改
                {
                    return;
                }
                selectedColumn = ColumnIndex;
                Point tmpPoint = listView1.PointToClient(Cursor.Position);
                ListViewItem.ListViewSubItem subitem = listView1.HitTest(tmpPoint).SubItem;
                this.txtInput = new TextBox();

                //locate the txtinput and hide it. txtInput为TextBox
                this.txtInput.Parent = this.listView1;

                //begin edit
                if (selectedItem != null)
                {
                    //nDClick = this.listView1.Items[listView1.SelectedIndices[0]].Index;
                    rect.X = StartX;
                    rect.Width = this.listView1.Columns[ColumnIndex].Width; //得到长度和ListView的列的长度相同                    
                    this.txtInput.Bounds = rect;
                    this.txtInput.Multiline = true;
                    //显示文本框
                    this.txtInput.Text = selectedItem.SubItems[ColumnIndex].Text;
                    this.txtInput.Tag = selectedItem.SubItems[ColumnIndex];
                    this.txtInput.KeyPress += new KeyPressEventHandler(txtInput_KeyPress);
                    this.txtInput.Focus();
                }
            }
            catch (Exception ex)
            {

            }

        }

        //添加回车保存内容
        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == 13)
                {
                    if (this.txtInput != null)
                    {
                        ListViewItem.ListViewSubItem lvst = (ListViewItem.ListViewSubItem)this.txtInput.Tag;
                        ListView.SelectedIndexCollection c = listView1.SelectedIndices;
                        string s = listView1.Items[c[0]].Text;
                        lvst.Text = this.txtInput.Text;

                        if (selectedItem != null)
                        {
                            string paramName = selectedItem.SubItems[1].Text;

                            string nodeName = "";
                            switch (selectedColumn)
                            {
                                case 3:
                                    nodeName = "DefaultValue";
                                    switch(paramName)
                                    {
                                        case "WorkStationCount":
                                        case "TaskCount":
                                        case "TaskIDStartValue":
                                        case "PLC_Port":
                                        case "ScrewMachinePort1":
                                        case "PLC_Rack":
                                        case "PLC_Slot":
                                        case "MCodeLength":
                                            if(!utility.IsNumberic(txtInput.Text))
                                            {
                                                utility.ShowMessage("请输入正确的数值");
                                                initListview();
                                                return;
                                            }
                                            break;
                                        case "":
                                            if (!PDAMaster.CheckUtil.IsIp(txtInput.Text))
                                            {
                                                utility.ShowMessage("请输入正确的数值");
                                                return;
                                            }
                                            break;


                                    }
                                    break;
                                case 4:
                                    nodeName = "Remark";
                                    break;
                            }
                            ModifyXmlInformation(utility._xmlSystemParamFilePath, paramName, nodeName, txtInput.Text);
                        }
                        this.txtInput.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.txtInput != null)
                {
                    if (this.txtInput.Text.Length > 0)
                    {
                        ListViewItem.ListViewSubItem lvst = (ListViewItem.ListViewSubItem)this.txtInput.Tag;

                        //检查PN是否存在
                        int Index = 0;
                        //if (CheckPnIsExist(this.txtInput.Text, ref Index))
                        //{
                        //    this.txtInput.Text = "";
                        //    MessageBox.Show("第" + (Index + 1) + "行已经存在相同PN号，请重新输入!");
                        //}
                        //else
                        //{
                        //    lvst.Text = this.txtInput.Text;
                        //}
                    }

                    this.txtInput.Dispose();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtInput != null)
                {
                    if (this.txtInput.Text.Length > 0)
                    {
                        ListViewItem.ListViewSubItem lvst = (ListViewItem.ListViewSubItem)this.txtInput.Tag;

                        //检查PN是否存在
                        int Index = 0;
                        //if (CheckPnIsExist(this.txtInput.Text, ref Index))
                        //{
                        //    this.txtInput.Text = "";
                        //    MessageBox.Show("第" + (Index + 1) + "行已经存在相同PN号，请重新输入!");
                        //}
                        //else
                        //{
                        //    lvst.Text = this.txtInput.Text;
                        //}

                    }

                    this.txtInput.Dispose();
                }
            }
            catch (Exception ex)
            {

            }

        }
        bool ModifyXmlInformation(string xmlFilePath, string paramName, string nodeName, string nodeValue)
        {
            try
            {
                XmlDocument myXmlDoc = new XmlDocument();
                
                myXmlDoc.Load(xmlFilePath);
                string xPath = string.Format("/paramlist/param[@ParamName=\"{0}\"]", paramName);
                XmlNode xn = myXmlDoc.SelectSingleNode(xPath);
                XmlElement xe = (XmlElement)xn;
                xe.GetElementsByTagName(nodeName).Item(0).InnerText = nodeValue;
                myXmlDoc.Save(xmlFilePath);
                utility.paramModeList.Clear();
                utility.getParamList();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /* 更新参数
          string utility._xmlSystemParamFilePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                if (!File.Exists(utility._xmlSystemParamFilePath + "\\systemParam.xml"))
                {
                    //logger.Fatal("参数文件xmlparam.xml丢失！");
                    utility.ShowMessage("参数文件systemParam.xml丢失！");
                    Application.Exit();
                }
                string filename = utility._xmlSystemParamFilePath + "\\systemParam.xml";
                bool b = Configuration.ModifyXmlInformation(filename, "WorkStationCount", "DefaultValue", "100");
                if(!b)
                {
                    utility.ShowMessage("更新参数失败");
                }
         */


    }
}
