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
    public partial class LiquidRoadDebug : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;
        public LiquidRoadDebug()
        {
            InitializeComponent();
        }

        private void LiquidRoadDebug_Load(object sender, EventArgs e)
        {
            //异步方法
            BeginInvoke(new Action(loadLiquidRoadDebugInfo));
            
        }
        /// <summary>
        /// 加载液路调试信息
        /// </summary>
        private void loadLiquidRoadDebugInfo()
        {
            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "液路调试")
                {
                    lblSampleNeedle.Text = sub.ComponetList[0].Name;
                    btnCheckSamProWV.Text = sub.ComponetList[0].CommandList[0].FullName;

                    lblReagentNeedle.Text = sub.ComponetList[1].Name;
                    btnDetectionReagentNeedle1.Text = sub.ComponetList[1].CommandList[0].FullName;
                    btnDetectionReagentNeedle2.Text = sub.ComponetList[1].CommandList[1].FullName;

                    lblEightJointsRinse.Text = sub.ComponetList[2].Name;
                    btnDetectionIV.Text = sub.ComponetList[2].CommandList[0].FullName;
                    btnDetectionSV.Text = sub.ComponetList[2].CommandList[1].FullName;

                    lblStirrer.Text = sub.ComponetList[3].Name;
                    btnStirrer1DFV.Text = sub.ComponetList[3].CommandList[0].FullName;
                    btnStirrer2DFV.Text = sub.ComponetList[3].CommandList[1].FullName;

                    lblLiquidWay.Text = sub.ComponetList[4].Name;
                    btnChangingWater.Text = sub.ComponetList[4].CommandList[0].FullName;
                    btnVacuumPumpVP3LLM.Text = sub.ComponetList[4].CommandList[1].FullName;
                    btnConstantTempLLM.Text = sub.ComponetList[4].CommandList[0].FullName;
                    btnPureWaterTankLLM.Text = sub.ComponetList[4].CommandList[1].FullName;
                    btnOverflowTankLLM.Text = sub.ComponetList[4].CommandList[0].FullName;

                    labelControl1.Text = sub.ComponetList[5].Name;
                    btnEquipmentStatusIE.Text = sub.ComponetList[5].CommandList[0].FullName;

                    lblGearPumpGP1.Text = sub.ComponetList[6].Name;
                    btnOpenGP.Text = sub.ComponetList[6].CommandList[0].FullName;
                    btnCloseGP.Text = sub.ComponetList[6].CommandList[1].FullName;

                    lblNegativePressurePumpVP2.Text = sub.ComponetList[7].Name;
                    btnOpenNPP.Text = sub.ComponetList[7].CommandList[0].FullName;
                    btnCloseNPP.Text = sub.ComponetList[7].CommandList[1].FullName;

                    lblVacuumPumpVP1.Text = sub.ComponetList[8].Name;
                    btnOpenVP.Text = sub.ComponetList[8].CommandList[0].FullName;
                    btnCloseVP.Text = sub.ComponetList[8].CommandList[1].FullName;
                }
            }
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            string strSender = "";
            Subsystem ConfigureInfo = MachineInfo.SubsystemList.Find(str => str.Name == "液路调试");
            switch (((SimpleButton)sender).Name)
            {
                case "btnCheckSamProWV":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblSampleNeedle.Text).CommandList.Find(command => command.FullName == btnCheckSamProWV.Text).Name;
                    break;
                case "btnDetectionReagentNeedle1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentNeedle.Text).CommandList.Find(command => command.FullName == btnDetectionReagentNeedle1.Text).Name;
                    break;
                case "btnDetectionReagentNeedle2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblReagentNeedle.Text).CommandList.Find(command => command.FullName == btnDetectionReagentNeedle2.Text).Name;
                    break;
                case "btnDetectionIV":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblEightJointsRinse.Text).CommandList.Find(command => command.FullName == btnDetectionIV.Text).Name;
                    break;
                case "btnDetectionSV":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblEightJointsRinse.Text).CommandList.Find(command => command.FullName == btnDetectionSV.Text).Name;
                    break;
                case "btnStirrer1DFV":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer.Text).CommandList.Find(command => command.FullName == btnStirrer1DFV.Text).Name;
                    break;
                case "btnStirrer2DFV":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer.Text).CommandList.Find(command => command.FullName == btnStirrer2DFV.Text).Name;
                    break;
                case "btnChangingWater":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblLiquidWay.Text).CommandList.Find(command => command.FullName == btnChangingWater.Text).Name;
                    break;
                case "btnVacuumPumpVP3LLM":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblLiquidWay.Text).CommandList.Find(command => command.FullName == btnVacuumPumpVP3LLM.Text).Name;
                    break;
                case "btnConstantTempLLM":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblLiquidWay.Text).CommandList.Find(command => command.FullName == btnConstantTempLLM.Text).Name;
                    break;
                case "btnPureWaterTankLLM":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblLiquidWay.Text).CommandList.Find(command => command.FullName == btnPureWaterTankLLM.Text).Name;
                    break;
                case "btnOverflowTankLLM":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblLiquidWay.Text).CommandList.Find(command => command.FullName == btnOverflowTankLLM.Text).Name;
                    break;
                case "btnEquipmentStatusIE":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == labelControl1.Text).CommandList.Find(command => command.FullName == btnEquipmentStatusIE.Text).Name;
                    break;
                case "btnOpenGP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblGearPumpGP1.Text).CommandList.Find(command => command.FullName == btnOpenGP.Text).Name;
                    break;
                case "btnCloseGP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblGearPumpGP1.Text).CommandList.Find(command => command.FullName == btnCloseGP.Text).Name;
                    break;
                case "btnOpenNPP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblNegativePressurePumpVP2.Text).CommandList.Find(command => command.FullName == btnOpenNPP.Text).Name;
                    break;
                case "btnCloseNPP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblNegativePressurePumpVP2.Text).CommandList.Find(command => command.FullName == btnCloseNPP.Text).Name;
                    break;
                case "btnOpenVP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblVacuumPumpVP1.Text).CommandList.Find(command => command.FullName == btnOpenVP.Text).Name;
                    break;
                case "btnCloseVP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblVacuumPumpVP1.Text).CommandList.Find(command => command.FullName == btnCloseVP.Text).Name;
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
