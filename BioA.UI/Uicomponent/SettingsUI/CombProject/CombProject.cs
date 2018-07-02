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
using BioA.UI;
using BioA.UI.ServiceReference1;
using BioA.Common.IO;
using BioA.Common;
using System.ServiceModel;
using System.Threading;

namespace BioA.UI
{
    public partial class CombProject : DevExpress.XtraEditors.XtraUserControl
    {
        CombProjectPage1 combProjectPage1;
        CombProjectPage2 combprojectPage2;
        CombProjectPage3 combprojectPage3;
        CombProjectPage4 combprojectPage4;

        private List<string> lstAssayProInfos = new List<string>();

        public CombProject()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;

            combProjectPage1 = new CombProjectPage1();

            combprojectPage2 = new CombProjectPage2();
            combprojectPage3 = new CombProjectPage3();
            combprojectPage4 = new CombProjectPage4();
        }

        private void AssayProInfoForComb(object sender)
        {
            string a = XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity);
            //serviceClient.ClientSendMsgToService(ModuleInfo.SettingsCombProject, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsCombProject, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryCombProjectNameAllInfo":
                    List<CombProjectInfo> lstCombProInfos = (List<CombProjectInfo>)XmlUtility.Deserialize(typeof(List<CombProjectInfo>), sender as string);
                    InitialCombProjectList(lstCombProInfos);
                    break;
                case "ProjectPageinfo":
                    lstAssayProInfos = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    InitialCombProInfos(lstAssayProInfos);
                    break;
                case "QueryProjectByCombProName":
                    List<string> lstProjects = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    combProjectPage1.SelectedProjects = lstProjects;
                    combprojectPage2.SelectedProjects = lstProjects;
                    combprojectPage3.SelectedProjects = lstProjects;
                    combprojectPage4.SelectedProjects = lstProjects;
                    break;
                case "AddCombProjectName":
                    string strResult = sender as string;
                    if (strResult == "项目已存在！")
                    {
                        MessageBox.Show("该项目已存在，请修改组合项目名称!");
                        return;
                    }
                    else
                    {
                        AssayProInfoForComb(new CommunicationEntity("QueryCombProjectNameAllInfo", null));
                        InitialCombProInfos(lstAssayProInfos);
                        this.Invoke(new EventHandler(delegate { 
                            txtCombProjectName.Text = string.Empty;
                            txtRemark.Text = string.Empty;
                        }));
                    }
                    break;
                case "UpdateCombProjectName":
                    int intUpdateResult = (int)sender;
                    if (intUpdateResult <= 0)
                    {
                        MessageBox.Show("更新失败！");
                    }
                    else
                    {
                        AssayProInfoForComb(new CommunicationEntity("QueryCombProjectNameAllInfo", null));
                        InitialCombProInfos(lstAssayProInfos);
                        this.Invoke(new EventHandler(delegate
                        {
                            txtCombProjectName.Text = string.Empty;
                            txtRemark.Text = string.Empty;
                        }));
                    }
                    break;
                case "DeleteCombProjectName":
                    int intResult = (int)sender;
                    if (intResult <= 0)
                    {
                        MessageBox.Show("删除失败！");
                        return;
                    }
                    else
                    {
                        AssayProInfoForComb(new CommunicationEntity("QueryCombProjectNameAllInfo", null));
                        InitialCombProInfos(lstAssayProInfos);
                        this.Invoke(new EventHandler(delegate
                        {
                            txtCombProjectName.Text = string.Empty;
                            txtRemark.Text = string.Empty;
                        }));
                    }
                    break;
                default:
                    break;
            }
        }

        private void InitialCombProjectList(List<CombProjectInfo> lstCombProInfos)
        {
            //lstvCombProject
            this.Invoke(new EventHandler(delegate
                {
                    lstvCombProject.RefreshDataSource();
                    int i = 1;
                    DataTable dt = new DataTable();

                    dt.Columns.Add("编号");
                    dt.Columns.Add("项目名称");
                    dt.Columns.Add("组合项目数量");
                    dt.Columns.Add("备注");
                    if (lstCombProInfos.Count != 0)
                    {
                        foreach (CombProjectInfo combProInfo in lstCombProInfos)
                        {
                            dt.Rows.Add(new object[] { i, combProInfo.CombProjectName, combProInfo.CombProjectCount, combProInfo.Remarks });

                            i++;
                        }
                    }
                    this.lstvCombProject.DataSource = dt;
                    lstvCombProject_Click(null, null);
                }));

        }

        private void InitialCombProInfos(List<string> lstAssayProInfos)
        {
            combProjectPage1.ResetControlState();
            combprojectPage2.ResetControlState();
            combprojectPage3.ResetControlState();
            combprojectPage4.ResetControlState();

            combProjectPage1.LstAssayProInfos = lstAssayProInfos;
            combprojectPage2.LstAssayProInfos = lstAssayProInfos;
            combprojectPage3.LstAssayProInfos = lstAssayProInfos;
            combprojectPage4.LstAssayProInfos = lstAssayProInfos;
            this.Invoke(new EventHandler(delegate {
                xtraTabControl1.SelectedTabPageIndex = 0;
            }));
            
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            List<string> lstSelectInfo = new List<string>();
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                if (!xtraTabPage1.Controls.Contains(combProjectPage1))
                    xtraTabPage1.Controls.Add(combProjectPage1);
                lstSelectInfo = combProjectPage1.GetSelectedProjects();
                combProjectPage1.LstAssayProInfos = lstAssayProInfos;
                combProjectPage1.SelectedProjects = lstSelectInfo;
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                if (!xtraTabPage2.Controls.Contains(combprojectPage2))
                    xtraTabPage2.Controls.Add(combprojectPage2);
                lstSelectInfo = combprojectPage2.GetSelectedProjects();
                combprojectPage2.LstAssayProInfos = lstAssayProInfos;
                combprojectPage2.SelectedProjects = lstSelectInfo;
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                if (!xtraTabPage3.Controls.Contains(combprojectPage3))
                    xtraTabPage3.Controls.Add(combprojectPage3);
                lstSelectInfo = combprojectPage3.GetSelectedProjects();
                combprojectPage3.LstAssayProInfos = lstAssayProInfos;
                combprojectPage3.SelectedProjects = lstSelectInfo;
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                if (!xtraTabPage4.Controls.Contains(combprojectPage4))
                    xtraTabPage4.Controls.Add(combprojectPage4);
                lstSelectInfo = combprojectPage4.GetSelectedProjects();
                combprojectPage4.LstAssayProInfos = lstAssayProInfos;
                combprojectPage4.SelectedProjects = lstSelectInfo;
            }
        }

        private void CombProject_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadCombProject));
            
        }
        private void loadCombProject()
        {
            xtraTabPage1.Controls.Add(combProjectPage1);
            xtraTabPage2.Controls.Add(combprojectPage2);
            xtraTabPage3.Controls.Add(combprojectPage3);
            xtraTabPage4.Controls.Add(combprojectPage4);

            AssayProInfoForComb(new CommunicationEntity("ProjectPageinfo", null));
            AssayProInfoForComb(new CommunicationEntity("QueryCombProjectNameAllInfo", null));

        }

        private void lstvCombProject_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                combProjectPage1.ResetControlState();
                combprojectPage2.ResetControlState();
                combprojectPage3.ResetControlState();
                combprojectPage4.ResetControlState();

                string combProject = string.Empty;
                
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                this.Invoke(new EventHandler(delegate
                {
                    txtCombProjectName.Text = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                    txtRemark.Text = this.gridView1.GetRowCellValue(selectedHandle, "备注").ToString();
                }));
                

                combProject = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                CommunicationEntity communicationEntity = new CommunicationEntity();
                communicationEntity.StrmethodName = "QueryProjectByCombProName";
                communicationEntity.ObjParam = combProject;
                AssayProInfoForComb(communicationEntity);
            }
        }
        /// <summary>
        /// 添加组合项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdded_Click(object sender, EventArgs e)
        {
            if (txtCombProjectName.Text.Trim() != string.Empty)
            {
                CombProjectInfo combProInfo = new CombProjectInfo();
                combProInfo.CombProjectName = txtCombProjectName.Text.Trim();
                List<string> lstProInfos = new List<string>();  // 被选中项目集合
                if (combProjectPage1.GetSelectedProjects().Count > 0)
                {
                    lstProInfos.AddRange(combProjectPage1.GetSelectedProjects());
                }

                if (combprojectPage2.GetSelectedProjects().Count > 0)
                {
                    lstProInfos.AddRange(combprojectPage2.GetSelectedProjects());
                }

                if (combprojectPage3.GetSelectedProjects().Count > 0)
                {
                    lstProInfos.AddRange(combprojectPage3.GetSelectedProjects());
                }

                if (combprojectPage4.GetSelectedProjects().Count > 0)
                {
                    lstProInfos.AddRange(combprojectPage4.GetSelectedProjects());
                }

                combProInfo.ProjectNames = lstProInfos;
                combProInfo.CombProjectCount = combProInfo.ProjectNames.Count;
                combProInfo.Remarks = txtRemark.Text.Trim();

                CommunicationEntity communicationEntity = new CommunicationEntity();
                communicationEntity.StrmethodName = "AddCombProjectName";
                communicationEntity.ObjParam = XmlUtility.Serializer(typeof(CombProjectInfo), combProInfo);
                AssayProInfoForComb(communicationEntity);
            }
        }
        /// <summary>
        /// 删除组合项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0 && txtCombProjectName.Text.Trim() != null)
            {
                if (MessageBoxDraw.ShowMsg("是否确认删除组合项目？", MsgType.Question) == DialogResult.OK)
                {
                    CombProjectInfo combProInfo = new CombProjectInfo();

                    combProInfo.CombProjectName = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "项目名称").ToString();

                    List<CombProjectInfo> lstCombProInfos = new List<CombProjectInfo>();
                    lstCombProInfos.Add(combProInfo);

                    CommunicationEntity communicationEntity = new CommunicationEntity();
                    communicationEntity.StrmethodName = "DeleteCombProjectName";
                    communicationEntity.ObjParam = XmlUtility.Serializer(typeof(List<CombProjectInfo>), lstCombProInfos);
                    AssayProInfoForComb(communicationEntity);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0 && txtCombProjectName.Text != null)
            {
                CombProjectInfo combProInfo = new CombProjectInfo();
                if (txtCombProjectName.Text.Trim() == "")
                {
                    MessageBoxDraw.ShowMsg("请输入组合项目名称！", MsgType.Warning);
                    return;
                }
                combProInfo.CombProjectName = txtCombProjectName.Text.Trim();
                List<string> lstProInfos = new List<string>();  // 被选中项目集合
                if (combProjectPage1.GetSelectedProjects().Count > 0)
                {
                    lstProInfos.AddRange(combProjectPage1.GetSelectedProjects());
                }

                if (combprojectPage2.GetSelectedProjects().Count > 0)
                {
                    lstProInfos.AddRange(combprojectPage2.GetSelectedProjects());
                }

                if (combprojectPage3.GetSelectedProjects().Count > 0)
                {
                    lstProInfos.AddRange(combprojectPage3.GetSelectedProjects());
                }

                if (combprojectPage4.GetSelectedProjects().Count > 0)
                {
                    lstProInfos.AddRange(combprojectPage4.GetSelectedProjects());
                }

                combProInfo.ProjectNames = lstProInfos;
                combProInfo.CombProjectCount = combProInfo.ProjectNames.Count;
                combProInfo.Remarks = txtRemark.Text.Trim();

                CommunicationEntity communicationEntity = new CommunicationEntity();
                communicationEntity.StrmethodName = "UpdateCombProjectName";
                communicationEntity.ObjParam = XmlUtility.Serializer(typeof(CombProjectInfo),new CombProjectInfo(){ CombProjectName = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "项目名称").ToString()});
                communicationEntity.ObjLastestParam = XmlUtility.Serializer(typeof(CombProjectInfo), combProInfo);
                AssayProInfoForComb(communicationEntity);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int selectedHandle;
            selectedHandle = this.gridView1.GetSelectedRows()[0];
            this.Invoke(new EventHandler(delegate
            {
                txtCombProjectName.Text = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                txtRemark.Text = this.gridView1.GetRowCellValue(selectedHandle, "备注").ToString();
            }));


            CommunicationEntity communicationEntity = new CommunicationEntity();
            communicationEntity.StrmethodName = "QueryProjectByCombProName";
            communicationEntity.ObjParam = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
            AssayProInfoForComb(communicationEntity);
        }
    }
}
