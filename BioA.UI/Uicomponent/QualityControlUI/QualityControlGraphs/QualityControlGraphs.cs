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
using BioA.Service;

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
        /// <summary>
        /// 保存质控结果信息
        /// </summary>
        List<QCResultForUIInfo> results;
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> qcGraphsDic = new Dictionary<string, object[]>();

        //结果设置表信息
        private List<ResultSetInfo> lstResultSetInfo;

        public QualityControlGraphs()
        {
            InitializeComponent();
            
            //checkedListBox1.SetItemChecked(0,true);
            //checkedListBox1.SetItemChecked(1, true);
            //checkedListBox1.SetItemChecked(2, true);
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }
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
                ResultSetInfo ss = lstResultSetInfo.SingleOrDefault(v => v.ProjectName == results[i].ProjectName) as ResultSetInfo;
                obj[i + 1] = Math.Round(results[i].ConcResult, ss != null ? ss.RadixPointNum : 4, MidpointRounding.AwayFromZero);
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
            //AccumulationTimeSeries = new Series(caption, viewType);
            //for (int i = 1; i < dt.Columns.Count; i++)
            //{
                string argument = dt.Columns[index].ColumnName;//参数名称
                string value = (string)dt.Rows[0][index];//参数值
                if (value != string.Empty)
                {
                    AccumulationTimeSeries.Points.Add(new SeriesPoint(argument, value));
                }
            //}

            //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
            ////也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
            //AccumulationTimeSeries.ArgumentScaleType = ScaleType.Qualitative;
            //AccumulationTimeSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签

            return AccumulationTimeSeries;
        }
        private void CreateChart(DataTable dt)
        {
            #region Series
            //创建几个图形的对象
            AccumulationTimeSeries.Points.Clear();
            Series series1;
            Series series2;
            Series series3;
            Series series4;
            List<Series> list = new List<Series>();
                for (int i = 0; i < results.Count; i++)
                {
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
                    else if ((str < (results[i].TargetMean + results[i].TargetSD * 2) && str >= results[i].TargetMean + results[i].TargetSD) ||
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
                    else if ((str < (results[i].TargetMean + results[i].TargetSD * 3) && str >= results[i].TargetMean + results[i].TargetSD * 2) ||
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
                    else if (str > (results[i].TargetMean + results[i].TargetSD * 3) ||
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
            
            #endregion
            chartControl1.Series.AddRange(list.ToArray());
            this.Invoke(new EventHandler(delegate { 
                this.btnSearch.Enabled = true;
            }));
        }

      

          
        private void MyPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("XXX医院质控报告模版", new Font(new FontFamily("黑体"), 18), System.Drawing.Brushes.Black, 500, 10);

            e.Graphics.DrawString(string.Format("报告日期：" + DateTime.Now.ToShortDateString()), new Font(new FontFamily("新宋体"), 10), System.Drawing.Brushes.Black, 960, 50);
            //信息的名称
            e.Graphics.DrawLine(Pens.Black, 8, 70, 1161, 70);
            e.Graphics.DrawString("项目：" + cboProjectName.Text, new Font(new FontFamily("新宋体"), 10), System.Drawing.Brushes.Black, 15, 80);
            e.Graphics.DrawString("水平：" + txtHorizontalValue.Text, new Font(new FontFamily("新宋体"), 10), System.Drawing.Brushes.Black, 145, 80);

            e.Graphics.DrawString("质控日期：" + dtpStartTime.Value.ToShortDateString() + "--" + dtpEndTime.Value.ToShortDateString(), 
                new Font(new FontFamily("新宋体"), 10), System.Drawing.Brushes.Black, 280, 80);
                
            e.Graphics.DrawString("质控品：" + cboQCName.Text , new Font(new FontFamily("新宋体"), 10), System.Drawing.Brushes.Black, 15, 105);
            e.Graphics.DrawString(string.Format("靶值：{0}", this.txtTargetValue.Text), new Font(new FontFamily("新宋体"), 10), System.Drawing.Brushes.Black, 205,105);
            e.Graphics.DrawString(string.Format("标准差：{0}", this.txtStandardDeviationValue.Text), new Font(new FontFamily("新宋体"), 10), System.Drawing.Brushes.Black, 305, 105);
            //e.Graphics.DrawLine(Pens.Black, 8, 105, 1161, 105);
            try
            {
                //信息
                Image image = Image.FromFile(@"D:\屏幕截图\" + fileName);
                e.Graphics.DrawImage(image, new RectangleF(30, 130, 1350, 700));
            }
            catch (Exception ex)
            {
                LogInfo.WriteProcessLog("MyPrintDocument_PrintPage(object sender, PrintPageEventArgs e) == " + ex.ToString(), Module.QualityControl);
            }
            if (results != null)
            {
                List<float> lstFloat = new List<float>();
                foreach (var item in results)
                {
                    lstFloat.Add(item.ConcResult);
                }
                StatValue sd = StatDatas.GetStateValue(lstFloat);
                e.Graphics.DrawString(string.Format("统计量：" + sd.N.ToString() + "      " +
                    "平均值：" + sd.MEAN.ToString("#0.00") + "      " +
                    "标准差：" + sd.SD.ToString("#0.00") + "      " +
                    "CV%：" + sd.CV.ToString("#0.00%") + "      " +
                    "极差：" + sd.R.ToString("#0.00")),
                    new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 30, 660);
            }
            //e.Graphics.DrawString(string.Format("靶值：{0}", this.txtTargetValue.Text), new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 30, 600);
            //e.Graphics.DrawString(string.Format("标准差：{0}", this.txtStandardDeviationValue.Text), new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 135, 600);
            e.Graphics.DrawLine(Pens.Black, 8, 700, 1161, 700);
            e.Graphics.DrawString(string.Format("制作人：{0}", Program.userInfo == null ? "" : Program.userInfo.UserName), new Font(new FontFamily("黑体"), 12), System.Drawing.Brushes.Black, 900, 710);
            //e.Graphics.DrawString(mean, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 10, 798);
            //e.Graphics.DrawString(sd, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 95, 798);

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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {

            this.Invoke(new EventHandler(delegate { chartControl1.Series.Clear(); }));
            QCResultForUIInfo qcResForUIInfo = new QCResultForUIInfo();
            if (cboProjectName.Text != "")
                qcResForUIInfo.ProjectName = cboProjectName.SelectedItem.ToString();
            else
            {
                MessageBox.Show("项目名称不能为空！");
                return;
            }

            if (cboQCName.Text != "")
                qcResForUIInfo.QCName = cboQCName.SelectedItem.ToString();
            else
            {
                MessageBox.Show("请选择质控品名！");
                return;
            }
            this.Invoke(new EventHandler(delegate
            {
                this.btnSearch.Enabled = false;
            }));
            
            qcResForUIInfo.QCTimeStartTS = System.Convert.ToDateTime((dtpStartTime.Value).ToShortDateString());
            qcResForUIInfo.QCTimeEndTS = System.Convert.ToDateTime((dtpEndTime.Value).AddDays(1).ToShortDateString());
            if (results != null)
            {
                results = null;
            }
            //qcGraphsDic.Clear();
            //qcGraphsDic.Add("QueryQCResultForQCGraphics", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), qcResForUIInfo) });
            //ClientSendInfoToServices(qcGraphsDic);
            results = new QCGraphics().QueryQCResultForQCGraphics("QueryQCResultForQCGraphics", qcResForUIInfo);
            QualityControlPicture(results);
        }

        /// <summary>
        /// 打印报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


            this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 827, 1169);
            printDocument1.DefaultPageSettings.Landscape = true;    //横向打印

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
            this.lstResultSetInfo = QueryResultSetTb.QueryResultSetInfo;
        }
        private void loadQCGraphsInfoLoad()
        {
            //获取项目名称
            qcGraphsDic.Add("QueryProjectName",null);
            //获取质控信息
            qcGraphsDic.Add("QueryQCAllInfo",null);
            //获取控制项目信息
            qcGraphsDic.Add("GetsQCRelationProInfo", null);
            ClientSendInfoToServices(qcGraphsDic);
        }

        private void ClientSendInfoToServices(Dictionary<string, object[]> sender)
        {
            var sendToServicesInfoThread = new Thread(() =>
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.QCGraphic, sender)
            )
            { IsBackground = true };
            sendToServicesInfoThread.Start();
        }

        /// <summary>
        /// 所有质控项目信息
        /// </summary>
        List<QCRelationProjectInfo> lstQCRelationProjects = null;
        /// <summary>
        /// 所有质控信息
        /// </summary>
        List<QualityControlInfo> lstQCInfo = null;
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
                    lstQCInfo = (List<QualityControlInfo>)XmlUtility.Deserialize(typeof(List<QualityControlInfo>), sender as string);
                    break;
                case "GetsQCRelationProInfo":
                     lstQCRelationProjects = (List<QCRelationProjectInfo>)XmlUtility.Deserialize(typeof(List<QCRelationProjectInfo>), sender as string);
                    break;
                case "QueryQCResultForQCGraphics":
                    // 包含信息有水平浓度、质控时间、质控结果浓度
                    results = (List<QCResultForUIInfo>)XmlUtility.Deserialize(typeof(List<QCResultForUIInfo>), sender as string);
                    QualityControlPicture(results);
                    break;
                default:
                    break;
            }
        }

        private void QualityControlPicture(List<QCResultForUIInfo> lstQCResultForUIs)
        {
            if (lstQCResultForUIs.Count > 0)
            {
                var seriesThread = new Thread(() =>
                {
                    DataTable dtHigh = CreateData(lstQCResultForUIs);
                    CreateChart(dtHigh);
                })
                { IsBackground = true };
                seriesThread.Start();
            }
            else
            {
                if(this._TemporaryQCProjectInfo != null)
                {
                    Dline(_TemporaryQCProjectInfo);
                }
                string str = string.Format("该质控项目在[{0} ~ {1}]时间段中没有数据", dtpStartTime.Value.ToShortDateString(), dtpEndTime.Value.AddDays(1).ToShortDateString());
                this.Invoke(new EventHandler(delegate
                {
                    this.btnSearch.Enabled = true;
                }));
                MessageBox.Show(str);
            }

        }
        private QCRelationProjectInfo _TemporaryQCProjectInfo;
        //初始化视图
        private Series AccumulationTimeSeries;
        /// <summary>
        /// 画SD图形
        /// </summary>
        /// <param name="results"></param>
        private void Dline(QCRelationProjectInfo results)
        {
            try
            {
                if (results != null)
                {
                    if(AccumulationTimeSeries == null)
                    {
                        AccumulationTimeSeries = new Series("QCSD", ViewType.Line);
                        AccumulationTimeSeries.ArgumentScaleType = ScaleType.Qualitative;
                        AccumulationTimeSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//是否显示
                        chartControl1.Series.Add(AccumulationTimeSeries);
                    }
                    else
                    {
                        chartControl1.Series.Clear();
                        AccumulationTimeSeries = null;
                        AccumulationTimeSeries = new Series("QCSD", ViewType.Line);
                        AccumulationTimeSeries.ArgumentScaleType = ScaleType.Qualitative;
                        AccumulationTimeSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//是否显示
                        chartControl1.Series.Add(AccumulationTimeSeries);
                    }
                    this._TemporaryQCProjectInfo = results;
                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    if (diagram != null)
                    {
                        diagram.AxisY.ConstantLines.Clear();
                        chartControl1.Titles.Clear();
                        ChartTitle SD1title = new ChartTitle();
                        SD1title.Text = "● Mean/±1SD — 正常！" + "     ● ±2SD警告！" + "     ● ±3SD错误！";
                        SD1title.TextColor = Color.OrangeRed;
                        SD1title.Font = new System.Drawing.Font("Tahoma", (float)10);
                        chartControl1.Titles.Add(SD1title);
                        
                        double StandardDeviation1 = Math.Round((double)(results.TargetMean + results.TargetSD), 1, MidpointRounding.AwayFromZero);
                        ConstantLine constantLine1 = new ConstantLine("1SD", StandardDeviation1);
                        diagram.AxisY.ConstantLines.Add(constantLine1);
                        constantLine1.Color = Color.Blue; //直线颜色
                        constantLine1.Title.TextColor = Color.Blue;   //直线文本字体颜色
                        constantLine1.LineStyle.Thickness = 2;
                        double StandardDeviation2 = Math.Round((double)(results.TargetMean + results.TargetSD * 2), 1, MidpointRounding.AwayFromZero);
                        ConstantLine constantLine4 = new ConstantLine("2SD", StandardDeviation2);
                        diagram.AxisY.ConstantLines.Add(constantLine4);
                        constantLine4.Color = Color.Orange; //直线颜色
                        constantLine4.Title.TextColor = Color.Orange;   //直线文本字体颜色
                        constantLine4.LineStyle.Thickness = 2;
                        double StandardDeviation3 = Math.Round((double)(results.TargetMean + results.TargetSD * 3), 1, MidpointRounding.AwayFromZero);
                        ConstantLine constantLine5 = new ConstantLine("3SD", StandardDeviation3);
                        diagram.AxisY.ConstantLines.Add(constantLine5);
                        constantLine5.Color = Color.Red; //直线颜色  
                        constantLine5.Title.TextColor = Color.Red;   //直线文本字体颜色
                        constantLine5.LineStyle.Thickness = 2;
                        double Mean = Math.Round(results.TargetMean, 1, MidpointRounding.AwayFromZero);
                        ConstantLine constantLine2 = new ConstantLine("MEAN", Mean);
                        diagram.AxisY.ConstantLines.Add(constantLine2);
                        constantLine2.Color = Color.Green;
                        constantLine2.Title.TextColor = Color.Green;
                        constantLine2.LineStyle.Thickness = 2;
                        double Negative1SD = Math.Round((double)(results.TargetMean - results.TargetSD), 1, MidpointRounding.AwayFromZero);
                        ConstantLine constantLine3 = new ConstantLine("-1SD", Negative1SD);
                        diagram.AxisY.ConstantLines.Add(constantLine3);
                        constantLine3.Color = Color.Blue;
                        constantLine3.Title.TextColor = Color.Blue;
                        constantLine3.LineStyle.Thickness = 2;
                        constantLine3.Title.ShowBelowLine = true;
                        double Negative2SD = Math.Round((double)(results.TargetMean - results.TargetSD * 2), 1, MidpointRounding.AwayFromZero);
                        ConstantLine constantLine6 = new ConstantLine("-2SD", Negative2SD);
                        diagram.AxisY.ConstantLines.Add(constantLine6);
                        constantLine6.Color = Color.Orange;
                        constantLine6.Title.TextColor = Color.Orange;
                        constantLine6.LineStyle.Thickness = 2;
                        constantLine6.Title.ShowBelowLine = true;
                        double Negative3SD = Math.Round((double)(results.TargetMean - results.TargetSD * 3), 1, MidpointRounding.AwayFromZero);
                        ConstantLine constantLine7 = new ConstantLine("-3SD", Negative3SD);
                        diagram.AxisY.ConstantLines.Add(constantLine7);
                        constantLine7.Color = Color.Red;
                        constantLine7.Title.TextColor = Color.Red;
                        constantLine7.LineStyle.Thickness = 2;
                        constantLine7.Title.ShowBelowLine = true;
                        //设置Y轴的图像显示最大值和最小值
                        double VRMin = 0;
                        double VRMax = 0;
                        if (results.TargetSD <= 0.1)
                        {
                            VRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 0.5), 1, MidpointRounding.AwayFromZero);
                            VRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 0.5), 1, MidpointRounding.AwayFromZero);
                        }
                        else if (results.TargetSD <= 0.3)
                        {
                            VRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 2), 1, MidpointRounding.AwayFromZero);
                            VRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 2), 1, MidpointRounding.AwayFromZero);
                        }
                        else if (results.TargetSD <= 0.5)
                        {
                            VRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 3), 1, MidpointRounding.AwayFromZero);
                            VRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 3), 1, MidpointRounding.AwayFromZero);
                        }
                        else if (results.TargetSD <= 1)
                        {
                            VRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 5), 1, MidpointRounding.AwayFromZero);
                            VRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 5), 1, MidpointRounding.AwayFromZero);
                        }
                        else if (results.TargetSD <= 5)
                        {
                            VRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 15), 1, MidpointRounding.AwayFromZero);
                            VRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 15), 1, MidpointRounding.AwayFromZero);
                        }
                        else if (results.TargetSD <= 10)
                        {
                            VRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 25), 1, MidpointRounding.AwayFromZero);
                            VRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 25), 1, MidpointRounding.AwayFromZero);
                        }
                        else if (results.TargetSD <= 20)
                        {
                            VRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 40), 1, MidpointRounding.AwayFromZero);
                            VRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 40), 1, MidpointRounding.AwayFromZero);
                        }
                        else if (results.TargetSD <= 50)
                        {
                            VRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 60), 1, MidpointRounding.AwayFromZero);
                            VRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 60), 1, MidpointRounding.AwayFromZero);
                        }
                        else if (results.TargetSD <= 100)
                        {
                            VRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 75), 1, MidpointRounding.AwayFromZero);
                            VRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 75), 1, MidpointRounding.AwayFromZero);
                        }
                        diagram.AxisY.VisualRange.SetMinMaxValues(VRMin, VRMax);
                        //设置Y轴的最大值和最小值
                        double WRMin = Math.Round((double)(results.TargetMean - 3 * results.TargetSD - 100), 1, MidpointRounding.AwayFromZero);
                        double WRMax = Math.Round((double)(results.TargetMean + 3 * results.TargetSD + 100), 1, MidpointRounding.AwayFromZero);
                        diagram.AxisY.WholeRange.SetMinMaxValues(WRMin, WRMax);

                        diagram.AxisX.VisualRange.SetMinMaxValues(0, 10);
                        diagram.AxisX.WholeRange.SetMinMaxValues(0, 10);

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
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("质控图异常："+ e.ToString(),Module.QualityControl);
            }
        }
        /// <summary>
        /// 项目名称下拉框：值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> qcId = new List<int>();
            foreach (var item in lstQCRelationProjects)
            {
                if (item.ProjectName == cboProjectName.Text)
                {
                    qcId.Add(item.QCID);
                }
            }
            if (qcId != null && qcId.Count > 0)
            {
                cboQCName.Properties.Items.Clear();
                cboQCName.SelectedIndex = -1;
                foreach (var qcInfo in lstQCInfo)
                {
                    if (qcId.Exists(x => x == qcInfo.QCID))
                    {
                        cboQCName.Properties.Items.Add(qcInfo.QCName);
                    }
                }
                cboQCName.SelectedIndex = 0;
            }
            else
            {
                cboQCName.Properties.Items.Clear();
                cboQCName.Text = "";
                txtHorizontalValue.Text = "";
                txtStandardDeviationValue.Text = "";
                txtTargetValue.Text = "";
            }
            
        }
        /// <summary>
        /// 质控品名称下拉框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboQCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var qualityControls = lstQCInfo.Find(x => x.QCName == cboQCName.Text);
            QCRelationProjectInfo qcRelationProject = lstQCRelationProjects.Find(x => x.QCID == qualityControls.QCID && x.ProjectName == cboProjectName.Text);
            if (qcRelationProject != null)
            {
                txtTargetValue.Text = Convert.ToString(qcRelationProject.TargetMean);
                txtStandardDeviationValue.Text = Convert.ToString(qcRelationProject.TargetSD);
                txtHorizontalValue.Text = qualityControls.HorizonLevel;
                this.Dline(qcRelationProject);
                
            }
        }
    }
}