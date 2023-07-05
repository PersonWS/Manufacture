
namespace sln_BYSJJ
{
    partial class frmDSVInterlockingConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDSVInterlockingConfig));
            this.txtSN = new System.Windows.Forms.TextBox();
            this.radioButtonSV_lock = new System.Windows.Forms.RadioButton();
            this.radioButtonOffline = new System.Windows.Forms.RadioButton();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxDatabaseName = new System.Windows.Forms.TextBox();
            this.textBoxDB_User = new System.Windows.Forms.TextBox();
            this.textBoxDBPassword = new System.Windows.Forms.TextBox();
            this.comboBoxFunction = new System.Windows.Forms.ComboBox();
            this.comboBoxPassForNoDB = new System.Windows.Forms.ComboBox();
            this.comboBoxShowWindow = new System.Windows.Forms.ComboBox();
            this.comboBoxDebug = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxLineGroup = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSW_User = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxStationId = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.buttonTest2 = new System.Windows.Forms.Button();
            this.rbtnOK = new System.Windows.Forms.RadioButton();
            this.rbtnNG = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelResult = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSN
            // 
            this.txtSN.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSN.Location = new System.Drawing.Point(152, 354);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(286, 29);
            this.txtSN.TabIndex = 36;
            // 
            // radioButtonSV_lock
            // 
            this.radioButtonSV_lock.AutoSize = true;
            this.radioButtonSV_lock.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonSV_lock.Location = new System.Drawing.Point(107, 8);
            this.radioButtonSV_lock.Name = "radioButtonSV_lock";
            this.radioButtonSV_lock.Size = new System.Drawing.Size(83, 24);
            this.radioButtonSV_lock.TabIndex = 34;
            this.radioButtonSV_lock.Text = "互锁模式";
            this.radioButtonSV_lock.UseVisualStyleBackColor = true;
            // 
            // radioButtonOffline
            // 
            this.radioButtonOffline.AutoSize = true;
            this.radioButtonOffline.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonOffline.Location = new System.Drawing.Point(6, 8);
            this.radioButtonOffline.Name = "radioButtonOffline";
            this.radioButtonOffline.Size = new System.Drawing.Size(83, 24);
            this.radioButtonOffline.TabIndex = 35;
            this.radioButtonOffline.Text = "离线模式";
            this.radioButtonOffline.UseVisualStyleBackColor = true;
            // 
            // buttonTest
            // 
            this.buttonTest.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTest.Location = new System.Drawing.Point(477, 366);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(133, 34);
            this.buttonTest.TabIndex = 32;
            this.buttonTest.Text = "是否加工测试";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOK.Location = new System.Drawing.Point(477, 327);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(94, 34);
            this.buttonOK.TabIndex = 33;
            this.buttonOK.Text = "保存";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.textBoxDatabaseName);
            this.groupBox1.Controls.Add(this.textBoxDB_User);
            this.groupBox1.Controls.Add(this.textBoxDBPassword);
            this.groupBox1.Controls.Add(this.comboBoxFunction);
            this.groupBox1.Controls.Add(this.comboBoxPassForNoDB);
            this.groupBox1.Controls.Add(this.comboBoxShowWindow);
            this.groupBox1.Controls.Add(this.comboBoxDebug);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxLineGroup);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxSW_User);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.textBoxStationId);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.textBoxServerName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(9, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(788, 252);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "互锁信息";
            // 
            // textBoxDatabaseName
            // 
            this.textBoxDatabaseName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxDatabaseName.Location = new System.Drawing.Point(128, 96);
            this.textBoxDatabaseName.Name = "textBoxDatabaseName";
            this.textBoxDatabaseName.Size = new System.Drawing.Size(174, 29);
            this.textBoxDatabaseName.TabIndex = 1;
            // 
            // textBoxDB_User
            // 
            this.textBoxDB_User.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxDB_User.Location = new System.Drawing.Point(128, 59);
            this.textBoxDB_User.Name = "textBoxDB_User";
            this.textBoxDB_User.Size = new System.Drawing.Size(174, 29);
            this.textBoxDB_User.TabIndex = 1;
            // 
            // textBoxDBPassword
            // 
            this.textBoxDBPassword.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxDBPassword.Location = new System.Drawing.Point(128, 22);
            this.textBoxDBPassword.Name = "textBoxDBPassword";
            this.textBoxDBPassword.Size = new System.Drawing.Size(174, 29);
            this.textBoxDBPassword.TabIndex = 1;
            // 
            // comboBoxFunction
            // 
            this.comboBoxFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFunction.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.comboBoxFunction.FormattingEnabled = true;
            this.comboBoxFunction.Location = new System.Drawing.Point(432, 170);
            this.comboBoxFunction.Name = "comboBoxFunction";
            this.comboBoxFunction.Size = new System.Drawing.Size(157, 29);
            this.comboBoxFunction.TabIndex = 2;
            // 
            // comboBoxPassForNoDB
            // 
            this.comboBoxPassForNoDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPassForNoDB.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.comboBoxPassForNoDB.FormattingEnabled = true;
            this.comboBoxPassForNoDB.Items.AddRange(new object[] {
            "是",
            "否"});
            this.comboBoxPassForNoDB.Location = new System.Drawing.Point(432, 133);
            this.comboBoxPassForNoDB.Name = "comboBoxPassForNoDB";
            this.comboBoxPassForNoDB.Size = new System.Drawing.Size(157, 29);
            this.comboBoxPassForNoDB.TabIndex = 2;
            // 
            // comboBoxShowWindow
            // 
            this.comboBoxShowWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShowWindow.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.comboBoxShowWindow.FormattingEnabled = true;
            this.comboBoxShowWindow.Items.AddRange(new object[] {
            "是",
            "否"});
            this.comboBoxShowWindow.Location = new System.Drawing.Point(432, 96);
            this.comboBoxShowWindow.Name = "comboBoxShowWindow";
            this.comboBoxShowWindow.Size = new System.Drawing.Size(157, 29);
            this.comboBoxShowWindow.TabIndex = 2;
            // 
            // comboBoxDebug
            // 
            this.comboBoxDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDebug.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.comboBoxDebug.FormattingEnabled = true;
            this.comboBoxDebug.Items.AddRange(new object[] {
            "是",
            "否"});
            this.comboBoxDebug.Location = new System.Drawing.Point(432, 59);
            this.comboBoxDebug.Name = "comboBoxDebug";
            this.comboBoxDebug.Size = new System.Drawing.Size(157, 29);
            this.comboBoxDebug.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(314, 173);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 22);
            this.label10.TabIndex = 0;
            this.label10.Text = "Function";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(314, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 22);
            this.label9.TabIndex = 0;
            this.label9.Text = "PassForNoDB";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(314, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 22);
            this.label8.TabIndex = 0;
            this.label8.Text = "ShowWindow";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(314, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 22);
            this.label7.TabIndex = 0;
            this.label7.Text = "Debug";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxLineGroup
            // 
            this.textBoxLineGroup.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxLineGroup.Location = new System.Drawing.Point(432, 22);
            this.textBoxLineGroup.Name = "textBoxLineGroup";
            this.textBoxLineGroup.Size = new System.Drawing.Size(157, 29);
            this.textBoxLineGroup.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(591, 176);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 17);
            this.label14.TabIndex = 0;
            this.label14.Text = "功能选项";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(591, 132);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(187, 38);
            this.label13.TabIndex = 0;
            this.label13.Text = "输入为 False 时表示没网络时结果为 Fail，否则没网络时为 Pass ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(591, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(157, 36);
            this.label12.TabIndex = 0;
            this.label12.Text = "结果为 Fail 时是否显示 Fail 窗口信息";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(591, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 36);
            this.label11.TabIndex = 0;
            this.label11.Text = "是否进入调试状态，是否允许强制 Pass";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(591, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "线体编号(例如“1” ，不能为空) ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(314, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 22);
            this.label5.TabIndex = 0;
            this.label5.Text = "LineGroup";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxSW_User
            // 
            this.textBoxSW_User.Enabled = false;
            this.textBoxSW_User.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSW_User.Location = new System.Drawing.Point(128, 207);
            this.textBoxSW_User.Name = "textBoxSW_User";
            this.textBoxSW_User.Size = new System.Drawing.Size(174, 29);
            this.textBoxSW_User.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(36, 207);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 29);
            this.label18.TabIndex = 0;
            this.label18.Text = "SW_User";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxStationId
            // 
            this.textBoxStationId.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxStationId.Location = new System.Drawing.Point(128, 170);
            this.textBoxStationId.Name = "textBoxStationId";
            this.textBoxStationId.Size = new System.Drawing.Size(174, 29);
            this.textBoxStationId.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(36, 170);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(92, 29);
            this.label17.TabIndex = 0;
            this.label17.Text = "StationID";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxServerName.Location = new System.Drawing.Point(128, 133);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(174, 29);
            this.textBoxServerName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(36, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 29);
            this.label4.TabIndex = 0;
            this.label4.Text = "服务器名称";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 29);
            this.label3.TabIndex = 0;
            this.label3.Text = "数据库名称";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据库的用户名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库的密码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(30, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 22);
            this.label15.TabIndex = 27;
            this.label15.Text = "工作模式";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(10, 357);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(136, 22);
            this.label16.TabIndex = 28;
            this.label16.Text = "测试产品主SN码";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Gray;
            this.label19.Location = new System.Drawing.Point(655, 324);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 17);
            this.label19.TabIndex = 29;
            this.label19.Text = "(先保存后测试)";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonTest2
            // 
            this.buttonTest2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTest2.Location = new System.Drawing.Point(477, 406);
            this.buttonTest2.Name = "buttonTest2";
            this.buttonTest2.Size = new System.Drawing.Size(133, 34);
            this.buttonTest2.TabIndex = 32;
            this.buttonTest2.Text = "上传结果测试";
            this.buttonTest2.UseVisualStyleBackColor = true;
            this.buttonTest2.Click += new System.EventHandler(this.buttonTest2_Click);
            // 
            // rbtnOK
            // 
            this.rbtnOK.AutoSize = true;
            this.rbtnOK.Checked = true;
            this.rbtnOK.Location = new System.Drawing.Point(26, 7);
            this.rbtnOK.Name = "rbtnOK";
            this.rbtnOK.Size = new System.Drawing.Size(35, 16);
            this.rbtnOK.TabIndex = 37;
            this.rbtnOK.TabStop = true;
            this.rbtnOK.Text = "OK";
            this.rbtnOK.UseVisualStyleBackColor = true;
            // 
            // rbtnNG
            // 
            this.rbtnNG.AutoSize = true;
            this.rbtnNG.Location = new System.Drawing.Point(87, 7);
            this.rbtnNG.Name = "rbtnNG";
            this.rbtnNG.Size = new System.Drawing.Size(35, 16);
            this.rbtnNG.TabIndex = 37;
            this.rbtnNG.Text = "NG";
            this.rbtnNG.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonOffline);
            this.panel1.Controls.Add(this.radioButtonSV_lock);
            this.panel1.Location = new System.Drawing.Point(119, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 40);
            this.panel1.TabIndex = 38;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtnOK);
            this.panel2.Controls.Add(this.rbtnNG);
            this.panel2.Location = new System.Drawing.Point(632, 406);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(138, 32);
            this.panel2.TabIndex = 39;
            // 
            // labelResult
            // 
            this.labelResult.Location = new System.Drawing.Point(630, 366);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(140, 33);
            this.labelResult.TabIndex = 43;
            this.labelResult.Text = "-";
            this.labelResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(315, 219);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(128, 17);
            this.label21.TabIndex = 3;
            this.label21.Text = "登录的用户名禁止修改";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmDSVInterlockingConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.buttonTest2);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label19);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDSVInterlockingConfig";
            this.Text = "互锁模式";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDSVInterlockingConfig_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDSVInterlockingConfig_FormClosed);
            this.Load += new System.EventHandler(this.frmDSVInterlockingConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.RadioButton radioButtonSV_lock;
        private System.Windows.Forms.RadioButton radioButtonOffline;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxDatabaseName;
        private System.Windows.Forms.TextBox textBoxDB_User;
        private System.Windows.Forms.TextBox textBoxDBPassword;
        private System.Windows.Forms.ComboBox comboBoxFunction;
        private System.Windows.Forms.ComboBox comboBoxPassForNoDB;
        private System.Windows.Forms.ComboBox comboBoxShowWindow;
        private System.Windows.Forms.ComboBox comboBoxDebug;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxLineGroup;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSW_User;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxStationId;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonTest2;
        private System.Windows.Forms.RadioButton rbtnOK;
        private System.Windows.Forms.RadioButton rbtnNG;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label label21;
    }
}