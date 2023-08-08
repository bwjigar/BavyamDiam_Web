using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class UserActivityController : BaseController
    {
        API _api = new API();
        // GET: UserActivity
        public ActionResult UserActivity()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUserActivityUpdateList(UserActivityUpdateRequest _obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(_obj);
            string _response = _api.CallAPI(Constants.GetUserActivityDetail, inputJson);
            ServiceResponse<UserActivityUpdateResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserActivityUpdateResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult DownloadActivityUpdateList(UserActivityUpdateRequest _obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(_obj);
            string _response = _api.CallAPI(Constants.DownloadActivityUpdate, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}