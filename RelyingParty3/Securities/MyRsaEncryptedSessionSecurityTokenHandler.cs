using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace RelyingParty3.Securities
{
    /// <summary>
    /// This class encrypts the session security token using the RSA key
    /// of the relying party's service certificate.
    /// </summary>
    public class MyRsaEncryptedSessionSecurityTokenHandler: SessionSecurityTokenHandler
    {
        static List<CookieTransform> _transforms;
        static MyRsaEncryptedSessionSecurityTokenHandler()
        {
            X509Certificate2 serviceCertificate = MyCertificateUtil.GetCertificate(
            StoreName.My,
            StoreLocation.LocalMachine, "CN=localhost");

            _transforms = new List<CookieTransform>()
            {
                new DeflateCookieTransform(),
                new RsaEncryptionCookieTransform(serviceCertificate),
                new RsaSignatureCookieTransform(serviceCertificate)
            };
        }

       /// <summary>
       /// 
       /// </summary>
        public MyRsaEncryptedSessionSecurityTokenHandler()
            :base(_transforms.AsReadOnly())
        {

        }
        
    }
}