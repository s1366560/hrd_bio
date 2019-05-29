namespace BioA.UI
{
    partial class SMPResultSend
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
            this.CESendCurrentSample = new DevExpress.XtraEditors.CheckEdit();
            this.CESendSelectedSample = new DevExpress.XtraEditors.CheckEdit();
            this.CESendAllSample = new DevExpress.XtraEditors.CheckEdit();
            this.ButPrintState = new System.Windows.Forms.Button();
            this.ButReturn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CESendCurrentSample.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CESendSelectedSample.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CESendAllSample.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // CESendCurrentSample
            // 
            this.CESendCurrentSample.EditValue = true;
            this.CESendCurrentSample.Location = new System.Drawing.Point(37, 21);
            this.CESendCurrentSample.Name = "CESendCurrentSample";
            this.CESendCurrentSample.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CESendCurrentSample.Properties.Appearance.Options.UseFont = true;
            this.CESendCurrentSample.Properties.Caption = "  发送当前样本。";
            this.CESendCurrentSample.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.CESendCurrentSample.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.CESendCurrentSample.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.CESendCurrentSample.Size = new System.Drawing.Size(146, 21);
            this.CESendCurrentSample.TabIndex = 3;
            this.CESendCurrentSample.Click += new System.EventHandler(this.CECurrentSample_Click);
            // 
            // CESendSelectedSample
            // 
            this.CESendSelectedSample.Location = new System.Drawing.Point(37, 64);
            this.CESendSelectedSample.Name = "CESendSelectedSample";
            this.CESendSelectedSample.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CESendSelectedSample.Properties.Appearance.Options.UseFont = true;
            this.CESendSelectedSample.Properties.Caption = "  发送已选样本。";
            this.CESendSelectedSample.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.CESendSelectedSample.Size = new System.Drawing.Size(146, 21);
            this.CESendSelectedSample.TabIndex = 4;
            this.CESendSelectedSample.Click += new System.EventHandler(this.CESelectedSample_Click);
            // 
            // CESendAllSample
            // 
            this.CESendAllSample.Location = new System.Drawing.Point(37, 109);
            this.CESendAllSample.Name = "CESendAllSample";
            this.CESendAllSample.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CESendAllSample.Properties.Appearance.Options.UseFont = true;
            this.CESendAllSample.Properties.Caption = "  发送所有样本。";
            this.CESendAllSample.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.CESendAllSample.Size = new System.Drawing.Size(146, 21);
            this.CESendAllSample.TabIndex = 5;
            this.CESendAllSample.Click += new System.EventHandler(this.CEAllSample_Click);
            // 
            // ButPrintState
            // 
            this.ButPrintState.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButPrintState.Location = new System.Drawing.Point(164, 136);
            this.ButPrintState.Name = "ButPrintState";
            this.ButPrintState.Size = new System.Drawing.Size(88, 32);
            this.ButPrintState.TabIndex = 9;
            this.ButPrintState.Text = "发送";
            this.ButPrintState.UseVisualStyleBackColor = true;
            this.ButPrintState.Click += new System.EventHandler(this.ButPrintState_Click);
            // 
            // ButReturn
            // 
            this.ButReturn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButReturn.Location = new System.Drawing.Point(258, 136);
            this.ButReturn.Name = "ButReturn";
            this.ButReturn.Size = new System.Drawing.Size(88, 32);
            this.ButReturn.TabIndex = 10;
            this.ButReturn.Text = "返回";
            this.ButReturn.UseVisualStyleBackColor = true;
            this.ButReturn.Click += new System.EventHandler(this.ButReturn_Click);
            // 
            // SMPResultSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 175);
            this.Controls.Add(this.ButReturn);
            this.Controls.Add(this.ButPrintState);
            this.Controls.Add(this.CESendAllSample);
            this.Controls.Add(this.CESendSelectedSample);
            this.Controls.Add(this.CESendCurrentSample);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SMPResultSend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.CESendCurrentSample.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CESendSelectedSample.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CESendAllSample.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.CheckEdit CESendCurrentSample;
        private DevExpress.XtraEditors.CheckEdit CESendSelectedSample;
        private DevExpress.XtraEditors.CheckEdit CESendAllSample;
        private System.Windows.Forms.Button ButPrintState;
        private System.Windows.Forms.Button ButReturn;
    }
}