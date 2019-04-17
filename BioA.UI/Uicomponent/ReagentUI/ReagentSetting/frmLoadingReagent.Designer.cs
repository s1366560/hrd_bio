namespace BioA.UI
{
    partial class frmLoadingReagent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadingReagent));
            this.lblReagentDisc = new DevExpress.XtraEditors.LabelControl();
            this.lblProChoice = new DevExpress.XtraEditors.LabelControl();
            this.lblExpirationDate = new DevExpress.XtraEditors.LabelControl();
            this.lblReagentName = new DevExpress.XtraEditors.LabelControl();
            this.lblReagentPos = new DevExpress.XtraEditors.LabelControl();
            this.lblBarCode = new DevExpress.XtraEditors.LabelControl();
            this.lblLotNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblContainer = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtBarcode = new DevExpress.XtraEditors.TextEdit();
            this.txtReagentName = new DevExpress.XtraEditors.TextEdit();
            this.txtBatchNum = new DevExpress.XtraEditors.TextEdit();
            this.cboProjectCheck = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboReagentType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboReagentVol = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboReagentPos = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dtpValidDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReagentName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectCheck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReagentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReagentVol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReagentPos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpValidDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpValidDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReagentDisc
            // 
            this.lblReagentDisc.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReagentDisc.Appearance.Options.UseFont = true;
            this.lblReagentDisc.Location = new System.Drawing.Point(57, 94);
            this.lblReagentDisc.Name = "lblReagentDisc";
            this.lblReagentDisc.Size = new System.Drawing.Size(70, 17);
            this.lblReagentDisc.TabIndex = 0;
            this.lblReagentDisc.Text = "试剂类型：";
            // 
            // lblProChoice
            // 
            this.lblProChoice.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProChoice.Appearance.Options.UseFont = true;
            this.lblProChoice.Location = new System.Drawing.Point(57, 225);
            this.lblProChoice.Name = "lblProChoice";
            this.lblProChoice.Size = new System.Drawing.Size(70, 17);
            this.lblProChoice.TabIndex = 3;
            this.lblProChoice.Text = "项目选择：";
            // 
            // lblExpirationDate
            // 
            this.lblExpirationDate.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpirationDate.Appearance.Options.UseFont = true;
            this.lblExpirationDate.Location = new System.Drawing.Point(411, 225);
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Size = new System.Drawing.Size(70, 17);
            this.lblExpirationDate.TabIndex = 4;
            this.lblExpirationDate.Text = "有效期限：";
            // 
            // lblReagentName
            // 
            this.lblReagentName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReagentName.Appearance.Options.UseFont = true;
            this.lblReagentName.Location = new System.Drawing.Point(57, 160);
            this.lblReagentName.Name = "lblReagentName";
            this.lblReagentName.Size = new System.Drawing.Size(70, 17);
            this.lblReagentName.TabIndex = 5;
            this.lblReagentName.Text = "试剂名称：";
            // 
            // lblReagentPos
            // 
            this.lblReagentPos.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReagentPos.Appearance.Options.UseFont = true;
            this.lblReagentPos.Location = new System.Drawing.Point(57, 288);
            this.lblReagentPos.Name = "lblReagentPos";
            this.lblReagentPos.Size = new System.Drawing.Size(70, 17);
            this.lblReagentPos.TabIndex = 6;
            this.lblReagentPos.Text = "试剂位置：";
            // 
            // lblBarCode
            // 
            this.lblBarCode.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarCode.Appearance.Options.UseFont = true;
            this.lblBarCode.Location = new System.Drawing.Point(411, 160);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(42, 17);
            this.lblBarCode.TabIndex = 7;
            this.lblBarCode.Text = "条码：";
            // 
            // lblLotNumber
            // 
            this.lblLotNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLotNumber.Appearance.Options.UseFont = true;
            this.lblLotNumber.Location = new System.Drawing.Point(411, 288);
            this.lblLotNumber.Name = "lblLotNumber";
            this.lblLotNumber.Size = new System.Drawing.Size(42, 17);
            this.lblLotNumber.TabIndex = 8;
            this.lblLotNumber.Text = "批号：";
            // 
            // lblContainer
            // 
            this.lblContainer.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContainer.Appearance.Options.UseFont = true;
            this.lblContainer.Location = new System.Drawing.Point(411, 94);
            this.lblContainer.Name = "lblContainer";
            this.lblContainer.Size = new System.Drawing.Size(42, 17);
            this.lblContainer.TabIndex = 9;
            this.lblContainer.Text = "容量：";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(461, 397);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 49);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(197, 397);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 49);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(484, 157);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Properties.Appearance.Options.UseFont = true;
            this.txtBarcode.Size = new System.Drawing.Size(164, 24);
            this.txtBarcode.TabIndex = 14;
            // 
            // txtReagentName
            // 
            this.txtReagentName.Location = new System.Drawing.Point(161, 157);
            this.txtReagentName.Name = "txtReagentName";
            this.txtReagentName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReagentName.Properties.Appearance.Options.UseFont = true;
            this.txtReagentName.Size = new System.Drawing.Size(175, 24);
            this.txtReagentName.TabIndex = 16;
            // 
            // txtBatchNum
            // 
            this.txtBatchNum.Location = new System.Drawing.Point(484, 285);
            this.txtBatchNum.Name = "txtBatchNum";
            this.txtBatchNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchNum.Properties.Appearance.Options.UseFont = true;
            this.txtBatchNum.Size = new System.Drawing.Size(164, 24);
            this.txtBatchNum.TabIndex = 17;
            // 
            // cboProjectCheck
            // 
            this.cboProjectCheck.Location = new System.Drawing.Point(161, 222);
            this.cboProjectCheck.Name = "cboProjectCheck";
            this.cboProjectCheck.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProjectCheck.Properties.Appearance.Options.UseFont = true;
            this.cboProjectCheck.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProjectCheck.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboProjectCheck.Size = new System.Drawing.Size(175, 24);
            this.cboProjectCheck.TabIndex = 20;
            // 
            // cboReagentType
            // 
            this.cboReagentType.EditValue = "";
            this.cboReagentType.Location = new System.Drawing.Point(161, 91);
            this.cboReagentType.Name = "cboReagentType";
            this.cboReagentType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReagentType.Properties.Appearance.Options.UseFont = true;
            this.cboReagentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReagentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboReagentType.Size = new System.Drawing.Size(175, 24);
            this.cboReagentType.TabIndex = 22;
            this.cboReagentType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit2_SelectedIndexChanged);
            // 
            // cboReagentVol
            // 
            this.cboReagentVol.Location = new System.Drawing.Point(484, 91);
            this.cboReagentVol.Name = "cboReagentVol";
            this.cboReagentVol.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReagentVol.Properties.Appearance.Options.UseFont = true;
            this.cboReagentVol.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReagentVol.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboReagentVol.Size = new System.Drawing.Size(164, 24);
            this.cboReagentVol.TabIndex = 23;
            this.cboReagentVol.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit3_SelectedIndexChanged);
            // 
            // cboReagentPos
            // 
            this.cboReagentPos.Location = new System.Drawing.Point(161, 285);
            this.cboReagentPos.Name = "cboReagentPos";
            this.cboReagentPos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReagentPos.Properties.Appearance.Options.UseFont = true;
            this.cboReagentPos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReagentPos.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboReagentPos.Size = new System.Drawing.Size(175, 24);
            this.cboReagentPos.TabIndex = 24;
            // 
            // dtpValidDate
            // 
            this.dtpValidDate.EditValue = null;
            this.dtpValidDate.Location = new System.Drawing.Point(484, 222);
            this.dtpValidDate.Name = "dtpValidDate";
            this.dtpValidDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpValidDate.Properties.Appearance.Options.UseFont = true;
            this.dtpValidDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpValidDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpValidDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtpValidDate.Size = new System.Drawing.Size(164, 24);
            this.dtpValidDate.TabIndex = 21;
            // 
            // frmLoadingReagent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 484);
            this.Controls.Add(this.cboReagentPos);
            this.Controls.Add(this.cboReagentVol);
            this.Controls.Add(this.cboReagentType);
            this.Controls.Add(this.dtpValidDate);
            this.Controls.Add(this.cboProjectCheck);
            this.Controls.Add(this.txtBatchNum);
            this.Controls.Add(this.txtReagentName);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblContainer);
            this.Controls.Add(this.lblLotNumber);
            this.Controls.Add(this.lblBarCode);
            this.Controls.Add(this.lblReagentPos);
            this.Controls.Add(this.lblReagentName);
            this.Controls.Add(this.lblExpirationDate);
            this.Controls.Add(this.lblProChoice);
            this.Controls.Add(this.lblReagentDisc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmLoadingReagent";
            this.Text = "装载试剂";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLoadingReagent_FormClosing);
            //this.Load += new System.EventHandler(this.frmLoadingReagent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReagentName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectCheck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReagentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReagentVol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReagentPos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpValidDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpValidDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblReagentDisc;
        private DevExpress.XtraEditors.LabelControl lblProChoice;
        private DevExpress.XtraEditors.LabelControl lblExpirationDate;
        private DevExpress.XtraEditors.LabelControl lblReagentName;
        private DevExpress.XtraEditors.LabelControl lblReagentPos;
        private DevExpress.XtraEditors.LabelControl lblBarCode;
        private DevExpress.XtraEditors.LabelControl lblLotNumber;
        private DevExpress.XtraEditors.LabelControl lblContainer;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtBarcode;
        private DevExpress.XtraEditors.TextEdit txtReagentName;
        private DevExpress.XtraEditors.TextEdit txtBatchNum;
        private DevExpress.XtraEditors.ComboBoxEdit cboProjectCheck;
        private DevExpress.XtraEditors.ComboBoxEdit cboReagentType;
        private DevExpress.XtraEditors.ComboBoxEdit cboReagentVol;
        private DevExpress.XtraEditors.ComboBoxEdit cboReagentPos;
        private DevExpress.XtraEditors.DateEdit dtpValidDate;
    }
}