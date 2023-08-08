using SunriseWeb.Data;
using SunriseWeb.Filter;
using SunriseWeb.Models;
using SunriseWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Lib.Models;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class AccountController : BaseController
    {
        API _api = new API();
        // GET: Account
        public ActionResult ChangePassword()
        {
            ChangePasswordModel obj = new ChangePasswordModel();
            return View(obj);

        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel _obj)
        {
            if (_obj.Password != SessionFacade.UserSession.Password)
            {
                ModelState.AddModelError("Password", "Password is not match with current password");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var input = new
                    {
                        Password = _obj.NewPassword
                    };
                    string inputJson = (new JavaScriptSerializer()).Serialize(input);
                    string _response = _api.CallAPI(Constants.ChangePassword, inputJson);

                    CommonResponse _data = new CommonResponse();
                    _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);

                    TempData["Message"] = _data.Message;
                }
                catch (WebException ex)
                {
                    //if (ex.Status)
                    var webException = ex as WebException;
                    if ((Convert.ToString(webException.Status)).ToUpper() == "PROTOCOLERROR")
                    {
                        OAuthErrorMsg error =
                            JsonConvert.DeserializeObject<OAuthErrorMsg>(
                           API.ExtractResponseString(webException));
                        TempData["Message"] = error.error_description;
                    }
                    else
                    {
                        TempData["Message"] = ex.Message;
                    }
                }
                return RedirectToAction("ChangePassword", "Account");
            }
            return View(_obj);
        }

        public ActionResult Index()
        {
            string _response = _api.CallAPI(Constants.GetUserDetails, string.Empty);
            ServiceResponse<UserListResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserListResponse>>(_response);
            AccountModel obj = new AccountModel();
            var user = _data.Data.FirstOrDefault();
            if (user != null)
            {
                obj.FirstName = user.sFirstName;
                obj.LastName = user.sLastName;
                obj.CompanyName = user.sCompName;
                obj.Address1 = user.sCompAddress;
                obj.Address2 = user.sCompAddress2;
                obj.City = user.sCompCity;
                obj.ZipCode = user.sCompZipcode;
                obj.Country = user.sCompCountry;

                if (!string.IsNullOrEmpty(user.sCompMobile))
                {
                    obj.Mobile1STDCode = user.sCompMobile.LastIndexOf('-') == -1 ? "0" : user.sCompMobile.Substring(0, (user.sCompMobile.LastIndexOf('-')));
                    obj.Mobile1 = user.sCompMobile.Substring(user.sCompMobile.LastIndexOf('-') + 1, user.sCompMobile.Length - (user.sCompMobile.LastIndexOf('-') + 1));
                }
                if (!string.IsNullOrEmpty(user.sCompMobile2))
                {
                    obj.Mobile2STDCode = user.sCompMobile2.LastIndexOf('-') == -1 ? "0" : user.sCompMobile2.Substring(0, (user.sCompMobile2.LastIndexOf('-')));
                    obj.Mobile2 = user.sCompMobile2.Substring(user.sCompMobile2.LastIndexOf('-') + 1, user.sCompMobile2.Length - (user.sCompMobile2.LastIndexOf('-') + 1));
                }
                if (!string.IsNullOrEmpty(user.sCompPhone))
                {
                    obj.Office1STDCode = user.sCompPhone.LastIndexOf('-') == -1 ? "0" : user.sCompPhone.Substring(0, (user.sCompPhone.LastIndexOf('-')));
                    obj.OfficePh1 = user.sCompPhone.Substring(user.sCompPhone.LastIndexOf('-') + 1, user.sCompPhone.Length - (user.sCompPhone.LastIndexOf('-') + 1));
                }
                if (!string.IsNullOrEmpty(user.sCompPhone2))
                {
                    obj.Office2STDCode = user.sCompPhone2.LastIndexOf('-') == -1 ? "0" : user.sCompPhone2.Substring(0, (user.sCompPhone2.LastIndexOf('-')));
                    obj.OfficePh2 = user.sCompPhone2.Substring(user.sCompPhone2.LastIndexOf('-') + 1, user.sCompPhone2.Length - (user.sCompPhone2.LastIndexOf('-') + 1));
                }
                if (!string.IsNullOrEmpty(user.sCompFaxNo))
                {
                    obj.FaxSTDCode = user.sCompFaxNo.LastIndexOf('-') == -1 ? "0" : user.sCompFaxNo.Substring(0, (user.sCompFaxNo.LastIndexOf('-')));
                    obj.FaxNo = user.sCompFaxNo.Substring(user.sCompFaxNo.LastIndexOf('-') + 1, user.sCompFaxNo.Length - (user.sCompFaxNo.LastIndexOf('-') + 1));
                }

                obj.Website = user.sWebsite;
                obj.EmailId1 = user.sCompEmail;
                obj.EmailId2 = user.scompemail2;
                obj.RapId = user.sRapNetId;
                obj.BusiRegNo = user.sCompRegNo;
                obj.WeChatId = user.sWeChatId;
                obj.SkypeId = user.sSkypeId;
                obj.CompCityId = user.iCompCityId;
                obj.CompCountryId = user.iCompCountryId;
            }

            return View(obj);
        }

        [HttpPost]
        public JsonResult UpdateUser(UserProfileDetails _obj, IEnumerable<HttpPostedFileBase> _empImg)
        {
            if (ModelState.IsValid)
            {
                string inputJson = (new JavaScriptSerializer()).Serialize(_obj);

                string _response = _api.CallAPI(Constants.UpdateUserProfileDetails, inputJson);
                CommonResponse _data = new CommonResponse();
                CommonResponse _dataProfilePic = new CommonResponse();
                try
                {
                    _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
                   

                    if (_data.Status == "1")
                    {
                        if (_empImg != null && _obj.IsProfileChanged == "1")
                        {
                            byte[] data;
                            using (Stream inputStream = _empImg.FirstOrDefault().InputStream)
                            {
                                MemoryStream memoryStream = inputStream as MemoryStream;
                                if (memoryStream == null)
                                {
                                    memoryStream = new MemoryStream();
                                    inputStream.CopyTo(memoryStream);
                                }
                                data = memoryStream.ToArray();
                            }

                            UserProfilePictureDetails picDetails = new UserProfilePictureDetails
                            {
                                Photo = data,
                                FileExtenstion = Path.GetExtension(_empImg.FirstOrDefault().FileName)
                            };

                            inputJson = (new JavaScriptSerializer()).Serialize(picDetails);
                            _response = _api.CallAPI(Constants.UpdateUserProfilePicture, inputJson);
                            _dataProfilePic = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);

                            if (_dataProfilePic.Status == "1" && _data.Status == "1")
                            {
                                SessionFacade.UserSession.ProfileImage = Convert.ToBase64String(data);
                                _data.Message = "Profile Details Successfully Updated..!";
                                _data.Status = "1";
                            }
                            else if (_dataProfilePic.Status == "0" && _data.Status == "1")
                            {
                                _data.Message = _dataProfilePic.Message;
                                _data.Status = "0";
                            }
                            else if (_dataProfilePic.Status == "1" && _data.Status == "0")
                            {
                                SessionFacade.UserSession.ProfileImage = Convert.ToBase64String(data);
                                _data.Message = "Profile Pic Updated, Other Deatils Not Updated..!";
                                _data.Status = "0";
                            }
                        }
                        else
                        {
                            _data.Message = "Profile Details Successfully Updated..!";
                            _data.Status = "1";
                        }
                    }
                    else
                    {
                        _data.Message = "Profile Details Not Updated..!";
                        _data.Status = "0";
                    }

                }
                catch (Exception ex)
                {
                    if (_dataProfilePic.Status == null && _data.Status == "1")
                    {
                        _data.Message = "Profile Details Successfully Updated,Profile Pic is not updated it's size must be less than 500KB ";
                        _data.Status = "0";
                    }
                    else
                    {
                        _data.Message = "Profile Details Not Updated..!";
                        _data.Status = "0";
                    }
                }
                return Json(_data);
            }

            return Json("");
        }

        [HttpPost]
        public JsonResult GetProfileImage(UserProfileDetails _obj)
        {

            string _response = _api.CallAPI(Constants.GetUserProfilePicture, string.Empty);
            try
            {
                // byte[] image = (new JavaScriptSerializer()).Deserialize<byte[]>(_response);
                return Json(_response.Replace("\"", ""));
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
    }
}