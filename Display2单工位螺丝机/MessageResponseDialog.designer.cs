using System.Windows.Forms;
namespace ScrewMachineManagementSystem
{
    partial class MessageResponseDialog
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelMsg = new System.Windows.Forms.Label();
            this.cbtOK = new System.Windows.Forms.Button();
            this.cbtNo = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // labelMsg
            // 
            this.labelMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelMsg.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMsg.ForeColor = System.Drawing.Color.Red;
            this.labelMsg.Location = new System.Drawing.Point(12, 34);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(484, 83);
            this.labelMsg.TabIndex = 2;
            this.labelMsg.Text = "确定要退出吗？";
            this.labelMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbtOK
            // 
            this.cbtOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbtOK.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtOK.Location = new System.Drawing.Point(390, 127);
            this.cbtOK.Name = "cbtOK";
            this.cbtOK.Size = new System.Drawing.Size(100, 34);
            this.cbtOK.TabIndex = 1;
            this.cbtOK.Text = "是[Enter]";
            this.cbtOK.UseVisualStyleBackColor = false;
            this.cbtOK.Click += new System.EventHandler(this.cbtOK_Click);
            this.cbtOK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbtOK_KeyDown);
            // 
            // cbtNo
            // 
            this.cbtNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbtNo.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtNo.Location = new System.Drawing.Point(284, 127);
            this.cbtNo.Name = "cbtNo";
            this.cbtNo.Size = new System.Drawing.Size(100, 34);
            this.cbtNo.TabIndex = 2;
            this.cbtNo.Text = "否[Esc]";
            this.cbtNo.UseVisualStyleBackColor = false;
            this.cbtNo.Click += new System.EventHandler(this.cbtNo_Click);
            this.cbtNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbtNo_KeyDown);
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.Blue;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(508, 27);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "    系统提示";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Location = new System.Drawing.Point(4, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 139);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // MessageResponseDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(508, 171);
            this.ControlBox = false;
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.cbtNo);
            this.Controls.Add(this.cbtOK);
            this.Controls.Add(this.labelMsg);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageResponseDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统提示";
            this.Load += new System.EventHandler(this.MessageDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Label labelMsg;
        private Button cbtOK;
        private Button cbtNo;
        private Label labelTitle;
        private GroupBox groupBox1;
    }
}