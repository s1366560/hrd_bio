namespace BioA.UI
{
    partial class PrintType
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
            this.CECurrentSample = new DevExpress.XtraEditors.CheckEdit();
            this.CESelectedSample = new DevExpress.XtraEditors.CheckEdit();
            this.CEAllSample = new DevExpress.XtraEditors.CheckEdit();
            this.ButPrintState = new System.Windows.Forms.Button();
            this.ButReturn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CECurrentSample.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CESelectedSample.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CEAllSample.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // CECurrentSample
            // 
            this.CECurrentSample.EditValue = true;
            this.CECurrentSample.Location = new System.Drawing.Point(37, 21);
            this.CECurrentSample.Name = "CECurrentSample";
            this.CECurrentSample.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CECurrentSample.Properties.Appearance.Options.UseFont = true;
            this.CECurrentSample.Properties.Caption = "  打印当前样本。";
            this.CECurrentSample.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.CECurrentSample.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.CECurrentSample.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.CECurrentSample.Size = new System.Drawing.Size(146, 21);
            this.CECurrentSample.TabIndex = 3;
            this.CECurrentSample.Click += new System.EventHandler(this.CECurrentSample_Click);
            // 
            // CESelectedSample
            // 
            this.CESelectedSample.Location = new System.Drawing.Point(37, 64);
            this.CESelectedSample.Name = "CESelectedSample";
            this.CESelectedSample.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CESelectedSample.Properties.Appearance.Options.UseFont = true;
            this.CESelectedSample.Properties.Caption = "  打印已选样本。";
            this.CESelectedSample.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.CESelectedSample.Size = new System.Drawing.Size(146, 21);
            this.CESelectedSample.TabIndex = 4;
            this.CESelectedSample.Click += new System.EventHandler(this.CESelectedSample_Click);
            // 
            // CEAllSample
            // 
            this.CEAllSample.Location = new System.Drawing.Point(37, 109);
            this.CEAllSample.Name = "CEAllSample";
            this.CEAllSample.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CEAllSample.Properties.Appearance.Options.UseFont = true;
            this.CEAllSample.Properties.Caption = "  打印所有样本。";
            this.CEAllSample.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.CEAllSample.Size = new System.Drawing.Size(146, 21);
            this.CEAllSample.TabIndex = 5;
            this.CEAllSample.Click += new System.EventHandler(this.CEAllSample_Click);
            // 
            // ButPrintState
            // 
            this.ButPrintState.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButPrintState.Location = new System.Drawing.Point(164, 136);
            this.ButPrintState.Name = "ButPrintState";
            this.ButPrintState.Size = new System.Drawing.Size(88, 32);
            this.ButPrintState.TabIndex = 9;
            this.ButPrintState.Text = "开始打印";
            this.ButPrintState.UseVisualStyleBackColor = true;
            this.ButPrintState.Click += new System.EventHandler(this.ButPrintState_Click);
            // 
            // ButReturn
            // 
            this.ButReturn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButReturn.Location = new System.Drawing.Point(258, 136);
            this.ButReturn.Name = "ButReturn";
            this.ButReturn.Size = new System.Drawing.Size(88, 32);
            this.ButReturn.TabIndex = 10;
            this.ButReturn.Text = "返回";
            this.ButReturn.UseVisualStyleBackColor = true;
            this.ButReturn.Click += new System.EventHandler(this.ButReturn_Click);
            // 
            // PrintType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 175);
            this.Controls.Add(this.ButReturn);
            this.Controls.Add(this.ButPrintState);
            this.Controls.Add(this.CEAllSample);
            this.Controls.Add(this.CESelectedSample);
            this.Controls.Add(this.CECurrentSample);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.CECurrentSample.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CESelectedSample.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CEAllSample.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.CheckEdit CECurrentSample;
        private DevExpress.XtraEditors.CheckEdit CESelectedSample;
        private DevExpress.XtraEditors.CheckEdit CEAllSample;
        private System.Windows.Forms.Button ButPrintState;
        private System.Windows.Forms.Button ButReturn;
    }
}