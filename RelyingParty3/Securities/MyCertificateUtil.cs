using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace RelyingParty3.Securities
{
    /// <summary>
    /// X509证书帮助类
    /// </summary>
    public class MyCertificateUtil
    {
        /// <summary>
        /// 当指定的存储名与区下找到与主题相关的证书
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="subjectName"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificate(StoreName name,StoreLocation location,string subjectName)
        {
            X509Store store = new X509Store(name, location);

            X509Certificate2Collection certificates = null;

            store.Open(OpenFlags.ReadOnly);

            try
            {
                certificates = store.Certificates;
                var certs = certificates.OfType<X509Certificate2>().Where(x => x.SubjectName.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase)).ToList();

                if (certs.Count == 0)
                    //未找到该名下的证书
                    throw new ApplicationException(string.Format("No certificate was found for subject Name {0}", subjectName));
                else if (certs.Count > 1)
                    //找到多个该名下的同名证书
                    throw new ApplicationException(string.Format("There are multiple certificates for subject Name {0}", subjectName));

                return new X509Certificate2(certs[0]);
            }
            finally
            {
                if (certificates != null)
                {
                    for (var i = 0; i < certificates.Count; i++)
                    {
                        var cert = certificates[i];
                        cert.Reset();
                    }
                }
                store.Close();
            }


        }
    }
}