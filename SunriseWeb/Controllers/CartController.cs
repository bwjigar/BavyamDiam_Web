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
    public class CartController : BaseController
    {
        // GET: Cart
        API _api = new API();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCartStoneList(ViewCartRequest req)
        {
            var input = new
            {
                PageNo = req.PageNo,
                OrderBy = req.OrderBy,
                FromDate = req.FromDate,
                ToDate = req.ToDate,
                RefNo1 = req.RefNo1,
                CompanyName = req.CompanyName,
                PageSize = req.PageSize,
                SubUser = req.SubUser,

                //RefNo = "",
                //OfferTrans="",
                //Location = req.Location,
                //Shape = req.Shape,
                //Color = req.Color,
                //Polish = req.Polish,
                //Pointer = req.Pointer,
                //Lab = req.Lab,
                //Fls = req.Fls,
                //Clarity = req.Clarity,
                //Cut = req.Cut,
                //Symm = req.Symm,
                //Status = req.Status,
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.ViewCart, inputJson);
            ServiceResponse<ViewCartResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ViewCartResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}