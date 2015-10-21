using System;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace WindowsAuthWeb
{
    /// <summary>
    /// ShowWindowsIdentity 的摘要说明
    /// </summary>
    public class ShowWindowsIdentity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // 要观察【模拟】的影响，
            // 可以[启用/禁止]web.config中的设置：<identity impersonate="true"/>

            context.Response.ContentType = "text/plain";

            context.Response.Write(Environment.UserDomainName + "\\" + Environment.UserName + "\r\n");

            WindowsPrincipal winPrincipal = (WindowsPrincipal)HttpContext.Current.User;
            context.Response.Write(string.Format("HttpContext.Current.User.Identity: {0}, {1}\r\n",
                winPrincipal.Identity.AuthenticationType, winPrincipal.Identity.Name));

            WindowsPrincipal winPrincipal2 = (WindowsPrincipal)Thread.CurrentPrincipal;
            context.Response.Write(string.Format("Thread.CurrentPrincipal.Identity: {0}, {1}\r\n",
                winPrincipal2.Identity.AuthenticationType, winPrincipal2.Identity.Name));

            // WindowsIdentity.GetCurrent获取的是当前运行的Win32线程的安全上下文标识，
            // 而ASP.NET线程的安全标识其实是从IIS的进程中继承的，
            // 这与HttpContext.Current.User.Identity得到的对象是不同的
            // 为此ASP.NET提供了“模拟”功能，允许线程以特定的Windows帐户的安全上下文来访问资源
            // 模拟只是在ASP.NET应用程序访问Windows系统资源时需要应用Windows的安全检查功能才会有用
            WindowsIdentity winId = WindowsIdentity.GetCurrent();
            context.Response.Write(string.Format("WindowsIdentity.GetCurrent(): {0}, {1}",
                winId.AuthenticationType, winId.Name));
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