namespace SocketClentRemake
{
    partial class Form1
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
            this.Text_Panel = new System.Windows.Forms.Panel();
            this.Account_Read_Only_Text = new System.Windows.Forms.TextBox();
            this.Account_Text = new System.Windows.Forms.TextBox();
            this.Control_Panel = new System.Windows.Forms.Panel();
            this.Choose_Dir_Button = new System.Windows.Forms.Button();
            this.Return_Files_Button = new System.Windows.Forms.Button();
            this.Receive_Files_Button = new System.Windows.Forms.Button();
            this.Log_In_Panel = new System.Windows.Forms.Panel();
            this.Show_Log_In_Statue = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Send_Button = new System.Windows.Forms.Button();
            this.Text_Panel.SuspendLayout();
            this.Control_Panel.SuspendLayout();
            this.Log_In_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Text_Panel
            // 
            this.Text_Panel.Controls.Add(this.Account_Read_Only_Text);
            this.Text_Panel.Controls.Add(this.Account_Text);
            this.Text_Panel.Location = new System.Drawing.Point(25, 28);
            this.Text_Panel.Name = "Text_Panel";
            this.Text_Panel.Size = new System.Drawing.Size(266, 85);
            this.Text_Panel.TabIndex = 13;
            // 
            // Account_Read_Only_Text
            // 
            this.Account_Read_Only_Text.Location = new System.Drawing.Point(17, 17);
            this.Account_Read_Only_Text.Name = "Account_Read_Only_Text";
            this.Account_Read_Only_Text.ReadOnly = true;
            this.Account_Read_Only_Text.Size = new System.Drawing.Size(230, 22);
            this.Account_Read_Only_Text.TabIndex = 4;
            this.Account_Read_Only_Text.Text = "帳號";
            // 
            // Account_Text
            // 
            this.Account_Text.Location = new System.Drawing.Point(17, 45);
            this.Account_Text.Name = "Account_Text";
            this.Account_Text.ReadOnly = true;
            this.Account_Text.Size = new System.Drawing.Size(230, 22);
            this.Account_Text.TabIndex = 1;
            // 
            // Control_Panel
            // 
            this.Control_Panel.Controls.Add(this.Choose_Dir_Button);
            this.Control_Panel.Controls.Add(this.Return_Files_Button);
            this.Control_Panel.Controls.Add(this.Receive_Files_Button);
            this.Control_Panel.Location = new System.Drawing.Point(42, 141);
            this.Control_Panel.Name = "Control_Panel";
            this.Control_Panel.Size = new System.Drawing.Size(218, 152);
            this.Control_Panel.TabIndex = 12;
            // 
            // Choose_Dir_Button
            // 
            this.Choose_Dir_Button.Location = new System.Drawing.Point(17, 12);
            this.Choose_Dir_Button.Name = "Choose_Dir_Button";
            this.Choose_Dir_Button.Size = new System.Drawing.Size(186, 40);
            this.Choose_Dir_Button.TabIndex = 10;
            this.Choose_Dir_Button.Text = "選擇資料夾";
            this.Choose_Dir_Button.UseVisualStyleBackColor = true;
            this.Choose_Dir_Button.Click += new System.EventHandler(this.Choose_Dir_Button_Click);
            // 
            // Return_Files_Button
            // 
            this.Return_Files_Button.Location = new System.Drawing.Point(17, 105);
            this.Return_Files_Button.Name = "Return_Files_Button";
            this.Return_Files_Button.Size = new System.Drawing.Size(186, 40);
            this.Return_Files_Button.TabIndex = 9;
            this.Return_Files_Button.Text = "上傳檔案";
            this.Return_Files_Button.UseVisualStyleBackColor = true;
            this.Return_Files_Button.Click += new System.EventHandler(this.Return_Files_Button_Click);
            // 
            // Receive_Files_Button
            // 
            this.Receive_Files_Button.Location = new System.Drawing.Point(17, 58);
            this.Receive_Files_Button.Name = "Receive_Files_Button";
            this.Receive_Files_Button.Size = new System.Drawing.Size(186, 40);
            this.Receive_Files_Button.TabIndex = 8;
            this.Receive_Files_Button.Text = "下載檔案";
            this.Receive_Files_Button.UseVisualStyleBackColor = true;
            this.Receive_Files_Button.Click += new System.EventHandler(this.Receive_Files_Button_Click);
            // 
            // Log_In_Panel
            // 
            this.Log_In_Panel.Controls.Add(this.Show_Log_In_Statue);
            this.Log_In_Panel.Controls.Add(this.textBox1);
            this.Log_In_Panel.Controls.Add(this.Send_Button);
            this.Log_In_Panel.Location = new System.Drawing.Point(70, 129);
            this.Log_In_Panel.Name = "Log_In_Panel";
            this.Log_In_Panel.Size = new System.Drawing.Size(173, 164);
            this.Log_In_Panel.TabIndex = 11;
            // 
            // Show_Log_In_Statue
            // 
            this.Show_Log_In_Statue.Location = new System.Drawing.Point(15, 27);
            this.Show_Log_In_Statue.Name = "Show_Log_In_Statue";
            this.Show_Log_In_Statue.ReadOnly = true;
            this.Show_Log_In_Statue.Size = new System.Drawing.Size(141, 22);
            this.Show_Log_In_Statue.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 75);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(141, 22);
            this.textBox1.TabIndex = 1;
            // 
            // Send_Button
            // 
            this.Send_Button.Location = new System.Drawing.Point(15, 118);
            this.Send_Button.Name = "Send_Button";
            this.Send_Button.Size = new System.Drawing.Size(144, 22);
            this.Send_Button.TabIndex = 0;
            this.Send_Button.Text = "Send";
            this.Send_Button.UseVisualStyleBackColor = true;
            this.Send_Button.Click += new System.EventHandler(this.Send_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 434);
            this.Controls.Add(this.Log_In_Panel);
            this.Controls.Add(this.Control_Panel);
            this.Controls.Add(this.Text_Panel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Text_Panel.ResumeLayout(false);
            this.Text_Panel.PerformLayout();
            this.Control_Panel.ResumeLayout(false);
            this.Log_In_Panel.ResumeLayout(false);
            this.Log_In_Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Text_Panel;
        private System.Windows.Forms.TextBox Account_Read_Only_Text;
        private System.Windows.Forms.TextBox Account_Text;
        private System.Windows.Forms.Panel Control_Panel;
        private System.Windows.Forms.Button Choose_Dir_Button;
        private System.Windows.Forms.Button Return_Files_Button;
        private System.Windows.Forms.Button Receive_Files_Button;
        private System.Windows.Forms.Panel Log_In_Panel;
        private System.Windows.Forms.TextBox Show_Log_In_Statue;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Send_Button;
    }
}

