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
using BioA.Common.IO;
using BioA.Common;
using System.Threading;

namespace BioA.UI
{
    public delegate void SendMaintenanceNameDelegate(string MaintenanceName);

    public partial class RMThirdMenu : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;
        
        /// <summary>
        /// 存储用户名
        /// </summary>
        public static string _userName;

        UltravioletRays ultravioletRays;
        WaterBlankCheck waterBlankCheck;
        CleaningMaintenance cleaningMaintenance;
        BlankInterface blankInterface;

        public RMThirdMenu(string userName)
        {
            InitializeComponent();
            _userName = userName;
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {

            switch (strMethod)
            {
                case "QueryWaterBlankValueByWave":
                    List<CuvetteBlankInfo> lstAllCuvBlank;
                    lstAllCuvBlank = XmlUtility.Deserialize(typeof(List<CuvetteBlankInfo>), sender as string) as List<CuvetteBlankInfo>;
                    waterBlankCheck.ListCuveBlankInfo = lstAllCuvBlank;
                    //List<CuvetteBlankInfo> lstWLCData  = new List<CuvetteBlankInfo>();
                    //for (int i = 0; i < lstAllCuvBlank.Count; i++)
                    //{
                    //    if (lstAllCuvBlank[i].WaveLength == 340)
                    //    {
                    //        lstWLCData.Add(lstAllCuvBlank[i]);
                    //    }
                    //}
                    //if (lstWLCData.Count > 0)
                    //{
                    //    waterBlankCheck.LstCuvBlk = lstWLCData;
                    //}
                    this.Invoke(new EventHandler(delegate { xtraTabPage1.Controls.Remove(blankInterface); }));
                    break;
                case "QueryNewPhotemetricValue":
                    List<List<OffSetGain>> LstNewAndOldPhotoGain = new List<List<OffSetGain>>();
                    LstNewAndOldPhotoGain = XmlUtility.Deserialize(typeof(List<List<OffSetGain>>), sender as string) as List<List<OffSetGain>>;
                    if (LstNewAndOldPhotoGain.Count > 0)
                    {
                        ultravioletRays.LstNewPhotoGain = LstNewAndOldPhotoGain[0];
                        ultravioletRays.LstOldPhotoGain = LstNewAndOldPhotoGain[1];
                    }
                    break;
                //case "QueryOldPhotemetricValue":
                //    ultravioletRays.LstOldPhotoGain = XmlUtility.Deserialize(typeof(List<OffSetGain>), sender as string) as List<OffSetGain>;
                //    break;
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                xtraTabPage1.Controls.Clear();
                waterBlankCheck = new WaterBlankCheck();
                blankInterface = new BlankInterface();
                waterBlankCheck.SendNetworkEvent += SendNetwork_Event;
                waterBlankCheck.SendMaintenanceNameEvent += SendMaintenanceName_Event;
                xtraTabPage1.Controls.Add(blankInterface);
                xtraTabPage1.Controls.Add(waterBlankCheck);
                this.Invoke(new EventHandler(delegate { xtraTabPage1.Controls.Remove(blankInterface); }));
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                xtraTabPage2.Controls.Clear();
                ultravioletRays = new UltravioletRays();
                ultravioletRays.SendNetworkEvent += SendNetwork_Event;
                ultravioletRays.SendMaintenanceNameEvent += SendMaintenanceName_Event;
                xtraTabPage2.Controls.Add(ultravioletRays);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                xtraTabPage3.Controls.Clear();
                
                cleaningMaintenance = new CleaningMaintenance();
                cleaningMaintenance.SendNetworkEvent += SendNetwork_Event;
                cleaningMaintenance.SendMaintenanceNameEvent += SendMaintenanceName_Event;
                xtraTabPage3.Controls.Add(cleaningMaintenance);
            }
           
        }

        private void SendNetwork_Event(string sender)
        {
            if (SendNetworkEvent != null)
                SendNetworkEvent(sender);
        }
        /// <summary>
        /// 保存保养日志信息
        /// </summary>
        /// <param name="maintenanceName"></param>
        private void SendMaintenanceName_Event(string maintenanceName)
        {

            MaintenanceLogInfo maintenanceLogInfo = new MaintenanceLogInfo();
            maintenanceLogInfo.UserName = _userName;
            maintenanceLogInfo.LogDetails = maintenanceName;
            maintenanceLogInfo.LogDateTime = DateTime.Now;
            var MaintenanceLogInfoThread = new Thread(() => 
            { 
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.Login, new Dictionary<string, object[]>() { { "SaveMintenanceLog", new object[] { XmlUtility.Serializer(typeof(MaintenanceLogInfo), maintenanceLogInfo) } } });
            });
            MaintenanceLogInfoThread.IsBackground = true;
            MaintenanceLogInfoThread.Start();
        }

        private void RMThirdMenu_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                waterBlankCheck = new WaterBlankCheck();
                blankInterface = new BlankInterface();
                waterBlankCheck.SendNetworkEvent += SendNetwork_Event;
                waterBlankCheck.SendMaintenanceNameEvent += SendMaintenanceName_Event;
                xtraTabPage1.Controls.Add(blankInterface);
                xtraTabPage1.Controls.Add(waterBlankCheck);
                xtraTabPage1.Controls.Remove(blankInterface);
            }));
        }
    }
}
