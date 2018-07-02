namespace BioA.UI
{
    partial class CalibrationTrace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrationTrace));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            this.lblInspectPro = new DevExpress.XtraEditors.LabelControl();
            this.lblEndTime = new DevExpress.XtraEditors.LabelControl();
            this.lblStartTime = new DevExpress.XtraEditors.LabelControl();
            this.lblReagentName = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtCheckPro = new DevExpress.XtraEditors.TextEdit();
            this.txtReagentName = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtSampleType = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.deStartTime = new DevExpress.XtraEditors.DateEdit();
            this.deEndTime = new DevExpress.XtraEditors.DateEdit();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckPro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReagentName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInspectPro
            // 
            this.lblInspectPro.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspectPro.Appearance.Options.UseFont = true;
            this.lblInspectPro.Location = new System.Drawing.Point(12, 19);
            this.lblInspectPro.Name = "lblInspectPro";
            this.lblInspectPro.Size = new System.Drawing.Size(70, 17);
            this.lblInspectPro.TabIndex = 0;
            this.lblInspectPro.Text = "检测项目：";
            // 
            // lblEndTime
            // 
            this.lblEndTime.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndTime.Appearance.Options.UseFont = true;
            this.lblEndTime.Location = new System.Drawing.Point(770, 19);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(70, 17);
            this.lblEndTime.TabIndex = 1;
            this.lblEndTime.Text = "结束时间：";
            // 
            // lblStartTime
            // 
            this.lblStartTime.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartTime.Appearance.Options.UseFont = true;
            this.lblStartTime.Location = new System.Drawing.Point(535, 20);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(70, 17);
            this.lblStartTime.TabIndex = 2;
            this.lblStartTime.Text = "开始时间：";
            // 
            // lblReagentName
            // 
            this.lblReagentName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReagentName.Appearance.Options.UseFont = true;
            this.lblReagentName.Location = new System.Drawing.Point(362, 19);
            this.lblReagentName.Name = "lblReagentName";
            this.lblReagentName.Size = new System.Drawing.Size(70, 17);
            this.lblReagentName.TabIndex = 3;
            this.lblReagentName.Text = "试剂名称：";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(998, 651);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 53);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtCheckPro
            // 
            this.txtCheckPro.EditValue = "";
            this.txtCheckPro.Location = new System.Drawing.Point(76, 18);
            this.txtCheckPro.Name = "txtCheckPro";
            this.txtCheckPro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckPro.Properties.Appearance.Options.UseFont = true;
            this.txtCheckPro.Properties.ReadOnly = true;
            this.txtCheckPro.Size = new System.Drawing.Size(100, 24);
            this.txtCheckPro.TabIndex = 15;
            // 
            // txtReagentName
            // 
            this.txtReagentName.EditValue = "";
            this.txtReagentName.Location = new System.Drawing.Point(426, 16);
            this.txtReagentName.Name = "txtReagentName";
            this.txtReagentName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReagentName.Properties.Appearance.Options.UseFont = true;
            this.txtReagentName.Properties.ReadOnly = true;
            this.txtReagentName.Size = new System.Drawing.Size(100, 24);
            this.txtReagentName.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(1016, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 43);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSampleType
            // 
            this.txtSampleType.EditValue = "";
            this.txtSampleType.Location = new System.Drawing.Point(251, 18);
            this.txtSampleType.Name = "txtSampleType";
            this.txtSampleType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSampleType.Properties.Appearance.Options.UseFont = true;
            this.txtSampleType.Properties.ReadOnly = true;
            this.txtSampleType.Size = new System.Drawing.Size(100, 24);
            this.txtSampleType.TabIndex = 14;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(187, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 17);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "样本类型：";
            // 
            // deStartTime
            // 
            this.deStartTime.EditValue = null;
            this.deStartTime.Location = new System.Drawing.Point(605, 18);
            this.deStartTime.Name = "deStartTime";
            this.deStartTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deStartTime.Properties.Appearance.Options.UseFont = true;
            this.deStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartTime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.deStartTime.Size = new System.Drawing.Size(159, 24);
            this.deStartTime.TabIndex = 22;
            // 
            // deEndTime
            // 
            this.deEndTime.EditValue = null;
            this.deEndTime.Location = new System.Drawing.Point(837, 18);
            this.deEndTime.Name = "deEndTime";
            this.deEndTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deEndTime.Properties.Appearance.Options.UseFont = true;
            this.deEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndTime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.deEndTime.Size = new System.Drawing.Size(166, 24);
            this.deEndTime.TabIndex = 23;
            // 
            // chartControl1
            // 
            this.chartControl1.DataBindings = null;
            xyDiagram1.AxisX.Label.Font = new System.Drawing.Font("Tahoma", 10F);
            xyDiagram1.AxisX.MinorCount = 1;
            xyDiagram1.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram1.AxisX.Title.Text = "项目检测次数";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "9";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "0";
            xyDiagram1.AxisX.VisualRange.SideMarginsValue = 0.5D;
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisX.WholeRange.SideMarginsValue = 0.5D;
            xyDiagram1.AxisY.GridLines.MinorVisible = true;
            xyDiagram1.AxisY.Interlaced = true;
            xyDiagram1.AxisY.MinorCount = 9;
            xyDiagram1.AxisY.NumericScaleOptions.AutoGrid = false;
            xyDiagram1.AxisY.NumericScaleOptions.GridSpacing = 0.1D;
            xyDiagram1.AxisY.Tickmarks.Length = 6;
            xyDiagram1.AxisY.Title.Text = "吸光度";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisualRange.Auto = false;
            xyDiagram1.AxisY.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "1.5";
            xyDiagram1.AxisY.VisualRange.MinValueSerializable = "-0.5";
            xyDiagram1.AxisY.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisY.WholeRange.Auto = false;
            xyDiagram1.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "4.1";
            xyDiagram1.AxisY.WholeRange.MinValueSerializable = "-4";
            xyDiagram1.AxisY.WholeRange.SideMarginsValue = 0D;
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            xyDiagram1.EnableAxisYScrolling = true;
            xyDiagram1.EnableAxisYZooming = true;
            xyDiagram1.LabelsResolveOverlappingMinIndent = 10;
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartControl1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chartControl1.Legend.MarkerVisible = false;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(11, 54);
            this.chartControl1.Name = "chartControl1";
            pointSeriesLabel1.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.JustifyAroundPoint;
            series1.Label = pointSeriesLabel1;
            series1.LegendName = "Default Legend";
            series1.Name = "Series1";
            series1.TopNOptions.Mode = DevExpress.XtraCharts.TopNMode.ThresholdPercent;
            series1.View = lineSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.SeriesTemplate.LegendName = "Default Legend";
            this.chartControl1.Size = new System.Drawing.Size(1100, 592);
            this.chartControl1.TabIndex = 4;
            this.chartControl1.QueryCursor += new DevExpress.XtraCharts.QueryCursorEventHandler(this.chartControl1_QueryCursor);
            // 
            // CalibrationTrace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 707);
            this.Controls.Add(this.deEndTime);
            this.Controls.Add(this.deStartTime);
            this.Controls.Add(this.txtSampleType);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtReagentName);
            this.Controls.Add(this.txtCheckPro);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.lblReagentName);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.lblInspectPro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CalibrationTrace";
            this.Text = "校准历史追溯";
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckPro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReagentName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblInspectPro;
        private DevExpress.XtraEditors.LabelControl lblEndTime;
        private DevExpress.XtraEditors.LabelControl lblStartTime;
        private DevExpress.XtraEditors.LabelControl lblReagentName;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtCheckPro;
        private DevExpress.XtraEditors.TextEdit txtReagentName;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.TextEdit txtSampleType;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deStartTime;
        private DevExpress.XtraEditors.DateEdit deEndTime;
        private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}