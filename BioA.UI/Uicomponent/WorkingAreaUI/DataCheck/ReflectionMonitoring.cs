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
using DevExpress.XtraCharts;
using BioA.Service;

namespace BioA.UI
{
    public partial class ReflectionMonitoring : DevExpress.XtraEditors.XtraForm
    {
        private List<SampleResultInfo> lstSampleResInfo = new List<SampleResultInfo>();
        /// <summary>
        /// List 样本检测结果
        /// </summary>
        public List<SampleResultInfo> LstSampleResInfo
        {
            get { return lstSampleResInfo; }
            set { lstSampleResInfo = value; }
        }

        private SampleResultInfo sampleResInfo = new SampleResultInfo();
        /// <summary>
        /// 样本检测结果
        /// </summary>
        public SampleResultInfo SampleResInfo
        {
            get { return sampleResInfo; }
            set 
            { 
                sampleResInfo = value; 
                if (lstSampleResInfo.Count > 0)
                {
                    sampleResInfo = lstSampleResInfo.Find((obj) => { return sampleResInfo.ProjectName == obj.ProjectName && sampleResInfo.SampleCompletionTime == obj.SampleCompletionTime; });
                }
            }
        }

        private SampleInfoForResult sampleInfoForRes = new SampleInfoForResult();
 
        public SampleInfoForResult SampleInfoForRes
        {
            get { return sampleInfoForRes; }
            set { sampleInfoForRes = value; }
        }

        private TimeCourseInfo sampleReactionInfo = new TimeCourseInfo();

