namespace BioA.UI
{
    partial class UserManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserManagement));
            this.btnUsereditor = new DevExpress.XtraEditors.SimpleButton();
            this.btnUserdelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnUsercreation = new DevExpress.XtraEditors.SimpleButton();
            this.lblUserlist = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUsereditor
            // 
            this.btnUsereditor.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsereditor.Appearance.Options.UseFont = true;
            this.btnUsereditor.Image = ((System.Drawing.Image)(resources.GetObject("btnUsereditor.Image")));
            this.btnUsereditor.Location = new System.Drawing.Point(1584, 174);
            this.btnUsereditor.Name = "btnUsereditor";
            this.btnUsereditor.Size = new System.Drawing.Size(110, 45);
            this.btnUsereditor.TabIndex = 12;
            this.btnUsereditor.Text = "用户编辑";
            this.btnUsereditor.Click += new System.EventHandler(this.btnUsereditor_Click);
            // 
            // btnUserdelete
            // 
            this.btnUserdelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserdelete.Appearance.Options.UseFont = true;
            this.btnUserdelete.Image = ((System.Drawing.Image)(resources.GetObject("btnUserdelete.Image")));
            this.btnUserdelete.Location = new System.Drawing.Point(1584, 123);
            this.btnUserdelete.Name = "btnUserdelete";
            this.btnUserdelete.Size = new System.Drawing.Size(110, 45);
            this.btnUserdelete.TabIndex = 11;
            this.btnUserdelete.Text = "用户删除";
            this.btnUserdelete.Click += new System.EventHandler(this.btnUserdelete_Click);
            // 
            // btnUsercreation
            // 
            this.btnUsercreation.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsercreation.Appearance.Options.UseFont = true;
            this.btnUsercreation.Image = ((System.Drawing.Image)(resources.GetObject("btnUsercreation.Image")));
            this.btnUsercreation.Location = new System.Drawing.Point(1584, 72);
            this.btnUsercreation.Name = "btnUsercreation";
            this.btnUsercreation.Size = new System.Drawing.Size(110, 45);
            this.btnUsercreation.TabIndex = 10;
            this.btnUsercreation.Text = "用户创建";
            this.btnUsercreation.Click += new System.EventHandler(this.btnUsercreation_Click);
            // 
            // lblUserlist
            // 
            this.lblUserlist.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserlist.Appearance.Options.UseFont = true;
            this.lblUserlist.Location = new System.Drawing.Point(44, 49);
            this.lblUserlist.Name = "lblUserlist";
            this.lblUserlist.Size = new System.Drawing.Size(56, 17);
            this.lblUserlist.TabIndex = 9;
            this.lblUserlist.Text = "用户列表";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(44, 72);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1534, 799);
            this.gridControl1.TabIndex = 13;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // UserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnUsereditor);
            this.Controls.Add(this.btnUserdelete);
            this.Controls.Add(this.btnUsercreation);
            this.Controls.Add(this.lblUserlist);
            this.Name = "UserManagement";
            this.Size = new System.Drawing.Size(1717, 906);
            this.Load += new System.EventHandler(this.UserManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnUsereditor;
        private DevExpress.XtraEditors.SimpleButton btnUserdelete;
        private DevExpress.XtraEditors.SimpleButton btnUsercreation;
        private DevExpress.XtraEditors.LabelControl lblUserlist;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
