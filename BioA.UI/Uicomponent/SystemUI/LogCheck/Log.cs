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
using System.Threading;

namespace BioA.UI
{
    public partial class Log : DevExpress.XtraEditors.XtraUserControl
    {
        //private BioAServiceClient serviceClient;
        //private LogCallBack notifyCallBack;
        Alertlog Maintenancealertlog;
        Alertlog OperationLogInfo;
        Alertlog AlarmLog;
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> logDictionary = new Dictionary<string, object[]>();
        public Log()
        {
            InitializeComponent();
            //notifyCallBack = new LogCallBack();
            
            //AlarmLog.LogEvent += alertlog_LogEvent;
            //Maintenancealertlog.LogEvent += alertlog_LogEvent;           
            //serviceClient = new BioAServiceClient(new InstanceContext(notifyCallBack));
            // 注册客户端
            //serviceClient.RegisterClient(XmlUtility.Serializer(typeof(ModuleInfo), ModuleInfo.SystemLogCheck));
            //notifyCallBack.DataTransferEvent += DataTransfer_Event;
          
        }

        private void alertlog_LogEvent(Dictionary<string, object[]> sender)
        {
            var logThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SystemLogCheck, sender);
            });
            logThread.IsBackground = true;
            logThread.Start();
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

                case "SelectAlarmLogInfoByUName":
                     lstalarmLogInfo = (List<AlarmLogInfo>)XmlUtility.Deserialize(typeof(List<AlarmLogInfo>), sender as string);
                     AlarmLog.AlarmLogInfoAdd(lstalarmLogInfo);
                    break;
                
            }
        }
        /// <summary>
        /// 标题点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                Maintenancealertlog = new Alertlog();
                Maintenancealertlog.btnRemove();
                Maintenancealertlog.LogEvent += alertlog_LogEvent;
                xtraTabPage1.Controls.Add(Maintenancealertlog);

            }            
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                OperationLogInfo = new Alertlog();
                //CommunicationEntity DataConfig = new CommunicationEntity();
                //DataConfig.StrmethodName = "QueryOperationLogInfo";
                //DataConfig.ObjParam = "";
                logDictionary.Clear();
                //获取操作日志信息
                logDictionary.Add("QueryOperationLogInfo", null);
                alertlog_LogEvent(logDictionary);
                OperationLogInfo.btnRemove();
                Maintenancealertlog.LogEvent += alertlog_LogEvent;
                xtraTabPage3.Controls.Add(OperationLogInfo);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                AlarmLog = new Alertlog();
                //CommunicationEntity DataConfig = new CommunicationEntity();
                //DataConfig.StrmethodName = "QueryAlarmLogInfo";
                //DataConfig.ObjParam = "";
                logDictionary.Clear();
                //获取报警日志信息
                logDictionary.Add("QueryAlarmLogInfo", null);
                alertlog_LogEvent(logDictionary);
                Maintenancealertlog.LogEvent += alertlog_LogEvent;
                xtraTabPage4.Controls.Add(AlarmLog);              
            }
           
        }

        private void Log_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadLog));
        }
        private void loadLog()
        {
            Maintenancealertlog = new Alertlog();
            Maintenancealertlog.btnRemove();
            Maintenancealertlog.LogEvent += alertlog_LogEvent;
            xtraTabPage1.Controls.Add(Maintenancealertlog);
        }
       
    }
}
