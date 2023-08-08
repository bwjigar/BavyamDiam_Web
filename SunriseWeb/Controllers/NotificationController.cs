using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class NotificationController : BaseController
    {
        API _api = new API();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NotificationEdit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetNotificationList(NotificationGetRequest _obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(_obj);
            string _response = _api.CallAPI(Constants.GetNotifications, inputJson);
            ServiceResponse<NotificationGetResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<NotificationGetResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}