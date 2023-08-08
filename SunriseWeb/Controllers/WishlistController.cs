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
    public class WishlistController : BaseController
    {
        // GET: Wishlist
        API _api = new API();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin_Wishlist()
        {
            return View();
        }
        public JsonResult GetWishListStone(string Location, string Shape,string Color, string Polish, string Pointer, string Lab, 
            string Fls, string Clarity, string Cut, string Symm,string PageNo, string OrderBy,string PageSize, bool SubUser = false)
        {
            var input = new
            {
                IsAdmin= false,
                RefNo = "",
                OfferTrans = "",
                Location = Location,
                Shape = Shape,
                Color = Color,
                Polish = Polish,
                Pointer = Pointer,
                Lab = Lab,
                Fls = Fls,
                Clarity = Clarity,
                Cut = Cut,
                Symm = Symm,
                PageNo = PageNo,
                OrderBy = OrderBy,
                PageSize = PageSize,
                SubUser = SubUser
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.ViewWishList, inputJson);
            ServiceResponse<ViewWishListResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ViewWishListResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAdminWishListStone(string RefNoCerti, string CompName, string Location, string Shape, string Color, string Polish, string Pointer, string Lab, string Fls, string Clarity, string Cut, string Symm, string PageNo, string OrderBy,
            string FromDate, string ToDate, string Status, string PageSize)
        {

            var input = new
            {
                PageNo = PageNo,
                IsAdmin = true,
                RefNoCerti = RefNoCerti,
                CompName = CompName,
                RefNo = "",
                Location = Location,
                Shape = Shape,
                Color = Color,
                Polish = Polish,
                Pointer = Pointer,
                Lab = Lab,
                Fls = Fls,
                Clarity = Clarity,
                Cut = Cut,
                Symm = Symm,
                OrderBy = OrderBy,
                FromDate = FromDate,
                ToDate = ToDate,
                Status = Status,
                PageSize = PageSize
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.ViewWishList, inputJson);
            ServiceResponse<ViewWishListResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ViewWishListResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DownloadWishlist(string RefNo, string Status, string RefNoCerti,string CompName, bool isAssistBy, string PageNo,
            string Location, string Shape, string Color, string Polish, string Pointer, string Lab,
            string Fls, string Clarity, string Cut, string Symm, string OrderBy, string FromDate, string ToDate, string FormName, string ActivityType, string iUserid_certi_no)
        {

            var input = new
            {
                IsAdmin = true,
                RefNo = RefNo,
                RefNoCerti = RefNoCerti,
                CompName = CompName,
                IsAssistBy = isAssistBy,
                PageNo = PageNo,
                Location = Location,
                Shape = Shape,
                Color = Color,
                Polish = Polish,
                Pointer = Pointer,
                Lab = Lab,
                Fls = Fls,
                Clarity = Clarity,
                Cut = Cut,
                Symm = Symm,
                OrderBy = OrderBy,
                FromDate = FromDate,
                ToDate = ToDate,
                Status = Status,
                FormName = FormName,
                ActivityType = ActivityType,
                iUserid_certi_no = iUserid_certi_no
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DownloadWishList, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}