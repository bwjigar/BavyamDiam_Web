using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunriseWeb.Models
{
    public class Account
    {
    }
   
    public class ColumnsUserModel
    {
        //public List<UserModelListingModel> UserModel { get; set; }
        public List<SelectListItem> UserModel { get; set; }
        public ColumnsUserModel()
        {
            //UserModel = new List<UserModelListingModel>();
            UserModel = new List<SelectListItem>();
        }
    }
    public class UserModelListingModel
    {
        public long iSr { get; set; }
        public long iUserid { get; set; }
        public string sFullName { get; set; }
    }
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter your User Name.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your Password.")]
        public string Password { get; set; }
        public bool isRemember { get; set; }
        public bool isSwitchClassic { get; set; }
    }

    public class ForgotPassword
    {
        [Required(ErrorMessage = "Please enter your User Name.")]
        public string Username { get; set; }
    }
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Please enter your current Password.")]
        public string Password { get; set; }
        [StringLength(30, MinimumLength = 7, ErrorMessage = "Please enter minimum 6 character PassWord.")]
        [Required(ErrorMessage = "Please enter New Password.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please enter confirm Password.")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "New password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AccountModel
    {
        [Required(ErrorMessage = "Please Enter First Name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Company Name.")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please Enter Address1.")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "Please Enter Address2.")]
        public string Address2 { get; set; }
        [Required(ErrorMessage = "Please Enter City.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Enter Zip Code.")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Please Enter Country.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please Enter ISD Code of Mobile No.")]
        public string Mobile1STDCode { get; set; }
        [Required(ErrorMessage = "Please Enter Mobile No.")]
        public string Mobile1 { get; set; }
        public string Mobile2STDCode { get; set; }
        public string Mobile2 { get; set; }

        [Required(ErrorMessage = "Please Enter STD Code of Phone No 1.")]
        public string Office1STDCode { get; set; }
        [Required(ErrorMessage = "Please Enter Phone No.")]
        public string OfficePh1 { get; set; }
        public string Office2STDCode { get; set; }
        public string OfficePh2 { get; set; }

        public string FaxSTDCode { get; set; }
        public string FaxNo { get; set; }
        public string Website { get; set; }

        [Required(ErrorMessage = "Please Enter Email ID 1.")]
        [EmailAddress(ErrorMessage = "Please Enter Valid  Email ID 1 Format.")]
        public string EmailId1 { get; set; }
        [EmailAddress(ErrorMessage = "Please Enter Valid  Email ID 2 Format.")]
        public string EmailId2 { get; set; }
        public string BusiRegNo { get; set; }
        public string WeChatId { get; set; }
        public string SkypeId { get; set; }
        public string RapId { get; set; }
        public int CompCityId { get; set; }
        public int CompCountryId { get; set; }
    }

    public class UserDetailsModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public bool IsActive { get; set; }
        public string Suspended { get; set; }
        public bool IsCompanyUser { get; set; }
        public bool IsPrimary { get; set; }
        public int EmpID1 { get; set; }
        public int EmpID2 { get; set; }
        public string StockType { get; set; }
        public string PassportId { get; set; }
        public string HkId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompCity { get; set; }
        public string CompZipCode { get; set; }
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
        public int iCompCityId { get; set; }
        public int iCompCountryId { get; set; }
        public string FortunePartyCode { get; set; }
        public string DBA { get; set; }
        public string Remark { get; set; }
        public string SupplierId { get; set; }
        public string SupplierIdLst { get; set; }
        public SelectList EmpList1 { get; set; }
        public SelectList EmpList2 { get; set; }
        public SelectList StockTypeList { get; set; }
        public SelectList SupplierList { get; set; }
        public int MessageId { get; set; }
        public SelectList MessageList { get; set; }
        public Boolean? OrderApproved { get; set; }
    }
}