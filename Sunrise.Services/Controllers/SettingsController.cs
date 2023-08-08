using DAL;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunrise.Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Xml.Serialization;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Settings")]
    public class SettingsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetColumnsSettings([FromBody]JObject data)
        {
            ColumnsRequest columnsrequest = new ColumnsRequest();
            try
            {
                columnsrequest = JsonConvert.DeserializeObject<ColumnsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }
           
            try
            {
                if ((columnsrequest.UserId.ToString() != "" && columnsrequest.UserId != null ? columnsrequest.UserId : 0) == 0)
                {
                    columnsrequest.UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                }

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (columnsrequest.UserId > 0)
                    para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(columnsrequest.UserId)));
                else
                    para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ColumnsSettings_Select_Sunrise", para.ToArray(), false);

                List<ColumnsSettingsResponse> columnsSettingsResponse = new List<ColumnsSettingsResponse>();
                columnsSettingsResponse = DataTableExtension.ToList<ColumnsSettingsResponse>(dt);
                if (columnsSettingsResponse.Count > 0)
                {
                  return Ok(new ServiceResponse<ColumnsSettingsResponse>
                    {
                        Data = columnsSettingsResponse,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ColumnsSettingsResponse>
                    {
                        Data = columnsSettingsResponse,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ColumnsSettingsResponse>
                {
                    Data = new List<ColumnsSettingsResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult SaveColumnsSettings([FromBody]JObject data)
        {
          ColumnsSettingsRequest columnsSettingsRequest = new ColumnsSettingsRequest();

            try
            {
                columnsSettingsRequest = JsonConvert.DeserializeObject<ColumnsSettingsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                if ((columnsSettingsRequest.Userid.ToString() != "" && columnsSettingsRequest.Userid != null ? columnsSettingsRequest.Userid : 0) == 0)
                {
                    columnsSettingsRequest.Userid = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                }
                
                string columnsSettingsValue = IPadCommon.ToXML<List<ColumnsSettings>>(columnsSettingsRequest.ColumnsSettings);
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iParaType", System.Data.DbType.String, System.Data.ParameterDirection.Input, "User"));

                if (columnsSettingsRequest.Userid > 0)
                    para.Add(db.CreateParam("iParaValue", DbType.String, ParameterDirection.Input, Convert.ToInt64(columnsSettingsRequest.Userid)));
                else
                    para.Add(db.CreateParam("iParaValue", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("isDefault", DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("columnsDefs", DbType.String, ParameterDirection.Input, columnsSettingsValue));


                DataTable dt = db.ExecuteSP("ColumnsSettings_Ins_Sunrise", para.ToArray(), false);

                //List<ColumnsSettingsResponse> columnsSettingsResponse = new List<ColumnsSettingsResponse>();
                //columnsSettingsResponse = DataTableExtension.ToList<ColumnsSettingsResponse>(dt);
                if (dt.Rows.Count > 0)
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Message = "Something Went wrong.",
                        Status = "0"
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
                    Error = ex.StackTrace
                });
            }
        }
        [HttpPost]
        public IHttpActionResult GetUserMas([FromBody]JObject data)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                DataTable dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<ColumnsUserResponse> list = new List<ColumnsUserResponse>();
                    list = DataTableExtension.ToList<ColumnsUserResponse>(dt);

                    return Ok(new ServiceResponse<ColumnsUserResponse>
                    {
                        Data = list,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ColumnsUserResponse>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ServiceResponse<ColumnsUserResponse>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
    }
}
