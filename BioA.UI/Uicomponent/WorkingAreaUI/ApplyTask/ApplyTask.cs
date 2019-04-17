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
using BioA.Service;
using BioA.SqlMaps;
using DevExpress.Office.Utils;

namespace BioA.UI
{
    public partial class ApplyTask : DevExpress.XtraEditors.XtraUserControl
    {
        //项目第一页~~ 4页 窗体
        private ProjectPage1 projectPage1 = new ProjectPage1(); 
        private ProjectPage2 projectPage2 = new ProjectPage2();
        private ProjectPage3 projectPage3 = new ProjectPage3();
        private ProjectPage4 projectPage4 =new ProjectPage4();
        //组合项目第一页和第二页窗体
        private ProCombPage1 proCombPage1 =new ProCombPage1();
        private ProCombPage2 proCombPage2 =new ProCombPage2();
        // 最大样本号
        private int intMaxSampleNum = 0;
        // 任务列表信息存储
        List<SampleInfo> lstSampleInfo = new List<SampleInfo>();
        // 样本稀释比例
        List<float> lstDilutionRatio = new List<float>();
        
        // 项目稀释设置
        List<string[]> lstDiluteInfos = new List<string[]>();
        //存储客户端发送信息给服务器的参数集合
        Dictionary<string, object[]> dic = new Dictionary<string, object[]>();
        /// <summary>
        /// 存储所有获取到的组合项目名和项目名
        /// </summary>
        private List<CombProjectInfo> lstCombProInfo = new List<CombProjectInfo>();
        //批量录入信息窗体
        frmBatchInput batchInput = new frmBatchInput();
        //病人信息窗体
        PatientInfoFrm patientInfofrm;
        private MyBatis myBatis = new MyBatis();
        //申明一个获取任务状态的委托
        public delegate bool getOPID();
        public event getOPID getopid;
        public ApplyTask()
        {
            InitializeComponent();
            xtraTabPage1.Controls.Add(projectPage1);
            xtraTabPage2.Controls.Add(projectPage2);
            xtraTabPage3.Controls.Add(projectPage3);
            xtraTabPage4.Controls.Add(projectPage4);
            xtraTabPage5.Controls.Add(proCombPage1);
            xtraTabPage6.Controls.Add(proCombPage2);
            proCombPage1.clickProCombNamePageEvent += HenderClickProCombNamePageEvent;
            proCombPage2.clickProCombNamePage2Event += HenderClickProCombNamePageEvent;
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.FocusedRow.ForeColor = Color.Red;
            gridView1.Appearance.Row.Font = font;

            combPanelNum.Properties.Items.AddRange(RunConfigureUtility.SamplePanel);
            combPosNum.Properties.Items.AddRange(RunConfigureUtility.SamplePosition);
            combSampleType.Properties.Items.AddRange(RunConfigureUtility.SampleTypes);
            combSampleContainer.Properties.Items.AddRange(RunConfigureUtility.SampleContainerList);


            
        }
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ApplyTask_Load(object o, EventArgs e)
        {
            ApplyTaskInit();
        }

        public void ClearApplyTaskMemberVariable()
        {
            intMaxSampleNum = 0;
            lstSampleInfo.Clear();
            lstDiluteInfos.Clear();
            dic.Clear();
            lstDilutionRatio.Clear();
            lstCombProInfo.Clear();
            combSampleType.SelectedIndex = -1;
            combSampleContainer.SelectedIndex = -1;
        }
        /// <summary>
        /// 异步启动线程
        /// </summary>
        public void ApplyTaskInit()
        {
            combSampleType.SelectedIndex = 1;
            combSampleContainer.SelectedIndex = 0;
            //获取最大样本编号
            dic.Add("QueryMaxSampleNum", new object[] { "" });
            //获取所有结果单位
            dic.Add("QuerySampleDiluteRatio", null);
            //获取所有任务
            dic.Add("QueryApplyTaskLsvt", new object[] { "" });
            //获取所有组合项目信息
            dic.Add("QueryCombProjectNameAllInfo", new object[] { "" });
            //获取所有组合项目名和生化项目名
            dic.Add("QueryProjectAndCombProName", null);
            ClientSendToServices(dic);
            this.txtBarCode.Focus();
        }

        /// <summary>
        /// 任务执行中获取任务执行状态
        /// </summary>
        public void QueryTasksStatus()
        {
            dic.Clear();
            dic.Add("QueryApplyTaskLsvt", new object[] { "" });
            ClientSendToServices(dic);
        }

