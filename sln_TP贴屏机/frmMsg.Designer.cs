
namespace sln_TP
{
    partial class frmMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMsg));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clmTIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnBJLB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnDW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnBJXX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timUI = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmTIME,
            this.clnBJLB,
            this.clnDW,
            this.clnBJXX});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(766, 265);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // clmTIME
            // 
            this.clmTIME.DataPropertyName = "时间";
            this.clmTIME.HeaderText = "时间";
            this.clmTIME.Name = "clmTIME";
            this.clmTIME.ReadOnly = true;
            // 
            // clnBJLB
            // 
            this.clnBJLB.DataPropertyName = "报警类别";
            this.clnBJLB.HeaderText = "报警类别";
            this.clnBJLB.Name = "clnBJLB";
            this.clnBJLB.ReadOnly = true;
            // 
            // clnDW
            // 
            this.clnDW.DataPropertyName = "点位";
            this.clnDW.HeaderText = "点位";
            this.clnDW.Name = "clnDW";
            this.clnDW.ReadOnly = true;
            // 
            // clnBJXX
            // 
            this.clnBJXX.DataPropertyName = "报警信息";
            this.clnBJXX.HeaderText = "报警信息";
            this.clnBJXX.Name = "clnBJXX";
            this.clnBJXX.ReadOnly = true;
            // 
            // timUI
            // 
            this.timUI.Tick += new System.EventHandler(this.timUI_Tick);
            // 
            // frmMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 265);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMsg";
            this.Text = "报警提示信息";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMsg_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMsg_FormClosed);
            this.Load += new System.EventHandler(this.frmMsg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timUI;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnBJLB;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnDW;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnBJXX;
    }
}