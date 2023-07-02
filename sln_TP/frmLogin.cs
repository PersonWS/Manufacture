using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sln_TP.Controller;
using sln_TP.Model;

namespace sln_TP
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            LogHelper.WriteLog("...");
        }
        void login()
        {
            string sn = labelUserID.Text;
            utility.isLogin = false;
            utility.loginUserID = "";
            if (sn.Length == 0 || sn.Length < 3)
            {
                MessageBox.Show("请输入用户ID");
                //utility.ShowMessage("请输入用户ID");
                return;
            }

            string usertype = sn.Substring(0, 2);
            string userid = sn.Substring(2, sn.Length - 2);
            List<UserTable> luser = CONT_UserTable.UserJoinTypeSelect(" userid='" + userid + "' and t.typeid='" + usertype + "'");
            if (luser.Count > 0)
            {
                utility.isLogin = true;
                utility.loginUserID = userid;
                if (usertype == "11")
                    utility.LoginModeEngineer = true;
                else
                    utility.LoginModeEngineer = false;

                LogHelper.WriteLog("登录成功");
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请输入正确的用户ID");
                //utility.ShowMessage("请输入正确的用户ID");
            }
        }

        private void labelSetParams_Click(object sender, EventArgs e)
        {
            login();
        }

        private void labelCancel_Click(object sender, EventArgs e)
        {
            LogHelper.WriteLog("用户取消登录");
            Close();
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    LogHelper.WriteLog("用户取消登录");
                    Close();
                    break;
                case Keys.Enter:
                    login();
                    break;
                case Keys.Back:
                    labelUserID.Text = "";
                    break;
                default:
                    labelUserID.Text = (labelUserID.Text + (char)e.KeyCode).ToLower();

                    break;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Activate();
            SplashScreen.SplashScreen.CloseForm();
        }
    }
}
