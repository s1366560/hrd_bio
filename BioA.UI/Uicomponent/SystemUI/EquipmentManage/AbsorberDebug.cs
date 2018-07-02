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
using System.Threading;
using BioA.Common;

namespace BioA.UI
{
    public partial class AbsorberDebug : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;
        public AbsorberDebug()
        {
            InitializeComponent();
        }
        
        private void AbsorberDebug_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(AbsorberDebugInfo));
        }

        private void AbsorberDebugInfo()
        {
            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "吸量器调试")
                {
                    lblSampleArmDebug.Text = sub.ComponetList[0].Name;
                    btnInitializeSMD.Text = sub.ComponetList[0].CommandList[0].FullName;
                    btnWaterChannelTCOR.Text = sub.ComponetList[0].CommandList[1].FullName;
                    btnWaterChannelTCIR.Text = sub.ComponetList[0].CommandList[2].FullName;
                    btnWaterChannelTFC.Text = sub.ComponetList[0].CommandList[3].FullName;
                    btnWaterChannelTDC.Text = sub.ComponetList[0].CommandList[4].FullName;
                    btnDilutionCuvetteTC.Text = sub.ComponetList[0].CommandList[5].FullName;
                    btnSamplingPlaceTC.Text = sub.ComponetList[0].CommandList[6].FullName;
                    btnCuvetteTWC.Text = sub.ComponetList[0].CommandList[7].FullName;
                    btnToBottom.Text = sub.ComponetList[0].CommandList[8].FullName;
                    btnToTop.Text = sub.ComponetList[0].CommandList[9].FullName;

                    lblSampleAM.Text = sub.ComponetList[1].Name;
                    btnInitializeSAM.Text = sub.ComponetList[1].CommandList[0].FullName;
                    btnImbibitionSAM.Text = sub.ComponetList[1].CommandList[1].FullName;
                    btnSpitLiquidSAM.Text = sub.ComponetList[1].CommandList[2].FullName;
                    btnOpenSAM.Text = sub.ComponetList[1].CommandList[3].FullName;
                    btnCloseSAM.Text = sub.ComponetList[1].CommandList[4].FullName;

                    lblSampleArmCalibration.Text = sub.ComponetList[2].Name;
                    btnClockwiseCalibrationSA.Text = sub.ComponetList[2].CommandList[0].FullName;
                    btnAnticlockwiseCalibrationSA.Text = sub.ComponetList[2].CommandList[1].FullName;
                    btnUpCalibrationSA.Text = sub.ComponetList[2].CommandList[2].FullName;
                    btnDownCalibrationSA.Text = sub.ComponetList[2].CommandList[3].FullName;
                    btnSaveCalibrationSA.Text = sub.ComponetList[2].CommandList[4].FullName;

                    lblReagentArmDebug1.Text = sub.ComponetList[3].Name;
                    btnInitializeRAD1.Text = sub.ComponetList[3].CommandList[0].FullName;
                    btnWaterChannelTROR1.Text = sub.ComponetList[3].CommandList[1].FullName;
                    btnWaterChannelTRIT1.Text = sub.ComponetList[3].CommandList[2].FullName;
                    btnReagentTC1.Text = sub.ComponetList[3].CommandList[3].FullName;
                    btnFungistatTIG1.Text = sub.ComponetList[3].CommandList[4].FullName;
                    btnToBottomRAD1.Text = sub.ComponetList[3].CommandList[5].FullName;
                    btnToTopRAD1.Text = sub.ComponetList[3].CommandList[6].FullName;

                    lblReagentArmAM1.Text = sub.ComponetList[4].Name;
                    btnInitializeRAAM1.Text = sub.ComponetList[4].CommandList[0].FullName;
                    btnImbibitionSAM1.Text = sub.ComponetList[4].CommandList[1].FullName;
                    btnSpitLiquidSAM1.Text = sub.ComponetList[4].CommandList[2].FullName;
                    btnOpenSAM1.Text = sub.ComponetList[4].CommandList[3].FullName;
                    btnCloseSAM1.Text = sub.ComponetList[4].CommandList[4].FullName;

                    lblReagentArmCalibration1.Text = sub.ComponetList[5].Name;
                    btnClockwiseCalibrationSA1.Text = sub.ComponetList[5].CommandList[0].FullName;
                    btnAnticlockwiseCalibrationSA1.Text = sub.ComponetList[5].CommandList[1].FullName;
                    btnUpCalibrationSA1.Text = sub.ComponetList[5].CommandList[2].FullName;
                    btnDownCalibrationSA1.Text = sub.ComponetList[5].CommandList[3].FullName;
                    btnSaveCalibrationSA1.Text = sub.ComponetList[5].CommandList[4].FullName;

                    labelControl3.Text = sub.ComponetList[6].Name;
                    btnInitializeRAD2.Text = sub.ComponetList[6].CommandList[0].FullName;
                    btnWaterChannelTROR2.Text = sub.ComponetList[6].CommandList[1].FullName;
                    btnWaterChannelTRIT2.Text = sub.ComponetList[6].CommandList[2].FullName;
                    btnReagentTC2.Text = sub.ComponetList[6].CommandList[3].FullName;
                    btnFungistatTIG2.Text = sub.ComponetList[6].CommandList[4].FullName;
                    btnToBottomRAD2.Text = sub.ComponetList[6].CommandList[5].FullName;
                    btnToTopRAD2.Text = sub.ComponetList[6].CommandList[6].FullName;

                    labelControl2.Text = sub.ComponetList[7].Name;
                    btnInitializeRAAM2.Text = sub.ComponetList[7].CommandList[0].FullName;
                    btnImbibitionSAM2.Text = sub.ComponetList[7].CommandList[1].FullName;
                    btnSpitLiquidSAM2.Text = sub.ComponetList[7].CommandList[2].FullName;
                    btnOpenSAM2.Text = sub.ComponetList[7].CommandList[3].FullName;
                    btnCloseSAM2.Text = sub.ComponetList[7].CommandList[4].FullName;

                    labelControl1.Text = sub.ComponetList[8].Name;
                    btnClockwiseCalibrationSA2.Text = sub.ComponetList[8].CommandList[0].FullName;
                    btnAnticlockwiseCalibrationSA2.Text = sub.ComponetList[8].CommandList[1].FullName;
                    btnUpCalibrationSA2.Text = sub.ComponetList[8].CommandList[2].FullName;
                    btnDownCalibrationSA2.Text = sub.ComponetList[8].CommandList[3].FullName;
                    btnSaveCalibrationSA2.Text = sub.ComponetList[8].CommandList[4].FullName;
                }
            }
        }
        
        private void btnCommand_Click(object sender, EventArgs e)
        {
            
                string strSender = "";
                Subsystem ConfigureInfo = MachineInfo.SubsystemList.Find(str => str.Name == "吸量器调试");
                switch (((SimpleButton)sender).Name)
                {
                    case "btnInitializeSMD":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnInitializeSMD.Text).Name;
                        break;
                    case "btnWaterChannelTCOR":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnWaterChannelTCOR.Text).Name;
                        break;
                    case "btnWaterChannelTCIR":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnWaterChannelTCIR.Text).Name;
                        break;
                    case "btnWaterChannelTFC":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnWaterChannelTFC.Text).Name;
                        break;
                    case "btnWaterChannelTDC":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnWaterChannelTDC.Text).Name;
                        break;
                    case "btnDilutionCuvetteTC":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnDilutionCuvetteTC.Text).Name;
                        break;
                    case "btnSamplingPlaceTC":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnSamplingPlaceTC.Text).Name;
                        break;
                    case "btnCuvetteTWC":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnCuvetteTWC.Text).Name;
                        break;
                    case "btnToBottom":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnToBottom.Text).Name;
                        break;
                    case "btnToTop":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmDebug.Text).CommandList.Find(command => command.FullName == btnToTop.Text).Name;
                        break;
                    case "btnInitializeSAM":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleAM.Text).CommandList.Find(command => command.FullName == btnInitializeSAM.Text).Name;
                        break;
                    case "btnImbibitionSAM":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleAM.Text).CommandList.Find(command => command.FullName == btnImbibitionSAM.Text).Name;
                        break;
                    case "btnSpitLiquidSAM":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleAM.Text).CommandList.Find(command => command.FullName == btnSpitLiquidSAM.Text).Name;
                        break;
                    case "btnOpenSAM":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleAM.Text).CommandList.Find(command => command.FullName == btnOpenSAM.Text).Name;
                        break;
                    case "btnCloseSAM":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleAM.Text).CommandList.Find(command => command.FullName == btnCloseSAM.Text).Name;
                        break;
                    case "btnClockwiseCalibrationSA":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmCalibration.Text).CommandList.Find(command => command.FullName == btnClockwiseCalibrationSA.Text).Name;
                        break;
                    case "btnAnticlockwiseCalibrationSA":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmCalibration.Text).CommandList.Find(command => command.FullName == btnAnticlockwiseCalibrationSA.Text).Name;
                        break;
                    case "btnUpCalibrationSA":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmCalibration.Text).CommandList.Find(command => command.FullName == btnUpCalibrationSA.Text).Name;
                        break;
                    case "btnDownCalibrationSA":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmCalibration.Text).CommandList.Find(command => command.FullName == btnDownCalibrationSA.Text).Name;
                        break;
                    case "btnSaveCalibrationSA":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleArmCalibration.Text).CommandList.Find(command => command.FullName == btnSaveCalibrationSA.Text).Name;
                        break;
                    case "btnInitializeRAD1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmDebug1.Text).CommandList.Find(command => command.FullName == btnInitializeRAD1.Text).Name;
                        break;
                    case "btnWaterChannelTROR1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmDebug1.Text).CommandList.Find(command => command.FullName == btnWaterChannelTROR1.Text).Name;
                        break;
                    case "btnWaterChannelTRIT1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmDebug1.Text).CommandList.Find(command => command.FullName == btnWaterChannelTRIT1.Text).Name;
                        break;
                    case "btnReagentTC1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmDebug1.Text).CommandList.Find(command => command.FullName == btnReagentTC1.Text).Name;
                        break;
                    case "btnFungistatTIG1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmDebug1.Text).CommandList.Find(command => command.FullName == btnFungistatTIG1.Text).Name;
                        break;
                    case "btnToBottomRAD1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmDebug1.Text).CommandList.Find(command => command.FullName == btnToBottomRAD1.Text).Name;
                        break;
                    case "btnToTopRAD1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmDebug1.Text).CommandList.Find(command => command.FullName == btnToTopRAD1.Text).Name;
                        break;
                    case "btnInitializeRAAM1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmAM1.Text).CommandList.Find(command => command.FullName == btnInitializeRAAM1.Text).Name;
                        break;
                    case "btnImbibitionSAM1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmAM1.Text).CommandList.Find(command => command.FullName == btnImbibitionSAM1.Text).Name;
                        break;
                    case "btnSpitLiquidSAM1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmAM1.Text).CommandList.Find(command => command.FullName == btnSpitLiquidSAM1.Text).Name;
                        break;
                    case "btnOpenSAM1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmAM1.Text).CommandList.Find(command => command.FullName == btnOpenSAM1.Text).Name;
                        break;
                    case "btnCloseSAM1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmAM1.Text).CommandList.Find(command => command.FullName == btnCloseSAM1.Text).Name;
                        break;
                    case "btnClockwiseCalibrationSA1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmCalibration1.Text).CommandList.Find(command => command.FullName == btnClockwiseCalibrationSA1.Text).Name;
                        break;
                    case "btnAnticlockwiseCalibrationSA1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmCalibration1.Text).CommandList.Find(command => command.FullName == btnAnticlockwiseCalibrationSA1.Text).Name;
                        break;
                    case "btnUpCalibrationSA1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmCalibration1.Text).CommandList.Find(command => command.FullName == btnUpCalibrationSA1.Text).Name;
                        break;
                    case "btnDownCalibrationSA1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmCalibration1.Text).CommandList.Find(command => command.FullName == btnDownCalibrationSA1.Text).Name;
                        break;
                    case "btnSaveCalibrationSA1":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentArmCalibration1.Text).CommandList.Find(command => command.FullName == btnSaveCalibrationSA1.Text).Name;
                        break;
                    case "btnInitializeRAD2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl3.Text).CommandList.Find(command => command.FullName == btnInitializeRAD2.Text).Name;
                        break;
                    case "btnWaterChannelTROR2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl3.Text).CommandList.Find(command => command.FullName == btnWaterChannelTROR2.Text).Name;
                        break;
                    case "btnWaterChannelTRIT2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl3.Text).CommandList.Find(command => command.FullName == btnWaterChannelTRIT2.Text).Name;
                        break;
                    case "btnReagentTC2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl3.Text).CommandList.Find(command => command.FullName == btnReagentTC2.Text).Name;
                        break;
                    case "btnFungistatTIG2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl3.Text).CommandList.Find(command => command.FullName == btnFungistatTIG2.Text).Name;
                        break;
                    case "btnToBottomRAD2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl3.Text).CommandList.Find(command => command.FullName == btnToBottomRAD2.Text).Name;
                        break;
                    case "btnToTopRAD2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl3.Text).CommandList.Find(command => command.FullName == btnToTopRAD2.Text).Name;
                        break;
                    case "btnInitializeRAAM2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl2.Text).CommandList.Find(command => command.FullName == btnInitializeRAAM2.Text).Name;
                        break;
                    case "btnImbibitionSAM2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl2.Text).CommandList.Find(command => command.FullName == btnImbibitionSAM2.Text).Name;
                        break;
                    case "btnSpitLiquidSAM2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl2.Text).CommandList.Find(command => command.FullName == btnSpitLiquidSAM2.Text).Name;
                        break;
                    case "btnOpenSAM2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl2.Text).CommandList.Find(command => command.FullName == btnOpenSAM2.Text).Name;
                        break;
                    case "btnCloseSAM2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl2.Text).CommandList.Find(command => command.FullName == btnCloseSAM2.Text).Name;
                        break;
                    case "btnClockwiseCalibrationSA2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl1.Text).CommandList.Find(command => command.FullName == btnClockwiseCalibrationSA2.Text).Name;
                        break;
                    case "btnAnticlockwiseCalibrationSA2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl1.Text).CommandList.Find(command => command.FullName == btnAnticlockwiseCalibrationSA2.Text).Name;
                        break;
                    case "btnUpCalibrationSA2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl1.Text).CommandList.Find(command => command.FullName == btnUpCalibrationSA2.Text).Name;
                        break;
                    case "btnDownCalibrationSA2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl1.Text).CommandList.Find(command => command.FullName == btnDownCalibrationSA2.Text).Name;
                        break;
                    case "btnSaveCalibrationSA2":
                        strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl1.Text).CommandList.Find(command => command.FullName == btnSaveCalibrationSA2.Text).Name;
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
