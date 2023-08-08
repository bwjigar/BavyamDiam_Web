using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using SunriseWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class ApiController : BaseController
    {
        API _api = new API();

        // GET: Api
        public JsonResult UserwiseCompany_select(int iUserid)
        {
            var input = new
            {
                iUserid = iUserid
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.UserwiseCompany_select, inputJson);
            ServiceResponse<UserwiseCompany_select> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserwiseCompany_select>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FTPAPIPortalLog()
        {
            return View();
        }
        public JsonResult FTPAPIPortalLogList(string FromDate, string ToDate, int PageNo, int PageSize, string OrderBy, int Distinct, int IsFTP, string CompSearch)
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = PageNo,
                PageSize = PageSize,
                OrderBy = OrderBy,
                Distinct = Distinct,
                IsFTP = IsFTP,
                CompSearch = CompSearch
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.FTPAPIPortalLogList, inputJson);
            ServiceResponse<FTPAPIPortalLogResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<FTPAPIPortalLogResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult APIGroupLog()
        {
            return View();
        }
        public ActionResult Filter()
        {
            var input = new
            {
                ListValue = "DP"
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSearchParameter, inputJson);
            ServiceResponse<ListValueResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ListValueResponse>>(_response);

            ApiFilterModel apiFilter = new ApiFilterModel();
            apiFilter.IsModify = (Request.QueryString.Count > 0 && Request.QueryString["Type"].ToLower() == "modify") ? true : false;
            apiFilter.TransId = Convert.ToInt32(Request.QueryString.Count > 0 && Request.QueryString["TransId"] != "" ? Request.QueryString["TransId"] : "0");
            apiFilter.ShapeList = _data.Data.Where(a => a.ListType.ToLower() == "shape").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.ColorList = _data.Data.Where(a => a.ListType.ToLower() == "color").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.CutList = _data.Data.Where(a => a.ListType.ToLower() == "cut").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.ClarityList = _data.Data.Where(a => a.ListType.ToLower() == "clarity").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.PolishList = _data.Data.Where(a => a.ListType.ToLower() == "polish").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.SymmList = _data.Data.Where(a => a.ListType.ToLower() == "symm").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.FlsList = _data.Data.Where(a => a.ListType.ToLower() == "fls").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.ShadeList = _data.Data.Where(a => a.ListType.ToLower() == "shade").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.NattsList = _data.Data.Where(a => a.ListType.ToLower() == "table_natts").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.InclusionList = _data.Data.Where(a => a.ListType.ToLower() == "table_incl").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.LabList = _data.Data.Where(a => a.ListType.ToLower() == "lab").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.LocationList = _data.Data.Where(a => a.ListType.ToLower() == "location" && a.Id > 0).Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderByDescending(c => c.iSr).ToList();
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
            //_data.Data.; GetApiColumnsDetails
            _response = _api.CallAPI(Constants.GetApiColumnsDetails, string.Empty);
            ServiceResponse<ApiColumns> _coldata = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ApiColumns>>(_response);
            List<ColumnsSettingsModel> columns = new List<ColumnsSettingsModel>();
            int index = 0;
            foreach (var item in _coldata.Data)
            {
                columns.Add(new ColumnsSettingsModel()
                {
                    icolumnId = item.iid,
                    iPriority = index + 1,
                    IsActive = false,
                    sCustMiseCaption = item.caption,
                    sUser_ColumnName = item.caption
                });
                index++;
            }
            apiFilter.ColumnList = columns.OrderBy(i => i.iPriority).ToList();

            return View(apiFilter);
        }

        public JsonResult GetAPIData(int TransId)
        {
            var input = new
            {
                sTransId = TransId
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetApiViews, inputJson);
            ServiceResponse<ApiFilterResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ApiFilterResponse>>(_response);
            _data.Data.FirstOrDefault().APIURL1 = "https://www.sunrisediamonds.com.hk/inventory/" + _data.Data.FirstOrDefault().sApiName
                + "." + _data.Data.FirstOrDefault().export_type;
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApiColumns()
        {
            string _response = _api.CallAPI(Constants.GetApiColumnsDetails, string.Empty);
            ServiceResponse<ApiColumns> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ApiColumns>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(ApiDetails apiDetails)
        {
            if (apiDetails != null)
            {
                CommonResponse _data = new CommonResponse();
                if (apiDetails.iTransId > 0)
                    apiDetails.OperationType = "U";
                else
                    apiDetails.OperationType = "I";

                string inputJson = (new JavaScriptSerializer()).Serialize(apiDetails);

                string _response = _api.CallAPI(Constants.SaveApiFilter, inputJson);
                _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
                return Json(_data);
            }
            return Json("");
        }

        public ActionResult Views()
        {
            return View();
        }

        public JsonResult GetApiViews(string fromDate, string ToDate, string type, string value)
        {
            var input = new ApiFilterRequest();
            if (type == "UN" && value != "")
            {
                input = new ApiFilterRequest()
                {
                    dtFromDate = fromDate,
                    dtToDate = ToDate,
                    sUserId = value,
                    sEmpId = "",
                    sFullName = "",
                    sUserName = "",
                    sCompName = "",
                    sCountryName = "",
                    //sPgNo = pageNo,
                    //sPgSize = pageSize
                };
            }
            else if (type == "CM" && value != "")
            {
                input = new ApiFilterRequest()
                {
                    dtFromDate = fromDate,
                    dtToDate = ToDate,
                    sUserId = "",
                    sEmpId = "",
                    sFullName = "",
                    sUserName = "",
                    sCompName = value,
                    sCountryName = ""
                    //sPgNo = pageNo,
                    //sPgSize = pageSize
                };
            }
            else if (type == "CT" && value != "")
            {
                input = new ApiFilterRequest()
                {
                    dtFromDate = fromDate,
                    dtToDate = ToDate,
                    sUserId = "",
                    sEmpId = "",
                    sFullName = "",
                    sUserName = "",
                    sCompName = "",
                    sCountryName = value
                    //sPgNo = pageNo,
                    //sPgSize = pageSize
                };
            }
            else if (type == "CUN" && value != "")
            {
                input = new ApiFilterRequest()
                {
                    dtFromDate = fromDate,
                    dtToDate = ToDate,
                    sUserId = "",
                    sEmpId = "",
                    sFullName = value,
                    sUserName = "",
                    sCompName = "",
                    sCountryName = ""
                    //sPgNo = pageNo,
                    //sPgSize = pageSize
                };
            }
            else
            {
                input = new ApiFilterRequest()
                {
                    dtFromDate = fromDate,
                    dtToDate = ToDate,
                    sUserId = "",
                    sEmpId = "",
                    sFullName = "",
                    sUserName = "",
                    sCompName = "",
                    sCountryName = ""
                    //sPgNo = pageNo,
                    //sPgSize = pageSize
                };
            }

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetApiViews, inputJson);
            ServiceResponse<ApiFilterResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ApiFilterResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteApi(string TransId)
        {
            var input = new
            {
                sTransId = TransId
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DeleteApi, inputJson);
            ServiceResponse<CommonResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CommonResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DownloadApi(string UserId, string TransId, string ApiName, string ExportType)
        {
            var input = new
            {
                UserId = UserId,
                TransId = TransId,
                ApiName = ApiName,
                ExportType = ExportType
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DownloadApi, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadMethod()
        {
            var TransId = Convert.ToInt32(Request.QueryString.Count > 0 && Request.QueryString["TransId"] != "" ? Request.QueryString["TransId"] : "0");
            int For_iUserId = 0;
            var input = new
            {
                ListValue = "DP"
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSearchParameter, inputJson);
            ServiceResponse<ListValueResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ListValueResponse>>(_response);

            UploadMethodModel apiFilter = new UploadMethodModel();

            string _response2 = _api.CallAPI(Constants.GetUserMas, string.Empty);
            ServiceResponse<ColumnsUserResponse> _data2 = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ColumnsUserResponse>>(_response2);
            List<SelectListItem> col2 = new List<SelectListItem>();
            foreach (var a in _data2.Data)
            {
                col2.Add(new SelectListItem
                {
                    Text = a.sUsername + (a.sCompName != "" && a.sCompName != null ? " [" + a.sCompName + "]" : ""),
                    Value = a.iUserid.ToString()
                });
            }
            apiFilter.For_iUserIds = col2.OrderBy(i => i.Text).ToList();
            apiFilter.APIStatus = true;

            if (TransId != 0)
            {
                var input1 = new
                {
                    sTransId = TransId
                };
                string inputJson1 = (new JavaScriptSerializer()).Serialize(input1);
                string _response1 = _api.CallAPI(Constants.GetApiUploadMethod, inputJson1);
                ServiceResponse<ApiUploadMethodResponse> _data1 = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ApiUploadMethodResponse>>(_response1);

                apiFilter.iTransId = _data1.Data[0].iTransId;
                apiFilter.ApiMethod = _data1.Data[0].ApiMethod;
                apiFilter.WebAPIUserName = _data1.Data[0].WebAPIUserName;
                apiFilter.WebAPIPassword = _data1.Data[0].WebAPIPassword;
                apiFilter.FTPHost = _data1.Data[0].FTPHost;
                apiFilter.FTPUser = _data1.Data[0].FTPUser;
                apiFilter.FTPPass = _data1.Data[0].FTPPass;
                apiFilter.FTPType = _data1.Data[0].FTPType;
                apiFilter.FTPExportType = _data1.Data[0].FTPExportType;
                apiFilter.URLUserName = _data1.Data[0].URLUserName;
                apiFilter.URLPassword = _data1.Data[0].URLPassword;
                apiFilter.URLExportType = _data1.Data[0].URLExportType;
                apiFilter.APIUrl = _data1.Data[0].APIUrl;
                apiFilter.APIName = _data1.Data[0].APIName;
                apiFilter.APIStatus = _data1.Data[0].APIStatus;
                apiFilter.For_iUserId = _data1.Data[0].For_iUserId;
                apiFilter.CompanyName = _data1.Data[0].CompanyName;
                
                var input2 = new
                {
                    sTransId = TransId
                };

                string inputJson2 = (new JavaScriptSerializer()).Serialize(input2);
                _response = _api.CallAPI(Constants.GetApiCriteria, inputJson2);
                ServiceResponse<APIFiltersSettings> _coldata2 = (new JavaScriptSerializer()).Deserialize<ServiceResponse<APIFiltersSettings>>(_response);
                List<APIFiltersSettingsModel> columns2 = new List<APIFiltersSettingsModel>();
                int index2 = 0;
                foreach (var item in _coldata2.Data)
                {
                    columns2.Add(new APIFiltersSettingsModel()
                    {
                        Sr = item.Sr,
                        iVendor = item.iVendor,
                        iLocation = item.iLocation,
                        sShape = item.sShape,
                        sPointer = item.sPointer,
                        sColor = item.sColor,
                        sClarity = item.sClarity,
                        sCut = item.sCut,
                        sPolish = item.sPolish,
                        sSymm = item.sSymm,
                        sFls = item.sFls,
                        sLab = item.sLab,
                        dFromLength = item.dFromLength.Value,
                        dToLength = item.dToLength.Value,
                        dFromWidth = item.dFromWidth.Value,
                        dToWidth = item.dToWidth.Value,
                        dFromDepth = item.dFromDepth.Value,
                        dToDepth = item.dToDepth.Value,
                        dFromDepthPer = item.dFromDepthPer.Value,
                        dToDepthPer = item.dToDepthPer.Value,
                        dFromTablePer = item.dFromTablePer.Value,
                        dToTablePer = item.dToTablePer.Value,
                        dFromCrAng = item.dFromCrAng.Value,
                        dToCrAng = item.dToCrAng.Value,
                        dFromCrHt = item.dFromCrHt.Value,
                        dToCrHt = item.dToCrHt.Value,
                        dFromPavAng = item.dFromPavAng.Value,
                        dToPavAng = item.dToPavAng.Value,
                        dFromPavHt = item.dFromPavHt.Value,
                        dToPavHt = item.dToPavHt.Value,
                        dKeyToSymbol = item.dKeyToSymbol,
                        dCheckKTS = item.dCheckKTS,
                        dUNCheckKTS = item.dUNCheckKTS,
                        sBGM = item.sBGM,
                        sCrownBlack = item.sCrownBlack,
                        sTableBlack = item.sTableBlack,
                        sCrownWhite = item.sCrownWhite,
                        sTableWhite = item.sTableWhite,
                        Img = item.Img,
                        Vdo = item.Vdo,
                        PriceMethod = item.PriceMethod,
                        PricePer = item.PricePer
                    });
                    index2++;
                }
                apiFilter.APIFilters = columns2.ToList();
            }
            

            apiFilter.IsModify = (Request.QueryString.Count > 0 && Request.QueryString["Type"].ToLower() == "modify") ? true : false;
            apiFilter.TransId = Convert.ToInt32(Request.QueryString.Count > 0 && Request.QueryString["TransId"] != "" ? Request.QueryString["TransId"] : "0");
            apiFilter.ShapeList = _data.Data.Where(a => a.ListType.ToLower() == "shape").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.ColorList = _data.Data.Where(a => a.ListType.ToLower() == "color").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.CutList = _data.Data.Where(a => a.ListType.ToLower() == "cut").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.ClarityList = _data.Data.Where(a => a.ListType.ToLower() == "clarity").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.PolishList = _data.Data.Where(a => a.ListType.ToLower() == "polish").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.SymmList = _data.Data.Where(a => a.ListType.ToLower() == "symm").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.FlsList = _data.Data.Where(a => a.ListType.ToLower() == "fls").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.ShadeList = _data.Data.Where(a => a.ListType.ToLower() == "shade").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.NattsList = _data.Data.Where(a => a.ListType.ToLower() == "table_natts").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.InclusionList = _data.Data.Where(a => a.ListType.ToLower() == "table_incl").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.LabList = _data.Data.Where(a => a.ListType.ToLower() == "lab").Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).ToList();
            apiFilter.LocationList = _data.Data.Where(a => a.ListType.ToLower() == "location" && a.Id > 0).Select(b => new ListingModel() { iSr = b.Id, sName = b.Value }).OrderBy(c => c.iSr).ToList();
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
                new SelectListItem { Value = "Disc", Text = "Disc" },
                new SelectListItem { Value ="Value", Text = "Value" }

            };

            var input_V = new
            {
                PartyName="",
                ContactPerson = "",
                PartyPrefix = "",
                CountryId=0,
                PageNo = 0,
                PageSize = 0,
                OrderBy = 0
            };
            string inputJson_V = (new JavaScriptSerializer()).Serialize(input_V);
            string _response_V = _api.CallAPI(Constants.PartyInfo, inputJson_V);
            ServiceResponse<PartyInfoResponse> _data_V = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PartyInfoResponse>>(_response_V);
            List<SelectListItem> columns_V = new List<SelectListItem>();

            columns_V.Add(new SelectListItem { Text = "SUNRISE", Value = "SUNRISE" });
            foreach (var item in _data_V.Data)
            {
                columns_V.Add(new SelectListItem
                {
                    Text = item.sPartyName,
                    Value = item.sPartyName
                });
            }
            apiFilter.VendorList = columns_V.ToList();

            //apiFilter.VendorList = new List<SelectListItem>()
            //{
            //    new SelectListItem { Value = "Sunrise", Text = "Sunrise" },
            //    new SelectListItem { Value ="Shairu", Text = "Shairu" }
            //};
            apiFilter.FtpTypeList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "FTP", Text = "FTP" },
                new SelectListItem { Value ="SFTP", Text = "SFTP" }
            };
            //_data.Data.; GetApiColumnsDetails

            _response = _api.CallAPI(Constants.GetApiColumnsDetails, string.Empty);
            ServiceResponse<ApiColumns> _coldata = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ApiColumns>>(_response);
            List<ColumnsSettingsModel> columns = new List<ColumnsSettingsModel>();
            int index = 0;
            foreach (var item in _coldata.Data)
            {
                if (TransId != 0)
                {
                    columns.Add(new ColumnsSettingsModel()
                    {
                        icolumnId = item.iid,
                        iPriority = index + 1,
                        IsActive = false,
                        sCustMiseCaption = item.caption,
                        sUser_ColumnName = item.caption
                    });
                }
                else
                {
                    columns.Add(new ColumnsSettingsModel()
                    {
                        icolumnId = item.iid,
                        iPriority = index + 1,
                        IsActive = Convert.ToInt32(item.Display_Order) == 0 ? false : true,
                        sCustMiseCaption = item.caption,
                        sUser_ColumnName = item.caption
                    });

                }
                
                index++;
            }
            apiFilter.ColumnList = columns.OrderBy(i => i.iPriority).ToList();

            return View(apiFilter);
        }
        public JsonResult SaveUploadMethod(ApiUploadMethod apiuploadmethod)
        {
            if (apiuploadmethod != null)
            {
                CommonResponse _data = new CommonResponse();
                Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);

                string AbsoluteUri = url.AbsoluteUri;
                string AbsolutePath = url.AbsolutePath;
                string mainurl = AbsoluteUri.Replace(AbsolutePath, "");

                if (apiuploadmethod.ApiMethod == "URL")
                {
                    string DecodedUsername = EncodeServerName(apiuploadmethod.URLUserName);
                    string DecodedPassword = EncodeServerName(apiuploadmethod.URLPassword);

                    apiuploadmethod.APIUrl = mainurl + "/Api/URL?UN=" + DecodedUsername + "&PD=" + DecodedPassword + "&TransId=";
                }
                else if (apiuploadmethod.ApiMethod == "WEBAPI")
                {
                    apiuploadmethod.APIUrl = ConfigurationManager.AppSettings["APIURL"] + "/ApiSettings/BasicAuthLog?TransId=";
                }
                else if (apiuploadmethod.ApiMethod == "FTP")
                {
                    apiuploadmethod.APIUrl = mainurl + "/Api/DownloadAPIData?TransId=";
                }

                string inputJson = (new JavaScriptSerializer()).Serialize(apiuploadmethod);
                string _response = _api.CallAPI(Constants.SaveApiUploadMethod, inputJson);
                _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
                return Json(_data);
            }
            return Json("");
        }
        public ActionResult UploadMethodGet()
        {
            return View();
        }
        public JsonResult UploadMethodGetList(string fromDate, string ToDate, string Search, string sTransId, string PageNo, string PageSize, string OrderBy)
        {
            var input = new ApiFilterRequest();
            input = new ApiFilterRequest()
            {
                dtFromDate = fromDate,
                dtToDate = ToDate,
                sSearch = Search,
                sTransId = sTransId,
                sPgNo = PageNo,
                sPgSize = PageSize,
                OrderBy = OrderBy
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetApiUploadMethod, inputJson);
            ServiceResponse<ApiUploadMethodResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ApiUploadMethodResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UploadMethodExcelGet(string fromDate, string ToDate, string Search, string sTransId, string PageNo, string PageSize, string OrderBy)
        {
            var input = new ApiFilterRequest();
            input = new ApiFilterRequest()
            {
                dtFromDate = fromDate,
                dtToDate = ToDate,
                sSearch = Search,
                sTransId = sTransId,
                sPgNo = PageNo,
                sPgSize = PageSize,
                OrderBy = OrderBy
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.ExcelGetApiUploadMethod, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult URL(string UN, string PD, int TransId)
        {
            string username = DecodeServerName(UN);
            string password = DecodeServerName(PD);

            var input = new
            {
                Username = username,
                Password = password,
                TransId = TransId,
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.GetURLApi, inputJson);
            CommonResponse _data = new CommonResponse();
            _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);

            if (_data.Status == "1")
            {
                string path = _data.Message;
                string[] pathArr = path.Split('\\');
                string[] fileArr = pathArr.Last().Split('.');
                string fileName = fileArr.Last().ToString();

                Response.ContentType = fileArr.Last();
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + pathArr.Last() + "\"");
                Response.TransmitFile(_data.Message);
                Response.End();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(_data.Error, JsonRequestBehavior.AllowGet);
            }
        }
        public static string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }
        public static string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }
        public JsonResult DownloadAPIData(string TransId)
        {
            var input = new
            {
                TransId = TransId
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.CallAPIMethod, inputJson);
            CommonResponse _data = new CommonResponse();
            _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            if (_data.Status == "1")
            {
                return Json(_data.Message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(_data.Error, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult UploadMethodReport()
        {
            return View();
        }
        public JsonResult Get_UploadMethodReport(string UserName, int PageNo, int PageSize)
        {
            var input = new
            {
                UserName = UserName,
                PageNo = PageNo,
                PageSize = PageSize
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.Get_UploadMethodReport, inputJson);
            ServiceResponse<GetStockDiscRes> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<GetStockDiscRes>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_UploadMethodReport(string UserName, int PageNo, int PageSize)
        {
            var input = new
            {
                UserName = UserName,
                PageNo = PageNo,
                PageSize = PageSize
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.Excel_UploadMethodReport, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}