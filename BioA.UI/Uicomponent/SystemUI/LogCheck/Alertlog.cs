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
using BioA.Service;

namespace BioA.UI
{
    public partial class Alertlog : DevExpress.XtraEditors.XtraUserControl
    {

        public delegate void LogDelegate(Dictionary<string, object[]> sender);
        public event LogDelegate LogEvent;
        //操作数据库的类
        SystemLogCheck systemLogCheck = new SystemLogCheck();
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> alertLogDic = new Dictionary<string, object[]>();
        //操作日志数据源
        DataTable dt = new DataTable();

        bool bol = false;

        public Alertlog(bool b)
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            this.bol = b;

            dt.Columns.Add("用户名");
            dt.Columns.Add("时间");
            dt.Columns.Add("日志详情");
            this.gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 200;
            this.gridView1.Columns[1].Width = 200;
            this.gridView1.Columns[2].Width = 1000;
        }
        public void MaintenanceLogInfoAdd(List<MaintenanceLogInfo> lstmaintenanceLogInfo)
        {
           
            this.Invoke(new EventHandler(delegate
            {
                dt.Rows.Clear();
                if (lstmaintenanceLogInfo.Count != 0)
                {
                    foreach (MaintenanceLogInfo maintenanceLogInfo in lstmaintenanceLogInfo)
                    {
                        dt.Rows.Add(new object[] {maintenanceLogInfo.UserName, maintenanceLogInfo.LogDateTime, maintenanceLogInfo.LogDetails });
                    }
                }
                this.gridControl1.DataSource = dt;
            }));
        }

        public void Alertlog_Load(object sender, EventArgs e)
        {
            this.loadAlertlog();
        }
        private void loadAlertlog()
        {
            alertLogDic.Clear();
            if (bol == false)
            {
                lblDate.Visible = false;
                dtpStartTime.Visible = false;
                dtpEndTime.Visible = false;
                btnSearch.Visible = false;
                btnSelect.Visible = false;
                btnReverseSelection.Visible = false;
                simpleButton1.Visible = false;
                //获取保养日志信息
                alertLogDic.Add("QueryMaintenanceLogInfo", null);
                if (LogEvent != null)
                    LogEvent(alertLogDic);
            }
            else
            {
                dtpStartTime.Value = DateTime.Now.Date;
                dtpEndTime.Value = DateTime.Now.Date;
                //获取操作日志信息
                MaintenanceLogInfoAdd(systemLogCheck.QueryOperationLogInfo("QueryOperationLogInfo", dtpStartTime.Value.ToString(), dtpEndTime.Value.AddDays(1).ToString()));
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string logstartTime = dtpStartTime.Value.ToString("yyyy-MM-dd");
            string logEndTime = dtpEndTime.Value.AddDays(1).ToString("yyyy-MM-dd");
            //获取操作日志信息//
            MaintenanceLogInfoAdd(systemLogCheck.QueryOperationLogInfo("QueryOperationLogInfo", logstartTime, logEndTime));
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.gridView1.SelectAll();
        }
        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReverseSelection_Click(object sender, EventArgs e)
        {
            int[] aaa = this.gridView1.GetSelectedRows();
            gridView1.SelectAll();
            for (int i = 0; i < aaa.Length; i++)
            {
                gridView1.UnselectRow(aaa[i]);
            }
        }
        /// <summary>
        /// 删除操作日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int[] rows;
            List<string> lstDrawDateTime = new List<string>();
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                rows = this.gridView1.GetSelectedRows();
                foreach (int i in rows)
                {
                    string drawDate = Convert.ToDateTime(this.gridView1.GetRowCellValue(i, "时间").ToString()).ToString("yyyy-MM-dd HH:mm:ss");                    
                    lstDrawDateTime.Add(drawDate);
                }
                int count = systemLogCheck.DeleteOperationLogInfo("DeleteOperationLogInfo", lstDrawDateTime);
                if (count > 0)
                {
                    MaintenanceLogInfoAdd(systemLogCheck.QueryOperationLogInfo("QueryOperationLogInfo", dtpStartTime.Value.ToString(), dtpEndTime.Value.AddDays(1).ToString()));
                }                              
            }
        }

    }
}
