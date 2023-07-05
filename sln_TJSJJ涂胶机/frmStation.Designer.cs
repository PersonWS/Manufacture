
namespace sln_TJSJJ
{
    partial class frmStation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStation));
            this.timUI = new System.Windows.Forms.Timer(this.components);
            this.gbxFZSF = new System.Windows.Forms.GroupBox();
            this.lbl_BY_ZSFBJ = new System.Windows.Forms.Label();
            this.lbl_BY_YSFBJ = new System.Windows.Forms.Label();
            this.lbl_BY_XSFBJ = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.gbxFZSF.SuspendLayout();
            this.SuspendLayout();
            // 
            // timUI
            // 
            this.timUI.Enabled = true;
            this.timUI.Tick += new System.EventHandler(this.timUI_Tick);
            // 
            // gbxFZSF
            // 
            this.gbxFZSF.Controls.Add(this.lbl_BY_ZSFBJ);
            this.gbxFZSF.Controls.Add(this.lbl_BY_YSFBJ);
            this.gbxFZSF.Controls.Add(this.lbl_BY_XSFBJ);
            this.gbxFZSF.Controls.Add(this.label28);
            this.gbxFZSF.Controls.Add(this.label29);
            this.gbxFZSF.Controls.Add(this.label30);
            this.gbxFZSF.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxFZSF.Location = new System.Drawing.Point(12, 12);
            this.gbxFZSF.Name = "gbxFZSF";
            this.gbxFZSF.Size = new System.Drawing.Size(300, 467);
            this.gbxFZSF.TabIndex = 12;
            this.gbxFZSF.TabStop = false;
            this.gbxFZSF.Text = "工位状态";
            // 
            // lbl_BY_ZSFBJ
            // 
            this.lbl_BY_ZSFBJ.BackColor = System.Drawing.Color.White;
            this.lbl_BY_ZSFBJ.Location = new System.Drawing.Point(221, 120);
            this.lbl_BY_ZSFBJ.Name = "lbl_BY_ZSFBJ";
            this.lbl_BY_ZSFBJ.Size = new System.Drawing.Size(25, 25);
            this.lbl_BY_ZSFBJ.TabIndex = 1;
            // 
            // lbl_BY_YSFBJ
            // 
            this.lbl_BY_YSFBJ.BackColor = System.Drawing.Color.White;
            this.lbl_BY_YSFBJ.Location = new System.Drawing.Point(221, 80);
            this.lbl_BY_YSFBJ.Name = "lbl_BY_YSFBJ";
            this.lbl_BY_YSFBJ.Size = new System.Drawing.Size(25, 25);
            this.lbl_BY_YSFBJ.TabIndex = 1;
            // 
            // lbl_BY_XSFBJ
            // 
            this.lbl_BY_XSFBJ.BackColor = System.Drawing.Color.White;
            this.lbl_BY_XSFBJ.Location = new System.Drawing.Point(221, 40);
            this.lbl_BY_XSFBJ.Name = "lbl_BY_XSFBJ";
            this.lbl_BY_XSFBJ.Size = new System.Drawing.Size(25, 25);
            this.lbl_BY_XSFBJ.TabIndex = 1;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label28.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.Location = new System.Drawing.Point(25, 40);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(180, 25);
            this.label28.TabIndex = 0;
            this.label28.Text = "X轴伺服报警";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label29.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.Location = new System.Drawing.Point(25, 80);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(180, 25);
            this.label29.TabIndex = 0;
            this.label29.Text = "Y轴伺服报警";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label30.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.Location = new System.Drawing.Point(25, 120);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(180, 25);
            this.label30.TabIndex = 0;
            this.label30.Text = "Z轴伺服报警";
            // 
            // frmStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 630);
            this.Controls.Add(this.gbxFZSF);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStation";
            this.Text = "工位状态";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStation_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmStation_FormClosed);
            this.Load += new System.EventHandler(this.frmStation_Load);
            this.gbxFZSF.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timUI;
        private System.Windows.Forms.GroupBox gbxFZSF;
        private System.Windows.Forms.Label lbl_BY_ZSFBJ;
        private System.Windows.Forms.Label lbl_BY_YSFBJ;
        private System.Windows.Forms.Label lbl_BY_XSFBJ;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
    }
}