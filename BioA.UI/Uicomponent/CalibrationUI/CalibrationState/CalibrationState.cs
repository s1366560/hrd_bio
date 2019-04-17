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
using BioA.Common;
using BioA.Common.IO;
using System.Threading;
using BioA.Service;

namespace BioA.UI
{
    public partial class lstvCalibrationState : DevExpress.XtraEditors.XtraUserControl
    {
        CalibrationCurve calibrationCurve;
        ReactionProcessCB reactionProcessCB;

        CalibrationTrace calibrationTrace;
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> calibStateDictionary = new Dictionary<string, object[]>();
        public lstvCalibrationState()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.FocusedRow.Font = font;
            dt.Columns.Add("检测项目");
            dt.Columns.Add("样本类型");
            dt.Columns.Add("检测方法");
            //dt.Columns.Add("空白吸光度");
            //dt.Columns.Add("K斜率");
            //dt.Columns.Add("A因数");
            //dt.Columns.Add("B因数");
            //dt.Columns.Add("C因数");
            gridControl1.DataSource = dt;
        }

        private void calibrationCurve_CalibrationEvent(Dictionary<string, object[]> sender)
        {
            CalibrationStateSend(sender);
        }

        /// <summary>
        /// 校准曲线点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCalibCurve_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (calibrationCurve == null)
                {
                    calibrationCurve = new CalibrationCurve();
                    calibrationCurve.CalibrationEvent += calibrationCurve_CalibrationEvent;
                    calibrationCurve.StartPosition = FormStartPosition.CenterScreen;
                }
                CalibrationCurveInfo calibrationCurveInfo = new CalibrationCurveInfo();
                int selectedHandle = this.gridView1.GetSelectedRows()[0];
                calibrationCurveInfo.CalibType = this.gridView1.GetRowCellValue(selectedHandle, "检测方法").ToString();
                calibrationCurveInfo.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "检测项目").ToString();
                calibrationCurveInfo.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "样本类型").ToString();
                calibrationCurve.AddCalibrationCurve(calibrationCurveInfo);
                calibrationCurve.CalibrationCurve_Load(null,null);
                calibrationCurve.ShowDialog();
            }
        }

        /// <summary>
        /// 校准追溯点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalibTrace_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (calibrationTrace == null)
                {
                    calibrationTrace = new CalibrationTrace();
                    calibrationTrace.StartPosition = FormStartPosition.CenterScreen;
                }
                CalibrationResultinfo calibrationResultinfo = new CalibrationResultinfo();
                int selectedHandle = this.gridView1.GetSelectedRows()[0];
                calibrationResultinfo.CalibMethod= this.gridView1.GetRowCellValue(selectedHandle, "检测方法").ToString();
                calibrationResultinfo.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "检测项目").ToString();
                calibrationResultinfo.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "样本类型").ToString();
                calibStateDictionary.Clear();
                calibStateDictionary.Add("QueryCalibrationResultinfo", new object[] { XmlUtility.Serializer(typeof(CalibrationResultinfo), calibrationResultinfo) });
                CalibrationStateSend(calibStateDictionary);
                calibrationTrace.CalibrationAdd(calibrationResultinfo);
                calibrationTrace.ShowDialog();
            }
            
        }
        
        /// <summary>
        /// 反应进程点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReactionProcess_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (reactionProcessCB == null)
                {
                    reactionProcessCB = new ReactionProcessCB();
                    reactionProcessCB.CalibrationTimeCoursetEvent += calibrationCurve_CalibrationEvent;
                    reactionProcessCB.StartPosition = FormStartPosition.CenterScreen;
                }
                CalibrationResultinfo calibrationResultinfo = new CalibrationResultinfo();
                int selectedHandle = this.gridView1.GetSelectedRows()[0];
                calibrationResultinfo.CalibMethod = this.gridView1.GetRowCellValue(selectedHandle, "检测方法").ToString();
                calibrationResultinfo.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "检测项目").ToString();
                calibrationResultinfo.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "样本类型").ToString();

                reactionProcessCB.calibrationResult = calibrationResultinfo;
                reactionProcessCB.ReactionProcessCB_Load(null,null);
                reactionProcessCB.ShowDialog();
            }

        }

        private void CalibrationStateSend(Dictionary<string, object[]> sender)
        {
            var calibStateThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.CalibrationState, sender);
            });
            calibStateThread.IsBackground = true;
            calibStateThread.Start();
        }

        /// <summary>
        /// 存储校准品状态信息
        /// </summary>
        DataTable dt = new DataTable();
        public void CalibrationState_Load(object sender, EventArgs e)
        {
            AddCalibrationState(new Calibrator().QueryCalibrationState("QueryCalibrationState", ""));
        }

        void AddCalibrationState(List<CalibrationResultinfo> calibratorinfo)
        {
            dt.Rows.Clear();
            foreach (CalibrationResultinfo calibrationResultinfo in calibratorinfo)
            {
                dt.Rows.Add(new object[] { calibrationResultinfo.ProjectName,calibrationResultinfo.SampleType, calibrationResultinfo .CalibMethod});
            }
        }

        List<CalibrationResultinfo> Resultinfo = new List<CalibrationResultinfo>();
        List<SDTTableItem> calibrationCurveInfo = new List<SDTTableItem>();
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryCalibrationState":
                    List<CalibrationResultinfo> calibratorinfo = (List<CalibrationResultinfo>)XmlUtility.Deserialize(typeof(List<CalibrationResultinfo>), sender as string);
                    AddCalibrationState(calibratorinfo);                    
                    break;
                case "QueryCalibrationResultinfo":
                    Resultinfo = (List<CalibrationResultinfo>)XmlUtility.Deserialize(typeof(List<CalibrationResultinfo>), sender as string);
                   // AddCalibrationState(Resultinfo);
                    calibrationTrace.CalibrationResultinfoAdd(Resultinfo);
                    break;
                case "QueryCalibrationCurveInfo":
                    calibrationCurveInfo = (List<SDTTableItem>)XmlUtility.Deserialize(typeof(List<SDTTableItem>), sender as string);
                    calibrationCurve.SelectedlistCalibrationCurve(calibrationCurveInfo);
                    break;
                case "SaveSDTTableItem":
                    string str = sender as string;
                    if (str == "校准曲线保存成功！")
                    {
                        calibrationCurve.StrResult = str;
                    }
                    else
                    {
                        calibrationCurve.StrResult = str;
                    }
                    break;
                case "QuerysDTTableItem":
                    List<SDTTableItem> lisSDTTableItem = (List<SDTTableItem>)XmlUtility.Deserialize(typeof(List<SDTTableItem>), sender as string);
                    //form1.sDTTableItem = lisSDTTableItem;
                    //form1.add(lisSDTTableItem);
                    break;
                case "QueryCalibrationReactionProcess":
                    TimeCourseInfo calibrationReactionProcess = (TimeCourseInfo)XmlUtility.Deserialize(typeof(TimeCourseInfo), sender as string);
                    if (calibrationReactionProcess != null)
                    {
                        reactionProcessCB.SampleReactionInfo = calibrationReactionProcess;
                        //reactionProcessCB.Add(calibrationReactionProcess[0]);
                    }
                    break;
                case "QueryCalibrationResultInfoAndTimeCUVNO":
                    List<CalibrationResultinfo> lstCalibrationResultInfoAndTimeCUVNO = (List<CalibrationResultinfo>)XmlUtility.Deserialize(typeof(List<CalibrationResultinfo>), sender as string);
                    reactionProcessCB.calibrationResultInfoAndTimeCUVNOAdd(lstCalibrationResultInfoAndTimeCUVNO);
                    break;

            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            //form1.ShowDialog();
        }
    }
}
