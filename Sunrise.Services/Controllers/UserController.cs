using DAL;
using EpExcelExportLib;
using ExcelExportLib;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Oracle.DataAccess.Client;
using Sunrise.Services.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private const String ProfilePhotoPath = "~/UserProfileImages/";
        DataTableExcelExport ge;
        DataTableExcelExport ep_ge;
        private static UInt32 DiscNormalStyleindex;
        private static UInt32 CutNormalStyleindex;
        private static UInt32 STatusBkgrndIndx;

        /// <summary>
        /// Web service to check and Login into Sunrise web 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Login([FromBody]JObject data)
        {
            LoginRequest loginRequest = new LoginRequest();
            try
            {
                loginRequest = JsonConvert.DeserializeObject<LoginRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new LoginResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            LoginResponse resp;
            try
            {
                resp = CheckLogin(loginRequest);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
            return Ok(resp);
        }

        /// <summary>
        /// To get key manager data and other account related data after login
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetKeyAccountData([FromBody]JObject data)
        {
            LoginRequest loginRequest = new LoginRequest();
            try
            {
                loginRequest = JsonConvert.DeserializeObject<LoginRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<KeyAccountDataResponse>
                {
                    Data = new List<KeyAccountDataResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("ssUsername", DbType.String, ParameterDirection.Input, loginRequest.UserName));
                para.Add(db.CreateParam("ssPassword", DbType.String, ParameterDirection.Input, loginRequest.Password));
                DataTable dt = db.ExecuteSP("UserMas_SelectByUsername", para.ToArray(), false);

                List<KeyAccountDataResponse> keyAccountDataResponse = DataTableExtension.ToList<KeyAccountDataResponse>(dt);
                keyAccountDataResponse.FirstOrDefault().Password = loginRequest.Password;

                return Ok(new ServiceResponse<KeyAccountDataResponse>
                {
                    Data = keyAccountDataResponse,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<KeyAccountDataResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [AllowAnonymous]
        public IHttpActionResult RegisterUser([FromBody]JObject data)
        {
            UserRegistrationRequest userRequest = new UserRegistrationRequest();
            try
            {
                userRequest = JsonConvert.DeserializeObject<UserRegistrationRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            bool field_status = false;
            if (string.IsNullOrEmpty(userRequest.UserName))
                field_status = true;
            if (string.IsNullOrEmpty(userRequest.Password))
                field_status = true;
            if (string.IsNullOrEmpty(userRequest.FirstName))
                field_status = true;
            if (string.IsNullOrEmpty(userRequest.LastName))
                field_status = true;
            if (string.IsNullOrEmpty(userRequest.CompanyName))
                field_status = true;
            if (string.IsNullOrEmpty(userRequest.CompEmail))
                field_status = true;
            if (string.IsNullOrEmpty(userRequest.CompMobile))
                field_status = true;

            if (field_status == true)
            {
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            CommonResponse resp;
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("ssUsername", DbType.String, ParameterDirection.Input, userRequest.UserName));
                para.Add(db.CreateParam("ssPassword", DbType.String, ParameterDirection.Input, userRequest.Password));
                para.Add(db.CreateParam("ssFirstName", DbType.String, ParameterDirection.Input, userRequest.FirstName));
                para.Add(db.CreateParam("ssLastName", DbType.String, ParameterDirection.Input, userRequest.LastName));
                para.Add(db.CreateParam("ssCompName", DbType.String, ParameterDirection.Input, userRequest.CompanyName));
                para.Add(db.CreateParam("ssCompEmail", DbType.String, ParameterDirection.Input, userRequest.CompEmail));
                para.Add(db.CreateParam("ssCompMobile", DbType.String, ParameterDirection.Input, userRequest.CompMobile));

                DataTable dt = db.ExecuteSP("ipd_usermas_insert", para.ToArray(), false);

                if (dt.Rows.Count > 0 && dt.Rows[0]["STATUS"].ToString() == "Y")
                {
                    ServicesCommon.EmailNewRegistration_New(userRequest.CompEmail, userRequest.FirstName + " " + userRequest.LastName, userRequest.UserName, userRequest.Password, userRequest.CompanyName, userRequest.Lang);

                    string Adminemail = null;
                    db = new Database(Request);
                    para = new List<IDbDataParameter>();
                    para.Clear();
                    DataTable DT_AdminLst = db.ExecuteSP("AdminList_Get", para.ToArray(), false);

                    if (DT_AdminLst != null && DT_AdminLst.Rows.Count > 0)
                    {
                        for (int I = 0; I <= DT_AdminLst.Rows.Count - 1; I++)
                        {
                            if (DT_AdminLst.Rows[I]["sCompEmail"] != null && DT_AdminLst.Rows[I]["sCompEmail"].ToString() != "")
                            {
                                Adminemail += DT_AdminLst.Rows[I]["sCompEmail"].ToString() + ",";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(Adminemail))
                    {
                        Adminemail = Adminemail.Remove(Adminemail.Length - 1);
                    }
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ORDER_ADMIN_EMAILID"]))
                        Adminemail += ConfigurationManager.AppSettings["ORDER_ADMIN_EMAILID"];

                    ServicesCommon.EmailNewRegistrationToAdmin("Customer", Adminemail, userRequest.UserName, userRequest.FirstName, userRequest.LastName, null, null, null, userRequest.CompMobile, null, userRequest.CompanyName, userRequest.CompanyAddress, userRequest.CompCity, userRequest.CompCountry, userRequest.CompMobile, userRequest.CompPhone, userRequest.CompEmail, "In-active", Convert.ToInt32(dt.Rows[0]["IUSERID"]));

                    List<IDbDataParameter> para1;
                    Database db2 = new Database(Request);
                    para1 = new List<IDbDataParameter>();

                    para1.Add(db2.CreateParam("p_for_username", DbType.String, ParameterDirection.Input, userRequest.UserName.ToUpper()));
                    para1.Add(db2.CreateParam("p_for_password", DbType.String, ParameterDirection.Input, userRequest.Password));
                    para1.Add(db2.CreateParam("p_for_source", DbType.String, ParameterDirection.Input, DBNull.Value));
                    para1.Add(db2.CreateParam("p_for_ip_add", DbType.String, ParameterDirection.Input, DBNull.Value));
                    para1.Add(db2.CreateParam("p_for_udid", DbType.String, ParameterDirection.Input, DBNull.Value));
                    para1.Add(db2.CreateParam("p_for_type", DbType.String, ParameterDirection.Input, DBNull.Value));
                    para1.Add(db2.CreateParam("p_for_MobileModel", DbType.String, ParameterDirection.Input, DBNull.Value));
                    para1.Add(db2.CreateParam("p_for_AppVersion", DbType.String, ParameterDirection.Input, DBNull.Value));
                    para1.Add(db2.CreateParam("p_for_Location", DbType.String, ParameterDirection.Input, DBNull.Value));

                    DataTable dt1 = db2.ExecuteSP("ipd_check_login", para1.ToArray(), false);
                    string Message = string.Empty, AssistName1 = string.Empty, AssistMobile1 = string.Empty, AssistEmail1 = string.Empty, AssistDetail = string.Empty;
                    if (dt1.Rows[0]["USER_NAME"].ToString().Length > 0)
                    {
                        //Message = GetUserIsActive(dt1.Rows[0]["AssistBy1"].ToString() != "" ? dt1.Rows[0]["AssistBy1"].ToString() : "", dt1.Rows[0]["mob_AssistBy1"].ToString() != "" ? dt1.Rows[0]["mob_AssistBy1"].ToString() : "", dt1.Rows[0]["Email_AssistBy1"].ToString() != "" ? dt1.Rows[0]["Email_AssistBy1"].ToString() : "");
                        //Message = "<div style=\"color:red\">User Is Not Active, Kindly Contact Administrator : </div>" + GetAdminDetail();
                        Message = "<div style=\"color:blue\">We Will review your details and notify activation via email</div>";
                    }
                    resp = new CommonResponse();
                    resp.Status = "1";
                    resp.Message = Message;
                    resp.Error = "";
                    return Ok(resp);
                }
                else if (dt.Rows.Count > 0 && dt.Rows[0]["STATUS"].ToString() == "X")
                {
                    resp = new CommonResponse();
                    resp.Status = "0";
                    resp.Message = "User name is already exists.";
                    resp.Error = "";
                    return Ok(resp);
                }

                resp = new CommonResponse();
                resp.Status = "0";
                resp.Message = "User registration failed.";
                resp.Error = "";

                return Ok(resp);

            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "FAIL",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        public IHttpActionResult hardik_log([FromBody] JObject data)
        {
            try
            {
                hardik_log_req userRequest = new hardik_log_req();
                try
                {
                    userRequest = JsonConvert.DeserializeObject<hardik_log_req>(data.ToString());
                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    return Ok(new CommonResponse
                    {
                        Message = "Input Parameters are not in the proper format",
                        Status = "0"
                    });
                }

                CommonResponse resp = new CommonResponse();

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (string.IsNullOrEmpty(userRequest.Data))
                    para.Add(db.CreateParam("Data", DbType.String, ParameterDirection.Input, "BLANK"));
                else
                    para.Add(db.CreateParam("Data", DbType.String, ParameterDirection.Input, userRequest.Data));

                DataTable dt = db.ExecuteSP("hardik_log_Insert", para.ToArray(), false);

                resp.Status = "1";
                resp.Message = "SUCCESS";
                resp.Error = "";
                return Ok(resp);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "FAIL",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        public IHttpActionResult ForgotPassword([FromBody]JObject data)
        {
            try
            {
                LoginRequest userRequest = new LoginRequest();
                try
                {
                    userRequest = JsonConvert.DeserializeObject<LoginRequest>(data.ToString());
                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    return Ok(new CommonResponse
                    {
                        Message = "Input Parameters are not in the proper format",
                        Status = "0"
                    });
                }

                CommonResponse resp = new CommonResponse();
                MailMessage xloMail = new MailMessage();
                SmtpClient xloSmtp = new SmtpClient();
                try
                {
                    Database db = new Database(Request);
                    List<IDbDataParameter> para;
                    para = new List<IDbDataParameter>();

                    para.Add(db.CreateParam("p_for_username", DbType.String, ParameterDirection.Input, userRequest.UserName));

                    DataTable dt = db.ExecuteSP("IPD_Forget_PassWord", para.ToArray(), false);

                    if (dt.Rows.Count == 0)
                    {
                        resp.Status = "0";
                        resp.Message = "Username is invalid or in-active.";
                        resp.Error = "";
                        return Ok(resp);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Email"].ToString()))
                        {
                            ServicesCommon.EmailForgotPassword(dt.Rows[0]["Email"].ToString(), dt.Rows[0]["Full_Name"].ToString(), userRequest.UserName, dt.Rows[0]["Password"].ToString());

                            string emailAdd = dt.Rows[0]["Email"].ToString();
                            emailAdd = emailAdd.Substring(0, 3) + "*".PadLeft(emailAdd.Length - 8).Replace(" ", "*") + emailAdd.Substring(emailAdd.Length - 5);
                            resp.Status = "1";
                            resp.Message = "Your account information have been sent to you on " + emailAdd;
                            resp.Error = "";
                            return Ok(resp);
                        }
                        else
                        {
                            resp.Status = "0";
                            resp.Message = "Your email address is invalid, please contact our Administrator.";
                            resp.Error = "";
                            return Ok(resp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    resp.Status = "0";
                    resp.Message = ex.ToString();
                    resp.Error = ex.Message;
                    return Ok(resp);
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "FAIL",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult UpdatePassword([FromBody]JObject data)
        {
            try
            {
                LoginRequest userRequest = new LoginRequest();
                try
                {
                    userRequest = JsonConvert.DeserializeObject<LoginRequest>(data.ToString());
                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    return Ok(new CommonResponse
                    {
                        Message = "Input Parameters are not in the proper format",
                        Status = "0"
                    });
                }

                CommonResponse resp = new CommonResponse();
                try
                {
                    int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                    Database db = new Database(Request);
                    List<IDbDataParameter> para;
                    para = new List<IDbDataParameter>();

                    para.Add(db.CreateParam("iiUserid", DbType.String, ParameterDirection.Input, userID));
                    para.Add(db.CreateParam("ssPassword", DbType.String, ParameterDirection.Input, userRequest.Password));

                    DataTable dt = db.ExecuteSP("ipd_usermas_updatepassword", para.ToArray(), false);

                    if (dt.Rows.Count == 0)
                    {
                        resp.Status = "0";
                        resp.Message = "Username is invalid or in-active.";
                        resp.Error = "";
                        return Ok(resp);
                    }
                    else
                    {
                        if (dt.Rows[0]["STATUS"].ToString() == "Y")
                        {
                            resp.Status = "1";
                            resp.Message = "Password changed.";
                            resp.Error = "";
                            return Ok(resp);
                        }
                        else
                        {
                            resp.Status = "0";
                            resp.Message = "Password not changed.";
                            resp.Error = "";
                            return Ok(resp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    resp.Status = "0";
                    resp.Message = ex.ToString();
                    resp.Error = ex.Message;
                    return Ok(resp);
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "FAIL",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetFullUserList([FromBody] JObject data)
        {
            UserListRequest userListRequest = new UserListRequest();
            try
            {
                userListRequest = JsonConvert.DeserializeObject<UserListRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                //userListRequest.UserID = "";

                DataTable dt = GetFullUserListInner(userListRequest);
                List<UserListResponse> userListResponses = new List<UserListResponse>();
                userListResponses = DataTableExtension.ToList<UserListResponse>(dt);

                if (userListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<UserListResponse>
                    {
                        Data = userListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else if (userListResponses.Count == 0 && dt != null)
                {
                    return Ok(new ServiceResponse<UserListResponse>
                    {
                        Data = userListResponses,
                        Message = "No records found.",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<UserListResponse>
                    {
                        Data = userListResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserListResponse>
                {
                    Data = new List<UserListResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetUserDetails()
        {
            try
            {
                UserListRequest userListRequest = new UserListRequest();

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                userListRequest.UserID = userID.ToString();

                DataTable dt = GetFullUserListInner(userListRequest);
                List<UserListResponse> userListResponses = new List<UserListResponse>();
                userListResponses = DataTableExtension.ToList<UserListResponse>(dt);

                if (userListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<UserListResponse>
                    {
                        Data = userListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<UserListResponse>
                    {
                        Data = userListResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserListResponse>
                {
                    Data = new List<UserListResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateUserProfileDetails([FromBody] JObject data)
        {
            UserProfileDetails userDetails = new UserProfileDetails();
            try
            {
                userDetails = JsonConvert.DeserializeObject<UserProfileDetails>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                string UserName = (Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserName").FirstOrDefault().Value;

                userDetails.UserID = userID;

                UserListRequest userListRequest = new UserListRequest();
                userListRequest.UserID = userID.ToString();
                DataTable dt = GetFullUserListInner(userListRequest);
                if (dt != null && dt.Rows.Count > 0)
                {
                    String PrevFirstName = Convert.ToString(dt.Rows[0]["sFirstName"]); String PrevLastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                    String PrevCompanyName = Convert.ToString(dt.Rows[0]["sCompName"]); String PrevCompanyAddress = Convert.ToString(dt.Rows[0]["sCompAddress"]);
                    String PrevCompanyAddress2 = Convert.ToString(dt.Rows[0]["sCompAddress2"]); String PrevCompanyAddress3 = Convert.ToString(dt.Rows[0]["sCompAddress3"]);
                    String PrevCompCity = Convert.ToString(dt.Rows[0]["sCompCity"]); String PrevCompRegNo = Convert.ToString(dt.Rows[0]["sCompRegNo"]);
                    String PrevCompZipCode = Convert.ToString(dt.Rows[0]["sCompZipCode"]); String PrevCompState = Convert.ToString(dt.Rows[0]["sCompState"]);
                    String PrevCompCountry = Convert.ToString(dt.Rows[0]["sCompCountry"]); String PrevCompMobile = Convert.ToString(dt.Rows[0]["sCompMobile"]);
                    String PrevCompMobile2 = Convert.ToString(dt.Rows[0]["sCompMobile2"]); String PrevCompPhone = Convert.ToString(dt.Rows[0]["sCompPhone"]);
                    String PrevCompPhone2 = Convert.ToString(dt.Rows[0]["sCompPhone2"]); String PrevCompFaxNo = Convert.ToString(dt.Rows[0]["sCompFaxNo"]);
                    String PrevCompEmail = Convert.ToString(dt.Rows[0]["sCompEmail"]); String PrevRapnetID = Convert.ToString(dt.Rows[0]["sRapnetID"]);

                    if (UpdateUserDetailInner(userDetails))
                    {
                        resp.Status = "1";
                        resp.Message = "Update user detail successfully.";
                        resp.Error = "";

                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();
                        Database db1 = new Database();
                        para.Add(db1.CreateParam("sUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(userID)));
                        DataTable dtToMailList = db1.ExecuteSP("UserMas_SelectEmailByUserId", para.ToArray(), false);

                        string lsToMail = "";
                        foreach (DataRow row in dtToMailList.Rows)
                            lsToMail += row["sEmail"].ToString() + ",";

                        if (lsToMail.Length > 0)
                            lsToMail = lsToMail.Remove(lsToMail.Length - 1);

                        Lib.Models.Common.EmailChangeProfileToAdmin(lsToMail, UserName, userDetails.CompanyName, userDetails.FirstName, PrevFirstName,
                            userDetails.LastName, PrevLastName, PrevCompanyAddress, userDetails.CompanyAddress,
                             PrevCompanyAddress2, userDetails.CompanyAddress2, PrevCompanyAddress3, userDetails.CompanyAddress3, PrevCompCity,
                             userDetails.CompCity, PrevCompZipCode, userDetails.CompZipCode, PrevCompState, userDetails.CompState,
                             PrevCompCountry, userDetails.CompCountry, PrevCompMobile, userDetails.CompMobile, PrevCompMobile2, userDetails.CompMobile2,
                             PrevCompPhone, userDetails.CompPhone, PrevCompPhone2, userDetails.CompPhone2, PrevCompFaxNo, userDetails.CompFaxNo,
                             PrevCompEmail, userDetails.CompEmail, "", "", PrevRapnetID, userDetails.RapnetID, PrevCompRegNo, userDetails.CompRegNo, userID);
                    }
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Failed to update user detail.";
                    resp.Error = "";
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetUserProfilePicture()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                string[] FileList = Directory.GetFiles(HostingEnvironment.MapPath(ProfilePhotoPath), userID + ".*");
                if (FileList.Length > 0)
                {
                    return Ok(File.ReadAllBytes(FileList[0]));
                }
                else
                {
                    return Ok<byte[]>(null);
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok<byte[]>(null);
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateUserProfilePicture([FromBody] JObject data)
        {
            UserProfilePictureDetails userProfileDetails = new UserProfilePictureDetails();
            try
            {
                userProfileDetails = JsonConvert.DeserializeObject<UserProfilePictureDetails>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                CommonResponse resp = new CommonResponse();


                if (userProfileDetails.Photo != null)
                {
                    String[] FileList = Directory.GetFiles(HostingEnvironment.MapPath(ProfilePhotoPath), userID + ".*");
                    foreach (String item in FileList)
                    {
                        File.Delete(item);
                    }

                    File.WriteAllBytes(HostingEnvironment.MapPath(ProfilePhotoPath) + userID + userProfileDetails.FileExtenstion, userProfileDetails.Photo);

                    resp.Status = "1";
                    resp.Message = "Profile Picture uploaded successfully.";
                    resp.Error = "";
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Failed to upload profile picture.";
                    resp.Error = "";
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = ex.Message.ToString(),
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult AddUser([FromBody] JObject data)
        {
            UserDetails userDetails = new UserDetails();
            try
            {
                userDetails = JsonConvert.DeserializeObject<UserDetails>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                DataTable _dtuserchk = CheckUserName(userDetails.UserName);
                if (_dtuserchk != null)
                {
                    if (_dtuserchk.Rows.Count != 0)
                    {
                        resp.Status = "0";
                        resp.Message = "Username is already exist.";
                        resp.Error = "";
                        return Ok(resp);
                    }
                }
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                DAL.Usermas objUser = new DAL.Usermas();
                DataTable dtUser = objUser.UserMas_SelectByPara(null, null, null, null, userDetails.UserName, null, null, null, null, null, null, null, null, null);
                if (dtUser.Rows.Count == 0)
                {
                    Database db = new Database();
                    List<IDbDataParameter> para = new List<IDbDataParameter>();

                    para.Add(db.CreateParam("ssUsername", DbType.String, ParameterDirection.Input, userDetails.UserName));
                    para.Add(db.CreateParam("ssPassword", DbType.String, ParameterDirection.Input, userDetails.Password));
                    para.Add(db.CreateParam("ssFirstName", DbType.String, ParameterDirection.Input, userDetails.FirstName));
                    para.Add(db.CreateParam("ssLastName", DbType.String, ParameterDirection.Input, userDetails.LastName));
                    para.Add(db.CreateParam("ssCompEmail", DbType.String, ParameterDirection.Input, userDetails.CompEmail));
                    para.Add(db.CreateParam("ssCompMobile", DbType.String, ParameterDirection.Input, userDetails.CompMobile));
                    para.Add(db.CreateParam("bbIsActive", DbType.Boolean, ParameterDirection.Input, userDetails.IsActive));
                    para.Add(db.CreateParam("byiUserType", DbType.Int16, ParameterDirection.Input, userDetails.UserType));
                    para.Add(db.CreateParam("iiModifiedBy", DbType.Int64, ParameterDirection.Input, userID));
                    para.Add(db.CreateParam("iiUserid", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                    DataTable dt = db.ExecuteSP("UserMas_Insert_New", para.ToArray(), false);

                    IPadCommon.EmailNewAdminRegistration(userDetails.UserName, userDetails.Password, userDetails.FirstName, userDetails.LastName, userDetails.CompEmail, userDetails.CompMobile, userDetails.IsActive);

                    string Adminemail = null;
                    db = new Database(Request);
                    para = new List<IDbDataParameter>();
                    para.Clear();
                    DataTable DT_AdminLst = db.ExecuteSP("AdminList_Get", para.ToArray(), false);

                    if (DT_AdminLst != null && DT_AdminLst.Rows.Count > 0)
                    {
                        for (int I = 0; I <= DT_AdminLst.Rows.Count - 1; I++)
                        {
                            if (DT_AdminLst.Rows[I]["sCompEmail"] != null && DT_AdminLst.Rows[I]["sCompEmail"].ToString() != "" && Convert.ToInt32(DT_AdminLst.Rows[I]["iUserid"]) != Convert.ToInt32(dt.Rows[0]["iiUserid"]))
                            {
                                Adminemail += DT_AdminLst.Rows[I]["sCompEmail"].ToString() + ",";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(Adminemail))
                    {
                        Adminemail = Adminemail.Remove(Adminemail.Length - 1);
                    }
                    else {
                        Adminemail = ConfigurationManager.AppSettings["BCCEmail"];
                    }

                    IPadCommon.EmailNewRegistrationToAdmin("Admin", Adminemail, userDetails.UserName, userDetails.FirstName, userDetails.LastName, null, null, null, null, null,
                        userDetails.CompanyName, userDetails.CompanyAddress, null, userDetails.CompCountry, userDetails.CompMobile,
                        userDetails.CompPhone, userDetails.CompEmail, userDetails.IsActive, Convert.ToInt32(userDetails.iiUserid));

                    resp.Status = "1";
                    resp.Message = "User Save Sucessfully.";
                    resp.Error = "";
                    return Ok(resp);

                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Username is already exist.";
                    resp.Error = "";
                    return Ok(resp);
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateUser([FromBody] JObject data)
        {
            UserDetails userDetails = new UserDetails();
            try
            {
                userDetails = JsonConvert.DeserializeObject<UserDetails>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                userDetails.ModifiedByID = userID;
                CultureInfo enGB = new CultureInfo("en-GB");
                DateTime FrmDate = Convert.ToDateTime(Lib.Models.Common.GetHKTime(), enGB);

                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("iiUserid", DbType.String, ParameterDirection.Input, userDetails.UserID));
                para.Add(db.CreateParam("bbIsActive", DbType.String, ParameterDirection.Input, userDetails.IsActive));

                if (string.IsNullOrEmpty(userDetails.FirstName))
                    para.Add(db.CreateParam("ssFirstName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("ssFirstName", DbType.String, ParameterDirection.Input, userDetails.FirstName));

                if (string.IsNullOrEmpty(userDetails.LastName))
                    para.Add(db.CreateParam("ssLastName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("ssLastName", DbType.String, ParameterDirection.Input, userDetails.LastName));

                if (string.IsNullOrEmpty(userDetails.CompanyName))
                    para.Add(db.CreateParam("ssCompName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("ssCompName", DbType.String, ParameterDirection.Input, userDetails.CompanyName));

                if (string.IsNullOrEmpty(userDetails.CompEmail))
                    para.Add(db.CreateParam("ssCompEmail", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("ssCompEmail", DbType.String, ParameterDirection.Input, userDetails.CompEmail));

                if (string.IsNullOrEmpty(userDetails.CompMobile))
                    para.Add(db.CreateParam("ssCompMobile", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("ssCompMobile", DbType.String, ParameterDirection.Input, userDetails.CompMobile));

                para.Add(db.CreateParam("iiModifiedBy", DbType.String, ParameterDirection.Input, userDetails.ModifiedByID.ToString()));

                DataTable dt = db.ExecuteSP("UserMas_Update_Cust", para.ToArray(), false);

                Database db2 = new Database();

                List<IDbDataParameter> para2 = new List<IDbDataParameter>();
                para2.Clear();
                para2.Add(db2.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(userDetails.UserID)));
                DataTable dtUserDetail = db2.ExecuteSP("UserMas_SelectOne", para2.ToArray(), false);

                db2 = new Database();
                para2.Clear();
                para2.Add(db2.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userDetails.UserID)));
                DataTable UserMas_SelectByPara = db2.ExecuteSP("UserMas_SelectByPara", para2.ToArray(), false);

                string _stremail = dtUserDetail.Rows[0]["day_diff"].ToString();
                resp.Status = "1";
                resp.Message = "User Detail Update successfully.";
                resp.Error = "";

                if (userDetails.IsActive.ToLower() == "true" && (userDetails.PrevIsActive.ToString().Length == 0 || userDetails.PrevIsActive.ToLower() == "false"))
                {
                    if (userDetails.Suspended == "Suspended" || Convert.ToInt32(UserMas_SelectByPara.Rows[0]["DAYS"]) > 150)
                    {
                        //----Update Suspended Date
                        Database dbp = new Database();
                        List<IDbDataParameter> paradb;
                        paradb = new List<IDbDataParameter>();
                        paradb.Clear();
                        paradb.Add(dbp.CreateParam("iiUserid", DbType.String, ParameterDirection.Input, Convert.ToInt32(userDetails.UserID)));
                        paradb.Add(dbp.CreateParam("bIsActive", DbType.String, ParameterDirection.Input, 1));
                        dbp.ExecuteSP("UserMas_Update_Suspended_Date", paradb.ToArray(), false);
                    }

                    string lsCCMail = "";
                    Database dbs = new Database();
                    List<IDbDataParameter> paradbs;
                    paradbs = new List<IDbDataParameter>();
                    paradbs.Clear();
                    paradbs.Add(dbs.CreateParam("sUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(userDetails.UserID)));
                    DataTable dtToMailList = dbs.ExecuteSP("UserMas_SelectEmailByUserId", paradbs.ToArray(), false);

                    foreach (DataRow row in dtToMailList.Rows)
                    {
                        lsCCMail += row["sEmail"].ToString() + ",";
                    }

                    if (lsCCMail.Length > 0)
                        lsCCMail = lsCCMail.Remove(lsCCMail.Length - 1);

                    if (_stremail == "Y")
                    {
                        Lib.Models.Common.EmailMemberActiveStatus(userDetails.CompEmail, lsCCMail,
                        userDetails.FirstName + " " + userDetails.LastName, userDetails.UserName, userDetails.CompanyName, null,
                        Convert.ToInt32(userDetails.UserID.ToString()), "");
                    }
                }

                return Ok(resp);

            }
            catch (Exception ex)
            {

                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult DeleteUser([FromBody] JObject data)
        {
            UserProfileDetails userDetails = new UserProfileDetails();
            try
            {
                userDetails = JsonConvert.DeserializeObject<UserProfileDetails>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("Delete_By", DbType.Int16, ParameterDirection.Input, userID));

                if (userDetails.UserID > 0)
                    para.Add(db.CreateParam("iiUserid", DbType.String, ParameterDirection.Input, userDetails.UserID));
                else
                    para.Add(db.CreateParam("iiUserid", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("UserMas_Delete", para.ToArray(), false);

                resp.Message = dt.Rows[0]["Message"].ToString();
                resp.Status = dt.Rows[0]["Status"].ToString();

                return Ok(resp);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult DownloadUser([FromBody] JObject data)
        {
            UserListRequest userListRequest = new UserListRequest();
            try
            {
                userListRequest = JsonConvert.DeserializeObject<UserListRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                userListRequest.UserID = userID.ToString();

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userID)));
                DataTable _dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);
                string iUserType = "";
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    iUserType = _dt.Rows[0]["iUserType"].ToString();
                }

                DataTable dt = GetFullUserListInner(userListRequest);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string fileName = string.Empty;
                    string _path = ConfigurationManager.AppSettings["data"];
                    string _realpath = HostingEnvironment.MapPath("~/ExcelFile/");

                    fileName = "Manage User " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");

                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    EpExcelExport.CreateUserExcel(dt, _realpath, _realpath + fileName + ".xlsx", _livepath, iUserType);

                    string _strxml = _path + fileName + ".xlsx";
                    return Ok(_strxml);
                }
                else
                    return Ok("No record found");
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserListResponse>
                {
                    Data = new List<UserListResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        #region NonAction Methods
        [NonAction]
        public LoginResponse CheckLogin(LoginRequest loginRequest)
        {
            String UserName, Password, Source, IpAddress, UDID, LoginMode, DeviseType, DeviceName = "", AppVersion = "", Location = "", Login = "";
            LoginResponse resp = new LoginResponse();
            try
            {
                UserName = loginRequest.UserName;
                Password = loginRequest.Password;
                Source = loginRequest.Source;
                IpAddress = loginRequest.IpAddress;
                UDID = loginRequest.UDID;
                LoginMode = loginRequest.LoginMode;
                DeviseType = loginRequest.DeviseType;
                DeviceName = loginRequest.DeviceName;
                AppVersion = loginRequest.AppVersion;
                Location = loginRequest.Location;
                Login = loginRequest.Login;

                string _strcheck = "";
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("p_for_username", DbType.String, ParameterDirection.Input, UserName.ToUpper()));
                para.Add(db.CreateParam("p_for_password", DbType.String, ParameterDirection.Input, Password));
                para.Add(db.CreateParam("p_for_source", DbType.String, ParameterDirection.Input, Source));
                para.Add(db.CreateParam("p_for_ip_add", DbType.String, ParameterDirection.Input, IpAddress));
                para.Add(db.CreateParam("p_for_udid", DbType.String, ParameterDirection.Input, UDID));
                para.Add(db.CreateParam("p_for_type", DbType.String, ParameterDirection.Input, DeviseType));

                if (DeviceName == null)
                    DeviceName = "";

                if (DeviceName != "")
                    para.Add(db.CreateParam("p_for_MobileModel", DbType.String, ParameterDirection.Input, DeviceName));
                else
                    para.Add(db.CreateParam("p_for_MobileModel", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (AppVersion == null)
                    AppVersion = "";

                if (AppVersion != "")
                    para.Add(db.CreateParam("p_for_AppVersion", DbType.String, ParameterDirection.Input, AppVersion));
                else
                    para.Add(db.CreateParam("p_for_AppVersion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (Location == null)
                    Location = "";

                if (Location != "")
                    para.Add(db.CreateParam("p_for_Location", DbType.String, ParameterDirection.Input, Location));
                else
                    para.Add(db.CreateParam("p_for_Location", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ipd_check_login", para.ToArray(), false);
                
                string AdminDetail = GetAdminDetail();

                DataTable dts = null;
                if (dt.Rows[0]["USER_NAME"].ToString().Length > 0)
                {
                    Database dbb = new Database(Request);
                    List<IDbDataParameter> paras;
                    paras = new List<IDbDataParameter>();

                    paras.Add(dbb.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, dt.Rows[0]["User_Code"]));
                    dts = dbb.ExecuteSP("Get_SusPended_user", paras.ToArray(), false);
                }

                if (Convert.ToBoolean(dt.Rows[0]["IsDeleted_App"]) == true)
                {
                    resp = new LoginResponse();
                    resp.Status = "0";

                    resp.Message = "<div style=\"color:red\">Your Account has been Deleted</div>";
                }

                else if (dt.Rows[0]["USER_NAME"].ToString().Length == 0)
                {
                    resp = new LoginResponse();
                    resp.Status = "0";

                    //int AssistId = (dt.Rows[0]["AssistBy1"] != null && dt.Rows[0]["AssistBy1"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["AssistBy1"].ToString()) : 0;
                    //string AssistDetail = GetAssistDetail(AssistId);
                    
                    

                    //resp.Message = "<div style=\"color:red\">Wrong User Name or Password Kindly Contact : </div>" + AssistDetail; TOLD ME KISHAN(ASK BY JIGNESH BHAI IN WHATSAPP) AT 28-06-2021 11:01
                    //resp.Message = "<div style=\"color:red\">User Name '" + UserName + "' or Password is Wrong, Kindly Contact : </div>" + AssistDetail;
                    if (dt.Rows[0]["iUserType"].ToString() == "1")
                    {
                        resp.Message = "<div style=\"color:red\">User Name '" + UserName + "' or Password is Wrong</div>";
                    }
                    else
                    {
                        resp.Message = "<div style=\"color:red\">User Name '" + UserName + "' or Password is Wrong, Kindly Contact Administrator : </div>" + AdminDetail;
                    }
                }

                else if ((Boolean)dt.Rows[0]["STATUS"] == false)
                {
                    resp = new LoginResponse();
                    if (Convert.ToInt32(dt.Rows[0]["DAYS"]) > 150 && Convert.ToInt32(dt.Rows[0]["IS_ADMIN"]) == 0 && Convert.ToInt32(dt.Rows[0]["IS_EMP"]) == 0 && Convert.ToInt32(dt.Rows[0]["IS_GUEST"]) == 0)
                    {
                        //var str = "";
                        //if (dt.Rows[0]["AssistBy1"].ToString() != "")
                        //{
                        //    str = str + "<div><i class=\"fa fa-user\" style=\"font-size: 20px;color: teal;\"></i>&nbsp;" + dt.Rows[0]["AssistBy1"].ToString() + "</div>";
                        //}
                        //if (dt.Rows[0]["mob_AssistBy1"].ToString() != "")
                        //{
                        //    str = str + "<div><i class=\"fa fa-mobile\" style=\"font-size: 25px;color: #27c4cc;\"></i>&nbsp;" + dt.Rows[0]["mob_AssistBy1"].ToString() + "</div>";
                        //}
                        //if (dt.Rows[0]["Email_AssistBy1"].ToString() != "")
                        //{
                        //    str = str + "<div><i class=\"fa fa-envelope-o\" style=\"font-size: 20px;color: red;\"></i>&nbsp;" + dt.Rows[0]["Email_AssistBy1"].ToString() + "</div>";
                        //}
                        //if (dt.Rows[0]["wechat_AssistBy1"].ToString() != "")
                        //{
                        //    str = str + "<div><i class=\"fa fa-weixin\" style=\"font-size: 21px;color: #2dc100;\"></i>&nbsp;" + dt.Rows[0]["wechat_AssistBy1"].ToString() + "</div>";
                        //}
                        resp.Status = "0";
                        //resp.Message = "Your Account Is Suspended Kindly Contact At : " + str;
                        if (dt.Rows[0]["iUserType"].ToString() == "1")
                        {
                            resp.Message = "Your Account Is Suspended";
                        }
                        else
                        {
                            resp.Message = "Your Account Is Suspended, Kindly Contact Administrator : " + AdminDetail;
                        }
                        //   resp.TokenNo = "";
                    }
                    else
                    {
                        if (Login != "LWD")
                        {
                            resp.Status = "0";
                            //resp.Message = GetUserIsActive(dt.Rows[0]["AssistBy1"].ToString() != "" ? dt.Rows[0]["AssistBy1"].ToString() : "", dt.Rows[0]["mob_AssistBy1"].ToString() != "" ? dt.Rows[0]["mob_AssistBy1"].ToString() : "", dt.Rows[0]["Email_AssistBy1"].ToString() != "" ? dt.Rows[0]["Email_AssistBy1"].ToString() : "");
                            if (dt.Rows[0]["iUserType"].ToString() == "1")
                            {
                                resp.Message = "<div style=\"color:red\">User Is Not Active</div>";
                            }
                            else
                            {
                                resp.Message = "<div style=\"color:red\">User Is Not Active, Kindly Contact Administrator : </div>" + AdminDetail;
                            }
                        }
                        else
                        {
                            _strcheck = "Y";
                        }
                    }
                }
                else if (dts.Rows.Count > 0 && Convert.ToBoolean(dts.Rows[0]["TotalDays"]) == true)
                {
                    resp = new LoginResponse();
                    Database dbUp = new Database(Request);
                    List<IDbDataParameter> paraUp;
                    paraUp = new List<IDbDataParameter>();

                    paraUp.Add(dbUp.CreateParam("iiUserid", DbType.Int64, ParameterDirection.Input, dt.Rows[0]["User_Code"]));
                    paraUp.Add(dbUp.CreateParam("bIsActive", DbType.Boolean, ParameterDirection.Input, false));

                    DataTable dtUp = dbUp.ExecuteSP("UserMas_ActiveInactive", paraUp.ToArray(), false);

                    string CompName = "", ToEmailId = "", BccEmailAdd = "";
                    db = new Database(Request);
                    para = new List<IDbDataParameter>();
                    para.Clear();
                    DataTable DT_AdminLst = db.ExecuteSP("AdminList_Get", para.ToArray(), false);

                    if (DT_AdminLst != null && DT_AdminLst.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["sCompEmail"].ToString() != "")
                            {
                                BccEmailAdd += dt.Rows[j]["sCompEmail"].ToString() + ",";
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(BccEmailAdd))
                    {
                        BccEmailAdd = ConfigurationManager.AppSettings["BCCEmail"].ToString();
                    }
                    CompName = dt.Rows[0]["Comp_Name"].ToString();
                    ToEmailId = dt.Rows[0]["sCompEmail"].ToString();

                    Lib.Models.Common.EmailOfSuspendedUser("Bhavyam Diam – Account Suspend – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"), ToEmailId, BccEmailAdd.TrimEnd(','), dt.Rows[0]["USER_NAME"].ToString(), CompName);

                    //var str = "";
                    //if (dt.Rows[0]["AssistBy1"].ToString() != "")
                    //{
                    //    str = str + "<div><i class=\"fa fa-user\" style=\"font-size: 20px;color: teal;\"></i>&nbsp;" + dt.Rows[0]["AssistBy1"].ToString() + "</div>";
                    //}
                    //if (dt.Rows[0]["mob_AssistBy1"].ToString() != "")
                    //{
                    //    str = str + "<div><i class=\"fa fa-mobile\" style=\"font-size: 25px;color: #27c4cc;\"></i>&nbsp;" + dt.Rows[0]["mob_AssistBy1"].ToString() + "</div>";
                    //}
                    //if (dt.Rows[0]["Email_AssistBy1"].ToString() != "")
                    //{
                    //    str = str + "<div><i class=\"fa fa-envelope-o\" style=\"font-size: 20px;color: red;\"></i>&nbsp;" + dt.Rows[0]["Email_AssistBy1"].ToString() + "</div>";
                    //}
                    //if (dt.Rows[0]["wechat_AssistBy1"].ToString() != "")
                    //{
                    //    str = str + "<div><i class=\"fa fa-weixin\" style=\"font-size: 21px;color: #2dc100;\"></i>&nbsp;" + dt.Rows[0]["wechat_AssistBy1"].ToString() + "</div>";
                    //}
                    resp.Status = "0";
                    //resp.Message = "Your Account Is Suspended Kindly Contact At : " + str;
                    if (dt.Rows[0]["iUserType"].ToString() == "1")
                    {
                        resp.Message = "Your Account Is Suspended";
                    }
                    else
                    {
                        resp.Message = "Your Account Is Suspended, Kindly Contact Administrator" + AdminDetail;
                    }
                    //resp.TokenNo = "";
                }
                else
                {
                    resp = new LoginResponse();
                    Database dbUp = new Database(Request);
                    List<IDbDataParameter> paraUp;
                    paraUp = new List<IDbDataParameter>();

                    paraUp.Add(dbUp.CreateParam("iiUserid", DbType.Int64, ParameterDirection.Input, dt.Rows[0]["User_Code"]));
                    paraUp.Add(dbUp.CreateParam("bIsActive", DbType.Boolean, ParameterDirection.Input, true));

                    DataTable dtUp = dbUp.ExecuteSP("UserMas_Update_Suspended_Date", paraUp.ToArray(), true);

                    resp.UserName = UserName;
                    //resp.TokenNo = Guid.NewGuid().ToString();
                    resp.Status = "1";
                    resp.Message = "SUCCESS";
                    resp.UserID = Convert.ToInt32(dt.Rows[0]["USER_CODE"]);
                    resp.IsAdmin = Convert.ToInt32(dt.Rows[0]["IS_ADMIN"]);
                    resp.IsEmp = Convert.ToInt32(dt.Rows[0]["IS_EMP"]);
                    resp.IsGuest = Convert.ToInt32(dt.Rows[0]["IS_GUEST"]);
                    resp.TransID = Convert.ToInt32(dt.Rows[0]["TRANS_ID"]);
                }

                if (Login == "LWD" && _strcheck == "Y")
                {
                    resp = new LoginResponse();
                    resp.UserName = UserName;
                    //resp.TokenNo = Guid.NewGuid().ToString();
                    resp.Status = "1";
                    resp.Message = "SUCCESS";
                    resp.UserID = Convert.ToInt32(dt.Rows[0]["USER_CODE"]);
                    resp.IsAdmin = Convert.ToInt32(dt.Rows[0]["IS_ADMIN"]);
                    resp.IsEmp = Convert.ToInt32(dt.Rows[0]["IS_EMP"]);
                    resp.IsGuest = Convert.ToInt32(dt.Rows[0]["IS_GUEST"]);
                    resp.TransID = Convert.ToInt32(dt.Rows[0]["TRANS_ID"]);
                }

            }
            catch (Exception ex)
            {
                resp.Status = "0";
                resp.Message = "Something Went wrong.\nPlease try again later";
                DAL.Common.InsertErrorLog(ex, null, Request);
            }
            return resp;
        }
        [NonAction]
        private string GetUserIsActive(string AssistName, string AssistMobile, string AssistEmail)
        {
            string AssistDetail = string.Empty;
            AssistDetail = "<table><tbody>";
            //AssistDetail += "<tr><td><i class=\"fa fa-user\" style=\"font-size: 20px; color: teal;\"></i></td>";
            //AssistDetail += "<td>&nbsp;" + AssistName + "</td>";
            //AssistDetail += "</tr>";
            AssistDetail += "<tr><td><i class=\"fa fa-mobile\" style=\"font-size: 25px; color: #27c4cc;\"></i></td>";
            AssistDetail += "<td>&nbsp;" + "+852-27235100" + "</td>"; // AssistMobile
            AssistDetail += "</tr><tr>";
            AssistDetail += "<td><i class=\"fa fa-envelope-o\" style=\"font-size: 20px; color: red;\"></i></td>";
            AssistDetail += "<td>&nbsp;" + "support@sunrisediam.com" + "</td>"; // AssistEmail
            AssistDetail += "</tr></tbody></table>";
            //return "<div style=\"color:red\">User Is Not Active, Please Contact : </div>" + AssistDetail;
            return "<div style=\"color:red\">User Is Not Active, Kindly Contact Administrator</div>" + AssistDetail;
        }
        [NonAction]
        private string GetAssistDetail(int UserId)
        {
            string AssistDetail = string.Empty;
            if (UserId > 0)
            {
                string AssistName1 = string.Empty, AssistMobile1 = string.Empty, AssistEmail1 = string.Empty, WeChatId = string.Empty;
                Database db2 = new Database();
                List<IDbDataParameter> para2 = new List<IDbDataParameter>();
                para2.Add(db2.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(UserId)));
                DataTable dt = db2.ExecuteSP("UserMas_SelectOne", para2.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    AssistName1 = dt.Rows[0]["sFirstName"].ToString() + " " + dt.Rows[0]["sLastName"].ToString();
                    AssistMobile1 = (dt.Rows[0]["sCompMobile"] != null && dt.Rows[0]["sCompMobile"].ToString() != "" ? dt.Rows[0]["sCompMobile"].ToString() : "85227235100");
                    AssistEmail1 = (dt.Rows[0]["sCompEmail"] != null && dt.Rows[0]["sCompEmail"].ToString() != "" ? dt.Rows[0]["sCompEmail"].ToString() : "support@sunrisediam.com");

                    AssistDetail = "<table><tbody>";
                    AssistDetail += "<tr><td><i class=\"fa fa-user\" style=\"font-size: 20px; color: teal;\"></i></td>";
                    AssistDetail += "<td>&nbsp;" + AssistName1 + "</td>";
                    AssistDetail += "</tr><tr>";
                    AssistDetail += "<td><i class=\"fa fa-mobile\" style=\"font-size: 25px; color: #27c4cc;\"></i></td>";
                    AssistDetail += "<td>&nbsp;" + AssistMobile1 + "</td>";
                    AssistDetail += "</tr><tr>";
                    AssistDetail += "<td><i class=\"fa fa-envelope-o\" style=\"font-size: 20px; color: red;\"></i></td>";
                    AssistDetail += "<td>&nbsp;" + AssistEmail1 + "</td>";
                    AssistDetail += "</tr></tbody></table>";
                }
            }
            else
            {
                AssistDetail = "<table><tbody>";
                AssistDetail += "<tr><td><i class=\"fa fa-mobile\" style=\"font-size: 25px; color: #27c4cc;\"></i></td>";
                AssistDetail += "<td>&nbsp;+852-2723 5100</td>";
                AssistDetail += "</tr><tr>";
                AssistDetail += "<td><i class=\"fa fa-envelope-o\" style=\"font-size: 20px; color: red;\"></i></td>";
                AssistDetail += "<td>&nbsp;support@sunrisediam.com</td>";
                AssistDetail += "</tr></tbody></table>";
            }

            return AssistDetail;
        }
        [NonAction]
        private string GetAdminDetail()
        {
            string AdminDetail = string.Empty, Name = string.Empty, Mobile = string.Empty, Email = string.Empty;
            Database db = new Database();
            List<IDbDataParameter> para = new List<IDbDataParameter>();
            DataTable dt = db.ExecuteSP("AdminList_Get", para.ToArray(), false);

            if (dt != null && dt.Rows.Count > 0)
            {
                AdminDetail = "<table style=\"margin-top: 5px;font-size: 13px;\"><tbody>";

                foreach (DataRow row in dt.Rows)
                {
                    Name = row["sFirstName"].ToString() + " " + row["sLastName"].ToString();
                    Mobile = row["sCompMobile"].ToString();
                    Email = row["sCompEmail"].ToString();

                    //AdminDetail += "<tr style=\"border: 0.1px #0000ff1a solid;\"><td><i class=\"fa fa-user\" style=\"font-size: 20px;color: teal;margin-left: 4px;\"></i></td>";
                    //AdminDetail += "<td style=\"padding: 5px;\">" + Name + (Email != "" ? " <div style=\"color: blue;\">" + Email + "</div>" : "") + (Mobile != "" ? "<div style=\"color: green;\">+91 " + Mobile + "</div>" : "") + "</td></tr>";
                    
                    AdminDetail += "<tr style=\"border: 0.1px #0000ff1a solid;\">";
                    AdminDetail += "<td>";
                    AdminDetail += (Name != "" ? "<div><i class=\"fa fa-user\" style=\"font-size: 19px;color: teal;margin-left: 4px;\"></i></div>" : "");
                    AdminDetail += (Email != "" ? "<div><i class=\"fa fa-envelope-o\" style=\"font-size: 16px;color: red;margin-left: 4px;\"></i></div>" : "");
                    AdminDetail += (Mobile != "" ? "<div><i class=\"fa fa-mobile\" style=\"font-size: 23px;color: #27c4cc;margin-left: 7px;\"></i></div>" : "");
                    AdminDetail += "</td>";
                    AdminDetail += "<td style=\"padding: 5px;\">";
                    AdminDetail += (Name != "" ? "<div>"+ Name + "</div>" : "");
                    AdminDetail += (Email != "" ? "<div style=\"color: blue;\">"+ Email + "</div>" : "");
                    AdminDetail += (Mobile != "" ? "<div style=\"color: green;\">"+ Mobile.Replace("-"," ") + "</div>" : "");
                    AdminDetail += "</td>";
                    AdminDetail += "</tr>";


                }
                AdminDetail += "</tbody></table>";
            }
            else
            {
                AdminDetail = "";
            }

            return AdminDetail;
        }

        [NonAction]
        private DataTable GetFullUserListInner(UserListRequest UserRequest)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                String vIsActive = "";
                if (!string.IsNullOrEmpty(UserRequest.UserStatus))
                {
                    if (UserRequest.UserStatus.ToUpper() == "A")
                        vIsActive = "1";
                    if (UserRequest.UserStatus.ToUpper() == "I")
                        vIsActive = "0";
                }
                if (UserRequest.IsEmployee != null)
                {
                    if (UserRequest.IsEmployee == "1")
                    {
                        UserRequest.assistby = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value).ToString();
                    }
                }

                if (string.IsNullOrEmpty(UserRequest.UserID))
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(UserRequest.UserID)));

                if (string.IsNullOrEmpty(UserRequest.UserType))
                    para.Add(db.CreateParam("iUserType", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("iUserType", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(UserRequest.UserType)));

                if (string.IsNullOrEmpty(vIsActive))
                    para.Add(db.CreateParam("bIsActive", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("bIsActive", DbType.String, ParameterDirection.Input, vIsActive));

                if (string.IsNullOrEmpty(UserRequest.UserFullName))
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, UserRequest.UserFullName));

                if (string.IsNullOrEmpty(UserRequest.UserName))
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, UserRequest.UserName));

                if (string.IsNullOrEmpty(UserRequest.CompanyName))
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, UserRequest.CompanyName));

                if (string.IsNullOrEmpty(UserRequest.CountryName))
                    para.Add(db.CreateParam("sCountryName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("sCountryName", DbType.String, ParameterDirection.Input, UserRequest.CountryName));

                if (string.IsNullOrEmpty(UserRequest.UserStatus) || UserRequest.UserStatus.ToUpper() != "S")
                    para.Add(db.CreateParam("bSuspendedUser", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("bSuspendedUser", DbType.String, ParameterDirection.Input, 1));

                if (string.IsNullOrEmpty(UserRequest.PageNo))
                    para.Add(db.CreateParam("iPgNo", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.String, ParameterDirection.Input, UserRequest.PageNo));

                para.Add(db.CreateParam("iPgSize", DbType.String, ParameterDirection.Input, "50"));
                if (!string.IsNullOrEmpty(UserRequest.SortColumn) && !string.IsNullOrEmpty(UserRequest.SortDirection))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, UserRequest.SortColumn + " " + UserRequest.SortDirection));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, "dtCreatedDate desc"));

                if (UserRequest.Emp1 == null)
                    UserRequest.Emp1 = "";

                if (UserRequest.Emp1 == "")
                    para.Add(db.CreateParam("iEmployeeId1", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("iEmployeeId1", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(UserRequest.Emp1)));

                if (UserRequest.Emp2 == null)
                    UserRequest.Emp2 = "";
                if (UserRequest.Emp2 == "")
                    para.Add(db.CreateParam("iEmployeeId2", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("iEmployeeId2", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(UserRequest.Emp2)));

                if (UserRequest.assistby == null)
                    UserRequest.assistby = "";

                if (UserRequest.assistby == "")
                    para.Add(db.CreateParam("AssistById", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("AssistById", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(UserRequest.assistby)));

                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, UserRequest.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, UserRequest.ActivityType));
                para.Add(db.CreateParam("PrimaryUser", DbType.Boolean, ParameterDirection.Input, UserRequest.PrimaryUser));

                if (string.IsNullOrEmpty(UserRequest.FilterType))
                    para.Add(db.CreateParam("FilterType", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("FilterType", DbType.String, ParameterDirection.Input, UserRequest.FilterType));

                if (string.IsNullOrEmpty(UserRequest.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, UserRequest.FromDate));

                if (string.IsNullOrEmpty(UserRequest.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, UserRequest.ToDate));

                if (string.IsNullOrEmpty(UserRequest.FortunePartyCode))
                    para.Add(db.CreateParam("FortunePartyCode", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("FortunePartyCode", DbType.String, ParameterDirection.Input, UserRequest.FortunePartyCode));

                DataTable dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private Boolean UpdateUserDetailInner(UserProfileDetails User)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("iiUserid", DbType.String, ParameterDirection.Input, User.UserID));
                para.Add(db.CreateParam("ssFirstName", DbType.String, ParameterDirection.Input, User.FirstName));
                para.Add(db.CreateParam("ssLastName", DbType.String, ParameterDirection.Input, User.LastName));
                para.Add(db.CreateParam("dadtBirthDate", DbType.String, ParameterDirection.Input, User.BirthDate));
                para.Add(db.CreateParam("ssCompName", DbType.String, ParameterDirection.Input, User.CompanyName));
                para.Add(db.CreateParam("ssCompAddress", DbType.String, ParameterDirection.Input, User.CompanyAddress));
                para.Add(db.CreateParam("ssCompAddress2", DbType.String, ParameterDirection.Input, User.CompanyAddress2));
                para.Add(db.CreateParam("ssCompAddress3", DbType.String, ParameterDirection.Input, User.CompanyAddress3));
                para.Add(db.CreateParam("ssCompCity", DbType.String, ParameterDirection.Input, User.CompCity));
                para.Add(db.CreateParam("ssCompZipcode", DbType.String, ParameterDirection.Input, User.CompZipCode));
                para.Add(db.CreateParam("ssCompState", DbType.String, ParameterDirection.Input, User.CompState));
                para.Add(db.CreateParam("ssCompCountry", DbType.String, ParameterDirection.Input, User.CompCountry));
                para.Add(db.CreateParam("ssCompMobile", DbType.String, ParameterDirection.Input, User.CompMobile));
                para.Add(db.CreateParam("ssCompMobile2", DbType.String, ParameterDirection.Input, User.CompMobile2));

                para.Add(db.CreateParam("ssCompPhone", DbType.String, ParameterDirection.Input, User.CompPhone));
                para.Add(db.CreateParam("ssCompPhone2", DbType.String, ParameterDirection.Input, User.CompPhone2));

                para.Add(db.CreateParam("ssCompFaxNo", DbType.String, ParameterDirection.Input, User.CompFaxNo));
                para.Add(db.CreateParam("ssCompEmail", DbType.String, ParameterDirection.Input, User.CompEmail));
                para.Add(db.CreateParam("ssRapNetId", DbType.String, ParameterDirection.Input, User.RapnetID));
                para.Add(db.CreateParam("ssCompRegNo", DbType.String, ParameterDirection.Input, User.CompRegNo));

                if (User.WeChatId == null)
                    User.WeChatId = "";
                if (User.SkypeId == null)
                    User.SkypeId = "";
                if (User.Website == null)
                    User.Website = "";

                para.Add(db.CreateParam("ssWeChatId", DbType.String, ParameterDirection.Input, User.WeChatId));
                para.Add(db.CreateParam("ssSkypeId", DbType.String, ParameterDirection.Input, User.SkypeId));
                para.Add(db.CreateParam("ssWebsite", DbType.String, ParameterDirection.Input, User.Website));

                para.Add(db.CreateParam("siCompCityId", DbType.String, ParameterDirection.Input, User.CompCityId));
                para.Add(db.CreateParam("siCompCountryId", DbType.String, ParameterDirection.Input, User.CompCountryId));

                DataTable dt = db.ExecuteSP("ipd_usermas_update", para.ToArray(), false);
                if (dt.Rows.Count > 0 && dt.Rows[0]["STATUS"].ToString() == "Y")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private DataTable CheckUserName(string suserName)
        {
            Database db = new Database();
            List<IDbDataParameter> para = new List<IDbDataParameter>();

            para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, suserName));
            DataTable dt = db.ExecuteSP("get_user_Detail", para.ToArray(), false);
            return dt;
        }

        [NonAction]
        private bool? CreateUserExcel(DataTable ExportTable, string FolderPath, string FileName)
        {
            bool flag = false;
            try
            {
                GridView gvData = new GridView();
                gvData.AutoGenerateColumns = false;
                gvData.ShowFooter = true;

                BoundField sUsername = new BoundField(); sUsername.DataField = "sUsername"; sUsername.HeaderText = "User Name";
                gvData.Columns.Add(sUsername);

                BoundField sFullName = new BoundField(); sFullName.DataField = "sFullName"; sFullName.HeaderText = "Name";
                gvData.Columns.Add(sFullName);

                BoundField sCompName = new BoundField(); sCompName.DataField = "sCompName"; sCompName.HeaderText = "Company Name";
                gvData.Columns.Add(sCompName);

                BoundField sEmp1 = new BoundField(); sEmp1.DataField = "Emp1"; sEmp1.HeaderText = "Emp1";
                gvData.Columns.Add(sEmp1);
                BoundField sEmp2 = new BoundField(); sEmp2.DataField = "Emp2"; sEmp2.HeaderText = "Emp2";
                gvData.Columns.Add(sEmp2);

                BoundField dtCreatedDate = new BoundField(); dtCreatedDate.DataField = "dtCreatedDate"; dtCreatedDate.HeaderText = "Created Date";
                gvData.Columns.Add(dtCreatedDate);

                BoundField sUserType = new BoundField(); sUserType.DataField = "sUserType"; sUserType.HeaderText = "User Type";
                gvData.Columns.Add(sUserType);

                BoundField Suspended = new BoundField(); Suspended.DataField = "Suspended"; Suspended.HeaderText = "Account Suspended";
                gvData.Columns.Add(Suspended);

                BoundField bIsActive = new BoundField(); bIsActive.DataField = "bIsActive"; bIsActive.HeaderText = "Status";
                gvData.Columns.Add(bIsActive);

                BoundField sCompAddress = new BoundField(); sCompAddress.DataField = "sCompAddress"; sCompAddress.HeaderText = "Address1";
                gvData.Columns.Add(sCompAddress);

                BoundField sCompAddress2 = new BoundField(); sCompAddress2.DataField = "sCompAddress2"; sCompAddress2.HeaderText = "Address2";
                gvData.Columns.Add(sCompAddress2);

                BoundField sCompCity = new BoundField(); sCompCity.DataField = "sCompCity"; sCompCity.HeaderText = "City";
                gvData.Columns.Add(sCompCity);

                BoundField sCompZipcode = new BoundField(); sCompZipcode.DataField = "sCompZipcode"; sCompZipcode.HeaderText = "Zipcode";
                gvData.Columns.Add(sCompZipcode);

                BoundField sCompCountry = new BoundField(); sCompCountry.DataField = "sCompCountry"; sCompCountry.HeaderText = "Country";
                gvData.Columns.Add(sCompCountry);

                BoundField sCompMobile = new BoundField(); sCompMobile.DataField = "sCompMobile"; sCompMobile.HeaderText = "Mobile1";
                gvData.Columns.Add(sCompMobile);

                BoundField sCompMobile2 = new BoundField(); sCompMobile2.DataField = "sCompMobile2"; sCompMobile2.HeaderText = "Mobile2";
                gvData.Columns.Add(sCompMobile2);

                BoundField sCompPhone = new BoundField(); sCompPhone.DataField = "sCompPhone"; sCompPhone.HeaderText = "Phone1";
                gvData.Columns.Add(sCompPhone);

                BoundField sCompPhone2 = new BoundField(); sCompPhone2.DataField = "sCompPhone2"; sCompPhone2.HeaderText = "Phone2";
                gvData.Columns.Add(sCompPhone2);

                BoundField sCompFaxNo = new BoundField(); sCompFaxNo.DataField = "sCompFaxNo"; sCompFaxNo.HeaderText = "Fax No";
                gvData.Columns.Add(sCompFaxNo);

                BoundField sCompEmail = new BoundField(); sCompEmail.DataField = "sCompEmail"; sCompEmail.HeaderText = "Email";
                gvData.Columns.Add(sCompEmail);

                BoundField sWebsite = new BoundField(); sWebsite.DataField = "sWebsite"; sWebsite.HeaderText = "Website";
                gvData.Columns.Add(sWebsite);

                BoundField sSkypeId = new BoundField(); sSkypeId.DataField = "sSkypeId"; sSkypeId.HeaderText = "Skype Id";
                gvData.Columns.Add(sSkypeId);

                BoundField sWeChatId = new BoundField(); sWeChatId.DataField = "sWeChatId"; sWeChatId.HeaderText = "WeChat Id";
                gvData.Columns.Add(sWeChatId);

                BoundField sCompRegNo = new BoundField(); sCompRegNo.DataField = "sCompRegNo"; sCompRegNo.HeaderText = "Bussiness Reg. No";
                gvData.Columns.Add(sCompRegNo);

                BoundField sRapNetId = new BoundField(); sRapNetId.DataField = "sRapNetId"; sRapNetId.HeaderText = "Rap Id";
                gvData.Columns.Add(sRapNetId);

                gvData.DataSource = ExportTable;
                gvData.DataBind();

                GridViewEpExcelExport ep_ge;
                ep_ge = new GridViewEpExcelExport(gvData, "ManageUser", "ManageUser");

                ep_ge.BeforeCreateColumnEvent += Ep_BeforeCreateColumnEventHandler;
                ep_ge.AfterCreateCellEvent += Ep_AfterCreateCellEventHandler;
                ep_ge.FillingWorksheetEvent += Ep_FillingWorksheetEventHandler;
                ep_ge.AddHeaderEvent += Ep_AddHeaderEventHandler;

                MemoryStream ms = new MemoryStream();
                ep_ge.CreateExcel(ms, HostingEnvironment.MapPath("~/Temp/Excel/"));

                //string parentPath = FolderPath;
                //string fileName = string.Empty;
                //MemoryStream ms = new MemoryStream();
                //ge.CreateExcel(ms);
                File.WriteAllBytes(FileName, ms.ToArray());

                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
            return flag;
        }

        [NonAction]
        private static void Ep_FillingWorksheetEventHandler(object sender, ref EpExcelExport.FillingWorksheetEventArgs e)
        {
            EpExcelExport ee = (EpExcelExport)sender;
            EpExcelExport.ExcelFormat format = new EpExcelExport.ExcelFormat();

            format = new EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            format.isbold = true;
            DiscNormalStyleindex = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.isbold = true;
            CutNormalStyleindex = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            // format.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Yellow.ToArgb());
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            STatusBkgrndIndx = ee.AddStyle(format);
        }

        [NonAction]
        private static void Ep_BeforeCreateColumnEventHandler(object sender, ref EpExcelExport.ExcelHeader e)
        {
            if (e.Caption == "User Name")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 22;
            }
            if (e.Caption == "Name")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 22;
            }
            if (e.Caption == "Company Name")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 25;
                e.NumFormat = "#0.00";
            }
            if (e.Caption == "Emp1")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 22;
            }
            if (e.Caption == "Emp2")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 22;
            }
            if (e.Caption == "Created Date")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "User Type")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "Account Suspended")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "Status")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }

            if (e.Caption == "Address1")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 25;
            }
            if (e.Caption == "Address2")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 25;
            }
            if (e.Caption == "City")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "Zipcode")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            if (e.Caption == "Country")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            if (e.Caption == "Mobile1")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            if (e.Caption == "Mobile2")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            if (e.Caption == "Phone1")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            if (e.Caption == "Phone2")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            if (e.Caption == "Fax No")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "Email")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "Website")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "Skype Id")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "WeChat Id")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "Bussiness Reg. No")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            if (e.Caption == "Rap Id")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
        }

        [NonAction]
        private static void Ep_AfterCreateCellEventHandler(object sender, ref EpExcelExport.ExcelCellFormat e)
        {
            if (e.tableArea == EpExcelExport.TableArea.Header)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.isbold = true;
            }
            else if (e.tableArea == EpExcelExport.TableArea.Detail)
            {
                if (e.ColumnName == "Account Suspended")
                {
                    e.StyleInd = STatusBkgrndIndx;
                }
                //else if (e.ColumnName == "Zipcode")
                //{
                //    DataRow row = ((DataRowView)((GridViewRow)e.GridRow).DataItem).Row
                //}
            }
            else if (e.tableArea == EpExcelExport.TableArea.Footer)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.isbold = true;
            }
        }

        [NonAction]
        private void Ep_AddHeaderEventHandler(object sender, ref EpExcelExport.AddHeaderEventArgs e)
        {
            EpExcelExport ee = (EpExcelExport)sender;

            EpExcelExportLib.EpExcelExport.ExcelCellFormat f = new EpExcelExportLib.EpExcelExport.ExcelCellFormat();
            f.isbold = true;
            f.fontsize = 11;
            UInt32 statusind = ee.AddStyle(f);
            EpExcelExportLib.EpExcelExport.ExcelCellFormat c = new EpExcelExportLib.EpExcelExport.ExcelCellFormat();
            c.isbold = true;
            c.fontsize = 24;
            c.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(0, 112, 192).ToArgb());
            UInt32 xInd = ee.AddStyle(c);

            ee.SetCellValue("B1", " SUNRISE DIAMONDS LTD. ", xInd);
        }

        [HttpPost]
        public IHttpActionResult UserListSearch([FromBody]JObject data)
        {
            UserListSearchRequest req = new UserListSearchRequest();
            try
            {
                req = JsonConvert.DeserializeObject<UserListSearchRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserListSearchResponse>
                {
                    Data = new List<UserListSearchResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(req.FromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, Convert.ToDateTime(req.FromDate)));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToDate))
                    para.Add(db.CreateParam("dtTodate", DbType.DateTime, ParameterDirection.Input, Convert.ToDateTime(req.ToDate)));
                else
                    para.Add(db.CreateParam("dtTodate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserFullName))
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, req.UserFullName));
                else
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CountryName))
                    para.Add(db.CreateParam("sCountry", DbType.String, ParameterDirection.Input, req.CountryName));
                else
                    para.Add(db.CreateParam("sCountry", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserName))
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CompanyName))
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, req.CompanyName));
                else
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserType))
                    para.Add(db.CreateParam("iUserType", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.UserType)));
                else
                    para.Add(db.CreateParam("iUserType", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PageNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PageSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("UserMas_SelectByDate_UserType", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<UserListSearchResponse> list = new List<UserListSearchResponse>();
                    list = DataTableExtension.ToList<UserListSearchResponse>(dt);

                    if (list.Count > 0)
                    {
                        return Ok(new ServiceResponse<UserListSearchResponse>
                        {
                            Data = list,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<UserListSearchResponse>
                        {
                            Data = null,
                            Message = "Something Went wrong.",
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<UserListSearchResponse>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserListSearchResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult DownloadUserListSearch([FromBody]JObject data)
        {
            UserListSearchRequest req = new UserListSearchRequest();
            try
            {
                req = JsonConvert.DeserializeObject<UserListSearchRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserListSearchResponse>
                {
                    Data = new List<UserListSearchResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                if (!string.IsNullOrEmpty(req.FromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, Convert.ToDateTime(req.FromDate)));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToDate))
                    para.Add(db.CreateParam("dtTodate", DbType.DateTime, ParameterDirection.Input, Convert.ToDateTime(req.ToDate)));
                else
                    para.Add(db.CreateParam("dtTodate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserFullName))
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, req.UserFullName));
                else
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CountryName))
                    para.Add(db.CreateParam("sCountry", DbType.String, ParameterDirection.Input, req.CountryName));
                else
                    para.Add(db.CreateParam("sCountry", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserName))
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CompanyName))
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, req.CompanyName));
                else
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserType))
                    para.Add(db.CreateParam("iUserType", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.UserType)));
                else
                    para.Add(db.CreateParam("iUserType", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PageNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PageSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, userID));
                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, req.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, req.ActivityType));

                DataTable dt = db.ExecuteSP("UserMas_SelectByDate_UserType", para.ToArray(), false);

                if (dt.Rows.Count > 0)
                {
                    string filename = "";
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    filename = "UserList " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    EpExcelExport.CreateUserList(dt.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(new CommonResponse
                    {
                        Message = _strxml,
                        Status = "1",
                        Error = ""
                    });
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Message = "No data found.",
                        Status = "0",
                        Error = ""
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0",
                    Error = ""
                });
            }
        }
        #endregion
        [HttpPost]
        public IHttpActionResult NewsMst([FromBody]JObject data)
        {
            NewsMst Ns = new NewsMst();
            News N = new News();
            try
            {
                Ns = JsonConvert.DeserializeObject<NewsMst>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<NewsMst>
                {
                    Data = new List<NewsMst>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                if (Ns.Flag == "Insert")
                {
                    N.NewsMas_Insert(Ns.Description, Convert.ToDateTime(Ns.FromDate), Convert.ToDateTime(Ns.ToDate), Ns.FontColor, "");
                }
                if (Ns.Flag == "Update")
                {
                    N.Newsmas_Update(Ns.iID, Ns.Description, Convert.ToDateTime(Ns.FromDate), Convert.ToDateTime(Ns.ToDate), Ns.FontColor, "");
                }
                if (Ns.Flag == "Delete")
                {
                    N.Newsmas_Delete(Ns.iID);
                }
                if (Ns.Flag == "Select")
                {
                    DataTable dt = N.NewsMas_Select(null, null, null, null);
                    List<NewsMst> ListResponses = new List<NewsMst>();
                    ListResponses = DataTableExtension.ToList<NewsMst>(dt);
                    if (ListResponses.Count > 0)
                    {
                        return Ok(new ServiceResponse<NewsMst>
                        {
                            Data = ListResponses,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                }
                if (Ns.Flag == "SelectId")
                {
                    DataTable dt = N.News_Select_By_Id();
                    List<NewsMst> ListResponses = new List<NewsMst>();
                    ListResponses = DataTableExtension.ToList<NewsMst>(dt);
                    if (ListResponses.Count > 0)
                    {
                        return Ok(new ServiceResponse<NewsMst>
                        {
                            Data = ListResponses,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                }
                return Ok(new ServiceResponse<NewsMst>
                {
                    Data = new List<NewsMst>(),
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<KeyAccountDataResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult ErrorLogMst([FromBody]JObject data)
        {
            ErrorLog Elog = new ErrorLog();
            ErrorLgRequest _Elog = new ErrorLgRequest();
            try
            {
                _Elog = JsonConvert.DeserializeObject<ErrorLgRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<NewsMst>
                {
                    Data = new List<NewsMst>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                DataTable dt = Elog.ErrorLog_Select(Convert.ToDateTime(_Elog.FromDate), Convert.ToDateTime(_Elog.ToDate), null, null, null, null, null, _Elog.MSearch, _Elog.PageNo, _Elog.PageSize, _Elog.OrderBy);
                List<ErrorLgResponse> ListResponses = new List<ErrorLgResponse>();
                ListResponses = DataTableExtension.ToList<ErrorLgResponse>(dt);
                if (ListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<ErrorLgResponse>
                    {
                        Data = ListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                return Ok(new ServiceResponse<ErrorLgResponse>
                {
                    Data = new List<ErrorLgResponse>(),
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<KeyAccountDataResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult NotifyList([FromBody]JObject data)
        {
            NotifyRequest notifyrequest = new NotifyRequest();

            try
            {
                notifyrequest = JsonConvert.DeserializeObject<NotifyRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (!string.IsNullOrEmpty(notifyrequest.SearchList))
                    para.Add(db.CreateParam("SearchList", DbType.String, ParameterDirection.Input, notifyrequest.SearchList));
                else
                    para.Add(db.CreateParam("SearchList", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifyrequest.NotifyId.ToString()))
                    para.Add(db.CreateParam("NotifyId", DbType.String, ParameterDirection.Input, notifyrequest.NotifyId));
                else
                    para.Add(db.CreateParam("NotifyId", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifyrequest.PageNo.ToString()))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, notifyrequest.PageNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifyrequest.PageSize.ToString()))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, notifyrequest.PageSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifyrequest.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, notifyrequest.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("NotificationUser_GET", para.ToArray(), false);

                List<NotifyResponse> ListResponses = new List<NotifyResponse>();
                ListResponses = DataTableExtension.ToList<NotifyResponse>(dtData);

                if (ListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<NotifyResponse>
                    {
                        Data = ListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<NotifyResponse>
                    {
                        Data = new List<NotifyResponse>(),
                        Message = "No Record Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<NotifyResponse>
                {
                    Data = new List<NotifyResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult NotifySave([FromBody]JObject data)
        {
            // Api_Insert_sunrise	
            NotifySaveRequest notifysaverequest = new NotifySaveRequest();
            try
            {
                notifysaverequest = JsonConvert.DeserializeObject<NotifySaveRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                notifysaverequest.iUserId = userID;


                var db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(notifysaverequest.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, notifysaverequest.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifysaverequest.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, notifysaverequest.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifysaverequest.Message))
                    para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, notifysaverequest.Message));
                else
                    para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifysaverequest.UserIdList))
                    para.Add(db.CreateParam("UserIdList", DbType.String, ParameterDirection.Input, notifysaverequest.UserIdList));
                else
                    para.Add(db.CreateParam("UserIdList", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));

                if (!string.IsNullOrEmpty(notifysaverequest.NotifyId.ToString()))
                    para.Add(db.CreateParam("NotifyId", DbType.Int32, ParameterDirection.Input, notifysaverequest.NotifyId));
                else
                    para.Add(db.CreateParam("NotifyId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dtData1 = db.ExecuteSP("NotificationUser_Insert", para.ToArray(), false);

                if ((dtData1.Rows[0]["NotifyId"].ToString() != "" ? Int32.Parse(dtData1.Rows[0]["NotifyId"].ToString()) : 0) > 0)
                {
                    if (notifysaverequest.NotifyId == null || notifysaverequest.NotifyId == 0)
                    {
                        resp.Status = "1";
                        resp.Message = dtData1.Rows[0]["NotifyId"].ToString();
                        resp.Error = "";
                    }
                    else
                    {
                        resp.Status = "1";
                        resp.Message = dtData1.Rows[0]["NotifyId"].ToString();
                        resp.Error = "";
                    }
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Failed to save Notification details !";
                    resp.Error = "";
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult NotifyDetList([FromBody]JObject data)
        {
            NotifyDetRequest notifydetrequest = new NotifyDetRequest();

            try
            {
                notifydetrequest = JsonConvert.DeserializeObject<NotifyDetRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (!string.IsNullOrEmpty(notifydetrequest.FromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, notifydetrequest.FromDate));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifydetrequest.ToDate))
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, notifydetrequest.ToDate));
                else
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifydetrequest.PageNo.ToString()))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, notifydetrequest.PageNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifydetrequest.PageSize.ToString()))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, notifydetrequest.PageSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(notifydetrequest.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, notifydetrequest.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("NotificationDetUser_GET", para.ToArray(), false);

                List<NotifyDetResponse> ListResponses = new List<NotifyDetResponse>();
                ListResponses = DataTableExtension.ToList<NotifyDetResponse>(dtData);

                if (ListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<NotifyDetResponse>
                    {
                        Data = ListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<NotifyDetResponse>
                    {
                        Data = new List<NotifyDetResponse>(),
                        Message = "No Record Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<NotifyDetResponse>
                {
                    Data = new List<NotifyDetResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult NotifyGet_User([FromBody]JObject data)
        {
            NotifyRequest notifyrequest = new NotifyRequest();

            try
            {
                notifyrequest = JsonConvert.DeserializeObject<NotifyRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                notifyrequest.iUserId = userID;

                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                para.Add(db.CreateParam("iUserid", DbType.Int32, ParameterDirection.Input, notifyrequest.iUserId));

                System.Data.DataTable dtData = db.ExecuteSP("Notify_GET", para.ToArray(), false);

                List<NotifyGet_UserResponse> ListResponses = new List<NotifyGet_UserResponse>();
                ListResponses = DataTableExtension.ToList<NotifyGet_UserResponse>(dtData);

                if (ListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<NotifyGet_UserResponse>
                    {
                        Data = ListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<NotifyGet_UserResponse>
                    {
                        Data = new List<NotifyGet_UserResponse>(),
                        Message = "No Notification Found",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult IP_Wise_Login_Detail([FromBody]JObject data)
        {
            IP_Wise_Login_Detail ip_wise_login_detailrequest = new IP_Wise_Login_Detail();

            try
            {
                ip_wise_login_detailrequest = JsonConvert.DeserializeObject<IP_Wise_Login_Detail>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (!string.IsNullOrEmpty(ip_wise_login_detailrequest.UserId.ToString()))
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, ip_wise_login_detailrequest.UserId));
                else
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ip_wise_login_detailrequest.IPAddress))
                    para.Add(db.CreateParam("IPAddress", DbType.String, ParameterDirection.Input, ip_wise_login_detailrequest.IPAddress));
                else
                    para.Add(db.CreateParam("IPAddress", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ip_wise_login_detailrequest.Type))
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, ip_wise_login_detailrequest.Type));
                else
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("IP_Wise_Login_Detail_Stored_CRUD", para.ToArray(), false);
                List<IP_Wise_Login_Detail> ListResponses = new List<IP_Wise_Login_Detail>();
                if (ip_wise_login_detailrequest.Type == "GET")
                {
                    ListResponses = DataTableExtension.ToList<IP_Wise_Login_Detail>(dtData);
                }
                return Ok(new ServiceResponse<IP_Wise_Login_Detail>
                {
                    Data = ListResponses,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult PacketTraceGetList([FromBody]JObject data)
        {
            Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
            List<OracleParameter> paramList = new List<OracleParameter>();
            PacketTrace_Request obj = new PacketTrace_Request();
            try
            {
                obj = JsonConvert.DeserializeObject<PacketTrace_Request>(data.ToString());

                OracleParameter param3 = new OracleParameter("p_for_comp", OracleDbType.Int32);
                param3.Value = 1;
                paramList.Add(param3);

                OracleParameter param4 = new OracleParameter("p_for_seq", OracleDbType.Int32);
                param4.Value = null;
                paramList.Add(param4);

                OracleParameter param5 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param5.Direction = ParameterDirection.Output;
                paramList.Add(param5);

                if (obj.StockId != "")
                {
                    OracleParameter param1 = new OracleParameter("p_ref_no", OracleDbType.Varchar2);
                    param1.Value = obj.StockId;
                    paramList.Add(param1);

                    OracleParameter param2 = new OracleParameter("p_certi", OracleDbType.Varchar2);
                    param2.Value = null;
                    paramList.Add(param2);
                }
                else if (obj.CertiNo != "")
                {
                    OracleParameter param1 = new OracleParameter("p_ref_no", OracleDbType.Varchar2);
                    param1.Value = null;
                    paramList.Add(param1);

                    OracleParameter param2 = new OracleParameter("p_certi", OracleDbType.Varchar2);
                    param2.Value = obj.CertiNo;
                    paramList.Add(param2);
                }

                System.Data.DataTable dtData = oracleDbAccess.CallSP("GET_TRANS", paramList);

                List<PacketTrace_Response> ListResponses = new List<PacketTrace_Response>();
                ListResponses = DataTableExtension.ToListOracle<PacketTrace_Response>(dtData);

                return Ok(new ServiceResponse<PacketTrace_Response>
                {
                    Data = ListResponses,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<PacketTrace_Response>
                {
                    Data = new List<PacketTrace_Response>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
            finally
            {
                oracleDbAccess = null;
                paramList = null;
                obj = null;
            }
        }

        [HttpPost]
        public IHttpActionResult OrderDisc_InsUpd([FromBody]JObject data)
        {
            try
            {
                OrderDisc_InsUpd OrderDisc = new OrderDisc_InsUpd();
                OrderDisc = JsonConvert.DeserializeObject<OrderDisc_InsUpd>(data.ToString());
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("IUType", DbType.String, ParameterDirection.Input, OrderDisc.IUType));
                para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, OrderDisc.Id));
                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, userID));

                if (!string.IsNullOrEmpty(OrderDisc.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.Date, ParameterDirection.Input, OrderDisc.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.Date, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(OrderDisc.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.Date, ParameterDirection.Input, OrderDisc.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.Date, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(OrderDisc.Type))
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, OrderDisc.Type));
                else
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(OrderDisc.Discount))
                    para.Add(db.CreateParam("Discount", DbType.String, ParameterDirection.Input, OrderDisc.Discount));
                else
                    para.Add(db.CreateParam("Discount", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(OrderDisc.Value))
                    para.Add(db.CreateParam("Value", DbType.String, ParameterDirection.Input, OrderDisc.Value));
                else
                    para.Add(db.CreateParam("Value", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("OrderDisc_InsUpd", para.ToArray(), false);

                return Ok(new CommonResponse
                {
                    Message = dt.Rows[0]["Message"].ToString(),
                    Status = dt.Rows[0]["Status"].ToString(),
                    Error = ""
                });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse()
                {
                    Status = "0",
                    Message = "Something Went wrong.\nPlease try again later",
                    Error = ""
                });
            }
        }
        [HttpPost]
        public IHttpActionResult OrderDisc_Select([FromBody]JObject data)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                DataTable dt = db.ExecuteSP("OrderDisc_Select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<OrderDisc_Select> response = new List<OrderDisc_Select>();
                    response = DataTableExtension.ToList<OrderDisc_Select>(dt);
                    if (response.Count > 0)
                    {
                        return Ok(new ServiceResponse<OrderDisc_Select>
                        {
                            Data = response,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<OrderDisc_Select>
                        {
                            Data = null,
                            Message = "No records found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<OrderDisc_Select>
                    {
                        Data = null,
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<OrderDisc_Select>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Set_App_info([FromBody]JObject data)
        {
            App_info_Request App_info_Request = new App_info_Request();
            try
            {
                App_info_Request = JsonConvert.DeserializeObject<App_info_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(App_info_Request.Login_Type))
                    para.Add(db.CreateParam("Login_Type", DbType.String, ParameterDirection.Input, App_info_Request.Login_Type));
                else
                    para.Add(db.CreateParam("Login_Type", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(App_info_Request.App_Version))
                    para.Add(db.CreateParam("App_Version", DbType.String, ParameterDirection.Input, App_info_Request.App_Version));
                else
                    para.Add(db.CreateParam("App_Version", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(App_info_Request.flg))
                    para.Add(db.CreateParam("flg", DbType.String, ParameterDirection.Input, App_info_Request.flg));
                else
                    para.Add(db.CreateParam("flg", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("updflg", DbType.Int64, ParameterDirection.Input, 0));

                DataTable dt = db.ExecuteSP("App_info_Ins_upd", para.ToArray(), false);

                String msg = string.Empty;
                if (App_info_Request.flg == "I")
                {
                    msg = "Application Information Saved Successfully";
                }
                else if (App_info_Request.flg == "U")
                {
                    msg = "Application Information Updated Successfully";
                }
                else if (App_info_Request.flg == "D")
                {
                    msg = "Application Information Deleted Successfully";
                }
                return Ok(new CommonResponse
                {
                    Message = msg,
                    Status = "1",
                    Error = "0"
                });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0",
                    Error = "1"
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get_App_info([FromBody]JObject data)
        {
            App_info App_info = new App_info();
            try
            {
                App_info = JsonConvert.DeserializeObject<App_info>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(App_info.DeviceType))
                    para.Add(db.CreateParam("p_for_Type", DbType.String, ParameterDirection.Input, App_info.DeviceType));
                else
                    para.Add(db.CreateParam("p_for_Type", DbType.String, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("Get_App_info", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<App_info> response = new List<App_info>();
                    response = DataTableExtension.ToList<App_info>(dt);
                    if (response.Count > 0)
                    {
                        return Ok(new ServiceResponse<App_info>
                        {
                            Data = response,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<App_info>
                        {
                            Data = null,
                            Message = "No records found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<App_info>
                    {
                        Data = null,
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<App_info>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_UserMgt([FromBody]JObject data)
        {
            UserMgtRequest usermgtrequest = new UserMgtRequest();

            try
            {
                usermgtrequest = JsonConvert.DeserializeObject<UserMgtRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                if (usermgtrequest.iUserId == 0)
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, usermgtrequest.iUserId));

                if (!string.IsNullOrEmpty(usermgtrequest.iPgNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(usermgtrequest.iPgNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtrequest.iPgSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(usermgtrequest.iPgSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtrequest.sOrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, usermgtrequest.sOrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtrequest.Search))
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, usermgtrequest.Search));
                else
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("Active", DbType.Boolean, ParameterDirection.Input, usermgtrequest.Active));
                para.Add(db.CreateParam("InActive", DbType.Boolean, ParameterDirection.Input, usermgtrequest.InActive));

                System.Data.DataTable dtData = db.ExecuteSP("Get_UserManagement", para.ToArray(), false);

                List<UserMgtResponse> ListResponses = new List<UserMgtResponse>();
                ListResponses = DataTableExtension.ToList<UserMgtResponse>(dtData);

                if (ListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<UserMgtResponse>
                    {
                        Data = ListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<UserMgtResponse>
                    {
                        Data = new List<UserMgtResponse>(),
                        Message = "No Record Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserMgtResponse>
                {
                    Data = new List<UserMgtResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Excel_UserMgt([FromBody]JObject data)
        {
            UserMgtRequest usermgtrequest = new UserMgtRequest();
            try
            {
                usermgtrequest = JsonConvert.DeserializeObject<UserMgtRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }
            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                if (usermgtrequest.iUserId == 0)
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, usermgtrequest.iUserId));

                if (!string.IsNullOrEmpty(usermgtrequest.iPgNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(usermgtrequest.iPgNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtrequest.iPgSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(usermgtrequest.iPgSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtrequest.sOrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, usermgtrequest.sOrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtrequest.Search))
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, usermgtrequest.Search));
                else
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("Active", DbType.Boolean, ParameterDirection.Input, usermgtrequest.Active));
                para.Add(db.CreateParam("InActive", DbType.Boolean, ParameterDirection.Input, usermgtrequest.InActive));

                System.Data.DataTable dtData = db.ExecuteSP("Get_UserManagement", para.ToArray(), false);
                dtData.Rows.Remove(dtData.Rows[0]);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    string filename = "User Management " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    EpExcelExport.Excel_UserManagement(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                {
                    return Ok("No data found.");
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Something Went wrong.\nPlease try again later");
            }
        }
        [HttpPost]
        public IHttpActionResult Save_UserMgt([FromBody]JObject data)
        {
            UserMgtSave_Request usermgtsave_request = new UserMgtSave_Request();
            try
            {
                usermgtsave_request = JsonConvert.DeserializeObject<UserMgtSave_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("UserMgt_By", DbType.Int32, ParameterDirection.Input, userID));

                if (!string.IsNullOrEmpty(usermgtsave_request.Type))
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, usermgtsave_request.Type));
                else
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (usermgtsave_request.iUserId > 0)
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(usermgtsave_request.iUserId)));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtsave_request.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, usermgtsave_request.UserName));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtsave_request.Password))
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, usermgtsave_request.Password));
                else
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtsave_request.FirstName))
                    para.Add(db.CreateParam("FirstName", DbType.String, ParameterDirection.Input, usermgtsave_request.FirstName));
                else
                    para.Add(db.CreateParam("FirstName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtsave_request.LastName))
                    para.Add(db.CreateParam("LastName", DbType.String, ParameterDirection.Input, usermgtsave_request.LastName));
                else
                    para.Add(db.CreateParam("LastName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtsave_request.MobileNo))
                    para.Add(db.CreateParam("MobileNo", DbType.String, ParameterDirection.Input, usermgtsave_request.MobileNo));
                else
                    para.Add(db.CreateParam("MobileNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(usermgtsave_request.EmailId))
                    para.Add(db.CreateParam("EmailId", DbType.String, ParameterDirection.Input, usermgtsave_request.EmailId));
                else
                    para.Add(db.CreateParam("EmailId", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("IsActive", DbType.Boolean, ParameterDirection.Input, usermgtsave_request.IsActive));
                para.Add(db.CreateParam("SearchStock", DbType.Boolean, ParameterDirection.Input, usermgtsave_request.SearchStock));
                para.Add(db.CreateParam("PlaceOrder", DbType.Boolean, ParameterDirection.Input, usermgtsave_request.PlaceOrder));
                para.Add(db.CreateParam("OrderHisrory", DbType.Boolean, ParameterDirection.Input, usermgtsave_request.OrderHisrory));
                para.Add(db.CreateParam("MyCart", DbType.Boolean, ParameterDirection.Input, usermgtsave_request.MyCart));
                para.Add(db.CreateParam("MyWishlist", DbType.Boolean, ParameterDirection.Input, usermgtsave_request.MyWishlist));
                para.Add(db.CreateParam("QuickSearch", DbType.Boolean, ParameterDirection.Input, usermgtsave_request.QuickSearch));

                System.Data.DataTable dt = db.ExecuteSP("UserManagement_CRUD", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Status"].ToString()) == 1)
                {
                    return Ok(new CommonResponse
                    {
                        Message = dt.Rows[0]["Message"].ToString(),
                        Status = "1",
                        Error = ""
                    });
                }
                else if (Convert.ToInt32(dt.Rows[0]["Status"].ToString()) == 0)
                {
                    return Ok(new CommonResponse
                    {
                        Message = dt.Rows[0]["Message"].ToString(),
                        Status = "-1",
                        Error = ""
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Data = new List<CommonResponse>(),
                        Message = "Something Went wrong.\nPlease try again later",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult FortunePartyCode_Exist([FromBody]JObject data)
        {
            FortunePartyCode_Exist_Request fortunepartycode_exist_request = new FortunePartyCode_Exist_Request();
            try
            {
                fortunepartycode_exist_request = JsonConvert.DeserializeObject<FortunePartyCode_Exist_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, fortunepartycode_exist_request.iUserId));
                para.Add(db.CreateParam("FortunePartyCode", DbType.Int32, ParameterDirection.Input, fortunepartycode_exist_request.FortunePartyCode));

                System.Data.DataTable dt = db.ExecuteSP("Get_FortuneCode_Exists", para.ToArray(), false);

                return Ok(new CommonResponse
                {
                    Message = dt.Rows[0]["Message"].ToString(),
                    Status = dt.Rows[0]["Status"].ToString(),
                    Error = ""
                });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult GetCompanyForUserMgt([FromBody]JObject data)
        {
            GetCompanyForUserMgt_Request request = new GetCompanyForUserMgt_Request();
            try
            {
                request = JsonConvert.DeserializeObject<GetCompanyForUserMgt_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<GetCompanyForUserMgt_Request>
                {
                    Data = new List<GetCompanyForUserMgt_Request>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, userID));

                if (!string.IsNullOrEmpty(request.Search))
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, request.Search));
                else
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_UserMgt_Company", para.ToArray(), false);

                List<GetCompanyForUserMgt_Response> response = new List<GetCompanyForUserMgt_Response>();
                response = DataTableExtension.ToList<GetCompanyForUserMgt_Response>(dt);
                if (response.Count > 0)
                {
                    return Ok(new ServiceResponse<GetCompanyForUserMgt_Response>
                    {
                        Data = response,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<GetCompanyForUserMgt_Response>
                    {
                        Data = response,
                        Message = "No Data Found",
                        Status = "-1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<GetCompanyForUserMgt_Response>
                {
                    Data = new List<GetCompanyForUserMgt_Response>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult GetCompanyForHoldStonePlaceOrder([FromBody]JObject data)
        {
            GetCompanyForUserMgt_Request request = new GetCompanyForUserMgt_Request();
            try
            {
                request = JsonConvert.DeserializeObject<GetCompanyForUserMgt_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<GetCompanyForUserMgt_Request>
                {
                    Data = new List<GetCompanyForUserMgt_Request>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, userID));

                DataTable dt = db.ExecuteSP("Get_Company_Hold_PlaceOrder", para.ToArray(), false);

                List<GetCompanyForUserMgt_Response> response = new List<GetCompanyForUserMgt_Response>();
                response = DataTableExtension.ToList<GetCompanyForUserMgt_Response>(dt);
                if (response.Count > 0)
                {
                    return Ok(new ServiceResponse<GetCompanyForUserMgt_Response>
                    {
                        Data = response,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<GetCompanyForUserMgt_Response>
                    {
                        Data = response,
                        Message = "No Data Found",
                        Status = "-1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<GetCompanyForUserMgt_Response>
                {
                    Data = new List<GetCompanyForUserMgt_Response>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult UserDetailGet([FromBody]JObject data)
        {
            UserDetailGet_Req userdetailget_req = new UserDetailGet_Req();
            try
            {
                userdetailget_req = JsonConvert.DeserializeObject<UserDetailGet_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserDetailGet_Req>
                {
                    Data = new List<UserDetailGet_Req>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (!string.IsNullOrEmpty(userdetailget_req.UserId.ToString()))
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, userdetailget_req.UserId));
                else
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("UserDetailGet", para.ToArray(), false);
                List<UserDetailGet_Res> ListResponses = new List<UserDetailGet_Res>();
                ListResponses = DataTableExtension.ToList<UserDetailGet_Res>(dtData);

                return Ok(new ServiceResponse<UserDetailGet_Res>
                {
                    Data = ListResponses,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserDetailGet_Res>
                {
                    Data = new List<UserDetailGet_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_MessageMst([FromBody]JObject data)
        {
            MessageMstSelect_Request req = new MessageMstSelect_Request();
            try
            {
                req = JsonConvert.DeserializeObject<MessageMstSelect_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                if (req.Id > 0)
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, req.Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.iPgNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.iPgNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.iPgSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.iPgSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.sOrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, req.sOrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.IsActive))
                    para.Add(db.CreateParam("IsActive", DbType.Boolean, ParameterDirection.Input, req.IsActive));
                else
                    para.Add(db.CreateParam("IsActive", DbType.Boolean, ParameterDirection.Input, DBNull.Value));


                System.Data.DataTable dtData = db.ExecuteSP("MessageMst_Select", para.ToArray(), false);

                List<MessageMstSelect_Response> ListResponses = new List<MessageMstSelect_Response>();
                ListResponses = DataTableExtension.ToList<MessageMstSelect_Response>(dtData);

                if (ListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<MessageMstSelect_Response>
                    {
                        Data = ListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<MessageMstSelect_Response>
                    {
                        Data = new List<MessageMstSelect_Response>(),
                        Message = "No Record Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<MessageMstSelect_Response>
                {
                    Data = new List<MessageMstSelect_Response>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult MessageMst_Save([FromBody]JObject data)
        {
            MessageMstSave_Request req = new MessageMstSave_Request();
            try
            {
                req = JsonConvert.DeserializeObject<MessageMstSave_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                var db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));

                if (req.Id > 0)
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, req.Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.MessageName))
                    para.Add(db.CreateParam("MessageName", DbType.String, ParameterDirection.Input, req.MessageName));
                else
                    para.Add(db.CreateParam("MessageName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Message))
                    para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, req.Message));
                else
                    para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("IsLogout", DbType.Boolean, ParameterDirection.Input, req.IsLogout));
                para.Add(db.CreateParam("IsActive", DbType.Boolean, ParameterDirection.Input, req.IsActive));
                para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, req.Type));

                DataTable dtData = db.ExecuteSP("MessageMst_Save", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = dtData.Rows[0]["Message"].ToString(),
                        Status = dtData.Rows[0]["Status"].ToString()
                    });
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = "Something Went wrong.\nPlease try again later",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_UserStatusReport([FromBody]JObject data)
        {
            GetUserStatusReport_Request userstatusreportreq = new GetUserStatusReport_Request();

            try
            {
                userstatusreportreq = JsonConvert.DeserializeObject<GetUserStatusReport_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (!string.IsNullOrEmpty(userstatusreportreq.ActivityStatus))
                    para.Add(db.CreateParam("ActivityStatus", DbType.String, ParameterDirection.Input, userstatusreportreq.ActivityStatus));
                else
                    para.Add(db.CreateParam("ActivityStatus", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, userstatusreportreq.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, userstatusreportreq.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.PageNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userstatusreportreq.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.PageSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userstatusreportreq.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.OrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, userstatusreportreq.OrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("UserStatus_Report", para.ToArray(), false);

                List<GetUserStatusReport_Response> ListResponses = new List<GetUserStatusReport_Response>();
                ListResponses = DataTableExtension.ToList<GetUserStatusReport_Response>(dtData);

                if (ListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<GetUserStatusReport_Response>
                    {
                        Data = ListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<GetUserStatusReport_Response>
                    {
                        Data = new List<GetUserStatusReport_Response>(),
                        Message = "No Record Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<GetUserStatusReport_Response>
                {
                    Data = new List<GetUserStatusReport_Response>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Excel_UserStatusReport([FromBody]JObject data)
        {
            GetUserStatusReport_Request userstatusreportreq = new GetUserStatusReport_Request();

            try
            {
                userstatusreportreq = JsonConvert.DeserializeObject<GetUserStatusReport_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (!string.IsNullOrEmpty(userstatusreportreq.ActivityStatus))
                    para.Add(db.CreateParam("ActivityStatus", DbType.String, ParameterDirection.Input, userstatusreportreq.ActivityStatus));
                else
                    para.Add(db.CreateParam("ActivityStatus", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, userstatusreportreq.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, userstatusreportreq.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.PageNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userstatusreportreq.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.PageSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userstatusreportreq.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(userstatusreportreq.OrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, userstatusreportreq.OrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("UserStatus_Report", para.ToArray(), false);

                if (dtData.Rows.Count > 0)
                {
                    string filename = "";
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    filename = "User Status Report " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    EpExcelExport.UserActivityStatusReport(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath, Convert.ToDateTime(userstatusreportreq.FromDate), Convert.ToDateTime(userstatusreportreq.ToDate));

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                {
                    return Ok("No data found.");
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Something Went wrong.\nPlease try again later");
            }
        }
        
        [HttpPost]
        public IHttpActionResult UserDelete()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                var db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));

                DataTable dt = db.ExecuteSP("UserDelete", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "Y")
                {
                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = "Something Went wrong.\nPlease try again later",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                }); 
            }
        }
    }
}