        public TimeCourseInfo SampleReactionInfo
        {
            get { return sampleReactionInfo; }
            set
            {
                sampleReactionInfo = value;
                BeginInvoke(new Action(() => 
                { 
                    if (sampleReactionInfo != null)
                    {
                        this.CUVNO.Visible = true;
                        labCUVNO.Text = sampleReactionInfo.CUVNO.ToString();
                        Series series = new Series("ReactionLine", ViewType.Line);
                        series.ArgumentScaleType = ScaleType.Qualitative;
                        //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
                        if (sampleReactionInfo.Cuv1Wm != 0)
                            series.Points.Add(new SeriesPoint(1, ((sampleReactionInfo.Cuv1Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv1Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                        if (sampleReactionInfo.Cuv2Wm != 0)
                            series.Points.Add(new SeriesPoint(2, (sampleReactionInfo.Cuv2Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv2Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv3Wm != 0)
                            series.Points.Add(new SeriesPoint(3, (sampleReactionInfo.Cuv3Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv3Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv4Wm != 0)
                            series.Points.Add(new SeriesPoint(4, (sampleReactionInfo.Cuv4Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv4Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv5Wm != 0)
                            series.Points.Add(new SeriesPoint(5, (sampleReactionInfo.Cuv5Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv5Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv6Wm != 0)
                            series.Points.Add(new SeriesPoint(6, (sampleReactionInfo.Cuv6Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv6Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv7Wm != 0)
                            series.Points.Add(new SeriesPoint(7, (sampleReactionInfo.Cuv7Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv7Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv8Wm != 0)
                            series.Points.Add(new SeriesPoint(8, (sampleReactionInfo.Cuv8Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv8Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv9Wm != 0)
                            series.Points.Add(new SeriesPoint(9, (sampleReactionInfo.Cuv9Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv9Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv10Wm != 0)
                            series.Points.Add(new SeriesPoint(10, (sampleReactionInfo.Cuv10Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv10Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv11Wm != 0)
                            series.Points.Add(new SeriesPoint(11, (sampleReactionInfo.Cuv11Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv11Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv12Wm != 0)
                            series.Points.Add(new SeriesPoint(12, (sampleReactionInfo.Cuv12Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv12Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv13Wm != 0)
                            series.Points.Add(new SeriesPoint(13, (sampleReactionInfo.Cuv13Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv13Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv14Wm != 0)
                            series.Points.Add(new SeriesPoint(14, (sampleReactionInfo.Cuv14Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv14Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv15Wm != 0)
                            series.Points.Add(new SeriesPoint(15, (sampleReactionInfo.Cuv15Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv15Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv16Wm != 0)
                            series.Points.Add(new SeriesPoint(16, (sampleReactionInfo.Cuv16Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv16Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv17Wm != 0)
                            series.Points.Add(new SeriesPoint(17, (sampleReactionInfo.Cuv17Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv17Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv18Wm != 0)
                            series.Points.Add(new SeriesPoint(18, (sampleReactionInfo.Cuv18Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv18Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv19Wm != 0)
                            series.Points.Add(new SeriesPoint(19, (sampleReactionInfo.Cuv19Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv19Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv20Wm != 0)
                            series.Points.Add(new SeriesPoint(20, (sampleReactionInfo.Cuv20Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv20Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv21Wm != 0)
                            series.Points.Add(new SeriesPoint(21, (sampleReactionInfo.Cuv21Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv21Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv22Wm != 0)
                            series.Points.Add(new SeriesPoint(22, (sampleReactionInfo.Cuv22Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv22Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv23Wm != 0)
                            series.Points.Add(new SeriesPoint(23, (sampleReactionInfo.Cuv23Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv23Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv24Wm != 0)
                            series.Points.Add(new SeriesPoint(24, (sampleReactionInfo.Cuv24Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv24Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv25Wm != 0)
                            series.Points.Add(new SeriesPoint(25, (sampleReactionInfo.Cuv25Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv25Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv26Wm != 0)
                            series.Points.Add(new SeriesPoint(26, (sampleReactionInfo.Cuv26Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv26Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv27Wm != 0)
                            series.Points.Add(new SeriesPoint(27, (sampleReactionInfo.Cuv27Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv27Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv28Wm != 0)
                            series.Points.Add(new SeriesPoint(28, (sampleReactionInfo.Cuv28Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv28Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv29Wm != 0)
                            series.Points.Add(new SeriesPoint(29, (sampleReactionInfo.Cuv29Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv29Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv30Wm != 0)
                            series.Points.Add(new SeriesPoint(30, (sampleReactionInfo.Cuv30Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv30Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv31Wm != 0)
                            series.Points.Add(new SeriesPoint(31, (sampleReactionInfo.Cuv31Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv31Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv32Wm != 0)
                            series.Points.Add(new SeriesPoint(32, (sampleReactionInfo.Cuv32Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv32Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv33Wm != 0)
                            series.Points.Add(new SeriesPoint(33, (sampleReactionInfo.Cuv33Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv33Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv34Wm != 0)
                            series.Points.Add(new SeriesPoint(34, (sampleReactionInfo.Cuv34Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv34Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv35Wm != 0)
                            series.Points.Add(new SeriesPoint(35, (sampleReactionInfo.Cuv35Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv35Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv36Wm != 0)
                            series.Points.Add(new SeriesPoint(36, (sampleReactionInfo.Cuv36Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv36Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv37Wm != 0)
                            series.Points.Add(new SeriesPoint(37, (sampleReactionInfo.Cuv37Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv37Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv38Wm != 0)
                            series.Points.Add(new SeriesPoint(38, (sampleReactionInfo.Cuv38Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv38Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv39Wm != 0)
                            series.Points.Add(new SeriesPoint(39, (sampleReactionInfo.Cuv39Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv39Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv40Wm != 0)
                            series.Points.Add(new SeriesPoint(40, (sampleReactionInfo.Cuv40Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv40Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv41Wm != 0)
                            series.Points.Add(new SeriesPoint(41, (sampleReactionInfo.Cuv41Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv41Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv42Wm != 0)
                            series.Points.Add(new SeriesPoint(42, (sampleReactionInfo.Cuv42Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv42Ws - sampleReactionInfo.CuvBlkWs)));
                        if (sampleReactionInfo.Cuv43Wm != 0)
                            series.Points.Add(new SeriesPoint(43, (sampleReactionInfo.Cuv43Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv43Ws - sampleReactionInfo.CuvBlkWs)));

                        
                        series.View.Color = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
                        chartReaction.Series.Add(series);
                        chartReaction.Show();
                    }
                })); 
            }
        }

        bool bAudit = false;

        public ReflectionMonitoring(bool IsAudit)
        {
            InitializeComponent();
            this.ControlBox = false;
            bAudit = IsAudit;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.labCUVNO.Text = "";
            this.Close();
        }

        private void ReflectionMonitoring_Load(object sender, EventArgs e)
        {
            //异步方法调用
            BeginInvoke(new Action(loadReflectionMonitoring));
            
        }

        private void loadReflectionMonitoring()
        {
            CUVNO.Visible = false;
            txtSampleNum.Text = sampleInfoForRes.SampleNum.ToString();
            dtpApplyTime.Value = sampleResInfo.SampleCreateTime;
            txtSampleName.Text = sampleResInfo.ProjectName;
            txtConcResult.Text = sampleResInfo.ConcResult.ToString();

            chartReaction.Series.Clear();

            string taskState = string.Empty;
            switch (sampleResInfo.SampleCompletionStatus)
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
            txtProjectState.Text = taskState;
            if (bAudit)
            {
                TimeCourseInfo timeCourseInfoResult = new WorkingAreaDataCheck().QueryCommonTaskReaction("QueryTimeCourse", sampleResInfo);
                this.SampleReactionInfo = timeCourseInfoResult;
            }
            else
            {
                TimeCourseInfo timeCourseInfoResult = new WorkingAreaDataCheck().QueryCommonTaskReaction("QueryTimeCourse", sampleResInfo);
                this.SampleReactionInfo = timeCourseInfoResult;
            }
        }

    }
}