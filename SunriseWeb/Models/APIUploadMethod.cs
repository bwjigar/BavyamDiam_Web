using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunriseWeb.Models
{
    public class APIUploadMethod
    {
        public string ApiMethod { get; set; }
        //public string FtpMethod { get; set; }
        //public string UrlMethod { get; set; }
        public string WebAPIUserName { get; set; }
        public string WebAPIPassword { get; set; }
        public string FTPUserName { get; set; }
        public string FTPPassword { get; set; }
        public string FTPHost { get; set; }
        public string FTPUser { get; set; }
        public string FTPPass { get; set; }
        public string FtpType { get; set; }
        public string URLUserName { get; set; }
        public string URLPassword { get; set; }
        public string URLExportType { get; set; }

        public string Img { get; set; }
        public string Vdo { get; set; }
        
        public List<ListingModel> ShapeList { get; set; }
        public List<ListingModel> ColorList { get; set; }
        public List<ListingModel> ClarityList { get; set; }
        public List<ListingModel> CutList { get; set; }
        public List<ListingModel> PolishList { get; set; }
        public List<ListingModel> SymmList { get; set; }
        public List<ListingModel> FlsList { get; set; }
        public List<ListingModel> ShadeList { get; set; }
        public List<ListingModel> NattsList { get; set; }
        public List<ListingModel> InclusionList { get; set; }
        public List<ListingModel> LabList { get; set; }
        public List<ListingModel> LocationList { get; set; }
        public List<SelectListItem> ExportTypeList { get; set; }
        public List<SelectListItem> FtpTypeList { get; set; }
        public List<SelectListItem> OccuranceList { get; set; }
        public List<SelectListItem> SeparatorList { get; set; }
        public List<ListingModel> BGMList { get; set; }
        public List<ListingModel> CrnBlackList { get; set; }
        public List<ListingModel> TblBlackList { get; set; }
        public List<ListingModel> CrnWhiteList { get; set; }
        public List<ListingModel> TblWhiteList { get; set; }
        public List<SelectListItem> PricingMethodList { get; set; }
        public List<SelectListItem> VendorList { get; set; }
        public string shapeLst { get; set; }
        public string colorLst { get; set; }
        public string clarityLst { get; set; }
        public string cutLst { get; set; }
        public string polishLst { get; set; }
        public string symLst { get; set; }
        public string fluoLst { get; set; }
        public string locationLst { get; set; }
        public string labLst { get; set; }
        public string ShadeLst { get; set; }
        public string NattsLst { get; set; }
        public string InclusionLst { get; set; }
        public string BgmLst { get; set; }
        public string CrnBlackLst { get; set; }
        public string TblBlackLst { get; set; }
        public string CrnWhiteLst { get; set; }
        public string TblWhiteLst { get; set; }

        public string FromCarat { get; set; }
        public string ToCarat { get; set; }
        public string FromDisc { get; set; }
        public string ToDisc { get; set; }
        public string CaratValue { get; set; }
        public string FromLength { get; set; }
        public string ToLength { get; set; }
        public string FromWidth { get; set; }
        public string ToWidth { get; set; }
        public string FromDepth { get; set; }
        public string ToDepth { get; set; }
        public string FromDepthPer { get; set; }
        public string ToDepthPer { get; set; }
        public string FromTablePer { get; set; }
        public string ToTablePer { get; set; }
        public string FromCrAng { get; set; }
        public string ToCrAng { get; set; }
        public string FromCrHt { get; set; }
        public string ToCrHt { get; set; }
        public string FromPavAng { get; set; }
        public string ToPavAng { get; set; }
        public string FromPavHt { get; set; }
        public string ToPavHt { get; set; }
        public string APIName { get; set; }
        

        public string UploadTime { get; set; }
        public string Separator { get; set; }
        public string Repeat { get; set; }
        public string Email { get; set; }
        public string sMailUploadTime { get; set; }
        public string FromLocation { get; set; }
        public bool ApiUrl { get; set; }
        public string APIURL1 { get; set; }
        public bool IsModify { get; set; }
        public int TransId { get; set; }
        public string dTransDate { get; set; }
        public string PricingMethod { get; set; }
        public string Vendor { get; set; }
        public double Percentage { get; set; }
        public List<ColumnsSettingsModel> ColumnList { get; set; }

        public APIUploadMethod()
        {
            ShapeList = new List<ListingModel>();
            ColorList = new List<ListingModel>();
            ClarityList = new List<ListingModel>();
            CutList = new List<ListingModel>();
            PolishList = new List<ListingModel>();
            SymmList = new List<ListingModel>();
            FlsList = new List<ListingModel>();
            ShadeList = new List<ListingModel>();
            NattsList = new List<ListingModel>();
            InclusionList = new List<ListingModel>();
            LabList = new List<ListingModel>();
            LocationList = new List<ListingModel>();
            OccuranceList = new List<SelectListItem>();
            ExportTypeList = new List<SelectListItem>();
            FtpTypeList = new List<SelectListItem>();
            SeparatorList = new List<SelectListItem>();
            BGMList = new List<ListingModel>();
            CrnBlackList = new List<ListingModel>();
            TblBlackList = new List<ListingModel>();
            CrnWhiteList = new List<ListingModel>();
            TblWhiteList = new List<ListingModel>();
            PricingMethodList = new List<SelectListItem>();
            VendorList = new List<SelectListItem>();
            FromCarat = "0.00";
            ToCarat = "0.00";
            FromLength = "0.00";
            ToLength = "0.00";
            FromWidth = "0.00";
            ToWidth = "0.00";
            FromDepth = "0.00";
            ToDepth = "0.00";
            FromDepthPer = "0.00";
            ToDepthPer = "0.00";
            FromTablePer = "0.00";
            ToTablePer = "0.00";
            FromCrAng = "0.00";
            ToCrAng = "0.00";
            FromCrHt = "0.00";
            ToCrHt = "0.00";
            FromPavAng = "0.00";
            ToPavHt = "0.00";
            FromDisc = "0.00";
            ToDisc = "0.00";

            ColumnList = new List<ColumnsSettingsModel>();
        }

    }
}