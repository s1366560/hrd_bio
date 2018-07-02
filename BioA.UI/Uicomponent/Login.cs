using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.Common;
using BioA.Common.IO;
using System.Diagnostics;

namespace BioA.UI
{
    public partial class Login : Form
    {
        //同步信号
        ManualResetEvent _AllDone = new ManualResetEvent(false);

        public delegate void LoginDataTransferDelegate(object sender);
        public event LoginDataTransferDelegate LoginEvent;

        public Login()
        {
            InitializeComponent();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBoxDraw.ShowMsg("用户名输入不能为空", "警告", MsgType.Warning);
                return;
            }

            if (txtPassword.Text == "")
            {
                MessageBoxDraw.ShowMsg("密码输入不能为空", "警告", MsgType.Warning);
                return;
            }
            string password = EncryptionText.EncryptDES(txtPassword.Text, KeyManager.PWDKey);
            
            btnLogin.Enabled = false;
            lblStarting.Text = "登录中";
            timer1.Start();
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.Login, XmlUtility.Serializer(typeof(CommunicationEntity),
                new CommunicationEntity("UserLogin", XmlUtility.Serializer(typeof(string[]), new string[] { txtUserName.Text, password}))));
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "UserLogin":
                    if ((sender as string) == "登录成功！")
                    {
                        CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.Login, XmlUtility.Serializer(typeof(CommunicationEntity),
                            new CommunicationEntity("QueryUserAuthority", txtUserName.Text)));
                        Thread.Sleep(5000);
                        this.Invoke(new EventHandler(delegate
                            {
                                timer1.Stop();
                                this.Close();
                            }));
                    }
                    else
                    {
                        this.Invoke(new EventHandler(delegate
                            {
                                lblStarting.Text = "登录失败，请重新登录！";
                            }));
                    }
                    break;
                case "QueryUserAuthority":
                    UserInfo userInfo = XmlUtility.Deserialize(typeof(UserInfo), sender as string) as UserInfo;
                    if (LoginEvent != null)
                        LoginEvent(userInfo);
                    break;
            }
        }

        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            if (i <= 6)
            {
                lblStarting.Text += "•";
            }
            else
            {
                lblStarting.Text = "登录中";
                i = 0;
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }
    }
}
