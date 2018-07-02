namespace BioA.UI.Uicomponent.SystemUI.Maintenance
{
    partial class CleaningMaintenance
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
            this.rtxtWaterExchange = new System.Windows.Forms.RichTextBox();
            this.rtxtCleanSystemWarn = new System.Windows.Forms.RichTextBox();
            this.rtxtCleanSystem = new System.Windows.Forms.RichTextBox();
            this.rtxtCleanSampleNeedle = new System.Windows.Forms.RichTextBox();
            this.btnCleanSN = new DevExpress.XtraEditors.SimpleButton();
            this.btnCleanSystem = new DevExpress.XtraEditors.SimpleButton();
            this.btnWaterExchange = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // rtxtWaterExchange
            // 
            this.rtxtWaterExchange.BackColor = System.Drawing.SystemColors.Control;
            this.rtxtWaterExchange.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtxtWaterExchange.Location = new System.Drawing.Point(1171, 118);
            this.rtxtWaterExchange.Name = "rtxtWaterExchange";
            this.rtxtWaterExchange.ReadOnly = true;
            this.rtxtWaterExchange.Size = new System.Drawing.Size(380, 512);
            this.rtxtWaterExchange.TabIndex = 11;
            this.rtxtWaterExchange.Text = "";
            // 
            // rtxtCleanSystemWarn
            // 
            this.rtxtCleanSystemWarn.BackColor = System.Drawing.SystemColors.Control;
            this.rtxtCleanSystemWarn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtxtCleanSystemWarn.Location = new System.Drawing.Point(641, 428);
            this.rtxtCleanSystemWarn.Name = "rtxtCleanSystemWarn";
            this.rtxtCleanSystemWarn.ReadOnly = true;
            this.rtxtCleanSystemWarn.Size = new System.Drawing.Size(380, 200);
            this.rtxtCleanSystemWarn.TabIndex = 10;
            this.rtxtCleanSystemWarn.Text = "";
            // 
            // rtxtCleanSystem
            // 
            this.rtxtCleanSystem.BackColor = System.Drawing.SystemColors.Control;
            this.rtxtCleanSystem.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtxtCleanSystem.Location = new System.Drawing.Point(641, 118);
            this.rtxtCleanSystem.Name = "rtxtCleanSystem";
            this.rtxtCleanSystem.Size = new System.Drawing.Size(380, 312);
            this.rtxtCleanSystem.TabIndex = 9;
            this.rtxtCleanSystem.Text = "";
            // 
            // rtxtCleanSampleNeedle
            // 
            this.rtxtCleanSampleNeedle.BackColor = System.Drawing.SystemColors.Control;
            this.rtxtCleanSampleNeedle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtxtCleanSampleNeedle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtxtCleanSampleNeedle.Location = new System.Drawing.Point(111, 118);
            this.rtxtCleanSampleNeedle.Name = "rtxtCleanSampleNeedle";
            this.rtxtCleanSampleNeedle.Size = new System.Drawing.Size(380, 512);
            this.rtxtCleanSampleNeedle.TabIndex = 8;
            this.rtxtCleanSampleNeedle.Text = "";
            // 
            // btnCleanSN
            // 
            this.btnCleanSN.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCleanSN.Appearance.Options.UseFont = true;
            this.btnCleanSN.Location = new System.Drawing.Point(272, 664);
            this.btnCleanSN.Name = "btnCleanSN";
            this.btnCleanSN.Size = new System.Drawing.Size(89, 43);
            this.btnCleanSN.TabIndex = 12;
            this.btnCleanSN.Text = "清洗样本针";
            // 
            // btnCleanSystem
            // 
            this.btnCleanSystem.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCleanSystem.Appearance.Options.UseFont = true;
            this.btnCleanSystem.Location = new System.Drawing.Point(797, 664);
            this.btnCleanSystem.Name = "btnCleanSystem";
            this.btnCleanSystem.Size = new System.Drawing.Size(89, 43);
            this.btnCleanSystem.TabIndex = 13;
            this.btnCleanSystem.Text = "清洗系统";
            // 
            // btnWaterExchange
            // 
            this.btnWaterExchange.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWaterExchange.Appearance.Options.UseFont = true;
            this.btnWaterExchange.Location = new System.Drawing.Point(1336, 664);
            this.btnWaterExchange.Name = "btnWaterExchange";
            this.btnWaterExchange.Size = new System.Drawing.Size(89, 43);
            this.btnWaterExchange.TabIndex = 14;
            this.btnWaterExchange.Text = "水交换";
            // 
            // CleaningMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnWaterExchange);
            this.Controls.Add(this.btnCleanSystem);
            this.Controls.Add(this.btnCleanSN);
            this.Controls.Add(this.rtxtWaterExchange);
            this.Controls.Add(this.rtxtCleanSystemWarn);
            this.Controls.Add(this.rtxtCleanSystem);
            this.Controls.Add(this.rtxtCleanSampleNeedle);
            this.Name = "CleaningMaintenance";
            this.Size = new System.Drawing.Size(1762, 806);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtWaterExchange;
        private System.Windows.Forms.RichTextBox rtxtCleanSystemWarn;
        private System.Windows.Forms.RichTextBox rtxtCleanSystem;
        private System.Windows.Forms.RichTextBox rtxtCleanSampleNeedle;
        private DevExpress.XtraEditors.SimpleButton btnCleanSN;
        private DevExpress.XtraEditors.SimpleButton btnCleanSystem;
        private DevExpress.XtraEditors.SimpleButton btnWaterExchange;
    }
}
