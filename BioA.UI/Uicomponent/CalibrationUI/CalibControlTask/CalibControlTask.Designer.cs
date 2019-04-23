namespace BioA.UI
{
    partial class CalibControlTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibControlTask));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lstvTask = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpCombProject = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.grpProject = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.combSampleType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtSumpleNum = new DevExpress.XtraEditors.TextEdit();
            this.lblSampleNum = new DevExpress.XtraEditors.LabelControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lstvTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCombProject)).BeginInit();
            this.grpCombProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpProject)).BeginInit();
            this.grpProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combSampleType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSumpleNum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnSave.Appearance.Font")));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lstvTask
            // 
            gridLevelNode1.RelationName = "Level1";
            this.lstvTask.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            resources.ApplyResources(this.lstvTask, "lstvTask");
            this.lstvTask.MainView = this.gridView1;
            this.lstvTask.Name = "lstvTask";
            this.lstvTask.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.lstvTask;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // grpCombProject
            // 
            resources.ApplyResources(this.grpCombProject, "grpCombProject");
            this.grpCombProject.Name = "grpCombProject";
            this.grpCombProject.SelectedTabPage = this.xtraTabPage5;
            this.grpCombProject.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage5,
            this.xtraTabPage6});
            this.grpCombProject.Click += new System.EventHandler(this.grpCombProject_Click);
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Name = "xtraTabPage5";
            resources.ApplyResources(this.xtraTabPage5, "xtraTabPage5");
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Name = "xtraTabPage6";
            resources.ApplyResources(this.xtraTabPage6, "xtraTabPage6");
            // 
            // grpProject
            // 
            resources.ApplyResources(this.grpProject, "grpProject");
            this.grpProject.Name = "grpProject";
            this.grpProject.SelectedTabPage = this.xtraTabPage1;
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
            resources.ApplyResources(this.xtraTabPage1, "xtraTabPage1");
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            resources.ApplyResources(this.xtraTabPage2, "xtraTabPage2");
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Name = "xtraTabPage3";
            resources.ApplyResources(this.xtraTabPage3, "xtraTabPage3");
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Name = "xtraTabPage4";
            resources.ApplyResources(this.xtraTabPage4, "xtraTabPage4");
            // 
            // combSampleType
            // 
            resources.ApplyResources(this.combSampleType, "combSampleType");
            this.combSampleType.Name = "combSampleType";
            this.combSampleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("combSampleType.Properties.Buttons"))))});
            this.combSampleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combSampleType.SelectedIndexChanged += new System.EventHandler(this.combSampleType_SelectedIndexChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl6.Appearance.Font")));
            this.labelControl6.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControl6, "labelControl6");
            this.labelControl6.Name = "labelControl6";
            // 
            // txtSumpleNum
            // 
            resources.ApplyResources(this.txtSumpleNum, "txtSumpleNum");
            this.txtSumpleNum.Name = "txtSumpleNum";
            this.txtSumpleNum.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtSumpleNum.Properties.Appearance.Font")));
            this.txtSumpleNum.Properties.Appearance.Options.UseFont = true;
            this.txtSumpleNum.Properties.ReadOnly = true;
            // 
            // lblSampleNum
            // 
            this.lblSampleNum.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblSampleNum.Appearance.Font")));
            this.lblSampleNum.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.lblSampleNum, "lblSampleNum");
            this.lblSampleNum.Name = "lblSampleNum";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnDelete.Appearance.Font")));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // CalibControlTask
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.combSampleType);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtSumpleNum);
            this.Controls.Add(this.lblSampleNum);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lstvTask);
            this.Controls.Add(this.grpCombProject);
            this.Controls.Add(this.grpProject);
            this.Name = "CalibControlTask";
            ((System.ComponentModel.ISupportInitialize)(this.lstvTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCombProject)).EndInit();
            this.grpCombProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpProject)).EndInit();
            this.grpProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.combSampleType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSumpleNum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl lstvTask;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTab.XtraTabControl grpCombProject;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5 ;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage6;
        private DevExpress.XtraTab.XtraTabControl grpProject;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraEditors.ComboBoxEdit combSampleType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtSumpleNum;
        private DevExpress.XtraEditors.LabelControl lblSampleNum;
        private DevExpress.XtraEditors.SimpleButton btnDelete;

    }
}
