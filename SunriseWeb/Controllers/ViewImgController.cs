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
    public class ViewImgController : BaseController
    {
        API _api = new API();
        // GET: ViewImg
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetPacketDet_SelectOne(string sRefNo)
        {
            var input = new
            {
                StockId = sRefNo
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.GetPacketDet, inputJson);
            ServiceResponse<PacketDet_Response> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PacketDet_Response>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Check()
        {
            return View();
        }
    }
}