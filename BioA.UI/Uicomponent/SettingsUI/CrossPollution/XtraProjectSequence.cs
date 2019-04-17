using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BioA.Service;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using BioA.Common;

namespace BioA.UI
{
    public partial class XtraProjectSequence : DevExpress.XtraEditors.XtraForm
    {
        //存储改变顺序的项目测试顺序
        private List<ProjectRunSequenceInfo> lstProjectNames = new List<ProjectRunSequenceInfo>();
        
        
        DataTable dt = new DataTable();

        public XtraProjectSequence()
        {
            InitializeComponent();

            //this.CloseBox = false;                      //关闭按钮
            this.MaximizeBox = false;                   //最大化按钮
            this.MinimizeBox = false;                   //最小化按钮
            lstProjectTestSequence.RefreshDataSource();

            dt.Columns.Add("项目");
            lstProjectTestSequence.DataSource = dt;
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void XtraProjectSequence_Load(object sender, EventArgs e)
        {
            lstProjectNames.Clear();
            lstProjectNames = new SettingsReagentNeedle().QueryAllProjectRunSequenceInfo("QueryAllProjectRunSequenceInfo");
            ShowProjectSequence(lstProjectNames);
        }

        /// <summary>
        /// 显示项目测试顺序
        /// </summary>
        /// <param name="lstProjectName"></param>
        private void ShowProjectSequence(List<ProjectRunSequenceInfo> lstProjectName)
        {
            if (lstProjectName.Count > 0)
            {
                dt.Rows.Clear();
                foreach (ProjectRunSequenceInfo projectName in lstProjectName)
                {
                    dt.Rows.Add(projectName.ProjectName);
                }
                lstProjectTestSequence.DataSource = dt;
            }
        }

        private int index = 0;
        /// <summary>
        /// 上移按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simButShiftUp_Click(object sender, EventArgs e)
        {
            bool b = false;
            index = gridView1.GetSelectedRows()[0];
            if (index <= 0)
            {
                return;
            }
            int cur = index;
            ProjectRunSequenceInfo projectName = this.lstProjectNames.ElementAt(cur);
            this.lstProjectNames.RemoveAt(cur);
            this.lstProjectNames.Insert(cur - 1, projectName);
            ShowProjectSequence(lstProjectNames);
            RecordFocus(b);
        }
        /// <summary>
        /// 下移按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simButShiftDown_Click(object sender, EventArgs e)
        {
            bool b = true;
            index = gridView1.GetSelectedRows()[0];
            if (index >= lstProjectNames.Count - 1 || index < 0)
            {
                return;
            }
            int cur = index;
            ProjectRunSequenceInfo projectName = this.lstProjectNames.ElementAt(cur);
            this.lstProjectNames.RemoveAt(cur);
            this.lstProjectNames.Insert(cur + 1, projectName);
            ShowProjectSequence(lstProjectNames);
            this.RecordFocus(b);
        }
        /// <summary>
        /// 记录上次选择的焦点
        /// </summary>
        /// <param name="b"></param>
        private void RecordFocus(bool b)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                gridView1.UnselectRow(i);
            }
            if (b)
            {
                gridView1.FocusedRowHandle = index + 1;
                gridView1.SelectRow(index + 1);
            }
            else
            {
                gridView1.FocusedRowHandle = index - 1;
                gridView1.SelectRow(index - 1);
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
                        lstProjectTestSequence.Cursor = Cursors.Hand;

                    else
                        lstProjectTestSequence.Cursor = Cursors.Default;
                }
            }
        }
        //鼠标滑过gridview时，鼠标所指行显示浅蓝色
        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
            {
                e.Appearance.BackColor = Color.CornflowerBlue;
                e.Appearance.BackColor2 = Color.CornflowerBlue;
            }
        }
        //获取指定点的gridview视图坐标信息
        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));
            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simButCancel_Click(object sender, EventArgs e)
        {
            lstProjectNames.Clear();
            lstProjectNames = new SettingsReagentNeedle().QueryAllProjectRunSequenceInfo("QueryAllProjectRunSequenceInfo");
            ShowProjectSequence(lstProjectNames);
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simButSave_Click(object sender, EventArgs e)
        {
            SettingsReagentNeedle settingReagentNeedle = new SettingsReagentNeedle();
            settingReagentNeedle.DeleteAllProjectRunSequenceInfo("DeleteAllProjectRunSequenceInfo");
            string result = settingReagentNeedle.SaveProjectRunSequenceInfo("SaveProjectRunSequenceInfo", lstProjectNames);
            if (result != null)
            {
                this.Invoke(new EventHandler(delegate { MessageBox.Show(result); }));
            }
        }
    }
}