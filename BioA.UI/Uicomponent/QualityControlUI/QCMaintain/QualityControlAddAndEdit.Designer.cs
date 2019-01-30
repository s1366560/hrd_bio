namespace BioA.UI
{
    partial class QualityControlAddAndEdit
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QualityControlAddAndEdit));
            this.lblQCName = new DevExpress.XtraEditors.LabelControl();
            this.lblExpirationDate = new DevExpress.XtraEditors.LabelControl();
            this.lblPosition = new DevExpress.XtraEditors.LabelControl();
            this.lblBatchNumber = new DevExpress.XtraEditors.LabelControl();
            this.lstvQCMaintainInfos = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.txtQCName = new DevExpress.XtraEditors.TextEdit();
            this.txtLotNum = new DevExpress.XtraEditors.TextEdit();
            this.dtpInvalidDate = new System.Windows.Forms.DateTimePicker();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.combLevelConc = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtManufacturer = new DevExpress.XtraEditors.TextEdit();
            this.cboPosition = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvQCMaintainInfos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combLevelConc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPosition.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQCName
            // 
            this.lblQCName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQCName.Appearance.Options.UseFont = true;
            this.lblQCName.Location = new System.Drawing.Point(67, 10);
            this.lblQCName.Name = "lblQCName";
            this.lblQCName.Size = new System.Drawing.Size(84, 17);
            this.lblQCName.TabIndex = 0;
            this.lblQCName.Text = "质控品名称：";
            // 
            // lblExpirationDate
            // 
            this.lblExpirationDate.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpirationDate.Appearance.Options.UseFont = true;
            this.lblExpirationDate.Location = new System.Drawing.Point(468, 54);
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Size = new System.Drawing.Size(70, 17);
            this.lblExpirationDate.TabIndex = 2;
            this.lblExpirationDate.Text = "失效日期：";
            // 
            // lblPosition
            // 
            this.lblPosition.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.Appearance.Options.UseFont = true;
            this.lblPosition.Location = new System.Drawing.Point(808, 12);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(42, 17);
            this.lblPosition.TabIndex = 3;
            this.lblPosition.Text = "位置：";
            // 
            // lblBatchNumber
            // 
            this.lblBatchNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchNumber.Appearance.Options.UseFont = true;
            this.lblBatchNumber.Location = new System.Drawing.Point(570, 12);
            this.lblBatchNumber.Name = "lblBatchNumber";
            this.lblBatchNumber.Size = new System.Drawing.Size(42, 17);
            this.lblBatchNumber.TabIndex = 4;
            this.lblBatchNumber.Text = "批号：";
            // 
            // lstvQCMaintainInfos
            // 
            this.lstvQCMaintainInfos.Location = new System.Drawing.Point(43, 94);
            this.lstvQCMaintainInfos.MainView = this.gridView1;
            this.lstvQCMaintainInfos.Name = "lstvQCMaintainInfos";
            this.lstvQCMaintainInfos.Size = new System.Drawing.Size(935, 523);
            this.lstvQCMaintainInfos.TabIndex = 5;
            this.lstvQCMaintainInfos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.lstvQCMaintainInfos;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(768, 630);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 48);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(885, 630);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(93, 48);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "取消";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtQCName
            // 
            this.txtQCName.Location = new System.Drawing.Point(157, 10);
            this.txtQCName.Name = "txtQCName";
            this.txtQCName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQCName.Properties.Appearance.Options.UseFont = true;
            this.txtQCName.Size = new System.Drawing.Size(146, 24);
            this.txtQCName.TabIndex = 8;
            // 
            // txtLotNum
            // 
            this.txtLotNum.Location = new System.Drawing.Point(618, 12);
            this.txtLotNum.Name = "txtLotNum";
            this.txtLotNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotNum.Properties.Appearance.Options.UseFont = true;
            this.txtLotNum.Size = new System.Drawing.Size(164, 24);
            this.txtLotNum.TabIndex = 9;
            // 
            // dtpInvalidDate
            // 
            this.dtpInvalidDate.Location = new System.Drawing.Point(544, 50);
            this.dtpInvalidDate.Name = "dtpInvalidDate";
            this.dtpInvalidDate.Size = new System.Drawing.Size(148, 22);
            this.dtpInvalidDate.TabIndex = 11;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(344, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 17);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "水平浓度：";
            // 
            // combLevelConc
            // 
            this.combLevelConc.Location = new System.Drawing.Point(420, 12);
            this.combLevelConc.Name = "combLevelConc";
            this.combLevelConc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combLevelConc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combLevelConc.Size = new System.Drawing.Size(100, 20);
            this.combLevelConc.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(67, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 17);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "生产厂家：";
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Location = new System.Drawing.Point(157, 51);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManufacturer.Properties.Appearance.Options.UseFont = true;
            this.txtManufacturer.Size = new System.Drawing.Size(249, 24);
            this.txtManufacturer.TabIndex = 15;
            // 
            // cboPosition
            // 
            this.cboPosition.Location = new System.Drawing.Point(856, 12);
            this.cboPosition.Name = "cboPosition";
            this.cboPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPosition.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboPosition.Size = new System.Drawing.Size(100, 20);
            this.cboPosition.TabIndex = 16;
            // 
            // QualityControlAddAndEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 692);
            this.Controls.Add(this.cboPosition);
            this.Controls.Add(this.txtManufacturer);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.combLevelConc);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dtpInvalidDate);
            this.Controls.Add(this.txtLotNum);
            this.Controls.Add(this.txtQCName);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lstvQCMaintainInfos);
            this.Controls.Add(this.lblBatchNumber);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblExpirationDate);
            this.Controls.Add(this.lblQCName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "QualityControlAddAndEdit";
            this.Load += new System.EventHandler(this.QualityControlAddAndEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lstvQCMaintainInfos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combLevelConc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPosition.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblQCName;
        private DevExpress.XtraEditors.LabelControl lblExpirationDate;
        private DevExpress.XtraEditors.LabelControl lblPosition;
        private DevExpress.XtraEditors.LabelControl lblBatchNumber;
        private DevExpress.XtraGrid.GridControl lstvQCMaintainInfos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.TextEdit txtQCName;
        private DevExpress.XtraEditors.TextEdit txtLotNum;
        private System.Windows.Forms.DateTimePicker dtpInvalidDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit combLevelConc;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtManufacturer;
        private DevExpress.XtraEditors.ComboBoxEdit cboPosition;
    }
}