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
using BioA.Common.Machine;
using BioA.Common;
using BioA.Common.IO;

namespace BioA.UI
{
    public partial class LightSystem : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> lightSystenDic = new Dictionary<string, object[]>();

        private ManuOffsetGain manuOffsetGainInfo = new ManuOffsetGain();
        /// <summary>
        /// 获取的历史光度计检测结果
        /// </summary>
        public ManuOffsetGain ManuOffsetGainInfo
        {
            get
            {
                return manuOffsetGainInfo;
            }
            set
            {
                manuOffsetGainInfo = value;
                if (manuOffsetGainInfo != null)
                {
                    this.BeginInvoke(new EventHandler(delegate {
                        cboWaveLength.Text = manuOffsetGainInfo.WaveLength.ToString();
                        txtOffset.Text = manuOffsetGainInfo.OffSet.ToString();
                        txtGain.Text = manuOffsetGainInfo.Gain.ToString();
                        txtVoltage.Text = manuOffsetGainInfo.Voltage.ToString();
                        if (manuOffsetGainInfo.Voltage < 0.000001)
                        {
                            txtAbs.Text = "0.00";
                        }
                        else
                        {
                            txtAbs.Text = ((float)Math.Log10(10 / manuOffsetGainInfo.Voltage) * RunConfigureUtility.LightSpan).ToString("#0.0000");
                        }

                        if (manuOffsetGainInfo.Voltage > (float)System.Convert.ToDouble(txtMaxVoltage.Text))
                        {
                            txtMaxVoltage.Text = manuOffsetGainInfo.Voltage.ToString();
                        }
                        else
                        {
                            txtMaxVoltage.Text = "10";
                        }

                        if (manuOffsetGainInfo.Voltage < float.Parse(txtMinVoltage.Text) && (manuOffsetGainInfo.Voltage < -0.000001) || manuOffsetGainInfo.Voltage > 0.000001)
                        {
                            txtMinVoltage.Text = manuOffsetGainInfo.Voltage.ToString();
                        }
                        else
                        {
                            txtMinVoltage.Text = "0";
                        }
                        lightSystenDic.Clear();
                        lightSystenDic.Add("GetLatestOffSetGain", new object[] { cboWaveLength.Text.ToString() });
                        CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SystemEquipmentManage,lightSystenDic);
                    }));
                   
                }
            }
        }

        private int iInitialRes = 0;

        public int IInitialRes
        {
            get { return iInitialRes; }
            set
            {
                iInitialRes = value;
                if (iInitialRes == 0)
                {
                    this.Invoke(new EventHandler(delegate {
                        MessageBoxDraw.ShowMsg("更新光度计信息失败！", MsgType.Exception);
                    }));
                }
                else
                {
                    SendNetworkEvent(MachineInfo.SubsystemList.Find(str => str.Name == "Common").ComponetList.Find(componet => componet.Name == "Maintance").CommandList.Find(command => command.FullName == btnBegin.Text).Name);
                }
            }
        }

        private OffSetGain offSetGainInfo = new OffSetGain();

        public OffSetGain OffSetGainInfo
        {
            get { return offSetGainInfo; }
            set
            {
                offSetGainInfo = value;
                if (offSetGainInfo != null)
                {
                    this.Invoke(new EventHandler(delegate {
                        txtOffset.Text = offSetGainInfo.OffSet.ToString();
                        txtGain.Text = offSetGainInfo.Gain.ToString();
                    }));
                } 
            }
        }


        public LightSystem()
        {
            InitializeComponent();
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            string strSender = "";
            Subsystem ConfigureInfo = MachineInfo.SubsystemList.Find(str => str.Name == "Common");
            switch (((SimpleButton)sender).Name)
            {
                case "btnBegin":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == "Maintance").CommandList.Find(command => command.FullName == btnBegin.Text).Name;
                    break;
                case "btnStop":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == "Maintance").CommandList.Find(command => command.FullName == btnStop.Text).Name;
                    break;
                default:
                    break;
            }

            if (((SimpleButton)sender).Name == "btnBegin" && cboWaveLength.SelectedIndex >= 0)
            {
                ManuOffsetGain manuGain = new ManuOffsetGain();
                manuGain.WaveLength = int.Parse(cboWaveLength.SelectedItem.ToString());
                if (txtGain.Text == null || txtGain.Text == "")
                {
                    manuGain.Gain = 80;
                }
                else
                {
                    manuGain.Gain = int.Parse(txtGain.Text);
                }

                if (txtOffset.Text == null || txtOffset.Text == "")
                {
                    manuGain.OffSet = 255;
                }
                else
                {
                    manuGain.OffSet = int.Parse(txtOffset.Text);
                }
                lightSystenDic.Clear();
                lightSystenDic.Add("InitialPhotometerManualCheck", new object[] { XmlUtility.Serializer(typeof(ManuOffsetGain), manuGain) });
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SystemEquipmentManage, lightSystenDic);

                return;
            }


            if (SendNetworkEvent != null && strSender != "" && cboWaveLength.SelectedIndex >= 0)
            {
                SendNetworkEvent(strSender);
            }
        }

        private void LightSystem_Load(object sender, EventArgs e)
        {
            //异步方法调用要加载的数据
            BeginInvoke(new Action(loadLightSystemInfo));            
        }

        private void loadLightSystemInfo()
        {
            txtMaxVoltage.Text = "10";
            txtMinVoltage.Text = "0";

            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "Common")
                {
                    btnBegin.Text = sub.ComponetList[1].CommandList[6].FullName;
                    btnStop.Text = sub.ComponetList[1].CommandList[7].FullName;
                }
            }

            cboWaveLength.Items.AddRange(RunConfigureUtility.WaveLengthList.ToArray());
            lightSystenDic.Add("QueryManuOffsetGain", null);
            CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SystemEquipmentManage, lightSystenDic);
        }
    }
}