        /// <summary>
        /// 处理组合项目名点击事件
        /// </summary>
        /// <param name="sender"></param>
        private bool HenderClickProCombNamePageEvent(string sender, string tag)
        {

            exceptionItemInfoList.Clear();
            //存储项目名称
            List<string> lstProNames = new List<string>();
            foreach (CombProjectInfo combProInfo in lstCombProInfo)
            {
                if (combProInfo.CombProjectName == sender)
                {
                    lstProNames.Add(combProInfo.ProjectName);
                }
            }
            if (lstProNames.Count > 0)
            {
                bool ret1 = setprojectpage(lstProNames, projectPage1.Controls, tag);

                bool ret2 = setprojectpage(lstProNames, projectPage2.Controls, tag);

                bool ret3 = setprojectpage(lstProNames, projectPage3.Controls, tag);

                bool ret4 = setprojectpage(lstProNames, projectPage4.Controls, tag);
                if (exceptionItemInfoList.Count > 0)
                {
                    string resultInfo = string.Join(",",exceptionItemInfoList.Select(s => "[" + s + "]"));
                    this.Invoke(new EventHandler(delegate { MessageBox.Show(resultInfo + "项目参数有误！"); }));
                }
                if (ret1 == false && ret2 == false && ret3 == false && ret4 == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        private List<string> exceptionItemInfoList = new List<string>();

        /// <summary>
        /// 提示组合项目中出现有问题的项目提示
        /// </summary>
        /// <param name="selectedProjects"></param>
        /// <param name="Controls"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private bool setprojectpage(List<string> selectedProjects, ControlCollection Controls, string tag)
        {
            bool flag = false;
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
        /// 把客户端信息发送给服务器
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="sender"></param>
        private void ClientSendToServices(Dictionary<string, object[]> param)
        {
            new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaApplyTask, param);
            })
            { IsBackground = true }.Start();
        }

        //样本盘号
        List<int> lstPanel = new List<int>();
        //样本位置
        List<int> lstPosition = new List<int>();
        //样本编号
        List<int> lstSamNum = new List<int>();
        /// <summary>
        /// 盘号和位置对应关系集合
        /// </summary>
        List<string> lstPanelPos = new List<string>();
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
                    this.BeginInvoke(new EventHandler(delegate{grpProject.SelectedTabPageIndex = 0 ;}));
                    break;
                case "QueryCombProjectNameAllInfo":
                    List<string> lstCombProName = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    proCombPage1.LstProjectGroups = lstCombProName;
                    proCombPage2.LstAssayProInfos = lstCombProName;
                    this.BeginInvoke(new EventHandler(delegate { grpCombProject.SelectedTabPageIndex = 0; }));
                    break;
                case "QueryApplyTaskLsvt":
                    lstSampleInfo = (List<SampleInfo>)XmlUtility.Deserialize(typeof(List<SampleInfo>), sender as string);
                    //项目任务数据存储
                    DataTable dt = new DataTable();
                    dt.Columns.Add("样本编号");
                    dt.Columns.Add("盘号");
                    dt.Columns.Add("位置");
                    dt.Columns.Add("样本状态");

                    lstPanel.Clear();
                    lstPosition.Clear();
                    lstSamNum.Clear();
                    dt.Rows.Clear();
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

                            dt.Rows.Add(new object[] { s.SampleNum.ToString(),s.PanelNum.ToString(), s.SamplePos.ToString(), strState });
                            lstPanel.Add(s.PanelNum);
                            lstPosition.Add(s.SamplePos);
                            lstSamNum.Add(s.SampleNum);
                            lstPanelPos.Add(s.PanelNum+"|"+s.SamplePos);
                        }
                    }
                    this.Invoke(new EventHandler(delegate
                        {
                            lstvTask.DataSource = dt;
                            this.SamplePanelAndPosNum();
                            txtSampleNum.Text = lstSamNum.Count > 0 ? (lstSamNum.Max() + 1).ToString() : (1).ToString();
                            intMaxSampleNum = System.Convert.ToInt32(txtSampleNum.Text) - 1;
                        }));
                    break;
                case "QuerySampleDiluteRatio":
                    lstDilutionRatio = (List<float>)XmlUtility.Deserialize(typeof(List<float>), sender as string);
                    break;
                case "QueryProjectAndCombProName":
                    lstCombProInfo = (List<CombProjectInfo>)XmlUtility.Deserialize(typeof(List<CombProjectInfo>), sender as string);
                    break;
                case "QueryTaskInfoBySampleNum":
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
                case "QueryDepartmentInfo":
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
        /// <summary>
        /// 扫码创建项目任务事件
        /// </summary>
        public void SMPScanBracodeCreateTask_Event()
        {
            this.ApplyTask_Load(null,null);
        }

