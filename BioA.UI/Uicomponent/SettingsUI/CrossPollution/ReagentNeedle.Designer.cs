namespace BioA.UI.Uicomponent.SettingsUI.CrossPollution
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkR2 = new DevExpress.XtraEditors.CheckEdit();
            this.chkR1 = new DevExpress.XtraEditors.CheckEdit();
            this.lblPollutionSource = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textEdit4 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblKind = new DevExpress.XtraEditors.LabelControl();
            this.lblUsageAmount = new DevExpress.XtraEditors.LabelControl();
            this.lblR1 = new DevExpress.XtraEditors.LabelControl();
            this.lblPollutionProject = new DevExpress.XtraEditors.LabelControl();
            this.lblByPollution = new DevExpress.XtraEditors.LabelControl();
            this.lblContaminatedProject = new DevExpress.XtraEditors.LabelControl();
            this.lblReagentNeedle = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblNumberOfCleans = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkR2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkR1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPollutionSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(586, 103);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(897, 549);
            this.gridControl1.TabIndex = 22;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn8,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn7});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "编号";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "针试剂";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "污染项目名称";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "被污染项目名称";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "清洗剂类型";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "清洗剂体积";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "清洗次数";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Location = new System.Drawing.Point(412, 658);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(89, 46);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "删除";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(204, 710);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 46);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "保存";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(412, 710);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 46);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(204, 658);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(89, 46);
            this.simpleButton1.TabIndex = 18;
            this.simpleButton1.Text = "新增";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textEdit1);
            this.groupControl1.Controls.Add(this.lblNumberOfCleans);
            this.groupControl1.Controls.Add(this.comboBoxEdit1);
            this.groupControl1.Controls.Add(this.chkR2);
            this.groupControl1.Controls.Add(this.chkR1);
            this.groupControl1.Controls.Add(this.lblPollutionSource);
            this.groupControl1.Controls.Add(this.textEdit4);
            this.groupControl1.Controls.Add(this.textEdit3);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lblKind);
            this.groupControl1.Controls.Add(this.lblUsageAmount);
            this.groupControl1.Controls.Add(this.lblR1);
            this.groupControl1.Controls.Add(this.lblPollutionProject);
            this.groupControl1.Controls.Add(this.lblByPollution);
            this.groupControl1.Controls.Add(this.lblContaminatedProject);
            this.groupControl1.Controls.Add(this.lblReagentNeedle);
            this.groupControl1.Location = new System.Drawing.Point(158, 103);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(422, 549);
            this.groupControl1.TabIndex = 17;
            this.groupControl1.Text = "试剂针防污策略";
            // 
            // chkR2
            // 
            this.chkR2.Location = new System.Drawing.Point(207, 89);
            this.chkR2.Name = "chkR2";
            this.chkR2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkR2.Properties.Appearance.Options.UseFont = true;
            this.chkR2.Properties.Caption = "R2针";
            this.chkR2.Size = new System.Drawing.Size(93, 21);
            this.chkR2.TabIndex = 24;
            // 
            // chkR1
            // 
            this.chkR1.Location = new System.Drawing.Point(108, 89);
            this.chkR1.Name = "chkR1";
            this.chkR1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkR1.Properties.Appearance.Options.UseFont = true;
            this.chkR1.Properties.Caption = "R1针";
            this.chkR1.Size = new System.Drawing.Size(93, 21);
            this.chkR1.TabIndex = 23;
            // 
            // lblPollutionSource
            // 
            this.lblPollutionSource.Location = new System.Drawing.Point(184, 178);
            this.lblPollutionSource.Name = "lblPollutionSource";
            this.lblPollutionSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lblPollutionSource.Size = new System.Drawing.Size(100, 20);
            this.lblPollutionSource.TabIndex = 21;
            // 
            // textEdit4
            // 
            this.textEdit4.Location = new System.Drawing.Point(315, 372);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Size = new System.Drawing.Size(73, 20);
            this.textEdit4.TabIndex = 20;
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(156, 372);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(71, 20);
            this.textEdit3.TabIndex = 19;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(33, 331);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(56, 17);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "清洗液：";
            // 
            // lblKind
            // 
            this.lblKind.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKind.Appearance.Options.UseFont = true;
            this.lblKind.Location = new System.Drawing.Point(108, 373);
            this.lblKind.Name = "lblKind";
            this.lblKind.Size = new System.Drawing.Size(42, 17);
            this.lblKind.TabIndex = 9;
            this.lblKind.Text = "种类：";
            // 
            // lblUsageAmount
            // 
            this.lblUsageAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsageAmount.Appearance.Options.UseFont = true;
            this.lblUsageAmount.Location = new System.Drawing.Point(253, 373);
            this.lblUsageAmount.Name = "lblUsageAmount";
            this.lblUsageAmount.Size = new System.Drawing.Size(56, 17);
            this.lblUsageAmount.TabIndex = 8;
            this.lblUsageAmount.Text = "使用量：";
            // 
            // lblR1
            // 
            this.lblR1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblR1.Appearance.Options.UseFont = true;
            this.lblR1.Location = new System.Drawing.Point(33, 146);
            this.lblR1.Name = "lblR1";
            this.lblR1.Size = new System.Drawing.Size(56, 17);
            this.lblR1.TabIndex = 6;
            this.lblR1.Text = "污染源：";
            // 
            // lblPollutionProject
            // 
            this.lblPollutionProject.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPollutionProject.Appearance.Options.UseFont = true;
            this.lblPollutionProject.Location = new System.Drawing.Point(108, 179);
            this.lblPollutionProject.Name = "lblPollutionProject";
            this.lblPollutionProject.Size = new System.Drawing.Size(70, 17);
            this.lblPollutionProject.TabIndex = 5;
            this.lblPollutionProject.Text = "污染项目：";
            // 
            // lblByPollution
            // 
            this.lblByPollution.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblByPollution.Appearance.Options.UseFont = true;
            this.lblByPollution.Location = new System.Drawing.Point(33, 228);
            this.lblByPollution.Name = "lblByPollution";
            this.lblByPollution.Size = new System.Drawing.Size(70, 17);
            this.lblByPollution.TabIndex = 4;
            this.lblByPollution.Text = "被污染源：";
            // 
            // lblContaminatedProject
            // 
            this.lblContaminatedProject.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContaminatedProject.Appearance.Options.UseFont = true;
            this.lblContaminatedProject.Location = new System.Drawing.Point(108, 271);
            this.lblContaminatedProject.Name = "lblContaminatedProject";
            this.lblContaminatedProject.Size = new System.Drawing.Size(75, 17);
            this.lblContaminatedProject.TabIndex = 3;
            this.lblContaminatedProject.Text = "被污染项目:";
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
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(198, 270);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(100, 20);
            this.comboBoxEdit1.TabIndex = 25;
            // 
            // lblNumberOfCleans
            // 
            this.lblNumberOfCleans.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfCleans.Appearance.Options.UseFont = true;
            this.lblNumberOfCleans.Location = new System.Drawing.Point(33, 467);
            this.lblNumberOfCleans.Name = "lblNumberOfCleans";
            this.lblNumberOfCleans.Size = new System.Drawing.Size(70, 17);
            this.lblNumberOfCleans.TabIndex = 26;
            this.lblNumberOfCleans.Text = "清洗次数：";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(108, 466);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(89, 20);
            this.textEdit1.TabIndex = 27;
            // 
            // ReagentNeedle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl1);
            this.Name = "ReagentNeedle";
            this.Size = new System.Drawing.Size(1730, 852);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkR2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkR1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPollutionSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit chkR2;
        private DevExpress.XtraEditors.CheckEdit chkR1;
        private DevExpress.XtraEditors.ComboBoxEdit lblPollutionSource;
        private DevExpress.XtraEditors.TextEdit textEdit4;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblKind;
        private DevExpress.XtraEditors.LabelControl lblUsageAmount;
        private DevExpress.XtraEditors.LabelControl lblR1;
        private DevExpress.XtraEditors.LabelControl lblPollutionProject;
        private DevExpress.XtraEditors.LabelControl lblByPollution;
        private DevExpress.XtraEditors.LabelControl lblContaminatedProject;
        private DevExpress.XtraEditors.LabelControl lblReagentNeedle;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl lblNumberOfCleans;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    }
}
