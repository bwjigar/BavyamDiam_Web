
namespace Lib.Models
{
    public class UserActivityUpdateRequest
    {
        public string UserID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string EmpID { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string PageNo { get; set; }
        public string PageSize { get; set; }
        public string OrderBy { get; set; }
        public string ComUserName { get; set; }
    }

    public class UserActivityUpdateResponse
    {
        public long iTotalRec { get; set; }
        public int iTransId { get; set; }
        public string dTransDate { get; set; }
        public string dTransFullDate { get; set; }
        public string sUsername { get; set; }
        public string sCompName { get; set; }
        public string sLab { get; set; }
        public string Cts { get; set; }
        public string sShape { get; set; }
        public string sColor { get; set; }
        public string sClarity { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public string Disc { get; set; }
        public string sPointer { get; set; }
        public string RapAmount { get; set; }
        public string NetAmount { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Depth { get; set; }
        public string DepthPer { get; set; }
        public string TablePer { get; set; }
        public string CrAng { get; set; }
        public string CrHt { get; set; }
        public string PavAng { get; set; }
        public string PavHt { get; set; }
        public string sShade { get; set; }
        public string sInclusion { get; set; }
        public string sTableNatts { get; set; }
        public string sCrownInclusion { get; set; }
        public string sCrownNatts { get; set; }
        public string sLuster { get; set; }
        public string sLocation { get; set; }
        public string sStatus { get; set; }
        public string sFormName { get; set; }
        public string sActivityType { get; set; }
        public string BrowserName { get; set; }
        public string IPAddr { get; set; }
        public string LoginType { get; set; }
    }
}
