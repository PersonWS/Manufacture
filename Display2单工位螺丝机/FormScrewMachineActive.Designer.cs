namespace ScrewMachineManagementSystem
{
    partial class FormScrewMachineActive
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTighten = new System.Windows.Forms.Label();
            this.labelUnscrew = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelTr = new System.Windows.Forms.Label();
            this.labelun = new System.Windows.Forms.Label();
            this.labelFree = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelBUSY = new System.Windows.Forms.Label();
            this.labelErr = new System.Windows.Forms.Label();
            this.labelOK = new System.Windows.Forms.Label();
            this.buttonTighten = new System.Windows.Forms.Button();
            this.buttonUnscrew = new System.Windows.Forms.Button();
            this.buttonFree = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(121, 13);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(192, 28);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(54, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "任务号";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelTighten);
            this.groupBox1.Controls.Add(this.labelUnscrew);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.labelTr);
            this.groupBox1.Controls.Add(this.labelun);
            this.groupBox1.Controls.Add(this.labelFree);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(39, 49);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(371, 115);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IO输入";
            // 
            // labelTighten
            // 
            this.labelTighten.AutoSize = true;
            this.labelTighten.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTighten.Location = new System.Drawing.Point(240, 78);
            this.labelTighten.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTighten.Name = "labelTighten";
            this.labelTighten.Size = new System.Drawing.Size(37, 20);
            this.labelTighten.TabIndex = 0;
            this.labelTighten.Text = "正转";
            // 
            // labelUnscrew
            // 
            this.labelUnscrew.AutoSize = true;
            this.labelUnscrew.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelUnscrew.Location = new System.Drawing.Point(156, 78);
            this.labelUnscrew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUnscrew.Name = "labelUnscrew";
            this.labelUnscrew.Size = new System.Drawing.Size(37, 20);
            this.labelUnscrew.TabIndex = 0;
            this.labelUnscrew.Text = "反转";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(66, 78);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "自由转";
            // 
            // labelTr
            // 
            this.labelTr.BackColor = System.Drawing.Color.Transparent;
            this.labelTr.Font = new System.Drawing.Font("微软雅黑", 51.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTr.ForeColor = System.Drawing.Color.DarkGray;
            this.labelTr.Location = new System.Drawing.Point(224, -18);
            this.labelTr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTr.Name = "labelTr";
            this.labelTr.Size = new System.Drawing.Size(80, 106);
            this.labelTr.TabIndex = 1;
            this.labelTr.Text = "●";
            this.labelTr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelun
            // 
            this.labelun.BackColor = System.Drawing.Color.Transparent;
            this.labelun.Font = new System.Drawing.Font("微软雅黑", 51.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelun.ForeColor = System.Drawing.Color.DarkGray;
            this.labelun.Location = new System.Drawing.Point(140, -18);
            this.labelun.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelun.Name = "labelun";
            this.labelun.Size = new System.Drawing.Size(80, 106);
            this.labelun.TabIndex = 1;
            this.labelun.Text = "●";
            this.labelun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFree
            // 
            this.labelFree.BackColor = System.Drawing.Color.Transparent;
            this.labelFree.Font = new System.Drawing.Font("微软雅黑", 51.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFree.ForeColor = System.Drawing.Color.DarkGray;
            this.labelFree.Location = new System.Drawing.Point(56, -18);
            this.labelFree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFree.Name = "labelFree";
            this.labelFree.Size = new System.Drawing.Size(80, 106);
            this.labelFree.TabIndex = 1;
            this.labelFree.Text = "●";
            this.labelFree.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.labelBUSY);
            this.groupBox2.Controls.Add(this.labelErr);
            this.groupBox2.Controls.Add(this.labelOK);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(39, 175);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(371, 121);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IO输出";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(240, 81);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "BUSY";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(160, 81);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "OK";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(74, 81);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "ERR";
            // 
            // labelBUSY
            // 
            this.labelBUSY.BackColor = System.Drawing.Color.Transparent;
            this.labelBUSY.Font = new System.Drawing.Font("微软雅黑", 51.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBUSY.ForeColor = System.Drawing.Color.DarkGray;
            this.labelBUSY.Location = new System.Drawing.Point(224, -12);
            this.labelBUSY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBUSY.Name = "labelBUSY";
            this.labelBUSY.Size = new System.Drawing.Size(80, 106);
            this.labelBUSY.TabIndex = 1;
            this.labelBUSY.Text = "●";
            this.labelBUSY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelErr
            // 
            this.labelErr.BackColor = System.Drawing.Color.Transparent;
            this.labelErr.Font = new System.Drawing.Font("微软雅黑", 51.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelErr.ForeColor = System.Drawing.Color.DarkGray;
            this.labelErr.Location = new System.Drawing.Point(56, -12);
            this.labelErr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelErr.Name = "labelErr";
            this.labelErr.Size = new System.Drawing.Size(80, 106);
            this.labelErr.TabIndex = 1;
            this.labelErr.Text = "●";
            this.labelErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOK
            // 
            this.labelOK.BackColor = System.Drawing.Color.Transparent;
            this.labelOK.Font = new System.Drawing.Font("微软雅黑", 51.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOK.ForeColor = System.Drawing.Color.DarkGray;
            this.labelOK.Location = new System.Drawing.Point(140, -12);
            this.labelOK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOK.Name = "labelOK";
            this.labelOK.Size = new System.Drawing.Size(80, 106);
            this.labelOK.TabIndex = 1;
            this.labelOK.Text = "●";
            this.labelOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonTighten
            // 
            this.buttonTighten.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTighten.Location = new System.Drawing.Point(445, 35);
            this.buttonTighten.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTighten.Name = "buttonTighten";
            this.buttonTighten.Size = new System.Drawing.Size(128, 53);
            this.buttonTighten.TabIndex = 3;
            this.buttonTighten.Text = "拧紧";
            this.buttonTighten.UseVisualStyleBackColor = true;
            // 
            // buttonUnscrew
            // 
            this.buttonUnscrew.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonUnscrew.Location = new System.Drawing.Point(445, 105);
            this.buttonUnscrew.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUnscrew.Name = "buttonUnscrew";
            this.buttonUnscrew.Size = new System.Drawing.Size(128, 53);
            this.buttonUnscrew.TabIndex = 3;
            this.buttonUnscrew.Text = "拧松";
            this.buttonUnscrew.UseVisualStyleBackColor = true;
            // 
            // buttonFree
            // 
            this.buttonFree.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonFree.Location = new System.Drawing.Point(445, 175);
            this.buttonFree.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFree.Name = "buttonFree";
            this.buttonFree.Size = new System.Drawing.Size(128, 53);
            this.buttonFree.TabIndex = 3;
            this.buttonFree.Text = "自由转";
            this.buttonFree.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 313);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(615, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // FormScrewMachineActive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 335);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonFree);
            this.Controls.Add(this.buttonUnscrew);
            this.Controls.Add(this.buttonTighten);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScrewMachineActive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "调试运行";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormScrewMachineActive_FormClosing);
            this.Load += new System.EventHandler(this.FormScrewMachineActive_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelFree;
        private System.Windows.Forms.Label labelTighten;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelUnscrew;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelTr;
        private System.Windows.Forms.Label labelun;
        private System.Windows.Forms.Label labelBUSY;
        private System.Windows.Forms.Label labelErr;
        private System.Windows.Forms.Label labelOK;
        private System.Windows.Forms.Button buttonTighten;
        private System.Windows.Forms.Button buttonUnscrew;
        private System.Windows.Forms.Button buttonFree;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}