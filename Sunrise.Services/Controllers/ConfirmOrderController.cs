using DAL;
using EpExcelExportLib;
using Lib.Constants;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;
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
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/ConfirmOrder")]
    public class ConfirmOrderController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetOrderHistory([FromBody] JObject data)
        {
            OrderHistoryRequest orderHistoryRequest = new OrderHistoryRequest();
            try
            {
                orderHistoryRequest = JsonConvert.DeserializeObject<OrderHistoryRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<OrderHistoryResponse>
                {
                    Data = new List<OrderHistoryResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable dtData = GetOrderHistoryInner(orderHistoryRequest);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataRow[] dra = dtData.Select("iSr IS NULL");
                    OrderSummary searchSummary = new OrderSummary();
                    if (dra.Length > 0)
                    {
                        searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                        searchSummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                        searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["sRefNo"]);
                        searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["dCts"]);
                        searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble((dra[0]["dRapAmount"].ToString() != "" && dra[0]["dRapAmount"].ToString() != null ? dra[0]["dRapAmount"] : "0"));
                        searchSummary.TOT_NET_AMOUNT = Convert.ToDouble((dra[0]["Net_Value"].ToString() != "" && dra[0]["Net_Value"].ToString() != null ? dra[0]["Net_Value"] : "0"));  //Convert.ToDouble(dra[0]["dNetPrice"]);
                        searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((dra[0]["Final_Disc"].ToString() != "" && dra[0]["Final_Disc"].ToString() != null ? dra[0]["Final_Disc"] : "0")); //Convert.ToDouble(dra[0]["sSupplDisc"]);
                    }

                    dtData.DefaultView.RowFilter = "iSr IS NOT NULL";
                    dtData = dtData.DefaultView.ToTable();
                    if (dtData != null && dtData.Rows.Count > 0)
                    {

                        List<OrderList> listOrder = new List<OrderList>();
                        listOrder = DataTableExtension.ToList<OrderList>(dtData);
                        List<OrderHistoryResponse> orderHistoryResponse = new List<OrderHistoryResponse>();

                        //orderHistoryResponse = DataTableExtension.ToList<OrderHistoryResponse>(dtData);
                        if (listOrder.Count > 0)
                        { //List<string> lst = dtData.AsDataView().ToTable(true, "CompanyName").ToList();
                          //var a = (from r in dtData.AsEnumerable()
                          //  select r["CompanyName"]).Distinct().ToList();
                            orderHistoryResponse.Add(new OrderHistoryResponse()
                            {
                                DataList = listOrder,
                                DataSummary = searchSummary

                            });

                            return Ok(new ServiceResponse<OrderHistoryResponse>
                            {
                                Data = orderHistoryResponse,
                                Message = "SUCCESS",
                                Status = "1"
                            });
                        }
                        else
                        {
                            return Ok(new ServiceResponse<OrderHistoryResponse>
                            {
                                Data = orderHistoryResponse,
                                Message = "Something Went wrong.",
                                Status = "0"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new ServiceResponse<OrderHistoryResponse>
                        {
                            Data = null,
                            Message = "No data found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<OrderHistoryResponse>
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
                return Ok(new ServiceResponse<OrderHistoryResponse>
                {
                    Data = new List<OrderHistoryResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult GetOrderHistoryFilters([FromBody] JObject data)
        {
            OrderHistoryRequest orderHistoryRequest = new OrderHistoryRequest();
            try
            {
                orderHistoryRequest = JsonConvert.DeserializeObject<OrderHistoryRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<OrderHistoryFiltersResponse>
                {
                    Data = new List<OrderHistoryFiltersResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable dtData = GetOrderHistoryInner(orderHistoryRequest);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    dtData.DefaultView.RowFilter = "iSr IS NOT NULL";
                    dtData = dtData.DefaultView.ToTable();
                    if (dtData != null && dtData.Rows.Count > 0)
                    {

                        List<OrderList> listOrder = new List<OrderList>();
                        listOrder = DataTableExtension.ToList<OrderList>(dtData);
                        List<OrderHistoryFiltersResponse> orderHistoryFiltersResponse = new List<OrderHistoryFiltersResponse>();

                        if (listOrder.Count > 0)
                        {
                            orderHistoryFiltersResponse.Add(new OrderHistoryFiltersResponse()
                            {
                                Companies = dtData.AsDataView().ToTable(true, "CompanyName").Rows.OfType<DataRow>().Select(dr => Convert.ToString(dr["CompanyName"])).ToList(),
                                Customers = dtData.AsDataView().ToTable(true, "CustomerName").Rows.OfType<DataRow>().Select(dr => Convert.ToString(dr["CustomerName"])).ToList(),
                                Status = dtData.AsDataView().ToTable(true, "sStoneStatus").Rows.OfType<DataRow>().Select(dr => Convert.ToString(dr["sStoneStatus"])).ToList(),
                                Users = dtData.AsDataView().ToTable(true, "UserName").Rows.OfType<DataRow>().Select(dr => Convert.ToString(dr["UserName"])).ToList(),
                                Locations = dtData.AsDataView().ToTable(true, "Location").Rows.OfType<DataRow>().Select(dr => Convert.ToString(dr["Location"])).ToList()
                            });

                            return Ok(new ServiceResponse<OrderHistoryFiltersResponse>
                            {
                                Data = orderHistoryFiltersResponse,
                                Message = "SUCCESS",
                                Status = "1"
                            });
                        }
                        else
                        {
                            return Ok(new ServiceResponse<OrderHistoryFiltersResponse>
                            {
                                Data = orderHistoryFiltersResponse,
                                Message = "Something Went wrong.",
                                Status = "0"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new ServiceResponse<OrderHistoryFiltersResponse>
                        {
                            Data = null,
                            Message = "No data found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<OrderHistoryFiltersResponse>
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
                return Ok(new ServiceResponse<OrderHistoryFiltersResponse>
                {
                    Data = new List<OrderHistoryFiltersResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [NonAction]
        private DataTable GetOrderHistoryInner(OrderHistoryRequest orderHistoryRequest)
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(orderHistoryRequest.iUserid_FullOrderDate))
                    para.Add(db.CreateParam("iOrderid_sRefNo", DbType.String, ParameterDirection.Input, orderHistoryRequest.iUserid_FullOrderDate));
                else
                    para.Add(db.CreateParam("iOrderid_sRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (userID > 0)
                    para.Add(db.CreateParam("iUserId", DbType.String, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("iUserId", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, orderHistoryRequest.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, orderHistoryRequest.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.PageNo))
                    para.Add(db.CreateParam("PageNo", DbType.Int16, ParameterDirection.Input, Convert.ToInt16(orderHistoryRequest.PageNo)));
                else
                    para.Add(db.CreateParam("PageNo", DbType.Int16, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.RefNo))
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, (orderHistoryRequest.RefNo)));
                else
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(orderHistoryRequest.CommonName))
                    para.Add(db.CreateParam("CommonName", DbType.String, ParameterDirection.Input, (orderHistoryRequest.CommonName)));
                else
                    para.Add(db.CreateParam("CommonName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(orderHistoryRequest.CompanyName))
                    para.Add(db.CreateParam("CompanyName", DbType.String, ParameterDirection.Input, (orderHistoryRequest.CompanyName)));
                else
                    para.Add(db.CreateParam("CompanyName", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.CustomerName))
                    para.Add(db.CreateParam("CustomerName", DbType.String, ParameterDirection.Input, (orderHistoryRequest.CustomerName)));
                else
                    para.Add(db.CreateParam("CustomerName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(orderHistoryRequest.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, (orderHistoryRequest.OrderBy)));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("PgSize", DbType.String, ParameterDirection.Input, (orderHistoryRequest.PgSize)));

                para.Add(db.CreateParam("DateStatus", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.DateStatus));
                para.Add(db.CreateParam("SubUser", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.SubUser));
                //para.Add(db.CreateParam("ConfirmOrder", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.ConfirmOrder));
                //para.Add(db.CreateParam("NotConfirmOrder", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.NotConfirmOrder));
                DataTable dsData = db.ExecuteSP("IPD_Get_Order_History_WithSupplierPrice_Sunrise", para.ToArray(), false);

                return dsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult DownloadOrderHistory([FromBody] JObject data)
        {
            OrderHistoryRequest orderHistoryRequest = new OrderHistoryRequest();
            try
            {
                orderHistoryRequest = JsonConvert.DeserializeObject<OrderHistoryRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<OrderHistoryResponse>
                {
                    Data = new List<OrderHistoryResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataSet ds = new DataSet();
                DataTable dtData = GetOrderHistoryInner(orderHistoryRequest);
                DataTable dtSumm = new DataTable();

                dtSumm.Columns.Add("TOT_PAGE", typeof(Int32));
                dtSumm.Columns.Add("PAGE_SIZE", typeof(Int32));
                dtSumm.Columns.Add("TOT_PCS", typeof(Int32));
                dtSumm.Columns.Add("TOT_CTS", typeof(Decimal));
                dtSumm.Columns.Add("TOT_RAP_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("TOT_NET_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("AVG_PRICE_PER_CTS", typeof(Decimal));
                dtSumm.Columns.Add("AVG_SALES_DISC_PER", typeof(Decimal));

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataRow[] dra = dtData.Select("iSr IS NULL");

                    if (dra.Length > 0)
                    {
                        DataRow dr = dtSumm.NewRow();
                        dr["TOT_PAGE"] = dra[0]["TOTAL_PAGE"];
                        dr["PAGE_SIZE"] = dra[0]["PAGE_SIZE"];
                        dr["TOT_PCS"] = dra[0]["sRefNo"];
                        dr["TOT_CTS"] = dra[0]["dCts"];
                        dr["TOT_RAP_AMOUNT"] = (dra[0]["dRapAmount"].ToString() != "" && dra[0]["dRapAmount"].ToString() != null ? dra[0]["dRapAmount"] : "0");
                        dr["TOT_NET_AMOUNT"] = dra[0]["dNetPrice"];
                        // dr["AVG_PRICE_PER_CTS"] = dra[0]["dNetPrice"];
                        dr["AVG_SALES_DISC_PER"] = (dra[0]["sSupplDisc"].ToString() != "" && dra[0]["sSupplDisc"].ToString() != null ? dra[0]["sSupplDisc"] : "0");
                        dtSumm.Rows.Add(dr);
                    }


                    dtData.DefaultView.RowFilter = "iSr IS NOT NULL";
                    dtData = dtData.DefaultView.ToTable();
                    dtSumm.TableName = "SummaryTable";
                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        string filename = "SupplierOrderHistory " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                        string _path = ConfigurationManager.AppSettings["data"];
                        string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                        string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                        //EpExcelExport.Excel_Generate(dtData.DefaultView.ToTable(), realpath + "Data" + random + ".xlsx");
                        EpExcelExport.CreatePurchaseOrderExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath,
                                                        Convert.ToDateTime(orderHistoryRequest.FromDate), Convert.ToDateTime(orderHistoryRequest.ToDate), orderHistoryRequest.isAdmin, orderHistoryRequest.isEmp);

                        string _strxml = _path + filename + ".xlsx";
                        return Ok(_strxml);
                    }
                    return Ok("No data found.");
                }

                return Ok("No data found.");
                //ds.Tables.Add(dtData);
                //ds.Tables.Add(dtSumm);

                //return ds;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Something Went wrong.\nPlease try again later");
            }
        }

        [HttpPost]
        public IHttpActionResult ConfirMorderSuccess()
        {
            try
            {
                List<ConfirmPlaceOrderResponse> res_list = new List<ConfirmPlaceOrderResponse>();

                res_list.Add(new ConfirmPlaceOrderResponse()
                {
                    RefNo = "GH-10001",
                    SunriseStatus = "Confirm",
                    SupplierName = "SHAIRU GEMS DIAMONDS PVT. LTD - (S)",
                    SupplierStatus = "Your Transaction Done Successfully",
                });
                res_list.Add(new ConfirmPlaceOrderResponse()
                {
                    RefNo = "GH-10002",
                    SunriseStatus = "Not Available",
                    SupplierName = "SHAIRU GEMS DIAMONDS PVT. LTD - (S)",
                    SupplierStatus = "This Stone(S) Are Subject To Availability",
                });
                res_list.Add(new ConfirmPlaceOrderResponse()
                {
                    RefNo = "GH-10003",
                    SunriseStatus = "Error",
                    SupplierName = "SHAIRU GEMS DIAMONDS PVT. LTD - (S)",
                    SupplierStatus = "Not Processed",
                });
                res_list.Add(new ConfirmPlaceOrderResponse()
                {
                    RefNo = "GH-10004",
                    SunriseStatus = "Error",
                    SupplierName = "SHAIRU GEMS DIAMONDS PVT. LTD - (S)",
                    SupplierStatus = "Failed To Confirm Stone",
                });
                res_list.Add(new ConfirmPlaceOrderResponse()
                {
                    RefNo = "GH-10005",
                    SunriseStatus = "Error",
                    SupplierName = "SHAIRU GEMS DIAMONDS PVT. LTD - (S)",
                    SupplierStatus = "No Record Will Be Proceed",
                });
                res_list.Add(new ConfirmPlaceOrderResponse()
                {
                    RefNo = "11111",
                    SunriseStatus = "Confirm",
                    SupplierName = "J.B. AND BROTHERS PVT. LTD - (S)",
                    SupplierStatus = "Buying Successful",
                });
                res_list.Add(new ConfirmPlaceOrderResponse()
                {
                    RefNo = "22222",
                    SunriseStatus = "Pending",
                    SupplierName = "J.B. AND BROTHERS PVT. LTD - (S)",
                    SupplierStatus = "Error Please Try Again!",
                });
                res_list.Add(new ConfirmPlaceOrderResponse()
                {
                    RefNo = "33333",
                    SunriseStatus = "Not Available",
                    SupplierName = "J.B. AND BROTHERS PVT. LTD - (S)",
                    SupplierStatus = "Packet Unavailable!",
                });
                res_list.Add(new ConfirmPlaceOrderResponse()
                {
                    RefNo = "44444",
                    SunriseStatus = "Error",
                    SupplierName = "J.B. AND BROTHERS PVT. LTD - (S)",
                    SupplierStatus = "Invalid Ref No!",
                });


                if (res_list.Count() > 0)
                {
                    return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                    {
                        Data = res_list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                    {
                        Data = res_list,
                        Message = "No Record will be Proceed",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                {
                    Data = new List<ConfirmPlaceOrderResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        //[NonAction]
        //private void CallSupplierAPI(int UserId, string Refno, string Comments)
        //{

        //    try
        //    {
        //        DataTable dtSupplierRef = GetSupplierWiseRefno(Refno, UserId);
        //        if (dtSupplierRef != null && dtSupplierRef.Rows.Count > 0)
        //        {
        //            for (int i = 0; i <= dtSupplierRef.Rows.Count - 1; i++)
        //            {
        //                if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "SHAIRU GEMS DIAMONDS PVT. LTD - (S)".ToString().ToUpper())
        //                {
        //                    if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
        //                    {
        //                        string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
        //                        string _LoginResponse = GetShairuAPITaken();
        //                        ShairuApiLoginResponse _data = new ShairuApiLoginResponse();
        //                        _data = (new JavaScriptSerializer()).Deserialize<ShairuApiLoginResponse>(_LoginResponse);
        //                        if (_data.UserId > 0)
        //                        {
        //                            foreach (string StoneId in RefnoList)
        //                            {
        //                                if (StoneId.ToString() != "")
        //                                {
        //                                    string _HoldResponse = ShairuHoldStoneRequest(_data.TokenId.ToString(), StoneId, _data.UserId, Comments);
        //                                    ShairuApiHoldResponse _Holddata = new ShairuApiHoldResponse();
        //                                    _Holddata = (new JavaScriptSerializer()).Deserialize<ShairuApiHoldResponse>(_HoldResponse);
        //                                    InsertSupplierAPIResponse(StoneId, _Holddata.Status, _Holddata.Message, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "", null);
        //                                }
        //                            }
        //                        }

        //                    }
        //                }

        //                if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "J.B. AND BROTHERS PVT. LTD - (S)".ToString().ToUpper())
        //                {
        //                    if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
        //                    {
        //                        string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
        //                        foreach (string StoneId in RefnoList)
        //                        {
        //                            if (StoneId.ToString() != "")
        //                            {
        //                                string _HoldResponse = JBHoldRequest("1234|-12.3");//JBHoldRequest(StoneId);
        //                                JBApiHoldResponse _Holddata = new JBApiHoldResponse();
        //                                _Holddata = (new JavaScriptSerializer()).Deserialize<JBApiHoldResponse>(_HoldResponse);
        //                                InsertSupplierAPIResponse(StoneId, _Holddata.Status, "Success", "J.B. AND BROTHERS PVT. LTD - (S)", "", null, LogInID);
        //                            }
        //                        }
        //                    }
        //                    //    JBHoldRequest("1234|-12.3");
        //                }

        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        [NonAction]
        private DataTable GetSupplierWiseRefno(string Refno, int UserId)
        {
            Database db = new Database();

            List<IDbDataParameter> para;
            para = new List<IDbDataParameter>();
            para.Add(db.CreateParam("sRefno", DbType.String, ParameterDirection.Input, Refno));
            para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, UserId));

            DataTable dt = db.ExecuteSP("RefNoWiseSupplierName_Get", para.ToArray(), false);

            return dt;


        }

        public string GetShairuAPITaken()
        {
            String API = "https://shairugems.net:8011/api/Buyer/login";
            var input = new LoginRequest
            {
                UserName = "samit_gandhi",
                Password = "missme@hk"

            };
            string InputPara = string.Join("&", input.GetType()
                                                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                        .Where(p => p.GetValue(input, null) != null)
                               .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(input).ToString())}"));

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/x-www-form-urlencoded";
            client.Encoding = Encoding.UTF8;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string json;

            try
            {
                json = client.UploadString(API, InputPara);
                return json;
            }
            catch (WebException ex)
            {
                //if (ex.Status)
                var webException = ex as WebException;

                return json = "";
            }
            catch (Exception ex)
            {
                json = ex.Message;
                return json;
            }
        }

        public string ShairuHoldStoneRequest(string Token, string StoneId, int UserId, string Comment)
        {
            string apiUrl = "https://shairugems.net:8011/api/Buyer/HoldStone";
            var input = new
            {
                StoneID = StoneId,
                UserID = UserId,
                //Comments = Comment,
                Comments = "Confirm",
                TokenId = Token
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers.Add("Authorization", "Bearer " + Token);
            client.Headers.Add("Content-type", "application/json");
            client.Encoding = Encoding.UTF8;
            string json = string.Empty;
            try
            {
                json = client.UploadString(apiUrl, "POST", inputJson);
            }
            catch (WebException ex)
            {
            }
            return json;
        }

        public Boolean InsertSupplierAPIResponse(string Refno, string ResponseMessage, string Status, string Supplier, string ExceptionMessage, int? OrderId, string SuppValue = "", string DeviceType = "", string IpAddress = "", string LabEntryResponse = "", int LogInID = 0)
        {
            try
            {

                //int LogInID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();



                if (!string.IsNullOrEmpty(Refno))
                    para.Add(db.CreateParam("Refno", System.Data.DbType.String, System.Data.ParameterDirection.Input, Refno.ToString()));
                else
                    para.Add(db.CreateParam("Refno", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(ResponseMessage))
                    para.Add(db.CreateParam("ResponseMessage", System.Data.DbType.String, System.Data.ParameterDirection.Input, ResponseMessage.ToString()));
                else
                    para.Add(db.CreateParam("ResponseMessage", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(Status))
                    para.Add(db.CreateParam("Status", System.Data.DbType.String, System.Data.ParameterDirection.Input, Status.ToString()));
                else
                    para.Add(db.CreateParam("Status", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(Supplier))
                    para.Add(db.CreateParam("Supplier", System.Data.DbType.String, System.Data.ParameterDirection.Input, Supplier.ToString()));
                else
                    para.Add(db.CreateParam("Supplier", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(ExceptionMessage))
                    para.Add(db.CreateParam("ExceptionMessage", System.Data.DbType.String, System.Data.ParameterDirection.Input, ExceptionMessage.ToString()));
                else
                    para.Add(db.CreateParam("ExceptionMessage", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (OrderId != null)
                    para.Add(db.CreateParam("OrderId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, OrderId));
                else
                    para.Add(db.CreateParam("OrderId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(SuppValue))
                    para.Add(db.CreateParam("OrderPrice", System.Data.DbType.Double, System.Data.ParameterDirection.Input, Convert.ToDouble(SuppValue)));
                else
                    para.Add(db.CreateParam("OrderPrice", System.Data.DbType.Double, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(DeviceType))
                    para.Add(db.CreateParam("DeviceType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DeviceType.ToString()));
                else
                    para.Add(db.CreateParam("DeviceType", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(IpAddress))
                    para.Add(db.CreateParam("IpAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, IpAddress.ToString()));
                else
                    para.Add(db.CreateParam("IpAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(LabEntryResponse))
                    para.Add(db.CreateParam("LabEntryResponse", System.Data.DbType.String, System.Data.ParameterDirection.Input, LabEntryResponse.ToString()));
                else
                    para.Add(db.CreateParam("LabEntryResponse", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                para.Add(db.CreateParam("LoginId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, LogInID));

                db.ExecuteSP("SupplierApiResponse_Insert", para.ToArray(), false);
                return true;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return false;
            }
        }

        public string JBHoldRequest(string StoneNo)
        {
            string ResultReturn = "";
            try
            {
                string APIUrl = @"https://websvr.jbbros.com/jbapi.aspx?UserId={0}&APIKey={1}&Action={2}&PacketList={3}";
                string UserId = "sunrisediamonds";
                string APIKey = "90F2D641-7968-4BB4-BA69-E323F732AF01";
                string Action = "B";
                string PacketList = StoneNo;

                APIUrl = string.Format(APIUrl, UserId, APIKey, Action, PacketList);
                using (WebClient tClient = new WebClient())
                {
                    tClient.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                    ResultReturn = tClient.DownloadString(APIUrl);
                }

                return ResultReturn;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [NonAction]
        private DataTable GetSupplierWiseOrderRefno(string Refno, int UserId, int OrderId)
        {
            Database db = new Database();

            List<IDbDataParameter> para;
            para = new List<IDbDataParameter>();
            para.Add(db.CreateParam("sRefno", DbType.String, ParameterDirection.Input, Refno));
            para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, UserId));
            para.Add(db.CreateParam("iOrderId", DbType.Int32, ParameterDirection.Input, OrderId));
            DataTable dt = db.ExecuteSP("OrderRefNoWiseSupplierName_Get", para.ToArray(), false);

            return dt;


        }
        [NonAction]
        private DataTable RefNo_Get_Using_Supplier_Stone_No_in_OrderHis(string Supplier_Stone_No, int OrderId)
        {
            Database db = new Database();

            List<IDbDataParameter> para;
            para = new List<IDbDataParameter>();
            para.Add(db.CreateParam("Supplier_Stone_No", DbType.String, ParameterDirection.Input, Supplier_Stone_No));
            para.Add(db.CreateParam("iOrderId", DbType.Int32, ParameterDirection.Input, OrderId));
            DataTable dt = db.ExecuteSP("RefNo_Get_Using_Supplier_Stone_No_in_OrderHis", para.ToArray(), false);
            return dt;
        }
        [NonAction]
        private DataTable UpdateOrderDetIsConfirmStatus(string Refno, int OrderId)
        {
            Database db = new Database();

            List<IDbDataParameter> para;
            para = new List<IDbDataParameter>();
            para.Add(db.CreateParam("sRefno", DbType.String, ParameterDirection.Input, Refno));

            para.Add(db.CreateParam("iOrderId", DbType.Int32, ParameterDirection.Input, OrderId));
            DataTable dt = db.ExecuteSP("UpdateOrderDetConfirmStatus", para.ToArray(), false);

            return dt;


        }

        [NonAction]
        private bool SendPurchaseMail(string type, string when, string OrderId, String Comments, String CustUserid, String LogInID, string Refno, string Price, string SupplierStatus = "", string SunriseStatus = "")
        {
            try
            {
                //Done by [RJ] dated on 26-Apr-2016 as per doc - 315 for send order notificartion by telegram
                //Start......
                try
                {
                    /* Temporary commented for Local development and testing -- Divya here*/
                    //FortuneService.ServiceSoapClient wbService = new FortuneService.ServiceSoapClient();
                    //FortuneService.CommonResultResponse cResult = wbService.OrderDetailNotification(OrderId.ToString(), userid);
                    /* Temporary commented for Local development and testing -- Ends Here*/
                }
                catch (Exception ext)
                {
                    throw ext;
                }
                //Over.......

                string lsToMail = "";

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Clear();
                para.Add(db.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(CustUserid)));
                DataTable dtCustUserDetail = db.ExecuteSP("UserMas_SelectOne", para.ToArray(), false);   //For Assist Det

                Database db1 = new Database(Request);
                para.Clear();
                para.Add(db1.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(LogInID)));
                DataTable dtToMailList = db1.ExecuteSP("UserMas_SelectOne", para.ToArray(), false);    //Login User Email

                //foreach (DataRow row in dtToMailList.Rows)
                //    //if (Convert.ToInt16(row["iUserType"]) == 1)
                //    lsToMail += row["sEmail"].ToString() + ",";

                //if (lsToMail.Length > 0)
                //    lsToMail = lsToMail.Remove(lsToMail.Length - 1);






                String sNotes = Comments;

                //Send Oder email to Customer
                // Change By Hitesh Bcoz when employee place order than order mail not receive to employee
                //if (dtUserDetail.Rows[0]["sCompEmail"].ToString().Length > 0)
                if (dtCustUserDetail != null && dtCustUserDetail.Rows.Count > 0)
                {
                    //Add by MoniL : 05-02-2018 : Doc 977
                    string custMail = "";

                    if (dtToMailList.Rows[0]["sCompEmail"].ToString() == "" || dtToMailList.Rows[0]["sCompEmail"].ToString() == null)
                    {
                        custMail = dtToMailList.Rows[0]["sEmail"].ToString();
                        if (dtToMailList.Rows[0]["sEmailPersonal"].ToString() != "" && dtToMailList.Rows[0]["sEmailPersonal"].ToString() != null)
                            custMail += "," + dtToMailList.Rows[0]["sEmailPersonal"];
                    }
                    else
                    {
                        custMail = dtToMailList.Rows[0]["sCompEmail"].ToString();
                        if (dtToMailList.Rows[0]["sCompEmail2"].ToString() != "" && dtToMailList.Rows[0]["sCompEmail2"].ToString() != null)
                            custMail += "," + dtToMailList.Rows[0]["sCompEmail2"];
                    }

                    custMail = custMail.Trim(',');
                    string assistbyemail = null;
                    if (dtCustUserDetail.Rows[0]["Email_AssistBy1"] != null && dtCustUserDetail.Rows[0]["Email_AssistBy1"].ToString() != "")
                    {
                        assistbyemail = assistbyemail + dtCustUserDetail.Rows[0]["Email_AssistBy1"].ToString() + ",";
                        // loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sWeChatId1"].ToString() + "</td></tr>");
                    }
                    if (dtCustUserDetail.Rows[0]["Email_AssistBy2"] != null && dtCustUserDetail.Rows[0]["Email_AssistBy2"].ToString() != "")
                    {
                        assistbyemail = assistbyemail + dtCustUserDetail.Rows[0]["Email_AssistBy2"].ToString() + ",";
                        // loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sWeChatId1"].ToString() + "</td></tr>");
                    }

                    if (!string.IsNullOrEmpty(assistbyemail))
                    {
                        assistbyemail = assistbyemail.Remove(assistbyemail.Length - 1);

                        //assistbyemail += ",samit@sunrisediam.com,jignesh@sunrisediam.com";//,tejash@brainwaves.co.in
                    }
                    //assistbyemail += ",samit@sunrisediam.com,jignesh@sunrisediam.com";//,tejash@brainwaves.co.in

                    string CompanyName = dtCustUserDetail.Rows[0]["sCompName"].ToString();
                    string UserName = dtCustUserDetail.Rows[0]["sUsername"].ToString();



                    IPadCommon.EmailPurchaseOrder(type, when, custMail, Lib.Models.Common.GetHKTime(), assistbyemail, OrderId, Refno, Price, Comments, CompanyName, UserName, SupplierStatus, SunriseStatus);


                }

                ////Send Oder email to Admin and Employee
                //IPadCommon.EmailNewOrderToAdmin(Convert.ToInt32(userid), lsToMail, Convert.ToInt32(iOrderID), Lib.Models.Common.GetHKTime(), 1, dtUserDetail.Rows[0]["sFirstName"] + " " + dtUserDetail.Rows[0]["sLastName"], dtUserDetail.Rows[0]["sCompName"].ToString(),
                //                dtUserDetail.Rows[0]["sCompAddress"] + (String.IsNullOrEmpty(dtUserDetail.Rows[0]["sCompAddress2"].ToString()) ? "" : ", " + dtUserDetail.Rows[0]["sCompAddress2"]) + ", " + dtUserDetail.Rows[0]["sCity"],
                //                dtUserDetail.Rows[0]["sPhone"].ToString(), dtUserDetail.Rows[0]["sMobile"].ToString(), dtUserDetail.Rows[0]["sCompEmail"].ToString(), sNotes);


                return true;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        public string InsertLabAutoEntry(string UserId, string Refno, string PlaceOrderRemark, string LabEntryStatus)
        {
            try
            {
                Database db2 = new Database();
                List<IDbDataParameter> para2 = new List<IDbDataParameter>();
                para2.Add(db2.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(UserId)));
                DataTable dt = db2.ExecuteSP("UserMas_SelectOne", para2.ToArray(), false);

                string Party = string.Empty;
                //Party = dt.Rows[0]["PartyCompanyName"].ToString(); //ask by TJ at 03-11-2021 
                if (dt.Rows[0]["isemp"].ToString() == "1")
                {
                    Party = PlaceOrderRemark;
                }
                else
                {
                    Party = dt.Rows[0]["PartyCompanyName"].ToString();
                }

                //  int AssistBy = Convert.ToInt32(dt.Rows[0]["iEmpId"].ToString());
                int AssistBy = Convert.ToInt32((dt.Rows[0]["iEmpId"].ToString() == null || dt.Rows[0]["iEmpId"].ToString() == "") ? "10" : dt.Rows[0]["iEmpId"].ToString());
                string FortunePartyCode = dt.Rows[0]["FortunePartyCode"].ToString();

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();


                OracleParameter param1 = new OracleParameter("P_for_party", OracleDbType.Varchar2);
                param1.Value = Party;
                paramList.Add(param1);

                OracleParameter param2 = new OracleParameter("p_for_assit", OracleDbType.Int32);
                param2.Value = AssistBy;
                paramList.Add(param2);

                OracleParameter param3 = new OracleParameter("p_for_refno", OracleDbType.Varchar2);
                param3.Value = Refno;
                paramList.Add(param3);

                OracleParameter param4 = new OracleParameter("p_for_partycode", OracleDbType.Varchar2);
                param4.Value = FortunePartyCode;
                paramList.Add(param4);

                OracleParameter param5 = new OracleParameter("p_status", OracleDbType.Varchar2);
                param5.Value = LabEntryStatus;
                paramList.Add(param5);

                OracleParameter param6 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param6.Direction = ParameterDirection.Output;
                paramList.Add(param6);



                System.Data.DataTable dt1 = oracleDbAccess.CallSP("get_lab_auto_entry", paramList);

                string result = "";
                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        result = dt1.Rows[0][0].ToString();
                    }
                    else
                    {
                        result = "Fail";
                    }
                }
                else
                {
                    result = "Fail";
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPost]
        public IHttpActionResult GetSupplierOrderLogData([FromBody] JObject data)
        {
            OrderHistoryRequest orderHistoryRequest = new OrderHistoryRequest();
            try
            {
                orderHistoryRequest = JsonConvert.DeserializeObject<OrderHistoryRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<OrderHistoryResponse>
                {
                    Data = new List<OrderHistoryResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable dtData = GetSupplierOrderLogDataInner(orderHistoryRequest);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataRow[] dra = dtData.Select("iSr IS NULL");
                    OrderSummary searchSummary = new OrderSummary();
                    if (dra.Length > 0)
                    {
                        searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                        searchSummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                        searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["sRefNo"]);
                        searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["dCts"]);
                        searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble((dra[0]["dRapAmount"].ToString() != "" && dra[0]["dRapAmount"].ToString() != null ? dra[0]["dRapAmount"] : "0"));
                        searchSummary.TOT_NET_AMOUNT = Convert.ToDouble((dra[0]["Net_Value"].ToString() != "" && dra[0]["Net_Value"].ToString() != null ? dra[0]["Net_Value"] : "0"));  //Convert.ToDouble(dra[0]["dNetPrice"]);
                        searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((dra[0]["Final_Disc"].ToString() != "" && dra[0]["Final_Disc"].ToString() != null ? dra[0]["Final_Disc"] : "0")); //Convert.ToDouble(dra[0]["sSupplDisc"]);
                    }

                    dtData.DefaultView.RowFilter = "iSr IS NOT NULL";
                    dtData = dtData.DefaultView.ToTable();
                    if (dtData != null && dtData.Rows.Count > 0)
                    {

                        List<OrderList> listOrder = new List<OrderList>();
                        listOrder = DataTableExtension.ToList<OrderList>(dtData);
                        List<OrderHistoryResponse> orderHistoryResponse = new List<OrderHistoryResponse>();

                        //orderHistoryResponse = DataTableExtension.ToList<OrderHistoryResponse>(dtData);
                        if (listOrder.Count > 0)
                        { //List<string> lst = dtData.AsDataView().ToTable(true, "CompanyName").ToList();
                          //var a = (from r in dtData.AsEnumerable()
                          //  select r["CompanyName"]).Distinct().ToList();
                            orderHistoryResponse.Add(new OrderHistoryResponse()
                            {
                                DataList = listOrder,
                                DataSummary = searchSummary

                            });

                            return Ok(new ServiceResponse<OrderHistoryResponse>
                            {
                                Data = orderHistoryResponse,
                                Message = "SUCCESS",
                                Status = "1"
                            });
                        }
                        else
                        {
                            return Ok(new ServiceResponse<OrderHistoryResponse>
                            {
                                Data = orderHistoryResponse,
                                Message = "Something Went wrong.",
                                Status = "0"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new ServiceResponse<OrderHistoryResponse>
                        {
                            Data = null,
                            Message = "No data found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<OrderHistoryResponse>
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
                return Ok(new ServiceResponse<OrderHistoryResponse>
                {
                    Data = new List<OrderHistoryResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [NonAction]
        private DataTable GetSupplierOrderLogDataInner(OrderHistoryRequest orderHistoryRequest)
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(orderHistoryRequest.iUserid_FullOrderDate))
                    para.Add(db.CreateParam("iOrderid_sRefNo", DbType.String, ParameterDirection.Input, orderHistoryRequest.iUserid_FullOrderDate));
                else
                    para.Add(db.CreateParam("iOrderid_sRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (userID > 0)
                    para.Add(db.CreateParam("iUserId", DbType.String, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("iUserId", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, orderHistoryRequest.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, orderHistoryRequest.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.PageNo))
                    para.Add(db.CreateParam("PageNo", DbType.Int16, ParameterDirection.Input, Convert.ToInt16(orderHistoryRequest.PageNo)));
                else
                    para.Add(db.CreateParam("PageNo", DbType.Int16, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.RefNo))
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, (orderHistoryRequest.RefNo)));
                else
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(orderHistoryRequest.CommonName))
                    para.Add(db.CreateParam("CommonName", DbType.String, ParameterDirection.Input, (orderHistoryRequest.CommonName)));
                else
                    para.Add(db.CreateParam("CommonName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(orderHistoryRequest.CompanyName))
                    para.Add(db.CreateParam("CompanyName", DbType.String, ParameterDirection.Input, (orderHistoryRequest.CompanyName)));
                else
                    para.Add(db.CreateParam("CompanyName", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.CustomerName))
                    para.Add(db.CreateParam("CustomerName", DbType.String, ParameterDirection.Input, (orderHistoryRequest.CustomerName)));
                else
                    para.Add(db.CreateParam("CustomerName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(orderHistoryRequest.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, (orderHistoryRequest.OrderBy)));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("PgSize", DbType.String, ParameterDirection.Input, (orderHistoryRequest.PgSize)));

                para.Add(db.CreateParam("DateStatus", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.DateStatus));
                para.Add(db.CreateParam("SubUser", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.SubUser));
                //para.Add(db.CreateParam("ConfirmOrder", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.ConfirmOrder));
                //para.Add(db.CreateParam("NotConfirmOrder", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.NotConfirmOrder));
                DataTable dsData = db.ExecuteSP("IPD_Get_SupplierOrderLogData_Sunrise", para.ToArray(), false);

                return dsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IHttpActionResult DownloadSupplierOrderLog([FromBody] JObject data)
        {
            OrderHistoryRequest orderHistoryRequest = new OrderHistoryRequest();
            try
            {
                orderHistoryRequest = JsonConvert.DeserializeObject<OrderHistoryRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<OrderHistoryResponse>
                {
                    Data = new List<OrderHistoryResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataSet ds = new DataSet();
                DataTable dtData = GetSupplierOrderLogDataInner(orderHistoryRequest);
                DataTable dtSumm = new DataTable();

                dtSumm.Columns.Add("TOT_PAGE", typeof(Int32));
                dtSumm.Columns.Add("PAGE_SIZE", typeof(Int32));
                dtSumm.Columns.Add("TOT_PCS", typeof(Int32));
                dtSumm.Columns.Add("TOT_CTS", typeof(Decimal));
                dtSumm.Columns.Add("TOT_RAP_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("TOT_NET_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("AVG_PRICE_PER_CTS", typeof(Decimal));
                dtSumm.Columns.Add("AVG_SALES_DISC_PER", typeof(Decimal));

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataRow[] dra = dtData.Select("iSr IS NULL");

                    if (dra.Length > 0)
                    {
                        DataRow dr = dtSumm.NewRow();
                        dr["TOT_PAGE"] = dra[0]["TOTAL_PAGE"];
                        dr["PAGE_SIZE"] = dra[0]["PAGE_SIZE"];
                        dr["TOT_PCS"] = dra[0]["sRefNo"];
                        dr["TOT_CTS"] = dra[0]["dCts"];
                        dr["TOT_RAP_AMOUNT"] = (dra[0]["dRapAmount"].ToString() != "" && dra[0]["dRapAmount"].ToString() != null ? dra[0]["dRapAmount"] : "0");
                        dr["TOT_NET_AMOUNT"] = dra[0]["dNetPrice"];
                        // dr["AVG_PRICE_PER_CTS"] = dra[0]["dNetPrice"];
                        dr["AVG_SALES_DISC_PER"] = (dra[0]["sSupplDisc"].ToString() != "" && dra[0]["sSupplDisc"].ToString() != null ? dra[0]["sSupplDisc"] : "0");
                        dtSumm.Rows.Add(dr);
                    }


                    dtData.DefaultView.RowFilter = "iSr IS NOT NULL";
                    dtData = dtData.DefaultView.ToTable();
                    dtSumm.TableName = "SummaryTable";
                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        string filename = "SupplierOrderLog_" + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                        string _path = ConfigurationManager.AppSettings["data"];
                        string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                        string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                        //EpExcelExport.Excel_Generate(dtData.DefaultView.ToTable(), realpath + "Data" + random + ".xlsx");
                        EpExcelExport.CreateSupplierOrderLogExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath,
                                                        Convert.ToDateTime(orderHistoryRequest.FromDate), Convert.ToDateTime(orderHistoryRequest.ToDate), orderHistoryRequest.isAdmin, orderHistoryRequest.isEmp);

                        string _strxml = _path + filename + ".xlsx";
                        return Ok(_strxml);
                    }
                    return Ok("No data found.");
                }

                return Ok("No data found.");
                //ds.Tables.Add(dtData);
                //ds.Tables.Add(dtSumm);

                //return ds;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Something Went wrong.\nPlease try again later");
            }
        }
        [NonAction]
        private DataTable PurchaseOrderLog_Insert(string sRefNo, bool IsStart, bool IsEnd, int Id, int LogInID)
        {
            try
            {
                //int LogInID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("sRefNo", DbType.String, System.Data.ParameterDirection.Input, sRefNo.ToString()));
                para.Add(db.CreateParam("IsStart", DbType.Boolean, System.Data.ParameterDirection.Input, IsStart));
                para.Add(db.CreateParam("IsEnd", DbType.Boolean, System.Data.ParameterDirection.Input, IsEnd));
                para.Add(db.CreateParam("LoginBy", DbType.Int32, System.Data.ParameterDirection.Input, LogInID));

                if (Id > 0)
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("PurchaseOrderLog_Insert", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [NonAction]
        private DataTable PurchaseOrder_RefCheck_ExistofNot(string sRefNo, string OrderId)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Refno_OrderId", DbType.String, System.Data.ParameterDirection.Input, sRefNo +"_"+ OrderId));

                DataTable dt = db.ExecuteSP("PurchaseOrder_RefCheck_ExistofNot", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [NonAction]
        private DataTable LabEntryLog_Insert(string sRefNo, bool IsStart, bool IsEnd, int Id, int LogInID)
        {
            try
            {
                //int LogInID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("sRefNo", DbType.String, System.Data.ParameterDirection.Input, sRefNo.ToString()));
                para.Add(db.CreateParam("IsStart", DbType.Boolean, System.Data.ParameterDirection.Input, IsStart));
                para.Add(db.CreateParam("IsEnd", DbType.Boolean, System.Data.ParameterDirection.Input, IsEnd));
                para.Add(db.CreateParam("LoginBy", DbType.Int32, System.Data.ParameterDirection.Input, LogInID));

                if (Id > 0)
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("LabEntryLog_Insert", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [HttpPost]
        public IHttpActionResult PurchaseOrder_Delete([FromBody]JObject data)
        {
            PurchaseOrder_Delete_Request req = new PurchaseOrder_Delete_Request();
            try
            {
                req = JsonConvert.DeserializeObject<PurchaseOrder_Delete_Request>(data.ToString());
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
                para.Add(db.CreateParam("iOrderId_sRefNo", DbType.String, ParameterDirection.Input, req.iOrderId_sRefNo));

                DataTable dtData = db.ExecuteSP("PurchaseOrder_Delete", para.ToArray(), false);

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

        #region Confirm Order API for Mobile Application

        [HttpPost]
        public IHttpActionResult PlaceConfirmOrderUsingApi_1([FromBody] JObject data)
        {
            SupplierApiOrderRequest confirmOrderRequest = new SupplierApiOrderRequest();
            int LogInID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
            try
            {
                confirmOrderRequest = JsonConvert.DeserializeObject<SupplierApiOrderRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            #region input param insert in sql table

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Refno", typeof(string));
                dt.Columns.Add("Orderid", typeof(string));
                dt.Columns.Add("UserId", typeof(string));
                dt.Columns.Add("SuppValue", typeof(string));
                dt.Columns.Add("Comments", typeof(string));
                dt.Columns.Add("DeviceType", typeof(string));
                dt.Columns.Add("IpAddress", typeof(string));
                dt.Columns.Add("Entry_UserId", typeof(string));

                if (confirmOrderRequest.Orders.Count() > 0)
                {
                    for (int i = 0; i < confirmOrderRequest.Orders.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["Refno"] = confirmOrderRequest.Orders[i].Refno.ToString();
                        dr["Orderid"] = confirmOrderRequest.Orders[i].Orderid.ToString();
                        dr["UserId"] = confirmOrderRequest.Orders[i].UserId.ToString();
                        dr["SuppValue"] = confirmOrderRequest.Orders[i].SuppValue.ToString();
                        dr["Comments"] = confirmOrderRequest.Orders[i].Comments;
                        dr["DeviceType"] = confirmOrderRequest.DeviceType;
                        dr["IpAddress"] = confirmOrderRequest.IpAddress;
                        dr["Entry_UserId"] = LogInID;

                        dt.Rows.Add(dr);
                    }

                    Database db = new Database(Request);
                    DataTable dtData = new DataTable();
                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dtData = db.ExecuteSP("ConfirmOrder_ApiRequest_Insert", para.ToArray(), false);

                    if (dtData != null && dtData.Rows.Count > 0 && dtData.Rows[0]["Status"].ToString() == "1")
                    {
                        
                        #region main code of purchase order
                        
                        try
                        {
                            List<ObjOrderLst> list_1 = new List<ObjOrderLst>();
                            ObjOrderLst sub = new ObjOrderLst();
                            bool status;

                            List<ConfirmPlaceOrderResponse> res_list = new List<ConfirmPlaceOrderResponse>();
                            string MsgRef = string.Empty, LabResponse = string.Empty, SunriseStatus = string.Empty, SupplierStatus = string.Empty, LabEntryStatus = string.Empty;

                            foreach (var obj in confirmOrderRequest.Orders)
                            {
                                status = true;

                                foreach (var obj1 in list_1)
                                {
                                    if (obj.Refno == obj1.Refno && obj.Orderid == obj1.Orderid)
                                    {
                                        status = false;
                                    }
                                }

                                if (status == true)
                                {
                                    sub = new ObjOrderLst();
                                    sub.Refno = obj.Refno;
                                    sub.Orderid = obj.Orderid;
                                    sub.UserId = obj.UserId;
                                    sub.SuppValue = obj.SuppValue;
                                    sub.Comments = obj.Comments;
                                    list_1.Add(sub);

                                    DataTable DT_RefCheck = PurchaseOrder_RefCheck_ExistofNot(obj.Refno, obj.Orderid.ToString());
                                    if (DT_RefCheck != null && DT_RefCheck.Rows.Count > 0)
                                    {
                                        if (DT_RefCheck.Rows[0]["Status"].ToString() == "1")
                                        {
                                            DataTable dtSupplierRef = GetSupplierWiseOrderRefno(obj.Refno, LogInID, obj.Orderid);
                                            if (dtSupplierRef != null && dtSupplierRef.Rows.Count > 0)
                                            {
                                                for (int i = 0; i <= dtSupplierRef.Rows.Count - 1; i++)
                                                {
                                                    if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "SHAIRU GEMS DIAMONDS PVT. LTD - (S)".ToString().ToUpper())
                                                    {
                                                        if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
                                                        {
                                                            string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
                                                            string _LoginResponse = GetShairuAPITaken();
                                                            ShairuApiLoginResponse _data = new ShairuApiLoginResponse();
                                                            _data = (new JavaScriptSerializer()).Deserialize<ShairuApiLoginResponse>(_LoginResponse);
                                                            if (_data.UserId > 0)
                                                            {
                                                                foreach (string StoneId in RefnoList)
                                                                {
                                                                    SunriseStatus = string.Empty; SupplierStatus = string.Empty; LabEntryStatus = string.Empty;

                                                                    ShairuApiHoldResponse _Holddata = new ShairuApiHoldResponse();
                                                                    if (StoneId.ToString() != "")
                                                                    {
                                                                        DataTable DT_PurLg1 = PurchaseOrderLog_Insert(StoneId, true, false, 0, LogInID);
                                                                        int PurLg_Id = 0;
                                                                        if (DT_PurLg1 != null && DT_PurLg1.Rows.Count > 0)
                                                                        {
                                                                            PurLg_Id = Convert.ToInt32(DT_PurLg1.Rows[0]["Id"].ToString());
                                                                        }

                                                                        string _HoldResponse = ShairuHoldStoneRequest(_data.TokenId.ToString(), StoneId, _data.UserId, obj.Comments);
                                                                        _Holddata = (new JavaScriptSerializer()).Deserialize<ShairuApiHoldResponse>(_HoldResponse);

                                                                        SupplierStatus = _Holddata.Message;

                                                                        if (SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY"))
                                                                        {
                                                                            SunriseStatus = "Confirm";
                                                                            LabEntryStatus = "Confirm";
                                                                        }
                                                                        else if (SupplierStatus.ToUpper().Contains("ORDER PLACED SUCCESSFULLY."))
                                                                        {
                                                                            SunriseStatus = "Confirm";
                                                                            LabEntryStatus = "Confirm";
                                                                        }
                                                                        else if (SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY. THIS REF NO ARE NOT PROCESSED"))
                                                                        {
                                                                            SunriseStatus = "Busy";
                                                                            LabEntryStatus = "Busy";
                                                                        }
                                                                        else if (SupplierStatus.ToUpper().Contains("THIS STONE(S) ARE SUBJECT TO AVAILABILITY"))
                                                                        {
                                                                            SunriseStatus = "Busy";
                                                                            LabEntryStatus = "Waiting";
                                                                        }
                                                                        else if (SupplierStatus.ToUpper().Contains("NOT PROCESSED"))
                                                                        {
                                                                            SunriseStatus = "Error";
                                                                            LabEntryStatus = "Waiting";
                                                                        }
                                                                        else if (SupplierStatus.ToUpper().Contains("FAILED TO CONFIRM STONE"))
                                                                        {
                                                                            SunriseStatus = "Error";
                                                                            LabEntryStatus = "Waiting";
                                                                        }
                                                                        else if (SupplierStatus.ToUpper().Contains("NO RECORD WILL BE PROCEED"))
                                                                        {
                                                                            SunriseStatus = "Error";
                                                                            LabEntryStatus = "Waiting";
                                                                        }

                                                                        if (_Holddata.Status != null &&
                                                                                (
                                                                                    (_Holddata.Status.ToUpper() == "SUCCESS" && SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY")) ||
                                                                                    (_Holddata.Status.ToUpper() == "1" && SupplierStatus.ToUpper().Contains("ORDER PLACED SUCCESSFULLY."))
                                                                                )
                                                                            )
                                                                        {
                                                                            #region SHAIRU API RETURN SUCCESS

                                                                            try
                                                                            {
                                                                                DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                                                int LbLg_Id = 0;
                                                                                if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                                                {
                                                                                    LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                                                }

                                                                                LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                                                DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                                            }
                                                                            catch (Exception ex)
                                                                            {
                                                                                DAL.Common.InsertErrorLog(ex, null, Request);
                                                                                LabResponse = ex.Message.ToString();
                                                                            }

                                                                            DataTable dtUpdate = UpdateOrderDetIsConfirmStatus(obj.Refno, obj.Orderid);

                                                                            if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                                                                            {
                                                                                if (dtUpdate.Rows[0]["Msg"].ToString() == "Success")
                                                                                {
                                                                                    InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType, confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                                                }
                                                                                else
                                                                                {
                                                                                    InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                                            }

                                                                            try
                                                                            {
                                                                                SendPurchaseMail("MANUAL", "SUCCESS", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), LogInID.ToString(), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                                            }
                                                                            catch (Exception ex)
                                                                            {
                                                                                DAL.Common.InsertErrorLog(ex, null, Request);
                                                                            }

                                                                            MsgRef += obj.Refno + ",";

                                                                            #endregion
                                                                        }
                                                                        else
                                                                        {
                                                                            #region SHAIRU API RETURN FAIL

                                                                            if (_Holddata.Status == null)
                                                                            {
                                                                                SupplierStatus = "";
                                                                                SunriseStatus = "Not Available";
                                                                                LabEntryStatus = "Reject";
                                                                            }

                                                                            try
                                                                            {
                                                                                DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                                                int LbLg_Id = 0;
                                                                                if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                                                {
                                                                                    LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                                                }

                                                                                LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                                                DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                                            }
                                                                            catch (Exception ex)
                                                                            {
                                                                                DAL.Common.InsertErrorLog(ex, null, Request);
                                                                                LabResponse = ex.Message.ToString();
                                                                            }

                                                                            InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);

                                                                            try
                                                                            {
                                                                                SendPurchaseMail("MANUAL", "ERROR", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), LogInID.ToString(), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                                            }
                                                                            catch (Exception ex)
                                                                            {
                                                                                DAL.Common.InsertErrorLog(ex, null, Request);
                                                                            }

                                                                            #endregion
                                                                        }


                                                                        DataTable DT_PurLg2 = PurchaseOrderLog_Insert(StoneId, false, true, PurLg_Id, LogInID);

                                                                        res_list.Add(new ConfirmPlaceOrderResponse()
                                                                        {
                                                                            RefNo = StoneId,
                                                                            SunriseStatus = (SunriseStatus == "" ? "Fail" : SunriseStatus),
                                                                            SupplierName = dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper(),
                                                                            SupplierStatus = SupplierStatus,
                                                                            LabEntryStatus = LabEntryStatus
                                                                        });
                                                                    }
                                                                }
                                                            }

                                                        }
                                                    }
                                                    else if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "J.B. AND BROTHERS PVT. LTD - (S)".ToString().ToUpper())
                                                    {
                                                        if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
                                                        {
                                                            string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
                                                            foreach (string StoneId in RefnoList)
                                                            {
                                                                SunriseStatus = string.Empty;
                                                                if (StoneId.ToString() != "")
                                                                {
                                                                    DataTable DT_PurLg1 = PurchaseOrderLog_Insert(StoneId, true, false, 0, LogInID);
                                                                    int PurLg_Id = 0;
                                                                    if (DT_PurLg1 != null && DT_PurLg1.Rows.Count > 0)
                                                                    {
                                                                        PurLg_Id = Convert.ToInt32(DT_PurLg1.Rows[0]["Id"].ToString());
                                                                    }

                                                                    string _HoldResponse = JBHoldRequest(StoneId + "|" + obj.SuppValue.ToString());
                                                                    List<JBApiHoldResponse> _Holddata = new List<JBApiHoldResponse>();

                                                                    _Holddata = (new JavaScriptSerializer()).Deserialize<List<JBApiHoldResponse>>(_HoldResponse);

                                                                    SupplierStatus = _Holddata[0].Status;

                                                                    if (SupplierStatus.ToUpper().Contains("BUYING SUCCESSFUL."))
                                                                    {
                                                                        SunriseStatus = "Confirm";
                                                                        LabEntryStatus = "Confirm";
                                                                    }
                                                                    else if (SupplierStatus.ToUpper().Contains("ERROR, PLEASE TRY AGAIN!"))
                                                                    {
                                                                        SunriseStatus = "Pending";
                                                                        LabEntryStatus = "Waiting";
                                                                    }
                                                                    else if (SupplierStatus.ToUpper().Contains("PACKET UNAVAILABLE!"))
                                                                    {
                                                                        SunriseStatus = "Not Available";
                                                                        LabEntryStatus = "Busy";
                                                                    }
                                                                    else if (SupplierStatus.ToUpper().Contains("INVALID REF NO!"))
                                                                    {
                                                                        SunriseStatus = "Error";
                                                                        LabEntryStatus = "Waiting";
                                                                    }
                                                                    else if (SupplierStatus.ToUpper().Contains("PACKET(S) ARE ON MEMO."))
                                                                    {
                                                                        SunriseStatus = "Busy";
                                                                        LabEntryStatus = "Busy";
                                                                    }

                                                                    if (SupplierStatus != null && SupplierStatus.ToUpper() == "BUYING SUCCESSFUL.")
                                                                    {
                                                                        #region JB API RETURN SUCCESS

                                                                        try
                                                                        {
                                                                            DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                                            int LbLg_Id = 0;
                                                                            if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                                            {
                                                                                LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                                            }

                                                                            LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                                            DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                                            LabResponse = ex.Message.ToString();
                                                                        }

                                                                        DataTable dtUpdate = UpdateOrderDetIsConfirmStatus(obj.Refno, obj.Orderid);

                                                                        if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                                                                        {
                                                                            if (dtUpdate.Rows[0]["Msg"].ToString() == "Success")
                                                                            {
                                                                                InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue.ToString(), confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                                            }
                                                                            else
                                                                            {
                                                                                InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                                        }

                                                                        try
                                                                        {
                                                                            SendPurchaseMail("MANUAL", "SUCCESS", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), LogInID.ToString(), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                                        }

                                                                        MsgRef += obj.Refno + ",";

                                                                        #endregion
                                                                    }
                                                                    else
                                                                    {
                                                                        #region JB API RETURN FAIL

                                                                        if (SupplierStatus == null)
                                                                        {
                                                                            SupplierStatus = "";
                                                                            SunriseStatus = "Not Available";
                                                                            LabEntryStatus = "Reject";
                                                                        }

                                                                        try
                                                                        {
                                                                            DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                                            int LbLg_Id = 0;
                                                                            if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                                            {
                                                                                LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                                            }

                                                                            LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                                            DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                                            LabResponse = ex.Message.ToString();
                                                                        }

                                                                        InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "FAIL", "J.B. AND BROTHERS PVT. LTD - (S)", "", obj.Orderid, _Holddata[0].Price, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);

                                                                        try
                                                                        {
                                                                            SendPurchaseMail("MANUAL", "ERROR", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), LogInID.ToString(), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                                        }

                                                                        #endregion
                                                                    }

                                                                    DataTable DT_PurLg2 = PurchaseOrderLog_Insert(StoneId, false, true, PurLg_Id, LogInID);

                                                                    string sRefNo = string.Empty;
                                                                    DataTable RefNo_list = RefNo_Get_Using_Supplier_Stone_No_in_OrderHis(StoneId, obj.Orderid);
                                                                    if (RefNo_list != null && RefNo_list.Rows.Count > 0)
                                                                    {
                                                                        sRefNo = RefNo_list.Rows[0]["sRefNo"].ToString();
                                                                    }
                                                                    sRefNo = sRefNo == "" ? StoneId : sRefNo;
                                                                    res_list.Add(new ConfirmPlaceOrderResponse()
                                                                    {
                                                                        RefNo = sRefNo,
                                                                        SunriseStatus = (SunriseStatus == "" ? "Fail" : SunriseStatus),
                                                                        SupplierName = dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper(),
                                                                        SupplierStatus = SupplierStatus,
                                                                        LabEntryStatus = LabEntryStatus
                                                                    });
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                                                {
                                                    Data = res_list,
                                                    Message = "No Record will be Proceed",
                                                    Status = "0"
                                                });
                                            }
                                        }
                                    }
                                }
                            }

                            if (res_list.Count() > 0)
                            {
                                return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                                {
                                    Data = res_list,
                                    Message = "SUCCESS",
                                    Status = "1"
                                });
                            }
                            else
                            {
                                return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                                {
                                    Data = res_list,
                                    Message = "No Record will be Proceed",
                                    Status = "0"
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            DAL.Common.InsertErrorLog(ex, null, Request);
                            return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                            {
                                Data = new List<ConfirmPlaceOrderResponse>(),
                                Message = "Something Went wrong.\nPlease try again later",
                                Status = "0"
                            });
                        }
                        
                        #endregion

                    }
                    else
                    {
                        return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                        {
                            Data = null,
                            Message = dtData.Rows[0]["Message"].ToString(),
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                    {
                        Data = null,
                        Message = "No Record will be Proceed",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request); 
                return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                {
                    Data = new List<ConfirmPlaceOrderResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }

            #endregion
        }

        #endregion

        #region Confirm Order API for Web

        [HttpPost]
        public IHttpActionResult PlaceConfirmOrderUsingApi_Web_1([FromBody]JObject data)
        {
            SupplierApiOrderRequest confirmOrderRequest = new SupplierApiOrderRequest();
            int LogInID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
            try
            {
                confirmOrderRequest = JsonConvert.DeserializeObject<SupplierApiOrderRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            #region input param insert in sql table
            
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Refno", typeof(string));
                dt.Columns.Add("Orderid", typeof(string));
                dt.Columns.Add("UserId", typeof(string));
                dt.Columns.Add("SuppValue", typeof(string));
                dt.Columns.Add("Comments", typeof(string));
                dt.Columns.Add("DeviceType", typeof(string));
                dt.Columns.Add("IpAddress", typeof(string));
                dt.Columns.Add("Entry_UserId", typeof(string));
                //dt.Columns.Add("IsAutoFromCust", typeof(string));

                if (confirmOrderRequest.Orders.Count() > 0)
                {
                    for (int i = 0; i < confirmOrderRequest.Orders.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["Refno"] = confirmOrderRequest.Orders[i].Refno.ToString();
                        dr["Orderid"] = confirmOrderRequest.Orders[i].Orderid.ToString();
                        dr["UserId"] = confirmOrderRequest.Orders[i].UserId.ToString();
                        dr["SuppValue"] = confirmOrderRequest.Orders[i].SuppValue.ToString();
                        dr["Comments"] = confirmOrderRequest.Orders[i].Comments;
                        dr["DeviceType"] = confirmOrderRequest.DeviceType;
                        dr["IpAddress"] = confirmOrderRequest.IpAddress;
                        dr["Entry_UserId"] = LogInID;
                        //dr["IsAutoFromCust"] = confirmOrderRequest.IsAutoFromCust;

                        dt.Rows.Add(dr);
                    }

                    Database db = new Database(Request);
                    DataTable dtData = new DataTable();
                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dtData = db.ExecuteSP("ConfirmOrder_ApiRequest_Insert", para.ToArray(), false);

                    if (dtData != null && dtData.Rows.Count > 0 && dtData.Rows[0]["Status"].ToString() == "1")
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = dtData.Rows[0]["Mas_Id"].ToString(),
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
                        Message = "No Record will be Proceed",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = ex.Message,
                    Status = "0"
                });
            }
            
            #endregion
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult PlaceConfirmOrderUsingApi_Web_2([FromBody] JObject data)
        {
            JObject test1 = JObject.Parse(data.ToString());

            SupplierApiOrderRequest_ obj_ = new SupplierApiOrderRequest_();
            SupplierApiOrderRequest confirmOrderRequest = new SupplierApiOrderRequest();

            int LogInID = 0;
            //int LogInID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

            #region input param get for purchase order

            try
            {
                //confirmOrderRequest = JsonConvert.DeserializeObject<SupplierApiOrderRequest>(data.ToString());
                obj_ = JsonConvert.DeserializeObject<SupplierApiOrderRequest_>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (obj_.Mas_Id > 0)
                    para.Add(db.CreateParam("Mas_Id", DbType.Int64, ParameterDirection.Input, obj_.Mas_Id));
                else
                    para.Add(db.CreateParam("Mas_Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ConfirmOrder_ApiRequest_Get", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    LogInID = Convert.ToInt32(dt.Rows[0]["Entry_UserId"].ToString());
                    confirmOrderRequest.DeviceType = dt.Rows[0]["DeviceType"].ToString();
                    confirmOrderRequest.IpAddress = dt.Rows[0]["IpAddress"].ToString();

                    List<ObjOrderLst> sublist = new List<ObjOrderLst>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ObjOrderLst sub = new ObjOrderLst();
                        sub.Refno = dt.Rows[i]["Refno"].ToString();
                        sub.Orderid = Convert.ToInt32(dt.Rows[i]["Orderid"].ToString());
                        sub.UserId = Convert.ToInt32(dt.Rows[i]["UserId"].ToString());
                        sub.SuppValue = dt.Rows[i]["SuppValue"].ToString();
                        sub.Comments = dt.Rows[i]["Comments"].ToString();
                        sublist.Add(sub);
                    }
                    confirmOrderRequest.Orders = sublist;
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            #endregion

            #region main code of purchase order

            try
            {
                List<ObjOrderLst> list_1 = new List<ObjOrderLst>();
                ObjOrderLst sub = new ObjOrderLst();
                bool status;

                List<ConfirmPlaceOrderResponse> res_list = new List<ConfirmPlaceOrderResponse>();
                string MsgRef = string.Empty, LabResponse = string.Empty, SunriseStatus = string.Empty, SupplierStatus = string.Empty, LabEntryStatus = string.Empty;

                foreach (var obj in confirmOrderRequest.Orders)
                {
                    status = true;

                    foreach (var obj1 in list_1)
                    {
                        if (obj.Refno == obj1.Refno && obj.Orderid == obj1.Orderid)
                        {
                            status = false;
                        }
                    }

                    if (status == true)
                    {
                        sub = new ObjOrderLst();
                        sub.Refno = obj.Refno;
                        sub.Orderid = obj.Orderid;
                        sub.UserId = obj.UserId;
                        sub.SuppValue = obj.SuppValue;
                        sub.Comments = obj.Comments;
                        list_1.Add(sub);


                        DataTable dtSupplierRef = GetSupplierWiseOrderRefno(obj.Refno, LogInID, obj.Orderid);
                        if (dtSupplierRef != null && dtSupplierRef.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dtSupplierRef.Rows.Count - 1; i++)
                            {
                                if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "SHAIRU GEMS DIAMONDS PVT. LTD - (S)".ToString().ToUpper())
                                {
                                    if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
                                    {
                                        string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
                                        string _LoginResponse = GetShairuAPITaken();
                                        ShairuApiLoginResponse _data = new ShairuApiLoginResponse();
                                        _data = (new JavaScriptSerializer()).Deserialize<ShairuApiLoginResponse>(_LoginResponse);
                                        if (_data.UserId > 0)
                                        {
                                            foreach (string StoneId in RefnoList)
                                            {
                                                SunriseStatus = string.Empty; SupplierStatus = string.Empty; LabEntryStatus = string.Empty;

                                                ShairuApiHoldResponse _Holddata = new ShairuApiHoldResponse();
                                                if (StoneId.ToString() != "")
                                                {
                                                    DataTable DT_PurLg1 = PurchaseOrderLog_Insert(StoneId, true, false, 0, LogInID);
                                                    int PurLg_Id = 0;
                                                    if (DT_PurLg1 != null && DT_PurLg1.Rows.Count > 0)
                                                    {
                                                        PurLg_Id = Convert.ToInt32(DT_PurLg1.Rows[0]["Id"].ToString());
                                                    }

                                                    string _HoldResponse = ShairuHoldStoneRequest(_data.TokenId.ToString(), StoneId, _data.UserId, obj.Comments);
                                                    _Holddata = (new JavaScriptSerializer()).Deserialize<ShairuApiHoldResponse>(_HoldResponse);

                                                    SupplierStatus = _Holddata.Message;

                                                    if (SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY"))
                                                    {
                                                        SunriseStatus = "Confirm";
                                                        LabEntryStatus = "Confirm";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("ORDER PLACED SUCCESSFULLY."))
                                                    {
                                                        SunriseStatus = "Confirm";
                                                        LabEntryStatus = "Confirm";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY. THIS REF NO ARE NOT PROCESSED"))
                                                    {
                                                        SunriseStatus = "Busy";
                                                        LabEntryStatus = "Busy";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("THIS STONE(S) ARE SUBJECT TO AVAILABILITY"))
                                                    {
                                                        SunriseStatus = "Busy";
                                                        LabEntryStatus = "Waiting";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("NOT PROCESSED"))
                                                    {
                                                        SunriseStatus = "Error";
                                                        LabEntryStatus = "Waiting";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("FAILED TO CONFIRM STONE"))
                                                    {
                                                        SunriseStatus = "Error";
                                                        LabEntryStatus = "Waiting";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("NO RECORD WILL BE PROCEED"))
                                                    {
                                                        SunriseStatus = "Error";
                                                        LabEntryStatus = "Waiting";
                                                    }

                                                    if (_Holddata.Status != null &&
                                                            (
                                                                (_Holddata.Status.ToUpper() == "SUCCESS" && SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY")) ||
                                                                (_Holddata.Status.ToUpper() == "1" && SupplierStatus.ToUpper().Contains("ORDER PLACED SUCCESSFULLY."))
                                                            )
                                                        )
                                                    {
                                                        #region SHAIRU API RETURN SUCCESS

                                                        try
                                                        {
                                                            DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                            int LbLg_Id = 0;
                                                            if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                            {
                                                                LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                            }

                                                            LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                            DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                            LabResponse = ex.Message.ToString();
                                                        }

                                                        DataTable dtUpdate = UpdateOrderDetIsConfirmStatus(obj.Refno, obj.Orderid);

                                                        if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                                                        {
                                                            if (dtUpdate.Rows[0]["Msg"].ToString() == "Success")
                                                            {
                                                                InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType, confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                            }
                                                            else
                                                            {
                                                                InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                        }

                                                        try
                                                        {
                                                            SendPurchaseMail("MANUAL", "SUCCESS", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), LogInID.ToString(), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                        }

                                                        MsgRef += obj.Refno + ",";

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        #region SHAIRU API RETURN FAIL

                                                        if (_Holddata.Status == null)
                                                        {
                                                            SupplierStatus = "";
                                                            SunriseStatus = "Not Available";
                                                            LabEntryStatus = "Reject";
                                                        }

                                                        try
                                                        {
                                                            DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                            int LbLg_Id = 0;
                                                            if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                            {
                                                                LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                            }

                                                            LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                            DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                            LabResponse = ex.Message.ToString();
                                                        }

                                                        InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);

                                                        try
                                                        {
                                                            SendPurchaseMail("MANUAL", "ERROR", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), LogInID.ToString(), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                        }

                                                        #endregion
                                                    }


                                                    DataTable DT_PurLg2 = PurchaseOrderLog_Insert(StoneId, false, true, PurLg_Id, LogInID);

                                                    res_list.Add(new ConfirmPlaceOrderResponse()
                                                    {
                                                        RefNo = StoneId,
                                                        SunriseStatus = (SunriseStatus == "" ? "Fail" : SunriseStatus),
                                                        SupplierName = dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper(),
                                                        SupplierStatus = SupplierStatus,
                                                        LabEntryStatus = LabEntryStatus
                                                    });
                                                }
                                            }
                                        }

                                    }
                                }
                                else if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "J.B. AND BROTHERS PVT. LTD - (S)".ToString().ToUpper())
                                {
                                    if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
                                    {
                                        string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
                                        foreach (string StoneId in RefnoList)
                                        {
                                            SunriseStatus = string.Empty;
                                            if (StoneId.ToString() != "")
                                            {
                                                DataTable DT_PurLg1 = PurchaseOrderLog_Insert(StoneId, true, false, 0, LogInID);
                                                int PurLg_Id = 0;
                                                if (DT_PurLg1 != null && DT_PurLg1.Rows.Count > 0)
                                                {
                                                    PurLg_Id = Convert.ToInt32(DT_PurLg1.Rows[0]["Id"].ToString());
                                                }

                                                string _HoldResponse = JBHoldRequest(StoneId + "|" + obj.SuppValue.ToString());
                                                List<JBApiHoldResponse> _Holddata = new List<JBApiHoldResponse>();

                                                _Holddata = (new JavaScriptSerializer()).Deserialize<List<JBApiHoldResponse>>(_HoldResponse);

                                                SupplierStatus = _Holddata[0].Status;

                                                if (SupplierStatus.ToUpper().Contains("BUYING SUCCESSFUL."))
                                                {
                                                    SunriseStatus = "Confirm";
                                                    LabEntryStatus = "Confirm";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("ERROR, PLEASE TRY AGAIN!"))
                                                {
                                                    SunriseStatus = "Pending";
                                                    LabEntryStatus = "Waiting";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("PACKET UNAVAILABLE!"))
                                                {
                                                    SunriseStatus = "Not Available";
                                                    LabEntryStatus = "Busy";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("INVALID REF NO!"))
                                                {
                                                    SunriseStatus = "Error";
                                                    LabEntryStatus = "Waiting";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("PACKET(S) ARE ON MEMO."))
                                                {
                                                    SunriseStatus = "Busy";
                                                    LabEntryStatus = "Busy";
                                                }

                                                if (SupplierStatus != null && SupplierStatus.ToUpper() == "BUYING SUCCESSFUL.")
                                                {
                                                    #region JB API RETURN SUCCESS

                                                    try
                                                    {
                                                        DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                        int LbLg_Id = 0;
                                                        if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                        {
                                                            LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                        }

                                                        LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                        DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                        LabResponse = ex.Message.ToString();
                                                    }

                                                    DataTable dtUpdate = UpdateOrderDetIsConfirmStatus(obj.Refno, obj.Orderid);

                                                    if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                                                    {
                                                        if (dtUpdate.Rows[0]["Msg"].ToString() == "Success")
                                                        {
                                                            InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue.ToString(), confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                        }
                                                        else
                                                        {
                                                            InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                    }

                                                    try
                                                    {
                                                        SendPurchaseMail("MANUAL", "SUCCESS", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), LogInID.ToString(), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                    }

                                                    MsgRef += obj.Refno + ",";

                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region JB API RETURN FAIL

                                                    if (SupplierStatus == null)
                                                    {
                                                        SupplierStatus = "";
                                                        SunriseStatus = "Not Available";
                                                        LabEntryStatus = "Reject";
                                                    }

                                                    try
                                                    {
                                                        DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                        int LbLg_Id = 0;
                                                        if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                        {
                                                            LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                        }

                                                        LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                        DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                        LabResponse = ex.Message.ToString();
                                                    }

                                                    InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "FAIL", "J.B. AND BROTHERS PVT. LTD - (S)", "", obj.Orderid, _Holddata[0].Price, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);

                                                    try
                                                    {
                                                        SendPurchaseMail("MANUAL", "ERROR", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), LogInID.ToString(), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                    }

                                                    #endregion
                                                }

                                                DataTable DT_PurLg2 = PurchaseOrderLog_Insert(StoneId, false, true, PurLg_Id, LogInID);

                                                string sRefNo = string.Empty;
                                                DataTable RefNo_list = RefNo_Get_Using_Supplier_Stone_No_in_OrderHis(StoneId, obj.Orderid);
                                                if (RefNo_list != null && RefNo_list.Rows.Count > 0)
                                                {
                                                    sRefNo = RefNo_list.Rows[0]["sRefNo"].ToString();
                                                }
                                                sRefNo = sRefNo == "" ? StoneId : sRefNo;
                                                res_list.Add(new ConfirmPlaceOrderResponse()
                                                {
                                                    RefNo = sRefNo,
                                                    SunriseStatus = (SunriseStatus == "" ? "Fail" : SunriseStatus),
                                                    SupplierName = dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper(),
                                                    SupplierStatus = SupplierStatus,
                                                    LabEntryStatus = LabEntryStatus
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                            {
                                Data = res_list,
                                Message = "No Record will be Proceed",
                                Status = "0"
                            });
                        }
                    }
                }

                if (res_list.Count() > 0)
                {
                    return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                    {
                        Data = res_list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                    {
                        Data = res_list,
                        Message = "No Record will be Proceed",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                {
                    Data = new List<ConfirmPlaceOrderResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }

            #endregion
        }

        #endregion





        #region When User Placed Order Then Auto Purchase Confirm Order Place For Web

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult AUTO_PlaceConfirmOrderUsingApi_Web([FromBody] JObject data)
        {
            JObject test1 = JObject.Parse(data.ToString());
            SupplierApiOrderRequest confirmOrderRequest = new SupplierApiOrderRequest();
            SupplierApiOrderRequest_AUTO confirmOrderRequest_tmp = new SupplierApiOrderRequest_AUTO();

            int LogInID = 0, Entry_UserId = 0;

            #region input param get for purchase order

            try
            {
                confirmOrderRequest_tmp = JsonConvert.DeserializeObject<SupplierApiOrderRequest_AUTO>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());

                confirmOrderRequest.iOrderid_sRefNo = confirmOrderRequest_tmp.iOrderid_sRefNo;
                confirmOrderRequest.DeviceType = confirmOrderRequest_tmp.DeviceType;
                confirmOrderRequest.IpAddress = confirmOrderRequest_tmp.IpAddress;

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(confirmOrderRequest_tmp.iOrderid_sRefNo))
                    para.Add(db.CreateParam("iOrderid_sRefNo", DbType.String, ParameterDirection.Input, confirmOrderRequest_tmp.iOrderid_sRefNo));
                else
                    para.Add(db.CreateParam("iOrderid_sRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(confirmOrderRequest_tmp.DeviceType))
                    para.Add(db.CreateParam("DeviceType", DbType.String, ParameterDirection.Input, confirmOrderRequest_tmp.DeviceType));
                else
                    para.Add(db.CreateParam("DeviceType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(confirmOrderRequest_tmp.IpAddress))
                    para.Add(db.CreateParam("IpAddress", DbType.String, ParameterDirection.Input, confirmOrderRequest_tmp.IpAddress));
                else
                    para.Add(db.CreateParam("IpAddress", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt_1 = db.ExecuteSP("ConfirmOrder_AUTO_ApiRequest_Insert", para.ToArray(), false);
                
                if (dt_1 != null && dt_1.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt_1.Rows[0]["Mas_Id"]) > 0 && dt_1.Rows[0]["Status"].ToString() == "1")
                    {
                        db = new Database(Request); 
                        para = new List<IDbDataParameter>();
                        para.Add(db.CreateParam("Mas_Id", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(dt_1.Rows[0]["Mas_Id"])));

                        DataTable dt = db.ExecuteSP("ConfirmOrder_ApiRequest_Get", para.ToArray(), false);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(dt.Rows[0]["IsAutoFromCust"].ToString()) == true)
                            {
                                LogInID = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                                Entry_UserId = Convert.ToInt32(dt.Rows[0]["Entry_UserId"].ToString());
                            }
                            else
                            {
                                LogInID = Convert.ToInt32(dt.Rows[0]["Entry_UserId"].ToString());
                            }
                            confirmOrderRequest.DeviceType = dt.Rows[0]["DeviceType"].ToString();
                            confirmOrderRequest.IpAddress = dt.Rows[0]["IpAddress"].ToString();

                            List<ObjOrderLst> sublist = new List<ObjOrderLst>();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ObjOrderLst sub = new ObjOrderLst();
                                sub.Refno = dt.Rows[i]["Refno"].ToString();
                                sub.Orderid = Convert.ToInt32(dt.Rows[i]["Orderid"].ToString());
                                sub.UserId = Convert.ToInt32(dt.Rows[i]["UserId"].ToString());
                                sub.SuppValue = dt.Rows[i]["SuppValue"].ToString();
                                sub.Comments = dt.Rows[i]["Comments"].ToString();
                                sublist.Add(sub);
                            }
                            confirmOrderRequest.Orders = sublist;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            #endregion

            #region main code of purchase order

            try
            {
                List<ObjOrderLst> list_1 = new List<ObjOrderLst>();
                ObjOrderLst sub = new ObjOrderLst();
                bool status;

                List<ConfirmPlaceOrderResponse> res_list = new List<ConfirmPlaceOrderResponse>();
                string MsgRef = string.Empty, LabResponse = string.Empty, SunriseStatus = string.Empty, SupplierStatus = string.Empty, LabEntryStatus = string.Empty;

                foreach (var obj in confirmOrderRequest.Orders)
                {
                    status = true;

                    foreach (var obj1 in list_1)
                    {
                        if (obj.Refno == obj1.Refno && obj.Orderid == obj1.Orderid)
                        {
                            status = false;
                        }
                    }

                    if (status == true)
                    {
                        sub = new ObjOrderLst();
                        sub.Refno = obj.Refno;
                        sub.Orderid = obj.Orderid;
                        sub.UserId = obj.UserId;
                        sub.SuppValue = obj.SuppValue;
                        sub.Comments = obj.Comments;
                        list_1.Add(sub);


                        DataTable dtSupplierRef = GetSupplierWiseOrderRefno(obj.Refno, LogInID, obj.Orderid);
                        if (dtSupplierRef != null && dtSupplierRef.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dtSupplierRef.Rows.Count - 1; i++)
                            {
                                if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "SHAIRU GEMS DIAMONDS PVT. LTD - (S)".ToString().ToUpper())
                                {
                                    if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
                                    {
                                        string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
                                        string _LoginResponse = GetShairuAPITaken();
                                        ShairuApiLoginResponse _data = new ShairuApiLoginResponse();
                                        _data = (new JavaScriptSerializer()).Deserialize<ShairuApiLoginResponse>(_LoginResponse);
                                        if (_data.UserId > 0)
                                        {
                                            foreach (string StoneId in RefnoList)
                                            {
                                                SunriseStatus = string.Empty; SupplierStatus = string.Empty; LabEntryStatus = string.Empty;

                                                ShairuApiHoldResponse _Holddata = new ShairuApiHoldResponse();
                                                if (StoneId.ToString() != "")
                                                {
                                                    DataTable DT_PurLg1 = PurchaseOrderLog_Insert(StoneId, true, false, 0, LogInID);
                                                    int PurLg_Id = 0;
                                                    if (DT_PurLg1 != null && DT_PurLg1.Rows.Count > 0)
                                                    {
                                                        PurLg_Id = Convert.ToInt32(DT_PurLg1.Rows[0]["Id"].ToString());
                                                    }

                                                    string _HoldResponse = ShairuHoldStoneRequest(_data.TokenId.ToString(), StoneId, _data.UserId, obj.Comments);
                                                    _Holddata = (new JavaScriptSerializer()).Deserialize<ShairuApiHoldResponse>(_HoldResponse);

                                                    SupplierStatus = _Holddata.Message;

                                                    if (SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY"))
                                                    {
                                                        SunriseStatus = "Confirm";
                                                        LabEntryStatus = "Confirm";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("ORDER PLACED SUCCESSFULLY."))
                                                    {
                                                        SunriseStatus = "Confirm";
                                                        LabEntryStatus = "Confirm";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY. THIS REF NO ARE NOT PROCESSED"))
                                                    {
                                                        SunriseStatus = "Busy";
                                                        LabEntryStatus = "Busy";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("THIS STONE(S) ARE SUBJECT TO AVAILABILITY"))
                                                    {
                                                        SunriseStatus = "Busy";
                                                        LabEntryStatus = "Waiting";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("NOT PROCESSED"))
                                                    {
                                                        SunriseStatus = "Error";
                                                        LabEntryStatus = "Waiting";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("FAILED TO CONFIRM STONE"))
                                                    {
                                                        SunriseStatus = "Error";
                                                        LabEntryStatus = "Waiting";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("NO RECORD WILL BE PROCEED"))
                                                    {
                                                        SunriseStatus = "Error";
                                                        LabEntryStatus = "Waiting";
                                                    }

                                                    if (_Holddata.Status != null &&
                                                            (
                                                                (_Holddata.Status.ToUpper() == "SUCCESS" && SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY")) ||
                                                                (_Holddata.Status.ToUpper() == "1" && SupplierStatus.ToUpper().Contains("ORDER PLACED SUCCESSFULLY."))
                                                            )
                                                        )
                                                    {
                                                        #region SHAIRU API RETURN SUCCESS

                                                        try
                                                        {
                                                            DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                            int LbLg_Id = 0;
                                                            if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                            {
                                                                LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                            }

                                                            LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                            DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                            LabResponse = ex.Message.ToString();
                                                        }

                                                        DataTable dtUpdate = UpdateOrderDetIsConfirmStatus(obj.Refno, obj.Orderid);

                                                        if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                                                        {
                                                            if (dtUpdate.Rows[0]["Msg"].ToString() == "Success")
                                                            {
                                                                InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType, confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                            }
                                                            else
                                                            {
                                                                InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                        }

                                                        try
                                                        {
                                                            SendPurchaseMail("AUTO", "SUCCESS", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), (Entry_UserId != 0 ? Entry_UserId.ToString() : LogInID.ToString()), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                        }

                                                        MsgRef += obj.Refno + ",";

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        #region SHAIRU API RETURN FAIL

                                                        if (_Holddata.Status == null)
                                                        {
                                                            SupplierStatus = "";
                                                            SunriseStatus = "Not Available";
                                                            LabEntryStatus = "Reject";
                                                        }

                                                        try
                                                        {
                                                            DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                            int LbLg_Id = 0;
                                                            if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                            {
                                                                LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                            }

                                                            LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                            DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                            LabResponse = ex.Message.ToString();
                                                        }

                                                        InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);

                                                        try
                                                        {
                                                            SendPurchaseMail("AUTO", "ERROR", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), (Entry_UserId != 0 ? Entry_UserId.ToString() : LogInID.ToString()), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                        }

                                                        #endregion
                                                    }


                                                    DataTable DT_PurLg2 = PurchaseOrderLog_Insert(StoneId, false, true, PurLg_Id, LogInID);

                                                    res_list.Add(new ConfirmPlaceOrderResponse()
                                                    {
                                                        RefNo = StoneId,
                                                        SunriseStatus = (SunriseStatus == "" ? "Fail" : SunriseStatus),
                                                        SupplierName = dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper(),
                                                        SupplierStatus = SupplierStatus,
                                                        LabEntryStatus = LabEntryStatus
                                                    });
                                                }
                                            }
                                        }

                                    }
                                }
                                else if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "J.B. AND BROTHERS PVT. LTD - (S)".ToString().ToUpper())
                                {
                                    if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
                                    {
                                        string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
                                        foreach (string StoneId in RefnoList)
                                        {
                                            SunriseStatus = string.Empty;
                                            if (StoneId.ToString() != "")
                                            {
                                                DataTable DT_PurLg1 = PurchaseOrderLog_Insert(StoneId, true, false, 0, LogInID);
                                                int PurLg_Id = 0;
                                                if (DT_PurLg1 != null && DT_PurLg1.Rows.Count > 0)
                                                {
                                                    PurLg_Id = Convert.ToInt32(DT_PurLg1.Rows[0]["Id"].ToString());
                                                }

                                                string _HoldResponse = JBHoldRequest(StoneId + "|" + obj.SuppValue.ToString());
                                                List<JBApiHoldResponse> _Holddata = new List<JBApiHoldResponse>();

                                                _Holddata = (new JavaScriptSerializer()).Deserialize<List<JBApiHoldResponse>>(_HoldResponse);

                                                SupplierStatus = _Holddata[0].Status;

                                                if (SupplierStatus.ToUpper().Contains("BUYING SUCCESSFUL."))
                                                {
                                                    SunriseStatus = "Confirm";
                                                    LabEntryStatus = "Confirm";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("ERROR, PLEASE TRY AGAIN!"))
                                                {
                                                    SunriseStatus = "Pending";
                                                    LabEntryStatus = "Waiting";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("PACKET UNAVAILABLE!"))
                                                {
                                                    SunriseStatus = "Not Available";
                                                    LabEntryStatus = "Busy";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("INVALID REF NO!"))
                                                {
                                                    SunriseStatus = "Error";
                                                    LabEntryStatus = "Waiting";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("PACKET(S) ARE ON MEMO."))
                                                {
                                                    SunriseStatus = "Busy";
                                                    LabEntryStatus = "Busy";
                                                }

                                                if (SupplierStatus != null && SupplierStatus.ToUpper() == "BUYING SUCCESSFUL.")
                                                {
                                                    #region JB API RETURN SUCCESS

                                                    try
                                                    {
                                                        DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                        int LbLg_Id = 0;
                                                        if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                        {
                                                            LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                        }

                                                        LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                        DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                        LabResponse = ex.Message.ToString();
                                                    }

                                                    DataTable dtUpdate = UpdateOrderDetIsConfirmStatus(obj.Refno, obj.Orderid);

                                                    if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                                                    {
                                                        if (dtUpdate.Rows[0]["Msg"].ToString() == "Success")
                                                        {
                                                            InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue.ToString(), confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                        }
                                                        else
                                                        {
                                                            InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                    }

                                                    try
                                                    {
                                                        SendPurchaseMail("AUTO", "SUCCESS", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), (Entry_UserId != 0 ? Entry_UserId.ToString() : LogInID.ToString()), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                    }

                                                    MsgRef += obj.Refno + ",";

                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region JB API RETURN FAIL

                                                    if (SupplierStatus == null)
                                                    {
                                                        SupplierStatus = "";
                                                        SunriseStatus = "Not Available";
                                                        LabEntryStatus = "Reject";
                                                    }

                                                    try
                                                    {
                                                        DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                        int LbLg_Id = 0;
                                                        if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                        {
                                                            LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                        }

                                                        LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                        DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                        LabResponse = ex.Message.ToString();
                                                    }

                                                    InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "FAIL", "J.B. AND BROTHERS PVT. LTD - (S)", "", obj.Orderid, _Holddata[0].Price, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);

                                                    try
                                                    {
                                                        SendPurchaseMail("AUTO", "ERROR", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), (Entry_UserId != 0 ? Entry_UserId.ToString() : LogInID.ToString()), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                    }

                                                    #endregion
                                                }

                                                DataTable DT_PurLg2 = PurchaseOrderLog_Insert(StoneId, false, true, PurLg_Id, LogInID);

                                                string sRefNo = string.Empty;
                                                DataTable RefNo_list = RefNo_Get_Using_Supplier_Stone_No_in_OrderHis(StoneId, obj.Orderid);
                                                if (RefNo_list != null && RefNo_list.Rows.Count > 0)
                                                {
                                                    sRefNo = RefNo_list.Rows[0]["sRefNo"].ToString();
                                                }
                                                sRefNo = sRefNo == "" ? StoneId : sRefNo;
                                                res_list.Add(new ConfirmPlaceOrderResponse()
                                                {
                                                    RefNo = sRefNo,
                                                    SunriseStatus = (SunriseStatus == "" ? "Fail" : SunriseStatus),
                                                    SupplierName = dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper(),
                                                    SupplierStatus = SupplierStatus,
                                                    LabEntryStatus = LabEntryStatus
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                            {
                                Data = res_list,
                                Message = "No Record will be Proceed",
                                Status = "0"
                            });
                        }
                    }
                }

                if (res_list.Count() > 0)
                {
                    return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                    {
                        Data = res_list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                    {
                        Data = res_list,
                        Message = "No Record will be Proceed",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                {
                    Data = new List<ConfirmPlaceOrderResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }

            #endregion
        }

        #endregion


        #region When User Placed Order Then Auto Purchase Confirm Order Place For Application

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult AUTO_PlaceConfirmOrderUsingApi_Application([FromBody] JObject data)
        {
            SupplierApiOrderRequest confirmOrderRequest = new SupplierApiOrderRequest();

            int LogInID = 0, Entry_UserId = 0;

            #region input param get for purchase order

            try
            {
                confirmOrderRequest = JsonConvert.DeserializeObject<SupplierApiOrderRequest>(data.ToString());

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(confirmOrderRequest.iOrderid_sRefNo))
                    para.Add(db.CreateParam("iOrderid_sRefNo", DbType.String, ParameterDirection.Input, confirmOrderRequest.iOrderid_sRefNo));
                else
                    para.Add(db.CreateParam("iOrderid_sRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(confirmOrderRequest.DeviceType))
                    para.Add(db.CreateParam("DeviceType", DbType.String, ParameterDirection.Input, confirmOrderRequest.DeviceType));
                else
                    para.Add(db.CreateParam("DeviceType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(confirmOrderRequest.IpAddress))
                    para.Add(db.CreateParam("IpAddress", DbType.String, ParameterDirection.Input, confirmOrderRequest.IpAddress));
                else
                    para.Add(db.CreateParam("IpAddress", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt_1 = db.ExecuteSP("ConfirmOrder_AUTO_ApiRequest_Insert", para.ToArray(), false);

                if (dt_1 != null && dt_1.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt_1.Rows[0]["Mas_Id"]) > 0 && dt_1.Rows[0]["Message"].ToString() == "SUCCESS")
                    {
                        db = new Database(Request); 
                        para = new List<IDbDataParameter>();
                        para.Add(db.CreateParam("Mas_Id", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(dt_1.Rows[0]["Mas_Id"])));

                        DataTable dt = db.ExecuteSP("ConfirmOrder_ApiRequest_Get", para.ToArray(), false);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(dt.Rows[0]["IsAutoFromCust"].ToString()) == true)
                            {
                                LogInID = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                                Entry_UserId = Convert.ToInt32(dt.Rows[0]["Entry_UserId"].ToString());
                            }
                            else
                            {
                                LogInID = Convert.ToInt32(dt.Rows[0]["Entry_UserId"].ToString());
                            }
                            confirmOrderRequest.DeviceType = dt.Rows[0]["DeviceType"].ToString();
                            confirmOrderRequest.IpAddress = dt.Rows[0]["IpAddress"].ToString();

                            List<ObjOrderLst> sublist = new List<ObjOrderLst>();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ObjOrderLst sub = new ObjOrderLst();
                                sub.Refno = dt.Rows[i]["Refno"].ToString();
                                sub.Orderid = Convert.ToInt32(dt.Rows[i]["Orderid"].ToString());
                                sub.UserId = Convert.ToInt32(dt.Rows[i]["UserId"].ToString());
                                sub.SuppValue = dt.Rows[i]["SuppValue"].ToString();
                                sub.Comments = dt.Rows[i]["Comments"].ToString();
                                sublist.Add(sub);
                            }
                            confirmOrderRequest.Orders = sublist;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            #endregion

            #region main code of purchase order

            try
            {
                List<ObjOrderLst> list_1 = new List<ObjOrderLst>();
                ObjOrderLst sub = new ObjOrderLst();
                bool status;

                List<ConfirmPlaceOrderResponse> res_list = new List<ConfirmPlaceOrderResponse>();
                string MsgRef = string.Empty, LabResponse = string.Empty, SunriseStatus = string.Empty, SupplierStatus = string.Empty, LabEntryStatus = string.Empty;

                foreach (var obj in confirmOrderRequest.Orders)
                {
                    status = true;

                    foreach (var obj1 in list_1)
                    {
                        if (obj.Refno == obj1.Refno && obj.Orderid == obj1.Orderid)
                        {
                            status = false;
                        }
                    }

                    if (status == true)
                    {
                        sub = new ObjOrderLst();
                        sub.Refno = obj.Refno;
                        sub.Orderid = obj.Orderid;
                        sub.UserId = obj.UserId;
                        sub.SuppValue = obj.SuppValue;
                        sub.Comments = obj.Comments;
                        list_1.Add(sub);


                        DataTable dtSupplierRef = GetSupplierWiseOrderRefno(obj.Refno, LogInID, obj.Orderid);
                        if (dtSupplierRef != null && dtSupplierRef.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dtSupplierRef.Rows.Count - 1; i++)
                            {
                                if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "SHAIRU GEMS DIAMONDS PVT. LTD - (S)".ToString().ToUpper())
                                {
                                    if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
                                    {
                                        string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
                                        string _LoginResponse = GetShairuAPITaken();
                                        ShairuApiLoginResponse _data = new ShairuApiLoginResponse();
                                        _data = (new JavaScriptSerializer()).Deserialize<ShairuApiLoginResponse>(_LoginResponse);
                                        if (_data.UserId > 0)
                                        {
                                            foreach (string StoneId in RefnoList)
                                            {
                                                SunriseStatus = string.Empty; SupplierStatus = string.Empty; LabEntryStatus = string.Empty;

                                                ShairuApiHoldResponse _Holddata = new ShairuApiHoldResponse();
                                                if (StoneId.ToString() != "")
                                                {
                                                    DataTable DT_PurLg1 = PurchaseOrderLog_Insert(StoneId, true, false, 0, LogInID);
                                                    int PurLg_Id = 0;
                                                    if (DT_PurLg1 != null && DT_PurLg1.Rows.Count > 0)
                                                    {
                                                        PurLg_Id = Convert.ToInt32(DT_PurLg1.Rows[0]["Id"].ToString());
                                                    }

                                                    string _HoldResponse = ShairuHoldStoneRequest(_data.TokenId.ToString(), StoneId, _data.UserId, obj.Comments);
                                                    _Holddata = (new JavaScriptSerializer()).Deserialize<ShairuApiHoldResponse>(_HoldResponse);

                                                    SupplierStatus = _Holddata.Message;

                                                    if (SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY"))
                                                    {
                                                        SunriseStatus = "Confirm";
                                                        LabEntryStatus = "Confirm";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("ORDER PLACED SUCCESSFULLY."))
                                                    {
                                                        SunriseStatus = "Confirm";
                                                        LabEntryStatus = "Confirm";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY. THIS REF NO ARE NOT PROCESSED"))
                                                    {
                                                        SunriseStatus = "Busy";
                                                        LabEntryStatus = "Busy";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("THIS STONE(S) ARE SUBJECT TO AVAILABILITY"))
                                                    {
                                                        SunriseStatus = "Busy";
                                                        LabEntryStatus = "Waiting";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("NOT PROCESSED"))
                                                    {
                                                        SunriseStatus = "Error";
                                                        LabEntryStatus = "Waiting";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("FAILED TO CONFIRM STONE"))
                                                    {
                                                        SunriseStatus = "Error";
                                                        LabEntryStatus = "Waiting";
                                                    }
                                                    else if (SupplierStatus.ToUpper().Contains("NO RECORD WILL BE PROCEED"))
                                                    {
                                                        SunriseStatus = "Error";
                                                        LabEntryStatus = "Waiting";
                                                    }

                                                    if (_Holddata.Status != null &&
                                                            (
                                                                (_Holddata.Status.ToUpper() == "SUCCESS" && SupplierStatus.ToUpper().Contains("YOUR TRANSACTION DONE SUCCESSFULLY")) ||
                                                                (_Holddata.Status.ToUpper() == "1" && SupplierStatus.ToUpper().Contains("ORDER PLACED SUCCESSFULLY."))
                                                            )
                                                        )
                                                    {
                                                        #region SHAIRU API RETURN SUCCESS

                                                        try
                                                        {
                                                            DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                            int LbLg_Id = 0;
                                                            if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                            {
                                                                LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                            }

                                                            LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                            DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                            LabResponse = ex.Message.ToString();
                                                        }

                                                        DataTable dtUpdate = UpdateOrderDetIsConfirmStatus(obj.Refno, obj.Orderid);

                                                        if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                                                        {
                                                            if (dtUpdate.Rows[0]["Msg"].ToString() == "Success")
                                                            {
                                                                InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType, confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                            }
                                                            else
                                                            {
                                                                InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                        }

                                                        try
                                                        {
                                                            SendPurchaseMail("AUTO", "SUCCESS", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), (Entry_UserId != 0 ? Entry_UserId.ToString() : LogInID.ToString()), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                        }

                                                        MsgRef += obj.Refno + ",";

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        #region SHAIRU API RETURN FAIL

                                                        if (_Holddata.Status == null)
                                                        {
                                                            SupplierStatus = "";
                                                            SunriseStatus = "Not Available";
                                                            LabEntryStatus = "Reject";
                                                        }

                                                        try
                                                        {
                                                            DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                            int LbLg_Id = 0;
                                                            if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                            {
                                                                LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                            }

                                                            LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                            DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                            LabResponse = ex.Message.ToString();
                                                        }

                                                        InsertSupplierAPIResponse(StoneId, SupplierStatus, _Holddata.Status, "SHAIRU GEMS DIAMONDS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);

                                                        try
                                                        {
                                                            SendPurchaseMail("AUTO", "ERROR", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), (Entry_UserId != 0 ? Entry_UserId.ToString() : LogInID.ToString()), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DAL.Common.InsertErrorLog(ex, null, Request);
                                                        }

                                                        #endregion
                                                    }


                                                    DataTable DT_PurLg2 = PurchaseOrderLog_Insert(StoneId, false, true, PurLg_Id, LogInID);

                                                    res_list.Add(new ConfirmPlaceOrderResponse()
                                                    {
                                                        RefNo = StoneId,
                                                        SunriseStatus = (SunriseStatus == "" ? "Fail" : SunriseStatus),
                                                        SupplierName = dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper(),
                                                        SupplierStatus = SupplierStatus,
                                                        LabEntryStatus = LabEntryStatus
                                                    });
                                                }
                                            }
                                        }

                                    }
                                }
                                else if (dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper() == "J.B. AND BROTHERS PVT. LTD - (S)".ToString().ToUpper())
                                {
                                    if (dtSupplierRef.Rows[i]["sRefno"].ToString() != "")
                                    {
                                        string[] RefnoList = dtSupplierRef.Rows[i]["sRefno"].ToString().Split(',');
                                        foreach (string StoneId in RefnoList)
                                        {
                                            SunriseStatus = string.Empty;
                                            if (StoneId.ToString() != "")
                                            {
                                                DataTable DT_PurLg1 = PurchaseOrderLog_Insert(StoneId, true, false, 0, LogInID);
                                                int PurLg_Id = 0;
                                                if (DT_PurLg1 != null && DT_PurLg1.Rows.Count > 0)
                                                {
                                                    PurLg_Id = Convert.ToInt32(DT_PurLg1.Rows[0]["Id"].ToString());
                                                }

                                                string _HoldResponse = JBHoldRequest(StoneId + "|" + obj.SuppValue.ToString());
                                                List<JBApiHoldResponse> _Holddata = new List<JBApiHoldResponse>();

                                                _Holddata = (new JavaScriptSerializer()).Deserialize<List<JBApiHoldResponse>>(_HoldResponse);

                                                SupplierStatus = _Holddata[0].Status;

                                                if (SupplierStatus.ToUpper().Contains("BUYING SUCCESSFUL."))
                                                {
                                                    SunriseStatus = "Confirm";
                                                    LabEntryStatus = "Confirm";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("ERROR, PLEASE TRY AGAIN!"))
                                                {
                                                    SunriseStatus = "Pending";
                                                    LabEntryStatus = "Waiting";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("PACKET UNAVAILABLE!"))
                                                {
                                                    SunriseStatus = "Not Available";
                                                    LabEntryStatus = "Busy";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("INVALID REF NO!"))
                                                {
                                                    SunriseStatus = "Error";
                                                    LabEntryStatus = "Waiting";
                                                }
                                                else if (SupplierStatus.ToUpper().Contains("PACKET(S) ARE ON MEMO."))
                                                {
                                                    SunriseStatus = "Busy";
                                                    LabEntryStatus = "Busy";
                                                }

                                                if (SupplierStatus != null && SupplierStatus.ToUpper() == "BUYING SUCCESSFUL.")
                                                {
                                                    #region JB API RETURN SUCCESS

                                                    try
                                                    {
                                                        DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                        int LbLg_Id = 0;
                                                        if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                        {
                                                            LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                        }

                                                        LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                        DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                        LabResponse = ex.Message.ToString();
                                                    }

                                                    DataTable dtUpdate = UpdateOrderDetIsConfirmStatus(obj.Refno, obj.Orderid);

                                                    if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                                                    {
                                                        if (dtUpdate.Rows[0]["Msg"].ToString() == "Success")
                                                        {
                                                            InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "", obj.Orderid, obj.SuppValue.ToString(), confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                        }
                                                        else
                                                        {
                                                            InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "SUCCESS", "J.B. AND BROTHERS PVT. LTD - (S)", "Update Fail In Sunrise OrderDet Table : " + dtUpdate.Rows[0]["Msg"].ToString(), obj.Orderid, obj.SuppValue, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);
                                                    }

                                                    try
                                                    {
                                                        SendPurchaseMail("AUTO", "SUCCESS", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), (Entry_UserId != 0 ? Entry_UserId.ToString() : LogInID.ToString()), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                    }

                                                    MsgRef += obj.Refno + ",";

                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region JB API RETURN FAIL

                                                    if (SupplierStatus == null)
                                                    {
                                                        SupplierStatus = "";
                                                        SunriseStatus = "Not Available";
                                                        LabEntryStatus = "Reject";
                                                    }

                                                    try
                                                    {
                                                        DataTable DT_LbLg1 = LabEntryLog_Insert(StoneId, true, false, 0, LogInID);
                                                        int LbLg_Id = 0;
                                                        if (DT_LbLg1 != null && DT_LbLg1.Rows.Count > 0)
                                                        {
                                                            LbLg_Id = Convert.ToInt32(DT_LbLg1.Rows[0]["Id"].ToString());
                                                        }

                                                        LabResponse = InsertLabAutoEntry(obj.UserId.ToString(), obj.Refno, obj.Comments, LabEntryStatus);

                                                        DataTable DT_LbLg2 = LabEntryLog_Insert(StoneId, false, true, LbLg_Id, LogInID);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                        LabResponse = ex.Message.ToString();
                                                    }

                                                    InsertSupplierAPIResponse(obj.Refno, SupplierStatus, "FAIL", "J.B. AND BROTHERS PVT. LTD - (S)", "", obj.Orderid, _Holddata[0].Price, confirmOrderRequest.DeviceType.ToString(), confirmOrderRequest.IpAddress, LabResponse, LogInID);

                                                    try
                                                    {
                                                        SendPurchaseMail("AUTO", "ERROR", obj.Orderid.ToString(), obj.Comments, obj.UserId.ToString(), (Entry_UserId != 0 ? Entry_UserId.ToString() : LogInID.ToString()), obj.Refno, obj.SuppValue, SupplierStatus, SunriseStatus);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DAL.Common.InsertErrorLog(ex, null, Request);
                                                    }

                                                    #endregion
                                                }

                                                DataTable DT_PurLg2 = PurchaseOrderLog_Insert(StoneId, false, true, PurLg_Id, LogInID);

                                                string sRefNo = string.Empty;
                                                DataTable RefNo_list = RefNo_Get_Using_Supplier_Stone_No_in_OrderHis(StoneId, obj.Orderid);
                                                if (RefNo_list != null && RefNo_list.Rows.Count > 0)
                                                {
                                                    sRefNo = RefNo_list.Rows[0]["sRefNo"].ToString();
                                                }
                                                sRefNo = sRefNo == "" ? StoneId : sRefNo;
                                                res_list.Add(new ConfirmPlaceOrderResponse()
                                                {
                                                    RefNo = sRefNo,
                                                    SunriseStatus = (SunriseStatus == "" ? "Fail" : SunriseStatus),
                                                    SupplierName = dtSupplierRef.Rows[i]["Supplier"].ToString().ToUpper(),
                                                    SupplierStatus = SupplierStatus,
                                                    LabEntryStatus = LabEntryStatus
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                            {
                                Data = res_list,
                                Message = "No Record will be Proceed",
                                Status = "0"
                            });
                        }
                    }
                }

                if (res_list.Count() > 0)
                {
                    return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                    {
                        Data = res_list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                    {
                        Data = res_list,
                        Message = "No Record will be Proceed",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ConfirmPlaceOrderResponse>
                {
                    Data = new List<ConfirmPlaceOrderResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }

            #endregion
        }

        #endregion









        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult ArrayCheck([FromBody] JObject data)
        {
            SupplierApiOrderRequest confirmOrderRequest = new SupplierApiOrderRequest();
            List<ObjOrderLst> sublist = new List<ObjOrderLst>();
            List<ObjOrderLst> list_1 = new List<ObjOrderLst>();
            ObjOrderLst sub = new ObjOrderLst();

            confirmOrderRequest.DeviceType = "Web";
            confirmOrderRequest.IpAddress = "123.45.254";

            sub = new ObjOrderLst();
            sub.Refno = "ABC";
            sub.Orderid = 123;
            sub.UserId = 1;
            sub.SuppValue = "523.56";
            sub.Comments = "Add By Hardik";
            sublist.Add(sub);

            sub = new ObjOrderLst();
            sub.Refno = "DEF";
            sub.Orderid = 456;
            sub.UserId = 1;
            sub.SuppValue = "923.56";
            sub.Comments = "Add By Hardik 1";
            sublist.Add(sub);

            sub = new ObjOrderLst();
            sub.Refno = "ABC";
            sub.Orderid = 123;
            sub.UserId = 1;
            sub.SuppValue = "523.56";
            sub.Comments = "Add By Hardik";
            sublist.Add(sub);

            sub = new ObjOrderLst();
            sub.Refno = "DEFRF";
            sub.Orderid = 45641;
            sub.UserId = 1;
            sub.SuppValue = "923.56";
            sub.Comments = "Add By Hardik 1";
            sublist.Add(sub);

            sub = new ObjOrderLst();
            sub.Refno = "DEF1RF";
            sub.Orderid = 45141;
            sub.UserId = 1;
            sub.SuppValue = "923.56";
            sub.Comments = "Add By Hardik 1";
            sublist.Add(sub);

            sub = new ObjOrderLst();
            sub.Refno = "DEF";
            sub.Orderid = 456;
            sub.UserId = 1;
            sub.SuppValue = "923.56";
            sub.Comments = "Add By Hardik 1";
            sublist.Add(sub);

            confirmOrderRequest.Orders = sublist;

            bool status;
            foreach (var obj in confirmOrderRequest.Orders)
            {
                status = true;

                foreach (var obj1 in list_1)
                {
                    if (obj.Refno == obj1.Refno && obj.Orderid == obj1.Orderid)
                    {
                        status = false;
                    }
                }

                if (status == true)
                {
                    sub = new ObjOrderLst();
                    sub.Refno = obj.Refno;
                    sub.Orderid = obj.Orderid;
                    sub.UserId = obj.UserId;
                    sub.SuppValue = obj.SuppValue;
                    sub.Comments = obj.Comments;
                    list_1.Add(sub);



                }


            }




            return Ok(new ServiceResponse<CommonResponse>
            {
                Data = new List<CommonResponse>(),
                Message = list_1.Count.ToString(),
                Status = "0"
            });
        }
    }
}
