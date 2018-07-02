﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using BioA.Common;
using BioA.Common.IO;
using System.Threading;

namespace BioA.UI
{
    public partial class ChemicalParameter : DevExpress.XtraEditors.XtraUserControl
    {
        ProjectParameter projectParameter;
        RangeParameter rangeParameter;
        CalibrationParameter calibrationParameter;
      
        public ChemicalParameter()
        {
            InitializeComponent();

            Thread.Sleep(1000);
            projectParameter = new ProjectParameter();
            projectParameter.AssayProInfoEvent += AssayProInfo_Event;


            xtraTabPage1.Controls.Add(projectParameter);
        }
        
        /// <summary>
        /// 生化项目信息事件响应方法
        /// </summary>
        /// <param name="strAccessSqlMethod"></param>
        /// <param name="sender"></param>
        private void AssayProInfo_Event(object sender)
        {
            string a = XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity);

            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }


        List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
        CommunicationEntity communicationEntity = new CommunicationEntity();

        AssayProjectParamInfo assProParamInfo = new AssayProjectParamInfo();
        AssayProjectCalibrationParamInfo calibParam = new AssayProjectCalibrationParamInfo();

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
                    projectParameter.LstAssayProInfos = lstAssayProInfos;
                    break;
                case "AssayProjectAdd":
                    string[] str = (string[])XmlUtility.Deserialize(typeof(string[]), sender as string);
                    if (str[4] == "项目创建成功！")
                    {
                        AssayProjectInfo assyInfo = new AssayProjectInfo();
                        assyInfo.ProjectName = str[0];
                        assyInfo.SampleType = str[1];
                        assyInfo.ProFullName = str[2];
                        assyInfo.ChannelNum = str[3];
                        lstAssayProInfos.Add(assyInfo);
                        projectParameter.LstAssayProInfos = lstAssayProInfos;
                    }
                    else if (str[4] == "项目创建失败，请联系管理员！")
                    {
                        projectParameter.LstAssayProInfos = lstAssayProInfos;
                    }
                    else if (str[4] == "该项目已存在，请重新录入。")
                    {
                        projectParameter.LstAssayProInfos = lstAssayProInfos;
                    } 
                    MessageBox.Show(str[4]);
                    //communicationEntity.StrmethodName = "QueryAssayProAllInfo";
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
                    break;
                case "AssayProjectEdit":
                    if ((int)sender == 0)
                    {
                        communicationEntity.StrmethodName = "QueryAssayProAllInfo";
                        CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
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
                        communicationEntity.StrmethodName = "QueryAssayProAllInfo";
                        CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
                    }
                    else
                    {
                        MessageBoxDraw.ShowMsg("请检查校准品、质控品、计算项目和组合项目是否包含被删除的项目，如果存在，不能删除！", MsgType.Warning);
                        return;
                    }
                    break;
                case "GetAssayProjectParamInfoByNameAndType":
                    assProParamInfo = (AssayProjectParamInfo)XmlUtility.Deserialize(typeof(AssayProjectParamInfo), sender as string);
                    projectParameter.AssProParamInfoList = assProParamInfo;
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
                    lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    calibrationParameter.LstAssayProInfos = lstAssayProInfos;
                    break;
                case "QueryCalibParamByProNameAndType":
                    AssayProjectCalibrationParamInfo calibParamInfo = (AssayProjectCalibrationParamInfo)XmlUtility.Deserialize(typeof(AssayProjectCalibrationParamInfo), sender as string);
                    calibrationParameter.CalibParamInfo = calibParamInfo;
                    break;
                case "QueryAssayProAllInfoForRangeParam":
                    lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    rangeParameter.LstAssayProInfos = lstAssayProInfos;
                    break;
                case "QueryRangeParamByProNameAndType":
                    AssayProjectRangeParamInfo rangeParamInfo = (AssayProjectRangeParamInfo)XmlUtility.Deserialize(typeof(AssayProjectRangeParamInfo), sender as string);
                    rangeParameter.RangeParamInfo = rangeParamInfo;
                    break;

                case "QueryCalibratorProinfo":
                    List<CalibratorProjectinfo> calibratorProjectinfo = (List<CalibratorProjectinfo>)XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), sender as string);
                    calibrationParameter.AddCalibrator(calibratorProjectinfo);
                    break;
                case "QueryCalib":
                    List<Calibratorinfo> lisCalibratorinfo = (List<Calibratorinfo>)XmlUtility.Deserialize(typeof(List<Calibratorinfo>), sender as string);
                    calibrationParameter.lisCalibratorinfo(lisCalibratorinfo);
                    break;
                case "QueryCalibrationCurve":
                    List<CalibrationCurveInfo> calibrationCurveInfo = (List<CalibrationCurveInfo>)XmlUtility.Deserialize(typeof(List<CalibrationCurveInfo>), sender as string);
                    calibrationParameter.AddcalibrationCurveInfo(calibrationCurveInfo);
                    break;
                default:
                    break;
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0 && xtraTabPage1.Controls.Contains(projectParameter)==false)
            {
                xtraTabPage1.Controls.Clear();
                projectParameter = new ProjectParameter();
                projectParameter.AssayProInfoEvent += AssayProInfo_Event;
                xtraTabPage1.Controls.Add(projectParameter);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1 && xtraTabPage1.Controls.Contains(calibrationParameter) == false)
            {
                xtraTabPage2.Controls.Clear();
                calibrationParameter = new CalibrationParameter();
                calibrationParameter.AssayProInfoForCalibParamEvent += AssayProInfo_Event;
                xtraTabPage2.Controls.Add(calibrationParameter);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2 && xtraTabPage1.Controls.Contains(rangeParameter) == false)
            {
                xtraTabPage3.Controls.Clear();
                rangeParameter = new RangeParameter();
                rangeParameter.AssayProInfoForRangeParamEvent += AssayProInfo_Event;
                xtraTabPage3.Controls.Add(rangeParameter);
            }
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
