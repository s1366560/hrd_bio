namespace BioA.UI
{
    partial class frmBatchInput
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSampleAmount = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rtxtInfo = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtStartSamNum = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartSamNum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(330, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "样本数量：";
            // 
            // txtSampleAmount
            // 
            this.txtSampleAmount.EditValue = "";
            this.txtSampleAmount.Location = new System.Drawing.Point(405, 34);
            this.txtSampleAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtSampleAmount.Name = "txtSampleAmount";
            this.txtSampleAmount.Properties.Appearance.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSampleAmount.Properties.Appearance.Options.UseFont = true;
            this.txtSampleAmount.Size = new System.Drawing.Size(75, 20);
            this.txtSampleAmount.TabIndex = 1;
            this.txtSampleAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSampleAmount_KeyPress);
            this.txtSampleAmount.Leave += new System.EventHandler(this.txtSampleAmount_Leave);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(124, 345);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 31);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "申请";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(372, 345);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 31);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(112, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "起始样本号：";
            // 
            // rtxtInfo
            // 
            this.rtxtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtInfo.Location = new System.Drawing.Point(115, 74);
            this.rtxtInfo.Name = "rtxtInfo";
            this.rtxtInfo.ReadOnly = true;
            this.rtxtInfo.Size = new System.Drawing.Size(365, 234);
            this.rtxtInfo.TabIndex = 6;
            this.rtxtInfo.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtStartSamNum
            // 
            this.txtStartSamNum.EditValue = "";
            this.txtStartSamNum.Location = new System.Drawing.Point(201, 34);
            this.txtStartSamNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtStartSamNum.Name = "txtStartSamNum";
            this.txtStartSamNum.Properties.Appearance.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStartSamNum.Properties.Appearance.Options.UseFont = true;
            this.txtStartSamNum.Properties.ReadOnly = true;
            this.txtStartSamNum.Size = new System.Drawing.Size(75, 20);
            this.txtStartSamNum.TabIndex = 7;
            this.txtStartSamNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartSamNum_KeyPress);
            this.txtStartSamNum.Leave += new System.EventHandler(this.txtStartSamNum_Leave);
            // 
            // frmBatchInput
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 419);
            this.Controls.Add(this.txtStartSamNum);
            this.Controls.Add(this.rtxtInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSampleAmount);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBatchInput";
            this.Text = "批量输入";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBatchInput_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartSamNum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtSampleAmount;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtxtInfo;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.TextEdit txtStartSamNum;
    }
}