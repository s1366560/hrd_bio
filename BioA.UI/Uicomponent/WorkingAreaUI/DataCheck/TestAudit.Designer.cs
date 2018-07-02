namespace BioA.UI
{
    partial class TestAudit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestAudit));
            this.lblSampieSize = new System.Windows.Forms.Label();
            this.lblSampleID = new System.Windows.Forms.Label();
            this.lblSampleStatus = new System.Windows.Forms.Label();
            this.lblSampletype = new System.Windows.Forms.Label();
            this.grcCheckResult = new DevExpress.XtraGrid.GridControl();
            this.grvCheckResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnReactionCurve = new DevExpress.XtraEditors.SimpleButton();
            this.btnAudit = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextSample = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreviousSample = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtSampleNum = new DevExpress.XtraEditors.TextEdit();
            this.txtSampleState = new DevExpress.XtraEditors.TextEdit();
            this.txtSampleID = new DevExpress.XtraEditors.TextEdit();
            this.txtSampleType = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpApplyTime = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.grcCheckResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCheckResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSampieSize
            // 
            this.lblSampieSize.AutoSize = true;
            this.lblSampieSize.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampieSize.Location = new System.Drawing.Point(156, 11);
            this.lblSampieSize.Name = "lblSampieSize";
            this.lblSampieSize.Size = new System.Drawing.Size(78, 17);
            this.lblSampieSize.TabIndex = 0;
            this.lblSampieSize.Text = "样本编号：";
            // 
            // lblSampleID
            // 
            this.lblSampleID.AutoSize = true;
            this.lblSampleID.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleID.Location = new System.Drawing.Point(428, 11);
            this.lblSampleID.Name = "lblSampleID";
            this.lblSampleID.Size = new System.Drawing.Size(64, 17);
            this.lblSampleID.TabIndex = 3;
            this.lblSampleID.Text = "样本ID：";
            // 
            // lblSampleStatus
            // 
            this.lblSampleStatus.AutoSize = true;
            this.lblSampleStatus.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleStatus.Location = new System.Drawing.Point(691, 11);
            this.lblSampleStatus.Name = "lblSampleStatus";
            this.lblSampleStatus.Size = new System.Drawing.Size(78, 17);
            this.lblSampleStatus.TabIndex = 6;
            this.lblSampleStatus.Text = "样本状态：";
            // 
            // lblSampletype
            // 
            this.lblSampletype.AutoSize = true;
            this.lblSampletype.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampletype.Location = new System.Drawing.Point(260, 53);
            this.lblSampletype.Name = "lblSampletype";
            this.lblSampletype.Size = new System.Drawing.Size(78, 17);
            this.lblSampletype.TabIndex = 7;
            this.lblSampletype.Text = "样本类型：";
            // 
            // grcCheckResult
            // 
            this.grcCheckResult.Location = new System.Drawing.Point(69, 81);
            this.grcCheckResult.MainView = this.grvCheckResult;
            this.grcCheckResult.Name = "grcCheckResult";
            this.grcCheckResult.Size = new System.Drawing.Size(842, 460);
            this.grcCheckResult.TabIndex = 8;
            this.grcCheckResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCheckResult});
            // 
            // grvCheckResult
            // 
            this.grvCheckResult.GridControl = this.grcCheckResult;
            this.grvCheckResult.Name = "grvCheckResult";
            this.grvCheckResult.OptionsView.ShowGroupPanel = false;
            // 
            // btnReactionCurve
            // 
            this.btnReactionCurve.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReactionCurve.Appearance.Options.UseFont = true;
            this.btnReactionCurve.Image = ((System.Drawing.Image)(resources.GetObject("btnReactionCurve.Image")));
            this.btnReactionCurve.Location = new System.Drawing.Point(397, 558);
            this.btnReactionCurve.Name = "btnReactionCurve";
            this.btnReactionCurve.Size = new System.Drawing.Size(106, 48);
            this.btnReactionCurve.TabIndex = 9;
            this.btnReactionCurve.Text = "反应曲线";
            this.btnReactionCurve.Click += new System.EventHandler(this.btnReactionCurve_Click);
            // 
            // btnAudit
            // 
            this.btnAudit.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAudit.Appearance.Options.UseFont = true;
            this.btnAudit.Image = ((System.Drawing.Image)(resources.GetObject("btnAudit.Image")));
            this.btnAudit.Location = new System.Drawing.Point(288, 558);
            this.btnAudit.Name = "btnAudit";
            this.btnAudit.Size = new System.Drawing.Size(106, 48);
            this.btnAudit.TabIndex = 10;
            this.btnAudit.Text = "审核";
            this.btnAudit.Click += new System.EventHandler(this.btnAudit_Click);
            // 
            // btnNextSample
            // 
            this.btnNextSample.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextSample.Appearance.Options.UseFont = true;
            this.btnNextSample.Image = ((System.Drawing.Image)(resources.GetObject("btnNextSample.Image")));
            this.btnNextSample.Location = new System.Drawing.Point(179, 558);
            this.btnNextSample.Name = "btnNextSample";
            this.btnNextSample.Size = new System.Drawing.Size(106, 48);
            this.btnNextSample.TabIndex = 11;
            this.btnNextSample.Text = "下一样本";
            this.btnNextSample.Click += new System.EventHandler(this.btnNextSample_Click);
            // 
            // btnPreviousSample
            // 
            this.btnPreviousSample.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreviousSample.Appearance.Options.UseFont = true;
            this.btnPreviousSample.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousSample.Image")));
            this.btnPreviousSample.Location = new System.Drawing.Point(69, 558);
            this.btnPreviousSample.Name = "btnPreviousSample";
            this.btnPreviousSample.Size = new System.Drawing.Size(106, 48);
            this.btnPreviousSample.TabIndex = 12;
            this.btnPreviousSample.Text = "上一样本";
            this.btnPreviousSample.Click += new System.EventHandler(this.btnPreviousSample_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(824, 558);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 48);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSampleNum
            // 
            this.txtSampleNum.Location = new System.Drawing.Point(229, 10);
            this.txtSampleNum.Name = "txtSampleNum";
            this.txtSampleNum.Properties.ReadOnly = true;
            this.txtSampleNum.Size = new System.Drawing.Size(100, 20);
            this.txtSampleNum.TabIndex = 17;
            // 
            // txtSampleState
            // 
            this.txtSampleState.Location = new System.Drawing.Point(764, 10);
            this.txtSampleState.Name = "txtSampleState";
            this.txtSampleState.Properties.ReadOnly = true;
            this.txtSampleState.Size = new System.Drawing.Size(100, 20);
            this.txtSampleState.TabIndex = 20;
            // 
            // txtSampleID
            // 
            this.txtSampleID.Location = new System.Drawing.Point(489, 10);
            this.txtSampleID.Name = "txtSampleID";
            this.txtSampleID.Properties.ReadOnly = true;
            this.txtSampleID.Size = new System.Drawing.Size(100, 20);
            this.txtSampleID.TabIndex = 21;
            // 
            // txtSampleType
            // 
            this.txtSampleType.Location = new System.Drawing.Point(333, 52);
            this.txtSampleType.Name = "txtSampleType";
            this.txtSampleType.Properties.ReadOnly = true;
            this.txtSampleType.Size = new System.Drawing.Size(100, 20);
            this.txtSampleType.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(500, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "申请时间：";
            // 
            // dtpApplyTime
            // 
            this.dtpApplyTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpApplyTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApplyTime.Location = new System.Drawing.Point(575, 53);
            this.dtpApplyTime.Name = "dtpApplyTime";
            this.dtpApplyTime.Size = new System.Drawing.Size(167, 22);
            this.dtpApplyTime.TabIndex = 25;
            // 
            // TestAudit
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 618);
            this.Controls.Add(this.dtpApplyTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSampleType);
            this.Controls.Add(this.txtSampleID);
            this.Controls.Add(this.txtSampleState);
            this.Controls.Add(this.txtSampleNum);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPreviousSample);
            this.Controls.Add(this.btnNextSample);
            this.Controls.Add(this.btnAudit);
            this.Controls.Add(this.btnReactionCurve);
            this.Controls.Add(this.grcCheckResult);
            this.Controls.Add(this.lblSampletype);
            this.Controls.Add(this.lblSampleStatus);
            this.Controls.Add(this.lblSampleID);
            this.Controls.Add(this.lblSampieSize);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TestAudit";
            this.Text = "测试审核";
            this.Load += new System.EventHandler(this.TestAudit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcCheckResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCheckResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSampleType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSampieSize;
        private System.Windows.Forms.Label lblSampleID;
        private System.Windows.Forms.Label lblSampleStatus;
        private System.Windows.Forms.Label lblSampletype;
        private DevExpress.XtraGrid.GridControl grcCheckResult;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCheckResult;
        private DevExpress.XtraEditors.SimpleButton btnReactionCurve;
        private DevExpress.XtraEditors.SimpleButton btnAudit;
        private DevExpress.XtraEditors.SimpleButton btnNextSample;
        private DevExpress.XtraEditors.SimpleButton btnPreviousSample;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtSampleNum;
        private DevExpress.XtraEditors.TextEdit txtSampleState;
        private DevExpress.XtraEditors.TextEdit txtSampleID;
        private DevExpress.XtraEditors.TextEdit txtSampleType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpApplyTime;
    }
}