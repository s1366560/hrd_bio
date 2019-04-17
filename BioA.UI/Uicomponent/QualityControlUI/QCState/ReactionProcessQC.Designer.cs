namespace BioA.UI
{
    partial class ReactionProcessQC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReactionProcessQC));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.lblTestItem = new DevExpress.XtraEditors.LabelControl();
            this.lblCupNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblConcentration = new DevExpress.XtraEditors.LabelControl();
            this.lblabsorbance = new DevExpress.XtraEditors.LabelControl();
            this.lblMeteringPoint = new DevExpress.XtraEditors.LabelControl();
            this.txtConcResult = new DevExpress.XtraEditors.TextEdit();
            this.txtAbsorb = new DevExpress.XtraEditors.TextEdit();
            this.txtReactionCupNum = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.chartQCReaction = new DevExpress.XtraCharts.ChartControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtProjectName = new DevExpress.XtraEditors.TextEdit();
            this.txtSampleType = new DevExpress.XtraEditors.TextEdit();
            this.txtQCName = new DevExpress.XtraEditors.TextEdit();
            this.txtLotNum = new DevExpress.XtraEditors.TextEdit();
            this.txtManufacturer = new DevExpress.XtraEditors.TextEdit();
            this.cboMeasurePoint = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtLevelConc = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcResult.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbsorb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReactionCupNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartQCReaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMeasurePoint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLevelConc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTestItem
            // 
            this.lblTestItem.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestItem.Appearance.Options.UseFont = true;
            this.lblTestItem.Location = new System.Drawing.Point(37, 7);
            this.lblTestItem.Name = "lblTestItem";
            this.lblTestItem.Size = new System.Drawing.Size(70, 17);
            this.lblTestItem.TabIndex = 0;
            this.lblTestItem.Text = "测试项目：";
            // 
            // lblCupNumber
            // 
            this.lblCupNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCupNumber.Appearance.Options.UseFont = true;
            this.lblCupNumber.Location = new System.Drawing.Point(31, 740);
            this.lblCupNumber.Name = "lblCupNumber";
            this.lblCupNumber.Size = new System.Drawing.Size(70, 17);
            this.lblCupNumber.TabIndex = 2;
            this.lblCupNumber.Text = "反应杯号：";
            // 
            // lblConcentration
            // 
            this.lblConcentration.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConcentration.Appearance.Options.UseFont = true;
            this.lblConcentration.Location = new System.Drawing.Point(488, 7);
            this.lblConcentration.Name = "lblConcentration";
            this.lblConcentration.Size = new System.Drawing.Size(42, 17);
            this.lblConcentration.TabIndex = 3;
            this.lblConcentration.Text = "浓度：";
            // 
            // lblabsorbance
            // 
            this.lblabsorbance.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblabsorbance.Appearance.Options.UseFont = true;
            this.lblabsorbance.Location = new System.Drawing.Point(452, 740);
            this.lblabsorbance.Name = "lblabsorbance";
            this.lblabsorbance.Size = new System.Drawing.Size(56, 17);
            this.lblabsorbance.TabIndex = 5;
            this.lblabsorbance.Text = "吸光度：";
            // 
            // lblMeteringPoint
            // 
            this.lblMeteringPoint.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMeteringPoint.Appearance.Options.UseFont = true;
            this.lblMeteringPoint.Location = new System.Drawing.Point(250, 740);
            this.lblMeteringPoint.Name = "lblMeteringPoint";
            this.lblMeteringPoint.Size = new System.Drawing.Size(56, 17);
            this.lblMeteringPoint.TabIndex = 6;
            this.lblMeteringPoint.Text = "测光点：";
            // 
            // txtConcResult
            // 
            this.txtConcResult.Location = new System.Drawing.Point(563, 4);
            this.txtConcResult.Name = "txtConcResult";
            this.txtConcResult.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConcResult.Properties.Appearance.Options.UseFont = true;
            this.txtConcResult.Properties.ReadOnly = true;
            this.txtConcResult.Size = new System.Drawing.Size(100, 24);
            this.txtConcResult.TabIndex = 7;
            // 
            // txtAbsorb
            // 
            this.txtAbsorb.Location = new System.Drawing.Point(514, 737);
            this.txtAbsorb.Name = "txtAbsorb";
            this.txtAbsorb.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAbsorb.Properties.Appearance.Options.UseFont = true;
            this.txtAbsorb.Properties.ReadOnly = true;
            this.txtAbsorb.Size = new System.Drawing.Size(100, 24);
            this.txtAbsorb.TabIndex = 8;
            // 
            // txtReactionCupNum
            // 
            this.txtReactionCupNum.Location = new System.Drawing.Point(107, 737);
            this.txtReactionCupNum.Name = "txtReactionCupNum";
            this.txtReactionCupNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReactionCupNum.Properties.Appearance.Options.UseFont = true;
            this.txtReactionCupNum.Properties.ReadOnly = true;
            this.txtReactionCupNum.Size = new System.Drawing.Size(100, 24);
            this.txtReactionCupNum.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1285, 730);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 36);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chartQCReaction
            // 
            this.chartQCReaction.DataBindings = null;
            xyDiagram1.AxisX.GridLines.Visible = true;
            xyDiagram1.AxisX.MinorCount = 1;
            xyDiagram1.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram1.AxisX.Title.Text = "测光点";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "44";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "0";
            xyDiagram1.AxisX.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisX.WholeRange.Auto = false;
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisX.WholeRange.MaxValueSerializable = "44";
            xyDiagram1.AxisX.WholeRange.MinValueSerializable = "0";
            xyDiagram1.AxisX.WholeRange.SideMarginsValue = 1D;
            xyDiagram1.AxisY.MinorCount = 9;
            xyDiagram1.AxisY.Title.Text = "吸光度";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisualRange.Auto = false;
            xyDiagram1.AxisY.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "1.2";
            xyDiagram1.AxisY.VisualRange.MinValueSerializable = "-0.4";
            xyDiagram1.AxisY.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisY.WholeRange.Auto = false;
            xyDiagram1.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "4";
            xyDiagram1.AxisY.WholeRange.MinValueSerializable = "-4";
            xyDiagram1.AxisY.WholeRange.SideMarginsValue = 0D;
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            xyDiagram1.EnableAxisYScrolling = true;
            xyDiagram1.EnableAxisYZooming = true;
            this.chartQCReaction.Diagram = xyDiagram1;
            this.chartQCReaction.Legend.Name = "Default Legend";
            this.chartQCReaction.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartQCReaction.Location = new System.Drawing.Point(19, 63);
            this.chartQCReaction.Name = "chartQCReaction";
            series1.Name = "Series 1";
            series1.View = lineSeriesView1;
            this.chartQCReaction.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartQCReaction.Size = new System.Drawing.Size(1351, 656);
            this.chartQCReaction.TabIndex = 13;
            chartTitle1.Text = "反应进程";
            this.chartQCReaction.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(279, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 17);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "样本类型：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(36, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 17);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "质控品名称：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(279, 41);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 17);
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "批号：";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(761, 39);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(70, 17);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "生产厂家：";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(126, 4);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjectName.Properties.Appearance.Options.UseFont = true;
            this.txtProjectName.Properties.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(125, 24);
            this.txtProjectName.TabIndex = 19;
            // 
            // txtSampleType
            // 
            this.txtSampleType.Location = new System.Drawing.Point(355, 4);
            this.txtSampleType.Name = "txtSampleType";
            this.txtSampleType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSampleType.Properties.Appearance.Options.UseFont = true;
            this.txtSampleType.Properties.ReadOnly = true;
            this.txtSampleType.Size = new System.Drawing.Size(100, 24);
            this.txtSampleType.TabIndex = 20;
            // 
            // txtQCName
            // 
            this.txtQCName.Location = new System.Drawing.Point(126, 38);
            this.txtQCName.Name = "txtQCName";
            this.txtQCName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQCName.Properties.Appearance.Options.UseFont = true;
            this.txtQCName.Properties.ReadOnly = true;
            this.txtQCName.Size = new System.Drawing.Size(125, 24);
            this.txtQCName.TabIndex = 21;
            // 
            // txtLotNum
            // 
            this.txtLotNum.Location = new System.Drawing.Point(355, 38);
            this.txtLotNum.Name = "txtLotNum";
            this.txtLotNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotNum.Properties.Appearance.Options.UseFont = true;
            this.txtLotNum.Properties.ReadOnly = true;
            this.txtLotNum.Size = new System.Drawing.Size(100, 24);
            this.txtLotNum.TabIndex = 22;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Location = new System.Drawing.Point(837, 38);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManufacturer.Properties.Appearance.Options.UseFont = true;
            this.txtManufacturer.Properties.ReadOnly = true;
            this.txtManufacturer.Size = new System.Drawing.Size(222, 24);
            this.txtManufacturer.TabIndex = 23;
            // 
            // cboMeasurePoint
            // 
            this.cboMeasurePoint.Location = new System.Drawing.Point(312, 737);
            this.cboMeasurePoint.Name = "cboMeasurePoint";
            this.cboMeasurePoint.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMeasurePoint.Properties.Appearance.Options.UseFont = true;
            this.cboMeasurePoint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMeasurePoint.Size = new System.Drawing.Size(100, 24);
            this.cboMeasurePoint.TabIndex = 11;
            this.cboMeasurePoint.SelectedIndexChanged += new System.EventHandler(this.cboMeasurePoint_SelectedIndexChanged);
            // 
            // txtLevelConc
            // 
            this.txtLevelConc.Location = new System.Drawing.Point(563, 38);
            this.txtLevelConc.Name = "txtLevelConc";
            this.txtLevelConc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLevelConc.Properties.Appearance.Options.UseFont = true;
            this.txtLevelConc.Properties.ReadOnly = true;
            this.txtLevelConc.Size = new System.Drawing.Size(100, 24);
            this.txtLevelConc.TabIndex = 25;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(487, 41);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(70, 17);
            this.labelControl5.TabIndex = 24;
            this.labelControl5.Text = "水平浓度：";
            // 
            // ReactionProcessQC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 773);
            this.Controls.Add(this.txtLevelConc);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtManufacturer);
            this.Controls.Add(this.txtLotNum);
            this.Controls.Add(this.txtQCName);
            this.Controls.Add(this.txtSampleType);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chartQCReaction);
            this.Controls.Add(this.cboMeasurePoint);
            this.Controls.Add(this.txtReactionCupNum);
            this.Controls.Add(this.txtAbsorb);
            this.Controls.Add(this.txtConcResult);
            this.Controls.Add(this.lblMeteringPoint);
            this.Controls.Add(this.lblabsorbance);
            this.Controls.Add(this.lblConcentration);
            this.Controls.Add(this.lblCupNumber);
            this.Controls.Add(this.lblTestItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ReactionProcessQC";
            this.Text = "反应进程";
            //this.Load += new System.EventHandler(this.ReactionProcessQC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtConcResult.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbsorb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReactionCupNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartQCReaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMeasurePoint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLevelConc.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTestItem;
        private DevExpress.XtraEditors.LabelControl lblCupNumber;
        private DevExpress.XtraEditors.LabelControl lblConcentration;
        private DevExpress.XtraEditors.LabelControl lblabsorbance;
        private DevExpress.XtraEditors.LabelControl lblMeteringPoint;
        private DevExpress.XtraEditors.TextEdit txtConcResult;
        private DevExpress.XtraEditors.TextEdit txtAbsorb;
        private DevExpress.XtraEditors.TextEdit txtReactionCupNum;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraCharts.ChartControl chartQCReaction;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtProjectName;
        private DevExpress.XtraEditors.TextEdit txtSampleType;
        private DevExpress.XtraEditors.TextEdit txtQCName;
        private DevExpress.XtraEditors.TextEdit txtLotNum;
        private DevExpress.XtraEditors.TextEdit txtManufacturer;
        private DevExpress.XtraEditors.ComboBoxEdit cboMeasurePoint;
        private DevExpress.XtraEditors.TextEdit txtLevelConc;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}