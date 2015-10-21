using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace FormsAuthWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // 为了能让上面的页面代码发挥工作，必须在页面显示前重新设置HttpContext.User对象。
            HttpApplication app = sender as HttpApplication;
            MyFormsPrincipal<UserInfo>.TrySetUserInfo(app.Context);
        }

        // 也可以参考下面的方法。
        //protected void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs e)
        //{
        //    // 这种方法将不检查ticket.Expiration是否已过期。
        //    // 如果采用上面的方法，FormsAuthenticationModule将会处理ticket过期问题。
        //    // 不过这种方法可以只调用FormsAuthentication.Decrypt()一次。

        //    e.User = MyFormsPrincipal<UserInfo>.TryParsePrincipal(e.Context.Request);
        //}

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}