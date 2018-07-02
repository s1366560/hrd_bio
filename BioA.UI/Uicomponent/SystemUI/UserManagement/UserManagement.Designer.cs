namespace BioA.UI.Uicomponent.SystemUI.UserManagement
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
            this.btnUsereditor = new DevExpress.XtraEditors.SimpleButton();
            this.btnUserdelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnUsercreation = new DevExpress.XtraEditors.SimpleButton();
            this.lblUserlist = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUsereditor
            // 
            this.btnUsereditor.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsereditor.Appearance.Options.UseFont = true;
            this.btnUsereditor.Location = new System.Drawing.Point(1618, 174);
            this.btnUsereditor.Name = "btnUsereditor";
            this.btnUsereditor.Size = new System.Drawing.Size(76, 45);
            this.btnUsereditor.TabIndex = 12;
            this.btnUsereditor.Text = "用户编辑";
            this.btnUsereditor.Click += new System.EventHandler(this.btnUsereditor_Click);
            // 
            // btnUserdelete
            // 
            this.btnUserdelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserdelete.Appearance.Options.UseFont = true;
            this.btnUserdelete.Location = new System.Drawing.Point(1618, 123);
            this.btnUserdelete.Name = "btnUserdelete";
            this.btnUserdelete.Size = new System.Drawing.Size(76, 45);
            this.btnUserdelete.TabIndex = 11;
            this.btnUserdelete.Text = "用户删除";
            // 
            // btnUsercreation
            // 
            this.btnUsercreation.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsercreation.Appearance.Options.UseFont = true;
            this.btnUsercreation.Location = new System.Drawing.Point(1618, 72);
            this.btnUsercreation.Name = "btnUsercreation";
            this.btnUsercreation.Size = new System.Drawing.Size(76, 45);
            this.btnUsercreation.TabIndex = 10;
            this.btnUsercreation.Text = "用户创建";
            this.btnUsercreation.Click += new System.EventHandler(this.btnUsercreation_Click);
            // 
            // lblUserlist
            // 
            this.lblUserlist.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserlist.Appearance.Options.UseFont = true;
            this.lblUserlist.Location = new System.Drawing.Point(34, 34);
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
            this.gridControl1.Size = new System.Drawing.Size(1559, 799);
            this.gridControl1.TabIndex = 13;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "用户账户";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "用户名称";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "创建时间";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "拥有权限";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}
