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
using DevExpress.XtraCharts;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Drawing2D;
using BioA.Common;
using BioA.Common.IO;
using System.Drawing.Imaging;
using System.Threading;

namespace BioA.UI
{
    public partial class QualityControlGraphs : DevExpress.XtraEditors.XtraUserControl
    {
        private PrintDocument printDocument1 = new PrintDocument();
        PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        string fileName;

        
          //以下用户可自定义
        //当前要打印文本的字体及字号
        private static Font TableFont = new Font("Verdana", 10, FontStyle.Regular);
        //表头字体
        private Font HeadFont = new Font("Verdana", 20, FontStyle.Bold);
        //表头文字
        private string HeadText = string.Empty;
        //表头高度
        private int HeadHeight = 40;
        //表的基本单位
        private int[] XUnit;
        private int YUnit = TableFont.Height * 2;
        //以下为模块内部使用
        private PrintDocument DataTablePrinter;
        private DataRow DataGridRow;
        private DataTable DataTablePrint;
        //当前要所要打印的记录行数,由计算得到
        private int PageRecordNumber;
        //正要打印的页号
        private int PrintingPageNumber = 1;
        //已经打印完的记录数
        private int PrintRecordComplete;
        private int PLeft;
        private int PTop;
        private int PRight;
        private int PBottom;
        private int PWidth;
        private int PHeigh;
        //当前画笔颜色
        private SolidBrush DrawBrush = new SolidBrush(Color.Black);
        //每页打印的记录条数
        private int PrintRecordNumber;
        //第一页打印的记录条数
        private int FirstPrintRecordNumber;
        //总共应该打印的页数
        private int TotalPage;
        //与列名无关的统计数据行的类目数（如，总计，小计......）
        public int TotalNum = 0;

        List<QCResultForUIInfo> results;


