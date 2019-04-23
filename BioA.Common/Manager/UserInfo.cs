// ================================================================================================
//
// 文件名（File Name）：              User.cs
//
// 功能描述（Description）：          用户实体类
//
// 数据表（Tables）：                 对应数据库User表
//
// 作者（Author）：                   冯旗
//
// 日期（Create Date）：              2017-6-21
//
// 修改记录（Revision History）：
//      R1:
//          修改人：
//          修改日期：
//          修改理由：
//
// ================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class UserInfo
    {
        public UserInfo()
        {
            userID = string.Empty;
            userName = string.Empty;
            userPassword = string.Empty;
            createTime = System.DateTime.Now;
            missionVerification = true;
            isSuperAdmin = false;
            isServeEngineer = false;
            isPermitDelete = false;
            isLocked = false;
            applyTask = true;
            dataCheck = true;
            calibDataCheck = true;
            reagentSetting = true;
            reagentState = true;
            calibState = true;
            calibMaintain = true;
            calibTask = true;
            qCState = true;
            qCMaintain = true;
            qCGraphic = true;
            qCTask = true;
            chemistryParam = true;
            combProject = true;
            calcProject = true;
            environmentParam = true;
            crossPollute = true;
            dataConfiguration = true;
            lISCommunicate = true;
            rouMaintain = true;
            userManage = true;
            departManage = true;
            logCheck = true;
            versionInfo = true;
            equipDebug = false;
            configuration = false;
            configurationScript = false;
        }
        /// <summary>
        /// 用户账户
        /// </summary>
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }
        /// <summary>
        /// 用户创建日期
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperAdmin
        {
            get { return isSuperAdmin; }
            set { isSuperAdmin = value; }
        }
        /// <summary>
        /// 是否是服务工程师
        /// </summary>
        public bool IsServeEngineer
        {
            get { return isServeEngineer; }
            set { isServeEngineer = value; }
        }
        /// <summary>
        /// 是否允许被删除
        /// </summary>
        public bool IsPermitDelete
        {
            get { return isPermitDelete; }
            set { isPermitDelete = value; }
        }
        /// <summary>
        /// 是否被锁定
        /// </summary>
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }


        private string          userID;
        private string          userName;
        private string          userPassword;
        private DateTime        createTime;
        private bool            isSuperAdmin;
        private bool            isServeEngineer;
        private bool            isPermitDelete;
        private bool            isLocked;
        private bool calibTask;
        private bool qCGraphic;
        private bool qCTask;


        private bool applyTask;
        /// <summary>
        /// 申请测试
        /// </summary>
        public bool ApplyTask
        {
            get { return applyTask; }
            set { applyTask = value; }
        }
        private bool dataCheck;
        /// <summary>
        /// 任务结果
        /// </summary>
        public bool DataCheck
        {
            get { return dataCheck; }
            set { dataCheck = value; }
        }

        private bool missionVerification;
        /// <summary>
        /// 任务核查
        /// </summary>
        public bool MissionVerification
        {
            get { return missionVerification; }
            set { missionVerification = value; }
        }

        private bool calibDataCheck;
        /// <summary>
        /// 校准审核
        /// </summary>
        public bool CalibDataCheck
        {
            get { return calibDataCheck; }
            set { calibDataCheck = value; }
        }
        private bool reagentSetting;
        /// <summary>
        /// 设置
        /// </summary>
        public bool ReagentSetting
        {
            get { return reagentSetting; }
            set { reagentSetting = value; }
        }
        private bool reagentState;
        /// <summary>
        /// 状态
        /// </summary>
        public bool ReagentState
        {
            get { return reagentState; }
            set { reagentState = value; }
        }
        private bool calibState;
        /// <summary>
        /// 校准状态
        /// </summary>
        public bool CalibState
        {
            get { return calibState; }
            set { calibState = value; }
        }
        private bool calibMaintain;
        /// <summary>
        /// 校准品维护
        /// </summary>
        public bool CalibMaintain
        {
            get { return calibMaintain; }
            set { calibMaintain = value; }
        }
        /// <summary>
        /// 校准任务
        /// </summary>
        public bool CalibTask
        {
            get { return calibTask; }
            set { calibTask = value; }
        }

        private bool qCState;
        /// <summary>
        /// 质量状态
        /// </summary>
        public bool QCState
        {
            get { return qCState; }
            set { qCState = value; }
        }
        private bool qCMaintain;
        /// <summary>
        /// 质控品维护
        /// </summary>
        public bool QCMaintain
        {
            get { return qCMaintain; }
            set { qCMaintain = value; }
        }

        public bool QCGraphic
        {
            get { return qCGraphic; }
            set { qCGraphic = value; }
        }

        public bool QCTask
        {
            get { return qCTask; }
            set { qCTask = value; }
        }

        private bool chemistryParam;
        /// <summary>
        /// 化学参数
        /// </summary>
        public bool ChemistryParam
        {
            get { return chemistryParam; }
            set { chemistryParam = value; }
        }
        private bool combProject;
        /// <summary>
        /// 组合项目
        /// </summary>
        public bool CombProject
        {
            get { return combProject; }
            set { combProject = value; }
        }
        private bool calcProject;
        /// <summary>
        /// 计算项目
        /// </summary>
        public bool CalcProject
        {
            get { return calcProject; }
            set { calcProject = value; }
        }
        private bool environmentParam;
        /// <summary>
        /// 环境参数
        /// </summary>
        public bool EnvironmentParam
        {
            get { return environmentParam; }
            set { environmentParam = value; }
        }
        private bool crossPollute;
        /// <summary>
        /// 交叉污染
        /// </summary>
        public bool CrossPollute
        {
            get { return crossPollute; }
            set { crossPollute = value; }
        }
        private bool dataConfiguration;
        /// <summary>
        /// 数据配置
        /// </summary>
        public bool DataConfiguration
        {
            get { return dataConfiguration; }
            set { dataConfiguration = value; }
        }
        private bool lISCommunicate;
        /// <summary>
        /// LIS通讯
        /// </summary>
        public bool LISCommunicate
        {
            get { return lISCommunicate; }
            set { lISCommunicate = value; }
        }
        private bool rouMaintain;
        /// <summary>
        /// 常规保养
        /// </summary>
        public bool RouMaintain
        {
            get { return rouMaintain; }
            set { rouMaintain = value; }
        }
        private bool equipDebug;
        /// <summary>
        /// 设备调试
        /// </summary>
        public bool EquipDebug
        {
            get { return equipDebug; }
            set { equipDebug = value; }
        }
        private bool userManage;
        /// <summary>
        /// 用户管理
        /// </summary>
        public bool UserManage
        {
            get { return userManage; }
            set { userManage = value; }
        }
        private bool departManage;
        /// <summary>
        /// 科室管理
        /// </summary>
        public bool DepartManage
        {
            get { return departManage; }
            set { departManage = value; }
        }
        private bool configuration;
        /// <summary>
        /// 试剂开放和扫描配置
        /// </summary>
        public bool Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }
        private bool logCheck;
        /// <summary>
        /// 日志查看
        /// </summary>
        public bool LogCheck
        {
            get { return logCheck; }
            set { logCheck = value; }
        }
        private bool versionInfo;
        /// <summary>
        /// 版本信息
        /// </summary>
        public bool VersionInfo
        {
            get { return versionInfo; }
            set { versionInfo = value; }
        }

        private bool configurationScript;
        /// <summary>
        /// 脚本配置
        /// </summary>
        public bool ConfigurationScript
        {
            get { return configurationScript; }
            set { configurationScript = value; }
        }

    }
}
