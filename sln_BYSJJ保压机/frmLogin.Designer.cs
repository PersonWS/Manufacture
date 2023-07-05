
namespace sln_BYSJJ
{
    partial class frmLogin
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
            this.labelCancel = new System.Windows.Forms.Label();
            this.labelSetParams = new System.Windows.Forms.Label();
            this.labelUserID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCancel
            // 
            this.labelCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCancel.Location = new System.Drawing.Point(34, 165);
            this.labelCancel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCancel.Name = "labelCancel";
            this.labelCancel.Size = new System.Drawing.Size(203, 41);
            this.labelCancel.TabIndex = 2;
            this.labelCancel.Text = "退出[Esc]";
            this.labelCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCancel.Click += new System.EventHandler(this.labelCancel_Click);
            // 
            // labelSetParams
            // 
            this.labelSetParams.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelSetParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSetParams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSetParams.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSetParams.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSetParams.Location = new System.Drawing.Point(34, 110);
            this.labelSetParams.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSetParams.Name = "labelSetParams";
            this.labelSetParams.Size = new System.Drawing.Size(203, 41);
            this.labelSetParams.TabIndex = 2;
            this.labelSetParams.Text = "登录[Enter]";
            this.labelSetParams.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelSetParams.Click += new System.EventHandler(this.labelSetParams_Click);
            // 
            // labelUserID
            // 
            this.labelUserID.BackColor = System.Drawing.Color.White;
            this.labelUserID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelUserID.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelUserID.Location = new System.Drawing.Point(69, 44);
            this.labelUserID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(168, 39);
            this.labelUserID.TabIndex = 3;
            this.labelUserID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(30, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "格式：AABBBB(类别-用户编号)";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑 Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(250, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "系统登录";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Font = new System.Drawing.Font("Wingdings", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label3.Location = new System.Drawing.Point(36, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 39);
            this.label3.TabIndex = 7;
            this.label3.Text = "J";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelCancel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelSetParams);
            this.groupBox1.Controls.Add(this.labelUserID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(36, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 242);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::sln_BYSJJ.Properties.Resources.logo_1;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(36, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(267, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(346, 372);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统登录";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLogin_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCancel;
        private System.Windows.Forms.Label labelSetParams;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}