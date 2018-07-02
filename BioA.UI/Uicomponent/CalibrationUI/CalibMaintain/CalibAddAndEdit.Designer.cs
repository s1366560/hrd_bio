namespace BioA.UI
{
    partial class CalibAddAndEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibAddAndEdit));
            this.lblCalibrationName = new DevExpress.XtraEditors.LabelControl();
            this.BatchNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblExpirationDate = new DevExpress.XtraEditors.LabelControl();
            this.cboCalibName = new DevExpress.XtraEditors.TextEdit();
            this.cboCalibBatchNumber = new DevExpress.XtraEditors.TextEdit();
            this.lstvProjectInfo = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboCalibPosition = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboCalibTManufacturer = new DevExpress.XtraEditors.TextEdit();
            this.cboCalibInvalidDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibBatchNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvProjectInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibTManufacturer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibInvalidDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibInvalidDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCalibrationName
            // 
            this.lblCalibrationName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalibrationName.Appearance.Options.UseFont = true;
            this.lblCalibrationName.Location = new System.Drawing.Point(27, 31);
            this.lblCalibrationName.Name = "lblCalibrationName";
            this.lblCalibrationName.Size = new System.Drawing.Size(84, 17);
            this.lblCalibrationName.TabIndex = 0;
            this.lblCalibrationName.Text = "校准品名称：";
            // 
            // BatchNumber
            // 
            this.BatchNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BatchNumber.Appearance.Options.UseFont = true;
            this.BatchNumber.Location = new System.Drawing.Point(232, 31);
            this.BatchNumber.Name = "BatchNumber";
            this.BatchNumber.Size = new System.Drawing.Size(42, 17);
            this.BatchNumber.TabIndex = 1;
            this.BatchNumber.Text = "批号：";
            // 
            // lblExpirationDate
            // 
            this.lblExpirationDate.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpirationDate.Appearance.Options.UseFont = true;
            this.lblExpirationDate.Location = new System.Drawing.Point(27, 70);
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Size = new System.Drawing.Size(70, 17);
            this.lblExpirationDate.TabIndex = 2;
            this.lblExpirationDate.Text = "失效日期：";
            // 
            // cboCalibName
            // 
            this.cboCalibName.Location = new System.Drawing.Point(117, 28);
            this.cboCalibName.Name = "cboCalibName";
            this.cboCalibName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCalibName.Properties.Appearance.Options.UseFont = true;
            this.cboCalibName.Size = new System.Drawing.Size(100, 24);
            this.cboCalibName.TabIndex = 3;
            // 
            // cboCalibBatchNumber
            // 
            this.cboCalibBatchNumber.Location = new System.Drawing.Point(280, 28);
            this.cboCalibBatchNumber.Name = "cboCalibBatchNumber";
            this.cboCalibBatchNumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCalibBatchNumber.Properties.Appearance.Options.UseFont = true;
            this.cboCalibBatchNumber.Size = new System.Drawing.Size(100, 24);
            this.cboCalibBatchNumber.TabIndex = 4;
            // 
            // lstvProjectInfo
            // 
            this.lstvProjectInfo.Location = new System.Drawing.Point(71, 109);
            this.lstvProjectInfo.MainView = this.gridView1;
            this.lstvProjectInfo.Name = "lstvProjectInfo";
            this.lstvProjectInfo.Size = new System.Drawing.Size(442, 481);
            this.lstvProjectInfo.TabIndex = 6;
            this.lstvProjectInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.lstvProjectInfo;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(393, 596);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 47);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "取消";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(117, 596);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 47);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(406, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 17);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "位置：";
            // 
            // cboCalibPosition
            // 
            this.cboCalibPosition.EditValue = "请选择";
            this.cboCalibPosition.Location = new System.Drawing.Point(455, 31);
            this.cboCalibPosition.Name = "cboCalibPosition";
            this.cboCalibPosition.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCalibPosition.Properties.Appearance.Options.UseFont = true;
            this.cboCalibPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCalibPosition.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboCalibPosition.Size = new System.Drawing.Size(100, 24);
            this.cboCalibPosition.TabIndex = 10;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(232, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 17);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "生产厂家：";
            // 
            // cboCalibTManufacturer
            // 
            this.cboCalibTManufacturer.Location = new System.Drawing.Point(308, 67);
            this.cboCalibTManufacturer.Name = "cboCalibTManufacturer";
            this.cboCalibTManufacturer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCalibTManufacturer.Properties.Appearance.Options.UseFont = true;
            this.cboCalibTManufacturer.Size = new System.Drawing.Size(247, 24);
            this.cboCalibTManufacturer.TabIndex = 12;
            // 
            // cboCalibInvalidDate
            // 
            this.cboCalibInvalidDate.EditValue = null;
            this.cboCalibInvalidDate.Location = new System.Drawing.Point(117, 67);
            this.cboCalibInvalidDate.Name = "cboCalibInvalidDate";
            this.cboCalibInvalidDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCalibInvalidDate.Properties.Appearance.Options.UseFont = true;
            this.cboCalibInvalidDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCalibInvalidDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCalibInvalidDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboCalibInvalidDate.Size = new System.Drawing.Size(100, 24);
            this.cboCalibInvalidDate.TabIndex = 13;
            // 
            // CalibAddAndEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 670);
            this.Controls.Add(this.cboCalibInvalidDate);
            this.Controls.Add(this.cboCalibTManufacturer);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cboCalibPosition);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lstvProjectInfo);
            this.Controls.Add(this.cboCalibBatchNumber);
            this.Controls.Add(this.cboCalibName);
            this.Controls.Add(this.lblExpirationDate);
            this.Controls.Add(this.BatchNumber);
            this.Controls.Add(this.lblCalibrationName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CalibAddAndEdit";
            this.Load += new System.EventHandler(this.CalibAddAndEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibBatchNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstvProjectInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibTManufacturer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibInvalidDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalibInvalidDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblCalibrationName;
        private DevExpress.XtraEditors.LabelControl BatchNumber;
        private DevExpress.XtraEditors.LabelControl lblExpirationDate;
        private DevExpress.XtraEditors.TextEdit cboCalibName;
        private DevExpress.XtraEditors.TextEdit cboCalibBatchNumber;
        private DevExpress.XtraGrid.GridControl lstvProjectInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboCalibPosition;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit cboCalibTManufacturer;
        private DevExpress.XtraEditors.DateEdit cboCalibInvalidDate;
    }
}