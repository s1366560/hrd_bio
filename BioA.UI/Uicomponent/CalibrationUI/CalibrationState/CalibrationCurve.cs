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
using DevExpress.XtraCharts;
using BioA.Common.IO;
using System.Threading;

namespace BioA.UI
{
    public partial class    CalibrationCurve : DevExpress.XtraEditors.XtraForm
    {
        public delegate void CalibrationDelegate(Dictionary<string, object[]> sender);
        public event CalibrationDelegate CalibrationEvent;

        List<CalibrationCurveInfo> listCalibrationCurveInfo = new List<CalibrationCurveInfo>();
        public CalibrationCurve()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        public string strResult;

        public string StrResult
        {
            get { return strResult; }
            set
            {
                strResult = value;
                this.Invoke(new EventHandler(delegate
                {
                    MessageBox.Show(strResult);
                    this.Close();
                }));
            }
        }

        public void AddCalibrationCurve(CalibrationCurveInfo calibrationCurveInfo)
        {           
            textEditProjectName.Text = calibrationCurveInfo.ProjectName;
            textEditSampleType.Text = calibrationCurveInfo.SampleType;
            textEditCalibMethod.Text = calibrationCurveInfo.CalibType;
            //  textEdit16.Text = calibrationCurveInfo.SampleType;                             
        }
       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex)
        {
            Series series = new Series(caption, viewType);
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                string argument = dt.Columns[i].ColumnName;//参数名称
                string value = (string)dt.Rows[rowIndex][i];//参数值
                if (value != string.Empty)
                {
                    series.Points.Add(new SeriesPoint(argument, value));
                }
            }
            //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
            //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签

