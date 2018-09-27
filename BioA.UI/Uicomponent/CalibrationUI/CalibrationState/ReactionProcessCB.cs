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
using DevExpress.XtraCharts;
using BioA.Common;
using BioA.Common.IO;

namespace BioA.UI
{
    public partial class ReactionProcessCB : DevExpress.XtraEditors.XtraForm
    {
        public delegate void CalibrationDelegate(Dictionary<string, object[]> sender);
        public event CalibrationDelegate CalibrationTimeCoursetEvent;
        public ReactionProcessCB()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //全局变量
        List<CalibrationResultinfo> lstCalibrationResultInfo = new List<CalibrationResultinfo>();
        /// <summary>
        /// 显示校准信息对应的反应进程数据
        /// </summary>
        /// <param name="calibrationResultInfoAndTimeCUVNO"></param>
        public void calibrationResultInfoAndTimeCUVNOAdd(List<CalibrationResultinfo> calibrationResultInfoAndTimeCUVNO)
        {
            List<CalibrationResultinfo> lstCalibReuslt = new List<CalibrationResultinfo>();
            CalibrationResultinfo c = null;
            List<string> lstCalibrationName = new List<string>();
            this.Invoke(new EventHandler(delegate { 
                foreach (CalibrationResultinfo calibrationResultInfo in calibrationResultInfoAndTimeCUVNO)
                {
                    lstCalibrationResultInfo.Add(calibrationResultInfo);
                    lstCalibrationName.Add(calibrationResultInfo.CalibratorName);
                    if (c == null || c.CalibrationDT == null || c.CalibrationDT != calibrationResultInfo.CalibrationDT)
                    {
                        c = new CalibrationResultinfo();
                        textEditProName.Text = c.ProjectName = calibrationResultInfo.ProjectName;
                        textEditSamType.Text = c.SampleType = calibrationResultInfo.SampleType;
                        c.CalibMethod = calibrationResultInfo.CalibMethod;
                        c.CalibrationDT = calibrationResultInfo.CalibrationDT;
                        lstCalibReuslt.Add(c);
                    }                              
                }
                comBoxEditCalibName.Properties.Items.AddRange(lstCalibrationName.Distinct().ToList());
                if (lstCalibReuslt.Count > 1)
                {
                    int i;
                    for (i = 0; i < lstCalibReuslt.Count; i++)
                    {
                        comboBoxCalibTime.Properties.Items.Add(lstCalibReuslt[i].CalibrationDT);
                    }
                    comboBoxCalibTime.Text = lstCalibReuslt[i-1].CalibrationDT.ToString();
                }
                else
                {
                    comboBoxCalibTime.Properties.Items.Add(lstCalibReuslt[0].CalibrationDT);
                    comboBoxCalibTime.Text = lstCalibReuslt[0].CalibrationDT.ToString();
                }
                comBoxEditCalibName.SelectedIndex = 0;
            }));
        }

