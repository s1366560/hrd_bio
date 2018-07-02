namespace BioA.UI
{
    partial class SamplePanelDebug
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
            this.btnInitialize = new DevExpress.XtraEditors.SimpleButton();
            this.lblSampleDish1Debug = new DevExpress.XtraEditors.LabelControl();
            this.btnBarCodeScanningTCS = new DevExpress.XtraEditors.SimpleButton();
            this.btnRotateSS = new DevExpress.XtraEditors.SimpleButton();
            this.btnClockwiseRotationOP = new DevExpress.XtraEditors.SimpleButton();
            this.btnInitializeSCB = new DevExpress.XtraEditors.SimpleButton();
            this.btnBarCodeIO = new DevExpress.XtraEditors.SimpleButton();
            this.btnSampleDishTBCS = new DevExpress.XtraEditors.SimpleButton();
            this.btnRotateAR = new DevExpress.XtraEditors.SimpleButton();
            this.btnContrarotateOP = new DevExpress.XtraEditors.SimpleButton();
            this.btnBarCodeIC = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnInitialize
            // 
            this.btnInitialize.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitialize.Appearance.Options.UseFont = true;
            this.btnInitialize.Location = new System.Drawing.Point(166, 257);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(150, 50);
            this.btnInitialize.TabIndex = 34;
            this.btnInitialize.Text = "初始化";
            this.btnInitialize.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // lblSampleDish1Debug
            // 
            this.lblSampleDish1Debug.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleDish1Debug.Appearance.Options.UseFont = true;
            this.lblSampleDish1Debug.Location = new System.Drawing.Point(166, 204);
            this.lblSampleDish1Debug.Name = "lblSampleDish1Debug";
            this.lblSampleDish1Debug.Size = new System.Drawing.Size(114, 23);
            this.lblSampleDish1Debug.TabIndex = 33;
            this.lblSampleDish1Debug.Text = "样本盘调试：";
            // 
            // btnBarCodeScanningTCS
            // 
            this.btnBarCodeScanningTCS.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarCodeScanningTCS.Appearance.Options.UseFont = true;
            this.btnBarCodeScanningTCS.Location = new System.Drawing.Point(811, 347);
            this.btnBarCodeScanningTCS.Name = "btnBarCodeScanningTCS";
            this.btnBarCodeScanningTCS.Size = new System.Drawing.Size(150, 50);
            this.btnBarCodeScanningTCS.TabIndex = 35;
            this.btnBarCodeScanningTCS.Text = "条码扫描位置校准保存";
            this.btnBarCodeScanningTCS.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnRotateSS
            // 
            this.btnRotateSS.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateSS.Appearance.Options.UseFont = true;
            this.btnRotateSS.Location = new System.Drawing.Point(578, 347);
            this.btnRotateSS.Name = "btnRotateSS";
            this.btnRotateSS.Size = new System.Drawing.Size(150, 50);
            this.btnRotateSS.TabIndex = 36;
            this.btnRotateSS.Text = "旋转一步";
            this.btnRotateSS.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnClockwiseRotationOP
            // 
            this.btnClockwiseRotationOP.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClockwiseRotationOP.Appearance.Options.UseFont = true;
            this.btnClockwiseRotationOP.Location = new System.Drawing.Point(360, 347);
            this.btnClockwiseRotationOP.Name = "btnClockwiseRotationOP";
            this.btnClockwiseRotationOP.Size = new System.Drawing.Size(150, 50);
            this.btnClockwiseRotationOP.TabIndex = 37;
            this.btnClockwiseRotationOP.Text = "顺时针旋转一脉冲";
            this.btnClockwiseRotationOP.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnInitializeSCB
            // 
            this.btnInitializeSCB.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitializeSCB.Appearance.Options.UseFont = true;
            this.btnInitializeSCB.Location = new System.Drawing.Point(166, 347);
            this.btnInitializeSCB.Name = "btnInitializeSCB";
            this.btnInitializeSCB.Size = new System.Drawing.Size(150, 50);
            this.btnInitializeSCB.TabIndex = 38;
            this.btnInitializeSCB.Text = "初始化位置校准保存";
            this.btnInitializeSCB.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnBarCodeIO
            // 
            this.btnBarCodeIO.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarCodeIO.Appearance.Options.UseFont = true;
            this.btnBarCodeIO.Location = new System.Drawing.Point(1183, 257);
            this.btnBarCodeIO.Name = "btnBarCodeIO";
            this.btnBarCodeIO.Size = new System.Drawing.Size(150, 50);
            this.btnBarCodeIO.TabIndex = 39;
            this.btnBarCodeIO.Text = "条码器开";
            this.btnBarCodeIO.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnSampleDishTBCS
            // 
            this.btnSampleDishTBCS.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSampleDishTBCS.Appearance.Options.UseFont = true;
            this.btnSampleDishTBCS.Location = new System.Drawing.Point(811, 257);
            this.btnSampleDishTBCS.Name = "btnSampleDishTBCS";
            this.btnSampleDishTBCS.Size = new System.Drawing.Size(150, 50);
            this.btnSampleDishTBCS.TabIndex = 40;
            this.btnSampleDishTBCS.Text = "样本盘至条码扫描位";
            this.btnSampleDishTBCS.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnRotateAR
            // 
            this.btnRotateAR.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateAR.Appearance.Options.UseFont = true;
            this.btnRotateAR.Location = new System.Drawing.Point(578, 257);
            this.btnRotateAR.Name = "btnRotateAR";
            this.btnRotateAR.Size = new System.Drawing.Size(150, 50);
            this.btnRotateAR.TabIndex = 41;
            this.btnRotateAR.Text = "旋转一圈";
            this.btnRotateAR.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnContrarotateOP
            // 
            this.btnContrarotateOP.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContrarotateOP.Appearance.Options.UseFont = true;
            this.btnContrarotateOP.Location = new System.Drawing.Point(360, 257);
            this.btnContrarotateOP.Name = "btnContrarotateOP";
            this.btnContrarotateOP.Size = new System.Drawing.Size(150, 50);
            this.btnContrarotateOP.TabIndex = 42;
            this.btnContrarotateOP.Text = "逆时针旋转一脉冲";
            this.btnContrarotateOP.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnBarCodeIC
            // 
            this.btnBarCodeIC.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarCodeIC.Appearance.Options.UseFont = true;
            this.btnBarCodeIC.Location = new System.Drawing.Point(1183, 347);
            this.btnBarCodeIC.Name = "btnBarCodeIC";
            this.btnBarCodeIC.Size = new System.Drawing.Size(150, 50);
            this.btnBarCodeIC.TabIndex = 43;
            this.btnBarCodeIC.Text = "条码器关";
            this.btnBarCodeIC.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // SamplePanelDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBarCodeIC);
            this.Controls.Add(this.btnContrarotateOP);
            this.Controls.Add(this.btnRotateAR);
            this.Controls.Add(this.btnSampleDishTBCS);
            this.Controls.Add(this.btnBarCodeIO);
            this.Controls.Add(this.btnInitializeSCB);
            this.Controls.Add(this.btnClockwiseRotationOP);
            this.Controls.Add(this.btnRotateSS);
            this.Controls.Add(this.btnBarCodeScanningTCS);
            this.Controls.Add(this.btnInitialize);
            this.Controls.Add(this.lblSampleDish1Debug);
            this.Name = "SamplePanelDebug";
            this.Size = new System.Drawing.Size(1441, 951);
            this.Load += new System.EventHandler(this.SamplePanelDebug_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnInitialize;
        private DevExpress.XtraEditors.LabelControl lblSampleDish1Debug;
        private DevExpress.XtraEditors.SimpleButton btnBarCodeScanningTCS;
        private DevExpress.XtraEditors.SimpleButton btnRotateSS;
        private DevExpress.XtraEditors.SimpleButton btnClockwiseRotationOP;
        private DevExpress.XtraEditors.SimpleButton btnInitializeSCB;
        private DevExpress.XtraEditors.SimpleButton btnBarCodeIO;
        private DevExpress.XtraEditors.SimpleButton btnSampleDishTBCS;
        private DevExpress.XtraEditors.SimpleButton btnRotateAR;
        private DevExpress.XtraEditors.SimpleButton btnContrarotateOP;
        private DevExpress.XtraEditors.SimpleButton btnBarCodeIC;
    }
}
