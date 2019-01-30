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
namespace BioA.UI
{
    public partial class ReagentSetting : DevExpress.XtraEditors.XtraUserControl
    {
        frmLoadingReagent frmloadingReagent;
        List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
        /// <summary>
        /// 存储客户端发送给服务器信息的集合
        /// </summary>
        private Dictionary<string, object[]> reagentDictionary = new Dictionary<string, object[]>();
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

        private void ReagentSetting_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(ReagentSettingLoad));
            
            dt1.Columns.Add("试剂名称");
            dt1.Columns.Add("试剂类型");
            dt1.Columns.Add("位置");
            dt1.Columns.Add("检测项目");
            dt1.Columns.Add("有效日期");
            dt1.Columns.Add("容器");
            dt1.Columns.Add("批号");
            //试剂1
            gridControl1.DataSource = dt1;
            this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[3].OptionsColumn.AllowEdit = false;
            //this.gridView1.Columns[4].OptionsColumn.AllowEdit = false;
            //this.gridView1.Columns[5].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[4].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[5].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[6].OptionsColumn.AllowEdit = false;

            dt2.Columns.Add("试剂名称");
            dt2.Columns.Add("试剂类型");
            dt2.Columns.Add("位置");
            dt2.Columns.Add("检测项目");
            dt2.Columns.Add("有效日期");
            dt2.Columns.Add("容器");
            dt2.Columns.Add("批号");
            //试剂2
            gridControl2.DataSource = dt2;
            this.gridView2.Columns[0].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[1].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[2].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[3].OptionsColumn.AllowEdit = false;
            //this.gridView2.Columns[4].OptionsColumn.AllowEdit = false;
            //this.gridView2.Columns[5].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[4].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[5].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[6].OptionsColumn.AllowEdit = false;

        }
        private void ReagentSettingLoad()
        {
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            gridView2.Appearance.HeaderPanel.Font = font;
            gridView2.Appearance.Row.Font = font;
            frmloadingReagent = new frmLoadingReagent();
            frmloadingReagent.GetsReagentEvent += GeTheReagentAfterPreservationEvent;

            lstReagentSettingsInfo = new BioA.Service.ReagentSetting().QueryReagentSettingsInfo("QueryReagentSetting1", "");
            //把获取到的数据绑定到gridControl1控件上,显示到界面
            //this.Invoke(new EventHandler(delegate { gridControl1.DataSource = lstReagentSettingsInfo; }));
            InitialReagentInfos(lstReagentSettingsInfo);

            lstReagentSettingsR2Info = new BioA.Service.ReagentSetting().QueryReagentSettingsInfo("QueryReagentSetting2", "");
            //把获取到的数据绑定到gridControl2控件上,显示到界面
            //this.Invoke(new EventHandler(delegate { gridControl2.DataSource = lstReagentSettingsR2Info; });
            InitialReagentInfos2(lstReagentSettingsR2Info);

            //reagentDictionary.Clear();
            //reagentDictionary.Add("QueryReagentSetting1", new object[] { "" });
            //reagentDictionary.Add("QueryReagentSetting2", new object[] { "" });
            //ClientSendToServicer(reagentDictionary);
        }
        /// <summary>
        /// 试剂保存成功后，触发事件执行的方法
        /// </summary>
        /// <param name="keyValuePairs"></param>
        private void GeTheReagentAfterPreservationEvent(Dictionary<string, ReagentSettingsInfo> keyValuePairs)
        {
            foreach (KeyValuePair<string, ReagentSettingsInfo> key in keyValuePairs)
            {
                if (key.Key == "R1")
                {
                    lstReagentSettingsInfo.Add(key.Value);
                    InitialReagentInfos(lstReagentSettingsInfo);
                }
                else
                {
                    lstReagentSettingsR2Info.Add(key.Value);
                    InitialReagentInfos2(lstReagentSettingsR2Info);
                }
                MessageBoxDraw.ShowMsgBox("成功装载试剂" + key.Key,"试剂装载",MsgType.OK);
            }
        }

        /// <summary>
        /// 绑定数据后，显示试剂2信息
        /// </summary>
        /// <param name="lstReagentSettingsInfo"></param>
        private void InitialReagentInfos2(List<ReagentSettingsInfo> lstReagentSettingsInfo)
        {
            this.Invoke(new EventHandler(delegate
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
            }));
        }
        /// <summary>
        /// 绑定数据后，显示试剂1信息
        /// </summary>
        /// <param name="lstReagentSettingsInfo"></param>
        private void InitialReagentInfos(List<ReagentSettingsInfo> lstReagentSettingsInfo)
        {
            this.Invoke(new EventHandler(delegate
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
            }));
        }
        List<ReagentSettingsInfo> lstReagentSettingsInfo = new List<ReagentSettingsInfo>();
        List<ReagentSettingsInfo> lstReagentSettingsR2Info = new List<ReagentSettingsInfo>();
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryReagentSetting1":
                    lstReagentSettingsInfo = (List<ReagentSettingsInfo>)XmlUtility.Deserialize(typeof(List<ReagentSettingsInfo>), sender as string);
                    //把获取到的数据绑定到gridControl1控件上,显示到界面
                    //this.Invoke(new EventHandler(delegate { gridControl1.DataSource = lstReagentSettingsInfo; }));
                    InitialReagentInfos(lstReagentSettingsInfo);
                    break;
                case "QueryReagentSetting2":
                    lstReagentSettingsR2Info = (List<ReagentSettingsInfo>)XmlUtility.Deserialize(typeof(List<ReagentSettingsInfo>), sender as string);
                    //把获取到的数据绑定到gridControl2控件上,显示到界面
                    //this.Invoke(new EventHandler(delegate { gridControl2.DataSource = lstReagentSettingsR2Info; });
                    InitialReagentInfos2(lstReagentSettingsR2Info);
                    break;
                case "reagentSettingAddR1":
                    if (sender as string == null)
                    {
                        MessageBoxDraw.ShowMsgBox("试剂R1装载失败！", "失败！", MsgType.OK);
                        this.Invoke(new EventHandler(delegate
                        {
                            frmloadingReagent.Close();
                        }));

                    }
                    else
                    {
                        frmloadingReagent.RecieveInfo = "R1";
                        this.Invoke(new EventHandler(delegate
                        {
                            frmloadingReagent.Close();
                        }));
                    }
                    break;
                case "reagentSettingAddR2":
                    if (sender as string == null)
                    {
                        MessageBoxDraw.ShowMsgBox("试剂R2装载失败！", "失败！", MsgType.OK);
                        this.Invoke(new EventHandler(delegate
                        {
                            frmloadingReagent.Close();
                        }));
                    }
                    else
                    {
                        frmloadingReagent.RecieveInfo = "R2";
                        this.Invoke(new EventHandler(delegate
                        {
                            frmloadingReagent.Close();
                        }));
                    }
                    break;
                case "DeleteReagentSettingsR1":
                    if ((int)sender > 0)
                    {
                        lstReagentSettingsInfo.RemoveAll(x => x.ProjectName == reagentSettingsInfo.ProjectName && x.ReagentName == reagentSettingsInfo.ReagentName);
                        InitialReagentInfos(lstReagentSettingsInfo);
                    }
                    else
                        MessageBox.Show("试剂1卸载失败！");
                    //ReagentSettingLoad();
                    break;
                case "DeleteReagentSettingsR2":
                    if ((int)sender > 0)
                    {
                        lstReagentSettingsR2Info.RemoveAll(x => x.ProjectName == reagentSettingsInfo.ProjectName && x.ReagentName == reagentSettingsInfo.ReagentName);
                        InitialReagentInfos2(lstReagentSettingsR2Info);
                    }
                    else
                        MessageBox.Show("试剂2卸载失败！");
                    //ReagentSettingLoad();
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
            frmloadingReagent.StartPosition = FormStartPosition.CenterScreen;
            frmloadingReagent.Text = "试剂装载R1";

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
            frmloadingReagent.LstUsedPos.Clear();
            frmloadingReagent.LstProjectName.Clear();
            frmloadingReagent.LstProjectName = lstProjectName;
            frmloadingReagent.LstUsedPos = lstPos;
            frmloadingReagent.LoadingReagentData();
            frmloadingReagent.ShowDialog();
        }
        /// <summary>
        /// 装载试剂2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frmloadingReagent.StartPosition = FormStartPosition.CenterScreen;
            frmloadingReagent.Text = "试剂装载R2";
            List<string> lstPos = new List<string>();
            foreach (ReagentSettingsInfo r in lstReagentSettingsR2Info)
            {
                lstPos.Add(r.Pos);
            } List<object> lstProjectName = new List<object>();
            foreach (ReagentSettingsInfo projectName in lstReagentSettingsR2Info)
            {
                lstProjectName.Add(projectName.ProjectName);
            }
            frmloadingReagent.LstUsedPos.Clear();
            frmloadingReagent.LstProjectName.Clear();
            frmloadingReagent.LstProjectName = lstProjectName;
            frmloadingReagent.LstUsedPos = lstPos;
            frmloadingReagent.LoadingReagentData();
            frmloadingReagent.ShowDialog();
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
                if (reagentSettingsInfo.ReagentName != null)
                {
                    //DataConfig.StrmethodName = "DeleteReagentSettingsR1";
                    //DataConfig.ObjParam = XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo);
                    reagentDictionary.Clear();
                    reagentDictionary.Add("DeleteReagentSettingsR1", new object[] { XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo) });
                    ClientSendToServicer(reagentDictionary);
                }

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
                if (reagentSettingsInfo.ReagentName != null)
                {
                    reagentDictionary.Clear();
                    reagentDictionary.Add("DeleteReagentSettingsR2", new object[] { XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo) });
                    ClientSendToServicer(reagentDictionary);
                }
            }
        }
    }
}
