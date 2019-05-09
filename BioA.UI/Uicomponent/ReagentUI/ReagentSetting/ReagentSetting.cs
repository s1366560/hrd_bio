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
using BioA.Common.IO;
using DevExpress.XtraGrid;
using System.Threading;
using BioA.Service;
using BioA.IBLL;
using BioA.BLL;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace BioA.UI
{
    public partial class ReagentSetting : DevExpress.XtraEditors.XtraUserControl
    {
        //试剂全开放界面
        frmLoadingReagent frmloadingReagent;
        //试剂本封闭界面
        LoadingReagentBlocking lReagentBlock;
        List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
        /// <summary>
        /// 存储客户端发送给服务器信息的集合
        /// </summary>
        private Dictionary<string, object[]> reagentDictionary = new Dictionary<string, object[]>();
        //配置试剂状态设置信息
        ReagentStateInfo rs;
        /// <summary>
        /// 保存试剂1设置信息
        /// </summary>
        DataTable dt1 = new DataTable();
        /// <summary>
        /// 保存试剂2设置信息
        /// </summary>
        DataTable dt2 = new DataTable();
        public ReagentSetting()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            gridView2.Appearance.HeaderPanel.Font = font;
            gridView2.Appearance.Row.Font = font;
            dt1.Columns.Add("试剂名称");
            dt1.Columns.Add("试剂类型");
            dt1.Columns.Add("位置");
            dt1.Columns.Add("检测项目");
            dt1.Columns.Add("有效日期");
            dt1.Columns.Add("容器");
            dt1.Columns.Add("批号");
            //试剂1
            gridControl1.DataSource = dt1;

            dt2.Columns.Add("试剂名称");
            dt2.Columns.Add("试剂类型");
            dt2.Columns.Add("位置");
            dt2.Columns.Add("检测项目");
            dt2.Columns.Add("有效日期");
            dt2.Columns.Add("容器");
            dt2.Columns.Add("批号");
            //试剂2
            gridControl2.DataSource = dt2;
        }
        
        private void ClientSendToServicer(Dictionary<string, object[]> sender)
        {
            var reagentSetThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.ReagentSetting, sender);
            });
            reagentSetThread.IsBackground = true;
            reagentSetThread.Start();
        }

        public void ReagentSetting_Load(object sender, EventArgs e)
        {
            lstReagentSettingsInfo.Clear();
            lstReagentSettingsR2Info.Clear();
            this.ReagentSettingLoad();
        }
        //试剂盘号
        private int Disk = 0;

        private void ReagentSettingLoad()
        {

            lstReagentSettingsInfo = new BioA.Service.ReagentSetting().QueryReagentSettingsInfo("QueryReagentSetting1", "");
            //把获取到的数据绑定到gridControl1控件上,显示到界面
            InitialReagentInfos(lstReagentSettingsInfo);

            lstReagentSettingsR2Info = new BioA.Service.ReagentSetting().QueryReagentSettingsInfo("QueryReagentSetting2", "");
            //把获取到的数据绑定到gridControl2控件上,显示到界面
            InitialReagentInfos2(lstReagentSettingsR2Info);
            rs = ReagentConfigInfoConstrunction.ReagentStateInfo;
            if (rs.ReagentStatusModule == 1)
            {
                if (frmloadingReagent == null)
                {
                    frmloadingReagent = new frmLoadingReagent();
                    frmloadingReagent.StartPosition = FormStartPosition.CenterScreen;
                    frmloadingReagent.GetsReagentEvent += GeTheReagentAfterPreservationEvent;
                }
            }
            else if (rs.ReagentStatusModule == 2)
            {
                if (lReagentBlock == null)
                {
                    lReagentBlock = new LoadingReagentBlocking();
                    lReagentBlock.StartPosition = FormStartPosition.CenterScreen;
                    lReagentBlock.ScannSingleReagentEvent += OnScannSingleReagentEvent;
                    lReagentBlock.InputReagentBarcodeEvent += OnInputReagentBarcodeEvent;
                    lReagentBlock.RefreshReagentInfoEvent += GeTheReagentAfterPreservationEvent;
                }
            }

        }

        public delegate void ReagentSettingHandler(object o);
        public event ReagentSettingHandler SendeScannReagentEvent;

        private void OnScannSingleReagentEvent(object sender)
        {
            if (SendeScannReagentEvent != null)
            {
                int[] s = new int[2];
                s[0] = this.Disk;
                s[1] =int.Parse(sender.ToString());
                SendeScannReagentEvent(s);
            }
        }

        private void OnInputReagentBarcodeEvent(object o)
        {
            int i = int.Parse(o.ToString());
            if (i == 1)
            {
                lstReagentSettingsInfo = new BioA.Service.ReagentSetting().QueryReagentSettingsInfo("QueryReagentSetting1", "");
                //把获取到的数据绑定到gridControl1控件上,显示到界面
                InitialReagentInfos(lstReagentSettingsInfo);

            }
            else if (i == 2)
            {
                lstReagentSettingsR2Info = new BioA.Service.ReagentSetting().QueryReagentSettingsInfo("QueryReagentSetting2", "");
                //把获取到的数据绑定到gridControl2控件上,显示到界面
                InitialReagentInfos2(lstReagentSettingsR2Info);

            }
        }
        /// <summary>
        /// 试剂保存成功后，触发事件执行的方法
        /// </summary>
        /// <param name="keyValuePairs"></param>
        private void GeTheReagentAfterPreservationEvent(int disk, ReagentSettingsInfo rs)
        {
            if (disk == 1)
            {
                lstReagentSettingsInfo.Add(rs);
                dt1.Rows.Add(new object[] { rs.ReagentName,rs.ReagentType ,rs.Pos, rs.ProjectName
                    //, reagentSettingsInfo.ResidualQuantity, reagentSettingsInfo.Measuredquantity
                    ,rs.ValidDate.ToString("yyyy-MM-dd"),rs.ReagentContainer,rs.BatchNum
                });
            }
            else if(disk == 2)
            {
                lstReagentSettingsR2Info.Add(rs);
                dt2.Rows.Add(new object[] { rs.ReagentName,rs.ReagentType ,rs.Pos, rs.ProjectName
                    //, reagentSettingsInfo.ResidualQuantity, reagentSettingsInfo.Measuredquantity
                    ,rs.ValidDate.ToString("yyyy-MM-dd"),rs.ReagentContainer,rs.BatchNum
                });
            }
            MessageBoxDraw.ShowMsgBox("试剂R" + disk + "装载完成！","试剂装载",MsgType.OK);
        }

        /// <summary>
        /// 绑定数据后，显示试剂2信息
        /// </summary>
        /// <param name="lstReagentSettingsInfo"></param>
        private void InitialReagentInfos2(List<ReagentSettingsInfo> lstReagentSettingsInfo)
        {
            gridControl2.RefreshDataSource();
            dt2.Rows.Clear();
            foreach (ReagentSettingsInfo reagentSettingsInfo in lstReagentSettingsInfo)
            {
                dt2.Rows.Add(new object[] { reagentSettingsInfo.ReagentName,reagentSettingsInfo.ReagentType ,reagentSettingsInfo.Pos, reagentSettingsInfo.ProjectName
                    //, reagentSettingsInfo.ResidualQuantity, reagentSettingsInfo.Measuredquantity
                    ,reagentSettingsInfo.ValidDate.ToString("yyyy-MM-dd"),reagentSettingsInfo.ReagentContainer,reagentSettingsInfo.BatchNum
                });
            }
        }
        /// <summary>
        /// 绑定数据后，显示试剂1信息
        /// </summary>
        /// <param name="lstReagentSettingsInfo"></param>
        private void InitialReagentInfos(List<ReagentSettingsInfo> lstReagentSettingsInfo)
        {
            gridControl1.RefreshDataSource();
            dt1.Rows.Clear();
            foreach (ReagentSettingsInfo reagentSettingsInfo in lstReagentSettingsInfo)
            {
                dt1.Rows.Add(new object[] { reagentSettingsInfo.ReagentName,reagentSettingsInfo.ReagentType ,reagentSettingsInfo.Pos, reagentSettingsInfo.ProjectName
                    //, reagentSettingsInfo.ResidualQuantity, reagentSettingsInfo.Measuredquantity
                    ,reagentSettingsInfo.ValidDate.ToString("yyyy-MM-dd"),reagentSettingsInfo.ReagentContainer,reagentSettingsInfo.BatchNum
                });
            }
        }

        private int iHandle = 1;

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
                    if (iHandle == 1)
                    {
                        gridView1.RefreshData();
                        gridView1.RefreshRow(hotTrackRow);
                    }
                    else
                    {
                        gridView2.RefreshData();
                        gridView2.RefreshRow(hotTrackRow);
                    }
                }
            }
        }

        /// <summary>
        /// 获取gridView1视图指定的坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            this.iHandle = 1;
            QuerMouseMove(sender, e);
        }
        /// <summary>
        /// 获取gridView2视图指定的坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_MouseMove(object sender, MouseEventArgs e)
        {
            this.iHandle = 2;
            QuerMouseMove(sender, e);
        }
        /// <summary>
        /// 鼠标滑过gridview1/gridview2时，鼠标所指行显示浅蓝色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)

                e.Appearance.BackColor = Color.CornflowerBlue;

        }
        /// <summary>
        /// get gridview2视图坐标信息/gridview1视图坐标信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuerMouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));
            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        List<ReagentSettingsInfo> lstReagentSettingsInfo = new List<ReagentSettingsInfo>();
        List<ReagentSettingsInfo> lstReagentSettingsR2Info = new List<ReagentSettingsInfo>();
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryReagentSetting1":
                    lstReagentSettingsInfo = (List<ReagentSettingsInfo>)XmlUtility.Deserialize(typeof(List<ReagentSettingsInfo>), sender as string);
                    InitialReagentInfos(lstReagentSettingsInfo);
                    break;
                case "QueryReagentSetting2":
                    lstReagentSettingsR2Info = (List<ReagentSettingsInfo>)XmlUtility.Deserialize(typeof(List<ReagentSettingsInfo>), sender as string);
                    InitialReagentInfos2(lstReagentSettingsR2Info);
                    break;
                case "DeleteReagentSettingsR1":
                    if ((int)sender > 0)
                    {
                        lstReagentSettingsInfo.RemoveAll(x => x.ProjectName == reagentSettingsInfo.ProjectName && x.ReagentName == reagentSettingsInfo.ReagentName);
                    }
                    else
                        MessageBox.Show("试剂1卸载失败！");
                    break;
                case "DeleteReagentSettingsR2":
                    if ((int)sender > 0)
                    {
                        lstReagentSettingsR2Info.RemoveAll(x => x.ProjectName == reagentSettingsInfo.ProjectName && x.ReagentName == reagentSettingsInfo.ReagentName);
                    }
                    else
                        MessageBox.Show("试剂2卸载失败！");
                    break;
                case "QueryAssayProAllInfo":
                    lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    frmloadingReagent.AssayProjectInfo = lstAssayProInfos;
                    break;
            }
        }

        /// <summary>
        /// 装载试剂1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadingReagent_Click(object sender, EventArgs e)
        {
            List<string> lstPos = new List<string>();
            foreach (ReagentSettingsInfo r in lstReagentSettingsInfo)
            {
                lstPos.Add(r.Pos);
            }
            List<object> lstProjectName = new List<object>();
            foreach (ReagentSettingsInfo projectName in lstReagentSettingsInfo)
            {
                lstProjectName.Add(projectName.ProjectName);
            }
            if (rs.ReagentStatusModule == 1)
            {
                frmloadingReagent.Text = "试剂装载R1";

                frmloadingReagent.LstUsedPos.Clear();
                frmloadingReagent.LstProjectName.Clear();
                frmloadingReagent.LstProjectName = lstProjectName;
                frmloadingReagent.LstUsedPos = lstPos;
                frmloadingReagent.LoadingReagentData();
                frmloadingReagent.frmLoadingReagent_Load(null,null);
                frmloadingReagent.ShowDialog();
            }
            if (rs.ReagentStatusModule == 2)
            {
                lReagentBlock.ReagentDisk = this.Disk = 1;
                lReagentBlock.Text = "试剂条码装载R1";
                lReagentBlock.LstProjectName = lstProjectName;
                lReagentBlock.LstPos = lstPos;
                lReagentBlock.LoadingReagentBlocking_Load(null,null);
                lReagentBlock.ShowDialog();
            }
        }
        /// <summary>
        /// 装载试剂2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            List<string> lstPos = new List<string>();
            foreach (ReagentSettingsInfo r in lstReagentSettingsR2Info)
            {
                lstPos.Add(r.Pos);
            } List<object> lstProjectName = new List<object>();
            foreach (ReagentSettingsInfo projectName in lstReagentSettingsR2Info)
            {
                lstProjectName.Add(projectName.ProjectName);
            }
            if (rs.ReagentStatusModule == 1)
            {
                
                frmloadingReagent.Text = "试剂装载R2";
                frmloadingReagent.LstUsedPos.Clear();
                frmloadingReagent.LstProjectName.Clear();
                frmloadingReagent.LstProjectName = lstProjectName;
                frmloadingReagent.LstUsedPos = lstPos;
                frmloadingReagent.LoadingReagentData();
                frmloadingReagent.frmLoadingReagent_Load(null, null);
                frmloadingReagent.ShowDialog();
            }
            else if (rs.ReagentStatusModule == 2)
            {
                lReagentBlock.ReagentDisk = this.Disk = 2;
                lReagentBlock.Text = "试剂条码装载R2";
                lReagentBlock.LstProjectName = lstProjectName;
                lReagentBlock.LstPos = lstPos;
                lReagentBlock.LoadingReagentBlocking_Load(null, null);
                lReagentBlock.ShowDialog();
            }
        }
        //存储删除试剂的信息
        private ReagentSettingsInfo reagentSettingsInfo;

        /// <summary>
        /// 卸载试剂1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnloadReagent_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                DialogResult result = MessageBoxDraw.ShowMsg("是否确认卸载试剂！", MsgType.YesNo);
                if (result == DialogResult.Yes)
                {
                }
                else
                {
                    return;
                }
                reagentSettingsInfo = new ReagentSettingsInfo();

                reagentSettingsInfo.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "检测项目").ToString();
                reagentSettingsInfo.ReagentName = this.gridView1.GetRowCellValue(selectedHandle, "试剂名称").ToString();
                reagentSettingsInfo.ReagentType = this.gridView1.GetRowCellValue(selectedHandle, "试剂类型").ToString();
                reagentSettingsInfo.Pos = this.gridView1.GetRowCellValue(selectedHandle, "位置").ToString();
                reagentDictionary.Clear();
                reagentDictionary.Add("DeleteReagentSettingsR1", new object[] { XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo) });
                ClientSendToServicer(reagentDictionary);
                dt1.Rows.Remove(dt1.Rows[selectedHandle]);
            }
        }
        /// <summary>
        /// 卸载试剂2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridView2.SelectedRowsCount > 0)
            {
                DialogResult result = MessageBoxDraw.ShowMsg("是否确认卸载试剂！", MsgType.YesNo);
                if (result == DialogResult.Yes)
                {

                }
                else
                {
                    return;
                }
                reagentSettingsInfo = new ReagentSettingsInfo();
                int selectedHandle;

                selectedHandle = this.gridView2.GetSelectedRows()[0];
                reagentSettingsInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "检测项目").ToString();
                reagentSettingsInfo.ReagentName = this.gridView2.GetRowCellValue(selectedHandle, "试剂名称").ToString();
                reagentSettingsInfo.ReagentType = this.gridView2.GetRowCellValue(selectedHandle, "试剂类型").ToString();
                reagentSettingsInfo.Pos = this.gridView2.GetRowCellValue(selectedHandle, "位置").ToString();
                reagentDictionary.Clear();
                reagentDictionary.Add("DeleteReagentSettingsR2", new object[] { XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo) });
                ClientSendToServicer(reagentDictionary);
                dt2.Rows.Remove(dt2.Rows[selectedHandle]);
            }
        }

    }
}
