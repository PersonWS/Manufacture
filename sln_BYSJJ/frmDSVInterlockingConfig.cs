using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using static sln_BYSJJ.SV_Interlocking;
using sln_BYSJJ.Model;
using sln_BYSJJ.Controller;
namespace sln_BYSJJ
{
    public partial class frmDSVInterlockingConfig : Form
    {
        public frmDSVInterlockingConfig()
        {
            InitializeComponent();
        }

        private void frmDSVInterlockingConfig_Load(object sender, EventArgs e)
        {
            foreach (SV_InterlockingFuncCode o in Enum.GetValues(typeof(SV_InterlockingFuncCode)))
            {
                comboBoxFunction.Items.Add(o.ToString());
            }
            radioButtonOffline.Checked = utility.dSV.workMode ? false : true;
            radioButtonSV_lock.Checked = utility.dSV.workMode ? true : false;
            textBoxDBPassword.Text = utility.dSV.DB_Password;
            textBoxDB_User.Text = utility.dSV.DB_User;
            textBoxDatabaseName.Text = utility.dSV.DatabaseName;
            textBoxServerName.Text = utility.dSV.ServerName;
            textBoxLineGroup.Text = utility.dSV.LineGroup;
            textBoxStationId.Text = utility.dSV.stationId;
            //textBoxSW_User.Text = utility.dSV.SW_User;
            textBoxSW_User.Text = utility.loginUserID;
            comboBoxDebug.Text = utility.dSV.Debug == true ? "是" : "否";
            comboBoxShowWindow.Text = utility.dSV.ShowWindow == true ? "是" : "否";
            comboBoxPassForNoDB.Text = utility.dSV.PassForNoDB == true ? "是" : "否";
            comboBoxFunction.Text = ((SV_InterlockingFuncCode)(utility.dSV.Function)).ToString();
        }

        private void frmDSVInterlockingConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }
        private void frmDSVInterlockingConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        public static int GetEnumValue(Type enumType, string enumName)
        {
            try
            {
                if (!enumType.IsEnum)
                    throw new ArgumentException("enumType必须是枚举类型");
                var values = Enum.GetValues(enumType);
                var ht = new Hashtable();
                foreach (var val in values)
                {
                    ht.Add(Enum.GetName(enumType, val), val);
                }
                return (int)ht[enumName];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string dbPassword = textBoxDBPassword.Text;
            string dbUser = textBoxDB_User.Text;
            string dbName = textBoxDatabaseName.Text;
            string serverName = textBoxServerName.Text;
            string lineGroup = textBoxLineGroup.Text;
            string stationid = textBoxStationId.Text;
            //string sw_user = textBoxSW_User.Text;
            string sw_user = utility.loginUserID;
            bool debug = comboBoxDebug.Text == "是" ? true : false;
            bool showWindow = comboBoxShowWindow.Text == "是" ? true : false;
            bool passForNoDB = comboBoxPassForNoDB.Text == "是" ? true : false;
            int function = GetEnumValue(typeof(SV_InterlockingFuncCode), comboBoxFunction.Text);

            if (string.IsNullOrEmpty(dbPassword))
            {
                textBoxDBPassword.Focus();
                MessageBox.Show("请输入登录数据库密码");
                //utility.ShowMessage("请输入登录数据库密码");
                return;
            }
            if (string.IsNullOrEmpty(dbUser))
            {
                textBoxDB_User.Focus();
                MessageBox.Show("请输入登录数据库的用户名");
                //utility.ShowMessage("请输入登录数据库的用户名");
                return;
            }
            if (string.IsNullOrEmpty(dbName))
            {
                textBoxDatabaseName.Focus();
                MessageBox.Show("请输入登录的数据库名称");
                //utility.ShowMessage("请输入登录的数据库名称");
                return;
            }

            if (string.IsNullOrEmpty(serverName))
            {
                textBoxServerName.Focus();
                MessageBox.Show("请输入数据库的服务器名称");
                //utility.ShowMessage("请输入数据库的服务器名称");
                return;
            }
            if (string.IsNullOrEmpty(stationid))
            {
                textBoxServerName.Focus();
                MessageBox.Show("请输入stationid名称");
                //utility.ShowMessage("请输入stationid名称");
                return;
            }
            if (string.IsNullOrEmpty(sw_user))
            {
                textBoxServerName.Focus();
                MessageBox.Show("请输入sw_user名称");
                //utility.ShowMessage("请输入sw_user名称");
                return;
            }
            if (string.IsNullOrEmpty(lineGroup))
            {
                textBoxLineGroup.Focus();
                MessageBox.Show("请输入线体编号");
                //utility.ShowMessage("请输入线体编号");
                return;
            }


            utility.dSV.DB_Password = dbPassword;
            utility.dSV.DB_User = dbUser;
            utility.dSV.DatabaseName = dbName;
            utility.dSV.ServerName = serverName;
            utility.dSV.LineGroup = lineGroup;
            utility.dSV.stationId = stationid;
            utility.dSV.SW_User = sw_user;
            utility.dSV.Debug = debug;
            utility.dSV.ShowWindow = showWindow;
            utility.dSV.PassForNoDB = passForNoDB;
            utility.dSV.Function = function;
            utility.dSV.OID = utility.loginUserID;
            utility.dSV.CID = utility.stationId;
            utility.dSV.workMode = radioButtonOffline.Checked ? false : true;
            if (Controller.CONT_DSVInterlockingInfo.Update(utility.dSV) > 0)
            {
                MessageBox.Show("互锁信息保存完成");
                //utility.ShowMessage("互锁信息保存完成");
            }
            else
            {
                MessageBox.Show("互锁信息保存失败，请检查");
                //utility.ShowMessage("互锁信息保存失败，请检查");
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            long s = SV_Interlocking.sv_lockInfo(txtSN.Text);
            if (s == 0)
            {
                labelResult.Text = "允许加工";
            }
            else if (s == -1)
            {
                labelResult.Text = "禁止加工";
            }
            else
            {
                MessageBox.Show("互锁验证失败！");
                //utility.ShowMessage("互锁验证失败！");
                return;
            }
        }

        private void buttonTest2_Click(object sender, EventArgs e)
        {
            if(rbtnOK.Checked)
            {
                updateMes(true);
            }
            else if(rbtnNG.Checked)
            {
                updateMes(false);
            }
        }

        private void updateMes(bool isok)
        {
            string logtitle = "";
            try
            {
                UUT_RESULT uut_result = new UUT_RESULT
                {
                    STATION_ID = utility.dSV.stationId,
                    UUT_SERIAL_NUMBER = txtSN.Text,
                    USER_LOGIN_NAME = utility.dSV.SW_User,
                    START_DATE_TIME = DateTime.Now,
                    UUT_STATUS = isok ? "Passed" : "Failed"
                };
                CONT_UUT_RESULT.InsertMesData(uut_result);
                logtitle = "上传mes成功" + (isok ? "OK,产品主SN码:" : "NG,产品主SN码:") + txtSN.Text;
                MessageBox.Show(logtitle);
            }
            catch (Exception exception)
            {
                logtitle = "上传mes失败," + exception.Message + (isok ? "OK,产品主SN码:" : "NG,产品主SN码:") + txtSN.Text;
                MessageBox.Show(logtitle);
            }
            finally
            {
                //this.writeLog(LogType.Error, logtitle);
                //this.FillInfoLog(logtitle);
            }
        }

    }
}
