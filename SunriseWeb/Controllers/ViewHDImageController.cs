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
    public class ViewHDImageController : Controller
    {
        API _api = new API();
        // GET: ViewHDImage
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetHDVideodDetail (string sRefNo)
        {
            var input = new
            {
                RefNo = sRefNo
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.GetViewHDImage, inputJson);
            ServiceResponse<ViewHDImageResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ViewHDImageResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}