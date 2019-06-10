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
using System.ServiceModel;
using BioA.Common;
using BioA.Common.IO;
using System.Threading;
using BioA.Service;

namespace BioA.UI
{
    public partial class ChemicalParameter : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 存储客户端发送信息给服务器
        /// </summary>
        private Dictionary<string, object[]> chemicalParamDic = new Dictionary<string, object[]>();

        private ProjectParameter projectParameter;
        private RangeParameter rangeParameter;
        private CalibrationParameter calibrationParameter;
      
        public ChemicalParameter()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 生化项目信息事件响应方法
        /// </summary>
        /// <param name="strAccessSqlMethod"></param>
        /// <param name="sender"></param>
        private void AssayProInfo_Event(Dictionary<string, object[]> sender)
        {
            //string a = XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity);
            if (sender != null)
            {
                var chemParamThread = new Thread(() =>
                {
                    CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SettingsChemicalParameter, sender);
                });
                chemParamThread.IsBackground = true;
                chemParamThread.Start();
                
            }
        }
        /// <summary>
        /// 保存所有生化项目信息
        /// </summary>
        List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
        /// <summary>
        /// 保存所有生化项目参数信息
        /// </summary>
        List<AssayProjectParamInfo> assProParamInfo = new List<AssayProjectParamInfo>();

        /// <summary>
        /// 接收数据传到窗体
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="sender"></param>
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryProjectResultUnits":
                    projectParameter.LstUnits = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    break;
                case "QueryAssayProAllInfo":
                    lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    this.Invoke(new EventHandler(delegate
                    {
                        if (xtraTabControl1.SelectedTabPageIndex == 0)
                            projectParameter.LstAssayProInfos = lstAssayProInfos;
                        else if (xtraTabControl1.SelectedTabPageIndex == 1)
                            calibrationParameter.LstAssayProInfos = lstAssayProInfos;
                        else
                            rangeParameter.LstAssayProInfos = lstAssayProInfos;
                    }));
                    break;
                case "AssayProjectAdd":
                    AssayProjectParamInfo assayProjectParamInfo = (AssayProjectParamInfo)XmlUtility.Deserialize(typeof(AssayProjectParamInfo), sender as string);
                    projectParameter.AssayProjectParamInfos = assayProjectParamInfo;
                    break;
                case "AssayProjectEdit":
                    projectParameter.EnditOrDeleteProInfoHandle = (int)sender;
                    if ((int)sender == 0)
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            chemicalParamDic.Clear();
                            chemicalParamDic.Add("QueryAssayProAllInfo", new object[] { "" });
                            AssayProInfo_Event(chemicalParamDic);
                            QueryResultSetTb queryRsult = new QueryResultSetTb(true);
                            List<ResultSetInfo> lstqueryResult = QueryResultSetTb.QueryResultSetInfo;
                            projectParameter.LstAssayProParamInfoAll = new SettingsChemicalParameter().QueryAssayProjectParamInfoAll("QueryAssayProjectParamInfoAll", QueryResultSetTb.QueryResultSetInfo);
                        }));
                    }
                    else
                    {
                        MessageBoxDraw.ShowMsg("请检查校准品、质控品、计算项目和组合项目是否包含修改的项目，如果存在，不能修改！", MsgType.Warning);
                        return;
                    }
                    break;
                case "AssayProjectDelete":
                    if ((int)sender == 0)
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            chemicalParamDic.Clear();
                            chemicalParamDic.Add("QueryAssayProAllInfo", new object[] { "" });
                            AssayProInfo_Event(chemicalParamDic);
                            QueryResultSetTb queryRsult = new QueryResultSetTb(true);
                            List<ResultSetInfo> lstqueryResult = QueryResultSetTb.QueryResultSetInfo;
                        }));
                    }
                    else
                    {
                        MessageBoxDraw.ShowMsg("请检查校准品、质控品、计算项目和组合项目是否包含被删除的项目，如果存在，不能删除！", MsgType.Warning);
                        return;
                    }
                    break;
                case "UpdateCalibParamByProNameAndType":
                    calibrationParameter.ProcessSuccessOrFailureInfo(sender as string);
                    break;
                case "UpdateAssayProjectParamInfo":
                    if ((int)sender == 0)
                    {
                        projectParameter.StrReceiveInfo = "保存失败！";
                    }
                    else
                    {
                        projectParameter.StrReceiveInfo = "保存成功！";
                    }
                    break;
                case "QueryAssayProAllInfoForCalibParam":   // 为校准参数界面获取所有项目信息
                    //lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    //calibrationParameter.LstAssayProInfos = lstAssayProInfos;
                    break;
                case "QueryCalibParamInfoAll":
                    //List<AssayProjectCalibrationParamInfo> lstCalibParamInfo = (List<AssayProjectCalibrationParamInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectCalibrationParamInfo>), sender as string);
                    //calibrationParameter.LstCalibParamInfo = lstCalibParamInfo;
                    //calibrationParameter.LstAssayProInfos = lstAssayProInfos;
                    break;
                case "QueryAssayProAllInfoForRangeParam":
                    //lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    //rangeParameter.LstAssayProInfos = lstAssayProInfos;
                    break;
                case "QueryRangeParamByProNameAndType":
                    AssayProjectRangeParamInfo rangeParamInfo = (AssayProjectRangeParamInfo)XmlUtility.Deserialize(typeof(AssayProjectRangeParamInfo), sender as string);
                    rangeParameter.RangeParamInfo = rangeParamInfo;
                    break;

                case "QueryCalibratorProinfo":
                    //List<CalibratorProjectinfo> calibratorProjectinfo = (List<CalibratorProjectinfo>)XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), sender as string);
                    //this.Invoke(new EventHandler(delegate { calibrationParameter.AddCalibrator(calibratorProjectinfo); }));
                    break;
                case "QueryCalib":
                    //List<Calibratorinfo> lisCalibratorinfo = (List<Calibratorinfo>)XmlUtility.Deserialize(typeof(List<Calibratorinfo>), sender as string);
                    //calibrationParameter.lisCalibratorinfo(lisCalibratorinfo);
                    break;
                case "QueryCalibrationCurve":
                    //List<CalibrationCurveInfo> calibrationCurveInfo = (List<CalibrationCurveInfo>)XmlUtility.Deserialize(typeof(List<CalibrationCurveInfo>), sender as string);
                    //calibrationParameter.AddcalibrationCurveInfo(calibrationCurveInfo);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 界面初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChemicalParameter_Load(object sender, EventArgs e)
        {
            if (projectParameter == null)
            {
                projectParameter = new ProjectParameter();
                projectParameter.AssayProInfoEvent += AssayProInfo_Event;
            }
            else
                projectParameter.ClearProjectParamMemberPropertier();
            if (xtraTabControl1.SelectedTabPageIndex != 0)
                xtraTabControl1.SelectedTabPageIndex = 0;
            else
            {
                projectParameter.ProjectParameter_Load(null, null);
                xtraTabPage1.Controls.Add(projectParameter);
            }
        }

        /// <summary>
        /// 清空本类成员属性值
        /// </summary>
        public void ClearChemicaParameterMemberPropertier()
        {
            this.chemicalParamDic.Clear();
            this.lstAssayProInfos.Clear();
            this.assProParamInfo.Clear();
        }
        /// <summary>
        /// 页面切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                xtraTabPage2.Controls.Clear();
                //xtraTabPage3.Controls.Clear();
                if (projectParameter == null)
                {
                    projectParameter = new ProjectParameter();
                    projectParameter.AssayProInfoEvent += AssayProInfo_Event;
                }
                projectParameter.ProjectParameter_Load(null, null);
                xtraTabPage1.Controls.Add(projectParameter);                
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                xtraTabPage1.Controls.Clear();
                //xtraTabPage3.Controls.Clear();
                if (calibrationParameter == null)
                {
                    calibrationParameter = new CalibrationParameter();
                    calibrationParameter.AssayProInfoForCalibParamEvent += AssayProInfo_Event;
                }
                if (lstAssayProInfos.Count != 0)
                {
                    calibrationParameter.ListAssayprojectInfos = lstAssayProInfos;
                }
                calibrationParameter.CalibrationParameter_Load(null,null);
                xtraTabPage2.Controls.Add(calibrationParameter);
            }
            //else if (xtraTabControl1.SelectedTabPageIndex == 2)
            //{
            //    xtraTabPage1.Controls.Clear();
            //    xtraTabPage2.Controls.Clear();
            //    if (rangeParameter == null)
            //    {
            //        rangeParameter = new RangeParameter();
            //        rangeParameter.AssayProInfoForRangeParamEvent += AssayProInfo_Event;
            //    }
            //    if (lstAssayProInfos.Count != 0)
            //    {
            //        rangeParameter.ListAssayprojectInfos = lstAssayProInfos;
            //    }
            //    xtraTabPage3.Controls.Add(rangeParameter);
            //}
        }
    }
}
