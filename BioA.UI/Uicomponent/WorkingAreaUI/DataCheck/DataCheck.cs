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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Paint;
using System.Reflection;
using System.Windows.Xps.Packaging;
using System.Windows.Documents;
using System.Windows.Markup;
using System.IO;
using System.Windows.Xps;
using System.Drawing.Printing;
using BioA.SqlMaps;
using BioA.Service;
using BioA.UI.ServiceReference1;

namespace BioA.UI
{
    public partial class DataCheck : DevExpress.XtraEditors.XtraUserControl
    {
        MyBatis myBatis = new MyBatis();

        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> dataCheckDic = new Dictionary<string, object[]>();
        // 检测结果存储表
        DataTable CheckResultDT = new DataTable();
        //存储病人操作状态信息
        DataTable dt = new DataTable();
        // 审核界面
        TestAudit testAudit;
        //显示病人样本信息
        List<SampleInfoForResult> sampleInfos = new List<SampleInfoForResult>();
        //显示所有样本结果信息
        List<SampleResultInfo> lstSamResultInfo = new List<SampleResultInfo>();
        
        //反应监控曲线窗体
        ReflectionMonitoring reflectionMonitoring;

        public DataCheck()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }
        /// <summary>
        /// 根据时间段查找样本结果状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Dictionary<string, bool> dctFilter = RunConfigureUtility.ChkSampleTaskState;
            string strFilter = string.Empty;
            foreach (KeyValuePair<string, bool> key in dctFilter)
            {
                strFilter += key.Key + ":" + key.Value + ",";
            }
            strFilter = strFilter.TrimEnd(',');
            SampleInfoForResult sampleInfo = new SampleInfoForResult();

            if (chkRoutineSample.Checked == true)
            {
                sampleInfo.SampleType += "常规样本:true,";
            }
            else
            {
                sampleInfo.SampleType += "常规样本:false,";
            }

            if (chkEmergencyTreatment.Checked == true)
            {
                sampleInfo.SampleType += "急诊:true";
            }
            else
            {
                sampleInfo.SampleType += "急诊:false";
            }

            if (txtSampleNumber.Text.Trim() != "")
            {
                sampleInfo.SampleNum = System.Convert.ToInt32(txtSampleNumber.Text);
            }

            if (txtName.Text.Trim() != null)
            {
                sampleInfo.PatientName = txtName.Text.Trim();
            }

            if (txtCaseNumber.Text.Trim() != "")
            {
                sampleInfo.SampleID = txtCaseNumber.Text.Trim();
            }

            sampleInfo.StartTime = dtpInspectTimeStart.Value;
            sampleInfo.EndTime = dtpInspectTimeOld.Value.AddDays(1);
            
