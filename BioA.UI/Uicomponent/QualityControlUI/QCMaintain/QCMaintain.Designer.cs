namespace BioA.UI
{
    partial class QCMaintain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QCMaintain));
            this.lblQCProducts = new DevExpress.XtraEditors.LabelControl();
            this.lblTestLtem = new DevExpress.XtraEditors.LabelControl();
            this.lstvQCInfo = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lstvQCRelativelyProject = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnLock = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnUnLock = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lstvQCInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvQCRelativelyProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQCProducts
            // 
            this.lblQCProducts.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQCProducts.Appearance.Options.UseFont = true;
            this.lblQCProducts.Location = new System.Drawing.Point(13, 47);
            this.lblQCProducts.Name = "lblQCProducts";
            this.lblQCProducts.Size = new System.Drawing.Size(56, 17);
            this.lblQCProducts.TabIndex = 0;
            this.lblQCProducts.Text = "质控品：";
            // 
            // lblTestLtem
            // 
            this.lblTestLtem.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestLtem.Appearance.Options.UseFont = true;
            this.lblTestLtem.Location = new System.Drawing.Point(866, 47);
            this.lblTestLtem.Name = "lblTestLtem";
            this.lblTestLtem.Size = new System.Drawing.Size(70, 17);
            this.lblTestLtem.TabIndex = 1;
            this.lblTestLtem.Text = "检测项目：";
            // 
            // lstvQCInfo
            // 
            gridLevelNode1.RelationName = "Level1";
            this.lstvQCInfo.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.lstvQCInfo.Location = new System.Drawing.Point(13, 70);
            this.lstvQCInfo.MainView = this.gridView1;
            this.lstvQCInfo.Name = "lstvQCInfo";
            this.lstvQCInfo.Size = new System.Drawing.Size(837, 778);
            this.lstvQCInfo.TabIndex = 2;
            this.lstvQCInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.lstvQCInfo.Click += new System.EventHandler(this.lstvQCInfo_Click);
            this.lstvQCInfo.DoubleClick += new System.EventHandler(this.btnEdit_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.lstvQCInfo;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // lstvQCRelativelyProject
            // 
            this.lstvQCRelativelyProject.Location = new System.Drawing.Point(866, 70);
            this.lstvQCRelativelyProject.MainView = this.gridView2;
            this.lstvQCRelativelyProject.Name = "lstvQCRelativelyProject";
            this.lstvQCRelativelyProject.Size = new System.Drawing.Size(837, 778);
            this.lstvQCRelativelyProject.TabIndex = 3;
            this.lstvQCRelativelyProject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.lstvQCRelativelyProject;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(411, 854);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(83, 49);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnLock
            // 
            this.btnLock.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLock.Appearance.Options.UseFont = true;
            this.btnLock.Image = ((System.Drawing.Image)(resources.GetObject("btnLock.Image")));
            this.btnLock.Location = new System.Drawing.Point(678, 854);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(83, 49);
            this.btnLock.TabIndex = 8;
            this.btnLock.Text = "冻结";
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(500, 854);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(83, 49);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "编辑";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(589, 854);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(83, 49);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUnLock
            // 
            this.btnUnLock.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnLock.Appearance.Options.UseFont = true;
            this.btnUnLock.Image = ((System.Drawing.Image)(resources.GetObject("btnUnLock.Image")));
            this.btnUnLock.Location = new System.Drawing.Point(767, 854);
            this.btnUnLock.Name = "btnUnLock";
            this.btnUnLock.Size = new System.Drawing.Size(83, 49);
            this.btnUnLock.TabIndex = 11;
            this.btnUnLock.Text = "激活";
            this.btnUnLock.Click += new System.EventHandler(this.btnUnLock_Click);
            // 
            // QCMaintain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUnLock);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lstvQCRelativelyProject);
            this.Controls.Add(this.lstvQCInfo);
            this.Controls.Add(this.lblTestLtem);
            this.Controls.Add(this.lblQCProducts);
            this.Name = "QCMaintain";
            this.Size = new System.Drawing.Size(1717, 906);
            this.Load += new System.EventHandler(this.QCMaintain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lstvQCInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvQCRelativelyProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblQCProducts;
        private DevExpress.XtraEditors.LabelControl lblTestLtem;
        private DevExpress.XtraGrid.GridControl lstvQCInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl lstvQCRelativelyProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnLock;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnUnLock;
    }
}
