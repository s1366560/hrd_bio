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

        bool bol = false;

        public Alertlog(bool b)
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            this.bol = b;
        }
        public void MaintenanceLogInfoAdd(List<MaintenanceLogInfo> lstmaintenanceLogInfo)
        {
            //if (this.IsHandleCreated)
            //{
           
                this.Invoke(new EventHandler(delegate
                {
                    gridView1.Columns.Clear();
                    gridControl1.RefreshDataSource();
                    DataTable dt = new DataTable();

                    dt.Columns.Add("用户名");
                    dt.Columns.Add("时间");
                    dt.Columns.Add("日志详情");
                    if (lstmaintenanceLogInfo.Count != 0)
                    {
                        foreach (MaintenanceLogInfo maintenanceLogInfo in lstmaintenanceLogInfo)
                        {
                            dt.Rows.Add(new object[] {maintenanceLogInfo.UserName, maintenanceLogInfo.LogDateTime, maintenanceLogInfo.LogDetails });
                        }
                    }
                    this.gridControl1.DataSource = dt;
                    this.gridView1.Columns[0].Width = 200;
                    this.gridView1.Columns[1].Width = 200;
                    this.gridView1.Columns[2].Width = 1000;
                    this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                    this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
                    this.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
                }));
            //}
        }

        private void Alertlog_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadAlertlog));
            
        }
        private void loadAlertlog()
        {
            alertLogDic.Clear();
            if (bol == false)
            {
                //获取保养日志信息
                alertLogDic.Add("QueryMaintenanceLogInfo", null);
                if (LogEvent != null)
                    LogEvent(alertLogDic);
            }
            else
            {
                //获取操作日志信息
                alertLogDic.Add("QueryOperationLogInfo", null);
                LogEvent(alertLogDic);
            }
        }
        /// <summary>
        /// 显示序列号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}
