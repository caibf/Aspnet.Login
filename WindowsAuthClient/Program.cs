using System;
using System.IO;
using System.Net;

namespace WindowsAuthClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 请把WindowsAuthWeb这个网站部署在IIS中，
                // 开启Windows身份认证方式，并禁止匿名用户访问。
                // 然后修改下面的访问地址。
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8085/Default.aspx");

                // 下面三行代码，启用任意一行都是可以的。
                request.UseDefaultCredentials = true;
                //request.Credentials = CredentialCache.DefaultCredentials;
                //request.Credentials = CredentialCache.DefaultNetworkCredentials;
                // 如果上面的三行代码全被注释了，那么将会看到401的异常信息。

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
            }
            catch (WebException wex)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("异常发生了。");
                Console.WriteLine("=====================================");
                Console.WriteLine(wex.Message);
            }

            Console.ReadKey();
        }




        //// 获取或设置请求的身份验证信息。
        ////
        //// 返回结果:
        ////     包含与该请求关联的身份验证凭据的 System.Net.ICredentials。默认为 null。
        //public override ICredentials Credentials { get; set; }


        //// 获取或设置一个 System.Boolean 值，该值控制默认凭据是否随请求一起发送。
        ////
        //// 返回结果:
        ////     如果使用默认凭据，则为 true；否则为 false。默认值为 false。
        //public override bool UseDefaultCredentials { get; set; }
    }
}
