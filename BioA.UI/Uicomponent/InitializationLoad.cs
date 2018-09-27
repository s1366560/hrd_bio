using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BioA.UI.Uicomponent
{
    public partial class InitializationLoad : UserControl
    {
        public InitializationLoad()
        {
            InitializeComponent();
            
        }

        private void InitializationLoad_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = 0;
            int timeCount = 0;
            bool flag = false;
            while (flag == false)
            {
                //progressBarControl1.Properties.Minimum = 0;
                ////设置一个最大值
                //progressBarControl1.Properties.Maximum = 1000;
                ////设置步长，即每次增加的数
                //progressBarControl1.Properties.Step = 1;

                //显示百分比
                //progressBarControl1.Properties.PercentView = true;
                //是否显示进度数
                //progressBarControl1.Properties.ShowTitle = true;    
                //////设置进度条的样式
                //progressBarControl1.LookAndFeel.SkinName = "Money Twins";
                //progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                //progressBarControl1.Position = count;
                ////处理当前消息队列中的所有windows消息
                //Application.DoEvents();
                progressBar1.Maximum = 200;

                Thread.Sleep(200);

                count =progressBar1.Value + 10;
                
                count = count > 200 ? 20 : count;
                progressBar1.Value = count;
                timeCount++;
                flag = timeCount > 42 ? true : false;
                //执行步长
                //progressBarControl1.PerformStep();

            }
            if (flag == true)
            {
                timer1.Stop();
                Visible = false;
            }
        }
    }
}
