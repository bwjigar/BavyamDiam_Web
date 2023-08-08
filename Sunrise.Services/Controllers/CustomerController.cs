using DAL;
using EpExcelExportLib;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Sunrise.Services.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Xml;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        public static int TotCount = 0;
        [HttpPost]
        public IHttpActionResult GetCustomer([FromBody]JObject data)
        {
            CustomerReq req = new CustomerReq();
            try
            {
                req = JsonConvert.DeserializeObject<CustomerReq>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Customer>
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

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("EmpId", DbType.Int32, ParameterDirection.Input, userID));

                if (!string.IsNullOrEmpty(req.SearchText))
                    para.Add(db.CreateParam("SearchText", DbType.String, ParameterDirection.Input, req.SearchText));
                else
                    para.Add(db.CreateParam("SearchText", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("UserMas_SelectByAssist_CompanyUserCustomerWise", para.ToArray(), false);
                List<Customer> customerList = new List<Customer>();
                customerList = DataTableExtension.ToList<Customer>(dt);

                if (customerList != null && customerList.Count > 0)
                {
                    return Ok(new ServiceResponse<Customer>
                    {
                        Data = customerList,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Customer>
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
                return Ok(new ServiceResponse<Customer>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetCustomerDisc([FromBody]JObject data)
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

                DataTable dt = db.ExecuteSP("CustomerDisc_Select", para.ToArray(), false);

                SearchSummary searchSummary = new SearchSummary();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] dra = dt.Select("RowNo IS NULL");
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["CustDiscId"]);
                }

                dt.DefaultView.RowFilter = "RowNo IS NOT NULL";
                dt = dt.DefaultView.ToTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<CustomerDisc> customerList = new List<CustomerDisc>();
                    customerList = DataTableExtension.ToList<CustomerDisc>(dt);
                    List<CustomerDiscResponse> customerResponses = new List<CustomerDiscResponse>();

                    customerResponses.Add(new CustomerDiscResponse()
                    {
                        DataList = customerList,
                        DataSummary = searchSummary
                    });

                    return Ok(new ServiceResponse<CustomerDiscResponse>
                    {
                        Data = customerResponses,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CustomerDiscResponse>
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
                return Ok(new ServiceResponse<CustomerDiscResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult SaveCustomerDisc([FromBody]JObject data)
        {
            CustomerDiscReq req = new CustomerDiscReq();
            try
            {
                req = JsonConvert.DeserializeObject<CustomerDiscReq>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CustomerDisc>
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

                req.xmlStr = req.xmlStr.Replace("%3C", "<");
                req.xmlStr = req.xmlStr.Replace("%3E", ">");
                req.xmlStr = req.xmlStr.Replace("%2C", ",");
                req.xmlStr = req.xmlStr.Replace("%20", " ");
                req.xmlStr = req.xmlStr.Replace("%28", "(");
                req.xmlStr = req.xmlStr.Replace("%29", ")");

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("loggedUserId", DbType.Int32, ParameterDirection.Input, userID));
                para.Add(db.CreateParam("CustId", DbType.String, ParameterDirection.Input, req.CustId));
                para.Add(db.CreateParam("TransId", DbType.Int32, ParameterDirection.Input, req.TransId));
                para.Add(db.CreateParam("Oper", DbType.String, ParameterDirection.Input, req.Oper));
                para.Add(db.CreateParam("Input", DbType.String, ParameterDirection.Input, req.xmlStr));

                DataTable dt = db.ExecuteSP("CustomerDisc_Crud", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Msg"].ToString() == "success")
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = dt.Rows[0]["Msg"].ToString(),
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = dt.Rows[0]["Msg"].ToString(),
                            Status = "0"
                        });
                    }
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
                return Ok(new ServiceResponse<CustomerDisc>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetPartyInfo([FromBody]JObject data)
        {
            PartyInfoReq req = new PartyInfoReq();
            try
            {
                req = JsonConvert.DeserializeObject<PartyInfoReq>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<PartyInfoResponse>
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

                if (!string.IsNullOrEmpty(req.PartyName))
                    para.Add(db.CreateParam("sPartyName", DbType.String, ParameterDirection.Input, req.PartyName));
                else
                    para.Add(db.CreateParam("sPartyName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ContactPerson))
                    para.Add(db.CreateParam("sContactPerson", DbType.String, ParameterDirection.Input, req.ContactPerson));
                else
                    para.Add(db.CreateParam("sContactPerson", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PartyPrefix))
                    para.Add(db.CreateParam("sPartyPrefix", DbType.String, ParameterDirection.Input, req.PartyPrefix));
                else
                    para.Add(db.CreateParam("sPartyPrefix", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (req.CountryId > 0)
                    para.Add(db.CreateParam("sCountryId", DbType.Int32, ParameterDirection.Input, req.CountryId));
                else
                    para.Add(db.CreateParam("sCountryId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (req.PageNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, req.PageNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (req.PageSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, req.PageSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("PartyInfo_SelectByPara", para.ToArray(), false);
                List<PartyInfoResponse> partyInfoList = new List<PartyInfoResponse>();
                partyInfoList = DataTableExtension.ToList<PartyInfoResponse>(dt);

                if (partyInfoList != null && partyInfoList.Count > 0)
                {
                    return Ok(new ServiceResponse<PartyInfoResponse>
                    {
                        Data = partyInfoList,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<PartyInfoResponse>
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
                return Ok(new ServiceResponse<PartyInfoResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetSupplier([FromBody]JObject data)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("Overseas_Supplier", para.ToArray(), false);
                List<PartyInfoResponse> partyInfoList = new List<PartyInfoResponse>();
                partyInfoList = DataTableExtension.ToList<PartyInfoResponse>(dt);

                if (partyInfoList != null && partyInfoList.Count > 0)
                {
                    return Ok(new ServiceResponse<PartyInfoResponse>
                    {
                        Data = partyInfoList,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<PartyInfoResponse>
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
                return Ok(new ServiceResponse<PartyInfoResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetUserDisc([FromBody]JObject data)
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

                DataTable dt = db.ExecuteSP("UserDisc_Select", para.ToArray(), false);

                dt.DefaultView.RowFilter = "iTransId IS NOT NULL";
                dt = dt.DefaultView.ToTable();

                SearchSummary searchSummary = new SearchSummary();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] dra = dt.Select("RowNo IS NULL");
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["iTransId"]);
                }

                dt.DefaultView.RowFilter = "RowNo IS NOT NULL";
                dt = dt.DefaultView.ToTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<CustomerDisc> customerList = new List<CustomerDisc>();
                    customerList = DataTableExtension.ToList<CustomerDisc>(dt);
                    List<CustomerDiscResponse> customerResponses = new List<CustomerDiscResponse>();

                    customerResponses.Add(new CustomerDiscResponse()
                    {
                        DataList = customerList,
                        DataSummary = searchSummary
                    });

                    return Ok(new ServiceResponse<CustomerDiscResponse>
                    {
                        Data = customerResponses,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CustomerDiscResponse>
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
                return Ok(new ServiceResponse<CustomerDiscResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_StockDiscMgtReport([FromBody]JObject data)
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

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, userID));

                if (!string.IsNullOrEmpty(req.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("PageNo", DbType.Int32, ParameterDirection.Input, req.PageNo));
                para.Add(db.CreateParam("PageSize", DbType.Int32, ParameterDirection.Input, req.PageSize));

                DataTable dt = db.ExecuteSP("StockDiscMgt_Select", para.ToArray(), false);

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
        public IHttpActionResult Excel_StockDiscMgtReport([FromBody]JObject data)
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

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, userID));

                if (!string.IsNullOrEmpty(req.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("PageNo", DbType.Int32, ParameterDirection.Input, req.PageNo));
                para.Add(db.CreateParam("PageSize", DbType.Int32, ParameterDirection.Input, req.PageSize));

                DataTable dt = db.ExecuteSP("StockDiscMgt_Select", para.ToArray(), false);

                dt.DefaultView.RowFilter = "RowNo IS NOT NULL";
                dt = dt.DefaultView.ToTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    string filename = "Stock & Disc Mgt. Report " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    EpExcelExport.CreateStockDiscExcel(dt.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);

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
        public IHttpActionResult GetUserDisc_Excel([FromBody]JObject data)
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

                DataTable dt = db.ExecuteSP("UserDisc_Select", para.ToArray(), false);

                dt.DefaultView.RowFilter = "iTransId IS NOT NULL";
                dt = dt.DefaultView.ToTable();

                SearchSummary searchSummary = new SearchSummary();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] dra = dt.Select("RowNo IS NULL");
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["iTransId"]);
                }

                dt.DefaultView.RowFilter = "RowNo IS NOT NULL";
                dt = dt.DefaultView.ToTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    string filename = "Stock & Disc Mgt. Report " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    EpExcelExport.CreateUserDiscExcel(dt.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);

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
        public IHttpActionResult SaveUserDisc([FromBody]JObject data)
        {
            CustomerDiscReq req = new CustomerDiscReq();
            try
            {
                req = JsonConvert.DeserializeObject<CustomerDiscReq>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CustomerDisc>
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

                req.xmlStr = req.xmlStr.Replace("%3C", "<");
                req.xmlStr = req.xmlStr.Replace("%3E", ">");
                req.xmlStr = req.xmlStr.Replace("%2C", ",");
                req.xmlStr = req.xmlStr.Replace("%20", " ");

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("loggedUserId", DbType.Int32, ParameterDirection.Input, userID));
                para.Add(db.CreateParam("CustId", DbType.String, ParameterDirection.Input, req.CustId));
                para.Add(db.CreateParam("TransId", DbType.Int32, ParameterDirection.Input, req.TransId));
                para.Add(db.CreateParam("Oper", DbType.String, ParameterDirection.Input, req.Oper));
                para.Add(db.CreateParam("Input", DbType.String, ParameterDirection.Input, req.xmlStr));

                DataTable dt = db.ExecuteSP("UserDisc_Crud", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Msg"].ToString() == "success")
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = dt.Rows[0]["Msg"].ToString(),
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = dt.Rows[0]["Msg"].ToString(),
                            Status = "0"
                        });
                    }
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
                return Ok(new ServiceResponse<CustomerDisc>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult SaveStockDisc([FromBody]JObject data)
        {
            SaveStockDiscReq savestockdiscreq = new SaveStockDiscReq();
            try
            {
                savestockdiscreq = JsonConvert.DeserializeObject<SaveStockDiscReq>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SaveStockDiscReq>
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
                para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, savestockdiscreq.Type));

                if (!string.IsNullOrEmpty(savestockdiscreq.Id.ToString()))
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, savestockdiscreq.Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(savestockdiscreq.UserIdList))
                    para.Add(db.CreateParam("UserIdList", DbType.String, ParameterDirection.Input, savestockdiscreq.UserIdList));
                else
                    para.Add(db.CreateParam("UserIdList", DbType.String, ParameterDirection.Input, DBNull.Value));

                string savestockdisc_filters = IPadCommon.ToXML<List<SaveStockDisc_Filters>>(savestockdiscreq.Filters);
                para.Add(db.CreateParam("Filters", DbType.String, ParameterDirection.Input, savestockdisc_filters));

                DataTable dtData = db.ExecuteSP("StockDiscMgt_Save", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    if (dtData.Rows[0]["Status"].ToString() == "1")
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = dtData.Rows[0]["Message"].ToString(),
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = dtData.Rows[0]["Message"].ToString(),
                            Status = "0"
                        });
                    }
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
                return Ok(new ServiceResponse<SaveStockDiscReq>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_StockDiscMgt([FromBody]JObject data)
        {
            StockDiscMgtRequest stockdiscmgtrequest = new StockDiscMgtRequest();
            try
            {
                stockdiscmgtrequest = JsonConvert.DeserializeObject<StockDiscMgtRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<StockDiscMgtRequest>
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

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, userID));

                if (!string.IsNullOrEmpty(stockdiscmgtrequest.UserList))
                    para.Add(db.CreateParam("UserList", DbType.String, ParameterDirection.Input, stockdiscmgtrequest.UserList));
                else
                    para.Add(db.CreateParam("UserList", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(stockdiscmgtrequest.sOrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, stockdiscmgtrequest.sOrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, stockdiscmgtrequest.iPgNo));
                para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, stockdiscmgtrequest.iPgSize));

                DataTable dt = db.ExecuteSP("Get_UserDiscUserList", para.ToArray(), false);
                List<StockDiscMgtResponse> stockdiscmgtresponse = new List<StockDiscMgtResponse>();
                stockdiscmgtresponse = DataTableExtension.ToList<StockDiscMgtResponse>(dt);

                if (stockdiscmgtresponse != null && stockdiscmgtresponse.Count > 0)
                {
                    return Ok(new ServiceResponse<StockDiscMgtResponse>
                    {
                        Data = stockdiscmgtresponse,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<StockDiscMgtResponse>
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
                return Ok(new ServiceResponse<StockDiscMgtResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult ImportStockDisc([FromBody]JObject data)
        {
            StockImportList objLst = new StockImportList();
            try
            {
                objLst = JsonConvert.DeserializeObject<StockImportList>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                DataTable dt = new DataTable();
                dt.Columns.Add("UserName", typeof(string));
                dt.Columns.Add("Supplier", typeof(string));
                dt.Columns.Add("Download", typeof(string));
                dt.Columns.Add("View", typeof(string));
                dt.Columns.Add("PriceMethod", typeof(string));
                dt.Columns.Add("PricePer", typeof(string));

                if (objLst.StockImport.Count() > 0)
                {
                    for (int i = 0; i < objLst.StockImport.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["UserName"] = objLst.StockImport[i].UserName.ToString();
                        dr["Supplier"] = objLst.StockImport[i].Supplier.ToString();
                        dr["Download"] = objLst.StockImport[i].Download.ToString();
                        dr["View"] = objLst.StockImport[i].View.ToString();
                        dr["PriceMethod"] = objLst.StockImport[i].PriceMethod.ToString();
                        dr["PricePer"] = objLst.StockImport[i].PricePer.ToString();

                        dt.Rows.Add(dr);
                    }

                    Database db = new Database(Request);
                    DataTable dtData = new DataTable();
                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dtData = db.ExecuteSP("ImportStockDisc_Insert", para.ToArray(), false);

                    if (dtData != null && dtData.Rows.Count > 0 && dtData.Rows[0]["Status"].ToString() == "1")
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = "Stock & Disc Import Successfully",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = "Stock & Disc Import Fail",
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = "No Record will be Proceed",
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
        public IHttpActionResult Get_SupplierPrefix([FromBody]JObject data)
        {
            SuppPrefix_Request req = new SuppPrefix_Request();
            try
            {
                req = JsonConvert.DeserializeObject<SuppPrefix_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SuppPrefix_Response>
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

                para.Add(db.CreateParam("SupplierPriceList_Id", DbType.String, ParameterDirection.Input, req.SupplierPriceList_Id));

                DataTable dt = db.ExecuteSP("SupplierPrefix_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<SuppPrefix_Response> get_suppprefix = new List<SuppPrefix_Response>();
                    get_suppprefix = DataTableExtension.ToList<SuppPrefix_Response>(dt);

                    return Ok(new ServiceResponse<SuppPrefix_Response>
                    {
                        Data = get_suppprefix,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<SuppPrefix_Response>
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
                return Ok(new ServiceResponse<SuppPrefix_Response>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Save_SuppPrefix([FromBody]JObject data)
        {
            Save_SuppPrefix_Request req = new Save_SuppPrefix_Request();
            try
            {
                req = JsonConvert.DeserializeObject<Save_SuppPrefix_Request>(data.ToString());
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
                DataTable dt = new DataTable();
                dt.Columns.Add("SupplierId", typeof(string));
                dt.Columns.Add("Pointer_Id", typeof(string));
                dt.Columns.Add("Location_Id", typeof(string));
                dt.Columns.Add("Prefix", typeof(string));

                if (req.SuppPre.Count() > 0)
                {
                    for (int i = 0; i < req.SuppPre.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["SupplierId"] = req.SuppPre[i].Supplier_Id.ToString();
                        dr["Pointer_Id"] = req.SuppPre[i].Pointer_Id.ToString();
                        dr["Location_Id"] = req.SuppPre[i].Location_Id.ToString();
                        dr["Prefix"] = req.SuppPre[i].Prefix.ToString();

                        dt.Rows.Add(dr);
                    }
                }

                Database db = new Database();
                DataTable dtData = new DataTable();
                List<SqlParameter> para = new List<SqlParameter>();

                SqlParameter param = new SqlParameter("tabledt", SqlDbType.Structured);
                param.Value = dt;
                para.Add(param);

                dtData = db.ExecuteSP("Supplier_Prefix_CRUD", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0 && dtData.Rows[0]["Status"].ToString() == "1")
                {
                    return Ok(new CommonResponse
                    {
                        Error = null,
                        Message = dtData.Rows[0]["Id"].ToString() + "_414_" + dtData.Rows[0]["Message"].ToString(),
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = null,
                        Message = "Prefix Set Fail",
                        Status = "0"
                    });
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
        public IHttpActionResult Delete_SuppPrefix([FromBody]JObject data)
        {
            SuppPrefix_Request req = new SuppPrefix_Request();
            try
            {
                req = JsonConvert.DeserializeObject<SuppPrefix_Request>(data.ToString());
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
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Supplier_Id", DbType.String, ParameterDirection.Input, req.Supplier_Id));

                DataTable dt = db.ExecuteSP("SupplierPrefix_Delete", para.ToArray(), false);

                return Ok(new CommonResponse
                {
                    Error = null,
                    Message = "SUCCESS",
                    Status = "1"
                });
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
        public IHttpActionResult StockUpload([FromBody]JObject data)
        {
            StockUploadRequest req = new StockUploadRequest();
            try
            {
                req = JsonConvert.DeserializeObject<StockUploadRequest>(data.ToString());
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
                string filename = req.connString;
                string MapPath = HostingEnvironment.MapPath("~/ExcelFile/");
                string filePath = MapPath + filename;

                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                DataTable dtData = Utility.ConvertXSLXtoDataTable("", connString);
                DataTable dtClone = new DataTable();

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataColumn Col = dtData.Columns.Add("SUPPLIER_ID", System.Type.GetType("System.String"));
                    Col.SetOrdinal(0);// to put the column in position 0;
                    foreach (DataRow row in dtData.Rows)
                    {
                        row.SetField("SUPPLIER_ID", req.Supplier);
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            if (row[i].ToString() == "")
                            {
                                row[i] = DBNull.Value;
                            }
                        }
                    }

                    DataColumn newColumn = new System.Data.DataColumn("TransferType", typeof(System.String));
                    newColumn.DefaultValue = "MANUAL";
                    dtData.Columns.Add(newColumn);

                    dtClone = dtData.Clone(); //just copy structure, no data
                    for (int i = 0; i < dtClone.Columns.Count; i++)
                    {
                        if (dtClone.Columns[i].DataType != typeof(string))
                            dtClone.Columns[i].DataType = typeof(string);
                    }
                    foreach (DataRow dr in dtData.Rows)
                    {
                        dtClone.ImportRow(dr);
                    }


                    if (dtClone != null && dtClone.Rows.Count > 0)
                    {
                        Database db = new Database(Request);
                        List<SqlParameter> para = new List<SqlParameter>();

                        SqlParameter param = new SqlParameter("tabledt", SqlDbType.Structured);
                        param.Value = dtClone;
                        para.Add(param);

                        DataTable dt = db.ExecuteSP("ManualAuto_StockDetail_Ora_Insert", para.ToArray(), false);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return Ok(new CommonResponse
                            {
                                Error = null,
                                Message = dt.Rows[0]["Message"].ToString(),
                                Status = dt.Rows[0]["Status"].ToString()
                            });
                        }
                        else
                        {
                            return Ok(new CommonResponse
                            {
                                Error = null,
                                Message = "Stock Upload in Issue",
                                Status = "0"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Error = null,
                            Message = "Stock Not Found",
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = null,
                        Message = "Stock Not Found",
                        Status = "0"
                    });
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
        public IHttpActionResult Get_SupplierMaster([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SuppPrefix_Response>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.Id > 0)
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.Search))
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, get_apiuploadmst.Search));
                else
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, get_apiuploadmst.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("For_RefNo_Pricing", DbType.Boolean, ParameterDirection.Input, get_apiuploadmst.For_RefNo_Pricing));

                DataTable dt = db.ExecuteSP("SupplierMaster_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_APIMst_Response> list = new List<Get_APIMst_Response>();
                    list = DataTableExtension.ToList<Get_APIMst_Response>(dt);

                    return Ok(new ServiceResponse<Get_APIMst_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_APIMst_Response>
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
                return Ok(new ServiceResponse<SuppPrefix_Response>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult SaveSupplierMaster([FromBody]JObject data)
        {
            Save_APIMst_Request save_apimst_req = new Save_APIMst_Request();
            try
            {
                save_apimst_req = JsonConvert.DeserializeObject<Save_APIMst_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                CommonResponse resp = new CommonResponse();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, save_apimst_req.Id));
                para.Add(db.CreateParam("SupplierURL", DbType.String, ParameterDirection.Input, save_apimst_req.SupplierURL));
                para.Add(db.CreateParam("SupplierName", DbType.String, ParameterDirection.Input, save_apimst_req.SupplierName));

                para.Add(db.CreateParam("SupplierResponseFormat", DbType.String, ParameterDirection.Input, save_apimst_req.SupplierResponseFormat));
                para.Add(db.CreateParam("FileName", DbType.String, ParameterDirection.Input, save_apimst_req.FileName));
                para.Add(db.CreateParam("FileLocation", DbType.String, ParameterDirection.Input, save_apimst_req.FileLocation));
                para.Add(db.CreateParam("LocationExportType", DbType.String, ParameterDirection.Input, save_apimst_req.LocationExportType));
                para.Add(db.CreateParam("RepeateveryType", DbType.String, ParameterDirection.Input, save_apimst_req.RepeateveryType));
                para.Add(db.CreateParam("Repeatevery", DbType.String, ParameterDirection.Input, save_apimst_req.Repeatevery));
                para.Add(db.CreateParam("Active", DbType.Boolean, ParameterDirection.Input, save_apimst_req.Active));
                para.Add(db.CreateParam("DiscInverse", DbType.Boolean, ParameterDirection.Input, save_apimst_req.DiscInverse));
                para.Add(db.CreateParam("NewRefNoGen", DbType.Boolean, ParameterDirection.Input, save_apimst_req.NewRefNoGen));
                para.Add(db.CreateParam("NewDiscGen", DbType.Boolean, ParameterDirection.Input, save_apimst_req.NewDiscGen));

                if (!string.IsNullOrEmpty(save_apimst_req.SupplierAPIMethod))
                    para.Add(db.CreateParam("SupplierAPIMethod", DbType.String, ParameterDirection.Input, save_apimst_req.SupplierAPIMethod));
                else
                    para.Add(db.CreateParam("SupplierAPIMethod", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(save_apimst_req.SupplierHitUrl))
                    para.Add(db.CreateParam("SupplierHitUrl", DbType.String, ParameterDirection.Input, save_apimst_req.SupplierHitUrl));
                else
                    para.Add(db.CreateParam("SupplierHitUrl", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(save_apimst_req.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, save_apimst_req.UserName));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(save_apimst_req.Password))
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, save_apimst_req.Password));
                else
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("SupplierMaster_CRUD", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Id"].ToString() != "0")
                    {
                        return Ok(new CommonResponse
                        {
                            Error = null,
                            Message = dt.Rows[0]["Id"].ToString(),
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Error = null,
                            Message = dt.Rows[0]["Message"].ToString(),
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = null,
                        Message = "No Data Found",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult Get_SuppColSettMas([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SuppColSettMas_Response>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.Id > 0)
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.Search))
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, get_apiuploadmst.Search));
                else
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, get_apiuploadmst.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("SupplierColSettingsMas_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_SuppColSettMas_Response> list = new List<Get_SuppColSettMas_Response>();
                    list = DataTableExtension.ToList<Get_SuppColSettMas_Response>(dt);

                    return Ok(new ServiceResponse<Get_SuppColSettMas_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SuppColSettMas_Response>
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
                return Ok(new ServiceResponse<Get_SuppColSettMas_Response>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Column_Mas_Select([FromBody]JObject data)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("COLUMN_MAS_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_Column_Mas_Response> list = new List<Get_Column_Mas_Response>();
                    list = DataTableExtension.ToList<Get_Column_Mas_Response>(dt);

                    return Ok(new ServiceResponse<Get_Column_Mas_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_Column_Mas_Response>
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
                return Ok(new ServiceResponse<Get_Column_Mas_Response>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult SupplierColSettings_ExistorNot([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.Id));

                DataTable dt = db.ExecuteSP("SupplierColSettings_ExistorNot", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return Ok(new CommonResponse
                    {
                        Error = null,
                        Message = dt.Rows[0]["Id"].ToString(),
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = null,
                        Message = "No data found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_SuppColSettDet([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SuppColSettDet_Response>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("SupplierColSettingsMasId", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.Id));

                DataTable dt = db.ExecuteSP("SupplierColSettingsDet_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_SuppColSettDet_Response> list = new List<Get_SuppColSettDet_Response>();
                    list = DataTableExtension.ToList<Get_SuppColSettDet_Response>(dt);

                    return Ok(new ServiceResponse<Get_SuppColSettDet_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SuppColSettDet_Response>
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
                return Ok(new ServiceResponse<Get_SuppColSettDet_Response>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Save_SuppColSettMas([FromBody]JObject data)
        {
            Save_SuppColSettMas_Request save_supcolsetmas = new Save_SuppColSettMas_Request();
            try
            {
                save_supcolsetmas = JsonConvert.DeserializeObject<Save_SuppColSettMas_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Supplier_Mas_Id", typeof(string));
                dt.Columns.Add("SupplierColumnName", typeof(string));
                dt.Columns.Add("Column_Mas_Id", typeof(string));
                dt.Columns.Add("DisplayOrder", typeof(string));
                dt.Columns.Add("DateFormatColumn", typeof(string));

                if (save_supcolsetmas.SuppColSett.Count() > 0)
                {
                    for (int i = 0; i < save_supcolsetmas.SuppColSett.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["Supplier_Mas_Id"] = save_supcolsetmas.SuppColSett[i].Supplier_Mas_Id.ToString();
                        dr["SupplierColumnName"] = save_supcolsetmas.SuppColSett[i].SupplierColumnName;
                        dr["Column_Mas_Id"] = save_supcolsetmas.SuppColSett[i].Column_Mas_Id.ToString();
                        dr["DisplayOrder"] = save_supcolsetmas.SuppColSett[i].DisplayOrder.ToString();
                        dr["DateFormatColumn"] = save_supcolsetmas.SuppColSett[i].DateFormatColumn;
                        
                        dt.Rows.Add(dr);
                    }
                }

                Database db = new Database();
                DataTable dtData = new DataTable();
                List<SqlParameter> para = new List<SqlParameter>();

                SqlParameter param = new SqlParameter("tableCol", SqlDbType.Structured);
                param.Value = dt;
                para.Add(param);

                dtData = db.ExecuteSP("SupplierColSettings_CRUD", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0 && dtData.Rows[0]["Status"].ToString() == "1")
                {
                    return Ok(new CommonResponse
                    {
                        Error = null,
                        Message = dtData.Rows[0]["Id"].ToString() + "_414_" + dtData.Rows[0]["Message"].ToString(),
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = null,
                        Message = "",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult SupplierColumnsGetFromAPI([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnsFromAPI_Response>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            int Supplier_Mas_Id = 0;
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.Id));
                DataTable dtAPI = db.ExecuteSP("SupplierMaster_select", para.ToArray(), false);

                if (dtAPI != null && dtAPI.Rows.Count > 0)
                {
                    TotCount = dtAPI.Rows.Count;
                    try
                    {
                        Supplier_Mas_Id = Convert.ToInt32(dtAPI.Rows[0]["Id"].ToString());

                        string _API = String.Empty, UserName = String.Empty, Password = String.Empty, filename = String.Empty, filefullpath = String.Empty;

                        DataTable dt_APIRes = new DataTable();

                        #region

                        //if (dtAPI.Rows[0]["SupplierResponseFormat"].ToString().ToUpper() == "XML")
                        //{
                        //    _API = dtAPI.Rows[0]["SupplierURL"].ToString();
                        //    string[] words = _API.Split('?');
                        //    String InputPara = string.Empty;
                        //    if (words.Length == 2)
                        //    {
                        //        InputPara = words[1].ToString();
                        //    }

                        //    WebClient client = new WebClient();
                        //    client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                        //    client.Encoding = Encoding.UTF8;
                        //    ServicePointManager.Expect100Continue = false;
                        //    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                        //    string xml = client.UploadString(_API, InputPara);
                        //    ConvertXmlStringToDataTable xDt = new ConvertXmlStringToDataTable();
                        //    XmlDocument doc = new XmlDocument();
                        //    doc.LoadXml(xml);
                        //    XmlElement root = doc.DocumentElement;
                        //    XmlNodeList elemList = root.GetElementsByTagName("Row");
                        //    dt_APIRes = xDt.ConvertXmlNodeListToDataTable(elemList);
                        //}
                        //else if (dtAPI.Rows[0]["SupplierResponseFormat"].ToString().ToUpper() == "JSON")
                        //{
                        //    if (dtAPI.Rows[0]["SupplierAPIMethod"].ToString().ToUpper() == "POST")
                        //    {
                        //        string json = string.Empty, Token = string.Empty;
                        //        _API = dtAPI.Rows[0]["SupplierURL"].ToString();
                        //        string[] words = _API.Split('?');
                        //        String InputPara = string.Empty;
                        //        if (words.Length == 2)
                        //        {
                        //            InputPara = words[1].ToString();
                        //        }

                        //        if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://API1.ANKITGEMS.COM:4443/APIUSER/LOGINCHECK")
                        //        {
                        //            //string Name = dtAPI.Rows[0]["UserName"].ToString();
                        //            //string password = dtAPI.Rows[0]["Password"].ToString();

                        //            //WebClient client = new WebClient();
                        //            //client.Headers.Add("Content-type", "application/json");
                        //            //client.Encoding = Encoding.UTF8;
                        //            //json = client.UploadString("https://api1.ankitgems.com:4443/apiuser/logincheck?Name=" + Name + "&password=" + password, "POST", "");

                        //            //AnkitGems _data = new AnkitGems();
                        //            //_data = (new JavaScriptSerializer()).Deserialize<AnkitGems>(json);
                        //            //Token = _data.data.accessToken;

                        //            //WebClient client1 = new WebClient();
                        //            //client1.Headers.Add("Authorization", "Bearer " + Token);
                        //            //client1.Headers.Add("Content-type", "application/json");
                        //            //client1.Encoding = Encoding.UTF8;
                        //            //json = client1.UploadString("https://api1.ankitgems.com:4443/apistock/stockdetail?page=1&limit=10000", "POST", "");

                        //            //JObject o = JObject.Parse(json);
                        //            //var t = string.Empty;
                        //            //if (o != null)
                        //            //{
                        //            //    var test = o.First;
                        //            //    if (test != null)
                        //            //    {
                        //            //        var test2 = test.First;
                        //            //        if (test2 != null)
                        //            //        {
                        //            //            Console.Write(test2);
                        //            //            t = test2.Root.Last.First.First.First.ToString();
                        //            //        }
                        //            //    }
                        //            //}
                        //            //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                        //            //json = JsonConvert.SerializeObject(json_1);
                        //        }
                        //        else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATA")
                        //        {
                        //            //string Name = dtAPI.Rows[0]["UserName"].ToString();
                        //            //string password = dtAPI.Rows[0]["Password"].ToString();

                        //            //SGLoginRequest sgl = new SGLoginRequest();
                        //            //sgl.UserName = Name;
                        //            //sgl.Password = password;

                        //            //String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                        //            //WebClient client = new WebClient();
                        //            //client.Headers.Add("Content-type", "application/json");
                        //            //client.Encoding = Encoding.UTF8;
                        //            //json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);

                        //            //SGLoginResponse sglr = new SGLoginResponse();
                        //            //sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                        //            //SGStockRequest sgr = new SGStockRequest();
                        //            //sgr.UserId = sglr.UserId;
                        //            //sgr.TokenId = sglr.TokenId;

                        //            //String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                        //            //WebClient client1 = new WebClient();
                        //            //client1.Headers.Add("Content-type", "application/json");
                        //            //client1.Encoding = Encoding.UTF8;
                        //            //json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockData", "POST", InputSRJson);

                        //            //var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                        //            //var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                        //            ////json_1=json_1.r
                        //            //json = JsonConvert.SerializeObject(json_1.Data, settings);
                        //            //json = json.Replace("null", "");
                        //        }
                        //        else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATAINDIA")
                        //        {
                        //            //string Name = dtAPI.Rows[0]["UserName"].ToString();
                        //            //string password = dtAPI.Rows[0]["Password"].ToString();

                        //            //SGLoginRequest sgl = new SGLoginRequest();
                        //            //sgl.UserName = Name;
                        //            //sgl.Password = password;

                        //            //String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                        //            //WebClient client = new WebClient();
                        //            //client.Headers.Add("Content-type", "application/json");
                        //            //client.Encoding = Encoding.UTF8;
                        //            //json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);

                        //            //SGLoginResponse sglr = new SGLoginResponse();
                        //            //sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                        //            //SGStockRequest sgr = new SGStockRequest();
                        //            //sgr.UserId = sglr.UserId;
                        //            //sgr.TokenId = sglr.TokenId;

                        //            //String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                        //            //WebClient client1 = new WebClient();
                        //            //client1.Headers.Add("Content-type", "application/json");
                        //            //client1.Encoding = Encoding.UTF8;
                        //            //json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataIndia", "POST", InputSRJson);

                        //            //var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                        //            //var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                        //            ////json_1=json_1.r
                        //            //json = JsonConvert.SerializeObject(json_1.Data, settings);
                        //            //json = json.Replace("null", "");
                        //        }
                        //        else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATADUBAI")
                        //        {
                        //            SGLoginRequest sgl = new SGLoginRequest();
                        //            sgl.UserName = "samit_gandhi";
                        //            sgl.Password = "missme@hk";

                        //            String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                        //            WebClient client = new WebClient();
                        //            client.Headers.Add("Content-type", "application/json");
                        //            client.Encoding = Encoding.UTF8;
                        //            json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);

                        //            SGLoginResponse sglr = new SGLoginResponse();
                        //            sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                        //            SGStockRequest sgr = new SGStockRequest();
                        //            sgr.UserId = sglr.UserId;
                        //            sgr.TokenId = sglr.TokenId;

                        //            String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                        //            WebClient client1 = new WebClient();
                        //            client1.Headers.Add("Content-type", "application/json");
                        //            client1.Encoding = Encoding.UTF8;
                        //            json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataDubai", "POST", InputSRJson);

                        //            var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                        //            var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                        //            //json_1=json_1.r
                        //            json = JsonConvert.SerializeObject(json_1.Data, settings);
                        //            json = json.Replace("null", "");
                        //        }
                        //        else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTP://PDHK.DIAMX.NET/API/STOCKSEARCH?APITOKEN=3C0DB41E-7B79-48C4-8CBD-1F718DB7263A")
                        //        {
                        //            //WebClient client = new WebClient();
                        //            //client.Headers.Add("Content-type", "application/json");
                        //            //client.Encoding = Encoding.UTF8;
                        //            //json = client.UploadString("http://pdhk.diamx.net/API/StockSearch?APIToken=3c0db41e-7b79-48c4-8cbd-1f718db7263a", "POST", "");

                        //            //JObject o = JObject.Parse(json);
                        //            //var t = string.Empty;
                        //            //if (o != null)
                        //            //{
                        //            //    var test = o.First;
                        //            //    if (test != null)
                        //            //    {
                        //            //        var test2 = test.First;
                        //            //        if (test2 != null)
                        //            //        {
                        //            //            Console.Write(test2);
                        //            //            t = test2.Root.Last.First.ToString();
                        //            //        }
                        //            //    }
                        //            //}
                        //            //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                        //            //json = JsonConvert.SerializeObject(json_1);
                        //        }
                        //        else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://STOCK.DDPL.COM/DHARAMWEBAPI/API/STOCKDISPAPI/GETDIAMONDDATA")
                        //        {
                        //            //Dharam _data = new Dharam();
                        //            //_data.uniqID = 23835;
                        //            //_data.company = "SUNRISE DIAMONDS LTD";
                        //            //_data.actCode = "Su@D123#4nd23";
                        //            //_data.selectAll = "";
                        //            //_data.StartIndex = 1;
                        //            //_data.count = 80000;
                        //            //_data.columns = "";
                        //            //_data.finder = "";
                        //            //_data.sort = "";

                        //            //string inputJson = (new JavaScriptSerializer()).Serialize(_data);

                        //            //WebClient client = new WebClient();
                        //            //client.Headers.Add("Content-type", "application/json");
                        //            //client.Encoding = Encoding.UTF8;

                        //            //json = client.UploadString("https://stock.ddpl.com/DharamWebApi/api/stockdispapi/getDiamondData", "POST", inputJson);

                        //            //JObject o = JObject.Parse(json);
                        //            //var t = string.Empty;
                        //            //if (o != null)
                        //            //{
                        //            //    var test = o.First;
                        //            //    if (test != null)
                        //            //    {
                        //            //        var test2 = test.First;
                        //            //        if (test2 != null)
                        //            //        {
                        //            //            Console.Write(test2);
                        //            //            t = test2.Root.Last.First.ToString();
                        //            //        }
                        //            //    }
                        //            //}
                        //            //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                        //            //json = JsonConvert.SerializeObject(json_1);
                        //        }
                        //        else
                        //        {
                        //            WebClient client = new WebClient();
                        //            //client.Headers.Add("Authorization", "Bearer " + Token);
                        //            client.Headers.Add("Content-type", "application/json");
                        //            client.Encoding = Encoding.UTF8;
                        //            json = client.UploadString(_API, "POST", InputPara);

                        //            if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://VAIBHAVGEMS.CO/PROVIDESTOCK.SVC/GETSTOCK")
                        //            {
                        //                //JObject o = JObject.Parse(json);
                        //                //var t = string.Empty;
                        //                //if (o != null)
                        //                //{
                        //                //    var test = o.First;
                        //                //    if (test != null)
                        //                //    {
                        //                //        var test2 = test.First;
                        //                //        if (test2 != null)
                        //                //        {
                        //                //            Console.Write(test2);
                        //                //            t = test2.First.First.ToString();
                        //                //        }
                        //                //    }
                        //                //}
                        //                //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                        //                //json = JsonConvert.SerializeObject(json_1);
                        //            }
                        //        }

                        //        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                        //        dt_APIRes = jDt.JsonStringToDataTable(json);

                        //    }
                        //    else
                        //    {
                        //        _API = dtAPI.Rows[0]["SupplierURL"].ToString();
                        //        string[] words = _API.Split('?');
                        //        String InputPara = string.Empty;
                        //        if (words.Length == 2)
                        //        {
                        //            InputPara = words[1].ToString();
                        //        }

                        //        WebClient client = new WebClient();
                        //        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                        //        ServicePointManager.Expect100Continue = false;
                        //        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                        //        string json = client.DownloadString(_API);

                        //        if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTP://203.187.204.211:801/?APIKEY=TEJAS")
                        //        {
                        //            ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                        //            dt_APIRes = jDt.JsonStringToDataTable(json);
                        //        }
                        //        else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://PCKNGNDSRV.AZUREWEBSITES.NET/ADMIN/STOCKSHARE/STOCKSHAREAPIRESULT?USERNAME=SUNRISEDIAMONDS&ACCESS_KEY=IXL8-1KGS-SA3C-E6HW-BRBA-IW4G-DSTU")
                        //        {
                        //            //JObject o = JObject.Parse(json);
                        //            //var t = string.Empty;
                        //            //if (o != null)
                        //            //{
                        //            //    var test = o.First;
                        //            //    if (test != null)
                        //            //    {
                        //            //        var test2 = test.First;
                        //            //        if (test2 != null)
                        //            //        {
                        //            //            Console.Write(test2);
                        //            //            t = o.Last.Last.ToString();
                        //            //        }
                        //            //    }
                        //            //}
                        //            //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                        //            //json = JsonConvert.SerializeObject(json_1);

                        //            //ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                        //            //dt_APIRes = jDt.JsonStringToDataTable(json);
                        //        }
                        //        else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTP://WWW.DIAMJOY.COM/API/USER/STOCK/11229/729F7B484FA22A5276B0CDADABC75147/?LANG=EN")
                        //        {
                        //            //JOY _data = (new JavaScriptSerializer()).Deserialize<JOY>(json);
                        //            //ConvertJsonObjectToDataTable jodt = new ConvertJsonObjectToDataTable();
                        //            //dt_APIRes = jodt.StringArrayToDataTable(_data.keys, _data.rows);

                        //        }
                        //        else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://API.DIAMARTHK.COM/API/CHANNELPARTNER/GETINVENTORY/SUNRISE/SUNRISE@1401")
                        //        {
                        //            //DiamartResponse res = (new JavaScriptSerializer()).Deserialize<DiamartResponse>(json);
                        //            //ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                        //            //dt_APIRes = jDt.JsonStringToDataTable(json);
                        //        }
                        //        else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://SJWORLDAPI.AZUREWEBSITES.NET/SHARE/SJAPI.ASMX/GETDATA?LOGINNAME=SUNRISE&PASSWORD=SUNRISE321")
                        //        {
                        //            //JObject o = JObject.Parse(json);
                        //            //var t = string.Empty;
                        //            //if (o != null)
                        //            //{
                        //            //    var test = o.First;
                        //            //    if (test != null)
                        //            //    {
                        //            //        var test2 = test.First;
                        //            //        if (test2 != null)
                        //            //        {
                        //            //            Console.Write(test2);
                        //            //            t = o.Last.Last.ToString();
                        //            //        }
                        //            //    }
                        //            //}
                        //            //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                        //            //json = JsonConvert.SerializeObject(json_1);

                        //            //ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                        //            //dt_APIRes = jDt.JsonStringToDataTable(json);
                        //        }
                        //        else
                        //        {
                        //            ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                        //            dt_APIRes = jDt.JsonStringToDataTable(json);
                        //        }

                        //    }

                        //}
                        //else if (dtAPI.Rows[0]["SupplierResponseFormat"].ToString().ToUpper() == "HTML")
                        //{
                        //    if (dtAPI.Rows[0]["SupplierAPIMethod"].ToString().ToUpper() == "GET")
                        //    {
                        //        if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://WWW.1314PG.COM/API/USER/STOCK/11738/8789AE77D94A9CFB109C1BA5143ABAB6/")
                        //        {
                        //            //_API = dtAPI.Rows[0]["SupplierURL"].ToString();
                        //            //WebClient client = new WebClient();
                        //            //client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                        //            //ServicePointManager.Expect100Continue = false;
                        //            //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                        //            //string response = client.DownloadString(_API);
                        //            //string[] res = response.Split('\n');

                        //            //string[] columns = res.Where(w => w == res[0]).ToArray();

                        //            //string[] rows = res.Where(w => w != res[0]).ToArray();


                        //            //ConvertStringArrayToDatatable saDt = new ConvertStringArrayToDatatable();

                        //            //dt_APIRes = saDt.StringArrayToDataTable(columns, rows);
                        //        }
                        //    }
                        //}
                        #endregion

                        if (dtAPI.Rows[0]["SupplierResponseFormat"].ToString().ToUpper() == "XML")
                        {
                            _API = dtAPI.Rows[0]["SupplierURL"].ToString();
                            string[] words = _API.Split('?');
                            String InputPara = string.Empty;
                            if (words.Length == 2)
                            {
                                InputPara = words[1].ToString();
                            }

                            WebClient client = new WebClient();
                            client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                            client.Encoding = Encoding.UTF8;
                            ServicePointManager.Expect100Continue = false;
                            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                            string xml = client.UploadString(_API, InputPara);
                            ConvertXmlStringToDataTable xDt = new ConvertXmlStringToDataTable();
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(xml);
                            XmlElement root = doc.DocumentElement;
                            XmlNodeList elemList = root.GetElementsByTagName("Row");
                            dt_APIRes = xDt.ConvertXmlNodeListToDataTable(elemList);
                        }
                        else if (dtAPI.Rows[0]["SupplierResponseFormat"].ToString().ToUpper() == "JSON")
                        {
                            if (dtAPI.Rows[0]["SupplierAPIMethod"].ToString().ToUpper() == "POST")
                            {
                                string json = string.Empty, Token = string.Empty;
                                _API = dtAPI.Rows[0]["SupplierURL"].ToString();
                                string[] words = _API.Split('?');
                                String InputPara = string.Empty;
                                if (words.Length == 2)
                                {
                                    InputPara = words[1].ToString();
                                }

                                if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://API1.ANKITGEMS.COM:4443/APIUSER/LOGINCHECK")
                                {
                                    string Name = dtAPI.Rows[0]["UserName"].ToString();
                                    string password = dtAPI.Rows[0]["Password"].ToString();

                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString("https://api1.ankitgems.com:4443/apiuser/logincheck?Name=" + Name + "&password=" + password, "POST", "");

                                    AnkitGems _data = new AnkitGems();
                                    _data = (new JavaScriptSerializer()).Deserialize<AnkitGems>(json);
                                    Token = _data.data.accessToken;

                                    WebClient client1 = new WebClient();
                                    client1.Headers.Add("Authorization", "Bearer " + Token);
                                    client1.Headers.Add("Content-type", "application/json");
                                    client1.Encoding = Encoding.UTF8;
                                    //2147483647
                                    //client1.Timeout = 600 * 60 * 1000;
                                    json = client1.UploadString("https://api1.ankitgems.com:4443/apistock/stockdetail?page=1&limit=99999", "POST", "");

                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.First;
                                            if (test2 != null)
                                            {
                                                Console.Write(test2);
                                                t = test2.Root.Last.First.First.First.ToString();
                                            }
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);
                                }
                                else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATA")
                                {
                                    string Name = dtAPI.Rows[0]["UserName"].ToString();
                                    string password = dtAPI.Rows[0]["Password"].ToString();

                                    SGLoginRequest sgl = new SGLoginRequest();
                                    sgl.UserName = "samit_gandhi";
                                    sgl.Password = "missme@hk";

                                    String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);

                                    SGLoginResponse sglr = new SGLoginResponse();
                                    sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                    SGStockRequest sgr = new SGStockRequest();
                                    sgr.UserId = sglr.UserId;
                                    sgr.TokenId = sglr.TokenId;

                                    String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                    WebClient client1 = new WebClient();
                                    client1.Headers.Add("Content-type", "application/json");
                                    client1.Encoding = Encoding.UTF8;
                                    json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockData", "POST", InputSRJson);

                                    var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                    var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                    //json_1=json_1.r
                                    json = JsonConvert.SerializeObject(json_1.Data, settings);
                                    json = json.Replace("null", "");
                                }
                                else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATAINDIA")
                                {
                                    string Name = dtAPI.Rows[0]["UserName"].ToString();
                                    string password = dtAPI.Rows[0]["Password"].ToString();

                                    SGLoginRequest sgl = new SGLoginRequest();
                                    sgl.UserName = "samit_gandhi";
                                    sgl.Password = "missme@hk";

                                    String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);

                                    SGLoginResponse sglr = new SGLoginResponse();
                                    sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                    SGStockRequest sgr = new SGStockRequest();
                                    sgr.UserId = sglr.UserId;
                                    sgr.TokenId = sglr.TokenId;

                                    String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                    WebClient client1 = new WebClient();
                                    client1.Headers.Add("Content-type", "application/json");
                                    client1.Encoding = Encoding.UTF8;
                                    json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataIndia", "POST", InputSRJson);

                                    var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                    var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                    //json_1=json_1.r
                                    json = JsonConvert.SerializeObject(json_1.Data, settings);
                                    json = json.Replace("null", "");
                                }
                                else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATADUBAI")
                                {
                                    string Name = dtAPI.Rows[0]["UserName"].ToString();
                                    string password = dtAPI.Rows[0]["Password"].ToString();

                                    SGLoginRequest sgl = new SGLoginRequest();
                                    sgl.UserName = "samit_gandhi";
                                    sgl.Password = "missme@hk";

                                    String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);

                                    SGLoginResponse sglr = new SGLoginResponse();
                                    sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                    SGStockRequest sgr = new SGStockRequest();
                                    sgr.UserId = sglr.UserId;
                                    sgr.TokenId = sglr.TokenId;

                                    String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                    WebClient client1 = new WebClient();
                                    client1.Headers.Add("Content-type", "application/json");
                                    client1.Encoding = Encoding.UTF8;
                                    json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataDubai", "POST", InputSRJson);

                                    var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                    var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                    //json_1=json_1.r
                                    json = JsonConvert.SerializeObject(json_1.Data, settings);
                                    json = json.Replace("null", "");
                                }
                                else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTP://PDHK.DIAMX.NET/API/STOCKSEARCH?APITOKEN=3C0DB41E-7B79-48C4-8CBD-1F718DB7263A")
                                {
                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString("http://pdhk.diamx.net/API/StockSearch?APIToken=3c0db41e-7b79-48c4-8cbd-1f718db7263a", "POST", "");

                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.First;
                                            if (test2 != null)
                                            {
                                                Console.Write(test2);
                                                t = test2.Root.Last.First.ToString();
                                            }
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);
                                }
                                else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://STOCK.DDPL.COM/DHARAMWEBAPI/API/STOCKDISPAPI/GETDIAMONDDATA")
                                {
                                    Dharam _data = new Dharam();
                                    _data.uniqID = 23835;
                                    _data.company = "SUNRISE DIAMONDS LTD";
                                    _data.actCode = "Su@D123#4nd23";
                                    _data.selectAll = "";
                                    _data.StartIndex = 1;
                                    _data.count = 80000;
                                    _data.columns = "";
                                    _data.finder = "";
                                    _data.sort = "";

                                    string inputJson = (new JavaScriptSerializer()).Serialize(_data);

                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;

                                    json = client.UploadString("https://stock.ddpl.com/DharamWebApi/api/stockdispapi/getDiamondData", "POST", inputJson);

                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.First;
                                            if (test2 != null)
                                            {
                                                Console.Write(test2);
                                                t = test2.Root.Last.First.ToString();
                                            }
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);
                                }
                                else
                                {
                                    WebClient client = new WebClient();
                                    //client.Headers.Add("Authorization", "Bearer " + Token);
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString(_API, "POST", InputPara);

                                    if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://VAIBHAVGEMS.CO/PROVIDESTOCK.SVC/GETSTOCK")
                                    {
                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.First;
                                            if (test != null)
                                            {
                                                var test2 = test.First;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = test2.First.First.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);
                                    }
                                }

                                ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                dt_APIRes = jDt.JsonStringToDataTable(json);

                            }
                            else
                            {
                                _API = dtAPI.Rows[0]["SupplierURL"].ToString();
                                string[] words = _API.Split('?');
                                String InputPara = string.Empty;
                                if (words.Length == 2)
                                {
                                    InputPara = words[1].ToString();
                                }

                                WebClient client = new WebClient();
                                client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                ServicePointManager.Expect100Continue = false;
                                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                string json = client.DownloadString(_API);

                                if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://PCKNGNDSRV.AZUREWEBSITES.NET/ADMIN/STOCKSHARE/STOCKSHAREAPIRESULT?USERNAME=SUNRISEDIAMONDS&ACCESS_KEY=IXL8-1KGS-SA3C-E6HW-BRBA-IW4G-DSTU")
                                {
                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.First;
                                            if (test2 != null)
                                            {
                                                Console.Write(test2);
                                                t = o.Last.Last.ToString();
                                            }
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);

                                    ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                    dt_APIRes = jDt.JsonStringToDataTable(json);
                                }
                                else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTP://WWW.DIAMJOY.COM/API/USER/STOCK/11229/729F7B484FA22A5276B0CDADABC75147/?LANG=EN")
                                {
                                    JOY _data = (new JavaScriptSerializer()).Deserialize<JOY>(json);
                                    ConvertJsonObjectToDataTable jodt = new ConvertJsonObjectToDataTable();
                                    dt_APIRes = jodt.StringArrayToDataTable(_data.keys, _data.rows);

                                }
                                else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://API.DIAMARTHK.COM/API/CHANNELPARTNER/GETINVENTORY/SUNRISE/SUNRISE@1401")
                                {
                                    DiamartResponse res = (new JavaScriptSerializer()).Deserialize<DiamartResponse>(json);
                                    ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                    dt_APIRes = jDt.JsonStringToDataTable(json);
                                }
                                else if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://SJWORLDAPI.AZUREWEBSITES.NET/SHARE/SJAPI.ASMX/GETDATA?LOGINNAME=SUNRISE&PASSWORD=SUNRISE321")
                                {
                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.First;
                                            if (test2 != null)
                                            {
                                                Console.Write(test2);
                                                t = o.Last.Last.ToString();
                                            }
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);

                                    ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                    dt_APIRes = jDt.JsonStringToDataTable(json);
                                }
                                else
                                {
                                    ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                    dt_APIRes = jDt.JsonStringToDataTable(json);
                                }

                            }

                        }
                        else if (dtAPI.Rows[0]["SupplierResponseFormat"].ToString().ToUpper() == "HTML")
                        {
                            if (dtAPI.Rows[0]["SupplierAPIMethod"].ToString().ToUpper() == "GET")
                            {
                                if (dtAPI.Rows[0]["SupplierURL"].ToString().ToUpper() == "HTTPS://WWW.1314PG.COM/API/USER/STOCK/11738/8789AE77D94A9CFB109C1BA5143ABAB6/")
                                {
                                    _API = dtAPI.Rows[0]["SupplierURL"].ToString();
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    string response = client.DownloadString(_API);
                                    string[] res = response.Split('\n');

                                    string[] columns = res.Where(w => w == res[0]).ToArray();

                                    string[] rows = res.Where(w => w != res[0]).ToArray();


                                    ConvertStringArrayToDatatable saDt = new ConvertStringArrayToDatatable();

                                    dt_APIRes = saDt.StringArrayToDataTable(columns, rows);
                                }
                            }
                        }

                        if (dt_APIRes != null && dt_APIRes.Rows.Count > 0)
                        {
                            SuppColsGetFromAPI_Log_Ins(Supplier_Mas_Id, "Success " + dt_APIRes.Columns.Count + " Columns Found.");

                            DataTable dtResult = new DataTable();
                            int currecs = 1;

                            dtResult.Columns.Add("Id", typeof(int));
                            dtResult.Columns.Add("SupplierColumn", typeof(string));

                            foreach (DataColumn column in dt_APIRes.Columns)
                            {
                                DataRow dr = dtResult.NewRow();
                                dr["Id"] = currecs;
                                dr["SupplierColumn"] = column.ColumnName;
                                currecs += 1;

                                dtResult.Rows.Add(dr);
                            }

                            List<Get_SupplierColumnsFromAPI_Response> list = new List<Get_SupplierColumnsFromAPI_Response>();
                            list = DataTableExtension.ToList<Get_SupplierColumnsFromAPI_Response>(dtResult);

                            return Ok(new ServiceResponse<Get_SupplierColumnsFromAPI_Response>
                            {
                                Data = list,
                                Message = "SUCCESS",
                                Status = "1"
                            });
                        }
                        else
                        {
                            SuppColsGetFromAPI_Log_Ins(Supplier_Mas_Id, "Supplier API in Columns not Found.");

                            return Ok(new ServiceResponse<Get_SupplierColumnsFromAPI_Response>
                            {
                                Data = null,
                                Message = "Supplier API in Columns not found.",
                                Status = "2"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        SuppColsGetFromAPI_Log_Ins(Supplier_Mas_Id, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());

                        return Ok(new ServiceResponse<Get_SupplierColumnsFromAPI_Response>
                        {
                            Data = null,
                            Message = ex.Message,
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SupplierColumnsFromAPI_Response>
                    {
                        Data = null,
                        Message = "Supplier Not Found.",
                        Status = "2"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnsFromAPI_Response>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static void SuppColsGetFromAPI_Log_Ins(int Supplier_Mas_Id, string message)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Supplier_Mas_Id", DbType.Int64, ParameterDirection.Input, Supplier_Mas_Id));
                para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, message));

                DataTable dt = db.ExecuteSP("SuppColsGetFromAPI_Log_Ins", para.ToArray(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Supplier_PriceList([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Supplier_PriceList_Response>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.Id > 0)
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.Supplier_Mas_Id > 0)
                    para.Add(db.CreateParam("Supplier_Mas_Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.Supplier_Mas_Id));
                else
                    para.Add(db.CreateParam("Supplier_Mas_Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.Search))
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, get_apiuploadmst.Search));
                else
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, get_apiuploadmst.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Supplier_PriceList_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_Supplier_PriceList_Response> list = new List<Get_Supplier_PriceList_Response>();
                    list = DataTableExtension.ToList<Get_Supplier_PriceList_Response>(dt);

                    return Ok(new ServiceResponse<Get_Supplier_PriceList_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_Supplier_PriceList_Response>
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
                return Ok(new ServiceResponse<Get_Supplier_PriceList_Response>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Supplier_PriceList_Delete([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Supplier_PriceList_Response>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.SupplierPriceList_Id > 0)
                    para.Add(db.CreateParam("SupplierPriceList_Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.SupplierPriceList_Id));
                else
                    para.Add(db.CreateParam("SupplierPriceList_Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Supplier_PriceList_Delete", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "1")
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = dt.Rows[0]["Message"].ToString(),
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = "",
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
        public IHttpActionResult SupplierGetFrom_PriceList([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Supplier_PriceList_Response>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.Supplier_Mas_Id > 0)
                    para.Add(db.CreateParam("Supplier_Mas_Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.Supplier_Mas_Id));
                else
                    para.Add(db.CreateParam("Supplier_Mas_Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.SupplierPriceList_Id > 0)
                    para.Add(db.CreateParam("SupplierPriceList_Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.SupplierPriceList_Id));
                else
                    para.Add(db.CreateParam("SupplierPriceList_Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("SupplierGetFrom_PriceList", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_Supplier_PriceList_Response> list = new List<Get_Supplier_PriceList_Response>();
                    list = DataTableExtension.ToList<Get_Supplier_PriceList_Response>(dt);

                    return Ok(new ServiceResponse<Get_Supplier_PriceList_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_Supplier_PriceList_Response>
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
                return Ok(new ServiceResponse<Get_Supplier_PriceList_Response>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_API_StockFilter([FromBody]JObject data)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("API_StockFilter", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_API_StockFilter_Response> list = new List<Get_API_StockFilter_Response>();
                    list = DataTableExtension.ToList<Get_API_StockFilter_Response>(dt);

                    return Ok(new ServiceResponse<Get_API_StockFilter_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_API_StockFilter_Response>
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
                return Ok(new ServiceResponse<Get_API_StockFilter_Response>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult SaveCustWiseDisc([FromBody]JObject data)
        {
            SaveCustWiseDisc_Req req = new SaveCustWiseDisc_Req();
            try
            {
                req = JsonConvert.DeserializeObject<SaveCustWiseDisc_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SaveCustWiseDisc_Req>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("DiscPer", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.DiscPer));
                para.Add(db.CreateParam("UserList", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.UserList));
                para.Add(db.CreateParam("Type", System.Data.DbType.String, System.Data.ParameterDirection.Input, req.Type));

                DataTable dt = db.ExecuteSP("SaveCustWiseDisc", para.ToArray(), false);

                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "SUCCESS",
                    Status = "1"
                });
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
        public IHttpActionResult GetCustWiseDisc([FromBody]JObject data)
        {
            StockDiscMgtRequest stockdiscmgtrequest = new StockDiscMgtRequest();
            try
            {
                stockdiscmgtrequest = JsonConvert.DeserializeObject<StockDiscMgtRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<GetCustWiseDisc_Res>
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

                if (!string.IsNullOrEmpty(stockdiscmgtrequest.sOrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, stockdiscmgtrequest.sOrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, stockdiscmgtrequest.iPgNo));
                para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, stockdiscmgtrequest.iPgSize));

                if (string.IsNullOrEmpty(stockdiscmgtrequest.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, stockdiscmgtrequest.UserName));

                if (string.IsNullOrEmpty(stockdiscmgtrequest.CompanyName))
                    para.Add(db.CreateParam("CompanyName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("CompanyName", DbType.String, ParameterDirection.Input, stockdiscmgtrequest.CompanyName));

                if (string.IsNullOrEmpty(stockdiscmgtrequest.UserFullName))
                    para.Add(db.CreateParam("UserFullName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("UserFullName", DbType.String, ParameterDirection.Input, stockdiscmgtrequest.UserFullName));

                if (string.IsNullOrEmpty(stockdiscmgtrequest.FilterType))
                    para.Add(db.CreateParam("FilterType", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("FilterType", DbType.String, ParameterDirection.Input, stockdiscmgtrequest.FilterType)); 
                
                if (string.IsNullOrEmpty(stockdiscmgtrequest.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, stockdiscmgtrequest.FromDate));

                if (string.IsNullOrEmpty(stockdiscmgtrequest.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, stockdiscmgtrequest.ToDate));

                DataTable dt = db.ExecuteSP("GetCustWiseDisc", para.ToArray(), false);

                List<GetCustWiseDisc_Res> stockdiscmgtresponse = new List<GetCustWiseDisc_Res>();
                stockdiscmgtresponse = DataTableExtension.ToList<GetCustWiseDisc_Res>(dt);

                if (stockdiscmgtresponse != null && stockdiscmgtresponse.Count > 0)
                {
                    return Ok(new ServiceResponse<GetCustWiseDisc_Res>
                    {
                        Data = stockdiscmgtresponse,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<GetCustWiseDisc_Res>
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
                return Ok(new ServiceResponse<GetCustWiseDisc_Res>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Save_SuppStockValue([FromBody]JObject data)
        {
            Save_SuppStockValue_Request save_suppstockvalue = new Save_SuppStockValue_Request();
            try
            {
                save_suppstockvalue = JsonConvert.DeserializeObject<Save_SuppStockValue_Request>(data.ToString());
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
                DataTable dt = new DataTable();
                dt.Columns.Add("SupplierId", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("GoodsType", typeof(string));
                dt.Columns.Add("Shape", typeof(string));
                dt.Columns.Add("Pointer", typeof(string));
                dt.Columns.Add("ColorType", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("INTENSITY", typeof(string));
                dt.Columns.Add("OVERTONE", typeof(string));
                dt.Columns.Add("FANCY_COLOR", typeof(string));
                dt.Columns.Add("Clarity", typeof(string));
                dt.Columns.Add("Cut", typeof(string));
                dt.Columns.Add("Polish", typeof(string));
                dt.Columns.Add("Symm", typeof(string));
                dt.Columns.Add("Fls", typeof(string));
                dt.Columns.Add("Lab", typeof(string));
                dt.Columns.Add("FromLength", typeof(string));
                dt.Columns.Add("ToLength", typeof(string));
                dt.Columns.Add("FromWidth", typeof(string));
                dt.Columns.Add("ToWidth", typeof(string));
                dt.Columns.Add("FromDepth", typeof(string));
                dt.Columns.Add("ToDepth", typeof(string));
                dt.Columns.Add("FromDepthPer", typeof(string));
                dt.Columns.Add("ToDepthPer", typeof(string));
                dt.Columns.Add("FromTablePer", typeof(string));
                dt.Columns.Add("ToTablePer", typeof(string));
                dt.Columns.Add("FromCrAng", typeof(string));
                dt.Columns.Add("ToCrAng", typeof(string));
                dt.Columns.Add("FromCrHt", typeof(string));
                dt.Columns.Add("ToCrHt", typeof(string));
                dt.Columns.Add("FromPavAng", typeof(string));
                dt.Columns.Add("ToPavAng", typeof(string));
                dt.Columns.Add("FromPavHt", typeof(string));
                dt.Columns.Add("ToPavHt", typeof(string));
                dt.Columns.Add("KeyToSymbol", typeof(string));
                dt.Columns.Add("CheckKTS", typeof(string));
                dt.Columns.Add("UNCheckKTS", typeof(string));
                dt.Columns.Add("BGM", typeof(string));
                dt.Columns.Add("CrownBlack", typeof(string));
                dt.Columns.Add("TableBlack", typeof(string));
                dt.Columns.Add("CrownWhite", typeof(string));
                dt.Columns.Add("TableWhite", typeof(string));
                dt.Columns.Add("Img", typeof(string));
                dt.Columns.Add("Vdo", typeof(string));
                dt.Columns.Add("PriceMethod", typeof(string));
                dt.Columns.Add("PricePer", typeof(string));

                if (save_suppstockvalue.Filters.Count() > 0)
                {
                    for (int i = 0; i < save_suppstockvalue.Filters.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["SupplierId"] = save_suppstockvalue.Filters[i].SupplierId;
                        dr["Location"] = save_suppstockvalue.Filters[i].Location;
                        dr["GoodsType"] = save_suppstockvalue.Filters[i].GoodsType;
                        dr["Shape"] = save_suppstockvalue.Filters[i].Shape;
                        dr["Pointer"] = save_suppstockvalue.Filters[i].Pointer;
                        dr["ColorType"] = save_suppstockvalue.Filters[i].ColorType;
                        dr["Color"] = save_suppstockvalue.Filters[i].Color;
                        dr["INTENSITY"] = save_suppstockvalue.Filters[i].INTENSITY;
                        dr["OVERTONE"] = save_suppstockvalue.Filters[i].OVERTONE;
                        dr["FANCY_COLOR"] = save_suppstockvalue.Filters[i].FANCY_COLOR;
                        dr["Clarity"] = save_suppstockvalue.Filters[i].Clarity;
                        dr["Cut"] = save_suppstockvalue.Filters[i].Cut;
                        dr["Polish"] = save_suppstockvalue.Filters[i].Polish;
                        dr["Symm"] = save_suppstockvalue.Filters[i].Symm;
                        dr["Fls"] = save_suppstockvalue.Filters[i].Fls;
                        dr["Lab"] = save_suppstockvalue.Filters[i].Lab;
                        dr["FromLength"] = save_suppstockvalue.Filters[i].FromLength;
                        dr["ToLength"] = save_suppstockvalue.Filters[i].ToLength;
                        dr["FromWidth"] = save_suppstockvalue.Filters[i].FromWidth;
                        dr["ToWidth"] = save_suppstockvalue.Filters[i].ToWidth;
                        dr["FromDepth"] = save_suppstockvalue.Filters[i].FromDepth;
                        dr["ToDepth"] = save_suppstockvalue.Filters[i].ToDepth;
                        dr["FromDepthPer"] = save_suppstockvalue.Filters[i].FromDepthPer;
                        dr["ToDepthPer"] = save_suppstockvalue.Filters[i].ToDepthPer;
                        dr["FromTablePer"] = save_suppstockvalue.Filters[i].FromTablePer;
                        dr["ToTablePer"] = save_suppstockvalue.Filters[i].ToTablePer;
                        dr["FromCrAng"] = save_suppstockvalue.Filters[i].FromCrAng;
                        dr["ToCrAng"] = save_suppstockvalue.Filters[i].ToCrAng;
                        dr["FromCrHt"] = save_suppstockvalue.Filters[i].FromCrHt;
                        dr["ToCrHt"] = save_suppstockvalue.Filters[i].ToCrHt;
                        dr["FromPavAng"] = save_suppstockvalue.Filters[i].FromPavAng;
                        dr["ToPavAng"] = save_suppstockvalue.Filters[i].ToPavAng;
                        dr["FromPavHt"] = save_suppstockvalue.Filters[i].FromPavHt;
                        dr["ToPavHt"] = save_suppstockvalue.Filters[i].ToPavHt;
                        dr["KeyToSymbol"] = save_suppstockvalue.Filters[i].KeyToSymbol;
                        dr["CheckKTS"] = save_suppstockvalue.Filters[i].CheckKTS;
                        dr["UNCheckKTS"] = save_suppstockvalue.Filters[i].UNCheckKTS;
                        dr["BGM"] = save_suppstockvalue.Filters[i].BGM;
                        dr["CrownBlack"] = save_suppstockvalue.Filters[i].CrownBlack;
                        dr["TableBlack"] = save_suppstockvalue.Filters[i].TableBlack;
                        dr["CrownWhite"] = save_suppstockvalue.Filters[i].CrownWhite;
                        dr["TableWhite"] = save_suppstockvalue.Filters[i].TableWhite;
                        dr["Img"] = save_suppstockvalue.Filters[i].Img;
                        dr["Vdo"] = save_suppstockvalue.Filters[i].Vdo;
                        dr["PriceMethod"] = save_suppstockvalue.Filters[i].PriceMethod;
                        dr["PricePer"] = save_suppstockvalue.Filters[i].PricePer;

                        dt.Rows.Add(dr);
                    }
                }

                Database db = new Database();
                DataTable dtData = new DataTable();
                List<SqlParameter> para = new List<SqlParameter>();

                SqlParameter param = new SqlParameter("tabledt", SqlDbType.Structured);
                param.Value = dt;
                para.Add(param);

                dtData = db.ExecuteSP("Supplier_StockValue_CRUD", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0 && dtData.Rows[0]["Status"].ToString() == "1")
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = dtData.Rows[0]["Id"].ToString() + "_414_" + dtData.Rows[0]["Message"].ToString(),
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = "",
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
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Save_SuppCostValue([FromBody]JObject data)
        {
            Save_SuppStockValue_Request save_suppcostvalue = new Save_SuppStockValue_Request();
            try
            {
                save_suppcostvalue = JsonConvert.DeserializeObject<Save_SuppStockValue_Request>(data.ToString());
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

                DataTable dt = new DataTable();
                dt.Columns.Add("SupplierId", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("GoodsType", typeof(string));
                dt.Columns.Add("Shape", typeof(string));
                dt.Columns.Add("Pointer", typeof(string));
                dt.Columns.Add("ColorType", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("INTENSITY", typeof(string));
                dt.Columns.Add("OVERTONE", typeof(string));
                dt.Columns.Add("FANCY_COLOR", typeof(string));
                dt.Columns.Add("Clarity", typeof(string));
                dt.Columns.Add("Cut", typeof(string));
                dt.Columns.Add("Polish", typeof(string));
                dt.Columns.Add("Symm", typeof(string));
                dt.Columns.Add("Fls", typeof(string));
                dt.Columns.Add("Lab", typeof(string));
                dt.Columns.Add("FromLength", typeof(string));
                dt.Columns.Add("ToLength", typeof(string));
                dt.Columns.Add("FromWidth", typeof(string));
                dt.Columns.Add("ToWidth", typeof(string));
                dt.Columns.Add("FromDepth", typeof(string));
                dt.Columns.Add("ToDepth", typeof(string));
                dt.Columns.Add("FromDepthPer", typeof(string));
                dt.Columns.Add("ToDepthPer", typeof(string));
                dt.Columns.Add("FromTablePer", typeof(string));
                dt.Columns.Add("ToTablePer", typeof(string));
                dt.Columns.Add("FromCrAng", typeof(string));
                dt.Columns.Add("ToCrAng", typeof(string));
                dt.Columns.Add("FromCrHt", typeof(string));
                dt.Columns.Add("ToCrHt", typeof(string));
                dt.Columns.Add("FromPavAng", typeof(string));
                dt.Columns.Add("ToPavAng", typeof(string));
                dt.Columns.Add("FromPavHt", typeof(string));
                dt.Columns.Add("ToPavHt", typeof(string));
                dt.Columns.Add("KeyToSymbol", typeof(string));
                dt.Columns.Add("CheckKTS", typeof(string));
                dt.Columns.Add("UNCheckKTS", typeof(string));
                dt.Columns.Add("BGM", typeof(string));
                dt.Columns.Add("CrownBlack", typeof(string));
                dt.Columns.Add("TableBlack", typeof(string));
                dt.Columns.Add("CrownWhite", typeof(string));
                dt.Columns.Add("TableWhite", typeof(string));
                dt.Columns.Add("Img", typeof(string));
                dt.Columns.Add("Vdo", typeof(string));
                dt.Columns.Add("PriceMethod", typeof(string));
                dt.Columns.Add("PricePer", typeof(string));

                if (save_suppcostvalue.Filters.Count() > 0)
                {
                    for (int i = 0; i < save_suppcostvalue.Filters.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["SupplierId"] = save_suppcostvalue.Filters[i].SupplierId;
                        dr["Location"] = save_suppcostvalue.Filters[i].Location;
                        dr["GoodsType"] = save_suppcostvalue.Filters[i].GoodsType;
                        dr["Shape"] = save_suppcostvalue.Filters[i].Shape;
                        dr["Pointer"] = save_suppcostvalue.Filters[i].Pointer;
                        dr["ColorType"] = save_suppcostvalue.Filters[i].ColorType;
                        dr["Color"] = save_suppcostvalue.Filters[i].Color;
                        dr["INTENSITY"] = save_suppcostvalue.Filters[i].INTENSITY;
                        dr["OVERTONE"] = save_suppcostvalue.Filters[i].OVERTONE;
                        dr["FANCY_COLOR"] = save_suppcostvalue.Filters[i].FANCY_COLOR;
                        dr["Clarity"] = save_suppcostvalue.Filters[i].Clarity;
                        dr["Cut"] = save_suppcostvalue.Filters[i].Cut;
                        dr["Polish"] = save_suppcostvalue.Filters[i].Polish;
                        dr["Symm"] = save_suppcostvalue.Filters[i].Symm;
                        dr["Fls"] = save_suppcostvalue.Filters[i].Fls;
                        dr["Lab"] = save_suppcostvalue.Filters[i].Lab;
                        dr["FromLength"] = save_suppcostvalue.Filters[i].FromLength;
                        dr["ToLength"] = save_suppcostvalue.Filters[i].ToLength;
                        dr["FromWidth"] = save_suppcostvalue.Filters[i].FromWidth;
                        dr["ToWidth"] = save_suppcostvalue.Filters[i].ToWidth;
                        dr["FromDepth"] = save_suppcostvalue.Filters[i].FromDepth;
                        dr["ToDepth"] = save_suppcostvalue.Filters[i].ToDepth;
                        dr["FromDepthPer"] = save_suppcostvalue.Filters[i].FromDepthPer;
                        dr["ToDepthPer"] = save_suppcostvalue.Filters[i].ToDepthPer;
                        dr["FromTablePer"] = save_suppcostvalue.Filters[i].FromTablePer;
                        dr["ToTablePer"] = save_suppcostvalue.Filters[i].ToTablePer;
                        dr["FromCrAng"] = save_suppcostvalue.Filters[i].FromCrAng;
                        dr["ToCrAng"] = save_suppcostvalue.Filters[i].ToCrAng;
                        dr["FromCrHt"] = save_suppcostvalue.Filters[i].FromCrHt;
                        dr["ToCrHt"] = save_suppcostvalue.Filters[i].ToCrHt;
                        dr["FromPavAng"] = save_suppcostvalue.Filters[i].FromPavAng;
                        dr["ToPavAng"] = save_suppcostvalue.Filters[i].ToPavAng;
                        dr["FromPavHt"] = save_suppcostvalue.Filters[i].FromPavHt;
                        dr["ToPavHt"] = save_suppcostvalue.Filters[i].ToPavHt;
                        dr["KeyToSymbol"] = save_suppcostvalue.Filters[i].KeyToSymbol;
                        dr["CheckKTS"] = save_suppcostvalue.Filters[i].CheckKTS;
                        dr["UNCheckKTS"] = save_suppcostvalue.Filters[i].UNCheckKTS;
                        dr["BGM"] = save_suppcostvalue.Filters[i].BGM;
                        dr["CrownBlack"] = save_suppcostvalue.Filters[i].CrownBlack;
                        dr["TableBlack"] = save_suppcostvalue.Filters[i].TableBlack;
                        dr["CrownWhite"] = save_suppcostvalue.Filters[i].CrownWhite;
                        dr["TableWhite"] = save_suppcostvalue.Filters[i].TableWhite;
                        dr["Img"] = save_suppcostvalue.Filters[i].Img;
                        dr["Vdo"] = save_suppcostvalue.Filters[i].Vdo;
                        dr["PriceMethod"] = save_suppcostvalue.Filters[i].PriceMethod;
                        dr["PricePer"] = save_suppcostvalue.Filters[i].PricePer;

                        dt.Rows.Add(dr);
                    }
                }

                Database db = new Database();
                DataTable dtData = new DataTable();
                List<SqlParameter> para = new List<SqlParameter>();

                SqlParameter param = new SqlParameter("tabledt", SqlDbType.Structured);
                param.Value = dt;
                para.Add(param);

                dtData = db.ExecuteSP("Supplier_CostValue_CRUD", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0 && dtData.Rows[0]["Status"].ToString() == "1")
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = dtData.Rows[0]["Id"].ToString() + "_414_" + dtData.Rows[0]["Message"].ToString(),
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = "",
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
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_SuppStockValue([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
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
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.SupplierPriceList_Id > 0)
                    para.Add(db.CreateParam("SupplierPriceList_Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.SupplierPriceList_Id));
                else
                    para.Add(db.CreateParam("SupplierPriceList_Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("SupplierStockValue_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<SuppStockValue> list = new List<SuppStockValue>();
                    list = DataTableExtension.ToList<SuppStockValue>(dt);

                    return Ok(new ServiceResponse<SuppStockValue>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<SuppStockValue>
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
                return Ok(new ServiceResponse<SuppStockValue>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_SuppCostValue([FromBody]JObject data)
        {
            Get_APIMst_Request get_apiuploadmst = new Get_APIMst_Request();
            try
            {
                get_apiuploadmst = JsonConvert.DeserializeObject<Get_APIMst_Request>(data.ToString());
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
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.SupplierPriceList_Id > 0)
                    para.Add(db.CreateParam("SupplierPriceList_Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.SupplierPriceList_Id));
                else
                    para.Add(db.CreateParam("SupplierPriceList_Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("SupplierCostValue_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<SuppStockValue> list = new List<SuppStockValue>();
                    list = DataTableExtension.ToList<SuppStockValue>(dt);

                    return Ok(new ServiceResponse<SuppStockValue>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<SuppStockValue>
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
                return Ok(new ServiceResponse<SuppStockValue>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Supplier_Auto_Stock_Upload()
        {
            int SuppMst_Id = 0;

            string path = HttpRuntime.AppDomainAppPath + "Supplier_Auto_Stock_Upload.txt";

            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                DataTable Final_dt = new DataTable();
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                DataTable dtSuppl = db.ExecuteSP("SupplierMasterScheduler_select", para.ToArray(), false);

                if (dtSuppl != null && dtSuppl.Rows.Count > 0)
                {
                    TotCount = dtSuppl.Rows.Count;
                    for (int i = 0; i < dtSuppl.Rows.Count; i++)
                    {
                        try
                        {
                            SuppMst_Id = Convert.ToInt32(dtSuppl.Rows[i]["Id"].ToString());

                            Supplier_Start_End(SuppMst_Id, "Start");

                            string tempPath = dtSuppl.Rows[i]["FileLocation"].ToString(),
                                APIFileName = dtSuppl.Rows[i]["FileName"].ToString(),
                            _API = String.Empty, UserName = String.Empty, Password = String.Empty, filename = String.Empty, filefullpath = String.Empty;

                            DataTable dt_APIRes = new DataTable();

                            if (!Directory.Exists(tempPath))
                            {
                                Directory.CreateDirectory(tempPath);
                            }

                            if (dtSuppl.Rows[i]["SupplierResponseFormat"].ToString().ToUpper() == "XML")
                            {
                                _API = dtSuppl.Rows[i]["SupplierURL"].ToString();
                                string[] words = _API.Split('?');
                                String InputPara = string.Empty;
                                if (words.Length == 2)
                                {
                                    InputPara = words[1].ToString();
                                }

                                WebClient client = new WebClient();
                                client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                client.Encoding = Encoding.UTF8;
                                ServicePointManager.Expect100Continue = false;
                                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                string xml = client.UploadString(_API, InputPara);
                                ConvertXmlStringToDataTable xDt = new ConvertXmlStringToDataTable();
                                XmlDocument doc = new XmlDocument();
                                doc.LoadXml(xml);
                                XmlElement root = doc.DocumentElement;
                                XmlNodeList elemList = root.GetElementsByTagName("Row");
                                dt_APIRes = xDt.ConvertXmlNodeListToDataTable(elemList);
                            }
                            else if (dtSuppl.Rows[i]["SupplierResponseFormat"].ToString().ToUpper() == "JSON")
                            {
                                if (dtSuppl.Rows[i]["SupplierAPIMethod"].ToString().ToUpper() == "POST")
                                {
                                    string json = string.Empty, Token = string.Empty;
                                    _API = dtSuppl.Rows[i]["SupplierURL"].ToString();
                                    string[] words = _API.Split('?');
                                    String InputPara = string.Empty;
                                    if (words.Length == 2)
                                    {
                                        InputPara = words[1].ToString();
                                    }

                                    if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://API1.ANKITGEMS.COM:4443/APIUSER/LOGINCHECK")
                                    {
                                        string Name = dtSuppl.Rows[i]["UserName"].ToString();
                                        string password = dtSuppl.Rows[i]["Password"].ToString();

                                        WebClient client = new WebClient();
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;
                                        json = client.UploadString("https://api1.ankitgems.com:4443/apiuser/logincheck?Name=" + Name + "&password=" + password, "POST", "");

                                        AnkitGems _data = new AnkitGems();
                                        _data = (new JavaScriptSerializer()).Deserialize<AnkitGems>(json);
                                        Token = _data.data.accessToken;

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Authorization", "Bearer " + Token);
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        //2147483647
                                        //client1.Timeout = 600 * 60 * 1000;
                                        json = client1.UploadString("https://api1.ankitgems.com:4443/apistock/stockdetail?page=1&limit=99999", "POST", "");

                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.First;
                                            if (test != null)
                                            {
                                                var test2 = test.First;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = test2.Root.Last.First.First.First.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);
                                    }
                                    else if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATA")
                                    {
                                        string Name = dtSuppl.Rows[i]["UserName"].ToString();
                                        string password = dtSuppl.Rows[i]["Password"].ToString();

                                        SGLoginRequest sgl = new SGLoginRequest();
                                        sgl.UserName = "samit_gandhi";
                                        sgl.Password = "missme@hk";

                                        String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                        WebClient client = new WebClient();
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;
                                        json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);

                                        SGLoginResponse sglr = new SGLoginResponse();
                                        sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                        SGStockRequest sgr = new SGStockRequest();
                                        sgr.UserId = sglr.UserId;
                                        sgr.TokenId = sglr.TokenId;

                                        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockData", "POST", InputSRJson);

                                        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        //json_1=json_1.r
                                        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        json = json.Replace("null", "");
                                    }
                                    else if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATAINDIA")
                                    {
                                        string Name = dtSuppl.Rows[i]["UserName"].ToString();
                                        string password = dtSuppl.Rows[i]["Password"].ToString();

                                        SGLoginRequest sgl = new SGLoginRequest();
                                        sgl.UserName = "samit_gandhi";
                                        sgl.Password = "missme@hk";

                                        String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                        WebClient client = new WebClient();
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;
                                        json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);

                                        SGLoginResponse sglr = new SGLoginResponse();
                                        sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                        SGStockRequest sgr = new SGStockRequest();
                                        sgr.UserId = sglr.UserId;
                                        sgr.TokenId = sglr.TokenId;

                                        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataIndia", "POST", InputSRJson);

                                        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        //json_1=json_1.r
                                        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        json = json.Replace("null", "");
                                    }
                                    else if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATADUBAI")
                                    {
                                        string Name = dtSuppl.Rows[i]["UserName"].ToString();
                                        string password = dtSuppl.Rows[i]["Password"].ToString();

                                        SGLoginRequest sgl = new SGLoginRequest();
                                        sgl.UserName = "samit_gandhi";
                                        sgl.Password = "missme@hk";

                                        String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                        WebClient client = new WebClient();
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;
                                        json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);

                                        SGLoginResponse sglr = new SGLoginResponse();
                                        sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                        SGStockRequest sgr = new SGStockRequest();
                                        sgr.UserId = sglr.UserId;
                                        sgr.TokenId = sglr.TokenId;

                                        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataDubai", "POST", InputSRJson);

                                        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        //json_1=json_1.r
                                        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        json = json.Replace("null", "");
                                    }
                                    else if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTP://PDHK.DIAMX.NET/API/STOCKSEARCH?APITOKEN=3C0DB41E-7B79-48C4-8CBD-1F718DB7263A")
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;
                                        json = client.UploadString("http://pdhk.diamx.net/API/StockSearch?APIToken=3c0db41e-7b79-48c4-8cbd-1f718db7263a", "POST", "");

                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.First;
                                            if (test != null)
                                            {
                                                var test2 = test.First;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = test2.Root.Last.First.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);
                                    }
                                    else if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://STOCK.DDPL.COM/DHARAMWEBAPI/API/STOCKDISPAPI/GETDIAMONDDATA")
                                    {
                                        Dharam _data = new Dharam();
                                        _data.uniqID = 23835;
                                        _data.company = "SUNRISE DIAMONDS LTD";
                                        _data.actCode = "Su@D123#4nd23";
                                        _data.selectAll = "";
                                        _data.StartIndex = 1;
                                        _data.count = 80000;
                                        _data.columns = "";
                                        _data.finder = "";
                                        _data.sort = "";

                                        string inputJson = (new JavaScriptSerializer()).Serialize(_data);

                                        WebClient client = new WebClient();
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;

                                        json = client.UploadString("https://stock.ddpl.com/DharamWebApi/api/stockdispapi/getDiamondData", "POST", inputJson);

                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.First;
                                            if (test != null)
                                            {
                                                var test2 = test.First;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = test2.Root.Last.First.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);
                                    }
                                    else
                                    {
                                        WebClient client = new WebClient();
                                        //client.Headers.Add("Authorization", "Bearer " + Token);
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;
                                        json = client.UploadString(_API, "POST", InputPara);

                                        if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://VAIBHAVGEMS.CO/PROVIDESTOCK.SVC/GETSTOCK")
                                        {
                                            JObject o = JObject.Parse(json);
                                            var t = string.Empty;
                                            if (o != null)
                                            {
                                                var test = o.First;
                                                if (test != null)
                                                {
                                                    var test2 = test.First;
                                                    if (test2 != null)
                                                    {
                                                        Console.Write(test2);
                                                        t = test2.First.First.ToString();
                                                    }
                                                }
                                            }
                                            var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                            json = JsonConvert.SerializeObject(json_1);
                                        }
                                    }

                                    ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                    dt_APIRes = jDt.JsonStringToDataTable(json);

                                }
                                else
                                {
                                    _API = dtSuppl.Rows[i]["SupplierURL"].ToString();
                                    string[] words = _API.Split('?');
                                    String InputPara = string.Empty;
                                    if (words.Length == 2)
                                    {
                                        InputPara = words[1].ToString();
                                    }

                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    string json = client.DownloadString(_API);

                                    if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://PCKNGNDSRV.AZUREWEBSITES.NET/ADMIN/STOCKSHARE/STOCKSHAREAPIRESULT?USERNAME=SUNRISEDIAMONDS&ACCESS_KEY=IXL8-1KGS-SA3C-E6HW-BRBA-IW4G-DSTU")
                                    {
                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.First;
                                            if (test != null)
                                            {
                                                var test2 = test.First;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = o.Last.Last.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);

                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTP://WWW.DIAMJOY.COM/API/USER/STOCK/11229/729F7B484FA22A5276B0CDADABC75147/?LANG=EN")
                                    {
                                        JOY _data = (new JavaScriptSerializer()).Deserialize<JOY>(json);
                                        ConvertJsonObjectToDataTable jodt = new ConvertJsonObjectToDataTable();
                                        dt_APIRes = jodt.StringArrayToDataTable(_data.keys, _data.rows);

                                    }
                                    else if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://API.DIAMARTHK.COM/API/CHANNELPARTNER/GETINVENTORY/SUNRISE/SUNRISE@1401")
                                    {
                                        DiamartResponse res = (new JavaScriptSerializer()).Deserialize<DiamartResponse>(json);
                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://SJWORLDAPI.AZUREWEBSITES.NET/SHARE/SJAPI.ASMX/GETDATA?LOGINNAME=SUNRISE&PASSWORD=SUNRISE321")
                                    {
                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.First;
                                            if (test != null)
                                            {
                                                var test2 = test.First;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = o.Last.Last.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);

                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }

                                }

                            }
                            else if (dtSuppl.Rows[i]["SupplierResponseFormat"].ToString().ToUpper() == "HTML")
                            {
                                if (dtSuppl.Rows[i]["SupplierAPIMethod"].ToString().ToUpper() == "GET")
                                {
                                    if (dtSuppl.Rows[i]["SupplierURL"].ToString().ToUpper() == "HTTPS://WWW.1314PG.COM/API/USER/STOCK/11738/8789AE77D94A9CFB109C1BA5143ABAB6/")
                                    {
                                        _API = dtSuppl.Rows[i]["SupplierURL"].ToString();
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        string response = client.DownloadString(_API);
                                        string[] res = response.Split('\n');

                                        string[] columns = res.Where(w => w == res[0]).ToArray();

                                        string[] rows = res.Where(w => w != res[0]).ToArray();


                                        ConvertStringArrayToDatatable saDt = new ConvertStringArrayToDatatable();

                                        dt_APIRes = saDt.StringArrayToDataTable(columns, rows);
                                    }
                                }
                            }

                            if (dt_APIRes.Rows.Count > 0)
                            {
                                db = new Database();
                                para = new List<IDbDataParameter>();

                                para.Add(db.CreateParam("Supplier_Mas_Id", DbType.Int64, ParameterDirection.Input, SuppMst_Id));
                                DataTable dtSupplCol = db.ExecuteSP("SupplierCol_CustomCol_select", para.ToArray(), false);

                                if (dtSupplCol != null && dtSupplCol.Rows.Count > 0)
                                {
                                    Final_dt = new DataTable();

                                    Final_dt.Columns.Add("SUPPLIER_ID", typeof(string));
                                    Final_dt.Columns.Add("sRefNo", typeof(string));
                                    Final_dt.Columns.Add("sShape", typeof(string));
                                    Final_dt.Columns.Add("sCertiNo", typeof(string));
                                    Final_dt.Columns.Add("sPointer", typeof(string));
                                    Final_dt.Columns.Add("sColor", typeof(string));
                                    Final_dt.Columns.Add("sClarity", typeof(string));
                                    Final_dt.Columns.Add("dCts", typeof(string));
                                    Final_dt.Columns.Add("dRepPrice", typeof(string));
                                    Final_dt.Columns.Add("Price_Per_Cts", typeof(string));
                                    Final_dt.Columns.Add("dDisc", typeof(string));
                                    Final_dt.Columns.Add("Total_Sales_Price", typeof(string));
                                    Final_dt.Columns.Add("sCut", typeof(string));
                                    Final_dt.Columns.Add("sPolish", typeof(string));
                                    Final_dt.Columns.Add("sSymm", typeof(string));
                                    Final_dt.Columns.Add("sFls", typeof(string));
                                    Final_dt.Columns.Add("dLength", typeof(string));
                                    Final_dt.Columns.Add("dWidth", typeof(string));
                                    Final_dt.Columns.Add("dDepth", typeof(string));
                                    Final_dt.Columns.Add("Measurement", typeof(string));
                                    Final_dt.Columns.Add("dDepthPer", typeof(string));
                                    Final_dt.Columns.Add("dTablePer", typeof(string));
                                    Final_dt.Columns.Add("sStatus", typeof(string));
                                    Final_dt.Columns.Add("sLab", typeof(string));
                                    Final_dt.Columns.Add("dCrAng", typeof(string));
                                    Final_dt.Columns.Add("dCrHt", typeof(string));
                                    Final_dt.Columns.Add("dPavAng", typeof(string));
                                    Final_dt.Columns.Add("dPavHt", typeof(string));
                                    Final_dt.Columns.Add("sGirdle", typeof(string));
                                    Final_dt.Columns.Add("sShade", typeof(string));
                                    Final_dt.Columns.Add("sInclusion", typeof(string));
                                    Final_dt.Columns.Add("sTableNatts", typeof(string));
                                    Final_dt.Columns.Add("sSideNatts", typeof(string));
                                    Final_dt.Columns.Add("sCulet", typeof(string));
                                    Final_dt.Columns.Add("dTableDepth", typeof(string));
                                    Final_dt.Columns.Add("sComments", typeof(string));
                                    Final_dt.Columns.Add("sSymbol", typeof(string));
                                    Final_dt.Columns.Add("sLuster", typeof(string));
                                    Final_dt.Columns.Add("sStrLn", typeof(string));
                                    Final_dt.Columns.Add("sLrHalf", typeof(string));
                                    Final_dt.Columns.Add("dGirdlePer", typeof(string));
                                    Final_dt.Columns.Add("sGirdleType", typeof(string));
                                    Final_dt.Columns.Add("sCrownInclusion", typeof(string));
                                    Final_dt.Columns.Add("sCrownNatts", typeof(string));
                                    Final_dt.Columns.Add("dCertiDate", typeof(string));
                                    Final_dt.Columns.Add("sImglink", typeof(string));
                                    Final_dt.Columns.Add("sVdoLink", typeof(string));
                                    Final_dt.Columns.Add("Certi_Path", typeof(string));
                                    Final_dt.Columns.Add("Location", typeof(string));
                                    Final_dt.Columns.Add("BGM", typeof(string));
                                    Final_dt.Columns.Add("Fancy_Amount", typeof(string));
                                    Final_dt.Columns.Add("Table_Open", typeof(string));
                                    Final_dt.Columns.Add("Crown_Open", typeof(string));
                                    Final_dt.Columns.Add("Pav_Open", typeof(string));
                                    Final_dt.Columns.Add("Girdle_Open", typeof(string));
                                    //Final_dt.Columns.Add("AMT", typeof(string));
                                    //Final_dt.Columns.Add("BASE_DISC", typeof(string));
                                    //Final_dt.Columns.Add("BASE_VAL", typeof(string));
                                    Final_dt.Columns.Add("sInscription", typeof(string));
                                    Final_dt.Columns.Add("OrderBy", typeof(string));
                                    Final_dt.Columns.Add("DiamondDate", typeof(string));
                                    Final_dt.Columns.Add("TransferType", typeof(string));


                                    foreach (DataRow row in dt_APIRes.Rows)
                                    {
                                        DataRow dr = Final_dt.NewRow();
                                        dr["SUPPLIER_ID"] = SuppMst_Id.ToString();

                                        foreach (DataRow row1 in dtSupplCol.Rows)
                                        {
                                            string sRefNo = (row1["CustomColName"].ToString() == "sRefNo" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sRefNo"].ToString());
                                            dr["sRefNo"] = (String.IsNullOrEmpty(sRefNo) ? null : sRefNo);
                                            
                                            string sShape = (row1["CustomColName"].ToString() == "sShape" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sShape"].ToString());
                                            dr["sShape"] = (String.IsNullOrEmpty(sShape) ? null : sShape);

                                            string sCertiNo = (row1["CustomColName"].ToString() == "sCertiNo" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sCertiNo"].ToString());
                                            dr["sCertiNo"] = (String.IsNullOrEmpty(sCertiNo) ? null : sCertiNo);

                                            string sPointer = (row1["CustomColName"].ToString() == "sPointer" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sPointer"].ToString());
                                            dr["sPointer"] = (String.IsNullOrEmpty(sPointer) ? null : sPointer);

                                            string sColor = (row1["CustomColName"].ToString() == "sColor" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sColor"].ToString());
                                            dr["sColor"] = (String.IsNullOrEmpty(sColor) ? null : sColor);

                                            string sClarity = (row1["CustomColName"].ToString() == "sClarity" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sClarity"].ToString());
                                            dr["sClarity"] = (String.IsNullOrEmpty(sClarity) ? null : sClarity);

                                            string dCts = (row1["CustomColName"].ToString() == "dCts" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dCts"].ToString());
                                            dr["dCts"] = (String.IsNullOrEmpty(dCts) ? null : dCts);

                                            string dRepPrice = (row1["CustomColName"].ToString() == "dRepPrice" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dRepPrice"].ToString());
                                            dr["dRepPrice"] = (String.IsNullOrEmpty(dRepPrice) ? null : dRepPrice);

                                            string Price_Per_Cts = (row1["CustomColName"].ToString() == "Price_Per_Cts" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Price_Per_Cts"].ToString());
                                            dr["Price_Per_Cts"] = (String.IsNullOrEmpty(Price_Per_Cts) ? null : Price_Per_Cts);

                                            string dDisc = (row1["CustomColName"].ToString() == "dDisc" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dDisc"].ToString());
                                            dr["dDisc"] = (String.IsNullOrEmpty(dDisc) ? null : dDisc);

                                            string Total_Sales_Price = (row1["CustomColName"].ToString() == "Total_Sales_Price" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Total_Sales_Price"].ToString());
                                            dr["Total_Sales_Price"] = (String.IsNullOrEmpty(Total_Sales_Price) ? null : Total_Sales_Price);

                                            string sCut = (row1["CustomColName"].ToString() == "sCut" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sCut"].ToString());
                                            dr["sCut"] = (String.IsNullOrEmpty(sCut) ? null : sCut);

                                            string sPolish = (row1["CustomColName"].ToString() == "sPolish" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sPolish"].ToString());
                                            dr["sPolish"] = (String.IsNullOrEmpty(sPolish) ? null : sPolish);

                                            string sSymm = (row1["CustomColName"].ToString() == "sSymm" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sSymm"].ToString());
                                            dr["sSymm"] = (String.IsNullOrEmpty(sSymm) ? null : sSymm);

                                            string sFls = (row1["CustomColName"].ToString() == "sFls" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sFls"].ToString());
                                            dr["sFls"] = (String.IsNullOrEmpty(sFls) ? null : sFls);

                                            string dLength = (row1["CustomColName"].ToString() == "dLength" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dLength"].ToString());
                                            dr["dLength"] = (String.IsNullOrEmpty(dLength) ? null : dLength);

                                            string dWidth = (row1["CustomColName"].ToString() == "dWidth" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dWidth"].ToString());
                                            dr["dWidth"] = (String.IsNullOrEmpty(dWidth) ? null : dWidth);

                                            string dDepth = (row1["CustomColName"].ToString() == "dDepth" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dDepth"].ToString());
                                            dr["dDepth"] = (String.IsNullOrEmpty(dDepth) ? null : dDepth);

                                            string Measurement = (row1["CustomColName"].ToString() == "Measurement" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Measurement"].ToString());
                                            dr["Measurement"] = (String.IsNullOrEmpty(Measurement) ? null : Measurement);

                                            string dDepthPer = (row1["CustomColName"].ToString() == "dDepthPer" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dDepthPer"].ToString());
                                            dr["dDepthPer"] = (String.IsNullOrEmpty(dDepthPer) ? null : dDepthPer);

                                            string dTablePer = (row1["CustomColName"].ToString() == "dTablePer" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dTablePer"].ToString());
                                            dr["dTablePer"] = (String.IsNullOrEmpty(dTablePer) ? null : dTablePer);

                                            string sStatus = (row1["CustomColName"].ToString() == "sStatus" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sStatus"].ToString());
                                            dr["sStatus"] = (String.IsNullOrEmpty(sStatus) ? null : sStatus);

                                            string sLab = (row1["CustomColName"].ToString() == "sLab" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sLab"].ToString());
                                            dr["sLab"] = (String.IsNullOrEmpty(sLab) ? null : sLab);

                                            string dCrAng = (row1["CustomColName"].ToString() == "dCrAng" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dCrAng"].ToString());
                                            dr["dCrAng"] = (String.IsNullOrEmpty(dCrAng) ? null : dCrAng);

                                            string dCrHt = (row1["CustomColName"].ToString() == "dCrHt" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dCrHt"].ToString());
                                            dr["dCrHt"] = (String.IsNullOrEmpty(dCrHt) ? null : dCrHt);

                                            string dPavAng = (row1["CustomColName"].ToString() == "dPavAng" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dPavAng"].ToString());
                                            dr["dPavAng"] = (String.IsNullOrEmpty(dPavAng) ? null : dPavAng);

                                            string dPavHt = (row1["CustomColName"].ToString() == "dPavHt" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dPavHt"].ToString());
                                            dr["dPavHt"] = (String.IsNullOrEmpty(dPavHt) ? null : dPavHt);

                                            string sGirdle = (row1["CustomColName"].ToString() == "sGirdle" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sGirdle"].ToString());
                                            dr["sGirdle"] = (String.IsNullOrEmpty(sGirdle) ? null : sGirdle);

                                            string sShade = (row1["CustomColName"].ToString() == "sShade" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sShade"].ToString());
                                            dr["sShade"] = (String.IsNullOrEmpty(sShade) ? null : sShade);

                                            string sInclusion = (row1["CustomColName"].ToString() == "sInclusion" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sInclusion"].ToString());
                                            dr["sInclusion"] = (String.IsNullOrEmpty(sInclusion) ? null : sInclusion);

                                            string sTableNatts = (row1["CustomColName"].ToString() == "sTableNatts" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sTableNatts"].ToString());
                                            dr["sTableNatts"] = (String.IsNullOrEmpty(sTableNatts) ? null : sTableNatts);

                                            string sSideNatts = (row1["CustomColName"].ToString() == "sSideNatts" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sSideNatts"].ToString());
                                            dr["sSideNatts"] = (String.IsNullOrEmpty(sSideNatts) ? null : sSideNatts);

                                            string sCulet = (row1["CustomColName"].ToString() == "sCulet" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sCulet"].ToString());
                                            dr["sCulet"] = (String.IsNullOrEmpty(sCulet) ? null : sCulet);

                                            string dTableDepth = (row1["CustomColName"].ToString() == "dTableDepth" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dTableDepth"].ToString());
                                            dr["dTableDepth"] = (String.IsNullOrEmpty(dTableDepth) ? null : dTableDepth);

                                            string sComments = (row1["CustomColName"].ToString() == "sComments" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sComments"].ToString());
                                            dr["sComments"] = (String.IsNullOrEmpty(sComments) ? null : sComments);

                                            string sSymbol = (row1["CustomColName"].ToString() == "sSymbol" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sSymbol"].ToString());
                                            dr["sSymbol"] = (String.IsNullOrEmpty(sSymbol) ? null : sSymbol);

                                            string sLuster = (row1["CustomColName"].ToString() == "sLuster" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sLuster"].ToString());
                                            dr["sLuster"] = (String.IsNullOrEmpty(sLuster) ? null : sLuster);

                                            string sStrLn = (row1["CustomColName"].ToString() == "sStrLn" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sStrLn"].ToString());
                                            dr["sStrLn"] = (String.IsNullOrEmpty(sStrLn) ? null : sStrLn);

                                            string sLrHalf = (row1["CustomColName"].ToString() == "sLrHalf" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sLrHalf"].ToString());
                                            dr["sLrHalf"] = (String.IsNullOrEmpty(sLrHalf) ? null : sLrHalf);

                                            string dGirdlePer = (row1["CustomColName"].ToString() == "dGirdlePer" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dGirdlePer"].ToString());
                                            dr["dGirdlePer"] = (String.IsNullOrEmpty(dGirdlePer) ? null : dGirdlePer);

                                            string sGirdleType = (row1["CustomColName"].ToString() == "sGirdleType" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sGirdleType"].ToString());
                                            dr["sGirdleType"] = (String.IsNullOrEmpty(sGirdleType) ? null : sGirdleType);

                                            string sCrownInclusion = (row1["CustomColName"].ToString() == "sCrownInclusion" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sCrownInclusion"].ToString());
                                            dr["sCrownInclusion"] = (String.IsNullOrEmpty(sCrownInclusion) ? null : sCrownInclusion);

                                            string sCrownNatts = (row1["CustomColName"].ToString() == "sCrownNatts" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sCrownNatts"].ToString());
                                            dr["sCrownNatts"] = (String.IsNullOrEmpty(sCrownNatts) ? null : sCrownNatts);

                                            string dCertiDate = (row1["CustomColName"].ToString() == "dCertiDate" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["dCertiDate"].ToString());
                                            dr["dCertiDate"] = (String.IsNullOrEmpty(dCertiDate) ? null : dCertiDate);

                                            string sImglink = (row1["CustomColName"].ToString() == "sImglink" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sImglink"].ToString());
                                            dr["sImglink"] = (String.IsNullOrEmpty(sImglink) ? null : sImglink);

                                            string sVdoLink = (row1["CustomColName"].ToString() == "sVdoLink" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sVdoLink"].ToString());
                                            dr["sVdoLink"] = (String.IsNullOrEmpty(sVdoLink) ? null : sVdoLink);

                                            string Certi_Path = (row1["CustomColName"].ToString() == "Certi_Path" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Certi_Path"].ToString());
                                            dr["Certi_Path"] = (String.IsNullOrEmpty(Certi_Path) ? null : Certi_Path);

                                            string Location = (row1["CustomColName"].ToString() == "Location" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Location"].ToString());
                                            dr["Location"] = (String.IsNullOrEmpty(Location) ? null : Location);

                                            string BGM = (row1["CustomColName"].ToString() == "BGM" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["BGM"].ToString());
                                            dr["BGM"] = (String.IsNullOrEmpty(BGM) ? null : BGM);

                                            string Fancy_Amount = (row1["CustomColName"].ToString() == "Fancy_Amount" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Fancy_Amount"].ToString());
                                            dr["Fancy_Amount"] = (String.IsNullOrEmpty(Fancy_Amount) ? null : Fancy_Amount);

                                            string Table_Open = (row1["CustomColName"].ToString() == "Table_Open" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Table_Open"].ToString());
                                            dr["Table_Open"] = (String.IsNullOrEmpty(Table_Open) ? null : Table_Open);

                                            string Crown_Open = (row1["CustomColName"].ToString() == "Crown_Open" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Crown_Open"].ToString());
                                            dr["Crown_Open"] = (String.IsNullOrEmpty(Crown_Open) ? null : Crown_Open);

                                            string Pav_Open = (row1["CustomColName"].ToString() == "Pav_Open" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Pav_Open"].ToString());
                                            dr["Pav_Open"] = (String.IsNullOrEmpty(Pav_Open) ? null : Pav_Open);

                                            string Girdle_Open = (row1["CustomColName"].ToString() == "Girdle_Open" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["Girdle_Open"].ToString());
                                            dr["Girdle_Open"] = (String.IsNullOrEmpty(Girdle_Open) ? null : Girdle_Open);

                                            //dr["AMT"] = (row1["CustomColName"].ToString() == "AMT" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["AMT"].ToString());
                                            //dr["BASE_DISC"] = (row1["CustomColName"].ToString() == "BASE_DISC" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["BASE_DISC"].ToString());
                                            //dr["BASE_VAL"] = (row1["CustomColName"].ToString() == "BASE_VAL" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["BASE_VAL"].ToString());

                                            string sInscription = (row1["CustomColName"].ToString() == "sInscription" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["sInscription"].ToString());
                                            dr["sInscription"] = (String.IsNullOrEmpty(sInscription) ? null : sInscription);

                                            string OrderBy = (row1["CustomColName"].ToString() == "OrderBy" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["OrderBy"].ToString());
                                            dr["OrderBy"] = (String.IsNullOrEmpty(OrderBy) ? null : OrderBy);

                                            string DiamondDate = (row1["CustomColName"].ToString() == "DiamondDate" && row1["SupplierColumnName"].ToString() != "" ? row[row1["SupplierColumnName"].ToString()].ToString() : dr["DiamondDate"].ToString());
                                            dr["DiamondDate"] = (String.IsNullOrEmpty(DiamondDate) ? null : DiamondDate);

                                            dr["TransferType"] = "AUTO";
                                        }

                                        Final_dt.Rows.Add(dr);
                                    }


                                    if (Final_dt != null && Final_dt.Rows.Count > 0)
                                    {
                                        db = new Database();
                                        DataTable SupStkUploadDT = new DataTable();
                                        List<SqlParameter> para1 = new List<SqlParameter>();

                                        SqlParameter param = new SqlParameter("tabledt", SqlDbType.Structured);
                                        param.Value = Final_dt;
                                        para1.Add(param);

                                        SupStkUploadDT = db.ExecuteSP("ManualAuto_StockDetail_Ora_Insert", para1.ToArray(), false);

                                        if (SupStkUploadDT != null)
                                        {
                                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                            sb.Append(SupStkUploadDT.Rows[0]["Message"].ToString() + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                            sb.AppendLine("");
                                            File.AppendAllText(path, sb.ToString());
                                            sb.Clear();


                                            string _tempPath = HostingEnvironment.MapPath("~/Temp/API/");
                                            if (!Directory.Exists(_tempPath))
                                            {
                                                Directory.CreateDirectory(_tempPath);
                                            }

                                            if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "XML")
                                            {
                                                filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".xml";
                                                filefullpath = _tempPath + filename;
                                                APIFileName = APIFileName + ".xml";

                                                if (File.Exists(filefullpath))
                                                {
                                                    File.Delete(filefullpath);
                                                }

                                                dt_APIRes.TableName = "Records";
                                                dt_APIRes.WriteXml(filefullpath);
                                            }
                                            else if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "CSV")
                                            {
                                                filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".csv";
                                                filefullpath = _tempPath + filename;
                                                APIFileName = APIFileName + ".csv";

                                                if (File.Exists(filefullpath))
                                                {
                                                    File.Delete(filefullpath);
                                                }

                                                StringBuilder sb1 = new StringBuilder();
                                                IEnumerable<string> columnNames = dt_APIRes.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                                                sb1.AppendLine(string.Join(",", columnNames));

                                                foreach (DataRow row in dt_APIRes.Rows)
                                                {
                                                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Replace(",", " "));
                                                    sb1.AppendLine(string.Join(",", fields));
                                                }
                                                File.WriteAllText(filefullpath, sb1.ToString());
                                            }
                                            else if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLSX)" || dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLS)")
                                            {
                                                if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLSX)")
                                                {
                                                    filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".xlsx";
                                                    filefullpath = _tempPath + filename;
                                                    APIFileName = APIFileName + ".xlsx";
                                                }
                                                else if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLS)")
                                                {
                                                    filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".xls";
                                                    filefullpath = _tempPath + filename;
                                                    APIFileName = APIFileName + ".xls";
                                                }

                                                if (File.Exists(filefullpath))
                                                {
                                                    File.Delete(filefullpath);
                                                }

                                                FileInfo newFile = new FileInfo(filefullpath);
                                                using (ExcelPackage pck = new ExcelPackage(newFile))
                                                {
                                                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(APIFileName);
                                                    pck.Workbook.Properties.Title = "API";
                                                    ws.Cells["A1"].LoadFromDataTable(dt_APIRes, true);

                                                    ws.View.FreezePanes(2, 1);
                                                    var allCells = ws.Cells[ws.Dimension.Address];
                                                    allCells.AutoFilter = true;
                                                    allCells.AutoFitColumns();

                                                    int rowStart = ws.Dimension.Start.Row;
                                                    int rowEnd = ws.Dimension.End.Row;
                                                    removingGreenTagWarning(ws, ws.Cells[1, 1, rowEnd, 100].Address);

                                                    var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                                                    headerCells.Style.Font.Bold = true;
                                                    headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                                                    pck.Save();
                                                }
                                            }
                                            else if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "JSON (FILE)")
                                            {
                                                filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".json";
                                                filefullpath = _tempPath + filename;
                                                APIFileName = APIFileName + ".json";

                                                if (File.Exists(filefullpath))
                                                {
                                                    File.Delete(filefullpath);
                                                }
                                                string json = IPadCommon.DataTableToJSONWithStringBuilder(dt_APIRes);
                                                File.WriteAllText(filefullpath, json);
                                            }
                                            if (File.Exists(filefullpath))
                                            {
                                                File.Copy(filefullpath, tempPath + "\\" + APIFileName, true);

                                                SupplierLog(SuppMst_Id, true, "Success");
                                            }
                                        }
                                        else
                                        {
                                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                            sb.Append("Supplier Stock Upload Failed, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                            sb.AppendLine("");
                                            File.AppendAllText(path, sb.ToString());
                                            sb.Clear();
                                        }
                                    }
                                    else
                                    {
                                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                        sb.Append("Supplier Final Stock Not Found, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                        sb.AppendLine("");
                                        File.AppendAllText(path, sb.ToString());
                                        sb.Clear();
                                    }
                                }
                                else
                                {
                                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                    sb.Append(dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier Column Setting Not Found, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                    sb.AppendLine("");
                                    File.AppendAllText(path, sb.ToString());
                                    sb.Clear();
                                }
                            }
                            else
                            {
                                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                sb.Append("Supplier Stock Not Found, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                sb.AppendLine("");
                                File.AppendAllText(path, sb.ToString());
                                sb.Clear();
                            }

                            Supplier_Start_End(SuppMst_Id, "End");
                        }
                        catch (Exception ex)
                        {
                            Supplier_Start_End(SuppMst_Id, "End");
                            SupplierLog(SuppMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());

                            //abc = 5;
                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                            sb.Append(ex.Message.ToString() + ' ' + ex.StackTrace.ToString() + ", Log Time: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                            sb.AppendLine("");
                            File.AppendAllText(path, sb.ToString());
                            sb.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message.ToString() + ' ' + ex.StackTrace.ToString() + ", Log Time: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
            }

            return Ok(new CommonResponse
            {
                Error = null,
                Message = "",
                Status = ""
            });
        }
        public static void Supplier_Start_End(int Supplier_Mas_Id, string type)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Supplier_Mas_Id", DbType.Int64, ParameterDirection.Input, Supplier_Mas_Id));
                para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, type));

                DataTable dt = db.ExecuteSP("Supplier_Get_Start_CRUD", para.ToArray(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SupplierLog(int Supplier_Mas_Id, bool FileTransfer, string message)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Supplier_Mas_Id", DbType.Int64, ParameterDirection.Input, Supplier_Mas_Id));
                para.Add(db.CreateParam("FileTransfer", DbType.Boolean, ParameterDirection.Input, FileTransfer));
                para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, message));

                DataTable dt = db.ExecuteSP("SupplierLog", para.ToArray(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void removingGreenTagWarning(ExcelWorksheet template1, string address)
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
    }
}
