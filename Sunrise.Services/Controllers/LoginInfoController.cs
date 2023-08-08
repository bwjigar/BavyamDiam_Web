using DAL;
using EpExcelExportLib;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Hosting;
using System.Web.Http;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/LoginInfo")]
    public class LoginInfoController : ApiController
    {
        [HttpPost]
        public IHttpActionResult LoginInfoDetail([FromBody]JObject data)
        {
            UserListSearchRequest req = new UserListSearchRequest();
            try
            {
                req = JsonConvert.DeserializeObject<UserListSearchRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<LoginInfoResponse>
                {
                    Data = new List<LoginInfoResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (req.FromDate != null)
                    para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, Convert.ToDateTime(req.FromDate)));
                else
                    para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.ToDate != null)
                    para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, Convert.ToDateTime(req.ToDate)));
                else
                    para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.CountryName != null)
                    para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.CountryName));
                else
                    para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.UserFullName != null)
                    para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.UserFullName));
                else
                    para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.UserName != null)
                    para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.CompanyName != null)
                    para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.CompanyName));
                else
                    para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.PageNo != null)
                    para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(req.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.PageSize != null)
                    para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(req.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

                if (string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("OrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.OrderBy));

                DataTable dt = db.ExecuteSP("LoginLog_SelectByUserId", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<LoginInfoResponse> list = new List<LoginInfoResponse>();
                    list = DataTableExtension.ToList<LoginInfoResponse>(dt);

                    if (list.Count > 0)
                    {
                        return Ok(new ServiceResponse<LoginInfoResponse>
                        {
                            Data = list,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<LoginInfoResponse>
                        {
                            Data = null,
                            Message = "Something Went wrong.",
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<LoginInfoResponse>
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
                return Ok(new ServiceResponse<LoginInfoResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult DownloadLoginInfo([FromBody]JObject data)
        {
            UserListSearchRequest req = new UserListSearchRequest();
            try
            {
                req = JsonConvert.DeserializeObject<UserListSearchRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<LoginInfoResponse>
                {
                    Data = new List<LoginInfoResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (req.FromDate != null)
                    para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, Convert.ToDateTime(req.FromDate)));
                else
                    para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.ToDate != null)
                    para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, Convert.ToDateTime(req.ToDate)));
                else
                    para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, userID));

                if (req.CountryName != null)
                    para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.CountryName));
                else
                    para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.UserFullName != null)
                    para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.UserFullName));
                else
                    para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.UserName != null)
                    para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.CompanyName != null)
                    para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.CompanyName));
                else
                    para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.PageNo != null)
                    para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(req.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

                if (req.PageSize != null)
                    para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(req.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, req.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, req.ActivityType));

                DataTable dt = db.ExecuteSP("LoginLog_SelectByUserId", para.ToArray(), false);

                if (dt.Rows.Count > 0)
                {
                    string filename = "";
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    filename = "LoginInfo" + DAL.Common.GetHKTime().ToString("dd.MM.yyyy");
                    EpExcelExport.CreateLoginInfo(dt.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);

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
                        Status = "1",
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
    }
}
