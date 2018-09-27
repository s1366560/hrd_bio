namespace BioA.UI
{
    partial class DiscreteStatisticsVM
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
            this.SampleNum = new System.Windows.Forms.Label();
            this.SD = new System.Windows.Forms.Label();
            this.Count = new System.Windows.Forms.Label();
            this.Mean = new System.Windows.Forms.Label();
            this.Range = new System.Windows.Forms.Label();
            this.CV = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.sampleNumValue = new System.Windows.Forms.Label();
            this.CountValue = new System.Windows.Forms.Label();
            this.MeanValue = new System.Windows.Forms.Label();
            this.SDValue = new System.Windows.Forms.Label();
            this.CVValue = new System.Windows.Forms.Label();
            this.RangeValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SampleNum
            // 
            this.SampleNum.AutoSize = true;
            this.SampleNum.BackColor = System.Drawing.Color.Transparent;
            this.SampleNum.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SampleNum.ForeColor = System.Drawing.Color.Black;
            this.SampleNum.Location = new System.Drawing.Point(30, 32);
            this.SampleNum.Name = "SampleNum";
            this.SampleNum.Size = new System.Drawing.Size(85, 16);
            this.SampleNum.TabIndex = 2;
            this.SampleNum.Text = "样本编号:";
            // 
            // SD
            // 
            this.SD.AutoSize = true;
            this.SD.BackColor = System.Drawing.Color.Transparent;
            this.SD.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SD.ForeColor = System.Drawing.Color.Black;
            this.SD.Location = new System.Drawing.Point(30, 151);
            this.SD.Name = "SD";
            this.SD.Size = new System.Drawing.Size(83, 19);
            this.SD.TabIndex = 3;
            this.SD.Text = "标准差:";
            // 
            // Count
            // 
            this.Count.AutoSize = true;
            this.Count.BackColor = System.Drawing.Color.Transparent;
            this.Count.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Count.ForeColor = System.Drawing.Color.Black;
            this.Count.Location = new System.Drawing.Point(30, 70);
            this.Count.Name = "Count";
            this.Count.Size = new System.Drawing.Size(83, 19);
            this.Count.TabIndex = 4;
            this.Count.Text = "统计量:";
            // 
            // Mean
            // 
            this.Mean.AutoSize = true;
            this.Mean.BackColor = System.Drawing.Color.Transparent;
            this.Mean.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Mean.ForeColor = System.Drawing.Color.Black;
            this.Mean.Location = new System.Drawing.Point(30, 111);
            this.Mean.Name = "Mean";
            this.Mean.Size = new System.Drawing.Size(83, 19);
            this.Mean.TabIndex = 5;
            this.Mean.Text = "平均值:";
            // 
            // Range
            // 
            this.Range.AutoSize = true;
            this.Range.BackColor = System.Drawing.Color.Transparent;
            this.Range.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Range.ForeColor = System.Drawing.Color.Black;
            this.Range.Location = new System.Drawing.Point(30, 235);
            this.Range.Name = "Range";
            this.Range.Size = new System.Drawing.Size(62, 19);
            this.Range.TabIndex = 6;
            this.Range.Text = "极差:";
            // 
            // CV
            // 
            this.CV.AutoSize = true;
            this.CV.BackColor = System.Drawing.Color.Transparent;
            this.CV.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CV.ForeColor = System.Drawing.Color.Black;
            this.CV.Location = new System.Drawing.Point(30, 193);
            this.CV.Name = "CV";
            this.CV.Size = new System.Drawing.Size(53, 19);
            this.CV.TabIndex = 7;
            this.CV.Text = "CV%:";
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(164, 274);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(93, 34);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "返   回";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // sampleNumValue
            // 
            this.sampleNumValue.AutoSize = true;
            this.sampleNumValue.BackColor = System.Drawing.Color.Transparent;
            this.sampleNumValue.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sampleNumValue.ForeColor = System.Drawing.Color.Black;
            this.sampleNumValue.Location = new System.Drawing.Point(134, 32);
            this.sampleNumValue.Name = "sampleNumValue";
            this.sampleNumValue.Size = new System.Drawing.Size(0, 19);
            this.sampleNumValue.TabIndex = 9;
            // 
            // CountValue
            // 
            this.CountValue.AutoSize = true;
            this.CountValue.BackColor = System.Drawing.Color.Transparent;
            this.CountValue.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CountValue.ForeColor = System.Drawing.Color.Black;
            this.CountValue.Location = new System.Drawing.Point(134, 70);
            this.CountValue.Name = "CountValue";
            this.CountValue.Size = new System.Drawing.Size(0, 19);
            this.CountValue.TabIndex = 10;
            // 
            // MeanValue
            // 
            this.MeanValue.AutoSize = true;
            this.MeanValue.BackColor = System.Drawing.Color.Transparent;
            this.MeanValue.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MeanValue.ForeColor = System.Drawing.Color.Black;
            this.MeanValue.Location = new System.Drawing.Point(134, 111);
            this.MeanValue.Name = "MeanValue";
            this.MeanValue.Size = new System.Drawing.Size(0, 19);
            this.MeanValue.TabIndex = 11;
            // 
            // SDValue
            // 
            this.SDValue.AutoSize = true;
            this.SDValue.BackColor = System.Drawing.Color.Transparent;
            this.SDValue.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SDValue.ForeColor = System.Drawing.Color.Black;
            this.SDValue.Location = new System.Drawing.Point(134, 153);
            this.SDValue.Name = "SDValue";
            this.SDValue.Size = new System.Drawing.Size(0, 19);
            this.SDValue.TabIndex = 12;
            // 
            // CVValue
            // 
            this.CVValue.AutoSize = true;
            this.CVValue.BackColor = System.Drawing.Color.Transparent;
            this.CVValue.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CVValue.ForeColor = System.Drawing.Color.Black;
            this.CVValue.Location = new System.Drawing.Point(134, 194);
            this.CVValue.Name = "CVValue";
            this.CVValue.Size = new System.Drawing.Size(0, 19);
            this.CVValue.TabIndex = 13;
            // 
            // RangeValue
            // 
            this.RangeValue.AutoSize = true;
            this.RangeValue.BackColor = System.Drawing.Color.Transparent;
            this.RangeValue.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RangeValue.ForeColor = System.Drawing.Color.Black;
            this.RangeValue.Location = new System.Drawing.Point(134, 235);
            this.RangeValue.Name = "RangeValue";
            this.RangeValue.Size = new System.Drawing.Size(0, 19);
            this.RangeValue.TabIndex = 14;
            // 
            // DiscreteStatisticsVM
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 320);
            this.Controls.Add(this.RangeValue);
            this.Controls.Add(this.CVValue);
            this.Controls.Add(this.SDValue);
            this.Controls.Add(this.MeanValue);
            this.Controls.Add(this.CountValue);
            this.Controls.Add(this.sampleNumValue);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.CV);
            this.Controls.Add(this.Range);
            this.Controls.Add(this.Mean);
            this.Controls.Add(this.Count);
            this.Controls.Add(this.SD);
            this.Controls.Add(this.SampleNum);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DiscreteStatisticsVM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "离散统计";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SampleNum;
        private System.Windows.Forms.Label SD;
        private System.Windows.Forms.Label Count;
        private System.Windows.Forms.Label Mean;
        private System.Windows.Forms.Label Range;
        private System.Windows.Forms.Label CV;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label sampleNumValue;
        private System.Windows.Forms.Label CountValue;
        private System.Windows.Forms.Label MeanValue;
        private System.Windows.Forms.Label SDValue;
        private System.Windows.Forms.Label CVValue;
        private System.Windows.Forms.Label RangeValue;
    }
}