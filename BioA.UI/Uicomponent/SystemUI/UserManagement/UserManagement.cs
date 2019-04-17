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
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using BioA.Common.IO;
using BioA.Common;
using System.Threading;

namespace BioA.UI
{
    public partial class UserManagement : DevExpress.XtraEditors.XtraUserControl
    {
        UserCeation userCeation ;

        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> userManagementDic = new Dictionary<string, object[]>();

        /// <summary>
        /// 存储所有用户信息
        /// </summary>
        DataTable dt = new DataTable();
        public UserManagement()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            
            dt.Columns.Add("用户账户");
            dt.Columns.Add("用户名称");
            dt.Columns.Add("创建时间");
            dt.Columns.Add("拥有权限");
            this.gridControl1.DataSource = dt;
            gridView1.Columns[0].Width = 200;
            gridView1.Columns[1].Width = 200;
            gridView1.Columns[2].Width = 200;
            gridView1.Columns[3].Width = 1000;

        }
        List<UserInfo> lstuserInfo = new List<UserInfo>();
        public void DataTransfer_Event(string strMethod, object sender)
        {
           
            switch (strMethod)
            {
                case "QueryUserInfo":
                    lstuserInfo = (List<UserInfo>)XmlUtility.Deserialize(typeof(List<UserInfo>), sender as string);
                    UserInfoAdd(lstuserInfo);
                    break;
                case "AddUserInfo":
                    QueryUserInfo();
                    break;
                case "EditUserInfo":
                    QueryUserInfo();
                    break;
                case "DeleteUserInfo":
                    int count = Convert.ToInt32(sender);
                    if (count!=0)
                    {
                        MessageBoxDraw.ShowMsg("删除失败！请先删除检验医师中的用户名！", MsgType.Exception);
                    }
                    QueryUserInfo();
                    break;
                case "QueryUserCeation":
                    UserInfo UserCeationInfo = new UserInfo();
                    UserCeationInfo = (UserInfo)XmlUtility.Deserialize(typeof(UserInfo), sender as string);
                    userCeation.AddUserCeation(UserCeationInfo);
                   
                    break;
                   
            }
        }
        private string PermissionAdd (UserInfo userInfo)
        {
            string str = "";
           if (userInfo.ApplyTask)
           {
               str = "申请测试,";
           }
           if (userInfo.DataCheck)
           {
               str += "任务结果,";
           }
           if (userInfo.CalibDataCheck)
           {
               str += "校准审核,";
           }
           if (userInfo.ReagentSetting)
           {
               str += "设置,";
           }
           if (userInfo.ReagentState)
           {
               str += "状态,";
           }
           if (userInfo.CalibState)
           {
               str += "校准状态,";
           }
           if (userInfo.CalibMaintain)
           {
               str += "校准品维护,";
           }
           if (userInfo.QCState)
           {
               str += "质控状态,";
           }
           if (userInfo.QCMaintain)
           {
               str += "质控品维护,";
           }
            
           if (userInfo.ChemistryParam)
           {
               str += "化学参数,";
           }
           if (userInfo.CombProject)
           {
               str += "组合项目,";
           }
           if (userInfo.CalcProject)
           {
               str += "计算项目,";
           }
           if (userInfo.EnvironmentParam)
           {
               str += "环境参数,";
           }
           if (userInfo.CrossPollute)
           {
               str += "防污策略,";
           }
           if (userInfo.DataConfiguration)
           {
               str += "数据配置,";
           }
           if (userInfo.LISCommunicate)
           {
               str += "LIS通信,";
           }
           if (userInfo.RouMaintain)
           {
               str += "常规保养,";
           }
           if (userInfo.EquipDebug)
           {
               str += "设备调试,";
           }
           if (userInfo.UserManage)
           {
               str += "用户管理,";
           }
           if (userInfo.DepartManage)
           {
               str += "科室管理,";
           }
           if (userInfo.Configuration)
           {
               str += "配置,";
           }
           if (userInfo.LogCheck)
           {
               str += "日志查看,";
           }
           if (userInfo.VersionInfo)
           {
               str += "版本信息,";
           }
            return str;
        }
        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <param name="lstuserInfo"></param>
        private void UserInfoAdd(List<UserInfo> lstuserInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridControl1.RefreshDataSource();

                dt.Rows.Clear();
                
                if (lstuserInfo.Count != 0)
                {
                    foreach (UserInfo QueryUserInfo in lstuserInfo)
                    {
                        dt.Rows.Add(new object[] { QueryUserInfo.UserID, QueryUserInfo.UserName, QueryUserInfo.CreateTime,PermissionAdd(QueryUserInfo)});
                    }
                }
            }));
        }
        
        private void btnUsercreation_Click(object sender, EventArgs e)
        {
            if (userCeation == null)
            {
                userCeation = new UserCeation();
                userCeation.DataHandleEvent += userCeation_DataHandleEvent;
                userCeation.StartPosition = FormStartPosition.CenterScreen;
            }
            userCeation.Text = "用户创建";
            userCeation.lblNotesLoad1();
            userCeation.UserCeation_Load(null,null);
            userCeation.ShowDialog();
        }
        /// <summary>
        /// 委托事件执行的方法
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sender"></param>
        private void userCeation_DataHandleEvent(string str ,UserInfo sender)
        {
            if (str == "用户创建")
            {
                userManagementDic.Clear();
                userManagementDic.Add("AddUserInfo", new object[] { XmlUtility.Serializer(typeof(UserInfo), sender as UserInfo) });
                UserManagementSend(userManagementDic);
            }
            else
            {
                if (sender != null)
                {
                    userManagementDic.Clear();
                    userManagementDic.Add("EditUserInfo", new object[] { XmlUtility.Serializer(typeof(UserInfo), sender as UserInfo), ID });
                    UserManagementSend(userManagementDic);
                }
            }
        }
        private string ID;
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUsereditor_Click(object sender, EventArgs e)
        {
            int selectedHandle;
            if (this.gridView1.SelectedRowsCount > 0)
            {
                if (userCeation == null)
                {
                    userCeation = new UserCeation();
                    userCeation.DataHandleEvent += userCeation_DataHandleEvent;
                    userCeation.StartPosition = FormStartPosition.CenterScreen;
                }
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                ID = this.gridView1.GetRowCellValue(selectedHandle, "用户账户").ToString();
                userManagementDic.Clear();
                userManagementDic.Add("QueryUserCeation", new object[] { ID });
                UserManagementSend(userManagementDic);

                userCeation.Text = "用户编辑";
                userCeation.lblNotesLoad2();
                userCeation.UserCeation_Load(null, null);
                userCeation.ShowDialog();
            }

        }
       
        private void QueryUserInfo()
        {
            userManagementDic.Clear();
            userManagementDic.Add("QueryUserInfo", new object[] { "" });
            UserManagementSend(userManagementDic);
        }
        /// <summary>
        /// 将信息发送给服务器处理
        /// </summary>
        /// <param name="sender"></param>
        private void UserManagementSend(Dictionary<string, object[]> sender)
        {
            var userManagementThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SystemUserManagement, sender);
            });
            userManagementThread.IsBackground = true;
            userManagementThread.Start();
        }
        public void UserManagement_Load(object sender, EventArgs e)
        {
            this.QueryUserInfo();
            this.ID = "";
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserdelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                ID = this.gridView1.GetRowCellValue(selectedHandle, "用户账户").ToString();
                string UserName = this.gridView1.GetRowCellValue(selectedHandle, "用户名称").ToString();
                userManagementDic.Clear();
                userManagementDic.Add("DeleteUserInfo", new object[] { ID, UserName });
                UserManagementSend(userManagementDic);
            }
        }
    }
}
