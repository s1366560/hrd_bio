namespace BioA.UI
{
    partial class DepartmentManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentManage));
            this.lblApplicationDepartment = new DevExpress.XtraEditors.LabelControl();
            this.lblLaboratoryPhysician = new DevExpress.XtraEditors.LabelControl();
            this.lblAuditPhysician = new DevExpress.XtraEditors.LabelControl();
            this.lblApplicationPhysician = new DevExpress.XtraEditors.LabelControl();
            this.grdApplyDepartments = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdApplyDoctor = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdCheckoutDoctor = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdAuditDoctor = new DevExpress.XtraGrid.GridControl();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSPAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnSPCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSPSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnSPDelete = new DevExpress.XtraEditors.SimpleButton();
            this.DeleteDoctor = new DevExpress.XtraEditors.SimpleButton();
            this.SaveDoctor = new DevExpress.XtraEditors.SimpleButton();
            this.CancelDoctor = new DevExpress.XtraEditors.SimpleButton();
            this.AddDoctor = new DevExpress.XtraEditors.SimpleButton();
            this.DeleteAuditDoctor = new DevExpress.XtraEditors.SimpleButton();
            this.AddAuditDoctor = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grdApplyDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdApplyDoctor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCheckoutDoctor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAuditDoctor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblApplicationDepartment
            // 
            this.lblApplicationDepartment.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationDepartment.Appearance.Options.UseFont = true;
            this.lblApplicationDepartment.Location = new System.Drawing.Point(84, 38);
            this.lblApplicationDepartment.Name = "lblApplicationDepartment";
            this.lblApplicationDepartment.Size = new System.Drawing.Size(70, 17);
            this.lblApplicationDepartment.TabIndex = 0;
            this.lblApplicationDepartment.Text = "申请科室：";
            // 
            // lblLaboratoryPhysician
            // 
            this.lblLaboratoryPhysician.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLaboratoryPhysician.Appearance.Options.UseFont = true;
            this.lblLaboratoryPhysician.Location = new System.Drawing.Point(874, 38);
            this.lblLaboratoryPhysician.Name = "lblLaboratoryPhysician";
            this.lblLaboratoryPhysician.Size = new System.Drawing.Size(70, 17);
            this.lblLaboratoryPhysician.TabIndex = 1;
            this.lblLaboratoryPhysician.Text = "检验医师：";
            // 
            // lblAuditPhysician
            // 
            this.lblAuditPhysician.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuditPhysician.Appearance.Options.UseFont = true;
            this.lblAuditPhysician.Location = new System.Drawing.Point(1263, 38);
            this.lblAuditPhysician.Name = "lblAuditPhysician";
            this.lblAuditPhysician.Size = new System.Drawing.Size(70, 17);
            this.lblAuditPhysician.TabIndex = 2;
            this.lblAuditPhysician.Text = "审核医师：";
            // 
            // lblApplicationPhysician
            // 
            this.lblApplicationPhysician.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationPhysician.Appearance.Options.UseFont = true;
            this.lblApplicationPhysician.Location = new System.Drawing.Point(481, 38);
            this.lblApplicationPhysician.Name = "lblApplicationPhysician";
            this.lblApplicationPhysician.Size = new System.Drawing.Size(70, 17);
            this.lblApplicationPhysician.TabIndex = 3;
            this.lblApplicationPhysician.Text = "申请医师：";
            // 
            // grdApplyDepartments
            // 
            this.grdApplyDepartments.Location = new System.Drawing.Point(84, 202);
            this.grdApplyDepartments.MainView = this.gridView1;
            this.grdApplyDepartments.Name = "grdApplyDepartments";
            this.grdApplyDepartments.Size = new System.Drawing.Size(260, 630);
            this.grdApplyDepartments.TabIndex = 4;
            this.grdApplyDepartments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.grdApplyDepartments.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdApplyDepartments;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // grdApplyDoctor
            // 
            this.grdApplyDoctor.Location = new System.Drawing.Point(481, 234);
            this.grdApplyDoctor.MainView = this.gridView2;
            this.grdApplyDoctor.Name = "grdApplyDoctor";
            this.grdApplyDoctor.Size = new System.Drawing.Size(260, 598);
            this.grdApplyDoctor.TabIndex = 5;
            this.grdApplyDoctor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.grdApplyDoctor.Click += new System.EventHandler(this.gridControl2_Click);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdApplyDoctor;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // grdCheckoutDoctor
            // 
            this.grdCheckoutDoctor.Location = new System.Drawing.Point(874, 64);
            this.grdCheckoutDoctor.MainView = this.gridView3;
            this.grdCheckoutDoctor.Name = "grdCheckoutDoctor";
            this.grdCheckoutDoctor.Size = new System.Drawing.Size(260, 768);
            this.grdCheckoutDoctor.TabIndex = 6;
            this.grdCheckoutDoctor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.grdCheckoutDoctor;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsCustomization.AllowFilter = false;
            this.gridView3.OptionsCustomization.AllowSort = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // grdAuditDoctor
            // 
            this.grdAuditDoctor.Location = new System.Drawing.Point(1263, 150);
            this.grdAuditDoctor.MainView = this.gridView4;
            this.grdAuditDoctor.Name = "grdAuditDoctor";
            this.grdAuditDoctor.Size = new System.Drawing.Size(260, 682);
            this.grdAuditDoctor.TabIndex = 7;
            this.grdAuditDoctor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            this.grdAuditDoctor.Click += new System.EventHandler(this.gridControl4_Click);
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.grdAuditDoctor;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // btnSPAdd
            // 
            this.btnSPAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSPAdd.Appearance.Options.UseFont = true;
            this.btnSPAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnSPAdd.Image")));
            this.btnSPAdd.Location = new System.Drawing.Point(84, 91);
            this.btnSPAdd.Name = "btnSPAdd";
            this.btnSPAdd.Size = new System.Drawing.Size(87, 49);
            this.btnSPAdd.TabIndex = 8;
            this.btnSPAdd.Text = "添加";
            this.btnSPAdd.Click += new System.EventHandler(this.btnSPAdd_Click);
            // 
            // btnSPCancel
            // 
            this.btnSPCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnSPCancel.Image")));
            this.btnSPCancel.Location = new System.Drawing.Point(257, 147);
            this.btnSPCancel.Name = "btnSPCancel";
            this.btnSPCancel.Size = new System.Drawing.Size(87, 49);
            this.btnSPCancel.TabIndex = 9;
            this.btnSPCancel.Text = "取消";
            this.btnSPCancel.Click += new System.EventHandler(this.btnSPCancel_Click);
            // 
            // btnSPSave
            // 
            this.btnSPSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSPSave.Image")));
            this.btnSPSave.Location = new System.Drawing.Point(84, 147);
            this.btnSPSave.Name = "btnSPSave";
            this.btnSPSave.Size = new System.Drawing.Size(87, 49);
            this.btnSPSave.TabIndex = 10;
            this.btnSPSave.Text = "保存";
            this.btnSPSave.Click += new System.EventHandler(this.btnSPSave_Click);
            // 
            // btnSPDelete
            // 
            this.btnSPDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnSPDelete.Image")));
            this.btnSPDelete.Location = new System.Drawing.Point(257, 92);
            this.btnSPDelete.Name = "btnSPDelete";
            this.btnSPDelete.Size = new System.Drawing.Size(87, 49);
            this.btnSPDelete.TabIndex = 11;
            this.btnSPDelete.Text = "删除";
            this.btnSPDelete.Click += new System.EventHandler(this.btnSPDelete_Click);
            // 
            // DeleteDoctor
            // 
            this.DeleteDoctor.Image = ((System.Drawing.Image)(resources.GetObject("DeleteDoctor.Image")));
            this.DeleteDoctor.Location = new System.Drawing.Point(654, 124);
            this.DeleteDoctor.Name = "DeleteDoctor";
            this.DeleteDoctor.Size = new System.Drawing.Size(87, 49);
            this.DeleteDoctor.TabIndex = 15;
            this.DeleteDoctor.Text = "删除";
            this.DeleteDoctor.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // SaveDoctor
            // 
            this.SaveDoctor.Image = ((System.Drawing.Image)(resources.GetObject("SaveDoctor.Image")));
            this.SaveDoctor.Location = new System.Drawing.Point(481, 179);
            this.SaveDoctor.Name = "SaveDoctor";
            this.SaveDoctor.Size = new System.Drawing.Size(87, 49);
            this.SaveDoctor.TabIndex = 14;
            this.SaveDoctor.Text = "保存";
            this.SaveDoctor.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // CancelDoctor
            // 
            this.CancelDoctor.Image = ((System.Drawing.Image)(resources.GetObject("CancelDoctor.Image")));
            this.CancelDoctor.Location = new System.Drawing.Point(654, 179);
            this.CancelDoctor.Name = "CancelDoctor";
            this.CancelDoctor.Size = new System.Drawing.Size(87, 49);
            this.CancelDoctor.TabIndex = 13;
            this.CancelDoctor.Text = "取消";
            this.CancelDoctor.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // AddDoctor
            // 
            this.AddDoctor.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddDoctor.Appearance.Options.UseFont = true;
            this.AddDoctor.Image = ((System.Drawing.Image)(resources.GetObject("AddDoctor.Image")));
            this.AddDoctor.Location = new System.Drawing.Point(481, 124);
            this.AddDoctor.Name = "AddDoctor";
            this.AddDoctor.Size = new System.Drawing.Size(87, 49);
            this.AddDoctor.TabIndex = 12;
            this.AddDoctor.Text = "添加";
            this.AddDoctor.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // DeleteAuditDoctor
            // 
            this.DeleteAuditDoctor.Image = ((System.Drawing.Image)(resources.GetObject("DeleteAuditDoctor.Image")));
            this.DeleteAuditDoctor.Location = new System.Drawing.Point(1436, 95);
            this.DeleteAuditDoctor.Name = "DeleteAuditDoctor";
            this.DeleteAuditDoctor.Size = new System.Drawing.Size(87, 49);
            this.DeleteAuditDoctor.TabIndex = 23;
            this.DeleteAuditDoctor.Text = "删除";
            this.DeleteAuditDoctor.Click += new System.EventHandler(this.simpleButton9_Click);
            // 
            // AddAuditDoctor
            // 
            this.AddAuditDoctor.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddAuditDoctor.Appearance.Options.UseFont = true;
            this.AddAuditDoctor.Image = ((System.Drawing.Image)(resources.GetObject("AddAuditDoctor.Image")));
            this.AddAuditDoctor.Location = new System.Drawing.Point(1263, 94);
            this.AddAuditDoctor.Name = "AddAuditDoctor";
            this.AddAuditDoctor.Size = new System.Drawing.Size(87, 49);
            this.AddAuditDoctor.TabIndex = 20;
            this.AddAuditDoctor.Text = "添加";
            this.AddAuditDoctor.Click += new System.EventHandler(this.simpleButton12_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(84, 61);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Size = new System.Drawing.Size(260, 24);
            this.textEdit1.TabIndex = 24;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Name = "gridColumn5";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(481, 61);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit2.Properties.Appearance.Options.UseFont = true;
            this.textEdit2.Size = new System.Drawing.Size(260, 24);
            this.textEdit2.TabIndex = 25;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = "请选择";
            this.comboBoxEdit1.Location = new System.Drawing.Point(557, 91);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEdit1.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(184, 24);
            this.comboBoxEdit1.TabIndex = 26;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(481, 94);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 17);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "申请科室：";
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.EditValue = "请选择";
            this.comboBoxEdit2.Location = new System.Drawing.Point(1263, 61);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEdit2.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit2.Size = new System.Drawing.Size(260, 24);
            this.comboBoxEdit2.TabIndex = 28;
            // 
            // DepartmentManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxEdit2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.DeleteAuditDoctor);
            this.Controls.Add(this.AddAuditDoctor);
            this.Controls.Add(this.DeleteDoctor);
            this.Controls.Add(this.SaveDoctor);
            this.Controls.Add(this.CancelDoctor);
            this.Controls.Add(this.AddDoctor);
            this.Controls.Add(this.btnSPDelete);
            this.Controls.Add(this.btnSPSave);
            this.Controls.Add(this.btnSPCancel);
            this.Controls.Add(this.btnSPAdd);
            this.Controls.Add(this.grdAuditDoctor);
            this.Controls.Add(this.grdCheckoutDoctor);
            this.Controls.Add(this.grdApplyDoctor);
            this.Controls.Add(this.grdApplyDepartments);
            this.Controls.Add(this.lblApplicationPhysician);
            this.Controls.Add(this.lblAuditPhysician);
            this.Controls.Add(this.lblLaboratoryPhysician);
            this.Controls.Add(this.lblApplicationDepartment);
            this.Name = "DepartmentManage";
            this.Size = new System.Drawing.Size(1702, 936);
            this.Load += new System.EventHandler(this.DepartmentManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdApplyDepartments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdApplyDoctor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCheckoutDoctor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAuditDoctor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblApplicationDepartment;
        private DevExpress.XtraEditors.LabelControl lblLaboratoryPhysician;
        private DevExpress.XtraEditors.LabelControl lblAuditPhysician;
        private DevExpress.XtraEditors.LabelControl lblApplicationPhysician;
        private DevExpress.XtraGrid.GridControl grdApplyDepartments;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl grdApplyDoctor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl grdCheckoutDoctor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.GridControl grdAuditDoctor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.SimpleButton btnSPAdd;
        private DevExpress.XtraEditors.SimpleButton btnSPCancel;
        private DevExpress.XtraEditors.SimpleButton btnSPSave;
        private DevExpress.XtraEditors.SimpleButton btnSPDelete;
        private DevExpress.XtraEditors.SimpleButton DeleteDoctor;
        private DevExpress.XtraEditors.SimpleButton SaveDoctor;
        private DevExpress.XtraEditors.SimpleButton CancelDoctor;
        private DevExpress.XtraEditors.SimpleButton AddDoctor;
        private DevExpress.XtraEditors.SimpleButton DeleteAuditDoctor;
        private DevExpress.XtraEditors.SimpleButton AddAuditDoctor;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
    }
}
