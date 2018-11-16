namespace BioA.UI
{
    partial class QualityControlGraphs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QualityControlGraphs));
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.QCTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblQCProduct = new DevExpress.XtraEditors.LabelControl();
            this.lblProName = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cboProjectName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboQCName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbHorizontal = new DevExpress.XtraEditors.LabelControl();
            this.lbTargetValue = new DevExpress.XtraEditors.LabelControl();
            this.lbStandardDeviation = new DevExpress.XtraEditors.LabelControl();
            this.txtTargetValue = new System.Windows.Forms.TextBox();
            this.txtStandardDeviationValue = new System.Windows.Forms.TextBox();
            this.txtHorizontalValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboQCName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            this.chartControl1.CrosshairOptions.ShowArgumentLabels = true;
            this.chartControl1.CrosshairOptions.ShowValueLabels = true;
            this.chartControl1.CrosshairOptions.ShowValueLine = true;
            this.chartControl1.DataBindings = null;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(114, 95);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(1388, 716);
            this.chartControl1.TabIndex = 58;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndTime.CustomFormat = "yyyy-MM-dd";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(1184, 52);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(111, 22);
            this.dtpEndTime.TabIndex = 57;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartTime.CustomFormat = "yyyy-MM-dd";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(1049, 52);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(109, 22);
            this.dtpStartTime.TabIndex = 56;
            // 
            // QCTime
            // 
            this.QCTime.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QCTime.Appearance.Options.UseFont = true;
            this.QCTime.Location = new System.Drawing.Point(973, 55);
            this.QCTime.Name = "QCTime";
            this.QCTime.Size = new System.Drawing.Size(70, 17);
            this.QCTime.TabIndex = 47;
            this.QCTime.Text = "质控时间：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(1168, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(5, 17);
            this.labelControl1.TabIndex = 46;
            this.labelControl1.Text = "-";
            // 
            // lblQCProduct
            // 
            this.lblQCProduct.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQCProduct.Appearance.Options.UseFont = true;
            this.lblQCProduct.Location = new System.Drawing.Point(314, 54);
            this.lblQCProduct.Name = "lblQCProduct";
            this.lblQCProduct.Size = new System.Drawing.Size(56, 17);
            this.lblQCProduct.TabIndex = 44;
            this.lblQCProduct.Text = "质控品：";
            // 
            // lblProName
            // 
            this.lblProName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProName.Appearance.Options.UseFont = true;
            this.lblProName.Location = new System.Drawing.Point(112, 53);
            this.lblProName.Name = "lblProName";
            this.lblProName.Size = new System.Drawing.Size(70, 17);
            this.lblProName.TabIndex = 40;
            this.lblProName.Text = "项目名称：";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(1423, 826);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(79, 47);
            this.simpleButton2.TabIndex = 60;
            this.simpleButton2.Text = "打印";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click_1);
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(1423, 42);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 47);
            this.btnSearch.TabIndex = 59;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboProjectName
            // 
            this.cboProjectName.Location = new System.Drawing.Point(183, 52);
            this.cboProjectName.Name = "cboProjectName";
            this.cboProjectName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProjectName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboProjectName.Size = new System.Drawing.Size(100, 20);
            this.cboProjectName.TabIndex = 61;
            this.cboProjectName.SelectedIndexChanged += new System.EventHandler(this.cboProjectName_SelectedIndexChanged);
            // 
            // cboQCName
            // 
            this.cboQCName.Location = new System.Drawing.Point(368, 53);
            this.cboQCName.Name = "cboQCName";
            this.cboQCName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboQCName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboQCName.Size = new System.Drawing.Size(100, 20);
            this.cboQCName.TabIndex = 62;
            this.cboQCName.SelectedIndexChanged += new System.EventHandler(this.cboQCName_SelectedIndexChanged);
            // 
            // lbHorizontal
            // 
            this.lbHorizontal.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHorizontal.Appearance.Options.UseFont = true;
            this.lbHorizontal.Location = new System.Drawing.Point(775, 55);
            this.lbHorizontal.Name = "lbHorizontal";
            this.lbHorizontal.Size = new System.Drawing.Size(70, 17);
            this.lbHorizontal.TabIndex = 66;
            this.lbHorizontal.Text = "水平浓度：";
            // 
            // lbTargetValue
            // 
            this.lbTargetValue.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTargetValue.Appearance.Options.UseFont = true;
            this.lbTargetValue.Location = new System.Drawing.Point(501, 54);
            this.lbTargetValue.Name = "lbTargetValue";
            this.lbTargetValue.Size = new System.Drawing.Size(42, 17);
            this.lbTargetValue.TabIndex = 67;
            this.lbTargetValue.Text = "靶值：";
            // 
            // lbStandardDeviation
            // 
            this.lbStandardDeviation.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStandardDeviation.Appearance.Options.UseFont = true;
            this.lbStandardDeviation.Location = new System.Drawing.Point(630, 56);
            this.lbStandardDeviation.Name = "lbStandardDeviation";
            this.lbStandardDeviation.Size = new System.Drawing.Size(56, 17);
            this.lbStandardDeviation.TabIndex = 68;
            this.lbStandardDeviation.Text = "标准差：";
            // 
            // txtTargetValue
            // 
            this.txtTargetValue.Location = new System.Drawing.Point(539, 52);
            this.txtTargetValue.Name = "txtTargetValue";
            this.txtTargetValue.Size = new System.Drawing.Size(65, 22);
            this.txtTargetValue.TabIndex = 69;
            // 
            // txtStandardDeviationValue
            // 
            this.txtStandardDeviationValue.Location = new System.Drawing.Point(681, 53);
            this.txtStandardDeviationValue.Name = "txtStandardDeviationValue";
            this.txtStandardDeviationValue.Size = new System.Drawing.Size(65, 22);
            this.txtStandardDeviationValue.TabIndex = 70;
            // 
            // txtHorizontalValue
            // 
            this.txtHorizontalValue.Location = new System.Drawing.Point(841, 53);
            this.txtHorizontalValue.Name = "txtHorizontalValue";
            this.txtHorizontalValue.Size = new System.Drawing.Size(65, 22);
            this.txtHorizontalValue.TabIndex = 71;
            // 
            // QualityControlGraphs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtHorizontalValue);
            this.Controls.Add(this.txtStandardDeviationValue);
            this.Controls.Add(this.txtTargetValue);
            this.Controls.Add(this.lbStandardDeviation);
            this.Controls.Add(this.lbTargetValue);
            this.Controls.Add(this.lbHorizontal);
            this.Controls.Add(this.cboQCName);
            this.Controls.Add(this.cboProjectName);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.QCTime);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblQCProduct);
            this.Controls.Add(this.lblProName);
            this.Name = "QualityControlGraphs";
            this.Size = new System.Drawing.Size(1721, 882);
            this.Load += new System.EventHandler(this.QualityControlGraphs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboQCName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private DevExpress.XtraEditors.LabelControl QCTime;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblQCProduct;
        private DevExpress.XtraEditors.LabelControl lblProName;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.ComboBoxEdit cboProjectName;
        private DevExpress.XtraEditors.ComboBoxEdit cboQCName;
        private DevExpress.XtraEditors.LabelControl lbHorizontal;
        private DevExpress.XtraEditors.LabelControl lbTargetValue;
        private DevExpress.XtraEditors.LabelControl lbStandardDeviation;
        private System.Windows.Forms.TextBox txtTargetValue;
        private System.Windows.Forms.TextBox txtStandardDeviationValue;
        private System.Windows.Forms.TextBox txtHorizontalValue;
    }
}
