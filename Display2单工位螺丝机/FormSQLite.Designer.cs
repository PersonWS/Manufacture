namespace ScrewMachineManagementSystem
{
    partial class FormSQLite
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
            this.buttonTask = new System.Windows.Forms.Button();
            this.buttonStep = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonTask
            // 
            this.buttonTask.Location = new System.Drawing.Point(63, 44);
            this.buttonTask.Name = "buttonTask";
            this.buttonTask.Size = new System.Drawing.Size(88, 32);
            this.buttonTask.TabIndex = 0;
            this.buttonTask.Text = "task";
            this.buttonTask.UseVisualStyleBackColor = true;
            this.buttonTask.Click += new System.EventHandler(this.buttonTask_Click);
            // 
            // buttonStep
            // 
            this.buttonStep.Location = new System.Drawing.Point(186, 44);
            this.buttonStep.Name = "buttonStep";
            this.buttonStep.Size = new System.Drawing.Size(88, 32);
            this.buttonStep.TabIndex = 0;
            this.buttonStep.Text = "step";
            this.buttonStep.UseVisualStyleBackColor = true;
            this.buttonStep.Click += new System.EventHandler(this.buttonStep_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(65, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "read task";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormSQLite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonStep);
            this.Controls.Add(this.buttonTask);
            this.Name = "FormSQLite";
            this.Text = "FormSQLite";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonTask;
        private System.Windows.Forms.Button buttonStep;
        private System.Windows.Forms.Button button1;
    }
}