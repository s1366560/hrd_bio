using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.Common;
using BioA.Service;
using System.Text.RegularExpressions;

namespace BioA.UI
{
    public delegate void PassUserInfo<T>(T t);

    public partial class UserCommon : Form
    {

        public event PassUserInfo<UserInfo> PassUserInfoEvent;

        public UserCommon()
        {
            InitializeComponent();
        }

        public List<UserInfo> userInfoList = new List<UserInfo>();

        // 提示信息的字体颜色
        Color c = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

        private void ButSave_Click(object sender, EventArgs e)
        {
            UserInfo u = userInfoList.SingleOrDefault(t => t.UserID == TxtAccount.Text.Trim());
            if (u != null)
            {
                if (u.UserID.Contains(TxtAccount.Text.Trim()))
                {
                    this.LabAccountPropmt.Text = "* 该账号已被使用,请重新输入！";
                    this.LabName.ForeColor = c;
                    this.TxtAccount.Focus();
                    return;
                }
            }
            if (System.Text.Encoding.Default.GetBytes(TxtName.Text).Length < 2)
            {
                this.LabName.Text = "* 账号名称长度不能小于4位！";
                this.LabName.ForeColor = c;
                this.TxtName.Focus();
                return;
            }
            if (System.Text.Encoding.Default.GetBytes(TxtName.Text).Length >= 15)
            {
                this.LabName.Text = "* 账号名称长度不能超过14位！";
                this.LabName.ForeColor = c;
                this.TxtName.Focus();
                return;
            }
            if (System.Text.Encoding.Default.GetBytes(TxtAccount.Text.Trim()).Length < 4)
            {
                this.LabAccountPropmt.Text = "* 账号长度不能小于4位！";
                this.LabAccountPropmt.ForeColor = c;
                this.TxtAccount.Focus();
                return;
            }
            if (TxtAccount.Text.Trim().Length >= 19)
            {
                this.LabAccountPropmt.Text = "* 账号长度不能超过18位！";
                this.LabAccountPropmt.ForeColor = c;
                this.TxtAccount.Focus();
                return;
            }
            if (TxtPassword.Text.Trim().Length >= 23)
            {
                this.LabUserPassword.Text = "* 密码长度不能超过22位！";
                this.LabUserPassword.ForeColor = c;
                this.TxtPassword.Focus();
                return;
            }
            else if (TxtPassword.Text.Trim().Length < 6)
            {
                this.LabUserPassword.Text = "* 密码长度不能少于6位！";
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

            UserInfo userInfo = new UserInfo();
            userInfo.UserID = TxtAccount.Text.Trim();
            userInfo.UserName = TxtName.Text.Trim();
            userInfo.UserPassword = EncryptionText.EncryptDES(TxtPassword.Text.Trim(), KeyManager.PWDKey);
            int iCount = new SystemUserManagement().AddUserInfo("AddUserInfo",userInfo);
            if (iCount > 0)
            {
                PassUserInfoEvent(userInfo);
                this.Invoke(new EventHandler(delegate
                {
                    MessageBox.Show("用户创建成功！");
                    this.Close(); this.Dispose();
                }));
            }
            else
                this.Invoke(new EventHandler(delegate { MessageBox.Show("用户创建失败！"); this.Close(); this.Dispose(); }));
        }
        /// <summary>
        /// 限制账号不能输入非法字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAccount_KeyPress(object sender, KeyPressEventArgs e)
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
        /// <summary>
        /// 限制账号输入的长度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAccount_EditValueChanged(object sender, EventArgs e)
        {
            if (TxtAccount.Text.Trim().Length >= 19)
            {
                this.LabAccountPropmt.Text = "* 账号长度不能超过18位！";
                this.LabAccountPropmt.ForeColor = c;
                this.TxtAccount.Focus();
            }
            else if (TxtAccount.Text.Trim() == "")
            {
                this.LabAccountPropmt.Text = "* 账号长度为2~18位.支持数字和大小写字母";
                this.LabAccountPropmt.ForeColor = Color.Gray;
            }
            else
                this.LabAccountPropmt.Text = "";
        }
        /// <summary>
        /// 限制密码输入的长度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (TxtPassword.Text.Trim().Length >= 23)
            {
                this.LabUserPassword.Text = "* 密码长度不能超过23位！";
                this.LabUserPassword.ForeColor = c;
                this.TxtPassword.Focus();
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
        /// 确认密码限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAffirmPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (TxtAffirmPassword.Text.Trim().Length >= 23)
            {
                this.LabAffirmPassw.Text = "* 密码长度不能超过22位！";
                this.LabAffirmPassw.ForeColor = c;
                this.TxtAffirmPassword.Focus();
            }
            else if (TxtAffirmPassword.Text.Trim() == "")
            {
                this.LabAffirmPassw.Text = "* 确认密码必须跟上面密码一致！";
                this.LabAffirmPassw.ForeColor = Color.Gray;
            }
            else
                this.LabAffirmPassw.Text = "";
        }
        /// <summary>
        /// 限制账号名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtName_EditValueChanged(object sender, EventArgs e)
        {
            if (System.Text.Encoding.Default.GetBytes(TxtName.Text).Length >= 15)
            {
                this.LabName.Text = "* 账号名称超过限定长度！";
                this.LabName.ForeColor = c;
                this.TxtName.Focus();
            }
            else if (TxtName.Text.Trim() == "")
            {
                this.LabName.Text = "* 中英文都可以,最长14个英文或者7个中文";
                this.LabName.ForeColor = Color.Gray;
            }
            else
                this.LabName.Text = "";
        }

        /// <summary>
        /// 清空上一次记录下来的缓存数据
        /// </summary>
        public void ClearCache()
        {
            this.TxtAccount.Text = "";
            this.TxtName.Text = "";
            this.LabUserPassword.Text = "* 密码长度为6~22位.支持数字和大小写字母";
            this.LabUserPassword.ForeColor = Color.Gray;
            this.LabAffirmPassw.Text = "* 确认密码必须跟上面密码一致！";
            this.LabAffirmPassw.ForeColor = Color.Gray;
        }

        private void ButCancel_Click(object sender, EventArgs e)
        {
            this.TxtAccount.Text = "";
            this.TxtName.Text = "";
            this.TxtPassword.Text = "";
            this.LabUserPassword.Text = "* 密码长度为6~22位.支持数字和大小写字母";
            this.LabUserPassword.ForeColor = Color.Gray;
            this.TxtAffirmPassword.Text = "";
            this.LabAffirmPassw.Text = "* 确认密码必须跟上面密码一致！";
            this.LabAffirmPassw.ForeColor = Color.Gray;
        }

    }
}
