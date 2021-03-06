﻿namespace BioA.UI
{
    partial class UserEdit
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
            this.TxtName = new DevExpress.XtraEditors.TextEdit();
            this.TxtAffirmPassword = new DevExpress.XtraEditors.TextEdit();
            this.TxtPassword = new DevExpress.XtraEditors.TextEdit();
            this.TxtAccount = new DevExpress.XtraEditors.TextEdit();
            this.LabAffirmPassw = new System.Windows.Forms.Label();
            this.LabUserPassword = new System.Windows.Forms.Label();
            this.LabAccountPropmt = new System.Windows.Forms.Label();
            this.LabName = new System.Windows.Forms.Label();
            this.LabAffirmPassword = new System.Windows.Forms.Label();
            this.LabPassword = new System.Windows.Forms.Label();
            this.LabUserName = new System.Windows.Forms.Label();
            this.LabAccount = new System.Windows.Forms.Label();
            this.ButCancel = new System.Windows.Forms.Button();
            this.ButSave = new System.Windows.Forms.Button();
            this.TxtOldPassword = new DevExpress.XtraEditors.TextEdit();
            this.LabOldPassword = new System.Windows.Forms.Label();
            this.LabOldPswDisplay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TxtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAffirmPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtOldPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtName
            // 
            this.TxtName.Location = new System.Drawing.Point(142, 43);
            this.TxtName.Name = "TxtName";
            this.TxtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtName.Properties.Appearance.Options.UseFont = true;
            this.TxtName.Properties.MaxLength = 20;
            this.TxtName.Size = new System.Drawing.Size(164, 24);
            this.TxtName.TabIndex = 1;
            // 
            // TxtAffirmPassword
            // 
            this.TxtAffirmPassword.Location = new System.Drawing.Point(142, 254);
            this.TxtAffirmPassword.Name = "TxtAffirmPassword";
            this.TxtAffirmPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAffirmPassword.Properties.Appearance.Options.UseFont = true;
            this.TxtAffirmPassword.Properties.MaxLength = 25;
            this.TxtAffirmPassword.Properties.UseSystemPasswordChar = true;
            this.TxtAffirmPassword.Size = new System.Drawing.Size(164, 24);
            this.TxtAffirmPassword.TabIndex = 5;
            this.TxtAffirmPassword.EditValueChanged += new System.EventHandler(this.TxtAffirmPassword_EditValueChanged);
            this.TxtAffirmPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtOldPassword_KeyPress);
            // 
            // TxtPassword
            // 
            this.TxtPassword.Cursor = System.Windows.Forms.Cursors.Default;
            this.TxtPassword.EditValue = "";
            this.TxtPassword.Location = new System.Drawing.Point(142, 203);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPassword.Properties.Appearance.Options.UseFont = true;
            this.TxtPassword.Properties.MaxLength = 25;
            this.TxtPassword.Properties.UseSystemPasswordChar = true;
            this.TxtPassword.Size = new System.Drawing.Size(164, 24);
            this.TxtPassword.TabIndex = 4;
            this.TxtPassword.EditValueChanged += new System.EventHandler(this.TxtPassword_EditValueChanged);
            this.TxtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtOldPassword_KeyPress);
            // 
            // TxtAccount
            // 
            this.TxtAccount.Location = new System.Drawing.Point(144, 96);
            this.TxtAccount.Name = "TxtAccount";
            this.TxtAccount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAccount.Properties.Appearance.Options.UseFont = true;
            this.TxtAccount.Properties.MaxLength = 20;
            this.TxtAccount.Size = new System.Drawing.Size(162, 24);
            this.TxtAccount.TabIndex = 2;
            // 
            // LabAffirmPassw
            // 
            this.LabAffirmPassw.AutoSize = true;
            this.LabAffirmPassw.Font = new System.Drawing.Font("SimHei", 9F);
            this.LabAffirmPassw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LabAffirmPassw.Location = new System.Drawing.Point(121, 281);
            this.LabAffirmPassw.Name = "LabAffirmPassw";
            this.LabAffirmPassw.Size = new System.Drawing.Size(0, 12);
            this.LabAffirmPassw.TabIndex = 28;
            // 
            // LabUserPassword
            // 
            this.LabUserPassword.AutoSize = true;
            this.LabUserPassword.Font = new System.Drawing.Font("SimHei", 9F);
            this.LabUserPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LabUserPassword.Location = new System.Drawing.Point(121, 231);
            this.LabUserPassword.Name = "LabUserPassword";
            this.LabUserPassword.Size = new System.Drawing.Size(0, 12);
            this.LabUserPassword.TabIndex = 27;
            // 
            // LabAccountPropmt
            // 
            this.LabAccountPropmt.AutoSize = true;
            this.LabAccountPropmt.Font = new System.Drawing.Font("SimHei", 9F);
            this.LabAccountPropmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LabAccountPropmt.Location = new System.Drawing.Point(120, 124);
            this.LabAccountPropmt.Name = "LabAccountPropmt";
            this.LabAccountPropmt.Size = new System.Drawing.Size(0, 12);
            this.LabAccountPropmt.TabIndex = 26;
            // 
            // LabName
            // 
            this.LabName.AutoSize = true;
            this.LabName.Font = new System.Drawing.Font("SimHei", 9F);
            this.LabName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LabName.Location = new System.Drawing.Point(120, 69);
            this.LabName.Name = "LabName";
            this.LabName.Size = new System.Drawing.Size(0, 12);
            this.LabName.TabIndex = 25;
            // 
            // LabAffirmPassword
            // 
            this.LabAffirmPassword.AutoSize = true;
            this.LabAffirmPassword.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabAffirmPassword.Location = new System.Drawing.Point(59, 258);
            this.LabAffirmPassword.Name = "LabAffirmPassword";
            this.LabAffirmPassword.Size = new System.Drawing.Size(77, 14);
            this.LabAffirmPassword.TabIndex = 24;
            this.LabAffirmPassword.Text = "确认密码：";
            // 
            // LabPassword
            // 
            this.LabPassword.AutoSize = true;
            this.LabPassword.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabPassword.Location = new System.Drawing.Point(59, 208);
            this.LabPassword.Name = "LabPassword";
            this.LabPassword.Size = new System.Drawing.Size(77, 14);
            this.LabPassword.TabIndex = 23;
            this.LabPassword.Text = "新 密 码：";
            // 
            // LabUserName
            // 
            this.LabUserName.AutoSize = true;
            this.LabUserName.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabUserName.Location = new System.Drawing.Point(59, 47);
            this.LabUserName.Name = "LabUserName";
            this.LabUserName.Size = new System.Drawing.Size(77, 14);
            this.LabUserName.TabIndex = 22;
            this.LabUserName.Text = "用户名称：";
            // 
            // LabAccount
            // 
            this.LabAccount.AutoSize = true;
            this.LabAccount.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabAccount.Location = new System.Drawing.Point(59, 101);
            this.LabAccount.Name = "LabAccount";
            this.LabAccount.Size = new System.Drawing.Size(77, 14);
            this.LabAccount.TabIndex = 21;
            this.LabAccount.Text = "账    号：";
            // 
            // ButCancel
            // 
            this.ButCancel.Location = new System.Drawing.Point(202, 306);
            this.ButCancel.Name = "ButCancel";
            this.ButCancel.Size = new System.Drawing.Size(78, 37);
            this.ButCancel.TabIndex = 20;
            this.ButCancel.Text = "取消";
            this.ButCancel.UseVisualStyleBackColor = true;
            this.ButCancel.Click += new System.EventHandler(this.ButCancel_Click);
            // 
            // ButSave
            // 
            this.ButSave.Cursor = System.Windows.Forms.Cursors.Default;
            this.ButSave.Location = new System.Drawing.Point(94, 306);
            this.ButSave.Name = "ButSave";
            this.ButSave.Size = new System.Drawing.Size(81, 37);
            this.ButSave.TabIndex = 19;
            this.ButSave.Text = "保存";
            this.ButSave.UseVisualStyleBackColor = true;
            this.ButSave.Click += new System.EventHandler(this.ButSave_Click);
            // 
            // TxtOldPassword
            // 
            this.TxtOldPassword.Cursor = System.Windows.Forms.Cursors.Default;
            this.TxtOldPassword.EditValue = "";
            this.TxtOldPassword.Location = new System.Drawing.Point(142, 149);
            this.TxtOldPassword.Name = "TxtOldPassword";
            this.TxtOldPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOldPassword.Properties.Appearance.Options.UseFont = true;
            this.TxtOldPassword.Properties.MaxLength = 25;
            this.TxtOldPassword.Properties.UseSystemPasswordChar = true;
            this.TxtOldPassword.Size = new System.Drawing.Size(164, 24);
            this.TxtOldPassword.TabIndex = 3;
            this.TxtOldPassword.EditValueChanged += new System.EventHandler(this.TxtOldPassword_EditValueChanged);
            this.TxtOldPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtOldPassword_KeyPress);
            // 
            // LabOldPassword
            // 
            this.LabOldPassword.AutoSize = true;
            this.LabOldPassword.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabOldPassword.Location = new System.Drawing.Point(59, 154);
            this.LabOldPassword.Name = "LabOldPassword";
            this.LabOldPassword.Size = new System.Drawing.Size(77, 14);
            this.LabOldPassword.TabIndex = 33;
            this.LabOldPassword.Text = "旧 密 码：";
            // 
            // LabOldPswDisplay
            // 
            this.LabOldPswDisplay.AutoSize = true;
            this.LabOldPswDisplay.Font = new System.Drawing.Font("SimHei", 9F);
            this.LabOldPswDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LabOldPswDisplay.Location = new System.Drawing.Point(120, 179);
            this.LabOldPswDisplay.Name = "LabOldPswDisplay";
            this.LabOldPswDisplay.Size = new System.Drawing.Size(0, 12);
            this.LabOldPswDisplay.TabIndex = 35;
            // 
            // UserEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 357);
            this.Controls.Add(this.LabOldPswDisplay);
            this.Controls.Add(this.TxtOldPassword);
            this.Controls.Add(this.LabOldPassword);
            this.Controls.Add(this.TxtName);
            this.Controls.Add(this.TxtAffirmPassword);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtAccount);
            this.Controls.Add(this.LabAffirmPassw);
            this.Controls.Add(this.LabUserPassword);
            this.Controls.Add(this.LabAccountPropmt);
            this.Controls.Add(this.LabName);
            this.Controls.Add(this.LabAffirmPassword);
            this.Controls.Add(this.LabPassword);
            this.Controls.Add(this.LabUserName);
            this.Controls.Add(this.LabAccount);
            this.Controls.Add(this.ButCancel);
            this.Controls.Add(this.ButSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserEdit";
            this.Text = "用户编辑";
            this.Load += new System.EventHandler(this.UserEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TxtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAffirmPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtOldPassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit TxtName;
        private DevExpress.XtraEditors.TextEdit TxtAffirmPassword;
        private DevExpress.XtraEditors.TextEdit TxtPassword;
        private DevExpress.XtraEditors.TextEdit TxtAccount;
        private System.Windows.Forms.Label LabAffirmPassw;
        private System.Windows.Forms.Label LabUserPassword;
        private System.Windows.Forms.Label LabAccountPropmt;
        private System.Windows.Forms.Label LabName;
        private System.Windows.Forms.Label LabAffirmPassword;
        private System.Windows.Forms.Label LabPassword;
        private System.Windows.Forms.Label LabUserName;
        private System.Windows.Forms.Label LabAccount;
        private System.Windows.Forms.Button ButCancel;
        private System.Windows.Forms.Button ButSave;
        private DevExpress.XtraEditors.TextEdit TxtOldPassword;
        private System.Windows.Forms.Label LabOldPassword;
        private System.Windows.Forms.Label LabOldPswDisplay;
    }
}