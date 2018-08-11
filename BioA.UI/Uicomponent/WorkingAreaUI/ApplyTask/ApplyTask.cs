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
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using BioA.Common;
using BioA.Common.Communication;
using BioA.Common.IO;
using System.Threading;
using System.Diagnostics;

namespace BioA.UI
{
    public partial class ApplyTask : DevExpress.XtraEditors.XtraUserControl
    {
        ProjectPage1 projectPage1;
        ProjectPage2 projectPage2;
        ProjectPage3 projectPage3;
        ProjectPage4 projectPage4;
        ProCombPage1 proCombPage1;
        ProCombPage2 proCombPage2;

        // 最大样本号
        private int intMaxSampleNum = 0;

        // 任务存储
        List<SampleInfo> lstSampleInfo = new List<SampleInfo>();
        // 样本稀释比例
        List<string> lstDilutionRatio = new List<string>();

        // 项目稀释设置
        List<string[]> lstDiluteInfos = new List<string[]>();
        //存储客户端发送信息给服务器的参数集合
        Dictionary<string, object[]> dic = new Dictionary<string, object[]>();

        frmBatchInput batchInput;
        PatientInfoFrm patientInfofrm;
        public ApplyTask()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyTask_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(ApplyTaskInit));
        }
        /// <summary>
        /// 异步启动线程
        /// </summary>
        private void ApplyTaskInit()
        {
            projectPage1 = new ProjectPage1();
            projectPage2 = new ProjectPage2();
            projectPage3 = new ProjectPage3();
            projectPage4 = new ProjectPage4();
            proCombPage1 = new ProCombPage1();
            proCombPage2 = new ProCombPage2();
            Console.WriteLine("ApplyTaskINit " + DateTime.Now.Ticks);
            xtraTabPage1.Controls.Add(projectPage1);
            xtraTabPage2.Controls.Add(projectPage2);
            xtraTabPage3.Controls.Add(projectPage3);
            xtraTabPage4.Controls.Add(projectPage4);
            xtraTabPage5.Controls.Add(proCombPage1);
            xtraTabPage6.Controls.Add(proCombPage2);
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.FocusedRow.ForeColor = Color.Red;
            gridView1.Appearance.Row.Font = font;
            combPanelNum.Properties.Items.AddRange(RunConfigureUtility.SamplePanel);
            combPosNum.Properties.Items.AddRange(RunConfigureUtility.SamplePosition);
            combSampleType.Properties.Items.AddRange(RunConfigureUtility.SampleTypes);
            combSampleContainer.Properties.Items.AddRange(RunConfigureUtility.SampleContainerList);
            
            batchInput = new frmBatchInput();
            patientInfofrm = new PatientInfoFrm();
            combSampleType.SelectedIndex = 1;
            combSampleContainer.SelectedIndex = 0;
            Console.WriteLine("ApplyTaskInit End1" + DateTime.Now.Ticks);
            Console.WriteLine("ApplyTaskInit CONN beg " + DateTime.Now.Ticks);
            // 获取最大样本编号
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryMaxSampleNum", null)));
            //// 获取稀释比例值
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QuerySampleDiluteRatio", null)));
            //// 获取项目名称
            ////CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProNameForApplyTask", combSampleType.SelectedItem.ToString())));
            //// 获取组合项目名称
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryCombProjectNameAllInfo", null)));
            //// 获取申请任务列表
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryApplyTaskLsvt", null)));

            //获取最大样本编号
            dic.Add("QueryMaxSampleNum", new object[] { "" });
            //获取所有结果单位
            dic.Add("QuerySampleDiluteRatio", null);
            //获取所有任务
            dic.Add("QueryApplyTaskLsvt", new object[] { "" });
            //获取所有组合项目信息
            dic.Add("QueryCombProjectNameAllInfo", new object[] { "" });
            ClientSendToServices(dic);
            //CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaApplyTask, dic);

            Console.WriteLine("ApplyTaskInit CONN end " + DateTime.Now.Ticks);
            
            DataTable dt = new DataTable();
            dt.Columns.Add("样本编号");
            dt.Columns.Add("位置");
            dt.Columns.Add("样本状态");

            this.lstvTask.DataSource = dt;
            Console.WriteLine("ApplyTaskInit CONN End " + DateTime.Now.Ticks);
        }

        /// <summary>
        /// 把客户端信息发送给服务器
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="sender"></param>
        private void ClientSendToServices(Dictionary<string, object[]> param)
        {
            var applyTaskThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaApplyTask, param);
            });
            applyTaskThread.IsBackground = true;
            applyTaskThread.Start();
        } 


        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryMaxSampleNum":
                    intMaxSampleNum = (int)sender;
                    this.Invoke(new EventHandler(delegate
                        {
                            txtSampleNum.Text = (intMaxSampleNum + 1).ToString();
                            txtBoxDetectionNum.Text = "1";
                        }));
                    break;
                case "QueryProNameForApplyTask":
                    List<string[]> lstProName = (List<string[]>)XmlUtility.Deserialize(typeof(List<string[]>), sender as string);
                    projectPage1.LstAssayProInfos = lstProName;
                    projectPage2.LstAssayProInfos = lstProName;
                    projectPage3.LstAssayProInfos = lstProName;
                    projectPage4.LstAssayProInfos = lstProName;
                    break;
                case "QueryCombProjectNameAllInfo":
                    List<string> lstCombProName = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    proCombPage1.LstProjectGroups = lstCombProName;
                    proCombPage2.LstAssayProInfos = lstCombProName;
                    break;
                case "QueryApplyTaskLsvt":
                    lstSampleInfo = (List<SampleInfo>)XmlUtility.Deserialize(typeof(List<SampleInfo>), sender as string);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("样本编号");
                    dt.Columns.Add("位置");
                    dt.Columns.Add("样本状态");

                    List<int> lstPanel = new List<int>();
                    List<int> lstPosition = new List<int>();
                    List<int> lstSamNum = new List<int>();

                    if (lstSampleInfo != null)
                    {
                        foreach (SampleInfo s in lstSampleInfo) 
                        {
                            string strState = string.Empty;
                            if (s.SampleState == 0)
                                strState = "待测";
                            else if (s.SampleState == 1)
                                strState = "检测中";
                            else if (s.SampleState == 2)
                                strState = "完成";
                            else if (s.SampleState == 3)
                                strState = "任务被终止";

                            dt.Rows.Add(new object[] { s.SampleNum.ToString(), s.SamplePos.ToString(), strState });
                            lstPanel.Add(s.PanelNum);
                            lstPosition.Add(s.SamplePos);
                            lstSamNum.Add(s.SampleNum);
                        }                    
                    }
                    this.Invoke(new EventHandler(delegate
                        {
                            lstvTask.DataSource = dt;

                            int iPanel = lstPanel.Count > 0 ? lstPanel.Max() : 0;
                            int iPosition = lstPanel.Count > 0 ? lstPosition.Max() : 0;

                            if (iPosition < 120)
                            {
                                if (iPanel > 0)
                                {
                                    combPanelNum.SelectedItem = iPanel.ToString();
                                }
                                else
                                {
                                    combPanelNum.SelectedItem = "1";
                                }
                                combPosNum.SelectedItem = (iPosition + 1).ToString();
                            }
                            else
                            {
                                combPanelNum.SelectedItem = (iPanel + 1).ToString();
                                combPosNum.SelectedItem = "1";
                            }

    
                            txtSampleNum.Text = lstSamNum.Count > 0 ? (lstSamNum.Max() + 1).ToString() : (1).ToString();
                            intMaxSampleNum = System.Convert.ToInt32(txtSampleNum.Text) - 1;
                        }));
                    break;
                case "QuerySampleDiluteRatio":
                    lstDilutionRatio = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    break;
                case "QueryProjectByCombProName":
                    List<string> lstProjects = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    projectPage1.SelectedProjects = lstProjects;
                    projectPage2.SelectedProjects = lstProjects;
                    projectPage3.SelectedProjects = lstProjects;
                    projectPage4.SelectedProjects = lstProjects;
                    break;
                case "QueryTaskInfoBySampleNum":
                    List<TaskInfo> lstTaskInfos = XmlUtility.Deserialize(typeof(List<TaskInfo>), sender as string) as List<TaskInfo>;
                    lstDiluteInfos.Clear();
                    List<string> lstProjects1 = new List<string>();
                    foreach (TaskInfo t in lstTaskInfos)
                    {
                        string[] strTaskInfo = new string[3];
                        strTaskInfo[0] = t.ProjectName;
                        strTaskInfo[1] = t.SampleDilute;
                        strTaskInfo[2] = t.DilutedRatio.ToString();
                        lstDiluteInfos.Add(strTaskInfo);
                        lstProjects1.Add(t.ProjectName);
                    }
                    projectPage1.SelectedProjects = lstProjects1;
                    projectPage2.SelectedProjects = lstProjects1;
                    projectPage3.SelectedProjects = lstProjects1;
                    projectPage4.SelectedProjects = lstProjects1;
                    
                    break;
                case "AddTask":
                    string strAddTaskInfo = sender as string;
                    if (strAddTaskInfo == "此样本任务已经存在，请重新录入！")
                    {
                        MessageBox.Show("此样本任务已经存在，请重新录入！");
                    }
                    // 获取申请任务列表
                    dic.Clear();
                    dic.Add("QueryApplyTaskLsvt", new object[] { "" });
                    ClientSendToServices(dic);
                    //CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaApplyTask, new Dictionary<string, List<object>>() { { "QueryApplyTaskLsvt", new List<object>() { "" } } });
                    lstDiluteInfos.Clear();
                    break;
                case "QueryPatientInfoBySampleNum":
                    PatientInfo patientInfo = XmlUtility.Deserialize(typeof(PatientInfo), sender as string) as PatientInfo;
                    patientInfofrm.PatientInfoByNum = patientInfo;
                    break;
                case "AddTaskForBatch":
                    List<string> lstResult = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    batchInput.LstReceiveInfo = lstResult;
                    break;
                case "QueryApplyApartment":
                    patientInfofrm.LstApplyDepartment = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    break;
                case "QueryApplyDoctor":
                    patientInfofrm.LstApplyDoctor = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    break;
                case "QueryCheckDoctor":
                    patientInfofrm.LstCheckDoctor = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    break;
                case "QueryInspectDoctor":
                    patientInfofrm.LstInspectDoctor = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    break;
                case "QueryPatientInfos":
                    patientInfofrm.LstPatientInfo = (List<PatientInfo>)XmlUtility.Deserialize(typeof(List<PatientInfo>), sender as string);
                    break;
                case "UpdatePatientInfo":
                    patientInfofrm.StrUpdateInfo = sender as string;
                    break;
                case "QueryTaskInfoForSamplePanel":
                    anologSamplePanel.TaskInfoForSamPanel = XmlUtility.Deserialize(typeof(TaskInfoForSamplePanelInfo), sender as string) as TaskInfoForSamplePanelInfo;
                    break;
                case "QuerySamplePanelState":
                    anologSamplePanel.LstSampleInfo = XmlUtility.Deserialize(typeof(List<SampleInfo>), sender as string) as List<SampleInfo>;
                    break;
                default:
                    break;
            }
        }

        private void grpProject_Click(object sender, EventArgs e)
        {
            if (grpProject.SelectedTabPageIndex == 0)
            {
               
                xtraTabPage1.Controls.Add(projectPage1);
            }
            else if (grpProject.SelectedTabPageIndex == 1)
            {
                xtraTabPage2.Controls.Add(projectPage1);
            }
            else if (grpProject.SelectedTabPageIndex == 2)
            {
                xtraTabPage3.Controls.Add(projectPage1);
            }
            else if (grpProject.SelectedTabPageIndex == 3)
            {
                xtraTabPage4.Controls.Add(projectPage1);
            }
        }

        private void grpCombProject_Click(object sender, EventArgs e)
        {
            if (grpCombProject.SelectedTabPageIndex == 0)
            {
                xtraTabPage5.Controls.Add(proCombPage1);
            }
            else if (grpCombProject.SelectedTabPageIndex == 1)
            {
                xtraTabPage6.Controls.Add(proCombPage1);
            }
        }

        private void btnPatientInfo_Click(object sender, EventArgs e)
        {
            patientInfofrm.StartPosition = FormStartPosition.CenterScreen;
            int selectedHandle;

            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                int SampleNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "样本编号").ToString());
                patientInfofrm.IntSelectedNum = SampleNum;

                List<int> lstSampleNum = new List<int>();
                foreach (SampleInfo s in lstSampleInfo)
                {
                    lstSampleNum.Add(s.SampleNum);
                }
                patientInfofrm.LstSampleNum = lstSampleNum;
                patientInfofrm.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选中任务后，录入病人信息！");
            }
            
        }
        /// <summary>
        /// 申请按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            List<int> lstPanel = new List<int>();
            List<int> lstPosition = new List<int>();
            List<int> lstSamNum = new List<int>();

            if (lstSampleInfo != null)
            {
                foreach (SampleInfo s in lstSampleInfo)
                {
                    lstPanel.Add(s.PanelNum);
                    lstPosition.Add(s.SamplePos);
                    lstSamNum.Add(s.SampleNum);
                }
            }

            txtSampleNum.Text = lstSamNum.Count > 0 ? (lstSamNum.Max() + 1).ToString() : (1).ToString();

            int iPanel = lstPanel.Count > 0 ? lstPanel.Max() : 0;
            int iPosition = lstPanel.Count > 0 ? lstPosition.Max() : 0;

            if (iPosition < 120)
            {
                if (iPanel > 0)
                {
                    combPanelNum.SelectedItem = iPanel.ToString();
                }
                else
                {
                    combPanelNum.SelectedItem = "1";
                }
                combPosNum.SelectedItem = (iPosition + 1).ToString();
            }
            else
            {
                combPanelNum.SelectedItem = (iPanel + 1).ToString();
                combPosNum.SelectedItem = "1";
            }
            projectPage1.ResetControlState();
            projectPage2.ResetControlState();
            projectPage3.ResetControlState();
            projectPage4.ResetControlState();
            proCombPage1.ResetControlState();
            proCombPage2.ResetControlState();

            lstDiluteInfos.Clear();
        }

        private void btnDilutionSetting_Click(object sender, EventArgs e)
        {
            frmDiluteSetting diluteForm = new frmDiluteSetting();
            diluteForm.DataTransferEvent += DataTransfer_Event;

            List<string> lstProNames = new List<string>();
            lstProNames.AddRange(projectPage1.GetSelectedProjects());
            lstProNames.AddRange(projectPage2.GetSelectedProjects());
            lstProNames.AddRange(projectPage3.GetSelectedProjects());
            lstProNames.AddRange(projectPage4.GetSelectedProjects());
            if (lstProNames.Count == 0)
            {
                MessageBox.Show("请选择您要设置的项目！");
                return;
            }

            List<string[]> lstTemporary = new List<string[]>();

            foreach (string[] diluteInfo in lstDiluteInfos)
            {
                if (lstProNames.Contains(diluteInfo[0]))
                {
                    lstTemporary.Add(diluteInfo);
                }
            }

            foreach (string str in lstProNames)
            {
                bool exist = false;
                foreach (string[] strInfo in lstTemporary)
                {
                    if (strInfo[0] == str)
                    {
                        exist = true;
                    }
                }

                if (exist == false)
                    lstTemporary.Add(new string[]{str,"常规体积", ""});
            }


            diluteForm.LstDiluteInfos = lstTemporary;
            diluteForm.LstDiluteRatio = lstDilutionRatio;
            diluteForm.ShowDialog();
        }

        private void DataTransfer_Event(List<string[]> lstDiluteInfo)
        {
            lstDiluteInfos = lstDiluteInfo;
        }

        private void txtSampleNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }  
        }
        /// <summary>
        /// 改变样本类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProNameForApplyTask", combSampleType.SelectedItem.ToString())));
            //CommunicationUI.ServiceClient.ClientSendMsgToServiceWon(ModuleInfo.WorkingAreaApplyTask, new Dictionary<string, object>() {{"QueryProNameForApplyTask", combSampleType.SelectedItem.ToString()} });
            if (dic.Count == 5)
            {
                dic.Clear();
                dic.Add("QueryProNameForApplyTask", new object[] { combSampleType.SelectedItem.ToString() });
                ClientSendToServices(dic);
                //CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaApplyTask, new Dictionary<string, List<object>>() { { "QueryProNameForApplyTask", new List<object>() { combSampleType.SelectedItem.ToString() } } });
            }
            else
            {
                dic.Add("QueryProNameForApplyTask", new object[] { combSampleType.SelectedItem.ToString()});
            }
            lstDiluteInfos.Clear();
        }
        /// <summary>
        /// 点击保存项目事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            // 1.判断样本编号是否为空，判断样本编号是否已申请任务，判断样本编号是否大于2400
            if (txtSampleNum.Text == "" || txtSampleNum.Text == null)
            {
                MessageBox.Show("样本号不能为空！");
                btnSave.Enabled = true;
                return;
            }
            if (System.Convert.ToInt32(txtSampleNum.Text) > 2400)
            {
                MessageBox.Show("输入最大样本号不能大于2400！");
                btnSave.Enabled = true;
                return;
            }
            foreach (SampleInfo s in lstSampleInfo)
            {
                if (s.SampleNum == System.Convert.ToInt32(txtSampleNum.Text))
                {
                    MessageBox.Show("此样本号已被申请任务，请修改样本号！");
                    btnSave.Enabled = true;
                    return;
                }
            }

            // 2.判断盘号+位置是否已被申请任务
            foreach (SampleInfo s in lstSampleInfo)
            {
                if (s.PanelNum == System.Convert.ToInt32(combPanelNum.SelectedItem.ToString()) && 
                    s.SamplePos == System.Convert.ToInt32(combPosNum.SelectedItem.ToString()))
                {
                    MessageBox.Show(string.Format("{0}样本盘中的{1}号位置已被占用，请重新选择！", combPanelNum.SelectedItem.ToString(), combPosNum.SelectedItem.ToString()));
                    btnSave.Enabled = true;
                    return;
                }
            }
            // 4.检测项目不能为空
            if (projectPage1.GetSelectedProjects().Count == 0 &&
                projectPage2.GetSelectedProjects().Count == 0 &&
                projectPage3.GetSelectedProjects().Count == 0 &&
                projectPage4.GetSelectedProjects().Count == 0)
            {
                MessageBox.Show("申请常规任务中检测项目不能为空，请选择待检测项目！");
                btnSave.Enabled = true;
                return;
            }

            DateTime createTime = DateTime.Now;

            SampleInfo sampleInfo = new SampleInfo();
            sampleInfo.SampleNum = System.Convert.ToInt32(txtSampleNum.Text);
            sampleInfo.PanelNum = System.Convert.ToInt32(combPanelNum.SelectedItem.ToString());
            sampleInfo.SamplePos = System.Convert.ToInt32(combPosNum.SelectedItem.ToString());
            sampleInfo.SampleType = combSampleType.SelectedItem.ToString();
            sampleInfo.SamContainer = combSampleContainer.SelectedItem.ToString();
            sampleInfo.IsEmergency = chkEmergency.Checked;
            sampleInfo.Barcode = txtBarCode.Text;
            sampleInfo.IsOperateDilution = chkManuallyDilute.Checked;
            sampleInfo.CreateTime = createTime;
            sampleInfo.SampleState = 0;

            List<TaskInfo> lstTaskInfo = new List<TaskInfo>();

            if (lstDiluteInfos.Count > 0)
            {
                foreach (string[] strDiluteInfo in lstDiluteInfos)
                {
                    TaskInfo taskInfo = new TaskInfo();
                    taskInfo.SampleNum = sampleInfo.SampleNum;
                    taskInfo.CreateDate = createTime;
                    taskInfo.ProjectName = strDiluteInfo[0];
                    taskInfo.SampleType = combSampleType.SelectedItem.ToString();
                    taskInfo.SampleDilute = strDiluteInfo[1];
                    if (taskInfo.SampleDilute == "自定义")
                    {
                        taskInfo.DilutedRatio = System.Convert.ToInt32(strDiluteInfo[2]);
                    }
                    else
                    {
                        taskInfo.DilutedRatio = 0;
                    }
                    taskInfo.InspectTimes = System.Convert.ToInt32(txtBoxDetectionNum.Text.Trim());
                    taskInfo.SendTimes = 0;
                    taskInfo.FinishTimes = 0;
                    taskInfo.TaskState = 0;

                    lstTaskInfo.Add(taskInfo);
                }
            }
            else
            {
                List<string> lstSelectedProName = new List<string>();
                lstSelectedProName.AddRange(projectPage1.GetSelectedProjects());
                lstSelectedProName.AddRange(projectPage2.GetSelectedProjects());
                lstSelectedProName.AddRange(projectPage3.GetSelectedProjects());
                lstSelectedProName.AddRange(projectPage4.GetSelectedProjects());

                foreach (string ProName in lstSelectedProName)
                {
                    TaskInfo taskInfo = new TaskInfo();
                    taskInfo.SampleNum = sampleInfo.SampleNum;
                    taskInfo.CreateDate = createTime;
                    taskInfo.ProjectName = ProName;
                    taskInfo.SampleType = combSampleType.SelectedItem.ToString();
                    taskInfo.SampleDilute = "常规体积";
                    taskInfo.DilutedRatio = 0;
                    taskInfo.InspectTimes = System.Convert.ToInt32(txtBoxDetectionNum.Text.Trim());
                    taskInfo.SendTimes = 0;
                    taskInfo.FinishTimes = 0;
                    taskInfo.TaskState = 0;

                    lstTaskInfo.Add(taskInfo);
                }
            }

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity),
            //    new CommunicationEntity("AddTask",
            //        XmlUtility.Serializer(typeof(SampleInfo), sampleInfo),
            //        XmlUtility.Serializer(typeof(List<TaskInfo>), lstTaskInfo))));
            dic.Clear();
            dic.Add("AddTask", new object[] { XmlUtility.Serializer(typeof(SampleInfo), sampleInfo), XmlUtility.Serializer(typeof(List<TaskInfo>), lstTaskInfo) });
            ClientSendToServices(dic);
            Thread.Sleep(4000);
            btnSave.Enabled = true;
        }
        /// <summary>
        /// 任务列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvTask_Click(object sender, EventArgs e)
        {
            int selectedHandle;

            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                int SampleNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "样本编号").ToString());
                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoBySampleNum", SampleNum.ToString())));
                dic.Clear();
                dic.Add("QueryTaskInfoBySampleNum", new object[] { SampleNum.ToString() });
                ClientSendToServices(dic);

                foreach (SampleInfo sampleInfo in lstSampleInfo)
                {
                    if (sampleInfo.SampleNum == SampleNum)
                    {
                        txtSampleNum.Text = sampleInfo.SampleNum.ToString();
                        combPanelNum.SelectedItem = sampleInfo.PanelNum.ToString();
                        combPosNum.SelectedItem = sampleInfo.SamplePos.ToString();
                        combSampleType.SelectedItem = sampleInfo.SampleType;
                        combSampleContainer.SelectedItem = sampleInfo.SamContainer;
                        chkEmergency.Checked = sampleInfo.IsEmergency;
                        txtBarCode.Text = sampleInfo.Barcode;
                        chkManuallyDilute.Checked = sampleInfo.IsOperateDilution;
                    }
                }
            }
        }
        /// <summary>
        /// 批量录入按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBatchInput_Click(object sender, EventArgs e)
        {
            if (projectPage1.GetSelectedProjects().Count == 0 &&
                projectPage2.GetSelectedProjects().Count == 0 &&
                projectPage3.GetSelectedProjects().Count == 0 &&
                projectPage4.GetSelectedProjects().Count == 0)
            {
                MessageBox.Show("检测项目不能为空，请选择待检测项目，进行批量输入！");
                return;
            }
            if (txtBoxDetectionNum.Text != "1")
            {
                MessageBox.Show("批量录入项目的检测次数只能是1次！");
                txtBoxDetectionNum.Text = "1";
                return;
            }

            batchInput.clera();

            List<int> lstSampleNum = new List<int>();
            foreach (SampleInfo s in lstSampleInfo)
            {
                lstSampleNum.Add(s.SampleNum);
            }
            batchInput.LstSampleNum = lstSampleNum;
            batchInput.SampleNum = txtSampleNum.Text;
            batchInput.DataTransferEvent += batchInput_DataTransferEvent;
            batchInput.EndBatchEvent += batchInput_EndBatchEvent;
            batchInput.StartPosition = FormStartPosition.CenterScreen;
            batchInput.ShowDialog();
        }
        /// <summary>
        /// 委托批量录入完成后要执行的事件
        /// </summary>
        private void batchInput_EndBatchEvent()
        {
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryApplyTaskLsvt", null)));
            dic.Clear();
            dic.Add("QueryApplyTaskLsvt", new object[] { "" });
            ClientSendToServices(dic);
        }
        /// <summary>
        /// 批量录入的委托事件
        /// </summary>
        /// <param name="intStartSamNum"></param>
        /// <param name="samAmount"></param>
        private void batchInput_DataTransferEvent(int intStartSamNum, int samAmount)
        {
            //批量录入给服务器传递参数的集合
            List<object> inputDictionary = new List<object>();
            List<int> lstSamNum = new List<int>();
            List<int[]> lstPos = new List<int[]>();
            foreach (SampleInfo s in lstSampleInfo)
            {
                lstSamNum.Add(s.SampleNum);
                lstPos.Add(new int[] { s.PanelNum, s.SamplePos });
            }

            for (int i = 0; i < samAmount; i++)
            {
                DateTime createTime = DateTime.Now;

                SampleInfo sampleInfo = new SampleInfo();
                // 样本号
                int samNum = 0;
                while (lstSamNum.Contains(intStartSamNum + i + samNum))
                {
                    samNum++;
                }
                sampleInfo.SampleNum = intStartSamNum + i + samNum;
                lstSamNum.Add(intStartSamNum + i + samNum);

                // 样本盘号和样本位置
                int panel = 0, pos = 0;

                bool b = true;
                while (b)
                {
                    bool exist = false;
                    foreach (int[] info in lstPos)
                    {
                        if (System.Convert.ToInt32(combPanelNum.SelectedItem.ToString()) + panel == info[0] && System.Convert.ToInt32(combPosNum.SelectedItem.ToString()) + pos == info[1])
                        {
                            exist = true;
                        }
                    }

                    if (exist)
                    {
                        if (pos >= 120)
                        {
                            panel++;
                            pos = 1;
                        }
                        else
                        {
                            pos++;
                        }
                    }
                    else
                    {
                        b = false;
                    }
                }


                sampleInfo.PanelNum = System.Convert.ToInt32(combPanelNum.SelectedItem.ToString()) + panel;
                sampleInfo.SamplePos = System.Convert.ToInt32(combPosNum.SelectedItem.ToString()) + pos;
                lstPos.Add(new int[] { System.Convert.ToInt32(combPanelNum.SelectedItem.ToString()) + panel, System.Convert.ToInt32(combPosNum.SelectedItem.ToString()) + pos });

                sampleInfo.SampleType = combSampleType.SelectedItem.ToString();
                sampleInfo.SamContainer = combSampleContainer.SelectedItem.ToString();
                sampleInfo.IsEmergency = chkEmergency.Checked;
                sampleInfo.Barcode = txtBarCode.Text;
                sampleInfo.IsOperateDilution = chkManuallyDilute.Checked;
                sampleInfo.CreateTime = createTime;
                sampleInfo.SampleState = 0;

                List<TaskInfo> lstTaskInfo = new List<TaskInfo>();

                if (lstDiluteInfos.Count > 0)
                {
                    foreach (string[] strDiluteInfo in lstDiluteInfos)
                    {
                        TaskInfo taskInfo = new TaskInfo();
                        taskInfo.SampleNum = sampleInfo.SampleNum;
                        taskInfo.CreateDate = createTime;
                        taskInfo.ProjectName = strDiluteInfo[0];
                        taskInfo.SampleDilute = strDiluteInfo[1];
                        if (taskInfo.SampleDilute == "自定义")
                        {
                            taskInfo.DilutedRatio = System.Convert.ToInt32(strDiluteInfo[2]);
                        }
                        else
                        {
                            taskInfo.DilutedRatio = 0;
                        }
                        taskInfo.InspectTimes = System.Convert.ToInt32(txtBoxDetectionNum.Text.Trim());
                        taskInfo.SendTimes = 0;
                        taskInfo.FinishTimes = 0;
                        taskInfo.TaskState = 0;

                        lstTaskInfo.Add(taskInfo);
                    }
                }
                else
                {
                    List<string> lstSelectedProName = new List<string>();
                    lstSelectedProName.AddRange(projectPage1.GetSelectedProjects());
                    lstSelectedProName.AddRange(projectPage2.GetSelectedProjects());
                    lstSelectedProName.AddRange(projectPage3.GetSelectedProjects());
                    lstSelectedProName.AddRange(projectPage4.GetSelectedProjects());

                    foreach (string ProName in lstSelectedProName)
                    {
                        TaskInfo taskInfo = new TaskInfo();
                        taskInfo.SampleNum = sampleInfo.SampleNum;
                        taskInfo.CreateDate = createTime;
                        taskInfo.ProjectName = ProName;
                        taskInfo.SampleType = sampleInfo.SampleType;
                        taskInfo.SampleDilute = "常规体积";
                        taskInfo.DilutedRatio = 0;
                        taskInfo.InspectTimes = System.Convert.ToInt32(txtBoxDetectionNum.Text.Trim());
                        taskInfo.SendTimes = 0;
                        taskInfo.FinishTimes = 0;
                        taskInfo.TaskState = 0;

                        lstTaskInfo.Add(taskInfo);
                    }
                }

                inputDictionary.Add(XmlUtility.Serializer(typeof(List<object>), new List<object>() { XmlUtility.Serializer(typeof(SampleInfo), sampleInfo), XmlUtility.Serializer(typeof(List<TaskInfo>), lstTaskInfo) }));
                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity),
                //    new CommunicationEntity("AddTaskForBatch",
                //        XmlUtility.Serializer(typeof(SampleInfo), sampleInfo),
                //        XmlUtility.Serializer(typeof(List<TaskInfo>), lstTaskInfo))));
            }
            //CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaApplyTask, new Dictionary<string, List<object>>() {{"AddTaskForBatch",inputDictionary}});
            dic.Clear();
            dic.Add("AddTaskForBatch",new object[]{inputDictionary});
            
        }
        AnologSamplePanel anologSamplePanel;
        private void btnSampleDishState_Click(object sender, EventArgs e)
        {
            anologSamplePanel = new AnologSamplePanel(combPanelNum.Text);
            anologSamplePanel.ShowDialog();
        }
        /// <summary>
        /// 限制检测次数输入只能是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxDetectionNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)8)
            {
                e.Handled = false;
            }
            
            
        }
        /// <summary>
        /// 限制检测次数不能超过161
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxDetectionNum_Leave(object sender, EventArgs e)
        {
            if (txtBoxDetectionNum.Text != "")
            {
                if (Convert.ToInt32(txtBoxDetectionNum.Text.Trim()) > 160)
                {
                    MessageBox.Show("输入的检测次数不能超过161！");
                    txtBoxDetectionNum.Focus();
                    return;
                }
                if (Convert.ToInt32(txtBoxDetectionNum.Text.Trim()) == 0)
                {
                    txtBoxDetectionNum.Text = "1";
                } 
            }
        }

    }
}
