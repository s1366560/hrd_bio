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
    public delegate void SendNetworkDelegate(string sender);
    public delegate void SendNetworkCommandDelegate(Command command);

    public partial class ReactionDiskDebug : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;
        public ReactionDiskDebug()
        {
            InitializeComponent();
        }

        private void ReactionDiskDebug_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadReactionDiskDebugInfo));
            
        }
        /// <summary>
        /// 加载反应盘调试信息
        /// </summary>
        private void loadReactionDiskDebugInfo()
        {
            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "反应盘调试")
                {
                    lblResponseDishDebug.Text = sub.ComponetList[0].Name;
                    btninitializeRD.Text = sub.ComponetList[0].CommandList[0].FullName;
                    btnLightPathAlignment.Text = sub.ComponetList[0].CommandList[1].FullName;
                    btnClockwiseRotationOP.Text = sub.ComponetList[0].CommandList[2].FullName;
                    btnContrarotateOP.Text = sub.ComponetList[0].CommandList[3].FullName;
                    btnRotateAR.Text = sub.ComponetList[0].CommandList[4].FullName;
                    btnRotateOCP.Text = sub.ComponetList[0].CommandList[5].FullName;
                    btnInitializeLocationCS.Text = sub.ComponetList[0].CommandList[6].FullName;
                    btnLightPathAlignmentCS.Text = sub.ComponetList[0].CommandList[7].FullName;

                    lblEightRinseDebug.Text = sub.ComponetList[1].Name;
                    btninitializeER.Text = sub.ComponetList[1].CommandList[0].FullName;
                    btnToBottom.Text = sub.ComponetList[1].CommandList[1].FullName;
                    btnToTop.Text = sub.ComponetList[1].CommandList[2].FullName;
                    btnUpCalibration.Text = sub.ComponetList[1].CommandList[3].FullName;
                    btnDownCalibration.Text = sub.ComponetList[1].CommandList[4].FullName;
                    btnSaveCalibration.Text = sub.ComponetList[1].CommandList[5].FullName;

                    lblResponseTC.Text = sub.ComponetList[2].Name;
                    btnprobeTD.Text = sub.ComponetList[2].CommandList[0].FullName;
                }
            }
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            string strSender = "";
            Subsystem ConfigureInfo = MachineInfo.SubsystemList.Find(str => str.Name == "反应盘调试");
            switch (((SimpleButton)sender).Name)
            {
                case "btninitializeRD":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblResponseDishDebug.Text).CommandList.Find(command => command.FullName == btninitializeRD.Text).Name;
                    break;
                case "btnLightPathAlignment":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblResponseDishDebug.Text).CommandList.Find(command => command.FullName == btnLightPathAlignment.Text).Name;
                    break;
                case "btnClockwiseRotationOP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblResponseDishDebug.Text).CommandList.Find(command => command.FullName == btnClockwiseRotationOP.Text).Name;
                    break;
                case "btnContrarotateOP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblResponseDishDebug.Text).CommandList.Find(command => command.FullName == btnContrarotateOP.Text).Name;
                    break;
                case "btnRotateAR":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblResponseDishDebug.Text).CommandList.Find(command => command.FullName == btnRotateAR.Text).Name;
                    break;
                case "btnRotateOCP":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblResponseDishDebug.Text).CommandList.Find(command => command.FullName == btnRotateOCP.Text).Name;
                    break;
                case "btnInitializeLocationCS":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblResponseDishDebug.Text).CommandList.Find(command => command.FullName == btnInitializeLocationCS.Text).Name;
                    break;
                case "btnLightPathAlignmentCS":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblResponseDishDebug.Text).CommandList.Find(command => command.FullName == btnLightPathAlignmentCS.Text).Name;
                    break;
                case "btninitializeER":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblEightRinseDebug.Text).CommandList.Find(command => command.FullName == btninitializeER.Text).Name;
                    break;
                case "btnToBottom":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblEightRinseDebug.Text).CommandList.Find(command => command.FullName == btnToBottom.Text).Name;
                    break;
                case "btnToTop":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblEightRinseDebug.Text).CommandList.Find(command => command.FullName == btnToTop.Text).Name;
                    break;
                case "btnUpCalibration":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblEightRinseDebug.Text).CommandList.Find(command => command.FullName == btnUpCalibration.Text).Name;
                    break;
                case "btnDownCalibration":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblEightRinseDebug.Text).CommandList.Find(command => command.FullName == btnDownCalibration.Text).Name;
                    break;
                case "btnSaveCalibration":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblEightRinseDebug.Text).CommandList.Find(command => command.FullName == btnSaveCalibration.Text).Name;
                    break;
                case "btnprobeTD":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblResponseTC.Text).CommandList.Find(command => command.FullName == btnprobeTD.Text).Name;
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
