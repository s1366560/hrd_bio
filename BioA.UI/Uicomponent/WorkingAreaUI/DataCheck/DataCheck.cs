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

namespace BioA.UI
{
    public partial class DataCheck : DevExpress.XtraEditors.XtraUserControl
    {
        // 检测结果存储表
        DataTable CheckResultDT = new DataTable();
        //存储病人操作状态信息
        DataTable dt = new DataTable();
        // 审核界面
        TestAudit testAudit;

        List<SampleInfoForResult> sampleInfos = new List<SampleInfoForResult>();
        List<SampleResultInfo> lstSamResultInfo = new List<SampleResultInfo>();
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

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck,
            //    XmlUtility.Serializer(typeof(CommunicationEntity),
            //    new CommunicationEntity("QueryCommonSampleData",
            //        XmlUtility.Serializer(typeof(SampleInfoForResult), sampleInfo),
            //        strFilter
            //        )));
            CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck, new Dictionary<string, List<object>>() { { "QueryCommonSampleData", new List<object>() { XmlUtility.Serializer(typeof(SampleInfoForResult), sampleInfo), strFilter } } });


        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ScreeningSample screeningSample = new ScreeningSample();
            screeningSample.StartPosition = FormStartPosition.CenterScreen;
            screeningSample.ShowDialog();
        }

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
            }

            testAudit.StartPosition = FormStartPosition.CenterScreen;
            testAudit.ShowDialog();
        }

        private void btnReactionMonitoring_Click(object sender, EventArgs e)
        {
            if (gridView2.SelectedRowsCount > 0 && gridView1.SelectedRowsCount > 0)
            {
                SampleInfoForResult sampleInfo = new SampleInfoForResult();
                int selectedNum = this.gridView1.GetSelectedRows()[0];

                sampleInfo.SampleNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedNum, "样本编号"));
                sampleInfo.CreateTime = System.Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedNum, "申请时间"));

                testAudit.SampleInfo = sampleInfo;

                reflectionMonitoring.LstSampleResInfo = lstSamResultInfo;
                int selectNum = gridView2.GetSelectedRows()[0];
                SampleResultInfo sampleRes = new SampleResultInfo();
                sampleRes.ProjectName = gridView2.GetRowCellValue(selectNum, "检测项目") as string;
                sampleRes.ConcResult = (float)System.Convert.ToDouble(gridView2.GetRowCellValue(selectNum, "检测结果"));
                sampleRes.SampleCreateTime = System.Convert.ToDateTime(gridView2.GetRowCellValue(selectNum, "申请时间"));
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
            CheckResultDT.Columns.Add("单位(参考范围)");
            CheckResultDT.Columns.Add("申请时间");
            CheckResultDT.Columns.Add("任务状态");
            CheckResultDT.Columns.Add("复查");
            CheckResultDT.Columns.Add("确认", typeof(Boolean));

            lstvInspectProInfo.DataSource = CheckResultDT;
            gridView2.Columns[0].OptionsColumn.AllowEdit = false;
            gridView2.Columns[1].OptionsColumn.AllowEdit = false;
            gridView2.Columns[2].OptionsColumn.AllowEdit = false;
            gridView2.Columns[3].OptionsColumn.AllowEdit = false;
            gridView2.Columns[4].OptionsColumn.AllowEdit = true;

            
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
            BeginInvoke(new Action(() =>
            {
                LoadCommonSampleData();
            }));
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

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck,
            //    XmlUtility.Serializer(typeof(CommunicationEntity),
            //    new CommunicationEntity("QueryCommonSampleData",
            //        XmlUtility.Serializer(typeof(SampleInfoForResult), sampleInfo),
            //        strFilter
            //        )));
            CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck, new Dictionary<string, List<object>>() { { "QueryCommonSampleData", new List<object>() { XmlUtility.Serializer(typeof(SampleInfoForResult), sampleInfo), strFilter } } });
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryCommonSampleData":
                    sampleInfos = (List<SampleInfoForResult>)XmlUtility.Deserialize(typeof(List<SampleInfoForResult>), sender as string);
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        //DataTable dt = new DataTable();
                        //dt.Columns.Add("样本编号");
                        //dt.Columns.Add("样本ID");
                        //dt.Columns.Add("样本类型");
                        //dt.Columns.Add("患者名称");
                        //dt.Columns.Add("性别");
                        //dt.Columns.Add("年龄");
                        //dt.Columns.Add("申请时间");
                        //dt.Columns.Add("样本状态");
                        //dt.Columns.Add("审核状态");
                        //dt.Columns.Add("打印状态");
                        //dt.Columns.Add("手动稀释");

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
                            BeginInvoke(new Action(() =>
                            {
                                lstvSampleInfo.DataSource = dt;
                                gridView1.SelectRow(0);
                                lstvSampleInfo_Click(null, null);
                            }));
                        }
                        else
                        {
                            CheckResultDT.Rows.Clear();
                        }
                    }));
                    break;
                case "QueryProjectResultBySampleNum":
                    lstSamResultInfo = (List<SampleResultInfo>)XmlUtility.Deserialize(typeof(List<SampleResultInfo>), sender as string);
                    this.BeginInvoke(new EventHandler(delegate
                        {
                            CheckResultDT.Rows.Clear();
                            foreach (SampleResultInfo s in lstSamResultInfo)
                            {
                                string taskState = string.Empty;
                                switch (s.TaskState)
                                {
                                    case 0:
                                        taskState = "待测";
                                        break;
                                    case 1:
                                        taskState = "正在执行";
                                        break;
                                    case 2:
                                        taskState = "已完成";
                                        break;
                                    case 3:
                                        taskState = "被暂停";
                                        break;
                                    default:
                                        break;
                                }
                                CheckResultDT.Rows.Add(new object[] { s.ProjectName, s.ConcResult, s.UnitAndRange, s.SampleCreateTime.ToString(), taskState, s.IsResurvey == true ? "是" : "否", s.Confirm });
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
                            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck,
                            //    XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProjectResultBySampleNum", XmlUtility.Serializer(typeof(string[]), communicate))));
                            CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck, new Dictionary<string, List<object>>() { { "QueryProjectResultBySampleNum", new List<object>(){ communicate } } });
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

        private void lstvSampleInfo_Click(object sender, EventArgs e)
        {
            int iSelected = 0;
            if (gridView1.SelectedRowsCount > 0)
            {
                iSelected = gridView1.GetSelectedRows()[0];
                int sampleNum = System.Convert.ToInt32(gridView1.GetRowCellValue(iSelected, "样本编号"));
                DateTime dt = System.Convert.ToDateTime(gridView1.GetRowCellValue(iSelected, "申请时间"));
                string sampleType = gridView1.GetRowCellValue(iSelected, "样本类型") as string;
                string[] communicate = new string[] { sampleNum.ToString(), dt.ToString(), sampleType };
                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck,
                //    XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProjectResultBySampleNum", XmlUtility.Serializer(typeof(string[]), communicate))));
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck, new Dictionary<string, List<object>>() { { "QueryProjectResultBySampleNum", new List<object>() { communicate } } });
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                int selectedCount = this.gridView1.GetSelectedRows()[0];
                int SampleNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedCount, "样本编号").ToString());
                DateTime dt = System.Convert.ToDateTime(gridView1.GetRowCellValue(selectedCount, "申请时间"));

                string[] communicate = new string[2];
                communicate[0] = SampleNum.ToString();
                communicate[1] = dt.ToString();

                if (MessageBox.Show(string.Format("确实要删除{0}的样本号为{1}的样本吗？", dt.ToShortDateString(), SampleNum.ToString()), "删除样本数据", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck,
                    //    XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("DeleteCommonSampleBySampleNum", XmlUtility.Serializer(typeof(string[]), communicate))));
                    CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck, new Dictionary<string, List<object>>() { { "DeleteCommonSampleBySampleNum", new List<object>() { communicate } } });
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
                        string sampleState = this.gridView1.GetRowCellValue(i, "样本状态") as string;
                        if (sampleState == "已完成")
                        {
                            string[] strCommunicate = new string[2];
                            strCommunicate[0] = this.gridView1.GetRowCellValue(i, "样本编号") as string;
                            strCommunicate[1] = this.gridView1.GetRowCellValue(i, "申请时间") as string;

                            lstCommunicate.Add(strCommunicate);
                        }

                    }
                    if (lstCommunicate.Count > 0)
                        //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck, XmlUtility.Serializer(typeof(CommunicationEntity),
                        //        new CommunicationEntity("BatchAuditSampleTest", XmlUtility.Serializer(typeof(List<string[]>), lstCommunicate))));
                        CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck, new Dictionary<string, List<object>>() { { "BatchAuditSampleTest", new List<object>() { XmlUtility.Serializer(typeof(List<string[]>), lstCommunicate) } } });
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
                string taskState = gridView2.GetRowCellValue(selectedCount, "任务状态") as string;
                if (taskState == "已完成" && System.Convert.ToDateTime(communicates[1]) > DateTime.Now.Date)
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("ReviewCheck", XmlUtility.Serializer(typeof(string[]), communicates))));
                    CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck, new Dictionary<string, List<object>>() { { "ReviewCheck", new List<object>() { communicates } } });
                else if (System.Convert.ToDateTime(communicates[1]) < DateTime.Now.Date)
                {
                    MessageBox.Show("仅能对当天已完成检测的任务进行复查操作！");
                }
                else if (taskState != "已完成")
                {
                    MessageBox.Show("当样本状态为" + taskState + "时，不能进行复查操作！");
                }
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
                    if ((this.gridView2.GetRowCellValue(i, "任务状态") as string) != "已完成")
                    {
                        MessageBox.Show("确认复查项目必须为已完成状态，请重新选择！");
                        return;
                    }
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

                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck, XmlUtility.Serializer(typeof(CommunicationEntity),
                //    new CommunicationEntity("ConfirmCommonTask", XmlUtility.Serializer(typeof(string[]), lstConfirmInfo))));
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck, new Dictionary<string, List<object>>() { {"ConfirmCommonTask", new List<object>() { lstConfirmInfo } } });
            }
        }
    }
}
