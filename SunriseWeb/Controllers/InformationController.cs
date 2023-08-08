using Lib.Models;
using Newtonsoft.Json;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class InformationController : BaseController
    {
        API _api = new API();
        // GET: Information
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customer()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(Information _obj, IEnumerable<HttpPostedFileBase> _Img)
        {
            if (_Img != null && _obj.IsProfileChanged == "1")
            {
                byte[] ImageData;
                using (Stream inputStream = _Img.FirstOrDefault().InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    ImageData = memoryStream.ToArray();
                }

                _obj.Image = ImageData;
                _obj.FileExtenstion = Path.GetExtension(_Img.FirstOrDefault().FileName);
            }

            string inputJson = (new JavaScriptSerializer()).Serialize(_obj);

            string _response = _api.CallAPI(Constants.SaveInfoDetails, inputJson);

            CommonResponse resObj = new CommonResponse();
            resObj = JsonConvert.DeserializeObject<CommonResponse>(_response);

            return Json(resObj);
        }

        public JsonResult GetList()
        {
            string _response = _api.CallAPI(Constants.GetInfoList,"");
            ServiceResponse<Information> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Information>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustList()
        {
            string _response = _api.CallAPI(Constants.GetCustInfoList, "");
            ServiceResponse<CustInformation> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CustInformation>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}