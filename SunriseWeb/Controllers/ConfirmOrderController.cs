using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class ConfirmOrderController : BaseController
    {
        API _api = new API();
        static SupplierApiOrderRequest_AUTO auto_req = new SupplierApiOrderRequest_AUTO();
        Data.Common _common = new Data.Common();

        // GET: ConfirmOrder
        public ActionResult ConfirmOrderHistory()
        {
            return View();
        }
        public ActionResult ConfirmOrder()
        {
            return View();
        }
        public JsonResult GetOrderHistoryData(string FromDate, String ToDate, string CommonName, string CompanyName, string StoneNoList, string Status, int PageNo, string CustomerName, string UserName, string Location, string OrderBy, string PgSize, bool PickUp = false, bool NotPickUp = false, bool Collected = false, bool NotCollected = false, bool DateStatus = false, bool SubUser = false)
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
            string _response = _api.CallAPI(Constants.UserConfirmOrderHistory, inputJson);
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
            string _response = _api.CallAPI(Constants.UserConfirmOrderHistoryFilter, inputJson);
            ServiceResponse<OrderHistoryFiltersResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderHistoryFiltersResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
            //GetOrderHistory
        }
        public JsonResult DownloadOrderHistory(string iOrderid_sRefNo, string FromDate, String ToDate, string CommonName, string CompanyName, string StoneNoList, string Status,
            string UserName, string CustomerName, string Location, string OrderBy,
            int PageNo, bool isAdmin, bool isEmp, string PgSize, string FormName, string ActivityType, bool PickUp = false, bool NotPickUp = false, bool Collected = false, bool NotCollected = false, bool DateStatus = false, bool SubUser = false)
        {
            var input = new
            {
                iUserid_FullOrderDate = iOrderid_sRefNo,
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
            string _response = _api.CallAPI(Constants.DownloadConfirmOrderHistory, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssistPersonDetail()
        {
            string _response = _api.CallAPI(Constants.GetAssistPersonDetail, "");
            ServiceResponse<OrderHistoryResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderHistoryResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CallSupplierApi_1(SupplierApiOrderRequest req)
        {
            Data.Common _common = new Data.Common();
            string ip = _common.gUserIPAddresss();
            req.IpAddress = ip;
            
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.PlaceConfirmOrderUsingApi_1, inputJson);
            ServiceResponse<ConfirmPlaceOrderResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ConfirmPlaceOrderResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CallSupplierApi_Web(SupplierApiOrderRequest req)
        {
            Data.Common _common = new Data.Common();
            string ip = _common.gUserIPAddresss();
            req.IpAddress = ip;

            string inputJson_1 = (new JavaScriptSerializer()).Serialize(req);
            string response_1 = _api.CallAPI(Constants.PlaceConfirmOrderUsingApi_Web_1, inputJson_1);
            CommonResponse data_1 = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response_1);

            if (data_1.Status == "1")
            {
                var input_2 = new
                {
                    Mas_Id = data_1.Message
                };

                string inputJson_2 = (new JavaScriptSerializer()).Serialize(input_2);
                string response_2 = _api.CallAPIUrlEncodedWithWebReq(Constants.PlaceConfirmOrderUsingApi_Web_2, inputJson_2);
                ServiceResponse<ConfirmPlaceOrderResponse> data_2 = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ConfirmPlaceOrderResponse>>(response_2);
                return Json(data_2, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(data_1, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ConfirMorderSuccess()
        {
            string _response = _api.CallAPI("/ConfirmOrder/ConfirMorderSuccess", string.Empty);
            ServiceResponse<ConfirmPlaceOrderResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ConfirmPlaceOrderResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierOrderLog()
        {
            return View();
        }

        public JsonResult GetSupplierOrderLogData(string FromDate, String ToDate, string CommonName, string CompanyName, string StoneNoList, int PageNo, string CustomerName, string OrderBy, string PgSize, bool DateStatus = false, bool SubUser = false)
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = PageNo,
                RefNo = StoneNoList,
                CommonName = CommonName,
                CompanyName = CompanyName,
                OrderBy = OrderBy,
                PgSize = PgSize,
                CustomerName = CustomerName,

                DateStatus = DateStatus,
                SubUser = SubUser
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSupplierOrderLog, inputJson);
            ServiceResponse<OrderHistoryResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderHistoryResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
            //GetOrderHistory
        }

        public JsonResult DownloadSupplierOrderLog(string iOrderid_sRefNo, string FromDate, String ToDate, string CommonName, string CompanyName, string StoneNoList,
         string CustomerName, string OrderBy,
         int PageNo, bool isAdmin, bool isEmp, string PgSize, string FormName, string ActivityType, bool DateStatus = false, bool SubUser = false)
        {
            var input = new
            {
                iUserid_FullOrderDate = iOrderid_sRefNo,
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = PageNo,
                RefNo = StoneNoList,
                CommonName = CommonName,
                CompanyName = CompanyName,
                CustomerName = CustomerName,

                OrderBy = OrderBy,

                isAdmin = isAdmin,
                isEmp = isEmp,
                PgSize = PgSize,
                FormName = FormName,
                ActivityType = ActivityType,

                DateStatus = DateStatus,
                SubUser = SubUser
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DownloadSuppOrderLog, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PurchaseOrder_Delete(PurchaseOrder_Delete_Request req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.PurchaseOrder_Delete, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AUTO_PlaceConfirmOrder(string iOrderid_sRefNo)
        {
            string ip = _common.gUserIPAddresss();

            auto_req.iOrderid_sRefNo = iOrderid_sRefNo;
            auto_req.DeviceType = "Web";
            auto_req.IpAddress = ip; 

            Thread Thread1 = new Thread(AUTO_PlaceConfirmOrder_Thread);
            Thread1.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void AUTO_PlaceConfirmOrder_Thread()
        {
            API _api = new API();
            string inputJson = (new JavaScriptSerializer()).Serialize(auto_req);
            string _response = _api.CallAPIUrlEncodedWithWebReq(Constants.AUTO_PlaceConfirmOrderUsingApi_Web, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
        }



        public JsonResult ArrayCheck()
        {
            string _response = _api.CallAPI("/ConfirmOrder/ArrayCheck", string.Empty);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}