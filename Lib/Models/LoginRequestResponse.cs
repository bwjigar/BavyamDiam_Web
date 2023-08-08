using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{

    public class LoginFullResponse
    {
        [JsonProperty("access_token")]
        public string access_token { get; set; }

        [JsonProperty("token_type")]
        public string token_type { get; set; }

        [JsonProperty("expires_in")]
        public int expires_in { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int IsAdmin { get; set; }
        public int IsEmp { get; set; }
        public int IsGuest { get; set; }
    }

    public class LoginResponse
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int IsAdmin { get; set; }
        public int IsEmp { get; set; }
        public int IsGuest { get; set; }
        public int TransID { get; set; }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string IpAddress { get; set; }
        public string UDID { get; set; }
        public string LoginMode { get; set; }
        public string DeviseType { get; set; }
        public string DeviceName { get; set; }
        public string AppVersion { get; set; }
        public string Location { get; set; }
        public string Login { get; set; }
        public string grant_type { get; set; }
    }

    public class KeyAccountDataResponse
    {
        public int? iUserid { get; set; }
        public string sUsername { get; set; }
        public string Password { get; set; }
        public string sFirstName { get; set; }
        public string sLastName { get; set; }
        public string sFullName { get; set; }
        public int iUserType { get; set; }
        public bool bIsActive { get; set; }
        public string sCompEmail { get; set; }
        public string sCompName { get; set; }
        public int DAYs { get; set; }
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
        public string sRapNetId { get; set; }
        public string sCompRegNo { get; set; }
        public int iEmpId { get; set; }
        public int iEmpId2 { get; set; }
        public short iLoginFailed { get; set; }
        public bool bIsDeleted { get; set; }
        public DateTime dtModifiedDate { get; set; }
        public int iModifiedBy { get; set; }
        public DateTime dtCreatedDate { get; set; }
        public string sWeChatId { get; set; }
        public string sCompEmail2 { get; set; }
        public string AssistBy1 { get; set; }
        public string AssistBy2 { get; set; }
        public string phono_AssistBy1 { get; set; }
        public string phono_AssistBy2 { get; set; }
        public string mob_AssistBy1 { get; set; }
        public string mob_AssistBy2 { get; set; }
        public string Email_AssistBy1 { get; set; }
        public string Email_AssistBy2 { get; set; }
        public string wechat_AssistBy1 { get; set; }
        public string skype_AssistBy1 { get; set; }
        public string imagePath_AssistBy1 { get; set; }
        public string day_diff { get; set; }
        public int isadmin { get; set; }
        public int isemp { get; set; }
        public string device_type { get; set; }
        public string ProfileImage { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSubuser { get; set; }
        public bool SearchStock { get; set; }
        public bool PlaceOrder { get; set; }
        public bool OrderHisrory { get; set; }
        public bool MyCart { get; set; }
        public bool MyWishlist { get; set; }
        public bool QuickSearch { get; set; }
        public bool IsAssistByForAnyUser { get; set; }
        public string FortunePartyCode { get; set; }
        public int? MessageId { get; set; }
        public bool? IsLogout { get; set; }
        public bool? MessageShow { get; set; }
    }
}
