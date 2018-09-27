namespace BioA.UI
{
    partial class ReagentState
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReagentState));
            this.gridReagentState = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnLocking = new DevExpress.XtraEditors.SimpleButton();
            this.btnReverseSelection = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.deblocking = new DevExpress.XtraEditors.SimpleButton();
            this.btnR1MarginDetection = new DevExpress.XtraEditors.SimpleButton();
            this.btnR2MarginDetection = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridReagentState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridReagentState
            // 
            this.gridReagentState.Location = new System.Drawing.Point(17, 39);
            this.gridReagentState.MainView = this.gridView1;
            this.gridReagentState.Name = "gridReagentState";
            this.gridReagentState.Size = new System.Drawing.Size(1689, 804);
            this.gridReagentState.TabIndex = 0;
            this.gridReagentState.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridReagentState;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseMove);
            // 
            // btnLocking
            // 
            this.btnLocking.Image = ((System.Drawing.Image)(resources.GetObject("btnLocking.Image")));
            this.btnLocking.Location = new System.Drawing.Point(1120, 859);
            this.btnLocking.Name = "btnLocking";
            this.btnLocking.Size = new System.Drawing.Size(84, 45);
            this.btnLocking.TabIndex = 2;
            this.btnLocking.Text = "锁定";
            this.btnLocking.Click += new System.EventHandler(this.btnLocking_Click);
            // 
            // btnReverseSelection
            // 
            this.btnReverseSelection.Image = ((System.Drawing.Image)(resources.GetObject("btnReverseSelection.Image")));
            this.btnReverseSelection.Location = new System.Drawing.Point(984, 859);
            this.btnReverseSelection.Name = "btnReverseSelection";
            this.btnReverseSelection.Size = new System.Drawing.Size(84, 45);
            this.btnReverseSelection.TabIndex = 3;
            this.btnReverseSelection.Text = "反选";
            this.btnReverseSelection.Click += new System.EventHandler(this.btnReverseSelection_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(850, 859);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(84, 45);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "全选";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // deblocking
            // 
            this.deblocking.Image = ((System.Drawing.Image)(resources.GetObject("deblocking.Image")));
            this.deblocking.Location = new System.Drawing.Point(1257, 859);
            this.deblocking.Name = "deblocking";
            this.deblocking.Size = new System.Drawing.Size(84, 45);
            this.deblocking.TabIndex = 5;
            this.deblocking.Text = "解锁";
            this.deblocking.Click += new System.EventHandler(this.deblocking_Click);
            // 
            // btnR1MarginDetection
            // 
            this.btnR1MarginDetection.Image = ((System.Drawing.Image)(resources.GetObject("btnR1MarginDetection.Image")));
            this.btnR1MarginDetection.Location = new System.Drawing.Point(1408, 859);
            this.btnR1MarginDetection.Name = "btnR1MarginDetection";
            this.btnR1MarginDetection.Size = new System.Drawing.Size(114, 45);
            this.btnR1MarginDetection.TabIndex = 6;
            this.btnR1MarginDetection.Text = "R1余量检测";
            this.btnR1MarginDetection.Click += new System.EventHandler(this.btnR1MarginDetection_Click);
            // 
            // btnR2MarginDetection
            // 
            this.btnR2MarginDetection.Image = ((System.Drawing.Image)(resources.GetObject("btnR2MarginDetection.Image")));
            this.btnR2MarginDetection.Location = new System.Drawing.Point(1554, 859);
            this.btnR2MarginDetection.Name = "btnR2MarginDetection";
            this.btnR2MarginDetection.Size = new System.Drawing.Size(114, 45);
            this.btnR2MarginDetection.TabIndex = 7;
            this.btnR2MarginDetection.Text = "R2余量检测";
            this.btnR2MarginDetection.Click += new System.EventHandler(this.btnR2MarginDetection_Click);
            // 
            // ReagentState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnR2MarginDetection);
            this.Controls.Add(this.btnR1MarginDetection);
            this.Controls.Add(this.deblocking);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnReverseSelection);
            this.Controls.Add(this.btnLocking);
            this.Controls.Add(this.gridReagentState);
            this.Name = "ReagentState";
            this.Size = new System.Drawing.Size(1745, 928);
            this.Load += new System.EventHandler(this.ReagentState_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridReagentState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridReagentState;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnLocking;
        private DevExpress.XtraEditors.SimpleButton btnReverseSelection;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.SimpleButton deblocking;
        private DevExpress.XtraEditors.SimpleButton btnR1MarginDetection;
        private DevExpress.XtraEditors.SimpleButton btnR2MarginDetection;
    }
}
