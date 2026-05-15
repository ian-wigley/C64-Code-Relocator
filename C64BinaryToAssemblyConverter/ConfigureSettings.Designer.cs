
namespace C64BinaryToAssemblyConverter
{
    partial class ConfigureSettings
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
            this.BranchLabel = new System.Windows.Forms.Label();
            this.JumpLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.NumberOfBytesPerLine = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BranchLabel
            // 
            this.BranchLabel.AutoSize = true;
            this.BranchLabel.Location = new System.Drawing.Point(26, 18);
            this.BranchLabel.Name = "BranchLabel";
            this.BranchLabel.Size = new System.Drawing.Size(70, 13);
            this.BranchLabel.TabIndex = 0;
            this.BranchLabel.Text = "Branch Label";
            // 
            // JumpLabel
            // 
            this.JumpLabel.AutoSize = true;
            this.JumpLabel.Location = new System.Drawing.Point(26, 44);
            this.JumpLabel.Name = "JumpLabel";
            this.JumpLabel.Size = new System.Drawing.Size(61, 13);
            this.JumpLabel.TabIndex = 1;
            this.JumpLabel.Text = "Jump Label";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(163, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(163, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(163, 70);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 4;
            // 
            // NumberOfBytesPerLine
            // 
            this.NumberOfBytesPerLine.AutoSize = true;
            this.NumberOfBytesPerLine.Location = new System.Drawing.Point(26, 73);
            this.NumberOfBytesPerLine.Name = "NumberOfBytesPerLine";
            this.NumberOfBytesPerLine.Size = new System.Drawing.Size(127, 13);
            this.NumberOfBytesPerLine.TabIndex = 5;
            this.NumberOfBytesPerLine.Text = "Number of Bytes Per Line";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(101, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Okay";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonClick);
            // 
            // ConfigureSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 141);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NumberOfBytesPerLine);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.JumpLabel);
            this.Controls.Add(this.BranchLabel);
            this.Name = "ConfigureSettings";
            this.Text = "ConfigureSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BranchLabel;
        private System.Windows.Forms.Label JumpLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label NumberOfBytesPerLine;
        private System.Windows.Forms.Button button1;
    }
}