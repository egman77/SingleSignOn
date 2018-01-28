using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace RelyingParty2.Securities
{
    public class MySampleRequestValidator:RequestValidator
    {
        protected override bool IsValidRequestString(
            HttpContext context,
            string value,
            RequestValidationSource requestValidationSource,
            string collectionKey,
            out int validationFailureIndex)
        {
            validationFailureIndex = 0;

            if(requestValidationSource==RequestValidationSource.Form&&
                collectionKey.Equals("wresult", StringComparison.Ordinal))
            {
                //创建登录消息
                //SignInResponseMessage message = WSFederationMessage.CreateFromFormPost(context.Request) as SignInResponseMessage;


                var hcw = new HttpContextWrapper(context);
                 //只要是WSFed的消息都算合法验证
                 WSFederationMessage message = WSFederationMessage.CreateFromFormPost(hcw.Request);

                if (message != null)
                    return true;
            }

            return base.IsValidRequestString(context, value, requestValidationSource, collectionKey, out validationFailureIndex);
        }
    }
}