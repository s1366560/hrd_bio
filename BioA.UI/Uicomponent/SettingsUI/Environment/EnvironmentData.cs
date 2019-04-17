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
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading;
using BioA.Service;

namespace BioA.UI
{
    public partial class EnvironmentData : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 存储客户端法送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> envmentDataDic = new Dictionary<string, object[]>();

        List<EnvironmentParamInfo> environmentParamInfoList = new List<EnvironmentParamInfo>();
        public EnvironmentData()
        {
            InitializeComponent();
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {
            
            switch (strMethod)
            {
                case "QueryEnvironmentParamInfo":
                    environmentParamInfoList = (List<EnvironmentParamInfo>)XmlUtility.Deserialize(typeof(List<EnvironmentParamInfo>), sender as string);
                    EnvironmentAdd(environmentParamInfoList);
                    break;
                case "UpdateEnvironmentParamInfo":
                    if ((int)sender > 0)
                    {
                        loadEnvironmentData();
                    }
                    break;
                default:
                    break;
            }
        }

        private void EnvironmentAdd(List<EnvironmentParamInfo> lstEnvironmentParamInfo)
        {
            if (lstEnvironmentParamInfo.Count > 0)
            {
                this.Invoke(new EventHandler(delegate
                {
                    txtReaSurplusWarn.Text = lstEnvironmentParamInfo[0].ReagentSurplus.ToString();
                    txtReaLowestVol.Text = lstEnvironmentParamInfo[0].ReagentLeastVol.ToString();
                    txtHighCuvette.Text = lstEnvironmentParamInfo[0].CuvetteBlankHigh.ToString();
                    txtLowCuvette.Text = lstEnvironmentParamInfo[0].CuvetteBlankLow.ToString();
                    txtWashSurplusWarn.Text = lstEnvironmentParamInfo[0].AbluentSurplus.ToString();
                    txtWashLowestVol.Text = lstEnvironmentParamInfo[0].AbluentLeastVol.ToString();
                    if (lstEnvironmentParamInfo[0].AutoFreezeTask)
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

        public void EnvironmentData_Load(object sender, EventArgs e)
        {
            this.loadEnvironmentData();
        }
        private void loadEnvironmentData()
        {
            RunningStateInfo runningstateinfo = new EnvironmentParameter().QueryRuningSateInfo("QueryRuningSateInfo");
            txthatchtemp.Text = runningstateinfo.TempOffset.ToString();
            comboBoxQCDCon.Text = runningstateinfo.QCSMPContainerType;
            comboBoxCalbDCon.Text = runningstateinfo.SDTSMPContainerType;

            envmentDataDic.Clear();
            envmentDataDic.Add("QueryEnvironmentParamInfo", null);
            EnvironmentDataLoad(envmentDataDic);
        }


        private void EnvironmentDataLoad(Dictionary<string, object[]> sender)
        {
            var envmentDataThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SettingsEnvironment, sender);
            });
            envmentDataThread.IsBackground = true;
            envmentDataThread.Start();
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
            if(!Regex.IsMatch(txthatchtemp.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                MessageBoxDraw.ShowMsg("孵育槽温控输入格式有误！", MsgType.Warning);
                return;
            }
            if (Convert.ToDouble(txthatchtemp.Text.Trim()) > 5)
            {
                MessageBoxDraw.ShowMsg("孵育槽温控不能超过5！", MsgType.Warning);
                return;
            }
            environmentParamInfo.ReagentSurplus = (float)Convert.ToDouble(txtReaSurplusWarn.Text);
            environmentParamInfo.ReagentLeastVol = (float)Convert.ToDouble(txtReaLowestVol.Text);
            environmentParamInfo.CuvetteBlankLow = (float)Convert.ToDouble(txtLowCuvette.Text);
            environmentParamInfo.CuvetteBlankHigh = (float)Convert.ToDouble(txtHighCuvette.Text);
            environmentParamInfo.AbluentSurplus = (float)Convert.ToDouble(txtWashSurplusWarn.Text);
            environmentParamInfo.AbluentLeastVol = (float)Convert.ToDouble(txtWashLowestVol.Text);
            running.QCSMPContainerType = comboBoxQCDCon.Text;
            running.SDTSMPContainerType = comboBoxCalbDCon.Text;
            running.TempOffset = (float)Convert.ToDouble(txthatchtemp.Text);

            if (chkReagentMarginLock.Checked ==true)
            {
             
                environmentParamInfo.AutoFreezeTask = true;
            }
            else
            {
                environmentParamInfo.AutoFreezeTask  = false;
            }
            envmentDataDic.Clear();
            envmentDataDic.Add("UpdateEnvironmentParamInfo", new object[] { XmlUtility.Serializer(typeof(EnvironmentParamInfo), environmentParamInfo), XmlUtility.Serializer(typeof(RunningStateInfo), running) });
            EnvironmentDataLoad(envmentDataDic);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnvironmentAdd(environmentParamInfoList);
        }
    }
}