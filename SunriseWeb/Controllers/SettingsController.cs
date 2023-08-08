using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Lib.Models;
using Newtonsoft.Json;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using SunriseWeb.Helper;
using SunriseWeb.Models;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class SettingsController : BaseController
    {
        API _api = new API();
        Data.Common _common = new Data.Common();

        // GET: Settings
        public ActionResult ColumnsSettings()
        {
            ColumnsUserModel usermodel = new ColumnsUserModel();
            string _response = _api.CallAPI(Constants.GetUserMas, string.Empty);
            ServiceResponse<ColumnsUserResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ColumnsUserResponse>>(_response);
            foreach (var a in _data.Data)
            {
                usermodel.UserModel.Add(new SelectListItem
                {
                    Text = a.sFullName + (a.sCompName != "" && a.sCompName != null ? " [" + a.sCompName + "]" : ""),
                    Value = a.iUserid.ToString()
                });
            }

            return View(usermodel);
        }
        public JsonResult SaveColumnsSettings(List<ColumnsSettings> obj, int UserId = 0)
        {
            var input = new
            {
                ColumnsSettings = obj,
                UserId = UserId
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.SaveColumnsSettings, inputJson);
            ServiceResponse<ColumnsSettingsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ColumnsSettingsResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetColumnSettingData(ColumnsRequest obj)
        {
            var input = new
            {
                UserId = obj.UserId
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetCoumnsSettings, inputJson);
            ServiceResponse<ColumnsSettingsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ColumnsSettingsResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StockPrefix()
        {
            return View();
        }
        public JsonResult SupplierGet()
        {
            var input = new { };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string response = _api.CallAPI(Constants.PartyInfo, inputJson);
            ServiceResponse<PartyInfoResponse> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PartyInfoResponse>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_SupplierPrefix(SuppPrefix_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst);
            string response = _api.CallAPI(Constants.Get_SupplierPrefix, inputJson);
            ServiceResponse<SuppPrefix_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SuppPrefix_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save_SuppPrefix(Save_SuppPrefix_Request save_supprefix)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(save_supprefix);
            string response = _api.CallAPI(Constants.Save_SuppPrefix, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete_SuppPrefix(SuppPrefix_Request delete_supprefix)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(delete_supprefix);
            string response = _api.CallAPI(Constants.Delete_SuppPrefix, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StockUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StockUpload(string DdlSupplierName)
        {
            if (!string.IsNullOrEmpty(DdlSupplierName) && Request.Files["FileUpload1"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                if (extension.Trim() == ".xlsx")
                {
                    string ProjPath = ConfigurationManager.AppSettings["ProjPath"];
                    string ServPath = ConfigurationManager.AppSettings["ServPath"];

                    string MapPath = HostingEnvironment.MapPath("~/Content/");
                    MapPath = MapPath.Replace(@"" + ProjPath, @"" + ServPath);

                    string date = Lib.Models.Common.GetHKTime().ToString("ddMMyyHHmmss");
                    string filename = date + " " + Request.Files["FileUpload1"].FileName;

                    string filePath = string.Format("{0}/{1}", MapPath, filename);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Request.Files["FileUpload1"].SaveAs(filePath);

                    //string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                    StockUploadRequest req = new StockUploadRequest();
                    req.Supplier = DdlSupplierName;
                    req.connString = filename;

                    string inputJson = (new JavaScriptSerializer()).Serialize(req);
                    string response = _api.CallAPI(Constants.StockUpload, inputJson);
                    CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);

                    TempData["Message"] = data.Message;
                    TempData["Status"] = data.Status;
                }
                else
                {
                    TempData["Message"] = "Please Upload Files in .xlsx format";
                    TempData["Status"] = "0";
                }
            }

            return View();
        }
        public ActionResult SupplierMas()
        {
            return View();
        }
        public JsonResult Get_SupplierMaster(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst);
            string response = _api.CallAPI(Constants.Get_SupplierMaster, inputJson);
            ServiceResponse<Get_APIMst_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_APIMst_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SupplierDet()
        {
            return View();
        }
        public JsonResult SaveSupplierMaster(Save_APIMst_Request saveapimst)
        {
            CommonResponse _data = new CommonResponse();
            Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            string mainurl = url.AbsoluteUri.Replace(url.AbsolutePath, "");

            if (saveapimst.Id == 0)
            {
                saveapimst.SupplierHitUrl = mainurl + "/Settings/APIGet?Id=";
            }

            string inputJson = (new JavaScriptSerializer()).Serialize(saveapimst);
            string response = _api.CallAPI(Constants.SaveSupplierMaster, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SuppColSettings()
        {
            return View();
        }
        public JsonResult Get_SuppColSettMas(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst);
            string response = _api.CallAPI(Constants.Get_SuppColSettMas, inputJson);
            ServiceResponse<Get_SuppColSettMas_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SuppColSettMas_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SuppColSettingsDet()
        {
            return View();
        }
        public JsonResult Get_Column_Mas_Select()
        {
            string response = _api.CallAPI(Constants.Get_Column_Mas_Select, string.Empty);
            ServiceResponse<Get_Column_Mas_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_Column_Mas_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SupplierColSettings_ExistorNot(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst); 
            string response = _api.CallAPI(Constants.SupplierColSettings_ExistorNot, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_SuppColSettDet(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst); 
            string response = _api.CallAPI(Constants.Get_SuppColSettDet, inputJson);
            ServiceResponse<Get_SuppColSettDet_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SuppColSettDet_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save_SuppColSettMas(Save_SuppColSettMas_Request save_supcolsetmas)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(save_supcolsetmas);
            string response = _api.CallAPI(Constants.Save_SuppColSettMas, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SupplierColumnsGetFromAPI(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst);
            string response = _api.CallAPI(Constants.SupplierColumnsGetFromAPI, inputJson); 
            ServiceResponse<Get_SupplierColumnsFromAPI_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SupplierColumnsFromAPI_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SuppPriceList()
        {
            return View();
        }
        public JsonResult TabSessionSet(string Type)
        {
            Session["TabSessionSet"] = Type;
            return Json(Type, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TabSessionGet()
        {
            return Json((Session["TabSessionSet"] == null ? "StockPrefix" : Session["TabSessionSet"]), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Supplier_PriceList(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst);
            string response = _api.CallAPI(Constants.Get_Supplier_PriceList, inputJson);
            ServiceResponse<Get_Supplier_PriceList_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_Supplier_PriceList_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Supplier_PriceList_Delete(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst);
            string response = _api.CallAPI(Constants.Supplier_PriceList_Delete, inputJson);
            ServiceResponse<Get_Supplier_PriceList_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_Supplier_PriceList_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SuppPriceListDet()
        {
            return View();
        }
        public JsonResult SupplierGetFrom_PriceList(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst);
            string response = _api.CallAPI(Constants.SupplierGetFrom_PriceList, inputJson);
            ServiceResponse<Get_Supplier_PriceList_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_Supplier_PriceList_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_API_StockFilter()
        {
            string response = _api.CallAPI(Constants.Get_API_StockFilter, string.Empty);
            ServiceResponse<Get_API_StockFilter_Response> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_API_StockFilter_Response>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save_SuppStockValue(Save_SuppStockValue_Request save_suppstockvalue)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(save_suppstockvalue); 
            string response = _api.CallAPI(Constants.Save_SuppStockValue, inputJson);
            ServiceResponse<CommonResponse> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CommonResponse>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save_SuppCostValue(Save_SuppStockValue_Request save_suppcostvalue)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(save_suppcostvalue);
            string response = _api.CallAPI(Constants.Save_SuppCostValue, inputJson);
            ServiceResponse<CommonResponse> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CommonResponse>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_SuppStockValue(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst);
            string response = _api.CallAPI(Constants.Get_SuppStockValue, inputJson);
            ServiceResponse<SuppStockValue> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SuppStockValue>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_SuppCostValue(Get_APIMst_Request get_apimst)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(get_apimst);
            string response = _api.CallAPI(Constants.Get_SuppCostValue, inputJson);
            ServiceResponse<SuppStockValue> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SuppStockValue>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}