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

namespace BioA.UI
{
    public partial class MissionInspection : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate bool getopid();
        public event getopid GetOpidEvent;

        public MissionInspection()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 样本盘界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MissionInspection_Load(object sender, EventArgs e)
        {
            SampleDisk sampleDisk = new SampleDisk();
            sampleDisk.getOPID += GetOPIDEvent;
            xtraTabPage1.Controls.Add(sampleDisk);
        }
        /// <summary>
        /// 获取机器状态
        /// </summary>
        /// <returns></returns>
        private bool GetOPIDEvent()
        {
            return GetOpidEvent();
        }
    }
}
