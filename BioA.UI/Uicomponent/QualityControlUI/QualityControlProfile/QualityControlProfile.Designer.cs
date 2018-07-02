namespace BioA.UI.Uicomponent.QualityControlUI.QualityControlProfile
{
    partial class QualityControlProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QualityControlProfile));
            this.QCchartControl = new DevExpress.XtraCharts.ChartControl();
            this.QCTime2 = new System.Windows.Forms.DateTimePicker();
            this.QCTime1 = new System.Windows.Forms.DateTimePicker();
            this.QCTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblQCProduct = new DevExpress.XtraEditors.LabelControl();
            this.lblLotNum = new DevExpress.XtraEditors.LabelControl();
            this.lblPosition = new DevExpress.XtraEditors.LabelControl();
            this.lblProName = new DevExpress.XtraEditors.LabelControl();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.cmbEntryName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbQCproducts = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbLotNum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbPosition = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.QCchartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntryName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQCproducts.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLotNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPosition.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // QCchartControl
            // 
            this.QCchartControl.DataBindings = null;
            this.QCchartControl.Legend.Name = "Default Legend";
            this.QCchartControl.Location = new System.Drawing.Point(176, 148);
            this.QCchartControl.Name = "QCchartControl";
            this.QCchartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.QCchartControl.Size = new System.Drawing.Size(1388, 666);
            this.QCchartControl.TabIndex = 58;
            // 
            // QCTime2
            // 
            this.QCTime2.CalendarFont = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QCTime2.Location = new System.Drawing.Point(1332, 97);
            this.QCTime2.Name = "QCTime2";
            this.QCTime2.Size = new System.Drawing.Size(200, 22);
            this.QCTime2.TabIndex = 57;
            // 
            // QCTime1
            // 
            this.QCTime1.CalendarFont = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QCTime1.Location = new System.Drawing.Point(1115, 97);
            this.QCTime1.Name = "QCTime1";
            this.QCTime1.Size = new System.Drawing.Size(200, 22);
            this.QCTime1.TabIndex = 56;
            // 
            // QCTime
            // 
            this.QCTime.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QCTime.Appearance.Options.UseFont = true;
            this.QCTime.Location = new System.Drawing.Point(1039, 98);
            this.QCTime.Name = "QCTime";
            this.QCTime.Size = new System.Drawing.Size(70, 17);
            this.QCTime.TabIndex = 47;
            this.QCTime.Text = "质控时间：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(1321, 98);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(5, 17);
            this.labelControl1.TabIndex = 46;
            this.labelControl1.Text = "-";
            // 
            // lblQCProduct
            // 
            this.lblQCProduct.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQCProduct.Appearance.Options.UseFont = true;
            this.lblQCProduct.Location = new System.Drawing.Point(394, 98);
            this.lblQCProduct.Name = "lblQCProduct";
            this.lblQCProduct.Size = new System.Drawing.Size(56, 17);
            this.lblQCProduct.TabIndex = 44;
            this.lblQCProduct.Text = "质控品：";
            // 
            // lblLotNum
            // 
            this.lblLotNum.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLotNum.Appearance.Options.UseFont = true;
            this.lblLotNum.Location = new System.Drawing.Point(606, 98);
            this.lblLotNum.Name = "lblLotNum";
            this.lblLotNum.Size = new System.Drawing.Size(42, 17);
            this.lblLotNum.TabIndex = 43;
            this.lblLotNum.Text = "批号：";
            // 
            // lblPosition
            // 
            this.lblPosition.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.Appearance.Options.UseFont = true;
            this.lblPosition.Location = new System.Drawing.Point(803, 98);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(70, 17);
            this.lblPosition.TabIndex = 42;
            this.lblPosition.Text = "生产厂家：";
            // 
            // lblProName
            // 
            this.lblProName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProName.Appearance.Options.UseFont = true;
            this.lblProName.Location = new System.Drawing.Point(176, 99);
            this.lblProName.Name = "lblProName";
            this.lblProName.Size = new System.Drawing.Size(70, 17);
            this.lblProName.TabIndex = 40;
            this.lblProName.Text = "项目名称：";
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(1485, 835);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 47);
            this.btnPrint.TabIndex = 60;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.simpleButton2_Click_1);
            // 
            // btnQuery
            // 
            this.btnQuery.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.Appearance.Options.UseFont = true;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(1388, 835);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 47);
            this.btnQuery.TabIndex = 59;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // cmbEntryName
            // 
            this.cmbEntryName.Location = new System.Drawing.Point(252, 95);
            this.cmbEntryName.Name = "cmbEntryName";
            this.cmbEntryName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEntryName.Properties.Appearance.Options.UseFont = true;
            this.cmbEntryName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEntryName.Size = new System.Drawing.Size(100, 24);
            this.cmbEntryName.TabIndex = 61;
            // 
            // cmbQCproducts
            // 
            this.cmbQCproducts.Location = new System.Drawing.Point(456, 95);
            this.cmbQCproducts.Name = "cmbQCproducts";
            this.cmbQCproducts.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQCproducts.Properties.Appearance.Options.UseFont = true;
            this.cmbQCproducts.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQCproducts.Size = new System.Drawing.Size(100, 24);
            this.cmbQCproducts.TabIndex = 62;
            // 
            // cmbLotNum
            // 
            this.cmbLotNum.Location = new System.Drawing.Point(654, 96);
            this.cmbLotNum.Name = "cmbLotNum";
            this.cmbLotNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLotNum.Properties.Appearance.Options.UseFont = true;
            this.cmbLotNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLotNum.Size = new System.Drawing.Size(100, 24);
            this.cmbLotNum.TabIndex = 63;
            // 
            // cmbPosition
            // 
            this.cmbPosition.Location = new System.Drawing.Point(879, 96);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPosition.Properties.Appearance.Options.UseFont = true;
            this.cmbPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPosition.Size = new System.Drawing.Size(100, 24);
            this.cmbPosition.TabIndex = 64;
            // 
            // QualityControlProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbPosition);
            this.Controls.Add(this.cmbLotNum);
            this.Controls.Add(this.cmbQCproducts);
            this.Controls.Add(this.cmbEntryName);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.QCchartControl);
            this.Controls.Add(this.QCTime2);
            this.Controls.Add(this.QCTime1);
            this.Controls.Add(this.QCTime);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblQCProduct);
            this.Controls.Add(this.lblLotNum);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblProName);
            this.Name = "QualityControlProfile";
            this.Size = new System.Drawing.Size(1721, 906);
            ((System.ComponentModel.ISupportInitialize)(this.QCchartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntryName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQCproducts.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLotNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPosition.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl QCchartControl;
        private System.Windows.Forms.DateTimePicker QCTime2;
        private System.Windows.Forms.DateTimePicker QCTime1;
        private DevExpress.XtraEditors.LabelControl QCTime;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblQCProduct;
        private DevExpress.XtraEditors.LabelControl lblLotNum;
        private DevExpress.XtraEditors.LabelControl lblPosition;
        private DevExpress.XtraEditors.LabelControl lblProName;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.ComboBoxEdit cmbEntryName;
        private DevExpress.XtraEditors.ComboBoxEdit cmbQCproducts;
        private DevExpress.XtraEditors.ComboBoxEdit cmbLotNum;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPosition;
    }
}
