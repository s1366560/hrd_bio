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
        private WaterBlankCheck waterBlankCheck;
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

        public void RMThirdMenu_Load(object sender, EventArgs e)
        {
            if (waterBlankCheck == null)
            {
                waterBlankCheck = new WaterBlankCheck();
                waterBlankCheck.SendNetworkEvent += SendNetwork_Event;
                waterBlankCheck.SendMaintenanceNameEvent += SendMaintenanceName_Event;
                xtraTabPage1.Controls.Add(waterBlankCheck);
            }
            waterBlankCheck.WaterBlankCheck_Load();
        }
        /// <summary>
        /// 页面切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                if (!xtraTabPage1.Contains(waterBlankCheck))
                    xtraTabPage1.Controls.Add(waterBlankCheck);
                waterBlankCheck.WaterBlankCheck_Load();
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                if (ultravioletRays == null)
                {
                    ultravioletRays = new UltravioletRays();
                    ultravioletRays.SendNetworkEvent += SendNetwork_Event;
                    ultravioletRays.SendMaintenanceNameEvent += SendMaintenanceName_Event;
                    xtraTabPage2.Controls.Add(ultravioletRays);
                }
                if(!xtraTabPage2.Contains(ultravioletRays))
                    xtraTabPage2.Controls.Add(ultravioletRays);
                ultravioletRays.UltravioletRays_Load(null,null);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                if (cleaningMaintenance == null)
                {
                    cleaningMaintenance = new CleaningMaintenance();
                    cleaningMaintenance.SendNetworkEvent += SendNetwork_Event;
                    cleaningMaintenance.SendMaintenanceNameEvent += SendMaintenanceName_Event;
                    xtraTabPage3.Controls.Add(cleaningMaintenance);
                }
                if (!xtraTabPage3.Contains(cleaningMaintenance))
                    xtraTabPage3.Controls.Add(cleaningMaintenance);
                cleaningMaintenance.CleaningMaintenance_Load(null,null);
            }
        }
    }
}
