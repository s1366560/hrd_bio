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
using BioA.Common.IO;
using System.Threading;

namespace BioA.UI
{
    public partial class CalibControlTask : DevExpress.XtraEditors.XtraUserControl
    {
        //校准项目第一页~~四页窗体
        private CalibProjectPage1 projectPage1 = new CalibProjectPage1();
        private CalibProjectPage2 projectPage2 = new CalibProjectPage2();
        private CalibProjectPage3 projectPage3 = new CalibProjectPage3();
        private CalibProjectPage4 projectPage4 = new CalibProjectPage4();
        //校准组合项目第一二页窗体
        private CalibProCombPage2 calibProCombPage2 = new CalibProCombPage2();
        private CalibProCombPage1 calibProCombPage1 = new CalibProCombPage1();
        private List<string[]> lstQCRelateProject = new List<string[]>();
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
        /// 校准任务数据表
        /// </summary>
        DataTable dt = new DataTable();
        /// <summary>
        /// 传递访问数据的方法名和参数个数的泛型集合
        /// </summary>
        Dictionary<string, object[]> calibDictionary = new Dictionary<string, object[]>();
        /// <summary>
        /// 判断是否可添加校准任务
        /// </summary>
        public bool isAddCalibTask = true;
        //申明一个获取任务状态的委托
        public delegate bool getOPID();
        public event getOPID getopid;
        public CalibControlTask()
        {
            InitializeComponent();
            calibProCombPage1.clickCombProNameEvent += HandleClickCombProNameEvent;
            calibProCombPage2.clickCombProNamePage2Event += HandleClickCombProNameEvent;
            xtraTabPage1.Controls.Add(projectPage1);
            xtraTabPage2.Controls.Add(projectPage2);
            xtraTabPage3.Controls.Add(projectPage3);
            xtraTabPage4.Controls.Add(projectPage4);
            xtraTabPage5.Controls.Add(calibProCombPage1);
            xtraTabPage6.Controls.Add(calibProCombPage2);
            combSampleType.Properties.Items.AddRange(RunConfigureUtility.SampleTypes);

            dt.Columns.Add("顺序号");
            dt.Columns.Add("项目名称");
            dt.Columns.Add("校准品名");
            dt.Columns.Add("重复次数");
            dt.Columns.Add("状态");
            this.lstvTask.DataSource = dt;
        }
        //加载页面
        public void CalibControlTask_Load(object sender, EventArgs e)
        {
            CalibControlTaskInit();
            
        }
        public void Clear()
        {
            lstQCRelateProject.Clear();
            lstProjects.Clear();
            lstCombProInfo.Clear();
            intPos = 0;
            calibDictionary.Clear();
            isAddCalibTask = true;
            lstSerumProject.Clear();
            lstUrinePeoject.Clear();
            lstBlankProject.Clear();
            lstCombProName.Clear();
            sampleBlank = null;
            sampleSerum = null;
            sampleUrine = null;
        }
        private void CalibControlTaskInit()
        {
            if (combSampleType.SelectedIndex != 1)
                combSampleType.SelectedIndex = 1;
            else
                combSampleType_SelectedIndexChanged(null,null);
            //获取所有任务信息
            calibDictionary.Add("QueryCalibratorinfoTask", new object[] { "" });
            //获取所有组合项目信息
            calibDictionary.Add("QueryCombProjectNameAllInfo", new object[] { "" });
            //获取所有组合项目名和项目名
            calibDictionary.Add("QueryProjectAndCombProName", null);
            ClientSendDataToServices(calibDictionary);
        }
        /// <summary>
        /// 发送数据给服务器
        /// </summary>
        /// <param name="pairs"></param>
        private void ClientSendDataToServices(Dictionary<string, object[]> pairs)
        {
            var calibThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.CalibControlTask, pairs);
            });
            calibThread.IsBackground = true;
            calibThread.Start();
        }
        /// <summary>
        /// 任务执行中获取任务执行状态
        /// </summary>
        public void QueryTasksStatus()
        {
            calibDictionary.Clear();
            calibDictionary.Add("QueryCalibratorinfoTask", new object[] { "" });
            ClientSendDataToServices(calibDictionary);
        }

        /// <summary>
        /// 处理组合项目名点击事件
        /// </summary>
        /// <param name="sender"></param>
        private bool HandleClickCombProNameEvent(string sender, string tag)
        {
            //存储项目名称
            lstProjects.Clear();
            exceptionItemInfoList.Clear();
            foreach (CombProjectInfo combProInfo in lstCombProInfo)
            {
                if (combProInfo.CombProjectName == sender)
                {
                    lstProjects.Add(combProInfo.ProjectName);
                }
            }
            if (lstProjects.Count > 0)
            {
                bool ret1 = SetProjectPage(lstProjects, projectPage1.Controls, tag);
                if (ret1 == false)
                    return ret1;
                bool ret2 = SetProjectPage(lstProjects, projectPage2.Controls, tag);
                if (ret2 == false)
                    return ret2;
                bool ret3 = SetProjectPage(lstProjects, projectPage3.Controls, tag);
                if (ret3 == false)
                    return ret3;
                bool ret4 = SetProjectPage(lstProjects, projectPage4.Controls, tag);
                if (exceptionItemInfoList.Count > 0)
                {
                    string resultInfo = string.Join(",", exceptionItemInfoList.Select(s => "[" + s + "]"));
                    this.Invoke(new EventHandler(delegate { MessageBox.Show(resultInfo + "项目参数有误！"); }));
                }
                if (ret4 == false)
                {
                    return ret4;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        private bool SetProjectPage(List<string> selectedProjects, ControlCollection Controls, string tag)
        {
            bool flag = true;
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(System.Windows.Forms.Button))
                {
                    foreach (string str in selectedProjects)
                    {
                        if (control.Text == str)
                        {
                            if (control.ForeColor == Color.Black && tag == "0")
                            {
                                control.Tag = "1";

                                this.Invoke(new EventHandler(delegate
                                {
                                    control.ForeColor = Color.Red;
                                }));
                                flag = true;
                            }
                            else if (control.ForeColor == Color.Red && tag == "1")
                            {
                                control.Tag = "0";

                                this.Invoke(new EventHandler(delegate
                                {
                                    control.ForeColor = Color.Black;
                                }));
                                flag = true;
                            }
                            else if (control.ForeColor == Color.Orange && tag == "0")
                            {
                                exceptionItemInfoList.Add(control.Text);
                            }
                        }
                    }


                }
            }
            return flag;
        }
        /// <summary>
        /// 组合项目中的项目参数有误
        /// </summary>
        private List<string> exceptionItemInfoList = new List<string>();


        /// <summary>
        /// xtratabcontrol控件的标签页点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grpProject_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
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
            this.BeginInvoke(new EventHandler(delegate
            {
                dt.Rows.Clear();
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
                    this.Invoke(new EventHandler(delegate { txtSumpleNum.Text = "S" + (max + 1).ToString();
                        if (this.btnSave.Enabled == true) ;

                        else
                            this.btnSave.Enabled = true;
                    }));
                    
                    GridAdd(lstCalibrationCurveInfo);
                    break;
                case "QueryCombProjectNameAllInfo":
                    lstCombProName = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    calibProCombPage1.LstProjectGroups = lstCombProName;
                    calibProCombPage2.LstAssayProInfos = lstCombProName;
                    this.BeginInvoke(new EventHandler(delegate { grpCombProject.SelectedTabPageIndex = 0; }));
                    break;
                case "QueryProjectAndCombProName":
                    lstCombProInfo = (List<CombProjectInfo>)XmlUtility.Deserialize(typeof(List<CombProjectInfo>), sender as string);
                    break;
                case "QueryProjectNameInfoByCalib":
                    lstQCRelateProject = (List<string[]>)XmlUtility.Deserialize(typeof(List<string[]>), sender as string);

                    projectPage1.LstAssayProInfos = lstQCRelateProject;
                    projectPage2.LstAssayProInfos = lstQCRelateProject;
                    projectPage3.LstAssayProInfos = lstQCRelateProject;
                    projectPage4.LstAssayProInfos = lstQCRelateProject;
                    this.Invoke(new EventHandler(delegate { grpProject.SelectedTabPageIndex = 0; }));
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
            if (getopid != null)
            {
                if (!getopid())
                {
                    MessageBox.Show("当前任务正在测试，暂停后方可继续下任务!");
                    return;
                }
            }
            this.btnSave.Enabled = false;
            if (projectPage1.GetSelectedProjects().Count == 0 && 
                projectPage2.GetSelectedProjects().Count == 0 &&
                projectPage3.GetSelectedProjects().Count == 0 &&
                projectPage4.GetSelectedProjects().Count == 0)
            {
                MessageBox.Show("请选择校准项目！");
                this.btnSave.Enabled = true;
                return;            
            }
            if (intPos >= System.Convert.ToInt32(txtSumpleNum.Text.Substring(1)))
            {
                MessageBox.Show("此任务已经存在！");
                this.btnSave.Enabled = true;
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
            sampleBlank = null;
            sampleSerum = null;
            sampleUrine = null;
            lstBlankProject.Clear();
            lstUrinePeoject.Clear();
            lstSerumProject.Clear();
            calibDictionary.Clear();
            //保存任务信息
            calibDictionary.Add("QueryCalibratorinfoTask", new object[] { XmlUtility.Serializer(typeof(List<CalibratorinfoTask>), lstCalibratorinfoTask) });
            combSampleType_SelectedIndexChanged(null, null);
            ClientSendDataToServices(calibDictionary);
        }

        /// <summary>
        /// 组合项目点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grpCombProject_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
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
                if (control.Tag != null)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        if (control.Tag.ToString() == "1")
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
            }
        }
    }
}
