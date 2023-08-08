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
    public class PairSearchController : BaseController
    {
        API _api = new API();
        // GET: PairSearch
        public ActionResult PairSearch()
        {
            return View();
        }
        public JsonResult GetSearchStock(SearchDiamondsRequest obj, string tabNo)
        {
            Session["PairSearchDiamondStock" + tabNo + ""] = obj;
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.PairSearchStock, inputJson);
            ServiceResponse<PairSearchDiamondsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PairSearchDiamondsResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModifyStockParameter(string tabNo)
        {
            SearchDiamondsRequest obj = (SearchDiamondsRequest)Session["PairSearchDiamondStock" + tabNo + ""];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}