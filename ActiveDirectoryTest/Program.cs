using System;
using System.DirectoryServices;
using System.Management;

namespace ActiveDirectoryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.UserDomainName);
            Console.WriteLine(Environment.UserName); // 当前用户的登录名
            Console.WriteLine(GetDomainName());
            Console.WriteLine("------------------------------------------------");

            //ShowUserInfo(Environment.UserName, GetDomainName());

            // Ctrl+F5开始执行不调试：是有“请按任意键继续...”的
            Console.ReadKey();
        }

        #region AllProperties
        private static string AllProperties = "name,givenName,samaccountname,mail";
        // LDAP支持的属性
        /*
        private static string AllProperties = @"
homemdb
distinguishedname
countrycode
cn
lastlogoff
mailnickname
dscorepropagationdata
msexchhomeservername
msexchmailboxsecuritydescriptor
msexchalobjectversion
usncreated
objectguid
whenchanged
memberof
msexchuseraccountcontrol
accountexpires
displayname
primarygroupid
badpwdcount
objectclass
instancetype
objectcategory
samaccounttype
whencreated
lastlogon
useraccountcontrol
physicaldeliveryofficename
samaccountname
usercertificate
givenname
mail
userparameters
adspath
homemta
msexchmailboxguid
pwdlastset
logoncount
codepage
name
usnchanged
legacyexchangedn
proxyaddresses
department
userprincipalname
badpasswordtime
objectsid
sn
mdbusedefaults
telephonenumber
showinaddressbook
msexchpoliciesincluded
textencodedoraddress
lastlogontimestamp
company
";*/
		#endregion

        public static void ShowUserInfo(string loginName, string domainName)
        {
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(domainName))
                return;

            string[] properties = AllProperties.Split(new char[] { '\r', '\n', ',' }, 
                                    StringSplitOptions.RemoveEmptyEntries);
            try
            {
                // 使用DirectoryEntry和DirectorySearcher从托管代码中访问 Active Directory 域服务
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + domainName);
                DirectorySearcher search = new DirectorySearcher(entry);
                // 指示LDAP格式筛选器
                search.Filter = "(samaccountname=" + loginName + ")";

                foreach (var p in properties)
                    search.PropertiesToLoad.Add(p); // 设置搜索过程中要检索的属性列表

                SearchResult result = search.FindOne();
                if (result != null)
                {
                    foreach (var p in properties)
                    {
                        ResultPropertyValueCollection collection = result.Properties[p];
                        for (var i = 0; i < collection.Count; i++)
                            Console.WriteLine(p + ":" + collection[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string GetDomainName()
        {
            // 注意：这段代码需要在Windows XP及较新版本的操作系统中才能正常运行。
            // 这个域名不一定与System.Environment.UserDomainName相同
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
