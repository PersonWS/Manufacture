namespace ScrewMachineManagementSystem.CenterControl
{
    partial class CenterDemo
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
            this.txt_showMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_StartCenterControl = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_snRequest = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lab_isManufacture = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lab_ok = new System.Windows.Forms.Label();
            this.lab_ng = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lab_PLC_ConnectState = new System.Windows.Forms.Label();
            this.btn_StopCenterControl = new System.Windows.Forms.Button();
            this.btn_clearLog = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lab_manufactureResultRecept_apply = new System.Windows.Forms.Label();
            this.lab_interlock_apply = new System.Windows.Forms.Label();
            this.lab_manufactureDeny_apply = new System.Windows.Forms.Label();
            this.lab_manufacturePermission_apply = new System.Windows.Forms.Label();
            this.lab_snWrite_apply = new System.Windows.Forms.Label();
            this.lab_manufactureResultRecept = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lab_snWrite = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lab_manufacturePermission = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lab_interlock = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lab_manufactureDeny = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lab_sn = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lab_lastProcessName = new System.Windows.Forms.Label();
            this.btn_sn_set = new System.Windows.Forms.Button();
            this.txt_SN = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_lastProcessName = new System.Windows.Forms.TextBox();
            this.btn_set_lastProcessName = new System.Windows.Forms.Button();
            this.btn_saveReult = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_forceResultOK = new System.Windows.Forms.Button();
            this.btn_ForceManufacturePermission = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_showMessage
            // 
            this.txt_showMessage.Location = new System.Drawing.Point(10, 429);
            this.txt_showMessage.Multiline = true;
            this.txt_showMessage.Name = "txt_showMessage";
            this.txt_showMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_showMessage.Size = new System.Drawing.Size(762, 158);
            this.txt_showMessage.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "message:";
            // 
            // btn_StartCenterControl
            // 
            this.btn_StartCenterControl.Location = new System.Drawing.Point(496, 2);
            this.btn_StartCenterControl.Name = "btn_StartCenterControl";
            this.btn_StartCenterControl.Size = new System.Drawing.Size(136, 68);
            this.btn_StartCenterControl.TabIndex = 2;
            this.btn_StartCenterControl.Text = "StartCenterControl";
            this.btn_StartCenterControl.UseVisualStyleBackColor = true;
            this.btn_StartCenterControl.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "SN码请求[1.DBX0.0]：";
            // 
            // lab_snRequest
            // 
            this.lab_snRequest.AutoSize = true;
            this.lab_snRequest.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_snRequest.ForeColor = System.Drawing.Color.DimGray;
            this.lab_snRequest.Location = new System.Drawing.Point(156, 38);
            this.lab_snRequest.Name = "lab_snRequest";
            this.lab_snRequest.Size = new System.Drawing.Size(28, 19);
            this.lab_snRequest.TabIndex = 4;
            this.lab_snRequest.Text = "●";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "是否加工[1.DBX0.1]：";
            // 
            // lab_isManufacture
            // 
            this.lab_isManufacture.AutoSize = true;
            this.lab_isManufacture.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_isManufacture.ForeColor = System.Drawing.Color.DimGray;
            this.lab_isManufacture.Location = new System.Drawing.Point(156, 78);
            this.lab_isManufacture.Name = "lab_isManufacture";
            this.lab_isManufacture.Size = new System.Drawing.Size(28, 19);
            this.lab_isManufacture.TabIndex = 6;
            this.lab_isManufacture.Text = "●";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "结果 OK [1.DBX0.2]：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "结果 NG [1.DBX0.3]：";
            // 
            // lab_ok
            // 
            this.lab_ok.AutoSize = true;
            this.lab_ok.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ok.ForeColor = System.Drawing.Color.DimGray;
            this.lab_ok.Location = new System.Drawing.Point(156, 118);
            this.lab_ok.Name = "lab_ok";
            this.lab_ok.Size = new System.Drawing.Size(28, 19);
            this.lab_ok.TabIndex = 9;
            this.lab_ok.Text = "●";
            // 
            // lab_ng
            // 
            this.lab_ng.AutoSize = true;
            this.lab_ng.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ng.ForeColor = System.Drawing.Color.DimGray;
            this.lab_ng.Location = new System.Drawing.Point(156, 158);
            this.lab_ng.Name = "lab_ng";
            this.lab_ng.Size = new System.Drawing.Size(28, 19);
            this.lab_ng.TabIndex = 10;
            this.lab_ng.Text = "●";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "PLC_Connect_state：";
            // 
            // lab_PLC_ConnectState
            // 
            this.lab_PLC_ConnectState.AutoSize = true;
            this.lab_PLC_ConnectState.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_PLC_ConnectState.ForeColor = System.Drawing.Color.Red;
            this.lab_PLC_ConnectState.Location = new System.Drawing.Point(134, 20);
            this.lab_PLC_ConnectState.Name = "lab_PLC_ConnectState";
            this.lab_PLC_ConnectState.Size = new System.Drawing.Size(28, 19);
            this.lab_PLC_ConnectState.TabIndex = 12;
            this.lab_PLC_ConnectState.Text = "●";
            // 
            // btn_StopCenterControl
            // 
            this.btn_StopCenterControl.Location = new System.Drawing.Point(638, 2);
            this.btn_StopCenterControl.Name = "btn_StopCenterControl";
            this.btn_StopCenterControl.Size = new System.Drawing.Size(136, 68);
            this.btn_StopCenterControl.TabIndex = 13;
            this.btn_StopCenterControl.Text = "StopCenterControl";
            this.btn_StopCenterControl.UseVisualStyleBackColor = true;
            this.btn_StopCenterControl.Click += new System.EventHandler(this.btn_StopCenterControl_Click);
            // 
            // btn_clearLog
            // 
            this.btn_clearLog.Location = new System.Drawing.Point(672, 393);
            this.btn_clearLog.Name = "btn_clearLog";
            this.btn_clearLog.Size = new System.Drawing.Size(100, 36);
            this.btn_clearLog.TabIndex = 14;
            this.btn_clearLog.Text = "clear Message";
            this.btn_clearLog.UseVisualStyleBackColor = true;
            this.btn_clearLog.Click += new System.EventHandler(this.btn_clearLog_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lab_snRequest);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lab_isManufacture);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lab_ng);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lab_ok);
            this.groupBox1.Location = new System.Drawing.Point(14, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 233);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLC-->PC[DB2001]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(157, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "状态";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.lab_manufactureResultRecept_apply);
            this.groupBox2.Controls.Add(this.lab_interlock_apply);
            this.groupBox2.Controls.Add(this.lab_manufactureDeny_apply);
            this.groupBox2.Controls.Add(this.lab_manufacturePermission_apply);
            this.groupBox2.Controls.Add(this.lab_snWrite_apply);
            this.groupBox2.Controls.Add(this.lab_manufactureResultRecept);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lab_snWrite);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lab_manufacturePermission);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lab_interlock);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.lab_manufactureDeny);
            this.groupBox2.Location = new System.Drawing.Point(248, 60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 233);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PC-->PLC[DB2002]";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(181, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 20;
            this.label12.Text = "申请";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(146, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "状态";
            // 
            // lab_manufactureResultRecept_apply
            // 
            this.lab_manufactureResultRecept_apply.AutoSize = true;
            this.lab_manufactureResultRecept_apply.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_manufactureResultRecept_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufactureResultRecept_apply.Location = new System.Drawing.Point(181, 198);
            this.lab_manufactureResultRecept_apply.Name = "lab_manufactureResultRecept_apply";
            this.lab_manufactureResultRecept_apply.Size = new System.Drawing.Size(28, 19);
            this.lab_manufactureResultRecept_apply.TabIndex = 17;
            this.lab_manufactureResultRecept_apply.Text = "●";
            // 
            // lab_interlock_apply
            // 
            this.lab_interlock_apply.AutoSize = true;
            this.lab_interlock_apply.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_interlock_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_interlock_apply.Location = new System.Drawing.Point(181, 158);
            this.lab_interlock_apply.Name = "lab_interlock_apply";
            this.lab_interlock_apply.Size = new System.Drawing.Size(28, 19);
            this.lab_interlock_apply.TabIndex = 16;
            this.lab_interlock_apply.Text = "●";
            // 
            // lab_manufactureDeny_apply
            // 
            this.lab_manufactureDeny_apply.AutoSize = true;
            this.lab_manufactureDeny_apply.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_manufactureDeny_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufactureDeny_apply.Location = new System.Drawing.Point(181, 118);
            this.lab_manufactureDeny_apply.Name = "lab_manufactureDeny_apply";
            this.lab_manufactureDeny_apply.Size = new System.Drawing.Size(28, 19);
            this.lab_manufactureDeny_apply.TabIndex = 15;
            this.lab_manufactureDeny_apply.Text = "●";
            // 
            // lab_manufacturePermission_apply
            // 
            this.lab_manufacturePermission_apply.AutoSize = true;
            this.lab_manufacturePermission_apply.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_manufacturePermission_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufacturePermission_apply.Location = new System.Drawing.Point(181, 78);
            this.lab_manufacturePermission_apply.Name = "lab_manufacturePermission_apply";
            this.lab_manufacturePermission_apply.Size = new System.Drawing.Size(28, 19);
            this.lab_manufacturePermission_apply.TabIndex = 14;
            this.lab_manufacturePermission_apply.Text = "●";
            // 
            // lab_snWrite_apply
            // 
            this.lab_snWrite_apply.AutoSize = true;
            this.lab_snWrite_apply.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_snWrite_apply.ForeColor = System.Drawing.Color.DimGray;
            this.lab_snWrite_apply.Location = new System.Drawing.Point(181, 38);
            this.lab_snWrite_apply.Name = "lab_snWrite_apply";
            this.lab_snWrite_apply.Size = new System.Drawing.Size(28, 19);
            this.lab_snWrite_apply.TabIndex = 13;
            this.lab_snWrite_apply.Text = "●";
            // 
            // lab_manufactureResultRecept
            // 
            this.lab_manufactureResultRecept.AutoSize = true;
            this.lab_manufactureResultRecept.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_manufactureResultRecept.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufactureResultRecept.Location = new System.Drawing.Point(147, 198);
            this.lab_manufactureResultRecept.Name = "lab_manufactureResultRecept";
            this.lab_manufactureResultRecept.Size = new System.Drawing.Size(28, 19);
            this.lab_manufactureResultRecept.TabIndex = 12;
            this.lab_manufactureResultRecept.Text = "●";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 200);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 12);
            this.label15.TabIndex = 11;
            this.label15.Text = "结果收到[2.DBX0.2]：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "SN码写入[2.DBX1.0]：";
            // 
            // lab_snWrite
            // 
            this.lab_snWrite.AutoSize = true;
            this.lab_snWrite.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_snWrite.ForeColor = System.Drawing.Color.DimGray;
            this.lab_snWrite.Location = new System.Drawing.Point(147, 38);
            this.lab_snWrite.Name = "lab_snWrite";
            this.lab_snWrite.Size = new System.Drawing.Size(28, 19);
            this.lab_snWrite.TabIndex = 4;
            this.lab_snWrite.Text = "●";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "允许加工[2.DBX0.0]：";
            // 
            // lab_manufacturePermission
            // 
            this.lab_manufacturePermission.AutoSize = true;
            this.lab_manufacturePermission.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_manufacturePermission.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufacturePermission.Location = new System.Drawing.Point(147, 78);
            this.lab_manufacturePermission.Name = "lab_manufacturePermission";
            this.lab_manufacturePermission.Size = new System.Drawing.Size(28, 19);
            this.lab_manufacturePermission.TabIndex = 6;
            this.lab_manufacturePermission.Text = "●";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 12);
            this.label11.TabIndex = 7;
            this.label11.Text = "禁止加工[2.DBX0.1]：";
            // 
            // lab_interlock
            // 
            this.lab_interlock.AutoSize = true;
            this.lab_interlock.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_interlock.ForeColor = System.Drawing.Color.DimGray;
            this.lab_interlock.Location = new System.Drawing.Point(147, 158);
            this.lab_interlock.Name = "lab_interlock";
            this.lab_interlock.Size = new System.Drawing.Size(28, 19);
            this.lab_interlock.TabIndex = 10;
            this.lab_interlock.Text = "●";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(25, 160);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 12);
            this.label13.TabIndex = 8;
            this.label13.Text = "互锁结果[2.DBX0.3]：";
            // 
            // lab_manufactureDeny
            // 
            this.lab_manufactureDeny.AutoSize = true;
            this.lab_manufactureDeny.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_manufactureDeny.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufactureDeny.Location = new System.Drawing.Point(147, 118);
            this.lab_manufactureDeny.Name = "lab_manufactureDeny";
            this.lab_manufactureDeny.Size = new System.Drawing.Size(28, 19);
            this.lab_manufactureDeny.TabIndex = 9;
            this.lab_manufactureDeny.Text = "●";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(224, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 12);
            this.label14.TabIndex = 17;
            this.label14.Text = "Production SN：";
            // 
            // lab_sn
            // 
            this.lab_sn.AutoSize = true;
            this.lab_sn.Location = new System.Drawing.Point(325, 12);
            this.lab_sn.Name = "lab_sn";
            this.lab_sn.Size = new System.Drawing.Size(11, 12);
            this.lab_sn.TabIndex = 18;
            this.lab_sn.Text = "*";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(224, 35);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 12);
            this.label16.TabIndex = 19;
            this.label16.Text = "Last  Process：";
            // 
            // lab_lastProcessName
            // 
            this.lab_lastProcessName.AutoSize = true;
            this.lab_lastProcessName.Location = new System.Drawing.Point(325, 34);
            this.lab_lastProcessName.Name = "lab_lastProcessName";
            this.lab_lastProcessName.Size = new System.Drawing.Size(11, 12);
            this.lab_lastProcessName.TabIndex = 20;
            this.lab_lastProcessName.Text = "*";
            // 
            // btn_sn_set
            // 
            this.btn_sn_set.Location = new System.Drawing.Point(638, 76);
            this.btn_sn_set.Name = "btn_sn_set";
            this.btn_sn_set.Size = new System.Drawing.Size(136, 68);
            this.btn_sn_set.TabIndex = 21;
            this.btn_sn_set.Text = "1.SN_SET";
            this.btn_sn_set.UseVisualStyleBackColor = true;
            this.btn_sn_set.Click += new System.EventHandler(this.btn_sn_set_Click);
            // 
            // txt_SN
            // 
            this.txt_SN.Location = new System.Drawing.Point(496, 98);
            this.txt_SN.Multiline = true;
            this.txt_SN.Name = "txt_SN";
            this.txt_SN.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_SN.Size = new System.Drawing.Size(136, 46);
            this.txt_SN.TabIndex = 22;
            this.txt_SN.Text = "LZ:1234567890";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(522, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 12);
            this.label17.TabIndex = 23;
            this.label17.Text = "↓ Type SN ↓";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(499, 159);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(131, 12);
            this.label18.TabIndex = 26;
            this.label18.Text = "↓Type Last Process↓";
            // 
            // txt_lastProcessName
            // 
            this.txt_lastProcessName.Location = new System.Drawing.Point(496, 174);
            this.txt_lastProcessName.Multiline = true;
            this.txt_lastProcessName.Name = "txt_lastProcessName";
            this.txt_lastProcessName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_lastProcessName.Size = new System.Drawing.Size(136, 46);
            this.txt_lastProcessName.TabIndex = 25;
            this.txt_lastProcessName.Text = "BYJ";
            // 
            // btn_set_lastProcessName
            // 
            this.btn_set_lastProcessName.Location = new System.Drawing.Point(638, 152);
            this.btn_set_lastProcessName.Name = "btn_set_lastProcessName";
            this.btn_set_lastProcessName.Size = new System.Drawing.Size(136, 68);
            this.btn_set_lastProcessName.TabIndex = 24;
            this.btn_set_lastProcessName.Text = "2.SetLastProcessName";
            this.btn_set_lastProcessName.UseVisualStyleBackColor = true;
            this.btn_set_lastProcessName.Click += new System.EventHandler(this.btn_set_lastProcessName_Click);
            // 
            // btn_saveReult
            // 
            this.btn_saveReult.Location = new System.Drawing.Point(638, 226);
            this.btn_saveReult.Name = "btn_saveReult";
            this.btn_saveReult.Size = new System.Drawing.Size(136, 68);
            this.btn_saveReult.TabIndex = 27;
            this.btn_saveReult.Text = "3.SaveResult";
            this.btn_saveReult.UseVisualStyleBackColor = true;
            this.btn_saveReult.Click += new System.EventHandler(this.btn_saveReult_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_forceResultOK);
            this.groupBox3.Controls.Add(this.btn_ForceManufacturePermission);
            this.groupBox3.Location = new System.Drawing.Point(14, 299);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 103);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "强制指令区域";
            // 
            // btn_forceResultOK
            // 
            this.btn_forceResultOK.Location = new System.Drawing.Point(156, 20);
            this.btn_forceResultOK.Name = "btn_forceResultOK";
            this.btn_forceResultOK.Size = new System.Drawing.Size(136, 68);
            this.btn_forceResultOK.TabIndex = 30;
            this.btn_forceResultOK.Text = "强制结果收到";
            this.btn_forceResultOK.UseVisualStyleBackColor = true;
            this.btn_forceResultOK.Click += new System.EventHandler(this.btn_forceResultOK_Click);
            // 
            // btn_ForceManufacturePermission
            // 
            this.btn_ForceManufacturePermission.Location = new System.Drawing.Point(14, 20);
            this.btn_ForceManufacturePermission.Name = "btn_ForceManufacturePermission";
            this.btn_ForceManufacturePermission.Size = new System.Drawing.Size(136, 68);
            this.btn_ForceManufacturePermission.TabIndex = 29;
            this.btn_ForceManufacturePermission.Text = "强制允许加工";
            this.btn_ForceManufacturePermission.UseVisualStyleBackColor = true;
            this.btn_ForceManufacturePermission.Click += new System.EventHandler(this.btn_ForceManufacturePermission_Click);
            // 
            // CenterDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 599);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_saveReult);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txt_lastProcessName);
            this.Controls.Add(this.btn_set_lastProcessName);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txt_SN);
            this.Controls.Add(this.btn_sn_set);
            this.Controls.Add(this.lab_lastProcessName);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lab_sn);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_clearLog);
            this.Controls.Add(this.btn_StopCenterControl);
            this.Controls.Add(this.lab_PLC_ConnectState);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btn_StartCenterControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_showMessage);
            this.Name = "CenterDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CenterDemo|螺丝机(装饰条)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_showMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_StartCenterControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lab_snRequest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lab_isManufacture;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lab_ok;
        private System.Windows.Forms.Label lab_ng;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lab_PLC_ConnectState;
        private System.Windows.Forms.Button btn_StopCenterControl;
        private System.Windows.Forms.Button btn_clearLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lab_snWrite;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lab_manufacturePermission;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lab_interlock;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lab_manufactureDeny;
        private System.Windows.Forms.Label lab_manufactureResultRecept;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lab_manufactureResultRecept_apply;
        private System.Windows.Forms.Label lab_interlock_apply;
        private System.Windows.Forms.Label lab_manufactureDeny_apply;
        private System.Windows.Forms.Label lab_manufacturePermission_apply;
        private System.Windows.Forms.Label lab_snWrite_apply;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lab_sn;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lab_lastProcessName;
        private System.Windows.Forms.Button btn_sn_set;
        private System.Windows.Forms.TextBox txt_SN;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_lastProcessName;
        private System.Windows.Forms.Button btn_set_lastProcessName;
        private System.Windows.Forms.Button btn_saveReult;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_ForceManufacturePermission;
        private System.Windows.Forms.Button btn_forceResultOK;
    }
}