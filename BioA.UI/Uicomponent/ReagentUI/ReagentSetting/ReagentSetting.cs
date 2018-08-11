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

                CommunicationEntity DataConfig = new CommunicationEntity();
                ReagentSettingsInfo reagentSettingsInfo = new ReagentSettingsInfo();

                reagentSettingsInfo.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "检测项目").ToString();
                reagentSettingsInfo.ReagentName = this.gridView1.GetRowCellValue(selectedHandle, "试剂名称").ToString();
                if (reagentSettingsInfo.ReagentName != null)
                {
                    //DataConfig.StrmethodName = "DeleteReagentSettingsR1";
                    //DataConfig.ObjParam = XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo);
                    reagentDictionary.Clear();
                    reagentDictionary.Add("DeleteReagentSettingsR1", new object[] { XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo) });
                    ReagentSettingLoad(reagentDictionary);
                }

            }
        }

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

        private void ReagentSettingLoad(Dictionary<string, object[]> sender)
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

            //CommunicationEntity Reagent1 = new CommunicationEntity();
            //Reagent1.StrmethodName = "QueryReagentSetting1";
            //Reagent1.ObjParam = "";
            //ReagentSettingLoad(Reagent1);
            //CommunicationEntity Reagent2 = new CommunicationEntity();
            //Reagent2.StrmethodName = "QueryReagentSetting2";
            //Reagent2.ObjParam = "";
            //ReagentSettingLoad(Reagent2);
            reagentDictionary.Clear();
            reagentDictionary.Add("QueryReagentSetting1", new object[] { "" });
            reagentDictionary.Add("QueryReagentSetting2", new object[] { "" });
            ReagentSettingLoad(reagentDictionary);
        }

        private void InitialReagentInfos2(List<ReagentSettingsInfo> lstReagentSettingsInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridControl2.RefreshDataSource();

                //DataTable dt = new DataTable();
                //dt.Columns.Add("试剂名称");
                //dt.Columns.Add("试剂类型");
                //dt.Columns.Add("位置");
                //dt.Columns.Add("检测项目");
                //dt.Columns.Add("有效日期");
                //dt.Columns.Add("容器");
                //dt.Columns.Add("批号");
                foreach (ReagentSettingsInfo reagentSettingsInfo in lstReagentSettingsInfo)
                {
                    dt2.Rows.Add(new object[] { reagentSettingsInfo.ReagentName,reagentSettingsInfo.ReagentType ,reagentSettingsInfo.Pos, reagentSettingsInfo.ProjectName
                        //, reagentSettingsInfo.ResidualQuantity, reagentSettingsInfo.Measuredquantity
                        ,reagentSettingsInfo.ValidDate.ToString("yyyy-MM-dd"),reagentSettingsInfo.ReagentContainer,reagentSettingsInfo.BatchNum
                    });
                }
                
                
            }));
        }
        private void InitialReagentInfos(List<ReagentSettingsInfo> lstReagentSettingsInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridControl1.RefreshDataSource();
                //DataTable dt = new DataTable();
                //dt.Columns.Add("试剂名称");
                //dt.Columns.Add("试剂类型");
                //dt.Columns.Add("位置");
                //dt.Columns.Add("检测项目");
                //dt.Columns.Add("有效日期");
                //dt.Columns.Add("容器");
                //dt.Columns.Add("批号");
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
                    string str = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    if (str == "")
                    {
                        ReagentSettingLoad();
                        this.Invoke(new EventHandler(delegate
                        {
                            frmloadingReagent.Close();
                        }));

                    }
                    else
                        frmloadingReagent.RecieveInfo = str;
                    break;
                case "reagentSettingAddR2":
                    string str2 = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    if (str2 == "")
                    {
                        ReagentSettingLoad();
                        this.Invoke(new EventHandler(delegate
                        {
                            frmloadingReagent.Close();
                        }));
                    }
                    else
                        frmloadingReagent.RecieveInfo = str2;
                    break;
                case "DeleteReagentSettingsR1":
                    string DeleteCount = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    ReagentSettingLoad();
                    break;
                case "DeleteReagentSettingsR2":
                    string DeleteCount2 = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    ReagentSettingLoad();
                    break;
                case "QueryAssayProAllInfo":
                    lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    frmloadingReagent.AssayProjectInfo = lstAssayProInfos;
                    break;
            }
        }

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

                CommunicationEntity DataConfig = new CommunicationEntity();
                ReagentSettingsInfo reagentSettingsInfo = new ReagentSettingsInfo();
                int selectedHandle;

                selectedHandle = this.gridView2.GetSelectedRows()[0];
                reagentSettingsInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "检测项目").ToString();
                reagentSettingsInfo.ReagentName = this.gridView2.GetRowCellValue(selectedHandle, "试剂名称").ToString();
                if (reagentSettingsInfo.ReagentName != null)
                {
                    //DataConfig.StrmethodName = "DeleteReagentSettingsR2";
                    //DataConfig.ObjParam = XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo);
                    reagentDictionary.Clear();
                    reagentDictionary.Add("DeleteReagentSettingsR2", new object[] { XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo) });
                    ReagentSettingLoad(reagentDictionary);
                }
            }
        }
    }
}
