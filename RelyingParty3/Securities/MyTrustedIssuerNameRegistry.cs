using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;

namespace RelyingParty3.Securities
{
    public class MyTrustedIssuerNameRegistry : Microsoft.IdentityModel.Tokens.IssuerNameRegistry
    {
        /// <summary>
        /// 根据已获取的安全令牌中提取出发布者名
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException">在没有找到可信任的发行者名称时抛出此异常</exception>
        public override string GetIssuerName(SecurityToken securityToken)
        {
            if (securityToken is X509SecurityToken x509token)
            {
                if (String.Equals(x509token.Certificate.SubjectName.Name, "CN=localhost"))
                {
                    return x509token.Certificate.SubjectName.Name;
                }
            }

            throw new SecurityTokenException("未找到可信任的发行者名称");
        }
    }
}