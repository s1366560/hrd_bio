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
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using BioA.Common;
using BioA.Common.IO;

namespace BioA.UI
{
    public partial class Log : DevExpress.XtraEditors.XtraUserControl
    {
        //private BioAServiceClient serviceClient;
        //private LogCallBack notifyCallBack;
        Alertlog Maintenancealertlog;
        Alertlog OperationLogInfo;
        Alertlog AlarmLog;

       
        public Log()
        {
            InitializeComponent();
            //notifyCallBack = new LogCallBack();
            Maintenancealertlog = new Alertlog();
            OperationLogInfo = new Alertlog();
            AlarmLog = new Alertlog();
            //AlarmLog.LogEvent += alertlog_LogEvent;
            //Maintenancealertlog.LogEvent += alertlog_LogEvent;           
            //serviceClient = new BioAServiceClient(new InstanceContext(notifyCallBack));
            // 注册客户端
            //serviceClient.RegisterClient(XmlUtility.Serializer(typeof(ModuleInfo), ModuleInfo.SystemLogCheck));
            //notifyCallBack.DataTransferEvent += DataTransfer_Event;
          
        }

        private void alertlog_LogEvent(object sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SystemLogCheck, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }

        List<MaintenanceLogInfo> maintenanceLogInfo = new List<MaintenanceLogInfo>();
        List<AlarmLogInfo> lstalarmLogInfo = new List<AlarmLogInfo>();
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
               
                case "QueryMaintenanceLogInfo":
                 
                    maintenanceLogInfo = (List<MaintenanceLogInfo>)XmlUtility.Deserialize(typeof(List<MaintenanceLogInfo>), sender as string);
                    Maintenancealertlog.MaintenanceLogInfoAdd(maintenanceLogInfo);
                   
                    break;

                case "QueryOperationLogInfo":

                    maintenanceLogInfo = (List<MaintenanceLogInfo>)XmlUtility.Deserialize(typeof(List<MaintenanceLogInfo>), sender as string);
                    OperationLogInfo.MaintenanceLogInfoAdd(maintenanceLogInfo);

                    break;

                case "QueryAlarmLogInfo":

                    lstalarmLogInfo = (List<AlarmLogInfo>)XmlUtility.Deserialize(typeof(List<AlarmLogInfo>), sender as string);
                    AlarmLog.AlarmLogInfoAdd(lstalarmLogInfo);

                    break;

                case "SelectAlarmLogInfo":

                     lstalarmLogInfo = (List<AlarmLogInfo>)XmlUtility.Deserialize(typeof(List<AlarmLogInfo>), sender as string);
                     AlarmLog.AlarmLogInfoAdd(lstalarmLogInfo);

                    break;
                
            }
        }

       

        private void LogSend(CommunicationEntity sender)
        {
            //serviceClient.ClientSendMsgToService(ModuleInfo.SystemLogCheck, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                Maintenancealertlog.btnRemove();
                Maintenancealertlog.LogEvent += alertlog_LogEvent;
                xtraTabPage1.Controls.Add(Maintenancealertlog);

            }            
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "QueryOperationLogInfo";
                DataConfig.ObjParam = "";
                alertlog_LogEvent(DataConfig);
                OperationLogInfo.btnRemove();
                xtraTabPage3.Controls.Add(OperationLogInfo);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "QueryAlarmLogInfo";
                DataConfig.ObjParam = "";
                alertlog_LogEvent(DataConfig);               
                xtraTabPage4.Controls.Add(AlarmLog);              
            }
           
        }

        private void Log_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadLog));
        }
        private void loadLog()
        {
            Maintenancealertlog.btnRemove();
            Maintenancealertlog.LogEvent += alertlog_LogEvent;
            xtraTabPage1.Controls.Add(Maintenancealertlog);
        }
       
    }
}
