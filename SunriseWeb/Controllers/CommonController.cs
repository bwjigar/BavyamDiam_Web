using Lib.Models;
using SunriseWeb.Data;
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
    public class CommonController : BaseController
    {
        API _api = new API();
        // GET: Common
        public ActionResult Indexh()
        {
            return View();
        }
        public JsonResult GetCitystateCountryAutoComplete(string sSearch, int CountryId = 0)
        {
            var input = new
            {
                sSearch = sSearch,
                CountryId = CountryId
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.CityListAutocomplete, inputJson);
            ServiceResponse<CityListAutocompleteResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CityListAutocompleteResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCountryAutoComplete(string sSearch)
        {
            var input = new
            {
                sSearch = sSearch
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.CountryListAutoComplete, inputJson);
            ServiceResponse<CityListAutocompleteResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CityListAutocompleteResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OverseasStockExcelDownload(SearchDiamondsRequest obj, string tabNo)
        {
            obj.PageNo = "0";
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPIWithoutToken(Constants.OverseasStockDownloadDNA, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountryList()
        {
            string _response = _api.CallAPI(Constants.CountryList, string.Empty);
            ServiceResponse<CountryMasterResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<CountryMasterResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StockExcelDownloadBySearchObject(StockDownloadRequest obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.StockDownload, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StockExcelDownloadByStoneId(string StoneId, string FormName, string ActivityType, string ColorType = "", string UsedFor = "")
        {
            var input = new
            {
                StoneID = StoneId,
                FormName = FormName,
                ActivityType = ActivityType,
                PgSize = 0,
                full = "N",
                IsAll = 0,
                ColorType = ColorType,
                UsedFor = UsedFor
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.StockDownload, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StockExcelDownloadByStoneIdWithoutToken(string StoneId)
        {
            var input = new
            {
                StoneID = StoneId,
                PgSize = 0,
                full = "N"
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.StockDownloadWithoutToken, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult WishExcelDownloadByStoneId(string IsAdmin,string RefNo, string Location, string Shape, string Color, string Polish, 
            string Pointer, string Lab,string Fls, string Clarity, string Cut, string Symm, string PageNo, string FormName, string ActivityType,
            bool SubUser = false, string iUserid_certi_no = "", bool IsAssistBy = false)
        {
            var input = new
            {
                IsAdmin = IsAdmin,
                RefNo = RefNo,
                Location = Location,
                Shape = Shape,
                Color = Color,
                Polish = Polish,
                Pointer = Pointer,
                Lab = Lab,
                Fls = Fls,
                Clarity = Clarity,
                Cut = Cut,
                Symm = Symm,
                PageNo = PageNo,
                FormName = FormName,
                ActivityType = ActivityType,
                SubUser = SubUser,
                iUserid_certi_no = iUserid_certi_no,
                IsAssistBy = IsAssistBy
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DownloadWishList, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CartExcelDownloadByStoneId(ViewCartRequest req)
        {
            var input = new
            {
                RefNo1 = req.RefNo1,
                RefNo = req.RefNo,
                OfferTrans = "",
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
                PageNo = req.PageNo,
                FromDate = req.FromDate,
                ToDate = req.ToDate,
                Status = req.Status,
                CompanyName = req.CompanyName,
                FormName = req.FormName,
                ActivityType = req.ActivityType,
                SubUser = req.SubUser
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.CartStockDownload, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PairSearchExcelDownloadBySearchObject(SearchDiamondsRequest obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.DownloadPairSearchExcel, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PairSearchMediaDownloadBySearchObject(SearchDiamondsRequest obj, string MediaType)
        {
            obj.PageNo = "0";
            obj.DownloadMedia = MediaType;
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.DownloadPairSearchMedia, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CartMediaDownloadBySearchObject(ViewCartRequest obj)
        {
            obj.PageNo = "0";
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.DownloadCartSearchMedia, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StockMediaDownloadBySearchObject(SearchDiamondsRequest obj, string MediaType)
        {
            obj.PageNo = "0";
            obj.DownloadMedia = MediaType;
            string inputJson = (new JavaScriptSerializer()).Serialize(obj);
            string _response = _api.CallAPI(Constants.DownloadStockMedia, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StockMediaDownloadByStoneId(string StoneId, string MediaType, string FormName, string ActivityType, string UsedFor)
        {
            var input = new
            {
                StoneID = StoneId,
                PageNo = 0,
                DownloadMedia = MediaType,
                FormName = FormName,
                ActivityType = ActivityType,
                UsedFor = UsedFor
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DownloadStockMedia, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StockMediaDownloadByStoneIdWithoutToken(string StoneId, string MediaType)
        {
            var input = new
            {
                StoneID = StoneId,
                PageNo = 0,
                DownloadMedia = MediaType
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.DownloadStockMediaWithoutToken, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OverseasDNAMediaDownloadByURLWithoutToken(string URL, string Type, string StoneId)
        {
            var input = new
            {
                URL = URL,
                Type = Type,
                StoneId = StoneId
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.OverseasDNAMediaDownloadByURL, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetParameter(string ListValue)
        {
            var input = new
            {
                ListValue = ListValue
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSearchParameter, inputJson);
            ServiceResponse<ListValueResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<ListValueResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompareStones(string stoneNo)
        {
            var input = new
            {
                StoneID = stoneNo
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetSearchStock, inputJson);
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
    }
}