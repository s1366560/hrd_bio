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
using BioA.Common;
using BioA.Service;
using System.Runtime.InteropServices;

namespace BioA.UI
{
    public partial class LogOutLogin : DevExpress.XtraEditors.XtraForm
    {

        public delegate void LoginSuccess();

        public event LoginSuccess LoginSuccessEvent;

        public LogOutLogin()
        {
            InitializeComponent();
            this.ComBoxAccount.Focus();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButLoging_Click(object sender, EventArgs e)
        {
            if (ComBoxAccount.Text == "用户名/账号" || (ComBoxAccount.Text == "" && ComBoxAccount.Text == string.Empty))
            {
                MessageBox.Show("账号不能为空！");
                this.ComBoxAccount.Focus();
                return;
            }

            if (TextPasswordValue.Text == "" && TextPasswordValue.Text == string.Empty)
            {
                MessageBox.Show("密码不能为空！");
                this.TextPasswordValue.Focus();
                return;
            }
            string account = ComBoxAccount.Text;
            string password = EncryptionText.EncryptDES(TextPasswordValue.Text, KeyManager.PWDKey);
            ILogin loging = new ILogin();
            string result = loging.UserLogin("UserLogin", account, password);
            if (result == "登录成功！")
            {
                UserLoginInfo.SetUserLogInfo(loging.QueryUserAuthority("QueryUserAuthority", account));
                LoginSuccessEvent();
                this.Close();
                this.Dispose();
            }
        }

    }
}