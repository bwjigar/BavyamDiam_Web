using Lib.Models;
using SunriseWeb.Filter;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SunriseWeb.Data;
using Constants = SunriseWeb.Data.Constants;
using System.Configuration;
using SunriseWeb.Helper;
using System;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class LabStockController : BaseController
    {
        API _api = new API();
        // GET: LabStock
        public ActionResult Index()
        {
            return View();
        }
        public ContentResult LabStockAPI()
        {
            string loginUrl = ConfigurationManager.AppSettings["NewLabWebsiteURL"], Username = string.Empty, Password = string.Empty;
            int iUserid = Convert.ToInt32(SessionFacade.UserSession.iUserid);

            UserDetailGet_Req req = new UserDetailGet_Req();
            req.UserId = iUserid;

            var _inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _Response = _api.CallAPI(Constants.UserDetailGet, _inputJson);

            ServiceResponse<UserDetailGet_Res> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserDetailGet_Res>>(_Response);
            if (_data.Data != null)
            {
                Username = _data.Data[0].sUsername;
                Password = _data.Data[0].sPassword;

                //Session.Abandon();
                //var _input1 = new
                //{
                //    IPAddress = GetIpValue(),
                //    UserId = 0,
                //    Type = "EXPIRED"
                //};
                //var _inputJson_1 = (new JavaScriptSerializer()).Serialize(_input1);
                //string _Response_1 = _api.CallAPI(Constants.IP_Wise_Login_Detail, _inputJson_1);
                //Response.Cookies["Userid_DNA"].Value = "0";
                //string _response = _api.CallAPIWithoutToken(Constants.LogoutWithoutToken, "");

                return Content("<form action='" + loginUrl + "' id='login-form' method='post'>" +
                    "<input type='hidden' name='Username' value='" + Username + "' /><input type='hidden' name='Password' value='" + Password + "' />" +
                    "</form><script>document.getElementById('login-form').submit();</script>");
            }
            else
            {
                return Content("");
            }
        }
        //public string GetIpValue()
        //{
        //    string ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //    if (string.IsNullOrEmpty(ipAdd))
        //    {
        //        ipAdd = Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    else
        //    {
        //        // lblIPAddress.Text = ipAdd;
        //    }
        //    return ipAdd;
        //}
        public JsonResult GetTransId(string FromDate, string ToDate)
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetTransId, inputJson);
            TransValueResponse _data = (new JavaScriptSerializer()).Deserialize<TransValueResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Lab_GetTransId(TransValueRequest req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.Lab_GetTransId, inputJson);
            ServiceResponse<TransValue> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<TransValue>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CustomerExcel(LabStockDownloadRequest obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.GenerateCustomerExcel, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LabSearchExcel(LabSearchStockDownloadRequest obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPIUrlEncodedWithWebReq(Constants.LabSearchExcel, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LabSearchGrid(LabSearchStockDownloadRequest obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPIUrlEncodedWithWebReq(Constants.LabSearchGrid, inputJson);
            ServiceResponse<LabSearchStockGridResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<LabSearchStockGridResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LabSearchGridExcel(LabSearchStockDownloadRequest obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPIUrlEncodedWithWebReq(Constants.LabSearchGridExcel, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ByRequest_CRUD(ByRequestReq obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.ByRequest_CRUD, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ByRequest_Cart(ByRequestReq obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.ByRequest_Cart, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LabByRequestCart()
        {
            return View();
        }
        public JsonResult LabByRequestCartGet(ByRequestReq obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.LabByRequestCartGet, inputJson);
            ServiceResponse<LabSearchStockGridResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<LabSearchStockGridResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LabByRequest()
        {
            return View();
        }
        public JsonResult LabByRequestGet(ByRequestReq obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.LabByRequestGet, inputJson);
            ServiceResponse<LabSearchStockGridResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<LabSearchStockGridResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ByRequest_ApproveReject(ByRequestReq obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.ByRequest_ApproveReject, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ByRequest_Apply_Disc(ByRequestReq req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.ByRequest_Apply_Disc, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}