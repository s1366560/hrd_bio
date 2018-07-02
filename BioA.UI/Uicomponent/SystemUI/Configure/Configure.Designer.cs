namespace BioA.UI
{
    partial class Configure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configure));
            this.lblFunctionalConfigura = new DevExpress.XtraEditors.LabelControl();
            this.chkSDBROpen = new DevExpress.XtraEditors.CheckEdit();
            this.chkRDBROpen = new DevExpress.XtraEditors.CheckEdit();
            this.chkHBROpen = new DevExpress.XtraEditors.CheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkSDBROpen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRDBROpen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHBROpen.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFunctionalConfigura
            // 
            this.lblFunctionalConfigura.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFunctionalConfigura.Appearance.Options.UseFont = true;
            this.lblFunctionalConfigura.Location = new System.Drawing.Point(595, 242);
            this.lblFunctionalConfigura.Name = "lblFunctionalConfigura";
            this.lblFunctionalConfigura.Size = new System.Drawing.Size(70, 17);
            this.lblFunctionalConfigura.TabIndex = 0;
            this.lblFunctionalConfigura.Text = "功能配置：";
            // 
            // chkSDBROpen
            // 
            this.chkSDBROpen.Location = new System.Drawing.Point(632, 338);
            this.chkSDBROpen.Name = "chkSDBROpen";
            this.chkSDBROpen.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSDBROpen.Properties.Appearance.Options.UseFont = true;
            this.chkSDBROpen.Properties.Caption = "样本盘条码器开放";
            this.chkSDBROpen.Size = new System.Drawing.Size(206, 21);
            this.chkSDBROpen.TabIndex = 4;
            // 
            // chkRDBROpen
            // 
            this.chkRDBROpen.Location = new System.Drawing.Point(632, 504);
            this.chkRDBROpen.Name = "chkRDBROpen";
            this.chkRDBROpen.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRDBROpen.Properties.Appearance.Options.UseFont = true;
            this.chkRDBROpen.Properties.Caption = "试剂盘条码器开放";
            this.chkRDBROpen.Size = new System.Drawing.Size(206, 21);
            this.chkRDBROpen.TabIndex = 8;
            // 
            // chkHBROpen
            // 
            this.chkHBROpen.Location = new System.Drawing.Point(632, 422);
            this.chkHBROpen.Name = "chkHBROpen";
            this.chkHBROpen.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHBROpen.Properties.Appearance.Options.UseFont = true;
            this.chkHBROpen.Properties.Caption = "手持条码器开放";
            this.chkHBROpen.Size = new System.Drawing.Size(206, 21);
            this.chkHBROpen.TabIndex = 9;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "序号";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 74;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "试剂位置";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 119;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "是否开放";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 119;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(1308, 710);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 53);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Appearance.Options.UseFont = true;
            this.btnConfirm.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirm.Image")));
            this.btnConfirm.Location = new System.Drawing.Point(1188, 710);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 53);
            this.btnConfirm.TabIndex = 13;
            this.btnConfirm.Text = "确定";
            // 
            // Configure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkHBROpen);
            this.Controls.Add(this.chkRDBROpen);
            this.Controls.Add(this.chkSDBROpen);
            this.Controls.Add(this.lblFunctionalConfigura);
            this.Name = "Configure";
            this.Size = new System.Drawing.Size(1450, 887);
            ((System.ComponentModel.ISupportInitialize)(this.chkSDBROpen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRDBROpen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHBROpen.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblFunctionalConfigura;
        private DevExpress.XtraEditors.CheckEdit chkSDBROpen;
        private DevExpress.XtraEditors.CheckEdit chkRDBROpen;
        private DevExpress.XtraEditors.CheckEdit chkHBROpen;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
    }
}
