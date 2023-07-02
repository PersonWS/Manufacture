
namespace ScrewMachineManagementSystem
{
    partial class FormUser
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelDelete = new System.Windows.Forms.Label();
            this.labelEdit = new System.Windows.Forms.Label();
            this.labelAdd = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.labelDelete);
            this.panel1.Controls.Add(this.labelEdit);
            this.panel1.Controls.Add(this.labelAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(912, 47);
            this.panel1.TabIndex = 1;
            // 
            // labelDelete
            // 
            this.labelDelete.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelDelete.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDelete.Location = new System.Drawing.Point(563, 9);
            this.labelDelete.Name = "labelDelete";
            this.labelDelete.Size = new System.Drawing.Size(95, 31);
            this.labelDelete.TabIndex = 4;
            this.labelDelete.Text = "删除";
            this.labelDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelDelete.Click += new System.EventHandler(this.labelDelete_Click);
            // 
            // labelEdit
            // 
            this.labelEdit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelEdit.Enabled = false;
            this.labelEdit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelEdit.Location = new System.Drawing.Point(462, 9);
            this.labelEdit.Name = "labelEdit";
            this.labelEdit.Size = new System.Drawing.Size(95, 31);
            this.labelEdit.TabIndex = 4;
            this.labelEdit.Text = "编辑";
            this.labelEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelEdit.Click += new System.EventHandler(this.labelEdit_Click);
            // 
            // labelAdd
            // 
            this.labelAdd.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelAdd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelAdd.Location = new System.Drawing.Point(361, 9);
            this.labelAdd.Name = "labelAdd";
            this.labelAdd.Size = new System.Drawing.Size(95, 31);
            this.labelAdd.TabIndex = 3;
            this.labelAdd.Text = "新用户";
            this.labelAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelAdd.Click += new System.EventHandler(this.labelAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(912, 511);
            this.panel2.TabIndex = 2;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(912, 511);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // FormUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 558);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUser";
            this.Text = "用户信息";
            this.Load += new System.EventHandler(this.FormUser_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label labelAdd;
        private System.Windows.Forms.Label labelEdit;
        private System.Windows.Forms.Label labelDelete;
    }
}