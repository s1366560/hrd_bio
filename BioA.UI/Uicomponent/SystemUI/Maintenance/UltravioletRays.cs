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
using BioA.Common;
using BioA.Common.IO;
using BioA.Service;

namespace BioA.UI
{
    public partial class UltravioletRays : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;

        public event SendMaintenanceNameDelegate SendMaintenanceNameEvent;
        public UltravioletRays()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 光度计校准最新值
        /// </summary>
        private List<OffSetGain> lstNewPhotoGain = new List<OffSetGain>();
        public List<OffSetGain> LstNewPhotoGain
        {
            set 
            {
                lstNewPhotoGain = value;
                try
                {
                    if (lstNewPhotoGain.Count > 0)
                    {
                        this.Invoke(new EventHandler(delegate {
                            txtNewGain340.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[0])).Gain.ToString();
                            txtNewGain405.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[1])).Gain.ToString();
                            txtNewGain445.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[2])).Gain.ToString();
                            txtNewGain484.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[3])).Gain.ToString();
                            txtNewGain510.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[4])).Gain.ToString();
                            txtNewGain549.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[5])).Gain.ToString();
                            txtNewGain574.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[6])).Gain.ToString();
                            txtNewGain600.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[7])).Gain.ToString();
                            txtNewGain663.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[8])).Gain.ToString();
                            txtNewGain700.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[9])).Gain.ToString();
                            txtNewGain748.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[10])).Gain.ToString();
                            txtNewGain795.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[11])).Gain.ToString();
                            txtNewGainInsTime.Text = lstNewPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[11])).InspectTime.ToString();
                        }));
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
        /// <summary>
        /// 光度计校准历史值
        /// </summary>
        private List<OffSetGain> lstOldPhotoGain = new List<OffSetGain>();
        public List<OffSetGain> LstOldPhotoGain
        {
            set
            {
                lstOldPhotoGain = value;
                try
                {
                    if (lstOldPhotoGain.Count > 0)
                    {
                        this.Invoke(new EventHandler(delegate { 
                            txtOldGain340.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[0])).Gain.ToString();
                            txtOldGain405.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[1])).Gain.ToString();
                            txtOldGain445.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[2])).Gain.ToString();
                            txtOldGain484.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[3])).Gain.ToString();
                            txtOldGain510.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[4])).Gain.ToString();
                            txtOldGain549.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[5])).Gain.ToString();
                            txtOldGain574.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[6])).Gain.ToString();
                            txtOldGain600.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[7])).Gain.ToString();
                            txtOldGain663.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[8])).Gain.ToString();
                            txtOldGain700.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[9])).Gain.ToString();
                            txtOldGain748.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[10])).Gain.ToString();
                            txtOldGain795.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[11])).Gain.ToString();
                            txtOldGainInsTime.Text = lstOldPhotoGain.Find(x => x.WaveLength == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[11])).InspectTime.ToString();
                        }));
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        private void btnPhotometricCalibration_Click(object sender, EventArgs e)
        {
            string strSender = "";
            strSender = MachineInfo.SubsystemList.Find(str => str.Name == "Common").ComponetList.Find(componet => componet.Name == "Maintance").CommandList.Find(command => command.FullName == btnPhotometricCalibration.Text).Name;

            if (SendNetworkEvent != null && strSender != "")
            {
                SendNetworkEvent(strSender);
            }
            if(SendMaintenanceNameEvent != null)
            {
                SendMaintenanceNameEvent(btnPhotometricCalibration.Text);
            }
        }

        public void UltravioletRays_Load(object sender, EventArgs e)
        {
            this.loadPhotometerDetection();
        }

        private void loadPhotometerDetection()
        {
            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "Common")
                {
                    btnPhotometricCalibration.Text = sub.ComponetList[1].CommandList[5].FullName;
                }
            }


            List<List<OffSetGain>> LstNewAndOldPhotoGain = new SystemMaintenance().QueryNewPhotemetricValue("QueryOldPhotemetricValue");
            if (LstNewAndOldPhotoGain != null)
            {
                this.LstNewPhotoGain = LstNewAndOldPhotoGain[0];
                this.LstOldPhotoGain = LstNewAndOldPhotoGain[1];
            }
        }
    }
}
