using System;
using System.Collections.Generic;
using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SingleSignOn.Security
{
    /// <summary>
    /// 自定义安全令牌服务配置类
    /// </summary>
    public class CustomSecurityTokenServiceConfiguration : SecurityTokenServiceConfiguration
    {
        
        /// <summary>
        /// 加锁对象
        /// </summary>
        private static readonly object synRoot = new object();

        private const string CustomSecurityTokenServiceConfigurationKey = "CustomSecurityTokenServiceConfigurationKey";

        public CustomSecurityTokenServiceConfiguration()
            : base(WebConfigurationManager.AppSettings["IssuerName"])
        {
            //设置安全令牌服务类
            this.SecurityTokenService = typeof(CustomSecurityTokenService);
        }


        public static CustomSecurityTokenServiceConfiguration Current
        {
            get
            {
                HttpApplicationState app = HttpContext.Current.Application;

                if (app.Get(CustomSecurityTokenServiceConfigurationKey) is CustomSecurityTokenServiceConfiguration config)
                    return config;

                //双检锁,获取临界区
                lock (synRoot)
                {

                    config = app.Get(CustomSecurityTokenServiceConfigurationKey) as CustomSecurityTokenServiceConfiguration;
                    //类型模式匹配
                    //if(app.Get(CustomSecurityTokenServiceConfigurationKey) is CustomSecurityTokenServiceConfiguration config)
                    if (config == null)
                    {
                        config = new CustomSecurityTokenServiceConfiguration();
                        //提取证书
                        var signingCredentials = new X509SigningCredentials(CustomSecurityTokenService.GetCertificate(WebConfigurationManager.AppSettings["SigningCertificateName"]));
                        //设置证书
                        config.SigningCredentials = signingCredentials;
                        //加入缓存
                        app.Add(CustomSecurityTokenServiceConfigurationKey, config);

                    }

                    return config;
                }
            }
        }
    }
}