namespace BioA.UI
{
    partial class LogOutLogin
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
            this.LabAccount = new System.Windows.Forms.Label();
            this.LabPassword = new System.Windows.Forms.Label();
            this.TextPasswordValue = new System.Windows.Forms.TextBox();
            this.ButLoging = new System.Windows.Forms.Button();
            this.ComBoxAccount = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // LabAccount
            // 
            this.LabAccount.AutoSize = true;
            this.LabAccount.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabAccount.Location = new System.Drawing.Point(59, 53);
            this.LabAccount.Name = "LabAccount";
            this.LabAccount.Size = new System.Drawing.Size(80, 16);
            this.LabAccount.TabIndex = 0;
            this.LabAccount.Text = "账   号：";
            // 
            // LabPassword
            // 
            this.LabPassword.AutoSize = true;
            this.LabPassword.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabPassword.Location = new System.Drawing.Point(59, 118);
            this.LabPassword.Name = "LabPassword";
            this.LabPassword.Size = new System.Drawing.Size(80, 16);
            this.LabPassword.TabIndex = 4;
            this.LabPassword.Text = "密   码：";
            // 
            // TextPasswordValue
            // 
            this.TextPasswordValue.Cursor = System.Windows.Forms.Cursors.Default;
            this.TextPasswordValue.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextPasswordValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TextPasswordValue.Location = new System.Drawing.Point(145, 115);
            this.TextPasswordValue.Name = "TextPasswordValue";
            this.TextPasswordValue.Size = new System.Drawing.Size(149, 26);
            this.TextPasswordValue.TabIndex = 5;
            this.TextPasswordValue.UseSystemPasswordChar = true;
            // 
            // ButLoging
            // 
            this.ButLoging.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButLoging.Location = new System.Drawing.Point(126, 184);
            this.ButLoging.Name = "ButLoging";
            this.ButLoging.Size = new System.Drawing.Size(106, 34);
            this.ButLoging.TabIndex = 6;
            this.ButLoging.Text = " 登   录 ";
            this.ButLoging.UseVisualStyleBackColor = true;
            this.ButLoging.Click += new System.EventHandler(this.ButLoging_Click);
            // 
            // ComBoxAccount
            // 
            this.ComBoxAccount.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ComBoxAccount.FormattingEnabled = true;
            this.ComBoxAccount.Location = new System.Drawing.Point(145, 50);
            this.ComBoxAccount.Name = "ComBoxAccount";
            this.ComBoxAccount.Size = new System.Drawing.Size(149, 24);
            this.ComBoxAccount.TabIndex = 7;
            // 
            // LogOutLogin
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 244);
            this.ControlBox = false;
            this.Controls.Add(this.ComBoxAccount);
            this.Controls.Add(this.ButLoging);
            this.Controls.Add(this.TextPasswordValue);
            this.Controls.Add(this.LabPassword);
            this.Controls.Add(this.LabAccount);
            this.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LogOutLogin";
            this.Text = "用户登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabAccount;
        private System.Windows.Forms.Label LabPassword;
        private System.Windows.Forms.Button ButLoging;
        private System.Windows.Forms.ComboBox ComBoxAccount;
        private System.Windows.Forms.TextBox TextPasswordValue;
    }
}