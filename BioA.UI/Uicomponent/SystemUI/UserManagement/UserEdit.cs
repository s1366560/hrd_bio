using BioA.Common;
using BioA.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioA.UI
{
    public partial class UserEdit : Form
    {

        public event PassUserInfo<UserInfo> PassUserInfoEditEvent;

        public UserEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 限制密码不能输入非法字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtOldPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar <= '9' && e.KeyChar >= '0' || e.KeyChar <= 'Z' && e.KeyChar >= 'A' || e.KeyChar <= 'z' && e.KeyChar >= 'a' || e.KeyChar == '\b')
            {
                e.Handled = false;

            }
            else
            {
                Regex rg = new Regex("^[\u4e00-\u9fa5]$");
                if (rg.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = false;
                }
                else
                    e.Handled = true;
            }
        }
        //用户登录信息
        UserInfo userInfo = UserLoginInfo.GetUserLoginInfo();
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserEdit_Load(object sender, EventArgs e)
        {
            TxtName.Text = userInfo.UserName;
            TxtName.ReadOnly = true;
            TxtAccount.Text = userInfo.UserID;
            TxtAccount.ReadOnly = true;
            LabName.Text = "* 用户名称不能修改！";
            LabAccountPropmt.Text = "* 账号不能修改！";
        }


        // 提示信息的字体颜色
        Color c = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

        private void ButSave_Click(object sender, EventArgs e)
        {
            string OldPsw = EncryptionText.EncryptDES(TxtOldPassword.Text.Trim(), KeyManager.PWDKey);
            if (OldPsw != userInfo.UserPassword)
            {
                this.LabOldPswDisplay.Text = "* 该密码与您之前使用的密码不一致！";
                this.LabOldPswDisplay.ForeColor = c;
                this.TxtOldPassword.Focus();
                return;
            }
            if (TxtPassword.Text.Trim().Length == 22)
            {
                this.LabUserPassword.Text = "* 新密码长度不能超过22位！";
                this.LabUserPassword.ForeColor = c;
                this.TxtPassword.Focus();
                return;
            }
            else if (TxtPassword.Text.Trim().Length < 6)
            {
                this.LabUserPassword.Text = "* 新密码长度不能少于6位！";
                this.LabUserPassword.ForeColor = c;
                this.TxtPassword.Focus();
                return;
            }
            if (TxtAffirmPassword.Text.Trim() != TxtPassword.Text.Trim())
            {
                this.LabAffirmPassw.Text = "* 密码不匹配，请新输入！";
                this.LabAffirmPassw.ForeColor = c;
                this.TxtAffirmPassword.Focus();
                return;
            }
            else
                this.LabAffirmPassw.Text = "";

            userInfo.UserID = TxtAccount.Text.Trim();
            userInfo.UserName = TxtName.Text.Trim();
            userInfo.UserPassword = EncryptionText.EncryptDES(TxtPassword.Text.Trim(), KeyManager.PWDKey);
            int iCount = new SystemUserManagement().IUpdateCommonUserInfo(userInfo);
            if (iCount > 0)
            {
                PassUserInfoEditEvent(userInfo);
                this.Invoke(new EventHandler(delegate
                {
                    UserLoginInfo.SetUserLogInfo(userInfo);
                    MessageBox.Show("用户修改成功！");
                    this.Close(); this.Dispose();
                }));
            }
            else
                this.Invoke(new EventHandler(delegate { MessageBox.Show("用户修改失败！"); this.Close(); this.Dispose(); }));
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButCancel_Click(object sender, EventArgs e)
        {
            this.TxtOldPassword.Text = "";
            this.LabOldPswDisplay.Text = "* 请输入正在使用的密码！";
            this.LabOldPswDisplay.ForeColor = Color.Gray;
            this.TxtPassword.Text = "";
            this.LabUserPassword.Text = "* 密码长度为6~22位.支持数字和大小写字母";
            this.LabUserPassword.ForeColor = Color.Gray;
            this.TxtAffirmPassword.Text = "";
            this.LabAffirmPassw.Text = "* 确认密码必须跟上面密码一致！";
            this.LabAffirmPassw.ForeColor = Color.Gray;
        }
        /// <summary>
        /// 旧密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtOldPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (TxtOldPassword.Text.Trim().Length == 22)
            {
                this.LabOldPswDisplay.Text = "* 密码长度不能超过22位！";
            }
            else if (TxtOldPassword.Text.Trim() == "")
            {
                this.LabOldPswDisplay.Text = "* 请输入您之前使用的密码！";
                this.LabOldPswDisplay.ForeColor = Color.Gray;
            }
            else
                this.LabOldPswDisplay.Text = "";
        }
        /// <summary>
        /// 新密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (TxtPassword.Text.Trim().Length == 22)
            {
                this.LabUserPassword.Text = "* 密码长度不能超过22位！";
            }
            else if (TxtPassword.Text.Trim() == "")
            {
                this.LabUserPassword.Text = "* 密码长度为6~22位.支持数字和大小写字母";
                this.LabUserPassword.ForeColor = Color.Gray;
            }
            else
                this.LabUserPassword.Text = "";
        }
        /// <summary>
        /// 确认密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAffirmPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (TxtAffirmPassword.Text.Trim().Length == 22)
            {
                this.LabAffirmPassw.Text = "* 密码长度不能超过22位！";
            }
            else if (TxtAffirmPassword.Text.Trim() == "")
            {
                this.LabAffirmPassw.Text = "* 确认密码必须跟上面密码一致！";
                this.LabAffirmPassw.ForeColor = Color.Gray;
            }
            else
                this.LabAffirmPassw.Text = "";
        }

    }
}
