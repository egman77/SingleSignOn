using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelyingParty1.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {

            //发出验证请求, 中间为返回地址
             //FederatedAuthentication.WSFederationAuthenticationModule.RedirectToIdentityProvider("customsts.dev", "http://localhost:26756/user", true);
            FederatedAuthentication.WSFederationAuthenticationModule.RedirectToIdentityProvider("MyAsk.dev", "http://localhost:26756/user", true);
            return View();
        }
    }
}