            dataCheckDic.Clear();
            dataCheckDic.Add("QueryCommonSampleData", new object[] { XmlUtility.Serializer(typeof(SampleInfoForResult), sampleInfo), strFilter });
            SendToServices(dataCheckDic);

        }
        /// <summary>
        /// 筛选点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            ScreeningSample screeningSample = new ScreeningSample();
            screeningSample.StartPosition = FormStartPosition.CenterScreen;
            screeningSample.ShowDialog();
        }
        /// <summary>
        /// 筛选启动点击改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFilterOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFilterOpen.Checked)
            {
                chkFilterClose.Checked = false;
            }
            else if (chkFilterOpen.Checked == false && chkFilterClose.Checked == false)
            {
                chkFilterOpen.Checked = true;
            }
        }
        /// <summary>
        /// 筛选关闭点击改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFilterClose_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFilterClose.Checked)
            {
                chkFilterOpen.Checked = false;
            }
            else if (chkFilterClose.Checked == false && chkFilterOpen.Checked == false)
            {
                chkFilterClose.Checked = true;
            }
        }
        /// <summary>
        /// 审核点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExamine_Click(object sender, EventArgs e)
        {
            testAudit.SampleInfos = sampleInfos;

            if (this.gridView1.SelectedRowsCount > 0)
            {
                SampleInfoForResult sampleInfo = new SampleInfoForResult();
                int selectedNum = this.gridView1.GetSelectedRows()[0];

                sampleInfo.SampleNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedNum, "样本编号"));
                sampleInfo.CreateTime = System.Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedNum, "申请时间"));

                testAudit.SampleInfo = sampleInfo;
                testAudit.CurrentClickLineNumber =gridView1.FocusedRowHandle + 1;
                testAudit.StartPosition = FormStartPosition.CenterScreen;
                testAudit.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择病人信息！");
                return;
            }

        }
        /// <summary>
        /// 反应监控点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReactionMonitoring_Click(object sender, EventArgs e)
        {
            if (gridView2.SelectedRowsCount > 0 && gridView1.SelectedRowsCount > 0)
            {
                SampleInfoForResult sampleInfo = new SampleInfoForResult();
                int selectedNum = this.gridView1.GetSelectedRows()[0];

                sampleInfo.SampleNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedNum, "样本编号"));
                sampleInfo.CreateTime = System.Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedNum, "申请时间"));

                reflectionMonitoring.LstSampleResInfo = lstSamResultInfo;
                int selectNum = gridView2.GetSelectedRows()[0];
                SampleResultInfo sampleRes = new SampleResultInfo();
                sampleRes.ProjectName = gridView2.GetRowCellValue(selectNum, "检测项目") as string;
                sampleRes.ConcResult = (float)System.Convert.ToDouble(gridView2.GetRowCellValue(selectNum, "检测结果"));
                sampleRes.SampleCompletionTime = System.Convert.ToDateTime(gridView2.GetRowCellValue(selectNum, "测试时间"));
                reflectionMonitoring.SampleResInfo = sampleRes;
                reflectionMonitoring.SampleInfoForRes = sampleInfo;

                reflectionMonitoring.StartPosition = FormStartPosition.CenterScreen;
                reflectionMonitoring.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择病人信息！");
                return;
            }

        }

        private void DataCheck_Load(object sender, EventArgs e)
        {
            //chkFilterOpen.Checked = 
            //异步方法调用
            BeginInvoke(new Action(loadDataCheck));

        }
        private void loadDataCheck()
        {
            gridView2.OptionsSelection.MultiSelect = true;
            gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView2.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;

            testAudit = new TestAudit();
            reflectionMonitoring = new ReflectionMonitoring(false);
            if ((RunConfigureUtility.ChkSampleTaskState)["FilterSwitch"])
            {
                chkFilterOpen.Checked = true;
            }
            else
            {
                chkFilterClose.Checked = false;
            }
            chkRoutineSample.Checked = true;
            chkEmergencyTreatment.Checked = true;
            dtpInspectTimeStart.Value = DateTime.Now;
            dtpInspectTimeOld.Value = DateTime.Now;



            CheckResultDT.Columns.Add("检测项目");
            CheckResultDT.Columns.Add("检测结果");
            CheckResultDT.Columns.Add("单位");
            CheckResultDT.Columns.Add("测试时间");
            CheckResultDT.Columns.Add("进程编号");
            CheckResultDT.Columns.Add("任务状态");
            CheckResultDT.Columns.Add("复查");
            CheckResultDT.Columns.Add("备注");
            CheckResultDT.Columns.Add("确认", typeof(Boolean));

            lstvInspectProInfo.DataSource = CheckResultDT;
            gridView2.Columns[0].Width = 60;
            gridView2.Columns[1].Width = 40;
            gridView2.Columns[2].Width = 40;
            gridView2.Columns[4].Width = 50;
            gridView2.Columns[5].Width = 30;
            gridView2.Columns[7].Width = 50;
            gridView2.Columns[8].Width = 30;
            gridView2.Columns[0].OptionsColumn.AllowEdit = false;
            gridView2.Columns[1].OptionsColumn.AllowEdit = false;
            gridView2.Columns[2].OptionsColumn.AllowEdit = false;
            gridView2.Columns[3].OptionsColumn.AllowEdit = false;
            gridView2.Columns[4].OptionsColumn.AllowEdit = false;
            gridView2.Columns[5].OptionsColumn.AllowEdit = false;
            gridView2.Columns[6].OptionsColumn.AllowEdit = false;
            gridView2.Columns[7].OptionsColumn.AllowEdit = false;
            gridView2.Columns[8].OptionsColumn.AllowEdit = true;

            dt.Columns.Add("样本编号");
            dt.Columns.Add("样本ID");
            dt.Columns.Add("样本类型");
            dt.Columns.Add("患者名称");
            dt.Columns.Add("性别");
            dt.Columns.Add("年龄");
            dt.Columns.Add("申请时间");
            dt.Columns.Add("样本状态");
            dt.Columns.Add("审核状态");
            dt.Columns.Add("打印状态");
            dt.Columns.Add("手动稀释");
            lstvSampleInfo.DataSource = dt;

            //异步线程调用
            BeginInvoke(new Action(LoadCommonSampleData));
        }

        /// <summary>
        /// 发送给服务器
        /// </summary>
        /// <param name="param"></param>
        private void SendToServices(Dictionary<string, object[]> param)
        {
            new Thread(() => { CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck, param); }) { IsBackground = true }.Start();
        }

        private void LoadCommonSampleData()
        {
            Dictionary<string, bool> dctFilter = RunConfigureUtility.ChkSampleTaskState;
            string strFilter = string.Empty;
            foreach (KeyValuePair<string, bool> key in dctFilter)
            {
                strFilter += key.Key + ":" + key.Value + ",";
            }
            strFilter = strFilter.TrimEnd(',');
            SampleInfoForResult sampleInfo = new SampleInfoForResult();

            if (chkRoutineSample.Checked == true)
            {
                sampleInfo.SampleType += "常规样本:true,";
            }
            else
            {
                sampleInfo.SampleType += "常规样本:false,";
            }

            if (chkEmergencyTreatment.Checked == true)
            {
                sampleInfo.SampleType += "急诊:true";
            }
            else
            {
                sampleInfo.SampleType += "急诊:false";
            }

            sampleInfo.StartTime = dtpInspectTimeStart.Value;
            sampleInfo.EndTime = dtpInspectTimeOld.Value.AddDays(1);
            dataCheckDic.Clear();
            dataCheckDic.Add("QueryCommonSampleData", new object[] { XmlUtility.Serializer(typeof(SampleInfoForResult), sampleInfo), strFilter });
            SendToServices(dataCheckDic);
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryCommonSampleData":
                    BeginInvoke(new Action(() =>
                    {
                        dt.Rows.Clear();
                        sampleInfos = (List<SampleInfoForResult>)XmlUtility.Deserialize(typeof(List<SampleInfoForResult>), sender as string);

                        foreach (SampleInfoForResult s in sampleInfos)
                        {
                            string sampleState = string.Empty;
                            switch (s.SampleState)
                            {
                                case 0:
                                    sampleState = "待测中";
                                    break;
                                case 1:
                                    sampleState = "检测中";
                                    break;
                                case 2:
                                    sampleState = "已完成";
                                    break;
                                case 3:
                                    sampleState = "被暂停";
                                    break;
                                default:
                                    break;
                            }
                            string age;
                            if (s.Age == 0)
                            {
                                age = "";
                            }
                            else
                            {
                                age = s.Age.ToString();
                            }

                            dt.Rows.Add(new object[] { s.SampleNum, s.SampleID, s.SampleType, s.PatientName, s.Sex, age, s.CreateTime, sampleState, s.IsAudit == false ? "未审核" : "已审核", s.PrintState == "" ? "未打印" : s.PrintState, s.IsOperateDilution ? "是" : "否" });
                        }
                        if (dt.Rows.Count > 0)
                        {
                            lstvSampleInfo.DataSource = dt;
                            gridView1.SelectRow(0);
                            lstvSampleInfo_Click(null, null);
                        }
                        else
                        {
                            CheckResultDT.Rows.Clear();
                        }
                    }));
                    break;
                case "QueryProjectResultBySampleNum":
                    BeginInvoke(new Action(() =>
                    {
                        CheckResultDT.Rows.Clear();
                        lstSamResultInfo = (List<SampleResultInfo>)XmlUtility.Deserialize(typeof(List<SampleResultInfo>), sender as string);
                        foreach (SampleResultInfo s in lstSamResultInfo)
                        {
                            string taskState = string.Empty;
                            switch (s.SampleCompletionStatus)
                            {
                                case 0:
                                    taskState = "异常";
                                    break;
                                case 1:
                                    taskState = "检测中";
                                    break;
                                case 2:
                                    taskState = "已完成";
                                    break;
                                default:
                                    break;
                            }
                            CheckResultDT.Rows.Add(new object[] { s.ProjectName, Math.Round(s.ConcResult, 4), s.UnitAndRange, s.SampleCompletionTime.ToString(),s.TCNO, taskState, s.IsResurvey == true ? "是" : "否", s.Remarks, s.Confirm });
                        }
                        if (lstSamResultInfo.Count > 0)
                        {
                            GetsReactionProcessData(lstSamResultInfo);
                        }
                    }));
                    break;
                case "QueryProjectResultForTestAudit":
                    testAudit.LstSampleResInfo = (List<SampleResultInfo>)XmlUtility.Deserialize(typeof(List<SampleResultInfo>), sender as string);
                    break;
                case "DeleteCommonSampleBySampleNum":
                    string deleteResult = sender as string;
                    if (deleteResult == "删除成功！")
                    {
                        LoadCommonSampleData();
                    }
                    else if (deleteResult == "删除失败！")
                    {
                        MessageBox.Show("删除失败！");
                        return;
                    }
                    break;
                case "ReviewCheck":
                    string reviewCheckRes = sender as string;//"复查任务添加失败！";
                    if (reviewCheckRes == "复查任务添加失败！")
                    {
                        MessageBox.Show("复查任务添加失败！请检查任务是否为当天任务以及是否为已完成状态。");
                        return;
                    }
                    else
                    {
                        int iSelected = 0;
                        if (gridView1.SelectedRowsCount > 0)
                        {
                            iSelected = gridView1.GetSelectedRows()[0];
                            int sampleNum = System.Convert.ToInt32(gridView1.GetRowCellValue(iSelected, "样本编号"));
                            DateTime dt = System.Convert.ToDateTime(gridView1.GetRowCellValue(iSelected, "申请时间"));
                            string sampleType = gridView1.GetRowCellValue(iSelected, "样本类型") as string;
                            string[] communicate = new string[] { sampleNum.ToString(), dt.ToString(), sampleType };
                            dataCheckDic.Clear();
                            dataCheckDic.Add("QueryProjectResultBySampleNum", new object[] { XmlUtility.Serializer(typeof(string[]), communicate) });
                            SendToServices(dataCheckDic);
                        }
                    }
                    break;
                case "AuditSampleTest":
                    if ((sender as string) == "审核成功！")
                    {
                        LoadCommonSampleData();
                    }
                    else
                    {
                        MessageBox.Show(sender as string);
                    }
                    break;
                case "QueryTimeCourse":
                    TimeCourseInfo sampleReactionInfo = (TimeCourseInfo)XmlUtility.Deserialize(typeof(TimeCourseInfo), sender as string);
                    reflectionMonitoring.SampleReactionInfo = sampleReactionInfo;
                    break;
                case "QueryCommonTaskReactionForAudit":
                    TimeCourseInfo sampleReacInfoForAudit = (TimeCourseInfo)XmlUtility.Deserialize(typeof(TimeCourseInfo), sender as string);
                    testAudit.SampleReactionInfo = sampleReacInfoForAudit;
                    break;
                case "BatchAuditSampleTest":
                    if ((sender as string) == "审核成功！")
                    {
                        LoadCommonSampleData();
                    }
                    else
                    {
                        MessageBox.Show(sender as string);
                    }
                    break;
                case "ConfirmCommonTask":
                    if ((sender as string) == "确认成功！")
                    {
                        lstvSampleInfo_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show(sender as string);
                    }
                    break;
            }
        }



        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridview = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            lstvInspectProInfo.DataSource = CheckResultDT;
        }

        /// 存储每点击一次来保存的样本编号
        private string sampleNum = null;

        private void lstvSampleInfo_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedDataRow() == null) return;//判断当前选中行是否为null
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.BackColor = Color.Black;
            //清空之前的样本编号
            if (sampleNum != null)
                sampleNum = null;
            //返回值
            sampleNum = gridView1.GetFocusedRowCellValue("样本编号").ToString();
            DateTime dt = System.Convert.ToDateTime(gridView1.GetFocusedRowCellValue("申请时间"));
            string sampleType = gridView1.GetFocusedRowCellValue("样本类型").ToString();
            string[] lstTaskResult = new string[] { sampleNum, dt.ToString(), sampleType };
            //dataCheckDic.Clear();
            //dataCheckDic.Add("QueryProjectResultBySampleNum", new object[] { XmlUtility.Serializer(typeof(string[]), lstTaskResult) });
            //SendToServices(dataCheckDic);
            
            lstSamResultInfo = new WorkingAreaDataCheck().QueryProjectResultBySampleNum("QueryProjectResultBySampleNum", lstTaskResult);
            BeginInvoke(new Action(() =>
            {
                CheckResultDT.Rows.Clear();
                foreach (SampleResultInfo s in lstSamResultInfo)
                {
                    string taskState = string.Empty;
                    switch (s.SampleCompletionStatus)
                    {
                        case 0:
                            taskState = "异常";
                            break;
                        case 1:
                            taskState = "检测中";
                            break;
                        case 2:
                            taskState = "已完成";
                            break;
                        default:
                            break;
                    }
                    CheckResultDT.Rows.Add(new object[] { s.ProjectName, Math.Round(s.ConcResult, 4), s.UnitAndRange, s.SampleCompletionTime.ToString(), s.TCNO, taskState, s.IsResurvey == true ? "是" : "否", s.Remarks, s.Confirm });
                }
                if (lstSamResultInfo.Count > 0)
                {
                    GetsReactionProcessData(lstSamResultInfo);
                }
            }));
            //}
        }
        /// <summary>
        /// 删除结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<SampleResultInfo> lstResultInfo= new List<SampleResultInfo>();
            if (gridView2.SelectedRowsCount > 0)
            {
                if (MessageBox.Show(string.Format("确实要删除样本结果吗？"), "删除样本数据", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    SampleResultInfo sampResultInfo = new SampleResultInfo();
                    foreach (int i in this.gridView2.GetSelectedRows())
                    {
                        sampResultInfo.ProjectName = this.gridView2.GetRowCellValue(i, "检测项目") as string;
                        sampResultInfo.TCNO =Convert.ToInt32(this.gridView2.GetRowCellValue(i, "进程编号"));
                        sampResultInfo.SampleCompletionTime = Convert.ToDateTime(this.gridView2.GetRowCellValue(i,"测试时间"));
                        lstResultInfo.Add(sampResultInfo);
                        sampResultInfo = null;
                    }
                    int result = new WorkAreaApplyTask().DeleteSampleResult(lstResultInfo);
                    if (result > 0)
                    {
                        foreach (var item in lstResultInfo)
                        {
                            lstSamResultInfo.RemoveAll(x => x.ProjectName == item.ProjectName && x.TCNO == item.TCNO && x.SampleCompletionTime == item.SampleCompletionTime);
                        }
                        CheckResultDT.Rows.Clear();
                        foreach (SampleResultInfo s in lstSamResultInfo)
                        {
                            string taskState = string.Empty;
                            switch (s.SampleCompletionStatus)
                            {
                                case 0:
                                    taskState = "异常";
                                    break;
                                case 1:
                                    taskState = "检测中";
                                    break;
                                case 2:
                                    taskState = "已完成";
                                    break;
                                default:
                                    break;
                            }
                            CheckResultDT.Rows.Add(new object[] { s.ProjectName, Math.Round(s.ConcResult, 4), s.UnitAndRange, s.SampleCompletionTime.ToString(), s.TCNO, taskState, s.IsResurvey == true ? "是" : "否", s.Remarks, s.Confirm });
                        }
                        MessageBox.Show("删除成功！");
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void btnBatchExamine_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                if (MessageBox.Show("仅会对已完成的样本进行审核，确定批量审核吗？", "批量审核", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    List<string[]> lstCommunicate = new List<string[]>();
                    foreach (int i in this.gridView1.GetSelectedRows())
                    {
                        //2018/9/2 21:46 注释掉
                        //string sampleState = this.gridView1.GetRowCellValue(i, "样本状态") as string;
                        //if (sampleState == "已完成")
                        //{
                        string[] strCommunicate = new string[2];
                        strCommunicate[0] = this.gridView1.GetRowCellValue(i, "样本编号") as string;
                        strCommunicate[1] = this.gridView1.GetRowCellValue(i, "申请时间") as string;

                        lstCommunicate.Add(strCommunicate);
                        //}

                    }
                    if (lstCommunicate.Count > 0)
                    {
                        dataCheckDic.Clear();
                        dataCheckDic.Add("BatchAuditSampleTest", new object[] { XmlUtility.Serializer(typeof(List<string[]>), lstCommunicate) });
                        SendToServices(dataCheckDic);
                    }
                }
            }
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            if (gridView1.GetSelectedRows().Count() > 0 && gridView2.GetSelectedRows().Count() > 0)
            {
                string[] communicates = new string[3];
                int selectedCount = gridView1.GetSelectedRows()[0];
                communicates[0] = gridView1.GetRowCellValue(selectedCount, "样本编号") as string;
                communicates[1] = gridView1.GetRowCellValue(selectedCount, "申请时间") as string;
                selectedCount = gridView2.GetSelectedRows()[0];
                communicates[2] = gridView2.GetRowCellValue(selectedCount, "检测项目") as string;
                //2018/9/2 注释掉
                //string taskState = gridView2.GetRowCellValue(selectedCount, "任务状态") as string;  
                if (/* taskState == "已完成" && */System.Convert.ToDateTime(communicates[1]) > DateTime.Now.Date)
                {
                    dataCheckDic.Clear();
                    dataCheckDic.Add("ReviewCheck", new object[] { XmlUtility.Serializer(typeof(string[]), communicates) });
                    SendToServices(dataCheckDic);
                }
                else if (System.Convert.ToDateTime(communicates[1]) < DateTime.Now.Date)
                {
                    MessageBox.Show("仅能对当天已完成检测的任务进行复查操作！");
                }
                //else if (taskState != "已完成")
                //{
                //    MessageBox.Show("当样本状态为" + taskState + "时，不能进行复查操作！");
                //}
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 复查项目仅能选择一项作为确认项,如果不选中，则默认第一次为确认项目
            //int a =lstSamResultInfo.Count;
            if (gridView2.GetSelectedRows().Count() > 0)
            {
                List<string> lstProjectName = new List<string>();
                foreach (int i in gridView2.GetSelectedRows())
                {
                    /* 2018/9/2 
                    if ((this.gridView2.GetRowCellValue(i, "任务状态") as string) != "已完成")
                    {
                        MessageBox.Show("确认复查项目必须为已完成状态，请重新选择！");
                        return;
                    }
                    */
                    lstProjectName.Add(this.gridView2.GetRowCellValue(i, "检测项目") as string);
                }

                foreach (string str in lstProjectName)
                {
                    if (lstProjectName.FindAll(obj => obj == str).Count > 1)
                    {
                        MessageBox.Show("确认复查项目格式有误，请重新选择！");
                        return;
                    }
                }

                List<string[]> lstConfirmInfo = new List<string[]>();
                foreach (int i in gridView2.GetSelectedRows())
                {
                    string[] confirmInfo = new string[3];
                    confirmInfo[0] = gridView2.GetRowCellValue(gridView1.GetSelectedRows()[0], "样本编号") as string;
                    confirmInfo[1] = gridView2.GetRowCellValue(i, "检测项目") as string;
                    confirmInfo[2] = gridView2.GetRowCellValue(i, "申请时间") as string;
                    lstConfirmInfo.Add(confirmInfo);
                }
                dataCheckDic.Clear();
                dataCheckDic.Add("ConfirmCommonTask", new object[] { XmlUtility.Serializer(typeof(string[]), lstConfirmInfo) });
                SendToServices(dataCheckDic);
            }
        }

        private void GetsReactionProcessData(List<SampleResultInfo> sampleResultInfos)
        {
            //List<>
        }
        /// <summary>
        /// 视图1： 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        /// <summary>
        /// 视图2：显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
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
                    gridView1.RefreshRow(prevHotTrackRow);

                    gridView1.RefreshRow(hotTrackRow);
                    if (hotTrackRow >= 0)
                        lstvSampleInfo.Cursor = Cursors.Hand;

                    else
                        lstvSampleInfo.Cursor = Cursors.Default;
                }
            }
        }
        //鼠标滑过gridview时，鼠标所指行显示浅蓝色
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)

                e.Appearance.BackColor = Color.CornflowerBlue;
        }
        //获取指定点的gridview视图坐标信息
        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));
            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }
        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReverseSelection_Click(object sender, EventArgs e)
        {
            int[] aaa = this.gridView2.GetSelectedRows();
            gridView2.SelectAll();
            for (int i = 0; i < aaa.Length; i++)
            {
                gridView2.UnselectRow(aaa[i]);
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            gridView2.SelectAll();
        }
        /// <summary>
        /// 离散统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiscreteStatistics_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                if (MessageBox.Show("仅会对已完成的样本进行离散统计，确定统计吗？", "离散统计", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    List<float> resultValues = new List<float>();
                    foreach (int i in this.gridView2.GetSelectedRows())
                    {
                        // 2018/9/2
                        //string sampleState = this.gridView2.GetRowCellValue(i, "样本状态") as string;
                        //if (sampleState == "已完成")
                        //{
                        float result = 0;
                        string resultQuery = this.gridView2.GetRowCellValue(i, "检测结果") as string;
                        try
                        {
                            result = float.Parse(resultQuery);
                        }
                        catch
                        {
                            result = 0;
                        }

                        resultValues.Add(result);
                        //}
                    }
                    DiscreteStatisticalInfo discreteStatisticalInfo = new DiscreteStatisticalInfo(sampleNum, resultValues);
                    DiscreteStatisticsVM discreteStatisticsVM = new DiscreteStatisticsVM(discreteStatisticalInfo);
                    discreteStatisticsVM.ShowDialog();
                }
            }
        }

        PrintType printType = null;
        /// <summary>
        /// 打印报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReportPrint_Click(object sender, EventArgs e)
        {
            printType = new PrintType();
            printType.PrintDelegateEvent += PrintReport_Event;
            printType.ShowDialog();
        }
        /// <summary>
        /// 处理打印报告事件
        /// </summary>
        /// <param name="sender"></param>
        private void PrintReport_Event(string sender)
        {
            if (sender == "1")
            {
                if (this.gridView1.SelectedRowsCount > 0)
                {
                    int number = this.gridView1.GetSelectedRows()[0];

                    string sample = this.gridView1.GetRowCellValue(number, "样本编号") as string;
                    DateTime dateTime = Convert.ToDateTime(this.gridView1.GetRowCellValue(number, "申请时间"));
                    BeginInvoke(new Action(() => { PatientResultInfo(sample, dateTime); }));
                    CreatePrintFile();
                    this.printType.Close();
                }
                else
                {
                    MessageBox.Show("请选择要打印的病人信息！");
                }

            }
            else if (sender == "2")
            {
                if (this.gridView1.GetSelectedRows().Count() > 0)
                {
                    foreach (int i in this.gridView1.GetSelectedRows())
                    {
                        string sample = this.gridView1.GetRowCellValue(i, "样本编号") as string;
                        DateTime dateTime = Convert.ToDateTime(this.gridView1.GetRowCellValue(i, "申请时间"));
                        BeginInvoke(new Action(() => { PatientResultInfo(sample, dateTime); }));
                        CreatePrintFile();
                    }
                    this.printType.Close();
                }
                else
                {
                    MessageBox.Show("请选择要打印的病人信息！");
                }
            }
            else if (sender == "3")
            {
                if (this.gridView1.GetSelectedRows().Count() > 0)
                {
                    foreach (int i in this.gridView1.GetSelectedRows())
                    {
                        string sample = this.gridView1.GetRowCellValue(i, "样本编号") as string;
                        DateTime dateTime = Convert.ToDateTime(this.gridView1.GetRowCellValue(i, "申请时间"));
                        BeginInvoke(new Action(() => { PatientResultInfo(sample, dateTime); }));
                        CreatePrintFile();
                    }
                    this.printType.Close();
                }
                else
                {
                    MessageBox.Show("请选择要打印的病人信息！");
                }
            }
        }

        private PrintDocument printDocument1;
        PrintPreviewDialog printPreviewDialog1;
        private void CreatePrintFile()
        {
            //设置打印用的纸张 当设置为Custom的时候，可以自定义纸张的大小，还可以选择A4,A5等常用纸型
            printDocument1 = new PrintDocument();

            //this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 827, 1169);
            printDocument1.DefaultPageSettings.Landscape = false;    //纵向打印

            //DataTablePrinter = new PrintDocument();

            //PageSetupDialog PageSetup = new PageSetupDialog();
            //PageSetup.Document = printDocument1;
            //printDocument1.DefaultPageSettings = PageSetup.PageSettings;
            //printDocument1.DefaultPageSettings.Landscape = false;//设置打印横向还是纵向

            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);

            printPreviewDialog1 = new PrintPreviewDialog();
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            
            //显示打印预览
            DialogResult result = printPreviewDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //    this.printPreviewDialog1.Close();
                //this.printDocument1.Print();

        }

        //记录已打印的行数
        int count = 0;
        /// <summary>
        /// 打印报告事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            
            //rowCount是除去打印过的行数后剩下的行数
            int rowCount = lstSampleReuslt.Count - count;
            //maxPageRow是当前设置下该页面可以打印的最大行数
            int maxPageRow = 35;
            e.Graphics.DrawString("XXXXXXXXXXXX医院", new Font(new FontFamily("黑体"), 18), System.Drawing.Brushes.Black, 320, 30);

            e.Graphics.DrawString(string.Format("样本编号："+ sampleInfoForResult.SampleNum), new Font(new FontFamily("新宋体"), 11), System.Drawing.Brushes.Black, 20, 70);
            //信息的名称

            e.Graphics.DrawString("姓名：" + sampleInfoForResult.PatientName + "     " + "性别：" + sampleInfoForResult.Sex + "     " + "年龄：" + sampleInfoForResult.Age,
                new Font(new FontFamily("新宋体"), 11), System.Drawing.Brushes.Black, 20, 98);
            e.Graphics.DrawString("科室：" + sampleInfoForResult.ApplyDepartment + "     " + "送检医生：" + sampleInfoForResult.InspectDoctor + "     " + "送检日期：" + sampleInfoForResult.InspectTime,
                new Font(new FontFamily("新宋体"), 11), System.Drawing.Brushes.Black, 20, 125);
            e.Graphics.DrawString("备注：" + sampleInfoForResult.Remarks,
                new Font(new FontFamily("新宋体"), 11), System.Drawing.Brushes.Black, 20, 148);
            //分割线样式和大小
            Pen pen = new Pen(Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(79)))), ((int)(((byte)(79))))), 2);
            e.Graphics.DrawLine(pen, 8, 171, 820, 171);
            e.Graphics.DrawString("项目",new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 15, 181);
            e.Graphics.DrawString("中文名称", new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 215, 181);
            e.Graphics.DrawString("结果", new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 415, 181);
            e.Graphics.DrawString("标识", new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 505, 181);
            e.Graphics.DrawString("单位", new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 605, 181);
            e.Graphics.DrawString("参考范围", new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 665, 181);
            //e.Graphics.DrawString("参考范围" + "         " + "中文名称" + "              " + "结果" + "          " + "标识" + "         " + "单位" + "         " + "参考范围",
            //    new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 15, 181);
            int withd = 181;
            if (maxPageRow >= rowCount)
            {
                while (count < lstSampleReuslt.Count)
                {
                    int columnCount = 0;
                    int height = withd += 25;
                    while (columnCount < 1)
                    {
                        //e.Graphics.DrawString(lstSampleReuslt[count].ProjectName + "         " + lstSampleReuslt[count].ChineseName + "               " + lstSampleReuslt[count].ConcResult + "        " +
                        //"" + "         " + lstSampleReuslt[count].UnitAndRange + "        " + "",
                        //new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 15, withd += 25);
                        
                        e.Graphics.DrawString(lstSampleReuslt[count].ProjectName, new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 15, height);
                        e.Graphics.DrawString(lstSampleReuslt[count].ChineseName, new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 215, height);
                        e.Graphics.DrawString(lstSampleReuslt[count].ConcResult.ToString("#0.0000"), new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 415, height);
                        e.Graphics.DrawString("", new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 505, height);
                        e.Graphics.DrawString(lstSampleReuslt[count].UnitAndRange, new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 605, height);
                        e.Graphics.DrawString("", new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 665, height);
                        columnCount++;
                    }
                    count++;
                }
            }
            else
            {
                do
                {
                    int columnCount = 0;
                    int height = withd += 25;
                    while (columnCount < 1)
                    {
                        
                        e.Graphics.DrawString(lstSampleReuslt[count].ProjectName, new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 15, height);
                        e.Graphics.DrawString(lstSampleReuslt[count].ChineseName, new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 215, height);
                        e.Graphics.DrawString(lstSampleReuslt[count].ConcResult.ToString(), new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 415, height);
                        e.Graphics.DrawString("", new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 505, height);
                        e.Graphics.DrawString(lstSampleReuslt[count].UnitAndRange, new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 605, height);
                        e.Graphics.DrawString("", new Font(new FontFamily("Consolas"), 11), System.Drawing.Brushes.Black, 665, height);
                        columnCount++;
                    }
                    count++;
                }
                while (count % maxPageRow > 0);
            }
            e.Graphics.DrawLine(pen, 8, withd + 25, 820, withd + 25);
            e.Graphics.DrawString("测试日期：" + sampleInfoForResult.InputTime + "        " + "检验员：" + sampleInfoForResult.InspectDoctor + "        " + "审核人：" + sampleInfoForResult.AuditDoctor + "      " + "报告日期：" + DateTime.Now.ToShortDateString(),
                new Font(new FontFamily("新宋体"), 11), System.Drawing.Brushes.Black, 15, withd + 32);
            // 指定HasMorePages值，如果页面最大行数小于剩下的行数，则返回true（还有），否则返回false
            if (maxPageRow < rowCount)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                count = 0;
            }
        }

        //样本病人信息
        SampleInfoForResult sampleInfoForResult = null;
        //样本结果信息
        List<SampleResultInfo> lstSampleReuslt = null;
        /// <summary>
        /// 获取病人样本结果信息
        /// </summary>
        /// <param name="samp"></param>
        /// <param name="dateTime"></param>
        private void PatientResultInfo(string samp, DateTime dateTime)
        {
            lstSampleReuslt = myBatis.GetSmpPrintValues(samp, dateTime, out sampleInfoForResult);
        }
    }
}
