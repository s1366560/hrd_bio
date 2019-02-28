namespace BioA.UI
{
    partial class ReactionDiskDebug
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
            this.btninitializeRD = new DevExpress.XtraEditors.SimpleButton();
            this.lblResponseDishDebug = new DevExpress.XtraEditors.LabelControl();
            this.btnLightPathAlignment = new DevExpress.XtraEditors.SimpleButton();
            this.btnClockwiseRotationOP = new DevExpress.XtraEditors.SimpleButton();
            this.btnContrarotateOP = new DevExpress.XtraEditors.SimpleButton();
            this.btnLightPathAlignmentCS = new DevExpress.XtraEditors.SimpleButton();
            this.btnInitializeLocationCS = new DevExpress.XtraEditors.SimpleButton();
            this.btnRotateOCP = new DevExpress.XtraEditors.SimpleButton();
            this.btnRotateAR = new DevExpress.XtraEditors.SimpleButton();
            this.lblEightRinseDebug = new DevExpress.XtraEditors.LabelControl();
            this.btninitializeER = new DevExpress.XtraEditors.SimpleButton();
            this.btnToBottom = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveCalibration = new DevExpress.XtraEditors.SimpleButton();
            this.btnprobeTD = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownCalibration = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpCalibration = new DevExpress.XtraEditors.SimpleButton();
            this.btnToTop = new DevExpress.XtraEditors.SimpleButton();
            this.lblResponseTC = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btninitializeRD
            // 
            this.btninitializeRD.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btninitializeRD.Appearance.Options.UseFont = true;
            this.btninitializeRD.Location = new System.Drawing.Point(197, 126);
            this.btninitializeRD.Name = "btninitializeRD";
            this.btninitializeRD.Size = new System.Drawing.Size(150, 50);
            this.btninitializeRD.TabIndex = 30;
            this.btninitializeRD.Text = "初始化";
            this.btninitializeRD.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // lblResponseDishDebug
            // 
            this.lblResponseDishDebug.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponseDishDebug.Appearance.Options.UseFont = true;
            this.lblResponseDishDebug.Location = new System.Drawing.Point(181, 81);
            this.lblResponseDishDebug.Name = "lblResponseDishDebug";
            this.lblResponseDishDebug.Size = new System.Drawing.Size(114, 23);
            this.lblResponseDishDebug.TabIndex = 29;
            this.lblResponseDishDebug.Text = "反应盘调试：";
            // 
            // btnLightPathAlignment
            // 
            this.btnLightPathAlignment.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLightPathAlignment.Appearance.Options.UseFont = true;
            this.btnLightPathAlignment.Location = new System.Drawing.Point(388, 126);
            this.btnLightPathAlignment.Name = "btnLightPathAlignment";
            this.btnLightPathAlignment.Size = new System.Drawing.Size(150, 50);
            this.btnLightPathAlignment.TabIndex = 31;
            this.btnLightPathAlignment.Text = "光路对准";
            this.btnLightPathAlignment.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnClockwiseRotationOP
            // 
            this.btnClockwiseRotationOP.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClockwiseRotationOP.Appearance.Options.UseFont = true;
            this.btnClockwiseRotationOP.Location = new System.Drawing.Point(587, 126);
            this.btnClockwiseRotationOP.Name = "btnClockwiseRotationOP";
            this.btnClockwiseRotationOP.Size = new System.Drawing.Size(150, 50);
            this.btnClockwiseRotationOP.TabIndex = 32;
            this.btnClockwiseRotationOP.Text = "顺时针旋转一脉冲";
            this.btnClockwiseRotationOP.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnContrarotateOP
            // 
            this.btnContrarotateOP.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContrarotateOP.Appearance.Options.UseFont = true;
            this.btnContrarotateOP.Location = new System.Drawing.Point(783, 126);
            this.btnContrarotateOP.Name = "btnContrarotateOP";
            this.btnContrarotateOP.Size = new System.Drawing.Size(150, 50);
            this.btnContrarotateOP.TabIndex = 33;
            this.btnContrarotateOP.Text = "逆时针旋转一脉冲";
            this.btnContrarotateOP.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnLightPathAlignmentCS
            // 
            this.btnLightPathAlignmentCS.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLightPathAlignmentCS.Appearance.Options.UseFont = true;
            this.btnLightPathAlignmentCS.Location = new System.Drawing.Point(783, 200);
            this.btnLightPathAlignmentCS.Name = "btnLightPathAlignmentCS";
            this.btnLightPathAlignmentCS.Size = new System.Drawing.Size(150, 50);
            this.btnLightPathAlignmentCS.TabIndex = 37;
            this.btnLightPathAlignmentCS.Text = "光路对准位置校准保存";
            this.btnLightPathAlignmentCS.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnInitializeLocationCS
            // 
            this.btnInitializeLocationCS.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitializeLocationCS.Appearance.Options.UseFont = true;
            this.btnInitializeLocationCS.Location = new System.Drawing.Point(587, 200);
            this.btnInitializeLocationCS.Name = "btnInitializeLocationCS";
            this.btnInitializeLocationCS.Size = new System.Drawing.Size(150, 50);
            this.btnInitializeLocationCS.TabIndex = 36;
            this.btnInitializeLocationCS.Text = "初始化位置校准保存";
            this.btnInitializeLocationCS.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnRotateOCP
            // 
            this.btnRotateOCP.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateOCP.Appearance.Options.UseFont = true;
            this.btnRotateOCP.Location = new System.Drawing.Point(388, 200);
            this.btnRotateOCP.Name = "btnRotateOCP";
            this.btnRotateOCP.Size = new System.Drawing.Size(150, 50);
            this.btnRotateOCP.TabIndex = 35;
            this.btnRotateOCP.Text = "旋转一比色杯位";
            this.btnRotateOCP.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnRotateAR
            // 
            this.btnRotateAR.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateAR.Appearance.Options.UseFont = true;
            this.btnRotateAR.Location = new System.Drawing.Point(197, 200);
            this.btnRotateAR.Name = "btnRotateAR";
            this.btnRotateAR.Size = new System.Drawing.Size(150, 50);
            this.btnRotateAR.TabIndex = 34;
            this.btnRotateAR.Text = "旋转一周";
            this.btnRotateAR.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // lblEightRinseDebug
            // 
            this.lblEightRinseDebug.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEightRinseDebug.Appearance.Options.UseFont = true;
            this.lblEightRinseDebug.Location = new System.Drawing.Point(181, 310);
            this.lblEightRinseDebug.Name = "lblEightRinseDebug";
            this.lblEightRinseDebug.Size = new System.Drawing.Size(133, 23);
            this.lblEightRinseDebug.TabIndex = 38;
            this.lblEightRinseDebug.Text = "八阶清洗调试：";
            // 
            // btninitializeER
            // 
            this.btninitializeER.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btninitializeER.Appearance.Options.UseFont = true;
            this.btninitializeER.Location = new System.Drawing.Point(197, 358);
            this.btninitializeER.Name = "btninitializeER";
            this.btninitializeER.Size = new System.Drawing.Size(150, 50);
            this.btninitializeER.TabIndex = 39;
            this.btninitializeER.Text = "初始化";
            this.btninitializeER.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnToBottom
            // 
            this.btnToBottom.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToBottom.Appearance.Options.UseFont = true;
            this.btnToBottom.Location = new System.Drawing.Point(388, 358);
            this.btnToBottom.Name = "btnToBottom";
            this.btnToBottom.Size = new System.Drawing.Size(150, 50);
            this.btnToBottom.TabIndex = 40;
            this.btnToBottom.Text = "至底";
            this.btnToBottom.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnSaveCalibration
            // 
            this.btnSaveCalibration.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCalibration.Appearance.Options.UseFont = true;
            this.btnSaveCalibration.Location = new System.Drawing.Point(587, 434);
            this.btnSaveCalibration.Name = "btnSaveCalibration";
            this.btnSaveCalibration.Size = new System.Drawing.Size(150, 50);
            this.btnSaveCalibration.TabIndex = 41;
            this.btnSaveCalibration.Text = "保存校准";
            this.btnSaveCalibration.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnprobeTD
            // 
            this.btnprobeTD.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprobeTD.Appearance.Options.UseFont = true;
            this.btnprobeTD.Location = new System.Drawing.Point(196, 601);
            this.btnprobeTD.Name = "btnprobeTD";
            this.btnprobeTD.Size = new System.Drawing.Size(150, 50);
            this.btnprobeTD.TabIndex = 43;
            this.btnprobeTD.Text = "探头温度显示";
            this.btnprobeTD.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnDownCalibration
            // 
            this.btnDownCalibration.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownCalibration.Appearance.Options.UseFont = true;
            this.btnDownCalibration.Location = new System.Drawing.Point(387, 434);
            this.btnDownCalibration.Name = "btnDownCalibration";
            this.btnDownCalibration.Size = new System.Drawing.Size(150, 50);
            this.btnDownCalibration.TabIndex = 44;
            this.btnDownCalibration.Text = "向下校准";
            this.btnDownCalibration.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnUpCalibration
            // 
            this.btnUpCalibration.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpCalibration.Appearance.Options.UseFont = true;
            this.btnUpCalibration.Location = new System.Drawing.Point(196, 434);
            this.btnUpCalibration.Name = "btnUpCalibration";
            this.btnUpCalibration.Size = new System.Drawing.Size(150, 50);
            this.btnUpCalibration.TabIndex = 45;
            this.btnUpCalibration.Text = "向上校准";
            this.btnUpCalibration.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnToTop
            // 
            this.btnToTop.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToTop.Appearance.Options.UseFont = true;
            this.btnToTop.Location = new System.Drawing.Point(588, 358);
            this.btnToTop.Name = "btnToTop";
            this.btnToTop.Size = new System.Drawing.Size(150, 50);
            this.btnToTop.TabIndex = 46;
            this.btnToTop.Text = "至顶";
            this.btnToTop.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // lblResponseTC
            // 
            this.lblResponseTC.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponseTC.Appearance.Options.UseFont = true;
            this.lblResponseTC.Location = new System.Drawing.Point(181, 547);
            this.lblResponseTC.Name = "lblResponseTC";
            this.lblResponseTC.Size = new System.Drawing.Size(152, 23);
            this.lblResponseTC.TabIndex = 47;
            this.lblResponseTC.Text = "反应盘温度校准：";
            // 
            // ReactionDiskDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblResponseTC);
            this.Controls.Add(this.btnToTop);
            this.Controls.Add(this.btnUpCalibration);
            this.Controls.Add(this.btnDownCalibration);
            this.Controls.Add(this.btnprobeTD);
            this.Controls.Add(this.btnSaveCalibration);
            this.Controls.Add(this.btnToBottom);
            this.Controls.Add(this.btninitializeER);
            this.Controls.Add(this.lblEightRinseDebug);
            this.Controls.Add(this.btnLightPathAlignmentCS);
            this.Controls.Add(this.btnInitializeLocationCS);
            this.Controls.Add(this.btnRotateOCP);
            this.Controls.Add(this.btnRotateAR);
            this.Controls.Add(this.btnContrarotateOP);
            this.Controls.Add(this.btnClockwiseRotationOP);
            this.Controls.Add(this.btnLightPathAlignment);
            this.Controls.Add(this.btninitializeRD);
            this.Controls.Add(this.lblResponseDishDebug);
            this.Name = "ReactionDiskDebug";
            this.Size = new System.Drawing.Size(1091, 711);
            this.Load += new System.EventHandler(this.ReactionDiskDebug_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btninitializeRD;
        private DevExpress.XtraEditors.LabelControl lblResponseDishDebug;
        private DevExpress.XtraEditors.SimpleButton btnLightPathAlignment;
        private DevExpress.XtraEditors.SimpleButton btnClockwiseRotationOP;
        private DevExpress.XtraEditors.SimpleButton btnContrarotateOP;
        private DevExpress.XtraEditors.SimpleButton btnLightPathAlignmentCS;
        private DevExpress.XtraEditors.SimpleButton btnInitializeLocationCS;
        private DevExpress.XtraEditors.SimpleButton btnRotateOCP;
        private DevExpress.XtraEditors.SimpleButton btnRotateAR;
        private DevExpress.XtraEditors.LabelControl lblEightRinseDebug;
        private DevExpress.XtraEditors.SimpleButton btninitializeER;
        private DevExpress.XtraEditors.SimpleButton btnToBottom;
        private DevExpress.XtraEditors.SimpleButton btnSaveCalibration;
        private DevExpress.XtraEditors.SimpleButton btnprobeTD;
        private DevExpress.XtraEditors.SimpleButton btnDownCalibration;
        private DevExpress.XtraEditors.SimpleButton btnUpCalibration;
        private DevExpress.XtraEditors.SimpleButton btnToTop;
        private DevExpress.XtraEditors.LabelControl lblResponseTC;
    }
}
