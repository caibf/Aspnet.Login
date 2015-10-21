﻿using System;
using System.Security.Principal;
using System.Web.Script.Serialization;

namespace FormsAuthWeb
{
    /// <summary>
    /// 如果用户信息的类型不需要用户组的判断，可以不实现IPrincipal接口。
    /// </summary>
    public class UserInfo : IPrincipal
    {
        public int UserId;
        public int GroupId;
        public string UserName;

        // 如果还有其它的用户信息，可以继续添加。

        public override string ToString()
        {
            return string.Format("UserId: {0}, GroupId: {1}, UserName: {2}, IsAdmin: {3}",
                UserId, GroupId, UserName, IsInRole("Admin"));
        }

        #region IPrincipal Members

        [ScriptIgnore]
        public IIdentity Identity
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsInRole(string role)
        {
            if (string.Compare(role, "Admin", true) == 0)
                return GroupId == 1;
            else
                return GroupId > 0;
        }

        #endregion
    }
}