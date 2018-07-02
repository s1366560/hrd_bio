﻿using System;
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

namespace BioA.UI
{
    public partial class UserManagement : DevExpress.XtraEditors.XtraUserControl
    {
        UserCeation userCeation ;
        public UserManagement()
        {
            InitializeComponent();
            userCeation = new UserCeation();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            userCeation.DataHandleEvent += userCeation_DataHandleEvent;
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
                   // string str = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    int count = Convert.ToInt32(sender);
                    if (count!=0)
                    {
                        MessageBoxDraw.ShowMsg("删除失败！请先删除检验医师中的用户名！", MsgType.Exception);
                    }
                    QueryUserInfo();
                    break;
                case "QueryUserCeation":
                    List<UserInfo> lstUserCeation = new List<UserInfo>();
                    lstUserCeation = (List<UserInfo>)XmlUtility.Deserialize(typeof(List<UserInfo>), sender as string);
                    userCeation.AddUserCeation(lstUserCeation[0]);
                   
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
               str += "数据审核,";
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
               str += "交叉污染,";
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
        private void UserInfoAdd(List<UserInfo> lstuserInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridControl1.RefreshDataSource();
               
                DataTable dt = new DataTable();

                dt.Columns.AddRange(new DataColumn[] { 
                    new DataColumn(){ColumnName = "用户账户", MaxLength = 200},
                    new DataColumn(){ColumnName = "用户名称", MaxLength = 200},
                    new DataColumn(){ColumnName = "创建时间", MaxLength = 200},
                    new DataColumn(){ColumnName = "拥有权限", MaxLength = 1500}
                });
               

                if (lstuserInfo.Count != 0)
                {
                    foreach (UserInfo QueryUserInfo in lstuserInfo)
                    {
                        dt.Rows.Add(new object[] { QueryUserInfo.UserID, QueryUserInfo.UserName, QueryUserInfo.CreateTime,PermissionAdd(QueryUserInfo)});
                    }
                }
                this.gridControl1.DataSource = dt;
                this.gridView1.Columns[0].Width = 200;
                this.gridView1.Columns[1].Width = 200;
                this.gridView1.Columns[2].Width = 200;
                this.gridView1.Columns[3].Width = 800;

            }));
        }
        
        private void btnUsercreation_Click(object sender, EventArgs e)
        {
            
            userCeation.Text = "用户创建";
            userCeation.lblNotesLoad1();
            userCeation.StartPosition = FormStartPosition.CenterScreen;
            userCeation.ShowDialog();
           // userCeation.DataHandleEvent += userCeation_DataHandleEvent;
        }

        private void userCeation_DataHandleEvent(string str ,object sender)
        {
            if (str == "用户创建")
            {
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "AddUserInfo";
                DataConfig.ObjParam = XmlUtility.Serializer(typeof(UserInfo), sender as UserInfo);
                UserManagementSend(DataConfig);
            }
            else
            {
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "EditUserInfo";
                DataConfig.ObjParam = XmlUtility.Serializer(typeof(UserInfo), sender as UserInfo);
                DataConfig.ObjLastestParam = ID;
                UserManagementSend(DataConfig);
            }
        }
        string ID;
        private void btnUsereditor_Click(object sender, EventArgs e)
        {
            int selectedHandle;
            if (this.gridView1.SelectedRowsCount > 0)
            {
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                ID = this.gridView1.GetRowCellValue(selectedHandle, "用户账户").ToString();
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "QueryUserCeation";
                DataConfig.ObjParam = ID;
            
                UserManagementSend(DataConfig);
                userCeation.Text = "用户编辑";
                userCeation.lblNotesLoad2();
                userCeation.StartPosition = FormStartPosition.CenterScreen;
                userCeation.ShowDialog();
            }

        }
       
        private void QueryUserInfo()
        {
            CommunicationEntity DataConfig = new CommunicationEntity();
            DataConfig.StrmethodName = "QueryUserInfo";
            DataConfig.ObjParam = "";
            UserManagementSend(DataConfig);
        }
        private void UserManagementSend(object sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SystemUserManagement, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }
        private void UserManagement_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(QueryUserInfo));
        }

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
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "DeleteUserInfo";
                DataConfig.ObjParam = ID;
                DataConfig.ObjLastestParam = UserName;
                UserManagementSend(DataConfig);
            }
        }
    }
}