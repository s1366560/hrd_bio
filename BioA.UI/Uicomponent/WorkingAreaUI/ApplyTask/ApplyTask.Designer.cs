namespace BioA.UI
{
    partial class ApplyTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplyTask));
            this.chkManuallyDilute = new DevExpress.XtraEditors.CheckEdit();
            this.chkEmergency = new DevExpress.XtraEditors.CheckEdit();
            this.lblSampleNum = new DevExpress.XtraEditors.LabelControl();
            this.lblDishNum = new DevExpress.XtraEditors.LabelControl();
            this.lblSampleContainer = new DevExpress.XtraEditors.LabelControl();
            this.lblBarcode = new DevExpress.XtraEditors.LabelControl();
            this.lblSampleType = new DevExpress.XtraEditors.LabelControl();
            this.lblSamplePos = new DevExpress.XtraEditors.LabelControl();
            this.grpProject = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.grpCombProject = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.combSampleType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lstvTask = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnBatchInput = new DevExpress.XtraEditors.SimpleButton();
            this.btnPatientInfo = new DevExpress.XtraEditors.SimpleButton();
            this.btnSampleDishState = new DevExpress.XtraEditors.SimpleButton();
            this.btnDilutionSetting = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSampleNum = new DevExpress.XtraEditors.TextEdit();
            this.txtBarCode = new DevExpress.XtraEditors.TextEdit();
            this.combSampleContainer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.combPanelNum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.combPosNum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labDetectionNum = new System.Windows.Forms.Label();
            this.txtBoxDetectionNum = new System.Windows.Forms.TextBox();
            this.simpleButCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkManuallyDilute.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmergency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProject)).BeginInit();
            this.grpProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCombProject)).BeginInit();
            this.grpCombProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combSampleType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combSampleContainer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combPanelNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combPosNum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkManuallyDilute
            // 
            this.chkManuallyDilute.Location = new System.Drawing.Point(469, 73);
            this.chkManuallyDilute.Name = "chkManuallyDilute";
            this.chkManuallyDilute.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkManuallyDilute.Properties.Appearance.Options.UseFont = true;
            this.chkManuallyDilute.Properties.Caption = " 手动稀释";
            this.chkManuallyDilute.Size = new System.Drawing.Size(127, 21);
            this.chkManuallyDilute.TabIndex = 1;
            // 
            // chkEmergency
            // 
            this.chkEmergency.Location = new System.Drawing.Point(1245, 45);
            this.chkEmergency.Name = "chkEmergency";
            this.chkEmergency.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmergency.Properties.Appearance.Options.UseFont = true;
            this.chkEmergency.Properties.Caption = " 急 诊";
            this.chkEmergency.Size = new System.Drawing.Size(113, 21);
            this.chkEmergency.TabIndex = 2;
            // 
            // lblSampleNum
            // 
            this.lblSampleNum.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleNum.Appearance.Options.UseFont = true;
            this.lblSampleNum.Location = new System.Drawing.Point(57, 43);
            this.lblSampleNum.Name = "lblSampleNum";
            this.lblSampleNum.Size = new System.Drawing.Size(70, 17);
            this.lblSampleNum.TabIndex = 3;
            this.lblSampleNum.Text = "样本编号：";
            // 
            // lblDishNum
            // 
            this.lblDishNum.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDishNum.Appearance.Options.UseFont = true;
            this.lblDishNum.Location = new System.Drawing.Point(285, 43);
            this.lblDishNum.Name = "lblDishNum";
            this.lblDishNum.Size = new System.Drawing.Size(42, 17);
            this.lblDishNum.TabIndex = 4;
            this.lblDishNum.Text = "盘号：";
            // 
            // lblSampleContainer
            // 
            this.lblSampleContainer.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleContainer.Appearance.Options.UseFont = true;
            this.lblSampleContainer.Location = new System.Drawing.Point(866, 43);
            this.lblSampleContainer.Name = "lblSampleContainer";
            this.lblSampleContainer.Size = new System.Drawing.Size(70, 17);
            this.lblSampleContainer.TabIndex = 6;
            this.lblSampleContainer.Text = "样本容器：";
            // 
            // lblBarcode
            // 
            this.lblBarcode.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode.Appearance.Options.UseFont = true;
            this.lblBarcode.Location = new System.Drawing.Point(58, 77);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(42, 17);
            this.lblBarcode.TabIndex = 7;
            this.lblBarcode.Text = "条码：";
            // 
            // lblSampleType
            // 
            this.lblSampleType.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleType.Appearance.Options.UseFont = true;
            this.lblSampleType.Location = new System.Drawing.Point(654, 43);
            this.lblSampleType.Name = "lblSampleType";
            this.lblSampleType.Size = new System.Drawing.Size(70, 17);
            this.lblSampleType.TabIndex = 8;
            this.lblSampleType.Text = "样本类型：";
            // 
            // lblSamplePos
            // 
            this.lblSamplePos.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSamplePos.Appearance.Options.UseFont = true;
            this.lblSamplePos.Location = new System.Drawing.Point(469, 43);
            this.lblSamplePos.Name = "lblSamplePos";
            this.lblSamplePos.Size = new System.Drawing.Size(42, 17);
            this.lblSamplePos.TabIndex = 11;
            this.lblSamplePos.Text = "位置：";
            // 
            // grpProject
            // 
            this.grpProject.Location = new System.Drawing.Point(13, 111);
            this.grpProject.Name = "grpProject";
            this.grpProject.SelectedTabPage = this.xtraTabPage1;
            this.grpProject.Size = new System.Drawing.Size(1340, 482);
            this.grpProject.TabIndex = 14;
            this.grpProject.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4});
            this.grpProject.Click += new System.EventHandler(this.grpProject_Click);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1334, 453);
            this.xtraTabPage1.TabPageWidth = 80;
            this.xtraTabPage1.Text = "第一页";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1334, 453);
            this.xtraTabPage2.TabPageWidth = 80;
            this.xtraTabPage2.Text = "第二页";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1334, 453);
            this.xtraTabPage3.TabPageWidth = 80;
            this.xtraTabPage3.Text = "第三页";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1334, 453);
            this.xtraTabPage4.TabPageWidth = 80;
            this.xtraTabPage4.Text = "第四页";
            // 
            // grpCombProject
            // 
            this.grpCombProject.Location = new System.Drawing.Point(14, 607);
            this.grpCombProject.Name = "grpCombProject";
            this.grpCombProject.SelectedTabPage = this.xtraTabPage5;
            this.grpCombProject.Size = new System.Drawing.Size(1339, 229);
            this.grpCombProject.TabIndex = 15;
            this.grpCombProject.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage5,
            this.xtraTabPage6});
            this.grpCombProject.Click += new System.EventHandler(this.grpCombProject_Click);
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(1333, 200);
            this.xtraTabPage5.TabPageWidth = 80;
            this.xtraTabPage5.Text = "第一页";
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Name = "xtraTabPage6";
            this.xtraTabPage6.Size = new System.Drawing.Size(1333, 200);
            this.xtraTabPage6.TabPageWidth = 80;
            this.xtraTabPage6.Text = "第二页";
            // 
            // combSampleType
            // 
            this.combSampleType.Location = new System.Drawing.Point(730, 40);
            this.combSampleType.Name = "combSampleType";
            this.combSampleType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combSampleType.Properties.Appearance.Options.UseFont = true;
            this.combSampleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combSampleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combSampleType.Size = new System.Drawing.Size(100, 24);
            this.combSampleType.TabIndex = 33;
            this.combSampleType.SelectedIndexChanged += new System.EventHandler(this.combSampleType_SelectedIndexChanged);
            // 
            // lstvTask
            // 
            this.lstvTask.Location = new System.Drawing.Point(1362, 41);
            this.lstvTask.MainView = this.gridView1;
            this.lstvTask.Name = "lstvTask";
            this.lstvTask.Size = new System.Drawing.Size(349, 790);
            this.lstvTask.TabIndex = 16;
            this.lstvTask.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.lstvTask.Click += new System.EventHandler(this.lstvTask_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.lstvTask;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(182, 852);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 43);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnApply
            // 
            this.btnApply.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Appearance.Options.UseFont = true;
            this.btnApply.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.Image")));
            this.btnApply.Location = new System.Drawing.Point(73, 852);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(103, 43);
            this.btnApply.TabIndex = 18;
            this.btnApply.Text = "申请";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnBatchInput
            // 
            this.btnBatchInput.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatchInput.Appearance.Options.UseFont = true;
            this.btnBatchInput.Image = ((System.Drawing.Image)(resources.GetObject("btnBatchInput.Image")));
            this.btnBatchInput.Location = new System.Drawing.Point(291, 852);
            this.btnBatchInput.Name = "btnBatchInput";
            this.btnBatchInput.Size = new System.Drawing.Size(103, 43);
            this.btnBatchInput.TabIndex = 19;
            this.btnBatchInput.Text = "批量输入";
            this.btnBatchInput.Click += new System.EventHandler(this.btnBatchInput_Click);
            // 
            // btnPatientInfo
            // 
            this.btnPatientInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientInfo.Appearance.Options.UseFont = true;
            this.btnPatientInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientInfo.Image")));
            this.btnPatientInfo.Location = new System.Drawing.Point(1362, 852);
            this.btnPatientInfo.Name = "btnPatientInfo";
            this.btnPatientInfo.Size = new System.Drawing.Size(118, 43);
            this.btnPatientInfo.TabIndex = 22;
            this.btnPatientInfo.Text = "病人信息";
            this.btnPatientInfo.Click += new System.EventHandler(this.btnPatientInfo_Click);
            // 
            // btnSampleDishState
            // 
            this.btnSampleDishState.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSampleDishState.Appearance.Options.UseFont = true;
            this.btnSampleDishState.Image = ((System.Drawing.Image)(resources.GetObject("btnSampleDishState.Image")));
            this.btnSampleDishState.Location = new System.Drawing.Point(1593, 852);
            this.btnSampleDishState.Name = "btnSampleDishState";
            this.btnSampleDishState.Size = new System.Drawing.Size(118, 43);
            this.btnSampleDishState.TabIndex = 23;
            this.btnSampleDishState.Text = "样本盘状态";
            this.btnSampleDishState.Click += new System.EventHandler(this.BtnSampleDishState_Click);
            // 
            // btnDilutionSetting
            // 
            this.btnDilutionSetting.Location = new System.Drawing.Point(1245, 81);
            this.btnDilutionSetting.Name = "btnDilutionSetting";
            this.btnDilutionSetting.Size = new System.Drawing.Size(103, 30);
            this.btnDilutionSetting.TabIndex = 37;
            this.btnDilutionSetting.Text = "稀释设置";
            this.btnDilutionSetting.Click += new System.EventHandler(this.btnDilutionSetting_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1794, 30);
            this.panelControl1.TabIndex = 37;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(770, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 19);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "任务申请";
            // 
            // txtSampleNum
            // 
            this.txtSampleNum.EditValue = "";
            this.txtSampleNum.Location = new System.Drawing.Point(133, 42);
            this.txtSampleNum.Name = "txtSampleNum";
            this.txtSampleNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSampleNum.Properties.Appearance.Options.UseFont = true;
            this.txtSampleNum.Size = new System.Drawing.Size(100, 24);
            this.txtSampleNum.TabIndex = 24;
            this.txtSampleNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSampleNum_KeyPress);
            // 
            // txtBarCode
            // 
            this.txtBarCode.EditValue = "";
            this.txtBarCode.Location = new System.Drawing.Point(133, 74);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarCode.Properties.Appearance.Options.UseFont = true;
            this.txtBarCode.Size = new System.Drawing.Size(300, 24);
            this.txtBarCode.TabIndex = 29;
            this.txtBarCode.EditValueChanged += new System.EventHandler(this.txtBarCode_EditValueChanged);
            // 
            // combSampleContainer
            // 
            this.combSampleContainer.EditValue = "";
            this.combSampleContainer.Location = new System.Drawing.Point(942, 40);
            this.combSampleContainer.Name = "combSampleContainer";
            this.combSampleContainer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combSampleContainer.Properties.Appearance.Options.UseFont = true;
            this.combSampleContainer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combSampleContainer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combSampleContainer.Size = new System.Drawing.Size(100, 24);
            this.combSampleContainer.TabIndex = 30;
            // 
            // combPanelNum
            // 
            this.combPanelNum.EditValue = "";
            this.combPanelNum.Location = new System.Drawing.Point(333, 40);
            this.combPanelNum.Name = "combPanelNum";
            this.combPanelNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combPanelNum.Properties.Appearance.Options.UseFont = true;
            this.combPanelNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combPanelNum.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combPanelNum.Size = new System.Drawing.Size(100, 24);
            this.combPanelNum.TabIndex = 31;
            this.combPanelNum.SelectedIndexChanged += new System.EventHandler(this.combPanelNum_SelectedIndexChanged);
            // 
            // combPosNum
            // 
            this.combPosNum.EditValue = "";
            this.combPosNum.Location = new System.Drawing.Point(517, 38);
            this.combPosNum.Name = "combPosNum";
            this.combPosNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combPosNum.Properties.Appearance.Options.UseFont = true;
            this.combPosNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combPosNum.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combPosNum.Size = new System.Drawing.Size(100, 24);
            this.combPosNum.TabIndex = 32;
            // 
            // labDetectionNum
            // 
            this.labDetectionNum.AutoSize = true;
            this.labDetectionNum.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDetectionNum.Location = new System.Drawing.Point(1071, 45);
            this.labDetectionNum.Name = "labDetectionNum";
            this.labDetectionNum.Size = new System.Drawing.Size(78, 17);
            this.labDetectionNum.TabIndex = 38;
            this.labDetectionNum.Text = "检测次数：";
            // 
            // txtBoxDetectionNum
            // 
            this.txtBoxDetectionNum.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxDetectionNum.Location = new System.Drawing.Point(1145, 43);
            this.txtBoxDetectionNum.MaxLength = 3;
            this.txtBoxDetectionNum.Name = "txtBoxDetectionNum";
            this.txtBoxDetectionNum.Size = new System.Drawing.Size(67, 24);
            this.txtBoxDetectionNum.TabIndex = 39;
            this.txtBoxDetectionNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBoxDetectionNum_KeyPress);
            this.txtBoxDetectionNum.Leave += new System.EventHandler(this.TxtBoxDetectionNum_Leave);
            // 
            // simpleButCancel
            // 
            this.simpleButCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButCancel.Appearance.Options.UseFont = true;
            this.simpleButCancel.Image = ((System.Drawing.Image)(resources.GetObject("simpleButCancel.Image")));
            this.simpleButCancel.Location = new System.Drawing.Point(400, 852);
            this.simpleButCancel.Name = "simpleButCancel";
            this.simpleButCancel.Size = new System.Drawing.Size(98, 43);
            this.simpleButCancel.TabIndex = 40;
            this.simpleButCancel.Text = "取消";
            this.simpleButCancel.Click += new System.EventHandler(this.SimpleButCancel_Click);
            // 
            // ApplyTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.simpleButCancel);
            this.Controls.Add(this.txtBoxDetectionNum);
            this.Controls.Add(this.labDetectionNum);
            this.Controls.Add(this.btnDilutionSetting);
            this.Controls.Add(this.combSampleType);
            this.Controls.Add(this.combPosNum);
            this.Controls.Add(this.combPanelNum);
            this.Controls.Add(this.combSampleContainer);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.txtSampleNum);
            this.Controls.Add(this.btnSampleDishState);
            this.Controls.Add(this.btnPatientInfo);
            this.Controls.Add(this.btnBatchInput);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lstvTask);
            this.Controls.Add(this.grpCombProject);
            this.Controls.Add(this.grpProject);
            this.Controls.Add(this.lblSamplePos);
            this.Controls.Add(this.lblSampleType);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.lblSampleContainer);
            this.Controls.Add(this.lblDishNum);
            this.Controls.Add(this.lblSampleNum);
            this.Controls.Add(this.chkEmergency);
            this.Controls.Add(this.chkManuallyDilute);
            this.Name = "ApplyTask";
            this.Size = new System.Drawing.Size(1794, 933);
            this.Load += new System.EventHandler(this.ApplyTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkManuallyDilute.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmergency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProject)).EndInit();
            this.grpProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpCombProject)).EndInit();
            this.grpCombProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.combSampleType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combSampleContainer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combPanelNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combPosNum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkManuallyDilute;
        private DevExpress.XtraEditors.CheckEdit chkEmergency;
        private DevExpress.XtraEditors.LabelControl lblSampleNum;
        private DevExpress.XtraEditors.LabelControl lblDishNum;
        private DevExpress.XtraEditors.LabelControl lblSampleContainer;
        private DevExpress.XtraEditors.LabelControl lblBarcode;
        private DevExpress.XtraEditors.LabelControl lblSampleType;
        private DevExpress.XtraEditors.LabelControl lblSamplePos;
        private DevExpress.XtraTab.XtraTabControl grpProject;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraTab.XtraTabControl grpCombProject;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage6;
        private DevExpress.XtraGrid.GridControl lstvTask;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.SimpleButton btnBatchInput;
        private DevExpress.XtraEditors.SimpleButton btnPatientInfo;
        private DevExpress.XtraEditors.SimpleButton btnSampleDishState;
        private DevExpress.XtraEditors.TextEdit txtSampleNum;
        private DevExpress.XtraEditors.TextEdit txtBarCode;
        private DevExpress.XtraEditors.ComboBoxEdit combSampleContainer;
        private DevExpress.XtraEditors.ComboBoxEdit combPanelNum;
        private DevExpress.XtraEditors.ComboBoxEdit combPosNum;
        private DevExpress.XtraEditors.ComboBoxEdit combSampleType;
        private DevExpress.XtraEditors.SimpleButton btnDilutionSetting;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Label labDetectionNum;
        private System.Windows.Forms.TextBox txtBoxDetectionNum;
        private DevExpress.XtraEditors.SimpleButton simpleButCancel;
    }
}
