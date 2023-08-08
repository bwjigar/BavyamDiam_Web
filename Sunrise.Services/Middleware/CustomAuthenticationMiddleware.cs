using Lib.Constants;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sunrise.Services.Middleware
{
    public class CustomAuthenticationMiddleware : OwinMiddleware
    {
        public CustomAuthenticationMiddleware(OwinMiddleware next)
        : base(next) { }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);

            if (context.Response.StatusCode == 400
                && context.Response.Headers.ContainsKey(
                          ServiceConstants.OwinChallengeFlag))
            {
                var headerValues = context.Response.Headers.GetValues
                      (ServiceConstants.OwinChallengeFlag);

                context.Response.StatusCode =
                       Convert.ToInt16(headerValues.FirstOrDefault());

                context.Response.Headers.Remove(
                       ServiceConstants.OwinChallengeFlag);
            }

        }
    }
}