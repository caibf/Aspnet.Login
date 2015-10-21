using System.DirectoryServices;
using System.Management;
using System.Web;

namespace WindowsAuthWeb.Common
{
    public class UserHelper
    {
        /// <summary>
        /// 活动目录中的搜索路径，也可根据实际情况来修改这个值。
        /// </summary>
        public static string DirectoryPath = "LDAP://" + GetDomainName();

        /// <summary>
        /// 获取与指定HttpContext相关的用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <remarks>
        /// 在Windows身份认证中，验证工作主要是由IIS实现的，
        /// WindowsAuthenticationModule其实只是负责创建WindowsPrincipal和WindowsIdentity而已。
        /// </remarks>
        /// <returns></returns>
        public static UserInfo GetCurrentUserInfo(HttpContext context)
        {
            string loginName = GetUserLoginName(context);
            if (string.IsNullOrEmpty(loginName))
                return null;

            return GetUserInfoByLoginName(loginName);
        }

        /// <summary>
        /// 根据指定的HttpContext对象，获取登录名。
        /// </summary>
        /// <param name="context"></param>
        /// <remarks>
        /// Environment.UserName是使用WIN32API中的GetUserName获取线程相关的用户名，
        /// 但ASP.NET运行在IIS中，线程相关的用户名就不一定是客户端的用户名了
        /// </remarks>
        /// <returns></returns>
        public static string GetUserLoginName(HttpContext context)
        {
            if (context == null)
                return null;

            if (context.Request.IsAuthenticated == false)
                return null;

            // User是一个WindowsPrincipal类型的对象
            string userName = context.User.Identity.Name;
            // 此时userName的格式为：UserDomainName\LoginName
            // 我们只需要后面的LoginName就可以了。

            string[] array = userName.Split(new char[] { '\\' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 2)
                return array[1];

            return null;
        }

        /// <summary>
        /// 根据登录名查询活动目录，获取用户信息。
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfoByLoginName(string loginName)
        {
            if (string.IsNullOrEmpty(loginName))
                return null;

            // 下面的代码将根据登录名查询用户在AD中的信息。
            // 为了提高性能，可以在此处增加一个缓存容器(Dictionary or Hashtable)。

            try
            {
                DirectoryEntry entry = new DirectoryEntry(DirectoryPath);
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + loginName + ")";

                search.PropertiesToLoad.Add("givenName");
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("mail");
                // 如果还需要从AD中获取其它的用户信息，请参考ActiveDirectoryDEMO

                SearchResult result = search.FindOne();

                if (result != null)
                {
                    UserInfo info = new UserInfo();
                    info.GivenName = result.Properties["givenName"][0].ToString();
                    info.FullName = result.Properties["cn"][0].ToString();
                    info.Email = result.Properties["mail"][0].ToString();
                    return info;
                }
            }
            catch
            {
                // 如果需要记录异常，请在此处添加代码。
            }
            return null;
        }


        private static string GetDomainName()
        {
            // 注意：这段代码需要在Windows XP及较新版本的操作系统中才能正常运行。
            SelectQuery query = new SelectQuery("Win32_ComputerSystem");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    if ((bool)mo["partofdomain"])
                        return mo["domain"].ToString();
                }
            }
            return null;
        }
    }
}