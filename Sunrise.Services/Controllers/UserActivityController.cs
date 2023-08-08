using DAL;
using EpExcelExportLib;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Hosting;
using System.Web.Http;


namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/UserActivity")]
    public class UserActivityController : ApiController
    {
        [HttpPost]
        public IHttpActionResult UserActivityDetail([FromBody]JObject data)
        {
            UserActivityUpdateRequest req = new UserActivityUpdateRequest();
            try
            {
                req = JsonConvert.DeserializeObject<UserActivityUpdateRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserActivityUpdateResponse>
                {
                    Data = new List<UserActivityUpdateResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(req.FromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.Date, ParameterDirection.Input, Convert.ToDateTime(req.FromDate)));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.Date, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToDate))
                    para.Add(db.CreateParam("dtToDate", DbType.Date, ParameterDirection.Input, Convert.ToDateTime(req.ToDate)));
                else
                    para.Add(db.CreateParam("dtToDate", DbType.Date, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserID))
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.UserID)));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.EmpID))
                    para.Add(db.CreateParam("iEmpId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.EmpID)));
                else
                    para.Add(db.CreateParam("iEmpId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserFullName))
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, req.UserFullName));
                else
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CountryName))
                    para.Add(db.CreateParam("sCountryName", DbType.String, ParameterDirection.Input, req.CountryName));
                else
                    para.Add(db.CreateParam("sCountryName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserName))
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CompanyName))
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, req.CompanyName));
                else
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PageNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PageSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("iOrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                else
                    para.Add(db.CreateParam("iOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ComUserName))
                    para.Add(db.CreateParam("ComUserName", DbType.String, ParameterDirection.Input, req.ComUserName));
                else
                    para.Add(db.CreateParam("ComUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("UserActivityDet_Select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<UserActivityUpdateResponse> list = new List<UserActivityUpdateResponse>();
                    list = DataTableExtension.ToList<UserActivityUpdateResponse>(dt);

                    if (list.Count > 0)
                    {
                        return Ok(new ServiceResponse<UserActivityUpdateResponse>
                        {
                            Data = list,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<UserActivityUpdateResponse>
                        {
                            Data = null,
                            Message = "Something Went wrong.",
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<UserActivityUpdateResponse>
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
                return Ok(new ServiceResponse<UserActivityUpdateResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult DownloadActivityUpdateList([FromBody]JObject data)
        {
            UserActivityUpdateRequest req = new UserActivityUpdateRequest();
            try
            {
                req = JsonConvert.DeserializeObject<UserActivityUpdateRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0",
                    Error = ""
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(req.FromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.Date, ParameterDirection.Input, Convert.ToDateTime(req.FromDate)));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.Date, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToDate))
                    para.Add(db.CreateParam("dtToDate", DbType.Date, ParameterDirection.Input, Convert.ToDateTime(req.ToDate)));
                else
                    para.Add(db.CreateParam("dtToDate", DbType.Date, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserID))
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.UserID)));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.EmpID))
                    para.Add(db.CreateParam("iEmpId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.EmpID)));
                else
                    para.Add(db.CreateParam("iEmpId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserFullName))
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, req.UserFullName));
                else
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CountryName))
                    para.Add(db.CreateParam("sCountryName", DbType.String, ParameterDirection.Input, req.CountryName));
                else
                    para.Add(db.CreateParam("sCountryName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserName))
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CompanyName))
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, req.CompanyName));
                else
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("iOrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                else
                    para.Add(db.CreateParam("iOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ComUserName))
                    para.Add(db.CreateParam("ComUserName", DbType.String, ParameterDirection.Input, req.ComUserName));
                else
                    para.Add(db.CreateParam("ComUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("UserActivityDet_Select", para.ToArray(), false);

                if (dt.Rows.Count > 0)
                {
                    string filename = "";
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    filename = "UserActivity" + DAL.Common.GetHKTime().ToString("dd.MM.yyyy");
                    EpExcelExport.CreateUserActivity(dt.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);

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
