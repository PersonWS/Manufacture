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
    public partial class FormUserEdit : Form
    {
        public FormUserEdit(string id)
        {
            InitializeComponent();
            _id = id;
        }
        string _id = string.Empty;
        private void FormUserEdit_Load(object sender, EventArgs e)
        {
            Dictionary<string,string> lt = CONT_UserTable.DictionaryUserType();
            foreach (var v in lt)
            {
                comboBox1.Items.Add(v.Key + "-" + v.Value);
            }
            if (string.IsNullOrEmpty(_id))
                groupBox1.Text = "新增";
            else
            {
                groupBox1.Text = "编辑";
                List<UserTable> l = CONT_UserTable.UserJoinTypeSelect(" u.id='" + _id + "'");
                if (l.Count > 0)
                {
                    textBoxUserID.Text = l[0].userID;
                    textBoxUserName.Text = l[0].userName;

                    if (l[0].UserTypes.TypeID != null)
                    {
                        foreach(var v in comboBox1.Items)
                        {
                            int index = v.ToString().IndexOf(l[0].UserTypes.TypeID);
                            if (index > -1)
                            {
                                comboBox1.SelectedIndex = index;
                                break;
                            }
                        }
                        
                        
                    }

                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            UserTable userTable = new UserTable();
            userTable.Id =Convert.ToInt32( _id);
            userTable.userID = textBoxUserID.Text; ;
            userTable.userName = textBoxUserName.Text;
           // userTable.flag = Convert.ToInt32(checkBoxFlag.Checked);
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("请选择用户类型", "系统提示");
                return;
            }
            userTable.TypeID = comboBox1.Text.Split('-')[0];

            if (string.IsNullOrEmpty(_id))
            {
                //new
                CONT_UserTable.Save(userTable);
            }
            else
            {
                CONT_UserTable.Update(userTable, _id);
            }
            this.DialogResult = DialogResult.OK;
            //Close();
        }
    }
}
