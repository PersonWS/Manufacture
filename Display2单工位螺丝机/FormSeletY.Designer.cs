namespace ScrewMachineManagementSystem
{
    partial class FormSeletY
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
            this.labelSelect1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelX = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelMcodeLength = new System.Windows.Forms.Label();
            this.labelPcodeLength = new System.Windows.Forms.Label();
            this.labelSN_M = new System.Windows.Forms.Label();
            this.labelMsg = new System.Windows.Forms.Label();
            this.labelScanedLenght = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelSN = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSelect1
            // 
            this.labelSelect1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelSelect1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSelect1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSelect1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSelect1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSelect1.Location = new System.Drawing.Point(248, 222);
            this.labelSelect1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelect1.Name = "labelSelect1";
            this.labelSelect1.Size = new System.Drawing.Size(170, 46);
            this.labelSelect1.TabIndex = 4;
            this.labelSelect1.Text = "确定";
            this.labelSelect1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelSelect1.Click += new System.EventHandler(this.labelSelect1_Click);
            this.labelSelect1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelSelect1_MouseDown);
            this.labelSelect1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelSelect1_MouseUp);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(443, 43);
            this.label1.TabIndex = 6;
            this.label1.Text = "请扫描产品SN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(77, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(600, 2);
            this.label3.TabIndex = 7;
            this.label3.Text = "label3";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.labelSN);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelX);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.labelMcodeLength);
            this.panel1.Controls.Add(this.labelPcodeLength);
            this.panel1.Controls.Add(this.labelSN_M);
            this.panel1.Controls.Add(this.labelSelect1);
            this.panel1.Controls.Add(this.labelMsg);
            this.panel1.Controls.Add(this.labelScanedLenght);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(668, 305);
            this.panel1.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = global::ScrewMachineManagementSystem.Properties.Resources.scancode;
            this.pictureBox1.Location = new System.Drawing.Point(3, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 169);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // labelX
            // 
            this.labelX.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelX.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX.ForeColor = System.Drawing.Color.Black;
            this.labelX.Location = new System.Drawing.Point(494, 222);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(170, 46);
            this.labelX.TabIndex = 10;
            this.labelX.Text = "关闭";
            this.labelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelX.Click += new System.EventHandler(this.labelX_Click);
            this.labelX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelSelect1_MouseDown);
            this.labelX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelSelect1_MouseUp);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(77, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(600, 2);
            this.label5.TabIndex = 7;
            this.label5.Text = "label3";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(86, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 21);
            this.label6.TabIndex = 6;
            this.label6.Text = "M码";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(572, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "X清除SN码";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelSelect1_MouseDown);
            this.label2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelSelect1_MouseUp);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(86, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "P码";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMcodeLength
            // 
            this.labelMcodeLength.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelMcodeLength.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMcodeLength.Location = new System.Drawing.Point(562, 129);
            this.labelMcodeLength.Name = "labelMcodeLength";
            this.labelMcodeLength.Size = new System.Drawing.Size(114, 31);
            this.labelMcodeLength.TabIndex = 6;
            this.labelMcodeLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPcodeLength
            // 
            this.labelPcodeLength.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelPcodeLength.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPcodeLength.Location = new System.Drawing.Point(562, 43);
            this.labelPcodeLength.Name = "labelPcodeLength";
            this.labelPcodeLength.Size = new System.Drawing.Size(114, 31);
            this.labelPcodeLength.TabIndex = 6;
            this.labelPcodeLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSN_M
            // 
            this.labelSN_M.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelSN_M.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSN_M.ForeColor = System.Drawing.Color.DarkGray;
            this.labelSN_M.Location = new System.Drawing.Point(77, 166);
            this.labelSN_M.Name = "labelSN_M";
            this.labelSN_M.Size = new System.Drawing.Size(600, 43);
            this.labelSN_M.TabIndex = 6;
            this.labelSN_M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelSN_M.TextChanged += new System.EventHandler(this.labelSN_M_TextChanged);
            // 
            // labelMsg
            // 
            this.labelMsg.BackColor = System.Drawing.SystemColors.ControlLight;
            this.labelMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelMsg.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMsg.ForeColor = System.Drawing.Color.Red;
            this.labelMsg.Location = new System.Drawing.Point(0, 279);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(668, 26);
            this.labelMsg.TabIndex = 6;
            this.labelMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelMsg.TextChanged += new System.EventHandler(this.labelSN_TextChanged);
            // 
            // labelScanedLenght
            // 
            this.labelScanedLenght.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelScanedLenght.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelScanedLenght.Location = new System.Drawing.Point(463, 9);
            this.labelScanedLenght.Name = "labelScanedLenght";
            this.labelScanedLenght.Size = new System.Drawing.Size(114, 31);
            this.labelScanedLenght.TabIndex = 6;
            this.labelScanedLenght.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelScanedLenght.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelSN
            // 
            this.labelSN.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelSN.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSN.ForeColor = System.Drawing.Color.DarkGray;
            this.labelSN.Location = new System.Drawing.Point(77, 73);
            this.labelSN.Name = "labelSN";
            this.labelSN.Size = new System.Drawing.Size(600, 43);
            this.labelSN.TabIndex = 11;
            this.labelSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormSeletY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(668, 305);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSeletY";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "扫描产品SN，选择工位";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSeletY_FormClosing);
            this.Load += new System.EventHandler(this.FormSeletY_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSeletY_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormSeletY_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelSelect1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelScanedLenght;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSN_M;
        private System.Windows.Forms.Label labelMcodeLength;
        private System.Windows.Forms.Label labelPcodeLength;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSN;
    }
}