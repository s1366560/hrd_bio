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
using BioA.Common;
using System.Threading;
using BioA.Common.IO;

namespace BioA.UI
{
    public partial class Alertlog : DevExpress.XtraEditors.XtraUserControl
    {

        public delegate void LogDelegate(Dictionary<string, object[]> sender);
        public event LogDelegate LogEvent;
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> alertLogDic = new Dictionary<string, object[]>();
        public Alertlog()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            

        }

      

        public void MaintenanceLogInfoAdd(List<MaintenanceLogInfo> lstmaintenanceLogInfo)
        {
            //if (this.IsHandleCreated)
            //{
           
                this.Invoke(new EventHandler(delegate
                {
                    gridView1.Columns.Clear();
                    gridControl1.RefreshDataSource();

                    int i = 1;
                    DataTable dt = new DataTable();

                    dt.Columns.Add("用户名");
                    dt.Columns.Add("日志详情");
                    dt.Columns.Add("时间");


                    if (lstmaintenanceLogInfo.Count != 0)
                    {
                        foreach (MaintenanceLogInfo maintenanceLogInfo in lstmaintenanceLogInfo)
                        {
                            dt.Rows.Add(new object[] {i, maintenanceLogInfo.UserName, maintenanceLogInfo.LogDetails, maintenanceLogInfo.LogDateTime });

                            i++;
                        }
                    }
                    this.gridControl1.DataSource = dt;
                    this.gridView1.Columns[0].Width = 200;
                    this.gridView1.Columns[1].Width = 1000;
                }));
            //}
        }

        public void btnRemove ()
        {
            this.Controls.Remove(btnSearch);
            this.Controls.Remove(lblUsername);
            this.Controls.Remove(textEdit1);
            this.Controls.Remove(lblDate);
            this.Controls.Remove(dtpStartTime);
            this.Controls.Remove(dtpEndTime);
            this.Controls.Remove(label1);

        }

        private void Alertlog_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadAlertlog));
            
        }
        private void loadAlertlog()
        {
            //if (LogEvent != null)
            //{
            //    CommunicationEntity DataConfig = new CommunicationEntity();
            //    DataConfig.StrmethodName = "QueryMaintenanceLogInfo";
            //    DataConfig.ObjParam = "";
            //    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SystemLogCheck, XmlUtility.Serializer(typeof(CommunicationEntity), DataConfig));
            //    //LogEvent(DataConfig);
            //}
            alertLogDic.Clear();
            //获取保养日志信息
            alertLogDic.Add("QueryMaintenanceLogInfo", null);
            if (LogEvent != null)
                LogEvent(alertLogDic);
        }



        internal void AlarmLogInfoAdd(List<AlarmLogInfo> lstalarmLogInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridView1.Columns.Clear();
                gridControl1.RefreshDataSource();

                int i = 1;
                DataTable dt = new DataTable();

                dt.Columns.Add("故障编码");
                dt.Columns.Add("报警原因");
                dt.Columns.Add("日志详情");
                dt.Columns.Add("报警级别");
                dt.Columns.Add("用户名");
                dt.Columns.Add("时间");


                if (lstalarmLogInfo.Count != 0)
                {
                    foreach (AlarmLogInfo alarmLogInfo in lstalarmLogInfo)
                    {
                        dt.Rows.Add(new object[] {i, alarmLogInfo.FaultCode, alarmLogInfo.AlarmReason, alarmLogInfo.LogDetails, alarmLogInfo.AlarmLevel, alarmLogInfo.UserName, alarmLogInfo.LogstartTime});

                        i++;
                    }
                }
                this.gridControl1.DataSource = dt;
                this.gridView1.Columns[0].Width = 100;
                this.gridView1.Columns[0].Width = 200;
                this.gridView1.Columns[0].Width = 700;
                this.gridView1.Columns[0].Width = 100;
                this.gridView1.Columns[0].Width = 200;
                this.gridView1.Columns[0].Width = 200;

            }));
        }
        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (LogEvent != null)
            {
                AlarmLogInfo alarmLogInfo = new AlarmLogInfo();
                alarmLogInfo.UserName = textEdit1.Text;
                alarmLogInfo.LogstartTime = dtpStartTime.Value;
                alarmLogInfo.LogEndTime = dtpEndTime.Value;
                //CommunicationEntity DataConfig = new CommunicationEntity();
                //DataConfig.StrmethodName = "SelectAlarmLogInfo";
                //DataConfig.ObjParam = XmlUtility.Serializer(typeof(AlarmLogInfo), alarmLogInfo);
                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SystemLogCheck, XmlUtility.Serializer(typeof(CommunicationEntity), DataConfig));
                //LogEvent(DataConfig);
                alertLogDic.Clear();
                alertLogDic.Add("SelectAlarmLogInfoByUName", new object[] { XmlUtility.Serializer(typeof(AlarmLogInfo), alarmLogInfo) });
                if (LogEvent != null)
                    LogEvent(alertLogDic);
            }
        }
    }
}
