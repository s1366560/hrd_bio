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

namespace BioA.UI.Uicomponent.QualityControlUI.QualityControlProfile
{
    public partial class QualityControlProfile : DevExpress.XtraEditors.XtraUserControl
    {
        private PrintDocument printDocument1 = new PrintDocument();
        PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        string fileName;
        public QualityControlProfile()
        {
            InitializeComponent();
        }

        private object[] Saveobj2(object[] obj)
        {
            object[] obj2 = new object[obj.Length];
            for (int i = 0; i < obj.Length; i++)
            {
                try
                {
                    if (Convert.ToInt32(obj[i]) >= 3 || Convert.ToInt32(obj[i]) <= -3)
                    {
                        obj2[i] = obj[i];


                    }
                    else if (Convert.ToInt32(obj[i]) >= 2 && Convert.ToInt32(obj[i - 1]) >= 2 && Convert.ToInt32(obj[i]) < 3 && Convert.ToInt32(obj[i - 1]) < 3
                        || Convert.ToInt32(obj[i]) <= -2 && Convert.ToInt32(obj[i - 1]) <= -2 && Convert.ToInt32(obj[i]) > -3 && Convert.ToInt32(obj[i - 1]) > -3)
                    {
                        obj2[i] = obj[i];

                    }
                    else if (Math.Abs(Convert.ToInt32(obj[i]) - Convert.ToInt32(obj[i - 1])) >= 4 && Convert.ToInt32(obj[i - 1]) < 3 && Convert.ToInt32(obj[i - 1]) > -3
                        && Convert.ToInt32(obj[i]) > -3 && Convert.ToInt32(obj[i]) < 3)
                    {
                        obj2[i] = obj[i];
                    }
                    else if (Convert.ToInt32(obj[i]) >= 1 && Convert.ToInt32(obj[i - 1]) >= 1 && Convert.ToInt32(obj[i - 2]) >= 1 && Convert.ToInt32(obj[i - 3]) >= 1
                        && Convert.ToInt32(obj[i]) < 3 && Convert.ToInt32(obj[i - 1]) < 3 && Convert.ToInt32(obj[i - 2]) < 3 && Convert.ToInt32(obj[i - 3]) < 3
                        || Convert.ToInt32(obj[i]) <= -1 && Convert.ToInt32(obj[i - 1]) <= -1 && Convert.ToInt32(obj[i - 2]) <= -1 && Convert.ToInt32(obj[i - 3]) <= -1
                         && Convert.ToInt32(obj[i]) > -3 && Convert.ToInt32(obj[i - 1]) > -3 && Convert.ToInt32(obj[i - 2]) > -3 && Convert.ToInt32(obj[i - 3]) > -3)
                    {
                        obj2[i] = obj[i];
                    }
                    else if (Convert.ToInt32(obj[i]) > 0 && Convert.ToInt32(obj[i - 1]) > 0 && Convert.ToInt32(obj[i - 2]) > 0 && Convert.ToInt32(obj[i - 3]) > 0 &&
                        Convert.ToInt32(obj[i - 4]) > 0 && Convert.ToInt32(obj[i - 5]) > 0 && Convert.ToInt32(obj[i - 6]) > 0 && Convert.ToInt32(obj[i - 7]) > 0 &&
                        Convert.ToInt32(obj[i - 8]) > 0 && Convert.ToInt32(obj[i - 9]) > 0 &&
                        (Convert.ToInt32(obj[i]) < 3 && Convert.ToInt32(obj[i - 1]) < 3 && Convert.ToInt32(obj[i - 2]) < 3 && Convert.ToInt32(obj[i - 3]) < 3 &&
                        Convert.ToInt32(obj[i - 4]) < 3 && Convert.ToInt32(obj[i - 5]) < 3 && Convert.ToInt32(obj[i - 6]) < 3 && Convert.ToInt32(obj[i - 7]) < 3 &&
                        Convert.ToInt32(obj[i - 8]) < 3 && Convert.ToInt32(obj[i - 9]) < 3
                       || Convert.ToInt32(obj[i]) < 0 && Convert.ToInt32(obj[i - 1]) < 0 && Convert.ToInt32(obj[i - 2]) < 0 && Convert.ToInt32(obj[i - 3]) < 0
                        && Convert.ToInt32(obj[i - 4]) < 0 && Convert.ToInt32(obj[i - 5]) < 0 && Convert.ToInt32(obj[i - 6]) < 0 && Convert.ToInt32(obj[i - 7]) < 0
                        && Convert.ToInt32(obj[i - 8]) < 0 && Convert.ToInt32(obj[i - 9]) < 0))
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

        private DataTable CreateData()
        {
            List<DateTime> datetime = data(QCTime1.Value, QCTime2.Value);
            DataTable dt = new DataTable();
            DataColumn[] dtc = new DataColumn[datetime.Count + 1];
            dtc[0] = new DataColumn("日期");


            for (int i = 0; i < datetime.Count; i++)
            {
                dtc[i + 1] = new DataColumn(datetime[i].ToString(), typeof(string));
            }


            dt.Columns.AddRange(dtc);
            object[] obj = new object[datetime.Count + 1];
            object[] obj2 = new object[datetime.Count + 1];
            obj[0] = "SD";
            obj2[0] = "XSD";
            Random rd = new Random();
            Random rd2 = new Random();
            for (int i = 1; i < datetime.Count + 1; i++)
            {
                int aa = rd.Next(-3, 4);
                int bb = rd.Next(-3, 4);
                obj[i] = aa;

            }

            obj2 = Saveobj2(obj);


            dt.Rows.Add(obj);
            dt.Rows.Add(obj2);

            return dt;
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
        private void CreateChart(DataTable dt)
        {
            #region Series
            //创建几个图形的对象
            Series series1 = CreateSeries("SD", ViewType.Line, dt, 0);
            Series series2 = CreateSeries("XSD", ViewType.Point, dt, 1);

            PointSeriesView pointSeriesView1 = new PointSeriesView();
            pointSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            pointSeriesView1.PointMarkerOptions.Size = 15;
            series2.View = pointSeriesView1;

            //LineSeriesView lineSeriesView1 = new LineSeriesView();
            //lineSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            //lineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            //lineSeriesView1.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Circle;
            //lineSeriesView1.LineMarkerOptions.Size = 15;
            //lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            //series1.View = lineSeriesView1;

            #endregion

            List<Series> list = new List<Series>() { series1, series2 };
            QCchartControl.Series.AddRange(list.ToArray());

        }

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = CreateData();
        //    // this.gridControl1.DataSource = dt;

        //    CreateChart(dt);
        //}

        //private void simpleButton2_Click(object sender, EventArgs e)
        //{
        //    if (Directory.Exists(" d:\\屏幕截图"))  //判断目录是否存在,不存在就创建
        //    { }
        //    else
        //    {
        //        DirectoryInfo directoryInfo = new DirectoryInfo(" d:\\屏幕截图");
        //        directoryInfo.Create();
        //    }
        //    //创建图片对象
        //    Bitmap bmp2 = new Bitmap(Screen.PrimaryScreen.Bounds.Width - 160, Screen.PrimaryScreen.Bounds.Height - 110);
        //    Graphics g2 = Graphics.FromImage(bmp2);  //创建画笔
        //    g2.CopyFromScreen(new Point(0, 0), new Point(-350, -330), bmp2.Size);//截屏
        //    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");//获得系统时间
        //    time = System.Text.RegularExpressions.Regex.Replace(time, @"[^0-9]+", "");//提取数字
        //    fileName = time + ".bmp"; //创建文件名
        //    bmp2.Save("d:\\屏幕截图\\" + fileName); //保存为文件  ,注意格式是否正确.
        //    bmp2.Dispose();//关闭对象
        //    g2.Dispose();//关闭画笔

        //    //   Series s = new Series();
        //    // chartControl1.SaveToFile(@"D:\abc");


        //    //s.SaveImage("d:\\dd.Bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        //    // printDocument1 为 打印控件
        //    //设置打印用的纸张 当设置为Custom的时候，可以自定义纸张的大小，还可以选择A4,A5等常用纸型
        //    PaperSize paperSize = new PaperSize();
        //    paperSize.PaperName = "Custum";


        //    this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 500, 600);
        //    this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
        //    //将写好的格式给打印预览控件以便预览
        //    printPreviewDialog1.Document = printDocument1;
        //    //显示打印预览
        //    DialogResult result = printPreviewDialog1.ShowDialog();
        //    //if (result == DialogResult.OK)
        //    //this.MyPrintDocument.Print();
        //}
        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        private void MyPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("XXX医院质控报告模版", new Font(new FontFamily("黑体"), 11), System.Drawing.Brushes.Black, 170, 10);

            //信息的名称
            e.Graphics.DrawLine(Pens.Black, 8, 30, 480, 30);
            e.Graphics.DrawString("项目：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 10, 35);
            e.Graphics.DrawString("总蛋白", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 40, 35);
            e.Graphics.DrawString("水平：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 95, 35);
            e.Graphics.DrawString("二", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 125, 35);

            e.Graphics.DrawString("报告日期：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 280, 35);
            e.Graphics.DrawString("2017/10/23", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 335, 35);
            e.Graphics.DrawString("-", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 397, 35);
            e.Graphics.DrawString("2017/10/24", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 405, 35);
            //e.Graphics.DrawString("总金额", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 400, 35);
            e.Graphics.DrawLine(Pens.Black, 8, 50, 480, 50);

            //信息

            //Bitmap bitmap = new Bitmap("c:\\微信截图_20171024141141.png");
            //e.Graphics.DrawImage(bitmap, e.MarginBounds.Left, e.MarginBounds.Top);
            //Image i = (Image)stream;
            Image i = Image.FromFile(@"D:\屏幕截图\" + fileName);

            Bitmap bit = new Bitmap(i);
            Image ig = resizeImage(bit, new Size(560, 480));
            Bitmap bit2 = new Bitmap(ig);
            bit2.Save("d:\\" + fileName);

            e.Graphics.DrawImage(ig, 12, 60);

            e.Graphics.DrawLine(Pens.Black, 8, 570, 480, 570);
            e.Graphics.DrawString("MEAN：1.232", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 10, 575);
            e.Graphics.DrawString("SD：1.663", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 95, 575);
        }

        private List<DateTime> data(DateTime beginDate, DateTime endDate)
        {

            List<DateTime> dateTime = new List<DateTime>();
            for (DateTime dt = beginDate; dt < endDate; dt = dt.AddDays(1))
            {
                dateTime.Add(dt);
            }

            return dateTime;
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            DataTable dt = CreateData();
            // this.gridControl1.DataSource = dt;

            CreateChart(dt);
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
            Bitmap bmp2 = new Bitmap(Screen.PrimaryScreen.Bounds.Width - 160, Screen.PrimaryScreen.Bounds.Height - 110);
            Graphics g2 = Graphics.FromImage(bmp2);  //创建画笔
            g2.CopyFromScreen(new Point(0, 0), new Point(-350, -330), bmp2.Size);//截屏
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");//获得系统时间
            time = System.Text.RegularExpressions.Regex.Replace(time, @"[^0-9]+", "");//提取数字
            fileName = time + ".bmp"; //创建文件名
            bmp2.Save("d:\\屏幕截图\\" + fileName); //保存为文件  ,注意格式是否正确.
            bmp2.Dispose();//关闭对象
            g2.Dispose();//关闭画笔

            //   Series s = new Series();
            // chartControl1.SaveToFile(@"D:\abc");


            //s.SaveImage("d:\\dd.Bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            // printDocument1 为 打印控件
            //设置打印用的纸张 当设置为Custom的时候，可以自定义纸张的大小，还可以选择A4,A5等常用纸型
            PaperSize paperSize = new PaperSize();
            paperSize.PaperName = "Custum";


            this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 500, 600);
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            //显示打印预览
            DialogResult result = printPreviewDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //this.MyPrintDocument.Print();
        }
    }
}
