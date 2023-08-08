using EpExcelExportLib;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunrise.Services.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Hosting;
using System.Web.Http;
using Oracle.DataAccess.Client;
using DAL;
using System.Net.Http;
using System.Security.Claims;
using System.Linq;
using System.Data.SqlClient;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/LabStock")]
    public class LabStockController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetTransId([FromBody]JObject data)
        {
            Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
            List<OracleParameter> paramList = new List<OracleParameter>();
            TransValueRequest req = new TransValueRequest();
            try
            {
                req = JsonConvert.DeserializeObject<TransValueRequest>(data.ToString());

                OracleParameter param1 = new OracleParameter("p_for_comp", OracleDbType.Int32);
                param1.Value = 1;
                paramList.Add(param1);

                OracleParameter param2 = new OracleParameter("p_from_date", OracleDbType.Date);
                param2.Value = req.FromDate;
                paramList.Add(param2);

                OracleParameter param3 = new OracleParameter("p_to_date", OracleDbType.Date);
                param3.Value = req.ToDate;
                paramList.Add(param3);

                OracleParameter param4 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param4.Direction = ParameterDirection.Output;
                paramList.Add(param4);

                System.Data.DataTable dt = oracleDbAccess.CallSP("GET_LAB_TRANSID", paramList);

                int rows = dt.Rows.Count;
                if (rows > 0)
                {
                    List<TransValue> list = new List<TransValue>();
                    int i = 0;
                    for (; i < rows; i++)
                    {
                        list.Add(new TransValue()
                        {
                            TransId = (dt.Rows[i]["TRANS_ID"] != null) ? Convert.ToInt32(dt.Rows[i]["TRANS_ID"]) : 0,
                            OfferName = (dt.Rows[i]["OFFER_NAME"] != null) ? Convert.ToString(dt.Rows[i]["OFFER_NAME"]) : ""
                        });
                    }

                    return Ok(new TransValueResponse() { Data = list, Msg = "success" });
                }
                else
                    return Ok(new TransValueResponse() { Data = null, Msg = "error" });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new TransValueResponse() { Data = null, Msg = "error" });
            }
            finally {
                oracleDbAccess = null;
                paramList = null;
                req = null;
            }
        }
        [HttpPost]
        public IHttpActionResult Lab_GetTransId([FromBody]JObject data)
        {
            TransValueRequest req = new TransValueRequest();
            try
            {
                req = JsonConvert.DeserializeObject<TransValueRequest>(data.ToString());

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(req.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, req.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, req.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_LabVendorList", para.ToArray(), false);

                List<TransValue> transvalue = new List<TransValue>();
                transvalue = DataTableExtension.ToList<TransValue>(dt);

                if (transvalue != null && transvalue.Count > 0)
                {
                    return Ok(new ServiceResponse<TransValue>
                    {
                        Data = transvalue,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<TransValue>
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
        public IHttpActionResult CustomerExcel([FromBody]JObject data)
        {
            Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
            List<OracleParameter> paramList = new List<OracleParameter>();
            LabStockDownloadRequest obj = new LabStockDownloadRequest();
            try
            {
                obj = JsonConvert.DeserializeObject<LabStockDownloadRequest>(data.ToString());
                if (!string.IsNullOrEmpty(obj.RefNo))
                {
                    obj.RefNo = obj.RefNo.ToUpper();
                }

                OracleParameter param1 = new OracleParameter("p_for_comp", OracleDbType.Int32);
                param1.Value = 1;
                paramList.Add(param1);

                OracleParameter param2 = new OracleParameter("p_from_date", OracleDbType.Date);
                param2.Value = obj.FromDate;
                paramList.Add(param2);

                OracleParameter param3 = new OracleParameter("p_to_date", OracleDbType.Date);
                param3.Value = obj.ToDate;
                paramList.Add(param3);

                OracleParameter param4 = new OracleParameter("p_trans_id", OracleDbType.Varchar2);
                param4.Value = obj.TransId;
                paramList.Add(param4);

                OracleParameter param5 = new OracleParameter("p_for_shape", OracleDbType.Varchar2);
                param5.Value = obj.Shape;
                paramList.Add(param5);

                OracleParameter param6 = new OracleParameter("p_for_color", OracleDbType.Varchar2);
                param6.Value = obj.Color;
                paramList.Add(param6);

                OracleParameter param7 = new OracleParameter("p_for_purity", OracleDbType.Varchar2);
                param7.Value = obj.Clarity;
                paramList.Add(param7);

                OracleParameter param8 = new OracleParameter("p_for_cut", OracleDbType.Varchar2);
                param8.Value = obj.Cut;
                paramList.Add(param8);

                OracleParameter param9 = new OracleParameter("p_for_symm", OracleDbType.Varchar2);
                param9.Value = obj.Symm;
                paramList.Add(param9);

                OracleParameter param10 = new OracleParameter("p_for_polish", OracleDbType.Varchar2);
                param10.Value = obj.Polish;
                paramList.Add(param10);

                OracleParameter param11 = new OracleParameter("p_for_fls", OracleDbType.Varchar2);
                param11.Value = obj.Fls;
                paramList.Add(param11);

                OracleParameter param12 = new OracleParameter("p_for_lab", OracleDbType.Varchar2);
                param12.Value = obj.Lab;
                paramList.Add(param12);

                //OracleParameter param13 = new OracleParameter("p_from_cts", OracleDbType.Decimal);
                //param13.Value = obj.FromCarat;
                //paramList.Add(param13);

                //OracleParameter param14 = new OracleParameter("p_to_cts", OracleDbType.Decimal);
                //param14.Value = obj.ToCarat;
                //paramList.Add(param14);

                OracleParameter param15 = new OracleParameter("p_for_cts", OracleDbType.Varchar2);
                param15.Value = obj.Pointer;
                paramList.Add(param15);

                OracleParameter param16 = new OracleParameter("p_for_ref", OracleDbType.Varchar2);
                param16.Value = obj.RefNo;
                paramList.Add(param16);

                OracleParameter param17 = new OracleParameter("p_from_length", OracleDbType.Double);
                param17.Value = ((obj.FromLength < 0 || obj.FromLength > 0) ? obj.FromLength : (double?)null);
                paramList.Add(param17);

                OracleParameter param18 = new OracleParameter("p_to_length", OracleDbType.Double);
                param18.Value = ((obj.ToLength < 0 || obj.ToLength > 0) ? obj.ToLength : (double?)null);
                paramList.Add(param18);

                OracleParameter param19 = new OracleParameter("p_from_width", OracleDbType.Double);
                param19.Value = ((obj.FromWidth < 0 || obj.FromWidth > 0) ? obj.FromWidth : (double?)null);
                paramList.Add(param19);

                OracleParameter param20 = new OracleParameter("p_to_width", OracleDbType.Double);
                param20.Value = ((obj.ToWidth < 0 || obj.ToWidth > 0) ? obj.ToWidth : (double?)null);
                paramList.Add(param20);

                OracleParameter param21 = new OracleParameter("p_from_depth", OracleDbType.Double);
                param21.Value = ((obj.FromDepth < 0 || obj.FromDepth > 0) ? obj.FromDepth : (double?)null);
                paramList.Add(param21);

                OracleParameter param22 = new OracleParameter("p_to_depth", OracleDbType.Double);
                param22.Value = ((obj.ToDepth < 0 || obj.ToDepth > 0) ? obj.ToDepth : (double?)null);
                paramList.Add(param22);

                OracleParameter param23 = new OracleParameter("p_from_disc", OracleDbType.Double);
                param23.Value = ((obj.FromDisc < 0 || obj.FromDisc > 0) ? obj.FromDisc : (double?)null);
                paramList.Add(param23);

                OracleParameter param24 = new OracleParameter("p_to_disc", OracleDbType.Double);
                param24.Value = ((obj.ToDisc < 0 || obj.ToDisc > 0) ? obj.ToDisc : (double?)null);
                paramList.Add(param24);

                OracleParameter param25 = new OracleParameter("p_from_price", OracleDbType.Double);
                param25.Value = ((obj.FromPrice < 0 || obj.FromPrice > 0) ? obj.FromPrice : (double?)null);
                paramList.Add(param25);

                OracleParameter param26 = new OracleParameter("p_to_price", OracleDbType.Double);
                param26.Value = ((obj.ToPrice < 0 || obj.ToPrice > 0) ? obj.ToPrice : (double?)null);
                paramList.Add(param26);

                OracleParameter param27 = new OracleParameter("p_from_amount", OracleDbType.Double);
                param27.Value = ((obj.FromAmount < 0 || obj.FromAmount > 0) ? obj.FromAmount : (double?)null);
                paramList.Add(param27);

                OracleParameter param28 = new OracleParameter("p_to_amount", OracleDbType.Double);
                param28.Value = ((obj.ToAmount < 0 || obj.ToAmount > 0) ? obj.ToAmount : (double?)null);
                paramList.Add(param28);

                OracleParameter param29 = new OracleParameter("p_from_depthper", OracleDbType.Double);
                param29.Value = ((obj.FromDepthper < 0 || obj.FromDepthper > 0) ? obj.FromDepthper : (double?)null);
                paramList.Add(param29);

                OracleParameter param30 = new OracleParameter("p_to_depthper", OracleDbType.Double);
                param30.Value = ((obj.ToDepthper < 0 || obj.ToDepthper > 0) ? obj.ToDepthper : (double?)null);
                paramList.Add(param30);

                OracleParameter param31 = new OracleParameter("p_from_tableper", OracleDbType.Double);
                param31.Value = ((obj.FromTableper < 0 || obj.FromTableper > 0) ? obj.FromTableper : (double?)null);
                paramList.Add(param31);

                OracleParameter param32 = new OracleParameter("p_to_tableper", OracleDbType.Double);
                param32.Value = ((obj.ToTableper < 0 || obj.ToTableper > 0) ? obj.ToTableper : (double?)null);
                paramList.Add(param32);

                OracleParameter param33 = new OracleParameter("p_for_tableblack", OracleDbType.Varchar2);
                param33.Value = obj.Tableblack;
                paramList.Add(param33);

                OracleParameter param34 = new OracleParameter("p_for_tablewhite", OracleDbType.Varchar2);
                param34.Value = obj.Tablewhite;
                paramList.Add(param34);

                OracleParameter param35 = new OracleParameter("p_for_crownblack", OracleDbType.Varchar2);
                param35.Value = obj.Crownblack;
                paramList.Add(param35);

                OracleParameter param36 = new OracleParameter("p_for_crownwhite", OracleDbType.Varchar2);
                param36.Value = obj.Crownwhite;
                paramList.Add(param36);

                OracleParameter param37 = new OracleParameter("p_from_crownangle", OracleDbType.Double);
                param37.Value = ((obj.FromCrownangle < 0 || obj.FromCrownangle > 0) ? obj.FromCrownangle : (double?)null);
                paramList.Add(param37);

                OracleParameter param38 = new OracleParameter("p_to_crownangle", OracleDbType.Double);
                param38.Value = ((obj.ToCrownangle < 0 || obj.ToCrownangle > 0) ? obj.ToCrownangle : (double?)null);
                paramList.Add(param38);

                OracleParameter param39 = new OracleParameter("p_from_crownheight", OracleDbType.Double);
                param39.Value = ((obj.FromCrownheight < 0 || obj.FromCrownheight > 0) ? obj.FromCrownheight : (double?)null);
                paramList.Add(param39);

                OracleParameter param40 = new OracleParameter("p_to_crownheight", OracleDbType.Double);
                param40.Value = ((obj.ToCrownheight < 0 || obj.ToCrownheight > 0) ? obj.ToCrownheight : (double?)null);
                paramList.Add(param40);

                OracleParameter param41 = new OracleParameter("p_from_pavangle", OracleDbType.Double);
                param41.Value = ((obj.FromPavangle < 0 || obj.FromPavangle > 0) ? obj.FromPavangle : (double?)null);
                paramList.Add(param41);

                OracleParameter param42 = new OracleParameter("p_to_pavangle", OracleDbType.Double);
                param42.Value = ((obj.ToPavangle < 0 || obj.ToPavangle > 0) ? obj.ToPavangle : (double?)null);
                paramList.Add(param42);

                OracleParameter param43 = new OracleParameter("p_from_pavheight", OracleDbType.Double);
                param43.Value = ((obj.FromPavheight < 0 || obj.FromPavheight > 0) ? obj.FromPavheight : (double?)null);
                paramList.Add(param43);

                OracleParameter param44 = new OracleParameter("p_to_pavheight", OracleDbType.Double);
                param44.Value = ((obj.ToPavheight < 0 || obj.ToPavheight > 0) ? obj.ToPavheight : (double?)null);
                paramList.Add(param44);

                /*
                 p_from_length number default null,  --d
                p_to_length number default null,   --d
                p_from_width number default null ,  --d
                p_to_width number default null,   --d
                p_from_depth number default null,   --d
                p_to_depth number default null,   --d
                p_from_disc number default null,
                p_to_disc number default null,
                p_from_price number default null,  --d
                p_to_price number default null,      --d
                p_from_amount number default null , --d
                p_to_amount number default null,    --d
                p_from_depthper number default null,  --d
                p_to_depthper number default null,  --d
                p_from_tableper number default null,   --d
                p_to_tableper number default null,    --d
                p_for_tableblack varchar2 default null, --d
                p_for_tablewhite varchar2 default null,  --d
                p_for_crownblack varchar2 default null,  --d
                p_for_crownwhite varchar2 default null,  --d
                p_from_crownangle number default null, --d                                                  
                p_to_crownangle number default null, --d
                p_from_crownheight number default null, --d                                                  
                p_to_crownheight number default null, --d
                p_from_pavangle number default null,    --d                                               
                p_to_pavangle number default null, --d
                p_from_pavheight number default null, --d                                                  
                p_to_pavheight number default null,
                 */

                OracleParameter param45 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param45.Direction = ParameterDirection.Output;
                paramList.Add(param45);

                System.Data.DataTable dt = oracleDbAccess.CallSP("GET_LAB_STOCK_WEBSITE", paramList);

                int rows = dt.Rows.Count;
                if (rows > 0)
                {
                    //dt.TableName = "Customer Lab Stock";

                    string filename = "";
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    if (obj.IsCustomer == 1)
                    {
                        filename = "Customer Lab Stock " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                        EpExcelExport.CreateCustomerExcel(dt.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }
                    else if (obj.IsCustomer == 2)
                    {
                        filename = "Customer Lab Stock " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                        EpExcelExport.CreateCustomerImageExcel(dt.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }
                    else
                    {
                        filename = "Supplier Lab Stock " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                        EpExcelExport.CreateSupplierExcel(dt.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                    return Ok("No data found.");
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Something Went wrong.\nPlease try again later");
            }
            finally
            {
                oracleDbAccess = null;
                paramList = null;
                obj = null;
            }
        }

        #region Lab Search Excel Create

        [NonAction]
        private DataTable GetLabSearchExcel(LabSearchStockDownloadRequest obj)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("ExcelType", DbType.Int32, ParameterDirection.Input, obj.ExcelType));

                if (!string.IsNullOrEmpty(obj.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, obj.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, obj.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.RefNo))
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, obj.RefNo.ToUpper()));
                else
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.iVendor))
                    para.Add(db.CreateParam("iVendor", DbType.String, ParameterDirection.Input, obj.iVendor));
                else
                    para.Add(db.CreateParam("iVendor", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sShape))
                    para.Add(db.CreateParam("sShape", DbType.String, ParameterDirection.Input, obj.sShape));
                else
                    para.Add(db.CreateParam("sShape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sPointer))
                    para.Add(db.CreateParam("sPointer", DbType.String, ParameterDirection.Input, obj.sPointer));
                else
                    para.Add(db.CreateParam("sPointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sColorType))
                    para.Add(db.CreateParam("sColorType", DbType.String, ParameterDirection.Input, obj.sColorType));
                else
                    para.Add(db.CreateParam("sColorType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sColor))
                    para.Add(db.CreateParam("sColor", DbType.String, ParameterDirection.Input, obj.sColor));
                else
                    para.Add(db.CreateParam("sColor", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sINTENSITY))
                    para.Add(db.CreateParam("sINTENSITY", DbType.String, ParameterDirection.Input, obj.sINTENSITY));
                else
                    para.Add(db.CreateParam("sINTENSITY", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sOVERTONE))
                    para.Add(db.CreateParam("sOVERTONE", DbType.String, ParameterDirection.Input, obj.sOVERTONE));
                else
                    para.Add(db.CreateParam("sOVERTONE", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sFANCY_COLOR))
                    para.Add(db.CreateParam("sFANCY_COLOR", DbType.String, ParameterDirection.Input, obj.sFANCY_COLOR));
                else
                    para.Add(db.CreateParam("sFANCY_COLOR", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sClarity))
                    para.Add(db.CreateParam("sClarity", DbType.String, ParameterDirection.Input, obj.sClarity));
                else
                    para.Add(db.CreateParam("sClarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sCut))
                    para.Add(db.CreateParam("sCut", DbType.String, ParameterDirection.Input, obj.sCut));
                else
                    para.Add(db.CreateParam("sCut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sPolish))
                    para.Add(db.CreateParam("sPolish", DbType.String, ParameterDirection.Input, obj.sPolish));
                else
                    para.Add(db.CreateParam("sPolish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sSymm))
                    para.Add(db.CreateParam("sSymm", DbType.String, ParameterDirection.Input, obj.sSymm));
                else
                    para.Add(db.CreateParam("sSymm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sFls))
                    para.Add(db.CreateParam("sFls", DbType.String, ParameterDirection.Input, obj.sFls));
                else
                    para.Add(db.CreateParam("sFls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sLab))
                    para.Add(db.CreateParam("sLab", DbType.String, ParameterDirection.Input, obj.sLab));
                else
                    para.Add(db.CreateParam("sLab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromDisc))
                    para.Add(db.CreateParam("dFromDisc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromDisc)));
                else
                    para.Add(db.CreateParam("dFromDisc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToDisc))
                    para.Add(db.CreateParam("dToDisc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToDisc)));
                else
                    para.Add(db.CreateParam("dToDisc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromTotAmt))
                    para.Add(db.CreateParam("dFromTotAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromTotAmt)));
                else
                    para.Add(db.CreateParam("dFromTotAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToTotAmt))
                    para.Add(db.CreateParam("dToTotAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToTotAmt)));
                else
                    para.Add(db.CreateParam("dToTotAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromLength))
                    para.Add(db.CreateParam("dFromLength", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromLength)));
                else
                    para.Add(db.CreateParam("dFromLength", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToLength))
                    para.Add(db.CreateParam("dToLength", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToLength)));
                else
                    para.Add(db.CreateParam("dToLength", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromWidth))
                    para.Add(db.CreateParam("dFromWidth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromWidth)));
                else
                    para.Add(db.CreateParam("dFromWidth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToWidth))
                    para.Add(db.CreateParam("dToWidth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToWidth)));
                else
                    para.Add(db.CreateParam("dToWidth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromDepth))
                    para.Add(db.CreateParam("dFromDepth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromDepth)));
                else
                    para.Add(db.CreateParam("dFromDepth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToDepth))
                    para.Add(db.CreateParam("dToDepth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToDepth)));
                else
                    para.Add(db.CreateParam("dToDepth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromDepthPer))
                    para.Add(db.CreateParam("dFromDepthPer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromDepthPer)));
                else
                    para.Add(db.CreateParam("dFromDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToDepthPer))
                    para.Add(db.CreateParam("dToDepthPer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToDepthPer)));
                else
                    para.Add(db.CreateParam("dToDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromTablePer))
                    para.Add(db.CreateParam("dFromTablePer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromTablePer)));
                else
                    para.Add(db.CreateParam("dFromTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToTablePer))
                    para.Add(db.CreateParam("dToTablePer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToTablePer)));
                else
                    para.Add(db.CreateParam("dToTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromCrAng))
                    para.Add(db.CreateParam("dFromCrAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromCrAng)));
                else
                    para.Add(db.CreateParam("dFromCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToCrAng))
                    para.Add(db.CreateParam("dToCrAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToCrAng)));
                else
                    para.Add(db.CreateParam("dToCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromCrHt))
                    para.Add(db.CreateParam("dFromCrHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromCrHt)));
                else
                    para.Add(db.CreateParam("dFromCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToCrHt))
                    para.Add(db.CreateParam("dToCrHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToCrHt)));
                else
                    para.Add(db.CreateParam("dToCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromPavAng))
                    para.Add(db.CreateParam("dFromPavAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromPavAng)));
                else
                    para.Add(db.CreateParam("dFromPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToPavAng))
                    para.Add(db.CreateParam("dToPavAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToPavAng)));
                else
                    para.Add(db.CreateParam("dToPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromPavHt))
                    para.Add(db.CreateParam("dFromPavHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromPavHt)));
                else
                    para.Add(db.CreateParam("dFromPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToPavHt))
                    para.Add(db.CreateParam("dToPavHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToPavHt)));
                else
                    para.Add(db.CreateParam("dToPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dKeytosymbol))
                    para.Add(db.CreateParam("dKeytosymbol", DbType.String, ParameterDirection.Input, obj.dKeytosymbol));
                else
                    para.Add(db.CreateParam("dKeytosymbol", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dCheckKTS))
                    para.Add(db.CreateParam("dCheckKTS", DbType.String, ParameterDirection.Input, obj.dCheckKTS));
                else
                    para.Add(db.CreateParam("dCheckKTS", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dUNCheckKTS))
                    para.Add(db.CreateParam("dUNCheckKTS", DbType.String, ParameterDirection.Input, obj.dUNCheckKTS));
                else
                    para.Add(db.CreateParam("dUNCheckKTS", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sBGM))
                    para.Add(db.CreateParam("sBGM", DbType.String, ParameterDirection.Input, obj.sBGM));
                else
                    para.Add(db.CreateParam("sBGM", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sCrownBlack))
                    para.Add(db.CreateParam("sCrownBlack", DbType.String, ParameterDirection.Input, obj.sCrownBlack));
                else
                    para.Add(db.CreateParam("sCrownBlack", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sTableBlack))
                    para.Add(db.CreateParam("sTableBlack", DbType.String, ParameterDirection.Input, obj.sTableBlack));
                else
                    para.Add(db.CreateParam("sTableBlack", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sCrownWhite))
                    para.Add(db.CreateParam("sCrownWhite", DbType.String, ParameterDirection.Input, obj.sCrownWhite));
                else
                    para.Add(db.CreateParam("sCrownWhite", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sTableWhite))
                    para.Add(db.CreateParam("sTableWhite", DbType.String, ParameterDirection.Input, obj.sTableWhite));
                else
                    para.Add(db.CreateParam("sTableWhite", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sTableOpen))
                    para.Add(db.CreateParam("sTableOpen", DbType.String, ParameterDirection.Input, obj.sTableOpen));
                else
                    para.Add(db.CreateParam("sTableOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sCrownOpen))
                    para.Add(db.CreateParam("sCrownOpen", DbType.String, ParameterDirection.Input, obj.sCrownOpen));
                else
                    para.Add(db.CreateParam("sCrownOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sPavOpen))
                    para.Add(db.CreateParam("sPavOpen", DbType.String, ParameterDirection.Input, obj.sPavOpen));
                else
                    para.Add(db.CreateParam("sPavOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sGirdleOpen))
                    para.Add(db.CreateParam("sGirdleOpen", DbType.String, ParameterDirection.Input, obj.sGirdleOpen));
                else
                    para.Add(db.CreateParam("sGirdleOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.Img))
                    para.Add(db.CreateParam("Img", DbType.String, ParameterDirection.Input, obj.Img));
                else
                    para.Add(db.CreateParam("Img", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.Vdo))
                    para.Add(db.CreateParam("Vdo", DbType.String, ParameterDirection.Input, obj.Vdo));
                else
                    para.Add(db.CreateParam("Vdo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.KTSBlank))
                    para.Add(db.CreateParam("KTSBlank", DbType.Boolean, ParameterDirection.Input, obj.KTSBlank));
                else
                    para.Add(db.CreateParam("KTSBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.LengthBlank))
                    para.Add(db.CreateParam("LengthBlank", DbType.Boolean, ParameterDirection.Input, obj.LengthBlank));
                else
                    para.Add(db.CreateParam("LengthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.WidthBlank))
                    para.Add(db.CreateParam("WidthBlank", DbType.Boolean, ParameterDirection.Input, obj.WidthBlank));
                else
                    para.Add(db.CreateParam("WidthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.DepthBlank))
                    para.Add(db.CreateParam("DepthBlank", DbType.Boolean, ParameterDirection.Input, obj.DepthBlank));
                else
                    para.Add(db.CreateParam("DepthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.DepthPerBlank))
                    para.Add(db.CreateParam("DepthPerBlank", DbType.Boolean, ParameterDirection.Input, obj.DepthPerBlank));
                else
                    para.Add(db.CreateParam("DepthPerBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.TablePerBlank))
                    para.Add(db.CreateParam("TablePerBlank", DbType.Boolean, ParameterDirection.Input, obj.TablePerBlank));
                else
                    para.Add(db.CreateParam("TablePerBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.CrAngBlank))
                    para.Add(db.CreateParam("CrAngBlank", DbType.Boolean, ParameterDirection.Input, obj.CrAngBlank));
                else
                    para.Add(db.CreateParam("CrAngBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.CrHtBlank))
                    para.Add(db.CreateParam("CrHtBlank", DbType.Boolean, ParameterDirection.Input, obj.CrHtBlank));
                else
                    para.Add(db.CreateParam("CrHtBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.PavAngBlank))
                    para.Add(db.CreateParam("PavAngBlank", DbType.Boolean, ParameterDirection.Input, obj.PavAngBlank));
                else
                    para.Add(db.CreateParam("PavAngBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.PavHtBlank))
                    para.Add(db.CreateParam("PavHtBlank", DbType.Boolean, ParameterDirection.Input, obj.PavHtBlank));
                else
                    para.Add(db.CreateParam("PavHtBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("GetLabSearch", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult LabSearchExcel([FromBody]JObject data)
        {
            Database db = new Database(Request);
            List<IDbDataParameter> para = new List<IDbDataParameter>();
            
            JObject test1 = JObject.Parse(data.ToString());
            LabSearchStockDownloadRequest obj = new LabSearchStockDownloadRequest();
            try
            {
                //obj = JsonConvert.DeserializeObject<LabSearchStockDownloadRequest>(data.ToString());
                obj = JsonConvert.DeserializeObject<LabSearchStockDownloadRequest>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                
                DataTable dtData = GetLabSearchExcel(obj);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    string filename = "";
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    if (obj.ExcelType == 1)
                    {
                        filename = "Customer " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                        EpExcelExport.CreateLabCustomerExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }
                    else if (obj.ExcelType == 2)
                    {
                        filename = "Customer " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                        EpExcelExport.CreateLabCustomerImageExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }
                    else if (obj.ExcelType == 3)
                    {
                        filename = "Supplier " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                        EpExcelExport.CreateLabSupplierExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }
                    else if (obj.ExcelType == 4)
                    {
                        filename = "Buyer List " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                        EpExcelExport.CreateMacroExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }


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

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult LabSearchExcel_MobileApp([FromBody]JObject data)
        {
            LabSearchStockDownloadRequest obj = new LabSearchStockDownloadRequest();
            try
            {
                obj = JsonConvert.DeserializeObject<LabSearchStockDownloadRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }
            
            Database db = new Database(Request);
            List<IDbDataParameter> para = new List<IDbDataParameter>();

            try
            {
                DataTable dtData = GetLabSearchExcel(obj);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    string filename = "";
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    if (obj.ExcelType == 1)
                    {
                        filename = "Customer " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                        EpExcelExport.CreateLabCustomerExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }
                    else if (obj.ExcelType == 2)
                    {
                        filename = "Customer " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                        EpExcelExport.CreateLabCustomerImageExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }
                    else if (obj.ExcelType == 3)
                    {
                        filename = "Supplier " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                        EpExcelExport.CreateLabSupplierExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }
                    else if (obj.ExcelType == 4)
                    {
                        filename = "Buyer List " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                        EpExcelExport.CreateMacroExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                    }

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

        #endregion

        #region Lab Search Grid

        [NonAction]
        private DataTable GetLabSearch_Grid(LabSearchStockDownloadRequest obj)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(obj.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, obj.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, obj.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.RefNo))
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, obj.RefNo.ToUpper()));
                else
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.iVendor))
                    para.Add(db.CreateParam("iVendor", DbType.String, ParameterDirection.Input, obj.iVendor));
                else
                    para.Add(db.CreateParam("iVendor", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sShape))
                    para.Add(db.CreateParam("sShape", DbType.String, ParameterDirection.Input, obj.sShape));
                else
                    para.Add(db.CreateParam("sShape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sPointer))
                    para.Add(db.CreateParam("sPointer", DbType.String, ParameterDirection.Input, obj.sPointer));
                else
                    para.Add(db.CreateParam("sPointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sColorType))
                    para.Add(db.CreateParam("sColorType", DbType.String, ParameterDirection.Input, obj.sColorType));
                else
                    para.Add(db.CreateParam("sColorType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sColor))
                    para.Add(db.CreateParam("sColor", DbType.String, ParameterDirection.Input, obj.sColor));
                else
                    para.Add(db.CreateParam("sColor", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sINTENSITY))
                    para.Add(db.CreateParam("sINTENSITY", DbType.String, ParameterDirection.Input, obj.sINTENSITY));
                else
                    para.Add(db.CreateParam("sINTENSITY", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sOVERTONE))
                    para.Add(db.CreateParam("sOVERTONE", DbType.String, ParameterDirection.Input, obj.sOVERTONE));
                else
                    para.Add(db.CreateParam("sOVERTONE", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sFANCY_COLOR))
                    para.Add(db.CreateParam("sFANCY_COLOR", DbType.String, ParameterDirection.Input, obj.sFANCY_COLOR));
                else
                    para.Add(db.CreateParam("sFANCY_COLOR", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sClarity))
                    para.Add(db.CreateParam("sClarity", DbType.String, ParameterDirection.Input, obj.sClarity));
                else
                    para.Add(db.CreateParam("sClarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sCut))
                    para.Add(db.CreateParam("sCut", DbType.String, ParameterDirection.Input, obj.sCut));
                else
                    para.Add(db.CreateParam("sCut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sPolish))
                    para.Add(db.CreateParam("sPolish", DbType.String, ParameterDirection.Input, obj.sPolish));
                else
                    para.Add(db.CreateParam("sPolish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sSymm))
                    para.Add(db.CreateParam("sSymm", DbType.String, ParameterDirection.Input, obj.sSymm));
                else
                    para.Add(db.CreateParam("sSymm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sFls))
                    para.Add(db.CreateParam("sFls", DbType.String, ParameterDirection.Input, obj.sFls));
                else
                    para.Add(db.CreateParam("sFls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sLab))
                    para.Add(db.CreateParam("sLab", DbType.String, ParameterDirection.Input, obj.sLab));
                else
                    para.Add(db.CreateParam("sLab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromDisc))
                    para.Add(db.CreateParam("dFromDisc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromDisc)));
                else
                    para.Add(db.CreateParam("dFromDisc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToDisc))
                    para.Add(db.CreateParam("dToDisc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToDisc)));
                else
                    para.Add(db.CreateParam("dToDisc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromTotAmt))
                    para.Add(db.CreateParam("dFromTotAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromTotAmt)));
                else
                    para.Add(db.CreateParam("dFromTotAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToTotAmt))
                    para.Add(db.CreateParam("dToTotAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToTotAmt)));
                else
                    para.Add(db.CreateParam("dToTotAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromLength))
                    para.Add(db.CreateParam("dFromLength", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromLength)));
                else
                    para.Add(db.CreateParam("dFromLength", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToLength))
                    para.Add(db.CreateParam("dToLength", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToLength)));
                else
                    para.Add(db.CreateParam("dToLength", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromWidth))
                    para.Add(db.CreateParam("dFromWidth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromWidth)));
                else
                    para.Add(db.CreateParam("dFromWidth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToWidth))
                    para.Add(db.CreateParam("dToWidth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToWidth)));
                else
                    para.Add(db.CreateParam("dToWidth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromDepth))
                    para.Add(db.CreateParam("dFromDepth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromDepth)));
                else
                    para.Add(db.CreateParam("dFromDepth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToDepth))
                    para.Add(db.CreateParam("dToDepth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToDepth)));
                else
                    para.Add(db.CreateParam("dToDepth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromDepthPer))
                    para.Add(db.CreateParam("dFromDepthPer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromDepthPer)));
                else
                    para.Add(db.CreateParam("dFromDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToDepthPer))
                    para.Add(db.CreateParam("dToDepthPer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToDepthPer)));
                else
                    para.Add(db.CreateParam("dToDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromTablePer))
                    para.Add(db.CreateParam("dFromTablePer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromTablePer)));
                else
                    para.Add(db.CreateParam("dFromTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToTablePer))
                    para.Add(db.CreateParam("dToTablePer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToTablePer)));
                else
                    para.Add(db.CreateParam("dToTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromCrAng))
                    para.Add(db.CreateParam("dFromCrAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromCrAng)));
                else
                    para.Add(db.CreateParam("dFromCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToCrAng))
                    para.Add(db.CreateParam("dToCrAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToCrAng)));
                else
                    para.Add(db.CreateParam("dToCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromCrHt))
                    para.Add(db.CreateParam("dFromCrHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromCrHt)));
                else
                    para.Add(db.CreateParam("dFromCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToCrHt))
                    para.Add(db.CreateParam("dToCrHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToCrHt)));
                else
                    para.Add(db.CreateParam("dToCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromPavAng))
                    para.Add(db.CreateParam("dFromPavAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromPavAng)));
                else
                    para.Add(db.CreateParam("dFromPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToPavAng))
                    para.Add(db.CreateParam("dToPavAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToPavAng)));
                else
                    para.Add(db.CreateParam("dToPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dFromPavHt))
                    para.Add(db.CreateParam("dFromPavHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dFromPavHt)));
                else
                    para.Add(db.CreateParam("dFromPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dToPavHt))
                    para.Add(db.CreateParam("dToPavHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(obj.dToPavHt)));
                else
                    para.Add(db.CreateParam("dToPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dKeytosymbol))
                    para.Add(db.CreateParam("dKeytosymbol", DbType.String, ParameterDirection.Input, obj.dKeytosymbol));
                else
                    para.Add(db.CreateParam("dKeytosymbol", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dCheckKTS))
                    para.Add(db.CreateParam("dCheckKTS", DbType.String, ParameterDirection.Input, obj.dCheckKTS));
                else
                    para.Add(db.CreateParam("dCheckKTS", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.dUNCheckKTS))
                    para.Add(db.CreateParam("dUNCheckKTS", DbType.String, ParameterDirection.Input, obj.dUNCheckKTS));
                else
                    para.Add(db.CreateParam("dUNCheckKTS", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sBGM))
                    para.Add(db.CreateParam("sBGM", DbType.String, ParameterDirection.Input, obj.sBGM));
                else
                    para.Add(db.CreateParam("sBGM", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sCrownBlack))
                    para.Add(db.CreateParam("sCrownBlack", DbType.String, ParameterDirection.Input, obj.sCrownBlack));
                else
                    para.Add(db.CreateParam("sCrownBlack", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sTableBlack))
                    para.Add(db.CreateParam("sTableBlack", DbType.String, ParameterDirection.Input, obj.sTableBlack));
                else
                    para.Add(db.CreateParam("sTableBlack", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sCrownWhite))
                    para.Add(db.CreateParam("sCrownWhite", DbType.String, ParameterDirection.Input, obj.sCrownWhite));
                else
                    para.Add(db.CreateParam("sCrownWhite", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sTableWhite))
                    para.Add(db.CreateParam("sTableWhite", DbType.String, ParameterDirection.Input, obj.sTableWhite));
                else
                    para.Add(db.CreateParam("sTableWhite", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sTableOpen))
                    para.Add(db.CreateParam("sTableOpen", DbType.String, ParameterDirection.Input, obj.sTableOpen));
                else
                    para.Add(db.CreateParam("sTableOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sCrownOpen))
                    para.Add(db.CreateParam("sCrownOpen", DbType.String, ParameterDirection.Input, obj.sCrownOpen));
                else
                    para.Add(db.CreateParam("sCrownOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sPavOpen))
                    para.Add(db.CreateParam("sPavOpen", DbType.String, ParameterDirection.Input, obj.sPavOpen));
                else
                    para.Add(db.CreateParam("sPavOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.sGirdleOpen))
                    para.Add(db.CreateParam("sGirdleOpen", DbType.String, ParameterDirection.Input, obj.sGirdleOpen));
                else
                    para.Add(db.CreateParam("sGirdleOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.Img))
                    para.Add(db.CreateParam("Img", DbType.String, ParameterDirection.Input, obj.Img));
                else
                    para.Add(db.CreateParam("Img", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.Vdo))
                    para.Add(db.CreateParam("Vdo", DbType.String, ParameterDirection.Input, obj.Vdo));
                else
                    para.Add(db.CreateParam("Vdo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.KTSBlank))
                    para.Add(db.CreateParam("KTSBlank", DbType.Boolean, ParameterDirection.Input, obj.KTSBlank));
                else
                    para.Add(db.CreateParam("KTSBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.LengthBlank))
                    para.Add(db.CreateParam("LengthBlank", DbType.Boolean, ParameterDirection.Input, obj.LengthBlank));
                else
                    para.Add(db.CreateParam("LengthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.WidthBlank))
                    para.Add(db.CreateParam("WidthBlank", DbType.Boolean, ParameterDirection.Input, obj.WidthBlank));
                else
                    para.Add(db.CreateParam("WidthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.DepthBlank))
                    para.Add(db.CreateParam("DepthBlank", DbType.Boolean, ParameterDirection.Input, obj.DepthBlank));
                else
                    para.Add(db.CreateParam("DepthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.DepthPerBlank))
                    para.Add(db.CreateParam("DepthPerBlank", DbType.Boolean, ParameterDirection.Input, obj.DepthPerBlank));
                else
                    para.Add(db.CreateParam("DepthPerBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.TablePerBlank))
                    para.Add(db.CreateParam("TablePerBlank", DbType.Boolean, ParameterDirection.Input, obj.TablePerBlank));
                else
                    para.Add(db.CreateParam("TablePerBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.CrAngBlank))
                    para.Add(db.CreateParam("CrAngBlank", DbType.Boolean, ParameterDirection.Input, obj.CrAngBlank));
                else
                    para.Add(db.CreateParam("CrAngBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.CrHtBlank))
                    para.Add(db.CreateParam("CrHtBlank", DbType.Boolean, ParameterDirection.Input, obj.CrHtBlank));
                else
                    para.Add(db.CreateParam("CrHtBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.PavAngBlank))
                    para.Add(db.CreateParam("PavAngBlank", DbType.Boolean, ParameterDirection.Input, obj.PavAngBlank));
                else
                    para.Add(db.CreateParam("PavAngBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.PavHtBlank))
                    para.Add(db.CreateParam("PavHtBlank", DbType.Boolean, ParameterDirection.Input, obj.PavHtBlank));
                else
                    para.Add(db.CreateParam("PavHtBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (obj.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, obj.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, 0));

                if (obj.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, obj.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, obj.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("GetLabSearch_Grid", para.ToArray(), false);
                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult LabSearchGrid([FromBody]JObject data)
        {
            Database db = new Database(Request);
            List<IDbDataParameter> para = new List<IDbDataParameter>();

            JObject test1 = JObject.Parse(data.ToString());
            LabSearchStockDownloadRequest obj = new LabSearchStockDownloadRequest();
            try
            {
                obj = JsonConvert.DeserializeObject<LabSearchStockDownloadRequest>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());

                DataTable dtData = GetLabSearch_Grid(obj);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataRow[] dra = dtData.Select("ID = 0");
                    LabSummary labsummary = new LabSummary();
                    if (dra.Length > 0)
                    {
                        labsummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                        labsummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                        labsummary.TOT_PCS = Convert.ToInt32(dra[0]["REF_NO"]);
                        labsummary.TOT_CTS = Convert.ToDouble(dra[0]["CTS"]);
                        labsummary.TOT_RAP_AMOUNT = Convert.ToDouble(dra[0]["RAP_VALUE"].ToString() != "" && dra[0]["RAP_VALUE"].ToString() != null ? dra[0]["RAP_VALUE"] : "0");
                        labsummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["SALES_DISC_VALUE"]);

                        labsummary.AVG_OFFER_DISC_PER = Convert.ToDouble(dra[0]["OFFER_DISC_PER"].ToString() != "" && dra[0]["OFFER_DISC_PER"].ToString() != null ? dra[0]["OFFER_DISC_PER"] : "0");
                        labsummary.TOT_OFFER_DISC_VALUE = Convert.ToDouble(dra[0]["OFFER_DISC_VALUE"]);
                        labsummary.AVG_SUPP_BASE_OFFER_PER = Convert.ToDouble(dra[0]["SUPP_BASE_OFFER_PER"].ToString() != "" && dra[0]["SUPP_BASE_OFFER_PER"].ToString() != null ? dra[0]["SUPP_BASE_OFFER_PER"] : "0");
                        labsummary.TOT_SUPP_BASE_VALUE = Convert.ToDouble(dra[0]["SUPP_BASE_VALUE"]);

                        labsummary.AVG_SALES_DISC_PER = Convert.ToDouble(dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0");
                    }

                    dtData.DefaultView.RowFilter = "ID <> 0";
                    dtData = dtData.DefaultView.ToTable();

                    List<LabSearchStockGridResponseInner> inner = new List<LabSearchStockGridResponseInner>();
                    inner = DataTableExtension.ToList<LabSearchStockGridResponseInner>(dtData);
                    List<LabSearchStockGridResponse> labsearchstockdownloadresponse = new List<LabSearchStockGridResponse>();

                    if (inner.Count > 0)
                    {
                        labsearchstockdownloadresponse.Add(new LabSearchStockGridResponse()
                        {
                            DataList = inner,
                            DataSummary = labsummary
                        });

                        return Ok(new ServiceResponse<LabSearchStockGridResponse>
                        {
                            Data = labsearchstockdownloadresponse,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<LabSearchStockGridResponse>
                        {
                            Data = labsearchstockdownloadresponse,
                            Message = "No Data Found",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<LabSearchStockGridResponse>
                    {
                        Data = new List<LabSearchStockGridResponse>(),
                        Message = "No Data Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<LabSearchStockGridResponse>
                {
                    Data = new List<LabSearchStockGridResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult LabSearchGrid_MobileApp([FromBody]JObject data)
        {
            Database db = new Database(Request);
            List<IDbDataParameter> para = new List<IDbDataParameter>();

            LabSearchStockDownloadRequest obj = new LabSearchStockDownloadRequest();
            try
            {
                obj = JsonConvert.DeserializeObject<LabSearchStockDownloadRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                DataTable dtData = GetLabSearch_Grid(obj);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataRow[] dra = dtData.Select("ID = 0");
                    LabSummary labsummary = new LabSummary();
                    if (dra.Length > 0)
                    {
                        labsummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                        labsummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                        labsummary.TOT_PCS = Convert.ToInt32(dra[0]["REF_NO"]);
                        labsummary.TOT_CTS = Convert.ToDouble(dra[0]["CTS"]);
                        labsummary.TOT_RAP_AMOUNT = Convert.ToDouble(dra[0]["RAP_VALUE"].ToString() != "" && dra[0]["RAP_VALUE"].ToString() != null ? dra[0]["RAP_VALUE"] : "0");
                        labsummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["SALES_DISC_VALUE"]);

                        labsummary.AVG_OFFER_DISC_PER = Convert.ToDouble(dra[0]["OFFER_DISC_PER"].ToString() != "" && dra[0]["OFFER_DISC_PER"].ToString() != null ? dra[0]["OFFER_DISC_PER"] : "0");
                        labsummary.TOT_OFFER_DISC_VALUE = Convert.ToDouble(dra[0]["OFFER_DISC_VALUE"]);
                        labsummary.AVG_SUPP_BASE_OFFER_PER = Convert.ToDouble(dra[0]["SUPP_BASE_OFFER_PER"].ToString() != "" && dra[0]["SUPP_BASE_OFFER_PER"].ToString() != null ? dra[0]["SUPP_BASE_OFFER_PER"] : "0");
                        labsummary.TOT_SUPP_BASE_VALUE = Convert.ToDouble(dra[0]["SUPP_BASE_VALUE"]);

                        labsummary.AVG_SALES_DISC_PER = Convert.ToDouble(dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0");
                    }

                    dtData.DefaultView.RowFilter = "ID <> 0";
                    dtData = dtData.DefaultView.ToTable();

                    List<LabSearchStockGridResponseInner> inner = new List<LabSearchStockGridResponseInner>();
                    inner = DataTableExtension.ToList<LabSearchStockGridResponseInner>(dtData);
                    List<LabSearchStockGridResponse> labsearchstockdownloadresponse = new List<LabSearchStockGridResponse>();

                    if (inner.Count > 0)
                    {
                        labsearchstockdownloadresponse.Add(new LabSearchStockGridResponse()
                        {
                            DataList = inner,
                            DataSummary = labsummary
                        });

                        return Ok(new ServiceResponse<LabSearchStockGridResponse>
                        {
                            Data = labsearchstockdownloadresponse,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<LabSearchStockGridResponse>
                        {
                            Data = labsearchstockdownloadresponse,
                            Message = "No Data Found",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<LabSearchStockGridResponse>
                    {
                        Data = new List<LabSearchStockGridResponse>(),
                        Message = "No Data Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<LabSearchStockGridResponse>
                {
                    Data = new List<LabSearchStockGridResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        #endregion

        #region Lab Search Excel

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult LabSearchGridExcel([FromBody]JObject data)
        {
            Database db = new Database(Request);
            List<IDbDataParameter> para = new List<IDbDataParameter>();

            JObject test1 = JObject.Parse(data.ToString());
            LabSearchStockDownloadRequest obj = new LabSearchStockDownloadRequest();
            try
            {
                obj = JsonConvert.DeserializeObject<LabSearchStockDownloadRequest>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());

                obj.PgSize = 10000000;
                DataTable dtData = GetLabSearch_Grid(obj);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    //DataRow[] dra = dtData.Select("ID = 0");
                    //LabSummary labsummary = new LabSummary();
                    //if (dra.Length > 0)
                    //{
                    //    labsummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                    //    labsummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                    //    labsummary.TOT_PCS = Convert.ToInt32(dra[0]["REF_NO"]);
                    //    labsummary.TOT_CTS = Convert.ToDouble(dra[0]["CTS"]);
                    //    labsummary.TOT_RAP_AMOUNT = Convert.ToDouble(dra[0]["RAP_VALUE"].ToString() != "" && dra[0]["RAP_VALUE"].ToString() != null ? dra[0]["RAP_VALUE"] : "0");
                    //    labsummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["SALES_DISC_VALUE"]);

                    //    labsummary.AVG_OFFER_DISC_PER = Convert.ToDouble(dra[0]["OFFER_DISC_PER"].ToString() != "" && dra[0]["OFFER_DISC_PER"].ToString() != null ? dra[0]["OFFER_DISC_PER"] : "0");
                    //    labsummary.TOT_OFFER_DISC_VALUE = Convert.ToDouble(dra[0]["OFFER_DISC_VALUE"]);
                    //    labsummary.AVG_SUPP_BASE_OFFER_PER = Convert.ToDouble(dra[0]["SUPP_BASE_OFFER_PER"].ToString() != "" && dra[0]["SUPP_BASE_OFFER_PER"].ToString() != null ? dra[0]["SUPP_BASE_OFFER_PER"] : "0");
                    //    labsummary.TOT_SUPP_BASE_VALUE = Convert.ToDouble(dra[0]["SUPP_BASE_VALUE"]);

                    //    labsummary.AVG_SALES_DISC_PER = Convert.ToDouble(dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0");
                    //}
                dtData.DefaultView.RowFilter = "ID <> 0";
                    dtData = dtData.DefaultView.ToTable();

                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        string filename = "";
                        string _path = ConfigurationManager.AppSettings["data"];
                        string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                        string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                        if (obj.ExcelType == 2)
                        {
                            filename = "Customer " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                            EpExcelExport.CreateLabCustomerImageExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                        }
                        else if (obj.ExcelType == 3)
                        {
                            filename = "Supplier " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");
                            EpExcelExport.CreateLabSupplierExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);
                        }
                        string _strxml = _path + filename + ".xlsx";
                        return Ok(_strxml);
                    }
                    else
                    {
                        return Ok("No data found.");
                    }
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

        #endregion

        [HttpPost]
        public IHttpActionResult ByRequest_CRUD([FromBody]JObject data)
        {
            ByRequestReq ByRequestReq = new ByRequestReq();
            try
            {
                ByRequestReq = JsonConvert.DeserializeObject<ByRequestReq>(data.ToString());
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
                ByRequestReq.UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                var db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("REF_NO", DbType.String, ParameterDirection.Input, ByRequestReq.REF_NO));
                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, ByRequestReq.UserId));
                para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, ByRequestReq.Type));
                
                DataTable dt = db.ExecuteSP("Lab_ByRequest_CRUD", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = dt.Rows[0]["Message"].ToString(),
                        Status = dt.Rows[0]["Status"].ToString()
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
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult ByRequest_Cart([FromBody]JObject data)
        {
            ByRequestReq ByRequestReq = new ByRequestReq();
            try
            {
                ByRequestReq = JsonConvert.DeserializeObject<ByRequestReq>(data.ToString());
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
                ByRequestReq.UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                var db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("REF_NO", DbType.String, ParameterDirection.Input, ByRequestReq.REF_NO));
                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, ByRequestReq.UserId));

                DataTable dt = db.ExecuteSP("Lab_ByRequest_Cart_CRUD", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = dt.Rows[0]["Message"].ToString(),
                        Status = dt.Rows[0]["Status"].ToString()
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
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [NonAction]
        private DataTable ByRequestCartGet(ByRequestReq obj)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(obj.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.Date, ParameterDirection.Input, obj.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.Date, ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(obj.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.Date, ParameterDirection.Input, obj.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.Date, ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(obj.REF_NO))
                    para.Add(db.CreateParam("REF_NO", DbType.String, ParameterDirection.Input, obj.REF_NO.ToUpper()));
                else
                    para.Add(db.CreateParam("REF_NO", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.EntryBy))
                    para.Add(db.CreateParam("EntryBy", DbType.String, ParameterDirection.Input, obj.EntryBy));
                else
                    para.Add(db.CreateParam("EntryBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (obj.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, obj.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, 0));

                if (obj.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, obj.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, obj.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, obj.UserId));

                DataTable dt = db.ExecuteSP("GetLab_ByRequest_Cart", para.ToArray(), false);
                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        
        [HttpPost]
        public IHttpActionResult LabByRequestCartGet([FromBody]JObject data)
        {
            ByRequestReq ByRequestReq = new ByRequestReq();
            try
            {
                ByRequestReq = JsonConvert.DeserializeObject<ByRequestReq>(data.ToString());
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
                ByRequestReq.UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                DataTable dtData = ByRequestCartGet(ByRequestReq);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataRow[] dra = dtData.Select("ID = 0");
                    LabSummary labsummary = new LabSummary();
                    if (dra.Length > 0)
                    {
                        labsummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                        labsummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                        labsummary.TOT_PCS = Convert.ToInt32(dra[0]["REF_NO"]);
                        labsummary.TOT_CTS = Convert.ToDouble(dra[0]["CTS"]);
                        labsummary.TOT_RAP_AMOUNT = Convert.ToDouble(dra[0]["RAP_VALUE"].ToString() != "" && dra[0]["RAP_VALUE"].ToString() != null ? dra[0]["RAP_VALUE"] : "0");
                        labsummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["SALES_DISC_VALUE"]);

                        labsummary.AVG_OFFER_DISC_PER = Convert.ToDouble(dra[0]["OFFER_DISC_PER"].ToString() != "" && dra[0]["OFFER_DISC_PER"].ToString() != null ? dra[0]["OFFER_DISC_PER"] : "0");
                        labsummary.TOT_OFFER_DISC_VALUE = Convert.ToDouble(dra[0]["OFFER_DISC_VALUE"]);
                        labsummary.AVG_SUPP_BASE_OFFER_PER = Convert.ToDouble(dra[0]["SUPP_BASE_OFFER_PER"].ToString() != "" && dra[0]["SUPP_BASE_OFFER_PER"].ToString() != null ? dra[0]["SUPP_BASE_OFFER_PER"] : "0");
                        labsummary.TOT_SUPP_BASE_VALUE = Convert.ToDouble(dra[0]["SUPP_BASE_VALUE"]);

                        labsummary.AVG_SALES_DISC_PER = Convert.ToDouble(dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0");
                    }

                    dtData.DefaultView.RowFilter = "ID <> 0";
                    dtData = dtData.DefaultView.ToTable();

                    List<LabSearchStockGridResponseInner> inner = new List<LabSearchStockGridResponseInner>();
                    inner = DataTableExtension.ToList<LabSearchStockGridResponseInner>(dtData);
                    List<LabSearchStockGridResponse> labsearchstockdownloadresponse = new List<LabSearchStockGridResponse>();

                    if (inner.Count > 0)
                    {
                        labsearchstockdownloadresponse.Add(new LabSearchStockGridResponse()
                        {
                            DataList = inner,
                            DataSummary = labsummary
                        });

                        return Ok(new ServiceResponse<LabSearchStockGridResponse>
                        {
                            Data = labsearchstockdownloadresponse,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<LabSearchStockGridResponse>
                        {
                            Data = labsearchstockdownloadresponse,
                            Message = "No Data Found",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<LabSearchStockGridResponse>
                    {
                        Data = new List<LabSearchStockGridResponse>(),
                        Message = "No Data Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<LabSearchStockGridResponse>
                {
                    Data = new List<LabSearchStockGridResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [NonAction]
        private DataTable ByRequestGet(ByRequestReq obj)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(obj.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.Date, ParameterDirection.Input, obj.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.Date, ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(obj.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.Date, ParameterDirection.Input, obj.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.Date, ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(obj.REF_NO))
                    para.Add(db.CreateParam("REF_NO", DbType.String, ParameterDirection.Input, obj.REF_NO.ToUpper()));
                else
                    para.Add(db.CreateParam("REF_NO", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.EntryBy))
                    para.Add(db.CreateParam("EntryBy", DbType.String, ParameterDirection.Input, obj.EntryBy));
                else
                    para.Add(db.CreateParam("EntryBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (obj.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, obj.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, 0));

                if (obj.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, obj.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(obj.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, obj.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, obj.UserId));
                para.Add(db.CreateParam("Pending", DbType.Boolean, ParameterDirection.Input, obj.Pending));
                para.Add(db.CreateParam("Approve", DbType.Boolean, ParameterDirection.Input, obj.Approve));
                para.Add(db.CreateParam("Reject", DbType.Boolean, ParameterDirection.Input, obj.Reject));
                para.Add(db.CreateParam("Supp_Status", DbType.Boolean, ParameterDirection.Input, obj.Supp_Status));

                DataTable dt = db.ExecuteSP("GetLab_ByRequest", para.ToArray(), false);
                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        [HttpPost]
        public IHttpActionResult LabByRequestGet([FromBody]JObject data)
        {
            ByRequestReq ByRequestReq = new ByRequestReq();
            try
            {
                ByRequestReq = JsonConvert.DeserializeObject<ByRequestReq>(data.ToString());
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
                ByRequestReq.UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                DataTable dtData = ByRequestGet(ByRequestReq);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataRow[] dra = dtData.Select("ID = 0");
                    LabSummary labsummary = new LabSummary();
                    if (dra.Length > 0)
                    {
                        labsummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                        labsummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                        labsummary.TOT_PCS = Convert.ToInt32(dra[0]["REF_NO"]);
                        labsummary.TOT_CTS = Convert.ToDouble(dra[0]["CTS"]);
                        labsummary.TOT_RAP_AMOUNT = Convert.ToDouble(dra[0]["RAP_VALUE"].ToString() != "" && dra[0]["RAP_VALUE"].ToString() != null ? dra[0]["RAP_VALUE"] : "0");
                        labsummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["SALES_DISC_VALUE"]);

                        labsummary.AVG_OFFER_DISC_PER = Convert.ToDouble(dra[0]["OFFER_DISC_PER"].ToString() != "" && dra[0]["OFFER_DISC_PER"].ToString() != null ? dra[0]["OFFER_DISC_PER"] : "0");
                        labsummary.TOT_OFFER_DISC_VALUE = Convert.ToDouble(dra[0]["OFFER_DISC_VALUE"]);
                        labsummary.AVG_SUPP_BASE_OFFER_PER = Convert.ToDouble(dra[0]["SUPP_BASE_OFFER_PER"].ToString() != "" && dra[0]["SUPP_BASE_OFFER_PER"].ToString() != null ? dra[0]["SUPP_BASE_OFFER_PER"] : "0");
                        labsummary.TOT_SUPP_BASE_VALUE = Convert.ToDouble(dra[0]["SUPP_BASE_VALUE"]);

                        labsummary.AVG_SALES_DISC_PER = Convert.ToDouble(dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0");
                    }

                    dtData.DefaultView.RowFilter = "ID <> 0";
                    dtData = dtData.DefaultView.ToTable();

                    List<LabSearchStockGridResponseInner> inner = new List<LabSearchStockGridResponseInner>();
                    inner = DataTableExtension.ToList<LabSearchStockGridResponseInner>(dtData);
                    List<LabSearchStockGridResponse> labsearchstockdownloadresponse = new List<LabSearchStockGridResponse>();

                    if (inner.Count > 0)
                    {
                        labsearchstockdownloadresponse.Add(new LabSearchStockGridResponse()
                        {
                            DataList = inner,
                            DataSummary = labsummary
                        });

                        return Ok(new ServiceResponse<LabSearchStockGridResponse>
                        {
                            Data = labsearchstockdownloadresponse,
                            Message = "SUCCESS",
                            Status = "1"
                        });
                    }
                    else
                    {
                        return Ok(new ServiceResponse<LabSearchStockGridResponse>
                        {
                            Data = labsearchstockdownloadresponse,
                            Message = "No Data Found",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<LabSearchStockGridResponse>
                    {
                        Data = new List<LabSearchStockGridResponse>(),
                        Message = "No Data Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<LabSearchStockGridResponse>
                {
                    Data = new List<LabSearchStockGridResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult ByRequest_ApproveReject([FromBody]JObject data)
        {
            ByRequestReq ByRequestReq = new ByRequestReq();
            try
            {
                ByRequestReq = JsonConvert.DeserializeObject<ByRequestReq>(data.ToString());
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

                if (Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value) == 41)
                {
                    var db = new Database();
                    List<IDbDataParameter> para = new List<IDbDataParameter>();

                    para.Add(db.CreateParam("REF_NO", DbType.String, ParameterDirection.Input, ByRequestReq.REF_NO));
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, ByRequestReq.Type));

                    DataTable dt = db.ExecuteSP("Lab_ByRequest_ApproveReject", para.ToArray(), false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = dt.Rows[0]["Message"].ToString(),
                            Status = dt.Rows[0]["Status"].ToString()
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
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = "You are not Authorized Person to Approve or Reject Buy Request",
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
        public IHttpActionResult ByRequest_Apply_Disc([FromBody]JObject data)
        {
            ByRequestReq ByRequestReq = new ByRequestReq();
            try
            {
                ByRequestReq = JsonConvert.DeserializeObject<ByRequestReq>(data.ToString());
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
                int userid = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                
                if (userid == 2003)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SupplierStatus", typeof(string));
                    dt.Columns.Add("CostDisc", typeof(string));
                    dt.Columns.Add("CostValue", typeof(string));
                    dt.Columns.Add("CostPriceCts", typeof(string));
                    dt.Columns.Add("REF_NO_Userid", typeof(string));
                    dt.Columns.Add("REF_NO", typeof(string));
                    dt.Columns.Add("Userid", typeof(string));

                    for (int i = 0; i < ByRequestReq.CostCharges.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["SupplierStatus"] = ByRequestReq.CostCharges[i].SupplierStatus.ToString();
                        dr["CostDisc"] = ByRequestReq.CostCharges[i].CostDisc.ToString();
                        dr["CostValue"] = ByRequestReq.CostCharges[i].CostValue.ToString();
                        dr["CostPriceCts"] = ByRequestReq.CostCharges[i].CostPriceCts.ToString();
                        dr["REF_NO_Userid"] = ByRequestReq.CostCharges[i].REF_NO_Userid.ToString();
                        dr["REF_NO"] = ByRequestReq.CostCharges[i].REF_NO.ToString();
                        dr["Userid"] = userid.ToString();

                        dt.Rows.Add(dr);
                    }

                    var db = new Database();
                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    DataTable dt1 = db.ExecuteSP("Lab_ByRequest_Apply_Disc", para.ToArray(), false);

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = dt1.Rows[0]["Message"].ToString(),
                            Status = dt1.Rows[0]["Status"].ToString()
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
                else
                {
                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = "You are not Authorized Person to Apply Disc(%)",
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
    }
}
