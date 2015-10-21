using FishDemoCodeLib;
using System.Web.Security;
using System.Web.UI;

namespace FormsAuthWeb
{
    public partial class Default : System.Web.UI.Page
    {
        [SubmitMethod(AutoRedirect = true)]
        public void NormalLogin()
        {
            string loginName = Request.Form["loginName"];
            if (string.IsNullOrEmpty(loginName))
                return;

            FormsAuthentication.SetAuthCookie(loginName, true);
            bool test = Page.Response.IsRequestBeingRedirected;
            TryRedirect();
        }

        [SubmitMethod(AutoRedirect = true)]
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        private void TryRedirect()
        {
            string returnUrl = Request.QueryString["ReturnUrl"];
            if (string.IsNullOrEmpty(returnUrl) == false)
                Response.Redirect(returnUrl);
        }

        [SubmitMethod(AutoRedirect = true)]
        public void CustomizeLogin()
        {
            // -----------------------------------------------------------------
            // 注意：演示代码为了简单，这里不检查用户名与密码是否正确。
            // -----------------------------------------------------------------

            string loginName = Request.Form["loginName"];
            if (string.IsNullOrEmpty(loginName))
                return;

            UserInfo userinfo = new UserInfo();
            int.TryParse(Request.Form["UserId"], out userinfo.UserId);
            int.TryParse(Request.Form["GroupId"], out userinfo.GroupId);
            userinfo.UserName = Request.Form["UserName"];

            // 登录状态100分钟内有效
            MyFormsPrincipal<UserInfo>.SignIn(loginName, userinfo, 100);

            TryRedirect();
        }
    }
}