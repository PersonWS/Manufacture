
namespace sln_BYSJJ
{
    partial class frmJg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJg));
            this.label1 = new System.Windows.Forms.Label();
            this.lblJg = new System.Windows.Forms.Label();
            this.lbltime = new System.Windows.Forms.Label();
            this.timUI = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 70F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(24, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 124);
            this.label1.TabIndex = 0;
            this.label1.Text = "结果:";
            // 
            // lblJg
            // 
            this.lblJg.Font = new System.Drawing.Font("宋体", 80F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblJg.ForeColor = System.Drawing.Color.Red;
            this.lblJg.Location = new System.Drawing.Point(379, 64);
            this.lblJg.Name = "lblJg";
            this.lblJg.Size = new System.Drawing.Size(163, 124);
            this.lblJg.TabIndex = 1;
            this.lblJg.Text = "OK";
            // 
            // lbltime
            // 
            this.lbltime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltime.ForeColor = System.Drawing.Color.Red;
            this.lbltime.Location = new System.Drawing.Point(70, 214);
            this.lbltime.Name = "lbltime";
            this.lbltime.Size = new System.Drawing.Size(139, 23);
            this.lbltime.TabIndex = 2;
            this.lbltime.Text = "10秒后自动关闭";
            // 
            // timUI
            // 
            this.timUI.Enabled = true;
            this.timUI.Tick += new System.EventHandler(this.timUI_Tick);
            // 
            // frmJg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 271);
            this.Controls.Add(this.lbltime);
            this.Controls.Add(this.lblJg);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmJg";
            this.Text = "结果";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmJg_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmJg_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblJg;
        private System.Windows.Forms.Label lbltime;
        public System.Windows.Forms.Timer timUI;
    }
}