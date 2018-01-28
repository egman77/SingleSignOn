using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Web;
using Microsoft.IdentityModel.Web.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace RelyingParty3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            //FederatedAuthentication.ServiceConfigurationCreated += OnServiceConfigurationCreated;

          
        }

        ///// <summary>
        ///// By default, WIF uses DPAPI to encrypt token.
        ///// But DPAPI is not supported in Windows Azure.
        ///// So we use a certificate instead.
        ///// </summary>
        //void OnServiceConfigurationCreated(object sender, ServiceConfigurationCreatedEventArgs e)
        //{
        //    List<CookieTransform> sessionTransforms = new List<CookieTransform>(new CookieTransform[]
        //    {
        //        new DeflateCookieTransform(),
        //        new RsaEncryptionCookieTransform(e.ServiceConfiguration.ServiceCertificate),
        //        new RsaSignatureCookieTransform(e.ServiceConfiguration.ServiceCertificate)
        //    });
        //    SessionSecurityTokenHandler sessionHandler = new SessionSecurityTokenHandler(sessionTransforms.AsReadOnly());
        //    e.ServiceConfiguration.SecurityTokenHandlers.AddOrReplace(sessionHandler);
        //}

        //void OnServiceConfigurationCreated(object sender, FederationConfigurationCreatedEventArgs e)
        //{
        //    // Change cookie encryption type from DPAPI to RSA. This avoids a security exception due to a cookie size limit with the SSO cookie. See http://fabriccontroller.net/blog/posts/key-not-valid-for-use-in-specified-state-exception-when-working-with-the-access-control-service/
        //    var sessionTransforms = new List<CookieTransform>(new CookieTransform[] {
        //        new DeflateCookieTransform(),
        //        new RsaEncryptionCookieTransform(e.FederationConfiguration.ServiceCertificate),
        //        new RsaSignatureCookieTransform(e.FederationConfiguration.ServiceCertificate)
        //    });

        //    var sessionHandler = new SessionSecurityTokenHandler(sessionTransforms.AsReadOnly());
        //    e.FederationConfiguration.IdentityConfiguration.SecurityTokenHandlers.AddOrReplace(sessionHandler);
        //}
    }
}