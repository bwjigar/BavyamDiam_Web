using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class UserRegistrationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyAddress3 { get; set; }
        public string CompCity { get; set; }
        public string CompZipCode { get; set; }
        public string CompState { get; set; }
        public string CompCountry { get; set; }
        public string CompMobile { get; set; }
        public string CompMobile2 { get; set; }
        public string CompPhone { get; set; }
        public string CompPhone2 { get; set; }
        public string CompFaxNo { get; set; }
        public string CompEmail { get; set; }
        public string RapnetID { get; set; }
        public string CompRegNo { get; set; }
        public string CompEmail2 { get; set; }
        public string WeChatId { get; set; }
        public string SkypeId { get; set; }
        public string Website { get; set; }
        public string DeviceType { get; set; }
        public int CompCityId { get; set; }
        public int CompCountryId { get; set; }
        public string Lang { get; set; }
        public string SupplierId { get; set; }
        public string SupplierIdLst { get; set; }
    }

    public class UserListRequest
    {
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserType { get; set; }
        public string UserStatus { get; set; }
        public string PageNo { get; set; }
        public string Emp1 { get; set; }
        public string Emp2 { get; set; }
        public string assistby { get; set; }
        public string UserID { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
        public string IsEmployee { get; set; }
        public bool PrimaryUser { get; set; }
        public string FilterType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FortunePartyCode { get; set; }
    }

    public class UserListResponse
    {
        public int iTotalRec { get; set; }
        public long iSr { get; set; }
        public int iUserid { get; set; }
        public string sUsername { get; set; }
        public string sPassword { get; set; }
        public string sFirstName { get; set; }
        public string sLastName { get; set; }
        public string sFullName { get; set; }
        public string UserDet { get; set; }
        public string sOtherName { get; set; }
        public string sAddress { get; set; }
        public string sAddress2 { get; set; }
        public string sAddress3 { get; set; }
        public string sCity { get; set; }
        public string sZipcode { get; set; }
        public string sState { get; set; }
        public string sCountry { get; set; }
        public string sMobile { get; set; }
        public string sPhone { get; set; }
        public string sEmail { get; set; }
        public string sEmailPersonal { get; set; }
        public string sPassportId { get; set; }
        public string sHkId { get; set; }
        public string sCompName { get; set; }
        public string sCompAddress { get; set; }
        public string sCompAddress2 { get; set; }
        public string sCompAddress3 { get; set; }
        public string sCompCity { get; set; }
        public string sCompZipcode { get; set; }
        public string sCompState { get; set; }
        public string sCompCountry { get; set; }
        public string sCompMobile { get; set; }
        public string sCompMobile2 { get; set; }
        public string sCompPhone { get; set; }
        public string sCompPhone2 { get; set; }
        public string sCompFaxNo { get; set; }
        public string sCompEmail { get; set; }
        public string sRapNetId { get; set; }
        public string sCompRegNo { get; set; }
        public int iUserType { get; set; }
        public string sUserType { get; set; }
        public int iEmpId { get; set; }
        public int iEmpId2 { get; set; }
        public string AssistBy1 { get; set; }
        public string AssistBy2 { get; set; }
        public int iLoginFailed { get; set; }
        public bool bIsActive { get; set; }
        public bool bIsDeleted { get; set; }
        public DateTime dtModifiedDate { get; set; }
        public int iModifiedBy { get; set; }
        public DateTime dtCreatedDate { get; set; }
        public bool bIsCompUser { get; set; }
        public int DAYS { get; set; }
        public DateTime dtSuspendedDate { get; set; }
        public string Suspended { get; set; }
        public string sStockCategory { get; set; }
        public string scompemail2 { get; set; }
        public string sCreatedDate { get; set; }
        public string sModifiedDate { get; set; }
        public string sWeChatId { get; set; }
        public string sSkypeId { get; set; }
        public string sWebsite { get; set; }
        public int iCompCityId { get; set; }
        public int iCompCountryId { get; set; }
        public string FortunePartyCode { get; set; }
        public string DBA { get; set; }
        public string Remark { get; set; }
        public bool DeletePermission { get; set; }
        public bool IsPrimary { get; set; }
        public bool SearchStock { get; set; }
        public bool PlaceOrder { get; set; }
        public bool OrderHisrory { get; set; }
        public bool MyCart { get; set; }
        public bool MyWishlist { get; set; }
        public bool QuickSearch { get; set; }
        public bool DeleteUser { get; set; }
        public bool SubUserCount { get; set; }
        public bool IsPrimaryUser { get; set; }
        public string SupplierId { get; set; }
        public int MessageId { get; set; }
        public string MessageName { get; set; }
        public Boolean? OrderApproved { get; set; }
        public string LastLoginDate { get; set; }
        public string LastActivationDate { get; set; }
        public string DaysFromLastActivation { get; set; }
        public string DaysFromLastLogin { get; set; }
        public string SUNRISE_View { get; set; }
        public string SHAIRU_View { get; set; }
        public string JB_View { get; set; }
        public string KGK_View { get; set; }
        public string SUNRISE_Download { get; set; }
        public string SHAIRU_Download { get; set; }
        public string JB_Download { get; set; }
        public string KGK_Download { get; set; }
        public string SupplierName { get; set; }
    }

    public class UserProfileDetails
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyAddress3 { get; set; }
        public string CompCity { get; set; }
        public string CompZipCode { get; set; }
        public string CompState { get; set; }
        public string CompCountry { get; set; }
        public string CompMobile { get; set; }
        public string CompMobile2 { get; set; }
        public string CompPhone { get; set; }
        public string CompPhone2 { get; set; }
        public string CompFaxNo { get; set; }
        public string CompEmail { get; set; }
        public string RapnetID { get; set; }
        public string CompRegNo { get; set; }
        public string WeChatId { get; set; }
        public string SkypeId { get; set; }
        public string Website { get; set; }
        public int CompCityId { get; set; }
        public int CompCountryId { get; set; }
        public string IsProfileChanged { get; set; }
    }

    public class UserProfilePictureDetails
    {
        public byte[] Photo { get; set; }
        public string FileExtenstion { get; set; }
    }

    public class UserDetails
    {
        public int UserID { get; set; }
        public long iiUserid { get; set; }
        public int ModifiedByID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public string PrevIsActive { get; set; }
        public string IsActive { get; set; }
        public string Suspended { get; set; }
        public string IsCompanyUser { get; set; }
        public string EmpID1 { get; set; }
        public string EmpID2 { get; set; }
        public string StockType { get; set; }
        public string PassportId { get; set; }
        public string HkId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyAddress3 { get; set; }
        public string CompCity { get; set; }
        public string CompZipCode { get; set; }
        public string CompState { get; set; }
        public string CompCountry { get; set; }
        public string CompMobile { get; set; }
        public string CompMobile2 { get; set; }
        public string CompPhone { get; set; }
        public string CompPhone2 { get; set; }
        public string CompFaxNo { get; set; }
        public string CompEmail { get; set; }
        public string CompEmail2 { get; set; }
        public string RapnetID { get; set; }
        public string CompRegNo { get; set; }
        public string WeChatId { get; set; }
        public string SkypeId { get; set; }
        public string Website { get; set; }
        public string UserType { get; set; }
        public string Lang { get; set; }
        public string FortunePartyCode { get; set; }
        public string DBA { get; set; }
        public string Remark { get; set; }
        public bool IsPrimary { get; set; }
        public string SupplierId { get; set; }
        public string MessageId { get; set; }
        public Boolean? OrderApproved { get; set; }
    }

    public class UserListSearchRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string UserType { get; set; }
        public string PageNo { get; set; }
        public string PageSize { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
        public string OrderBy { get; set; }
    }

    public class UserListSearchResponse
    {
        public long iTotalRec { get; set; }
        public string UserName { get; set; }
        public string JoinDate { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string sCompName { get; set; }
        public string Country { get; set; }
    }
    public class NewsMst
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Description { get; set; }
        public string FontColor { get; set; }
        public int? iID { get; set; }
        public string Flag { get; set; }
    }
    public class ErrorLgRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string MSearch { get; set; }
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }
        public string OrderBy { get; set; }
    }
    public class ErrorLgResponse
    {
        public long iTotalRec { get; set; }
        public long SrNo { get; set; }
        public long iId { get; set; }
        public string ErrorDate { get; set; }
        public long iUserId { get; set; }
        public string sUsername { get; set; }
        public string sFullName { get; set; }
        public string sCompName { get; set; }
        public string sIPAddress { get; set; }
        public string sErrorTrace { get; set; }
        public string sErrorMsg { get; set; }
        public string sErrorSite { get; set; }
        public string sErrorPage { get; set; }
    }
    public class CurrencyCountryListDetail
    {
        public string iCountryId { get; set; }
        public string sCountryCode { get; set; }
        public string sCountryName { get; set; }
        public string sSymbol { get; set; }
        public string dExRate { get; set; }
        public string sShortName { get; set; }
        public string sISDCode { get; set; }
        public string sCountryISOCode { get; set; }
    }
    public class NotifySaveRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Message { get; set; }
        public string UserIdList { get; set; }
        public int? NotifyId { get; set; }
        public int iUserId { get; set; }
    }
    public class NotifyRequest
    {
        public string SearchList { get; set; }
        public int? NotifyId { get; set; }
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }
        public string OrderBy { get; set; }
        public int? iUserId { get; set; }
    }
    public class NotifyResponse
    {
        public long iTotalRec { get; set; }
        public long SrNo { get; set; }
        public long Userid { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public string CompName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Search { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Message { get; set; }
        public bool IsDismiss { get; set; }
    }
    public class NotifyDetRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }
        public string OrderBy { get; set; }
    }
    public class NotifyDetResponse
    {
        public long iTotalRec { get; set; }
        public long SrNo { get; set; }
        public long NotifyId { get; set; }
        public string ValidityFromDate { get; set; }
        public string ValidityToDate { get; set; }
        public string Message { get; set; }
        public string CreationDate { get; set; }
        public long TotalUser { get; set; }
        public long DismissUser { get; set; }
    }
    public class NotifyGet_UserResponse
    {
        public long Id { get; set; }
        public bool IsDismiss { get; set; }
        public string Message { get; set; }
        public string ValidityFromDate { get; set; }
    }
    public class IP_Wise_Login_Detail
    {
        public string IPAddress { get; set; }
        public int? UserId { get; set; }
        public string Type { get; set; }
    }
    public class PacketTrace_Request
    {
        public string StockId { get; set; }
        public string CertiNo { get; set; }
    }
    public class PacketTrace_Response
    {
        public string TRANS_DATE { get; set; }
        public string PROCESS { get; set; }
        public string PARTY { get; set; }
        public string SOURCE_PARTY { get; set; }
        public string REF_NO { get; set; }
        public string CERTI_NO { get; set; }
        public decimal CTS { get; set; }
        public long SEQ_NO { get; set; }
        public string COLOR { get; set; }
        public string PURITY { get; set; }
        public string CUT { get; set; }
        public string LAB { get; set; }
        public string WEB_IMG_STATUS { get; set; }
        public string WEB_HDIMG_STATUS { get; set; }
        public string POLISH { get; set; }
        public string SYMM { get; set; }
        public string FLS { get; set; }
        public decimal DISC_OFFER { get; set; }
        public decimal RAP_PRICE { get; set; }
        public decimal RAP_VALUE { get; set; }
        public string SHADE_NAME { get; set; }
        public string BGM { get; set; }
    }
    public class OrderDisc_InsUpd
    {
        public string IUType { get; set; }
        public int Id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public string Discount { get; set; }
        public string Value { get; set; }
    }
    public class OrderDisc_Select
    {
        public long Sr { get; set; }
        public int Id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public string Discount { get; set; }
        public string Value { get; set; }
        public string Values { get; set; }
        public string LastModifyDate { get; set; }
        public string LastModifyBy { get; set; }
    }
    public class App_info_Request
    {
        public string Login_Type { get; set; }
        public string App_Version { get; set; }
        public string flg { get; set; }
        public int updflg { get; set; }
    }
    public class App_info
    {
        public string DeviceType { get; set; }
        public string AppVersion { get; set; }
    }
    public class PacketDet_Request
    {
        public string StockId { get; set; }
    }
    public class PacketDet_Response
    {
        public string sRefNo { get; set; }
        public string sShape { get; set; }
        public double dCts { get; set; }
        public string sColor { get; set; }
        public string sClarity { get; set; }
        public double dRepPrice { get; set; }
        public decimal dRapAmount { get; set; }
        public decimal dNetPrice { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public double dLength { get; set; }
        public double dWidth { get; set; }
        public double dDepth { get; set; }
        public double dDepthPer { get; set; }
        public double dTablePer { get; set; }
        public double dCrAng { get; set; }
        public double dCrHt { get; set; }
        public double dPavAng { get; set; }
        public double dPavHt { get; set; }
        public string sCertiNo { get; set; }
        public string sGirdle { get; set; }
        public double dDisc { get; set; }
        public string sLab { get; set; }
        public string sPointer { get; set; }
        public bool bImage { get; set; }
        public string sStatus { get; set; }
        public bool bHDMovie { get; set; }
        public string sShade { get; set; }
        public long iSeqNo { get; set; }
        public string sComments { get; set; }
        public string sSymbol { get; set; }
        public string sGirdleType { get; set; }
        public string sCulet { get; set; }
        public string sTableNatts { get; set; }
        public string sInclusion { get; set; }
        public double dGirdlePer { get; set; }
        public string sStrLn { get; set; }
        public string sLrHgt { get; set; }
        public string sOpen { get; set; }
        public string sStoneClarity { get; set; }
        public string sInscription { get; set; }
        public string sLuster { get; set; }
        public string sSideNatts { get; set; }
        public string sHNA { get; set; }
        public string sSideFtr { get; set; }
        public string sTableFtr { get; set; }
        public double dTableDepth { get; set; }
        public string sCrownNatts { get; set; }
        public string sCrownInclusion { get; set; }
        public double OrgDisc { get; set; }
        public string imgPr { get; set; }
        public string imgPr_L { get; set; }
        public string imgPrb { get; set; }
        public string imgPrb_L { get; set; }
        public string imgAs { get; set; }
        public string imgAs_L { get; set; }
        public string imgHt { get; set; }
        public string imgHt_L { get; set; }
        public string imgHb { get; set; }
        public string imgHb_L { get; set; }
        public string lsFirstImg { get; set; }
        public string litImageName { get; set; }
    }
    public class ViewHDImageRequest { 
        public string RefNo { get; set; }
    }
    public class ViewHDImageResponse
    {
       public string VideoPath { get; set; }

    }
    public class UserMgtRequest
    {
        public int iUserId { get; set; }
        public string iPgNo { get; set; }
        public string iPgSize { get; set; }
        public string sOrderBy { get; set; }
        public string Search { get; set; }
        public bool Active { get; set; }
        public bool InActive { get; set; }
    }
    public class UserMgtResponse
    {
        public long iSr { get; set; }
        public Single page_size { get; set; }
        public Single total_page { get; set; }
        public Single total_record { get; set; }
        public int iUserid { get; set; }
        public string CreatedDate { get; set; }
        public string CustName { get; set; }
        public string sUsername { get; set; }
        public string sCompName { get; set; }
        public string sCompMobile { get; set; }
        public string sCompEmail { get; set; }
        public string Assist1 { get; set; }
        public string Assist2 { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrimary { get; set; }
        public bool SearchStock { get; set; }
        public bool PlaceOrder { get; set; }
        public bool OrderHisrory { get; set; }
        public bool MyCart { get; set; }
        public bool MyWishlist { get; set; }
        public bool QuickSearch { get; set; }
    }
    public class UserMgtSave_Request
    {
        public string Type { get; set; }
        public int iUserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public bool IsActive { get; set; }
        public bool SearchStock { get; set; }
        public bool PlaceOrder { get; set; }
        public bool OrderHisrory { get; set; }
        public bool MyCart { get; set; }
        public bool MyWishlist { get; set; }
        public bool QuickSearch { get; set; }
    }
    public class FortunePartyCode_Exist_Request
    {
        public int iUserId { get; set; }
        public int FortunePartyCode { get; set; }
    }
    public class GetCompanyForUserMgt_Request
    {
        public string Search { get; set; }
    }
    public class GetCompanyForUserMgt_Response
    {
        public string iUserid { get; set; }
        public string CompName { get; set; }
        public string FortunePartyCode { get; set; }
        public string Username { get; set; }
        public string CustName { get; set; }
        public string AssistBy { get; set; }
    }
    public class StockDiscMgtRequest
    {
        public string UserList { get; set; }
        public string sOrderBy { get; set; }
        public string iPgNo { get; set; }
        public string iPgSize { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string UserFullName { get; set; }
        public string FilterType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    public class StockDiscMgtResponse
    {
        public Single iTotalRec { get; set; }
        public long iSr { get; set; }
        public int iUserid { get; set; }
        public string sUsername { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSubuser { get; set; }
        public string Type { get; set; }
        public string sCompName { get; set; }
        public string CustName { get; set; }
        public bool IsActive { get; set; }
    }
    public class UserDetailGet_Req
    {
        public int UserId { get; set; }
    }
    public class UserDetailGet_Res
    {
        public string sUsername { get; set; }
        public string sPassword { get; set; }
    }
    public class MessageMstSave_Request
    {
        public int iUserId { get; set; }
        public int Id { get; set; }
        public string MessageName { get; set; }
        public string Message { get; set; }
        public bool IsLogout { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
    }
    public class MessageMstSelect_Request
    {
        public int Id { get; set; }
        public string iPgNo { get; set; }
        public string iPgSize { get; set; }
        public string sOrderBy { get; set; }
        public string IsActive { get; set; }
    }
    public class MessageMstSelect_Response
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public int Id { get; set; }
        public string MessageName { get; set; }
        public string Message { get; set; }
        public bool IsLogout { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
    public class GetUserStatusReport_Request
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ActivityStatus { get; set; }
        public string OrderBy { get; set; }
        public string PageNo { get; set; }
        public string PageSize { get; set; }
    }
    public class GetUserStatusReport_Response
    {
        public int iTotalRec { get; set; }
        public long iSr { get; set; }
        public string sCreatedDate { get; set; }
        public string sFullName { get; set; }
        public string sUsername { get; set; }
        public string sCompName { get; set; }
        public string FortunePartyCode { get; set; }
        public string AssistBy1 { get; set; }
        public string AssistBy2 { get; set; }
        public string Activity { get; set; }
        public string ActivityDate { get; set; }
    }
}
