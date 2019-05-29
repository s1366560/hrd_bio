namespace BioA.UI
{
    partial class ReviewProjectSettings
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
            this.GridReviewProjectControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtBoxSampPos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtBoxCheckProject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBoxSampNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TextDecSamDilutionVol = new System.Windows.Forms.TextBox();
            this.TextDecDilutionVol = new System.Windows.Forms.TextBox();
            this.TextDecOriginalVol = new System.Windows.Forms.TextBox();
            this.TextIncSamDilutionVol = new System.Windows.Forms.TextBox();
            this.TextIncDilutionVol = new System.Windows.Forms.TextBox();
            this.TextIncOriginalVol = new System.Windows.Forms.TextBox();
            this.TextNorSamDilutionVol = new System.Windows.Forms.TextBox();
            this.TextNorDilutionVol = new System.Windows.Forms.TextBox();
            this.TextNorOriginalVol = new System.Windows.Forms.TextBox();
            this.CheBoxDecrement = new System.Windows.Forms.CheckBox();
            this.CheBoxIncrement = new System.Windows.Forms.CheckBox();
            this.CheBoxNormal = new System.Windows.Forms.CheckBox();
            this.IsCheBoxDilution = new System.Windows.Forms.CheckBox();
            this.TxtSampleType = new System.Windows.Forms.TextBox();
            this.LabSampleType = new System.Windows.Forms.Label();
            this.ButConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridReviewProjectControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridReviewProjectControl
            // 
            this.GridReviewProjectControl.Location = new System.Drawing.Point(12, 20);
            this.GridReviewProjectControl.MainView = this.gridView1;
            this.GridReviewProjectControl.Name = "GridReviewProjectControl";
            this.GridReviewProjectControl.Size = new System.Drawing.Size(845, 665);
            this.GridReviewProjectControl.TabIndex = 0;
            this.GridReviewProjectControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.GridReviewProjectControl.Click += new System.EventHandler(this.GridReviewProjectControl_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.GridReviewProjectControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtBoxSampPos);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TxtBoxCheckProject);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtBoxSampNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox1.Location = new System.Drawing.Point(886, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 151);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样本信息";
            // 
            // TxtBoxSampPos
            // 
            this.TxtBoxSampPos.BackColor = System.Drawing.SystemColors.Window;
            this.TxtBoxSampPos.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtBoxSampPos.Location = new System.Drawing.Point(82, 67);
            this.TxtBoxSampPos.Name = "TxtBoxSampPos";
            this.TxtBoxSampPos.ReadOnly = true;
            this.TxtBoxSampPos.Size = new System.Drawing.Size(69, 21);
            this.TxtBoxSampPos.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(23, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "样本位置";
            // 
            // TxtBoxCheckProject
            // 
            this.TxtBoxCheckProject.BackColor = System.Drawing.SystemColors.Window;
            this.TxtBoxCheckProject.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtBoxCheckProject.Location = new System.Drawing.Point(82, 109);
            this.TxtBoxCheckProject.Name = "TxtBoxCheckProject";
            this.TxtBoxCheckProject.ReadOnly = true;
            this.TxtBoxCheckProject.Size = new System.Drawing.Size(112, 21);
            this.TxtBoxCheckProject.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(23, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "检测项目";
            // 
            // TxtBoxSampNumber
            // 
            this.TxtBoxSampNumber.BackColor = System.Drawing.SystemColors.Window;
            this.TxtBoxSampNumber.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtBoxSampNumber.Location = new System.Drawing.Point(82, 22);
            this.TxtBoxSampNumber.Name = "TxtBoxSampNumber";
            this.TxtBoxSampNumber.ReadOnly = true;
            this.TxtBoxSampNumber.Size = new System.Drawing.Size(69, 21);
            this.TxtBoxSampNumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "样本编号";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TextDecSamDilutionVol);
            this.groupBox2.Controls.Add(this.TextDecDilutionVol);
            this.groupBox2.Controls.Add(this.TextDecOriginalVol);
            this.groupBox2.Controls.Add(this.TextIncSamDilutionVol);
            this.groupBox2.Controls.Add(this.TextIncDilutionVol);
            this.groupBox2.Controls.Add(this.TextIncOriginalVol);
            this.groupBox2.Controls.Add(this.TextNorSamDilutionVol);
            this.groupBox2.Controls.Add(this.TextNorDilutionVol);
            this.groupBox2.Controls.Add(this.TextNorOriginalVol);
            this.groupBox2.Controls.Add(this.CheBoxDecrement);
            this.groupBox2.Controls.Add(this.CheBoxIncrement);
            this.groupBox2.Controls.Add(this.CheBoxNormal);
            this.groupBox2.Controls.Add(this.IsCheBoxDilution);
            this.groupBox2.Controls.Add(this.TxtSampleType);
            this.groupBox2.Controls.Add(this.LabSampleType);
            this.groupBox2.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox2.Location = new System.Drawing.Point(886, 258);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 196);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "样本体积";
            // 
            // TextDecSamDilutionVol
            // 
            this.TextDecSamDilutionVol.Location = new System.Drawing.Point(149, 148);
            this.TextDecSamDilutionVol.Name = "TextDecSamDilutionVol";
            this.TextDecSamDilutionVol.ReadOnly = true;
            this.TextDecSamDilutionVol.Size = new System.Drawing.Size(61, 23);
            this.TextDecSamDilutionVol.TabIndex = 20;
            // 
            // TextDecDilutionVol
            // 
            this.TextDecDilutionVol.Location = new System.Drawing.Point(215, 148);
            this.TextDecDilutionVol.Name = "TextDecDilutionVol";
            this.TextDecDilutionVol.ReadOnly = true;
            this.TextDecDilutionVol.Size = new System.Drawing.Size(62, 23);
            this.TextDecDilutionVol.TabIndex = 19;
            // 
            // TextDecOriginalVol
            // 
            this.TextDecOriginalVol.Location = new System.Drawing.Point(82, 148);
            this.TextDecOriginalVol.Name = "TextDecOriginalVol";
            this.TextDecOriginalVol.ReadOnly = true;
            this.TextDecOriginalVol.Size = new System.Drawing.Size(60, 23);
            this.TextDecOriginalVol.TabIndex = 18;
            // 
            // TextIncSamDilutionVol
            // 
            this.TextIncSamDilutionVol.Location = new System.Drawing.Point(149, 110);
            this.TextIncSamDilutionVol.Name = "TextIncSamDilutionVol";
            this.TextIncSamDilutionVol.ReadOnly = true;
            this.TextIncSamDilutionVol.Size = new System.Drawing.Size(61, 23);
            this.TextIncSamDilutionVol.TabIndex = 17;
            // 
            // TextIncDilutionVol
            // 
            this.TextIncDilutionVol.Location = new System.Drawing.Point(215, 110);
            this.TextIncDilutionVol.Name = "TextIncDilutionVol";
            this.TextIncDilutionVol.ReadOnly = true;
            this.TextIncDilutionVol.Size = new System.Drawing.Size(62, 23);
            this.TextIncDilutionVol.TabIndex = 16;
            // 
            // TextIncOriginalVol
            // 
            this.TextIncOriginalVol.Location = new System.Drawing.Point(82, 110);
            this.TextIncOriginalVol.Name = "TextIncOriginalVol";
            this.TextIncOriginalVol.ReadOnly = true;
            this.TextIncOriginalVol.Size = new System.Drawing.Size(60, 23);
            this.TextIncOriginalVol.TabIndex = 15;
            // 
            // TextNorSamDilutionVol
            // 
            this.TextNorSamDilutionVol.Location = new System.Drawing.Point(149, 68);
            this.TextNorSamDilutionVol.Name = "TextNorSamDilutionVol";
            this.TextNorSamDilutionVol.ReadOnly = true;
            this.TextNorSamDilutionVol.Size = new System.Drawing.Size(61, 23);
            this.TextNorSamDilutionVol.TabIndex = 14;
            // 
            // TextNorDilutionVol
            // 
            this.TextNorDilutionVol.Location = new System.Drawing.Point(215, 70);
            this.TextNorDilutionVol.Name = "TextNorDilutionVol";
            this.TextNorDilutionVol.ReadOnly = true;
            this.TextNorDilutionVol.Size = new System.Drawing.Size(62, 23);
            this.TextNorDilutionVol.TabIndex = 13;
            // 
            // TextNorOriginalVol
            // 
            this.TextNorOriginalVol.Location = new System.Drawing.Point(82, 70);
            this.TextNorOriginalVol.Name = "TextNorOriginalVol";
            this.TextNorOriginalVol.ReadOnly = true;
            this.TextNorOriginalVol.Size = new System.Drawing.Size(60, 23);
            this.TextNorOriginalVol.TabIndex = 12;
            // 
            // CheBoxDecrement
            // 
            this.CheBoxDecrement.AutoSize = true;
            this.CheBoxDecrement.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheBoxDecrement.ForeColor = System.Drawing.Color.Black;
            this.CheBoxDecrement.Location = new System.Drawing.Point(22, 152);
            this.CheBoxDecrement.Name = "CheBoxDecrement";
            this.CheBoxDecrement.Size = new System.Drawing.Size(48, 16);
            this.CheBoxDecrement.TabIndex = 11;
            this.CheBoxDecrement.Text = "减量";
            this.CheBoxDecrement.UseVisualStyleBackColor = true;
            this.CheBoxDecrement.Click += new System.EventHandler(this.CheBoxDecrement_Click);
            // 
            // CheBoxIncrement
            // 
            this.CheBoxIncrement.AutoSize = true;
            this.CheBoxIncrement.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheBoxIncrement.ForeColor = System.Drawing.Color.Black;
            this.CheBoxIncrement.Location = new System.Drawing.Point(25, 114);
            this.CheBoxIncrement.Name = "CheBoxIncrement";
            this.CheBoxIncrement.Size = new System.Drawing.Size(48, 16);
            this.CheBoxIncrement.TabIndex = 10;
            this.CheBoxIncrement.Text = "增量";
            this.CheBoxIncrement.UseVisualStyleBackColor = true;
            this.CheBoxIncrement.Click += new System.EventHandler(this.CheBoxIncrement_Click);
            // 
            // CheBoxNormal
            // 
            this.CheBoxNormal.AutoSize = true;
            this.CheBoxNormal.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.CheBoxNormal.Cursor = System.Windows.Forms.Cursors.Default;
            this.CheBoxNormal.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheBoxNormal.ForeColor = System.Drawing.Color.Black;
            this.CheBoxNormal.Location = new System.Drawing.Point(25, 75);
            this.CheBoxNormal.Name = "CheBoxNormal";
            this.CheBoxNormal.Size = new System.Drawing.Size(48, 16);
            this.CheBoxNormal.TabIndex = 9;
            this.CheBoxNormal.Text = "正常";
            this.CheBoxNormal.UseVisualStyleBackColor = true;
            this.CheBoxNormal.Click += new System.EventHandler(this.CheBoxNormal_Click);
            // 
            // IsCheBoxDilution
            // 
            this.IsCheBoxDilution.AutoSize = true;
            this.IsCheBoxDilution.Location = new System.Drawing.Point(170, 30);
            this.IsCheBoxDilution.Name = "IsCheBoxDilution";
            this.IsCheBoxDilution.Size = new System.Drawing.Size(54, 18);
            this.IsCheBoxDilution.TabIndex = 8;
            this.IsCheBoxDilution.Text = "稀释";
            this.IsCheBoxDilution.UseVisualStyleBackColor = true;
            // 
            // TxtSampleType
            // 
            this.TxtSampleType.BackColor = System.Drawing.SystemColors.Window;
            this.TxtSampleType.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtSampleType.Location = new System.Drawing.Point(82, 29);
            this.TxtSampleType.Name = "TxtSampleType";
            this.TxtSampleType.ReadOnly = true;
            this.TxtSampleType.Size = new System.Drawing.Size(69, 21);
            this.TxtSampleType.TabIndex = 7;
            // 
            // LabSampleType
            // 
            this.LabSampleType.AutoSize = true;
            this.LabSampleType.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabSampleType.ForeColor = System.Drawing.Color.Black;
            this.LabSampleType.Location = new System.Drawing.Point(23, 35);
            this.LabSampleType.Name = "LabSampleType";
            this.LabSampleType.Size = new System.Drawing.Size(53, 12);
            this.LabSampleType.TabIndex = 6;
            this.LabSampleType.Text = "样本类型";
            // 
            // ButConfirm
            // 
            this.ButConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.ButConfirm.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButConfirm.Location = new System.Drawing.Point(1067, 476);
            this.ButConfirm.Name = "ButConfirm";
            this.ButConfirm.Size = new System.Drawing.Size(105, 35);
            this.ButConfirm.TabIndex = 3;
            this.ButConfirm.Text = "确 定";
            this.ButConfirm.UseVisualStyleBackColor = false;
            this.ButConfirm.Click += new System.EventHandler(this.ButConfirm_Click);
            // 
            // ReviewProjectSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 704);
            this.Controls.Add(this.ButConfirm);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GridReviewProjectControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReviewProjectSettings";
            this.ShowIcon = false;
            this.Text = "复查项目设定";
            this.Load += new System.EventHandler(this.ReviewProjectSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridReviewProjectControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ButConfirm;
        private DevExpress.XtraGrid.GridControl GridReviewProjectControl;
        private System.Windows.Forms.TextBox TxtBoxSampPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtBoxCheckProject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBoxSampNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtSampleType;
        private System.Windows.Forms.Label LabSampleType;
        private System.Windows.Forms.CheckBox CheBoxDecrement;
        private System.Windows.Forms.CheckBox CheBoxIncrement;
        private System.Windows.Forms.CheckBox IsCheBoxDilution;
        private System.Windows.Forms.TextBox TextDecSamDilutionVol;
        private System.Windows.Forms.TextBox TextDecDilutionVol;
        private System.Windows.Forms.TextBox TextDecOriginalVol;
        private System.Windows.Forms.TextBox TextIncSamDilutionVol;
        private System.Windows.Forms.TextBox TextIncDilutionVol;
        private System.Windows.Forms.TextBox TextIncOriginalVol;
        private System.Windows.Forms.TextBox TextNorSamDilutionVol;
        private System.Windows.Forms.TextBox TextNorDilutionVol;
        private System.Windows.Forms.TextBox TextNorOriginalVol;
        private System.Windows.Forms.CheckBox CheBoxNormal;
    }
}