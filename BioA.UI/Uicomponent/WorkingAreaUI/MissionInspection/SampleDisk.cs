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
using BioA.Service;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BioA.UI
{
    public partial class SampleDisk : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate bool GetOPID();
        public event GetOPID getOPID;

        //gridview数据表
        DataTable dt = new DataTable();

        public SampleDisk()
        {
            InitializeComponent();
            ComSampleNum.Items.AddRange(RunConfigureUtility.SamplePanel.ToArray());

            dt.Columns.Add("样本编号");
            dt.Columns.Add("样本位置");
            dt.Columns.Add("样本申请时间");
            dt.Columns.Add("条码");
            dt.Columns.Add("项目计划");
            for (int i = 1; i < 120; i++)
            {
                dt.Rows.Add("", "", "", "");
            }
            this.gridControl1.DataSource = dt;
            gridView1.Columns[0].Width = 30;
            gridView1.Columns[1].Width = 80;
            gridView1.Columns[2].Width = 80;
            gridView1.Columns[3].Width = 100;
            gridView1.Columns[4].Width = 480;
            
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SampleDisk_Load(object sender, EventArgs e)
        {
            if (ComSampleNum.SelectedIndex == 0)
                ComSampleNum_SelectedIndexChanged(null, null);
            else
                ComSampleNum.SelectedIndex = 0;
        }
        /// <summary>
        /// 盘符改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComSampleNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<TaskInfo> lstTaskInfos = new WorkAreaApplyTask().GetTaskInfo("GetTaskInfo",Convert.ToInt32(ComSampleNum.Text));
                //this.Invoke(new EventHandler(delegate 
                //{
                    dt.Rows.Clear();
                    for (int i = 1; i <= 120; i++)
                    {
                        TaskInfo task = lstTaskInfos.SingleOrDefault(s => s.SamplePos == i);
                        if (task != null)
                        {
                            dt.Rows.Add(task.SampleNum,task.SamplePos, task.CreateDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), task.Barcode, task.ProjectName);
                        }
                        else
                            dt.Rows.Add("", "", "", "", "");
                    }
                    this.gridControl1.DataSource = dt;
                //}));
            
        }
        /// <summary>
        /// 显示行号
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

        //私有成员变量
        private int hotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        /// <summary>
        /// 存储测试点下面每一行的句柄
        /// </summary>
        private int HotTrackRow
        {
            get
            {
                return hotTrackRow;
            }
            set
            {
                if (hotTrackRow != value)
                {
                    int prevHotTrackRow = hotTrackRow;

                    hotTrackRow = value;
                    gridView1.RefreshRow(prevHotTrackRow);
                    gridView1.RefreshRow(hotTrackRow);
                    if (hotTrackRow >= 0)
                        gridControl1.Cursor = Cursors.Hand;
                    else
                        gridControl1.Cursor = Cursors.Default;
                }
            }
        }
        /// <summary>
        /// 鼠标移动到哪一行，背景色就为浅蓝色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == this.HotTrackRow)
                e.Appearance.BackColor = Color.CornflowerBlue;
        }
        /// <summary>
        /// 鼠标停下来获取到的行号的焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView gridview = sender as GridView;
            GridHitInfo info = gridview.CalcHitInfo(new Point(e.X, e.Y));
            if (info.InRowCell)
                this.HotTrackRow = info.RowHandle;
            else
                this.HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButFullSelection_Click(object sender, EventArgs e)
        {
            this.gridView1.SelectAll();
        }
        /// <summary>
        /// 外圈选取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButOuterRingSelection_Click(object sender, EventArgs e)
        {
            this.gridView1.ClearSelection();
            this.gridView1.FocusedRowHandle = 0;
            for (int i = 0; i < 40; i++)
            {
                this.gridView1.SelectRow(i);
            } 
        }
        /// <summary>
        /// 中间圈选取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButMiddleCircleSelection_Click(object sender, EventArgs e)
        {
            this.gridView1.ClearSelection();
            this.gridView1.FocusedRowHandle = 40;
            for (int i = 40; i < 80; i++)
            {
                this.gridView1.SelectRow(i);
            } 
        }
        /// <summary>
        /// 内圈选取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButInnerRingSelection_Click(object sender, EventArgs e)
        {
            this.gridView1.ClearSelection();
            this.gridView1.FocusedRowHandle = 80;
            for (int i = 80; i < 120; i++)
            {
                this.gridView1.SelectRow(i);
            } 
        }
        /// <summary>
        /// 反向选取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButReverseSelection_Click(object sender, EventArgs e)
        {
            int[] antiElection = this.gridView1.GetSelectedRows();
            this.gridView1.SelectAll();
            for (int i = 0; i < antiElection.Length; i++)
            {
                this.gridView1.UnselectRow(antiElection[i]);
                if (antiElection[0] == 0)
                {
                    this.gridView1.FocusedRowHandle = 80;
                }
                else if (antiElection[0] == 40)
                {
                    this.gridView1.FocusedRowHandle = 0;
                }
                else if (antiElection[0] == 80)
                {
                    this.gridView1.FocusedRowHandle = 0;
                }
            }
        }
        /// <summary>
        /// 清除样本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButClearSample_Click(object sender, EventArgs e)
        {
            if (getOPID != null && !getOPID())
            {
                MessageBoxDraw.ShowMsg("机器正在运行中，不能清除样本信息！", MsgType.OK);
            }
            else
            {
                try
                {
                    int[] rows = this.gridView1.GetSelectedRows();
                    List<TaskInfo> lstTaskInfo = new List<TaskInfo>();
                    TaskInfo task;
                    for (int i = 0; i < rows.Length; i++)
                    {
                        string samplelNum = this.gridView1.GetRowCellValue(rows[i], "样本编号").ToString();
                        if (samplelNum != "" && samplelNum != null)
                        {
                            task = new TaskInfo();
                            task.SampleNum = Convert.ToInt32(samplelNum);

                            task.CreateDate = Convert.ToDateTime(this.gridView1.GetRowCellValue(rows[i], "样本申请时间"));
                            lstTaskInfo.Add(task);
                        }
                    }
                    if (lstTaskInfo.Count > 0)
                    {
                        string result = new WorkAreaApplyTask().DeleteTaskAndSampleInfo("DeleteTaskAndSampleInfo", lstTaskInfo);
                        if (result.Substring(0, 1) == "1")
                        {
                            result = "选取样本信息清除成功!";
                            ComSampleNum_SelectedIndexChanged(null, null);
                        }
                        else if (result.Substring(0, 1) == "2")
                        {
                            result = result.Remove(0, 1) + ":样本信息清除成功！";
                            ComSampleNum_SelectedIndexChanged(null, null);
                        }
                        else
                            result = result.Remove(0, 1);
                        MessageBoxDraw.ShowMsg(result, MsgType.OK);

                    }
                }
                catch (FormatException fx)
                {
                    Console.WriteLine(fx.Message);
                }
            }
        }
        /// <summary>
        /// 样本、试剂仓扫描条形码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mbStartScan_Click(object sender, EventArgs e)
        {
            if (getOPID != null && !getOPID())
            {
                MessageBoxDraw.ShowMsg("机器正在运行中，不能进行条码扫描！", MsgType.OK);
                return;
            }
            else
            {
                int[] count = this.gridView1.GetSelectedRows();
                if (count.Length > 0)
                {
                    for (int i = 0; i < count.Length; i++)
                    {
                        if (count[i] <= 40)
                        {
                            ScanBarcodePosInfo samp = new ScanBarcodePosInfo();
                            samp.Disk = 1;
                            samp.Position = count[i] + 1;
                            //new Form1().SMPPositions.Enqueue(samp);
                            ScanBarcodePostEvent(samp);
                        }
                        else
                        {

                        }
                    }
                }
                else{
                    MessageBoxDraw.ShowMsg("请选择要扫描的位置！", MsgType.OK);
                    return;
                }
                //new Form1().SMPBarcodeSignal.Set();
                SMPBarcodeSignalEvent();
            }
        }
        /// <summary>
        /// 发送扫描样本仓要位置和盘号委托事件
        /// </summary>
        /// <param name="s"></param>
        public delegate void ScanBarcodePost(ScanBarcodePosInfo s);
        public event ScanBarcodePost ScanBarcodePostEvent;
        /// <summary>
        /// 发送样本扫码启动线程信号委托事件
        /// </summary>
        public delegate void SMPBarcodeSignal();
        public event SMPBarcodeSignal SMPBarcodeSignalEvent;

    }
}
