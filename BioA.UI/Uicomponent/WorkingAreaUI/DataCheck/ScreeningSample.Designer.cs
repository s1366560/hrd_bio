namespace BioA.UI.Uicomponent.WorkingAreaUI.DataCheck
{
    partial class ScreeningSample
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
            this.btnSever = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.chkInitialSampleTest = new DevExpress.XtraEditors.CheckEdit();
            this.chkOngoingSampleTes = new DevExpress.XtraEditors.CheckEdit();
            this.chkCompletedSampleTest = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chkInitialSampleTest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOngoingSampleTes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompletedSampleTest.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSever
            // 
            this.btnSever.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSever.Appearance.Options.UseFont = true;
            this.btnSever.Location = new System.Drawing.Point(93, 335);
            this.btnSever.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSever.Name = "btnSever";
            this.btnSever.Size = new System.Drawing.Size(83, 68);
            this.btnSever.TabIndex = 9;
            this.btnSever.Text = "保存";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Location = new System.Drawing.Point(251, 335);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(83, 68);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "取消";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // chkInitialSampleTest
            // 
            this.chkInitialSampleTest.Location = new System.Drawing.Point(114, 243);
            this.chkInitialSampleTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkInitialSampleTest.Name = "chkInitialSampleTest";
            this.chkInitialSampleTest.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInitialSampleTest.Properties.Appearance.Options.UseFont = true;
            this.chkInitialSampleTest.Properties.Caption = "未开始的样本测试复选框";
            this.chkInitialSampleTest.Size = new System.Drawing.Size(206, 21);
            this.chkInitialSampleTest.TabIndex = 7;
            // 
            // chkOngoingSampleTes
            // 
            this.chkOngoingSampleTes.Location = new System.Drawing.Point(114, 171);
            this.chkOngoingSampleTes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkOngoingSampleTes.Name = "chkOngoingSampleTes";
            this.chkOngoingSampleTes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOngoingSampleTes.Properties.Appearance.Options.UseFont = true;
            this.chkOngoingSampleTes.Properties.Caption = "正在进行的样本测试复选框";
            this.chkOngoingSampleTes.Size = new System.Drawing.Size(221, 21);
            this.chkOngoingSampleTes.TabIndex = 6;
            // 
            // chkCompletedSampleTest
            // 
            this.chkCompletedSampleTest.Location = new System.Drawing.Point(114, 94);
            this.chkCompletedSampleTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkCompletedSampleTest.Name = "chkCompletedSampleTest";
            this.chkCompletedSampleTest.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompletedSampleTest.Properties.Appearance.Options.UseFont = true;
            this.chkCompletedSampleTest.Properties.Caption = "已完成的样本测试复选框";
            this.chkCompletedSampleTest.Size = new System.Drawing.Size(206, 21);
            this.chkCompletedSampleTest.TabIndex = 5;
            // 
            // ScreeningSample
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 452);
            this.Controls.Add(this.btnSever);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.chkInitialSampleTest);
            this.Controls.Add(this.chkOngoingSampleTes);
            this.Controls.Add(this.chkCompletedSampleTest);
            this.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScreeningSample";
            this.Text = "筛选样本";
            ((System.ComponentModel.ISupportInitialize)(this.chkInitialSampleTest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOngoingSampleTes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompletedSampleTest.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSever;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.CheckEdit chkInitialSampleTest;
        private DevExpress.XtraEditors.CheckEdit chkOngoingSampleTes;
        private DevExpress.XtraEditors.CheckEdit chkCompletedSampleTest;
    }
}