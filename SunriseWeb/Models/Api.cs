using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunriseWeb.Models
{
    public class ApiFilterModel
    {
        public List<ListingModel> SupplierList { get; set; }
        public List<ListingModel> ShapeList { get; set; }
        public List<ListingModel> PointerList { get; set; }
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
        public List<SelectListItem> OccuranceList { get; set; }
        public List<SelectListItem> SeparatorList { get; set; }

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
        public string ExportType { get; set; }
        public string FTPName { get; set; }
        public string FTPUser { get; set; }
        public string FTPPassword { get; set; }
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

        public List<ColumnsSettingsModel> ColumnList { get; set; }
        public ApiFilterModel()
        {
            SupplierList = new List<ListingModel>();
            ShapeList = new List<ListingModel>();
            PointerList = new List<ListingModel>();
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
            SeparatorList = new List<SelectListItem>();

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

    public class UploadMethodModel
    {
        public long? iTransId { get; set; }
        public string ApiMethod { get; set; }
        public string WebAPIUserName { get; set; }
        public string WebAPIPassword { get; set; }
        public string FTPHost { get; set; }
        public string FTPUser { get; set; }
        public string FTPPass { get; set; }
        public string FTPType { get; set; }
        public string FTPExportType { get; set; }
        public string URLUserName { get; set; }
        public string URLPassword { get; set; }
        public string URLExportType { get; set; }
        public string Vendor { get; set; }
        public string Location { get; set; }
        public string Img { get; set; }
        public string Vdo { get; set; }
        public string PricingMethod { get; set; }
        public double? Percentage { get; set; }
        public string APIName { get; set; }
        public bool APIStatus { get; set; }
        public string APIUrl { get; set; }
        public string CompanyName { get; set; }
        public int For_iUserId { get; set; }

        public List<SelectListItem> For_iUserIds { get; set; }
        public List<ListingModel> SupplierList { get; set; }
        public List<ListingModel> ShapeList { get; set; }
        public List<ListingModel> PointerList { get; set; }
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
        public List<ListingModel> BGMList { get; set; }
        public List<ListingModel> CrnBlackList { get; set; }
        public List<ListingModel> CrnWhiteList { get; set; }
        public List<ListingModel> TblBlackList { get; set; }
        public List<ListingModel> TblWhiteList { get; set; }
        public List<SelectListItem> ExportTypeList { get; set; }
        public List<SelectListItem> OccuranceList { get; set; }
        public List<SelectListItem> SeparatorList { get; set; }
        public List<SelectListItem> PricingMethodList { get; set; }
        public List<SelectListItem> VendorList { get; set; }
        public List<SelectListItem> FtpTypeList { get; set; }

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
        public string ExportType { get; set; }
        public string FTPName { get; set; }
        public string UploadTime { get; set; }
        public string Separator { get; set; }
        public string Repeat { get; set; }
        public string Email { get; set; }
        public string sMailUploadTime { get; set; }
        public string FromLocation { get; set; }
        public string APIURL1 { get; set; }
        public bool IsModify { get; set; }
        public int TransId { get; set; }
        public string dTransDate { get; set; }
        public bool View { get; set; }
        public bool Download { get; set; }

        public List<ColumnsSettingsModel> ColumnList { get; set; }
        public List<APIFiltersSettingsModel> APIFilters { get; set; }
        //public List<VendorListModel> VendorList { get; set; }

        public UploadMethodModel()
        {
            SupplierList = new List<ListingModel>();
            ShapeList = new List<ListingModel>();
            PointerList = new List<ListingModel>();
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
            SeparatorList = new List<SelectListItem>();
            For_iUserIds = new List<SelectListItem>();
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
            APIFilters = new List<APIFiltersSettingsModel>();
            //VendorList = new List<VendorListModel>();
        }
    }
    public class ListingModel
    {
        public long iSr { get; set; }
        public string sName { get; set; }
        public bool isActive { get; set; }
    }

    public class ColumnsSettingsModel
    {
        public bool IsActive { get; set; }
        public int icolumnId { get; set; }
        public int iPriority { get; set; }
        public string sCustMiseCaption { get; set; }
        public string sUser_ColumnName { get; set; }
    }
    public class APIFiltersSettingsModel
    {
        public long? Id { get; set; }
        public string Sr { get; set; }
        public string iVendor { get; set; }
        public string iLocation { get; set; }
        public string sShape { get; set; }
        public string sPointer { get; set; }
        public string sColor { get; set; }
        public string sClarity { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public string sLab { get; set; }
        public Single dFromLength { get; set; }
        public Single dToLength { get; set; }
        public Single dFromWidth { get; set; }
        public Single dToWidth { get; set; }
        public Single dFromDepth { get; set; }
        public Single dToDepth { get; set; }
        public Single dFromDepthPer { get; set; }
        public Single dToDepthPer { get; set; }
        public Single dFromTablePer { get; set; }
        public Single dToTablePer { get; set; }
        public Single dFromCrAng { get; set; }
        public Single dToCrAng { get; set; }
        public Single dFromCrHt { get; set; }
        public Single dToCrHt { get; set; }
        public Single dFromPavAng { get; set; }
        public Single dToPavAng { get; set; }
        public Single dFromPavHt { get; set; }
        public Single dToPavHt { get; set; }
        public string dKeyToSymbol { get; set; }
        public string dCheckKTS { get; set; }
        public string dUNCheckKTS { get; set; }
        public string sBGM { get; set; }
        public string sCrownBlack { get; set; }
        public string sTableBlack { get; set; }
        public string sCrownWhite { get; set; }
        public string sTableWhite { get; set; }
        public string View { get; set; }
        public string Download { get; set; }
        public string Img { get; set; }
        public string Vdo { get; set; }
        public string PriceMethod { get; set; }
        public double? PricePer { get; set; }
    }
    public class VendorListModel
    {
        public int Id { get; set; }
        public string sPartyName { get; set; }
        public string sPartyPrefix { get; set; }
        public string sEmail { get; set; }
        public string sContactNo { get; set; }
        public string sContactPerson { get; set; }
        public DateTime dtCreatedDate { get; set; }
        public int sLocation { get; set; }
        public string sPath { get; set; }
    }

}