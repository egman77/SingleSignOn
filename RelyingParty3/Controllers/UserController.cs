using Microsoft.IdentityModel.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelyingParty3.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            //// Load Identity Configuration
            //FederationConfiguration config = FederatedAuthentication.FederationConfiguration;

           // FederatedAuthentication.ServiceConfiguration.

            var uri = new Uri("http://localhost:26758/");

            //// Sign out of WIF.
            WSFederationAuthenticationModule.FederatedSignOut(new Uri(ConfigurationManager.AppSettings["ida:Issuer"]), uri);

            return View();
        }
    }
}
