
namespace ScrewMachineManagementSystem
{
    partial class FormScrewDriver
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonTask = new System.Windows.Forms.Button();
            this.buttonTestRun = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonComm = new System.Windows.Forms.Button();
            this.panelForm = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labelAlert = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelProcess = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelTighteningResults = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelTimes = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelMaxTorsion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCylinderNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTaskID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelComm = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonRealChart = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonTask);
            this.panel1.Controls.Add(this.buttonTestRun);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonRealChart);
            this.panel1.Controls.Add(this.buttonComm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1220, 53);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonTask
            // 
            this.buttonTask.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTask.Location = new System.Drawing.Point(304, 12);
            this.buttonTask.Name = "buttonTask";
            this.buttonTask.Size = new System.Drawing.Size(125, 35);
            this.buttonTask.TabIndex = 0;
            this.buttonTask.Text = "任务规划";
            this.buttonTask.UseVisualStyleBackColor = true;
            this.buttonTask.Click += new System.EventHandler(this.buttonTask_Click);
            // 
            // buttonTestRun
            // 
            this.buttonTestRun.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTestRun.Location = new System.Drawing.Point(442, 12);
            this.buttonTestRun.Name = "buttonTestRun";
            this.buttonTestRun.Size = new System.Drawing.Size(125, 35);
            this.buttonTestRun.TabIndex = 0;
            this.buttonTestRun.Text = "调试执行";
            this.buttonTestRun.UseVisualStyleBackColor = true;
            this.buttonTestRun.Click += new System.EventHandler(this.buttonTestRun_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonClose.Location = new System.Drawing.Point(580, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(125, 35);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "返回";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonComm
            // 
            this.buttonComm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonComm.Location = new System.Drawing.Point(166, 12);
            this.buttonComm.Name = "buttonComm";
            this.buttonComm.Size = new System.Drawing.Size(125, 35);
            this.buttonComm.TabIndex = 0;
            this.buttonComm.Text = "连接电批";
            this.buttonComm.UseVisualStyleBackColor = true;
            this.buttonComm.Click += new System.EventHandler(this.buttonCommSetup_Click);
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelForm.Controls.Add(this.panel2);
            this.panelForm.Controls.Add(this.panelBottom);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 53);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(1220, 767);
            this.panelForm.TabIndex = 0;
            this.panelForm.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForm_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1220, 705);
            this.panel2.TabIndex = 7;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.Controls.Add(this.labelAlert);
            this.panelBottom.Controls.Add(this.label8);
            this.panelBottom.Controls.Add(this.labelProcess);
            this.panelBottom.Controls.Add(this.label7);
            this.panelBottom.Controls.Add(this.labelTighteningResults);
            this.panelBottom.Controls.Add(this.label6);
            this.panelBottom.Controls.Add(this.labelTimes);
            this.panelBottom.Controls.Add(this.label5);
            this.panelBottom.Controls.Add(this.labelMaxTorsion);
            this.panelBottom.Controls.Add(this.label4);
            this.panelBottom.Controls.Add(this.labelCylinderNumber);
            this.panelBottom.Controls.Add(this.label3);
            this.panelBottom.Controls.Add(this.labelTaskID);
            this.panelBottom.Controls.Add(this.label2);
            this.panelBottom.Controls.Add(this.labelComm);
            this.panelBottom.Controls.Add(this.label1);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 705);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1220, 62);
            this.panelBottom.TabIndex = 6;
            // 
            // labelAlert
            // 
            this.labelAlert.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelAlert.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAlert.ForeColor = System.Drawing.Color.Red;
            this.labelAlert.Location = new System.Drawing.Point(899, 31);
            this.labelAlert.Name = "labelAlert";
            this.labelAlert.Size = new System.Drawing.Size(121, 28);
            this.labelAlert.TabIndex = 0;
            this.labelAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelAlert.TextChanged += new System.EventHandler(this.labelAlert_TextChanged);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(899, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 28);
            this.label8.TabIndex = 0;
            this.label8.Text = "警报";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelProcess
            // 
            this.labelProcess.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelProcess.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelProcess.ForeColor = System.Drawing.Color.Lime;
            this.labelProcess.Location = new System.Drawing.Point(772, 31);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(121, 28);
            this.labelProcess.TabIndex = 0;
            this.labelProcess.Text = "进程";
            this.labelProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(772, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 28);
            this.label7.TabIndex = 0;
            this.label7.Text = "进程";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTighteningResults
            // 
            this.labelTighteningResults.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelTighteningResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTighteningResults.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTighteningResults.ForeColor = System.Drawing.Color.Red;
            this.labelTighteningResults.Location = new System.Drawing.Point(617, 31);
            this.labelTighteningResults.Name = "labelTighteningResults";
            this.labelTighteningResults.Size = new System.Drawing.Size(121, 28);
            this.labelTighteningResults.TabIndex = 0;
            this.labelTighteningResults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTighteningResults.TextChanged += new System.EventHandler(this.labelTighteningResults_TextChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(617, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 28);
            this.label6.TabIndex = 0;
            this.label6.Text = "拧紧结果";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTimes
            // 
            this.labelTimes.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelTimes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTimes.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTimes.ForeColor = System.Drawing.Color.Lime;
            this.labelTimes.Location = new System.Drawing.Point(490, 31);
            this.labelTimes.Name = "labelTimes";
            this.labelTimes.Size = new System.Drawing.Size(121, 28);
            this.labelTimes.TabIndex = 0;
            this.labelTimes.Text = "0";
            this.labelTimes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(490, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 28);
            this.label5.TabIndex = 0;
            this.label5.Text = "耗时(ms)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMaxTorsion
            // 
            this.labelMaxTorsion.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelMaxTorsion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMaxTorsion.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMaxTorsion.ForeColor = System.Drawing.Color.Lime;
            this.labelMaxTorsion.Location = new System.Drawing.Point(363, 31);
            this.labelMaxTorsion.Name = "labelMaxTorsion";
            this.labelMaxTorsion.Size = new System.Drawing.Size(121, 28);
            this.labelMaxTorsion.TabIndex = 0;
            this.labelMaxTorsion.Text = "0.00";
            this.labelMaxTorsion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(363, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 28);
            this.label4.TabIndex = 0;
            this.label4.Text = "最大扭力(kgf.cm)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCylinderNumber
            // 
            this.labelCylinderNumber.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelCylinderNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCylinderNumber.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCylinderNumber.ForeColor = System.Drawing.Color.Lime;
            this.labelCylinderNumber.Location = new System.Drawing.Point(251, 31);
            this.labelCylinderNumber.Name = "labelCylinderNumber";
            this.labelCylinderNumber.Size = new System.Drawing.Size(88, 28);
            this.labelCylinderNumber.TabIndex = 0;
            this.labelCylinderNumber.Text = "0.00";
            this.labelCylinderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(251, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 28);
            this.label3.TabIndex = 0;
            this.label3.Text = "圈数{r}";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTaskID
            // 
            this.labelTaskID.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelTaskID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTaskID.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTaskID.ForeColor = System.Drawing.Color.Lime;
            this.labelTaskID.Location = new System.Drawing.Point(141, 31);
            this.labelTaskID.Name = "labelTaskID";
            this.labelTaskID.Size = new System.Drawing.Size(88, 28);
            this.labelTaskID.TabIndex = 0;
            this.labelTaskID.Text = "00";
            this.labelTaskID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(141, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "任务号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelComm
            // 
            this.labelComm.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelComm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelComm.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelComm.ForeColor = System.Drawing.Color.Red;
            this.labelComm.Location = new System.Drawing.Point(37, 31);
            this.labelComm.Name = "labelComm";
            this.labelComm.Size = new System.Drawing.Size(88, 28);
            this.labelComm.TabIndex = 0;
            this.labelComm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelComm.TextChanged += new System.EventHandler(this.labelComm_TextChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(37, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "通信";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonRealChart
            // 
            this.buttonRealChart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRealChart.Location = new System.Drawing.Point(865, 12);
            this.buttonRealChart.Name = "buttonRealChart";
            this.buttonRealChart.Size = new System.Drawing.Size(125, 35);
            this.buttonRealChart.TabIndex = 0;
            this.buttonRealChart.Text = "波形";
            this.buttonRealChart.UseVisualStyleBackColor = true;
            this.buttonRealChart.Visible = false;
            this.buttonRealChart.Click += new System.EventHandler(this.buttonRealChart_Click);
            // 
            // FormScrewDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 820);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScrewDriver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "电批管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormScrewDriver_FormClosing);
            this.Load += new System.EventHandler(this.FormScrewDriver_Load);
            this.Resize += new System.EventHandler(this.FormScrewDriver_Resize);
            this.panel1.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Button buttonTask;
        private System.Windows.Forms.Button buttonComm;
        private System.Windows.Forms.Button buttonTestRun;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label labelAlert;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelTighteningResults;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelTimes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelMaxTorsion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCylinderNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTaskID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelComm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonRealChart;
    }
}