namespace BioA.UI
{
    partial class lstvCalibrationState
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lstvCalibrationState));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCalibTrace = new DevExpress.XtraEditors.SimpleButton();
            this.btnCalibCurve = new DevExpress.XtraEditors.SimpleButton();
            this.btnReactionProcess = new DevExpress.XtraEditors.SimpleButton();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(14, 48);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1567, 843);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnCalibTrace
            // 
            this.btnCalibTrace.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalibTrace.Appearance.Options.UseFont = true;
            this.btnCalibTrace.Image = ((System.Drawing.Image)(resources.GetObject("btnCalibTrace.Image")));
            this.btnCalibTrace.Location = new System.Drawing.Point(1587, 711);
            this.btnCalibTrace.Name = "btnCalibTrace";
            this.btnCalibTrace.Size = new System.Drawing.Size(114, 56);
            this.btnCalibTrace.TabIndex = 3;
            this.btnCalibTrace.Text = "校准追溯";
            this.btnCalibTrace.Click += new System.EventHandler(this.btnCalibTrace_Click);
            // 
            // btnCalibCurve
            // 
            this.btnCalibCurve.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalibCurve.Appearance.Options.UseFont = true;
            this.btnCalibCurve.Image = ((System.Drawing.Image)(resources.GetObject("btnCalibCurve.Image")));
            this.btnCalibCurve.Location = new System.Drawing.Point(1587, 773);
            this.btnCalibCurve.Name = "btnCalibCurve";
            this.btnCalibCurve.Size = new System.Drawing.Size(114, 56);
            this.btnCalibCurve.TabIndex = 4;
            this.btnCalibCurve.Text = "校准曲线";
            this.btnCalibCurve.Click += new System.EventHandler(this.btnCalibCurve_Click);
            // 
            // btnReactionProcess
            // 
            this.btnReactionProcess.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReactionProcess.Appearance.Options.UseFont = true;
            this.btnReactionProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnReactionProcess.Image")));
            this.btnReactionProcess.Location = new System.Drawing.Point(1587, 835);
            this.btnReactionProcess.Name = "btnReactionProcess";
            this.btnReactionProcess.Size = new System.Drawing.Size(114, 56);
            this.btnReactionProcess.TabIndex = 5;
            this.btnReactionProcess.Text = "反应进程";
            this.btnReactionProcess.Click += new System.EventHandler(this.btnReactionProcess_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1587, 660);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 45);
            this.button1.TabIndex = 6;
            this.button1.Text = "调试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstvCalibrationState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnReactionProcess);
            this.Controls.Add(this.btnCalibCurve);
            this.Controls.Add(this.btnCalibTrace);
            this.Controls.Add(this.gridControl1);
            this.Name = "lstvCalibrationState";
            this.Size = new System.Drawing.Size(1717, 906);
            this.Load += new System.EventHandler(this.CalibrationState_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnCalibTrace;
        private DevExpress.XtraEditors.SimpleButton btnCalibCurve;
        private DevExpress.XtraEditors.SimpleButton btnReactionProcess;
        private System.Windows.Forms.Button button1;
    }
}
