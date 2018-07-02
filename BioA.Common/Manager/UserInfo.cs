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
            userID = -1;
            userName = string.Empty;
            userPassword = string.Empty;
            createTime = System.DateTime.Now;
            isSuperAdmin = false;
            isServeEngineer = false;
            isPermitDelete = false;
            isLocked = false;
        }

        public int UserID
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


        private int             userID;
        private string          userName;
        private string          userPassword;
        private DateTime        createTime;
        private bool            isSuperAdmin;
        private bool            isServeEngineer;
        private bool            isPermitDelete;
        private bool            isLocked;
    }
}
