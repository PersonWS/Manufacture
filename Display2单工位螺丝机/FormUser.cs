using ScrewMachineManagementSystem.Controller;
using ScrewMachineManagementSystem.Model;
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
    public partial class FormUser : Form
    {
        public FormUser()
        {
            InitializeComponent();
        }

        void initListview()
        {

            listView1.Items.Clear();

            List<UserTable> l = CONT_UserTable.UserJoinTypeSelect(" u.flag>0");
            Dictionary<string, string> myDic = CONT_UserTable.DictionaryUserType();
            foreach (var l1 in l)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = l1.userID;  //第一列
                listViewItem.SubItems.Add(l1.userName);
                //string sFlag = "启用";
                //if(l1.flag==0)
                //    sFlag = "禁用";
                //listViewItem.SubItems.Add(sFlag);
                
                
                listViewItem.SubItems.Add(l1.UserTypes.TypeName);
                
                listViewItem.SubItems.Add(l1.permissiones);
                listViewItem.SubItems.Add(l1.Id.ToString());
                this.listView1.Items.Add(listViewItem);
            }
        }
        
        string _id = string.Empty;
        private void FormUser_Load(object sender, EventArgs e)
        {
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            int w = this.Width / 5 ;

            ColumnHeader columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "用户编号";
            columnHeader1.TextAlign = HorizontalAlignment.Center;
            columnHeader1.Width = w/2;

            ColumnHeader columnHeader2 = new ColumnHeader();
            columnHeader2.Text = "姓名";
            columnHeader2.TextAlign = HorizontalAlignment.Center;
            columnHeader2.Width = w/2;

            ColumnHeader columnHeader4 = new ColumnHeader();
            columnHeader4.Text = "类型";
            columnHeader4.TextAlign = HorizontalAlignment.Center;
            columnHeader4.Width = w / 2;

            //ColumnHeader columnHeader5 = new ColumnHeader();
            //columnHeader5.Text = "备注";
            //columnHeader5.TextAlign = HorizontalAlignment.Center;
            //columnHeader5.Width = w;
            
            ColumnHeader columnHeader6 = new ColumnHeader();
            columnHeader6.Text = "权限";
            columnHeader6.TextAlign = HorizontalAlignment.Center;
            columnHeader6.Width = w*2;

            ColumnHeader columnHeader7 = new ColumnHeader();
            columnHeader7.Text = "Id";
            columnHeader7.TextAlign = HorizontalAlignment.Center;
            columnHeader7.Width = 0;

            this.listView1.Columns.Add(columnHeader1);
            this.listView1.Columns.Add(columnHeader2);
            
            this.listView1.Columns.Add(columnHeader4);
            //this.listView1.Columns.Add(columnHeader5);
            this.listView1.Columns.Add(columnHeader6);
            this.listView1.Columns.Add(columnHeader7);
            initListview();
        }

        private void labelAdd_Click(object sender, EventArgs e)
        {
            FormUserEdit f = new FormUserEdit(null);
           if( f.ShowDialog()==DialogResult.OK)
            {
                initListview();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                _id = string.Empty;
                labelEdit.Enabled = false;
                return;
            }
            else
            {
                _id = listView1.SelectedItems[0].SubItems[4].Text;
                labelEdit.Enabled = true;
                //string type = listView1.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void labelEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_id))
            {
                FormUserEdit f = new FormUserEdit(_id);
                if (f.ShowDialog() == DialogResult.OK)
                {
                   
                    initListview();
                }

            }
        }

        private void labelDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_id))
            {
                if (utility.ShowMessageResponse("确定要删除选中的用户吗？") == DialogResult.Yes)
                {
                    Model.UserTable u = new UserTable();
                    u.Id =Convert.ToInt32( _id);
                    Controller.CONT_UserTable.Delete(u);
                    initListview();
                }
            }
        }
    }
}
