using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BioA.Common;
using BioA.Common.IO;
using System.Threading;
using BioA.Service;

namespace BioA.UI
{
    public partial class TestAudit : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> testAudtiDic = new Dictionary<string, object[]>();
        // 赋值给检测项目表
        DataTable dtCheckResult = new DataTable();

        //结果设置表信息
        private List<ResultSetInfo> lstResultSetInfo;

        /// <summary>
        /// 存储父窗体样本信息结果
        /// </summary>
        private List<SampleInfoForResult> sampleInfos = new List<SampleInfoForResult>();
        /// <summary>
        /// 父窗体样本信息
        /// </summary>
        public List<SampleInfoForResult> SampleInfos
        {
            get { return sampleInfos; }
            set { sampleInfos = value; }
        }

        private SampleInfoForResult sampleInfo = new SampleInfoForResult();
        /// <summary>
        /// 显示的样本信息
        /// </summary>
        public SampleInfoForResult SampleInfo
        {
            get { return sampleInfo; }
            set 
            {
                sampleInfo = value; 
                if (sampleInfos.Count > 0)
                {
                    sampleInfo = sampleInfos.Find((obj) => { return obj.SampleNum == SampleInfo.SampleNum && obj.CreateTime == sampleInfo.CreateTime; });
                    
                }
            }
        }
        /// <summary>
        /// gridView点击选中的行号
        /// </summary>
        public int CurrentClickLineNumber;

        private List<SampleResultInfo> lstSampleResInfo = new List<SampleResultInfo>();

