namespace BioA.UI
{
    partial class DataConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataConfig));
            this.lblResultUnitInput = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdateResultUnit = new DevExpress.XtraEditors.SimpleButton();
            this.btnResultUnitCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.btnDeleteDilutionRatio = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdateDilutionRatio = new DevExpress.XtraEditors.SimpleButton();
            this.btnDilutionRatioCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddDilutionRatio = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblResultUnitInput
            // 
            this.lblResultUnitInput.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultUnitInput.Appearance.Options.UseFont = true;
            this.lblResultUnitInput.Location = new System.Drawing.Point(57, 37);
            this.lblResultUnitInput.Name = "lblResultUnitInput";
            this.lblResultUnitInput.Size = new System.Drawing.Size(98, 17);
            this.lblResultUnitInput.TabIndex = 0;
            this.lblResultUnitInput.Text = "结果单位录入：";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(57, 203);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(299, 680);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(57, 144);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(98, 48);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdateResultUnit
            // 
            this.btnUpdateResultUnit.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateResultUnit.Appearance.Options.UseFont = true;
            this.btnUpdateResultUnit.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateResultUnit.Image")));
            this.btnUpdateResultUnit.Location = new System.Drawing.Point(258, 90);
            this.btnUpdateResultUnit.Name = "btnUpdateResultUnit";
            this.btnUpdateResultUnit.Size = new System.Drawing.Size(98, 48);
            this.btnUpdateResultUnit.TabIndex = 18;
            this.btnUpdateResultUnit.Text = "修改";
            this.btnUpdateResultUnit.Click += new System.EventHandler(this.btnUpdateResultUnit_Clik);
            // 
            // btnResultUnitCancel
            // 
            this.btnResultUnitCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResultUnitCancel.Appearance.Options.UseFont = true;
            this.btnResultUnitCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnResultUnitCancel.Image")));
            this.btnResultUnitCancel.Location = new System.Drawing.Point(258, 149);
            this.btnResultUnitCancel.Name = "btnResultUnitCancel";
            this.btnResultUnitCancel.Size = new System.Drawing.Size(98, 48);
            this.btnResultUnitCancel.TabIndex = 17;
            this.btnResultUnitCancel.Text = "取消";
            this.btnResultUnitCancel.Click += new System.EventHandler(this.btnResultUnitCancel_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(57, 90);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(98, 48);
            this.simpleButton1.TabIndex = 16;
            this.simpleButton1.Text = "新增";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(57, 60);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Size = new System.Drawing.Size(299, 24);
            this.textEdit1.TabIndex = 20;
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(400, 60);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit2.Properties.Appearance.Options.UseFont = true;
            this.textEdit2.Size = new System.Drawing.Size(299, 24);
            this.textEdit2.TabIndex = 27;
            // 
            // btnDeleteDilutionRatio
            // 
            this.btnDeleteDilutionRatio.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteDilutionRatio.Appearance.Options.UseFont = true;
            this.btnDeleteDilutionRatio.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteDilutionRatio.Image")));
            this.btnDeleteDilutionRatio.Location = new System.Drawing.Point(400, 144);
            this.btnDeleteDilutionRatio.Name = "btnDeleteDilutionRatio";
            this.btnDeleteDilutionRatio.Size = new System.Drawing.Size(98, 48);
            this.btnDeleteDilutionRatio.TabIndex = 26;
            this.btnDeleteDilutionRatio.Text = "删除";
            this.btnDeleteDilutionRatio.Click += new System.EventHandler(this.btnDeleteDilutionRatio_Click);
            // 
            // btnUpdateDilutionRatio
            // 
            this.btnUpdateDilutionRatio.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDilutionRatio.Appearance.Options.UseFont = true;
            this.btnUpdateDilutionRatio.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateDilutionRatio.Image")));
            this.btnUpdateDilutionRatio.Location = new System.Drawing.Point(601, 90);
            this.btnUpdateDilutionRatio.Name = "btnUpdateDilutionRatio";
            this.btnUpdateDilutionRatio.Size = new System.Drawing.Size(98, 48);
            this.btnUpdateDilutionRatio.TabIndex = 25;
            this.btnUpdateDilutionRatio.Text = "修改";
            this.btnUpdateDilutionRatio.Click += new System.EventHandler(this.btnUpdateDilutionRatio_Click);
            // 
            // btnDilutionRatioCancel
            // 
            this.btnDilutionRatioCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDilutionRatioCancel.Appearance.Options.UseFont = true;
            this.btnDilutionRatioCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnDilutionRatioCancel.Image")));
            this.btnDilutionRatioCancel.Location = new System.Drawing.Point(601, 144);
            this.btnDilutionRatioCancel.Name = "btnDilutionRatioCancel";
            this.btnDilutionRatioCancel.Size = new System.Drawing.Size(98, 48);
            this.btnDilutionRatioCancel.TabIndex = 24;
            this.btnDilutionRatioCancel.Text = "取消";
            this.btnDilutionRatioCancel.Click += new System.EventHandler(this.btnDilutionRatioCancel_Click);
            // 
            // btnAddDilutionRatio
            // 
            this.btnAddDilutionRatio.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDilutionRatio.Appearance.Options.UseFont = true;
            this.btnAddDilutionRatio.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDilutionRatio.Image")));
            this.btnAddDilutionRatio.Location = new System.Drawing.Point(400, 90);
            this.btnAddDilutionRatio.Name = "btnAddDilutionRatio";
            this.btnAddDilutionRatio.Size = new System.Drawing.Size(98, 48);
            this.btnAddDilutionRatio.TabIndex = 23;
            this.btnAddDilutionRatio.Text = "新增";
            this.btnAddDilutionRatio.Click += new System.EventHandler(this.btnAddDilutionRatio_Click);
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(400, 203);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(299, 680);
            this.gridControl2.TabIndex = 22;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.Click += new System.EventHandler(this.gridControl2_Click);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplaceHideCurrentRow;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(400, 37);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 17);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "稀释比例录入：";
            // 
            // DataConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.btnDeleteDilutionRatio);
            this.Controls.Add(this.btnUpdateDilutionRatio);
            this.Controls.Add(this.btnDilutionRatioCancel);
            this.Controls.Add(this.btnAddDilutionRatio);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdateResultUnit);
            this.Controls.Add(this.btnResultUnitCancel);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.lblResultUnitInput);
            this.Name = "DataConfig";
            this.Size = new System.Drawing.Size(1749, 933);
            this.Load += new System.EventHandler(this.DataConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblResultUnitInput;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnUpdateResultUnit;
        private DevExpress.XtraEditors.SimpleButton btnResultUnitCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.SimpleButton btnDeleteDilutionRatio;
        private DevExpress.XtraEditors.SimpleButton btnUpdateDilutionRatio;
        private DevExpress.XtraEditors.SimpleButton btnDilutionRatioCancel;
        private DevExpress.XtraEditors.SimpleButton btnAddDilutionRatio;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
