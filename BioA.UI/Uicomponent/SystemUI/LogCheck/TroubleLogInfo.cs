﻿using System;
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
    public partial class TroubleLogInfo : DevExpress.XtraEditors.XtraUserControl
    {
        //查询数据库的类
        SystemLogCheck systemLogCheck = new SystemLogCheck();
        /// <summary>
        /// 信息提示数据列表
        /// </summary>
        DataTable dt = new DataTable();
        public TroubleLogInfo()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            dt.Columns.Add("故障编码");
            dt.Columns.Add("时间");
            dt.Columns.Add("故障类型");
            dt.Columns.Add("故障单元");
            dt.Columns.Add("故障日志详情");
            this.grdControlTrouble.DataSource = dt;
            this.gridView1.Columns[0].Width = 100;
            this.gridView1.Columns[1].Width = 150;
            this.gridView1.Columns[2].Width = 150;
            this.gridView1.Columns[3].Width = 100;
            this.gridView1.Columns[4].Width = 700;
            

        }
        public void TroubleLogInfo_Load(object sender, EventArgs e)
        {
            this.LoadTroubleLog();
        }
        private void LoadTroubleLog()
        {
            dtpStartTime.Value = DateTime.Now.Date;
            dtpEndTime.Value = DateTime.Now.Date;
            TroubleLogInfoAdd(systemLogCheck.SelectTroubleLogInfoByTimeQuantum("SelectTroubleLogInfoByTimeQuantum", dtpStartTime.Value.ToString(), dtpEndTime.Value.AddDays(1).ToString()));

        }



        public void TroubleLogInfoAdd(List<TroubleLog> lstTroubleLogInfo)
        {
            dt.Rows.Clear();
            if (lstTroubleLogInfo.Count != 0)
            {
                foreach (TroubleLog troubleLogInfo in lstTroubleLogInfo)
                {
                    dt.Rows.Add(new object[] { troubleLogInfo.TroubleCode, troubleLogInfo.DrawDate, troubleLogInfo.TroubleType, troubleLogInfo.TroubleUnit, troubleLogInfo.TroubleInfo });
                }
            }
            this.grdControlTrouble.DataSource = dt;
        }
        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string logstartTime = dtpStartTime.Value.ToShortDateString();
            string logEndTime = dtpEndTime.Value.AddDays(1).ToShortDateString();
            TroubleLogInfoAdd(systemLogCheck.SelectTroubleLogInfoByTimeQuantum("SelectTroubleLogInfoByTimeQuantum", logstartTime, logEndTime));        
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
        /// 确认警告信息和错误信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<string> lstDrawDateTime = new List<string>();
            int[] rows;
            if(this.gridView1.GetSelectedRows().Count() > 0)
            {
                rows = this.gridView1.GetSelectedRows();
                foreach (int i in rows)
                {
                    string drawDate = Convert.ToDateTime(this.gridView1.GetRowCellValue(i, "时间").ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    drawDate = drawDate +"|"+ this.gridView1.GetRowCellValue(i, "故障编码").ToString();
                    lstDrawDateTime.Add(drawDate);
                }
                //消除重复的
                lstDrawDateTime =lstDrawDateTime.Distinct().ToList();
                int count = systemLogCheck.AffirmTroubleLogInfo("AffirmTroubleLogInfo", lstDrawDateTime);
                if(count >0)
                {
                    btnSearch_Click(null,null);
                }
            }
        }

        private int _result;

        public int Result
        {
            get { return _result; }
            set
            {
                _result = value;
                btnSearch_Click(null, null);
            }
        }
    }
}