        public QualityControlGraphs()
        {
            InitializeComponent();
            checkedListBox1.SetItemChecked(0,true);
            checkedListBox1.SetItemChecked(1, true);
            checkedListBox1.SetItemChecked(2, true);
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }
        private object[] Saveobj3(object[] obj, List<QCResultForUIInfo> results)
        {
            object[] obj2 = new object[obj.Length];
            obj2[0] = "XSD";
            for (int i = 1; i < obj.Length; i++)
            {
                try
                {
                    if (Convert.ToInt32(obj[i]) >= 2 && Convert.ToInt32(obj[i]) < 3 || Convert.ToInt32(obj[i]) <= -2 && Convert.ToInt32(obj[i])>-3)
                    {
                        obj2[i] = obj[i];
                    }                  
                    else
                    {
                        obj2[i] = string.Empty;
                    }
                }
                catch
                {
                    obj2[i] = string.Empty;
                }
            }
            return obj2;
        }
        private object[] Saveobj2(object[] obj, List<QCResultForUIInfo> results)
        {
            object[] obj2 = new object[obj.Length];
            obj2[0] = "XSD";
            for (int i = 1; i < obj.Length; i++)
            {
                try
                {
                    if (Convert.ToInt32(obj[i]) >= 3 || Convert.ToInt32(obj[i]) <= -3)
                    {
                        obj2[i] = obj[i];
                    }
                    else if (Convert.ToInt32(obj[i]) >= 2  && Convert.ToInt32(obj[i - 1]) >= 2  && Convert.ToInt32(obj[i]) < 3  && Convert.ToInt32(obj[i - 1]) < 3 
                        || Convert.ToInt32(obj[i]) <= -2  && Convert.ToInt32(obj[i - 1]) <= -2  && Convert.ToInt32(obj[i]) > -3 && Convert.ToInt32(obj[i - 1]) > -3 )
                    {
                        obj2[i] = obj[i];
                    }
                    else if (Math.Abs(Convert.ToInt32(obj[i]) - Convert.ToInt32(obj[i - 1])) >= 4 && Convert.ToInt32(obj[i - 1]) < 3  && Convert.ToInt32(obj[i - 1]) > -3 
                        && Convert.ToInt32(obj[i]) > -3 && Convert.ToInt32(obj[i]) < 3 )
                    {
                        obj2[i] = obj[i];
                    }
                    else if (Convert.ToInt32(obj[i]) >= 1  && Convert.ToInt32(obj[i - 1]) >= 1  && Convert.ToInt32(obj[i - 2]) >= 1  && Convert.ToInt32(obj[i - 3]) >= 1 
                        && Convert.ToInt32(obj[i]) < 3  && Convert.ToInt32(obj[i - 1]) < 3  && Convert.ToInt32(obj[i - 2]) < 3  && Convert.ToInt32(obj[i - 3]) < 3 
                        || Convert.ToInt32(obj[i]) <= -1  && Convert.ToInt32(obj[i - 1]) <= -1  && Convert.ToInt32(obj[i - 2]) <= -1 && Convert.ToInt32(obj[i - 3]) <= -1 
                         && Convert.ToInt32(obj[i]) > -3  && Convert.ToInt32(obj[i - 1]) > -3  && Convert.ToInt32(obj[i - 2]) > -3  && Convert.ToInt32(obj[i - 3]) > -3 )
                    {
                        obj2[i] = obj[i];
                    }
                    else if (
                        Convert.ToInt32(obj[i]) > 0 && Convert.ToInt32(obj[i - 1]) > 0 && Convert.ToInt32(obj[i - 2]) > 0 && Convert.ToInt32(obj[i - 3]) > 0 &&
                        Convert.ToInt32(obj[i - 4]) > 0 && Convert.ToInt32(obj[i - 5]) > 0 && Convert.ToInt32(obj[i - 6]) > 0 && Convert.ToInt32(obj[i - 7]) > 0 &&
                        Convert.ToInt32(obj[i - 8]) > 0 && Convert.ToInt32(obj[i - 9]) > 0 &&
                        Convert.ToInt32(obj[i]) < 3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 1]) < 3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 2]) < 3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 3]) < 3 * results[i - 1].TargetSD &&
                        Convert.ToInt32(obj[i - 4]) < 3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 5]) < 3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 6]) < 3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 7]) < 3 * results[i - 1].TargetSD &&
                        Convert.ToInt32(obj[i - 8]) < 3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 9]) < 3 * results[i - 1].TargetSD
                       || Convert.ToInt32(obj[i]) < 0 && Convert.ToInt32(obj[i - 1]) < 0 && Convert.ToInt32(obj[i - 2]) < 0 && Convert.ToInt32(obj[i - 3]) < 0
                        && Convert.ToInt32(obj[i - 4]) < 0 && Convert.ToInt32(obj[i - 5]) < 0 && Convert.ToInt32(obj[i - 6]) < 0 && Convert.ToInt32(obj[i - 7]) < 0
                        && Convert.ToInt32(obj[i - 8]) < 0 && Convert.ToInt32(obj[i - 9]) < 0 &&
                        Convert.ToInt32(obj[i]) > -3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 1]) > -3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 2]) > -3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 3]) > -3 * results[i - 1].TargetSD &&
                        Convert.ToInt32(obj[i - 4]) > -3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 5]) > -3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 6]) > -3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 7]) > -3 * results[i - 1].TargetSD &&
                        Convert.ToInt32(obj[i - 8]) > -3 * results[i - 1].TargetSD && Convert.ToInt32(obj[i - 9]) > -3 * results[i - 1].TargetSD
                           )
                    {
                        obj2[i] = obj[i];
                    }
                    else
                    {
                        obj2[i] = string.Empty;
                    }
                }
                catch
                {
                    obj2[i] = string.Empty;
                }
            }
            return obj2;
        }

        //private DataTable CreateData(List<QCResultForUIInfo> results)
        //{
        //    List<QCResultForUIInfo> resultsUIInfo = new List<QCResultForUIInfo>();
        //    List<QCResultForUIInfo> resultstimes = new List<QCResultForUIInfo>();
        //    for(int i=0;i<results.Count;i++)
        //    {
        //        for(int j=0;j<resultsUIInfo.Count;j++)
        //            {
        //                if (results[i].SampleCreateTime.ToShortDateString() == resultsUIInfo[j].SampleCreateTime.ToShortDateString()
        //                    && (results[i].ConcResult - results[i].TargetMean) * (1 / results[i].TargetSD) <
        //                    (resultsUIInfo[j].ConcResult - resultsUIInfo[j].TargetMean) * (1 / resultsUIInfo[j].TargetSD))
        //                    {
        //                        resultsUIInfo.Remove(resultsUIInfo[j]);
        //                        resultstimes.Add(resultsUIInfo[j]);
        //                        resultsUIInfo.Add(results[i]);
        //                    }
        //                else if (results[i].SampleCreateTime.ToShortDateString() == resultsUIInfo[j].SampleCreateTime.ToShortDateString()
        //                    && (results[i].ConcResult - results[i].TargetMean) * (1 / results[i].TargetSD) >
        //                    (resultsUIInfo[j].ConcResult - resultsUIInfo[j].TargetMean) * (1 / resultsUIInfo[j].TargetSD))
        //                    {
        //                        resultstimes.Add(resultsUIInfo[j]);
        //                    }
        //                else
        //                    {
        //                        resultsUIInfo.Add(results[i]);
        //                    }
        //            }                                                                         
                
        //    }
        //    resultsUIInfo.Sort(delegate(QCResultForUIInfo x, QCResultForUIInfo y)
        //    {
        //        return x.SampleCreateTime.CompareTo(y.SampleCreateTime);
        //    });
        //    DataTable dt = new DataTable();
        //    DataColumn[] dtc = new DataColumn[resultsUIInfo.Count + 1];
        //    dtc[0] = new DataColumn("日期");


        //    for (int i = 0; i < resultsUIInfo.Count; i++)
        //    {
        //        dtc[i + 1] = new DataColumn(resultsUIInfo[i].SampleCreateTime.ToString(), typeof(string));            
        //    }


        //    dt.Columns.AddRange(dtc);
        //    object[] obj = new object[resultsUIInfo.Count + 1];
        //    object[] obj2 = new object[resultsUIInfo.Count + 1];
        //    object[] obj5 = new object[resultsUIInfo.Count + 1];
        //    obj[0] = "SD";
        //    obj2[0] = "XSD";
        //    obj5[0] = "XSD";
        //    for (int i = 0; i < resultsUIInfo.Count; i++)
        //    {
        //        obj[i + 1] = (resultsUIInfo[i].ConcResult - resultsUIInfo[i].TargetMean) * (1 / resultsUIInfo[i].TargetSD);                
        //    }

        //    obj2 = Saveobj2(obj, resultsUIInfo);
        //    obj5 = Saveobj3(obj, resultsUIInfo);
        //    dt.Rows.Add(obj);
        //    dt.Rows.Add(obj2);
        //    dt.Rows.Add(obj5);
        //    object[] obj3 = new object[resultsUIInfo.Count + 1];
        //    for (int i = 0; i < resultsUIInfo.Count; i++)
        //    {
        //        for (int j = 0; j < resultstimes.Count; j++)
        //        {
        //            if (resultsUIInfo[i].SampleCreateTime.ToShortDateString() == resultstimes[j].SampleCreateTime.ToShortDateString())
        //            {
        //                obj3[i + 1] = (resultstimes[j].ConcResult - resultstimes[j].TargetMean) * (1 / resultstimes[j].TargetSD);
        //                resultstimes.Remove(resultstimes[j]);
        //            }
        //            else
        //            {
        //                obj3[i + 1] = string.Empty;
        //            }
        //        }
        //    }
        //    dt.Rows.Add(obj3);
        //    object[] obj4 = new object[resultsUIInfo.Count + 1];
        //    object[] obj6 = new object[resultsUIInfo.Count + 1];
        //    obj6 = Saveobj3(obj3, resultsUIInfo);
        //    obj4 = Saveobj2(obj3, resultsUIInfo);
        //    dt.Rows.Add(obj4);
        //    dt.Rows.Add(obj6);
        //    return dt;
        //}
        /// <summary>
        /// 创建表格数据
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private DataTable CreateData(List<QCResultForUIInfo> results)
        {
            results.Sort(delegate(QCResultForUIInfo x, QCResultForUIInfo y)
            {
                return x.SampleCreateTime.CompareTo(y.SampleCreateTime);
            });
            DataTable dt = new DataTable();
            DataColumn[] dtc = new DataColumn[results.Count + 1];
            dtc[0] = new DataColumn("日期");


            for (int i = 0; i < results.Count; i++)
            {
                dtc[i + 1] = new DataColumn(results[i].SampleCreateTime.ToString(), typeof(string));
            }


            dt.Columns.AddRange(dtc);
            object[] obj = new object[results.Count + 1];
            obj[0] = "浓度";
            for (int i = 0; i < results.Count; i++)
            {
                
                obj[i + 1] = Math.Round(results[i].ConcResult, 2, MidpointRounding.AwayFromZero);
            }
            dt.Rows.Add(obj);
            return dt;
        }
        /// <summary>
        /// 创建图形
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="viewType"></param>
        /// <param name="dt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int index)
        {
            Series series = new Series(caption, viewType);
            //for (int i = 1; i < dt.Columns.Count; i++)
            //{
                string argument = dt.Columns[index].ColumnName;//参数名称
                string value = (string)dt.Rows[0][index];//参数值
                if (value != string.Empty)
                {
                    series.Points.Add(new SeriesPoint(argument, value));
                }
            //}

            //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
            //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
            
            return series;
        }
        private void CreateChart(DataTable dt, string lorizonLevel)
        {
            #region Series
            //创建几个图形的对象

            Series series1;
            Series series2;
            Series series3;
            Series series4;
            //Series series2 = CreateSeries("1SD：正常！", ViewType.Line, dt, 1);
            //Series series3 = CreateSeries("2SD：警告！", ViewType.Line, dt, 2);
            //Series series4 = CreateSeries("3SD：错误！", ViewType.Line, dt, 3);
            //Series series5 = CreateSeries("XSD", ViewType.Point, dt, 4);
            //Series series6 = CreateSeries("XSD", ViewType.Point, dt, 5);
            List<Series> list = new List<Series>();
            if (lorizonLevel == "高" || lorizonLevel == "中" || lorizonLevel == "低")
            {
                for (int i = 0; i < results.Count; i++)
                {
                    //for (int j1 = 0; j1 < series1.Points.Count; j1++)
                    //{
                    float str = float.Parse(dt.Rows[0][i + 1].ToString());
                    if ((str < (results[i].TargetMean + results[i].TargetSD) && str >= results[i].TargetMean) ||
                        (str > (results[i].TargetMean - results[i].TargetSD) && str <= results[i].TargetMean))
                    {
                        series1 = CreateSeries("SD：正常！", ViewType.Line, dt, i + 1);
                        LineSeriesView lineSeriesView1 = new LineSeriesView();
                        lineSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(238)))));
                        lineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(238)))));
                        lineSeriesView1.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Circle;
                        lineSeriesView1.LineMarkerOptions.Size = 10;
                        lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series1.View = lineSeriesView1;
                        list.Add(series1);
                    }
                    if ((str < (results[i].TargetMean + results[i].TargetSD * 2) && str >= results[i].TargetMean + results[i].TargetSD) ||
                        (str > (results[i].TargetMean - results[i].TargetSD * 2) && str <= results[i].TargetMean - results[i].TargetSD))
                    {
                        series2 = CreateSeries("1SD：正常！", ViewType.Line, dt, i + 1);
                        LineSeriesView lineSeriesView2 = new LineSeriesView();
                        lineSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(238)))));
                        lineSeriesView2.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(238)))));
                        lineSeriesView2.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Circle;
                        lineSeriesView2.LineMarkerOptions.Size = 10;
                        lineSeriesView2.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series2.View = lineSeriesView2;
                        list.Add(series2);
                    }
                    if ((str < (results[i].TargetMean + results[i].TargetSD * 3) && str >= results[i].TargetMean + results[i].TargetSD * 2) ||
                        (str > (results[i].TargetMean - results[i].TargetSD * 3) && str <= results[i].TargetMean - results[i].TargetSD * 2))
                    {
                        series3 = CreateSeries("2SD：警告！", ViewType.Line, dt, i + 1);
                        LineSeriesView lineSeriesView3 = new LineSeriesView();
                        lineSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(154)))), ((int)(((byte)(0)))));
                        lineSeriesView3.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(154)))), ((int)(((byte)(0)))));
                        lineSeriesView3.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Circle;
                        lineSeriesView3.LineMarkerOptions.Size = 10;
                        lineSeriesView3.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series3.View = lineSeriesView3;
                        list.Add(series3);
                    }
                    if (str > (results[i].TargetMean + results[i].TargetSD * 3) ||
                        str < (results[i].TargetMean - results[i].TargetSD * 3))
                    {
                        series4 = CreateSeries("3SD：错误！", ViewType.Line, dt, i + 1);
                        LineSeriesView lineSeriesView4 = new LineSeriesView();
                        lineSeriesView4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        lineSeriesView4.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        lineSeriesView4.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Diamond;
                        lineSeriesView4.LineMarkerOptions.Size = 10;
                        lineSeriesView4.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series4.View = lineSeriesView4;
                        list.Add(series4);
                    }
                }
            }
            
            #endregion
            chartControl1.Series.AddRange(list.ToArray());
        }

      

          
        private void MyPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("XXX医院质控报告模版", new Font(new FontFamily("黑体"), 11), System.Drawing.Brushes.Black, 500, 10);

            //信息的名称
            e.Graphics.DrawLine(Pens.Black, 8, 30, 1161, 30);
            e.Graphics.DrawString("项目：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 10, 35);
            e.Graphics.DrawString("总蛋白", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 40, 35);
            e.Graphics.DrawString("水平：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 95, 35);
            e.Graphics.DrawString("二", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 125, 35);

            e.Graphics.DrawString("报告日期：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 280, 35);
            e.Graphics.DrawString("2017/10/23", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 335, 35);
            e.Graphics.DrawString("-", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 397, 35);
            e.Graphics.DrawString("2017/10/24", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 405, 35);
            //e.Graphics.DrawString("总金额", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 400, 35);
            e.Graphics.DrawLine(Pens.Black, 8, 50, 1161, 50);

            //信息
            Image image = Image.FromFile(@"D:\屏幕截图\" + fileName);
            e.Graphics.DrawImage(image, new RectangleF(30, 60, 1350, 700));
            e.Graphics.DrawLine(Pens.Black, 8, 790, 1161, 790);
            e.Graphics.DrawString("MEAN：1.232", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 10, 798);
            e.Graphics.DrawString("SD：1.663", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 95, 798);

            int tableWith = 0;
            string ColumnText;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            //打印表格线格式
            Pen Pen = new Pen(Brushes.Black, 1);

            #region 设置列宽

            //foreach (DataRow dr in DataTablePrint.Rows)
            //{
            //    for (int i = 0; i < DataTablePrint.Columns.Count; i++)
            //    {
            //        int colwidth = Convert.ToInt32(e.Graphics.MeasureString(dr[i].ToString().Trim(), TableFont).Width);
            //        if (colwidth > XUnit[i])
            //        {
            //            XUnit[i] = colwidth;
            //        }
            //    }
            //}

            //if (PrintingPageNumber == 1)
            //{
            //    for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
            //    {
            //        ColumnText = DataTablePrint.Columns[Cols].ColumnName.ToString().Trim();
            //        int colwidth = Convert.ToInt32(e.Graphics.MeasureString(ColumnText, TableFont).Width);
            //        if (colwidth > XUnit[Cols])
            //        {
            //            XUnit[Cols] = colwidth;
            //        }
            //    }
            //}
            //for (int i = 0; i < XUnit.Length; i++)
            //{
            //    tableWith += XUnit[i];
            //}
            #endregion

            //PLeft = (e.PageBounds.Width - tableWith) / 2;
            //int x = PLeft;
            //int y = PTop;
            //int stringY = PTop + (YUnit - TableFont.Height) / 2;
            //int rowOfTop = PTop;

            //第一页
            //if (PrintingPageNumber == 1)
            //{
            //    //打印表头
            //    e.Graphics.DrawString(HeadText, HeadFont, DrawBrush, new Point(e.PageBounds.Width / 2, PTop), sf);

            //    //设置为第一页时行数
            //    PageRecordNumber = FirstPrintRecordNumber;
            //    rowOfTop = y = PTop + HeadFont.Height + 10;
            //    stringY = PTop + HeadFont.Height + 10 + (YUnit - TableFont.Height) / 2;
            //}
            //else
            //{
            //    //计算,余下的记录条数是否还可以在一页打印,不满一页时为假
            //    if (DataTablePrint.Rows.Count - PrintRecordComplete >= PrintRecordNumber)
            //    {
            //        PageRecordNumber = PrintRecordNumber;
            //    }
            //    else
            //    {
            //        PageRecordNumber = DataTablePrint.Rows.Count - PrintRecordComplete;
            //    }
            //}

            #region 列名
            //if (PrintingPageNumber == 1 || PageRecordNumber > TotalNum)//最后一页只打印统计行时不打印列名
            //{
            //    //得到datatable的所有列名
            //    for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
            //    {
            //        ColumnText = DataTablePrint.Columns[Cols].ColumnName.ToString().Trim();

            //        int colwidth = Convert.ToInt32(e.Graphics.MeasureString(ColumnText, TableFont).Width);
            //        e.Graphics.DrawString(ColumnText, TableFont, DrawBrush, x, stringY);
            //        x += XUnit[Cols];
            //    }
            //}
            #endregion



            //e.Graphics.DrawLine(Pen, PLeft, rowOfTop, x, rowOfTop);
            //stringY += YUnit;
            //y += YUnit;
            //e.Graphics.DrawLine(Pen, PLeft, y, x, y);

            //当前页面已经打印的记录行数
            //int PrintingLine = 0;
            //while (PrintingLine < 3)
            //{
            //    x = PLeft;
            //    //确定要当前要打印的记录的行号
            //    DataGridRow = DataTablePrint.Rows[PrintRecordComplete];
            //    for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
            //    {
            //        e.Graphics.DrawString(DataGridRow[Cols].ToString().Trim(), TableFont, DrawBrush, x, stringY);
            //        x += XUnit[Cols];
            //    }
            //    stringY += YUnit;
            //    y += YUnit;
            //    e.Graphics.DrawLine(Pen, PLeft, y, x, y);

            //    PrintingLine += 1;
            //    PrintRecordComplete += 1;
            //    if (PrintRecordComplete >= DataTablePrint.Rows.Count)
            //    {
            //       e.HasMorePages = false;
            //        PrintRecordComplete = 0;
            //    }
            //}

            //e.Graphics.DrawLine(Pen, PLeft, rowOfTop, PLeft, y);
            //x = PLeft;
            //for (int Cols = 0; Cols < DataTablePrint.Columns.Count; Cols++)
            //{
            //    x += XUnit[Cols];
            //   e.Graphics.DrawLine(Pen, x, rowOfTop, x, y);
            //}



            //PrintingPageNumber += 1;

            //if (PrintingPageNumber > TotalPage)
            //{
            //    e.HasMorePages = false;
            //    PrintingPageNumber = 1;
            //    PrintRecordComplete = 0;
            //}
            //else
            //{
            //    e.HasMorePages = true;
            //}


        }


    




    

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(SelectAllQCResultByQCInfo));
            
        }
        /// <summary>
        /// 根据质控信息获取浓度结果信息
        /// </summary>
        private void SelectAllQCResultByQCInfo()
        {
            chartControl1.Series.Clear();
            QCResultForUIInfo qcResForUIInfo = new QCResultForUIInfo();
            if (cboProjectName.Text != "")
            {
                qcResForUIInfo.ProjectName = cboProjectName.SelectedItem.ToString();
            }
            if (cboQCName.Text != "")
            {
                qcResForUIInfo.QCName = cboQCName.SelectedItem.ToString();
            }
            if (cboLot.Text != "")
            {
                qcResForUIInfo.LotNum = cboLot.SelectedItem.ToString();
            }
            if (cboManufacturer.Text != "")
            {
                qcResForUIInfo.Manufacturer = cboManufacturer.SelectedItem.ToString();
            }
            qcResForUIInfo.QCTimeStartTS = System.Convert.ToDateTime((dtpStartTime.Value).ToShortDateString());
            qcResForUIInfo.QCTimeEndTS = System.Convert.ToDateTime((dtpEndTime.Value).AddDays(1).ToShortDateString());

            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCGraphic, XmlUtility.Serializer(typeof(CommunicationEntity),
                                                                                                             new CommunicationEntity("QueryQCResultForQCGraphics",
                                                                                                                                     XmlUtility.Serializer(typeof(QCResultForUIInfo),qcResForUIInfo))));
            // this.chartControl1.Visible = false;
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
           
            if (Directory.Exists(" d:\\屏幕截图"))  //判断目录是否存在,不存在就创建
            { }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(" d:\\屏幕截图");
                directoryInfo.Create();
            }
            //创建图片对象
            Bitmap bmp2 = new Bitmap(Screen.PrimaryScreen.Bounds.Width - 210, Screen.PrimaryScreen.Bounds.Height - 100);
            Graphics g2 = Graphics.FromImage(bmp2);  //创建画笔

            g2.CopyFromScreen(new Point(0, 0), new Point(-310, -285), bmp2.Size);//截屏
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");//获得系统时间
            time = System.Text.RegularExpressions.Regex.Replace(time, @"[^0-9]+", "");//提取数字
            fileName = time + ".bmp"; //创建文件名

            bmp2.Save("d:\\屏幕截图\\" + fileName); //保存为文件  ,注意格式是否正确.
            bmp2.Dispose();//关闭对象
            g2.Dispose();//关闭画笔

            //s.SaveImage("d:\\dd.Bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            // printDocument1 为 打印控件
            //设置打印用的纸张 当设置为Custom的时候，可以自定义纸张的大小，还可以选择A4,A5等常用纸型
            PaperSize paperSize = new PaperSize();
            paperSize.PaperName = "Custum";


            this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 827,1169);
            printDocument1.DefaultPageSettings.Landscape = true;    //横向打印

            //List<QCResultForUIInfo> QCResultHigh = new List<QCResultForUIInfo>();
            //List<QCResultForUIInfo> QCResultMin = new List<QCResultForUIInfo>();
            //List<QCResultForUIInfo> QCResultLow = new List<QCResultForUIInfo>();
            //for (int i = 0; i < results.Count; i++)
            //{
            //    if (results[i].HorizonLevel == "高")
            //    {
            //        QCResultHigh.Add(results[i]);
            //    }
            //    if (results[i].HorizonLevel == "中")
            //    {
            //        QCResultMin.Add(results[i]);
            //    }
            //    if (results[i].HorizonLevel == "低")
            //    {
            //        QCResultLow.Add(results[i]);
            //    }

            //}
      

            //DataTable dt = new DataTable();
            //DataColumn[] dtc = new DataColumn[results.Count + 1];
            //dtc[0] = new DataColumn("日期");


            //for (int i = 0; i < results.Count; i++)
            //{
            //    dtc[i + 1] = new DataColumn(results[i].SampleCreateTime.ToString(), typeof(string));
            //}


            //dt.Columns.AddRange(dtc);
            //object[] obj = new object[results.Count + 1];
            //object[] obj1 = new object[results.Count + 1];
            //object[] obj2 = new object[results.Count + 1];
           
            //obj[0] = "高浓度值";
            //obj1[0] = "中浓度值";
            //obj2[0] = "低浓度值";

            //if (QCResultHigh.Count > 0 && checkedListBox1.GetItemChecked(0))
            //{
            //    for (int i = 0; i < QCResultHigh.Count; i++)
            //    {
            //        obj[i + 1] = QCResultHigh[i].ConcResult;
            //    }
            //}
            //if (QCResultMin.Count > 0 && checkedListBox1.GetItemChecked(1))
            //{
            //    for (int i = 0; i < QCResultMin.Count; i++)
            //    {
            //        obj1[i + 1] = QCResultMin[i].ConcResult;
            //    }
            //}
            //if (QCResultLow.Count > 0 && checkedListBox1.GetItemChecked(2))
            //{
            //    for (int i = 0; i < QCResultLow.Count; i++)
            //    {
            //        obj2[i + 1] = QCResultLow[i].ConcResult;
            //    }
            //}

            ////for (int i = 0; i < results.Count; i++)
            ////{
            ////    obj[i + 1] = results[i].ConcResult ;
            ////}

          

            //dt.Rows.Add(obj);
            //dt.Rows.Add(obj1);
            //dt.Rows.Add(obj2);
            
            //DataTablePrint = dt;
           // HeadText = Title;

          
            //DataTablePrinter = new PrintDocument();

            PageSetupDialog PageSetup = new PageSetupDialog();
            PageSetup.Document = printDocument1;
            printDocument1.DefaultPageSettings = PageSetup.PageSettings;
            printDocument1.DefaultPageSettings.Landscape = true;//设置打印横向还是纵向
            //PLeft = 30; //DataTablePrinter.DefaultPageSettings.Margins.Left;
            PTop = 520; //DataTablePrinter.DefaultPageSettings.Margins.Top;
            //PRight = DataTablePrinter.DefaultPageSettings.Margins.Right;
            PBottom = printDocument1.DefaultPageSettings.Margins.Bottom;
            PWidth = printDocument1.DefaultPageSettings.Bounds.Width;
            PHeigh = printDocument1.DefaultPageSettings.Bounds.Height;
            //XUnit = new int[DataTablePrint.Columns.Count];
            //PrintRecordNumber = Convert.ToInt32((PHeigh - PTop - PBottom - YUnit) / YUnit);
            //FirstPrintRecordNumber = Convert.ToInt32((PHeigh - PTop - PBottom - HeadHeight - YUnit) / YUnit);

            //if (DataTablePrint.Rows.Count > PrintRecordNumber)
            //{
            //    if ((DataTablePrint.Rows.Count - FirstPrintRecordNumber) % PrintRecordNumber == 0)
            //    {
            //        TotalPage = (DataTablePrint.Rows.Count - FirstPrintRecordNumber) / PrintRecordNumber + 1;
            //    }
            //    else
            //    {
            //        TotalPage = (DataTablePrint.Rows.Count - FirstPrintRecordNumber) / PrintRecordNumber + 2;
            //    }
            //}
            //else
            //{
            //    TotalPage = 1;
            //}
            
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            //显示打印预览
            DialogResult result = printPreviewDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //this.MyPrintDocument.Print();
        }

        private void QualityControlGraphs_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadQCGraphsInfoLoad));
        }
        private void loadQCGraphsInfoLoad()
        {
            // 1.获取项目名称
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCGraphic, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProjectName", null)));
            // 2.获取质控信息
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCGraphic, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCAllInfo", null)));
        }
        /// <summary>
        /// 接收数据库数据传输
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="sender"></param>
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryProjectName":
                    List<string> lstProjectNames = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    this.Invoke(new EventHandler(delegate { cboProjectName.Properties.Items.AddRange(lstProjectNames); }));
                    
                    break;
                case "QueryQCAllInfo":
                    List<QualityControlInfo> lstQCInfo = (List<QualityControlInfo>)XmlUtility.Deserialize(typeof(List<QualityControlInfo>), sender as string);
                    this.Invoke(new EventHandler(delegate { 
                        foreach (QualityControlInfo QCInfo in lstQCInfo)
                        {
                            if (!cboQCName.Properties.Items.Contains(QCInfo.QCName))
                                cboQCName.Properties.Items.Add(QCInfo.QCName);

                            if (!cboLot.Properties.Items.Contains(QCInfo.LotNum))
                                cboLot.Properties.Items.Add(QCInfo.LotNum);

                            if (!cboManufacturer.Properties.Items.Contains(QCInfo.Manufacturer))
                                cboManufacturer.Properties.Items.Add(QCInfo.Manufacturer);
                        }
                    }));
                    break;
                case "QueryQCResultForQCGraphics":
                    // 包含信息有水平浓度、质控时间、质控结果浓度
                    results = (List<QCResultForUIInfo>)XmlUtility.Deserialize(typeof(List<QCResultForUIInfo>), sender as string);
                    List<QCResultForUIInfo> QCResultHigh = new List<QCResultForUIInfo>();
                    List<QCResultForUIInfo> QCResultMin = new List<QCResultForUIInfo>();
                    List<QCResultForUIInfo> QCResultLow = new List<QCResultForUIInfo>();
                    
                    for (int i=0;i<results.Count;i++)
                    {
                        if(results[i].HorizonLevel=="高")
                        {
                            QCResultHigh.Add(results[i]);
                        }
                        if(results[i].HorizonLevel=="中")
                        {
                            QCResultMin.Add(results[i]);
                        }
                        if (results[i].HorizonLevel == "低")
                        {
                            QCResultLow.Add(results[i]);
                        }

                    }
                    if (QCResultHigh.Count > 0 && checkedListBox1.GetItemChecked(0))
                    {
                        DataTable dtHigh = CreateData(QCResultHigh);
                        CreateChart(dtHigh, "高");
                    }
                    if (QCResultMin.Count > 0 && checkedListBox1.GetItemChecked(1))
                    {
                        DataTable dtMin = CreateData(QCResultMin);
                        CreateChart(dtMin, "中");
                    }
                    if (QCResultLow.Count > 0 && checkedListBox1.GetItemChecked(2))
                    {
                        DataTable dtLow = CreateData(QCResultLow);
                        CreateChart(dtLow, "低");
                    }
                    Dline(results);
                    Thread.Sleep(500);
                    break;
                default:
                    break;
            }
        }
        private void Dline(List<QCResultForUIInfo> results)
        {
            try
            {
                if (results.Count > 0)
                {
                    XYDiagram diagram = chartControl1.Diagram as XYDiagram;
                    //diagram.AxisY.ConstantLines.Clear();
                    chartControl1.Titles.Clear();
                    ChartTitle SD1title = new ChartTitle();
                    SD1title.Text = "● Mean/±1SD — 正常！" + "     ● ±2SD警告！" + "     ● ±3SD错误！";
                    SD1title.TextColor = Color.OrangeRed;
                    SD1title.Font = new System.Drawing.Font("Tahoma", (float)10);
                    chartControl1.Titles.Add(SD1title);

                    //diagram.AxisY.NumericScaleOptions.GridOffset = 0;
                    double StandardDeviation1 = Math.Round((double)(results[0].TargetMean + results[0].TargetSD), 1, MidpointRounding.AwayFromZero);
                    ConstantLine constantLine1 = new ConstantLine("1SD", StandardDeviation1);
                    constantLine1.Color = Color.Blue; //直线颜色
                    constantLine1.Title.TextColor = Color.Blue;   //直线文本字体颜色
                    constantLine1.LineStyle.Thickness = 2;
                    diagram.AxisY.ConstantLines.Add(constantLine1);
                    double StandardDeviation2 = Math.Round((double)(results[0].TargetMean + results[0].TargetSD * 2), 1, MidpointRounding.AwayFromZero);
                    ConstantLine constantLine4 = new ConstantLine("2SD", StandardDeviation2);
                    constantLine4.Color = Color.Orange; //直线颜色
                    constantLine4.Title.TextColor = Color.Orange;   //直线文本字体颜色
                    constantLine4.LineStyle.Thickness = 2;
                    diagram.AxisY.ConstantLines.Add(constantLine4);
                    double StandardDeviation3 = Math.Round((double)(results[0].TargetMean + results[0].TargetSD * 3), 1, MidpointRounding.AwayFromZero);
                    ConstantLine constantLine5 = new ConstantLine("3SD", StandardDeviation3);
                    constantLine5.Color = Color.Red; //直线颜色  
                    constantLine5.Title.TextColor = Color.Red;   //直线文本字体颜色
                    constantLine5.LineStyle.Thickness = 2;
                    diagram.AxisY.ConstantLines.Add(constantLine5);
                    double Mean = Math.Round(results[0].TargetMean, 1, MidpointRounding.AwayFromZero);
                    ConstantLine constantLine2 = new ConstantLine("MEAN", Mean);
                    constantLine2.Color = Color.Green;
                    constantLine2.Title.TextColor = Color.Green;
                    constantLine2.LineStyle.Thickness = 2;
                    diagram.AxisY.ConstantLines.Add(constantLine2);
                    double Negative1SD = Math.Round((double)(results[0].TargetMean - results[0].TargetSD), 1, MidpointRounding.AwayFromZero);
                    ConstantLine constantLine3 = new ConstantLine("-1SD", Negative1SD);
                    constantLine3.Color = Color.Blue;
                    constantLine3.Title.TextColor = Color.Blue;
                    constantLine3.LineStyle.Thickness = 2;
                    constantLine3.Title.ShowBelowLine = true;
                    diagram.AxisY.ConstantLines.Add(constantLine3);
                    double Negative2SD = Math.Round((double)(results[0].TargetMean - results[0].TargetSD * 2), 1, MidpointRounding.AwayFromZero);
                    ConstantLine constantLine6 = new ConstantLine("-2SD", Negative2SD);
                    constantLine6.Color = Color.Orange;
                    constantLine6.Title.TextColor = Color.Orange;
                    constantLine6.LineStyle.Thickness = 2;
                    constantLine6.Title.ShowBelowLine = true;
                    diagram.AxisY.ConstantLines.Add(constantLine6);
                    double Negative3SD = Math.Round((double)(results[0].TargetMean - results[0].TargetSD * 3), 1, MidpointRounding.AwayFromZero);
                    ConstantLine constantLine7 = new ConstantLine("-3SD", Negative3SD);
                    constantLine7.Color = Color.Red;
                    constantLine7.Title.TextColor = Color.Red;
                    constantLine7.LineStyle.Thickness = 2;
                    constantLine7.Title.ShowBelowLine = true;
                    diagram.AxisY.ConstantLines.Add(constantLine7);
                    //设置Y轴的图像显示最大值和最小值
                    double VRMin = Math.Round((double)(results[0].TargetMean - 3 * results[0].TargetSD - 20), 1, MidpointRounding.AwayFromZero);
                    double VRMax = Math.Round((double)(results[0].TargetMean + 3 * results[0].TargetSD + 20), 1, MidpointRounding.AwayFromZero);
                    diagram.AxisY.VisualRange.SetMinMaxValues(VRMin, VRMax);
                    //设置Y轴的最大值和最小值
                    double WRMin = Math.Round((double)(results[0].TargetMean - 3 * results[0].TargetSD - 20), 1, MidpointRounding.AwayFromZero);
                    double WRMax = Math.Round((double)(results[0].TargetMean + 3 * results[0].TargetSD + 20), 1, MidpointRounding.AwayFromZero);
                    diagram.AxisY.WholeRange.SetMinMaxValues(WRMin, WRMax);

                    //Legend legend = chartControl1.Legend;
                    //legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                    //legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                    //legend.Direction = LegendDirection.LeftToRight;
                    //legend.TextVisible = true;

                    diagram.AxisX.VisualRange.SetMinMaxValues(0, 10);
                    diagram.AxisX.WholeRange.SetMinMaxValues(0, results.Count + 10);

                    //设置Y轴
                    diagram.AxisY.Title.Text = "质控品浓度结果".ToString();
                    diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    diagram.AxisY.NumericScaleOptions.GridSpacing = 1;
                    diagram.AxisY.NumericScaleOptions.AutoGrid = false;
                    diagram.AxisY.VisualRange.Auto = false;
                    diagram.AxisY.VisualRange.AutoSideMargins = false;
                    diagram.AxisY.WholeRange.Auto = false;
                    diagram.AxisY.WholeRange.AutoSideMargins = false;
                    diagram.AxisY.MinorCount = 9;
                    //是否允许沿其Y轴滚动窗格
                    diagram.EnableAxisYScrolling = true;
                    diagram.EnableAxisYZooming = true;

                    //设置X轴
                    diagram.AxisX.Title.Text = "质控时间".ToString();
                    diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 1;
                    diagram.AxisX.NumericScaleOptions.AutoGrid = false;
                    diagram.AxisX.VisualRange.Auto = false;
                    diagram.AxisX.VisualRange.AutoSideMargins = false;
                    //diagram.AxisX.WholeRange.Auto = false;
                    //diagram.AxisX.WholeRange.AutoSideMargins = false;
                    diagram.AxisX.MinorCount = 9;
                    //是否允许沿其X轴滚动窗格
                    diagram.EnableAxisXScrolling = true;
                    diagram.EnableAxisXZooming = true;

                    // 启用X轴缩放
                    //diagram.EnableAxisXZooming = true;
                    //diagram.Panes[0].EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
                    //// 指定键盘和鼠标进行放大缩小
                    diagram.ZoomingOptions.UseKeyboard = false;
                    diagram.ZoomingOptions.UseKeyboardWithMouse = true;
                    diagram.ZoomingOptions.UseMouseWheel = true;

                }
            }
            catch (Exception e)
            {

            }
        }
    }
}