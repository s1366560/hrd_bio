namespace BioA.UI
{
    partial class ReagentNeedle
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReagentNeedle));
            this.lstvCrossPollution = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cboBePolSampleType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboPolSampleType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboWashing = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtWashingTimes = new DevExpress.XtraEditors.TextEdit();
            this.lblNumberOfCleans = new DevExpress.XtraEditors.LabelControl();
            this.cboPollutedSource = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkR2 = new DevExpress.XtraEditors.CheckEdit();
            this.chkR1 = new DevExpress.XtraEditors.CheckEdit();
            this.cboPollutionSource = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtUsingVol = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblKind = new DevExpress.XtraEditors.LabelControl();
            this.lblUsageAmount = new DevExpress.XtraEditors.LabelControl();
            this.lblR1 = new DevExpress.XtraEditors.LabelControl();
            this.lblPollutionProject = new DevExpress.XtraEditors.LabelControl();
            this.lblByPollution = new DevExpress.XtraEditors.LabelControl();
            this.lblContaminatedProject = new DevExpress.XtraEditors.LabelControl();
            this.lblReagentNeedle = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lstvCrossPollution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboBePolSampleType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPolSampleType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboWashing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWashingTimes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPollutedSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkR2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkR1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPollutionSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsingVol.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lstvCrossPollution
            // 
            gridLevelNode1.RelationName = "Level1";
            this.lstvCrossPollution.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.lstvCrossPollution.Location = new System.Drawing.Point(486, 50);
            this.lstvCrossPollution.MainView = this.gridView1;
            this.lstvCrossPollution.Name = "lstvCrossPollution";
            this.lstvCrossPollution.Size = new System.Drawing.Size(1182, 713);
            this.lstvCrossPollution.TabIndex = 22;
            this.lstvCrossPollution.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.lstvCrossPollution.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.lstvCrossPollution;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(613, 787);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 52);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(486, 787);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 52);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "保存修改";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(166, 787);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 52);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(60, 787);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(87, 52);
            this.simpleButton1.TabIndex = 18;
            this.simpleButton1.Text = "新增";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cboBePolSampleType);
            this.groupControl1.Controls.Add(this.cboPolSampleType);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.cboWashing);
            this.groupControl1.Controls.Add(this.txtWashingTimes);
            this.groupControl1.Controls.Add(this.lblNumberOfCleans);
            this.groupControl1.Controls.Add(this.cboPollutedSource);
            this.groupControl1.Controls.Add(this.chkR2);
            this.groupControl1.Controls.Add(this.chkR1);
            this.groupControl1.Controls.Add(this.cboPollutionSource);
            this.groupControl1.Controls.Add(this.txtUsingVol);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lblKind);
            this.groupControl1.Controls.Add(this.lblUsageAmount);
            this.groupControl1.Controls.Add(this.lblR1);
            this.groupControl1.Controls.Add(this.lblPollutionProject);
            this.groupControl1.Controls.Add(this.lblByPollution);
            this.groupControl1.Controls.Add(this.lblContaminatedProject);
            this.groupControl1.Controls.Add(this.lblReagentNeedle);
            this.groupControl1.Location = new System.Drawing.Point(56, 50);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(424, 713);
            this.groupControl1.TabIndex = 17;
            this.groupControl1.Text = "试剂针防污策略";
            // 
            // cboBePolSampleType
            // 
            this.cboBePolSampleType.EditValue = "请选择";
            this.cboBePolSampleType.Location = new System.Drawing.Point(123, 332);
            this.cboBePolSampleType.Name = "cboBePolSampleType";
            this.cboBePolSampleType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBePolSampleType.Properties.Appearance.Options.UseFont = true;
            this.cboBePolSampleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboBePolSampleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboBePolSampleType.Size = new System.Drawing.Size(100, 24);
            this.cboBePolSampleType.TabIndex = 32;
            this.cboBePolSampleType.SelectedIndexChanged += new System.EventHandler(this.cboBePolSampleType_SelectedIndexChanged);
            // 
            // cboPolSampleType
            // 
            this.cboPolSampleType.EditValue = "请选择";
            this.cboPolSampleType.Location = new System.Drawing.Point(123, 207);
            this.cboPolSampleType.Name = "cboPolSampleType";
            this.cboPolSampleType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPolSampleType.Properties.Appearance.Options.UseFont = true;
            this.cboPolSampleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPolSampleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboPolSampleType.Size = new System.Drawing.Size(100, 24);
            this.cboPolSampleType.TabIndex = 31;
            this.cboPolSampleType.SelectedIndexChanged += new System.EventHandler(this.cboPolSampleType_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(49, 208);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 17);
            this.labelControl1.TabIndex = 30;
            this.labelControl1.Text = "项目类型：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(49, 333);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 17);
            this.labelControl2.TabIndex = 29;
            this.labelControl2.Text = "项目类型：";
            // 
            // cboWashing
            // 
            this.cboWashing.EditValue = "请选择";
            this.cboWashing.Location = new System.Drawing.Point(97, 464);
            this.cboWashing.Name = "cboWashing";
            this.cboWashing.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWashing.Properties.Appearance.Options.UseFont = true;
            this.cboWashing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboWashing.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboWashing.Size = new System.Drawing.Size(122, 24);
            this.cboWashing.TabIndex = 28;
            // 
            // txtWashingTimes
            // 
            this.txtWashingTimes.Location = new System.Drawing.Point(108, 564);
            this.txtWashingTimes.Name = "txtWashingTimes";
            this.txtWashingTimes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWashingTimes.Properties.Appearance.Options.UseFont = true;
            this.txtWashingTimes.Size = new System.Drawing.Size(89, 24);
            this.txtWashingTimes.TabIndex = 27;
            // 
            // lblNumberOfCleans
            // 
            this.lblNumberOfCleans.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfCleans.Appearance.Options.UseFont = true;
            this.lblNumberOfCleans.Location = new System.Drawing.Point(33, 565);
            this.lblNumberOfCleans.Name = "lblNumberOfCleans";
            this.lblNumberOfCleans.Size = new System.Drawing.Size(70, 17);
            this.lblNumberOfCleans.TabIndex = 26;
            this.lblNumberOfCleans.Text = "清洗次数：";
            // 
            // cboPollutedSource
            // 
            this.cboPollutedSource.EditValue = "请选择";
            this.cboPollutedSource.Location = new System.Drawing.Point(316, 332);
            this.cboPollutedSource.Name = "cboPollutedSource";
            this.cboPollutedSource.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPollutedSource.Properties.Appearance.Options.UseFont = true;
            this.cboPollutedSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPollutedSource.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboPollutedSource.Size = new System.Drawing.Size(100, 24);
            this.cboPollutedSource.TabIndex = 25;
            // 
            // chkR2
            // 
            this.chkR2.Location = new System.Drawing.Point(207, 89);
            this.chkR2.Name = "chkR2";
            this.chkR2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkR2.Properties.Appearance.Options.UseFont = true;
            this.chkR2.Properties.Caption = "R2针";
            this.chkR2.Size = new System.Drawing.Size(61, 21);
            this.chkR2.TabIndex = 24;
            this.chkR2.CheckedChanged += new System.EventHandler(this.chkR2_CheckedChanged);
            // 
            // chkR1
            // 
            this.chkR1.Location = new System.Drawing.Point(108, 89);
            this.chkR1.Name = "chkR1";
            this.chkR1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkR1.Properties.Appearance.Options.UseFont = true;
            this.chkR1.Properties.Caption = "R1针";
            this.chkR1.Size = new System.Drawing.Size(55, 21);
            this.chkR1.TabIndex = 23;
            this.chkR1.CheckedChanged += new System.EventHandler(this.chkR1_CheckedChanged);
            // 
            // cboPollutionSource
            // 
            this.cboPollutionSource.EditValue = "请选择";
            this.cboPollutionSource.Location = new System.Drawing.Point(316, 207);
            this.cboPollutionSource.Name = "cboPollutionSource";
            this.cboPollutionSource.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPollutionSource.Properties.Appearance.Options.UseFont = true;
            this.cboPollutionSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPollutionSource.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboPollutionSource.Size = new System.Drawing.Size(100, 24);
            this.cboPollutionSource.TabIndex = 21;
            // 
            // txtUsingVol
            // 
            this.txtUsingVol.Location = new System.Drawing.Point(317, 464);
            this.txtUsingVol.Name = "txtUsingVol";
            this.txtUsingVol.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsingVol.Properties.Appearance.Options.UseFont = true;
            this.txtUsingVol.Size = new System.Drawing.Size(73, 24);
            this.txtUsingVol.TabIndex = 20;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(33, 423);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(56, 17);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "清洗液：";
            // 
            // lblKind
            // 
            this.lblKind.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKind.Appearance.Options.UseFont = true;
            this.lblKind.Location = new System.Drawing.Point(49, 465);
            this.lblKind.Name = "lblKind";
            this.lblKind.Size = new System.Drawing.Size(42, 17);
            this.lblKind.TabIndex = 9;
            this.lblKind.Text = "种类：";
            // 
            // lblUsageAmount
            // 
            this.lblUsageAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsageAmount.Appearance.Options.UseFont = true;
            this.lblUsageAmount.Location = new System.Drawing.Point(255, 465);
            this.lblUsageAmount.Name = "lblUsageAmount";
            this.lblUsageAmount.Size = new System.Drawing.Size(56, 17);
            this.lblUsageAmount.TabIndex = 8;
            this.lblUsageAmount.Text = "使用量：";
            // 
            // lblR1
            // 
            this.lblR1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblR1.Appearance.Options.UseFont = true;
            this.lblR1.Location = new System.Drawing.Point(33, 170);
            this.lblR1.Name = "lblR1";
            this.lblR1.Size = new System.Drawing.Size(56, 17);
            this.lblR1.TabIndex = 6;
            this.lblR1.Text = "污染源：";
            // 
            // lblPollutionProject
            // 
            this.lblPollutionProject.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPollutionProject.Appearance.Options.UseFont = true;
            this.lblPollutionProject.Location = new System.Drawing.Point(241, 208);
            this.lblPollutionProject.Name = "lblPollutionProject";
            this.lblPollutionProject.Size = new System.Drawing.Size(70, 17);
            this.lblPollutionProject.TabIndex = 5;
            this.lblPollutionProject.Text = "项目名称：";
            // 
            // lblByPollution
            // 
            this.lblByPollution.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblByPollution.Appearance.Options.UseFont = true;
            this.lblByPollution.Location = new System.Drawing.Point(33, 285);
            this.lblByPollution.Name = "lblByPollution";
            this.lblByPollution.Size = new System.Drawing.Size(70, 17);
            this.lblByPollution.TabIndex = 4;
            this.lblByPollution.Text = "被污染源：";
            // 
            // lblContaminatedProject
            // 
            this.lblContaminatedProject.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContaminatedProject.Appearance.Options.UseFont = true;
            this.lblContaminatedProject.Location = new System.Drawing.Point(241, 333);
            this.lblContaminatedProject.Name = "lblContaminatedProject";
            this.lblContaminatedProject.Size = new System.Drawing.Size(70, 17);
            this.lblContaminatedProject.TabIndex = 3;
            this.lblContaminatedProject.Text = "项目名称：";
            // 
            // lblReagentNeedle
            // 
            this.lblReagentNeedle.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReagentNeedle.Appearance.Options.UseFont = true;
            this.lblReagentNeedle.Location = new System.Drawing.Point(33, 55);
            this.lblReagentNeedle.Name = "lblReagentNeedle";
            this.lblReagentNeedle.Size = new System.Drawing.Size(56, 17);
            this.lblReagentNeedle.TabIndex = 0;
            this.lblReagentNeedle.Text = "试剂针：";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(263, 787);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(104, 52);
            this.simpleButton2.TabIndex = 23;
            this.simpleButton2.Text = "项目测试顺序";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // ReagentNeedle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.lstvCrossPollution);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl1);
            this.Name = "ReagentNeedle";
            this.Size = new System.Drawing.Size(1861, 962);
            ((System.ComponentModel.ISupportInitialize)(this.lstvCrossPollution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboBePolSampleType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPolSampleType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboWashing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWashingTimes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPollutedSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkR2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkR1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPollutionSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsingVol.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl lstvCrossPollution;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit chkR2;
        private DevExpress.XtraEditors.CheckEdit chkR1;
        private DevExpress.XtraEditors.ComboBoxEdit cboPollutionSource;
        private DevExpress.XtraEditors.TextEdit txtUsingVol;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblKind;
        private DevExpress.XtraEditors.LabelControl lblUsageAmount;
        private DevExpress.XtraEditors.LabelControl lblR1;
        private DevExpress.XtraEditors.LabelControl lblPollutionProject;
        private DevExpress.XtraEditors.LabelControl lblByPollution;
        private DevExpress.XtraEditors.LabelControl lblContaminatedProject;
        private DevExpress.XtraEditors.LabelControl lblReagentNeedle;
        private DevExpress.XtraEditors.TextEdit txtWashingTimes;
        private DevExpress.XtraEditors.LabelControl lblNumberOfCleans;
        private DevExpress.XtraEditors.ComboBoxEdit cboPollutedSource;
        private DevExpress.XtraEditors.ComboBoxEdit cboWashing;
        private DevExpress.XtraEditors.ComboBoxEdit cboBePolSampleType;
        private DevExpress.XtraEditors.ComboBoxEdit cboPolSampleType;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}
