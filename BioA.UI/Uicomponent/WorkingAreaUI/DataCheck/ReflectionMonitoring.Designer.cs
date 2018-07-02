namespace BioA.UI
{
    partial class ReflectionMonitoring
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReflectionMonitoring));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.txtProjectState = new DevExpress.XtraEditors.TextEdit();
            this.lblSampleStatus = new System.Windows.Forms.Label();
            this.lblSampleID = new System.Windows.Forms.Label();
            this.lblSampieSize = new System.Windows.Forms.Label();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtSampleNum = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpApplyTime = new System.Windows.Forms.DateTimePicker();
            this.txtSampleName = new DevExpress.XtraEditors.TextEdit();
            this.txtConcResult = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.chartReaction = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcResult.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartReaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtProjectState
            // 
            this.txtProjectState.Location = new System.Drawing.Point(629, 52);
            this.txtProjectState.Name = "txtProjectState";
            this.txtProjectState.Properties.ReadOnly = true;
            this.txtProjectState.Size = new System.Drawing.Size(90, 20);
            this.txtProjectState.TabIndex = 36;
            // 
            // lblSampleStatus
            // 
            this.lblSampleStatus.AutoSize = true;
            this.lblSampleStatus.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleStatus.Location = new System.Drawing.Point(557, 53);
            this.lblSampleStatus.Name = "lblSampleStatus";
            this.lblSampleStatus.Size = new System.Drawing.Size(78, 17);
            this.lblSampleStatus.TabIndex = 30;
            this.lblSampleStatus.Text = "项目状态：";
            // 
            // lblSampleID
            // 
            this.lblSampleID.AutoSize = true;
            this.lblSampleID.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleID.Location = new System.Drawing.Point(384, 15);
            this.lblSampleID.Name = "lblSampleID";
            this.lblSampleID.Size = new System.Drawing.Size(78, 17);
            this.lblSampleID.TabIndex = 27;
            this.lblSampleID.Text = "项目名称：";
            // 
            // lblSampieSize
            // 
            this.lblSampieSize.AutoSize = true;
            this.lblSampieSize.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampieSize.Location = new System.Drawing.Point(168, 17);
            this.lblSampieSize.Name = "lblSampieSize";
            this.lblSampieSize.Size = new System.Drawing.Size(78, 17);
            this.lblSampieSize.TabIndex = 24;
            this.lblSampieSize.Text = "样本编号：";
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(840, 549);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 48);
            this.btnClose.TabIndex = 40;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSampleNum
            // 
            this.txtSampleNum.Location = new System.Drawing.Point(242, 16);
            this.txtSampleNum.Name = "txtSampleNum";
            this.txtSampleNum.Properties.ReadOnly = true;
            this.txtSampleNum.Size = new System.Drawing.Size(100, 20);
            this.txtSampleNum.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(592, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 43;
            this.label1.Text = "申请时间：";
            // 
            // dtpApplyTime
            // 
            this.dtpApplyTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpApplyTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApplyTime.Location = new System.Drawing.Point(666, 12);
            this.dtpApplyTime.Name = "dtpApplyTime";
            this.dtpApplyTime.Size = new System.Drawing.Size(169, 22);
            this.dtpApplyTime.TabIndex = 44;
            // 
            // txtSampleName
            // 
            this.txtSampleName.Location = new System.Drawing.Point(456, 14);
            this.txtSampleName.Name = "txtSampleName";
            this.txtSampleName.Properties.ReadOnly = true;
            this.txtSampleName.Size = new System.Drawing.Size(90, 20);
            this.txtSampleName.TabIndex = 45;
            // 
            // txtConcResult
            // 
            this.txtConcResult.Location = new System.Drawing.Point(307, 52);
            this.txtConcResult.Name = "txtConcResult";
            this.txtConcResult.Properties.ReadOnly = true;
            this.txtConcResult.Size = new System.Drawing.Size(90, 20);
            this.txtConcResult.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(261, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 46;
            this.label2.Text = "结果：";
            // 
            // chartReaction
            // 
            this.chartReaction.DataBindings = null;
            xyDiagram1.AxisX.MinorCount = 1;
            xyDiagram1.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram1.AxisX.Title.Text = "测光点";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "42.5";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisX.WholeRange.Auto = false;
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisX.WholeRange.MaxValueSerializable = "42.5";
            xyDiagram1.AxisX.WholeRange.MinValueSerializable = "0";
            xyDiagram1.AxisX.WholeRange.SideMarginsValue = 1D;
            xyDiagram1.AxisY.MinorCount = 9;
            xyDiagram1.AxisY.NumericScaleOptions.AutoGrid = false;
            xyDiagram1.AxisY.NumericScaleOptions.GridSpacing = 0.1D;
            xyDiagram1.AxisY.Title.Text = "吸光度";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisualRange.Auto = false;
            xyDiagram1.AxisY.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "0.636857142857143";
            xyDiagram1.AxisY.VisualRange.MinValueSerializable = "-0.223142857142857";
            xyDiagram1.AxisY.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisY.WholeRange.Auto = false;
            xyDiagram1.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "4";
            xyDiagram1.AxisY.WholeRange.MinValueSerializable = "-4";
            xyDiagram1.AxisY.WholeRange.SideMarginsValue = 0.05D;
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            xyDiagram1.EnableAxisYScrolling = true;
            xyDiagram1.EnableAxisYZooming = true;
            this.chartReaction.Diagram = xyDiagram1;
            this.chartReaction.Legend.Name = "Default Legend";
            this.chartReaction.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartReaction.Location = new System.Drawing.Point(44, 91);
            this.chartReaction.Name = "chartReaction";
            pointSeriesLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Label = pointSeriesLabel1;
            series1.LegendName = "Default Legend";
            lineSeriesView1.LineMarkerOptions.BorderColor = System.Drawing.Color.White;
            lineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.White;
            series1.View = lineSeriesView1;
            this.chartReaction.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartReaction.Size = new System.Drawing.Size(906, 452);
            this.chartReaction.TabIndex = 41;
            chartTitle1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartTitle1.Text = "反应进程";
            this.chartReaction.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // ReflectionMonitoring
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 606);
            this.Controls.Add(this.txtConcResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSampleName);
            this.Controls.Add(this.dtpApplyTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartReaction);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtProjectState);
            this.Controls.Add(this.txtSampleNum);
            this.Controls.Add(this.lblSampleStatus);
            this.Controls.Add(this.lblSampleID);
            this.Controls.Add(this.lblSampieSize);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ReflectionMonitoring";
            this.Text = "反应监控/反应曲线";
            this.Load += new System.EventHandler(this.ReflectionMonitoring_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcResult.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartReaction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtProjectState;
        private System.Windows.Forms.Label lblSampleStatus;
        private System.Windows.Forms.Label lblSampleID;
        private System.Windows.Forms.Label lblSampieSize;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtSampleNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpApplyTime;
        private DevExpress.XtraEditors.TextEdit txtSampleName;
        private DevExpress.XtraEditors.TextEdit txtConcResult;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraCharts.ChartControl chartReaction;
    }
}