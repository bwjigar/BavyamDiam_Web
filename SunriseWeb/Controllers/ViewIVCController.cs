using Lib.Models;
using SunriseWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    public class ViewIVCController : Controller
    {
        API _api = new API();
        // GET: ViewIVC
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetStoneDetail(string sRefNo)
        {
            string inputJson = string.Empty;
            int iUserId = 0;

            var input = new
            {
                StoneID = sRefNo,
                UserId = 0
            };
            inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.GetSearchStockByStoneIDWithoutToken, inputJson);
            SearchStone _data1 = (new JavaScriptSerializer()).Deserialize<SearchStone>(_response);
            return Json(_data1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIVC_From_Fortune(string sRefNo)
        {
            var input = new
            {
                StoneID = sRefNo,
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.GetIVC_From_Fortune, inputJson);
            SearchStone _data1 = (new JavaScriptSerializer()).Deserialize<SearchStone>(_response);
            return Json(_data1, JsonRequestBehavior.AllowGet);
        }
    }
}