        private TimeCourseInfo sampleReactionInfo = new TimeCourseInfo();
        public TimeCourseInfo SampleReactionInfo
        {
            get { return sampleReactionInfo; }
            set
            {
                sampleReactionInfo = value;
                chartControl1.Series.Clear();
                if (sampleReactionInfo != null)
                {
                    Series series = new Series("ReactionLine", ViewType.Line);
                    //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
                    if (sampleReactionInfo.Cuv1Wm != 0)
                        series.Points.Add(new SeriesPoint(1, ((sampleReactionInfo.Cuv1Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv1Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv2Wm != 0)
                        series.Points.Add(new SeriesPoint(2, ((sampleReactionInfo.Cuv2Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv2Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv3Wm != 0)
                        series.Points.Add(new SeriesPoint(3, ((sampleReactionInfo.Cuv3Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv3Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv4Wm != 0)
                        series.Points.Add(new SeriesPoint(4, ((sampleReactionInfo.Cuv4Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv4Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv5Wm != 0)
                        series.Points.Add(new SeriesPoint(5, ((sampleReactionInfo.Cuv5Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv5Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv6Wm != 0)
                        series.Points.Add(new SeriesPoint(6, ((sampleReactionInfo.Cuv6Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv6Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv7Wm != 0)
                        series.Points.Add(new SeriesPoint(7, ((sampleReactionInfo.Cuv7Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv7Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv8Wm != 0)
                        series.Points.Add(new SeriesPoint(8, ((sampleReactionInfo.Cuv8Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv8Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv9Wm != 0)
                        series.Points.Add(new SeriesPoint(9, ((sampleReactionInfo.Cuv9Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv9Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv10Wm != 0)
                        series.Points.Add(new SeriesPoint(10, ((sampleReactionInfo.Cuv10Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv10Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv11Wm != 0)
                        series.Points.Add(new SeriesPoint(11, ((sampleReactionInfo.Cuv11Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv11Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv12Wm != 0)
                        series.Points.Add(new SeriesPoint(12, ((sampleReactionInfo.Cuv12Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv12Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv13Wm != 0)
                        series.Points.Add(new SeriesPoint(13, ((sampleReactionInfo.Cuv13Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv13Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv14Wm != 0)
                        series.Points.Add(new SeriesPoint(14, ((sampleReactionInfo.Cuv14Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv14Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv15Wm != 0)
                        series.Points.Add(new SeriesPoint(15, ((sampleReactionInfo.Cuv15Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv15Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv16Wm != 0)
                        series.Points.Add(new SeriesPoint(16, ((sampleReactionInfo.Cuv16Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv16Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv17Wm != 0)
                        series.Points.Add(new SeriesPoint(17, ((sampleReactionInfo.Cuv17Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv17Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv18Wm != 0)
                        series.Points.Add(new SeriesPoint(18, ((sampleReactionInfo.Cuv18Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv18Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv19Wm != 0)
                        series.Points.Add(new SeriesPoint(19, ((sampleReactionInfo.Cuv19Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv19Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv20Wm != 0)
                        series.Points.Add(new SeriesPoint(20, ((sampleReactionInfo.Cuv20Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv20Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv21Wm != 0)
                        series.Points.Add(new SeriesPoint(21, ((sampleReactionInfo.Cuv21Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv21Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv22Wm != 0)
                        series.Points.Add(new SeriesPoint(22, ((sampleReactionInfo.Cuv22Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv22Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv23Wm != 0)
                        series.Points.Add(new SeriesPoint(23, ((sampleReactionInfo.Cuv23Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv23Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv24Wm != 0)
                        series.Points.Add(new SeriesPoint(24, ((sampleReactionInfo.Cuv24Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv24Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv25Wm != 0)
                        series.Points.Add(new SeriesPoint(25, ((sampleReactionInfo.Cuv25Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv25Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv26Wm != 0)
                        series.Points.Add(new SeriesPoint(26, ((sampleReactionInfo.Cuv26Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv26Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv27Wm != 0)
                        series.Points.Add(new SeriesPoint(27, ((sampleReactionInfo.Cuv27Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv27Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv28Wm != 0)
                        series.Points.Add(new SeriesPoint(28, ((sampleReactionInfo.Cuv28Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv28Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv29Wm != 0)
                        series.Points.Add(new SeriesPoint(29, ((sampleReactionInfo.Cuv29Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv29Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv30Wm != 0)
                        series.Points.Add(new SeriesPoint(30, ((sampleReactionInfo.Cuv30Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv30Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv31Wm != 0)
                        series.Points.Add(new SeriesPoint(31, ((sampleReactionInfo.Cuv31Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv31Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv32Wm != 0)
                        series.Points.Add(new SeriesPoint(32, ((sampleReactionInfo.Cuv32Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv32Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv33Wm != 0)
                        series.Points.Add(new SeriesPoint(33, ((sampleReactionInfo.Cuv33Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv33Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv34Wm != 0)
                        series.Points.Add(new SeriesPoint(34, ((sampleReactionInfo.Cuv34Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv34Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv35Wm != 0)
                        series.Points.Add(new SeriesPoint(35, ((sampleReactionInfo.Cuv35Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv35Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv36Wm != 0)
                        series.Points.Add(new SeriesPoint(36, ((sampleReactionInfo.Cuv36Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv36Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv37Wm != 0)
                        series.Points.Add(new SeriesPoint(37, ((sampleReactionInfo.Cuv37Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv37Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv38Wm != 0)
                        series.Points.Add(new SeriesPoint(38, ((sampleReactionInfo.Cuv38Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv38Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv39Wm != 0)
                        series.Points.Add(new SeriesPoint(39, ((sampleReactionInfo.Cuv39Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv39Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv40Wm != 0)
                        series.Points.Add(new SeriesPoint(40, ((sampleReactionInfo.Cuv40Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv40Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv41Wm != 0)
                        series.Points.Add(new SeriesPoint(41, ((sampleReactionInfo.Cuv41Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv41Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv42Wm != 0)
                        series.Points.Add(new SeriesPoint(42, ((sampleReactionInfo.Cuv42Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv42Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));
                    if (sampleReactionInfo.Cuv43Wm != 0)
                        series.Points.Add(new SeriesPoint(43, ((sampleReactionInfo.Cuv43Wm - sampleReactionInfo.CuvBlkWm) - (sampleReactionInfo.Cuv43Ws - sampleReactionInfo.CuvBlkWs)).ToString("#0.0000")));

                    this.Invoke(new EventHandler(delegate
                    {
                        LineSeriesView lineSeriesView1 = new LineSeriesView();
                        lineSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
                        lineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
                        lineSeriesView1.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Circle;
                        lineSeriesView1.LineMarkerOptions.Size = 7;
                        //是否显示圆点标注
                        //lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series.View = lineSeriesView1;
                        List<Series> list = new List<Series>() { series };
                        chartControl1.Series.AddRange(list.ToArray());
                        //chartControl1.Series.Add(series);
                    }));
                }
                this.Invoke(new EventHandler(delegate
                {
                    this.comBoxEditCalibName.Enabled = true;
                    this.comboBoxCalibTime.Enabled = true;
                    this.comboBoxEditCuveNum.Enabled = true;
                }));
            }
        }
        /// <summary>
        /// 校准时间（下拉框改变事件）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxCalibTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据下拉选择的时间更新对应的比色杯编号
            comboBoxEditCuveNum.Properties.Items.Clear();
            comboBoxEditCuveNum.SelectedIndex = -1;
            foreach (CalibrationResultinfo cuvno in lstCalibrationResultInfo)
            {
                if (comboBoxCalibTime.Text == cuvno.CalibrationDT.ToString())
                {
                    comboBoxEditCuveNum.Properties.Items.Add(cuvno.CUVNO);
                }
            }
            comboBoxEditCuveNum.SelectedIndex = 0;
            

        }
        /// <summary>
        /// 反应进程编号(下拉框改变事件)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEditCuveNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据下拉框选择的比色表编号更新对应的校准信息
            TimeCourseInfo timecuvno = new TimeCourseInfo();
            int TestNum = 0;
            foreach (CalibrationResultinfo calib in lstCalibrationResultInfo)
            {
                if (comboBoxCalibTime.Text == calib.CalibrationDT.ToString() && comBoxEditCalibName.Text == calib.CalibratorName)
                {
                    TestNum++;
                }
                if(comBoxEditCalibName.Text == calib.CalibratorName && Convert.ToInt32(comboBoxEditCuveNum.Text) == calib.CUVNO)
                {
                    timecuvno.TimeCourseNo = calib.TCNO;
                    timecuvno.CUVNO = calib.CUVNO;
                }
            }
            comboBoxNum.Text = TestNum.ToString();
            this.comBoxEditCalibName.Enabled= false;
            this.comboBoxCalibTime.Enabled = false;
            this.comboBoxEditCuveNum.Enabled = false;
            if (CalibrationTimeCoursetEvent != null)
            {
                CalibrationTimeCoursetEvent(new Dictionary<string, object[]>() { { "QueryCalibrationReactionProcess", new object[] { XmlUtility.Serializer(typeof(TimeCourseInfo), timecuvno) } } });
            }
        }
        /// <summary>
        /// 校准品名称（下拉列表）改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comBoxEditCalibName_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEditCuveNum.Properties.Items.Clear();
            for (int i = 0; i < lstCalibrationResultInfo.Count; i++)
            {
                if (comboBoxCalibTime.Text == lstCalibrationResultInfo[i].CalibrationDT.ToString() && comBoxEditCalibName.Text == lstCalibrationResultInfo[i].CalibratorName)
                {
                    comboBoxEditCuveNum.Properties.Items.Add(lstCalibrationResultInfo[i].CUVNO);
                }
            }
            comboBoxEditCuveNum.SelectedIndex = 0;
        }
    }
}