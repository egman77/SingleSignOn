using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelyingParty3.Securities
{
    /// <summary>
    /// This class encrypts the session security token using the RSA key
    /// of the relying party's service certificate.
    /// </summary>
    public class RsaEncryptedSessionSecurityTokenHandler: SessionSecurityTokenHandler
    {

    }
}