namespace BioA.UI
{
    partial class ApplyQCTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplyQCTask));
            this.txtSumpleNum = new DevExpress.XtraEditors.TextEdit();
            this.lstvQCTask = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabcProject = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.lblSampleNum = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.combPosition = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtQCName = new DevExpress.XtraEditors.TextEdit();
            this.txtLotNum = new DevExpress.XtraEditors.TextEdit();
            this.txtQCConc = new DevExpress.XtraEditors.TextEdit();
            this.txtManufacturer = new DevExpress.XtraEditors.TextEdit();
            this.combSampleType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtSumpleNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvQCTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabcProject)).BeginInit();
            this.tabcProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCConc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combSampleType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSumpleNum
            // 
            this.txtSumpleNum.EditValue = "";
            this.txtSumpleNum.Location = new System.Drawing.Point(76, 55);
            this.txtSumpleNum.Name = "txtSumpleNum";
            this.txtSumpleNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSumpleNum.Properties.Appearance.Options.UseFont = true;
            this.txtSumpleNum.Properties.ReadOnly = true;
            this.txtSumpleNum.Size = new System.Drawing.Size(71, 24);
            this.txtSumpleNum.TabIndex = 51;
            // 
            // lstvQCTask
            // 
            this.lstvQCTask.Location = new System.Drawing.Point(1185, 58);
            this.lstvQCTask.MainView = this.gridView1;
            this.lstvQCTask.Name = "lstvQCTask";
            this.lstvQCTask.Size = new System.Drawing.Size(281, 679);
            this.lstvQCTask.TabIndex = 50;
            this.lstvQCTask.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.lstvQCTask.Click += new System.EventHandler(this.lstvQCTask_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.lstvQCTask;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // tabcProject
            // 
            this.tabcProject.Location = new System.Drawing.Point(24, 108);
            this.tabcProject.Name = "tabcProject";
            this.tabcProject.SelectedTabPage = this.xtraTabPage1;
            this.tabcProject.Size = new System.Drawing.Size(1155, 599);
            this.tabcProject.TabIndex = 48;
            this.tabcProject.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            this.tabcProject.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabcProject_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1149, 570);
            this.xtraTabPage1.TabPageWidth = 80;
            this.xtraTabPage1.Text = "第一页";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1149, 570);
            this.xtraTabPage2.TabPageWidth = 80;
            this.xtraTabPage2.Text = "第二页";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1149, 570);
            this.xtraTabPage3.TabPageWidth = 80;
            this.xtraTabPage3.Text = "第三页";
            // 
            // lblSampleNum
            // 
            this.lblSampleNum.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleNum.Appearance.Options.UseFont = true;
            this.lblSampleNum.Location = new System.Drawing.Point(24, 58);
            this.lblSampleNum.Name = "lblSampleNum";
            this.lblSampleNum.Size = new System.Drawing.Size(56, 17);
            this.lblSampleNum.TabIndex = 39;
            this.lblSampleNum.Text = "顺序号：";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(255, 721);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 43);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(161, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 17);
            this.labelControl1.TabIndex = 63;
            this.labelControl1.Text = "位置：";
            // 
            // btnApply
            // 
            this.btnApply.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Appearance.Options.UseFont = true;
            this.btnApply.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.Image")));
            this.btnApply.Location = new System.Drawing.Point(112, 721);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(103, 43);
            this.btnApply.TabIndex = 65;
            this.btnApply.Text = "申请";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // combPosition
            // 
            this.combPosition.Location = new System.Drawing.Point(198, 57);
            this.combPosition.Name = "combPosition";
            this.combPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combPosition.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combPosition.Size = new System.Drawing.Size(68, 20);
            this.combPosition.TabIndex = 66;
            this.combPosition.SelectedIndexChanged += new System.EventHandler(this.combPosition_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(440, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 17);
            this.labelControl2.TabIndex = 67;
            this.labelControl2.Text = "质控品名称：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(627, 59);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 17);
            this.labelControl3.TabIndex = 68;
            this.labelControl3.Text = "批号：";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(771, 58);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(84, 17);
            this.labelControl4.TabIndex = 69;
            this.labelControl4.Text = "质控品浓度：";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(957, 59);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(70, 17);
            this.labelControl5.TabIndex = 70;
            this.labelControl5.Text = "生产厂家：";
            // 
            // txtQCName
            // 
            this.txtQCName.EditValue = "";
            this.txtQCName.Location = new System.Drawing.Point(521, 57);
            this.txtQCName.Name = "txtQCName";
            this.txtQCName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQCName.Properties.Appearance.Options.UseFont = true;
            this.txtQCName.Properties.ReadOnly = true;
            this.txtQCName.Size = new System.Drawing.Size(100, 24);
            this.txtQCName.TabIndex = 71;
            // 
            // txtLotNum
            // 
            this.txtLotNum.EditValue = "";
            this.txtLotNum.Location = new System.Drawing.Point(665, 56);
            this.txtLotNum.Name = "txtLotNum";
            this.txtLotNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotNum.Properties.Appearance.Options.UseFont = true;
            this.txtLotNum.Properties.ReadOnly = true;
            this.txtLotNum.Size = new System.Drawing.Size(100, 24);
            this.txtLotNum.TabIndex = 72;
            // 
            // txtQCConc
            // 
            this.txtQCConc.EditValue = "";
            this.txtQCConc.Location = new System.Drawing.Point(851, 56);
            this.txtQCConc.Name = "txtQCConc";
            this.txtQCConc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQCConc.Properties.Appearance.Options.UseFont = true;
            this.txtQCConc.Properties.ReadOnly = true;
            this.txtQCConc.Size = new System.Drawing.Size(100, 24);
            this.txtQCConc.TabIndex = 73;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.EditValue = "";
            this.txtManufacturer.Location = new System.Drawing.Point(1025, 56);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManufacturer.Properties.Appearance.Options.UseFont = true;
            this.txtManufacturer.Properties.ReadOnly = true;
            this.txtManufacturer.Size = new System.Drawing.Size(100, 24);
            this.txtManufacturer.TabIndex = 74;
            // 
            // combSampleType
            // 
            this.combSampleType.Location = new System.Drawing.Point(350, 58);
            this.combSampleType.Name = "combSampleType";
            this.combSampleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combSampleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combSampleType.Size = new System.Drawing.Size(68, 20);
            this.combSampleType.TabIndex = 76;
            this.combSampleType.SelectedIndexChanged += new System.EventHandler(this.combSampleType_SelectedIndexChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(284, 58);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(70, 17);
            this.labelControl6.TabIndex = 75;
            this.labelControl6.Text = "样本类型：";
            // 
            // ApplyQCTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.combSampleType);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtManufacturer);
            this.Controls.Add(this.txtQCConc);
            this.Controls.Add(this.txtLotNum);
            this.Controls.Add(this.txtQCName);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.combPosition);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSumpleNum);
            this.Controls.Add(this.lstvQCTask);
            this.Controls.Add(this.tabcProject);
            this.Controls.Add(this.lblSampleNum);
            this.Name = "ApplyQCTask";
            this.Size = new System.Drawing.Size(1619, 803);
            this.Load += new System.EventHandler(this.ApplyQCTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSumpleNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvQCTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabcProject)).EndInit();
            this.tabcProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.combPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCConc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combSampleType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtSumpleNum;
        private DevExpress.XtraGrid.GridControl lstvQCTask;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTab.XtraTabControl tabcProject;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.LabelControl lblSampleNum;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.ComboBoxEdit combPosition;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtQCName;
        private DevExpress.XtraEditors.TextEdit txtLotNum;
        private DevExpress.XtraEditors.TextEdit txtQCConc;
        private DevExpress.XtraEditors.TextEdit txtManufacturer;
        private DevExpress.XtraEditors.ComboBoxEdit combSampleType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}
