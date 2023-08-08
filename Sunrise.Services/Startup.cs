using Microsoft.Owin;
using Owin;
using Sunrise.Services.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Sunrise.Services.Startup))]
namespace Sunrise.Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            app.Use<CustomAuthenticationMiddleware>();
            ConfigureAuth(app);
        }
    }
}