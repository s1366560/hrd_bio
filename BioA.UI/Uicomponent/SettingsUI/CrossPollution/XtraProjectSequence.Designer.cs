namespace BioA.UI
{
    partial class XtraProjectSequence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraProjectSequence));
            this.lstProjectTestSequence = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simButSave = new DevExpress.XtraEditors.SimpleButton();
            this.simButCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simButShiftUp = new DevExpress.XtraEditors.SimpleButton();
            this.simButShiftDown = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lstProjectTestSequence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lstProjectTestSequence
            // 
            this.lstProjectTestSequence.Location = new System.Drawing.Point(12, 12);
            this.lstProjectTestSequence.MainView = this.gridView1;
            this.lstProjectTestSequence.Name = "lstProjectTestSequence";
            this.lstProjectTestSequence.Size = new System.Drawing.Size(257, 508);
            this.lstProjectTestSequence.TabIndex = 23;
            this.lstProjectTestSequence.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.CornflowerBlue;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.CornflowerBlue;
            this.gridView1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView1.GridControl = this.lstProjectTestSequence;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseMove);
            // 
            // simButSave
            // 
            this.simButSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simButSave.Appearance.Options.UseFont = true;
            this.simButSave.Location = new System.Drawing.Point(95, 530);
            this.simButSave.Name = "simButSave";
            this.simButSave.Size = new System.Drawing.Size(73, 42);
            this.simButSave.TabIndex = 24;
            this.simButSave.Text = "保存";
            this.simButSave.Click += new System.EventHandler(this.simButSave_Click);
            // 
            // simButCancel
            // 
            this.simButCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simButCancel.Appearance.Options.UseFont = true;
            this.simButCancel.Location = new System.Drawing.Point(196, 530);
            this.simButCancel.Name = "simButCancel";
            this.simButCancel.Size = new System.Drawing.Size(73, 42);
            this.simButCancel.TabIndex = 25;
            this.simButCancel.Text = "取消";
            this.simButCancel.Click += new System.EventHandler(this.simButCancel_Click);
            // 
            // simButShiftUp
            // 
            this.simButShiftUp.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simButShiftUp.Appearance.Options.UseFont = true;
            this.simButShiftUp.Image = ((System.Drawing.Image)(resources.GetObject("simButShiftUp.Image")));
            this.simButShiftUp.Location = new System.Drawing.Point(275, 354);
            this.simButShiftUp.Name = "simButShiftUp";
            this.simButShiftUp.Size = new System.Drawing.Size(43, 39);
            this.simButShiftUp.TabIndex = 26;
            this.simButShiftUp.Click += new System.EventHandler(this.simButShiftUp_Click);
            // 
            // simButShiftDown
            // 
            this.simButShiftDown.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simButShiftDown.Appearance.Options.UseFont = true;
            this.simButShiftDown.Image = ((System.Drawing.Image)(resources.GetObject("simButShiftDown.Image")));
            this.simButShiftDown.Location = new System.Drawing.Point(275, 479);
            this.simButShiftDown.Name = "simButShiftDown";
            this.simButShiftDown.Size = new System.Drawing.Size(43, 39);
            this.simButShiftDown.TabIndex = 27;
            this.simButShiftDown.Click += new System.EventHandler(this.simButShiftDown_Click);
            // 
            // XtraProjectSequence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 584);
            this.Controls.Add(this.simButShiftDown);
            this.Controls.Add(this.simButShiftUp);
            this.Controls.Add(this.simButCancel);
            this.Controls.Add(this.simButSave);
            this.Controls.Add(this.lstProjectTestSequence);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "XtraProjectSequence";
            this.Load += new System.EventHandler(this.XtraProjectSequence_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lstProjectTestSequence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl lstProjectTestSequence;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simButSave;
        private DevExpress.XtraEditors.SimpleButton simButCancel;
        private DevExpress.XtraEditors.SimpleButton simButShiftUp;
        private DevExpress.XtraEditors.SimpleButton simButShiftDown;
    }
}