using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using SunriseWeb.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        API _api = new API();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult StockDownload(string StoneID="", string UsedFor ="")
        {
            //var UserID = SessionFacade.UserSession.iUserid;
            var input = new
            {
                StoneID = StoneID,
                CertiNo = "",
                Shape = "",
                Pointer = "",
                Color = "",
                Clarity = "",
                Cut = "",
                Polish = "",
                Symm = "",
                Fls = "",
                Lab = "",
                Inclusion = "",
                Natts = "",
                Shade = "",
                FromCts = "",
                ToCts = "",
                FormDisc = "",
                ToDisc = "",
                FormPricePerCts = "",
                ToPricePerCts = "",
                FormNetAmt = "",
                ToNetAmt = "",
                FormDepth = "",
                ToDepth = "",
                FormLength = "",
                ToLength = "",
                FormWidth = "",
                ToWidth = "",
                FormDepthPer = "",
                ToDepthPer = "",
                FormTablePer = "",
                ToTablePer = "",
                HasImage = "",
                HasHDMovie = "",
                IsPromotion = "",
                CrownInclusion = "",
                CrownNatts = "",
                Luster = "",
                Location = "",
                PageNo = "",
                TokenNo = "",
                StoneStatus = "",
                FromCrownAngle = "",
                ToCrownAngle = "",
                FromCrownHeight = "",
                ToCrownHeight = "",
                FromPavAngle = "",
                ToPavAngle = "",
                FromPavHeight = "",
                ToPavHeight = "",
                BGM = "",
                Black = "",
                SmartSearch = "",
                keytosymbol = "",
                Reviseflg = "",
                Loginpara = "",
                FormName = "Stock",
                ActivityType = "Excel Export",
                UsedFor = UsedFor
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.StockDownload, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TotalStockDownload()
        {
            string _response = _api.CallAPI(Constants.TotalStockDownload, string.Empty);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Chk_StockDisc_Avail()
        {
            string _response = _api.CallAPI(Constants.Chk_StockDisc_Avail, string.Empty);
            ServiceResponse<Chk_StockDisc_AvailResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Chk_StockDisc_AvailResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDashboardCount()
        {
            //var UserID = SessionFacade.UserSession.iUserid;
            //var input = new
            //{
            //    UserID = UserID
            //};
            //string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.StocDashboardCount, string.Empty);
            ServiceResponse<DashboardCountResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<DashboardCountResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLastLoggedin(string DeviceType)
        {
            var input = new
            {
                DeviceType = DeviceType
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetLastLoggedin, inputJson);
            ServiceResponse<GetLastLoggedinResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<GetLastLoggedinResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDynamicChartData(string para)
        {
            try
            {
                var input = new
                {
                    parameter = para
                };
                string inputJson = (new JavaScriptSerializer()).Serialize(input);
                string _response = _api.CallAPI(Constants.DynamicChart, inputJson);
                ServiceResponse<StockSummaryResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<StockSummaryResponse>>(_response);
                return Json(_data, JsonRequestBehavior.AllowGet);
            }
            catch (WebException ex)
            {

                string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();

                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetOrderSummaryChartData(int yearID)
        {
            var input = new
            {
                YearID = yearID
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.OrderSummaryChart, inputJson);
            ServiceResponse<OrderSummaryResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderSummaryResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYearData()
        {
           string _response = _api.CallAPI(Constants.YearDataForDashboard, string.Empty);
           ServiceResponse<YearMasterResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<YearMasterResponse>>(_response);
           return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangeLanguage(string LangCode)
        {
            if (HttpContext.Request.Cookies["language"] != null)
            {
                HttpContext.Response.Cookies["language"].Expires = DateTime.Now.AddDays(-30);
            }
            HttpContext.Response.Cookies["language"].Value = LangCode;
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult OpenEventModel()
        {
            ServiceResponse<Information> _data;

            if (SessionFacade.UserSession.iUserType == 3)
            {
                string _response = _api.CallAPI(Constants.GetFutureInfoList, "");
                _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Information>>(_response);
                return Json(_data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _data = new ServiceResponse<Information>()
                {
                    Data = null,
                    Status = "0",
                    Message = "Logged user is not customer"
                };
                return Json(_data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EventAction(EventActionRequest obj)
        {
            CommonResponse _data;
            if (SessionFacade.UserSession.iUserType == 3)
            {
                string inputJson = (new JavaScriptSerializer()).Serialize(obj);
                string _response = _api.CallAPI(Constants.SaveEventAction, inputJson);
                _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
                return Json(_data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _data = new CommonResponse()
                {
                    Error = "",
                    Status = "0",
                    Message = "Logged user is not customer"
                };
                return Json(_data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}