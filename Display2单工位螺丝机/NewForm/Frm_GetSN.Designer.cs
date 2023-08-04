namespace ScrewMachineManagementSystem
{
    partial class Frm_GetSN
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
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_SN_CheckCode = new System.Windows.Forms.TextBox();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            this.numericUpDownBoxQty = new System.Windows.Forms.NumericUpDown();
            this.textBoxMLenght = new System.Windows.Forms.TextBox();
            this.textBoxSNLenght = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_SN_maxLength = new System.Windows.Forms.TextBox();
            this.NumberOfScrews = new System.Windows.Forms.TextBox();
            this.textBoxTPID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_SN_Scan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBoxQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Blue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(677, 37);
            this.label2.TabIndex = 6;
            this.label2.Text = "扫描SN码";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOK.Location = new System.Drawing.Point(510, 203);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(118, 38);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.txt_SN_Scan);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Location = new System.Drawing.Point(-5, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 260);
            this.panel1.TabIndex = 12;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_SN_CheckCode);
            this.groupBox3.Controls.Add(this.comboBoxCustomer);
            this.groupBox3.Controls.Add(this.numericUpDownBoxQty);
            this.groupBox3.Controls.Add(this.textBoxMLenght);
            this.groupBox3.Controls.Add(this.textBoxSNLenght);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txt_SN_maxLength);
            this.groupBox3.Controls.Add(this.NumberOfScrews);
            this.groupBox3.Controls.Add(this.textBoxTPID);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(127, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(501, 114);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SN/配方信息";
            // 
            // txt_SN_CheckCode
            // 
            this.txt_SN_CheckCode.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SN_CheckCode.Location = new System.Drawing.Point(154, 26);
            this.txt_SN_CheckCode.Name = "txt_SN_CheckCode";
            this.txt_SN_CheckCode.Size = new System.Drawing.Size(320, 35);
            this.txt_SN_CheckCode.TabIndex = 21;
            this.txt_SN_CheckCode.TextChanged += new System.EventHandler(this.txt_SN_CheckCode_TextChanged);
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Location = new System.Drawing.Point(121, 168);
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.Size = new System.Drawing.Size(157, 35);
            this.comboBoxCustomer.TabIndex = 14;
            this.comboBoxCustomer.Visible = false;
            // 
            // numericUpDownBoxQty
            // 
            this.numericUpDownBoxQty.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDownBoxQty.Location = new System.Drawing.Point(388, 27);
            this.numericUpDownBoxQty.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownBoxQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBoxQty.Name = "numericUpDownBoxQty";
            this.numericUpDownBoxQty.Size = new System.Drawing.Size(87, 34);
            this.numericUpDownBoxQty.TabIndex = 13;
            this.numericUpDownBoxQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownBoxQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBoxQty.Visible = false;
            // 
            // textBoxMLenght
            // 
            this.textBoxMLenght.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMLenght.Location = new System.Drawing.Point(185, 126);
            this.textBoxMLenght.Name = "textBoxMLenght";
            this.textBoxMLenght.ReadOnly = true;
            this.textBoxMLenght.Size = new System.Drawing.Size(62, 34);
            this.textBoxMLenght.TabIndex = 11;
            this.textBoxMLenght.Visible = false;
            // 
            // textBoxSNLenght
            // 
            this.textBoxSNLenght.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSNLenght.Location = new System.Drawing.Point(121, 126);
            this.textBoxSNLenght.Name = "textBoxSNLenght";
            this.textBoxSNLenght.ReadOnly = true;
            this.textBoxSNLenght.Size = new System.Drawing.Size(62, 34);
            this.textBoxSNLenght.TabIndex = 11;
            this.textBoxSNLenght.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(303, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "任务数量";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Visible = false;
            // 
            // txt_SN_maxLength
            // 
            this.txt_SN_maxLength.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SN_maxLength.Location = new System.Drawing.Point(154, 69);
            this.txt_SN_maxLength.Name = "txt_SN_maxLength";
            this.txt_SN_maxLength.ReadOnly = true;
            this.txt_SN_maxLength.Size = new System.Drawing.Size(320, 34);
            this.txt_SN_maxLength.TabIndex = 10;
            this.txt_SN_maxLength.Text = "28";
            // 
            // NumberOfScrews
            // 
            this.NumberOfScrews.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NumberOfScrews.Location = new System.Drawing.Point(388, 126);
            this.NumberOfScrews.Name = "NumberOfScrews";
            this.NumberOfScrews.ReadOnly = true;
            this.NumberOfScrews.Size = new System.Drawing.Size(86, 34);
            this.NumberOfScrews.TabIndex = 10;
            this.NumberOfScrews.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumberOfScrews.Visible = false;
            // 
            // textBoxTPID
            // 
            this.textBoxTPID.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTPID.Location = new System.Drawing.Point(388, 69);
            this.textBoxTPID.Name = "textBoxTPID";
            this.textBoxTPID.ReadOnly = true;
            this.textBoxTPID.Size = new System.Drawing.Size(86, 34);
            this.textBoxTPID.TabIndex = 10;
            this.textBoxTPID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxTPID.Visible = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(6, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 27);
            this.label9.TabIndex = 8;
            this.label9.Text = "SN码最大长度";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(9, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 27);
            this.label8.TabIndex = 8;
            this.label8.Text = "任务客户";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(9, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 27);
            this.label4.TabIndex = 8;
            this.label4.Text = "SN码长度";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(303, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 25);
            this.label7.TabIndex = 8;
            this.label7.Text = "螺丝数量";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(322, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "配方号";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "SN     校验码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_SN_Scan
            // 
            this.txt_SN_Scan.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SN_Scan.Location = new System.Drawing.Point(287, 34);
            this.txt_SN_Scan.Name = "txt_SN_Scan";
            this.txt_SN_Scan.Size = new System.Drawing.Size(342, 35);
            this.txt_SN_Scan.TabIndex = 1;
            this.txt_SN_Scan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_SN_Scan_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(122, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 28);
            this.label6.TabIndex = 18;
            this.label6.Text = "请扫描SN码";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::ScrewMachineManagementSystem.Properties.Resources.NewTask;
            this.pictureBox1.Location = new System.Drawing.Point(17, 95);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 98);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // Frm_GetSN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(677, 281);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_GetSN";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormNewTaskOrder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_GetSN_FormClosing);
            this.Load += new System.EventHandler(this.FormNewTaskOrder_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBoxQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownBoxQty;
        private System.Windows.Forms.TextBox textBoxSNLenght;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_SN_maxLength;
        private System.Windows.Forms.TextBox textBoxTPID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_SN_Scan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox NumberOfScrews;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
        private System.Windows.Forms.TextBox textBoxMLenght;
        private System.Windows.Forms.TextBox txt_SN_CheckCode;
    }
}