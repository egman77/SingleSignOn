using SingleSignOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SingleSignOn.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            LoginModel loginModel = new LoginModel() {
                Username="abc",
                Password="123"
            };
            return View(loginModel);
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //模拟IP验证
                if (loginModel.Username == "abc" && loginModel.Password == "123")
                {
                    //如果有IP--Policy转换,而在此进行
                    FormsAuthentication.SetAuthCookie($"{loginModel.Username}@qq.com|1983-10-23|张三", true);
                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "The username or password provided is incorrect.");
                }
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(loginModel);
        }
    }
}
