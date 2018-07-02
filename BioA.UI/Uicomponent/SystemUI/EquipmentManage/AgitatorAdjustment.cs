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
    public partial class AgitatorAdjustment : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;
        public AgitatorAdjustment()
        {
            InitializeComponent();
        }

        private void AgitatorAdjustment_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(AgitatorAdjustmentInfo));
            
        }

        private void AgitatorAdjustmentInfo()
        {
            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "搅拌器调试")
                {
                    lblStirrer1.Text = sub.ComponetList[0].Name;
                    btnInitializelblStirrer1.Text = sub.ComponetList[0].CommandList[0].FullName;
                    btnToCuvette1.Text = sub.ComponetList[0].CommandList[1].FullName;
                    btnToWaterChannel1.Text = sub.ComponetList[0].CommandList[2].FullName;
                    btnBeginStir1.Text = sub.ComponetList[0].CommandList[3].FullName;
                    btnStopStir1.Text = sub.ComponetList[0].CommandList[4].FullName;

                    lblStirrer1Calibration.Text = sub.ComponetList[1].Name;
                    btnToBottom1.Text = sub.ComponetList[1].CommandList[0].FullName;
                    btnToTop1.Text = sub.ComponetList[1].CommandList[1].FullName;
                    btnClockwiseCalibration1.Text = sub.ComponetList[1].CommandList[2].FullName;
                    btnAnticlockwiseCalibration1.Text = sub.ComponetList[1].CommandList[3].FullName;
                    btnUpCalibration1.Text = sub.ComponetList[1].CommandList[4].FullName;
                    btnDownCalibration1.Text = sub.ComponetList[1].CommandList[5].FullName;
                    btnSaveCalibration1.Text = sub.ComponetList[1].CommandList[6].FullName;

                    lblStirrer2.Text = sub.ComponetList[2].Name;
                    btnInitializelblStirrer2.Text = sub.ComponetList[2].CommandList[0].FullName;
                    btnToCuvette2.Text = sub.ComponetList[2].CommandList[1].FullName;
                    btnToWaterChannel2.Text = sub.ComponetList[2].CommandList[2].FullName;
                    btnBeginStir2.Text = sub.ComponetList[2].CommandList[3].FullName;
                    btnStopStir2.Text = sub.ComponetList[2].CommandList[4].FullName;

                    lblStirrer1Calibration2.Text = sub.ComponetList[3].Name;
                    btnToBottom2.Text = sub.ComponetList[3].CommandList[0].FullName;
                    btnToTop2.Text = sub.ComponetList[3].CommandList[1].FullName;
                    btnClockwiseCalibration2.Text = sub.ComponetList[3].CommandList[2].FullName;
                    btnAnticlockwiseCalibration2.Text = sub.ComponetList[3].CommandList[3].FullName;
                    btnUpCalibration2.Text = sub.ComponetList[3].CommandList[4].FullName;
                    btnDownCalibration2.Text = sub.ComponetList[3].CommandList[5].FullName;
                    btnSaveCalibration2.Text = sub.ComponetList[3].CommandList[6].FullName;
                }
            }
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            string strSender = "";
            Subsystem ConfigureInfo = MachineInfo.SubsystemList.Find(str => str.Name == "搅拌器调试");
            switch (((SimpleButton)sender).Name)
            {
                case "btnInitializelblStirrer1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1.Text).CommandList.Find(command => command.FullName == btnInitializelblStirrer1.Text).Name;
                    break;
                case "btnToCuvette1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1.Text).CommandList.Find(command => command.FullName == btnToCuvette1.Text).Name;
                    break;
                case "btnToWaterChannel1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1.Text).CommandList.Find(command => command.FullName == btnToWaterChannel1.Text).Name;
                    break;
                case "btnBeginStir1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1.Text).CommandList.Find(command => command.FullName == btnBeginStir1.Text).Name;
                    break;
                case "btnStopStir1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1.Text).CommandList.Find(command => command.FullName == btnStopStir1.Text).Name;
                    break;
                case "btnToBottom1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration.Text).CommandList.Find(command => command.FullName == btnToBottom1.Text).Name;
                    break;
                case "btnToTop1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration.Text).CommandList.Find(command => command.FullName == btnToTop1.Text).Name;
                    break;
                case "btnClockwiseCalibration1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration.Text).CommandList.Find(command => command.FullName == btnClockwiseCalibration1.Text).Name;
                    break;
                case "btnAnticlockwiseCalibration1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration.Text).CommandList.Find(command => command.FullName == btnAnticlockwiseCalibration1.Text).Name;
                    break;
                case "btnUpCalibration1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration.Text).CommandList.Find(command => command.FullName == btnUpCalibration1.Text).Name;
                    break;
                case "btnDownCalibration1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration.Text).CommandList.Find(command => command.FullName == btnDownCalibration1.Text).Name;
                    break;
                case "btnSaveCalibration1":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration.Text).CommandList.Find(command => command.FullName == btnSaveCalibration1.Text).Name;
                    break;
                case "btnInitializelblStirrer2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer2.Text).CommandList.Find(command => command.FullName == btnInitializelblStirrer2.Text).Name;
                    break;
                case "btnToCuvette2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer2.Text).CommandList.Find(command => command.FullName == btnToCuvette2.Text).Name;
                    break;
                case "btnToWaterChannel2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer2.Text).CommandList.Find(command => command.FullName == btnToWaterChannel2.Text).Name;
                    break;
                case "btnBeginStir2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer2.Text).CommandList.Find(command => command.FullName == btnBeginStir2.Text).Name;
                    break;
                case "btnStopStir2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer2.Text).CommandList.Find(command => command.FullName == btnStopStir2.Text).Name;
                    break;
                case "btnToBottom2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration2.Text).CommandList.Find(command => command.FullName == btnToBottom2.Text).Name;
                    break;
                case "btnToTop2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration2.Text).CommandList.Find(command => command.FullName == btnToTop2.Text).Name;
                    break;
                case "btnClockwiseCalibration2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration2.Text).CommandList.Find(command => command.FullName == btnClockwiseCalibration2.Text).Name;
                    break;
                case "btnAnticlockwiseCalibration2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration2.Text).CommandList.Find(command => command.FullName == btnAnticlockwiseCalibration2.Text).Name;
                    break;
                case "btnUpCalibration2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration2.Text).CommandList.Find(command => command.FullName == btnUpCalibration2.Text).Name;
                    break;
                case "btnDownCalibration2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration2.Text).CommandList.Find(command => command.FullName == btnDownCalibration2.Text).Name;
                    break;
                case "btnSaveCalibration2":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == lblStirrer1Calibration2.Text).CommandList.Find(command => command.FullName == btnSaveCalibration2.Text).Name;
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
