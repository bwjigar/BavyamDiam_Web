using SunriseWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SunriseWeb.Filter
{
    public class AuthorizeActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                if (session != null && SessionFacade.UserSession == null)
                {
                    Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                    string action = String.Format("{3}", url.Scheme, Uri.SchemeDelimiter, url.Authority, url.AbsolutePath);

                    if (action != "/Api/URL" && action != "/Api/DownloadAPIData" && action != "/DNA/StoneDetail")
                    {
                        filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                }
                else if (SessionFacade.UserSession != null)
                {
                    string cntlr = filterContext.RouteData.Values["controller"].ToString();
                    string act = filterContext.RouteData.Values["action"].ToString();

                    if (SessionFacade.UserSession.IsLogout == true)
                    {
                        if (!((cntlr == "Dashboard" && act == "Index") ||
                            (cntlr == "Dashboard" && act == "GetLastLoggedin") ||
                            (cntlr == "Dashboard" && act == "GetDashboardCount") ||
                            (cntlr == "Dashboard" && act == "GetYearData") ||
                            (cntlr == "Dashboard" && act == "GetOrderSummaryChartData") ||
                            (cntlr == "Dashboard" && act == "GetDynamicChartData") ||
                            (cntlr == "User" && act == "Get_MessageMst") ||
                            (cntlr == "User" && act == "NotifyGet_User") ||
                            (cntlr == "SearchStock" && act == "Dashboard_GetSavedSearchList") ||
                            (cntlr == "SearchStock" && act == "Dashboard_GetRecentSearchList") ||
                            (cntlr == "SearchStock" && act == "GetDynamicChartData")))
                        {
                            filterContext.Result = new RedirectResult("~/Login/Index");
                        }
                    }
                    if (cntlr == "Information" && act == "Index")
                    { 
                        if (SessionFacade.UserSession.isadmin != 1)
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if (cntlr == "Information" && act == "Customer")
                    {
                        if (SessionFacade.UserSession.iUserType != 3)
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if ((cntlr == "LabStock" && act == "Index") || (cntlr == "LabStock" && act == "LabByRequest") || (cntlr == "LabStock" && act == "LabByRequestCart"))
                    {
                        if (!(SessionFacade.UserSession.isadmin == 1 || SessionFacade.UserSession.iUserid == 9 || SessionFacade.UserSession.iUserid == 10 
                            || SessionFacade.UserSession.iUserid == 15 || SessionFacade.UserSession.iUserid == 39 || SessionFacade.UserSession.iUserid == 41
                            || SessionFacade.UserSession.iUserid == 2922 || SessionFacade.UserSession.iUserid == 2003 || SessionFacade.UserSession.iUserid == 2528))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if (cntlr == "Notification" && act == "Index")
                    {
                        if (SessionFacade.UserSession.isadmin != 1)
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if (cntlr == "UserActivity" && act == "UserActivity")
                    {
                        if (SessionFacade.UserSession.isadmin != 1)
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    //else if (cntlr == "Offer" && act == "OfferHistory")
                    //{
                    //    if (SessionFacade.UserSession.isadmin != 1)
                    //        filterContext.Result = new RedirectResult("~/Login/Index");
                    //}
                    else if (cntlr == "User" && act == "UserList")
                    {
                        if (SessionFacade.UserSession.isadmin != 1)
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if (cntlr == "LoginInfo" && act == "LoginInfo")
                    {
                        if (SessionFacade.UserSession.isadmin != 1)
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if (cntlr == "Customer" && act == "CustomerDisc")
                    {
                        if (!(SessionFacade.UserSession.isadmin == 1 || SessionFacade.UserSession.isemp == 1))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if ((cntlr == "ConfirmOrder" && act == "ConfirmOrderHistory") || (cntlr == "ConfirmOrder" && act == "SupplierOrderLog"))
                    {
                        if (!((SessionFacade.UserSession.isadmin == 1 || SessionFacade.UserSession.iUserid == 9 ||
                            SessionFacade.UserSession.iUserid == 10 || SessionFacade.UserSession.iUserid == 15 ||
                            SessionFacade.UserSession.iUserid == 39) &&
                            SessionFacade.UserSession.sUsername.ToUpper() != "SUN_TEST"))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if (cntlr == "LabStock" && act == "LabStockAPI")
                    {
                        if (!(SessionFacade.UserSession.iUserid == 1 || SessionFacade.UserSession.iUserid == 8 || 
                            SessionFacade.UserSession.iUserid == 10 || SessionFacade.UserSession.iUserid == 2003 || 
                            SessionFacade.UserSession.iUserid == 5682 || SessionFacade.UserSession.iUserid == 41))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if (cntlr == "User" && act == "MessageMst")
                    {
                        if (!(SessionFacade.UserSession.iUserType == 1))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if (cntlr == "Order" && act == "HoldStoneReport")
                    {
                        if (!(SessionFacade.UserSession.iUserType == 1 || SessionFacade.UserSession.iUserType == 2))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}