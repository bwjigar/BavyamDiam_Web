using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class LoginInfoController : BaseController
    {
        API _api = new API();

        public ActionResult LoginInfo()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetLoginInfoList(UserListSearchRequest _obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(_obj);
            string _response = _api.CallAPI(Constants.GetLoginInfoDetail, inputJson);
            ServiceResponse<LoginInfoResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<LoginInfoResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DownloadLoginInfoList(UserListSearchRequest _obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(_obj);
            string _response = _api.CallAPI(Constants.DownloadLoginInfo, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}