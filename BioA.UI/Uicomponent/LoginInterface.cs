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
using System.IO;

namespace BioA.UI
{
    public partial class LoginInterface : Form
    {
        //同步信号
        ManualResetEvent _AllDone = new ManualResetEvent(false);
        /// <summary>
        /// 存储客户端发送信息给服务端的参数集合
        /// </summary>
        private Dictionary<string, object[]> loginDic = new Dictionary<string, object[]>();

        public delegate void LoginDataTransferDelegate(object sender);
        public event LoginDataTransferDelegate LoginEvent;

        public LoginInterface()
        {
            InitializeComponent();
            this.txtUserName.Text = "用户名/账号";
            this.txtUserName.ForeColor = Color.Gray;
            this.txtPassword.Text = "****";
            this.txtPassword.ForeColor = Color.Gray;
            
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtBoxUserName == false)
            {
                if(txtUserName.Text == "用户名/账号" || (txtUserName.Text == "" && txtUserName.Text == string.Empty))
                {
                    this.lblStarting.Text = "* 用户名不能为空！";
                    this.txtUserName.Focus();
                    return;
                }
            }

            if (txtBoxPassword == false)
            {
                if (txtPassword.Text == "  密码" || (txtPassword.Text == "" && txtPassword.Text == string.Empty))
                {
                    this.lblStarting.Text = "* 密码输入不能为空!";
                    this.txtPassword.Focus();
                    return;
                }
            }
            string password = EncryptionText.EncryptDES(txtPassword.Text, KeyManager.PWDKey);
            txtUserName.ReadOnly = true;
            txtPassword.ReadOnly = true;
            btnLogin.Enabled = false;
            lblStarting.Text = "登录中";
            overtime = 0;
            timer1.Start();
            //progBarLogin.Visible = true;
            //progBarLogin.Value = 0;
            loginDic.Clear();
            loginDic.Add("UserLogin", new object[] { txtUserName.Text, password });
            SendInfoToService(loginDic);
        }
        /// <summary>
        /// 发送信息给服务端
        /// </summary>
        /// <param name="sender"></param>
        private void SendInfoToService(Dictionary<string, object[]> sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.Login, sender);
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "UserLogin":
                    if ((sender as string) == "登录成功！")
                    {
                        loginDic.Clear();
                        loginDic.Add("QueryUserAuthority", new object[] { txtUserName.Text});
                        SendInfoToService(loginDic);
                        var serialThread = new Thread(LoadProcess);
                        serialThread.IsBackground = true;
                        serialThread.Start();
                        Thread.Sleep(5000);
                        this.Invoke(new EventHandler(delegate
                            {
                                //lblStarting.Text = "登录成功！";
                                timer1.Stop();
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }));
                    }
                    else
                    {
                        this.Invoke(new EventHandler(delegate
                            {
                                lblStarting.Text = "登录失败，请重新登录！";
                                timer1.Stop();
                                //progBarLogin.Visible = false;
                                btnLogin.Enabled = true;
                                txtUserName.ReadOnly = false;
                                txtPassword.ReadOnly = false;
                                
                            }));
                    }
                    break;
                case "QueryUserAuthority":
                    UserInfo userInfo = XmlUtility.Deserialize(typeof(UserInfo), sender as string) as UserInfo;
                    //Program.userInfo = userInfo;
                    UserLoginInfo.SetUserLogInfo(userInfo);
                    break;
            }
        }
        //登录超时
        int overtime = 0;
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
                overtime++;
                lblStarting.Text = "登录中";
                i = 0;
            }
            if(overtime > 2)
            {
                btnLogin.Enabled = true;
                txtUserName.ReadOnly = false;
                txtPassword.ReadOnly = false;
                lblStarting.Text = "登录超时，请重新登录！";
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBoxDraw.ShowMsg("确认要关闭系统吗？", MsgType.Question) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
                this.Dispose();
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }
        /// <summary>
        /// 异步线程启动串口连接
        /// </summary>
        public void LoadProcess()
        {
            Console.Write("dasdasdas");
            foreach (ProcessInfo s in RunConfigureUtility.ProcessPathList)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(s.ProcessName);
                    if (fileInfo.Extension.Equals(".exe"))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo(s.ProcessName);
                        startInfo.WindowStyle = ProcessWindowStyle.Normal;
                        startInfo.CreateNoWindow = true;

                        Process[] processes = Process.GetProcessesByName(fileInfo.Name.Replace(fileInfo.Extension, ""));
                        if (processes.Count() == 0)
                        {
                            Process process = new Process();
                            process.StartInfo = startInfo;

                            process.Start();

                            switch (s.RunLevel)
                            {
                                case 0: process.PriorityClass = ProcessPriorityClass.Idle; break;
                                case 1: process.PriorityClass = ProcessPriorityClass.BelowNormal; break;
                                case 2: process.PriorityClass = ProcessPriorityClass.Normal; break;
                                case 3: process.PriorityClass = ProcessPriorityClass.AboveNormal; break;
                                case 4: process.PriorityClass = ProcessPriorityClass.High; break;
                                case 5: process.PriorityClass = ProcessPriorityClass.RealTime; break;
                            }

                            //process.WaitForInputIdle();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        //判断账户和密码输入框是否有文本
        Boolean txtBoxUserName = false;
        Boolean txtBoxPassword = false;

        /// <summary>
        /// 失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserNameOrPassword_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if(textBox.Name == "txtUserName")
            {
                if (textBox.Text.Trim() == "")
                {
                    textBox.Text = "用户名/账号";
                    textBox.ForeColor = Color.Gray;
                    txtBoxUserName = false;
                }
                else
                    txtBoxUserName = true;
            }
            else if (textBox.Name == "txtPassword")
            {
                if (textBox.Text.Trim() == "")
                {
                    textBox.Text = "  密码";
                    textBox.ForeColor = Color.Gray;
                    txtBoxPassword = false;
                }
                else
                    txtBoxPassword = true;
            }
            
        }
        /// <summary>
        /// 获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserNameOrPassword_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            
            if (textBox.Name == "txtUserName")
            {
                if (txtBoxUserName == false)
                    textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
            else if (textBox.Name == "txtPassword")
            {
                if (txtBoxPassword == false)
                    textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }
    }

    public class UserLoginInfo
    {
        private static UserInfo userInfo= null;

        public UserLoginInfo()
        {
        }
        /// <summary>
        /// 获取用户参数
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetUserLoginInfo()
        {
            if (userInfo == null)
            {
                userInfo = new UserInfo();
            }
            return userInfo;
        }
        /// <summary>
        /// 设置用户参数
        /// </summary>
        /// <param name="user"></param>
        public static void SetUserLogInfo(UserInfo user)
        {
            userInfo = null;
            userInfo = user;
        }
    }
}
