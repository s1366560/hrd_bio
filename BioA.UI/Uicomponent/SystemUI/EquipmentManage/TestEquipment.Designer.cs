namespace BioA.UI
{
    partial class TestEquipment
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
            this.xtrEquipmentDebug = new DevExpress.XtraTab.XtraTabControl();
            this.xtrReactionTrayDebug = new DevExpress.XtraTab.XtraTabPage();
            this.xtrReagentTrayDebug = new DevExpress.XtraTab.XtraTabPage();
            this.xtrSampTrayDebug = new DevExpress.XtraTab.XtraTabPage();
            this.xtrAbsorberDebug = new DevExpress.XtraTab.XtraTabPage();
            this.xtrTreaterDebug = new DevExpress.XtraTab.XtraTabPage();
            this.xtrLiquidCircuitDebug = new DevExpress.XtraTab.XtraTabPage();
            this.xtrOpticalPathDebug = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.xtrEquipmentDebug)).BeginInit();
            this.xtrEquipmentDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtrEquipmentDebug
            // 
            this.xtrEquipmentDebug.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtrEquipmentDebug.Appearance.Options.UseFont = true;
            this.xtrEquipmentDebug.Location = new System.Drawing.Point(3, 36);
            this.xtrEquipmentDebug.Name = "xtrEquipmentDebug";
            this.xtrEquipmentDebug.SelectedTabPage = this.xtrReactionTrayDebug;
            this.xtrEquipmentDebug.Size = new System.Drawing.Size(1714, 870);
            this.xtrEquipmentDebug.TabIndex = 0;
            this.xtrEquipmentDebug.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtrReactionTrayDebug,
            this.xtrReagentTrayDebug,
            this.xtrSampTrayDebug,
            this.xtrAbsorberDebug,
            this.xtrTreaterDebug,
            this.xtrLiquidCircuitDebug,
            this.xtrOpticalPathDebug});
            this.xtrEquipmentDebug.Click += new System.EventHandler(this.xtraTabControl1_Click);
            // 
            // xtrReactionTrayDebug
            // 
            this.xtrReactionTrayDebug.Name = "xtrReactionTrayDebug";
            this.xtrReactionTrayDebug.Size = new System.Drawing.Size(1708, 841);
            this.xtrReactionTrayDebug.TabPageWidth = 100;
            this.xtrReactionTrayDebug.Text = "反应盘调试";
            // 
            // xtrReagentTrayDebug
            // 
            this.xtrReagentTrayDebug.Name = "xtrReagentTrayDebug";
            this.xtrReagentTrayDebug.Size = new System.Drawing.Size(1708, 841);
            this.xtrReagentTrayDebug.TabPageWidth = 100;
            this.xtrReagentTrayDebug.Text = "试剂盘调试";
            // 
            // xtrSampTrayDebug
            // 
            this.xtrSampTrayDebug.Name = "xtrSampTrayDebug";
            this.xtrSampTrayDebug.Size = new System.Drawing.Size(1708, 841);
            this.xtrSampTrayDebug.TabPageWidth = 100;
            this.xtrSampTrayDebug.Text = "样本盘调试";
            // 
            // xtrAbsorberDebug
            // 
            this.xtrAbsorberDebug.Name = "xtrAbsorberDebug";
            this.xtrAbsorberDebug.Size = new System.Drawing.Size(1708, 841);
            this.xtrAbsorberDebug.TabPageWidth = 100;
            this.xtrAbsorberDebug.Text = "吸量器调试";
            // 
            // xtrTreaterDebug
            // 
            this.xtrTreaterDebug.Name = "xtrTreaterDebug";
            this.xtrTreaterDebug.Size = new System.Drawing.Size(1708, 841);
            this.xtrTreaterDebug.TabPageWidth = 100;
            this.xtrTreaterDebug.Text = "搅拌器调试";
            // 
            // xtrLiquidCircuitDebug
            // 
            this.xtrLiquidCircuitDebug.Name = "xtrLiquidCircuitDebug";
            this.xtrLiquidCircuitDebug.Size = new System.Drawing.Size(1708, 841);
            this.xtrLiquidCircuitDebug.TabPageWidth = 100;
            this.xtrLiquidCircuitDebug.Text = "液路调试";
            // 
            // xtrOpticalPathDebug
            // 
            this.xtrOpticalPathDebug.Name = "xtrOpticalPathDebug";
            this.xtrOpticalPathDebug.Size = new System.Drawing.Size(1708, 841);
            this.xtrOpticalPathDebug.TabPageWidth = 100;
            this.xtrOpticalPathDebug.Text = "光路调试";
            // 
            // TestEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtrEquipmentDebug);
            this.Name = "TestEquipment";
            this.Size = new System.Drawing.Size(1717, 906);
            ((System.ComponentModel.ISupportInitialize)(this.xtrEquipmentDebug)).EndInit();
            this.xtrEquipmentDebug.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtrEquipmentDebug;
        private DevExpress.XtraTab.XtraTabPage xtrReactionTrayDebug;
        private DevExpress.XtraTab.XtraTabPage xtrReagentTrayDebug;
        private DevExpress.XtraTab.XtraTabPage xtrSampTrayDebug;
        private DevExpress.XtraTab.XtraTabPage xtrAbsorberDebug;
        private DevExpress.XtraTab.XtraTabPage xtrTreaterDebug;
        private DevExpress.XtraTab.XtraTabPage xtrLiquidCircuitDebug;
        private DevExpress.XtraTab.XtraTabPage xtrOpticalPathDebug;
    }
}
