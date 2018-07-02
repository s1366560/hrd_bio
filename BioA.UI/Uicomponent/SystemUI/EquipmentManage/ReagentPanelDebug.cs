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
    public partial class ReagentPanelDebug : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;
        public ReagentPanelDebug()
        {
            InitializeComponent();
        }

        private void ReagentPanelDebug_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadReagentPanelDebugInfo));
            
        }
        /// <summary>
        /// 加载试剂盘调试信息
        /// </summary>
        private void loadReagentPanelDebugInfo()
        {
            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "试剂盘调试")
                {
                    lblReagentDish1Debug.Text = sub.ComponetList[0].Name;
                    btnClockwiseRotationOP1.Text = sub.ComponetList[0].CommandList[0].FullName;
                    btnContrarotateOP1.Text = sub.ComponetList[0].CommandList[1].FullName;
                    btnRotateAR1.Text = sub.ComponetList[0].CommandList[2].FullName;
                    btnRotateSS1.Text = sub.ComponetList[0].CommandList[3].FullName;
                    btnOuterRingRestoration1.Text = sub.ComponetList[0].CommandList[4].FullName;
                    btnOuterRingRestorationCS1.Text = sub.ComponetList[0].CommandList[5].FullName;
                    btnInsideTrackRestoration1.Text = sub.ComponetList[0].CommandList[6].FullName;
                    btnInsideTrackRestorationCS1.Text = sub.ComponetList[0].CommandList[7].FullName;
                    btnReagentDishTBCS1.Text = sub.ComponetList[0].CommandList[8].FullName;
                    btnBarCodeScanningCS1.Text = sub.ComponetList[0].CommandList[9].FullName;
                    btnBarCodeIO1.Text = sub.ComponetList[0].CommandList[10].FullName;
                    btnBarCodeIC1.Text = sub.ComponetList[0].CommandList[11].FullName;

                    lblReagentDish2Debug.Text = sub.ComponetList[1].Name;
                    btnClockwiseRotationOP2.Text = sub.ComponetList[1].CommandList[0].FullName;
                    btnContrarotateOP2.Text = sub.ComponetList[1].CommandList[1].FullName;
                    btnRotateAR2.Text = sub.ComponetList[1].CommandList[2].FullName;
                    btnRotateSS2.Text = sub.ComponetList[1].CommandList[3].FullName;
                    btnOuterRingRestoration2.Text = sub.ComponetList[1].CommandList[4].FullName;
                    btnOuterRingRestorationCS2.Text = sub.ComponetList[1].CommandList[5].FullName;
                    btnInsideTrackRestoration2.Text = sub.ComponetList[1].CommandList[6].FullName;
                    btnInsideTrackRestorationCS2.Text = sub.ComponetList[1].CommandList[7].FullName;
                    btnReagentDishTBCS2.Text = sub.ComponetList[1].CommandList[8].FullName;
                    btnBarCodeScanningCS2.Text = sub.ComponetList[1].CommandList[9].FullName;
                    btnBarCodeIO2.Text = sub.ComponetList[1].CommandList[10].FullName;
                    btnBarCodeIC2.Text = sub.ComponetList[1].CommandList[11].FullName;
                }
            }
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            string strSender = "";
            Subsystem ConfigureInfo = MachineInfo.SubsystemList.Find(str => str.Name == "试剂盘调试");
            switch (((SimpleButton)sender).Name)
            {
                case "btnClockwiseRotationOP1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnClockwiseRotationOP1.Text).Name;
                    break;
                case "btnContrarotateOP1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnContrarotateOP1.Text).Name;
                    break;
                case "btnRotateAR1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnRotateAR1.Text).Name;
                    break;
                case "btnRotateSS1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnRotateSS1.Text).Name;
                    break;
                case "btnOuterRingRestoration1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnOuterRingRestoration1.Text).Name;
                    break;
                case "btnOuterRingRestorationCS1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnOuterRingRestorationCS1.Text).Name;
                    break;
                case "btnInsideTrackRestoration1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnInsideTrackRestoration1.Text).Name;
                    break;
                case "btnInsideTrackRestorationCS1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnInsideTrackRestorationCS1.Text).Name;
                    break;
                case "btnReagentDishTBCS1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnReagentDishTBCS1.Text).Name;
                    break;
                case "btnBarCodeScanningCS1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnBarCodeScanningCS1.Text).Name;
                    break;
                case "btnBarCodeIO1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnBarCodeIO1.Text).Name;
                    break;
                case "btnBarCodeIC1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish1Debug.Text).CommandList.Find(command => command.FullName == btnBarCodeIC1.Text).Name;
                    break;
                case "btnClockwiseRotationOP2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnClockwiseRotationOP2.Text).Name;
                    break;
                case "btnContrarotateOP2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnContrarotateOP2.Text).Name;
                    break;
                case "btnRotateAR2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnRotateAR2.Text).Name;
                    break;
                case "btnRotateSS2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnRotateSS2.Text).Name;
                    break;
                case "btnOuterRingRestoration2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnOuterRingRestoration2.Text).Name;
                    break;
                case "btnOuterRingRestorationCS2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnOuterRingRestorationCS2.Text).Name;
                    break;
                case "btnInsideTrackRestoration2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnInsideTrackRestoration2.Text).Name;
                    break;
                case "btnInsideTrackRestorationCS2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnInsideTrackRestorationCS2.Text).Name;
                    break;
                case "btnReagentDishTBCS2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnReagentDishTBCS2.Text).Name;
                    break;
                case "btnBarCodeScanningCS2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnBarCodeScanningCS2.Text).Name;
                    break;
                case "btnBarCodeIO2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnBarCodeIO2.Text).Name;
                    break;
                case "btnBarCodeIC2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentDish2Debug.Text).CommandList.Find(command => command.FullName == btnBarCodeIC2.Text).Name;
                    break;
                default:
                    break;
            }

            if (SendNetworkEvent != null && strSender != "")
            {
                SendNetworkEvent(strSender);
            }
        }

    }
}
