
using Lib.Constants;
using Lib.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Sunrise.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Sunrise.Services.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        #region Private Properties  

        /// <summary>  
        /// Public client ID property.  
        /// </summary>  
        private readonly string _publicClientID;
        #endregion

        #region Default Constructor method.  
        /// <summary>  
        /// Default Constructor method.  
        /// </summary>  
        /// <param name="publicClientID">Public client ID parameter</param>  
        public ApplicationOAuthProvider(string publicClientID)
        {
            if (publicClientID == null)
            {
                throw new ArgumentNullException(nameof(publicClientID));
            }

            // Settings.  
            _publicClientID = publicClientID;
        }

        #endregion

        #region Grant resource owner credentials override method.  

        /// <summary>  
        /// Grant resource owner credentials overload method.  
        /// </summary>  
        /// <param name="context">Context parameter</param>  
        /// <returns>Returns when task is completed</returns>  
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var form = await context.Request.ReadFormAsync();

            // Initialization.  
            string usernameVal = context.UserName;
            string passwordVal = context.Password;
            UserController userController = new UserController();
            LoginRequest loginRequest = new LoginRequest
            {
                UserName = usernameVal,
                Password = passwordVal,
                Source = form["Source"],
                IpAddress = form["IpAddress"],
                UDID = form["UDID"],
                LoginMode = form["LoginMode"],
                DeviseType = form["DeviseType"],
                DeviceName = form["DeviceName"],
                AppVersion = form["AppVersion"],
                Location = form["Location"],
                Login = form["Login"]
            };

            LoginResponse loginResponse = userController.CheckLogin(loginRequest);
            if (loginResponse.Message.ToUpper() != "SUCCESS")
            {
                // Settings.  
                context.SetError("invalid_grant", loginResponse.Message);
                // Retuen info.  

                //Add your flag to the header of the response
                context.Response.Headers.Add(ServiceConstants.OwinChallengeFlag,
                         new[] { ((int)HttpStatusCode.Unauthorized).ToString() });

                return;
            }

            // Initialization.  
            var claims = new List<Claim>();

            
            // Setting Claim Identities for OAUTH 2 protocol.  
            ClaimsIdentity oAuthClaimIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesClaimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

            // Setting user authentication.  
            AuthenticationProperties properties = CreateProperties(loginResponse,loginRequest);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthClaimIdentity, properties);

            if (loginResponse.Message.ToUpper() == "SUCCESS")
            {
                // Grant access to authorize user.  
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesClaimIdentity);
            }
        }

        #endregion

        #region Token endpoint override method.  

        /// <summary>  
        /// Token endpoint override method  
        /// </summary>  
        /// <param name="context">Context parameter</param>  
        /// <returns>Returns when task is completed</returns>  
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                // Adding.  
                if (property.Key.ToLower().StartsWith("identity_"))
                {
                    context.Identity.AddClaim(new Claim(property.Key.Replace("Identity_",""), property.Value));
                }
                else
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }                
            }

            //Add Claim to save User ID
            var userIDProp = context.Properties.Dictionary.Where(prop => prop.Key.ToLower() == "userid").FirstOrDefault();
            context.Identity.AddClaim(new Claim(userIDProp.Key.Replace("Identity_", ""), userIDProp.Value));
            //Add Claim to save Trans ID
            var transIDProp = context.Properties.Dictionary.Where(prop => prop.Key.ToLower() == "transid").FirstOrDefault();
            context.Identity.AddClaim(new Claim(transIDProp.Key.Replace("Identity_", ""), transIDProp.Value));

            // Return info.  
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate Client authntication override method  

        /// <summary>  
        /// Validate Client authntication override method  
        /// </summary>  
        /// <param name="context">Contect parameter</param>  
        /// <returns>Returns validation of client authentication</returns>  
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.  
            if (context.ClientId == null)
            {
                // Validate Authoorization.  
                context.Validated();
            }

            // Return info.  
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate client redirect URI override method  

        /// <summary>  
        /// Validate client redirect URI override method  
        /// </summary>  
        /// <param name="context">Context parmeter</param>  
        /// <returns>Returns validation of client redirect URI</returns>  
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            // Verification.  
            if (context.ClientId == _publicClientID)
            {
                // Initialization.  
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                // Verification.  
                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    // Validating.  
                    context.Validated();
                }
            }

            // Return info.  
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Create Authentication properties method.  

        /// <summary>  
        /// Create Authentication properties method.  
        /// </summary>  
        /// <param name="loginResponse">Login Response parameter</param>  
        /// <returns>Returns authenticated properties.</returns>  
        public static AuthenticationProperties CreateProperties(LoginResponse loginResponse, LoginRequest loginRequest)
        {
            // Settings.  
            IDictionary<string, string> data = loginResponse.GetType()
                                                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                            .ToDictionary(prop => prop.Name, prop => Convert.ToString(prop.GetValue(loginResponse, null)));

            IDictionary<string, string> reqdata = loginRequest.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => "Identity_" + prop.Name, prop => Convert.ToString(prop.GetValue(loginRequest, null)));
            foreach (KeyValuePair<string,string> reqItem in reqdata)
            {
                data.Add(reqItem.Key, reqItem.Value);
            }

            // Return info.  
            return new AuthenticationProperties(data);
        }

        #endregion

    }
}