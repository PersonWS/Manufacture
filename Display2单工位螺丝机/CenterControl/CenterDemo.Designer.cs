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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lab_snWrite = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lab_manufacturePermission = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lab_interlock = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lab_manufactureDeny = new System.Windows.Forms.Label();
            this.lab_manufactureResultRecept = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_showMessage
            // 
            this.txt_showMessage.Location = new System.Drawing.Point(12, 408);
            this.txt_showMessage.Multiline = true;
            this.txt_showMessage.Name = "txt_showMessage";
            this.txt_showMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_showMessage.Size = new System.Drawing.Size(762, 131);
            this.txt_showMessage.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "message:";
            // 
            // btn_StartCenterControl
            // 
            this.btn_StartCenterControl.Location = new System.Drawing.Point(638, 12);
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
            this.label2.Location = new System.Drawing.Point(25, 39);
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
            this.lab_snRequest.Location = new System.Drawing.Point(147, 37);
            this.lab_snRequest.Name = "lab_snRequest";
            this.lab_snRequest.Size = new System.Drawing.Size(28, 19);
            this.lab_snRequest.TabIndex = 4;
            this.lab_snRequest.Text = "●";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 81);
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
            this.lab_isManufacture.Location = new System.Drawing.Point(147, 75);
            this.lab_isManufacture.Name = "lab_isManufacture";
            this.lab_isManufacture.Size = new System.Drawing.Size(28, 19);
            this.lab_isManufacture.TabIndex = 6;
            this.lab_isManufacture.Text = "●";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "结果 OK [1.DBX0.2]：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 155);
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
            this.lab_ok.Location = new System.Drawing.Point(147, 115);
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
            this.lab_ng.Location = new System.Drawing.Point(147, 151);
            this.lab_ng.Name = "lab_ng";
            this.lab_ng.Size = new System.Drawing.Size(28, 19);
            this.lab_ng.TabIndex = 10;
            this.lab_ng.Text = "●";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 30);
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
            this.lab_PLC_ConnectState.Location = new System.Drawing.Point(134, 28);
            this.lab_PLC_ConnectState.Name = "lab_PLC_ConnectState";
            this.lab_PLC_ConnectState.Size = new System.Drawing.Size(28, 19);
            this.lab_PLC_ConnectState.TabIndex = 12;
            this.lab_PLC_ConnectState.Text = "●";
            // 
            // btn_StopCenterControl
            // 
            this.btn_StopCenterControl.Location = new System.Drawing.Point(638, 95);
            this.btn_StopCenterControl.Name = "btn_StopCenterControl";
            this.btn_StopCenterControl.Size = new System.Drawing.Size(136, 68);
            this.btn_StopCenterControl.TabIndex = 13;
            this.btn_StopCenterControl.Text = "StopCenterControl";
            this.btn_StopCenterControl.UseVisualStyleBackColor = true;
            this.btn_StopCenterControl.Click += new System.EventHandler(this.btn_StopCenterControl_Click);
            // 
            // btn_clearLog
            // 
            this.btn_clearLog.Location = new System.Drawing.Point(711, 410);
            this.btn_clearLog.Name = "btn_clearLog";
            this.btn_clearLog.Size = new System.Drawing.Size(61, 36);
            this.btn_clearLog.TabIndex = 14;
            this.btn_clearLog.Text = "clearLog";
            this.btn_clearLog.UseVisualStyleBackColor = true;
            this.btn_clearLog.Click += new System.EventHandler(this.btn_clearLog_Click);
            // 
            // groupBox1
            // 
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
            this.groupBox1.Size = new System.Drawing.Size(200, 200);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLC-->PC[DB2001]";
            // 
            // groupBox2
            // 
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
            this.groupBox2.Size = new System.Drawing.Size(200, 233);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PC-->PLC[DB2002]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 39);
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
            this.lab_snWrite.Location = new System.Drawing.Point(147, 37);
            this.lab_snWrite.Name = "lab_snWrite";
            this.lab_snWrite.Size = new System.Drawing.Size(28, 19);
            this.lab_snWrite.TabIndex = 4;
            this.lab_snWrite.Text = "●";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 81);
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
            this.lab_manufacturePermission.Location = new System.Drawing.Point(147, 75);
            this.lab_manufacturePermission.Name = "lab_manufacturePermission";
            this.lab_manufacturePermission.Size = new System.Drawing.Size(28, 19);
            this.lab_manufacturePermission.TabIndex = 6;
            this.lab_manufacturePermission.Text = "●";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 118);
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
            this.lab_interlock.Location = new System.Drawing.Point(147, 151);
            this.lab_interlock.Name = "lab_interlock";
            this.lab_interlock.Size = new System.Drawing.Size(28, 19);
            this.lab_interlock.TabIndex = 10;
            this.lab_interlock.Text = "●";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(25, 155);
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
            this.lab_manufactureDeny.Location = new System.Drawing.Point(147, 115);
            this.lab_manufactureDeny.Name = "lab_manufactureDeny";
            this.lab_manufactureDeny.Size = new System.Drawing.Size(28, 19);
            this.lab_manufactureDeny.TabIndex = 9;
            this.lab_manufactureDeny.Text = "●";
            // 
            // lab_manufactureResultRecept
            // 
            this.lab_manufactureResultRecept.AutoSize = true;
            this.lab_manufactureResultRecept.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_manufactureResultRecept.ForeColor = System.Drawing.Color.DimGray;
            this.lab_manufactureResultRecept.Location = new System.Drawing.Point(147, 184);
            this.lab_manufactureResultRecept.Name = "lab_manufactureResultRecept";
            this.lab_manufactureResultRecept.Size = new System.Drawing.Size(28, 19);
            this.lab_manufactureResultRecept.TabIndex = 12;
            this.lab_manufactureResultRecept.Text = "●";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 188);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 12);
            this.label15.TabIndex = 11;
            this.label15.Text = "结果收到[2.DBX0.2]：";
            // 
            // CenterDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 551);
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
            this.Text = "CenterDemo|螺丝机(装饰条)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}