        public List<SampleResultInfo> LstSampleResInfo
        {
            get { return lstSampleResInfo; }
            set 
            { 
                lstSampleResInfo = value;
                this.Invoke(new EventHandler(delegate
                    {
                        dtCheckResult.Clear();
                        foreach (SampleResultInfo s in lstSampleResInfo)
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
                            }

                            dtCheckResult.Rows.Add(new object[] { s.ProjectName, s.ConcResult, s.UnitAndRange, s.SampleCompletionTime.ToString(),s.TCNO, taskState, s.IsResurvey ? "是" : "否" });
                        }
                    }));
            }
        }

        private string recieveInfo = string.Empty;
        /// <summary>
        /// 接收数据库返回更新信息
        /// </summary>
        public string RecieveInfo
        {
            get { return recieveInfo; }
            set
            {
                recieveInfo = value;
                switch (recieveInfo)
                {
                    case "审核失败！":
                        this.Invoke(new EventHandler(delegate
                            {
                                MessageBox.Show("审核失败！");
                            }));
                        break;
                    case "审核成功！":
                        
                        break;
                }
            }
        }

        private TimeCourseInfo sampleReactionInfo = new TimeCourseInfo();

        public TimeCourseInfo SampleReactionInfo
        {
            get { return sampleReactionInfo; }
            set 
            { 
                sampleReactionInfo = value;
                reflectionMonitoring.SampleReactionInfo = sampleReactionInfo;
            }
        }

        ReflectionMonitoring reflectionMonitoring;

        public TestAudit()
        {
            InitializeComponent();
            this.ControlBox = false;
            dtCheckResult.Columns.Add("检测项目");
            dtCheckResult.Columns.Add("检测结果");
            dtCheckResult.Columns.Add("单位");
            dtCheckResult.Columns.Add("测试时间");
            dtCheckResult.Columns.Add("进程编号");
            dtCheckResult.Columns.Add("任务状态");
            dtCheckResult.Columns.Add("复查");
            grcCheckResult.DataSource = dtCheckResult;

            gridView1.Columns[0].OptionsColumn.AllowEdit = false;
            gridView1.Columns[1].OptionsColumn.AllowEdit = false;
            gridView1.Columns[2].OptionsColumn.AllowEdit = false;
            gridView1.Columns[3].OptionsColumn.AllowEdit = false;
            gridView1.Columns[4].OptionsColumn.AllowEdit = false;
            gridView1.Columns[5].OptionsColumn.AllowEdit = false;
            gridView1.Columns[6].OptionsColumn.AllowEdit = false;
        }

        private void TestAudit_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadTestAudit));
            this.lstResultSetInfo = QueryResultSetTb.QueryResultSetInfo;
        }
        /// <summary>
        /// 测试审核信息
        /// </summary>
        private void loadTestAudit()
        {
            
            reflectionMonitoring = new ReflectionMonitoring(true);


            //grcCheckResult.DataSource = dtCheckResult;
            txtSampleNum.Text = sampleInfo.SampleNum.ToString();
            txtSampleID.Text = sampleInfo.SampleID;

            string sampleState = string.Empty;
            switch (sampleInfo.SampleState)
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
            }

            txtSampleState.Text = sampleState;
            txtSampleType.Text = sampleInfo.SampleType;
            dtpApplyTime.Value = sampleInfo.CreateTime;

            if (txtSampleState.Text == "已完成" && sampleInfo.IsAudit == false)
            {
                btnAudit.Enabled = true;
            }
            else
            {
                btnAudit.Enabled = false;
            }

            if (this.CurrentClickLineNumber <= 1)
            {
                btnPreviousSample.Enabled = false;
            }
            else
            {
                btnPreviousSample.Enabled = true;
            }

            if (this.CurrentClickLineNumber >= sampleInfos.Count)
            {
                btnNextSample.Enabled = false;
            }
            else
            {
                btnNextSample.Enabled = true;
            }

            string[] communicate = new string[] { txtSampleNum.Text, sampleInfo.CreateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), txtSampleType.Text };
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck,
            //    XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProjectResultForTestAudit", XmlUtility.Serializer(typeof(string[]), communicate))));
            //testAudtiDic.Clear();
            //testAudtiDic.Add("QueryProjectResultForTestAudit", new object[] { XmlUtility.Serializer(typeof(string[]), communicate) });
            //SendToServices(testAudtiDic);
            lstSampleResInfo = new WorkingAreaDataCheck().QueryProjectResultBySampleNum("QueryProjectResultBySampleNum", communicate);
            {
                dtCheckResult.Clear();
                foreach (SampleResultInfo s in lstSampleResInfo)
                {
                    ResultSetInfo result = lstResultSetInfo.SingleOrDefault(v => v.ProjectName == s.ProjectName) as ResultSetInfo;
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
                    }

                    dtCheckResult.Rows.Add(new object[] { s.ProjectName, Math.Round(s.ConcResult, result != null && result.RadixPointNum != 100000000 ? result.RadixPointNum : 4), s.UnitAndRange, s.SampleCompletionTime.ToString(), s.TCNO, taskState, s.IsResurvey ? "是" : "否" });
                }
            }
        }
        
        private void SendToServices(Dictionary<string, object[]> param)
        {
            var testAuditThread = new Thread(() =>{
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaDataCheck,param);
            });
            testAuditThread.IsBackground = true;
            testAuditThread.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 反应监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReactionCurve_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                reflectionMonitoring.LstSampleResInfo = lstSampleResInfo;
                int selectNum = gridView1.GetSelectedRows()[0];
                SampleResultInfo sampleRes = new SampleResultInfo();
                sampleRes.ProjectName = gridView1.GetRowCellValue(selectNum, "检测项目") as string;
                sampleRes.ConcResult = (float)System.Convert.ToDouble(gridView1.GetRowCellValue(selectNum, "检测结果"));
                sampleRes.SampleCompletionTime = System.Convert.ToDateTime(gridView1.GetRowCellValue(selectNum, "测试时间"));
                reflectionMonitoring.SampleResInfo = sampleRes;
                reflectionMonitoring.SampleInfoForRes = sampleInfo;

                reflectionMonitoring.StartPosition = FormStartPosition.CenterScreen;
                reflectionMonitoring.ShowDialog();
            }
        }
        /// <summary>
        /// 上一个样本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreviousSample_Click(object sender, EventArgs e)
        {
            this.CurrentClickLineNumber--;
            if (this.CurrentClickLineNumber > 0)
            {
                sampleInfo = sampleInfos[this.CurrentClickLineNumber - 1];
                loadTestAudit();
            }
        }
        /// <summary>
        /// 下一个样本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextSample_Click(object sender, EventArgs e)
        {
            this.CurrentClickLineNumber++;
            if (this.CurrentClickLineNumber <= this.sampleInfos.Count)
            {
                sampleInfo = sampleInfos[CurrentClickLineNumber -1];
                //TestAudit_Load(null, null);
                loadTestAudit();
            }
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            string[] strCommunicate = new string[2];
            strCommunicate[0] = txtSampleNum.Text;
            strCommunicate[1] = dtpApplyTime.Value.ToString();

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaDataCheck,
            //    XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("AuditSampleTest", XmlUtility.Serializer(typeof(string[]), strCommunicate))));
            testAudtiDic.Clear();
            testAudtiDic.Add("AuditSampleTest", new object[] { XmlUtility.Serializer(typeof(string[]), strCommunicate) });
            SendToServices(testAudtiDic);
        }
    }
}