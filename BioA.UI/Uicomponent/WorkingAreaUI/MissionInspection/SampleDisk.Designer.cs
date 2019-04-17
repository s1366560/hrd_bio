namespace BioA.UI
{
    partial class SampleDisk
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ComSampleNum = new System.Windows.Forms.ComboBox();
            this.ButFullSelection = new DevExpress.XtraEditors.SimpleButton();
            this.ButOuterRingSelection = new DevExpress.XtraEditors.SimpleButton();
            this.ButMiddleCircleSelection = new DevExpress.XtraEditors.SimpleButton();
            this.ButInnerRingSelection = new DevExpress.XtraEditors.SimpleButton();
            this.ButReverseSelection = new DevExpress.XtraEditors.SimpleButton();
            this.ButClearSample = new DevExpress.XtraEditors.SimpleButton();
            this.mbStartScan = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(3, 30);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1685, 734);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.CornflowerBlue;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.LineVisible = true;
            this.labelControl1.Location = new System.Drawing.Point(749, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "盘  符：";
            // 
            // ComSampleNum
            // 
            this.ComSampleNum.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComSampleNum.ForeColor = System.Drawing.Color.Red;
            this.ComSampleNum.FormattingEnabled = true;
            this.ComSampleNum.Location = new System.Drawing.Point(800, 3);
            this.ComSampleNum.Name = "ComSampleNum";
            this.ComSampleNum.Size = new System.Drawing.Size(62, 25);
            this.ComSampleNum.TabIndex = 2;
            this.ComSampleNum.SelectedIndexChanged += new System.EventHandler(this.ComSampleNum_SelectedIndexChanged);
            // 
            // ButFullSelection
            // 
            this.ButFullSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButFullSelection.Appearance.Options.UseFont = true;
            this.ButFullSelection.Location = new System.Drawing.Point(3, 782);
            this.ButFullSelection.Name = "ButFullSelection";
            this.ButFullSelection.Size = new System.Drawing.Size(113, 35);
            this.ButFullSelection.TabIndex = 3;
            this.ButFullSelection.Text = "全部选取";
            this.ButFullSelection.Click += new System.EventHandler(this.ButFullSelection_Click);
            // 
            // ButOuterRingSelection
            // 
            this.ButOuterRingSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButOuterRingSelection.Appearance.Options.UseFont = true;
            this.ButOuterRingSelection.Location = new System.Drawing.Point(153, 782);
            this.ButOuterRingSelection.Name = "ButOuterRingSelection";
            this.ButOuterRingSelection.Size = new System.Drawing.Size(113, 35);
            this.ButOuterRingSelection.TabIndex = 4;
            this.ButOuterRingSelection.Text = "外圈选取";
            this.ButOuterRingSelection.Click += new System.EventHandler(this.ButOuterRingSelection_Click);
            // 
            // ButMiddleCircleSelection
            // 
            this.ButMiddleCircleSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButMiddleCircleSelection.Appearance.Options.UseFont = true;
            this.ButMiddleCircleSelection.Location = new System.Drawing.Point(305, 782);
            this.ButMiddleCircleSelection.Name = "ButMiddleCircleSelection";
            this.ButMiddleCircleSelection.Size = new System.Drawing.Size(113, 35);
            this.ButMiddleCircleSelection.TabIndex = 5;
            this.ButMiddleCircleSelection.Text = "中间圈选取";
            this.ButMiddleCircleSelection.Click += new System.EventHandler(this.ButMiddleCircleSelection_Click);
            // 
            // ButInnerRingSelection
            // 
            this.ButInnerRingSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButInnerRingSelection.Appearance.Options.UseFont = true;
            this.ButInnerRingSelection.Location = new System.Drawing.Point(464, 782);
            this.ButInnerRingSelection.Name = "ButInnerRingSelection";
            this.ButInnerRingSelection.Size = new System.Drawing.Size(113, 35);
            this.ButInnerRingSelection.TabIndex = 6;
            this.ButInnerRingSelection.Text = "内圈选取";
            this.ButInnerRingSelection.Click += new System.EventHandler(this.ButInnerRingSelection_Click);
            // 
            // ButReverseSelection
            // 
            this.ButReverseSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButReverseSelection.Appearance.Options.UseFont = true;
            this.ButReverseSelection.Location = new System.Drawing.Point(620, 782);
            this.ButReverseSelection.Name = "ButReverseSelection";
            this.ButReverseSelection.Size = new System.Drawing.Size(113, 35);
            this.ButReverseSelection.TabIndex = 7;
            this.ButReverseSelection.Text = "反向选取";
            this.ButReverseSelection.Click += new System.EventHandler(this.ButReverseSelection_Click);
            // 
            // ButClearSample
            // 
            this.ButClearSample.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButClearSample.Appearance.Options.UseFont = true;
            this.ButClearSample.Location = new System.Drawing.Point(768, 782);
            this.ButClearSample.Name = "ButClearSample";
            this.ButClearSample.Size = new System.Drawing.Size(113, 35);
            this.ButClearSample.TabIndex = 8;
            this.ButClearSample.Text = "清除样本";
            this.ButClearSample.Click += new System.EventHandler(this.ButClearSample_Click);
            // 
            // mbStartScan
            // 
            this.mbStartScan.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbStartScan.Appearance.Options.UseFont = true;
            this.mbStartScan.Location = new System.Drawing.Point(909, 783);
            this.mbStartScan.Name = "mbStartScan";
            this.mbStartScan.Size = new System.Drawing.Size(128, 34);
            this.mbStartScan.TabIndex = 9;
            this.mbStartScan.Text = "扫描条形码(外圈)";
            this.mbStartScan.Click += new System.EventHandler(this.mbStartScan_Click);
            // 
            // SampleDisk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mbStartScan);
            this.Controls.Add(this.ButClearSample);
            this.Controls.Add(this.ButReverseSelection);
            this.Controls.Add(this.ButInnerRingSelection);
            this.Controls.Add(this.ButMiddleCircleSelection);
            this.Controls.Add(this.ButOuterRingSelection);
            this.Controls.Add(this.ButFullSelection);
            this.Controls.Add(this.ComSampleNum);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.gridControl1);
            this.Name = "SampleDisk";
            this.Size = new System.Drawing.Size(1691, 819);
            //this.Load += new System.EventHandler(this.SampleDisk_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox ComSampleNum;
        private DevExpress.XtraEditors.SimpleButton ButFullSelection;
        private DevExpress.XtraEditors.SimpleButton ButOuterRingSelection;
        private DevExpress.XtraEditors.SimpleButton ButMiddleCircleSelection;
        private DevExpress.XtraEditors.SimpleButton ButInnerRingSelection;
        private DevExpress.XtraEditors.SimpleButton ButReverseSelection;
        private DevExpress.XtraEditors.SimpleButton ButClearSample;
        private DevExpress.XtraEditors.SimpleButton mbStartScan;
    }
}
