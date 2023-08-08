using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SunriseWeb.Models;
using SunriseWeb.Data;
using Lib.Models;
using System.Reflection;
using System.Runtime.Serialization;
using System.Net;
using Newtonsoft.Json;
using SunriseWeb.Helper;
using SunriseWeb.Resources;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Threading;

namespace SunriseWeb.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        API _api = new API();
        Data.Common _common = new Data.Common();
        public ActionResult Index()
        {
            UserLogin _obj = new UserLogin();
            if (Request.Cookies["Username"] != null && Request.Cookies["Password"] != null && Request.Cookies["IsRemember"] != null)
            {
                _obj.Username = Request.Cookies["Username"].Value.ToString();
                _obj.Password = Request.Cookies["Password"].Value.ToString();
                _obj.isRemember = Convert.ToBoolean(Request.Cookies["IsRemember"].Value);
            }
            ViewBag.Message = "Please enter correct username and password";
            return View(_obj);
        }
        public ActionResult Aboutus()
        {
            return View();
        }
        public ActionResult ExtensiveInventory()
        {
            return View();
        }
        public ActionResult Service()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Terms()
        {
            return View();
        }
        public ActionResult Policy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin _obj)
        {
            if (_obj.isSwitchClassic)
            {
                string loginUrl = ConfigurationManager.AppSettings["OldSiteURL"];
                return Content("<form action='"+ loginUrl + "' id='frmTest' method='post'>"+
                    "<input type='hidden' name='hdnUser' value='" + _obj.Username + "' /><input type='hidden' name='hdnPwd' value='" + _obj.Password +"' />"+
                    "</form><script>document.getElementById('frmTest').submit();</script>");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string _ipAddress = _common.gUserIPAddresss();
                    var input = new LoginRequest
                    {
                        UserName = _obj.Username,
                        Password = _obj.Password,
                        Source = "",
                        IpAddress = _ipAddress,
                        UDID = "",
                        LoginMode = "",
                        DeviseType = "Web",
                        DeviceName = "",
                        AppVersion = "",
                        Location = "",
                        Login = "",
                        grant_type = "password"
                    };
                    string inputJson = string.Join("&", input.GetType()
                                                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                .Where(p => p.GetValue(input, null) != null)
                                       .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(input).ToString())}"));


                    string _response = _api.CallAPIUrlEncoded(Constants.UserLogin, inputJson);
                    //string _response = _api.CallAPIUrlEncodedWithWebReq(Constants.UserLogin, inputJson);
                    if (_response.ToLower().Contains(@"""error") && _response.ToLower().Contains(@"""error_description"))
                    {
                        OAuthErrorMsg _authErrorMsg = new OAuthErrorMsg();
                        _authErrorMsg = (new JavaScriptSerializer()).Deserialize<OAuthErrorMsg>(_response);
                        inputJson = (new JavaScriptSerializer()).Serialize(input);
                        string _keyresponse = _api.CallAPIUrlEncoded(Constants.KeyAccountData, inputJson);
                        ServiceResponse<KeyAccountDataResponse> _objresponse = (new JavaScriptSerializer()).Deserialize<ServiceResponse<KeyAccountDataResponse>>(_keyresponse);
                        
                        TempData["Message"] = _authErrorMsg.error_description;
                    }
                    else
                    {
                        LoginFullResponse _data = new LoginFullResponse();
                        try
                        {
                            _data = (new JavaScriptSerializer()).Deserialize<LoginFullResponse>(_response);
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
                            TempData["Message"] = ex.Message;
                        }
                        catch (Exception ex)
                        {
                            TempData["Message"] = ex.Message;
                        }

                        if (_data.UserID > 0)
                        {
                            SessionFacade.TokenNo = _data.access_token;
                            inputJson = (new JavaScriptSerializer()).Serialize(input);
                            string _keyresponse = _api.CallAPI(Constants.KeyAccountData, inputJson);
                            ServiceResponse<KeyAccountDataResponse> _objresponse = (new JavaScriptSerializer()).Deserialize<ServiceResponse<KeyAccountDataResponse>>(_keyresponse);

                            string _imageResponse = _api.CallAPI(Constants.GetUserProfilePicture, string.Empty);

                            if (_objresponse.Data != null && _objresponse.Data.Count > 0)
                            {
                                SessionFacade.UserSession = _objresponse.Data.FirstOrDefault();
                                SessionFacade.UserSession.ProfileImage = _imageResponse.Replace("\"", "");

                                var obj = _objresponse.Data.FirstOrDefault();

                                Response.Cookies["Userid_DNA"].Value = obj.iUserid.Value.ToString();

                                var _input1 = new
                                {
                                    IPAddress = GetIpValue(),
                                    UserId = obj.iUserid,
                                    Type = "STORED"
                                };
                                var _inputJson_1 = (new JavaScriptSerializer()).Serialize(_input1);
                                string _Response_1 = _api.CallAPI(Constants.IP_Wise_Login_Detail, _inputJson_1); 

                            }
                            if (_obj.isRemember)
                            {
                                Response.Cookies["UserName"].Value = _obj.Username;
                                Response.Cookies["Password"].Value = _obj.Password;
                                Response.Cookies["IsRemember"].Value = _obj.isRemember.ToString();
                            }

                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            TempData["Message"] = _data.Message;
                        }
                    }
                }
                return View(_obj);
            }
        }
        public JsonResult CustModalOK()
        {
            SessionFacade.UserSession.MessageShow = false;
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public string GetIpValue()
        {
           string ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAdd))
            {
                ipAdd = Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
               // lblIPAddress.Text = ipAdd;
            }
            return ipAdd;
        }
        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }


        public string GetIPAddress()
        {
            string IPAddress = string.Empty;
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }
        public ActionResult ForgotPassword()
        {
            try
            {
                var userName = Request["ForgotUsername"];
                var input = new
                {
                    UserName = userName
                };
                string inputJson = (new JavaScriptSerializer()).Serialize(input);
                string _response = _api.CallAPIWithoutToken(Constants.ForgotPassword, inputJson);

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
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Logout()
        {
            Session.Abandon();

            var _input1 = new
            {
                IPAddress = GetIpValue(),
                UserId = 0,
                Type = "EXPIRED"
            };
            var _inputJson_1 = (new JavaScriptSerializer()).Serialize(_input1);
            string _Response_1 = _api.CallAPI(Constants.IP_Wise_Login_Detail, _inputJson_1);

            Response.Cookies["Userid_DNA"].Value = "0";

            string _response = _api.CallAPIWithoutToken(Constants.LogoutWithoutToken, "");            
            return RedirectToAction("Index", "Login");
        }
        public ActionResult _Logout()
        {
            ViewData["URL"] = ConfigurationManager.AppSettings["NewLabWebsiteLogoutURL"]; 
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserRegister(UserRegistrationRequest _obj)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Request.Cookies["language"] != null)
                {
                    _obj.Lang = HttpContext.Request.Cookies["language"].Value;
                }
                string _ipAddress = _common.gUserIPAddresss();
                string inputJson = (new JavaScriptSerializer()).Serialize(_obj);

                string _response = _api.CallAPIWithoutToken(Constants.UserRegister, inputJson);
                CommonResponse _data = new CommonResponse();
                try
                {
                    _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
                    //TempData["Message"] = "Thank you for registration. We will get back to you.";
                    TempData["Message"] = _data.Message;
                }
                catch (WebException ex)
                {
                    _data.Status = "-1";
                    var webException = ex as WebException;
                    if ((Convert.ToString(webException.Status)).ToUpper() == "PROTOCOLERROR")
                    {
                        OAuthErrorMsg error =
                            JsonConvert.DeserializeObject<OAuthErrorMsg>(
                           API.ExtractResponseString(webException));
                        _data.Message = error.error_description;
                    }
                    _data.Message = ex.Message;
                }
                catch (Exception ex)
                {
                    _data.Message = ex.Message;
                }

                return Json(_data);
            }
            return Json("");
        }
        public JsonResult ChangeLanguage(string LangCode)
        {
            if (HttpContext.Request.Cookies["language"] != null)
            {
                HttpContext.Response.Cookies["language"].Expires = DateTime.Now.AddDays(-30);
            }
            HttpContext.Response.Cookies["language"].Value = LangCode;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult StoneDetail(string StoneNo)
        {
            var input = new
            {
                StoneID = StoneNo
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.GetSearchStockByStoneID, inputJson);
            SearchStone _data = (new JavaScriptSerializer()).Deserialize<SearchStone>(_response);
            return View(_data);
        }
        public JsonResult OpenEventModel()
        {
            ServiceResponse<Information> _data;
            var input = new { };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.Login_Information_Get, inputJson);
            _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Information>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult WS_PacketTrace_Upload()
        {
            var input = new { };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIUrlEncodedWithWebReq("/Stock/WS_PacketTrace_Upload", inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        #region Thread :: WS_StockUpload_Ora
        public JsonResult WS_StockUpload_Ora()
        {
            Thread StockUpload = new Thread(StockUpload_Thread);
            StockUpload.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void StockUpload_Thread()
        {
            API _api = new API();
            
            string _response = _api.CallAPIUrlEncodedWithWebReq("/Stock/WS_StockUpload_Ora", string.Empty);
            //CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            //if (_data.Message != "" && _data.Message.Substring(0, 7) == "SUCCESS")
            //{
            //    _api.CallAPIUrlEncodedWithWebReq(Constants.DownloadStockExcelWhenStockUpload, string.Empty);
            //}
        }
        #endregion

        #region Thread :: WS_SalesDataUpdate_Ora
        public JsonResult WS_SalesDataUpdate_Ora()
        {
            Thread SalesDataUpdate_Ora = new Thread(SalesDataUpdate_Ora_Thread);
            SalesDataUpdate_Ora.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void SalesDataUpdate_Ora_Thread()
        {
            API _api = new API();
            var input = new { };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIUrlEncodedWithWebReq("/Stock/WS_SalesDataUpdate_Ora", inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
        }
        #endregion

        #region Thread :: WS_FTP_SFTP_Call
        public JsonResult WS_FTP_SFTP_Call()
        {
            Thread FTP_SFTP = new Thread(FTPSFTPUpload_Thread);
            FTP_SFTP.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void FTPSFTPUpload_Thread()
        {
            API _api = new API();
            var input = new { };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIUrlEncodedWithWebReq("/ApiSettings/WS_FTP_SFTP_Call", inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
        }
        #endregion

        #region Thread :: WS_GetStockAvail
        public JsonResult WS_GetStockAvail()
        {
            Thread GetStockAvail = new Thread(GetStockAvail_Thread);
            GetStockAvail.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void GetStockAvail_Thread()
        {
            API _api = new API();
            var input = new { };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIUrlEncodedWithWebReq("/Stock/WS_GetStockAvail", inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
        }
        #endregion

        #region Thread :: WS_PairStockUpload_Ora
        public JsonResult WS_PairStockUpload_Ora()
        {
            Thread PairStockUpload = new Thread(PairStockUpload_Thread);
            PairStockUpload.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void PairStockUpload_Thread()
        {
            API _api = new API();
            var input = new { };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIUrlEncodedWithWebReq("/Stock/WS_PairStockUpload_Ora", inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            if (_data.Message.Substring(0, 7) == "SUCCESS")
            {
                _api.CallAPIUrlEncodedWithWebReq(Constants.DownloadStockExcelWhenStockUpload, string.Empty);
            }
        }
        #endregion

        #region Thread :: Auto_Suspend_User_Make
        public JsonResult Auto_Suspend_User_Make()
        {
            Thread StockUpload = new Thread(Auto_Suspend_User_Make_Thread);
            StockUpload.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void Auto_Suspend_User_Make_Thread()
        {
            API _api = new API();
            string _response = _api.CallAPIUrlEncodedWithWebReq("/Stock/Auto_Suspend_User_Make", string.Empty);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
        }
        #endregion

        public JsonResult WS_GetGraphDetail_Ora()
        {
            var input = new { };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIUrlEncodedWithWebReq("/Stock/WS_GetGraphDetail_Ora", inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoginCheck()
        {
            string _response = _api.CallAPI("/Stock/LoginCheck", String.Empty);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data != null ? _data.Message : "UNAUTHORIZED", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Stock()
        {
            return View();
        }
        public JsonResult Ora_Stock_History(string FromDate, string ToDate, string PageNo, string PageSize)
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = PageNo,
                PageSize = PageSize
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI("/Stock/Ora_Stock_History", inputJson);
            ServiceResponse<Ora_Stock_History> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Ora_Stock_History>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        #region Thread :: WS_StockUpload_Ora
        public JsonResult WS_AutoStockUpload_Ora()
        {
            Thread StockUpload = new Thread(AutoStockUpload_Thread);
            StockUpload.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void AutoStockUpload_Thread()
        {
            API _api = new API();

            string _response = _api.CallAPIUrlEncodedWithWebReq("/Customer/Supplier_Auto_Stock_Upload", string.Empty);
        }
        #endregion
    }
}