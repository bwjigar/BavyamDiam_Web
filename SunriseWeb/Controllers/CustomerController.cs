using ExcelDataReader;
using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using SunriseWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class CustomerController : BaseController
    {
        API _api = new API();

        #region Overseas Customer Discount

        public ActionResult CustomerDisc()
        {
            return View();
        }
        public ActionResult CustomerDiscReport()
        {
            return View();
        }
        public JsonResult GetCustomer(string SearchText)
        {
            var input = new
            {
                SearchText = SearchText
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetCustomer, inputJson);
            ServiceResponse<Customer> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Customer>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerDisc(string UserName, int PageNo, int PageSize)
        {
            var input = new
            {
                UserName = UserName,
                PageNo = PageNo,
                PageSize = PageSize
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetCustomerDisc, inputJson);
            ServiceResponse<CustomerDiscResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CustomerDiscResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveCustomerDisc(string CustId, string Oper, string Input, int TransId)
        {
            var input = new
            {
                CustId = CustId,
                Oper = Oper,
                TransId = TransId,
                xmlStr = Input
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.SaveCustomerDisc, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSupplier()
        {
            string _response = _api.CallAPI(Constants.GetSupplier, "");
            ServiceResponse<PartyInfoResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PartyInfoResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region User Discount

        public ActionResult UserDisc()
        {
            return View();
        }
        public ActionResult UserDiscReport()
        {
            return View();
        }
        public ActionResult StockDiscMgt()
        {
            var TransId = Convert.ToInt32(Request.QueryString.Count > 0 && Request.QueryString["TransId"] != "" ? Request.QueryString["TransId"] : "0");
            var input = new
            {
                ListValue = "DP"
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSearchParameter, inputJson);
            ServiceResponse<ListValueResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ListValueResponse>>(_response);
            UploadMethodModel apiFilter = new UploadMethodModel();

            apiFilter.IsModify = (Request.QueryString.Count > 0 && Request.QueryString["Type"].ToLower() == "modify") ? true : false;
            apiFilter.TransId = Convert.ToInt32(Request.QueryString.Count > 0 && Request.QueryString["TransId"] != "" ? Request.QueryString["TransId"] : "0");

            _data.Data.Add(new ListValueResponse { Id = -0, Value = "ALL", ListType = "Shape" });
            apiFilter.ShapeList = _data.Data.Where(a => a.ListType.ToLower() == "shape").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(o => o.iSr).ToList();

            apiFilter.PointerList = _data.Data.Where(a => a.ListType.ToLower() == "pointer").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(o => o.iSr).ToList();

            _data.Data.Add(new ListValueResponse { Id = -0, Value = "ALL", ListType = "Color" });
            apiFilter.ColorList = _data.Data.Where(a => a.ListType.ToLower() == "color").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(o => o.iSr).ToList();

            _data.Data.Add(new ListValueResponse { Id = -0, Value = "ALL", ListType = "Cut" });
            apiFilter.CutList = _data.Data.Where(a => a.ListType.ToLower() == "cut").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(o => o.iSr).ToList();

            _data.Data.Add(new ListValueResponse { Id = -0, Value = "ALL", ListType = "Clarity" });
            apiFilter.ClarityList = _data.Data.Where(a => a.ListType.ToLower() == "clarity").Select(b => new ListingModel() { iSr = (b.Value == "ALL" ? b.Id : b.Id + 1), sName = b.Value }).OrderBy(o => o.iSr).ToList();

            apiFilter.PolishList = _data.Data.Where(a => a.ListType.ToLower() == "polish").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.SymmList = _data.Data.Where(a => a.ListType.ToLower() == "symm").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.FlsList = _data.Data.Where(a => a.ListType.ToLower() == "fls").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.ShadeList = _data.Data.Where(a => a.ListType.ToLower() == "shade").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.NattsList = _data.Data.Where(a => a.ListType.ToLower() == "table_natts").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.InclusionList = _data.Data.Where(a => a.ListType.ToLower() == "table_incl").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.LabList = _data.Data.Where(a => a.ListType.ToLower() == "lab").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();

            _data.Data.Add(new ListValueResponse { Id = -0, Value = "ALL", ListType = "Location" });
            apiFilter.LocationList = _data.Data.Where(a => a.ListType.ToLower() == "location").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(c => c.iSr).ToList();

            apiFilter.BGMList = _data.Data.Where(a => a.ListType.ToLower() == "bgm" && a.Id > 0).Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(c => c.iSr).ToList();
            apiFilter.CrnBlackList = _data.Data.Where(a => a.ListType.ToLower() == "crown_natts" && a.Id > 0).Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(c => c.iSr).ToList();
            apiFilter.CrnWhiteList = _data.Data.Where(a => a.ListType.ToLower() == "crown_incl" && a.Id > 0).Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(c => c.iSr).ToList();
            apiFilter.TblBlackList = _data.Data.Where(a => a.ListType.ToLower() == "table_natts" && a.Id > 0).Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(c => c.iSr).ToList();
            apiFilter.TblWhiteList = _data.Data.Where(a => a.ListType.ToLower() == "table_incl" && a.Id > 0).Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(c => c.iSr).ToList();
            apiFilter.ToPavAng = "0.00";
            apiFilter.FromPavHt = "0.00";
            apiFilter.ExportTypeList = new List<SelectListItem>() {
                new SelectListItem { Value = "EXCEL(.xlsx)", Text = "EXCEL(.xlsx)" },
                new SelectListItem { Value = "EXCEL(.xls)", Text = "EXCEL(.xls)" },
                new SelectListItem { Value = "CSV",         Text = "CSV" },
                new SelectListItem { Value = "XML",         Text = "XML" },
                new SelectListItem { Value = "JSON",        Text = "JSON" }
            };
            apiFilter.OccuranceList = new List<SelectListItem>() {
                new SelectListItem { Value = "D", Text = "Daily" },
                new SelectListItem { Value = "W", Text = "Weekly" },
                new SelectListItem { Value = "E", Text = "Weekdays" },
                new SelectListItem { Value = "M", Text = "Monthly" },
                new SelectListItem { Value = "Q", Text = "Quaterly" }
            };
            apiFilter.SeparatorList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "-", Text = "-" },
                new SelectListItem { Value ="*", Text = "*" },
                new SelectListItem { Value ="@", Text = "@" },
                new SelectListItem { Value ="/", Text = "/" }
            };
            apiFilter.PricingMethodList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Disc", Text = "Discount" },
                new SelectListItem { Value ="Value", Text = "Value" }
            };

            var input_V = new
            {
                PartyName = "",
                ContactPerson = "",
                PartyPrefix = "",
                CountryId = 0,
                PageNo = 0,
                PageSize = 0,
                OrderBy = 0
            };
            string inputJson_V = (new JavaScriptSerializer()).Serialize(input_V);
            string _response_V = _api.CallAPI(Constants.PartyInfo, inputJson_V);
            ServiceResponse<PartyInfoResponse> _data_V = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PartyInfoResponse>>(_response_V);
            _data_V.Data.Add(new PartyInfoResponse { Id = -0, sPartyName = "ALL" });
            _data_V.Data.Add(new PartyInfoResponse { Id = 0, sPartyName = "SUNRISE" });
            apiFilter.SupplierList = _data_V.Data.Select(b => new ListingModel() { iSr = b.Id, sName = b.sPartyName }).OrderBy(o => o.iSr).ToList();

            return View(apiFilter);
        }
        public ActionResult StockDiscMgtReport()
        {
            return View();
        }
        public JsonResult Get_StockDiscMgt(StockDiscMgtRequest stockdiscmgtrequest)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(stockdiscmgtrequest);
            string _response = _api.CallAPI(Constants.Get_StockDiscMgt, inputJson);
            ServiceResponse<StockDiscMgtResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<StockDiscMgtResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PartyInfo(PartyInfoReq req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.PartyInfo, inputJson);
            ServiceResponse<PartyInfoResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PartyInfoResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserDisc(string UserName, int PageNo, int PageSize)
        {
            var input = new
            {
                UserName = UserName,
                PageNo = PageNo,
                PageSize = PageSize
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetUserDisc, inputJson);
            ServiceResponse<CustomerDiscResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CustomerDiscResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUserDisc_Excel(string UserName, int PageNo, int PageSize)
        {
            var input = new
            {
                UserName = UserName,
                PageNo = PageNo,
                PageSize = PageSize
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetUserDisc_Excel, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_StockDiscMgtReport(string UserName, int PageNo, int PageSize)
        {
            var input = new
            {
                UserName = UserName,
                PageNo = PageNo,
                PageSize = PageSize
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.Get_StockDiscMgtReport, inputJson);
            ServiceResponse<GetStockDiscRes> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<GetStockDiscRes>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_StockDiscMgtReport(string UserName, int PageNo, int PageSize)
        {
            var input = new
            {
                UserName = UserName,
                PageNo = PageNo,
                PageSize = PageSize
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.Excel_StockDiscMgtReport, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveUserDisc(string CustId, string Oper, string Input, int TransId)
        {
            var input = new
            {
                CustId = CustId,
                TransId = TransId,
                Oper = Oper,
                xmlStr = Input
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.SaveUserDisc, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveStockDisc(SaveStockDiscReq savestockdiscreq)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(savestockdiscreq);
            string _response = _api.CallAPI(Constants.SaveStockDisc, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        private List<StockImport> GetDataFromCSVFile(Stream stream)
        {
            var empList = new List<StockImport>();
            try
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows)
                        {
                            if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                            if (objDataRow["UserName"].ToString() != "")
                            {
                                empList.Add(new StockImport()
                                {
                                    UserName = objDataRow["UserName"].ToString(),
                                    Supplier = objDataRow["Supplier"].ToString(),
                                    Download = objDataRow["Download"].ToString(),
                                    View = objDataRow["View"].ToString(),
                                    PriceMethod = objDataRow["Price Method"].ToString(),
                                    PricePer = objDataRow["Price Per."].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return empList;
        }

        [HttpPost]
        public async Task<ActionResult> ImportStockDisc(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {
                var fileData = GetDataFromCSVFile(importFile.InputStream);

                StockImportList objLst = new StockImportList();
                List<StockImport> subLst = new List<StockImport>();

                for (int i = 0; i < fileData.Count(); i++)
                {
                    StockImport sub = new StockImport();
                    sub.UserName = fileData[i].UserName;
                    sub.Supplier = fileData[i].Supplier;
                    sub.Download = fileData[i].Download;
                    sub.View = fileData[i].View;
                    sub.PriceMethod = fileData[i].PriceMethod;
                    sub.PricePer = fileData[i].PricePer;
                    subLst.Add(sub);
                }
                objLst.StockImport = subLst;

                string inputJson = (new JavaScriptSerializer()).Serialize(objLst);
                string _response = _api.CallAPI(Constants.ImportStockDisc, inputJson);
                CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
                return Json(_data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }
        public ActionResult CustWiseDisc()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveCustWiseDisc(SaveCustWiseDisc_Req obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string response = _api.CallAPI(Constants.SaveCustWiseDisc, inputJson);
            ServiceResponse<OfferCriteria> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OfferCriteria>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustWiseDisc(StockDiscMgtRequest obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string response = _api.CallAPI(Constants.GetCustWiseDisc, inputJson);
            ServiceResponse<GetCustWiseDisc_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<GetCustWiseDisc_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
