using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;

namespace SunriseWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(name: "newRoute",
            //    url: "Mvctest/{StoneNo}",
            //    defaults: new { controller = "CommonWithoutLogin", action = "StoneDetail", StoneNo = UrlParameter.Optional }
            //);
            //routes.MapRoute(
            //    name: "hardik",
            //    url: "hardik/show/{StoneNo}",
            //    defaults: new { controller = "CommonWithoutLogin", action = "StoneDetail"
            //    }
            //);
        }
    }
}
