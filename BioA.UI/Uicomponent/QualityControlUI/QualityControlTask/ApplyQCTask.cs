using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.Common;
using BioA.Common.IO;
using System.Threading;
using BioA.Service;

namespace BioA.UI
{
    public partial class ApplyQCTask : UserControl
    {
        // 项目页
        private QCProjectPage1 projectPage1 = new QCProjectPage1();
        private QCProjectPage2 projectPage2 = new QCProjectPage2();
        private QCProjectPage3 projectPage3 = new QCProjectPage3();
        // 质控品信息
        List<QualityControlInfo> lstQCInfos = new List<QualityControlInfo>();
        // 所有生化项目名称
        private List<string> lstAssayProInfos = new List<string>();

        private List<string[]> lstQCRelateProject = new List<string[]>();
        // 生化任务
        List<QCTaskInfo> lstQCSamples = new List<QCTaskInfo>();
        // 最大质控样本号
        int intMaxSamNum = 0;

        /// <summary>
        /// 存储客户端发送信息给服务器参数的集合
        /// </summary>
        private Dictionary<string, object[]> qcTaskDictionary = new Dictionary<string, object[]>();
        /// <summary>
        /// 存储质控任务保存是否成功：成功为true，反之为false
        /// </summary>
        private bool flag = false;
        /// <summary>
        /// 存储质控任务列表是否被点击：点击tru，反之false
        /// </summary>
        private bool taskFlag = false;
        //声明一个获取任务状态的委托
        public delegate bool getOPID();
        public event getOPID getopid;
        //定义一个被选任务关联的项目名称
        public List<string> lstprojectName = new List<string>();
        public ApplyQCTask()
        {
            InitializeComponent();
            
        }
    
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ApplyQCTask_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadApplyQCTask));
            
        }

        private void loadApplyQCTask()
        {
            //projectPage1 = new QCProjectPage1();
            //projectPage2 = new QCProjectPage2();
            //projectPage3 = new QCProjectPage3();
            xtraTabPage1.Controls.Add(projectPage1);
            xtraTabPage2.Controls.Add(projectPage2);
            xtraTabPage3.Controls.Add(projectPage3);
            combSampleType.Properties.Items.AddRange(RunConfigureUtility.SampleTypes);
            qcTaskDictionary.Clear();
            combSampleType.SelectedIndex = 1;
            /**
            //加载质控编号和质控任务
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryBigestQCTaskInfoForToday", null)));
            //获取质控品信息
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCAllInfoForUnLocked", null)));
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryAssayProNameAllInfo", combSampleType.SelectedItem.ToString())));
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCTaskForLstv", null)));
           **/
            qcTaskDictionary.Add("QueryBigestQCTaskInfoForToday", new object[] { "" });
            qcTaskDictionary.Add("QueryQCAllInfoForUnLocked", new object[] { "" });
            ClientSendToServices(qcTaskDictionary);
            
        }


        private void ClientSendToServices(Dictionary<string, object[]> param)
        {
            var qcTaskThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.QCTask, param);
            });
            qcTaskThread.IsBackground = true;
            qcTaskThread.Start();
        }
        /// <summary>
        /// 任务执行中获取任务执行状态
        /// </summary>
        public void QueryTasksStatus()
        {
            qcTaskDictionary.Clear();
            qcTaskDictionary.Add("QueryBigestQCTaskInfoForToday", new object[] { "" });
            ClientSendToServices(qcTaskDictionary);
        }

        /// <summary>
        /// 接收数据库数据传输
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="sender"></param>
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                //显示质控编号和质控任务
                case "QueryBigestQCTaskInfoForToday":
                    List<QCTaskInfo> lstQCTask = (List<QCTaskInfo>)XmlUtility.Deserialize(typeof(List<QCTaskInfo>), sender as string);
                    this.DisplayTaskInfo(lstQCTask);
                    break;
                    //显示所有项目信息
                case "QueryAssayProNameAllInfo":
                    lstAssayProInfos = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    projectPage1.LstAssayProInfos = lstAssayProInfos;
                    projectPage2.LstAssayProInfos = lstAssayProInfos;
                    projectPage3.LstAssayProInfos = lstAssayProInfos;
                    break;
                    //质控品信息
                case "QueryQCAllInfoForUnLocked":
                    lstQCInfos.Clear();
                    lstQCInfos = (List<QualityControlInfo>)XmlUtility.Deserialize(typeof(List<QualityControlInfo>), sender as string);
                    this.Invoke(new EventHandler(delegate
                        {
                            combPosition.Properties.Items.Clear();

                            List<string> lstQCPos = new List<string>();

                            foreach (QualityControlInfo qcInfo in lstQCInfos)
                            {
                                lstQCPos.Add(qcInfo.Pos);
                            }

                            lstQCPos.Sort();
                            combPosition.Properties.Items.AddRange(lstQCPos);
                            combPosition.Text = "请选择";
                        }));                    
                    break;

                case "QueryProjectNameInfoByQC":
                    lstQCRelateProject = (List<string[]>)XmlUtility.Deserialize(typeof(List<string[]>), sender as string);
                    this.Invoke(new EventHandler(delegate
                        {
                            tabcProject.SelectedTabPageIndex = 0;
                        }));
                    
                    projectPage1.ResetControlState();
                    projectPage2.ResetControlState();
                    projectPage3.ResetControlState();
                    projectPage1.SelectedProjects = lstQCRelateProject;
                    projectPage2.SelectedProjects = lstQCRelateProject;
                    projectPage3.SelectedProjects = lstQCRelateProject;
                    break;
                    //显示添加质控任务是否成功
                case "AddQCTask":
                    string strAddResult = sender as string;
                      
                    if (strAddResult == "添加质控任务失败！")
                    {
                        MessageBox.Show("添加质控任务失败！");
                        return;
                    }
                    else
                    {
                        //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryBigestQCTaskInfoForToday", null)));

                        //intMaxSamNum = intMaxSamNum + 1;
                        List<QCTaskInfo> lstQCTaskInfo = new QCTask().QueryBigestQCTaskInfoForToday("QueryBigestQCTaskInfoForToday");
                        this.DisplayTaskInfo(lstQCTaskInfo);
                        this.btnApply_Click(null,null);
                    }
                    break;
                    //获取质控任务。UI端已注释
                case "QueryQCTaskForLstv":
                        BeginInvoke(new Action(() =>
                        {
                            List<QCTaskInfo> lstQCTaskInfo = (List<QCTaskInfo>)XmlUtility.Deserialize(typeof(List<QCTaskInfo>), sender as string);

                            //foreach (QCTaskInfo qctask in lstQCTaskInfo)
                            //{
                            //    bool isExist = false;
                            //    foreach (QCTaskInfo qcSample in lstQCSamples)
                            //    {
                            //        if (qcSample.SampleNum == qctask.SampleNum)
                            //        {
                            //            isExist = true;
                            //        }
                            //    }

                            //    if (isExist == false)
                            //    {
                            //        lstQCSamples.Add(qctask);
                            //    }
                            //}
                            DataTable dt = new DataTable();
                            dt.Columns.Add("顺序号");
                            dt.Columns.Add("质控品位置");
                            dt.Columns.Add("任务状态");
                            foreach (QCTaskInfo qctask in lstQCTaskInfo)
                            {
                                string strState = "";
                                switch (qctask.TaskState)
                                {
                                    case 0:
                                        strState = "待测";
                                        break;
                                    case 1:
                                        strState = "执行中";
                                        break;
                                    case 2:
                                        strState = "已完成";
                                        break;
                                    case 3:
                                        strState = "被终止";
                                        break;
                                }
                                dt.Rows.Add(new object[] { qctask.SampleNum, qctask.Position, strState });
                            }

                            this.lstvQCTask.DataSource = dt;
                        }));
                    break;
                case "QueryQCTaskBySampleNum":
                    QCTaskInfoQuery qCTaskInfoQuery = (QCTaskInfoQuery)XmlUtility.Deserialize(typeof(QCTaskInfoQuery), sender as string);
                    if (qCTaskInfoQuery != null)
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            taskFlag = true;
                            qcTaskDictionary.Clear();
                            txtSumpleNum.Text = qCTaskInfoQuery.SampleNum;
                            combPosition.Text = qCTaskInfoQuery.Position;
                            combSampleType.SelectedItem = qCTaskInfoQuery.SampleType;
                            txtQCName.Text = qCTaskInfoQuery.QCName;
                            txtLotNum.Text = qCTaskInfoQuery.LotNum;
                            txtQCConc.Text = qCTaskInfoQuery.LevelConc;
                            txtManufacturer.Text = qCTaskInfoQuery.Manufacturer;

                            //projectPage1.ResetControlState();
                            //projectPage2.ResetControlState();
                            //projectPage3.ResetControlState();

                            projectPage1.TaskProjects = lstprojectName;// qCTaskInfoQuery.Projects;
                            projectPage2.TaskProjects = lstprojectName;// qCTaskInfoQuery.Projects;
                            projectPage3.TaskProjects = lstprojectName;// qCTaskInfoQuery.Projects;

                        }));
                        taskFlag = false;
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 显示质控任务信息
        /// </summary>
        /// <param name="lstQCTask"></param>
        private void DisplayTaskInfo(List<QCTaskInfo> lstQCTask)
        {
            this.Invoke(new EventHandler(delegate { 
                DataTable dt = new DataTable();
                dt.Columns.Add("顺序号");
                dt.Columns.Add("质控品位置");
                dt.Columns.Add("项目名称");
                dt.Columns.Add("任务状态");
                foreach (QCTaskInfo qcTask in lstQCTask)
                {
                    int sampNum = Convert.ToInt32(qcTask.SampleNum.Substring(1, 1));
                    if (sampNum > intMaxSamNum)
                    {
                        intMaxSamNum = sampNum;
                    }
                    string strState = "";
                    switch (qcTask.TaskState)
                    {
                        case 0:
                            strState = "待测";
                            break;
                        case 1:
                            strState = "执行中";
                            break;
                        case 2:
                            strState = "已完成";
                            break;
                        case 3:
                            strState = "被终止";
                            break;
                    }
                    dt.Rows.Add(new object[] { qcTask.SampleNum, qcTask.Position,qcTask.ProjectName,strState});
                }
                this.lstvQCTask.DataSource = dt;
                txtSumpleNum.Text = "C" + (intMaxSamNum + 1).ToString();
            }));
        }

        /// <summary>
        /// 保存任务
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
            if (combPosition.SelectedItem.ToString() == "" || combPosition.SelectedItem.ToString() == "请选择" || combPosition.SelectedItem == null)
            {
                MessageBox.Show("请选择质控品！");
                return;
            }

            if (projectPage1.GetSelectedProjects().Count == 0 &&
                projectPage2.GetSelectedProjects().Count == 0 &&
                projectPage3.GetSelectedProjects().Count == 0)
            {
                MessageBox.Show("请选择质控项目！");
                return;
            }

            if (intMaxSamNum >= System.Convert.ToInt32(txtSumpleNum.Text.Substring(1)))
            {
                MessageBox.Show("此任务已存在，如需创建新任务，请点击申请按钮！");
                return;
            }


            List<QCTaskInfo> lstQCTaskInfos = new List<QCTaskInfo>();
            DateTime dt = DateTime.Now;

            foreach (string qcName in projectPage1.GetSelectedProjects())                                                           
            {
                QCTaskInfo qcTaskInfo = new QCTaskInfo();

                qcTaskInfo.SampleNum = txtSumpleNum.Text;

                qcTaskInfo.Position = combPosition.SelectedItem.ToString();
                qcTaskInfo.SampleType = combSampleType.SelectedItem.ToString();

                qcTaskInfo.CreateDate = dt;
                qcTaskInfo.ProjectName = qcName;
                qcTaskInfo.InspectTimes = 1;

                lstQCTaskInfos.Add(qcTaskInfo);
            }
            foreach (string qcName in projectPage2.GetSelectedProjects())
            {
                QCTaskInfo qcTaskInfo = new QCTaskInfo();

                qcTaskInfo.SampleNum = txtSumpleNum.Text;

                qcTaskInfo.Position = combPosition.SelectedItem.ToString();
                qcTaskInfo.SampleType = combSampleType.SelectedItem.ToString();

                qcTaskInfo.CreateDate = dt;
                qcTaskInfo.ProjectName = qcName;
                qcTaskInfo.InspectTimes = 1;

                lstQCTaskInfos.Add(qcTaskInfo);
            }
            foreach (string qcName in projectPage3.GetSelectedProjects())
            {
                QCTaskInfo qcTaskInfo = new QCTaskInfo();

                qcTaskInfo.SampleNum = txtSumpleNum.Text;

                qcTaskInfo.Position = combPosition.SelectedItem.ToString();
                qcTaskInfo.SampleType = combSampleType.SelectedItem.ToString();

                qcTaskInfo.CreateDate = dt;
                qcTaskInfo.ProjectName = qcName;
                qcTaskInfo.InspectTimes = 1;

                lstQCTaskInfos.Add(qcTaskInfo);
            }
            qcTaskDictionary.Clear();
            qcTaskDictionary.Add("AddQCTask", new object[] { XmlUtility.Serializer(typeof(List<QCTaskInfo>), lstQCTaskInfos) });
            ClientSendToServices(qcTaskDictionary);
        }
        /// <summary>
        /// 质控品位置下拉框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (taskFlag)
            {
                //TODO.留给样本下拉框改变事件去查询数据库
                
            }
            else
            {
                if (combPosition.SelectedItem != null && combSampleType.SelectedItem != null)
                {
                    if (combPosition.SelectedItem.ToString() != "请选择")
                    {
                        foreach (QualityControlInfo qcInfo in lstQCInfos)
                        {
                            if (qcInfo.Pos == combPosition.SelectedItem.ToString())
                            {
                                txtQCName.Text = qcInfo.QCName;
                                txtLotNum.Text = qcInfo.LotNum;
                                txtQCConc.Text = qcInfo.HorizonLevel;
                                txtManufacturer.Text = qcInfo.Manufacturer;

                                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask,
                                //    XmlUtility.Serializer(typeof(CommunicationEntity),
                                //    new CommunicationEntity("QueryProjectNameInfoByQC", XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo), combSampleType.SelectedItem.ToString())));
                                qcTaskDictionary.Clear();
                                qcTaskDictionary.Add("QueryProjectNameInfoByQC", new object[] { XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo), combSampleType.SelectedItem.ToString() });
                                ClientSendToServices(qcTaskDictionary);
                            }
                        }
                    }
                    else
                    {
                        txtQCName.Text = "";
                        txtLotNum.Text = "";
                        txtQCConc.Text = "";
                        txtManufacturer.Text = "";
                    }
                }
            }
        }
        /// <summary>
        /// 点击申请按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            this.Invoke(new EventHandler(delegate { 
                txtSumpleNum.Text = "C" + (intMaxSamNum + 1).ToString();
                combPosition.Text = "请选择";
                combSampleType.SelectedIndex = 1;
                txtQCName.Text = "";
                txtLotNum.Text = "";
                txtQCConc.Text = "";
                txtManufacturer.Text = "";
                projectPage1.ResetControlState();
                projectPage2.ResetControlState();
                projectPage3.ResetControlState();
                lstQCRelateProject.Clear();
            }));
        }
        /// <summary>
        /// 样本类型下拉框改变按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryAssayProNameAllInfo", combSampleType.SelectedItem.ToString())));
            if (taskFlag)
            {
                qcTaskDictionary.Add("QueryAssayProNameAllInfo", new object[] { combSampleType.SelectedItem.ToString() });
                if (combPosition.SelectedItem.ToString() != "请选择")
                {
                    foreach (QualityControlInfo qcInfo in lstQCInfos)
                    {
                        if (qcInfo.Pos == combPosition.SelectedItem.ToString())
                        {
                            txtQCName.Text = qcInfo.QCName;
                            txtLotNum.Text = qcInfo.LotNum;
                            txtQCConc.Text = qcInfo.HorizonLevel;
                            txtManufacturer.Text = qcInfo.Manufacturer;

                            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask,
                            //    XmlUtility.Serializer(typeof(CommunicationEntity),
                            //    new CommunicationEntity("QueryProjectNameInfoByQC", XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo), combSampleType.SelectedItem.ToString())));
                            qcTaskDictionary.Add("QueryProjectNameInfoByQC", new object[] { XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo), combSampleType.SelectedItem.ToString() });
                        }
                    }
                }
                ClientSendToServices(qcTaskDictionary);
            }
            else
            {
                if (combPosition.SelectedItem != null)
                {
                    qcTaskDictionary.Clear();
                    qcTaskDictionary.Add("QueryAssayProNameAllInfo", new object[] { combSampleType.SelectedItem.ToString() });
                    if (flag)
                        qcTaskDictionary.Add("QueryBigestQCTaskInfoForToday", null);
                    if (combPosition.SelectedItem.ToString() != "请选择")
                    {
                        foreach (QualityControlInfo qcInfo in lstQCInfos)
                        {
                            if (qcInfo.Pos == combPosition.SelectedItem.ToString())
                            {
                                txtQCName.Text = qcInfo.QCName;
                                txtLotNum.Text = qcInfo.LotNum;
                                txtQCConc.Text = qcInfo.HorizonLevel;
                                txtManufacturer.Text = qcInfo.Manufacturer;

                                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask,
                                //    XmlUtility.Serializer(typeof(CommunicationEntity),
                                //    new CommunicationEntity("QueryProjectNameInfoByQC", XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo), combSampleType.SelectedItem.ToString())));
                                qcTaskDictionary.Add("QueryProjectNameInfoByQC", new object[] { XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo), combSampleType.SelectedItem.ToString() });
                            }
                        }
                    }
                    ClientSendToServices(qcTaskDictionary);
                }
                else
                {
                    qcTaskDictionary.Add("QueryAssayProNameAllInfo", new object[] { combSampleType.SelectedItem.ToString() });
                }
            }
        }
        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvQCTask_Click(object sender, EventArgs e)
        {
            CommunicationEntity communicationEntity = new CommunicationEntity();
            int selectedHandle;

            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                string strTaskNum = this.gridView1.GetRowCellValue(selectedHandle, "顺序号").ToString();   
                string projectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                lstprojectName.Clear();
                lstprojectName.Add(projectName);        
                //communicationEntity.StrmethodName = "QueryQCTaskBySampleNum";
                //communicationEntity.ObjParam = strTaskNum;
                qcTaskDictionary.Clear();
                qcTaskDictionary.Add("QueryQCTaskBySampleNum", new object[] { strTaskNum });
                ClientSendToServices(qcTaskDictionary);
            }
        }

        private void tabcProject_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tabcProject.SelectedTabPageIndex == 0)
            {
                if (!xtraTabPage1.Controls.Contains(projectPage1))
                {
                    xtraTabPage1.Controls.Add(projectPage1);
                    projectPage1.LstAssayProInfos = lstAssayProInfos;
                }


                projectPage1.SelectedProjects = lstQCRelateProject;
            }
            else if (tabcProject.SelectedTabPageIndex == 1)
            {
                if (!xtraTabPage2.Controls.Contains(projectPage2))
                {
                    xtraTabPage2.Controls.Add(projectPage2);
                    projectPage2.LstAssayProInfos = lstAssayProInfos;
                }
                projectPage2.SelectedProjects = lstQCRelateProject;
            }
            else if (tabcProject.SelectedTabPageIndex == 2)
            {
                if (!xtraTabPage3.Controls.Contains(projectPage3))
                {
                    xtraTabPage3.Controls.Add(projectPage3);
                    projectPage3.LstAssayProInfos = lstAssayProInfos;
                }
                projectPage3.SelectedProjects = lstQCRelateProject;
            }
        }
    }
}
