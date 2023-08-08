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
    public class NewArrivalController : BaseController
    {
        API _api = new API();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetNewArrivalStock(SearchDiamondsRequest obj)
        {
           string inputJson = (new JavaScriptSerializer()).Serialize(obj);
           string _response = _api.CallAPI(Constants.GetSearchStock, inputJson);
           ServiceResponse<SearchDiamondsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SearchDiamondsResponse>>(_response);
           return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}