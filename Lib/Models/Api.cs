using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class ApiFilterRequest
    {
        public string sTransId { get; set; }
        public string dtFromDate { get; set; }
        public string sSearch { get; set; }
        public string dtToDate { get; set; }
        public string sUserId { get; set; }
        public string sEmpId { get; set; }
        public string sFullName { get; set; }
        public string sUserName { get; set; }
        public string sCompName { get; set; }
        public string sCountryName { get; set; }
        public string sPgNo { get; set; }
        public string sPgSize { get; set; }
        public string OrderBy { get; set; }
    }

    public class ApiFilterResponse
    {
        public string dTransDate1 { get; set; }
        public string MailLastUploadTime1 { get; set; }
        public string APIURL1 { get; set; }
        public string ApiUrl { get; set; }
        public string Cts { get; set; }
        public string Disc { get; set; }
        public DateTime FTP_LAST_UPLOAD_DATE { get; set; }
        public string FTP_NAME { get; set; }
        public string FTP_PASSWORD { get; set; }
        public string FTP_UPLOAD_TIME { get; set; }
        public string Ftp_User { get; set; }
        public DateTime MailLastUploadTime { get; set; }
        public double dFromCrAng { get; set; }
        public double dFromCrHt { get; set; }
        public double dFromDepth { get; set; }
        public double dFromDepthPer { get; set; }
        public double dFromLength { get; set; }
        public double dFromPavAng { get; set; }
        public double dFromPavHt { get; set; }
        public double dFromTablePer { get; set; }
        public double dFromWidth { get; set; }
        public double dToCrAng { get; set; }
        public double dToCrHt { get; set; }
        public double dToDepth { get; set; }
        public double dToDepthPer { get; set; }
        public double dToLength { get; set; }
        public double dToPavAng { get; set; }
        public double dToPavHt { get; set; }
        public double dToTablePer { get; set; }
        public double dToWidth { get; set; }
        public DateTime dTransDate { get; set; }
        public string export_type { get; set; }
        public string iLocation { get; set; }
        public long iSr { get; set; }
        public int iTotalRec { get; set; }
        public int iTransId { get; set; }
        public int iUserId { get; set; }
        public string sApiName { get; set; }
        public string sClarity { get; set; }
        public string sColor { get; set; }
        public string sCrownInclusion { get; set; }
        public string sCrownNatts { get; set; }
        public string sCut { get; set; }
        public string sEmail { get; set; }
        public string sFls { get; set; }
        public string sInclusion { get; set; }
        public string sLab { get; set; }
        public string sMailUploadTime { get; set; }
        public string sPointer { get; set; }
        public string sPolish { get; set; }
        public string sRepeat { get; set; }
        public string sSeprator { get; set; }
        public string sShade { get; set; }
        public string sShape { get; set; }
        public string sSymm { get; set; }
        public string sTableNatts { get; set; }
        public string sUsername { get; set; }
        public string sfullname { get; set; }
        public List<ApiColumnsSettings> ColumnsSettings { get; set; }
        public ApiFilterResponse()
        {
            ColumnsSettings = new List<ApiColumnsSettings>();
        }

    }

    public class ApiDetails
    {
        public int? iTransId { get; set; }
        public int? iUserId { get; set; }
        public string sShape { get; set; }
        public string sColor { get; set; }
        public string sClarity { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public string sLab { get; set; }
        public string dFromcts { get; set; }
        public string dToCts { get; set; }
        public string sFromDisc { get; set; }
        public string dToDisc { get; set; }
        public string sPointer { get; set; }
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
        public string sShade { get; set; }
        public string sInclusion { get; set; }
        public string sTableNatts { get; set; }
        public string sApiName { get; set; }
        public string sExpType { get; set; }
        public string sCrownNatts { get; set; }
        public string sCrownInclusion { get; set; }
        public string sFtpName { get; set; }
        public string sftpUser { get; set; }
        public string sftpPass { get; set; }
        public string iFtpUploadTime { get; set; }
        public string sSeprator { get; set; }
        public string sRepeat { get; set; }
        public string sEmail { get; set; }
        public string sMailUploadTime { get; set; }
        public string iLocation { get; set; }
        public string ApiUrl { get; set; }
        public string OperationType { get; set; }

        public List<ApiColumnsSettings> ColumnsSettings { get; set; }
        public ApiDetails()
        {
            ColumnsSettings = new List<ApiColumnsSettings>();
        }
    }

    public class ApiColumnsSettings
    {
        public int iTransId { get; set; }
        public int icolumnId { get; set; }
        public string sUser_ColumnName { get; set; }
        public string sCustMiseCaption { get; set; }
        public int iPriority { get; set; }
        public bool IsActive { get; set; }
        public string sColumnName { get; set; }
        public string sCaption { get; set; }
        public string sUserCaption { get; set; }
        public int iSeqNo { get; set; }
    }

    public class APIFiltersSettings
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
        public Single? dFromLength { get; set; }
        public Single? dToLength { get; set; }
        public Single? dFromWidth { get; set; }
        public Single? dToWidth { get; set; }
        public Single? dFromDepth { get; set; }
        public Single? dToDepth { get; set; }
        public Single? dFromDepthPer { get; set; }
        public Single? dToDepthPer { get; set; }
        public Single? dFromTablePer { get; set; }
        public Single? dToTablePer { get; set; }
        public Single? dFromCrAng { get; set; }
        public Single? dToCrAng { get; set; }
        public Single? dFromCrHt { get; set; }
        public Single? dToCrHt { get; set; }
        public Single? dFromPavAng { get; set; }
        public Single? dToPavAng { get; set; }
        public Single? dFromPavHt { get; set; }
        public Single? dToPavHt { get; set; }
        public string dKeyToSymbol { get; set; }
        public string dCheckKTS { get; set; }
        public string dUNCheckKTS { get; set; }
        public string sBGM { get; set; }
        public string sCrownBlack { get; set; }
        public string sTableBlack { get; set; }
        public string sCrownWhite { get; set; }
        public string sTableWhite { get; set; }
        public string Img { get; set; }
        public string Vdo { get; set; }
        public string PriceMethod { get; set; }
        public double? PricePer { get; set; }
    }

    public class ApiColumns
    {
        public int iid { get; set; }
        public string caption { get; set; }
        public string Heading { get; set; }
        public int icolumnId { get; set; }
        public int iPriority { get; set; }
        public bool IsActive { get; set; }
        public string sCustMiseCaption { get; set; }
        public string sUser_ColumnName { get; set; }
        public string Display_Order { get; set; }
    }

    public class ApiExportRequest
    {
        public string UserId { get; set; }
        public string TransId { get; set; }
        public string ApiName { get; set; }
        public string ExportType { get; set; }
    }
    public class GetURLApiRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string TransId { get; set; }
    }
    public class ApiMethodRequest
    {
        public string UserId { get; set; }
        public string TransId { get; set; }

    }
}