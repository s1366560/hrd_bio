namespace BioA.UI
{
    partial class ReactionProcessCB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReactionProcessCB));
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            this.lblTestItem = new DevExpress.XtraEditors.LabelControl();
            this.lblCupNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblTestTimes = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxNum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textEditProName = new DevExpress.XtraEditors.TextEdit();
            this.labelCalibName = new DevExpress.XtraEditors.LabelControl();
            this.textEditSamType = new DevExpress.XtraEditors.TextEdit();
            this.labelSamType = new DevExpress.XtraEditors.LabelControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.comboBoxCalibTime = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelCalibTime = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditCuveNum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comBoxEditCalibName = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditProName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSamType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCalibTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCuveNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comBoxEditCalibName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTestItem
            // 
            this.lblTestItem.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestItem.Appearance.Options.UseFont = true;
            this.lblTestItem.Location = new System.Drawing.Point(17, 23);
            this.lblTestItem.Name = "lblTestItem";
            this.lblTestItem.Size = new System.Drawing.Size(70, 17);
            this.lblTestItem.TabIndex = 0;
            this.lblTestItem.Text = "测试项目：";
            // 
            // lblCupNumber
            // 
            this.lblCupNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCupNumber.Appearance.Options.UseFont = true;
            this.lblCupNumber.Location = new System.Drawing.Point(974, 23);
            this.lblCupNumber.Name = "lblCupNumber";
            this.lblCupNumber.Size = new System.Drawing.Size(70, 17);
            this.lblCupNumber.TabIndex = 2;
            this.lblCupNumber.Text = "反应杯号：";
            // 
            // lblTestTimes
            // 
            this.lblTestTimes.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestTimes.Appearance.Options.UseFont = true;
            this.lblTestTimes.Location = new System.Drawing.Point(600, 23);
            this.lblTestTimes.Name = "lblTestTimes";
            this.lblTestTimes.Size = new System.Drawing.Size(70, 17);
            this.lblTestTimes.TabIndex = 4;
            this.lblTestTimes.Text = "测试次数：";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(982, 626);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 56);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // comboBoxNum
            // 
            this.comboBoxNum.Location = new System.Drawing.Point(670, 20);
            this.comboBoxNum.Name = "comboBoxNum";
            this.comboBoxNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxNum.Properties.Appearance.Options.UseFont = true;
            this.comboBoxNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxNum.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxNum.Size = new System.Drawing.Size(60, 24);
            this.comboBoxNum.TabIndex = 17;
            // 
            // textEditProName
            // 
            this.textEditProName.Location = new System.Drawing.Point(88, 20);
            this.textEditProName.Name = "textEditProName";
            this.textEditProName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEditProName.Properties.Appearance.Options.UseFont = true;
            this.textEditProName.Properties.ReadOnly = true;
            this.textEditProName.Size = new System.Drawing.Size(100, 24);
            this.textEditProName.TabIndex = 18;
            // 
            // labelCalibName
            // 
            this.labelCalibName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCalibName.Appearance.Options.UseFont = true;
            this.labelCalibName.Location = new System.Drawing.Point(369, 23);
            this.labelCalibName.Name = "labelCalibName";
            this.labelCalibName.Size = new System.Drawing.Size(84, 17);
            this.labelCalibName.TabIndex = 19;
            this.labelCalibName.Text = "校准品名称：";
            // 
            // textEditSamType
            // 
            this.textEditSamType.Location = new System.Drawing.Point(271, 20);
            this.textEditSamType.Name = "textEditSamType";
            this.textEditSamType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEditSamType.Properties.Appearance.Options.UseFont = true;
            this.textEditSamType.Properties.ReadOnly = true;
            this.textEditSamType.Size = new System.Drawing.Size(83, 24);
            this.textEditSamType.TabIndex = 22;
            // 
            // labelSamType
            // 
            this.labelSamType.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSamType.Appearance.Options.UseFont = true;
            this.labelSamType.Location = new System.Drawing.Point(201, 23);
            this.labelSamType.Name = "labelSamType";
            this.labelSamType.Size = new System.Drawing.Size(70, 17);
            this.labelSamType.TabIndex = 21;
            this.labelSamType.Text = "样本类型：";
            // 
            // chartControl1
            // 
            this.chartControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.HotkeyField;
            this.chartControl1.DataBindings = null;
            xyDiagram2.AxisX.Label.Font = new System.Drawing.Font("Tahoma", 10F);
            xyDiagram2.AxisX.MinorCount = 1;
            xyDiagram2.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram2.AxisX.Thickness = 2;
            xyDiagram2.AxisX.Title.Text = "反应进程";
            xyDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisX.VisualRange.Auto = false;
            xyDiagram2.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram2.AxisX.VisualRange.MaxValueSerializable = "32.4333333333334";
            xyDiagram2.AxisX.VisualRange.MinValueSerializable = "-0.5";
            xyDiagram2.AxisX.VisualRange.SideMarginsValue = 0D;
            xyDiagram2.AxisX.WholeRange.Auto = false;
            xyDiagram2.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram2.AxisX.WholeRange.MaxValueSerializable = "44";
            xyDiagram2.AxisX.WholeRange.MinValueSerializable = "0";
            xyDiagram2.AxisX.WholeRange.SideMarginsValue = 0.5D;
            xyDiagram2.AxisY.MinorCount = 9;
            xyDiagram2.AxisY.NumericScaleOptions.AutoGrid = false;
            xyDiagram2.AxisY.NumericScaleOptions.GridSpacing = 0.1D;
            xyDiagram2.AxisY.Title.Text = "吸光度";
            xyDiagram2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.VisualRange.Auto = false;
            xyDiagram2.AxisY.VisualRange.AutoSideMargins = false;
            xyDiagram2.AxisY.VisualRange.MaxValueSerializable = "1.2";
            xyDiagram2.AxisY.VisualRange.MinValueSerializable = "-0.4";
            xyDiagram2.AxisY.VisualRange.SideMarginsValue = 0D;
            xyDiagram2.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            xyDiagram2.AxisY.WholeRange.Auto = false;
            xyDiagram2.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram2.AxisY.WholeRange.MaxValueSerializable = "4";
            xyDiagram2.AxisY.WholeRange.MinValueSerializable = "-4";
            xyDiagram2.AxisY.WholeRange.SideMarginsValue = 0D;
            xyDiagram2.EnableAxisXScrolling = true;
            xyDiagram2.EnableAxisXZooming = true;
            xyDiagram2.EnableAxisYScrolling = true;
            xyDiagram2.EnableAxisYZooming = true;
            xyDiagram2.PaneDistance = 1;
            this.chartControl1.Diagram = xyDiagram2;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(25, 68);
            this.chartControl1.Name = "chartControl1";
            series2.LegendName = "Default Legend";
            series2.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending;
            series2.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            series2.View = lineSeriesView2;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartControl1.Size = new System.Drawing.Size(1074, 552);
            this.chartControl1.TabIndex = 24;
            // 
            // comboBoxCalibTime
            // 
            this.comboBoxCalibTime.Location = new System.Drawing.Point(814, 22);
            this.comboBoxCalibTime.Name = "comboBoxCalibTime";
            this.comboBoxCalibTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCalibTime.Properties.Appearance.Options.UseFont = true;
            this.comboBoxCalibTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxCalibTime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxCalibTime.Size = new System.Drawing.Size(146, 24);
            this.comboBoxCalibTime.TabIndex = 25;
            this.comboBoxCalibTime.SelectedIndexChanged += new System.EventHandler(this.comboBoxCalibTime_SelectedIndexChanged);
            // 
            // labelCalibTime
            // 
            this.labelCalibTime.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCalibTime.Appearance.Options.UseFont = true;
            this.labelCalibTime.Location = new System.Drawing.Point(744, 23);
            this.labelCalibTime.Name = "labelCalibTime";
            this.labelCalibTime.Size = new System.Drawing.Size(70, 17);
            this.labelCalibTime.TabIndex = 26;
            this.labelCalibTime.Text = "校准时间：";
            // 
            // comboBoxEditCuveNum
            // 
            this.comboBoxEditCuveNum.Location = new System.Drawing.Point(1041, 20);
            this.comboBoxEditCuveNum.Name = "comboBoxEditCuveNum";
            this.comboBoxEditCuveNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEditCuveNum.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditCuveNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditCuveNum.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditCuveNum.Size = new System.Drawing.Size(62, 24);
            this.comboBoxEditCuveNum.TabIndex = 27;
            this.comboBoxEditCuveNum.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditCuveNum_SelectedIndexChanged);
            // 
            // comBoxEditCalibName
            // 
            this.comBoxEditCalibName.Location = new System.Drawing.Point(459, 20);
            this.comBoxEditCalibName.Name = "comBoxEditCalibName";
            this.comBoxEditCalibName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comBoxEditCalibName.Properties.Appearance.Options.UseFont = true;
            this.comBoxEditCalibName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comBoxEditCalibName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comBoxEditCalibName.Size = new System.Drawing.Size(124, 24);
            this.comBoxEditCalibName.TabIndex = 28;
            this.comBoxEditCalibName.SelectedIndexChanged += new System.EventHandler(this.comBoxEditCalibName_SelectedIndexChanged);
            // 
            // ReactionProcessCB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 691);
            this.Controls.Add(this.comBoxEditCalibName);
            this.Controls.Add(this.comboBoxEditCuveNum);
            this.Controls.Add(this.labelCalibTime);
            this.Controls.Add(this.comboBoxCalibTime);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.textEditSamType);
            this.Controls.Add(this.labelSamType);
            this.Controls.Add(this.labelCalibName);
            this.Controls.Add(this.textEditProName);
            this.Controls.Add(this.comboBoxNum);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTestTimes);
            this.Controls.Add(this.lblCupNumber);
            this.Controls.Add(this.lblTestItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ReactionProcessCB";
            this.Text = "反应进程";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditProName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSamType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCalibTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCuveNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comBoxEditCalibName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTestItem;
        private DevExpress.XtraEditors.LabelControl lblCupNumber;
        private DevExpress.XtraEditors.LabelControl lblTestTimes;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxNum;
        private DevExpress.XtraEditors.TextEdit textEditProName;
        private DevExpress.XtraEditors.LabelControl labelCalibName;
        private DevExpress.XtraEditors.TextEdit textEditSamType;
        private DevExpress.XtraEditors.LabelControl labelSamType;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxCalibTime;
        private DevExpress.XtraEditors.LabelControl labelCalibTime;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditCuveNum;
        private DevExpress.XtraEditors.ComboBoxEdit comBoxEditCalibName;
    }
}