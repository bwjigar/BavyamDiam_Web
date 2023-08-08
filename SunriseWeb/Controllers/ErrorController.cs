using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunriseWeb.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Error404()
        //{
        //    return View();
        //}
        public ActionResult Http404()
        {
            return View("Http404");
        }
    }
}