        private void grpProject_Click(object sender, EventArgs e)
        {
            if (grpProject.SelectedTabPageIndex == 0)
            {
                //xtraTabPage1.Controls.Add(projectPage1);
            }
            else if (grpProject.SelectedTabPageIndex == 1)
            {
                //xtraTabPage2.Controls.Add(projectPage1);
            }
            else if (grpProject.SelectedTabPageIndex == 2)
            {
                //xtraTabPage3.Controls.Add(projectPage1);
            }
            else if (grpProject.SelectedTabPageIndex == 3)
            {
                //xtraTabPage4.Controls.Add(projectPage1);
            }
        }

        private void grpCombProject_Click(object sender, EventArgs e)
        {
            if (grpCombProject.SelectedTabPageIndex == 0)
            {
                //xtraTabPage5.Controls.Add(proCombPage1);
            }
            else if (grpCombProject.SelectedTabPageIndex == 1)
            {
                //xtraTabPage6.Controls.Add(proCombPage2);
            }
        }
        /// <summary>
        /// 病人参数录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPatientInfo_Click(object sender, EventArgs e)
        {
            
            int selectedHandle;

            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                if (patientInfofrm == null)
                {
                    patientInfofrm = new PatientInfoFrm();
                    patientInfofrm.StartPosition = FormStartPosition.CenterScreen;
                }
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                int SampleNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "样本编号").ToString());
                patientInfofrm.IntSelectedNum = SampleNum;

                List<int> lstSampleNum = new List<int>();
                foreach (SampleInfo s in lstSampleInfo)
                {
                    lstSampleNum.Add(s.SampleNum);
                }
                patientInfofrm.LstSampleNum = lstSampleNum;
                patientInfofrm.PatientInfo_Load(null,null);
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

            txtSampleNum.Text = lstSamNum.Count > 0 ? (lstSamNum.Max() + 1).ToString() : (1).ToString();

            this.SamplePanelAndPosNum();
            SimpleButCancel_Click(null,null);

            lstDiluteInfos.Clear();
        }
        /// <summary>
        /// 稀释比例设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    lstTemporary.Add(new string[] { str, "常规体积", "" });
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
            if (combSampleType.SelectedIndex != -1)
            {
                if (dic.Count == 6 || dic.Count == 1)
                {
                    dic.Clear();
                    dic.Add("QueryProNameForApplyTask", new object[] { combSampleType.SelectedItem.ToString() });
                    ClientSendToServices(dic);
                }
                else
                {
                    dic.Add("QueryProNameForApplyTask", new object[] { combSampleType.SelectedItem.ToString() });
                }
                lstDiluteInfos.Clear();
            }
        }
        /// <summary>
        /// 点击保存项目事件
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
                    DialogResult dr = MessageBox.Show("此样本号已被申请任务，请修改样本号！");
                    btnSave.Enabled = true;
                    if (dr == DialogResult.OK)
                    {
                        btnApply_Click(null, null);
                    }
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

            string SaveIsSuccess = new WorkAreaApplyTask().AddTask("AddTask", sampleInfo, lstTaskInfo);
            if (SaveIsSuccess == "此样本任务已经存在，请重新录入！")
            {
                MessageBox.Show("此样本任务已经存在，请重新录入！");
                btnSave.Enabled = true;
                return;
            }
            else
            {
                dic.Clear();
                dic.Add("QueryApplyTaskLsvt", new object[] { "" });
                ClientSendToServices(dic);
            }
            btnSave.Enabled = true;
            SimpleButCancel_Click(null, null);
        }
        /// <summary>
        /// 任务列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvTask_Click(object sender, EventArgs e)
        {
            SimpleButCancel_Click(null, null);
            int selectedHandle;

            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                int SampleNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "样本编号").ToString());
                int PanelNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "盘号").ToString());

                foreach (SampleInfo sampleInfo in lstSampleInfo)
                {
                    if (sampleInfo.SampleNum == SampleNum && sampleInfo.PanelNum == PanelNum)
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
                List<TaskInfo> lstTaskInfos = new WorkAreaApplyTask().QueryTaskInfoBySampleNum("QueryTaskInfoBySampleNum", SampleNum.ToString());
                lstDiluteInfos.Clear();
                List<string> lstProjects1 = new List<string>();
                foreach (TaskInfo t in lstTaskInfos)
                {
                    string[] strTaskInfo = new string[3];
                    strTaskInfo[0] = t.ProjectName;
                    strTaskInfo[1] = t.SampleDilute;
                    strTaskInfo[2] = t.DilutedRatio.ToString();
                    txtBoxDetectionNum.Text = t.InspectTimes.ToString();
                    lstDiluteInfos.Add(strTaskInfo);
                    lstProjects1.Add(t.ProjectName);
                }
                projectPage1.SelectedProjects = lstProjects1;
                projectPage2.SelectedProjects = lstProjects1;
                projectPage3.SelectedProjects = lstProjects1;
                projectPage4.SelectedProjects = lstProjects1;
                
            }
        }
        /// <summary>
        /// 批量录入按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBatchInput_Click(object sender, EventArgs e)
        {
            if (getopid != null)
            {
                if (!getopid())
                {
                    MessageBox.Show("当前任务正在测试，暂停后方可继续下任务!");
                    return;
                }
            }
            if (projectPage1.GetSelectedProjects().Count == 0 &&
                projectPage2.GetSelectedProjects().Count == 0 &&
                projectPage3.GetSelectedProjects().Count == 0 &&
                projectPage4.GetSelectedProjects().Count == 0)
            {
                MessageBox.Show("检测项目不能为空，请选择待检测项目，进行批量输入！");
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
            object[] inputDictionary = new object[samAmount];
            List<object> lstObjec = new List<object>();
            List<int> lstSamNum = new List<int>();
            List<int[]> lstPos = new List<int[]>();
            foreach (SampleInfo s in lstSampleInfo)
            {
                lstSamNum.Add(s.SampleNum);
                lstPos.Add(new int[] { s.PanelNum, s.SamplePos });
            }

            // 样本盘号
            int panel = 0;

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
                //样本位置
                int pos = 0;

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
                        if (System.Convert.ToInt32(combPosNum.SelectedItem.ToString()) + pos >= 120)
                        {
                            panel++;
                            combPosNum.SelectedItem = "1";
                            pos = 0;
                            lstPos.Clear();
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
                inputDictionary[i] = (new object[] {sampleInfo,lstTaskInfo });
            }
            dic.Clear();
            //添加批量录入的任务信息
            WorkAreaApplyTask workAreaApplyTask = new WorkAreaApplyTask();
            batchInput.LstReceiveInfo = workAreaApplyTask.BatchAddTask("AddTaskForBatch", inputDictionary);
            MessageBox.Show("批量录入执行完成！");       
            batchInput.DataTransferEvent -= batchInput_DataTransferEvent;
            batchInput.Close();
        }
        AnologSamplePanel anologSamplePanel;
        private void BtnSampleDishState_Click(object sender, EventArgs e)
        {
            if (anologSamplePanel == null)
            {
                anologSamplePanel = new AnologSamplePanel();
            }
            anologSamplePanel.PanelNumber = combPanelNum.Text;
            anologSamplePanel.AnologSamplePanel_Load(null, null);
            anologSamplePanel.ShowDialog();
        }
        /// <summary>
        /// 限制检测次数输入只能是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtBoxDetectionNum_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtBoxDetectionNum_Leave(object sender, EventArgs e)
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
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleButCancel_Click(object sender, EventArgs e)
        {
            //调用取消函数
            ResetControlState(projectPage1.Controls);
            ResetControlState(projectPage2.Controls);
            ResetControlState(projectPage3.Controls);
            ResetControlState(projectPage4.Controls);
            ResetControlState(proCombPage1.Controls);
            ResetControlState(proCombPage2.Controls);
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
        /// <summary>
        /// 样本盘号和样本位置
        /// </summary>
        private void SamplePanelAndPosNum()
        {
            int iPanel = lstPanel.Count > 0 ? lstPanel.Max() : 0;


            int iPosition = lstPosition.Count > 0 ? lstPosition.Max() : 0;


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
            else if (iPosition == 120)
            {
                int posmax = 1;//最大位置
                List<int> pos = new List<int>();//最大盘号对应的所有位置的集合
                foreach (string s in lstPanelPos)
                {
                    string[] ss = s.Split('|');
                    if (ss[0] == iPanel.ToString())
                    {
                        pos.Add(Convert.ToInt32(ss[1]));
                    }
                    else
                    {
                        continue;
                    }
                }
                if (pos.Count > 0)
                {
                    posmax = pos.Max();
                    if (pos.Contains(120))
                    {
                        iPanel++;
                        posmax = 0;
                    }
                }
                combPanelNum.SelectedItem = iPanel.ToString();
                combPosNum.SelectedItem = (posmax+1).ToString();    
            }
        }
        /// <summary>
        /// 更新盘号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combPanelNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.combPanelNum.SelectedItem != null)
            //{
            //    myBatis.UpdateRunningTaskWorDisk("UpdateRunningTaskWorDisk", this.combPanelNum.SelectedItem.ToString());
            //}
        }

        public delegate void SMPBracodInput(string sender);
        /// <summary>
        /// 样本条码输入委托事件
        /// </summary>
        /// <param name="sender"></param>
        public event SMPBracodInput SMPBracodInputEvent;

        /// <summary>
        /// 样本条码输入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarCode_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtBarCode.Text.Length == 7)
            {
                if (getopid != null && !getopid())
                {
                    MessageBox.Show("当前任务正在测试，暂停后方可继续下任务!");
                    return;
                }
                this.SMPBracodInputEvent(this.txtBarCode.Text);
            }
            else
            {
                return;
            }
        }
    }
}
