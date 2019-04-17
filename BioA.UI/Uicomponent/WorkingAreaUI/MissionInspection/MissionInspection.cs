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
using BioA.Common;

namespace BioA.UI
{
    public partial class MissionInspection : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate bool getopid();
        public event getopid GetOpidEvent;

        public delegate void ScanBarcodePost(ScanBarcodePosInfo s);
        public event ScanBarcodePost ScanBarcodePostEvent;

        public delegate void SMPBarcodeSignal();
        public event SMPBarcodeSignal SMPBarcodeSignalEvent;

        SampleDisk sampleDisk = new SampleDisk();
        public MissionInspection()
        {
            InitializeComponent();
            sampleDisk.getOPID += GetOPIDEvent;
            sampleDisk.ScanBarcodePostEvent += ScanBarcodePostEvent_Event;
            sampleDisk.SMPBarcodeSignalEvent += SMPBarcodeSignalEvent_Event;
            xtraTabPage1.Controls.Add(sampleDisk);
        }
        /// <summary>
        /// 样本盘界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MissionInspection_Load(object sender, EventArgs e)
        {
            sampleDisk.SampleDisk_Load(null,null);
        }
        /// <summary>
        /// 获取机器状态
        /// </summary>
        /// <returns></returns>
        private bool GetOPIDEvent()
        {
            return GetOpidEvent();
        }
        /// <summary>
        /// 发送扫码样本仓要的位置和盘号委托事件
        /// </summary>
        /// <param name="s"></param>
        public void ScanBarcodePostEvent_Event(ScanBarcodePosInfo s)
        {
            this.ScanBarcodePostEvent(s);
        }
        /// <summary>
        /// 发送样本扫码命令启动线程信号委托事件
        /// </summary>
        public void SMPBarcodeSignalEvent_Event()
        {
            this.SMPBarcodeSignalEvent();
        }
    }
}
