using DAL;
using EpExcelExportLib;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Renci.SshNet;
using Sunrise.Services.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Script.Serialization;


namespace Sunrise.Services.Controllers
{
    public class ApiSettingsController : ApiController
    {
        public static string sImages = "";
        public static string sHdmovie = "";
        public static string strLab = "";
        public static string strLabLink = "";

        public static Int32 iTransid;
        public static Int32 iCalculateDiscount;
        public static UInt32 iFormulaApply;

        public static string CutOriginalCol = "";
        public static string DiscOriginalCol = "";
        public static string NetPriceOriginalCol = "";
        public static string RapAmountOriginalCol = "";

        public static string CtsOriginalCol = "";
        public static string RapPriceOriginalCol = "";

        public static string sDNALink = "";
        public static string sOtherImageLink = "";
        public static string sOtherVideoLink = "";
        public static string sOtherDnaLink = "";

        public static UInt32 DiscNormalStyleindex1;
        public static UInt32 CutNormalStyleindex1;

        public static int Log_TransId;
        public static int Log_UserId;
        public static int TotCount = 0;
        public static Boolean Status;
        public static string Error;

        [HttpPost]
        public IHttpActionResult GetApiViews([FromBody]JObject data)
        {
            ApiFilterRequest getApiRequest = new ApiFilterRequest();

            try
            {
                getApiRequest = JsonConvert.DeserializeObject<ApiFilterRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiFilterResponse>
                {
                    Data = new List<ApiFilterResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                long? iTransId = null, iPgNo = null, iPgSize = null, iUserId = null, iEmpId = null;
                DateTime? FrmDate = null;
                DateTime? ToDate = null;
                if (!string.IsNullOrEmpty(getApiRequest.sTransId))
                    iTransId = Convert.ToInt64(getApiRequest.sTransId);

                if (!string.IsNullOrEmpty(getApiRequest.sPgNo))
                    iPgNo = Convert.ToInt64(getApiRequest.sPgNo);

                if (!string.IsNullOrEmpty(getApiRequest.sPgSize))
                    iPgSize = Convert.ToInt64(getApiRequest.sPgSize);

                if (!string.IsNullOrEmpty(getApiRequest.sUserId))
                    iUserId = Convert.ToInt64(getApiRequest.sUserId);

                if (!string.IsNullOrEmpty(getApiRequest.sEmpId))
                    iEmpId = Convert.ToInt64(getApiRequest.sEmpId);

                System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                if (!String.IsNullOrEmpty(getApiRequest.dtFromDate))
                    FrmDate = Convert.ToDateTime(getApiRequest.dtFromDate, enGB);
                if (!String.IsNullOrEmpty(getApiRequest.dtToDate))
                    ToDate = Convert.ToDateTime(getApiRequest.dtToDate, enGB);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (iTransId != null)
                    para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, iTransId));
                else
                    para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (FrmDate != null)
                    para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, FrmDate));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                if (ToDate != null)
                    para.Add(db.CreateParam("dtToDate", DbType.DateTime, ParameterDirection.Input, ToDate));
                else
                    para.Add(db.CreateParam("dtToDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                if (iUserId != null)
                    para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, iUserId));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iEmpId != null)
                    para.Add(db.CreateParam("iEmpId", DbType.Int64, ParameterDirection.Input, iEmpId));
                else
                    para.Add(db.CreateParam("iEmpId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(getApiRequest.sFullName))
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, getApiRequest.sFullName));
                else
                    para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(getApiRequest.sUserName))
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, getApiRequest.sUserName));
                else
                    para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(getApiRequest.sCompName))
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, getApiRequest.sCompName));
                else
                    para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(getApiRequest.sCountryName))
                    para.Add(db.CreateParam("sCountryName", DbType.String, ParameterDirection.Input, getApiRequest.sCountryName));
                else
                    para.Add(db.CreateParam("sCountryName", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (iPgNo != null)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgSize != null)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("ApiMas_Select", para.ToArray(), false);

                dt.Columns.Add("dTransDate1", typeof(string));
                dt.Columns.Add("MailLastUploadTime1", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow drchange = dt.Rows[i];

                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["dTransDate"])))
                    {
                        try
                        {
                            DateTime _dttime = Convert.ToDateTime(dt.Rows[i]["dTransDate"]);
                            drchange["dTransDate1"] = _dttime.ToString("dd/MM/yyyy HH:MM tt");
                            dt.AcceptChanges();
                        }
                        catch { }
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["MailLastUploadTime"])))
                    {
                        try
                        {
                            DateTime _dttime = Convert.ToDateTime(dt.Rows[i]["MailLastUploadTime"]);
                            drchange["MailLastUploadTime1"] = _dttime.ToString("dd/MM/yyyy HH:MM tt");
                            dt.AcceptChanges();
                        }
                        catch { }
                    }
                }

                List<ApiFilterResponse> apiResponses = new List<ApiFilterResponse>();
                apiResponses = DataTableExtension.ToList<ApiFilterResponse>(dt);
                if (apiResponses.Count > 0)
                {
                    List<ApiColumnsSettings> columnsSettings;
                    foreach (var apiDetails in apiResponses)
                    {
                        db = new Database();
                        para = new List<IDbDataParameter>();
                        para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(iTransId)));
                        dt = db.ExecuteSP("ApiDet_Select", para.ToArray(), false);

                        if (dt.Rows.Count > 0)
                        {
                            columnsSettings = new List<ApiColumnsSettings>();
                            columnsSettings = DataTableExtension.ToList<ApiColumnsSettings>(dt);
                            apiDetails.ColumnsSettings = columnsSettings;
                        }
                    }

                    return Ok(new ServiceResponse<ApiFilterResponse>
                    {
                        Data = apiResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ApiFilterResponse>
                    {
                        Data = apiResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiFilterResponse>
                {
                    Data = new List<ApiFilterResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetApiDetails([FromBody]JObject data)
        {
            //pass sTransId only for api details
            ApiFilterRequest getApiRequest = new ApiFilterRequest();

            try
            {
                getApiRequest = JsonConvert.DeserializeObject<ApiFilterRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiFilterResponse>
                {
                    Data = new List<ApiFilterResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                long? iTransId = null;
                if (!string.IsNullOrEmpty(getApiRequest.sTransId))
                    iTransId = Convert.ToInt64(getApiRequest.sTransId);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (iTransId != null)
                    para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, iTransId));
                else
                    para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtToDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("iEmpId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("ApiMas_Select", para.ToArray(), false);

                dt.Columns.Add("dTransDate1", typeof(string));
                dt.Columns.Add("MailLastUploadTime1", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow drchange = dt.Rows[i];

                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["dTransDate"])))
                    {
                        try
                        {
                            DateTime _dttime = Convert.ToDateTime(dt.Rows[i]["dTransDate"]);
                            drchange["dTransDate1"] = _dttime.ToString("dd/MM/yyyy HH:MM tt");
                            dt.AcceptChanges();
                        }
                        catch { }
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["MailLastUploadTime"])))
                    {
                        try
                        {
                            DateTime _dttime = Convert.ToDateTime(dt.Rows[i]["MailLastUploadTime"]);
                            drchange["MailLastUploadTime1"] = _dttime.ToString("dd/MM/yyyy HH:MM tt");
                            dt.AcceptChanges();
                        }
                        catch { }
                    }
                }

                List<ApiDetails> apiResponses = new List<ApiDetails>();
                apiResponses = DataTableExtension.ToList<ApiDetails>(dt);
                if (apiResponses.Count > 0)
                {
                    List<ApiColumnsSettings> columnsSettings;
                    foreach (var apiDetails in apiResponses)
                    {
                        db = new Database();
                        para = new List<IDbDataParameter>();
                        para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(iTransId)));
                        dt = db.ExecuteSP("ApiDet_Select", para.ToArray(), false);

                        if (dt.Rows.Count > 0)
                        {
                            columnsSettings = new List<ApiColumnsSettings>();
                            columnsSettings = DataTableExtension.ToList<ApiColumnsSettings>(dt);
                            apiDetails.ColumnsSettings = columnsSettings;
                        }
                    }

                    return Ok(new ServiceResponse<ApiDetails>
                    {
                        Data = apiResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ApiDetails>
                    {
                        Data = apiResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiFilterResponse>
                {
                    Data = new List<ApiFilterResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult GetApiColumnsDetails()
        {

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = db.ExecuteSP("Api_Column_Select", para.ToArray(), false);

                List<ApiColumns> apiCols = new List<ApiColumns>();
                apiCols = DataTableExtension.ToList<ApiColumns>(dt);
                if (apiCols.Count > 0)
                {

                    return Ok(new ServiceResponse<ApiColumns>
                    {
                        Data = apiCols,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ApiColumns>
                    {
                        Data = apiCols,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiColumns>
                {
                    Data = new List<ApiColumns>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult DeleteApi([FromBody]JObject data)
        {
            //pass sTransId only for api details
            ApiFilterRequest getApiRequest = new ApiFilterRequest();

            try
            {
                getApiRequest = JsonConvert.DeserializeObject<ApiFilterRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiFilterResponse>
                {
                    Data = new List<ApiFilterResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                long? iTransId = null;
                if (!string.IsNullOrEmpty(getApiRequest.sTransId))
                    iTransId = Convert.ToInt64(getApiRequest.sTransId);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (iTransId != null)
                    para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, iTransId));
                else
                    para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("Api_delete_sunrise", para.ToArray(), false);

                if (dt == null)
                {
                    resp.Status = "0";
                    resp.Message = "Api not found.";
                    resp.Error = "";
                    return Ok(resp);
                }
                else
                {
                    if (dt.Rows[0]["STATUS"].ToString() == "Y")
                    {
                        resp.Status = "1";
                        resp.Message = "Api deleted successfully.";
                        resp.Error = "";
                        return Ok(resp);
                    }
                    else
                    {
                        resp.Status = "0";
                        resp.Message = "Api not deleted.";
                        resp.Error = "";
                        return Ok(resp);
                    }
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
        public IHttpActionResult SaveApiFilter([FromBody]JObject data)
        {
            // Api_Insert_sunrise
            ApiDetails apiDetails = new ApiDetails();

            try
            {
                apiDetails = JsonConvert.DeserializeObject<ApiDetails>(data.ToString());
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
                DataTable _dt = ApiviewAll(1, 1000);
                DataView _dv = new DataView(_dt);

                _dv.RowFilter = "sApiName = '" + apiDetails.sApiName.Trim() + "'";
                if (apiDetails.OperationType.ToUpper() == "U")
                    _dv.RowFilter += " AND iTransId <> " + apiDetails.iTransId;

                _dt = _dv.ToTable();
                if (_dt.Rows.Count > 0)
                {
                    resp.Status = "0";
                    resp.Message = "Api Name Same " + apiDetails.sApiName.Trim();
                    resp.Error = "";
                    return Ok(resp);
                }

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);


                CultureInfo enGB = new CultureInfo("en-GB");
                DateTime trDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now), enGB);
                Single? dFromcts1 = null, dToCts1 = null, sFromDisc1 = null, dToDisc1 = null, dFromLength1 = null, dToLength1 = null, dFromWidth1 = null, dToWidth1 = null;
                Single? dFromDepth1 = null, dToDepth1 = null, dFromDepthPer1 = null, dToDepthPer1 = null, dFromTablePer1 = null, dToTablePer1 = null, dFromCrAng1 = null, dToCrAng1 = null;
                Single? dFromCrHt1 = null, dToCrHt1 = null, dFromPavAng1 = null, dToPavAng1 = null, dFromPavHt1 = null, dToPavHt1 = null;
                long? iTransId = null, iFtpUploadTime1 = null, iLocation1 = null;
                if (!string.IsNullOrEmpty(apiDetails.dFromcts))
                    dFromcts1 = Convert.ToSingle(apiDetails.dFromcts);

                if (!string.IsNullOrEmpty(apiDetails.dToCts))
                    dToCts1 = Convert.ToSingle(apiDetails.dToCts);

                if (!string.IsNullOrEmpty(apiDetails.dToDisc))
                    sFromDisc1 = Convert.ToSingle(apiDetails.dToDisc);

                if (!string.IsNullOrEmpty(apiDetails.dToCts))
                    dToDisc1 = Convert.ToSingle(apiDetails.dToCts);

                if (!string.IsNullOrEmpty(apiDetails.dFromLength.ToString()))
                    dFromLength1 = Convert.ToSingle(apiDetails.dFromLength);

                if (!string.IsNullOrEmpty(apiDetails.dToLength.ToString()))
                    dToLength1 = Convert.ToSingle(apiDetails.dToLength);

                if (!string.IsNullOrEmpty(apiDetails.dFromWidth.ToString()))
                    dFromWidth1 = Convert.ToSingle(apiDetails.dFromWidth);

                if (!string.IsNullOrEmpty(apiDetails.dToWidth.ToString()))
                    dToWidth1 = Convert.ToSingle(apiDetails.dToWidth);

                if (!string.IsNullOrEmpty(apiDetails.dFromDepth.ToString()))
                    dFromDepth1 = Convert.ToSingle(apiDetails.dFromDepth);

                if (!string.IsNullOrEmpty(apiDetails.dToDepth.ToString()))
                    dToDepth1 = Convert.ToSingle(apiDetails.dToDepth);

                if (!string.IsNullOrEmpty(apiDetails.dFromDepthPer.ToString()))
                    dFromDepthPer1 = Convert.ToSingle(apiDetails.dFromDepthPer);

                if (!string.IsNullOrEmpty(apiDetails.dToDepthPer.ToString()))
                    dToDepthPer1 = Convert.ToSingle(apiDetails.dToDepthPer);

                if (!string.IsNullOrEmpty(apiDetails.dFromTablePer.ToString()))
                    dFromTablePer1 = Convert.ToSingle(apiDetails.dFromTablePer);

                if (!string.IsNullOrEmpty(apiDetails.dToTablePer.ToString()))
                    dToTablePer1 = Convert.ToSingle(apiDetails.dToTablePer);

                if (!string.IsNullOrEmpty(apiDetails.dFromCrAng.ToString()))
                    dFromCrAng1 = Convert.ToSingle(apiDetails.dFromCrAng);

                if (!string.IsNullOrEmpty(apiDetails.dToCrAng.ToString()))
                    dToCrAng1 = Convert.ToSingle(apiDetails.dToCrAng);

                if (!string.IsNullOrEmpty(apiDetails.dFromCrHt.ToString()))
                    dFromCrHt1 = Convert.ToSingle(apiDetails.dFromCrHt);

                if (!string.IsNullOrEmpty(apiDetails.dToCrHt.ToString()))
                    dToCrHt1 = Convert.ToSingle(apiDetails.dToCrHt);

                if (!string.IsNullOrEmpty(apiDetails.dFromPavAng.ToString()))
                    dFromPavAng1 = Convert.ToSingle(apiDetails.dFromPavAng);

                if (!string.IsNullOrEmpty(apiDetails.dToPavAng.ToString()))
                    dToPavAng1 = Convert.ToSingle(apiDetails.dToPavAng);

                if (!string.IsNullOrEmpty(apiDetails.dFromPavHt.ToString()))
                    dFromPavHt1 = Convert.ToSingle(apiDetails.dFromPavHt);

                if (!string.IsNullOrEmpty(apiDetails.dToPavHt.ToString()))
                    dToPavHt1 = Convert.ToSingle(apiDetails.dToPavHt);

                if (!string.IsNullOrEmpty(apiDetails.iFtpUploadTime))
                    iFtpUploadTime1 = Convert.ToInt64(apiDetails.iFtpUploadTime);


                if (apiDetails.iLocation != null)
                    iLocation1 = Convert.ToInt64(apiDetails.iLocation);

                var db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));
                para.Add(db.CreateParam("dTransDate", DbType.DateTime, ParameterDirection.Input, trDate));

                if (!string.IsNullOrEmpty(apiDetails.sShape))
                    para.Add(db.CreateParam("sShape", DbType.String, ParameterDirection.Input, apiDetails.sShape));
                else
                    para.Add(db.CreateParam("sShape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sColor))
                    para.Add(db.CreateParam("sColor", DbType.String, ParameterDirection.Input, apiDetails.sColor));
                else
                    para.Add(db.CreateParam("sColor", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sClarity))
                    para.Add(db.CreateParam("sClarity", DbType.String, ParameterDirection.Input, apiDetails.sClarity));
                else
                    para.Add(db.CreateParam("sClarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sCut))
                    para.Add(db.CreateParam("sCut", DbType.String, ParameterDirection.Input, apiDetails.sCut));
                else
                    para.Add(db.CreateParam("sCut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sPolish))
                    para.Add(db.CreateParam("sPolish", DbType.String, ParameterDirection.Input, apiDetails.sPolish));
                else
                    para.Add(db.CreateParam("sPolish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sSymm))
                    para.Add(db.CreateParam("sSymm", DbType.String, ParameterDirection.Input, apiDetails.sSymm));
                else
                    para.Add(db.CreateParam("sSymm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sFls))
                    para.Add(db.CreateParam("sFls", DbType.String, ParameterDirection.Input, apiDetails.sFls));
                else
                    para.Add(db.CreateParam("sFls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sLab))
                    para.Add(db.CreateParam("sLab", DbType.String, ParameterDirection.Input, apiDetails.sLab));
                else
                    para.Add(db.CreateParam("sLab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (dFromcts1 != null)
                    para.Add(db.CreateParam("dFromcts", DbType.Single, ParameterDirection.Input, dFromcts1));
                else
                    para.Add(db.CreateParam("dFromcts", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToCts1 != null)
                    para.Add(db.CreateParam("dToCts", DbType.Single, ParameterDirection.Input, dToCts1));
                else
                    para.Add(db.CreateParam("dToCts", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (sFromDisc1 != null)
                    para.Add(db.CreateParam("dFromDisc", DbType.Single, ParameterDirection.Input, sFromDisc1));
                else
                    para.Add(db.CreateParam("dFromDisc", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToDisc1 != null)
                    para.Add(db.CreateParam("dToDisc", DbType.Single, ParameterDirection.Input, dToDisc1));
                else
                    para.Add(db.CreateParam("dToDisc", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sPointer))
                    para.Add(db.CreateParam("sPointer", DbType.String, ParameterDirection.Input, apiDetails.sPointer));
                else
                    para.Add(db.CreateParam("sPointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (dFromLength1 != null)
                    para.Add(db.CreateParam("dFromLength", DbType.Single, ParameterDirection.Input, dFromLength1));
                else
                    para.Add(db.CreateParam("dFromLength", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToLength1 != null)
                    para.Add(db.CreateParam("dToLength", DbType.Single, ParameterDirection.Input, dToLength1));
                else
                    para.Add(db.CreateParam("dToLength", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dFromWidth1 != null)
                    para.Add(db.CreateParam("dFromWidth", DbType.Single, ParameterDirection.Input, dFromWidth1));
                else
                    para.Add(db.CreateParam("dFromWidth", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToWidth1 != null)
                    para.Add(db.CreateParam("dToWidth", DbType.Single, ParameterDirection.Input, dToWidth1));
                else
                    para.Add(db.CreateParam("dToWidth", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dFromDepth1 != null)
                    para.Add(db.CreateParam("dFromDepth", DbType.Single, ParameterDirection.Input, dFromDepth1));
                else
                    para.Add(db.CreateParam("dFromDepth", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToDepth1 != null)
                    para.Add(db.CreateParam("dToDepth", DbType.Single, ParameterDirection.Input, dToDepth1));
                else
                    para.Add(db.CreateParam("dToDepth", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dFromDepthPer1 != null)
                    para.Add(db.CreateParam("dFromDepthPer", DbType.Single, ParameterDirection.Input, dFromDepthPer1));
                else
                    para.Add(db.CreateParam("dFromDepthPer", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToDepthPer1 != null)
                    para.Add(db.CreateParam("dToDepthPer", DbType.Single, ParameterDirection.Input, dToDepthPer1));
                else
                    para.Add(db.CreateParam("dToDepthPer", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dFromTablePer1 != null)
                    para.Add(db.CreateParam("dFromTablePer", DbType.Single, ParameterDirection.Input, dFromTablePer1));
                else
                    para.Add(db.CreateParam("dFromTablePer", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToTablePer1 != null)
                    para.Add(db.CreateParam("dToTablePer", DbType.Single, ParameterDirection.Input, dToTablePer1));
                else
                    para.Add(db.CreateParam("dToTablePer", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dFromCrAng1 != null)
                    para.Add(db.CreateParam("dFromCrAng", DbType.Single, ParameterDirection.Input, dFromCrAng1));
                else
                    para.Add(db.CreateParam("dFromCrAng", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToCrAng1 != null)
                    para.Add(db.CreateParam("dToCrAng", DbType.Single, ParameterDirection.Input, dToCrAng1));
                else
                    para.Add(db.CreateParam("dToCrAng", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dFromCrHt1 != null)
                    para.Add(db.CreateParam("dFromCrHt", DbType.Single, ParameterDirection.Input, dFromCrHt1));
                else
                    para.Add(db.CreateParam("dFromCrHt", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToCrHt1 != null)
                    para.Add(db.CreateParam("dToCrHt", DbType.Single, ParameterDirection.Input, dToCrHt1));
                else
                    para.Add(db.CreateParam("dToCrHt", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dFromPavAng1 != null)
                    para.Add(db.CreateParam("dFromPavAng", DbType.Single, ParameterDirection.Input, dFromPavAng1));
                else
                    para.Add(db.CreateParam("dFromPavAng", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToPavAng1 != null)
                    para.Add(db.CreateParam("dToPavAng", DbType.Single, ParameterDirection.Input, dToPavAng1));
                else
                    para.Add(db.CreateParam("dToPavAng", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dFromPavHt1 != null)
                    para.Add(db.CreateParam("dFromPavHt", DbType.Single, ParameterDirection.Input, dFromPavHt1));
                else
                    para.Add(db.CreateParam("dFromPavHt", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (dToPavHt1 != null)
                    para.Add(db.CreateParam("dToPavHt", DbType.Single, ParameterDirection.Input, dToPavHt1));
                else
                    para.Add(db.CreateParam("dToPavHt", DbType.Single, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sShade))
                    para.Add(db.CreateParam("sShade", DbType.String, ParameterDirection.Input, apiDetails.sShade));
                else
                    para.Add(db.CreateParam("sShade", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sInclusion))
                    para.Add(db.CreateParam("sInclusion", DbType.String, ParameterDirection.Input, apiDetails.sInclusion));
                else
                    para.Add(db.CreateParam("sInclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sTableNatts))
                    para.Add(db.CreateParam("sTableNatts", DbType.String, ParameterDirection.Input, apiDetails.sTableNatts));
                else
                    para.Add(db.CreateParam("sTableNatts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sApiName))
                    para.Add(db.CreateParam("sApiName", DbType.String, ParameterDirection.Input, apiDetails.sApiName));
                else
                    para.Add(db.CreateParam("sApiName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sExpType))
                    para.Add(db.CreateParam("sExpType", DbType.String, ParameterDirection.Input, apiDetails.sExpType));
                else
                    para.Add(db.CreateParam("sExpType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sCrownNatts))
                    para.Add(db.CreateParam("sCrownNatts", DbType.String, ParameterDirection.Input, apiDetails.sCrownNatts));
                else
                    para.Add(db.CreateParam("sCrownNatts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sCrownInclusion))
                    para.Add(db.CreateParam("sCrownInclusion", DbType.String, ParameterDirection.Input, apiDetails.sCrownInclusion));
                else
                    para.Add(db.CreateParam("sCrownInclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sFtpName))
                    para.Add(db.CreateParam("sFtpName", DbType.String, ParameterDirection.Input, apiDetails.sFtpName));
                else
                    para.Add(db.CreateParam("sFtpName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sftpUser))
                    para.Add(db.CreateParam("sftpUser", DbType.String, ParameterDirection.Input, apiDetails.sftpUser));
                else
                    para.Add(db.CreateParam("sftpUser", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sftpPass))
                    para.Add(db.CreateParam("sftpPass", DbType.String, ParameterDirection.Input, apiDetails.sftpPass));
                else
                    para.Add(db.CreateParam("sftpPass", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (iFtpUploadTime1 != null)
                    para.Add(db.CreateParam("iFtpUploadTime", DbType.Int64, ParameterDirection.Input, iFtpUploadTime1));
                else
                    para.Add(db.CreateParam("iFtpUploadTime", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sSeprator))
                    para.Add(db.CreateParam("sSeprator", DbType.String, ParameterDirection.Input, apiDetails.sSeprator));
                else
                    para.Add(db.CreateParam("sSeprator", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sRepeat))
                    para.Add(db.CreateParam("sRepeat", DbType.String, ParameterDirection.Input, apiDetails.sRepeat));
                else
                    para.Add(db.CreateParam("sRepeat", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sEmail))
                    para.Add(db.CreateParam("sEmail", DbType.String, ParameterDirection.Input, apiDetails.sEmail));
                else
                    para.Add(db.CreateParam("sEmail", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.sMailUploadTime))
                    para.Add(db.CreateParam("sMailUploadTime", DbType.String, ParameterDirection.Input, apiDetails.sMailUploadTime));
                else
                    para.Add(db.CreateParam("sMailUploadTime", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("MailLastUploadTime", DbType.DateTime, ParameterDirection.Input, System.DateTime.Now));
                para.Add(db.CreateParam("FTP_LAST_UPLOAD_DATE", DbType.DateTime, ParameterDirection.Input, System.DateTime.Now));

                if (iLocation1 != null)
                    para.Add(db.CreateParam("iLocation", DbType.Int64, ParameterDirection.Input, iLocation1));
                else
                    para.Add(db.CreateParam("iLocation", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiDetails.ApiUrl))
                    para.Add(db.CreateParam("ApiUrl", DbType.String, ParameterDirection.Input, apiDetails.ApiUrl));
                else
                    para.Add(db.CreateParam("ApiUrl", DbType.String, ParameterDirection.Input, DBNull.Value));

                string columnsSettingsValue = IPadCommon.ToXML<List<ApiColumnsSettings>>(apiDetails.ColumnsSettings);
                para.Add(db.CreateParam("columnsDefs", DbType.String, ParameterDirection.Input, columnsSettingsValue));

                IDbDataParameter pr = db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Output, iTransId);
                para.Add(pr);

                para.Add(db.CreateParam("OpType", DbType.String, ParameterDirection.Input, apiDetails.OperationType));
                if (apiDetails.OperationType.ToUpper() == "U")
                {
                    para.Add(db.CreateParam("iModifyingTransId", DbType.Int64, ParameterDirection.Input, apiDetails.iTransId));
                }

                db.ExecuteSP("Api_InsUpd_sunrise", para.ToArray(), false);
                iTransId = Convert.ToInt64(pr.Value);

                if (iTransId > 0)
                {
                    resp.Status = "1";
                    resp.Message = "Api Save Successfully";
                    resp.Error = "";
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Failed to save api details";
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
        public IHttpActionResult GetApiUploadMethod([FromBody]JObject data)
        {
            ApiFilterRequest getApiRequest = new ApiFilterRequest();
            try
            {
                getApiRequest = JsonConvert.DeserializeObject<ApiFilterRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiFilterResponse>
                {
                    Data = new List<ApiFilterResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                long? iPgNo = null, iPgSize = null, iUserId = null, iEmpId = null;
                long? iTransId = null;
                if (!string.IsNullOrEmpty(getApiRequest.sTransId))
                    iTransId = Convert.ToInt64(getApiRequest.sTransId);

                DateTime? FrmDate = null;
                DateTime? ToDate = null;
                if (!string.IsNullOrEmpty(getApiRequest.sPgNo))
                    iPgNo = Convert.ToInt64(getApiRequest.sPgNo);
                if (!string.IsNullOrEmpty(getApiRequest.sPgSize))
                    iPgSize = Convert.ToInt64(getApiRequest.sPgSize);
                System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                if (!String.IsNullOrEmpty(getApiRequest.dtFromDate))
                    FrmDate = Convert.ToDateTime(getApiRequest.dtFromDate, enGB);
                if (!String.IsNullOrEmpty(getApiRequest.dtToDate))
                    ToDate = Convert.ToDateTime(getApiRequest.dtToDate, enGB);
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (!String.IsNullOrEmpty(getApiRequest.sTransId))
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, getApiRequest.sTransId));
                else
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));

                if (!string.IsNullOrEmpty(getApiRequest.dtFromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, getApiRequest.dtFromDate));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(getApiRequest.dtToDate))
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, getApiRequest.dtToDate));
                else
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(getApiRequest.sSearch))
                    para.Add(db.CreateParam("sSearch", DbType.String, ParameterDirection.Input, getApiRequest.sSearch));
                else
                    para.Add(db.CreateParam("sSearch", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (iPgNo != null)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgSize != null)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(getApiRequest.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, getApiRequest.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_select", para.ToArray(), false);
                List<ApiUploadMethodResponse> apiResponses = new List<ApiUploadMethodResponse>();
                apiResponses = DataTableExtension.ToList<ApiUploadMethodResponse>(dt);
                if (apiResponses.Count > 0)
                {
                    List<ApiColumnsSettings> columnsSettings;
                    foreach (var apiDetails in apiResponses)
                    {
                        db = new Database();
                        para = new List<IDbDataParameter>();
                        para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(iTransId)));
                        dt = db.ExecuteSP("ApiUploadMethodMst_DET_Select", para.ToArray(), false);
                        if (dt.Rows.Count > 0)
                        {
                            columnsSettings = new List<ApiColumnsSettings>();
                            columnsSettings = DataTableExtension.ToList<ApiColumnsSettings>(dt);
                            apiDetails.ColumnsSettings = columnsSettings;
                        }
                    }
                    return Ok(new ServiceResponse<ApiUploadMethodResponse>
                    {
                        Data = apiResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ApiUploadMethodResponse>
                    {
                        Data = new List<ApiUploadMethodResponse>(),
                        Message = "No Record Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiFilterResponse>
                {
                    Data = new List<ApiFilterResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult ExcelGetApiUploadMethod([FromBody]JObject data)
        {
            ApiFilterRequest getApiRequest = new ApiFilterRequest();
            try
            {
                getApiRequest = JsonConvert.DeserializeObject<ApiFilterRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiFilterResponse>
                {
                    Data = new List<ApiFilterResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                long? iPgNo = null, iPgSize = null;
                long? iTransId = null;
                if (!string.IsNullOrEmpty(getApiRequest.sTransId))
                    iTransId = Convert.ToInt64(getApiRequest.sTransId);

                DateTime? FrmDate = null;
                DateTime? ToDate = null;
                if (!string.IsNullOrEmpty(getApiRequest.sPgNo))
                    iPgNo = Convert.ToInt64(getApiRequest.sPgNo);
                if (!string.IsNullOrEmpty(getApiRequest.sPgSize))
                    iPgSize = Convert.ToInt64(getApiRequest.sPgSize);
                System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                if (!String.IsNullOrEmpty(getApiRequest.dtFromDate))
                    FrmDate = Convert.ToDateTime(getApiRequest.dtFromDate, enGB);
                if (!String.IsNullOrEmpty(getApiRequest.dtToDate))
                    ToDate = Convert.ToDateTime(getApiRequest.dtToDate, enGB);
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (!String.IsNullOrEmpty(getApiRequest.sTransId))
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, getApiRequest.sTransId));
                else
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));

                if (!string.IsNullOrEmpty(getApiRequest.dtFromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, getApiRequest.dtFromDate));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(getApiRequest.dtToDate))
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, getApiRequest.dtToDate));
                else
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(getApiRequest.sSearch))
                    para.Add(db.CreateParam("sSearch", DbType.String, ParameterDirection.Input, getApiRequest.sSearch));
                else
                    para.Add(db.CreateParam("sSearch", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (iPgNo != null)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgSize != null)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(getApiRequest.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, getApiRequest.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dtData = db.ExecuteSP("ApiUploadMethodMst_select", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    string filename = "Client FTP API List " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    EpExcelExport.CreateFTPAPIExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath,
                                                    Convert.ToDateTime(getApiRequest.dtFromDate), Convert.ToDateTime(getApiRequest.dtToDate));

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
        public IHttpActionResult GetApiCriteria([FromBody]JObject data)
        {
            ApiFilterRequest getApiRequest = new ApiFilterRequest();
            try
            {
                getApiRequest = JsonConvert.DeserializeObject<ApiFilterRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ApiFilterResponse>
                {
                    Data = new List<ApiFilterResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!String.IsNullOrEmpty(getApiRequest.sTransId))
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, getApiRequest.sTransId));
                else
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_Filters_Select", para.ToArray(), false);

                List<APIFiltersSettings> apifilters = new List<APIFiltersSettings>();
                apifilters = DataTableExtension.ToList<APIFiltersSettings>(dt);
                if (apifilters.Count > 0)
                {

                    return Ok(new ServiceResponse<APIFiltersSettings>
                    {
                        Data = apifilters,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<APIFiltersSettings>
                    {
                        Data = apifilters,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<APIFiltersSettings>
                {
                    Data = new List<APIFiltersSettings>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult ApiUploadMethod([FromBody]JObject data)
        {
            // Api_Insert_sunrise	
            ApiUploadMethod apiuploadmethod = new ApiUploadMethod();
            try
            {
                apiuploadmethod = JsonConvert.DeserializeObject<ApiUploadMethod>(data.ToString());
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
                DataTable _dt = ApiUploadMethod_viewAll(1, 1000, 0);
                DataView _dv = new DataView(_dt);
                _dv.RowFilter = "APIName = '" + apiuploadmethod.APIName.Trim() + "'";
                if (apiuploadmethod.iTransId > 0)
                    _dv.RowFilter += " AND iTransId <> '" + apiuploadmethod.iTransId + "'";
                _dt = _dv.ToTable();
                if (_dt.Rows.Count > 0)
                {
                    resp.Status = "0";
                    resp.Message = "File Name Same " + apiuploadmethod.APIName.Trim();
                    resp.Error = "";
                    return Ok(resp);
                }
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                CultureInfo enGB = new CultureInfo("en-GB");
                DateTime trDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now), enGB);

                var db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));
                if (!string.IsNullOrEmpty(apiuploadmethod.ApiMethod))
                    para.Add(db.CreateParam("ApiMethod", DbType.String, ParameterDirection.Input, apiuploadmethod.ApiMethod));
                else
                    para.Add(db.CreateParam("ApiMethod", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.WebAPIUserName))
                    para.Add(db.CreateParam("WebAPIUserName", DbType.String, ParameterDirection.Input, apiuploadmethod.WebAPIUserName));
                else
                    para.Add(db.CreateParam("WebAPIUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.WebAPIPassword))
                    para.Add(db.CreateParam("WebAPIPassword", DbType.String, ParameterDirection.Input, apiuploadmethod.WebAPIPassword));
                else
                    para.Add(db.CreateParam("WebAPIPassword", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPHost))
                    para.Add(db.CreateParam("FTPHost", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPHost));
                else
                    para.Add(db.CreateParam("FTPHost", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPUser))
                    para.Add(db.CreateParam("FTPUser", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPUser));
                else
                    para.Add(db.CreateParam("FTPUser", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPUser))
                    para.Add(db.CreateParam("FTPPass", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPPass));
                else
                    para.Add(db.CreateParam("FTPPass", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPType))
                    para.Add(db.CreateParam("FtpType", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPType));
                else
                    para.Add(db.CreateParam("FtpType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPExportType))
                    para.Add(db.CreateParam("FTPExportType", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPExportType));
                else
                    para.Add(db.CreateParam("FTPExportType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.URLUserName))
                    para.Add(db.CreateParam("URLUserName", DbType.String, ParameterDirection.Input, apiuploadmethod.URLUserName));
                else
                    para.Add(db.CreateParam("URLUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.URLPassword))
                    para.Add(db.CreateParam("URLPassword", DbType.String, ParameterDirection.Input, apiuploadmethod.URLPassword));
                else
                    para.Add(db.CreateParam("URLPassword", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.URLExportType))
                    para.Add(db.CreateParam("URLExportType", DbType.String, ParameterDirection.Input, apiuploadmethod.URLExportType));
                else
                    para.Add(db.CreateParam("URLExportType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.APIName))
                    para.Add(db.CreateParam("APIName", DbType.String, ParameterDirection.Input, apiuploadmethod.APIName));
                else
                    para.Add(db.CreateParam("APIName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.APIStatus.ToString()))
                    para.Add(db.CreateParam("APIStatus", DbType.String, ParameterDirection.Input, apiuploadmethod.APIStatus));
                else
                    para.Add(db.CreateParam("APIStatus", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (apiuploadmethod.For_iUserId > 0)
                    para.Add(db.CreateParam("For_iUserId", DbType.Int64, ParameterDirection.Input, apiuploadmethod.For_iUserId));
                else
                    para.Add(db.CreateParam("For_iUserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                string columnssettingsvalue = IPadCommon.ToXML<List<ApiColumnsSettings>>(apiuploadmethod.ColumnsSettings);
                para.Add(db.CreateParam("columnsDefs", DbType.String, ParameterDirection.Input, columnssettingsvalue));

                string apifilterssettingsvalue = IPadCommon.ToXML<List<APIFiltersSettings>>(apiuploadmethod.APIFilters);
                para.Add(db.CreateParam("APIFilters", DbType.String, ParameterDirection.Input, apifilterssettingsvalue));

                para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, apiuploadmethod.iTransId));

                para.Add(db.CreateParam("APIUrl", DbType.String, ParameterDirection.Input, apiuploadmethod.APIUrl));

                DataTable dtData1 = db.ExecuteSP("ApiUploadMethodMst_InsUpd", para.ToArray(), false);
                if ((dtData1.Rows[0]["iTransId"].ToString() != "" ? Int32.Parse(dtData1.Rows[0]["iTransId"].ToString()) : 0) > 0)
                {
                    if (apiuploadmethod.iTransId == null || apiuploadmethod.iTransId == 0)
                    {
                        resp.Status = "1";
                        resp.Message = dtData1.Rows[0]["iTransId"].ToString();
                        resp.Error = "";
                    }
                    else
                    {
                        resp.Status = "1";
                        resp.Message = dtData1.Rows[0]["iTransId"].ToString();
                        resp.Error = "";
                    }
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = dtData1.Rows[0]["Message"].ToString();
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
        public IHttpActionResult ExportApi([FromBody] JObject data)
        {
            ApiExportRequest apiExportRequest = new ApiExportRequest();
            try
            {
                apiExportRequest = JsonConvert.DeserializeObject<ApiExportRequest>(data.ToString());
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
                para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(apiExportRequest.TransId)));
                para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(apiExportRequest.UserId)));
                DataTable dt = db.ExecuteSP("ApiExcel_select_Sunrise", para.ToArray(), false);

                string tempPath = HostingEnvironment.MapPath("~/Temp/APIEXPORT/");
                string _path = ConfigurationManager.AppSettings["data"];
                _path = _path.Replace("/ExcelFile/", "");
                _path += "/Temp/APIEXPORT/";
                string filename = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    Directory.CreateDirectory(tempPath);
                    if (apiExportRequest.ExportType.ToUpper() == "XML")
                    {
                        try
                        {
                            File.Delete(tempPath + apiExportRequest.ApiName + ".xml");
                        }
                        catch { }
                        dt.TableName = "Records";
                        filename = tempPath + apiExportRequest.ApiName + ".xml";
                        dt.WriteXml(tempPath + apiExportRequest.ApiName + ".xml");
                        filename = _path + apiExportRequest.ApiName + ".xml";
                    }

                    else if (apiExportRequest.ExportType.ToUpper() == "CSV")
                    {
                        try
                        {
                            File.Delete(tempPath + apiExportRequest.ApiName + ".csv");
                        }
                        catch { }

                        filename = tempPath + apiExportRequest.ApiName + ".csv";
                        FileInfo newFile = new FileInfo(filename);
                        using (ExcelPackage pck = new ExcelPackage(newFile))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("APIData");
                            pck.Workbook.Properties.Title = "API";
                            ws.Cells["A1"].LoadFromDataTable(dt, true);

                            ws.View.FreezePanes(2, 1);
                            var allCells = ws.Cells[ws.Dimension.Address];
                            allCells.AutoFilter = true;
                            allCells.AutoFitColumns();

                            removingGreenTagWarning(ws, ws.Cells[1, 1, 100, 100].Address);

                            var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                            headerCells.Style.Font.Bold = true;
                            headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                            headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                            pck.Save();

                        }

                        filename = _path + apiExportRequest.ApiName + ".csv";
                    }

                    else if (apiExportRequest.ExportType.ToUpper() == "EXCEL(.XLSX)" || apiExportRequest.ExportType.ToUpper() == "EXCEL(.XLS)" || apiExportRequest.ExportType.ToUpper() == "EXCEL")
                    {
                        try
                        {
                            if (apiExportRequest.ExportType.ToUpper() == "EXCEL(.XLSX)" || apiExportRequest.ExportType.ToUpper() == "EXCEL")
                                File.Delete(tempPath + apiExportRequest.ApiName + ".xlsx");
                            else
                                File.Delete(tempPath + apiExportRequest.ApiName + ".xls");
                        }
                        catch { }

                        if (apiExportRequest.ExportType.ToUpper() == "EXCEL(.XLSX)" || apiExportRequest.ExportType.ToUpper() == "EXCEL")
                            filename = tempPath + apiExportRequest.ApiName + ".xlsx";
                        else
                            filename = tempPath + apiExportRequest.ApiName + ".xls";

                        MemoryStream ms = ExportToExcelEpPlus(dt, apiExportRequest.ApiName, tempPath, Convert.ToInt32(apiExportRequest.TransId));

                        if (File.Exists(filename))
                            File.Delete(filename);

                        FileStream file = new FileStream(filename, FileMode.CreateNew, FileAccess.Write);
                        ms.WriteTo(file);
                        file.Close();
                        ms.Close();

                        if (apiExportRequest.ExportType.ToUpper() == "EXCEL(.XLSX)" || apiExportRequest.ExportType.ToUpper() == "EXCEL")
                            filename = _path + apiExportRequest.ApiName + ".xlsx";
                        else
                            filename = _path + apiExportRequest.ApiName + ".xls";
                    }
                    else if (apiExportRequest.ExportType.ToUpper() == "JSON")
                    {
                        try
                        {
                            File.Delete(tempPath + apiExportRequest.ApiName + ".json");
                        }
                        catch { }
                        string json = IPadCommon.DataTableToJSONWithStringBuilder(dt);
                        File.WriteAllText(tempPath + apiExportRequest.ApiName + ".json", json);
                        filename = _path + apiExportRequest.ApiName + ".json";
                    }
                }
                else
                {
                    filename = "Error No data found";
                }
                return Ok(filename);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Error");
            }
        }

        [NonAction]
        private DataTable ApiviewAll(int iPgNo, int iPgSize)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtToDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("iEmpId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("sFullName", DbType.String, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("sCompName", DbType.String, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("sCountryName", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("ApiMas_Select", para.ToArray(), false);

                dt.Columns.Add("dTransDate1", typeof(string));
                dt.Columns.Add("MailLastUploadTime1", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow drchange = dt.Rows[i];

                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["dTransDate"])))
                    {
                        try
                        {
                            DateTime _dttime = Convert.ToDateTime(dt.Rows[i]["dTransDate"]);
                            drchange["dTransDate1"] = _dttime.ToString("dd/MM/yyyy HH:MM tt");
                            dt.AcceptChanges();
                        }
                        catch { }
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["MailLastUploadTime"])))
                    {
                        try
                        {
                            DateTime _dttime = Convert.ToDateTime(dt.Rows[i]["MailLastUploadTime"]);
                            drchange["MailLastUploadTime1"] = _dttime.ToString("dd/MM/yyyy HH:MM tt");
                            dt.AcceptChanges();
                        }
                        catch { }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [NonAction]
        private static MemoryStream ExportToExcelEpPlus(DataTable dtData, string sFileName, string sTempPath, int Transid)
        {
            System.Web.UI.WebControls.GridView gvData = new System.Web.UI.WebControls.GridView();
            gvData.AutoGenerateColumns = true;

            // string filename = "";

            if (dtData.Rows.Count > 0)
            {
                DataColumnCollection columns = dtData.Columns;

                iCalculateDiscount = 0;
                iFormulaApply = 0;
                iTransid = Transid;
                string sNetPrice = string.Empty, sRapAmount = string.Empty, sDiscount = string.Empty, sRapPrice = string.Empty;
                sNetPrice = GetColumnCaption("dNetPrice", iTransid);
                sRapAmount = GetColumnCaption("dRapAmount", iTransid);
                sDiscount = GetColumnCaption("dDisc", iTransid);
                sRapPrice = GetColumnCaption("dRepPrice", iTransid);

                if (sRapAmount != sRapPrice && sRapAmount != sNetPrice && sRapAmount != sDiscount && sNetPrice != sDiscount && sNetPrice != sRapPrice && sDiscount != sRapPrice)
                {
                    iFormulaApply = 1;
                    if (columns.Contains(Convert.ToString(sNetPrice)) && columns.Contains(Convert.ToString(sRapAmount)))
                    {
                        if (Convert.ToString(sNetPrice) != Convert.ToString(sRapAmount))
                        {
                            //ViewState["CalculateDisc"] = "true";
                            iCalculateDiscount = 1;
                        }
                    }
                }
                gvData.DataSource = dtData;
                gvData.DataBind();

            }
            GridViewEpExcelExport ep_ge;
            ep_ge = new GridViewEpExcelExport(gvData, "Latest Inventory", "TotalStock");

            ep_ge.BeforeCreateColumnEvent1 += Ep_BeforeCreateColumnEventHandler2;
            ep_ge.AfterCreateCellEvent += Ep_AfterCreateCellEventHandler1;
            ep_ge.FillingWorksheetEvent += Ep_FillingWorksheetEventHandler1;

            MemoryStream ms = new MemoryStream();

            //string temppath = ConfigurationManager.AppSettings["FilePath"];
            //temppath = temppath.Replace("bin\\Debug", "Temp\\Excel");
            ep_ge.CreateExcel(ms, sTempPath, Transid);

            MemoryStream memn = new MemoryStream();

            byte[] byteDatan = ms.ToArray();
            memn.Write(byteDatan, 0, byteDatan.Length);
            memn.Flush();
            memn.Position = 0;

            return ms;
        }

        [NonAction]
        protected static void Ep_BeforeCreateColumnEventHandler1(object sender, ref EpExcelExport.ExcelHeader e)
        {

            if (e.Caption == "SR")
            {
                //e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                //e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0";
            }
            else if (e.Caption == GetColumnCaption("bImage", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                // e.Caption = "Image";
                e.Width = 75;
                e.SummText = "Total";
                sImages = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("bHDMovie", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                // e.Caption = "HD Movie";
                e.Width = 75;
                e.SummText = "Pcs";
                sHdmovie = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sRefNo", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
                e.SummText = "Total Pcs :";
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Count;
                //e.SummText = "Total";
            }
            else if (e.Caption == GetColumnCaption("sLab", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
                //e.SummText = "Pcs";
                strLab = e.Caption;
            }
            // Change By Hitesh on [26-04-2017] as Per [Doc No 756]
            else if (e.Caption == GetColumnCaption("sLabLink", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 170;
                //e.SummText = "Pcs";
                strLabLink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sShape", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //e.SummFunction = DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Count;
            }
            else if (e.Caption == GetColumnCaption("sPointer", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 12.15;
            }
            else if (e.Caption == GetColumnCaption("sCertiNo", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13.30;
                //e.Caption = "Certi No";
            }
            else if (e.Caption == GetColumnCaption("sColor", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("sClarity", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                // e.Caption = "Clarity";
            }
            else if (e.Caption == GetColumnCaption("dCts", iTransid))
            {
                CtsOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                if (iFormulaApply == 1)
                {
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                }
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dRepPrice", iTransid))
            {
                RapPriceOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 16.140625;
                e.NumFormat = "#,##0";
            }
            else if (e.Caption == GetColumnCaption("dRapAmount", iTransid))
            {
                RapAmountOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                if (iFormulaApply == 1)
                {
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                }
                e.NumFormat = "#,##0";
                e.Width = 15.42578125;
            }
            else if (e.Caption == GetColumnCaption("dDisc", iTransid))
            {
                DiscOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
                e.Width = 12.5703125;
                //if (ViewState["CalculateDisc"] != null)
                if (iCalculateDiscount == 1)
                {
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                    e.SummFormula = "(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula(GetColumnCaption("dNetPrice", iTransid), EpExcelExport.TotalsRowFunctionValues.Sum) +
                                        "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula(GetColumnCaption("dRapAmount", iTransid), EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*-100";


                }
            }
            else if (e.Caption == GetColumnCaption("dNetPrice", iTransid))
            {
                NetPriceOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //e.NumFormat = "#0.00";
                e.NumFormat = "#,##0.00";
                if (iFormulaApply == 1)
                {
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                }
                e.Width = 15.42578125;
            }
            else if (e.Caption == GetColumnCaption("sCut", iTransid))
            {
                CutOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("sPolish", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("sSymm", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("sFls", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("dLength", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
                //e.Caption = "Length";
            }
            else if (e.Caption == GetColumnCaption("dWidth", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dDepth", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dDepthPer", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dTablePer", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("sInclusion", iTransid)) // need to change
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            else if (e.Caption == GetColumnCaption("sTableNatts", iTransid)) // need to change
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }

            else if (e.Caption == GetColumnCaption("sCrownInclusion", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            else if (e.Caption == GetColumnCaption("sCrownNatts", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            else if (e.Caption == GetColumnCaption("sLuster", iTransid))
            {
                //e.Caption = "Luster/Milky";
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            else if (e.Caption == GetColumnCaption("sShade", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            }
            else if (e.Caption == GetColumnCaption("dCrAng", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dCrHt", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dPavAng", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dPavHt", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("Girdle Type", iTransid)) // need to change
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //e.Caption = "Girdle Type";
            }
            else if (e.Caption == GetColumnCaption("sStatus", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            else if (e.Caption == GetColumnCaption("sCountryName", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            else if (e.Caption == GetColumnCaption("sSymbol", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            // change by Hitesh on [05-07-2017] as Per [Doc No 829]
            else if (e.Caption == GetColumnCaption("sDna", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 100;
                sDNALink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sOtherIMG", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 100;
                sOtherImageLink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sOtherVideo", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 100;
                sOtherVideoLink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sOtherDna", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 100;
                sOtherDnaLink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sStrLn", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            else if (e.Caption == GetColumnCaption("sLrHalf", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            else if (e.Caption == GetColumnCaption("dCertiDate", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
        }

        [NonAction]
        protected static void Ep_BeforeCreateColumnEventHandler2(object sender, ref EpExcelExport.ExcelHeader e, List<ApiColSettings> columnsSettings)
        {
            if (e.Caption == "SR")
            {
                //e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                //e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0";
            }
            else if (e.Caption == GetColumnCaption("bImage", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                // e.Caption = "Image";
                e.Width = 75;
                e.SummText = "Total";
                sImages = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("bHDMovie", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                // e.Caption = "HD Movie";
                e.Width = 75;
                e.SummText = "Pcs";
                sHdmovie = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sRefNo", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
                e.SummText = "Total Pcs :";
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Count;
                //e.SummText = "Total";
            }
            else if (e.Caption == GetColumnCaption("sLab", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
                //e.SummText = "Pcs";
                strLab = e.Caption;
            }
            // Change By Hitesh on [26-04-2017] as Per [Doc No 756]
            else if (e.Caption == GetColumnCaption("sLabLink", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 170;
                //e.SummText = "Pcs";
                strLabLink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sShape", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //e.SummFunction = DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Count;
            }
            else if (e.Caption == GetColumnCaption("sPointer", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 12.15;
            }
            else if (e.Caption == GetColumnCaption("sCertiNo", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13.30;
                //e.Caption = "Certi No";
            }
            else if (e.Caption == GetColumnCaption("sColor", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("sClarity", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                // e.Caption = "Clarity";
            }
            else if (e.Caption == GetColumnCaption("dCts", columnsSettings))
            {
                CtsOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                if (iFormulaApply == 1)
                {
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                }
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dRepPrice", columnsSettings))
            {
                RapPriceOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 16.140625;
                e.NumFormat = "#,##0";
            }
            else if (e.Caption == GetColumnCaption("dRapAmount", columnsSettings))
            {
                RapAmountOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                if (iFormulaApply == 1)
                {
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                }
                e.NumFormat = "#,##0";
                e.Width = 15.42578125;
            }
            else if (e.Caption == GetColumnCaption("dDisc", columnsSettings))
            {
                DiscOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
                e.Width = 12.5703125;
                //if (ViewState["CalculateDisc"] != null)
                if (iCalculateDiscount == 1)
                {
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                    e.SummFormula = "(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula(GetColumnCaption("dNetPrice", iTransid), EpExcelExport.TotalsRowFunctionValues.Sum) +
                                        "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula(GetColumnCaption("dRapAmount", iTransid), EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*-100";


                }
            }
            else if (e.Caption == GetColumnCaption("dNetPrice", columnsSettings))
            {
                NetPriceOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //e.NumFormat = "#0.00";
                e.NumFormat = "#,##0.00";
                if (iFormulaApply == 1)
                {
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                }
                e.Width = 15.42578125;
            }
            else if (e.Caption == GetColumnCaption("sCut", columnsSettings))
            {
                CutOriginalCol = e.Caption;
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("sPolish", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("sSymm", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("sFls", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else if (e.Caption == GetColumnCaption("dLength", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
                //e.Caption = "Length";
            }
            else if (e.Caption == GetColumnCaption("dWidth", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dDepth", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dDepthPer", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dTablePer", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("sInclusion", columnsSettings)) // need to change
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            else if (e.Caption == GetColumnCaption("sTableNatts", columnsSettings)) // need to change
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }

            else if (e.Caption == GetColumnCaption("sCrownInclusion", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            else if (e.Caption == GetColumnCaption("sCrownNatts", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            else if (e.Caption == GetColumnCaption("sLuster", columnsSettings))
            {
                //e.Caption = "Luster/Milky";
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            else if (e.Caption == GetColumnCaption("sShade", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            }
            else if (e.Caption == GetColumnCaption("dCrAng", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dCrHt", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dPavAng", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("dPavHt", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.NumFormat = "#0.00";
            }
            else if (e.Caption == GetColumnCaption("Girdle Type", columnsSettings)) // need to change
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //e.Caption = "Girdle Type";
            }
            else if (e.Caption == GetColumnCaption("sStatus", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            else if (e.Caption == GetColumnCaption("sCountryName", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            else if (e.Caption == GetColumnCaption("sSymbol", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 20;
            }
            // change by Hitesh on [05-07-2017] as Per [Doc No 829]
            else if (e.Caption == GetColumnCaption("sDna", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 100;
                sDNALink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sOtherIMG", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 100;
                sOtherImageLink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sOtherVideo", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 100;
                sOtherVideoLink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sOtherDna", columnsSettings))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                e.Width = 100;
                sOtherDnaLink = e.Caption;
            }
            else if (e.Caption == GetColumnCaption("sStrLn", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            else if (e.Caption == GetColumnCaption("sLrHalf", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            else if (e.Caption == GetColumnCaption("dCertiDate", iTransid))
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
        }

        [NonAction]
        protected static void Ep_FillingWorksheetEventHandler1(object sender, ref EpExcelExportLib.EpExcelExport.FillingWorksheetEventArgs e)
        {
            //UInt32 DiscNormalStyleindex;
            //UInt32 CutNormalStyleindex;

            EpExcelExport ee = (EpExcelExport)sender;
            EpExcelExport.ExcelFormat format = new EpExcelExport.ExcelFormat();

            format = new EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            DiscNormalStyleindex1 = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.isbold = true;
            CutNormalStyleindex1 = ee.AddStyle(format);
        }

        [NonAction]
        protected static void Ep_AfterCreateCellEventHandler1(object sender, ref EpExcelExportLib.EpExcelExport.ExcelCellFormat e)
        {
            if (e.tableArea == EpExcelExport.TableArea.Header)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                //e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.isbold = true;

            }
            else if (e.tableArea == EpExcelExport.TableArea.Detail)
            {
                if (e.ColumnName == Convert.ToString(CutOriginalCol)) // GetColumnCaption("sCut"))//cut
                {
                    if (e.Text == "3EX")
                        e.StyleInd = CutNormalStyleindex1;
                }
                else if (e.ColumnName == Convert.ToString(DiscOriginalCol))//Disc
                {
                    e.isbold = true;
                    e.StyleInd = DiscNormalStyleindex1;
                }
                else if (e.ColumnName == Convert.ToString(NetPriceOriginalCol)) // Net Amount
                {
                    if (iFormulaApply == 1)
                    {
                        //e.Formula = "TotalStock[[#This Row],[Rap Amt($)]]+(TotalStock[[#This Row],[Rap Amt($)]]*TotalStock[[#This Row],[Disc (%)]]/100)";
                        if (Convert.ToString(RapAmountOriginalCol) != "" && Convert.ToString(DiscOriginalCol) != "")
                        {
                            e.Formula = "TotalStock[[#This Row],[" + RapAmountOriginalCol + "]]+(TotalStock[[#This Row],[" + RapAmountOriginalCol + "]]*TotalStock[[#This Row],[" + DiscOriginalCol + "]]/100)";
                        }
                    }
                }
                else if (e.ColumnName == Convert.ToString(RapAmountOriginalCol))
                {
                    if (iFormulaApply == 1)
                    {
                        if (Convert.ToString(CtsOriginalCol) != "" && Convert.ToString(RapPriceOriginalCol) != "")
                        {
                            e.Formula = "TotalStock[[#This Row],[" + CtsOriginalCol + "]]*TotalStock[[#This Row],[" + RapPriceOriginalCol + "]]";
                        }
                    }
                }
                // change by hitesh as per [Doc No 478] on [03-09-2016] bcoz & sign replace with &amp; when export excel file
                else if (e.ColumnName == Convert.ToString(sImages))
                {
                    if (e.Text.Contains("&amp;"))
                    {
                        e.Text = e.Text.ToString().Replace("&amp;", "&");
                    }
                }
                else if (e.ColumnName == Convert.ToString(strLab))
                {
                    if (e.Text.Contains("&amp;"))
                    {
                        e.Text = e.Text.ToString().Replace("&amp;", "&");
                    }
                }
                // Change By Hitesh on [26-04-2017] as Per [Doc No 756]
                else if (e.ColumnName == Convert.ToString(strLabLink))
                {
                    if (e.Text.Contains("&amp;"))
                    {
                        e.Text = e.Text.ToString().Replace("&amp;", "&");
                    }
                }

                else if (e.ColumnName == Convert.ToString(sHdmovie))
                {
                    if (e.Text.Contains("&amp;"))
                    {
                        e.Text = e.Text.ToString().Replace("&amp;", "&");
                    }
                }
                // end [Doc No 478]
                //changes by hitesh as per [Doc No 829] on [05-07-2017]
                else if (e.ColumnName == Convert.ToString(sDNALink))
                {
                    if (e.Text.Contains("&amp;"))
                    {
                        e.Text = e.Text.ToString().Replace("&amp;", "&");
                    }
                }

                else if (e.ColumnName == Convert.ToString(sOtherImageLink))
                {
                    if (e.Text.Contains("&amp;"))
                    {
                        e.Text = e.Text.ToString().Replace("&amp;", "&");
                    }
                }
                else if (e.ColumnName == Convert.ToString(sOtherVideoLink))
                {
                    if (e.Text.Contains("&amp;"))
                    {
                        e.Text = e.Text.ToString().Replace("&amp;", "&");
                    }
                }
                else if (e.ColumnName == Convert.ToString(sOtherDnaLink))
                {
                    if (e.Text.Contains("&amp;"))
                    {
                        e.Text = e.Text.ToString().Replace("&amp;", "&");
                    }
                }
                //End by hitesh as per [Doc No 829] on [05-07-2017]
            }
            else if (e.tableArea == EpExcelExport.TableArea.Footer)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.isbold = true;
                //e.ul = OfficeOpenXml.Style.eUnderLineType.None;
                e.ul = OfficeOpenXml.Style.ExcelUnderLineType.None;

                if (e.ColumnName == GetColumnCaption("bImage", iTransid))
                {
                    e.Text = "Total:";
                }
                else if (e.ColumnName == GetColumnCaption("bHDMovie", iTransid))
                {
                    e.Text = "Pcs";
                }
                else if (e.ColumnName == GetColumnCaption("sRefNo", iTransid))
                {
                    e.Text = "Total Pcs :";
                }
            }

        }

        [NonAction]
        private static string GetColumnCaption(string sColCaption, int iTransId)
        {
            string sUserCaption = "";

            Database db = new Database();
            DataTable dt = new DataTable();
            List<IDbDataParameter> para = new List<IDbDataParameter>();
            para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(iTransId)));
            dt = db.ExecuteSP("ApiUploadMethodMst_DET_Select", para.ToArray(), false);
            if (dt.Rows.Count > 0)
            {
                List<ApiColumnsSettings> columnsSettings = new List<ApiColumnsSettings>();
                columnsSettings = DataTableExtension.ToList<ApiColumnsSettings>(dt);

                var Ucaption = columnsSettings.Where(cp => cp.sColumnName.Equals(sColCaption)).SingleOrDefault();
                if (Ucaption != null)
                {
                    sUserCaption = Ucaption.sUserCaption;
                }
                return sUserCaption.ToString().Trim();
            }
            return string.Empty;
        }
        [NonAction]
        private static string GetColumnUserCaption(string sColCaption, int iTransId)
        {
            string sUserCaption = "";
            Database db = new Database();
            DataTable dt = new DataTable();
            List<IDbDataParameter> para = new List<IDbDataParameter>();
            para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(iTransId)));
            dt = db.ExecuteSP("ApiUploadMethodMst_DET_Select", para.ToArray(), false);
            if (dt.Rows.Count > 0)
            {
                List<ApiColumnsSettings> columnsSettings = new List<ApiColumnsSettings>();
                columnsSettings = DataTableExtension.ToList<ApiColumnsSettings>(dt);
                var Ucaption = columnsSettings.Where(cp => cp.sColumnName.Equals(sColCaption)).SingleOrDefault();
                if (Ucaption != null)
                {
                    sUserCaption = Ucaption.sCustMiseCaption;
                }
                return sUserCaption.ToString().Trim();
            }
            return string.Empty;
        }
        [NonAction]
        private static string GetColumnCaption(string sColCaption, List<ApiColSettings> columnsSettings)
        {
            string sUserCaption = "";

            if (columnsSettings.Count > 0)
            {
                var Ucaption = columnsSettings.Where(cp => cp.sColumnName.Equals(sColCaption)).SingleOrDefault();
                if (Ucaption != null)
                {
                    sUserCaption = Ucaption.sUserCaption;
                }
                return sUserCaption.ToString().Trim();
            }
            return string.Empty;
        }


        #region Client FTP/API Related Functionality

        [NonAction]
        private DataTable ApiUploadMethod_viewAll(int iPgNo, int iPgSize, int TransId = 0)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (TransId > 0)
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, TransId));
                else
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtToDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_select", para.ToArray(), false);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [BasicAuthentication]
        public HttpResponseMessage BasicAuthLog(int TransId)
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                string message = string.Empty;
                CommonResponse resp = new CommonResponse();
                DataTable _dt = ApiUploadMethod_viewAll(1, 1000, TransId);
                if (_dt.Rows.Count > 0)
                {
                    DataView _dv = new DataView(_dt);
                    _dv.RowFilter = "WebAPIUserName = '" + username.Trim() + "'";
                    _dt = _dv.ToTable();
                    int TC = 0, TC1 = 0;
                    if (_dt.Rows.Count > 0)
                    {
                        TC = _dt.Rows.Count;
                    }
                    DataView _dv1 = new DataView(_dt);
                    _dv1.RowFilter = "APIStatus = 'True'";
                    _dt = _dv1.ToTable();
                    if (_dt.Rows.Count > 0)
                    {
                        TC1 = _dt.Rows.Count;
                    }

                    if (TC != 0 && TC1 == 0)
                    {
                        InsertApiMethodLog(Log_TransId, Log_UserId, 0, false, "API is In Active", "");
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                    }

                    Log_TransId = Convert.ToInt32(_dt.Rows[0]["iTransId"].ToString());
                    Log_UserId = Convert.ToInt32(_dt.Rows[0]["iUserId"].ToString());

                    Database db = new Database();
                    List<IDbDataParameter> para = new List<IDbDataParameter>();

                    if (!String.IsNullOrEmpty(_dt.Rows[0]["iTransId"].ToString()))
                        para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, _dt.Rows[0]["iTransId"].ToString()));
                    else
                        para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                    if (!String.IsNullOrEmpty(_dt.Rows[0]["iUserId"].ToString()))
                        para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, _dt.Rows[0]["iUserId"].ToString()));
                    else
                        para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                    DataTable dt = db.ExecuteSP("SelectAPIMethodData", para.ToArray(), false);
                    if (dt.Rows.Count > 0)
                    {
                        TotCount = dt.Rows.Count;

                        string tempPath = HostingEnvironment.MapPath("~/Temp/WEBAPI_EXPORT/");
                        string _path = ConfigurationManager.AppSettings["data"];
                        _path = _path.Replace("/ExcelFile/", "");
                        _path += "/Temp/WEBAPI_EXPORT/";
                        DateTime now = DateTime.Now;
                        string DATE = " " + now.Day + "" + now.Month + "" + now.Year + "" + now.Hour + "" + now.Minute + "" + now.Second;
                        if (!Directory.Exists(tempPath))
                        {
                            Directory.CreateDirectory(tempPath);
                        }

                        string filename = tempPath + _dt.Rows[0]["APIName"].ToString() + DATE + ".xlsx";
                        string FileForUpload = _path + _dt.Rows[0]["APIName"].ToString() + DATE + ".xlsx";

                        if (File.Exists(filename))
                        {
                            File.Delete(filename);
                        }

                        FileInfo newFile = new FileInfo(filename);
                        using (ExcelPackage pck = new ExcelPackage(newFile))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(_dt.Rows[0]["APIName"].ToString());
                            pck.Workbook.Properties.Title = "WEB API";
                            ws.Cells["A1"].LoadFromDataTable(dt, true);

                            ws.View.FreezePanes(2, 1);
                            var allCells = ws.Cells[ws.Dimension.Address];
                            allCells.AutoFilter = true;
                            allCells.AutoFitColumns();

                            removingGreenTagWarning(ws, ws.Cells[1, 1, 100, 100].Address);

                            var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                            headerCells.Style.Font.Bold = true;
                            headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                            headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                            pck.Save();
                        }

                        InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, true, "Success", FileForUpload);

                        return Request.CreateResponse(DataTableToJSONWithJSONNet(dt));
                    }
                    else
                    {
                        InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "BasicAuthLog : 404 Record Not Found", "");
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    //InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "404 Bad Request", "");
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "BasicAuthLog : " + ex.Message.ToString(), "");
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult GetURLApi([FromBody] JObject data)
        {
            GetURLApiRequest geturlapirequest = new GetURLApiRequest();
            try
            {
                geturlapirequest = JsonConvert.DeserializeObject<GetURLApiRequest>(data.ToString());
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
                DataTable _dt = ApiUploadMethod_viewAll(1, 1000, Convert.ToInt32(geturlapirequest.TransId));
                if (_dt.Rows.Count > 0)
                {
                    Log_TransId = Convert.ToInt32(_dt.Rows[0]["iTransId"].ToString());
                    Log_UserId = Convert.ToInt32(_dt.Rows[0]["iUserId"].ToString());

                    DataView _dv = new DataView(_dt);
                    _dv.RowFilter = "URLUserName = '" + geturlapirequest.Username.Trim() + "' AND URLPassword = '" + geturlapirequest.Password.Trim() + "'";
                    _dt = _dv.ToTable();
                    int TC = 0, TC1 = 0;
                    if (_dt.Rows.Count > 0)
                    {
                        TC = _dt.Rows.Count;
                    }
                    DataView _dv1 = new DataView(_dt);
                    _dv1.RowFilter = "APIStatus = 'True'";
                    _dt = _dv1.ToTable();
                    if (_dt.Rows.Count > 0)
                    {
                        TC1 = _dt.Rows.Count;
                    }

                    if (TC != 0 && TC1 == 0)
                    {
                        InsertApiMethodLog(Log_TransId, Log_UserId, 0, false, "GetURLApi : API is In Active", "");
                        return Ok(new CommonResponse
                        {
                            Message = "",
                            Status = "0",
                            Error = "API is In Active"
                        });
                    }
                    if (TC == 0)
                    {
                        InsertApiMethodLog(Log_TransId, Log_UserId, 0, false, "GetURLApi : 401 Unauthorized request", "");
                        return Ok(new CommonResponse
                        {
                            Message = "",
                            Status = "0",
                            Error = "401 Unauthorized request"
                        });
                    }


                    Database db = new Database();
                    List<IDbDataParameter> para = new List<IDbDataParameter>();

                    if (!String.IsNullOrEmpty(_dt.Rows[0]["iTransId"].ToString()))
                        para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, _dt.Rows[0]["iTransId"].ToString()));
                    else
                        para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                    if (!String.IsNullOrEmpty(_dt.Rows[0]["iUserId"].ToString()))
                        para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, _dt.Rows[0]["iUserId"].ToString()));
                    else
                        para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                    DataTable dt = db.ExecuteSP("SelectAPIMethodData", para.ToArray(), false);
                    string filename = "";

                    if (dt.Rows.Count > 0)
                    {
                        TotCount = dt.Rows.Count;

                        string tempPath = HostingEnvironment.MapPath("~/Temp/URLAPI_EXPORT/");
                        string _path = ConfigurationManager.AppSettings["data"];
                        _path = _path.Replace("/ExcelFile/", "");
                        _path += "/Temp/URLAPI_EXPORT/";
                        DateTime now = DateTime.Now;
                        string DATE = " " + now.Day + "" + now.Month + "" + now.Year + "" + now.Hour + "" + now.Minute + "" + now.Second;
                        string FileForUpload = "";

                        if (!Directory.Exists(tempPath))
                        {
                            Directory.CreateDirectory(tempPath);
                        }
                        if (_dt.Rows[0]["URLExportType"].ToString().ToUpper() == "XML")
                        {
                            filename = tempPath + _dt.Rows[0]["APIName"].ToString() + DATE + ".xml";
                            if (File.Exists(filename))
                            {
                                File.Delete(filename);
                            }

                            dt.TableName = "Records";
                            dt.WriteXml(tempPath + _dt.Rows[0]["APIName"].ToString() + DATE + ".xml");
                            FileForUpload = _path + _dt.Rows[0]["APIName"].ToString() + DATE + ".xml";
                        }
                        else if (_dt.Rows[0]["URLExportType"].ToString().ToUpper() == "CSV")
                        {
                            filename = tempPath + _dt.Rows[0]["APIName"].ToString() + DATE + ".csv";
                            if (File.Exists(filename))
                            {
                                File.Delete(filename);
                            }

                            //FileInfo newFile = new FileInfo(filename);
                            //using (ExcelPackage pck = new ExcelPackage(newFile))
                            //{
                            //    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(_dt.Rows[0]["APIName"].ToString());
                            //    pck.Workbook.Properties.Title = "API";
                            //    ws.Cells["A1"].LoadFromDataTable(dt, true);
                            //    pck.Save();
                            //}

                            StringBuilder sb = new StringBuilder();
                            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                            sb.AppendLine(string.Join(",", columnNames));

                            foreach (DataRow row in dt.Rows)
                            {
                                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Replace(",", " "));
                                sb.AppendLine(string.Join(",", fields));
                            }
                            File.WriteAllText(filename, sb.ToString());

                            FileForUpload = _path + _dt.Rows[0]["APIName"].ToString() + DATE + ".csv";
                        }
                        else if (_dt.Rows[0]["URLExportType"].ToString().ToUpper() == "EXCEL(.XLSX)" || _dt.Rows[0]["URLExportType"].ToString().ToUpper() == "EXCEL(.XLS)" || _dt.Rows[0]["URLExportType"].ToString().ToUpper() == "EXCEL")
                        {
                            if (_dt.Rows[0]["URLExportType"].ToString().ToUpper() == "EXCEL(.XLSX)" || _dt.Rows[0]["URLExportType"].ToString().ToUpper() == "EXCEL")
                            {
                                filename = tempPath + _dt.Rows[0]["APIName"].ToString() + DATE + ".xlsx";
                                FileForUpload = _path + _dt.Rows[0]["APIName"].ToString() + DATE + ".xlsx";
                            }
                            else
                            {
                                filename = tempPath + _dt.Rows[0]["APIName"].ToString() + DATE + ".xls";
                                FileForUpload = _path + _dt.Rows[0]["APIName"].ToString() + DATE + ".xls";
                            }

                            if (File.Exists(filename))
                            {
                                File.Delete(filename);
                            }

                            FileInfo newFile = new FileInfo(filename);
                            using (ExcelPackage pck = new ExcelPackage(newFile))
                            {
                                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(_dt.Rows[0]["APIName"].ToString());
                                pck.Workbook.Properties.Title = "API";
                                ws.Cells["A1"].LoadFromDataTable(dt, true);

                                ws.View.FreezePanes(2, 1);
                                var allCells = ws.Cells[ws.Dimension.Address];
                                allCells.AutoFilter = true;
                                allCells.AutoFitColumns();

                                int rowStart = ws.Dimension.Start.Row;
                                int rowEnd = ws.Dimension.End.Row;
                                removingGreenTagWarning(ws, ws.Cells[1, 1, rowEnd, 100].Address);
                                string ColCaption = "";
                                ColCaption = GetColumnUserCaption("Cts", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("Rap Price($)", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("Rap Amt($)", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("Disc(%)", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("Net Amt($)", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("Length", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("Width", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("Depth", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("DepthPer", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("TablePer", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("CrAng", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("CrHt", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("PavAng", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("PavHt", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }
                                ColCaption = GetColumnUserCaption("Girdle(%)", Log_TransId);
                                if (ColCaption != "")
                                {
                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                }

                                var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                                headerCells.Style.Font.Bold = true;
                                headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                                pck.Save();
                            }

                            //using (XLWorkbook wb = new XLWorkbook())
                            //{
                            //    wb.Worksheets.Add(dt, _dt.Rows[0]["APIName"].ToString());
                            //    wb.SaveAs(filename);
                            //}

                            //MemoryStream ms = ExportToExcelEpPlus(dt, _dt.Rows[0]["APIName"].ToString(), tempPath, Convert.ToInt32(_dt.Rows[0]["iTransId"].ToString()), "UploadMethod");
                            //FileStream file = new FileStream(filename, FileMode.CreateNew, FileAccess.Write);
                            //ms.WriteTo(file);
                            //file.Close();
                            //ms.Close();
                        }
                        else if (_dt.Rows[0]["URLExportType"].ToString().ToUpper() == "JSON")
                        {
                            filename = tempPath + _dt.Rows[0]["APIName"].ToString() + DATE + ".json";
                            if (File.Exists(filename))
                            {
                                File.Delete(filename);
                            }
                            string json = IPadCommon.DataTableToJSONWithStringBuilder(dt);
                            File.WriteAllText(tempPath + _dt.Rows[0]["APIName"].ToString() + DATE + ".json", json);
                            FileForUpload = _path + _dt.Rows[0]["APIName"].ToString() + DATE + ".json";
                        }

                        InsertApiMethodLog(Convert.ToInt32(_dt.Rows[0]["iTransId"].ToString()), Convert.ToInt32(_dt.Rows[0]["iUserId"].ToString()), TotCount, true, "Success", FileForUpload);
                        return Ok(new CommonResponse
                        {
                            Message = filename,
                            Status = "1",
                            Error = ""
                        });
                    }
                    else
                    {
                        InsertApiMethodLog(Convert.ToInt32(_dt.Rows[0]["iTransId"].ToString()), Convert.ToInt32(_dt.Rows[0]["iUserId"].ToString()), TotCount, false, "GetURLApi : 404 Record Not Found", "");
                        return Ok(new CommonResponse
                        {
                            Message = "",
                            Status = "0",
                            Error = "404 Record Not Found"
                        });
                    }
                }
                else
                {
                    //InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "GetURLApi : 400 Bad Request", "");
                    return Ok(new CommonResponse
                    {
                        Message = "",
                        Status = "0",
                        Error = "400 Bad Request"
                    });
                }

            }
            catch (Exception ex)
            {
                InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "GetURLApi : " + ex.Message.ToString(), "");
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "",
                    Status = "0",
                    Error = ex.Message
                });
            }
        }
        private void removingGreenTagWarning(ExcelWorksheet template1, string address)
        {
            var xdoc = template1.WorksheetXml;
            //Create the import nodes (note the plural vs singular
            var ignoredErrors = xdoc.CreateNode(System.Xml.XmlNodeType.Element, "ignoredErrors", xdoc.DocumentElement.NamespaceURI);
            var ignoredError = xdoc.CreateNode(System.Xml.XmlNodeType.Element, "ignoredError", xdoc.DocumentElement.NamespaceURI);
            ignoredErrors.AppendChild(ignoredError);

            //Attributes for the INNER node
            var sqrefAtt = xdoc.CreateAttribute("sqref");
            sqrefAtt.Value = address;// Or whatever range is needed....

            var flagAtt = xdoc.CreateAttribute("numberStoredAsText");
            flagAtt.Value = "1";

            ignoredError.Attributes.Append(sqrefAtt);
            ignoredError.Attributes.Append(flagAtt);

            //Now put the OUTER node into the worksheet xml
            xdoc.LastChild.AppendChild(ignoredErrors);
        }
        public int GetColumnByName(ExcelWorksheet ws, string columnName)
        {
            if (ws == null) throw new ArgumentNullException(nameof(ws));
            return ws.Cells["1:1"].First(c => c.Value.ToString() == columnName).Start.Column;
        }
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public static string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public static string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }

        [HttpPost]
        public IHttpActionResult GetApiMethodDetails([FromBody] JObject data)
        {
            ApiMethodRequest apiRequest = new ApiMethodRequest();
            try
            {
                apiRequest = JsonConvert.DeserializeObject<ApiMethodRequest>(data.ToString());
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
                DataTable dtApiDet = ApiUploadMethod_viewAll(1, 1000, Convert.ToInt32(apiRequest.TransId));

                if (dtApiDet != null && dtApiDet.Rows.Count > 0)
                {
                    Log_TransId = Convert.ToInt32(apiRequest.TransId);
                    Log_UserId = Convert.ToInt32(dtApiDet.Rows[0]["iUserId"].ToString());

                    //Database db = new Database();
                    //List<IDbDataParameter> para = new List<IDbDataParameter>();
                    //para.Add(db.CreateParam("@TransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(apiRequest.TransId)));

                    //DataTable dtApiDet = db.ExecuteSP("ApiUploadMethodMst_select", para.ToArray(), false);


                    DataView _dv = new DataView(dtApiDet);
                    _dv.RowFilter = "APIStatus = 'True'";
                    dtApiDet = _dv.ToTable();
                    int TC1 = 0;
                    if (dtApiDet.Rows.Count > 0)
                    {
                        TC1 = dtApiDet.Rows.Count;
                    }

                    if (TC1 == 0)
                    {
                        InsertApiMethodLog(Log_TransId, Log_UserId, 0, false, "GetApiMethodDetails : API is In Active", "");
                        return Ok(new CommonResponse
                        {
                            Message = "",
                            Status = "0",
                            Error = "API is In Active"
                        });
                    }

                    string APIMethod = dtApiDet.Rows[0]["ApiMethod"].ToString();
                    string APIName = dtApiDet.Rows[0]["APIName"].ToString();
                    if (APIMethod.ToString() == "FTP")
                    {
                        string FtpHost = dtApiDet.Rows[0]["FTPHost"].ToString();
                        string FtpUser = dtApiDet.Rows[0]["FTPUser"].ToString();
                        string FtpPass = dtApiDet.Rows[0]["FTPPass"].ToString();
                        string ExportType = dtApiDet.Rows[0]["FTPExportType"].ToString();
                        string FTPType = dtApiDet.Rows[0]["FTPType"].ToString();

                        Database dbnew = new Database();
                        List<IDbDataParameter> paranew = new List<IDbDataParameter>();
                        paranew.Add(dbnew.CreateParam("@TransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(apiRequest.TransId)));
                        paranew.Add(dbnew.CreateParam("@UserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(dtApiDet.Rows[0]["iUserId"].ToString())));
                        DataTable dtStock = dbnew.ExecuteSP("SelectAPIMethodData", paranew.ToArray(), false);

                        if (dtStock != null && dtStock.Rows.Count > 0)
                        {
                            TotCount = dtStock.Rows.Count;
                            string tempPath = HostingEnvironment.MapPath("~/Temp/FTPAPI_EXPORT/");
                            string _path = ConfigurationManager.AppSettings["data"];
                            _path = _path.Replace("/ExcelFile/", "");
                            _path += "/Temp/FTPAPI_EXPORT/";
                            string filename = "";
                            DateTime now = DateTime.Now;
                            string DATE = " " + now.Day + "" + now.Month + "" + now.Year + "" + now.Hour + "" + now.Minute + "" + now.Second;
                            string FileForUpload = "";
                            string FileForUploadServer = "";
                            if (!Directory.Exists(tempPath))
                            {
                                Directory.CreateDirectory(tempPath);
                            }

                            if (ExportType.ToUpper() == "CSV")
                            {
                                filename = tempPath + APIName + DATE + ".csv";
                                //  sourcepath = tempPath + APIName + ".csv"; ;
                                FileForUpload = APIName + DATE + ".csv";
                                FileForUploadServer = APIName + ".csv";
                                if (File.Exists(filename))
                                {
                                    File.Delete(filename);
                                }


                                //FileInfo newFile = new FileInfo(filename);
                                //using (ExcelPackage pck = new ExcelPackage(newFile))
                                //{
                                //    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(APIName);
                                //    pck.Workbook.Properties.Title = "API";
                                //    ws.Cells["A1"].LoadFromDataTable(dtStock, true);
                                //    pck.Save();
                                //}
                                StringBuilder sb = new StringBuilder();
                                IEnumerable<string> columnNames = dtStock.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                                sb.AppendLine(string.Join(",", columnNames));

                                foreach (DataRow row in dtStock.Rows)
                                {
                                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Replace(",", " "));
                                    sb.AppendLine(string.Join(",", fields));
                                }
                                File.WriteAllText(filename, sb.ToString());

                                //  filename = _path + APIName + ".csv";
                            }
                            if (ExportType.ToUpper() == "EXCEL(.XLSX)" || ExportType.ToUpper() == "EXCEL(.XLS)" || ExportType.ToUpper() == "EXCEL")
                            {

                                if (ExportType.ToUpper() == "EXCEL(.XLSX)" || ExportType.ToUpper() == "EXCEL")
                                {
                                    filename = tempPath + APIName + DATE + ".xlsx";
                                }
                                else
                                {
                                    filename = tempPath + APIName + DATE + ".xls";
                                }

                                if (File.Exists(filename))
                                {
                                    File.Delete(filename);
                                }

                                if (ExportType.ToUpper() == "EXCEL(.XLSX)" || ExportType.ToUpper() == "EXCEL")
                                {
                                    filename = tempPath + APIName + DATE + ".xlsx";
                                    //sourcepath = tempPath + APIName + ".xlsx";
                                    FileForUpload = APIName + DATE + ".xlsx";
                                    FileForUploadServer = APIName + ".xlsx";
                                }
                                else
                                {
                                    filename = tempPath + APIName + DATE + ".xls";
                                    // sourcepath = tempPath + APIName + ".xls";
                                    FileForUpload = APIName + DATE + ".xls";
                                    FileForUploadServer = APIName + ".xls";
                                }

                                FileInfo newFile = new FileInfo(filename);
                                using (ExcelPackage pck = new ExcelPackage(newFile))
                                {
                                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(APIName);
                                    pck.Workbook.Properties.Title = "API";
                                    ws.Cells["A1"].LoadFromDataTable(dtStock, true);

                                    ws.View.FreezePanes(2, 1);
                                    var allCells = ws.Cells[ws.Dimension.Address];
                                    allCells.AutoFilter = true;
                                    allCells.AutoFitColumns();

                                    int rowStart = ws.Dimension.Start.Row;
                                    int rowEnd = ws.Dimension.End.Row;
                                    removingGreenTagWarning(ws, ws.Cells[1, 1, rowEnd, 100].Address);
                                    string ColCaption = "";
                                    ColCaption = GetColumnUserCaption("Cts", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("Rap Price($)", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("Rap Amt($)", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("Disc(%)", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("Net Amt($)", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("Length", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("Width", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("Depth", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("DepthPer", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("TablePer", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("CrAng", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("CrHt", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("PavAng", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("PavHt", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }
                                    ColCaption = GetColumnUserCaption("Girdle(%)", Log_TransId);
                                    if (ColCaption != "")
                                    {
                                        ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                    }

                                    var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                                    headerCells.Style.Font.Bold = true;
                                    headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                                    pck.Save();
                                }

                                //using (XLWorkbook wb = new XLWorkbook())
                                //{
                                //    wb.Worksheets.Add(dtStock, APIName);
                                //    wb.SaveAs(filename);
                                //}

                                //System.Diagnostics.Process.Start(folderPath + "" + filename);
                                //MessageBox.Show("Export to Excel download sucessfully \n Path = C:\\Users\\Administrator\\Downloads\\Report_Data" + second + ".xlsx");



                                //MemoryStream ms = ExportToExcelEpPlus(dtStock, APIName, tempPath, Convert.ToInt32(apiRequest.TransId), "UploadMethod");

                                //FileStream file = new FileStream(filename, FileMode.CreateNew, FileAccess.Write);
                                //ms.WriteTo(file);
                                //file.Close();
                                //ms.Close();

                                if (ExportType.ToUpper() == "EXCEL(.XLSX)" || ExportType.ToUpper() == "EXCEL")
                                    filename = tempPath + APIName + DATE + ".xlsx";
                                else
                                    filename = tempPath + APIName + DATE + ".xls";

                            }
                            bool status;
                            try
                            {
                                string _email_body_1 = "<div style='font-family: verdana; font-size: 13px;'>" +
                                "<div>Following FTP/API/URL fail</div>" +
                                "<p>&nbsp;</p>" +
                                "<table border='1'>" +
                                "<tbody>" +
                                "<tr>" +
                                "<td style='padding: 3px; background-color: #3e3b3b2b;'><strong>No.</strong></td>" +
                                "<td style='padding: 3px; background-color: #3e3b3b2b;'><strong>FTP Name</strong></td>" +
                                "<td style='padding: 3px; background-color: #3e3b3b2b;'><strong>User Name</strong></td>" +
                                "<td style='padding: 3px; background-color: #3e3b3b2b;'><strong>Company Name</strong></td>" +
                                "</tr>";

                                if (FTPType.ToString().ToUpper() == "FTP")
                                {
                                    status = UploadFileToFTP(filename, FtpHost, FtpUser, FtpPass, FileForUploadServer);
                                    if (status == true)
                                    {
                                        InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, true, "Success", _path + FileForUpload);
                                        return Ok(new CommonResponse
                                        {
                                            Message = "Successfully Upload File To FTP",
                                            Status = "1",
                                            Error = ""
                                        });
                                    }
                                    else if (status == false)
                                    {
                                        _email_body_1 = _email_body_1 + "<tr>" +
                                        "<td style='padding: 2px;'>1.</td>" +
                                        "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["APIName"].ToString() + "</td>" +
                                        "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["Username"].ToString() + "</td>" +
                                        "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["CompanyName"].ToString() + "</td>" +
                                        "</tr>"+
                                        "</tbody>" +
                                        "</table>" +
                                        "</div>";
                                        ErrorEmailSend(_email_body_1);

                                        return Ok(new CommonResponse
                                        {
                                            Message = "Failed Upload File To FTP",
                                            Status = "1",
                                            Error = ""
                                        });
                                    }
                                }
                                if (FTPType.ToString().ToUpper() == "SFTP")
                                {
                                    status = UploadFileToSFTP(filename, FtpHost, FtpUser, FtpPass, FileForUploadServer);
                                    if (status == true)
                                    {
                                        InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, true, "Success", _path + FileForUpload);
                                        return Ok(new CommonResponse
                                        {
                                            Message = "Successfully Upload File To SFTP",
                                            Status = "1",
                                            Error = ""
                                        });
                                    }
                                    else if (status == false)
                                    {
                                        _email_body_1 = _email_body_1 + "<tr>" +
                                        "<td style='padding: 2px;'>1.</td>" +
                                        "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["APIName"].ToString() + "</td>" +
                                        "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["Username"].ToString() + "</td>" +
                                        "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["CompanyName"].ToString() + "</td>" +
                                        "</tr>" +
                                        "</tbody>" +
                                        "</table>" +
                                        "</div>";
                                        ErrorEmailSend(_email_body_1);

                                        return Ok(new CommonResponse
                                        {
                                            Message = "Failed Upload File To FTP",
                                            Status = "1",
                                            Error = ""
                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "GetApiMethodDetails : 404 Record Not Found", "");
                            return Ok(new CommonResponse
                            {
                                Message = "",
                                Status = "0",
                                Error = "404 Record Not Found"
                            });
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "GetApiMethodDetails : " + ex.Message.ToString(), "");
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Error");
            }
        }

        private bool UploadFileToFTP(string source, string ftpurl, string ftpusername, string ftppassword, string FileForUpload)
        {
            try
            {
                string filename = Path.GetFileName(source);
                string ftpfullpath = ftpurl + "/" + FileForUpload;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream fs = File.OpenRead(source);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                return true;
            }
            catch (WebException ex)
            {
                String status = ((FtpWebResponse)ex.Response).StatusDescription; 
                InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "UploadFileToFTP : Status : "+ status + " Message : " + ex.Message.ToString(), "");
                return false;
            }
        }

        private bool UploadFileToSFTP(string source, string ftpurl, string ftpusername, string ftppassword, string FileForUpload)
        {
            try
            {
                var host = ftpurl;
                var port = 22;
                var username = ftpusername;
                var password = ftppassword;
                string filename = Path.GetFileName(source);
                // path for file you want to upload

                string ftpfullpath = ftpurl;// + "/" + filename;
                                            //ftpfullpath = "180.235.132.173";

                using (var client = new SftpClient(ftpfullpath, port, username, password))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        using (var fileStream = new FileStream(source, FileMode.Open))
                        {

                            client.BufferSize = 4 * 1024; // bypass Payload error large files
                            client.UploadFile(fileStream, FileForUpload);
                        }
                    }
                }
                return true;
            }
            catch (WebException ex)
            {
                String status = ((FtpWebResponse)ex.Response).StatusDescription;
                InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "UploadFileToSFTP : Status : " + status + " Message : " + ex.Message.ToString(), "");
                return false;
            }


        }

        private static void ErrorEmailSend(string _email_body)
        {
            MailMessage xloMail = new MailMessage();
            SmtpClient xloSmtp = new SmtpClient();
            //Database db2 = new Database();
            //string _CustomerName = string.Empty;
            //List<IDbDataParameter> para2 = new List<IDbDataParameter>();
            //para2.Add(db2.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, UserId));
            //DataTable dt3 = db2.ExecuteSP("UserMas_SelectOne", para2.ToArray(), false);
            //if (dt3 != null && dt3.Rows.Count > 0)
            //{
            //    _CustomerName = dt3.Rows[0]["sFirstName"].ToString() + " " + dt3.Rows[0]["sLastName"].ToString();
            //}

            if (ConfigurationManager.AppSettings["Location"] == "H")
                xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
            else
                xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Shairu Gems");

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                xloMail.To.Add(ConfigurationManager.AppSettings["BCCEmail"]);

            xloMail.Bcc.Add("hardik@brainwaves.co.in");
            xloMail.Subject = "FTP/URL/API fail for upload";
            xloMail.Body = _email_body;

            xloMail.IsBodyHtml = true;

            xloSmtp.Timeout = 500000;
            xloSmtp.Send(xloMail);

            xloMail.AlternateViews.Dispose();
            xloMail.Dispose();
        }

        private static void InsertApiMethodLog(int TransId, int UserId, int TotCount, Boolean Status, string Error, string FileName)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("@UserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt64(UserId)));
                para.Add(db.CreateParam("@TransId", DbType.Int32, ParameterDirection.Input, Convert.ToInt64(TransId)));
                para.Add(db.CreateParam("@TotCount", DbType.Int32, ParameterDirection.Input, Convert.ToInt64(TotCount)));
                para.Add(db.CreateParam("@Status", DbType.Boolean, ParameterDirection.Input, Status));
                para.Add(db.CreateParam("@Error", DbType.String, ParameterDirection.Input, Error.ToString()));
                para.Add(db.CreateParam("@FileName", DbType.String, ParameterDirection.Input, FileName.ToString()));
                DataTable dt = db.ExecuteSP("ApiUploadMethodMstLog_Ins", para.ToArray(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult FTPAPIPortalLogList([FromBody]JObject data)
        {
            FTPAPIPortalLogRequest ftpapiportallogrequest = new FTPAPIPortalLogRequest();

            try
            {
                ftpapiportallogrequest = JsonConvert.DeserializeObject<FTPAPIPortalLogRequest>(data.ToString());
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

                if (!string.IsNullOrEmpty(ftpapiportallogrequest.FromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, ftpapiportallogrequest.FromDate));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ftpapiportallogrequest.ToDate))
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, ftpapiportallogrequest.ToDate));
                else
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ftpapiportallogrequest.Distinct.ToString()))
                    para.Add(db.CreateParam("Distinct", DbType.Int32, ParameterDirection.Input, ftpapiportallogrequest.Distinct));
                else
                    para.Add(db.CreateParam("Distinct", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ftpapiportallogrequest.PageNo.ToString()))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, ftpapiportallogrequest.PageNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ftpapiportallogrequest.PageSize.ToString()))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, ftpapiportallogrequest.PageSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ftpapiportallogrequest.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, ftpapiportallogrequest.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ftpapiportallogrequest.IsFTP.ToString()))
                    para.Add(db.CreateParam("IsFTP", DbType.Int32, ParameterDirection.Input, ftpapiportallogrequest.IsFTP));
                else
                    para.Add(db.CreateParam("IsFTP", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ftpapiportallogrequest.CompSearch))
                    para.Add(db.CreateParam("CompSearch", DbType.String, ParameterDirection.Input, ftpapiportallogrequest.CompSearch));
                else
                    para.Add(db.CreateParam("CompSearch", DbType.String, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("ApiUploadMethodLog_Select", para.ToArray(), false);

                List<FTPAPIPortalLogResponse> ListResponses = new List<FTPAPIPortalLogResponse>();
                ListResponses = DataTableExtension.ToList<FTPAPIPortalLogResponse>(dtData);

                if (ListResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<FTPAPIPortalLogResponse>
                    {
                        Data = ListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<FTPAPIPortalLogResponse>
                    {
                        Data = new List<FTPAPIPortalLogResponse>(),
                        Message = "No Record Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<FTPAPIPortalLogResponse>
                {
                    Data = new List<FTPAPIPortalLogResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult UserwiseCompany_select([FromBody]JObject data)
        {
            UserwiseCompany_select userwisecompany_select = new UserwiseCompany_select();

            try
            {
                userwisecompany_select = JsonConvert.DeserializeObject<UserwiseCompany_select>(data.ToString());
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

                if (userwisecompany_select.iUserid > 0)
                    para.Add(db.CreateParam("iUserid", DbType.Int64, ParameterDirection.Input, userwisecompany_select.iUserid));
                else
                    para.Add(db.CreateParam("iUserid", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("UserwiseCompany_select", para.ToArray(), false);

                List<UserwiseCompany_select> list = new List<UserwiseCompany_select>();
                list = DataTableExtension.ToList<UserwiseCompany_select>(dtData);

                if (list.Count > 0)
                {
                    return Ok(new ServiceResponse<UserwiseCompany_select>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<UserwiseCompany_select>
                    {
                        Data = new List<UserwiseCompany_select>(),
                        Message = "No Record Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserwiseCompany_select>
                {
                    Data = new List<UserwiseCompany_select>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        #endregion


        #region This Service calling in 'WS_FTP_SFTP' Windows Service 

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult WS_FTP_SFTP_Call()
        {
            string path = HttpContext.Current.Server.MapPath("~/FTP_Transfer_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            try
            {
                string _M = string.Empty, _FTPList = string.Empty, _SFTPList = string.Empty, _email_body = string.Empty, _error_api_list = "";
                bool Is_FTP_SMTP_FAIL = false;
                int FTP_Error_No = 0;

                _email_body = "<div style='font-family: verdana; font-size: 13px;'>" +
                "<div>Following FTP/API/URL fail</div>" +
                "<p>&nbsp;</p>" +
                "<table border='1'>" +
                "<tbody>" +
                "<tr>" +
                "<td style='padding: 3px; background-color: #3e3b3b2b;'><strong>No.</strong></td>" +
                "<td style='padding: 3px; background-color: #3e3b3b2b;'><strong>FTP Name</strong></td>" +
                "<td style='padding: 3px; background-color: #3e3b3b2b;'><strong>User Name</strong></td>" +
                "<td style='padding: 3px; background-color: #3e3b3b2b;'><strong>Company Name</strong></td>" +
                "</tr>";

                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_FTP_select", para.ToArray(), false);

                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        CommonResponse resp = new CommonResponse();
                        DataTable dtApiDet = ApiUploadMethod_viewAll(1, 1000, Convert.ToInt32(dr["iTransId"].ToString()));

                        if (dtApiDet != null && dtApiDet.Rows.Count > 0)
                        {
                            Log_TransId = Convert.ToInt32(dtApiDet.Rows[0]["iTransId"].ToString());
                            Log_UserId = Convert.ToInt32(dtApiDet.Rows[0]["iUserId"].ToString());

                            DataView _dv = new DataView(dtApiDet);
                            _dv.RowFilter = "APIStatus = 'True'";
                            dtApiDet = _dv.ToTable();

                            if (dtApiDet.Rows.Count == 0)
                            {
                                InsertApiMethodLog(Log_TransId, Log_UserId, 0, false, "GetApiMethodDetails : API is In Active", "");
                            }
                            else
                            {

                                string APIMethod = dtApiDet.Rows[0]["ApiMethod"].ToString();
                                string APIName = dtApiDet.Rows[0]["APIName"].ToString();
                                if (APIMethod.ToString() == "FTP")
                                {
                                    string FtpHost = dtApiDet.Rows[0]["FTPHost"].ToString();
                                    string FtpUser = dtApiDet.Rows[0]["FTPUser"].ToString();
                                    string FtpPass = dtApiDet.Rows[0]["FTPPass"].ToString();
                                    string ExportType = dtApiDet.Rows[0]["FTPExportType"].ToString();
                                    string FTPType = dtApiDet.Rows[0]["FTPType"].ToString();

                                    Database dbnew = new Database();
                                    List<IDbDataParameter> paranew = new List<IDbDataParameter>();
                                    paranew.Add(dbnew.CreateParam("@TransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(dtApiDet.Rows[0]["iTransId"].ToString())));
                                    paranew.Add(dbnew.CreateParam("@UserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(dtApiDet.Rows[0]["iUserId"].ToString())));
                                    DataTable dtStock = dbnew.ExecuteSP("SelectAPIMethodData", paranew.ToArray(), false);

                                    if (dtStock != null && dtStock.Rows.Count > 0)
                                    {
                                        TotCount = dtStock.Rows.Count;
                                        string tempPath = HostingEnvironment.MapPath("~/Temp/FTPAPI_EXPORT/");
                                        string _path = ConfigurationManager.AppSettings["data"];
                                        _path = _path.Replace("/ExcelFile/", "");
                                        _path += "/Temp/FTPAPI_EXPORT/";
                                        string filename = "";
                                        DateTime now = DateTime.Now;
                                        string DATE = " " + now.Day + "" + now.Month + "" + now.Year + "" + now.Hour + "" + now.Minute + "" + now.Second;
                                        string FileForUpload = "";
                                        string FileForUploadServer = "";
                                        if (!Directory.Exists(tempPath))
                                        {
                                            Directory.CreateDirectory(tempPath);
                                        }

                                        if (ExportType.ToUpper() == "CSV")
                                        {
                                            filename = tempPath + APIName + DATE + ".csv";
                                            //  sourcepath = tempPath + APIName + ".csv"; ;
                                            FileForUpload = APIName + DATE + ".csv";
                                            FileForUploadServer = APIName + ".csv";
                                            if (File.Exists(filename))
                                            {
                                                File.Delete(filename);
                                            }


                                            //FileInfo newFile = new FileInfo(filename);
                                            //using (ExcelPackage pck = new ExcelPackage(newFile))
                                            //{
                                            //    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(APIName);
                                            //    pck.Workbook.Properties.Title = "API";
                                            //    ws.Cells["A1"].LoadFromDataTable(dtStock, true);
                                            //    pck.Save();
                                            //}

                                            StringBuilder sb = new StringBuilder();
                                            IEnumerable<string> columnNames = dtStock.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                                            sb.AppendLine(string.Join(",", columnNames));

                                            foreach (DataRow row in dtStock.Rows)
                                            {
                                                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Replace(",", " "));
                                                sb.AppendLine(string.Join(",", fields));
                                            }
                                            File.WriteAllText(filename, sb.ToString());

                                            //  filename = _path + APIName + ".csv";
                                        }
                                        if (ExportType.ToUpper() == "EXCEL(.XLSX)" || ExportType.ToUpper() == "EXCEL(.XLS)" || ExportType.ToUpper() == "EXCEL")
                                        {

                                            if (ExportType.ToUpper() == "EXCEL(.XLSX)" || ExportType.ToUpper() == "EXCEL")
                                            {
                                                filename = tempPath + APIName + DATE + ".xlsx";
                                            }
                                            else
                                            {
                                                filename = tempPath + APIName + DATE + ".xls";
                                            }

                                            if (File.Exists(filename))
                                            {
                                                File.Delete(filename);
                                            }

                                            if (ExportType.ToUpper() == "EXCEL(.XLSX)" || ExportType.ToUpper() == "EXCEL")
                                            {
                                                filename = tempPath + APIName + DATE + ".xlsx";
                                                //sourcepath = tempPath + APIName + ".xlsx";
                                                FileForUpload = APIName + DATE + ".xlsx";
                                                FileForUploadServer = APIName + ".xlsx";
                                            }
                                            else
                                            {
                                                filename = tempPath + APIName + DATE + ".xls";
                                                // sourcepath = tempPath + APIName + ".xls";
                                                FileForUpload = APIName + DATE + ".xls";
                                                FileForUploadServer = APIName + ".xls";
                                            }

                                            FileInfo newFile = new FileInfo(filename);
                                            using (ExcelPackage pck = new ExcelPackage(newFile))
                                            {
                                                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(APIName);
                                                pck.Workbook.Properties.Title = "API";
                                                ws.Cells["A1"].LoadFromDataTable(dtStock, true);

                                                ws.View.FreezePanes(2, 1);
                                                var allCells = ws.Cells[ws.Dimension.Address];
                                                allCells.AutoFilter = true;
                                                allCells.AutoFitColumns();

                                                int rowStart = ws.Dimension.Start.Row;
                                                int rowEnd = ws.Dimension.End.Row;
                                                removingGreenTagWarning(ws, ws.Cells[1, 1, rowEnd, 100].Address);
                                                string ColCaption = "";
                                                ColCaption = GetColumnUserCaption("Cts", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("Rap Price($)", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("Rap Amt($)", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("Disc(%)", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("Net Amt($)", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("Length", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("Width", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("Depth", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("DepthPer", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("TablePer", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("CrAng", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("CrHt", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("PavAng", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("PavHt", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }
                                                ColCaption = GetColumnUserCaption("Girdle(%)", Log_TransId);
                                                if (ColCaption != "")
                                                {
                                                    ws.Column(GetColumnByName(ws, ColCaption)).Style.Numberformat.Format = "0.00";
                                                }

                                                var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                                                headerCells.Style.Font.Bold = true;
                                                headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                                headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                                                pck.Save();
                                            }

                                            if (ExportType.ToUpper() == "EXCEL(.XLSX)" || ExportType.ToUpper() == "EXCEL")
                                                filename = tempPath + APIName + DATE + ".xlsx";
                                            else
                                                filename = tempPath + APIName + DATE + ".xls";

                                        }
                                        bool status;
                                        try
                                        {
                                            if (FTPType.ToString().ToUpper() == "FTP")
                                            {
                                                status = UploadFileToFTP(filename, FtpHost, FtpUser, FtpPass, FileForUploadServer);
                                                if (status == true)
                                                {
                                                    InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, true, "Success", _path + FileForUpload);
                                                    _FTPList += FileForUploadServer + ", ";
                                                }
                                                else if (status == false)
                                                {
                                                    Is_FTP_SMTP_FAIL = true;
                                                    FTP_Error_No = FTP_Error_No + 1;
                                                    _error_api_list = _error_api_list + FTP_Error_No + "." + dtApiDet.Rows[0]["APIName"].ToString() + " & " + dtApiDet.Rows[0]["Username"].ToString() + ", ";

                                                    _email_body = _email_body + "<tr>" +
                                                    "<td style='padding: 2px;'>"+ FTP_Error_No + ".</td>" +
                                                    "<td style='padding: 2px;'>"+ dtApiDet.Rows[0]["APIName"].ToString() + "</td>" +
                                                    "<td style='padding: 2px;'>"+ dtApiDet.Rows[0]["Username"].ToString() + "</td>" +
                                                    "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["CompanyName"].ToString() + "</td>" +
                                                    "</tr>";
                                                }
                                            }
                                            if (FTPType.ToString().ToUpper() == "SFTP")
                                            {
                                                status = UploadFileToSFTP(filename, FtpHost, FtpUser, FtpPass, FileForUploadServer);
                                                if (status == true)
                                                {
                                                    InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, true, "Success", _path + FileForUpload);
                                                    _SFTPList += FileForUploadServer + ", ";
                                                }
                                                else if (status == false)
                                                {
                                                    Is_FTP_SMTP_FAIL = true;
                                                    FTP_Error_No = FTP_Error_No + 1;

                                                    _error_api_list = _error_api_list + FTP_Error_No + "." + dtApiDet.Rows[0]["APIName"].ToString() + " & " + dtApiDet.Rows[0]["Username"].ToString() + ", "; 
                                                    
                                                    _email_body = _email_body + "<tr>" +
                                                    "<td style='padding: 2px;'>" + FTP_Error_No + ".</td>" +
                                                    "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["APIName"].ToString() + "</td>" +
                                                    "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["Username"].ToString() + "</td>" +
                                                    "<td style='padding: 2px;'>" + dtApiDet.Rows[0]["CompanyName"].ToString() + "</td>" +
                                                    "</tr>";
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }
                                    else
                                    {
                                        InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "GetApiMethodDetails : 404 Record Not Found", "");
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        InsertApiMethodLog(Log_TransId, Log_UserId, TotCount, false, "GetApiMethodDetails : " + ex.Message.ToString(), "");
                        return Ok(new CommonResponse
                        {
                            Message = ex.Message,
                            Status = "0",
                            Error = ""
                        });
                    }
                }
                _email_body = _email_body + "</tbody>" +
                "</table>" +
                "</div>";

                if (Is_FTP_SMTP_FAIL == true)
                {
                    ErrorEmailSend(_email_body);
                }

                _M = (_FTPList != "" && _FTPList != null ? "FTP API Successfully Hit File Name : " + _FTPList.Remove(_FTPList.Length - 2) : "");
                _M += (_SFTPList != "" && _SFTPList != null
                    ?
                    (_FTPList != "" && _FTPList != null ? " AND " : "") +
                    "SFTP API Successfully Hit File Name : " + _SFTPList.Remove(_SFTPList.Length - 2)
                    : "");

                _M += (_error_api_list != "" && _error_api_list != null ? (_M != "" && _M != null ? " and FTP/URL/API fail for upload " : "FTP/URL/API fail for upload ") + _error_api_list.Remove(_error_api_list.Length - 2) : "");

                Database db1 = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para1 = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
                para1.Clear();

                para1.Add(db1.CreateParam("Message", System.Data.DbType.String, System.Data.ParameterDirection.Input, (_M != "" && _M != null ? _M : "SUCCESS")));
                System.Data.DataTable dtUserDetail = db1.ExecuteSP("FTP_Transfer_History_Insert", para1.ToArray(), false);

                StringBuilder _sb = new StringBuilder();
                _sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                _sb.Append((_M != "" && _M != null ? _M : "SUCCESS") + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                _sb.AppendLine("");
                File.AppendAllText(path, _sb.ToString());
                _sb.Clear();

                return Ok(new CommonResponse
                {
                    Message = (_M != "" && _M != null ? _M : "SUCCESS"),
                    Status = "1",
                    Error = ""
                });
            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse
                {
                    Message = ex.StackTrace,
                    Status = "0",
                    Error = ""
                });
            }
        }

        #endregion


        [HttpPost]
        public IHttpActionResult Get_UploadMethodReport([FromBody]JObject data)
        {
            CustomerDiscReq req = new CustomerDiscReq();
            try
            {
                req = JsonConvert.DeserializeObject<CustomerDiscReq>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CustomerDiscResponse>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(req.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("PageNo", DbType.Int32, ParameterDirection.Input, req.PageNo));
                para.Add(db.CreateParam("PageSize", DbType.Int32, ParameterDirection.Input, req.PageSize));

                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_Report", para.ToArray(), false);

                List<GetStockDiscRes> getstockdiscres = new List<GetStockDiscRes>();
                getstockdiscres = DataTableExtension.ToList<GetStockDiscRes>(dt);

                if (getstockdiscres != null && getstockdiscres.Count > 0)
                {
                    return Ok(new ServiceResponse<GetStockDiscRes>
                    {
                        Data = getstockdiscres,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<GetStockDiscRes>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<GetStockDiscRes>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Excel_UploadMethodReport([FromBody]JObject data)
        {
            CustomerDiscReq req = new CustomerDiscReq();
            try
            {
                req = JsonConvert.DeserializeObject<CustomerDiscReq>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CustomerDiscResponse>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(req.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("PageNo", DbType.Int32, ParameterDirection.Input, req.PageNo));
                para.Add(db.CreateParam("PageSize", DbType.Int32, ParameterDirection.Input, req.PageSize));

                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_Report", para.ToArray(), false);

                dt.DefaultView.RowFilter = "RowNo IS NOT NULL";
                dt = dt.DefaultView.ToTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    string filename = "Client FTP & API Portal Report " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    EpExcelExport.CreateUploadMethodReportExcel(dt.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);

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
    }
}
