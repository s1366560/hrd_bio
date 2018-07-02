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
using BioA.UI;
using System.IO.Ports;
using Microsoft.Win32;


namespace BioA.UI
{
    public partial class LISCommunicate : DevExpress.XtraEditors.XtraUserControl
    {
        public LISCommunicate()
        {
            InitializeComponent();
            ckhTCPIPLink.Checked = true;
        }

        List<LISCommunicateNetworkInfo> lstLISCommunicateNetworkInfo = new List<LISCommunicateNetworkInfo>();
        List<SerialCommunicationInfo> lstSerialCommunicationInfo = new List<SerialCommunicationInfo>();

        protected bool isNumberic(string message, out int result)
        {
            System.Text.RegularExpressions.Regex rex =
            new System.Text.RegularExpressions.Regex(@"^\d+$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = int.Parse(message);
                return true;
            }
            else
                return false;
        }
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "NetworkLISCommunicate":
                    lstLISCommunicateNetworkInfo = (List<LISCommunicateNetworkInfo>)XmlUtility.Deserialize(typeof(List<LISCommunicateNetworkInfo>), sender as string);
                    NetworkLISCommunicateAdd(lstLISCommunicateNetworkInfo);
                    break;

                case "SerialLISCommunicate":
                    lstSerialCommunicationInfo = (List<SerialCommunicationInfo>)XmlUtility.Deserialize(typeof(List<SerialCommunicationInfo>), sender as string);
                    SerialLISCommunicateAdd(lstSerialCommunicationInfo);
                    break;

                case "NetworkLISCommunicateUpDate":
                    if ((int)sender > 0)
                    {
                        CommunicationEntity NetworkcommunicationEntity = new CommunicationEntity();
                        NetworkcommunicationEntity.StrmethodName = "NetworkLISCommunicate";
                        NetworkcommunicationEntity.ObjParam = "";
                        LISCommunicateLoad(NetworkcommunicationEntity);
                    }

                    break;

                case "SerialLISCommunicateUpDate":
                    if ((int)sender > 0)
                    {
                        CommunicationEntity SerialCommunicationEntity = new CommunicationEntity();
                        SerialCommunicationEntity.StrmethodName = "SerialLISCommunicate";
                        SerialCommunicationEntity.ObjParam = "";
                        LISCommunicateLoad(SerialCommunicationEntity);
                    }
                    break;

