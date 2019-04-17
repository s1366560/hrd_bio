namespace BioA.UI
{
    partial class LightSystem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMinimumVoltage = new System.Windows.Forms.Label();
            this.lblExcursion = new System.Windows.Forms.Label();
            this.lblGain = new System.Windows.Forms.Label();
            this.lblVoltage = new System.Windows.Forms.Label();
            this.lblMaximumVoltage = new System.Windows.Forms.Label();
            this.lblAbsorbancy = new System.Windows.Forms.Label();
            this.lblWaveLength = new System.Windows.Forms.Label();
            this.txtMaxVoltage = new DevExpress.XtraEditors.TextEdit();
            this.txtVoltage = new DevExpress.XtraEditors.TextEdit();
            this.txtOffset = new DevExpress.XtraEditors.TextEdit();
            this.txtMinVoltage = new DevExpress.XtraEditors.TextEdit();
            this.txtAbs = new DevExpress.XtraEditors.TextEdit();
            this.txtGain = new DevExpress.XtraEditors.TextEdit();
            this.btnBegin = new DevExpress.XtraEditors.SimpleButton();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.cboWaveLength = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxVoltage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoltage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOffset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinVoltage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGain.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMinimumVoltage
            // 
            this.lblMinimumVoltage.AutoSize = true;
            this.lblMinimumVoltage.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMinimumVoltage.Location = new System.Drawing.Point(891, 377);
            this.lblMinimumVoltage.Name = "lblMinimumVoltage";
            this.lblMinimumVoltage.Size = new System.Drawing.Size(104, 19);
            this.lblMinimumVoltage.TabIndex = 31;
            this.lblMinimumVoltage.Text = "最小电压：";
            // 
            // lblExcursion
            // 
            this.lblExcursion.AutoSize = true;
            this.lblExcursion.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExcursion.Location = new System.Drawing.Point(658, 286);
            this.lblExcursion.Name = "lblExcursion";
            this.lblExcursion.Size = new System.Drawing.Size(66, 19);
            this.lblExcursion.TabIndex = 29;
            this.lblExcursion.Text = "偏移：";
            // 
            // lblGain
            // 
            this.lblGain.AutoSize = true;
            this.lblGain.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGain.Location = new System.Drawing.Point(891, 285);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(66, 19);
            this.lblGain.TabIndex = 27;
            this.lblGain.Text = "增益：";
            // 
            // lblVoltage
            // 
            this.lblVoltage.AutoSize = true;
            this.lblVoltage.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVoltage.Location = new System.Drawing.Point(394, 331);
            this.lblVoltage.Name = "lblVoltage";
            this.lblVoltage.Size = new System.Drawing.Size(66, 19);
            this.lblVoltage.TabIndex = 25;
            this.lblVoltage.Text = "电压：";
            // 
            // lblMaximumVoltage
            // 
            this.lblMaximumVoltage.AutoSize = true;
            this.lblMaximumVoltage.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMaximumVoltage.Location = new System.Drawing.Point(394, 377);
            this.lblMaximumVoltage.Name = "lblMaximumVoltage";
            this.lblMaximumVoltage.Size = new System.Drawing.Size(104, 19);
            this.lblMaximumVoltage.TabIndex = 23;
            this.lblMaximumVoltage.Text = "最大电压：";
            // 
            // lblAbsorbancy
            // 
            this.lblAbsorbancy.AutoSize = true;
            this.lblAbsorbancy.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAbsorbancy.Location = new System.Drawing.Point(891, 331);
            this.lblAbsorbancy.Name = "lblAbsorbancy";
            this.lblAbsorbancy.Size = new System.Drawing.Size(85, 19);
            this.lblAbsorbancy.TabIndex = 21;
            this.lblAbsorbancy.Text = "吸光度：";
            // 
            // lblWaveLength
            // 
            this.lblWaveLength.AutoSize = true;
            this.lblWaveLength.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWaveLength.Location = new System.Drawing.Point(394, 285);
            this.lblWaveLength.Name = "lblWaveLength";
            this.lblWaveLength.Size = new System.Drawing.Size(66, 19);
            this.lblWaveLength.TabIndex = 19;
            this.lblWaveLength.Text = "波长：";
            // 
            // txtMaxVoltage
            // 
            this.txtMaxVoltage.Location = new System.Drawing.Point(504, 374);
            this.txtMaxVoltage.Name = "txtMaxVoltage";
            this.txtMaxVoltage.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxVoltage.Properties.Appearance.Options.UseFont = true;
            this.txtMaxVoltage.Properties.ReadOnly = true;
            this.txtMaxVoltage.Size = new System.Drawing.Size(100, 24);
            this.txtMaxVoltage.TabIndex = 32;
            // 
            // txtVoltage
            // 
            this.txtVoltage.Location = new System.Drawing.Point(504, 330);
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVoltage.Properties.Appearance.Options.UseFont = true;
            this.txtVoltage.Properties.ReadOnly = true;
            this.txtVoltage.Size = new System.Drawing.Size(100, 24);
            this.txtVoltage.TabIndex = 33;
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(730, 282);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOffset.Properties.Appearance.Options.UseFont = true;
            this.txtOffset.Properties.ReadOnly = true;
            this.txtOffset.Size = new System.Drawing.Size(100, 24);
            this.txtOffset.TabIndex = 35;
            // 
            // txtMinVoltage
            // 
            this.txtMinVoltage.Location = new System.Drawing.Point(1001, 376);
            this.txtMinVoltage.Name = "txtMinVoltage";
            this.txtMinVoltage.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinVoltage.Properties.Appearance.Options.UseFont = true;
            this.txtMinVoltage.Properties.ReadOnly = true;
            this.txtMinVoltage.Size = new System.Drawing.Size(100, 24);
            this.txtMinVoltage.TabIndex = 36;
            // 
            // txtAbs
            // 
            this.txtAbs.Location = new System.Drawing.Point(1001, 330);
            this.txtAbs.Name = "txtAbs";
            this.txtAbs.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAbs.Properties.Appearance.Options.UseFont = true;
            this.txtAbs.Properties.ReadOnly = true;
            this.txtAbs.Size = new System.Drawing.Size(100, 24);
            this.txtAbs.TabIndex = 37;
            // 
            // txtGain
            // 
            this.txtGain.Location = new System.Drawing.Point(1001, 283);
            this.txtGain.Name = "txtGain";
            this.txtGain.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGain.Properties.Appearance.Options.UseFont = true;
            this.txtGain.Properties.ReadOnly = true;
            this.txtGain.Size = new System.Drawing.Size(100, 24);
            this.txtGain.TabIndex = 38;
            // 
            // btnBegin
            // 
            this.btnBegin.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBegin.Appearance.Options.UseFont = true;
            this.btnBegin.Location = new System.Drawing.Point(472, 465);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(167, 45);
            this.btnBegin.TabIndex = 39;
            this.btnBegin.Text = "开始";
            this.btnBegin.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnStop
            // 
            this.btnStop.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Appearance.Options.UseFont = true;
            this.btnStop.Location = new System.Drawing.Point(917, 465);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(167, 45);
            this.btnStop.TabIndex = 41;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // cboWaveLength
            // 
            this.cboWaveLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWaveLength.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWaveLength.FormattingEnabled = true;
            this.cboWaveLength.Location = new System.Drawing.Point(504, 285);
            this.cboWaveLength.Name = "cboWaveLength";
            this.cboWaveLength.Size = new System.Drawing.Size(100, 25);
            this.cboWaveLength.TabIndex = 42;
            // 
            // LightSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboWaveLength);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnBegin);
            this.Controls.Add(this.txtGain);
            this.Controls.Add(this.txtAbs);
            this.Controls.Add(this.txtMinVoltage);
            this.Controls.Add(this.txtOffset);
            this.Controls.Add(this.txtVoltage);
            this.Controls.Add(this.txtMaxVoltage);
            this.Controls.Add(this.lblMinimumVoltage);
            this.Controls.Add(this.lblExcursion);
            this.Controls.Add(this.lblGain);
            this.Controls.Add(this.lblVoltage);
            this.Controls.Add(this.lblMaximumVoltage);
            this.Controls.Add(this.lblAbsorbancy);
            this.Controls.Add(this.lblWaveLength);
            this.Name = "LightSystem";
            this.Size = new System.Drawing.Size(1618, 957);
            //this.Load += new System.EventHandler(this.LightSystem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxVoltage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoltage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOffset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinVoltage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGain.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMinimumVoltage;
        private System.Windows.Forms.Label lblExcursion;
        private System.Windows.Forms.Label lblGain;
        private System.Windows.Forms.Label lblVoltage;
        private System.Windows.Forms.Label lblMaximumVoltage;
        private System.Windows.Forms.Label lblAbsorbancy;
        private System.Windows.Forms.Label lblWaveLength;
        private DevExpress.XtraEditors.TextEdit txtMaxVoltage;
        private DevExpress.XtraEditors.TextEdit txtVoltage;
        private DevExpress.XtraEditors.TextEdit txtOffset;
        private DevExpress.XtraEditors.TextEdit txtMinVoltage;
        private DevExpress.XtraEditors.TextEdit txtAbs;
        private DevExpress.XtraEditors.TextEdit txtGain;
        private DevExpress.XtraEditors.SimpleButton btnBegin;
        private DevExpress.XtraEditors.SimpleButton btnStop;
        private System.Windows.Forms.ComboBox cboWaveLength;
    }
}
