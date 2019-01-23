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
using BioA.Service;

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
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SampleDisk_Load(object sender, EventArgs e)
        {
            ComSampleNum.Items.AddRange(RunConfigureUtility.SamplePanel.ToArray());
            
            dt.Columns.Add("样本编号");
            dt.Columns.Add("样本申请时间");
            dt.Columns.Add("条码");
            dt.Columns.Add("项目计划");
            for (int i = 1; i < 120; i++)
            {
                dt.Rows.Add("","","","");
            }
            this.gridControl1.DataSource = dt;
            gridView1.Columns[0].Width = 30;
            gridView1.Columns[1].Width = 80;
            gridView1.Columns[2].Width = 100;
            gridView1.Columns[3].Width = 480;
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
                this.Invoke(new EventHandler(delegate 
                {
                    dt.Rows.Clear();
                    for (int i = 1; i <= 120; i++)
                    {
                        TaskInfo task = lstTaskInfos.SingleOrDefault(s => s.SampleNum == i);
                        if (task != null)
                        {
                            dt.Rows.Add(task.SampleNum, task.CreateDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), task.Barcode, task.ProjectName);
                        }
                        else
                            dt.Rows.Add("", "", "", "");
                    }
                    this.gridControl1.DataSource = dt;
                }));
            
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
                        task.CreateDate = Convert.ToDateTime(this.gridView1.GetRowCellValue(i, "样本申请时间"));
                        lstTaskInfo.Add(task);
                    }
                }

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

                MessageBoxDraw.ShowMsg(result, MsgType.OK);
            }
        }

    }
}
