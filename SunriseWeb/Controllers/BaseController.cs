using SunriseWeb.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SunriseWeb.Controllers
{
    public class BaseController : Controller
    {

        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var reqCookies = HttpContext.Request.Cookies["language"];

            var language = string.Empty;
            if (reqCookies != null)
            {
                language = reqCookies.Value;
            }
            else{
                language = "en";
            }
            LanguageProvider.setCultureLanguage(language);

            base.OnActionExecuting(filterContext);
        }
    }
}