namespace BioA.UI
{
    partial class DataCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataCheck));
            this.btnSendLIS = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnExamine = new DevExpress.XtraEditors.SimpleButton();
            this.btnBatchExamine = new DevExpress.XtraEditors.SimpleButton();
            this.btnReactionMonitoring = new DevExpress.XtraEditors.SimpleButton();
            this.btnReview = new DevExpress.XtraEditors.SimpleButton();
            this.btnFilter = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lblTO = new DevExpress.XtraEditors.LabelControl();
            this.txtCaseNumber = new DevExpress.XtraEditors.TextEdit();
            this.lstvInspectProInfo = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtSampleNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblCaseNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblInspectTime = new DevExpress.XtraEditors.LabelControl();
            this.lblSampleNumber = new DevExpress.XtraEditors.LabelControl();
            this.grpSampleCheck = new System.Windows.Forms.GroupBox();
            this.chkEmergencyTreatment = new DevExpress.XtraEditors.CheckEdit();
            this.chkRoutineSample = new DevExpress.XtraEditors.CheckEdit();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.chkFilterClose = new DevExpress.XtraEditors.CheckEdit();
            this.chkFilterOpen = new DevExpress.XtraEditors.CheckEdit();
            this.btnReportPrint = new DevExpress.XtraEditors.SimpleButton();
            this.dtpInspectTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dtpInspectTimeOld = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lstvSampleInfo = new DevExpress.XtraGrid.GridControl();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnReverseSelection = new DevExpress.XtraEditors.SimpleButton();
            this.DiscreteStatistics = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaseNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvInspectProInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.grpSampleCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmergencyTreatment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoutineSample.Properties)).BeginInit();
            this.grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilterClose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilterOpen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvSampleInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSendLIS
            // 
            this.btnSendLIS.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendLIS.Appearance.Options.UseFont = true;
            this.btnSendLIS.Image = ((System.Drawing.Image)(resources.GetObject("btnSendLIS.Image")));
            this.btnSendLIS.Location = new System.Drawing.Point(348, 817);
            this.btnSendLIS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSendLIS.Name = "btnSendLIS";
            this.btnSendLIS.Size = new System.Drawing.Size(106, 44);
            this.btnSendLIS.TabIndex = 57;
            this.btnSendLIS.Text = "发送LIS";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(1489, 814);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(101, 44);
            this.btnDelete.TabIndex = 56;
            this.btnDelete.Text = "删除结果";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExamine
            // 
            this.btnExamine.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExamine.Appearance.Options.UseFont = true;
            this.btnExamine.Image = ((System.Drawing.Image)(resources.GetObject("btnExamine.Image")));
            this.btnExamine.Location = new System.Drawing.Point(460, 817);
            this.btnExamine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExamine.Name = "btnExamine";
            this.btnExamine.Size = new System.Drawing.Size(106, 44);
            this.btnExamine.TabIndex = 54;
            this.btnExamine.Text = "审核";
            this.btnExamine.Click += new System.EventHandler(this.btnExamine_Click);
            // 
            // btnBatchExamine
            // 
            this.btnBatchExamine.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatchExamine.Appearance.Options.UseFont = true;
            this.btnBatchExamine.Image = ((System.Drawing.Image)(resources.GetObject("btnBatchExamine.Image")));
            this.btnBatchExamine.Location = new System.Drawing.Point(570, 817);
            this.btnBatchExamine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBatchExamine.Name = "btnBatchExamine";
            this.btnBatchExamine.Size = new System.Drawing.Size(106, 44);
            this.btnBatchExamine.TabIndex = 53;
            this.btnBatchExamine.Text = "批量";
            this.btnBatchExamine.Click += new System.EventHandler(this.btnBatchExamine_Click);
            // 
            // btnReactionMonitoring
            // 
            this.btnReactionMonitoring.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReactionMonitoring.Appearance.Options.UseFont = true;
            this.btnReactionMonitoring.Image = ((System.Drawing.Image)(resources.GetObject("btnReactionMonitoring.Image")));
            this.btnReactionMonitoring.Location = new System.Drawing.Point(1045, 816);
            this.btnReactionMonitoring.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReactionMonitoring.Name = "btnReactionMonitoring";
            this.btnReactionMonitoring.Size = new System.Drawing.Size(109, 44);
            this.btnReactionMonitoring.TabIndex = 52;
            this.btnReactionMonitoring.Text = "反应监控";
            this.btnReactionMonitoring.Click += new System.EventHandler(this.btnReactionMonitoring_Click);
            // 
            // btnReview
            // 
            this.btnReview.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReview.Appearance.Options.UseFont = true;
            this.btnReview.Image = ((System.Drawing.Image)(resources.GetObject("btnReview.Image")));
            this.btnReview.Location = new System.Drawing.Point(1317, 815);
            this.btnReview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(78, 44);
            this.btnReview.TabIndex = 51;
            this.btnReview.Text = "复查";
            this.btnReview.Click += new System.EventHandler(this.btnReview_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.Appearance.Options.UseFont = true;
            this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
            this.btnFilter.Location = new System.Drawing.Point(238, 817);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(106, 44);
            this.btnFilter.TabIndex = 50;
            this.btnFilter.Text = "筛选";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(945, 64);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(94, 49);
            this.simpleButton1.TabIndex = 47;
            this.simpleButton1.Text = "查 找";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lblTO
            // 
            this.lblTO.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTO.Appearance.Options.UseFont = true;
            this.lblTO.Location = new System.Drawing.Point(544, 82);
            this.lblTO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblTO.Name = "lblTO";
            this.lblTO.Size = new System.Drawing.Size(5, 17);
            this.lblTO.TabIndex = 45;
            this.lblTO.Text = "-";
            // 
            // txtCaseNumber
            // 
            this.txtCaseNumber.Location = new System.Drawing.Point(791, 44);
            this.txtCaseNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCaseNumber.Name = "txtCaseNumber";
            this.txtCaseNumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaseNumber.Properties.Appearance.Options.UseFont = true;
            this.txtCaseNumber.Size = new System.Drawing.Size(89, 24);
            this.txtCaseNumber.TabIndex = 43;
            // 
            // lstvInspectProInfo
            // 
            this.lstvInspectProInfo.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstvInspectProInfo.Location = new System.Drawing.Point(815, 129);
            this.lstvInspectProInfo.MainView = this.gridView2;
            this.lstvInspectProInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstvInspectProInfo.Name = "lstvInspectProInfo";
            this.lstvInspectProInfo.Size = new System.Drawing.Size(891, 641);
            this.lstvInspectProInfo.TabIndex = 41;
            this.lstvInspectProInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.lstvInspectProInfo;
            this.gridView2.IndicatorWidth = 40;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView2_CustomDrawRowIndicator);
            this.gridView2.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView2_CellValueChanging);
            // 
            // txtSampleNumber
            // 
            this.txtSampleNumber.Location = new System.Drawing.Point(405, 45);
            this.txtSampleNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSampleNumber.Name = "txtSampleNumber";
            this.txtSampleNumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSampleNumber.Properties.Appearance.Options.UseFont = true;
            this.txtSampleNumber.Size = new System.Drawing.Size(89, 24);
            this.txtSampleNumber.TabIndex = 39;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(597, 45);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.Size = new System.Drawing.Size(89, 24);
            this.txtName.TabIndex = 37;
            // 
            // lblName
            // 
            this.lblName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Appearance.Options.UseFont = true;
            this.lblName.Location = new System.Drawing.Point(530, 46);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(42, 17);
            this.lblName.TabIndex = 35;
            this.lblName.Text = "姓名：";
            // 
            // lblCaseNumber
            // 
            this.lblCaseNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaseNumber.Appearance.Options.UseFont = true;
            this.lblCaseNumber.Location = new System.Drawing.Point(725, 46);
            this.lblCaseNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblCaseNumber.Name = "lblCaseNumber";
            this.lblCaseNumber.Size = new System.Drawing.Size(56, 17);
            this.lblCaseNumber.TabIndex = 33;
            this.lblCaseNumber.Text = "样本ID：";
            // 
            // lblInspectTime
            // 
            this.lblInspectTime.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspectTime.Appearance.Options.UseFont = true;
            this.lblInspectTime.Location = new System.Drawing.Point(335, 80);
            this.lblInspectTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblInspectTime.Name = "lblInspectTime";
            this.lblInspectTime.Size = new System.Drawing.Size(70, 17);
            this.lblInspectTime.TabIndex = 32;
            this.lblInspectTime.Text = "申请时间：";
            // 
            // lblSampleNumber
            // 
            this.lblSampleNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleNumber.Appearance.Options.UseFont = true;
            this.lblSampleNumber.Location = new System.Drawing.Point(336, 46);
            this.lblSampleNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblSampleNumber.Name = "lblSampleNumber";
            this.lblSampleNumber.Size = new System.Drawing.Size(70, 17);
            this.lblSampleNumber.TabIndex = 31;
            this.lblSampleNumber.Text = "样本编号：";
            // 
            // grpSampleCheck
            // 
            this.grpSampleCheck.Controls.Add(this.chkEmergencyTreatment);
            this.grpSampleCheck.Controls.Add(this.chkRoutineSample);
            this.grpSampleCheck.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSampleCheck.Location = new System.Drawing.Point(182, 39);
            this.grpSampleCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSampleCheck.Name = "grpSampleCheck";
            this.grpSampleCheck.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSampleCheck.Size = new System.Drawing.Size(122, 64);
            this.grpSampleCheck.TabIndex = 30;
            this.grpSampleCheck.TabStop = false;
            this.grpSampleCheck.Text = "查看：";
            // 
            // chkEmergencyTreatment
            // 
            this.chkEmergencyTreatment.Location = new System.Drawing.Point(21, 39);
            this.chkEmergencyTreatment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkEmergencyTreatment.Name = "chkEmergencyTreatment";
            this.chkEmergencyTreatment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmergencyTreatment.Properties.Appearance.Options.UseFont = true;
            this.chkEmergencyTreatment.Properties.Caption = "急诊";
            this.chkEmergencyTreatment.Size = new System.Drawing.Size(67, 21);
            this.chkEmergencyTreatment.TabIndex = 1;
            // 
            // chkRoutineSample
            // 
            this.chkRoutineSample.Location = new System.Drawing.Point(21, 17);
            this.chkRoutineSample.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRoutineSample.Name = "chkRoutineSample";
            this.chkRoutineSample.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRoutineSample.Properties.Appearance.Options.UseFont = true;
            this.chkRoutineSample.Properties.Caption = "常规样本";
            this.chkRoutineSample.Size = new System.Drawing.Size(86, 21);
            this.chkRoutineSample.TabIndex = 0;
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.chkFilterClose);
            this.grpFilter.Controls.Add(this.chkFilterOpen);
            this.grpFilter.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFilter.Location = new System.Drawing.Point(31, 38);
            this.grpFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpFilter.Size = new System.Drawing.Size(106, 53);
            this.grpFilter.TabIndex = 29;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "筛选：";
            // 
            // chkFilterClose
            // 
            this.chkFilterClose.Location = new System.Drawing.Point(62, 21);
            this.chkFilterClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkFilterClose.Name = "chkFilterClose";
            this.chkFilterClose.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFilterClose.Properties.Appearance.Options.UseFont = true;
            this.chkFilterClose.Properties.Caption = "关";
            this.chkFilterClose.Size = new System.Drawing.Size(38, 21);
            this.chkFilterClose.TabIndex = 1;
            this.chkFilterClose.CheckedChanged += new System.EventHandler(this.chkFilterClose_CheckedChanged);
            // 
            // chkFilterOpen
            // 
            this.chkFilterOpen.Location = new System.Drawing.Point(21, 21);
            this.chkFilterOpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkFilterOpen.Name = "chkFilterOpen";
            this.chkFilterOpen.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFilterOpen.Properties.Appearance.Options.UseFont = true;
            this.chkFilterOpen.Properties.Caption = "开";
            this.chkFilterOpen.Size = new System.Drawing.Size(35, 21);
            this.chkFilterOpen.TabIndex = 0;
            this.chkFilterOpen.CheckedChanged += new System.EventHandler(this.chkFilterOpen_CheckedChanged);
            // 
            // btnReportPrint
            // 
            this.btnReportPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportPrint.Appearance.Options.UseFont = true;
            this.btnReportPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnReportPrint.Image")));
            this.btnReportPrint.Location = new System.Drawing.Point(933, 817);
            this.btnReportPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReportPrint.Name = "btnReportPrint";
            this.btnReportPrint.Size = new System.Drawing.Size(106, 44);
            this.btnReportPrint.TabIndex = 58;
            this.btnReportPrint.Text = "报告打印";
            this.btnReportPrint.Click += new System.EventHandler(this.btnReportPrint_Click);
            // 
            // dtpInspectTimeStart
            // 
            this.dtpInspectTimeStart.CustomFormat = "yyyy-MM-dd";
            this.dtpInspectTimeStart.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpInspectTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInspectTimeStart.Location = new System.Drawing.Point(401, 80);
            this.dtpInspectTimeStart.Name = "dtpInspectTimeStart";
            this.dtpInspectTimeStart.Size = new System.Drawing.Size(137, 23);
            this.dtpInspectTimeStart.TabIndex = 59;
            // 
            // dtpInspectTimeOld
            // 
            this.dtpInspectTimeOld.CustomFormat = "yyyy-MM-dd";
            this.dtpInspectTimeOld.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpInspectTimeOld.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInspectTimeOld.Location = new System.Drawing.Point(555, 80);
            this.dtpInspectTimeOld.Name = "dtpInspectTimeOld";
            this.dtpInspectTimeOld.Size = new System.Drawing.Size(131, 23);
            this.dtpInspectTimeOld.TabIndex = 60;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(1402, 815);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 43);
            this.btnSave.TabIndex = 61;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(1046, 776);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(488, 17);
            this.label1.TabIndex = 62;
            this.label1.Text = "注：复查项目仅能选择一项作为确认项,如果不选中，则默认第一次为确认项目";
            // 
            // gridView1
            // 
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(1704, 838, 216, 187);
            this.gridView1.GridControl = this.lstvSampleInfo;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseMove);
            // 
            // lstvSampleInfo
            // 
            this.lstvSampleInfo.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstvSampleInfo.Location = new System.Drawing.Point(11, 129);
            this.lstvSampleInfo.MainView = this.gridView1;
            this.lstvSampleInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstvSampleInfo.Name = "lstvSampleInfo";
            this.lstvSampleInfo.Size = new System.Drawing.Size(798, 673);
            this.lstvSampleInfo.TabIndex = 40;
            this.lstvSampleInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.lstvSampleInfo.Click += new System.EventHandler(this.lstvSampleInfo_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(1161, 816);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(74, 43);
            this.btnSelect.TabIndex = 63;
            this.btnSelect.Text = "全选";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnReverseSelection
            // 
            this.btnReverseSelection.Image = ((System.Drawing.Image)(resources.GetObject("btnReverseSelection.Image")));
            this.btnReverseSelection.Location = new System.Drawing.Point(1241, 816);
            this.btnReverseSelection.Name = "btnReverseSelection";
            this.btnReverseSelection.Size = new System.Drawing.Size(70, 43);
            this.btnReverseSelection.TabIndex = 64;
            this.btnReverseSelection.Text = "反选";
            this.btnReverseSelection.Click += new System.EventHandler(this.btnReverseSelection_Click);
            // 
            // DiscreteStatistics
            // 
            this.DiscreteStatistics.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscreteStatistics.Appearance.Options.UseFont = true;
            this.DiscreteStatistics.Image = ((System.Drawing.Image)(resources.GetObject("DiscreteStatistics.Image")));
            this.DiscreteStatistics.Location = new System.Drawing.Point(1595, 815);
            this.DiscreteStatistics.Name = "DiscreteStatistics";
            this.DiscreteStatistics.Size = new System.Drawing.Size(111, 43);
            this.DiscreteStatistics.TabIndex = 65;
            this.DiscreteStatistics.Text = "离 散 统 计";
            this.DiscreteStatistics.Click += new System.EventHandler(this.DiscreteStatistics_Click);
            // 
            // DataCheck
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.DiscreteStatistics);
            this.Controls.Add(this.btnReverseSelection);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpInspectTimeOld);
            this.Controls.Add(this.dtpInspectTimeStart);
            this.Controls.Add(this.btnReportPrint);
            this.Controls.Add(this.btnSendLIS);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExamine);
            this.Controls.Add(this.btnBatchExamine);
            this.Controls.Add(this.btnReactionMonitoring);
            this.Controls.Add(this.btnReview);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.lblTO);
            this.Controls.Add(this.txtCaseNumber);
            this.Controls.Add(this.lstvInspectProInfo);
            this.Controls.Add(this.lstvSampleInfo);
            this.Controls.Add(this.txtSampleNumber);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCaseNumber);
            this.Controls.Add(this.lblInspectTime);
            this.Controls.Add(this.lblSampleNumber);
            this.Controls.Add(this.grpSampleCheck);
            this.Controls.Add(this.grpFilter);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "DataCheck";
            this.Size = new System.Drawing.Size(1767, 872);
            this.Load += new System.EventHandler(this.DataCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCaseNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvInspectProInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.grpSampleCheck.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkEmergencyTreatment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoutineSample.Properties)).EndInit();
            this.grpFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkFilterClose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilterOpen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvSampleInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSendLIS;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnExamine;
        private DevExpress.XtraEditors.SimpleButton btnBatchExamine;
        private DevExpress.XtraEditors.SimpleButton btnReactionMonitoring;
        private DevExpress.XtraEditors.SimpleButton btnReview;
        private DevExpress.XtraEditors.SimpleButton btnFilter;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl lblTO;
        private DevExpress.XtraEditors.TextEdit txtCaseNumber;
        private DevExpress.XtraGrid.GridControl lstvInspectProInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.TextEdit txtSampleNumber;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblCaseNumber;
        private DevExpress.XtraEditors.LabelControl lblInspectTime;
        private DevExpress.XtraEditors.LabelControl lblSampleNumber;
        private System.Windows.Forms.GroupBox grpSampleCheck;
        private DevExpress.XtraEditors.CheckEdit chkRoutineSample;
        private System.Windows.Forms.GroupBox grpFilter;
        private DevExpress.XtraEditors.CheckEdit chkFilterClose;
        private DevExpress.XtraEditors.CheckEdit chkFilterOpen;
        private DevExpress.XtraEditors.SimpleButton btnReportPrint;
        private System.Windows.Forms.DateTimePicker dtpInspectTimeStart;
        private System.Windows.Forms.DateTimePicker dtpInspectTimeOld;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl lstvSampleInfo;
        private DevExpress.XtraEditors.CheckEdit chkEmergencyTreatment;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.SimpleButton btnReverseSelection;
        private DevExpress.XtraEditors.SimpleButton DiscreteStatistics;
    }
}
