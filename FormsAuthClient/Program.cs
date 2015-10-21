using FishDemoCodeLib;
using System;
using System.Net;

namespace FormsAuthClient
{
    class Program
    {
        private static readonly string LoginUrl = "http://localhost:33007/default.aspx";
        private static readonly string MyInfoPageUrl = "http://localhost:33007/MyInfo.aspx";
        
        //static void Main(string[] args)
        //{
        //    HttpWebRequest request = MyHttpClient.CreateHttpWebRequest(MyInfoPageUrl);
        //    string html = MyHttpClient.GetResponseText(request);

        //    if (html.IndexOf("<span>cbfjay</span>") > 0)
        //        Console.WriteLine("调用成功。");
        //    else
        //        Console.WriteLine("页面结果不符合预期。");
        //}

        static void Main(string[] args)
        {
            CookieContainer cookieContainer = new CookieContainer();

            MyHttpClient.HttpPost(LoginUrl, "loginName=cbfjay&NormalLogin=autoTest", cookieContainer);
            string html = MyHttpClient.HttpGet(MyInfoPageUrl, cookieContainer);

            if (html.IndexOf("<span>cbfjay</span>") > 0)
                Console.WriteLine("调用成功。");
            else
                Console.WriteLine("页面结果不符合预期。");
        }
    }
}
