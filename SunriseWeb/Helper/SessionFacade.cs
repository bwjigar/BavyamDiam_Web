using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunriseWeb.Helper
{
    public static class SessionFacade
    {
        #region Private Constants
        private const string User = "UserSession";
        private const string Token = "TokenNo";
        #endregion

        #region Private Static Member Variables
        private static HttpContext thisContext;
        #endregion

        #region Public Static Methods
        ///
        /// Clears Session
        ///

        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }
        ///
        /// Abandons Session
        ///

        public static void Abandon()
        {
            ClearSession();
            HttpContext.Current.Session.Abandon();
        }
        #endregion
        #region Public Static Properties
        ///
        /// Gets/Sets Session for UserId
        ///

        public static KeyAccountDataResponse UserSession
        {
            get
            {
                if (HttpContext.Current.Session[User] == null)
                {
                    Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                    string action = String.Format("{3}", url.Scheme, Uri.SchemeDelimiter, url.Authority, url.AbsolutePath);

                    if (action != "/Api/URL" && action != "/Api/DownloadAPIData" && action != "/DNA/StoneDetail")
                    { 
                        HttpContext.Current.Response.Redirect("~/Login"); 
                    }

                    return null;
                }
                else
                    return (KeyAccountDataResponse)HttpContext.Current.Session[User];
            }
            set { HttpContext.Current.Session[User] = value; }
        }

        public static string TokenNo
        {
            get
            {
                if (HttpContext.Current.Session[Token] == null)
                    return null;
                else
                    return (string)HttpContext.Current.Session[Token];
            }
            set { HttpContext.Current.Session[Token] = value; }
        }
        #endregion
    }
}