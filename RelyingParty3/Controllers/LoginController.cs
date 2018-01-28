using Microsoft.IdentityModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelyingParty3.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            //如果没有登录
            if (!User.Identity.IsAuthenticated)
            {
                //发出验证请求, 中间为返回地址
                //FederatedAuthentication.WSFederationAuthenticationModule.RedirectToIdentityProvider("customsts.dev", "http://localhost:26756/user", true);
                // FederatedAuthentication.WSFederationAuthenticationModule.RedirectToIdentityProvider("MyAsk.dev", "http://localhost:26756/user", true);

                //RedirectToIdentityProvider与 Signin都可行
                // FederatedAuthentication.WSFederationAuthenticationModule.SignIn("xxx");
                FederatedAuthentication.WSFederationAuthenticationModule.RedirectToIdentityProvider("MyAsk.dev", "http://localhost:26758/user", true);
            }
           
            return View();
        }
    }
}
