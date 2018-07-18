using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.LookAndFeel;

namespace BioA.UI
{
    public partial class InterfaceLoad : DevExpress.XtraEditors.XtraUserControl
    {
        public InterfaceLoad()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region 进度条
            //设置一个最小值
            progressBarControl1.Properties.Minimum = 0;
            //设置一个最大值
            progressBarControl1.Properties.Maximum = 400;
            //设置步长，即每次增加的数
            progressBarControl1.Properties.Step = 10;
            progressBarControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            progressBarControl1.LookAndFeel.SkinName = "Money Twins";
            ////设置进度条的样式
            //progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            //当前值
            //progressBarControl1.Position = 1;
            //是否显示进度数据
            progressBarControl1.Properties.ShowTitle = true;
            //是否显示百分比
            progressBarControl1.Properties.PercentView = true;
            #endregion
            if (progressBarControl1.Position <= progressBarControl1.Properties.Maximum)
            {
                Application.DoEvents();
                progressBarControl1.PerformStep();
            }
            if (progressBarControl1.Position == progressBarControl1.Properties.Maximum)
            {
                timer1.Stop();
                this.Visible = false;
            }

        }

        private void InterfaceLoad_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => { this.timer1.Start(); }));
        }
    }
}
