using SingleSignOn.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Configuration;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SingleSignOn.Controllers
{
    public class HomeController : Controller
    {
        public const string Action = "wa";
        public const string SignIn = "wsignin1.0";
        public const string SignOut = "wsignout1.0";

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                var action = Request.QueryString[Action];

                //如果是登录请求
                if (action == SignIn)
                {
                    //转登录处理
                    var formData = ProcessSignIn(Request.Url, (ClaimsPrincipal)User);
                    return new ContentResult() { Content = formData, ContentType = "text/html" };
                }
                //如果是注销请求
                else if (action == SignOut)
                {
                    //转注销处理
                    ProcessSignOut(Request.Url, (ClaimsPrincipal)User, (HttpResponse)HttpContext.Items["HttpResponse"]);
                }
            }

            return View();
        }

        /// <summary>
        /// 登录请求处理
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string ProcessSignIn(Uri url, ClaimsPrincipal user)
        {
            //创建登录请求消息
            var requestMessage = (SignInRequestMessage)WSFederationMessage.CreateFromUri(url);
            //提取证书
          //  var signingCredentials = new X509SigningCredentials(CustomSecurityTokenService.GetCertificate(ConfigurationManager.AppSettings["SigningCertificateName"]));

            // Cache?
            //创建令牌服务配置类
            // var config = new SecurityTokenServiceConfiguration(ConfigurationManager.AppSettings["IssuerName"], signingCredentials);
            ////实例化自定义令牌服务类
            //var sts = new CustomSecurityTokenService(config);
            var sts = CustomSecurityTokenServiceConfiguration.Current.CreateSecurityTokenService(); 
            
            //交给真正的登录处理方法(颁发令牌)
            var responseMessage = FederatedPassiveSecurityTokenServiceOperations.ProcessSignInRequest(requestMessage, user, sts);
            //得到回复结果
            return responseMessage.WriteFormPost();
        }

        /// <summary>
        /// 注销请求处理
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="user"></param>
        /// <param name="response"></param>
        private static void ProcessSignOut(Uri uri, ClaimsPrincipal user, HttpResponse response)
        {
            // Prepare url to internal logout page (which signs-out of all relying parties).
            string url = uri.OriginalString;
            //int index = url.IndexOf("&wreply=");
            //if (index != -1)
            //{
            //    index += 8;
            //    string baseUrl = url.Substring(0, index);
            //    string wreply = url.Substring(index, url.Length - index);

            //    // Get the base url (domain and port).
            //    string strPathAndQuery = uri.PathAndQuery;
            //    string hostUrl = uri.AbsoluteUri.Replace(strPathAndQuery, "/");

            //    wreply = HttpUtility.UrlEncode(hostUrl + "logout?wreply=" + wreply);

            //    url = baseUrl + wreply; //目的是为了返回STS的logout页面
            //}

            // Redirect user to logout page (which signs out of all relying parties and redirects back to originating relying party).
            uri = new Uri(url);
            //准备注销的请求消息
            var requestMessage = (SignOutRequestMessage)WSFederationMessage.CreateFromUri(uri);
            //交给真正的注销处理方法(注销令牌)
            FederatedPassiveSecurityTokenServiceOperations.ProcessSignOutRequest(requestMessage, user, requestMessage.Reply, response);
        }
    }
}
