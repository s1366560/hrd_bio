namespace BioA.UI
{
    partial class PatientInfoFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientInfoFrm));
            this.grpPatientInfoCheck = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnPatientInfoSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnPatientInfoEdit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpPatientInfoCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPatientInfoCheck
            // 
            this.grpPatientInfoCheck.AutoSize = true;
            this.grpPatientInfoCheck.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grpPatientInfoCheck.Location = new System.Drawing.Point(37, 90);
            this.grpPatientInfoCheck.Name = "grpPatientInfoCheck";
            this.grpPatientInfoCheck.Size = new System.Drawing.Size(1404, 619);
            this.grpPatientInfoCheck.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1349, 732);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 58);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPatientInfoSelect
            // 
            this.btnPatientInfoSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientInfoSelect.Appearance.Options.UseFont = true;
            this.btnPatientInfoSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientInfoSelect.Image")));
            this.btnPatientInfoSelect.Location = new System.Drawing.Point(210, 12);
            this.btnPatientInfoSelect.Name = "btnPatientInfoSelect";
            this.btnPatientInfoSelect.Size = new System.Drawing.Size(137, 63);
            this.btnPatientInfoSelect.TabIndex = 1;
            this.btnPatientInfoSelect.Text = "病人信息查看";
            this.btnPatientInfoSelect.Click += new System.EventHandler(this.btnPatientInfoSelect_Click);
            // 
            // btnPatientInfoEdit
            // 
            this.btnPatientInfoEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientInfoEdit.Appearance.Options.UseFont = true;
            this.btnPatientInfoEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientInfoEdit.Image")));
            this.btnPatientInfoEdit.Location = new System.Drawing.Point(37, 12);
            this.btnPatientInfoEdit.Name = "btnPatientInfoEdit";
            this.btnPatientInfoEdit.Size = new System.Drawing.Size(137, 63);
            this.btnPatientInfoEdit.TabIndex = 0;
            this.btnPatientInfoEdit.Text = "病人信息编辑";
            this.btnPatientInfoEdit.Click += new System.EventHandler(this.btnPatientInfoEdit_Click);
            // 
            // PatientInfoFrm
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1491, 804);
            this.Controls.Add(this.grpPatientInfoCheck);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPatientInfoSelect);
            this.Controls.Add(this.btnPatientInfoEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PatientInfoFrm";
            this.Text = "病人信息";
            this.Load += new System.EventHandler(this.PatientInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpPatientInfoCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnPatientInfoEdit;
        private DevExpress.XtraEditors.SimpleButton btnPatientInfoSelect;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.PanelControl grpPatientInfoCheck;

    }
}