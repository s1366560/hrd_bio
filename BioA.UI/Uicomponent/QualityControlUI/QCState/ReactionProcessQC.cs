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
using System.Threading;

namespace BioA.UI
{
    public partial class ReactionProcessQC : DevExpress.XtraEditors.XtraForm
    {
        public ReactionProcessQC()
        {
            InitializeComponent();
            this.ControlBox = false;
            cboMeasurePoint.Properties.Items.AddRange(RunConfigureUtility.ReactionPoints);
        }
        private QCResultForUIInfo qCResInfo = new QCResultForUIInfo();

        public QCResultForUIInfo QCResInfo
        {
            get { return qCResInfo; }
            set
            {
                qCResInfo = value;
            }
        }

        private string strDateTime;
        public string StrDateTime
        {
            get { return strDateTime; }
            set { strDateTime = value; }
        }

        private TimeCourseInfo qCReactionInfo = new TimeCourseInfo();

        public TimeCourseInfo QCReactionInfo
        {
            get { return qCReactionInfo; }
            set
            {
                qCReactionInfo = value;
                //chartQCReaction.Series.Clear();
                //chartQCReaction.Series.Add(new );
                if (this.IsHandleCreated)
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        if (qCReactionInfo != null)
                        {
                            Series series = new Series("ReactionLine", ViewType.Line);
                            series.ArgumentScaleType = ScaleType.Qualitative;
                            //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
                            series.Points.Add(new SeriesPoint(1, (qCReactionInfo.Cuv1Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv1Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(2, (qCReactionInfo.Cuv2Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv2Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(3, (qCReactionInfo.Cuv3Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv3Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(4, (qCReactionInfo.Cuv4Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv4Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(5, (qCReactionInfo.Cuv5Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv5Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(6, (qCReactionInfo.Cuv6Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv6Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(7, (qCReactionInfo.Cuv7Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv7Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(8, (qCReactionInfo.Cuv8Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv8Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(9, (qCReactionInfo.Cuv9Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv9Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(10, (qCReactionInfo.Cuv10Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv10Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(11, (qCReactionInfo.Cuv11Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv11Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(12, (qCReactionInfo.Cuv12Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv12Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(13, (qCReactionInfo.Cuv13Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv13Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(14, (qCReactionInfo.Cuv14Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv14Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(15, (qCReactionInfo.Cuv15Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv15Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(16, (qCReactionInfo.Cuv16Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv16Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(17, (qCReactionInfo.Cuv17Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv17Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(18, (qCReactionInfo.Cuv18Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv18Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(19, (qCReactionInfo.Cuv19Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv19Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(20, (qCReactionInfo.Cuv20Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv20Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(21, (qCReactionInfo.Cuv21Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv21Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(22, (qCReactionInfo.Cuv22Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv22Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(23, (qCReactionInfo.Cuv23Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv23Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(24, (qCReactionInfo.Cuv24Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv24Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(25, (qCReactionInfo.Cuv25Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv25Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(26, (qCReactionInfo.Cuv26Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv26Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(27, (qCReactionInfo.Cuv27Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv27Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(28, (qCReactionInfo.Cuv28Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv28Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(29, (qCReactionInfo.Cuv29Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv29Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(30, (qCReactionInfo.Cuv30Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv30Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(31, (qCReactionInfo.Cuv31Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv31Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(32, (qCReactionInfo.Cuv32Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv32Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(33, (qCReactionInfo.Cuv33Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv33Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(34, (qCReactionInfo.Cuv34Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv34Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(35, (qCReactionInfo.Cuv35Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv35Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(36, (qCReactionInfo.Cuv36Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv36Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(37, (qCReactionInfo.Cuv37Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv37Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(38, (qCReactionInfo.Cuv38Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv38Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(39, (qCReactionInfo.Cuv39Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv39Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(40, (qCReactionInfo.Cuv40Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv40Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(41, (qCReactionInfo.Cuv41Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv41Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(42, (qCReactionInfo.Cuv42Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv42Ws - qCReactionInfo.CuvBlkWs)));
                            series.Points.Add(new SeriesPoint(43, (qCReactionInfo.Cuv43Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv43Ws - qCReactionInfo.CuvBlkWs)));

                            series.View.Color = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
                            chartQCReaction.Series.Add(series);
                            txtReactionCupNum.Text = qCReactionInfo.CUVNO.ToString();
                            cboMeasurePoint.Text = "1";
                            txtAbsorb.Text = ((qCReactionInfo.Cuv1Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv1Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                            chartQCReaction.Show();

                        }
                        else
                            MessageBoxDraw.ShowMsg("该任务没有检测执行或者正在执行中！", MsgType.OK);
                        btnClose.Enabled = true;
                    }));
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ReactionProcessQC_Load(object sender, EventArgs e)
        {
            chartQCReaction.Series.Clear();
            btnClose.Enabled = false;
            txtProjectName.Text = qCResInfo.ProjectName;
            txtSampleType.Text = qCResInfo.SampleType;
            txtConcResult.Text = qCResInfo.ConcResult.ToString();
            txtQCName.Text = qCResInfo.QCName;
            txtLotNum.Text = qCResInfo.LotNum;
            txtManufacturer.Text = qCResInfo.Manufacturer;
            txtLevelConc.Text = qCResInfo.HorizonLevel;

            this.loadReactionProcessQC();
        }

        private void loadReactionProcessQC()
        {
            CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.QCResult, new Dictionary<string, object[]>() { { "QueryTimeCourseByQCInfo", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), qCResInfo), strDateTime } } });
        }

        private void cboMeasurePoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboMeasurePoint.SelectedItem as string)
            {
                case "1":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv1Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv1Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "2":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv2Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv2Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "3":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv3Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv3Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "4":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv4Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv4Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "5":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv5Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv5Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "6":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv6Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv6Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "7":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv7Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv7Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "8":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv8Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv8Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "9":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv9Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv9Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "10":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv10Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv10Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "11":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv11Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv11Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "12":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv12Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv12Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "13":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv13Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv13Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "14":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv14Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv14Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "15":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv15Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv15Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "16":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv16Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv16Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "17":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv17Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv17Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "18":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv18Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv18Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "19":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv19Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv19Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "20":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv20Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv20Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "21":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv21Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv21Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "22":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv22Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv22Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "23":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv23Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv23Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "24":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv24Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv24Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "25":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv25Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv25Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "26":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv26Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv26Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "27":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv27Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv27Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "28":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv28Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv28Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "29":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv29Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv29Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "30":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv30Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv30Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "31":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv31Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv31Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "32":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv32Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv32Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "33":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv33Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv33Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "34":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv34Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv34Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "35":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv35Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv35Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "36":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv36Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv36Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "37":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv37Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv37Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "38":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv38Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv38Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "39":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv39Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv39Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "40":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv40Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv40Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "41":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv41Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv41Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "42":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv42Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv42Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                case "43":
                    txtAbsorb.Text = ((qCReactionInfo.Cuv43Wm - qCReactionInfo.CuvBlkWm) - (qCReactionInfo.Cuv43Ws - qCReactionInfo.CuvBlkWs)).ToString("#0.0000");
                    break;
                default:
                    break;
            }
        }
    }
}