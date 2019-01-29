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
using System.Diagnostics;
using BioA.Service;

namespace BioA.UI
{
    public partial class Log : DevExpress.XtraEditors.XtraUserControl
    {
        Alertlog Maintenancealertlog;
        Alertlog OperationLogInfo;
        TroubleLogInfo troubleLog;
        //用户名
        public string UserName = string.Empty;
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> logDictionary = new Dictionary<string, object[]>();
        public Log()
        {
            InitializeComponent();
          
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
        //List<TroubleLog> lstTroubleLogInfo = new List<TroubleLog>();
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

                case "SelectTroubleLogInfoByTimeQuantum":
                    List<TroubleLog> lstTroubleLogInfo = (List<TroubleLog>)XmlUtility.Deserialize(typeof(List<TroubleLog>), sender as string);
                    troubleLog.TroubleLogInfoAdd(lstTroubleLogInfo);
                    break;
                case "AffirmTroubleLogInfo":
                    int result = (int)sender;
                    if(result > 0)
                    {
                        troubleLog.Result = result;
                    }
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
                xtraTabPage1.Controls.Clear();
                troubleLog = new TroubleLogInfo();
                troubleLog.TroubleLogEvent += alertlog_LogEvent;
                xtraTabPage1.Controls.Add(troubleLog);

            }            
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                xtraTabPage3.Controls.Clear();
                Maintenancealertlog = new Alertlog(false);
                Maintenancealertlog.LogEvent += alertlog_LogEvent;
                xtraTabPage3.Controls.Add(Maintenancealertlog);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                xtraTabPage4.Controls.Clear();
                OperationLogInfo = new Alertlog(true);
                //logDictionary.Clear();
                OperationLogInfo.LogEvent += alertlog_LogEvent;
                xtraTabPage4.Controls.Add(OperationLogInfo);
            }
           
        }

        private void Log_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadLog));
        }
        private void loadLog()
        {
            troubleLog = new TroubleLogInfo();
            troubleLog.TroubleLogEvent += alertlog_LogEvent;
            xtraTabPage1.Controls.Add(troubleLog);
        }
        /// <summary>
        /// 关闭系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBoxDraw.ShowMsg("确认要关闭系统吗？", MsgType.Question) == System.Windows.Forms.DialogResult.OK)
            {
                Login login = new Login();
                login.SaveUserExitInfo("QueryUserAuthority", UserName);
                System.Diagnostics.Process[] ps = Process.GetProcessesByName("BioA.PLCController");
                foreach (System.Diagnostics.Process p in ps)
                {
                    p.Kill();
                }
                this.Dispose();                
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }
       
    }
}
