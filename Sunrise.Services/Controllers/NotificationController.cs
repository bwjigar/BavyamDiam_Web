using DAL;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Notification")]
    public class NotificationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult NotificationList([FromBody]JObject data)
        {
            NotificationGetRequest req = new NotificationGetRequest();
            try
            {
                req = JsonConvert.DeserializeObject<NotificationGetRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<NotificationGetResponse>
                {
                    Data = new List<NotificationGetResponse>(),
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

                if (!string.IsNullOrEmpty(req.TransId))
                    para.Add(db.CreateParam("iTransId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.TransId)));
                else
                    para.Add(db.CreateParam("iTransId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.NotificationName))
                    para.Add(db.CreateParam("sNotificationName", DbType.String, ParameterDirection.Input, req.NotificationName));
                else
                    para.Add(db.CreateParam("sNotificationName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.IsActive))
                    para.Add(db.CreateParam("bIsActive", DbType.Boolean, ParameterDirection.Input, Convert.ToBoolean(req.IsActive)));
                else
                    para.Add(db.CreateParam("bIsActive", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PageNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PageSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("NotificationMas_SelectByPara", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<NotificationGetResponse> list = new List<NotificationGetResponse>();
                    list = DataTableExtension.ToList<NotificationGetResponse>(dt);

                    if (list.Count > 0)
                    {
                        return Ok(new ServiceResponse<NotificationGetResponse>
                        {
                            Data = list,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<NotificationGetResponse>
                        {
                            Data = null,
                            Message = "Something Went wrong.",
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<NotificationGetResponse>
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
                return Ok(new ServiceResponse<NotificationGetResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
    }
}
