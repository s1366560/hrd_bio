using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.Common;
using BioA.UI.Uicomponent.SystemUI.Configure;

namespace ReagentDataMaking
{
    public partial class ReagentDataMaking : Form
    {
        public ReagentDataMaking()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.OpenExcelFile();
        }


        string ExccelFile = null;
        /// <summary>
        /// 打开文所在位置
        /// </summary>
        void OpenExcelFile()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".xlsx";
            ofd.Filter = "xlsx file|*.xlsx";
            if (ofd.ShowDialog() == true)
            {
                this.ExccelFile = ofd.FileName;
                new Thread(new ThreadStart(FileService)).Start();

            }
        }
        /// <summary>
        /// 创建数据文件
        /// </summary>
        void FileService()
        {
            this.Invoke(new EventHandler(delegate { 
                DataSet dataset = this.ReadEexcelFile(this.ExccelFile);
                this.CreateDataFile(dataset);
            }));
        }

        /// <summary>
        /// 读取Eexcel文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        DataSet ReadEexcelFile(string fileName)
        {
            this.DisplayDataDakingTip = new List<string>();
            this.DisplayDataDakingTip.Add("开始读取Eexcel文件数据....\r\n");

            string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + fileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1'";

            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConn);

            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM[Sheet1$]", strConn);
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet);
            }
            catch (Exception ex)
            {
                this.DisplayDataDakingTip.Add(ex.Message);
                //throw new ArgumentNullException("该Excel文件的工作表的名字不正确," + ex.Message);
            }

            return myDataSet;
        }
        /// <summary>
        /// 制作数据
        /// </summary>
        /// <param name="dataset"></param>
        void CreateDataFile(DataSet dataset)
        {
            if (dataset == null)
            {
                this.DisplayDataDakingTip.Add("数据对象为空\r\n");
            }

            string file = this.ExccelFile + ".data";
            if (File.Exists(file))
            {
                File.Delete(file);

                this.DisplayDataDakingTip.Add("删除.data文件...\r\n");
            }
            this.DisplayDataDakingTip.Add("生产.data文件...\r\n");

            this.WriteDataFile("delete from ReagentItemTb");

            string str1 = "insert ReagentItemTb(Code,ItemName,LongName,Unit,RadixPointNum,AnalyzeMethod,MainWaveLength,SubWaveLength,FirstPointS,FirstPointE,SecondPointS,SecondPointE,IncreaseVol,NormalVol,DecreaseVol,SDTVol,R1Vol,R2Vol,DoCount,SDTCount,QCSpace,ReacteDirect,LineSerumLimitMin,LineSerumLimitMax,ReagentAbsMin,ReagentAbsMax,SerumPanicLimitMin,SerumPanicLimitMax,Stiring1Force,Stiring2Force) values";
            if (dataset != null && dataset.Tables.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    string v = null;
                    for (int i = 0; i < row.ItemArray.Count(); i++)
                    {
                        if (row[i] == null)
                        {
                            v += "'',";
                        }
                        else
                        {
                            v += string.Format("'{0}',", row[i].ToString().Trim());
                            if (i == 0)
                            {
                                this.DisplayDataDakingTip.Add(row[i].ToString().Trim() + "生产完成. \r\n");
                            }
                        }
                    }
                    v = v.TrimEnd(',');
                    v = "(" + v + ")";

                    string d = str1 + v;

                    d = EncryptionText.EncryptDES(d, KeyManager.DataKey);
                    
                    this.WriteDataFile(d);
                }
            }

            this.DisplayDataDakingTip.Add("完成生产文件. ");
            this.memoEdit1.Lines = DisplayDataDakingTip.ToArray();
        }

        private List<string> DisplayDataDakingTip = null;
        /// <summary>
        /// 把数据写入到.data文件中
        /// </summary>
        /// <param name="info"></param>
        public void WriteDataFile(string info)
        {
            try
            {
                string FileName = this.ExccelFile + ".data";
                StreamWriter sw = new StreamWriter(FileName, true, Encoding.Unicode);

                sw.Write(info);
                sw.Write("\r\n");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("the datas is saving abnormally! exception:" + e.Message);
            }
        }
    }
}
