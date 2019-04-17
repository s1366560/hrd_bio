namespace BioA.UI
{
    partial class ReagentSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReagentSetting));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnLoadingReagent = new DevExpress.XtraEditors.SimpleButton();
            this.btnUnloadReagent = new DevExpress.XtraEditors.SimpleButton();
            this.试剂1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ButUninstallReagent2 = new DevExpress.XtraEditors.SimpleButton();
            this.ButLoadingReagent2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(17, 70);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(835, 769);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView3});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridControl1;
            this.gridView3.Name = "gridView3";
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(870, 70);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(835, 769);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // btnLoadingReagent
            // 
            this.btnLoadingReagent.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadingReagent.Image")));
            this.btnLoadingReagent.Location = new System.Drawing.Point(628, 845);
            this.btnLoadingReagent.Name = "btnLoadingReagent";
            this.btnLoadingReagent.Size = new System.Drawing.Size(109, 52);
            this.btnLoadingReagent.TabIndex = 3;
            this.btnLoadingReagent.Text = "装载试剂";
            this.btnLoadingReagent.Click += new System.EventHandler(this.btnLoadingReagent_Click);
            // 
            // btnUnloadReagent
            // 
            this.btnUnloadReagent.Image = ((System.Drawing.Image)(resources.GetObject("btnUnloadReagent.Image")));
            this.btnUnloadReagent.Location = new System.Drawing.Point(743, 845);
            this.btnUnloadReagent.Name = "btnUnloadReagent";
            this.btnUnloadReagent.Size = new System.Drawing.Size(109, 52);
            this.btnUnloadReagent.TabIndex = 4;
            this.btnUnloadReagent.Text = "卸载试剂";
            this.btnUnloadReagent.Click += new System.EventHandler(this.btnUnloadReagent_Click);
            // 
            // 试剂1
            // 
            this.试剂1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.试剂1.Appearance.Options.UseFont = true;
            this.试剂1.Location = new System.Drawing.Point(17, 47);
            this.试剂1.Name = "试剂1";
            this.试剂1.Size = new System.Drawing.Size(36, 17);
            this.试剂1.TabIndex = 9;
            this.试剂1.Text = "试剂1";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(870, 47);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 17);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "试剂2";
            // 
            // ButUninstallReagent2
            // 
            this.ButUninstallReagent2.Image = ((System.Drawing.Image)(resources.GetObject("ButUninstallReagent2.Image")));
            this.ButUninstallReagent2.Location = new System.Drawing.Point(1596, 845);
            this.ButUninstallReagent2.Name = "ButUninstallReagent2";
            this.ButUninstallReagent2.Size = new System.Drawing.Size(109, 52);
            this.ButUninstallReagent2.TabIndex = 12;
            this.ButUninstallReagent2.Text = "卸载试剂";
            this.ButUninstallReagent2.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ButLoadingReagent2
            // 
            this.ButLoadingReagent2.Image = ((System.Drawing.Image)(resources.GetObject("ButLoadingReagent2.Image")));
            this.ButLoadingReagent2.Location = new System.Drawing.Point(1481, 845);
            this.ButLoadingReagent2.Name = "ButLoadingReagent2";
            this.ButLoadingReagent2.Size = new System.Drawing.Size(109, 52);
            this.ButLoadingReagent2.TabIndex = 11;
            this.ButLoadingReagent2.Text = "装载试剂";
            this.ButLoadingReagent2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // ReagentSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButUninstallReagent2);
            this.Controls.Add(this.ButLoadingReagent2);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.试剂1);
            this.Controls.Add(this.btnUnloadReagent);
            this.Controls.Add(this.btnLoadingReagent);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.gridControl1);
            this.Name = "ReagentSetting";
            this.Size = new System.Drawing.Size(1774, 944);
            //this.Load += new System.EventHandler(this.ReagentSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton btnLoadingReagent;
        private DevExpress.XtraEditors.SimpleButton btnUnloadReagent;
        private DevExpress.XtraEditors.LabelControl 试剂1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton ButUninstallReagent2;
        private DevExpress.XtraEditors.SimpleButton ButLoadingReagent2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
    }
}
