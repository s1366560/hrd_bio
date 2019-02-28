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

namespace BioA.UI
{
    public partial class UserCeation : DevExpress.XtraEditors.XtraForm
    {
        public UserCeation()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        public delegate void DataHandle(string str, UserInfo sender);//声明一个委托
        public event DataHandle DataHandleEvent;//定义一个委托事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 创建用户清空上次缓存
        /// </summary>
        public void lblNotesLoad1()
        {
            lblNotes.Text = "* 输入不能为空！";
            labelControl1.Text = "* 输入不能为空！";
            labelControl2.Text = "* 输入不能为空！";
            labelControl3.Text = "* 输入不能为空！";
            this.textEdit1.Enabled = true;
            textEdit1.Text = "";
            textEdit2.Text = "";
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        public void lblNotesLoad2()
        {
            lblNotes.Text = "* 不可修改！";
            labelControl1.Text = "* 输入不能为空！";
            labelControl2.Text = "* 输入不能为空！";
            labelControl3.Text = "* 输入不能为空！";
            textEdit1.Enabled = false;
        }
        //UserInfo userInfo = new UserInfo();

        /// <summary>
        /// 编辑用户（显示账号信息和权限）
        /// </summary>
        /// <param name="userInfo"></param>
        public void AddUserCeation(UserInfo userInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                textEdit1.Text=Convert.ToString( userInfo.UserID) ;
                textEdit2.Text= userInfo.UserName ;
           
                if ( userInfo.ApplyTask)
                {
                    chkApplyTask.Checked = true;
                }
                else
                {
                    chkApplyTask.Checked = false;
                }
                if (userInfo.DataCheck)
                {
                    chkDataCheck.Checked = true;
                }
                else
                {
                    chkDataCheck.Checked = false;
                }
                if ( userInfo.CalibDataCheck)
                {
                    chkCalibDataCheck.Checked  = true;
                }
                else
                {
                    chkCalibDataCheck.Checked  = false;
                }
                if ( userInfo.ReagentSetting)
                {
                    chkReagentSetting.Checked = true;
                }
                else
                {
                    chkReagentSetting.Checked = false;
                }
                if (userInfo.ReagentState)
                {
                    chkReagentState.Checked = true;
                }
                else
                {
                    chkReagentState.Checked = false;
                }
                if ( userInfo.CalibState)
                {
                    chkCalibState.Checked = true;
                }
                else
                {
                    chkCalibState.Checked= false;
                }
                if (userInfo.CalibMaintain)
                {
                    chkCalibMaintain.Checked = true;
                }
                else
                {
                    chkCalibMaintain.Checked = false;
                }
                if ( userInfo.QCState)
                {
                    chkQCState.Checked = true;
                }
                else
                {
                    chkQCState.Checked = false;
                }
                if ( userInfo.QCMaintain)
                {
                    chkQCMaintain.Checked  = true;
                }
                else
                {
                    chkQCMaintain.Checked  = false;
                }
                if ( userInfo.ChemistryParam )
                {
                    chkChemistryParam.Checked = true;
                }
                else
                {
                    chkChemistryParam.Checked = false;
                }
                if ( userInfo.CombProject)
                {
                    chkCombProject.Checked = true;
                }
                else
                {
                    chkCombProject.Checked = false;
                }
                if ( userInfo.CalcProject)
                {
                    chkCalcProject.Checked= true;
                }
                else
                {
                    chkCalcProject.Checked = false;
                }
                if ( userInfo.EnvironmentParam)
                {
                    chkEnvironmentParam.Checked = true;
                }
                else
                {
                    chkEnvironmentParam.Checked = false;
                }
                if ( userInfo.CrossPollute)
                {
                    chkCrossPollute.Checked = true;
                }
                else
                {
                    chkCrossPollute.Checked = false;
                }
                if (userInfo.DataConfiguration)
                {
                    chkDataConfiguration.Checked = true;
                }
                else
                {
                    chkDataConfiguration.Checked = false;
                }
                if ( userInfo.LISCommunicate)
                {
                    chkLISCommunicate.Checked= true;
                }
                else
                {
                    chkLISCommunicate.Checked = false;
                }
                if ( userInfo.RouMaintain)
                {
                    chkRouMaintain.Checked = true;
                }
                else
                {
                    chkRouMaintain.Checked = false;
                }
                if ( userInfo.EquipDebug)
                {
                    chkEquipDebug.Checked = true;
                }
                else
                {
                    chkEquipDebug.Checked = false;
                }
                if ( userInfo.UserManage)
                {
                    chkUserManage.Checked= true;
                }
                else
                {
                    chkUserManage.Checked = false;
                }
                if ( userInfo.DepartManage)
                {
                    chkDepartManage.Checked = true;
                }
                else
                {
                    chkDepartManage.Checked = false;
                }
                if ( userInfo.Configuration)
                {
                    chkConfiguration.Checked = true;
                }
                else
                {
                    chkConfiguration.Checked = false;
                }
                if ( userInfo.LogCheck)
                {
                    chkLogCheck.Checked = true;
                }
                else
                {
                    chkLogCheck.Checked = false;
                }
                if ( userInfo.VersionInfo)
                {
                    chkVersionInfo.Checked = true;
                }
                else
                {
                    chkVersionInfo.Checked = false;
                }
            }));
        }
        /// <summary>
        /// 获取用户新增、修改信息和权限
        /// </summary>
        /// <returns></returns>
        private UserInfo Add()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.UserID = textEdit1.Text;
            userInfo.UserName = textEdit2.Text;
            if (textEdit3.Text == textEdit4.Text)
            {
                userInfo.UserPassword = EncryptionText.EncryptDES(textEdit3.Text, KeyManager.PWDKey);
            }
            else
            {
                return null;
            }
            if(chkApplyTask.Checked)
            {
                userInfo.ApplyTask = true;
            }
            else
            {
                userInfo.ApplyTask = false;
            }
            if (chkDataCheck.Checked)
            {
                userInfo.DataCheck = true;
            }
            else
            {
                userInfo.DataCheck = false;
            }
            if (chkCalibDataCheck.Checked)
            {
                userInfo.CalibDataCheck = true;
            }
            else
            {
                userInfo.CalibDataCheck = false;
            }
            if (chkReagentSetting.Checked)
            {
                userInfo.ReagentSetting = true;
            }
            else
            {
                userInfo.ReagentSetting = false;
            }
            if (chkReagentState.Checked)
            {
                userInfo.ReagentState = true;
            }
            else
            {
                userInfo.ReagentState = false;
            }
            if (chkCalibState.Checked)
            {
                userInfo.CalibState = true;
            }
            else
            {
                userInfo.CalibState = false;
            }
            if (chkCalibMaintain.Checked)
            {
                userInfo.CalibMaintain = true;
            }
            else
            {
                userInfo.CalibMaintain = false;
            }
            if (chkQCState.Checked)
            {
                userInfo.QCState = true;
            }
            else
            {
                userInfo.QCState = false;
            }
            if (chkQCMaintain.Checked)
            {
                userInfo.QCMaintain = true;
            }
            else
            {
                userInfo.QCMaintain = false;
            }
            if (chkChemistryParam.Checked)
            {
                userInfo.ChemistryParam = true;
            }
            else
            {
                userInfo.ChemistryParam = false;
            }
            if (chkCombProject.Checked)
            {
                userInfo.CombProject = true;
            }
            else
            {
                userInfo.CombProject = false;
            }
            if (chkCalcProject.Checked)
            {
                userInfo.CalcProject = true;
            }
            else
            {
                userInfo.CalcProject = false;
            }
            if (chkEnvironmentParam.Checked)
            {
                userInfo.EnvironmentParam = true;
            }
            else
            {
                userInfo.EnvironmentParam = false;
            }
            if (chkCrossPollute.Checked)
            {
                userInfo.CrossPollute = true;
            }
            else
            {
                userInfo.CrossPollute = false;
            }
            if (chkDataConfiguration.Checked)
            {
                userInfo.DataConfiguration = true;
            }
            else
            {
                userInfo.DataConfiguration = false;
            }
            if (chkLISCommunicate.Checked)
            {
                userInfo.LISCommunicate = true;
            }
            else
            {
                userInfo.LISCommunicate = false;
            }
            if (chkRouMaintain.Checked)
            {
                userInfo.RouMaintain = true;
            }
            else
            {
                userInfo.RouMaintain = false;
            }
            if (chkEquipDebug.Checked)
            {
                userInfo.EquipDebug = true;
            }
            else
            {
                userInfo.EquipDebug = false;
            }
            if (chkUserManage.Checked)
            {
                userInfo.UserManage = true;
            }
            else
            {
                userInfo.UserManage = false;
            }
            if (chkDepartManage.Checked)
            {
                userInfo.DepartManage = true;
            }
            else
            {
                userInfo.DepartManage = false;
            }
            if (chkConfiguration.Checked)
            {
                userInfo.Configuration = true;
            }
            else
            {
                userInfo.Configuration = false;
            }
            if (chkLogCheck.Checked)
            {
                userInfo.LogCheck = true;
            }
            else
            {
                userInfo.LogCheck = false;
            }
            if (chkVersionInfo.Checked)
            {
                userInfo.VersionInfo = true;
            }
            else
            {
                userInfo.VersionInfo = false;
            }
            return userInfo;
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textEdit3.Text!=textEdit4.Text)
            {
                MessageBoxDraw.ShowMsg("两次输入的密码不相同！", MsgType.Exception);
                return;
            }
            if (textEdit1.Text.Trim() == "")
            {
                MessageBoxDraw.ShowMsg("用户账户输入不能为空，请重新输入！", MsgType.Exception);
                return;
            }
            if (textEdit2.Text.Trim() == "")
            {
                MessageBoxDraw.ShowMsg("用户名称输入不能为空，请重新输入！", MsgType.Exception);
                return;
            }
            if (textEdit3.Text == "")
            {
                MessageBoxDraw.ShowMsg("密码输入不能为空，请重新输入！", MsgType.Exception);
                return;
            }

            if (this.Text == "用户创建")
            {
                if (DataHandleEvent != null)
                {
                    DataHandleEvent("用户创建", Add());
                }
            }
            else
            {
                if (DataHandleEvent != null)
                {
                    DataHandleEvent("用户编辑", Add());
                }
            }
            this.Close();
        }

        private void UserCeation_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadUserCeation));
           
        }
        private void loadUserCeation()
        {
            // 用户创建，初始化权限
            if (this.Text == "用户创建" && RunConfigureUtility.UserAuthorityInitial != null)
            {
                if (RunConfigureUtility.UserAuthorityInitial["申请测试"])
                {
                    chkApplyTask.Checked = true;
                }
                else
                {
                    chkApplyTask.Checked = true;
                }
                if (RunConfigureUtility.UserAuthorityInitial["任务结果"])
                {
                    chkDataCheck.Checked = true;
                }
                else
                {
                    chkDataCheck.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["校准审核"])
                {
                    chkCalibDataCheck.Checked = true;
                }
                else
                {
                    chkCalibDataCheck.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["设置"])
                {
                    chkReagentSetting.Checked = true;
                }
                else
                {
                    chkReagentSetting.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["状态"])
                {
                    chkReagentState.Checked = true;
                }
                else
                {
                    chkReagentState.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["校准状态"])
                {
                    chkCalibState.Checked = true;
                }
                else
                {
                    chkCalibState.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["校准品维护"])
                {
                    chkCalibMaintain.Checked = true;
                }
                else
                {
                    chkCalibMaintain.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["质控状态"])
                {
                    chkQCState.Checked = true;
                }
                else
                {
                    chkQCState.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["质控品维护"])
                {
                    chkQCMaintain.Checked = true;
                }
                else
                {
                    chkQCMaintain.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["化学参数"])
                {
                    chkChemistryParam.Checked = true;
                }
                else
                {
                    chkChemistryParam.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["组合项目"])
                {
                    chkCombProject.Checked = true;
                }
                else
                {
                    chkCombProject.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["计算项目"])
                {
                    chkCalcProject.Checked = true;
                }
                else
                {
                    chkCalcProject.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["环境参数"])
                {
                    chkEnvironmentParam.Checked = true;
                }
                else
                {
                    chkEnvironmentParam.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["防污策略"])
                {
                    chkCrossPollute.Checked = true;
                }
                else
                {
                    chkCrossPollute.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["数据配置"])
                {
                    chkDataConfiguration.Checked = true;
                }
                else
                {
                    chkDataConfiguration.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["LIS通讯"])
                {
                    chkLISCommunicate.Checked = true;
                }
                else
                {
                    chkLISCommunicate.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["常规保养"])
                {
                    chkRouMaintain.Checked = true;
                }
                else
                {
                    chkRouMaintain.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["设备调试"])
                {
                    chkEquipDebug.Checked = true;
                }
                else
                {
                    chkEquipDebug.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["用户管理"])
                {
                    chkUserManage.Checked = true;
                }
                else
                {
                    chkUserManage.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["科室管理"])
                {
                    chkDepartManage.Checked = true;
                }
                else
                {
                    chkDepartManage.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["配置"])
                {
                    chkConfiguration.Checked = true;
                }
                else
                {
                    chkConfiguration.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["日志查看"])
                {
                    chkLogCheck.Checked = true;
                }
                else
                {
                    chkLogCheck.Checked = false;
                }
                if (RunConfigureUtility.UserAuthorityInitial["版本信息"])
                {
                    chkVersionInfo.Checked = true;
                }
                else
                {
                    chkVersionInfo.Checked = false;
                }
            }
        }
    }
}