namespace BioA.UI.Uicomponent.SystemUI.Maintenance
{
    partial class PhotometricCalibration
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
            this.lalPhotometerTest = new DevExpress.XtraEditors.LabelControl();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.btnShut = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // lalPhotometerTest
            // 
            this.lalPhotometerTest.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lalPhotometerTest.Appearance.Options.UseFont = true;
            this.lalPhotometerTest.Location = new System.Drawing.Point(87, 80);
            this.lalPhotometerTest.Name = "lalPhotometerTest";
            this.lalPhotometerTest.Size = new System.Drawing.Size(133, 17);
            this.lalPhotometerTest.TabIndex = 0;
            this.lalPhotometerTest.Text = "确定要光度计校准吗?";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Appearance.Options.UseFont = true;
            this.btnConfirm.Location = new System.Drawing.Point(51, 194);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 40);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定";
            // 
            // btnShut
            // 
            this.btnShut.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShut.Appearance.Options.UseFont = true;
            this.btnShut.Location = new System.Drawing.Point(177, 194);
            this.btnShut.Name = "btnShut";
            this.btnShut.Size = new System.Drawing.Size(75, 40);
            this.btnShut.TabIndex = 2;
            this.btnShut.Text = "关闭";
            // 
            // PhotometricCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 294);
            this.Controls.Add(this.btnShut);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lalPhotometerTest);
            this.Name = "PhotometricCalibration";
            this.Text = "提示！";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lalPhotometerTest;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.SimpleButton btnShut;
    }
}