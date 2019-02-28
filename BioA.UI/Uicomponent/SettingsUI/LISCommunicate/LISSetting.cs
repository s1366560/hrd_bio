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
using System.IO.Ports;
using BioA.Service;
using BioA.Common;

namespace BioA.UI
{
    public partial class LISSetting : DevExpress.XtraEditors.XtraForm
    {
        //实例化数据库
        LISSettingService service = new LISSettingService();

        public LISSetting()
        {
            InitializeComponent();
            this.cboSerialPort.Properties.Items.AddRange(SerialPort.GetPortNames());
        }
        /// <summary>
        /// 通讯方式值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comBoxCommMode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.comBoxCommMode.Text == "串口")
            {
                this.grpNetworkSettings.Enabled = false;
                if(this.grpSerialPortSetting.Enabled == false)
                    this.grpSerialPortSetting.Enabled = true;
            }
            else
            {
                this.grpSerialPortSetting.Enabled = false;
                if(this.grpNetworkSettings.Enabled == false)
                    this.grpNetworkSettings.Enabled = true;
            }
        }
        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            object[] LISSettingList = new object[3];
            LISSettingInfo lisSetting = new LISSettingInfo();
            lisSetting.CommunicationMode = this.comBoxCommMode.Text;
            lisSetting.CommunicationDirection = this.comBoxCommDirection.Text;
            lisSetting.CommunicationOverTime = Convert.ToInt32(this.txtCommOverTime.Text == "" ? "1":txtCommOverTime.Text);
            lisSetting.RealTiimeSampleResults = this.checkBoxSampResult.Checked;
            LISSettingList[0] = lisSetting;
            if (this.comBoxCommMode.Text == "串口")
            {
                SerialCommunicationInfo lisSerialCommInfo = new SerialCommunicationInfo();
                lisSerialCommInfo.SerialName = this.cboSerialPort.Text;
                lisSerialCommInfo.BaudRate = Convert.ToInt32(this.cboBaudRate.Text);
                lisSerialCommInfo.DataBits = Convert.ToInt32(this.cboDataBit.Text);
                lisSerialCommInfo.StopBits = this.cboStopBits.Text;
                lisSerialCommInfo.Parity = this.cboParity.Text;
                LISSettingList[1] = lisSerialCommInfo;
            }
            else
            {
                LISCommunicateNetworkInfo lisCommNetwork = new LISCommunicateNetworkInfo();
                lisCommNetwork.IPAddress = this.txtServerIP.Text;
                lisCommNetwork.NetworkPort = this.txtPort.Text;
                LISSettingList[2] = lisCommNetwork;
            }
            service.UpdateLISSetingAndNetworkORSerialInfo(LISSettingList);
            button2_Click(null,null);
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LISSetting_Load(object sender, EventArgs e)
        {
            object[] lis = service.QueryLISSettingInfo() as object[];
            if (lis[0] as LISSettingInfo == null)
            {
                this.comBoxCommMode.SelectedIndex = 0;
                this.comBoxCommDirection.SelectedIndex = 0;
            }
            else
            {
                LISSettingInfo lisSetting = lis[0] as LISSettingInfo;
                this.comBoxCommMode.SelectedIndex = this.comBoxCommMode.Properties.Items.IndexOf(lisSetting.CommunicationMode);
                this.comBoxCommDirection.SelectedIndex = this.comBoxCommDirection.Properties.Items.IndexOf(lisSetting.CommunicationDirection);
                this.txtCommOverTime.Text = lisSetting.CommunicationOverTime.ToString();
                this.checkBoxSampResult.Checked = lisSetting.RealTiimeSampleResults; 
            }
            if (lis[1] as SerialCommunicationInfo == null)
            {
                this.cboSerialPort.SelectedIndex = 0;
                this.cboBaudRate.SelectedIndex = 0;
                this.cboDataBit.SelectedIndex = 0;
                this.cboStopBits.SelectedIndex = 0;
                this.cboParity.SelectedIndex = 0;
            }
            else
            {
                SerialCommunicationInfo lisSerial = lis[1] as SerialCommunicationInfo;
                this.cboSerialPort.SelectedIndex = this.cboSerialPort.Properties.Items.IndexOf(lisSerial.SerialName);
                this.cboBaudRate.SelectedIndex = this.cboBaudRate.Properties.Items.IndexOf(lisSerial.BaudRate.ToString());
                this.cboDataBit.SelectedIndex = this.cboDataBit.Properties.Items.IndexOf(lisSerial.DataBits.ToString());
                this.cboStopBits.SelectedIndex = this.cboStopBits.Properties.Items.IndexOf(lisSerial.StopBits);
                this.cboParity.SelectedIndex = this.cboParity.Properties.Items.IndexOf(lisSerial.Parity);
            }
            if (lis[2] as LISCommunicateNetworkInfo == null)
            {

            }
            else
            {
                LISCommunicateNetworkInfo lisNteWork = lis[2] as LISCommunicateNetworkInfo;
                this.txtServerIP.Text = lisNteWork.IPAddress;
                this.txtPort.Text = lisNteWork.NetworkPort;
            }
        }
        /// <summary>
        /// 清除、关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}