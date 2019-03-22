namespace BioA.UI
{
    partial class ConfigurationScript
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
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::BioA.UI.Uicomponent.SystemUI.Configure.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(769, 410);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "运 行 脚 本";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConfigurationScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Name = "ConfigurationScript";
            this.Size = new System.Drawing.Size(1717, 906);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;

    }
}
