using DAL;
using EpExcelExportLib;
using Lib.Constants;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunrise.Services.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Information")]
    public class InformationController : ApiController
    {
        private const String ImagePath = "~/InfoImages/";
        [HttpPost]
        public IHttpActionResult SaveInformation([FromBody]JObject data)
        {
            try
            {
                Information info = new Information();
                info = JsonConvert.DeserializeObject<Information>(data.ToString());

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("InformationID", DbType.Int32, ParameterDirection.Input, info.InformationID));
                para.Add(db.CreateParam("InformationName", DbType.String, ParameterDirection.Input, info.InformationName));
                para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, info.FromDate));
                para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, info.ToDate));
                para.Add(db.CreateParam("OptType", DbType.String, ParameterDirection.Input, info.OptType));
                para.Add(db.CreateParam("IsBeforeLogin", DbType.Boolean, ParameterDirection.Input, info.IsBeforeLogin));
                para.Add(db.CreateParam("NaturalHeight", DbType.String, ParameterDirection.Input, info.NaturalHeight));
                para.Add(db.CreateParam("NaturalWidth", DbType.String, ParameterDirection.Input, info.NaturalWidth));

                DataTable dt = db.ExecuteSP("Information_Crud", para.ToArray(), false);

                int id = 0;
                string msg = "";
                id = (dt.Rows[0]["Id"] != null) ? Convert.ToInt32(dt.Rows[0]["Id"]) : 0;
                msg = (dt.Rows[0]["Msg"] != null) ? Convert.ToString(dt.Rows[0]["Msg"]) : "";
                CommonResponse resp = new CommonResponse();

                if (id > 0)
                {
                    string path = HostingEnvironment.MapPath(ImagePath);
                    if (info.Image != null)
                    {
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        String[] FileList = Directory.GetFiles(path, id + ".*");
                        foreach (String item in FileList)
                        {
                            File.Delete(item);
                        }

                        File.WriteAllBytes(path + id + info.FileExtenstion, info.Image);

                        IDbDataParameter[] paraI = new IDbDataParameter[0];
                        db = new Database(Request);
                        db.ExecuteNonQuery("UPDATE sunrise.Information SET FileName = '"+ id + info.FileExtenstion + "' WHERE InformationID = " + id.ToString(), paraI, false);
                    }

                    if (info.OptType == "Delete")
                    {
                        String[] FileList1 = Directory.GetFiles(path, id + ".*");
                        foreach (String item in FileList1)
                        {
                            File.Delete(item);
                        }
                    }

                    resp.Status = "1";
                    resp.Message = msg;
                    resp.Error = "";
                }
                else
                {
                    resp.Status = "1";
                    resp.Message = msg;
                    resp.Error = "";
                }
                return Ok(resp);
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
        public IHttpActionResult GetInformations([FromBody]JObject data)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                DataTable dt = db.ExecuteSP("Information_Select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Information> response = new List<Information>();
                    response = DataTableExtension.ToList<Information>(dt);
                    if (response.Count > 0)
                    {
                        return Ok(new ServiceResponse<Information>
                        {
                            Data = response,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<Information>
                        {
                            Data = null,
                            Message = "No records found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<Information>
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
                return Ok(new ServiceResponse<Information>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetFutureInformations([FromBody]JObject data)
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, userID));

                DataTable dt = db.ExecuteSP("Information_Future_Select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Information> response = new List<Information>();
                    response = DataTableExtension.ToList<Information>(dt);
                    if (response.Count > 0)
                    {
                        return Ok(new ServiceResponse<Information>
                        {
                            Data = response,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<Information>
                        {
                            Data = null,
                            Message = "No records found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<Information>
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
                return Ok(new ServiceResponse<Information>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        public IHttpActionResult Login_Information_Get([FromBody]JObject data)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = db.ExecuteSP("Login_Information_Get", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Information> response = new List<Information>();
                    response = DataTableExtension.ToList<Information>(dt);
                    if (response.Count > 0)
                    {
                        return Ok(new ServiceResponse<Information>
                        {
                            Data = response,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<Information>
                        {
                            Data = null,
                            Message = "No records found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<Information>
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
                return Ok(new ServiceResponse<Information>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult SaveEventAction([FromBody]JObject data)
        {
            try
            {
                EventActionRequest info = new EventActionRequest();
                info = JsonConvert.DeserializeObject<EventActionRequest>(data.ToString());

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("InformationID", DbType.Int32, ParameterDirection.Input, info.InformationID));
                para.Add(db.CreateParam("Action", DbType.String, ParameterDirection.Input, info.Action));
                para.Add(db.CreateParam("CustomerID", DbType.String, ParameterDirection.Input, userID));

                DataTable dt = db.ExecuteSP("CustInformation_Crud", para.ToArray(), false);

                string msg = "";
                msg = (dt.Rows[0]["Msg"] != null) ? Convert.ToString(dt.Rows[0]["Msg"]) : "";

                CommonResponse resp = new CommonResponse();
                if (msg.Contains("successfully"))
                {
                    resp.Status = "1";
                }
                else
                {
                    resp.Status = "0";
                }

                resp.Message = msg;
                resp.Error = "";

                return Ok(resp);
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
        public IHttpActionResult GetCustInformations([FromBody]JObject data)
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, userID));
                DataTable dt = db.ExecuteSP("CustInformation_Select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<CustInformation> response = new List<CustInformation>();
                    response = DataTableExtension.ToList<CustInformation>(dt);
                    if (response.Count > 0)
                    {
                        return Ok(new ServiceResponse<CustInformation>
                        {
                            Data = response,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<Information>
                        {
                            Data = null,
                            Message = "No records found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<Information>
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
                return Ok(new ServiceResponse<CustInformation>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
    }
}
