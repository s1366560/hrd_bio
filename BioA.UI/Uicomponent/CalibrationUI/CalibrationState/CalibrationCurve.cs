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
                if (listCalibrationCurve != null )
                {
                    if (listCalibrationCurve.BlkItem != null && listCalibrationCurve.BlkItem != "")
                    {
                        textEditBlkConc.Text = listCalibrationCurve.BlkConc.ToString();
                        textEditBlkAbs.Text = listCalibrationCurve.BlkAbs.ToString();
                    }
                    if (listCalibrationCurve.Calib1Item != null && listCalibrationCurve.Calib1Item != "")
                    {
                        textEditSDT1Conc.Text = listCalibrationCurve.SDT1Conc.ToString();
                        textEditSDT1Abs.Text = listCalibrationCurve.SDT1Abs.ToString();
                    }
                    if (listCalibrationCurve.Calib2Item != null && listCalibrationCurve.Calib2Item != "")
                    {
                        textEditSDT2Conc.Text = listCalibrationCurve.SDT2Conc.ToString();
                        textEditSDT2Abs.Text = listCalibrationCurve.SDT2Abs.ToString();
                    }
                    if (listCalibrationCurve.Calib3Item != null && listCalibrationCurve.Calib3Item != "")
                    {
                        textEditSDT3Conc.Text = listCalibrationCurve.SDT3Conc.ToString();
                        textEditSDT3Abs.Text = listCalibrationCurve.SDT3Abs.ToString();
                    }
                    if (listCalibrationCurve.Calib4Item != null && listCalibrationCurve.Calib4Item != "")
                    {
                        textEditSDT4Conc.Text = listCalibrationCurve.SDT4Conc.ToString();
                        textEditSDT4Abs.Text = listCalibrationCurve.SDT4Abs.ToString();
                    }
                    if (listCalibrationCurve.Calib5Item != null && listCalibrationCurve.Calib5Item != "")
                    {
                        textEditSDT5Conc.Text = listCalibrationCurve.SDT5Conc.ToString();
                        textEditSDT5Abs.Text = listCalibrationCurve.SDT5Abs.ToString();
                    }
                    if(listCalibrationCurve.Calib6Item !=null && listCalibrationCurve.Calib6Item != "")
                    {
                        textEditSDT6Conc.Text = listCalibrationCurve.SDT6Conc.ToString();
                        textEditSDT6Abs.Text = listCalibrationCurve.SDT6Abs.ToString();
                    }
                }
            }));


        }

        List<SDTTableItem> lisCalibrationCurve = new List<SDTTableItem>();
        public void SelectedlistCalibrationCurve(List<SDTTableItem> listCalibrationCurve)
        {
            List<DateTime> str = new List<DateTime>();

             this.Invoke(new EventHandler(delegate
                {
                    for (int i = 0; i < listCalibrationCurve.Count; i++)
                    {
                        lisCalibrationCurve.Add(listCalibrationCurve[i]);
                        str.Add(listCalibrationCurve[i].DrawDate);

                        if (i != listCalibrationCurve.Count - 1)
                        {
                            if (listCalibrationCurve[i].DrawDate < listCalibrationCurve[i + 1].DrawDate)
                            {
                                comBoxEditCalibTime.Text = listCalibrationCurve[i + 1].DrawDate.ToString();
                            }
                            else
                            {
                                comBoxEditCalibTime.Text = listCalibrationCurve[0].DrawDate.ToString();
                            }
                        }                          
                    }
                    //str.Sort();
                    comBoxEditCalibTime.Properties.Items.Clear();
                    //List<DateTime> str1 = str.Distinct().ToList();
                    if (str.Count > 1)
                    {
                        for (int j = 0; j < str.Count; j++)
                        {
                            comBoxEditCalibTime.Properties.Items.Add(str[j]);
                        }
                    }
                    else
                    {
                        comBoxEditCalibTime.Properties.Items.AddRange(str);
                        comBoxEditCalibTime.SelectedIndex = 0;
                    }

                    //List<SDTTableItem> calibrationCurve = new List<SDTTableItem>();
                    //for (int i = 0; i < lisCalibrationCurve.Count; i++)
                    //{
                    //    if ((comBoxEditCalibTime.SelectedIndex = i).ToString() == lisCalibrationCurve[i].DrawDate.ToString())
                    //    {
                    //        calibrationCurve.Add(lisCalibrationCurve[i]);
                    //    }
                    //}
                    if (lisCalibrationCurve.Count > 1)
                    {
                        CalibConcentrationAdd(lisCalibrationCurve[lisCalibrationCurve.Count -1]);
                        chartControl1.Series.Clear();
                        DataTable dt = new DataTable();
                        dt = CreateData();
                        CreateChart(dt);
                    }
                    else if(lisCalibrationCurve.Count == 1)
                    {
                        CalibConcentrationAdd(lisCalibrationCurve[0]);
                        chartControl1.Series.Clear();
                        DataTable dt = new DataTable();
                        dt = CreateData();
                        CreateChart(dt);
                    }
                }));           
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textEditBlkConc.Text = "";
            textEditBlkAbs.Text = "";
            textEditSDT1Conc.Text = "";
            textEditSDT1Abs.Text = "";
            textEditSDT2Conc.Text = "";
            textEditSDT2Abs.Text = "";
            textEditSDT3Conc.Text = "";
            textEditSDT3Abs.Text = "";
            textEditSDT4Conc.Text = "";
            textEditSDT4Abs.Text = "";
            textEditSDT5Conc.Text = "";
            textEditSDT5Abs.Text = "";
            textEditSDT6Conc.Text = "";
            textEditSDT6Abs.Text = "";
            //List<SDTTableItem> calibrationCurve = new List<SDTTableItem>();
            //for(int i = 0; i < lisCalibrationCurve.Count;i++)
            //{               
            //    if(comboBoxEdit1.Text==lisCalibrationCurve[i].DrawDate.ToString())
            //    {
            //        calibrationCurve.Add(lisCalibrationCurve[i]);
            //    }               
            //}
            SDTTableItem calibrationCurve = new SDTTableItem();
            string projectName = textEditProjectName.Text;
            string CalibMethod = textEditCalibMethod.Text;
            string sampleType = textEditSampleType.Text;
            string drawDate = comBoxEditCalibTime.Text;
            DateTime DrawDate = Convert.ToDateTime(drawDate);
            //int drawDate  = time.IndexOf('/');
            //int calibDate = time.IndexOf('/', drawDate);
            //string drDate = time.Substring(0, drawDate);
            //string caDate = time.Substring(drawDate + 1, calibDate);
            //DateTime DrawDate = DateTime.ParseExact(drDate, "yyyyMMdd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            //DateTime CalibDate = DateTime.ParseExact(caDate, "yyyyMMdd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SDTTableItem sDTTableItem = new SDTTableItem();
            sDTTableItem.ProjectName= textEditProjectName.Text;
            sDTTableItem.SampleType = textEditSampleType.Text;
            if (textEditCalibMethod.Text.Trim() != "")
            {
                sDTTableItem.CalibMethod = textEditCalibMethod.Text;
            }
            else
            {
                MessageBox.Show("校准方法不能为空！");
                
            }

            
            if (textEditBlkConc.Text.Trim() != "")
            {
                sDTTableItem.BlkAbs = (float)Convert.ToDouble(textEditBlkAbs.Text);
                sDTTableItem.BlkConc = (float)Convert.ToDouble(textEditBlkConc.Text);
            }
            if (textEditSDT1Conc.Text.Trim() != "")
            {
                sDTTableItem.SDT1Abs = (float)Convert.ToDouble(textEditSDT1Abs.Text);
                sDTTableItem.SDT1Conc = (float)Convert.ToDouble(textEditSDT1Conc.Text);
            }
            if (textEditSDT2Conc.Text.Trim() != "")
            {
                sDTTableItem.SDT2Abs = (float)Convert.ToDouble(textEditSDT2Abs.Text);
                sDTTableItem.SDT2Conc = (float)Convert.ToDouble(textEditSDT2Conc.Text);
            }
            if (textEditSDT3Conc.Text.Trim() != "")
            {
                sDTTableItem.SDT3Abs = (float)Convert.ToDouble(textEditSDT3Abs.Text);
                sDTTableItem.SDT3Conc = (float)Convert.ToDouble(textEditSDT3Conc.Text);
            }
            if (textEditSDT4Conc.Text.Trim() != "")
            {
                sDTTableItem.SDT4Abs = (float)Convert.ToDouble(textEditSDT4Abs.Text);
                sDTTableItem.SDT4Conc = (float)Convert.ToDouble(textEditSDT4Conc.Text);
            }
            if (textEditSDT5Conc.Text.Trim() != "")
            {
                sDTTableItem.SDT5Abs = (float)Convert.ToDouble(textEditSDT5Abs.Text);
                sDTTableItem.SDT5Conc = (float)Convert.ToDouble(textEditSDT5Conc.Text);
            }
            if (textEditSDT6Conc.Text.Trim() != "")
            {
                sDTTableItem.SDT6Abs = (float)Convert.ToDouble(textEditSDT6Abs.Text);
                sDTTableItem.SDT6Conc = (float)Convert.ToDouble(textEditSDT6Conc.Text);
            }
            if (CalibrationEvent!=null)
            {
                CalibrationEvent(new Dictionary<string, object[]>() { { "AddSDTTableItem", new object[] { XmlUtility.Serializer(typeof(SDTTableItem), sDTTableItem) } } });
            }

        }

        private void CalibrationCurve_Load(object sender, EventArgs e)
        {

        }

      
       
    }
}