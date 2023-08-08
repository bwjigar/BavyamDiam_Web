using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using SunriseWeb.Helper;
using SunriseWeb.Models;
using SunriseWeb.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class SearchStockController : BaseController
    {
        API _api = new API();
        // GET: SearchStock
        public ActionResult Search(string type = "", string Set = "")
        {
            if (type == "" && Set == "")
            {
                Session["RecentSearchDiamondStock"] = null;
                Session["SavedSearchDiamondStock"] = null;
                Session["QuickSearchDiamondStock"] = null;
                Session["SmartSearchDiamondStock"] = null;
                Session["SmartSearchDisplayed"] = null;
            }
            ViewBag.type = type;
            ViewBag.Set = Set;
            return View();
        }

        public ActionResult SmartSearchURLSet(SmartSearchURL obj)
        {
            Session["SmartSearchDisplayed"] = null;
            if (obj.URL != "")
            {
                Session["SmartSearchURL"] = obj;
            }
            ViewBag.SmartSearchURL = obj;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult SmartSearchURLGet()
        {
            SmartSearchURL obj = (SmartSearchURL)Session["SmartSearchURL"];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RecentSearch()
        {
            return View();
        }
        public ActionResult QuickSearch()
        {
            return View();
        }
        public ActionResult SaveSearch()
        {
            return View();
        }
        public ActionResult CurrencyCountryListGet(int iCountryId = 0)
        {
            var input = new
            {
                iCountryId = iCountryId
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.CurrencyCountryList, inputJson);
            ServiceResponse<CurrencyCountryListDetail> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CurrencyCountryListDetail>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExpireAvailStock(int iId)
        {
            var input = new {
                iId = iId
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.ExpireAvailStock, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStockAvail()
        {
            var input = new { };
            string inputJson = (new JavaScriptSerializer()).Serialize(input); 
            string _response = _api.CallAPI(Constants.GetStockAvail, inputJson);
            ServiceResponse<SearchDiamondsRequest> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SearchDiamondsRequest>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StoneDetail(string StoneNo)
        {
            var input = new
            {
                StoneID = StoneNo
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSearchStockByStoneID, inputJson);
            SearchStone _data = (new JavaScriptSerializer()).Deserialize<SearchStone>(_response);
            return View(_data);
        }

        public ActionResult StoneDetail1(string StoneNo)
        {
            var input = new
            {
                StoneID = StoneNo
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSearchStockByStoneID, inputJson);
            SearchStone _data = (new JavaScriptSerializer()).Deserialize<SearchStone>(_response);
            return View(_data);
        }

        public JsonResult GetRecentSearchList()
        {
            string _response = _api.CallAPI(Constants.GetRecentSearch, string.Empty);
            ServiceResponse<RecentSearchResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<RecentSearchResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Dashboard_GetRecentSearchList()
        {
            string _response = _api.CallAPI(Constants.Dashboard_GetRecentSearch, string.Empty);
            ServiceResponse<RecentSearchResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<RecentSearchResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSavedSearchList()
        {
            string _response = _api.CallAPI(Constants.GetSavedSearch, string.Empty);
            ServiceResponse<UserSearchResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserSearchResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Dashboard_GetSavedSearchList()
        {
            string _response = _api.CallAPI(Constants.Dashboard_GetSavedSearch, string.Empty);
            ServiceResponse<UserSearchResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserSearchResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSearchParameter()
        {
            var input = new
            {
                ListValue = "DP"
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSearchParameter, inputJson);
            ServiceResponse<ListValueResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ListValueResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetKeyToSymbolList()
        {
            string _response = _api.CallAPI(Constants.GetKeyToSymbolList, string.Empty);
            ServiceResponse<KeyToSymbolResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<KeyToSymbolResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetColumnsCaptionsList()
        {
            string _response = _api.CallAPI(Constants.GetColumnsCaptionsList, string.Empty);
            ServiceResponse<ColumnsCaptionsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ColumnsCaptionsResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetColumnsConfigForSearch()
        {
            string _response = _api.CallAPI(Constants.GetColumnsConfigForSearch, string.Empty);
            ServiceResponse<ColumnsConfigForSearchResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ColumnsConfigForSearchResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSearchStock(SearchDiamondsRequest obj, string tabNo)
        {
            Session["SearchDiamondStock" + tabNo + ""] = obj;
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.GetSearchStock, inputJson);
            ServiceResponse<SearchDiamondsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SearchDiamondsResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveGetSearchStock(SearchDiamondsRequest obj, string tabNo)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.SaveNoFoundSearchStock, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchStockExcelDownload(StockDownloadRequest obj, string tabNo)
        {
            SearchDiamondsRequest objSession = (SearchDiamondsRequest)Session["SearchDiamondStock" + tabNo + ""];
            obj.Pointer = objSession.Pointer;
            obj.Shape = objSession.Shape;
            obj.Lab = objSession.Lab;
            obj.Color = objSession.Color;
            obj.Polish = objSession.Polish;
            obj.Clarity = objSession.Clarity;
            obj.Cut = objSession.Cut;
            obj.Symm = objSession.Symm;
            obj.Fls = objSession.Fls;
            obj.Location = objSession.Location;
            obj.FormName = obj.FormName;
            obj.ActivityType = obj.ActivityType;
            obj.IsAll = 1;

            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.StockDownload, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchStockMediaDownload(SearchDiamondsRequest obj, string MediaType, string tabNo)
        {
            SearchDiamondsRequest objSession = (SearchDiamondsRequest)Session["SearchDiamondStock" + tabNo + ""];
            obj.Pointer = objSession.Pointer;
            obj.Shape = objSession.Shape;
            obj.Lab = objSession.Lab;
            obj.Color = objSession.Color;
            obj.Polish = objSession.Polish;
            obj.Clarity = objSession.Clarity;
            obj.Cut = objSession.Cut;
            obj.Symm = objSession.Symm;
            obj.Fls = objSession.Fls;
            obj.Location = objSession.Location;
            obj.FormName = obj.FormName;
            obj.ActivityType = obj.ActivityType;

            obj.PageNo = "0";
            obj.DownloadMedia = MediaType;
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.DownloadStockMedia, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OrderHistoryMediaDownload(SearchDiamondsRequest obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.OrderHistoryMediaDownload, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchStockEmailStone(SearchDiamondsRequest SearchCriteria, string ToAddress, string Comments, string tabNo, bool isRevised = false)
        {
            SearchDiamondsRequest objSession = (SearchDiamondsRequest)Session["SearchDiamondStock" + tabNo + ""];
            SearchCriteria.Pointer = objSession.Pointer;
            SearchCriteria.Shape = objSession.Shape;
            SearchCriteria.Lab = objSession.Lab;
            SearchCriteria.Color = objSession.Color;
            SearchCriteria.Polish = objSession.Polish;
            SearchCriteria.Clarity = objSession.Clarity;
            SearchCriteria.Cut = objSession.Cut;
            SearchCriteria.Symm = objSession.Symm;
            SearchCriteria.Fls = objSession.Fls;
            SearchCriteria.Location = objSession.Location;

            var input = new
            {
                SearchCriteria = SearchCriteria,
                ToAddress = ToAddress,
                Comments = Comments,
                IsRevised = isRevised,
                IsAll = 1
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.EmailAllStones, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModifyStockParameter(string tabNo)
        {
            SearchDiamondsRequest obj = (SearchDiamondsRequest)Session["SearchDiamondStock" + tabNo + ""];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddToCart(string stoneNo, string transType)
        {
            var input = new
            {
                StoneID = stoneNo,
                OfferTrans = "",
                TransType = transType
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.AddRemoveToCart, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddOverseasToCart(string stoneNo)
        {
            var input = new
            {
                StoneID = stoneNo,
                OfferTrans = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.AddOverseasToCart, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveToCart(string removeToCart)
        {
            var input = new
            {
                removeToCart = removeToCart
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.RemoveToCart, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddToWishlist(string stoneNo, string transType)
        {
            var input = new
            {
                StoneID = stoneNo,
                TransType = transType
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.AddRemoveToWishList, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CartEmailSelectedStone(ViewCartRequest req)
        {
            var input = new
            {
                OfferTrans = req.OfferTrans,
                RefNo = req.RefNo,
                ToAddress = req.ToAddress,
                Comments = req.Comments,
                isAdmin = req.isAdmin,
                isEmp = req.isEmp,
                Location = req.Location,
                Shape = req.Shape,
                Color = req.Color,
                Polish = req.Polish,
                Pointer = req.Pointer,
                Lab = req.Lab,
                Fls = req.Fls,
                Clarity = req.Clarity,
                Cut = req.Cut,
                Symm = req.Symm,
                FromDate = req.FromDate,
                ToDate = req.ToDate,
                RefNo1 = req.RefNo1,
                CompanyName = req.CompanyName,
                PageNo = "0",
                SubUser = req.SubUser
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.CartEmailStones, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PairEmailSelectedStone(SearchDiamondsRequest SearchCriteria, string ToAddress, string Comments)
        {
            SearchCriteria.PageNo = "0";
            var input = new
            {
                SearchCriteria = SearchCriteria,
                ToAddress = ToAddress,
                Comments = Comments
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.PairEmailStones, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmailSelectedStone(string StoneID, string ToAddress, string Comments,string FormName,string ActivityType, string ColorType = "")
        {
            var input = new
            {
                StoneID = StoneID,
                ToAddress = ToAddress,
                Comments = Comments,
                FormName = FormName,
                ActivityType = ActivityType,
                IsAll = 0,
                ColorType = ColorType
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.SelectedEmailStones, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MyWishlist_EmailSelectedStone(ViewWishListRequest req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.MyWishlistEmailStones, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EmailAllStone(SearchDiamondsRequest SearchCriteria, string ToAddress, string Comments, bool isRevised = false)
        {
            var input = new
            {
                SearchCriteria = SearchCriteria,
                ToAddress = ToAddress,
                Comments = Comments,
                IsRevised = isRevised
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.EmailAllStones, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult StoneSmartSearch(string StoneList)
        {
            SearchDiamondsRequest obj = new SearchDiamondsRequest();
            obj.SmartSearch = StoneList;
            Session["SmartSearchDiamondStock"] = obj;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult StoreSmartSearchGet()
        {
            SearchDiamondsRequest obj = (SearchDiamondsRequest)Session["SmartSearchDiamondStock"];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SmartSearchDisplayedSet(bool status)
        {
            Session["SmartSearchDisplayed"] = status;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult SmartSearchDisplayedGet()
        {
            bool status = Convert.ToBoolean(Session["SmartSearchDisplayed"]);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult RecentSearchDataSessionStore(SearchDiamondsRequest obj)
        {
            Session["RecentSearchDiamondStock"] = obj;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult RecentSearchDataSessionGet()
        {
            SearchDiamondsRequest obj = (SearchDiamondsRequest)Session["RecentSearchDiamondStock"];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SavedSearchDataSessionStore(SearchDiamondsRequest obj)
        {
            Session["SavedSearchDiamondStock"] = obj;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult SavedSearchDataSessionGet()
        {
            SearchDiamondsRequest obj = (SearchDiamondsRequest)Session["SavedSearchDiamondStock"];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ModifySavedSearchDataSessionStore(SearchDiamondsRequest obj)
        {
            Session["ModifySavedSearchDiamondStock"] = obj;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModifySavedSearchDataSessionGet()
        {
            SearchDiamondsRequest obj = (SearchDiamondsRequest)Session["ModifySavedSearchDiamondStock"];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChartSearchDataSessionStore(SearchDiamondsRequest obj)
        {
            Session["ChartSearchDiamondStock"] = obj;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChartSearchDataSessionGet()
        {
            SearchDiamondsRequest obj = (SearchDiamondsRequest)Session["ChartSearchDiamondStock"];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteUserSearch(int SearchID)
        {
            var input = new
            {
                SearchID = SearchID
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DeleteUserSearch, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetQuickSearchList(string cut, string Fls, string FormName, string ActivityType)
        {
            var input = new
            {
                Cut = cut,
                Fls = Fls,
                FormName = FormName,
                ActivityType = ActivityType
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetQuickSearch, inputJson);
            ServiceResponse<QuickSearchResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<QuickSearchResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSubQuickSearchData(string Pointer, string ColorGroup, string PurityGroup, string cut, string Fls)
        {
            var input = new
            {
                Cut = cut,
                Fls = Fls,
                Pointer = Pointer,
                ColorGroup = ColorGroup,
                PurityGroup = PurityGroup
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSubQuickSearch, inputJson);
            ServiceResponse<SubQuickSearchResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SubQuickSearchResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetFinalQuickSearchData(SearchDiamondsRequest obj)
        {
            Session["QuickSearchDiamondStock"] = obj;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult QuickSearchDataSessionGet()
        {
            SearchDiamondsRequest obj = (SearchDiamondsRequest)Session["QuickSearchDiamondStock"];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RevisedPrice()
        {
            return View();
        }
        public JsonResult SaveSearchData(string SearchId, string Searchname, SearchDiamondsRequest model)
        {
            model.SearchID = SearchId;
            model.SearchName = Searchname;
            string inputJson = (new JavaScriptSerializer()).Serialize(model);
            string _response = _api.CallAPI(Constants.UserSaveSearch, inputJson);
            ServiceResponse<CommonResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CommonResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GET_Scheme_Disc()
        {
            string _response = _api.CallAPI(Constants.GET_Scheme_Disc, string.Empty);
            ServiceResponse<Scheme_Disc> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Scheme_Disc>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FancyColor_Image_Status_Get()
        {
            string _response = _api.CallAPI(Constants.FancyColor_Image_Status_Get, string.Empty);
            ServiceResponse<FancyColor_Image> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<FancyColor_Image>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FancyColor_Image_Status_Set()
        {
            string _response = _api.CallAPI(Constants.FancyColor_Image_Status_Set, string.Empty);
            ServiceResponse<FancyColor_Image> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<FancyColor_Image>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OrderHistory_Video_Status_Get()
        {
            string _response = _api.CallAPI(Constants.OrderHistory_Video_Status_Get, string.Empty);
            ServiceResponse<OrderHistory_Video> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderHistory_Video>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRevisedPriceStock(int pageNO, string certiNo, string Location, string Shape, 
            string Color, string Polish, string Pointer, string Lab, string Fls, string Clarity, string Cut, string Symm)
        {
            var input = new
            {
                pageNO = pageNO,
                certiNo = certiNo,
                Location = Location,
                Shape = Shape,
                Color = Color,
                Polish = Polish,
                Pointer = Pointer,
                Lab = Lab,
                Fls = Fls,
                Clarity = Clarity,
                Cut = Cut,
                Symm = Symm
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.RevisedStock, inputJson);
            ServiceResponse<SearchDiamondsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SearchDiamondsResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        #region Overseas
        public ActionResult OverseasSearch()
        {
            return View();
        }
        public JsonResult GetOverseasColumnsConfigForSearch()
        {
            string _response = _api.CallAPI(Constants.GetOverseasColumnsConfigForSearch, string.Empty);
            ServiceResponse<ColumnsConfigForSearchResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ColumnsConfigForSearchResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetModifyOverseasStockParameter(string tabNo)
        {
            SearchDiamondsRequest obj = (SearchDiamondsRequest)Session["SearchDiamondOverseasStock" + tabNo + ""];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSearchOverseasStock(SearchDiamondsRequest obj, string tabNo)
        {
            Session["SearchDiamondOverseasStock" + tabNo + ""] = obj;
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.GetSearchOverseasStock, inputJson);
            ServiceResponse<SearchDiamondsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SearchDiamondsResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OverseasStockExcelDownload(SearchDiamondsRequest obj, string tabNo)
        {
            if (tabNo != "0")
            {
                SearchDiamondsRequest objSession = (SearchDiamondsRequest)Session["SearchDiamondOverseasStock" + tabNo + ""];
                obj.Pointer = objSession.Pointer;
                obj.Shape = objSession.Shape;
                obj.Lab = objSession.Lab;
                obj.Color = objSession.Color;
                obj.Polish = objSession.Polish;
                obj.Clarity = objSession.Clarity;
                obj.Cut = objSession.Cut;
                obj.Symm = objSession.Symm;
                obj.Fls = objSession.Fls;
                obj.Location = objSession.Location;
                obj.FormName = obj.FormName;
                obj.ActivityType = obj.ActivityType;
            }

            obj.PageNo = "0";
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.OverseasStockDownload, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SaveOverseasColumnsSettings(List<ColumnsSettings> obj)
        {
            var input = new
            {
                ColumnsSettings = obj
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.SaveOverseasColumnsSettings, inputJson);
            ServiceResponse<ColumnsSettingsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ColumnsSettingsResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompareOverseasStones(string stoneNo)
        {
            var input = new
            {
                StoneID = stoneNo
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSearchOverseasStock, inputJson);
            ServiceResponse<SearchDiamondsResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SearchDiamondsResponse>>(_response);

            ServiceResponse<CompareStoneResult> _compareStone = new ServiceResponse<CompareStoneResult>();
            if (_data.Status == "1" && _data.Data.Count > 0)
            {
                _compareStone.Data = new List<CompareStoneResult>();
                CompareStoneResult res = new CompareStoneResult();
                foreach (var item in _data.Data.FirstOrDefault().DataList)
                {
                    res.ReferenceNo.Add(string.IsNullOrEmpty(item.stone_ref_no) ? "-" : item.stone_ref_no);
                    if (Convert.ToBoolean(item.bPRimg))
                    {
                        res.Imge1.Add(Constants.ImageCdnLink + item.certi_no + "/PR.jpg");
                    }
                    else
                    {
                        res.Imge1.Add("");
                    }

                    res.Status.Add(string.IsNullOrEmpty(item.status) ? "-" : item.status);
                    res.Shape.Add(string.IsNullOrEmpty(item.shape) ? "-" : item.shape);
                    res.Lab.Add(string.IsNullOrEmpty(item.lab) ? "-" : item.lab);
                    res.CertiNo.Add(string.IsNullOrEmpty(item.certi_no) ? "-" : item.certi_no);
                    res.Shade.Add(string.IsNullOrEmpty(item.shade) ? "-" : item.shade);
                    res.Color.Add(string.IsNullOrEmpty(item.color) ? "-" : item.color);
                    res.Clarity.Add(string.IsNullOrEmpty(item.clarity) ? "-" : item.clarity);
                    res.CaratWeight.Add(string.IsNullOrEmpty(item.cts.ToString()) ? "-" : item.cts.ToString());
                    res.RapPrice.Add(string.IsNullOrEmpty(item.cur_rap_rate.ToString()) ? "-" : item.cur_rap_rate.ToString());
                    res.RapAmt.Add(string.IsNullOrEmpty(item.rap_amount.ToString()) ? "-" : item.rap_amount.ToString());
                    res.Disc.Add(string.IsNullOrEmpty(item.sales_disc_per.ToString()) ? "-" : item.sales_disc_per.ToString());
                    res.Cut.Add(string.IsNullOrEmpty(item.cut) ? "-" : item.cut);
                    res.Polish.Add(string.IsNullOrEmpty(item.polish) ? "-" : item.polish);
                    res.Symmetry.Add(string.IsNullOrEmpty(item.symm) ? "-" : item.symm);
                    res.Flurescence.Add(string.IsNullOrEmpty(item.fls) ? "-" : item.fls);
                    res.Length.Add(string.IsNullOrEmpty(item.length.ToString()) ? "-" : item.length.ToString());
                    res.Width.Add(string.IsNullOrEmpty(item.width.ToString()) ? "-" : item.width.ToString());
                    res.Depth.Add(string.IsNullOrEmpty(item.depth.ToString()) ? "-" : item.depth.ToString());
                    res.TotalDepth.Add(string.IsNullOrEmpty(item.depth_per.ToString()) ? "-" : item.depth_per.ToString());
                    res.Table.Add(string.IsNullOrEmpty(item.table_per.ToString()) ? "-" : item.table_per.ToString());
                    res.KeytoSymbol.Add(string.IsNullOrEmpty(item.symbol) ? "-" : item.symbol);
                    res.table_natts.Add(string.IsNullOrEmpty(item.table_natts) ? "-" : item.table_natts);
                    res.Crown_Natts.Add(string.IsNullOrEmpty(item.Crown_Natts) ? "-" : item.Crown_Natts);
                    res.inclusion.Add(string.IsNullOrEmpty(item.inclusion) ? "-" : item.inclusion);
                    res.Crown_Inclusion.Add(string.IsNullOrEmpty(item.Crown_Inclusion) ? "-" : item.Crown_Inclusion);
                    res.CrAng.Add(string.IsNullOrEmpty(item.crown_angle.ToString()) ? "-" : item.crown_angle.ToString());
                    res.CrHt.Add(string.IsNullOrEmpty(item.crown_height.ToString()) ? "-" : item.crown_height.ToString());
                    res.PavAng.Add(string.IsNullOrEmpty(item.pav_angle.ToString()) ? "-" : item.pav_angle.ToString());
                    res.PavHt.Add(string.IsNullOrEmpty(item.pav_height.ToString()) ? "-" : item.pav_height.ToString());
                    res.GirdleType.Add(string.IsNullOrEmpty(item.girdle_type) ? "-" : item.girdle_type);
                    res.net_amount.Add(string.IsNullOrEmpty(item.net_amount.ToString()) ? "-" : item.net_amount.ToString());

                }
                _compareStone.Data.Add(res);
            }
            else
            {
                _compareStone.Data = new List<CompareStoneResult>();
            }
            _compareStone.Status = _data.Status;
            _compareStone.Message = _data.Message;
            return Json(_compareStone, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmailAllOverseasStone(SearchDiamondsRequest SearchCriteria, string ToAddress, string Comments, string tabNo)
        {
            SearchDiamondsRequest objSession = (SearchDiamondsRequest)Session["SearchDiamondOverseasStock" + tabNo + ""];

            SearchCriteria.Pointer = objSession.Pointer;
            SearchCriteria.Shape = objSession.Shape;
            SearchCriteria.Lab = objSession.Lab;
            SearchCriteria.Color = objSession.Color;
            SearchCriteria.Polish = objSession.Polish;
            SearchCriteria.Clarity = objSession.Clarity;
            SearchCriteria.Cut = objSession.Cut;
            SearchCriteria.Symm = objSession.Symm;
            SearchCriteria.Fls = objSession.Fls;
            SearchCriteria.Location = objSession.Location;

            var input = new
            {
                SearchCriteria = SearchCriteria,
                ToAddress = ToAddress,
                Comments = Comments
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.EmailAllOverseasStone, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OverseasStockMediaDownload(SearchDiamondsRequest obj, string MediaType, string tabNo)
        {
            SearchDiamondsRequest objSession = (SearchDiamondsRequest)Session["SearchDiamondOverseasStock" + tabNo + ""];
            obj.Pointer = objSession.Pointer;
            obj.Shape = objSession.Shape;
            obj.Lab = objSession.Lab;
            obj.Color = objSession.Color;
            obj.Polish = objSession.Polish;
            obj.Clarity = objSession.Clarity;
            obj.Cut = objSession.Cut;
            obj.Symm = objSession.Symm;
            obj.Fls = objSession.Fls;
            obj.Location = objSession.Location;
            obj.FormName = obj.FormName;
            obj.ActivityType = obj.ActivityType;

            obj.PageNo = "0";
            obj.DownloadMedia = MediaType;
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.OverseasStockMediaDownload, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        #endregion
        public JsonResult Hold_Stone_Avail_Customers(SearchDiamondsRequest req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Hold_Stone_Avail_Customers, inputJson);
            ServiceResponse<CommonResponse> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CommonResponse>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PlaceOrder(string stoneNo, string Hold_StoneID = "", string comments = "")
        {
            var input = new
            {
                StoneID = stoneNo,
                Hold_StoneID = Hold_StoneID,
                Comments = comments
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.ConfirmOrder, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PlaceOrder_Web(ConfirmOrderRequest_Web req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.PurchaseConfirmOrder_Web, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PlaceOrder_Web_1(ConfirmOrderRequest_Web_1 req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.PurchaseConfirmOrder_Web_1, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Final_PlaceOrder(Final_PlaceOrderRequest req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.Final_PlaceOrder, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HoldStone_1(HoldStoneRequest_1 req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.HoldStone_1, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReleaseStone_1(HoldStoneRequest_1 req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.ReleaseStone_1, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}