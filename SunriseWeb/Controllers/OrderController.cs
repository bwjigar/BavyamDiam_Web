using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class OrderController : BaseController
    {
        API _api = new API();
        // GET: Order
        public ActionResult OrderHistory()
        {
            return View();
        }
        public ActionResult HoldStoneReport()
        {
            return View();
        }
        public ActionResult ConfirmOrder()
        {
            return View();
        }
        public JsonResult GetOrderHistoryData(string FromDate, String ToDate,string CommonName, string CompanyName, string StoneNoList, string Status, int PageNo, string CustomerName, string UserName, string Location, string OrderBy, string PgSize, bool PickUp = false, bool NotPickUp = false, bool Collected = false, bool NotCollected = false, bool DateStatus = false, bool SubUser = false)
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = PageNo,
                RefNo = StoneNoList,
                CommonName = CommonName,
                CompanyName = CompanyName,
                Status = Status,
                CustomerName = CustomerName,
                UserName = UserName,
                Location = Location,
                OrderBy = OrderBy,
                PgSize = PgSize,
                PickUp = PickUp,
                NotPickUp = NotPickUp,
                Collected = Collected,
                NotCollected = NotCollected,
                DateStatus = DateStatus,
                SubUser = SubUser
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.UserOrderHistory, inputJson);
            ServiceResponse<OrderHistoryResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderHistoryResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
            //GetOrderHistory
        }
        public JsonResult GetOrderHistoryFilterData(string FromDate, String ToDate, string CommonName, string CompanyName, string StoneNoList, string Status, string CustomerName, string UserName, string Location, string OrderBy, string PgSize, bool PickUp = false, bool NotPickUp = false, bool Collected = false, bool NotCollected = false, bool DateStatus = false, bool SubUser = false)
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = 0,
                RefNo = StoneNoList,
                CommonName = CommonName,
                CompanyName = CompanyName,
                CustomerName = CustomerName,
                UserName = UserName,
                Status = Status,
                Location = Location,
                OrderBy = OrderBy,
                PgSize = PgSize,
                PickUp = PickUp,
                NotPickUp = NotPickUp,
                Collected = Collected,
                NotCollected = NotCollected,
                DateStatus = DateStatus,
                SubUser = SubUser
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.UserOrderHistoryFilter, inputJson);
            ServiceResponse<OrderHistoryFiltersResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderHistoryFiltersResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
            //GetOrderHistory
        }
        public JsonResult DownloadOrderHistory(string iUserid_FullOrderDate, string FromDate, String ToDate,string CommonName, string CompanyName, string StoneNoList, string Status,
            string UserName, string CustomerName, string Location, string OrderBy, 
            int PageNo, bool isAdmin, bool isEmp, string PgSize, string FormName, string ActivityType, bool PickUp = false, bool NotPickUp = false, bool Collected = false, bool NotCollected = false, bool DateStatus = false, bool SubUser = false)
        {
            var input = new
            {
                iUserid_FullOrderDate = iUserid_FullOrderDate,
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = PageNo,
                RefNo = StoneNoList,
                CommonName = CommonName,
                CompanyName = CompanyName,
                CustomerName = CustomerName,
                UserName = UserName,
                Location = Location,
                OrderBy = OrderBy,
                Status = Status,
                isAdmin = isAdmin,
                isEmp = isEmp,
                PgSize = PgSize,
                FormName = FormName,
                ActivityType = ActivityType,
                PickUp = PickUp,
                NotPickUp = NotPickUp,
                Collected = Collected,
                NotCollected = NotCollected,
                DateStatus = DateStatus,
                SubUser = SubUser
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DownloadOrderHistory, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetConfirmOrderHistoryData(string FromDate, string ToDate, string StoneNoList,string CompanyName,string OrderBy,string PageNo, string PageSize, string Assist = "")
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = PageNo,
                PageSize = PageSize,
                RefNo = StoneNoList,
                CompanyName = CompanyName,
                OrderBy = OrderBy,
                Assist = Assist
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.AdminOrderHistory, inputJson);
            ServiceResponse<OrderConfirmResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderConfirmResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
            //GetOrderHistory
        }
        public JsonResult ConfirmOrder_Excel(string FromDate, string ToDate, string StoneNoList, string CompanyName, string OrderBy, string PageNo, string PageSize, string Assist = "")
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = PageNo,
                PageSize = PageSize,
                RefNo = StoneNoList,
                CompanyName = CompanyName,
                OrderBy = OrderBy,
                Assist = Assist
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.ConfirmOrder_Excel, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ExcludeStoneFromStockInsert(string OrderDetId)
        {
            var input = new
            {
                OrderId = OrderDetId,
                bIsExcludeStk = false
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.ExcludeStoneFromStockInsert, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssistPersonDetail()
        {
            string _response = _api.CallAPI(Constants.GetAssistPersonDetail, "");
            ServiceResponse<OrderHistoryResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderHistoryResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetHoldHistory(OrderHistoryRequest req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.GetHoldHistory, inputJson);
            ServiceResponse<SearchDiamondsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SearchDiamondsResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}