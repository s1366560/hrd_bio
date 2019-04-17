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
    public partial class CalibrationTrace : DevExpress.XtraEditors.XtraForm
    {

        List<CalibrationResultinfo> results = new List<CalibrationResultinfo>();
        public CalibrationTrace()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.chartControl1.Series.Clear();
        }

         public void CalibrationAdd(CalibrationResultinfo calibrationResultinfo)
        {
            results.Clear();
            txtCheckPro.Text = calibrationResultinfo.ProjectName;
            txtSampleType.Text = calibrationResultinfo.SampleType;
            txtReagentName.Text = calibrationResultinfo.CalibratorName;
            deStartTime.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            deEndTime.Text = DateTime.Now.ToShortDateString();
        }

         public void CalibrationResultinfoAdd(List<CalibrationResultinfo> calibrationResultinfo)
         {
             foreach (CalibrationResultinfo calibrationResultinfo1 in calibrationResultinfo)
             {
                 results.Add(calibrationResultinfo1);
                 //if (calibrationResultinfo1.SampleType == txtSampleType.Text && calibrationResultinfo1.ProjectName == txtCheckPro.Text )
                 //{
                 //    results.Add(calibrationResultinfo1);
                 //   // && calibrationResultinfo[i].CalibrationDT < dateEdit1.DateTime && calibrationResultinfo[i].CalibrationDT > dateEdit2.DateTime
                 //}
             }
            DataTable dt = CreateData(results);
            CreateChart(dt);
         }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            chartControl1.Series.Clear();
            if (deStartTime.Text == "" && deStartTime.Text == string.Empty)
            {
                
                MessageBox.Show("请选择起始时间！");
                return;
            }
            if (deEndTime.Text == "" && deEndTime.Text == string.Empty)
            {
                
                MessageBox.Show("请选择结束时间！");
                return;
            }
            DataTable dt = CreateData(results);
            CreateChart(dt);
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

        private DataTable CreateData(List<CalibrationResultinfo> results)
        {
            List<CalibrationResultinfo> calibrationResultinfo = new List<CalibrationResultinfo>();
           
            foreach (CalibrationResultinfo result in results)
            {
                if (result.CalibrationDT > deStartTime.DateTime && result.CalibrationDT < deEndTime.DateTime.AddDays(1))
                {
                    calibrationResultinfo.Add(result);
                }
            }

            DataTable dt = new DataTable();
            DataColumn[] dtc = new DataColumn[calibrationResultinfo.Count + 1];
            dtc[0] = new DataColumn("项目检测次数");
            object[] obj = new object[calibrationResultinfo.Count + 1];
            obj[0] = "吸光度";

            for (int i = 0; i < calibrationResultinfo.Count; i++)
            {

                //dtc[i + 1] = new DataColumn(calibrationResultinfo[i].CalibrationDT.ToString("#0.0000"), typeof(string));
                dtc[i + 1] = new DataColumn((i + 1) + "\r\n" + calibrationResultinfo[i].CalibrationDT.ToString(), typeof(string));
                obj[i + 1] = calibrationResultinfo[i].CalibAbs;
               
            }

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

        private void chartControl1_QueryCursor(object sender, QueryCursorEventArgs e)
        {
            if (e.CursorType == CursorType.Hand || e.CursorType == CursorType.Grab)
            {
                e.Cursor = Cursors.Default;
            }
        }
    }
}