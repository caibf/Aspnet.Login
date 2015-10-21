using System;
using System.Diagnostics;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace FormsAuthWeb
{
    /// <summary>
    /// 测试FormsAuthentication.Decrypt的性能
    /// </summary>
    public class TestFormsAuthenticationDecrypt : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            UserInfo userinfo = new UserInfo { UserName = "Xlive1991", UserId = 78, GroupId = 1 };
            string json = new JavaScriptSerializer().Serialize(userinfo);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                2, "cbfjay", DateTime.Now, DateTime.Now.AddDays(1), true, json);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            FormsAuthenticationTicket ticket2 = null;

            Stopwatch watch =Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
                ticket2 = FormsAuthentication.Decrypt(encryptedTicket);
            watch.Stop();

            context.Response.Write(watch.Elapsed.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}