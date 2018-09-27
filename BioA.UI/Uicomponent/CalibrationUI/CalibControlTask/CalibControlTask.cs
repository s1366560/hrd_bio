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
using System.Threading;

namespace BioA.UI
{
    public partial class CalibControlTask : DevExpress.XtraEditors.XtraUserControl
    {
        CalibProjectPage1 projectPage1;
        CalibProjectPage2 projectPage2;
        CalibProjectPage3 projectPage3;
        CalibProjectPage4 projectPage4;
        CalibProCombPage2 calibProCombPage2;
        CalibProCombPage1 calibProCombPage1;
        private List<string> lstAssayProInfos = new List<string>();
        private List<string[]> lstQCRelateProject = new List<string[]>();
        //private List<string[]> lstQCRelateProject1 = new List<string[]>();
        /// <summary>
        /// 存储所有组合项目对应的生化项目信息
        /// </summary>
        private List<string> lstProjects = new List<string>();
        /// <summary>
        /// 存储组合项目名和项目名
        /// </summary>
        private List<CombProjectInfo> lstCombProInfo = new List<CombProjectInfo>();
        //样本编号
        int intPos = 0;

        /// <summary>
        /// 传递访问数据的方法名和参数个数的泛型集合
        /// </summary>
        Dictionary<string, object[]> calibDictionary = new Dictionary<string, object[]>();
        public CalibControlTask()
        {
            InitializeComponent();
            
            
        }
        //加载页面
        private void CalibControlTask_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(CalibControlTaskInit));
            
        }
        private void CalibControlTaskInit()
        {
            projectPage1 = new CalibProjectPage1();
            projectPage2 = new CalibProjectPage2();
            projectPage3 = new CalibProjectPage3();
            projectPage4 = new CalibProjectPage4();
            calibProCombPage1 = new CalibProCombPage1();
            calibProCombPage2 = new CalibProCombPage2();
            calibProCombPage1.clickCombProNameEvent += HandleClickCombProNameEvent;
            calibProCombPage2.clickCombProNamePage2Event += HandleClickCombProNameEvent;
            xtraTabPage1.Controls.Add(projectPage1);
            xtraTabPage2.Controls.Add(projectPage2);
            xtraTabPage3.Controls.Add(projectPage3);
            xtraTabPage4.Controls.Add(projectPage4);
            xtraTabPage5.Controls.Add(calibProCombPage1);
            xtraTabPage6.Controls.Add(calibProCombPage2);
            combSampleType.Properties.Items.AddRange(RunConfigureUtility.SampleTypes);
            combSampleType.SelectedIndex = 1;
            
            ////获取任务信息
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryCalibratorinfoTask", null)));
            ////获取所有组合项目信息
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryCombProjectNameAllInfo", null)));
            var calibThread = new Thread(() =>
            {
                //获取所有任务信息
                calibDictionary.Add("QueryCalibratorinfoTask", new object[] { "" });
                //获取所有组合项目信息
                calibDictionary.Add("QueryCombProjectNameAllInfo", new object[] { "" });
                //获取所有组合项目名和项目名
                calibDictionary.Add("QueryProjectAndCombProName", null);
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.CalibControlTask, calibDictionary);
            });
            calibThread.IsBackground = true;
            calibThread.Start();
        }
        /// <summary>
        /// 处理组合页面委托事件传递过来的数据
        /// </summary>
        /// <param name="sender"></param>
        private void HandleClickCombProNameEvent(string sender)
        {
            lstProjects.Clear();
            foreach (CombProjectInfo combProInfo in lstCombProInfo)
            {
                if (combProInfo.CombProjectName == sender)
                {
                    lstProjects.Add(combProInfo.ProjectName);
                }
            }
            if (lstProjects.Count > 0)
            {
                projectPage1.SelectedProjects = lstProjects;
                projectPage2.SelectedProjects = lstProjects;
                projectPage3.SelectedProjects = lstProjects;
                projectPage4.SelectedProjects = lstProjects;
            }
        }

        /// <summary>
        /// xtratabcontrol控件的标签页点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grpProject_Click(object sender, EventArgs e)   
        {
            if (grpProject.SelectedTabPageIndex == 0)
            {
                if (!xtraTabPage1.Controls.Contains(projectPage1))
                {
                    xtraTabPage1.Controls.Add(projectPage1);
                }
            }
            else if (grpProject.SelectedTabPageIndex == 1)
            {
                if (!xtraTabPage2.Controls.Contains(projectPage2))
                {
                    xtraTabPage2.Controls.Add(projectPage2);
                }
            }
            else if (grpProject.SelectedTabPageIndex == 2)
            {
                if (!xtraTabPage3.Controls.Contains(projectPage3))
                {
                    xtraTabPage3.Controls.Add(projectPage3);
                }
            }
            else if (grpProject.SelectedTabPageIndex == 3)
            {
                if (!xtraTabPage4.Controls.Contains(projectPage4))
                {
                    xtraTabPage4.Controls.Add(projectPage4);
                }
            }
        }

        /// <summary>
        /// 显示校准任务信息
        /// </summary>
        /// <param name="lstCalibrationCurveInfo"></param>
        private void GridAdd(List<CalibratorinfoTask> lstCalibrationCurveInfo)
        {           
            lstvTask.RefreshDataSource();
            this.BeginInvoke(new EventHandler(delegate
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("顺序号");
                dt.Columns.Add("项目名称");
                dt.Columns.Add("校准品名");
                dt.Columns.Add("重复次数");
                dt.Columns.Add("状态");
                foreach (CalibratorinfoTask task in lstCalibrationCurveInfo)
                {
                    string tasks = string.Empty;
                    switch (task.TaskState)
                    {
                        case 0:
                            tasks = "待测";
                            break;
                        case 1:
                            tasks = "检测中";
                            break;
                        case 2:
                            tasks = "已完成";
                            break;
                        case 3:
                            tasks = "任务被终止";
                            break;
                    }
                    dt.Rows.Add(new object[] { task.SampleNum,task.ProjectName, task.CalibName, task.InspectTimes, tasks});
                }
                this.lstvTask.DataSource = dt;
                this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[3].OptionsColumn.AllowEdit = false; 
            }));
        }
        #region 接收样本类型获取到的所有项目名称
        /// <summary>
        /// 保存样本是血清的所有项目
        /// </summary>
        List<string[]> lstSerumProject = new List<string[]>();
        /// <summary>
        /// 保存样本是尿液的所有项目
        /// </summary>
        List<string[]> lstUrinePeoject = new List<string[]>();
        /// <summary>
        /// 保存样本为 空 的所有项目
        /// </summary>
        List<string[]> lstBlankProject = new List<string[]>();
        #endregion;
        /// <summary>
        /// 存储所有的组合项目名
        /// </summary>
        List<string> lstCombProName = new List<string>();
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryCalibratorinfoTask":
                    List<CalibratorinfoTask> lstCalibrationCurveInfo = (List<CalibratorinfoTask>)XmlUtility.Deserialize(typeof(List<CalibratorinfoTask>), sender as string);
                    int max = 0;
                    for (int i = 0; i < lstCalibrationCurveInfo.Count; i++)
                    {
                        int str = System.Convert.ToInt32(lstCalibrationCurveInfo[i].SampleNum.Substring(1));
                        if (str > max)
                        { 
                            max = str;
                        }
                    }
                    this.Invoke(new EventHandler(delegate { txtSumpleNum.Text = "S" + (max + 1).ToString(); }));
                   
                    GridAdd(lstCalibrationCurveInfo);
                    //保存任务之后加载校准品编号
                    
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryBigestCalibCTaskInfoForToday", null)));
                    ////保存任务之后加载项目信息
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity),
                    //        new CommunicationEntity("QueryProjectNameInfoByCalib", combSampleType.SelectedItem.ToString())));
                    break;
                //case "QueryAssayProNameAllInfo":
                //    lstAssayProInfos = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                //    projectPage1.LstAssayProInfos = lstAssayProInfos;
                //    projectPage2.LstAssayProInfos = lstAssayProInfos;
                //    projectPage3.LstAssayProInfos = lstAssayProInfos;
                //    projectPage4.LstAssayProInfos = lstAssayProInfos;
                //    //获取项目是否可用，不能用就（提示错误信息），能用显示（黑字体）
                //    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity),
                //            new CommunicationEntity("QueryProjectNameInfoByCalib", combSampleType.SelectedItem.ToString())));
                //    break;
                case "QueryCombProjectNameAllInfo":
                    lstCombProName = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    calibProCombPage1.LstProjectGroups = lstCombProName;
                    calibProCombPage2.LstAssayProInfos = lstCombProName;

                    break;
                //case "QueryProjectByCombProName":
                //    lstProjects = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                //    projectPage1.SelectedProjects = lstProjects;
                //    projectPage2.SelectedProjects = lstProjects;
                //    projectPage3.SelectedProjects = lstProjects;
                //    projectPage4.SelectedProjects = lstProjects;
                //    break;
                case "QueryProjectAndCombProName":
                    lstCombProInfo = (List<CombProjectInfo>)XmlUtility.Deserialize(typeof(List<CombProjectInfo>), sender as string);
                    break;
                //case "QueryAssayProNameAll":
                //    List<CalibratorinfoTask> lstCalibrationInfo = (List<CalibratorinfoTask>)XmlUtility.Deserialize(typeof(List<CalibratorinfoTask>), sender as string);
                //    GridAdd(lstCalibrationInfo);
                //    break;
                //case "QueryBigestCalibCTaskInfoForToday":
                //    intPos = (int)sender;
                //    this.Invoke(new EventHandler(delegate { txtSumpleNum.Text = "S" + (intPos + 1).ToString(); }));
                //    break;
                case "QueryProjectNameInfoByCalib":
                    lstQCRelateProject = (List<string[]>)XmlUtility.Deserialize(typeof(List<string[]>), sender as string);

                    grpProject.SelectedTabPageIndex = 0;
                    projectPage1.LstAssayProInfos = lstQCRelateProject;
                    projectPage2.LstAssayProInfos = lstQCRelateProject;
                    projectPage3.LstAssayProInfos = lstQCRelateProject;
                    projectPage4.LstAssayProInfos = lstQCRelateProject;
                    if (combSampleType.SelectedItem.ToString() == sampleSerum)
                    {
                        lstSerumProject = lstQCRelateProject;
                    }
                    else if (combSampleType.SelectedItem.ToString() == sampleUrine)
                    {
                        lstUrinePeoject = lstQCRelateProject;
                    }
                    else if (combSampleType.SelectedItem.ToString().Trim() == sampleBlank)
                    {
                        lstBlankProject = lstQCRelateProject;
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 保存校准任务点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryCalibrationCurveInfo", "血清")));
            
            if (projectPage1.GetSelectedProjects().Count == 0 && 
                projectPage2.GetSelectedProjects().Count == 0 &&
                projectPage3.GetSelectedProjects().Count == 0 &&
                projectPage4.GetSelectedProjects().Count == 0)
            {
                MessageBox.Show("请选择校准项目！");
                return;            
            }
            if (intPos >= System.Convert.ToInt32(txtSumpleNum.Text.Substring(1)))
            {
                MessageBox.Show("此任务已经存在！");
                return;
            }
            
            List<CalibratorinfoTask> lstCalibratorinfoTask = new List<CalibratorinfoTask>();
            DateTime dt = DateTime.Now;
            foreach (string qcName in projectPage1.GetSelectedProjects())
            {
                CalibratorinfoTask qcTaskInfo = new CalibratorinfoTask();                
                qcTaskInfo.CreateDate = dt;
                qcTaskInfo.ProjectName = qcName;
                qcTaskInfo.SampleType = combSampleType.Text;
                qcTaskInfo.SampleNum = txtSumpleNum.Text;
                lstCalibratorinfoTask.Add(qcTaskInfo);
            }
            foreach (string qcName in projectPage2.GetSelectedProjects())
            {
                CalibratorinfoTask qcTaskInfo = new CalibratorinfoTask();
                qcTaskInfo.CreateDate = dt;
                qcTaskInfo.ProjectName = qcName;
                qcTaskInfo.SampleType = combSampleType.Text;
                qcTaskInfo.SampleNum = txtSumpleNum.Text;
                lstCalibratorinfoTask.Add(qcTaskInfo);
            }
            foreach (string qcName in projectPage3.GetSelectedProjects())
            {
                CalibratorinfoTask qcTaskInfo = new CalibratorinfoTask();
                qcTaskInfo.CreateDate = dt;
                qcTaskInfo.ProjectName = qcName;
                qcTaskInfo.SampleType = combSampleType.Text;
                qcTaskInfo.SampleNum = txtSumpleNum.Text;
                lstCalibratorinfoTask.Add(qcTaskInfo);
            }
            foreach (string qcName in projectPage4.GetSelectedProjects())
            {
                CalibratorinfoTask qcTaskInfo = new CalibratorinfoTask();
                qcTaskInfo.CreateDate = dt;
                qcTaskInfo.ProjectName = qcName;
                qcTaskInfo.SampleType = combSampleType.Text;
                qcTaskInfo.SampleNum = txtSumpleNum.Text;
                lstCalibratorinfoTask.Add(qcTaskInfo);
            }

            //保存任务信息
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryCalibratorinfoTask", XmlUtility.Serializer(typeof(List<CalibratorinfoTask>),lstCalibratorinfoTask))));
            CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.CalibControlTask, new Dictionary<string, object[]>() { { "QueryCalibratorinfoTask", new object[] { XmlUtility.Serializer(typeof(List<CalibratorinfoTask>), lstCalibratorinfoTask) } } });
        }

        /// <summary>
        /// 组合项目点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grpCombProject_Click(object sender, EventArgs e)
        {
            if (grpCombProject.SelectedTabPageIndex == 0)
            {
                if (!xtraTabPage5.Controls.Contains(calibProCombPage1))
                {
                    xtraTabPage5.Controls.Add(calibProCombPage1);
                }
                //calibProCombPage1.SelectedProjects = lstQCRelateProject;
            }
            else if (grpCombProject.SelectedTabPageIndex == 1)
            {
                if (!xtraTabPage6.Controls.Contains(calibProCombPage2))
                {
                    xtraTabPage6.Controls.Add(calibProCombPage2);
                    //calibProCombPage2.LstAssayProInfos = lstCombProName;
                }
                //calibProCombPage2.SelectedProjects = lstQCRelateProject;
            }
        }

        /// <summary>
        /// 取消所有选中的项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //调用取消函数
            ResetControlState(projectPage1.Controls);
            ResetControlState(projectPage2.Controls);
            ResetControlState(projectPage3.Controls);
            ResetControlState(projectPage4.Controls);
            ResetControlState(calibProCombPage1.Controls);
            ResetControlState(calibProCombPage2.Controls);
        }
        //取消选中的项目
        public void ResetControlState(ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.GetType() == typeof(System.Windows.Forms.Button))
                {
                    if (control.Tag == "1")
                    {
                        control.Tag = "0";
                        this.Invoke(new EventHandler(delegate
                        {
                            control.ForeColor = Color.Black;
                            control.Enabled = true;
                        }));
                    }
                }
            }
        }
        #region 保存下拉框的样本类型
        /// <summary>
        /// 样本血清
        /// </summary>
        string sampleSerum = null;
        /// <summary>
        /// 样本尿液
        /// </summary>
        string sampleUrine = null;
        /// <summary>
        /// 样本 空
        /// </summary>
        string sampleBlank = null;
        #endregion
        /// <summary>
        /// 校准类型下拉框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSumpleNum.Text != null)
            {

                if (combSampleType.SelectedItem.ToString() == "血清" && sampleSerum == null)
                {
                    sampleSerum = combSampleType.SelectedItem.ToString();
                    //获取所有的项目信息
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity),
                    //    new CommunicationEntity("QueryProjectNameInfoByCalib", combSampleType.SelectedItem.ToString())));
                    calibDictionary.Add("QueryProjectNameInfoByCalib", new object[] { sampleSerum });
                }
                else
                {
                    if (combSampleType.SelectedItem.ToString() == sampleSerum)
                    {
                        grpProject.SelectedTabPageIndex = 0;
                        projectPage1.LstAssayProInfos = lstSerumProject;
                        projectPage2.LstAssayProInfos = lstSerumProject;
                        projectPage3.LstAssayProInfos = lstSerumProject;
                        projectPage4.LstAssayProInfos = lstSerumProject;
                    }

                }
                if (combSampleType.SelectedItem.ToString() == "尿液" && sampleUrine == null)
                {
                    sampleUrine = combSampleType.SelectedItem.ToString();
                    //获取所有的项目信息
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity),
                    //    new CommunicationEntity("QueryProjectNameInfoByCalib", combSampleType.SelectedItem.ToString())));
                    CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.CalibControlTask, new Dictionary<string, object[]>() { { "QueryProjectNameInfoByCalib", new object[] { sampleUrine } } });
                }
                else
                {
                    if (combSampleType.SelectedItem.ToString() == sampleUrine)
                    {
                        grpProject.SelectedTabPageIndex = 0;
                        projectPage1.LstAssayProInfos = lstUrinePeoject;
                        projectPage2.LstAssayProInfos = lstUrinePeoject;
                        projectPage3.LstAssayProInfos = lstUrinePeoject;
                        projectPage4.LstAssayProInfos = lstUrinePeoject;
                    }
                }
                if ((combSampleType.SelectedItem.ToString()).Trim() == "" && sampleBlank == null)
                {
                    sampleBlank = (combSampleType.SelectedItem.ToString()).Trim();
                    //获取所有的项目信息
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity),
                    //    new CommunicationEntity("QueryProjectNameInfoByCalib", combSampleType.SelectedItem.ToString())));
                    CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.CalibControlTask, new Dictionary<string, object[]>() { { "QueryProjectNameInfoByCalib", new object[] { sampleBlank } } });
                }
                else
                {
                    if (combSampleType.SelectedItem.ToString() == sampleBlank)
                    {
                        grpProject.SelectedTabPageIndex = 0;
                        projectPage1.LstAssayProInfos = lstBlankProject;
                        projectPage2.LstAssayProInfos = lstBlankProject;
                        projectPage3.LstAssayProInfos = lstBlankProject;
                        projectPage4.LstAssayProInfos = lstBlankProject;
                    }
                }
                //获取所有的项目信息
                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity),
                //    new CommunicationEntity("QueryProjectNameInfoByCalib", combSampleType.SelectedItem.ToString())));
            }
        }
    }
}
