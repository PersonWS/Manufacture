
namespace ScrewMachineManagementSystem
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timerScanSN = new System.Windows.Forms.Timer(this.components);
            this.timerRTU = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lab_ok = new System.Windows.Forms.Label();
            this.lab_ng = new System.Windows.Forms.Label();
            this.lab_isManufacture = new System.Windows.Forms.Label();
            this.lab_snRequest = new System.Windows.Forms.Label();
            this.lab_screwClear_plc = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lab_manufactureDeny = new System.Windows.Forms.Label();
            this.lab_interlock = new System.Windows.Forms.Label();
            this.lab_manufacturePermission = new System.Windows.Forms.Label();
            this.lab_snWrite = new System.Windows.Forms.Label();
            this.lab_manufactureResultRecept = new System.Windows.Forms.Label();
            this.lab_snWrite_apply = new System.Windows.Forms.Label();
            this.lab_manufacturePermission_apply = new System.Windows.Forms.Label();
            this.lab_manufactureDeny_apply = new System.Windows.Forms.Label();
            this.lab_interlock_apply = new System.Windows.Forms.Label();
            this.lab_manufactureResultRecept_apply = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_ScrewClearOK = new System.Windows.Forms.Label();
            this.lab_ScrewClearOK_apply = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelRunMode = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.listBoxInfoLog = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txt_plcSN = new System.Windows.Forms.TextBox();
            this.txt_scannerSN = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelSystemName = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelHostName = new System.Windows.Forms.Label();
            this.labelUserID = new System.Windows.Forms.Label();
            this.comboBoxLineMode = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelExit = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelHomeStaus = new System.Windows.Forms.Label();
            this.label1StartTask = new System.Windows.Forms.Label();
            this.labelTaskOrderID = new System.Windows.Forms.Label();
            this.label1ScanCode = new System.Windows.Forms.Label();
            this.labelSystem = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.labelAlermQuery = new System.Windows.Forms.Label();
            this.labelRefresh = new System.Windows.Forms.Label();
            this.labelResetNumber = new System.Windows.Forms.Label();
            this.lab_centerControl = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lab_screwState = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.lab_plcState = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.groupBox3.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timerScanSN
            // 
            this.timerScanSN.Enabled = true;
            this.timerScanSN.Interval = 200;
            this.timerScanSN.Tick += new System.EventHandler(this.timerScanSN_Tick);
            // 
            // timerRTU
            // 
            this.timerRTU.Enabled = true;
            this.timerRTU.Interval = 80;
            this.timerRTU.Tick += new System.EventHandler(this.timerRTU_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // sqLiteCommand1
            // 
            this.sqLiteCommand1.CommandText = null;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox9);
            this.groupBox3.Controls.Add(this.groupBox10);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.lab_screwClear_plc);
            this.groupBox10.Controls.Add(this.lab_snRequest);
            this.groupBox10.Controls.Add(this.lab_isManufacture);
            this.groupBox10.Controls.Add(this.lab_ng);
            this.groupBox10.Controls.Add(this.lab_ok);
            this.groupBox10.Controls.Add(this.label20);
            this.groupBox10.Controls.Add(this.label19);
            this.groupBox10.Controls.Add(this.label21);
            this.groupBox10.Controls.Add(this.label27);
            this.groupBox10.Controls.Add(this.label28);
            this.groupBox10.Controls.Add(this.label29);
            resources.ApplyResources(this.groupBox10, "groupBox10");
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.TabStop = false;
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            this.label27.Tag = "";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // lab_ok
            // 
            resources.ApplyResources(this.lab_ok, "lab_ok");
            this.lab_ok.ForeColor = System.Drawing.Color.DimGray;
            this.lab_ok.Name = "lab_ok";
            // 
            // lab_ng
            // 
            resources.ApplyResources(this.lab_ng, "lab_ng");
            this.lab_ng.ForeColor = System.Drawing.Color.DimGray;
            this.lab_ng.Name = "lab_ng";
            // 
            // lab_isManufacture
            // 
            resources.ApplyResources(this.lab_isManufacture, "lab_isManufacture");
            this.lab_isManufacture.ForeColor = System.Drawing.Color.DimGray;
            this.lab_isManufacture.Name = "lab_isManufacture";
            this.lab_isManufacture.Tag = "";
            // 
            // lab_snRequest
            // 
            resources.ApplyResources(this.lab_snRequest, "lab_snRequest");
            this.lab_snRequest.ForeColor = System.Drawing.Color.DimGray;
            this.lab_snRequest.Name = "lab_snRequest";
            // 
            // lab_screwClear_plc
            // 
            resources.ApplyResources(this.lab_screwClear_plc, "lab_screwClear_plc");
            this.lab_screwClear_plc.ForeColor = System.Drawing.Color.DimGray;
            this.lab_screwClear_plc.Name = "lab_screwClear_plc";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.lab_ScrewClearOK_apply);
            this.groupBox9.Controls.Add(this.lab_ScrewClearOK);
            this.groupBox9.Controls.Add(this.label2);
            this.groupBox9.Controls.Add(this.label12);
            this.groupBox9.Controls.Add(this.label9);
            this.groupBox9.Controls.Add(this.lab_manufactureResultRecept_apply);
            this.groupBox9.Controls.Add(this.lab_interlock_apply);
            this.groupBox9.Controls.Add(this.lab_manufactureDeny_apply);
            this.groupBox9.Controls.Add(this.lab_manufacturePermission_apply);
            this.groupBox9.Controls.Add(this.lab_snWrite_apply);
            this.groupBox9.Controls.Add(this.lab_manufactureResultRecept);
            this.groupBox9.Controls.Add(this.lab_snWrite);
            this.groupBox9.Controls.Add(this.lab_manufacturePermission);
            this.groupBox9.Controls.Add(this.lab_interlock);
            this.groupBox9.Controls.Add(this.lab_manufactureDeny);
            this.groupBox9.Controls.Add(this.label3);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.label8);
            this.groupBox9.Controls.Add(this.label16);
            this.groupBox9.Controls.Add(this.label17);
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lab_manufactureDeny
            // 
            resources.ApplyResources(this.lab_manufactureDeny, "lab_manufactureDeny");
            this.lab_manufactureDeny.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufactureDeny.Name = "lab_manufactureDeny";
            // 
            // lab_interlock
            // 
            resources.ApplyResources(this.lab_interlock, "lab_interlock");
            this.lab_interlock.ForeColor = System.Drawing.Color.DimGray;
            this.lab_interlock.Name = "lab_interlock";
            // 
            // lab_manufacturePermission
            // 
            resources.ApplyResources(this.lab_manufacturePermission, "lab_manufacturePermission");
            this.lab_manufacturePermission.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufacturePermission.Name = "lab_manufacturePermission";
            // 
            // lab_snWrite
            // 
            resources.ApplyResources(this.lab_snWrite, "lab_snWrite");
            this.lab_snWrite.ForeColor = System.Drawing.Color.DimGray;
            this.lab_snWrite.Name = "lab_snWrite";
            // 
            // lab_manufactureResultRecept
            // 
            resources.ApplyResources(this.lab_manufactureResultRecept, "lab_manufactureResultRecept");
            this.lab_manufactureResultRecept.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufactureResultRecept.Name = "lab_manufactureResultRecept";
            // 
            // lab_snWrite_apply
            // 
            resources.ApplyResources(this.lab_snWrite_apply, "lab_snWrite_apply");
            this.lab_snWrite_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_snWrite_apply.Name = "lab_snWrite_apply";
            // 
            // lab_manufacturePermission_apply
            // 
            resources.ApplyResources(this.lab_manufacturePermission_apply, "lab_manufacturePermission_apply");
            this.lab_manufacturePermission_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufacturePermission_apply.Name = "lab_manufacturePermission_apply";
            // 
            // lab_manufactureDeny_apply
            // 
            resources.ApplyResources(this.lab_manufactureDeny_apply, "lab_manufactureDeny_apply");
            this.lab_manufactureDeny_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufactureDeny_apply.Name = "lab_manufactureDeny_apply";
            // 
            // lab_interlock_apply
            // 
            resources.ApplyResources(this.lab_interlock_apply, "lab_interlock_apply");
            this.lab_interlock_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_interlock_apply.Name = "lab_interlock_apply";
            // 
            // lab_manufactureResultRecept_apply
            // 
            resources.ApplyResources(this.lab_manufactureResultRecept_apply, "lab_manufactureResultRecept_apply");
            this.lab_manufactureResultRecept_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufactureResultRecept_apply.Name = "lab_manufactureResultRecept_apply";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lab_ScrewClearOK
            // 
            resources.ApplyResources(this.lab_ScrewClearOK, "lab_ScrewClearOK");
            this.lab_ScrewClearOK.ForeColor = System.Drawing.Color.DimGray;
            this.lab_ScrewClearOK.Name = "lab_ScrewClearOK";
            // 
            // lab_ScrewClearOK_apply
            // 
            resources.ApplyResources(this.lab_ScrewClearOK_apply, "lab_ScrewClearOK_apply");
            this.lab_ScrewClearOK_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_ScrewClearOK_apply.Name = "lab_ScrewClearOK_apply";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.labelRunMode);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // labelRunMode
            // 
            resources.ApplyResources(this.labelRunMode, "labelRunMode");
            this.labelRunMode.ForeColor = System.Drawing.Color.Black;
            this.labelRunMode.Name = "labelRunMode";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.listBoxInfoLog);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // listBoxInfoLog
            // 
            this.listBoxInfoLog.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.listBoxInfoLog, "listBoxInfoLog");
            this.listBoxInfoLog.FormattingEnabled = true;
            this.listBoxInfoLog.Name = "listBoxInfoLog";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txt_scannerSN);
            this.panel3.Controls.Add(this.txt_plcSN);
            this.panel3.Controls.Add(this.label31);
            this.panel3.Controls.Add(this.label30);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // txt_plcSN
            // 
            resources.ApplyResources(this.txt_plcSN, "txt_plcSN");
            this.txt_plcSN.Name = "txt_plcSN";
            // 
            // txt_scannerSN
            // 
            resources.ApplyResources(this.txt_scannerSN, "txt_scannerSN");
            this.txt_scannerSN.Name = "txt_scannerSN";
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.panel2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 30;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelSystemName);
            resources.ApplyResources(this.panelTop, "panelTop");
            this.panelTop.Name = "panelTop";
            this.panelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTop_Paint);
            // 
            // labelSystemName
            // 
            resources.ApplyResources(this.labelSystemName, "labelSystemName");
            this.labelSystemName.Name = "labelSystemName";
            // 
            // pictureBoxLogo
            // 
            resources.ApplyResources(this.pictureBoxLogo, "pictureBoxLogo");
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.TabStop = false;
            // 
            // labelTime
            // 
            resources.ApplyResources(this.labelTime, "labelTime");
            this.labelTime.Name = "labelTime";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.comboBoxLineMode);
            this.groupBox2.Controls.Add(this.labelUserID);
            this.groupBox2.Controls.Add(this.labelHostName);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label6);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Name = "label6";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label13, "label13");
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Name = "label13";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Name = "label10";
            // 
            // labelHostName
            // 
            this.labelHostName.BackColor = System.Drawing.Color.White;
            this.labelHostName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelHostName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.labelHostName, "labelHostName");
            this.labelHostName.ForeColor = System.Drawing.Color.Black;
            this.labelHostName.Name = "labelHostName";
            // 
            // labelUserID
            // 
            this.labelUserID.BackColor = System.Drawing.Color.White;
            this.labelUserID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelUserID.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.labelUserID, "labelUserID");
            this.labelUserID.ForeColor = System.Drawing.Color.Black;
            this.labelUserID.Name = "labelUserID";
            // 
            // comboBoxLineMode
            // 
            resources.ApplyResources(this.comboBoxLineMode, "comboBoxLineMode");
            this.comboBoxLineMode.FormattingEnabled = true;
            this.comboBoxLineMode.Items.AddRange(new object[] {
            resources.GetString("comboBoxLineMode.Items"),
            resources.GetString("comboBoxLineMode.Items1")});
            this.comboBoxLineMode.Name = "comboBoxLineMode";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel7, 3);
            this.panel7.Controls.Add(this.lab_centerControl);
            this.panel7.Controls.Add(this.labelResetNumber);
            this.panel7.Controls.Add(this.labelRefresh);
            this.panel7.Controls.Add(this.labelAlermQuery);
            this.panel7.Controls.Add(this.label18);
            this.panel7.Controls.Add(this.labelSystem);
            this.panel7.Controls.Add(this.label1ScanCode);
            this.panel7.Controls.Add(this.labelTaskOrderID);
            this.panel7.Controls.Add(this.label1StartTask);
            this.panel7.Controls.Add(this.labelHomeStaus);
            this.panel7.Controls.Add(this.labelMin);
            this.panel7.Controls.Add(this.labelExit);
            this.panel7.Controls.Add(this.labelLogin);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // labelLogin
            // 
            this.labelLogin.BackColor = System.Drawing.Color.Transparent;
            this.labelLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.labelLogin, "labelLogin");
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Click += new System.EventHandler(this.labelLogin_Click);
            this.labelLogin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.labelLogin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // labelExit
            // 
            this.labelExit.BackColor = System.Drawing.Color.Transparent;
            this.labelExit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelExit.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.labelExit, "labelExit");
            this.labelExit.Name = "labelExit";
            this.labelExit.Click += new System.EventHandler(this.labelExit_Click);
            this.labelExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.labelExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // labelMin
            // 
            this.labelMin.BackColor = System.Drawing.Color.Transparent;
            this.labelMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMin.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.labelMin, "labelMin");
            this.labelMin.Name = "labelMin";
            this.labelMin.Click += new System.EventHandler(this.labelMin_Click);
            this.labelMin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.labelMin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // labelHomeStaus
            // 
            this.labelHomeStaus.BackColor = System.Drawing.Color.Lime;
            this.labelHomeStaus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelHomeStaus.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.labelHomeStaus, "labelHomeStaus");
            this.labelHomeStaus.Name = "labelHomeStaus";
            // 
            // label1StartTask
            // 
            this.label1StartTask.BackColor = System.Drawing.Color.Transparent;
            this.label1StartTask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1StartTask.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.label1StartTask, "label1StartTask");
            this.label1StartTask.Name = "label1StartTask";
            this.label1StartTask.Click += new System.EventHandler(this.label1StartTask_Click);
            this.label1StartTask.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.label1StartTask.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // labelTaskOrderID
            // 
            this.labelTaskOrderID.BackColor = System.Drawing.Color.Red;
            this.labelTaskOrderID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.labelTaskOrderID, "labelTaskOrderID");
            this.labelTaskOrderID.Name = "labelTaskOrderID";
            this.labelTaskOrderID.TextChanged += new System.EventHandler(this.labelTaskOrderID_TextChanged);
            // 
            // label1ScanCode
            // 
            this.label1ScanCode.BackColor = System.Drawing.Color.Transparent;
            this.label1ScanCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1ScanCode.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.label1ScanCode, "label1ScanCode");
            this.label1ScanCode.Name = "label1ScanCode";
            this.label1ScanCode.Click += new System.EventHandler(this.label1ScanCode_Click);
            this.label1ScanCode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.label1ScanCode.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // labelSystem
            // 
            this.labelSystem.BackColor = System.Drawing.Color.Transparent;
            this.labelSystem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSystem.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.labelSystem, "labelSystem");
            this.labelSystem.Name = "labelSystem";
            this.labelSystem.Click += new System.EventHandler(this.label8_Click);
            this.labelSystem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.labelSystem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            this.label18.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.label18.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // labelAlermQuery
            // 
            this.labelAlermQuery.BackColor = System.Drawing.Color.Transparent;
            this.labelAlermQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelAlermQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.labelAlermQuery, "labelAlermQuery");
            this.labelAlermQuery.Name = "labelAlermQuery";
            this.labelAlermQuery.Click += new System.EventHandler(this.labelAlermQuery_Click);
            this.labelAlermQuery.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.labelAlermQuery.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // labelRefresh
            // 
            this.labelRefresh.BackColor = System.Drawing.Color.Transparent;
            this.labelRefresh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.labelRefresh, "labelRefresh");
            this.labelRefresh.Name = "labelRefresh";
            this.labelRefresh.Click += new System.EventHandler(this.labelRefresh_Click);
            this.labelRefresh.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.labelRefresh.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // labelResetNumber
            // 
            this.labelResetNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelResetNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelResetNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.labelResetNumber, "labelResetNumber");
            this.labelResetNumber.Name = "labelResetNumber";
            this.labelResetNumber.Click += new System.EventHandler(this.labelResetNumber_Click);
            this.labelResetNumber.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseDown);
            this.labelResetNumber.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1StartTask_MouseUp);
            // 
            // lab_centerControl
            // 
            this.lab_centerControl.BackColor = System.Drawing.Color.Transparent;
            this.lab_centerControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_centerControl.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_centerControl, "lab_centerControl");
            this.lab_centerControl.Name = "lab_centerControl";
            this.lab_centerControl.Click += new System.EventHandler(this.lab_centerControl_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelTime, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxLogo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelTop, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lab_plcState);
            this.groupBox4.Controls.Add(this.lab_screwState);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // lab_screwState
            // 
            this.lab_screwState.BackColor = System.Drawing.Color.Transparent;
            this.lab_screwState.BlinkInterval = 500;
            resources.ApplyResources(this.lab_screwState, "lab_screwState");
            this.lab_screwState.ForeColor = System.Drawing.Color.Black;
            this.lab_screwState.Label = "电批";
            this.lab_screwState.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Right;
            this.lab_screwState.LedColor = System.Drawing.Color.Gray;
            this.lab_screwState.LedSize = new System.Drawing.SizeF(20F, 20F);
            this.lab_screwState.Name = "lab_screwState";
            this.lab_screwState.Renderer = null;
            this.lab_screwState.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.lab_screwState.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            this.lab_screwState.Tag = "";
            // 
            // lab_plcState
            // 
            this.lab_plcState.BackColor = System.Drawing.Color.Transparent;
            this.lab_plcState.BlinkInterval = 500;
            resources.ApplyResources(this.lab_plcState, "lab_plcState");
            this.lab_plcState.ForeColor = System.Drawing.Color.Black;
            this.lab_plcState.Label = "PLC";
            this.lab_plcState.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Right;
            this.lab_plcState.LedColor = System.Drawing.Color.Gray;
            this.lab_plcState.LedSize = new System.Drawing.SizeF(20F, 20F);
            this.lab_plcState.Name = "lab_plcState";
            this.lab_plcState.Renderer = null;
            this.lab_plcState.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.lab_plcState.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.groupBox3.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timerScanSN;
        private System.Windows.Forms.Timer timerRTU;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer3;
        private System.Data.SQLite.SQLiteCommand sqLiteCommand1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label lab_ScrewClearOK_apply;
        private System.Windows.Forms.Label lab_ScrewClearOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lab_manufactureResultRecept_apply;
        private System.Windows.Forms.Label lab_interlock_apply;
        private System.Windows.Forms.Label lab_manufactureDeny_apply;
        private System.Windows.Forms.Label lab_manufacturePermission_apply;
        private System.Windows.Forms.Label lab_snWrite_apply;
        private System.Windows.Forms.Label lab_manufactureResultRecept;
        private System.Windows.Forms.Label lab_snWrite;
        private System.Windows.Forms.Label lab_manufacturePermission;
        private System.Windows.Forms.Label lab_interlock;
        private System.Windows.Forms.Label lab_manufactureDeny;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label lab_screwClear_plc;
        private System.Windows.Forms.Label lab_snRequest;
        private System.Windows.Forms.Label lab_isManufacture;
        private System.Windows.Forms.Label lab_ng;
        private System.Windows.Forms.Label lab_ok;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Panel panel1;
        private LBSoft.IndustrialCtrls.Leds.LBLed lab_plcState;
        private LBSoft.IndustrialCtrls.Leds.LBLed lab_screwState;
        private System.Windows.Forms.Label labelRunMode;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox listBoxInfoLog;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txt_scannerSN;
        private System.Windows.Forms.TextBox txt_plcSN;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lab_centerControl;
        private System.Windows.Forms.Label labelResetNumber;
        private System.Windows.Forms.Label labelRefresh;
        private System.Windows.Forms.Label labelAlermQuery;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label labelSystem;
        private System.Windows.Forms.Label label1ScanCode;
        private System.Windows.Forms.Label labelTaskOrderID;
        private System.Windows.Forms.Label label1StartTask;
        private System.Windows.Forms.Label labelHomeStaus;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelExit;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxLineMode;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Label labelHostName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelSystemName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

