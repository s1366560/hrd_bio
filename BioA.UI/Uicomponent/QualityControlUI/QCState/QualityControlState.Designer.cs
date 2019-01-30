namespace BioA.UI
{
    partial class QualityControlState
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QualityControlState));
            this.lblProName = new DevExpress.XtraEditors.LabelControl();
            this.lblLotNum = new DevExpress.XtraEditors.LabelControl();
            this.lblQCProduct = new DevExpress.XtraEditors.LabelControl();
            this.lblQCTime = new DevExpress.XtraEditors.LabelControl();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtLotNum = new DevExpress.XtraEditors.TextEdit();
            this.dtpQCStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpQCEndTime = new System.Windows.Forms.DateTimePicker();
            this.lstQCResult = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnReactionProcess = new DevExpress.XtraEditors.SimpleButton();
            this.btnRepeat = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.txtProjectName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtQCName = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstQCResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProName
            // 
            this.lblProName.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProName.Appearance.Options.UseFont = true;
            this.lblProName.Location = new System.Drawing.Point(16, 49);
            this.lblProName.Name = "lblProName";
            this.lblProName.Size = new System.Drawing.Size(70, 17);
            this.lblProName.TabIndex = 0;
            this.lblProName.Text = "项目名称：";
            // 
            // lblLotNum
            // 
            this.lblLotNum.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLotNum.Appearance.Options.UseFont = true;
            this.lblLotNum.Location = new System.Drawing.Point(393, 49);
            this.lblLotNum.Name = "lblLotNum";
            this.lblLotNum.Size = new System.Drawing.Size(42, 17);
            this.lblLotNum.TabIndex = 2;
            this.lblLotNum.Text = "批号：";
            // 
            // lblQCProduct
            // 
            this.lblQCProduct.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQCProduct.Appearance.Options.UseFont = true;
            this.lblQCProduct.Location = new System.Drawing.Point(209, 49);
            this.lblQCProduct.Name = "lblQCProduct";
            this.lblQCProduct.Size = new System.Drawing.Size(56, 17);
            this.lblQCProduct.TabIndex = 3;
            this.lblQCProduct.Text = "质控品：";
            // 
            // lblQCTime
            // 
            this.lblQCTime.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQCTime.Appearance.Options.UseFont = true;
            this.lblQCTime.Location = new System.Drawing.Point(617, 49);
            this.lblQCTime.Name = "lblQCTime";
            this.lblQCTime.Size = new System.Drawing.Size(70, 17);
            this.lblQCTime.TabIndex = 4;
            this.lblQCTime.Text = "质控时间：";
            // 
            // lblTo
            // 
            this.lblTo.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Appearance.Options.UseFont = true;
            this.lblTo.Location = new System.Drawing.Point(832, 46);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(5, 17);
            this.lblTo.TabIndex = 5;
            this.lblTo.Text = "-";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txtLotNum
            // 
            this.txtLotNum.Location = new System.Drawing.Point(441, 46);
            this.txtLotNum.Name = "txtLotNum";
            this.txtLotNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotNum.Properties.Appearance.Options.UseFont = true;
            this.txtLotNum.Size = new System.Drawing.Size(157, 24);
            this.txtLotNum.TabIndex = 9;
            // 
            // dtpQCStartTime
            // 
            this.dtpQCStartTime.CustomFormat = "yyyy-MM-dd";
            this.dtpQCStartTime.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpQCStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpQCStartTime.Location = new System.Drawing.Point(693, 46);
            this.dtpQCStartTime.Name = "dtpQCStartTime";
            this.dtpQCStartTime.Size = new System.Drawing.Size(133, 24);
            this.dtpQCStartTime.TabIndex = 11;
            // 
            // dtpQCEndTime
            // 
            this.dtpQCEndTime.CustomFormat = "yyyy-MM-dd";
            this.dtpQCEndTime.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpQCEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpQCEndTime.Location = new System.Drawing.Point(843, 46);
            this.dtpQCEndTime.Name = "dtpQCEndTime";
            this.dtpQCEndTime.Size = new System.Drawing.Size(130, 24);
            this.dtpQCEndTime.TabIndex = 12;
            // 
            // lstQCResult
            // 
            this.lstQCResult.Location = new System.Drawing.Point(12, 82);
            this.lstQCResult.MainView = this.gridView1;
            this.lstQCResult.Name = "lstQCResult";
            this.lstQCResult.Size = new System.Drawing.Size(1589, 811);
            this.lstQCResult.TabIndex = 13;
            this.lstQCResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.lstQCResult;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(1116, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 37);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReactionProcess
            // 
            this.btnReactionProcess.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReactionProcess.Appearance.Options.UseFont = true;
            this.btnReactionProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnReactionProcess.Image")));
            this.btnReactionProcess.Location = new System.Drawing.Point(1607, 82);
            this.btnReactionProcess.Name = "btnReactionProcess";
            this.btnReactionProcess.Size = new System.Drawing.Size(100, 53);
            this.btnReactionProcess.TabIndex = 15;
            this.btnReactionProcess.Text = "反应进程";
            this.btnReactionProcess.Click += new System.EventHandler(this.btnReactionProcess_Click);
            // 
            // btnRepeat
            // 
            this.btnRepeat.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepeat.Appearance.Options.UseFont = true;
            this.btnRepeat.Image = ((System.Drawing.Image)(resources.GetObject("btnRepeat.Image")));
            this.btnRepeat.Location = new System.Drawing.Point(1607, 141);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(100, 53);
            this.btnRepeat.TabIndex = 16;
            this.btnRepeat.Text = "重复性";
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(1611, 831);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 53);
            this.btnEdit.TabIndex = 18;
            this.btnEdit.Text = "编辑";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(92, 48);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtProjectName.Size = new System.Drawing.Size(100, 20);
            this.txtProjectName.TabIndex = 19;
            this.txtProjectName.SelectedIndexChanged += new System.EventHandler(this.txtProjectName_SelectedIndexChanged);
            // 
            // txtQCName
            // 
            this.txtQCName.Location = new System.Drawing.Point(271, 48);
            this.txtQCName.Name = "txtQCName";
            this.txtQCName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtQCName.Size = new System.Drawing.Size(100, 20);
            this.txtQCName.TabIndex = 20;
            this.txtQCName.SelectedIndexChanged += new System.EventHandler(this.txtQCName_SelectedIndexChanged);
            // 
            // QualityControlState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtQCName);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnRepeat);
            this.Controls.Add(this.btnReactionProcess);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lstQCResult);
            this.Controls.Add(this.dtpQCEndTime);
            this.Controls.Add(this.dtpQCStartTime);
            this.Controls.Add(this.txtLotNum);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblQCTime);
            this.Controls.Add(this.lblQCProduct);
            this.Controls.Add(this.lblLotNum);
            this.Controls.Add(this.lblProName);
            this.Name = "QualityControlState";
            this.Size = new System.Drawing.Size(1721, 906);
            this.Load += new System.EventHandler(this.QualityControlState_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstQCResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblProName;
        private DevExpress.XtraEditors.LabelControl lblLotNum;
        private DevExpress.XtraEditors.LabelControl lblQCProduct;
        private DevExpress.XtraEditors.LabelControl lblQCTime;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private DevExpress.XtraEditors.TextEdit txtLotNum;
        private System.Windows.Forms.DateTimePicker dtpQCStartTime;
        private System.Windows.Forms.DateTimePicker dtpQCEndTime;
        private DevExpress.XtraGrid.GridControl lstQCResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnReactionProcess;
        private DevExpress.XtraEditors.SimpleButton btnRepeat;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.ComboBoxEdit txtProjectName;
        private DevExpress.XtraEditors.ComboBoxEdit txtQCName;
    }
}