            return series;
        }


        private DataTable CreateData()
        {           
            DataTable dt = new DataTable();
            DataColumn[] dtc = new DataColumn[8];
            dtc[0] = new DataColumn("浓度");
            dtc[1] = new DataColumn(textEditBlkConc.Text);
            dtc[2] = new DataColumn(textEditSDT1Conc.Text);
            dtc[3] = new DataColumn(textEditSDT2Conc.Text);
            dtc[4] = new DataColumn(textEditSDT3Conc.Text);
            dtc[5] = new DataColumn(textEditSDT4Conc.Text);
            dtc[6] = new DataColumn(textEditSDT5Conc.Text);
            dtc[7] = new DataColumn(textEditSDT6Conc.Text);
            object[] obj = new object[8];
            obj[0] = "吸光度";
            obj[1] = textEditBlkAbs.Text;
            obj[2] = textEditSDT1Abs.Text;
            obj[3] = textEditSDT2Abs.Text;
            obj[4] = textEditSDT3Abs.Text;
            obj[5] = textEditSDT4Abs.Text;
            obj[6] = textEditSDT5Abs.Text;
            obj[7] = textEditSDT6Abs.Text;         
            dt.Columns.AddRange(dtc);
            dt.Rows.Add(obj);
            return dt;
        }


        private void CreateChart(DataTable dt)
        {

            //创建几个图形的对象
            Series series1 = CreateSeries("吸光度", ViewType.Line, dt, 0);
            LineSeriesView lineSeriesView1 = new LineSeriesView();
            lineSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            lineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            lineSeriesView1.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Circle;
            lineSeriesView1.LineMarkerOptions.Size = 10;
            lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.View = lineSeriesView1;
            List<Series> list = new List<Series>() { series1 };
            chartControl1.Series.AddRange(list.ToArray());


        }

        private void CalibConcentrationAdd(SDTTableItem listCalibrationCurve)
        {
            //listCalibrationCurve.Sort(delegate(SDTTableItem x, SDTTableItem y)
            //{
            //    return x.BlkConc.CompareTo(y.BlkAbs);
            //});

            this.Invoke(new EventHandler(delegate
            {
                float f = 0.0f;
                if (listCalibrationCurve != null )
                {
                    if (listCalibrationCurve.BlkItem != null && listCalibrationCurve.BlkItem != "")
                    {
                        textEditBlkConc.Text = listCalibrationCurve.BlkConc.ToString("#0.00");
                        textEditBlkAbs.Text = listCalibrationCurve.BlkAbs.ToString("#0.0000");
                        this.textEditBlkFactor.Text = "0.00";
                    }
                    if (listCalibrationCurve.Calib1Item != null && listCalibrationCurve.Calib1Item != "")
                    {
                        textEditSDT1Conc.Text = listCalibrationCurve.SDT1Conc.ToString("#0.00");
                        textEditSDT1Abs.Text = listCalibrationCurve.SDT1Abs.ToString("#0.0000");
                        try
                        {
                            f = listCalibrationCurve.SDT1Conc / (listCalibrationCurve.SDT1Abs - listCalibrationCurve.BlkAbs);
                        }
                        catch
                        {
                            f = 0.0f;
                        }
                        this.textEditFactor1.Text = float.IsInfinity(f) == true ? "0.00" : f.ToString("#0.00");
                        this.textEditFactor1.Text = float.IsNaN(f) == true ? "0.00" : f.ToString("#0.00");
                    }
                    if (listCalibrationCurve.Calib2Item != null && listCalibrationCurve.Calib2Item != "")
                    {
                        textEditSDT2Conc.Text = listCalibrationCurve.SDT2Conc.ToString("#0.00");
                        textEditSDT2Abs.Text = listCalibrationCurve.SDT2Abs.ToString("#0.0000");
                        try
                        {
                            f = listCalibrationCurve.SDT2Conc / (listCalibrationCurve.SDT2Abs - listCalibrationCurve.BlkAbs);
                        }
                        catch
                        {
                            f = 0.0f;
                        }
                        this.textEditFactor2.Text = float.IsInfinity(f) == true ? "0.00":f.ToString("#0.00");
                        this.textEditFactor2.Text = float.IsNaN(f) == true ? "0.00" : f.ToString("#0.00");
                    }
                    if (listCalibrationCurve.Calib3Item != null && listCalibrationCurve.Calib3Item != "")
                    {
                        textEditSDT3Conc.Text = listCalibrationCurve.SDT3Conc.ToString("#0.00");
                        textEditSDT3Abs.Text = listCalibrationCurve.SDT3Abs.ToString("#0.0000");
                        try
                        {
                            f = listCalibrationCurve.SDT3Conc / (listCalibrationCurve.SDT3Abs - listCalibrationCurve.BlkAbs);
                        }
                        catch
                        {
                            f = 0.0f;
                        }
                        this.textEditFactor3.Text = float.IsInfinity(f) == true ? "0.00" : f.ToString("#0.00");
                        this.textEditFactor3.Text = float.IsNaN(f) == true ? "0.00" : f.ToString("#0.00");
                    }
                    if (listCalibrationCurve.Calib4Item != null && listCalibrationCurve.Calib4Item != "")
                    {
                        textEditSDT4Conc.Text = listCalibrationCurve.SDT4Conc.ToString("#0.00");
                        textEditSDT4Abs.Text = listCalibrationCurve.SDT4Abs.ToString("#0.0000");
                        try
                        {
                            f = listCalibrationCurve.SDT4Conc / (listCalibrationCurve.SDT4Abs - listCalibrationCurve.BlkAbs);
                        }
                        catch
                        {
                            f = 0.0f;
                        }
                        this.textEditFactor4.Text = float.IsInfinity(f) == true ? "0.00" : f.ToString("#0.00");
                        this.textEditFactor4.Text = float.IsNaN(f) == true ? "0.00" : f.ToString("#0.00");
                    }
                    if (listCalibrationCurve.Calib5Item != null && listCalibrationCurve.Calib5Item != "")
                    {
                        textEditSDT5Conc.Text = listCalibrationCurve.SDT5Conc.ToString("#0.00");
                        textEditSDT5Abs.Text = listCalibrationCurve.SDT5Abs.ToString("#0.0000");
                        try
                        {
                            f = listCalibrationCurve.SDT5Conc / (listCalibrationCurve.SDT5Abs - listCalibrationCurve.BlkAbs);
                        }
                        catch
                        {
                            f = 0.0f;
                        }
                        this.textEditFactor5.Text = float.IsInfinity(f) == true ? "0.00" : f.ToString("#0.00");
                        this.textEditFactor5.Text = float.IsNaN(f) == true ? "0.00" : f.ToString("#0.00");
                    }
                    if(listCalibrationCurve.Calib6Item !=null && listCalibrationCurve.Calib6Item != "")
                    {
                        textEditSDT6Conc.Text = listCalibrationCurve.SDT6Conc.ToString("#0.00");
                        textEditSDT6Abs.Text = listCalibrationCurve.SDT6Abs.ToString("#0.0000");
                        try
                        {
                            f = listCalibrationCurve.SDT6Conc / (listCalibrationCurve.SDT6Abs - listCalibrationCurve.BlkAbs);
                        }
                        catch
                        {
                            f = 0.0f;
                        }
                        this.textEditFactor6.Text = float.IsInfinity(f) == true ? "0.00" : f.ToString("#0.00");
                        this.textEditFactor6.Text = float.IsNaN(f) == true ? "0.00" : f.ToString("#0.00");
                    }
                }
            }));


        }

        List<SDTTableItem> lisCalibrationCurve = new List<SDTTableItem>();
        public void SelectedlistCalibrationCurve(List<SDTTableItem> listCalibrationCurve)
        {
            //List<DateTime> str = new List<DateTime>();

             this.Invoke(new EventHandler(delegate
                {
                    int i;
                    for (i = 0; i < listCalibrationCurve.Count; i++)
                    {
                        lisCalibrationCurve.Add(listCalibrationCurve[i]);
                        comBoxEditCalibTime.Properties.Items.Add(listCalibrationCurve[i].DrawDate);
                        if(listCalibrationCurve[i].IsUsed == true)
                        {
                            comBoxEditCalibTime.SelectedIndex = i;
                        }
                    }
                    if(comBoxEditCalibTime.Text == "" && listCalibrationCurve.Count > 0)
                    {
                        comBoxEditCalibTime.SelectedIndex = i - 1;
                    }
                }));           
        }

        private void comBoxEditCalibTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            textEditBlkConc.Text = "";
            textEditBlkAbs.Text = "";
            textEditBlkFactor.Text = "";
            textEditSDT1Conc.Text = "";
            textEditSDT1Abs.Text = "";
            textEditFactor1.Text = "";
            textEditSDT2Conc.Text = "";
            textEditSDT2Abs.Text = "";
            textEditFactor2.Text = "";
            textEditSDT3Conc.Text = "";
            textEditSDT3Abs.Text = "";
            textEditFactor3.Text = "";
            textEditSDT4Conc.Text = "";
            textEditSDT4Abs.Text = "";
            textEditFactor4.Text = "";
            textEditSDT5Conc.Text = "";
            textEditSDT5Abs.Text = "";
            textEditFactor5.Text = "";
            textEditSDT6Conc.Text = "";
            textEditSDT6Abs.Text = "";
            textEditFactor6.Text = "";

            SDTTableItem calibrationCurve = new SDTTableItem();
            string projectName = textEditProjectName.Text;
            string CalibMethod = textEditCalibMethod.Text;
            string sampleType = textEditSampleType.Text;
            DateTime DrawDate = Convert.ToDateTime(comBoxEditCalibTime.Text);
            foreach (SDTTableItem sdt in lisCalibrationCurve)
            {
                if (sdt.ProjectName == projectName && sdt.SampleType == sampleType && sdt.CalibMethod == CalibMethod && sdt.DrawDate == DrawDate)
                {
                    calibrationCurve = sdt;
                }
            }         
            CalibConcentrationAdd(calibrationCurve);
            chartControl1.Series.Clear();
            DataTable dt = new DataTable();
            dt = CreateData();
            CreateChart(dt);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (textEditCalibMethod.Text != "K系数法")
            {
                SDTTableItem sDTTableItem = new SDTTableItem();
                sDTTableItem.ProjectName = textEditProjectName.Text;
                sDTTableItem.SampleType = textEditSampleType.Text;
                sDTTableItem.DrawDate = Convert.ToDateTime(comBoxEditCalibTime.Text);
                sDTTableItem.IsUsed = true;
                if (textEditCalibMethod.Text.Trim() != "")
                {
                    sDTTableItem.CalibMethod = textEditCalibMethod.Text;
                }
                else
                {
                    MessageBox.Show("校准方法不能为空！");
                }
                if (CalibrationEvent != null)
                {
                    CalibrationEvent(new Dictionary<string, object[]>() { { "SaveSDTTableItem", new object[] { XmlUtility.Serializer(typeof(SDTTableItem), sDTTableItem) } } });
                }
            }
            else
            {
                MessageBox.Show("改项目没有对应的校准曲线！不能保存！");
            }

        }

      
       
    }
}