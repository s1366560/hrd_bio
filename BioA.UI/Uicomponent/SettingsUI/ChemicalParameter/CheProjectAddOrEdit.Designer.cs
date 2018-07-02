namespace BioA.UI.Uicomponent.SettingsUI.ChemicalParameter
{
    partial class CheProjectAddOrEdit
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
            this.lblProShortName = new DevExpress.XtraEditors.LabelControl();
            this.lblChannelNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblProLongName = new DevExpress.XtraEditors.LabelControl();
            this.lblSampleType = new DevExpress.XtraEditors.LabelControl();
            this.txtProShortName = new DevExpress.XtraEditors.TextEdit();
            this.txtProLongName = new DevExpress.XtraEditors.TextEdit();
            this.txtChannelNumber = new DevExpress.XtraEditors.TextEdit();
            this.cboSampleType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtProShortName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProLongName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChannelNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSampleType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProShortName
            // 
            this.lblProShortName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProShortName.Appearance.Options.UseFont = true;
            this.lblProShortName.Location = new System.Drawing.Point(116, 75);
            this.lblProShortName.Name = "lblProShortName";
            this.lblProShortName.Size = new System.Drawing.Size(70, 17);
            this.lblProShortName.TabIndex = 0;
            this.lblProShortName.Text = "项目简称：";
            // 
            // lblChannelNumber
            // 
            this.lblChannelNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChannelNumber.Appearance.Options.UseFont = true;
            this.lblChannelNumber.Location = new System.Drawing.Point(116, 271);
            this.lblChannelNumber.Name = "lblChannelNumber";
            this.lblChannelNumber.Size = new System.Drawing.Size(56, 17);
            this.lblChannelNumber.TabIndex = 1;
            this.lblChannelNumber.Text = "通道号：";
            // 
            // lblProLongName
            // 
            this.lblProLongName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProLongName.Appearance.Options.UseFont = true;
            this.lblProLongName.Location = new System.Drawing.Point(116, 208);
            this.lblProLongName.Name = "lblProLongName";
            this.lblProLongName.Size = new System.Drawing.Size(70, 17);
            this.lblProLongName.TabIndex = 2;
            this.lblProLongName.Text = "项目全称：";
            // 
            // lblSampleType
            // 
            this.lblSampleType.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleType.Appearance.Options.UseFont = true;
            this.lblSampleType.Location = new System.Drawing.Point(116, 145);
            this.lblSampleType.Name = "lblSampleType";
            this.lblSampleType.Size = new System.Drawing.Size(70, 17);
            this.lblSampleType.TabIndex = 3;
            this.lblSampleType.Text = "样本类型：";
            // 
            // txtProShortName
            // 
            this.txtProShortName.Location = new System.Drawing.Point(192, 72);
            this.txtProShortName.Name = "txtProShortName";
            this.txtProShortName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProShortName.Properties.Appearance.Options.UseFont = true;
            this.txtProShortName.Size = new System.Drawing.Size(121, 24);
            this.txtProShortName.TabIndex = 4;
            // 
            // txtProLongName
            // 
            this.txtProLongName.Location = new System.Drawing.Point(192, 205);
            this.txtProLongName.Name = "txtProLongName";
            this.txtProLongName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProLongName.Properties.Appearance.Options.UseFont = true;
            this.txtProLongName.Size = new System.Drawing.Size(121, 24);
            this.txtProLongName.TabIndex = 5;
            // 
            // txtChannelNumber
            // 
            this.txtChannelNumber.Location = new System.Drawing.Point(192, 268);
            this.txtChannelNumber.Name = "txtChannelNumber";
            this.txtChannelNumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChannelNumber.Properties.Appearance.Options.UseFont = true;
            this.txtChannelNumber.Size = new System.Drawing.Size(121, 24);
            this.txtChannelNumber.TabIndex = 6;
            // 
            // cboSampleType
            // 
            this.cboSampleType.EditValue = "血清";
            this.cboSampleType.Location = new System.Drawing.Point(192, 142);
            this.cboSampleType.Name = "cboSampleType";
            this.cboSampleType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSampleType.Properties.Appearance.Options.UseFont = true;
            this.cboSampleType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.cboSampleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSampleType.Properties.DropDownRows = 5;
            this.cboSampleType.Properties.Items.AddRange(new object[] {
            "尿液",
            "血清"});
            this.cboSampleType.Size = new System.Drawing.Size(121, 26);
            this.cboSampleType.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(277, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 59);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Appearance.Options.UseFont = true;
            this.btnConfirm.Location = new System.Drawing.Point(87, 345);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 59);
            this.btnConfirm.TabIndex = 9;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // CheProjectAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 440);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboSampleType);
            this.Controls.Add(this.txtChannelNumber);
            this.Controls.Add(this.txtProLongName);
            this.Controls.Add(this.txtProShortName);
            this.Controls.Add(this.lblSampleType);
            this.Controls.Add(this.lblProLongName);
            this.Controls.Add(this.lblChannelNumber);
            this.Controls.Add(this.lblProShortName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CheProjectAddOrEdit";
            this.Text = "新建项目";
            ((System.ComponentModel.ISupportInitialize)(this.txtProShortName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProLongName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChannelNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSampleType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblProShortName;
        private DevExpress.XtraEditors.LabelControl lblChannelNumber;
        private DevExpress.XtraEditors.LabelControl lblProLongName;
        private DevExpress.XtraEditors.LabelControl lblSampleType;
        private DevExpress.XtraEditors.TextEdit txtProShortName;
        private DevExpress.XtraEditors.TextEdit txtProLongName;
        private DevExpress.XtraEditors.TextEdit txtChannelNumber;
        private DevExpress.XtraEditors.ComboBoxEdit cboSampleType;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
    }
}