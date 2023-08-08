using SunriseWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SunriseWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //void Session_Start(object sender, EventArgs e)
        //{
        //    Session.Timeout = 20; //minutes
        //}
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            HttpException httpException = exception as HttpException;

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        routeData.Values.Add("action", "Http404");
                        Response.Clear();
                        Server.ClearError();
                        Response.TrySkipIisCustomErrors = true;

                        IController errorController = new ErrorController();
                        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                        break;
                }
            }
        }
    }
}
