using ScrewMachineManagementSystem.Model;
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
    /// <summary>
    /// 电批任务设置
    /// </summary>
    public partial class FormScrewMachineTask : Form
    {
        public FormScrewMachineTask()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 动作模式button数组，0拧紧，1拧松，2自由，3附加项
        /// </summary>
        Button[] button4s = new Button[4];
        /// <summary>
        /// 选中的actionID，0拧紧，1拧松，2自由，3附加项，默认拧紧0
        /// </summary>
        int selectedModeID = 0;
        /// <summary>
        /// 选中的任务id，默认0；
        /// </summary>
        ushort selectedTaskIndex = 0;
        /// <summary>
        /// 开始允许保存datagridview更新的数据，endedit
        /// </summary>
        bool isStartUpdateRTU = false;

        /*DataGridView单元格处于编辑状态触发KeyDown等事件
         * https://blog.csdn.net/wobckr/article/details/77983199
         */

        string str = File.ReadAllText(utility.modbusRtuConfigFile, Encoding.Default);

        // utility.modbusObject = JsonConvert.DeserializeObject<Model.ModbusObject>(str);
        void initButton4()
        {
            button4s[0] = button1;
            button4s[1] = button4;
            button4s[2] = button5;
            button4s[3] = button6;
            button4s[0].BackColor = Color.SkyBlue;
            //button4s[0].BackColor = SystemColors.Control;
            for (int i = 0; i < button4s.Length; i++)
            {
                button4s[i].Tag = i;
                button4s[i].Click += Button4s_Click;
            }
        }
        private void Button4s_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            selectedModeID = Convert.ToInt32(b.Tag);
            setbuttoncolor(selectedModeID);
            switch (selectedModeID)
            {
                case 0://拧紧
                case 1://拧松
                case 2://自由
                    setSteplist();

                    break;
                case 3://附加项
                    break;
            }
        }
        void setbuttoncolor(int index)
        {
            for (int i = 0; i < button4s.Length; i++)
            {
                if (i == index)
                {
                    button4s[i].BackColor = Color.SkyBlue;
                }
                else
                    button4s[i].BackColor = SystemColors.Control;
            }
        }

        private void FormScrewMachineTask_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;

            initButton4();
            utility.CylinderNumberRatios = Controller.CONT_CylinderNumberRatios.LoadList();
            treeView1.LabelEdit = true;
            TreeNode node = new TreeNode();

            node.Name = "全部";
            node.Text = "全部";
            //将数据集加载到树形控件当中
            List<Model.TPTaskInfo> l = Controller.CONT_TPTaskInfo.LoadList("");

            if (l.Count == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    Model.TPTaskInfo tp = new Model.TPTaskInfo();
                    tp.TaskId = i;
                    tp.TaskName = i.ToString();
                    l.Add(tp);
                }
                Controller.CONT_TPTaskInfo.Insert(l);
                l = Controller.CONT_TPTaskInfo.LoadList("");
            }
            foreach (var v in l)
            {

                node = new TreeNode();
                node.Name = v.TaskId.ToString();
                string ss = (string.IsNullOrEmpty(v.TaskName)) ? v.TaskId.ToString().PadLeft(2, '0') : v.TaskName;
                node.Text = v.TaskId.ToString().PadLeft(2, '0') + ":" + ss;
                node.Tag = v.TaskId;
                treeView1.Nodes.Add(node);

            }



            treeView1.ExpandAll();
            treeView1.Focus();
            treeView1.SelectedNode = treeView1.Nodes[0];
            selectedTaskIndex = Convert.ToUInt16(treeView1.Nodes[0].Tag);//任务索引

            //SerialCommunication.setSerial();
            setSteplist();
            //timer1.Enabled = true;
        }








        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panel1, 0, 1, 0, 0);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panel4, 0, 5, 0, 0);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panel2, 0, 0, 0, 0);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            utility.setPanelBorder(e, panel2, 0, 1, 0, 2);
        }



        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)  //单击鼠标左键才响应
            {
                if (e.Node.Level == 0)                               //判断子节点才响应
                {
                    selectedTaskIndex = Convert.ToUInt16(e.Node.Tag);//任务索引
                    setSteplist();                                      //取出


                }
            }
        }

        void setSteplist()
        {
            isStartUpdateRTU = false;
            try
            {
                utility.isTempCommand = true;
                ushort address0 = 0;
                ushort[] registerBuffer = new ushort[0];
                int bufferIndex = 0;
                //选定的selectedModeID，不重复的stepid
                string formatString = "#0.00";
                var newList = utility.screwMachineStepDatas.Where(p => p.modeid == selectedModeID).Select(a => new { a.stepid }).Distinct().ToList();

                var v = utility.screwMachineStepDatas.Where(p => p.modeid == selectedModeID).ToList();
                if (v.Count > 0)
                {
                    address0 = (ushort)(selectedTaskIndex * utility.screwMachineAddressInterval);//已经乘以任务间隔150
                    address0 = (ushort)(v[0].startaddress + address0);
                    if (!(DateTime.Now.Year == 2022 && DateTime.Now.Month == 7 && DateTime.Now.Day == 26))
                    {
                        if (!utility.modbusObjects[0].serialPort.IsOpen)
                            utility.modbusObjects[0].serialPort.Open();

                        registerBuffer = SerialCommunication.master[0].ReadHoldingRegisters((byte)utility.modbusObjects[0].slaveid, address0, (ushort)v.Count);
                        //SerialCommunication.serialPort.Close();
                    }

                }
                int[,] addresses = new int[v.Count, 2];
                int rowindex = 0;

                foreach (var z in v)
                {
                    addresses[rowindex, 0] = z.startaddress;
                    addresses[rowindex, 1] = z.stepid;
                    rowindex++;
                }
                dataGridView1.Rows.Clear();
                foreach (var item in newList)
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = "step" + item.stepid.ToString("D2");

                    //取每个step的第一值
                    int vIndex = item.stepid * 5 + 0;

                    double ratio = getCylinderNumberRatio(addresses, vIndex, ref formatString);

                    string values = (registerBuffer[vIndex] * ratio).ToString(formatString);
                    this.dataGridView1.Rows[index].Cells[1].Value = values;
                    this.dataGridView1.Rows[index].Cells[1].Tag = address0 + vIndex;
                    //取每个step的第二值
                    vIndex = item.stepid * 5 + 1;

                    ratio = 1;// getCylinderNumberRatio(addresses, vIndex, ref formatString);
                    values = (registerBuffer[vIndex] * ratio).ToString(formatString);

                    this.dataGridView1.Rows[index].Cells[2].Value = values;
                    this.dataGridView1.Rows[index].Cells[2].Tag = address0 + vIndex;
                    //读
                }

                //var newList1 = utility.screwmachinetaskdatas.Where(p => p.modeid == selectedModeID && p.isuse > 0).ToList();
                //newList1.Sort((l, r) => l.isuse.CompareTo(r.isuse));

                dataGridView2.Rows.Clear();


                var newList1 = utility.screwmachinetaskdatas.Where(p => p.modeid == selectedModeID).ToList();

                address0 = (ushort)(newList1[0].startaddress + selectedTaskIndex * utility.screwMachineAddressInterval);// v[0].startaddress;
                ushort pointnums = (ushort)newList1.Count();

                if (!utility.modbusObjects[0].serialPort.IsOpen)
                    utility.modbusObjects[0].serialPort.Open();
                registerBuffer = SerialCommunication.master[0].ReadHoldingRegisters((byte)utility.modbusObjects[0].slaveid, address0, pointnums);





                foreach (var item in newList1)
                {
                    ushort baseaddress = (ushort)(address0 + bufferIndex - utility.screwMachineAddressInterval * selectedTaskIndex);


                    double ratio = getCylinderNumberRatio(baseaddress, ref formatString);
                    int index = this.dataGridView2.Rows.Add();

                    this.dataGridView2.Rows[index].Cells[0].Value = item.model;
                    this.dataGridView2.Rows[index].Cells[1].Value = item.name;
                    this.dataGridView2.Rows[index].Cells[2].Tag = address0 + bufferIndex;
                    this.dataGridView2.Rows[index].Cells[2].Value = (ratio * registerBuffer[bufferIndex]).ToString(formatString);
                    //cells[2].Value读取设备，address=utility.screwMachineAddressInterval*selectedTaskIndex+item.startaddress
                    this.dataGridView2.Rows[index].Cells[3].Value = item.scopes;
                    this.dataGridView2.Rows[index].Cells[4].Value = item.units;
                    this.dataGridView2.Rows[index].Cells[5].Value = item.remark;
                    this.dataGridView2.Rows[index].Cells[6].Value = item.Id;

                    bufferIndex++;
                }
                // timer1.Enabled = false;
                isStartUpdateRTU = true;
                utility.isTempCommand = false;
            }
            catch (Exception ex)
            {
                utility.isTempCommand = false;
                isStartUpdateRTU = false;
                if (ex.Source.Equals("System"))
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + ex.Message);


                }

            }
        }
        /// <summary>
        /// 取得系数
        /// </summary>
        /// <param name="z">第一列是address,第二列是stepid</param>
        /// <param name="vIndex"></param>
        /// <returns></returns>
        private static double getCylinderNumberRatio(int[,] z, int vIndex, ref string stringFormat)
        {
            double ratio = 1;  //系数
            int stepid = z[vIndex, 1];
            int address = z[vIndex, 0];

            ushort baseAddress = (ushort)(address - stepid * 5);   //step0对应的地址
            var lratio = utility.CylinderNumberRatios.Where(x => x.baseAddress == baseAddress).ToList();
            stringFormat = "";
            if (lratio.Count == 1)  //有返回表示是有系数
            {
                ratio = lratio[0].ratio;
                stringFormat = "#0.00";
            }
            return ratio;
        }
        private static double getCylinderNumberRatio(ushort baseaddress, ref string stringFormat)
        {
            double ratio = 1;  //系数
            var lratio = utility.CylinderNumberRatios.Where(x => x.baseAddress == baseaddress).ToList();
            stringFormat = "";
            if (lratio.Count == 1)  //有返回表示是有系数数
            {
                ratio = lratio[0].ratio;
                stringFormat = "#0.00";
            }
            return ratio;
        }




        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            labelMsg.Text = "";
            if (!isStartUpdateRTU)
                return;
            switch (e.ColumnIndex)
            {
                case 2:  //更新寄存器

                    DataGridViewCell dgvc = dataGridView2[e.ColumnIndex, e.RowIndex];
                    int a = Convert.ToInt32(dgvc.Tag);

                    ushort address = (ushort)a; //当前单元格地址
                    if (!utility.IsDouble(dgvc.Value.ToString()))
                    {
                        return;
                    }
                    double b = Convert.ToDouble(dgvc.Value);
                    ushort uvalue = 0; //当前单元格数值
                    string stringformat = "";
                    ushort address0 = (ushort)(address - utility.screwMachineAddressInterval * selectedTaskIndex);
                    //double ratio = getCylinderNumberRatio(address, ref stringformat);
                    double ratio = getCylinderNumberRatio(address0, ref stringformat);
                    uvalue = (ushort)(b / ratio);
                    //MessageBox.Show(string.Format("扭力系数：{0}，扭力值：{1}",ratio,uvalue), "更新寄存器");
                    utility.isTempCommand = true;
                    if (!utility.modbusObjects[0].serialPort.IsOpen)
                        utility.modbusObjects[0].serialPort.Open();
                    SerialCommunication.master[0].WriteSingleRegister((byte)utility.modbusObjects[0].slaveid, address, uvalue);
                    utility.isTempCommand = false;
                    dgvc.Value = b.ToString(stringformat);
                    break;
                case 3:  //更新数据库
                    try
                    {
                        int Id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[6].Value);
                        List<Model.Screwmachinetaskdata> newList1 = utility.screwmachinetaskdatas.Where(p => p.Id == Id).ToList();

                        newList1[0].scopes = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                        Controller.CONT_ScrewMachineTaskData.Update(newList1[0]);
                        //toolStripStatusLabel1.Text = "数据保存成功.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("更新配置文件出错" + ex.Message, "系统提示");
                    }
                    break;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                labelMsg.Text = "";
                if (!isStartUpdateRTU)
                    return;
                if (e.ColumnIndex == 0)
                    return;

                DataGridViewCell dgvc = dataGridView1[e.ColumnIndex, e.RowIndex];
                int a = Convert.ToInt32(dgvc.Tag);
                ushort address = (ushort)(a); //当前单元格地址
                int b = Convert.ToInt32(dgvc.Value);
                ushort uvalue = (ushort)(b); //当前单元格数值
                string stringformat = "";
                if (e.ColumnIndex == 1)
                {
                    uvalue = (ushort)(uvalue / 0.01);
                    stringformat = "#.00";
                }
                utility.isTempCommand = true;
                if (!utility.modbusObjects[0].serialPort.IsOpen)
                    utility.modbusObjects[0].serialPort.Open();
                SerialCommunication.master[0].WriteSingleRegister((byte)utility.modbusObjects[0].slaveid, address, uvalue);
                utility.isTempCommand = false;
                dgvc.Value = b.ToString(stringformat);
                labelMsg.Text = "数据更新完成";
            }
            catch (Exception ex)
            {
                utility.ShowMessage("输入错误，" + ex.Message);
            }
        }

        private void buttonDevice2PC_Click(object sender, EventArgs e)
        {
            if (utility.ShowMessageResponse("確定要导出设备参数到文件吗？") != DialogResult.Yes)
                return;
            try
            {
                ushort startaddress = 4000;
                ushort interval = 100;
                ushort lastaddress = 6399;
                ushort timers = (ushort)((lastaddress - startaddress + 1) / 100);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "归档文件另存为";
                sfd.Filter = "电批参数归档文件(*.GDWJ|*.gdwj";
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
                    for (int index = 0; index < timers; index++)
                    {
                        ushort[] registerBuffer = SerialCommunication.master[0].ReadHoldingRegisters((byte)utility.modbusObjects[0].slaveid, (ushort)(startaddress + index * interval), interval);
                        string ssrows = string.Format("{0},{1}", startaddress + index * interval, interval);
                        string values = "";
                        for (int index2 = 0; index2 < registerBuffer.Length; index2++)
                        {
                            ushort value = registerBuffer[index2];
                            if (values == "")
                                values = value.ToString();
                            else
                                values = values + "," + value.ToString();
                        }
                        sw.WriteLine(ssrows);
                        sw.WriteLine(values);
                    }
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                    utility.ShowMessage("导出数据完成！" + Environment.NewLine + filepath);
                }
            }
            catch (Exception ex)
            {
                utility.ShowMessage("导出数据失败！" + Environment.NewLine + ex.Message);
            }
            //读设备数据
            //取任务号
            //int taskId = Convert.ToInt32(treeView1.SelectedNode.Tag);
            ////1任务数据，4000-4050

            //ushort[]  registerBuffer = SerialCommunication.master[0].ReadHoldingRegisters((byte)utility.modbusObjects[0].slaveid, (ushort)(4000+taskId*150), 51);
            //List<Model.screwmachinetasklist> screwmachinetasklists = new List<Model.screwmachinetasklist>();
            //for(int i=0;i< registerBuffer.Length; i++)
            //{
            //    int address = 4000 +  i;
            //    Model.screwmachinetasklist sc=new Model.screwmachinetasklist();
            //    List<Model.Screwmachinetaskdata> sc0 = utility.screwmachinetaskdatas.Where(data => data.startaddress == address).ToList();
            //    if(sc0.Count==1)
            //    {
            //        sc.Id = sc0[0].Id;
            //        sc.Daddress = address+ taskId * 150;
            //        sc.Dvalue=registerBuffer[i];
            //        sc.TaskId = taskId;
            //        sc.UPD=DateTime.Now;
            //        screwmachinetasklists.Add(sc);
            //    }

            //}
            //Controller.CONT_ScrewMachineTaskList.UpdateLists(screwmachinetasklists);
            //2step数据。4060-4149
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            //treeView1.LabelEdit = false;
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', '.', ',', '!' }) == -1)
                    {
                        Model.TPTaskInfo tPTask = new Model.TPTaskInfo();
                        tPTask.TaskId = Convert.ToInt32(e.Node.Tag);
                        tPTask.TaskName = e.Label;
                        //MessageBox.Show(e.Node.Tag.ToString());
                        Controller.CONT_TPTaskInfo.Update(tPTask);
                        e.Node.EndEdit(false);
                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and 
                           place the node in edit mode again. */
                        e.CancelEdit = true;
                        e.Node.BeginEdit();
                        utility.ShowMessage("名称不能包含: '@','.', ',', '!'，修改无效");

                    }
                }
                else
                {
                    /* Cancel the label edit action, inform the user, and 
                       place the node in edit mode again. */
                    e.CancelEdit = true;
                    utility.ShowMessage("任务名称名称不能空");
                    e.Node.BeginEdit();
                }
            }
        }

        private void buttonPC2Device_Click(object sender, EventArgs e)
        {
            if (utility.ShowMessageResponse("確定要想当前设备导入参数吗？") != DialogResult.Yes)
                return;
            //定义一个文件打开控件
            OpenFileDialog ofd = new OpenFileDialog();
            //设置打开对话框的初始目录，默认目录为exe运行文件所在的路径
            ofd.InitialDirectory = Application.StartupPath;
            //设置打开对话框的标题
            ofd.Title = "请选择要打开的参数文件";
            //设置打开对话框可以多选
            ofd.Multiselect = true;
            //设置对话框打开的文件类型
            ofd.Filter = "设备参数文件|*.gdwj";

            //设置对话框是否记忆之前打开的目录
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //获取用户选择的文件完整路径
                    string filePath = ofd.FileName;
                    //获取对话框中所选文件的文件名和扩展名，文件名不包括路径
                    string fileName = ofd.SafeFileName;

                    using (FileStream fsRead = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        StreamReader sr = new StreamReader(fsRead, Encoding.UTF8);
                        string strLine = "";//记录每次读取的一行记录
                        List<string> strAllLine = new List<string>();
                        while ((strLine = sr.ReadLine()) != null)
                        {
                            strAllLine.Add(strLine);

                        }
                        sr.Close();
                        for (int index = 0; index < strAllLine.Count / 2; index++)
                        {
                            string[] linestr = strAllLine[index * 2].Split(',');
                            string[] linevalues = strAllLine[index * 2 + 1].Split(',');
                            int startaddress = Convert.ToInt32(linestr[0]);
                            int buffercount = Convert.ToInt32(linestr[1]);
                            ushort[] buffers = new ushort[buffercount];
                            if (linestr.Length == 2)
                            {
                                if (buffercount == linevalues.Length)
                                {
                                    buffers = Array.ConvertAll(linevalues, ushort.Parse);

                                    SerialCommunication.master[0].WriteMultipleRegistersAsync((byte)utility.modbusObjects[0].slaveid, (ushort)(startaddress), buffers);
                                    System.Threading.Thread.Sleep(50);
                                }
                            }
                        }
                    }
                    utility.ShowMessage("向设备导入参数成功！");
                }
                catch (Exception ex)
                {
                    utility.ShowMessage("向设备导入参数失败！" + ex.Message);
                }
            }
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "单元格不能为空";
                    e.Cancel = true;
                }
                double n = 0;
                //先转化成int类型，尝试转化
                if (!double.TryParse(e.FormattedValue.ToString(), out n))
                {
                    //    //可以的话，再进一步范围判断
                    //    if (n < 1 || n > 100)
                    //    {
                    //        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "必须在1~100之内！";
                    //    }
                    //    else
                    //    {
                    //        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                    //    }
                    //}
                    //else
                    //{
                    utility.ShowMessage("请输入数字！");
                    e.Cancel = true;
                }
            }
        }
    }


}
