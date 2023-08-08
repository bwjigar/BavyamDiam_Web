using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class UserwiseCompany_select
    {
        public int iUserid { get; set; }
        public string CompanyName { get; set; }
    }
    public class ApiUploadMethodResponse
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public string Username { get; set; }
        public string CustomerName { get; set; }
        public string AssistBy { get; set; }
        public string sUsername { get; set; }
        public string sfullname { get; set; }
        public string dCreationDate { get; set; }
        public string dTransDate { get; set; }
        public long? iTransId { get; set; }
        public int? iUserId { get; set; }
        public string ApiMethod { get; set; }
        public string ApiMethodName { get; set; }
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
        //public string iVendor { get; set; }
        //public string iLocation { get; set; }
        //public string sShape { get; set; }
        //public string sPointer { get; set; }
        //public string sColor { get; set; }
        //public string sClarity { get; set; }
        //public string sCut { get; set; }
        //public string sPolish { get; set; }
        //public string sSymm { get; set; }
        //public string sFls { get; set; }
        //public string sLab { get; set; }
        //public Single? dFromLength { get; set; }
        //public Single? dToLength { get; set; }
        //public Single? dFromWidth { get; set; }
        //public Single? dToWidth { get; set; }
        //public Single? dFromDepth { get; set; }
        //public Single? dToDepth { get; set; }
        //public Single? dFromDepthPer { get; set; }
        //public Single? dToDepthPer { get; set; }
        //public Single? dFromTablePer { get; set; }
        //public Single? dToTablePer { get; set; }
        //public Single? dFromCrAng { get; set; }
        //public Single? dToCrAng { get; set; }
        //public Single? dFromCrHt { get; set; }
        //public Single? dToCrHt { get; set; }
        //public Single? dFromPavAng { get; set; }
        //public Single? dToPavAng { get; set; }
        //public Single? dFromPavHt { get; set; }
        //public Single? dToPavHt { get; set; }
        //public string dKeyToSymbol { get; set; }
        //public string dCheckKTS { get; set; }
        //public string dUNCheckKTS { get; set; }
        //public string sBGM { get; set; }
        //public string sCrownBlack { get; set; }
        //public string sTableBlack { get; set; }
        //public string sCrownWhite { get; set; }
        //public string sTableWhite { get; set; }
        //public string Img { get; set; }
        //public string Vdo { get; set; }
        //public string PriceMethod { get; set; }
        //public double? PricePer { get; set; }
        public string APIName { get; set; }
        public string APIUrl { get; set; }
        public bool APIStatus { get; set; }
        public int For_iUserId { get; set; }
        public string CompanyName { get; set; }
        
        public List<APIFiltersSettings> APIFilters { get; set; }
        public List<ApiColumnsSettings> ColumnsSettings { get; set; }
        public ApiUploadMethodResponse()
        {
            APIFilters = new List<APIFiltersSettings>(); 
            ColumnsSettings = new List<ApiColumnsSettings>();
        }

    }
    
    public class ApiUploadMethod
    {
        public int? iTransId { get; set; }
        public int? iUserId { get; set; }
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
        //public string iVendor { get; set; }
        //public string iLocation { get; set; }
        //public string sShape { get; set; }
        //public string sPointer { get; set; }
        //public string sColor { get; set; }
        //public string sClarity { get; set; }
        //public string sCut { get; set; }
        //public string sPolish { get; set; }
        //public string sSymm { get; set; }
        //public string sFls { get; set; }
        //public string sLab { get; set; }
        //public Single dFromLength { get; set; }
        //public Single dToLength { get; set; }
        //public Single dFromWidth { get; set; }
        //public Single dToWidth { get; set; }
        //public Single dFromDepth { get; set; }
        //public Single dToDepth { get; set; }
        //public Single dFromDepthPer { get; set; }
        //public Single dToDepthPer { get; set; }
        //public Single dFromTablePer { get; set; }
        //public Single dToTablePer { get; set; }
        //public Single dFromCrAng { get; set; }
        //public Single dToCrAng { get; set; }
        //public Single dFromCrHt { get; set; }
        //public Single dToCrHt { get; set; }
        //public Single dFromPavAng { get; set; }
        //public Single dToPavAng { get; set; }
        //public Single dFromPavHt { get; set; }
        //public Single dToPavHt { get; set; }
        //public string dKeyToSymbol { get; set; }
        //public string dCheckKTS { get; set; }
        //public string dUNCheckKTS { get; set; }
        //public string sBGM { get; set; }
        //public string sCrownBlack { get; set; }
        //public string sTableBlack { get; set; }
        //public string sCrownWhite { get; set; }
        //public string sTableWhite { get; set; }
        //public string Img { get; set; }
        //public string Vdo { get; set; }
        //public string PriceMethod { get; set; }
        //public decimal? PricePer { get; set; }
        public string APIName { get; set; }
        public string APIUrl { get; set; }
        public bool APIStatus { get; set; }
        public int For_iUserId { get; set; }
        public List<APIFiltersSettings> APIFilters { get; set; }
        public List<ApiColumnsSettings> ColumnsSettings { get; set; }
        public ApiUploadMethod()
        {
            APIFilters = new List<APIFiltersSettings>(); 
            ColumnsSettings = new List<ApiColumnsSettings>();
        }
    }
    public class FTPAPIPortalLogRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public int Distinct { get; set; }
        public int IsFTP { get; set; }
        public string CompSearch { get; set; }
    }
    public class FTPAPIPortalLogResponse
    {
        public long iTotalRec { get; set; }
        public long SrNo { get; set; }
        public long Id { get; set; }
        public long TransId { get; set; }
        public string EntryDate1 { get; set; }
        public string EntryDate { get; set; }
        public long UserId { get; set; }
        public string TotalCount { get; set; }
        public bool Status { get; set; }
        public string Error { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FullName { get; set; }
        public string CompName { get; set; }
        public string ApiMethod { get; set; }
        public string APIName { get; set; }
        public string APIUrl { get; set; }
        public string For_iUserId { get; set; }
        public string EDate { get; set; }
        public string ETime { get; set; }
        public string CustomerName { get; set; }
        public string Username { get; set; }
        public string PwdLenth { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string AssistBy { get; set; }
        public string FTPUser { get; set; }
        public string FTPPwdLenth { get; set; }
        public string FTPPass { get; set; }
        public string OnlyFileName { get; set; }
    }
}
