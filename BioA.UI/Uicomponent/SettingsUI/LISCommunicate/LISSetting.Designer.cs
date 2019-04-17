namespace BioA.UI
{
    partial class LISSetting
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
            this.grpSerialPortSetting = new DevExpress.XtraEditors.GroupControl();
            this.cboStopBits = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboBaudRate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblStopBit = new DevExpress.XtraEditors.LabelControl();
            this.lblBaud = new DevExpress.XtraEditors.LabelControl();
            this.cboParity = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboDataBit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboSerialPort = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblParityBit = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblCom = new DevExpress.XtraEditors.LabelControl();
            this.grpNetworkSettings = new DevExpress.XtraEditors.GroupControl();
            this.txtPort = new DevExpress.XtraEditors.TextEdit();
            this.txtServerIP = new DevExpress.XtraEditors.TextEdit();
            this.lblPort = new DevExpress.XtraEditors.LabelControl();
            this.lblServerIP = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCommOverTime = new System.Windows.Forms.TextBox();
            this.checkBoxSampResult = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comBoxCommDirection = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.comBoxCommMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grpSerialPortSetting)).BeginInit();
            this.grpSerialPortSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStopBits.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBaudRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboParity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataBit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSerialPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpNetworkSettings)).BeginInit();
            this.grpNetworkSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comBoxCommDirection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comBoxCommMode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSerialPortSetting
            // 
            this.grpSerialPortSetting.Controls.Add(this.cboStopBits);
            this.grpSerialPortSetting.Controls.Add(this.cboBaudRate);
            this.grpSerialPortSetting.Controls.Add(this.lblStopBit);
            this.grpSerialPortSetting.Controls.Add(this.lblBaud);
            this.grpSerialPortSetting.Controls.Add(this.cboParity);
            this.grpSerialPortSetting.Controls.Add(this.cboDataBit);
            this.grpSerialPortSetting.Controls.Add(this.cboSerialPort);
            this.grpSerialPortSetting.Controls.Add(this.lblParityBit);
            this.grpSerialPortSetting.Controls.Add(this.labelControl1);
            this.grpSerialPortSetting.Controls.Add(this.lblCom);
            this.grpSerialPortSetting.Location = new System.Drawing.Point(457, 32);
            this.grpSerialPortSetting.Name = "grpSerialPortSetting";
            this.grpSerialPortSetting.Size = new System.Drawing.Size(200, 236);
            this.grpSerialPortSetting.TabIndex = 8;
            this.grpSerialPortSetting.Text = "串口设置";
            // 
            // cboStopBits
            // 
            this.cboStopBits.Location = new System.Drawing.Point(80, 147);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStopBits.Properties.Appearance.Options.UseFont = true;
            this.cboStopBits.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStopBits.Properties.Items.AddRange(new object[] {
            "无",
            "1",
            "1.5",
            "2"});
            this.cboStopBits.Size = new System.Drawing.Size(100, 24);
            this.cboStopBits.TabIndex = 37;
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.Location = new System.Drawing.Point(80, 73);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBaudRate.Properties.Appearance.Options.UseFont = true;
            this.cboBaudRate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboBaudRate.Properties.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200"});
            this.cboBaudRate.Size = new System.Drawing.Size(100, 24);
            this.cboBaudRate.TabIndex = 36;
            // 
            // lblStopBit
            // 
            this.lblStopBit.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStopBit.Appearance.Options.UseFont = true;
            this.lblStopBit.Location = new System.Drawing.Point(15, 150);
            this.lblStopBit.Name = "lblStopBit";
            this.lblStopBit.Size = new System.Drawing.Size(56, 17);
            this.lblStopBit.TabIndex = 35;
            this.lblStopBit.Text = "停止位：";
            // 
            // lblBaud
            // 
            this.lblBaud.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaud.Appearance.Options.UseFont = true;
            this.lblBaud.Location = new System.Drawing.Point(15, 73);
            this.lblBaud.Name = "lblBaud";
            this.lblBaud.Size = new System.Drawing.Size(56, 17);
            this.lblBaud.TabIndex = 34;
            this.lblBaud.Text = "波特率：";
            // 
            // cboParity
            // 
            this.cboParity.Location = new System.Drawing.Point(80, 186);
            this.cboParity.Name = "cboParity";
            this.cboParity.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboParity.Properties.Appearance.Options.UseFont = true;
            this.cboParity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboParity.Properties.Items.AddRange(new object[] {
            "None",
            "Old",
            "Even",
            "Mark",
            "Space"});
            this.cboParity.Size = new System.Drawing.Size(100, 24);
            this.cboParity.TabIndex = 33;
            // 
            // cboDataBit
            // 
            this.cboDataBit.Location = new System.Drawing.Point(80, 111);
            this.cboDataBit.Name = "cboDataBit";
            this.cboDataBit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDataBit.Properties.Appearance.Options.UseFont = true;
            this.cboDataBit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDataBit.Properties.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cboDataBit.Size = new System.Drawing.Size(100, 24);
            this.cboDataBit.TabIndex = 32;
            // 
            // cboSerialPort
            // 
            this.cboSerialPort.Location = new System.Drawing.Point(80, 31);
            this.cboSerialPort.Name = "cboSerialPort";
            this.cboSerialPort.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSerialPort.Properties.Appearance.Options.UseFont = true;
            this.cboSerialPort.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSerialPort.Size = new System.Drawing.Size(100, 24);
            this.cboSerialPort.TabIndex = 31;
            // 
            // lblParityBit
            // 
            this.lblParityBit.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParityBit.Appearance.Options.UseFont = true;
            this.lblParityBit.Location = new System.Drawing.Point(15, 189);
            this.lblParityBit.Name = "lblParityBit";
            this.lblParityBit.Size = new System.Drawing.Size(61, 17);
            this.lblParityBit.TabIndex = 30;
            this.lblParityBit.Text = "奇偶校验:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(15, 114);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 17);
            this.labelControl1.TabIndex = 29;
            this.labelControl1.Text = "数据位：";
            // 
            // lblCom
            // 
            this.lblCom.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCom.Appearance.Options.UseFont = true;
            this.lblCom.Location = new System.Drawing.Point(15, 33);
            this.lblCom.Name = "lblCom";
            this.lblCom.Size = new System.Drawing.Size(42, 17);
            this.lblCom.TabIndex = 28;
            this.lblCom.Text = "串口：";
            // 
            // grpNetworkSettings
            // 
            this.grpNetworkSettings.Controls.Add(this.txtPort);
            this.grpNetworkSettings.Controls.Add(this.txtServerIP);
            this.grpNetworkSettings.Controls.Add(this.lblPort);
            this.grpNetworkSettings.Controls.Add(this.lblServerIP);
            this.grpNetworkSettings.Location = new System.Drawing.Point(17, 32);
            this.grpNetworkSettings.Name = "grpNetworkSettings";
            this.grpNetworkSettings.Size = new System.Drawing.Size(200, 236);
            this.grpNetworkSettings.TabIndex = 9;
            this.grpNetworkSettings.Text = "网络设置";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(30, 160);
            this.txtPort.Name = "txtPort";
            this.txtPort.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Properties.Appearance.Options.UseFont = true;
            this.txtPort.Size = new System.Drawing.Size(110, 24);
            this.txtPort.TabIndex = 14;
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(30, 66);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerIP.Properties.Appearance.Options.UseFont = true;
            this.txtServerIP.Size = new System.Drawing.Size(165, 24);
            this.txtServerIP.TabIndex = 13;
            // 
            // lblPort
            // 
            this.lblPort.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Appearance.Options.UseFont = true;
            this.lblPort.Location = new System.Drawing.Point(17, 133);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(42, 17);
            this.lblPort.TabIndex = 12;
            this.lblPort.Text = "端口：";
            // 
            // lblServerIP
            // 
            this.lblServerIP.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerIP.Appearance.Options.UseFont = true;
            this.lblServerIP.Location = new System.Drawing.Point(17, 39);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(87, 17);
            this.lblServerIP.TabIndex = 11;
            this.lblServerIP.Text = "LIS服务器IP：";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.txtCommOverTime);
            this.groupControl1.Controls.Add(this.checkBoxSampResult);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.comBoxCommDirection);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.comBoxCommMode);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Location = new System.Drawing.Point(237, 32);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 236);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "常规设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(138, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "秒";
            // 
            // txtCommOverTime
            // 
            this.txtCommOverTime.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommOverTime.Location = new System.Drawing.Point(85, 118);
            this.txtCommOverTime.Name = "txtCommOverTime";
            this.txtCommOverTime.Size = new System.Drawing.Size(54, 24);
            this.txtCommOverTime.TabIndex = 8;
            // 
            // checkBoxSampResult
            // 
            this.checkBoxSampResult.AutoSize = true;
            this.checkBoxSampResult.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSampResult.Location = new System.Drawing.Point(18, 157);
            this.checkBoxSampResult.Name = "checkBoxSampResult";
            this.checkBoxSampResult.Size = new System.Drawing.Size(167, 21);
            this.checkBoxSampResult.TabIndex = 7;
            this.checkBoxSampResult.Text = "实时发送样本测试结果";
            this.checkBoxSampResult.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "通讯超时:";
            // 
            // comBoxCommDirection
            // 
            this.comBoxCommDirection.Location = new System.Drawing.Point(86, 74);
            this.comBoxCommDirection.Name = "comBoxCommDirection";
            this.comBoxCommDirection.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comBoxCommDirection.Properties.Appearance.Options.UseFont = true;
            this.comBoxCommDirection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comBoxCommDirection.Properties.Items.AddRange(new object[] {
            "单向",
            "双向"});
            this.comBoxCommDirection.Size = new System.Drawing.Size(88, 24);
            this.comBoxCommDirection.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "通讯模式:";
            // 
            // comBoxCommMode
            // 
            this.comBoxCommMode.Location = new System.Drawing.Point(86, 31);
            this.comBoxCommMode.Name = "comBoxCommMode";
            this.comBoxCommMode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comBoxCommMode.Properties.Appearance.Options.UseFont = true;
            this.comBoxCommMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comBoxCommMode.Properties.Items.AddRange(new object[] {
            "网络",
            "串口"});
            this.comBoxCommMode.Size = new System.Drawing.Size(88, 24);
            this.comBoxCommMode.TabIndex = 1;
            this.comBoxCommMode.SelectedValueChanged += new System.EventHandler(this.comBoxCommMode_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "通讯方式:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(495, 295);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 29);
            this.button2.TabIndex = 6;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(576, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 5;
            this.button1.Text = "应用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LISSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(676, 350);
            this.Controls.Add(this.grpSerialPortSetting);
            this.Controls.Add(this.grpNetworkSettings);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LISSetting";
            this.Text = "LIS设置";
            //this.Load += new System.EventHandler(this.LISSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpSerialPortSetting)).EndInit();
            this.grpSerialPortSetting.ResumeLayout(false);
            this.grpSerialPortSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStopBits.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBaudRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboParity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataBit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSerialPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpNetworkSettings)).EndInit();
            this.grpNetworkSettings.ResumeLayout(false);
            this.grpNetworkSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comBoxCommDirection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comBoxCommMode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpSerialPortSetting;
        private DevExpress.XtraEditors.ComboBoxEdit cboStopBits;
        private DevExpress.XtraEditors.ComboBoxEdit cboBaudRate;
        private DevExpress.XtraEditors.LabelControl lblStopBit;
        private DevExpress.XtraEditors.LabelControl lblBaud;
        private DevExpress.XtraEditors.ComboBoxEdit cboParity;
        private DevExpress.XtraEditors.ComboBoxEdit cboDataBit;
        private DevExpress.XtraEditors.ComboBoxEdit cboSerialPort;
        private DevExpress.XtraEditors.LabelControl lblParityBit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblCom;
        private DevExpress.XtraEditors.GroupControl grpNetworkSettings;
        private DevExpress.XtraEditors.TextEdit txtPort;
        private DevExpress.XtraEditors.TextEdit txtServerIP;
        private DevExpress.XtraEditors.LabelControl lblPort;
        private DevExpress.XtraEditors.LabelControl lblServerIP;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCommOverTime;
        private System.Windows.Forms.CheckBox checkBoxSampResult;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit comBoxCommMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.ComboBoxEdit comBoxCommDirection;
        private System.Windows.Forms.Label label3;
    }
}