                default:
                    break;
            }
        }

        private void NetworkLISCommunicateAdd(List<LISCommunicateNetworkInfo> lstLISCommunicateNetworkInfo)
        {
            if (lstLISCommunicateNetworkInfo.Count > 0)
            {
                this.Invoke(new EventHandler(delegate
                {
                    txtServerIP.Text = lstLISCommunicateNetworkInfo[0].IPAddress;
                    txtPort.Text = lstLISCommunicateNetworkInfo[0].NetworkPort;
                    if (lstLISCommunicateNetworkInfo[0].RealTimeSendResult == true)
                    {
                        chkTestResults.Checked = true;
                    }
                    if (lstLISCommunicateNetworkInfo[0].Reconnection == true)
                    {
                        chkDisconnectAutoReconnect.Checked = true;
                    }
                    if (lstLISCommunicateNetworkInfo[0].StartingUp == true)
                    {
                        chkBootAutoLinkLISServe.Checked = true;
                    }
                }));
            }
        }
        private void SerialLISCommunicateAdd(List<SerialCommunicationInfo> lstSerialCommunicationInfo)
        {
            if (lstSerialCommunicationInfo.Count > 0)
            {
                 this.Invoke(new EventHandler(delegate
                {
                cboSerialPort.SelectedItem = lstSerialCommunicationInfo[0].SerialName;
                cboBaudRate.SelectedItem = lstSerialCommunicationInfo[0].BaudRate.ToString();
                cboDataBit.SelectedItem = lstSerialCommunicationInfo[0].DataBits.ToString();
                cboStopBits.SelectedItem = lstSerialCommunicationInfo[0].StopBits.ToString();
                cboParity.SelectedItem = lstSerialCommunicationInfo[0].Parity.ToString();
                txtCommunicateTime.Text = lstSerialCommunicationInfo[0].CommunicateionOvertime.ToString();
                txtReConnectTime.Text = lstSerialCommunicationInfo[0].ReConnectionTime.ToString();
                //textEdit8.Text = lstSerialCommunicationInfo[0].CommunicateionType.ToString();
                if (lstSerialCommunicationInfo[0].RealTimeSendResult == true)
                {
                    checkEdit1.Checked = true;
                }
                if (lstSerialCommunicationInfo[0].Reconnection == true)
                {
                    checkEdit2.Checked = true;
                }
                if (lstSerialCommunicationInfo[0].StartingUp == true)
                {
                    checkEdit3.Checked = true;
                }
                }));
            }

        }



        private void LISCommunicateLoad(object sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsLISCommunicate, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }

        private void LISCommunicate_Load(object sender, EventArgs e)
        {
            this.cboSerialPort.Properties.Items.Clear();
            this.cboBaudRate.Properties.Items.Clear();
            this.cboDataBit.Properties.Items.Clear();
            this.cboParity.Properties.Items.Clear();
            this.cboStopBits.Properties.Items.Clear();
            string[] ArryPort = SerialPort.GetPortNames();

            this.cboSerialPort.Properties.Items.AddRange(ArryPort);
            
            // 1200、2400、4800、9600、19200、38400、43000、56000、57600
            cboBaudRate.Properties.Items.AddRange(new object[] { 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 });
            cboBaudRate.SelectedIndex = 3;

            cboDataBit.Properties.Items.AddRange(new object[] { 5, 6, 7, 8 });
            cboDataBit.SelectedIndex = 3;
            cboParity.Properties.Items.AddRange(new object[]{ 0, 1, 2, 3, 4 });
            cboParity.SelectedIndex = 0;
            cboStopBits.Properties.Items.AddRange(new object[] { 1, 1.5, 2 });
            cboStopBits.SelectedIndex = 0;

            CommunicationEntity NetworkcommunicationEntity = new CommunicationEntity();
            NetworkcommunicationEntity.StrmethodName = "NetworkLISCommunicate";
            NetworkcommunicationEntity.ObjParam = "";
            LISCommunicateLoad(NetworkcommunicationEntity);
            CommunicationEntity SerialCommunicationEntity = new CommunicationEntity();
            SerialCommunicationEntity.StrmethodName = "SerialLISCommunicate";
            SerialCommunicationEntity.ObjParam = "";
            LISCommunicateLoad(SerialCommunicationEntity);

            
           
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            string message1 = txtServerIP.Text.Trim();
            string message2 = txtPort.Text.Trim();
            int result;
            if (message1 != string.Empty)
            {
                if (isNumberic(message2, out result))
                {

                }
                else
                {
                    MessageBoxDraw.ShowMsg("端口号输入有误，请重新输入！", MsgType.Exception);
                    return;
                }
            }
            else
            {
                MessageBoxDraw.ShowMsg("请输入服务器IP地址！", MsgType.Exception);
                return;
            }

            LISCommunicateNetworkInfo lISCommunicateNetworkInfo = new LISCommunicateNetworkInfo();
                lISCommunicateNetworkInfo.IPAddress =  txtServerIP.Text;
                lISCommunicateNetworkInfo.NetworkPort =  txtPort.Text ;
                if (chkTestResults.Checked == true)
                {
                    lISCommunicateNetworkInfo.RealTimeSendResult = true;                    
                }
                else
                {
                    lISCommunicateNetworkInfo.RealTimeSendResult = false;  
                }
                if (chkDisconnectAutoReconnect.Checked == true)
                {
                    lISCommunicateNetworkInfo.Reconnection = true;
                }
                else
                {
                    lISCommunicateNetworkInfo.Reconnection = false;
                }
                if (chkBootAutoLinkLISServe.Checked == true)
                {
                    lISCommunicateNetworkInfo.StartingUp = true;
                }
                else
                {
                    lISCommunicateNetworkInfo.StartingUp =false;
                }

                if (lISCommunicateNetworkInfo != null)
                {
                    CommunicationEntity NetworkcommunicationEntity = new CommunicationEntity();
                    NetworkcommunicationEntity.StrmethodName = "NetworkLISCommunicateUpDate";
                    NetworkcommunicationEntity.ObjParam = XmlUtility.Serializer(typeof(LISCommunicateNetworkInfo), lISCommunicateNetworkInfo);
                    LISCommunicateLoad(NetworkcommunicationEntity);
                }
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SerialCommunicationInfo serialCommunicationInfo = new SerialCommunicationInfo();

            if (cboSerialPort.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择串口！", MsgType.Warning);
                return;
            }


            string message1 = txtCommunicateTime.Text.Trim();
            string message2 = txtReConnectTime.Text.Trim();
            int result;
            if (message1 != "")
            {
                if (isNumberic(message1, out result))
                {
                    serialCommunicationInfo.CommunicateionOvertime = (float)Convert.ToDouble(txtCommunicateTime.Text);
                    
                }
                else
                {
                    MessageBoxDraw.ShowMsg("您的输入有误，请重新输入！", MsgType.Warning);
                    return;
                }
            }
            if (message2 != "")
            {
                if (isNumberic(message2, out result))
                {
                    serialCommunicationInfo.ReConnectionTime = (float)Convert.ToDouble(txtReConnectTime.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("您的输入有误，请重新输入！", MsgType.Warning);
                    return;
                }
            }


            
            serialCommunicationInfo.SerialName = cboSerialPort.Text;
            serialCommunicationInfo.BaudRate = Convert.ToInt32(cboBaudRate.Text);
            serialCommunicationInfo.DataBits = Convert.ToInt32(cboDataBit.Text);
            serialCommunicationInfo.StopBits = Convert.ToInt32(cboStopBits.Text);
            serialCommunicationInfo.Parity = Convert.ToInt32(cboParity.Text);
            //serialCommunicationInfo.CommunicateionType= textEdit8.Text ;
            
            if (checkEdit1.Checked == true)
            {
                serialCommunicationInfo.RealTimeSendResult = true;
            }
            else
            {
                serialCommunicationInfo.RealTimeSendResult = false;
            }

            if (checkEdit2.Checked == true)
            {
                serialCommunicationInfo.Reconnection = true;
            }
            else
            {
                serialCommunicationInfo.Reconnection = false;
            }
            if (checkEdit3.Checked == true)
            {
                serialCommunicationInfo.StartingUp = true;
            }
            else
            {
                serialCommunicationInfo.StartingUp = false;
            }
            CommunicationEntity NetworkcommunicationEntity = new CommunicationEntity();
            if (serialCommunicationInfo != null)
            {
                NetworkcommunicationEntity.StrmethodName = "SerialLISCommunicateUpDate";
                NetworkcommunicationEntity.ObjParam = XmlUtility.Serializer(typeof(SerialCommunicationInfo), serialCommunicationInfo);
                LISCommunicateLoad(NetworkcommunicationEntity);
            }
        }

        private void ckhTCPIPLink_CheckedChanged(object sender, EventArgs e)
        {
            if (ckhTCPIPLink.Checked==true)
            {
                chkComLink.Checked = false;
                cboDataBit.Enabled = false;               
                cboBaudRate.Enabled = false;
                cboSerialPort.Enabled = false;
                cboParity.Enabled = false;
                txtCommunicateTime.Enabled = false;
                //textEdit8.Enabled = false;
                cboStopBits.Enabled = false;
                txtReConnectTime.Enabled = false;
                checkEdit1.Enabled = false;
                checkEdit2.Enabled = false;
                checkEdit3.Enabled = false;
                simpleButton1.Enabled = false;
                simpleButton2.Enabled = false;

                ckhTCPIPLink.Checked = true;
                txtServerIP.Enabled = true;
                txtPort.Enabled = true;
                chkBootAutoLinkLISServe.Enabled = true;
                chkDisconnectAutoReconnect.Enabled = true;
                chkTestResults.Enabled = true;
                btnLink.Enabled = true;
                btnOff.Enabled = true;
            }
        }

        private void chkComLink_CheckedChanged(object sender, EventArgs e)
        {
            if (chkComLink.Checked == true)
            {
                ckhTCPIPLink.Checked = false;
                txtServerIP.Enabled = false;
                txtPort.Enabled = false;
                chkBootAutoLinkLISServe.Enabled = false;
                chkDisconnectAutoReconnect.Enabled = false;
                chkTestResults.Enabled = false;
                btnLink.Enabled = false;
                btnOff.Enabled = false;

                chkComLink.Checked = true;
                cboDataBit.Enabled = true;
                cboBaudRate.Enabled = true;
                cboSerialPort.Enabled = true;
                cboParity.Enabled = true;
                txtCommunicateTime.Enabled = true;
                //textEdit8.Enabled = true;
                cboStopBits.Enabled = true;
                txtReConnectTime.Enabled = true;
                checkEdit1.Enabled = true;
                checkEdit2.Enabled = true;
                checkEdit3.Enabled = true;
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = true;

            }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {

        } 
    }
}
