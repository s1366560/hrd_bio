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

        /// <summary>
        /// 客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> combProDic = new Dictionary<string, object[]>();
        /// <summary>
        /// 项目信息集合
        /// </summary>
        private List<string> lstAssayProInfos = new List<string>();
        /// <summary>
        /// 存储所有组合项目绑定到数据表上
        /// </summary>
        DataTable dt = new DataTable();
        /// <summary>
        /// 保存所有组合项目名和项目名
        /// </summary>
        List<CombProjectInfo> lstProjectAndCombProName;
        /// <summary>
        /// 存储组合项目对应的项目名称
        /// </summary>
        private List<string> lstProName = new List<string>();
        public CombProject()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;

            dt.Columns.Add("编号");
            dt.Columns.Add("项目名称");
            dt.Columns.Add("组合项目数量");
            dt.Columns.Add("备注");
            this.lstvCombProject.DataSource = dt;
        }

        private void AssayProInfoForComb(object sender)
        {
            //string a = XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity);
            //serviceClient.ClientSendMsgToService(ModuleInfo.SettingsCombProject, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsCombProject, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
            var combProThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SettingsCombProject, sender as Dictionary<string, object[]>);
            });
            combProThread.IsBackground = true;
            combProThread.Start();
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
                    lstAssayProInfos.Clear();
                    lstAssayProInfos = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    InitialCombProInfos(lstAssayProInfos);
                    break;
                case "QueryProjectAndCombProName":
                    lstProjectAndCombProName = (List<CombProjectInfo>)XmlUtility.Deserialize(typeof(List<CombProjectInfo>), sender as string);
                    //combProjectPage1.SelectedProjects = lstProjects;
                    //combprojectPage2.SelectedProjects = lstProjects;
                    //combprojectPage3.SelectedProjects = lstProjects;
                    //combprojectPage4.SelectedProjects = lstProjects;
                    break;
                case "AddCombProjectName":
                    int strResult =(int)sender;
                    if (strResult == 0)
                    {
                        MessageBox.Show("添加失败！");
                        return;
                    }
                    else
                    {
                        //AssayProInfoForComb(new CommunicationEntity("QueryCombProjectNameAllInfo", null));
                        //InitialCombProInfos(lstAssayProInfos);
                        //this.Invoke(new EventHandler(delegate { 
                        //    txtCombProjectName.Text = string.Empty;
                        //    txtRemark.Text = string.Empty;
                        //}));
                        BeginInvoke(new Action(loadCombProject));
                        MessageBox.Show("添加成功！");

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
                        //AssayProInfoForComb(new CommunicationEntity("QueryCombProjectNameAllInfo", null));
                        //InitialCombProInfos(lstAssayProInfos);
                        //this.Invoke(new EventHandler(delegate
                        //{
                        //    txtCombProjectName.Text = string.Empty;
                        //    txtRemark.Text = string.Empty;
                        //}));
                        BeginInvoke(new Action(loadCombProject));
                        MessageBox.Show("更新成功！");
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
                        //AssayProInfoForComb(new CommunicationEntity("QueryCombProjectNameAllInfo", null));
                        //InitialCombProInfos(lstAssayProInfos);
                        //this.Invoke(new EventHandler(delegate
                        //{
                        //    txtCombProjectName.Text = string.Empty;
                        //    txtRemark.Text = string.Empty;
                        //}));
                        BeginInvoke(new Action(loadCombProject));
                        MessageBox.Show("删除成功！");
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 显示所有组合项目信息
        /// </summary>
        /// <param name="lstCombProInfos"></param>
        private void InitialCombProjectList(List<CombProjectInfo> lstCombProInfos)
        {
            //lstvCombProject
            BeginInvoke(new Action(() =>
            {
                lstvCombProject.RefreshDataSource();
                int i = 1;
                dt.Rows.Clear();
                if (lstCombProInfos.Count != 0)
                {
                    foreach (CombProjectInfo combProInfo in lstCombProInfos)
                    {
                        dt.Rows.Add(new object[] { i, combProInfo.CombProjectName, combProInfo.CombProjectCount, combProInfo.Remarks });

                        i++;
                    }
                }
                
                lstvCombProject_Click(null, null);
            }));

        }
        /// <summary>
        /// 显示所有项目名称
        /// </summary>
        /// <param name="lstAssayProInfos"></param>
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
        /// <summary>
        /// 页面点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            combProjectPage1 = new CombProjectPage1();
            combprojectPage2 = new CombProjectPage2();
            combprojectPage3 = new CombProjectPage3();
            combprojectPage4 = new CombProjectPage4();
            xtraTabPage1.Controls.Add(combProjectPage1);
            xtraTabPage2.Controls.Add(combprojectPage2);
            xtraTabPage3.Controls.Add(combprojectPage3);
            xtraTabPage4.Controls.Add(combprojectPage4);
            BeginInvoke(new Action(loadCombProject));
        }
        private void loadCombProject()
        {
            

            //AssayProInfoForComb(new CommunicationEntity("ProjectPageinfo", null));
            //AssayProInfoForComb(new CommunicationEntity("QueryCombProjectNameAllInfo", null));
            combProDic.Clear();
            //获取所有项目信息
            combProDic.Add("ProjectPageinfo", null);
            //获取所有组合项目名称和项目名称
            combProDic.Add("QueryProjectAndCombProName", null);
            //获取所有组合项目信息
            combProDic.Add("QueryCombProjectNameAllInfo", null);
            AssayProInfoForComb(combProDic);
        }
        /// <summary>
        /// 组合项目信息列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvCombProject_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                combProjectPage1.ResetControlState();
                combprojectPage2.ResetControlState();
                combprojectPage3.ResetControlState();
                combprojectPage4.ResetControlState();
                lstProName.Clear();
                string combProjectName = string.Empty;
                
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                this.Invoke(new EventHandler(delegate
                {
                    txtCombProjectName.Text = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                    txtRemark.Text = this.gridView1.GetRowCellValue(selectedHandle, "备注").ToString();
                }));
                combProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                foreach (CombProjectInfo combProInfo in lstProjectAndCombProName)
                {
                    if (combProInfo.CombProjectName == combProjectName)
                    {
                        lstProName.Add(combProInfo.ProjectName);
                    }
                }
                combProjectPage1.SelectedProjects = lstProName;
                combprojectPage2.SelectedProjects = lstProName;
                combprojectPage3.SelectedProjects = lstProName;
                combprojectPage4.SelectedProjects = lstProName;
                //CommunicationEntity communicationEntity = new CommunicationEntity();
                //communicationEntity.StrmethodName = "QueryProjectAndCombProName";
                //communicationEntity.ObjParam = combProject;
                //AssayProInfoForComb(communicationEntity);
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
                if (txtCombProjectName.Text == this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "项目名称").ToString())
                {
                    MessageBox.Show("新增的组合项目名已经存在！");
                    return;
                }
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

                //CommunicationEntity communicationEntity = new CommunicationEntity();
                //communicationEntity.StrmethodName = "AddCombProjectName";
                //communicationEntity.ObjParam = XmlUtility.Serializer(typeof(CombProjectInfo), combProInfo);
                combProDic.Clear();
                combProDic.Add("AddCombProjectName", new object[] { XmlUtility.Serializer(typeof(CombProjectInfo), combProInfo) });
                AssayProInfoForComb(combProDic);
                combProjectPage1.ResetControlState();
                combprojectPage2.ResetControlState();
                combprojectPage3.ResetControlState();
                combprojectPage4.ResetControlState();
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

                    //CommunicationEntity communicationEntity = new CommunicationEntity();
                    //communicationEntity.StrmethodName = "DeleteCombProjectName";
                    //communicationEntity.ObjParam = XmlUtility.Serializer(typeof(List<CombProjectInfo>), lstCombProInfos);
                    combProDic.Clear();
                    combProDic.Add("DeleteCombProjectName", new object[] { XmlUtility.Serializer(typeof(List<CombProjectInfo>), lstCombProInfos) });
                    AssayProInfoForComb(combProDic);
                }
            }
        }
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                string combProjectNameOld =this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "项目名称").ToString();
                //CommunicationEntity communicationEntity = new CommunicationEntity();
                //communicationEntity.StrmethodName = "UpdateCombProjectName";
                //communicationEntity.ObjParam = XmlUtility.Serializer(typeof(CombProjectInfo),new CombProjectInfo(){ CombProjectName = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "项目名称").ToString()});
                //communicationEntity.ObjLastestParam = XmlUtility.Serializer(typeof(CombProjectInfo), combProInfo);
                combProDic.Clear();
                combProDic.Add("UpdateCombProjectName", new object[] { combProjectNameOld, XmlUtility.Serializer(typeof(CombProjectInfo), combProInfo) });
                AssayProInfoForComb(combProDic);
            }
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //int selectedHandle;
            //selectedHandle = this.gridView1.GetSelectedRows()[0];
            //this.Invoke(new EventHandler(delegate
            //{
            //    txtCombProjectName.Text = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
            //    txtRemark.Text = this.gridView1.GetRowCellValue(selectedHandle, "备注").ToString();
            //}));


            //CommunicationEntity communicationEntity = new CommunicationEntity();
            //communicationEntity.StrmethodName = "QueryProjectByCombProName";
            //communicationEntity.ObjParam = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
            //AssayProInfoForComb(communicationEntity);
            this.txtCombProjectName.Text = "";
            this.txtRemark.Text = "";
            combProjectPage1.ResetControlState();
            combprojectPage2.ResetControlState();
            combprojectPage3.ResetControlState();
            combprojectPage4.ResetControlState();
        }
    }
}
