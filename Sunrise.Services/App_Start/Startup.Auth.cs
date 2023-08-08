using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Sunrise.Services.Providers;

[assembly: OwinStartup(typeof(Sunrise.Services.Startup))]

namespace Sunrise.Services
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientID { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user  
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider  
            // Configure the sign in cookie  
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/api/User/Login"),
                LogoutPath = new PathString("/Login/Index"),
                ExpireTimeSpan = TimeSpan.FromMinutes(60.0),
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow  
            PublicClientID = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/User/Login"),
                Provider = new ApplicationOAuthProvider(PublicClientID),
                //AuthorizeEndpointPath = new PathString("/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(4),
                AllowInsecureHttp = true //Don't do this in production ONLY FOR DEVELOPING: ALLOW INSECURE HTTP!  
            };

            // Enable the application to use bearer tokens to authenticate users  
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
