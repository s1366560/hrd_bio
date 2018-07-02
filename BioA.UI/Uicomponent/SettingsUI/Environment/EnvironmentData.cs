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
using BioA.Common;
using BioA.Common.IO;
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using System.Text.RegularExpressions;

namespace BioA.UI
{
    public partial class EnvironmentData : DevExpress.XtraEditors.XtraUserControl
    {
        List<EnvironmentParamInfo> environmentParamInfo = new List<EnvironmentParamInfo>();
        public EnvironmentData()
        {
            InitializeComponent();
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {
            
            switch (strMethod)
            {
                case "QueryEnvironmentParamInfo":
                    environmentParamInfo = (List<EnvironmentParamInfo>)XmlUtility.Deserialize(typeof(List<EnvironmentParamInfo>), sender as string);
                    EnvironmentAdd(environmentParamInfo);
                    break;
                case "UpdateEnvironmentParamInfo":
                    if ((int)sender > 0)
                    {
                        CommunicationEntity DatacommunicationEntity = new CommunicationEntity();
                        DatacommunicationEntity.StrmethodName = "QueryEnvironmentParamInfo";
                        DatacommunicationEntity.ObjParam = "";
                        EnvironmentDataLoad(DatacommunicationEntity);
                    }
                    break;
                default:
                    break;
            }
        }

        private void EnvironmentAdd(List<EnvironmentParamInfo> environmentParamInfo)
        {
            if (environmentParamInfo.Count > 0)
            {
                this.Invoke(new EventHandler(delegate
                {
                    txtReaSurplusWarn.Text = environmentParamInfo[0].ReagentSurplus.ToString();
                    txtReaLowestVol.Text = environmentParamInfo[0].ReagentLeastVol.ToString();
                    txtLowCuvette.Text = environmentParamInfo[0].CuvetteBlankHigh.ToString();
                    txtHighCuvette.Text = environmentParamInfo[0].CuvetteBlankLow.ToString();
                    txtWashSurplusWarn.Text = environmentParamInfo[0].AbluentSurplus.ToString();
                    txtWashLowestVol.Text = environmentParamInfo[0].AbluentLeastVol.ToString();
                    if (environmentParamInfo[0].AutoFreezeTask)
                    {
                        chkReagentMarginLock.Checked = true;
                    }
                    else
                    {
                        chkReagentMarginLock.Checked = false;
                    }
                }));
            }
        }

        private void EnvironmentData_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadEnvironmentData));
        }
        private void loadEnvironmentData()
        {
            CommunicationEntity DatacommunicationEntity = new CommunicationEntity();
            DatacommunicationEntity.StrmethodName = "QueryEnvironmentParamInfo";
            DatacommunicationEntity.ObjParam = "";
            EnvironmentDataLoad(DatacommunicationEntity);
        }


        private void EnvironmentDataLoad(object sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsEnvironment, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            RunningStateInfo running = new RunningStateInfo();
            EnvironmentParamInfo environmentParamInfo = new EnvironmentParamInfo();

            if (!Regex.IsMatch(txtLowCuvette.Text.Trim(), @"^(-?\d+)(\.\d+)?$") || !Regex.IsMatch(txtHighCuvette.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                MessageBoxDraw.ShowMsg("比色杯空白最低值或最高值输入格式有误！", MsgType.Warning);
                return;
            }

            if ((float)Convert.ToDouble(txtLowCuvette.Text) > (float)Convert.ToDouble(txtHighCuvette.Text))
            {
                MessageBoxDraw.ShowMsg("比色杯空白最低值应小于或等于最高值！", MsgType.Warning);
                return;
            }

            if (!Regex.IsMatch(txtReaLowestVol.Text.Trim(), @"^(-?\d+)(\.\d+)?$") || !Regex.IsMatch(txtReaSurplusWarn.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                MessageBoxDraw.ShowMsg("试剂设置输入格式有误！", MsgType.Warning);
                return;
            }

            if ((float)Convert.ToDouble(txtReaLowestVol.Text) > (float)Convert.ToDouble(txtReaSurplusWarn.Text))
            {
                MessageBoxDraw.ShowMsg("试剂最小体积应小于试剂余量报警体积！", MsgType.Warning);
                return;
            }

            if (!Regex.IsMatch(txtWashLowestVol.Text.Trim(), @"^(-?\d+)(\.\d+)?$") || !Regex.IsMatch(txtWashSurplusWarn.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                MessageBoxDraw.ShowMsg("清洗剂设置输入格式有误！", MsgType.Warning);
                return;
            }

            if ((float)Convert.ToDouble(txtWashLowestVol.Text) > (float)Convert.ToDouble(txtWashSurplusWarn.Text))
            {
                MessageBoxDraw.ShowMsg("清洗剂最小体积应小于清洗剂余量报警体积！", MsgType.Warning);
                return;
            }

            environmentParamInfo.ReagentSurplus = (float)Convert.ToDouble(txtReaSurplusWarn.Text);
            environmentParamInfo.ReagentLeastVol = (float)Convert.ToDouble(txtReaLowestVol.Text);
            environmentParamInfo.CuvetteBlankHigh = (float)Convert.ToDouble(txtLowCuvette.Text);
            environmentParamInfo.CuvetteBlankLow = (float)Convert.ToDouble(txtHighCuvette.Text);
            environmentParamInfo.AbluentSurplus = (float)Convert.ToDouble(txtWashSurplusWarn.Text);
            environmentParamInfo.AbluentLeastVol = (float)Convert.ToDouble(txtWashLowestVol.Text);
            running.QCSMPContainerType = comboBoxQCDCon.Text;
            running.SDTSMPContainerType = comboBoxCalbDCon.Text;
            if (chkReagentMarginLock.Checked ==true)
            {
             
                environmentParamInfo.AutoFreezeTask = true;
            }
            else
            {
                environmentParamInfo.AutoFreezeTask  = false;
            }
            CommunicationEntity DatacommunicationEntity = new CommunicationEntity();
            DatacommunicationEntity.StrmethodName = "UpdateEnvironmentParamInfo";
            DatacommunicationEntity.ObjParam = XmlUtility.Serializer(typeof(EnvironmentParamInfo), environmentParamInfo);
            DatacommunicationEntity.ObjLastestParam = XmlUtility.Serializer(typeof(RunningStateInfo), running);
            EnvironmentDataLoad(DatacommunicationEntity);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnvironmentAdd(environmentParamInfo);
        }
    }
}