namespace ScrewMachineManagementSystem.CenterControl
{
    partial class CenterDemo
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_snRequest = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lab_isManufacture = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lab_ok = new System.Windows.Forms.Label();
            this.lab_ng = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lab_PLC_ConnectState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 408);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(762, 131);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "message:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(638, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 68);
            this.button1.TabIndex = 2;
            this.button1.Text = "StartCenterControl";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "SN码请求[1.DBX0.0]：";
            // 
            // lab_snRequest
            // 
            this.lab_snRequest.AutoSize = true;
            this.lab_snRequest.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_snRequest.Location = new System.Drawing.Point(134, 66);
            this.lab_snRequest.Name = "lab_snRequest";
            this.lab_snRequest.Size = new System.Drawing.Size(28, 19);
            this.lab_snRequest.TabIndex = 4;
            this.lab_snRequest.Text = "●";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "是否加工[1.DBX0.1]：";
            // 
            // lab_isManufacture
            // 
            this.lab_isManufacture.AutoSize = true;
            this.lab_isManufacture.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_isManufacture.Location = new System.Drawing.Point(134, 104);
            this.lab_isManufacture.Name = "lab_isManufacture";
            this.lab_isManufacture.Size = new System.Drawing.Size(28, 19);
            this.lab_isManufacture.TabIndex = 6;
            this.lab_isManufacture.Text = "●";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "结果 OK [1.DBX0.2]：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "结果 NG [1.DBX0.3]：";
            // 
            // lab_ok
            // 
            this.lab_ok.AutoSize = true;
            this.lab_ok.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ok.Location = new System.Drawing.Point(134, 144);
            this.lab_ok.Name = "lab_ok";
            this.lab_ok.Size = new System.Drawing.Size(28, 19);
            this.lab_ok.TabIndex = 9;
            this.lab_ok.Text = "●";
            // 
            // lab_ng
            // 
            this.lab_ng.AutoSize = true;
            this.lab_ng.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ng.Location = new System.Drawing.Point(134, 180);
            this.lab_ng.Name = "lab_ng";
            this.lab_ng.Size = new System.Drawing.Size(28, 19);
            this.lab_ng.TabIndex = 10;
            this.lab_ng.Text = "●";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "PLC_Connect_state：";
            // 
            // lab_PLC_ConnectState
            // 
            this.lab_PLC_ConnectState.AutoSize = true;
            this.lab_PLC_ConnectState.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_PLC_ConnectState.Location = new System.Drawing.Point(134, 28);
            this.lab_PLC_ConnectState.Name = "lab_PLC_ConnectState";
            this.lab_PLC_ConnectState.Size = new System.Drawing.Size(28, 19);
            this.lab_PLC_ConnectState.TabIndex = 12;
            this.lab_PLC_ConnectState.Text = "●";
            // 
            // CenterDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 551);
            this.Controls.Add(this.lab_PLC_ConnectState);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lab_ng);
            this.Controls.Add(this.lab_ok);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lab_isManufacture);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lab_snRequest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "CenterDemo";
            this.Text = "CenterDemo|螺丝机(装饰条)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lab_snRequest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lab_isManufacture;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lab_ok;
        private System.Windows.Forms.Label lab_ng;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lab_PLC_ConnectState;
    }
}