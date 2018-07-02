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
using BioA.Common.Machine;

namespace BioA.UI
{
    public partial class SamplePanelDebug : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;
        public SamplePanelDebug()
        {
            InitializeComponent();
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            string strSender = "";
            Subsystem ConfigureInfo = MachineInfo.SubsystemList.Find(str => str.Name == "样本盘调试");
            switch (((SimpleButton)sender).Name)
            {
                case "btnInitialize":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnInitialize.Text).Name;
                    break;
                case "btnContrarotateOP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnContrarotateOP.Text).Name;
                    break;
                case "btnRotateAR":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnRotateAR.Text).Name;
                    break;
                case "btnSampleDishTBCS":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnSampleDishTBCS.Text).Name;
                    break;
                case "btnInitializeSCB":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnInitializeSCB.Text).Name;
                    break;
                case "btnClockwiseRotationOP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnClockwiseRotationOP.Text).Name;
                    break;
                case "btnRotateSS":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnRotateSS.Text).Name;
                    break;
                case "btnBarCodeScanningTCS":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnBarCodeScanningTCS.Text).Name;
                    break;
                case "btnBarCodeIO":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnBarCodeIO.Text).Name;
                    break;
                case "btnBarCodeIC":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleDish1Debug.Text).CommandList.Find(command => command.FullName == btnBarCodeIC.Text).Name;
                    break;
                default:
                    break;
            }

            if (SendNetworkEvent != null && strSender != "")
            {
                SendNetworkEvent(strSender);
            }
        }

        private void SamplePanelDebug_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadSamplePanelDebugInfo));
            
        }
        /// <summary>
        /// 加载样本盘调试信息
        /// </summary>
        private void loadSamplePanelDebugInfo()
        {
            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "样本盘调试")
                {
                    lblSampleDish1Debug.Text = sub.ComponetList[0].Name;
                    btnInitialize.Text = sub.ComponetList[0].CommandList[0].FullName;
                    btnContrarotateOP.Text = sub.ComponetList[0].CommandList[1].FullName;
                    btnRotateAR.Text = sub.ComponetList[0].CommandList[2].FullName;
                    btnSampleDishTBCS.Text = sub.ComponetList[0].CommandList[3].FullName;
                    btnInitializeSCB.Text = sub.ComponetList[0].CommandList[4].FullName;
                    btnClockwiseRotationOP.Text = sub.ComponetList[0].CommandList[5].FullName;
                    btnRotateSS.Text = sub.ComponetList[0].CommandList[6].FullName;
                    btnBarCodeScanningTCS.Text = sub.ComponetList[0].CommandList[7].FullName;
                    btnBarCodeIO.Text = sub.ComponetList[0].CommandList[8].FullName;
                    btnBarCodeIC.Text = sub.ComponetList[0].CommandList[9].FullName;
                }
            }
        }
    }
}
