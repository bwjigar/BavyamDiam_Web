using DAL;
using EpExcelExportLib;
using ExcelExportLib;
using Lib.Constants;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Sunrise.Services.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Oracle.DataAccess.Client;
using System.Data.SqlClient;
using System.Text;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Stock")]
    public class StockController : ApiController
    {
        DataTableExcelExport ge;
        DataTableEpExcelExport ep_ge;
        UInt32 DiscNormalStyleindex;
        UInt32 CutNormalStyleindex;
        UInt32 InscStyleindex;

        [HttpPost]
        public IHttpActionResult DownloadStockExcel([FromBody] JObject data)
        {
            StockDownloadRequest stockDownloadRequest = new StockDownloadRequest();

            try
            {
                stockDownloadRequest = JsonConvert.DeserializeObject<StockDownloadRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userID)));
                DataTable dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);
                string iUserType = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    iUserType = dt.Rows[0]["iUserType"].ToString();
                }


                if (stockDownloadRequest.Luster == null)
                {
                    stockDownloadRequest.Luster = "";
                }
                if (stockDownloadRequest.Location == null)
                {
                    stockDownloadRequest.Location = "";
                }

                DataSet ds = new DataSet();

                DataTable dtSumm = new DataTable();

                dtSumm.Columns.Add("TOT_PAGE", typeof(Int32));
                dtSumm.Columns.Add("PAGE_SIZE", typeof(Int32));
                dtSumm.Columns.Add("TOT_PCS", typeof(Int32));
                dtSumm.Columns.Add("TOT_CTS", typeof(Decimal));
                dtSumm.Columns.Add("TOT_RAP_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("TOT_NET_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("AVG_PRICE_PER_CTS", typeof(Decimal));
                dtSumm.Columns.Add("AVG_SALES_DISC_PER", typeof(Decimal));

                if (String.IsNullOrEmpty(stockDownloadRequest.CrownInclusion))
                    stockDownloadRequest.CrownInclusion = "";
                if (String.IsNullOrEmpty(stockDownloadRequest.CrownNatts))
                    stockDownloadRequest.CrownNatts = "";
                string _struser = "";
                try
                {
                    _struser = Convert.ToString(userID);
                }
                catch
                {
                    _struser = "";
                }

                DataTable dtData = SearchDiamondsFullStock("Y", stockDownloadRequest.StoneID, stockDownloadRequest.CertiNo, stockDownloadRequest.Shape, stockDownloadRequest.Pointer, stockDownloadRequest.Color, stockDownloadRequest.Clarity, stockDownloadRequest.Cut, stockDownloadRequest.Polish, stockDownloadRequest.Symm, stockDownloadRequest.Fls, stockDownloadRequest.Lab, stockDownloadRequest.Luster, stockDownloadRequest.Location, stockDownloadRequest.Inclusion, stockDownloadRequest.Natts, stockDownloadRequest.Shade, stockDownloadRequest.FromCts, stockDownloadRequest.ToCts, stockDownloadRequest.FormDisc, stockDownloadRequest.ToDisc,
                stockDownloadRequest.FormPricePerCts, stockDownloadRequest.ToPricePerCts, stockDownloadRequest.FormNetAmt, stockDownloadRequest.ToNetAmt, stockDownloadRequest.FormDepth, stockDownloadRequest.ToDepth, stockDownloadRequest.FormLength, stockDownloadRequest.ToLength,
                stockDownloadRequest.FormWidth, stockDownloadRequest.ToWidth, stockDownloadRequest.FormDepthPer, stockDownloadRequest.ToDepthPer, stockDownloadRequest.FormTablePer, stockDownloadRequest.ToTablePer, stockDownloadRequest.HasImage, stockDownloadRequest.HasHDMovie, stockDownloadRequest.IsPromotion, stockDownloadRequest.ShapeColorPurity, stockDownloadRequest.Reviseflg, stockDownloadRequest.CrownInclusion, stockDownloadRequest.CrownNatts, stockDownloadRequest.PageNo, _struser, stockDownloadRequest.TokenNo, stockDownloadRequest.StoneStatus, stockDownloadRequest.FromCrownAngle, stockDownloadRequest.ToCrownAngle, stockDownloadRequest.FromCrownHeight, stockDownloadRequest.ToCrownHeight, stockDownloadRequest.FromPavAngle, stockDownloadRequest.ToPavAngle, stockDownloadRequest.FromPavHeight, stockDownloadRequest.ToPavHeight, stockDownloadRequest.BGM, stockDownloadRequest.Black, stockDownloadRequest.SmartSearch, stockDownloadRequest.keytosymbol, "", "", stockDownloadRequest.FormName, stockDownloadRequest.ActivityType,
                stockDownloadRequest.ColorType, stockDownloadRequest.Intensity, stockDownloadRequest.Overtone, stockDownloadRequest.Fancy_Color, stockDownloadRequest.Table_Open, stockDownloadRequest.Crown_Open, stockDownloadRequest.Pav_Open, stockDownloadRequest.Girdle_Open, stockDownloadRequest.UsedFor, stockDownloadRequest.NewArrival);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataRow[] dra = dtData.Select("Sr IS NULL");
                    if (dra.Length > 0)
                    {
                        DataRow dr = dtSumm.NewRow();
                        dr["TOT_PAGE"] = dra[0]["TOTAL_PAGE"];
                        dr["PAGE_SIZE"] = dra[0]["PAGE_SIZE"];
                        dr["TOT_PCS"] = dra[0]["stone_ref_no"];
                        dr["TOT_CTS"] = dra[0]["CTS"];
                        dr["TOT_RAP_AMOUNT"] = Convert.ToDouble((dra[0]["RAP_AMOUNT"].ToString() != "" && dra[0]["RAP_AMOUNT"].ToString() != null ? dra[0]["RAP_AMOUNT"] : "0"));
                        dr["TOT_NET_AMOUNT"] = dra[0]["NET_AMOUNT"];
                        dr["AVG_PRICE_PER_CTS"] = dra[0]["PRICE_PER_CTS"];
                        dr["AVG_SALES_DISC_PER"] = Convert.ToDouble((dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0"));
                        dtSumm.Rows.Add(dr);
                    }

                    dtData.DefaultView.RowFilter = "Sr IS NOT NULL";
                    dtData = dtData.DefaultView.ToTable();
                    dtSumm.TableName = "SummaryTable";

                    string filename = "";
                    //if (string.IsNullOrEmpty(stockDownloadRequest.StoneID) && string.IsNullOrEmpty(stockDownloadRequest.CertiNo) && string.IsNullOrEmpty(stockDownloadRequest.Shape) && string.IsNullOrEmpty(stockDownloadRequest.Pointer) && string.IsNullOrEmpty(stockDownloadRequest.Color) && string.IsNullOrEmpty(stockDownloadRequest.Clarity) && string.IsNullOrEmpty(stockDownloadRequest.Cut) && string.IsNullOrEmpty(stockDownloadRequest.Polish) && string.IsNullOrEmpty(stockDownloadRequest.Symm) && string.IsNullOrEmpty(stockDownloadRequest.Fls) && string.IsNullOrEmpty(stockDownloadRequest.Lab) && string.IsNullOrEmpty(stockDownloadRequest.Luster) && string.IsNullOrEmpty(stockDownloadRequest.Location) && string.IsNullOrEmpty(stockDownloadRequest.Inclusion) && string.IsNullOrEmpty(stockDownloadRequest.Natts) && string.IsNullOrEmpty(stockDownloadRequest.Shade) && string.IsNullOrEmpty(stockDownloadRequest.FromCts) && string.IsNullOrEmpty(stockDownloadRequest.ToCts) && string.IsNullOrEmpty(stockDownloadRequest.FormDisc) && string.IsNullOrEmpty(stockDownloadRequest.ToDisc) && string.IsNullOrEmpty(stockDownloadRequest.FormPricePerCts) && string.IsNullOrEmpty(stockDownloadRequest.ToPricePerCts) && string.IsNullOrEmpty(stockDownloadRequest.FormNetAmt) && string.IsNullOrEmpty(stockDownloadRequest.ToNetAmt) && string.IsNullOrEmpty(stockDownloadRequest.FormDepth) && string.IsNullOrEmpty(stockDownloadRequest.ToDepth) && string.IsNullOrEmpty(stockDownloadRequest.FormLength) && string.IsNullOrEmpty(stockDownloadRequest.ToLength) && string.IsNullOrEmpty(stockDownloadRequest.FormWidth) && string.IsNullOrEmpty(stockDownloadRequest.ToWidth) && string.IsNullOrEmpty(stockDownloadRequest.FormDepthPer) && string.IsNullOrEmpty(stockDownloadRequest.ToDepthPer) && string.IsNullOrEmpty(stockDownloadRequest.FormTablePer) && string.IsNullOrEmpty(stockDownloadRequest.ToTablePer) && string.IsNullOrEmpty(stockDownloadRequest.HasImage) && string.IsNullOrEmpty(stockDownloadRequest.HasHDMovie) && string.IsNullOrEmpty(stockDownloadRequest.IsPromotion) && string.IsNullOrEmpty("") && string.IsNullOrEmpty(stockDownloadRequest.Reviseflg) && string.IsNullOrEmpty(stockDownloadRequest.CrownInclusion) && string.IsNullOrEmpty(stockDownloadRequest.CrownNatts) && string.IsNullOrEmpty(stockDownloadRequest.TokenNo) && string.IsNullOrEmpty(stockDownloadRequest.StoneStatus) && string.IsNullOrEmpty(stockDownloadRequest.FromCrownAngle) && string.IsNullOrEmpty(stockDownloadRequest.ToCrownAngle) && string.IsNullOrEmpty(stockDownloadRequest.FromCrownHeight) && string.IsNullOrEmpty(stockDownloadRequest.ToCrownHeight) && string.IsNullOrEmpty(stockDownloadRequest.FromPavAngle) && string.IsNullOrEmpty(stockDownloadRequest.ToPavAngle) && string.IsNullOrEmpty(stockDownloadRequest.FromPavHeight) && string.IsNullOrEmpty(stockDownloadRequest.ToPavHeight) && string.IsNullOrEmpty(stockDownloadRequest.BGM) && string.IsNullOrEmpty(stockDownloadRequest.Black) && string.IsNullOrEmpty(stockDownloadRequest.SmartSearch) && string.IsNullOrEmpty(stockDownloadRequest.keytosymbol))
                    //{
                    //    filename = "Sunrise Diamonds Inventory " + DAL.Common.GetHKTime().ToString("dd.MM.yyyy");
                    //}
                    //else
                    //{
                    //    filename = "Sunrise Diamonds Selection " + DAL.Common.GetHKTime().ToString("dd.MM.yyyy");
                    //}

                    if (stockDownloadRequest.FormName == "New Arrival")
                        filename = "NewArrival " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    else if (stockDownloadRequest.FormName == "Revised Price")
                        filename = "RevisedPrice " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    else if (stockDownloadRequest.IsAll == 1)
                        filename = "Bhavyam Diam Inventory " + Lib.Models.Common.GetHKTime().ToString("dd.MM.yy");
                    else if (stockDownloadRequest.IsAll == 0)
                        filename = "Bhavyam Diam Selection " + Lib.Models.Common.GetHKTime().ToString("dd.MM.yy");

                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    //EpExcelExport.Excel_Generate(dtData.DefaultView.ToTable(), realpath + "Data" + random + ".xlsx");
                    EpExcelExport.CreateExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath, stockDownloadRequest.ColorType, iUserType);

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                {
                    return Ok("No Stock found as per filter criteria !");
                }
                //ds.Tables.Add(dtData);
                //ds.Tables.Add(dtSumm);                

                //return ds;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult DownloadStockExcelWhenStockUpload()
        {
            StockDownloadRequest stockDownloadRequest = new StockDownloadRequest();
            stockDownloadRequest.FormName = "Stock";
            stockDownloadRequest.FormName = "Excel Export";

            try
            {
                int userID = 1;

                if (stockDownloadRequest.Luster == null)
                {
                    stockDownloadRequest.Luster = "";
                }
                if (stockDownloadRequest.Location == null)
                {
                    stockDownloadRequest.Location = "";
                }

                DataSet ds = new DataSet();

                DataTable dtSumm = new DataTable();

                dtSumm.Columns.Add("TOT_PAGE", typeof(Int32));
                dtSumm.Columns.Add("PAGE_SIZE", typeof(Int32));
                dtSumm.Columns.Add("TOT_PCS", typeof(Int32));
                dtSumm.Columns.Add("TOT_CTS", typeof(Decimal));
                dtSumm.Columns.Add("TOT_RAP_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("TOT_NET_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("AVG_PRICE_PER_CTS", typeof(Decimal));
                dtSumm.Columns.Add("AVG_SALES_DISC_PER", typeof(Decimal));

                if (String.IsNullOrEmpty(stockDownloadRequest.CrownInclusion))
                    stockDownloadRequest.CrownInclusion = "";
                if (String.IsNullOrEmpty(stockDownloadRequest.CrownNatts))
                    stockDownloadRequest.CrownNatts = "";
                string _struser = "";
                try
                {
                    _struser = Convert.ToString(userID);
                }
                catch
                {
                    _struser = "";
                }

                DataTable dtData = SearchDiamondsFullStock("Y", stockDownloadRequest.StoneID, stockDownloadRequest.CertiNo, stockDownloadRequest.Shape, stockDownloadRequest.Pointer, stockDownloadRequest.Color, stockDownloadRequest.Clarity, stockDownloadRequest.Cut, stockDownloadRequest.Polish, stockDownloadRequest.Symm, stockDownloadRequest.Fls, stockDownloadRequest.Lab, stockDownloadRequest.Luster, stockDownloadRequest.Location, stockDownloadRequest.Inclusion, stockDownloadRequest.Natts, stockDownloadRequest.Shade, stockDownloadRequest.FromCts, stockDownloadRequest.ToCts, stockDownloadRequest.FormDisc, stockDownloadRequest.ToDisc,
                stockDownloadRequest.FormPricePerCts, stockDownloadRequest.ToPricePerCts, stockDownloadRequest.FormNetAmt, stockDownloadRequest.ToNetAmt, stockDownloadRequest.FormDepth, stockDownloadRequest.ToDepth, stockDownloadRequest.FormLength, stockDownloadRequest.ToLength,
                stockDownloadRequest.FormWidth, stockDownloadRequest.ToWidth, stockDownloadRequest.FormDepthPer, stockDownloadRequest.ToDepthPer, stockDownloadRequest.FormTablePer, stockDownloadRequest.ToTablePer, stockDownloadRequest.HasImage, stockDownloadRequest.HasHDMovie, stockDownloadRequest.IsPromotion, stockDownloadRequest.ShapeColorPurity, stockDownloadRequest.Reviseflg, stockDownloadRequest.CrownInclusion, stockDownloadRequest.CrownNatts, stockDownloadRequest.PageNo, _struser, stockDownloadRequest.TokenNo, stockDownloadRequest.StoneStatus, stockDownloadRequest.FromCrownAngle, stockDownloadRequest.ToCrownAngle, stockDownloadRequest.FromCrownHeight, stockDownloadRequest.ToCrownHeight, stockDownloadRequest.FromPavAngle, stockDownloadRequest.ToPavAngle, stockDownloadRequest.FromPavHeight, stockDownloadRequest.ToPavHeight, stockDownloadRequest.BGM, stockDownloadRequest.Black, stockDownloadRequest.SmartSearch, stockDownloadRequest.keytosymbol, "", "", stockDownloadRequest.FormName, stockDownloadRequest.ActivityType,
                stockDownloadRequest.ColorType, stockDownloadRequest.Intensity, stockDownloadRequest.Overtone, stockDownloadRequest.Fancy_Color, stockDownloadRequest.Table_Open, stockDownloadRequest.Crown_Open, stockDownloadRequest.Pav_Open, stockDownloadRequest.Girdle_Open, stockDownloadRequest.UsedFor, stockDownloadRequest.NewArrival);

                DataRow[] dra = dtData.Select("Sr IS NULL");
                if (dra.Length > 0)
                {
                    DataRow dr = dtSumm.NewRow();
                    dr["TOT_PAGE"] = dra[0]["TOTAL_PAGE"];
                    dr["PAGE_SIZE"] = dra[0]["PAGE_SIZE"];
                    dr["TOT_PCS"] = dra[0]["stone_ref_no"];
                    dr["TOT_CTS"] = dra[0]["CTS"];
                    dr["TOT_RAP_AMOUNT"] = Convert.ToDouble((dra[0]["RAP_AMOUNT"].ToString() != "" && dra[0]["RAP_AMOUNT"].ToString() != null ? dra[0]["RAP_AMOUNT"] : "0"));
                    dr["TOT_NET_AMOUNT"] = dra[0]["NET_AMOUNT"];
                    dr["AVG_PRICE_PER_CTS"] = dra[0]["PRICE_PER_CTS"];
                    dr["AVG_SALES_DISC_PER"] = Convert.ToDouble((dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0"));
                    dtSumm.Rows.Add(dr);
                }

                dtData.DefaultView.RowFilter = "Sr IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();
                dtSumm.TableName = "SummaryTable";

                string filename = "Bhavyam Diam Inventory " + Lib.Models.Common.GetHKTime().ToString("dd-MM-yyyy");

                string _path = ConfigurationManager.AppSettings["data"];
                string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                EpExcelExport.CreateExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath, stockDownloadRequest.ColorType, "");

                string _strxml = _path + filename + ".xlsx";
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = new DataTable();
                para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "Add"));
                para.Add(db.CreateParam("FullStockExcelPath", DbType.String, ParameterDirection.Input, _strxml));
                dt = db.ExecuteSP("FullStockExcel_GetSet", para.ToArray(), false);

                return Ok("");
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [HttpPost]
        public IHttpActionResult TotalStockDownload()
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = new DataTable();
                para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "Get"));
                dt = db.ExecuteSP("FullStockExcel_GetSet", para.ToArray(), false);
                if (dt != null)
                {
                    return Ok(dt.Rows[0]["FullStockExcelPath"].ToString());
                }
                else
                {
                    return Ok("");
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult DownloadStockExcelWithoutToken([FromBody] JObject data)
        {
            StockDownloadRequest stockDownloadRequest = new StockDownloadRequest();

            try
            {
                stockDownloadRequest = JsonConvert.DeserializeObject<StockDownloadRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                //int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                if (stockDownloadRequest.Luster == null)
                {
                    stockDownloadRequest.Luster = "";
                }
                if (stockDownloadRequest.Location == null)
                {
                    stockDownloadRequest.Location = "";
                }

                DataSet ds = new DataSet();

                DataTable dtSumm = new DataTable();

                dtSumm.Columns.Add("TOT_PAGE", typeof(Int32));
                dtSumm.Columns.Add("PAGE_SIZE", typeof(Int32));
                dtSumm.Columns.Add("TOT_PCS", typeof(Int32));
                dtSumm.Columns.Add("TOT_CTS", typeof(Decimal));
                dtSumm.Columns.Add("TOT_RAP_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("TOT_NET_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("AVG_PRICE_PER_CTS", typeof(Decimal));
                dtSumm.Columns.Add("AVG_SALES_DISC_PER", typeof(Decimal));

                if (String.IsNullOrEmpty(stockDownloadRequest.CrownInclusion))
                    stockDownloadRequest.CrownInclusion = "";
                if (String.IsNullOrEmpty(stockDownloadRequest.CrownNatts))
                    stockDownloadRequest.CrownNatts = "";
                string _struser = "";
                //try
                //{
                //    _struser = Convert.ToString(userID);
                //}
                //catch
                //{
                //    _struser = "";
                //}

                DataTable dtData = SearchDiamondsFullStock("Y", stockDownloadRequest.StoneID, stockDownloadRequest.CertiNo, 
                    stockDownloadRequest.Shape, stockDownloadRequest.Pointer, stockDownloadRequest.Color, stockDownloadRequest.Clarity, 
                    stockDownloadRequest.Cut, stockDownloadRequest.Polish, stockDownloadRequest.Symm, stockDownloadRequest.Fls, 
                    stockDownloadRequest.Lab, stockDownloadRequest.Luster, stockDownloadRequest.Location, stockDownloadRequest.Inclusion, 
                    stockDownloadRequest.Natts, stockDownloadRequest.Shade, stockDownloadRequest.FromCts, stockDownloadRequest.ToCts, 
                    stockDownloadRequest.FormDisc, stockDownloadRequest.ToDisc, stockDownloadRequest.FormPricePerCts, 
                    stockDownloadRequest.ToPricePerCts, stockDownloadRequest.FormNetAmt, stockDownloadRequest.ToNetAmt, 
                    stockDownloadRequest.FormDepth, stockDownloadRequest.ToDepth, stockDownloadRequest.FormLength, stockDownloadRequest.ToLength,
                    stockDownloadRequest.FormWidth, stockDownloadRequest.ToWidth, stockDownloadRequest.FormDepthPer, stockDownloadRequest.ToDepthPer,
                    stockDownloadRequest.FormTablePer, stockDownloadRequest.ToTablePer, stockDownloadRequest.HasImage, 
                    stockDownloadRequest.HasHDMovie, stockDownloadRequest.IsPromotion, "", stockDownloadRequest.Reviseflg, 
                    stockDownloadRequest.CrownInclusion, stockDownloadRequest.CrownNatts, stockDownloadRequest.PageNo, _struser, 
                    stockDownloadRequest.TokenNo, stockDownloadRequest.StoneStatus, stockDownloadRequest.FromCrownAngle, 
                    stockDownloadRequest.ToCrownAngle, stockDownloadRequest.FromCrownHeight, stockDownloadRequest.ToCrownHeight, 
                    stockDownloadRequest.FromPavAngle, stockDownloadRequest.ToPavAngle, stockDownloadRequest.FromPavHeight, 
                    stockDownloadRequest.ToPavHeight, stockDownloadRequest.BGM, stockDownloadRequest.Black, stockDownloadRequest.SmartSearch, 
                    stockDownloadRequest.keytosymbol, "", "", stockDownloadRequest.ColorType, stockDownloadRequest.Intensity, 
                    stockDownloadRequest.Overtone, stockDownloadRequest.Fancy_Color, stockDownloadRequest.Table_Open, 
                    stockDownloadRequest.Crown_Open, stockDownloadRequest.Pav_Open, stockDownloadRequest.Girdle_Open, "", 
                    stockDownloadRequest.NewArrival);

                DataRow[] dra = dtData.Select("P_SEQ_NO IS NULL");
                if (dra.Length > 0)
                {
                    DataRow dr = dtSumm.NewRow();
                    dr["TOT_PAGE"] = dra[0]["TOTAL_PAGE"];
                    dr["PAGE_SIZE"] = dra[0]["PAGE_SIZE"];
                    dr["TOT_PCS"] = dra[0]["stone_ref_no"];
                    dr["TOT_CTS"] = dra[0]["CTS"];
                    dr["TOT_RAP_AMOUNT"] = Convert.ToDouble((dra[0]["RAP_AMOUNT"].ToString() != "" && dra[0]["RAP_AMOUNT"].ToString() != null ? dra[0]["RAP_AMOUNT"] : "0"));
                    dr["TOT_NET_AMOUNT"] = dra[0]["NET_AMOUNT"];
                    dr["AVG_PRICE_PER_CTS"] = dra[0]["PRICE_PER_CTS"];
                    dr["AVG_SALES_DISC_PER"] = Convert.ToDouble((dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0"));
                    dtSumm.Rows.Add(dr);
                }

                dtData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();
                dtSumm.TableName = "SummaryTable";

                string filename = "";
                //if (string.IsNullOrEmpty(stockDownloadRequest.StoneID) && string.IsNullOrEmpty(stockDownloadRequest.CertiNo) && string.IsNullOrEmpty(stockDownloadRequest.Shape) && string.IsNullOrEmpty(stockDownloadRequest.Pointer) && string.IsNullOrEmpty(stockDownloadRequest.Color) && string.IsNullOrEmpty(stockDownloadRequest.Clarity) && string.IsNullOrEmpty(stockDownloadRequest.Cut) && string.IsNullOrEmpty(stockDownloadRequest.Polish) && string.IsNullOrEmpty(stockDownloadRequest.Symm) && string.IsNullOrEmpty(stockDownloadRequest.Fls) && string.IsNullOrEmpty(stockDownloadRequest.Lab) && string.IsNullOrEmpty(stockDownloadRequest.Luster) && string.IsNullOrEmpty(stockDownloadRequest.Location) && string.IsNullOrEmpty(stockDownloadRequest.Inclusion) && string.IsNullOrEmpty(stockDownloadRequest.Natts) && string.IsNullOrEmpty(stockDownloadRequest.Shade) && string.IsNullOrEmpty(stockDownloadRequest.FromCts) && string.IsNullOrEmpty(stockDownloadRequest.ToCts) && string.IsNullOrEmpty(stockDownloadRequest.FormDisc) && string.IsNullOrEmpty(stockDownloadRequest.ToDisc) && string.IsNullOrEmpty(stockDownloadRequest.FormPricePerCts) && string.IsNullOrEmpty(stockDownloadRequest.ToPricePerCts) && string.IsNullOrEmpty(stockDownloadRequest.FormNetAmt) && string.IsNullOrEmpty(stockDownloadRequest.ToNetAmt) && string.IsNullOrEmpty(stockDownloadRequest.FormDepth) && string.IsNullOrEmpty(stockDownloadRequest.ToDepth) && string.IsNullOrEmpty(stockDownloadRequest.FormLength) && string.IsNullOrEmpty(stockDownloadRequest.ToLength) && string.IsNullOrEmpty(stockDownloadRequest.FormWidth) && string.IsNullOrEmpty(stockDownloadRequest.ToWidth) && string.IsNullOrEmpty(stockDownloadRequest.FormDepthPer) && string.IsNullOrEmpty(stockDownloadRequest.ToDepthPer) && string.IsNullOrEmpty(stockDownloadRequest.FormTablePer) && string.IsNullOrEmpty(stockDownloadRequest.ToTablePer) && string.IsNullOrEmpty(stockDownloadRequest.HasImage) && string.IsNullOrEmpty(stockDownloadRequest.HasHDMovie) && string.IsNullOrEmpty(stockDownloadRequest.IsPromotion) && string.IsNullOrEmpty("") && string.IsNullOrEmpty(stockDownloadRequest.Reviseflg) && string.IsNullOrEmpty(stockDownloadRequest.CrownInclusion) && string.IsNullOrEmpty(stockDownloadRequest.CrownNatts) && string.IsNullOrEmpty(stockDownloadRequest.TokenNo) && string.IsNullOrEmpty(stockDownloadRequest.StoneStatus) && string.IsNullOrEmpty(stockDownloadRequest.FromCrownAngle) && string.IsNullOrEmpty(stockDownloadRequest.ToCrownAngle) && string.IsNullOrEmpty(stockDownloadRequest.FromCrownHeight) && string.IsNullOrEmpty(stockDownloadRequest.ToCrownHeight) && string.IsNullOrEmpty(stockDownloadRequest.FromPavAngle) && string.IsNullOrEmpty(stockDownloadRequest.ToPavAngle) && string.IsNullOrEmpty(stockDownloadRequest.FromPavHeight) && string.IsNullOrEmpty(stockDownloadRequest.ToPavHeight) && string.IsNullOrEmpty(stockDownloadRequest.BGM) && string.IsNullOrEmpty(stockDownloadRequest.Black) && string.IsNullOrEmpty(stockDownloadRequest.SmartSearch) && string.IsNullOrEmpty(stockDownloadRequest.keytosymbol))
                //{
                //    filename = "Sunrise Diamonds Inventory " + DAL.Common.GetHKTime().ToString("dd.MM.yyyy");
                //}
                //else
                //{
                //    filename = "Sunrise Diamonds Selection " + DAL.Common.GetHKTime().ToString("dd.MM.yyyy");
                //}

                if (stockDownloadRequest.FormName == "New Arrival")
                    filename = "NewArrival " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                else if (stockDownloadRequest.FormName == "Revised Price")
                    filename = "RevisedPrice " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                else
                    filename = "SearchStock " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");

                string _path = ConfigurationManager.AppSettings["data"];
                string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                //EpExcelExport.Excel_Generate(dtData.DefaultView.ToTable(), realpath + "Data" + random + ".xlsx");
                EpExcelExport.CreateExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath, "", "");

                string _strxml = _path + filename + ".xlsx";
                return Ok(_strxml);
                //ds.Tables.Add(dtData);
                //ds.Tables.Add(dtSumm);                

                //return ds;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [HttpPost]
        public IHttpActionResult CartStockDownloadExcel([FromBody] JObject data)
        {
            ViewCartRequest viewCartRequest = new ViewCartRequest();
            try
            {
                viewCartRequest = JsonConvert.DeserializeObject<ViewCartRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ViewCartResponse>
                {
                    Data = new List<ViewCartResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                int transID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == ServiceConstants.SessionTransID).FirstOrDefault().Value);

                DataTable dtData = ViewCartInner(viewCartRequest, userID, transID);
                dtData.DefaultView.RowFilter = "sr IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                string filename = "MyCart " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                string _path = ConfigurationManager.AppSettings["data"];
                string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                EpExcelExport.CreateCartExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath, viewCartRequest.isAdmin, viewCartRequest.isEmp);

                string _strxml = _path + filename + ".xlsx";
                return Ok(_strxml);
                //ds.Tables.Add(dtData);
                //ds.Tables.Add(dtSumm);                

                //return ds;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult DownloadCartSearchMedia([FromBody] JObject data)
        {
            ViewCartRequest viewCartRequest = new ViewCartRequest();
            try
            {
                viewCartRequest = JsonConvert.DeserializeObject<ViewCartRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ViewCartResponse>
                {
                    Data = new List<ViewCartResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                int transID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == ServiceConstants.SessionTransID).FirstOrDefault().Value);

                DataTable dtData = ViewCartInner(viewCartRequest, userID, transID);
                dtData.DefaultView.RowFilter = "sr IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                string subFolderZipName = viewCartRequest.DownloadMedia.ToLower() == "image" ? "StoneImages" :
                                      viewCartRequest.DownloadMedia.ToLower() == "video" ? "StoneVideos" :
                                      viewCartRequest.DownloadMedia.ToLower() == "certificate" ? "StoneCertificates" : "";
                subFolderZipName += DAL.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                string _path = ConfigurationManager.AppSettings["data"];
                string _realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _downloadURL = viewCartRequest.DownloadMedia.ToLower() == "image" ? ConfigurationManager.AppSettings["Img"] :
                                      viewCartRequest.DownloadMedia.ToLower() == "video" ? ConfigurationManager.AppSettings["HDVIDEO"] :
                                      viewCartRequest.DownloadMedia.ToLower() == "certificate" ? "" : "";


                string _str = string.Empty;
                if (viewCartRequest.DownloadMedia.ToLower() == "pdf")
                {
                    Models.PdfTemplate.CartExportToPdf(dtData, _realpath + subFolderZipName + ".pdf");
                    _str = _path + subFolderZipName + ".pdf";
                    return Ok(_str);
                }
                else if (CreateMediaZipCart(dtData, viewCartRequest.DownloadMedia.ToLower(), _realpath, _realpath + subFolderZipName, subFolderZipName, _downloadURL))
                {
                    _str = _path + subFolderZipName + ".zip";
                    return Ok(_str);
                }
                else
                {
                    if (viewCartRequest.DownloadMedia.ToLower() == "video")
                    {
                        return Ok("Error to download video, video is not MP4..!");
                    }
                    else if (viewCartRequest.DownloadMedia.ToLower() == "image")
                    {
                        return Ok("Image is not available in this stone !");
                    }
                    else if (viewCartRequest.DownloadMedia.ToLower() == "certificate")
                    {
                        return Ok("Certificate is not available in this stone !");
                    }
                    else
                    {
                        return Ok("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
            //return Ok("Error");
        }

        [NonAction]
        private DataTable ViewCartInner(ViewCartRequest ViewCart, int UserID, int TransID)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                #region parameter
                para.Add(db.CreateParam("p_for_transid", DbType.String, ParameterDirection.Input, TransID));
                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, UserID));
                if (ViewCart.OfferTrans != null && ViewCart.OfferTrans != "")
                {
                    para.Add(db.CreateParam("p_for_offer", DbType.String, ParameterDirection.Input, ViewCart.OfferTrans));
                }
                if (ViewCart.RefNo == null || ViewCart.RefNo == "")
                    para.Add(db.CreateParam("refno", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("refno", DbType.String, ParameterDirection.Input, ViewCart.RefNo));

                if (!string.IsNullOrEmpty(ViewCart.Location))
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, ViewCart.Location.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Shape))
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, ViewCart.Shape.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Pointer))
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, ViewCart.Pointer.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Lab))
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, ViewCart.Lab.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Color))
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, ViewCart.Color.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Clarity))
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, ViewCart.Clarity.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Cut))
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, ViewCart.Cut.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Polish))
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, ViewCart.Polish.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Symm))
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, ViewCart.Symm.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Fls))
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, ViewCart.Fls.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.ToDate))
                    para.Add(db.CreateParam("p_for_ToDate", DbType.String, ParameterDirection.Input, ViewCart.ToDate));
                else
                    para.Add(db.CreateParam("p_for_ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.FromDate))
                    para.Add(db.CreateParam("p_for_FromDate", DbType.String, ParameterDirection.Input, ViewCart.FromDate));
                else
                    para.Add(db.CreateParam("p_for_FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.Status))
                    para.Add(db.CreateParam("p_for_status", DbType.String, ParameterDirection.Input, ViewCart.Status));
                else
                    para.Add(db.CreateParam("p_for_status", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.RefNo1))
                    para.Add(db.CreateParam("refno1", DbType.String, ParameterDirection.Input, ViewCart.RefNo1));
                else
                    para.Add(db.CreateParam("refno1", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.CompanyName))
                    para.Add(db.CreateParam("p_for_CompanyName", DbType.String, ParameterDirection.Input, ViewCart.CompanyName));
                else
                    para.Add(db.CreateParam("p_for_CompanyName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewCart.PageSize))
                    para.Add(db.CreateParam("p_for_PageSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(ViewCart.PageSize)));
                else
                    para.Add(db.CreateParam("p_for_PageSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("p_for_page", DbType.String, ParameterDirection.Input, ViewCart.PageNo));
                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, ViewCart.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, ViewCart.ActivityType));
                para.Add(db.CreateParam("SubUser", DbType.Boolean, ParameterDirection.Input, ViewCart.SubUser));
                #endregion

                DataTable dt = db.ExecuteSP("IPD_Get_Cart_Det_Sunrise", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult GetDashboardCnt()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (userID > 0)
                    para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("Sp_GetDashboardCnt", para.ToArray(), false);

                List<DashboardCountResponse> dashboardCountResponses = new List<DashboardCountResponse>();
                dashboardCountResponses = DataTableExtension.ToList<DashboardCountResponse>(dt);
                if (dashboardCountResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<DashboardCountResponse>
                    {
                        Data = dashboardCountResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<DashboardCountResponse>
                    {
                        Data = dashboardCountResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<DashboardCountResponse>
                {
                    Data = new List<DashboardCountResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Chk_StockDisc_Avail()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (userID > 0)
                    para.Add(db.CreateParam("iUserid", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));
                else
                    para.Add(db.CreateParam("iUserid", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("Chk_StockDisc_Avail", para.ToArray(), false);

                List<Chk_StockDisc_AvailResponse> chk_stockdisc_availresponse = new List<Chk_StockDisc_AvailResponse>();
                chk_stockdisc_availresponse = DataTableExtension.ToList<Chk_StockDisc_AvailResponse>(dt);
                if (chk_stockdisc_availresponse.Count > 0)
                {
                    return Ok(new ServiceResponse<Chk_StockDisc_AvailResponse>
                    {
                        Data = chk_stockdisc_availresponse,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Chk_StockDisc_AvailResponse>
                    {
                        Data = chk_stockdisc_availresponse,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Chk_StockDisc_AvailResponse>
                {
                    Data = new List<Chk_StockDisc_AvailResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult GetLastLoggedin([FromBody] JObject data)
        {
            GetLastLoggedinRequest GetLastLoggedinRequest = new GetLastLoggedinRequest();

            //try
            //{
            //    GetLastLoggedinRequest = JsonConvert.DeserializeObject<GetLastLoggedinRequest>(data.ToString());
            //}
            //catch (Exception ex)
            //{
            //    DAL.Common.InsertErrorLog(ex, null, Request);
            //    return Ok(new ServiceResponse<GetLastLoggedinRequest>
            //    {
            //        Data = new List<GetLastLoggedinRequest>(),
            //        Message = "Input Parameters are not in the proper format",
            //        Status = "0"
            //    });
            //}
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (userID > 0)
                    para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));
                else
                    para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(GetLastLoggedinRequest.DeviceType))
                    para.Add(db.CreateParam("DeviceType", DbType.String, ParameterDirection.Input, GetLastLoggedinRequest.DeviceType));
                else
                    para.Add(db.CreateParam("DeviceType", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("UserLastLogin_DateTime", para.ToArray(), false);

                List<GetLastLoggedinResponse> GetLastLoggedinResponse = new List<GetLastLoggedinResponse>();
                GetLastLoggedinResponse = DataTableExtension.ToList<GetLastLoggedinResponse>(dt);
                if (GetLastLoggedinResponse.Count > 0)
                {
                    return Ok(new ServiceResponse<GetLastLoggedinResponse>
                    {
                        Data = GetLastLoggedinResponse,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<GetLastLoggedinResponse>
                    {
                        Data = GetLastLoggedinResponse,
                        Message = "NO FOUND",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<DashboardCountResponse>
                {
                    Data = new List<DashboardCountResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetStockSummary([FromBody] JObject data)
        {
            StockSummaryRequest stockSummaryRequest = new StockSummaryRequest();

            try
            {
                stockSummaryRequest = JsonConvert.DeserializeObject<StockSummaryRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<StockSummaryResponse>
                {
                    Data = new List<StockSummaryResponse>(),
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

                if (stockSummaryRequest.parameter != null)
                    para.Add(db.CreateParam("spara", DbType.String, ParameterDirection.Input, stockSummaryRequest.parameter));
                else
                    para.Add(db.CreateParam("spara", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (userID > 0)
                    para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(userID)));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Stock_Summary", para.ToArray(), false);
                List<StockSummaryResponse> stockSummaryResponses = new List<StockSummaryResponse>();
                stockSummaryResponses = DataTableExtension.ToList<StockSummaryResponse>(dt);
                if (stockSummaryResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<StockSummaryResponse>
                    {
                        Data = stockSummaryResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<StockSummaryResponse>
                    {
                        Data = stockSummaryResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<StockSummaryResponse>
                {
                    Data = new List<StockSummaryResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetListValue([FromBody] JObject data)
        {
            ListValueRequest listValueRequest = new ListValueRequest();

            try
            {
                listValueRequest = JsonConvert.DeserializeObject<ListValueRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ListValueResponse>
                {
                    Data = new List<ListValueResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (listValueRequest.ListValue != null)
                    para.Add(db.CreateParam("ListType", DbType.String, ParameterDirection.Input, listValueRequest.ListValue));
                else
                    para.Add(db.CreateParam("ListType", DbType.String, ParameterDirection.Input, DBNull.Value));

                // Change By hitesh on [21-03-2017] as per Priyanka & Disha
                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userID)));
                para.Add(db.CreateParam("iEmpId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("GetListValue", para.ToArray(), false);
                List<ListValueResponse> listValueResponses = new List<ListValueResponse>();
                listValueResponses = DataTableExtension.ToList<ListValueResponse>(dt);
                if (listValueResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<ListValueResponse>
                    {
                        Data = listValueResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ListValueResponse>
                    {
                        Data = listValueResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<StockSummaryResponse>
                {
                    Data = new List<StockSummaryResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetKeyToSymbol()
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("get_key_to_symbol", para.ToArray(), false);
                List<KeyToSymbolResponse> keyToSymbolResponses = new List<KeyToSymbolResponse>();
                keyToSymbolResponses = DataTableExtension.ToList<KeyToSymbolResponse>(dt);
                if (keyToSymbolResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<KeyToSymbolResponse>
                    {
                        Data = keyToSymbolResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<KeyToSymbolResponse>
                    {
                        Data = keyToSymbolResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<KeyToSymbolResponse>
                {
                    Data = new List<KeyToSymbolResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetColumnsConfigUserWise()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (userID > 0)
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ColumnConfDet_Select", para.ToArray(), false);

                List<ColumnsConfigUserWiseResponse> columnsConfigs = new List<ColumnsConfigUserWiseResponse>();
                columnsConfigs = DataTableExtension.ToList<ColumnsConfigUserWiseResponse>(dt);
                if (columnsConfigs.Count > 0)
                {
                    return Ok(new ServiceResponse<ColumnsConfigUserWiseResponse>
                    {
                        Data = columnsConfigs,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ColumnsConfigUserWiseResponse>
                    {
                        Data = columnsConfigs,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ColumnsConfigUserWiseResponse>
                {
                    Data = new List<ColumnsConfigUserWiseResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetColumnsCaptionsList()
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("col_master", para.ToArray(), false);

                List<ColumnsCaptionsResponse> columnsCaptions = new List<ColumnsCaptionsResponse>();
                columnsCaptions = DataTableExtension.ToList<ColumnsCaptionsResponse>(dt);
                if (columnsCaptions.Count > 0)
                {
                    return Ok(new ServiceResponse<ColumnsCaptionsResponse>
                    {
                        Data = columnsCaptions,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ColumnsCaptionsResponse>
                    {
                        Data = columnsCaptions,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ColumnsCaptionsResponse>
                {
                    Data = new List<ColumnsCaptionsResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetColumnsConfigForSearch()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (userID > 0)
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ColumnConfDet_SearchResult", para.ToArray(), false);

                List<ColumnsConfigForSearchResponse> columnsConfigs = new List<ColumnsConfigForSearchResponse>();
                columnsConfigs = DataTableExtension.ToList<ColumnsConfigForSearchResponse>(dt);
                if (columnsConfigs.Count > 0)
                {
                    return Ok(new ServiceResponse<ColumnsConfigForSearchResponse>
                    {
                        Data = columnsConfigs,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ColumnsConfigForSearchResponse>
                    {
                        Data = columnsConfigs,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ColumnsConfigForSearchResponse>
                {
                    Data = new List<ColumnsConfigForSearchResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetSearchStock([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();

            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsResponse>
                {
                    Data = new List<SearchDiamondsResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                searchDiamondsRequest.UserID = userID;
                DataTable dtData = SearchStockInner(searchDiamondsRequest, true, false);

                DataRow[] dra = dtData.Select("Sr IS NULL");
                SearchSummary searchSummary = new SearchSummary();
                if (dra.Length > 0)
                {
                    searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                    searchSummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["STONE_REF_NO"]);
                    searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["CTS"]);
                    searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble((dra[0]["RAP_AMOUNT"].ToString() != "" && dra[0]["RAP_AMOUNT"].ToString() != null ? dra[0]["RAP_AMOUNT"] : "0"));
                    searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["NET_AMOUNT"]);
                    searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(dra[0]["PRICE_PER_CTS"]);
                    searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0"));
                }

                dtData.DefaultView.RowFilter = "Sr IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                SearchDiamondsResponse searchDiamondsResponse = new SearchDiamondsResponse();

                List<SearchStone> listSearchStone = new List<SearchStone>();
                listSearchStone = DataTableExtension.ToList<SearchStone>(dtData);
                List<SearchDiamondsResponse> searchDiamondsResponses = new List<SearchDiamondsResponse>();

                if (listSearchStone.Count > 0)
                {
                    searchDiamondsResponses.Add(new SearchDiamondsResponse()
                    {
                        DataList = listSearchStone,
                        DataSummary = searchSummary
                    });

                    return Ok(new ServiceResponse<SearchDiamondsResponse>
                    {
                        Data = searchDiamondsResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<SearchDiamondsResponse>
                    {
                        Data = searchDiamondsResponses,
                        Message = "No Data Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsResponse>
                {
                    Data = new List<SearchDiamondsResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult SaveGetSearchStock([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();

            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsResponse>
                {
                    Data = new List<SearchDiamondsResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                searchDiamondsRequest.UserID = userID;

                DataTable dtData = SearchStockInner(searchDiamondsRequest, true, false);

                List<SearchDiamondsResponse> listSearchStone = new List<SearchDiamondsResponse>();
                listSearchStone = DataTableExtension.ToList<SearchDiamondsResponse>(dtData);

                return Ok(new ServiceResponse<SearchDiamondsResponse>
                {
                    Data = listSearchStone,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsResponse>
                {
                    Data = new List<SearchDiamondsResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetSearchStockByStoneID([FromBody] JObject data)
        {
            SearchByStoneIDRequest searchByStoneIDRequest = new SearchByStoneIDRequest();

            try
            {
                searchByStoneIDRequest = JsonConvert.DeserializeObject<SearchByStoneIDRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                DataTable dtData = GetDiamondDetailInner(searchByStoneIDRequest.StoneID, userID.ToString());

                //dtData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                //dtData = dtData.DefaultView.ToTable();

                List<SearchStone> listSearchStone = new List<SearchStone>();
                listSearchStone = DataTableExtension.ToList<SearchStone>(dtData);

                if (listSearchStone.Count > 0)
                {
                    return Ok(listSearchStone.FirstOrDefault());
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetSearchStockByStoneIDWithoutToken([FromBody] JObject data)
        {
            SearchByStoneIDRequest searchByStoneIDRequest = new SearchByStoneIDRequest();

            try
            {
                searchByStoneIDRequest = JsonConvert.DeserializeObject<SearchByStoneIDRequest>(data.ToString());
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

                para.Add(db.CreateParam("p_for_refno", System.Data.DbType.String, System.Data.ParameterDirection.Input, searchByStoneIDRequest.StoneID));

                if (searchByStoneIDRequest.UserId > 0)
                    para.Add(db.CreateParam("p_for_usercode", System.Data.DbType.String, System.Data.ParameterDirection.Input, searchByStoneIDRequest.UserId.ToString()));
                else
                    para.Add(db.CreateParam("p_for_usercode", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("ipd_diamond_Detail", para.ToArray(), false);

                //dtData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                //dtData = dtData.DefaultView.ToTable();

                List<SearchStone> listSearchStone = new List<SearchStone>();
                listSearchStone = DataTableExtension.ToList<SearchStone>(dtData);

                if (listSearchStone.Count > 0)
                {
                    return Ok(listSearchStone.FirstOrDefault());
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult LogoutWithoutToken([FromBody] JObject data)
        {
            try
            {
                if (System.Web.HttpContext.Current.Application["UserIdCookie"] != null)
                {
                    System.Web.HttpContext.Current.Application.Remove("UserIdCookie");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [NonAction]
        private System.Data.DataTable GetDiamondDetailInner(String StoneID, String UserID)
        {
            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                para.Add(db.CreateParam("p_for_refno", System.Data.DbType.String, System.Data.ParameterDirection.Input, StoneID));
                para.Add(db.CreateParam("p_for_usercode", System.Data.DbType.String, System.Data.ParameterDirection.Input, UserID));

                System.Data.DataTable dt = db.ExecuteSP("ipd_diamond_Detail", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult GetQuickSearch([FromBody] JObject data)
        {
            QuickSearchRequest quickSearchRequest = new QuickSearchRequest();

            try
            {
                quickSearchRequest = JsonConvert.DeserializeObject<QuickSearchRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<QuickSearchResponse>
                {
                    Data = new List<QuickSearchResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                quickSearchRequest.iUserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                DataTable dt = QuickSearchInner(quickSearchRequest);

                List<QuickSearchResponse> quickSearchResponses = new List<QuickSearchResponse>();
                quickSearchResponses = DataTableExtension.ToList<QuickSearchResponse>(dt);
                if (quickSearchResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<QuickSearchResponse>
                    {
                        Data = quickSearchResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<QuickSearchResponse>
                    {
                        Data = quickSearchResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<QuickSearchResponse>
                {
                    Data = new List<QuickSearchResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetSubQuickSearch([FromBody] JObject data)
        {
            SubQuickSearchRequest subQuickSearchRequest = new SubQuickSearchRequest();

            try
            {
                subQuickSearchRequest = JsonConvert.DeserializeObject<SubQuickSearchRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsRequest>
                {
                    Data = new List<SearchDiamondsRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable dt = GetSubQuickSearchInner(subQuickSearchRequest);

                List<SubQuickSearchResponse> subQuickSearchResponses = new List<SubQuickSearchResponse>();
                subQuickSearchResponses = DataTableExtension.ToList<SubQuickSearchResponse>(dt);
                if (subQuickSearchResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<SubQuickSearchResponse>
                    {
                        Data = subQuickSearchResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<SubQuickSearchResponse>
                    {
                        Data = subQuickSearchResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SubQuickSearchResponse>
                {
                    Data = new List<SubQuickSearchResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult GetRecentSearch()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (userID > 0)
                    para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("IPD_RECENT_SEARCH", para.ToArray(), false);

                List<RecentSearchResponse> recentSearchResponses = new List<RecentSearchResponse>();
                recentSearchResponses = DataTableExtension.ToList<RecentSearchResponse>(dt);
                if (recentSearchResponses.Count > 0)
                {
                    string description = string.Empty;
                    foreach (var item in recentSearchResponses)
                    {
                        description = string.Empty;
                        if (!string.IsNullOrEmpty(item.SHAPE))
                        {
                            description += ",Shape: " + item.SHAPE;
                        }
                        if (!string.IsNullOrEmpty(item.POINTER))
                        {
                            description += ",Carat Weight: " + item.POINTER;
                        }
                        if (item.FROM_CTS > 0.0 && item.TO_CTS > 0.0)
                        {
                            description += ",Carat Weight: " + item.FROM_CTS.ToString("#,##0.00") + "-" + item.TO_CTS.ToString("#,##0.00");
                        }
                        if (!string.IsNullOrEmpty(item.ColorType))
                        {
                            if (item.ColorType == "Regular")
                            {
                                description += ",Color: Regular: " + (item.COLOR != "" && item.COLOR != null ? item.COLOR : "All");
                            }
                            else if (item.ColorType == "Fancy")
                            {
                                if (!string.IsNullOrEmpty(item.Intensity) || !string.IsNullOrEmpty(item.Overtone) || !string.IsNullOrEmpty(item.Fancy_Color))
                                {
                                    description += ",Color: Fancy: ";
                                    if (!string.IsNullOrEmpty(item.Intensity))
                                    {
                                        description += "Intensity :- " + item.Intensity + ", ";
                                    }
                                    if (!string.IsNullOrEmpty(item.Overtone))
                                    {
                                        description += "Overtone :- " + item.Overtone + ", ";
                                    }
                                    if (!string.IsNullOrEmpty(item.Fancy_Color))
                                    {
                                        description += "Fancy :- " + item.Fancy_Color + ", ";
                                    }
                                    if (!string.IsNullOrEmpty(item.Intensity) || !string.IsNullOrEmpty(item.Overtone) || !string.IsNullOrEmpty(item.Fancy_Color))
                                    {
                                        //description.Remove(description.Length - 2, 1);
                                        description = description.Remove(description.Length - 2);
                                    }
                                }
                                else
                                {
                                    description += ",Color: Fancy: All";
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(item.CLARITY))
                        {
                            description += ",Clarity: " + item.CLARITY;
                        }
                        if (!string.IsNullOrEmpty(item.CUT))
                        {
                            description += ",Cut: " + item.CUT;
                        }
                        if (!string.IsNullOrEmpty(item.POLISH))
                        {
                            description += ",Polish: " + item.POLISH;
                        }
                        if (!string.IsNullOrEmpty(item.SYMM))
                        {
                            description += ",Sym: " + item.SYMM;
                        }
                        if (!string.IsNullOrEmpty(item.FLS))
                        {
                            description += ",Fls: " + item.FLS;
                        }
                        if (!string.IsNullOrEmpty(item.SHADE))
                        {
                            description += ",BGM: " + item.SHADE;
                        }
                        if (!string.IsNullOrEmpty(item.LOCATION))
                        {
                            description += ",Location: " + item.LOCATION;
                        }
                        if (!string.IsNullOrEmpty(item.NATTS))
                        {
                            description += ",Table Black: " + item.NATTS;
                        }
                        if (!string.IsNullOrEmpty(item.CROWN_NATTS))
                        {
                            description += ",Crown Black: " + item.CROWN_NATTS;
                        }
                        if (!string.IsNullOrEmpty(item.LAB))
                        {
                            description += ",Lab: " + item.LAB;
                        }
                        if (!string.IsNullOrEmpty(item.INCLUSION))
                        {
                            description += ",Table White: " + item.INCLUSION;
                        }
                        if (!string.IsNullOrEmpty(item.CROWN_INCLUSION))
                        {
                            description += ",Crown White: " + item.CROWN_INCLUSION;
                        }
                        /*if (!string.IsNullOrEmpty(item.LUSTER)) { }*/
                        if (!string.IsNullOrEmpty(item.skeytosymbol))
                        {
                            description += ",Key To Symbol: " + item.skeytosymbol;
                        }
                        if (item.FROM_DISC > 0.0 && item.TO_DISC > 0.0)
                        {
                            description += ",Discount: " + item.FROM_DISC.ToString() + "-" + item.TO_DISC.ToString();
                        }
                        if (item.FROM_PRICECTS > 0.0 && item.TO_PRICECTS > 0.0)
                        {
                            description += ",Price/Cts USD: " + item.FROM_PRICECTS.ToString() + "-" + item.TO_PRICECTS.ToString();
                        }
                        if (item.FROM_NETAMT > 0.0 && item.TO_NETAMT > 0.0)
                        {
                            description += ",Total Amount: " + item.FROM_NETAMT.ToString() + "-" + item.TO_NETAMT.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.IMAGE.ToString()) || !string.IsNullOrEmpty(item.MOVIE.ToString()))
                        {
                            description += ",Media: " + (!string.IsNullOrEmpty(item.IMAGE.ToString()) ? "Image-" + item.IMAGE.ToString() : "")
                                + (!string.IsNullOrEmpty(item.MOVIE.ToString()) ? "Video-" + item.MOVIE.ToString() : "");
                        }
                        if (item.FROM_LENGTH > 0.0 && item.TO_LENGTH > 0.0)
                        {
                            description += ",Length: " + item.FROM_LENGTH.ToString() + "-" + item.TO_LENGTH.ToString();
                        }
                        if (item.FROM_WIDTH > 0.0 && item.TO_WIDTH > 0.0)
                        {
                            description += ",Width: " + item.FROM_WIDTH.ToString() + "-" + item.TO_WIDTH.ToString();
                        }
                        /* if (!string.IsNullOrEmpty(item.FROM_DEPTH)) { }
                         if (!string.IsNullOrEmpty(item.TO_DEPTH)) { }*/
                        if (item.FROM_DEPTH_PER > 0.0 && item.TO_DEPTH_PER > 0.0)
                        {
                            description += ",Depth%: " + item.FROM_DEPTH_PER.ToString() + "-" + item.TO_DEPTH_PER.ToString();
                        }
                        if (item.FROM_TABLE_PER > 0.0 && item.TO_TABLE_PER > 0.0)
                        {
                            description += ",Table%: " + item.FROM_TABLE_PER.ToString() + "-" + item.TO_TABLE_PER.ToString();
                        }

                        /*  if (!string.IsNullOrEmpty(item.PROMOTION)) { }
                          if (!string.IsNullOrEmpty(item.SHAPE_COLOR_PURITY)) { } */
                        if (item.FROM_CR_ANG > 0.0 && item.TO_CR_ANG > 0.0)
                        {
                            description += ",Cr Ang: " + item.FROM_CR_ANG.ToString() + "-" + item.TO_CR_ANG.ToString();
                        }
                        if (item.FROM_CR_HT > 0.0 && item.TO_CR_HT > 0.0)
                        {
                            description += ",Cr Ht: " + item.FROM_CR_HT.ToString() + "-" + item.TO_CR_HT.ToString();
                        }
                        if (item.FROM_PAV_ANG > 0.0 && item.TO_PAV_ANG > 0.0)
                        {
                            description += ",Pav Ang: " + item.FROM_PAV_ANG.ToString() + "-" + item.TO_PAV_ANG.ToString();
                        }
                        if (item.FROM_PAV_HT > 0.0 && item.TO_PAV_HT > 0.0)
                        {
                            description += ",Pav Ht: " + item.FROM_PAV_HT.ToString() + "-" + item.TO_PAV_HT.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.Table_Open))
                        {
                            description += ",Table Open: " + item.Table_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Crown_Open))
                        {
                            description += ",Crown Open: " + item.Crown_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Pav_Open))
                        {
                            description += ",Pav Open: " + item.Pav_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Girdle_Open))
                        {
                            description += ",Girdle Open: " + item.Girdle_Open;
                        }
                        if (!string.IsNullOrEmpty(item.STONE_REF_NO))
                        {
                            description += ",Ref No / Certi No: " + item.STONE_REF_NO;
                        }

                        /*if (!string.IsNullOrEmpty(item.FROM_RAP_AMOUNT)) { }
                        if (!string.IsNullOrEmpty(item.TO_RAP_AMOUNT)) { }
                        if (!string.IsNullOrEmpty(item.FormName)) { }*/

                        if (!string.IsNullOrEmpty(description))
                        {
                            description = description.StartsWith(",") ? description.Remove(0, 1) : description;
                        }
                        else
                        {
                            description = "All Criteria";
                        }

                        item.Description = description;
                    }

                    return Ok(new ServiceResponse<RecentSearchResponse>
                    {
                        Data = recentSearchResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<RecentSearchResponse>
                    {
                        Data = recentSearchResponses,
                        Message = "No Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<RecentSearchResponse>
                {
                    Data = new List<RecentSearchResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult Dashboard_GetRecentSearch()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (userID > 0)
                    para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("IPD_Recent_Search_Dashboard", para.ToArray(), false);

                List<RecentSearchResponse> recentSearchResponses = new List<RecentSearchResponse>();
                recentSearchResponses = DataTableExtension.ToList<RecentSearchResponse>(dt);
                if (recentSearchResponses.Count > 0)
                {
                    string description = string.Empty;
                    foreach (var item in recentSearchResponses)
                    {
                        description = string.Empty;
                        if (!string.IsNullOrEmpty(item.SHAPE))
                        {
                            description += ",<span class='dark'>Shape</span>: " + item.SHAPE;
                        }
                        if (!string.IsNullOrEmpty(item.POINTER))
                        {
                            description += ",<span class='dark'>Carat Weight</span>: " + item.POINTER;
                        }
                        if (item.FROM_CTS > 0.0 && item.TO_CTS > 0.0)
                        {
                            description += ",<span class='dark'>Carat Weight</span>: " + item.FROM_CTS.ToString("#,##0.00") + "-" + item.TO_CTS.ToString("#,##0.00");
                        }
                        if (!string.IsNullOrEmpty(item.ColorType))
                        {
                            if (item.ColorType == "Regular")
                            {
                                description += ",<span class='dark'>Color</span>: Regular: " + (item.COLOR != "" && item.COLOR != null ? item.COLOR : "All");
                            }
                            else if (item.ColorType == "Fancy")
                            {
                                if (!string.IsNullOrEmpty(item.Intensity) || !string.IsNullOrEmpty(item.Overtone) || !string.IsNullOrEmpty(item.Fancy_Color))
                                {
                                    description += ",<span class='dark'>Color</span>: Fancy: ";
                                    if (!string.IsNullOrEmpty(item.Intensity))
                                    {
                                        description += "Intensity :- " + item.Intensity + ", ";
                                    }
                                    if (!string.IsNullOrEmpty(item.Overtone))
                                    {
                                        description += "Overtone :- " + item.Overtone + ", ";
                                    }
                                    if (!string.IsNullOrEmpty(item.Fancy_Color))
                                    {
                                        description += "Fancy :- " + item.Fancy_Color + ", ";
                                    }
                                    if (!string.IsNullOrEmpty(item.Intensity) || !string.IsNullOrEmpty(item.Overtone) || !string.IsNullOrEmpty(item.Fancy_Color))
                                    {
                                        //description.Remove(description.Length - 2, 1);
                                        description = description.Remove(description.Length - 2);
                                    }
                                }
                                else
                                {
                                    description += ",<span class='dark'>Color</span>: Fancy: All";
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(item.CLARITY))
                        {
                            description += ",<span class='dark'>Clarity</span>: " + item.CLARITY;
                        }
                        if (!string.IsNullOrEmpty(item.CUT))
                        {
                            description += ",<span class='dark'>Cut</span>: " + item.CUT;
                        }
                        if (!string.IsNullOrEmpty(item.POLISH))
                        {
                            description += ",<span class='dark'>Polish</span>: " + item.POLISH;
                        }
                        if (!string.IsNullOrEmpty(item.SYMM))
                        {
                            description += ",<span class='dark'>Sym</span>: " + item.SYMM;
                        }
                        if (!string.IsNullOrEmpty(item.FLS))
                        {
                            description += ",<span class='dark'>Fls</span>: " + item.FLS;
                        }
                        if (!string.IsNullOrEmpty(item.SHADE))
                        {
                            description += ",<span class='dark'>BGM</span>: " + item.SHADE;
                        }
                        if (!string.IsNullOrEmpty(item.LOCATION))
                        {
                            description += ",<span class='dark'>Location</span>: " + item.LOCATION;
                        }
                        if (!string.IsNullOrEmpty(item.NATTS))
                        {
                            description += ",<span class='dark'>Table Black</span>: " + item.NATTS;
                        }
                        if (!string.IsNullOrEmpty(item.CROWN_NATTS))
                        {
                            description += ",<span class='dark'>Crown Black</span>: " + item.CROWN_NATTS;
                        }
                        if (!string.IsNullOrEmpty(item.LAB))
                        {
                            description += ",<span class='dark'>Lab</span>: " + item.LAB;
                        }
                        if (!string.IsNullOrEmpty(item.INCLUSION))
                        {
                            description += ",<span class='dark'>Table White</span>: " + item.INCLUSION;
                        }
                        if (!string.IsNullOrEmpty(item.CROWN_INCLUSION))
                        {
                            description += ",<span class='dark'>Crown White</span>: " + item.CROWN_INCLUSION;
                        }
                        /*if (!string.IsNullOrEmpty(item.LUSTER)) { }*/
                        if (!string.IsNullOrEmpty(item.skeytosymbol))
                        {
                            description += ",<span class='dark'>Key To Symbol</span>: " + item.skeytosymbol;
                        }
                        if (item.FROM_DISC > 0.0 && item.TO_DISC > 0.0)
                        {
                            description += ",<span class='dark'>Discount</span>: " + item.FROM_DISC.ToString() + "-" + item.TO_DISC.ToString();
                        }
                        if (item.FROM_PRICECTS > 0.0 && item.TO_PRICECTS > 0.0)
                        {
                            description += ",<span class='dark'>Price/Cts USD</span>: " + item.FROM_PRICECTS.ToString() + "-" + item.TO_PRICECTS.ToString();
                        }
                        if (item.FROM_NETAMT > 0.0 && item.TO_NETAMT > 0.0)
                        {
                            description += ",<span class='dark'>Total Amount</span>: " + item.FROM_NETAMT.ToString() + "-" + item.TO_NETAMT.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.IMAGE.ToString()) || !string.IsNullOrEmpty(item.MOVIE.ToString()))
                        {
                            description += ",<span class='dark'>Media</span>: " + (!string.IsNullOrEmpty(item.IMAGE.ToString()) ? "Image-" + item.IMAGE.ToString() : "")
                                + (!string.IsNullOrEmpty(item.MOVIE.ToString()) ? "Video-" + item.MOVIE.ToString() : "");
                        }
                        if (item.FROM_LENGTH > 0.0 && item.TO_LENGTH > 0.0)
                        {
                            description += ",<span class='dark'>Length</span>: " + item.FROM_LENGTH.ToString() + "-" + item.TO_LENGTH.ToString();
                        }
                        if (item.FROM_WIDTH > 0.0 && item.TO_WIDTH > 0.0)
                        {
                            description += ",<span class='dark'>Width</span>: " + item.FROM_WIDTH.ToString() + "-" + item.TO_WIDTH.ToString();
                        }
                        /* if (!string.IsNullOrEmpty(item.FROM_DEPTH)) { }
                         if (!string.IsNullOrEmpty(item.TO_DEPTH)) { }*/
                        if (item.FROM_DEPTH_PER > 0.0 && item.TO_DEPTH_PER > 0.0)
                        {
                            description += ",<span class='dark'>Depth%</span>: " + item.FROM_DEPTH_PER.ToString() + "-" + item.TO_DEPTH_PER.ToString();
                        }
                        if (item.FROM_TABLE_PER > 0.0 && item.TO_TABLE_PER > 0.0)
                        {
                            description += ",<span class='dark'>Table%</span>: " + item.FROM_TABLE_PER.ToString() + "-" + item.TO_TABLE_PER.ToString();
                        }

                        /*  if (!string.IsNullOrEmpty(item.PROMOTION)) { }
                          if (!string.IsNullOrEmpty(item.SHAPE_COLOR_PURITY)) { } */
                        if (item.FROM_CR_ANG > 0.0 && item.TO_CR_ANG > 0.0)
                        {
                            description += ",<span class='dark'>Cr Ang</span>: " + item.FROM_CR_ANG.ToString() + "-" + item.TO_CR_ANG.ToString();
                        }
                        if (item.FROM_CR_HT > 0.0 && item.TO_CR_HT > 0.0)
                        {
                            description += ",<span class='dark'>Cr Ht</span>: " + item.FROM_CR_HT.ToString() + "-" + item.TO_CR_HT.ToString();
                        }
                        if (item.FROM_PAV_ANG > 0.0 && item.TO_PAV_ANG > 0.0)
                        {
                            description += ",<span class='dark'>Pav Ang</span>: " + item.FROM_PAV_ANG.ToString() + "-" + item.TO_PAV_ANG.ToString();
                        }
                        if (item.FROM_PAV_HT > 0.0 && item.TO_PAV_HT > 0.0)
                        {
                            description += ",<span class='dark'>Pav Ht</span>: " + item.FROM_PAV_HT.ToString() + "-" + item.TO_PAV_HT.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.Table_Open))
                        {
                            description += ",<span class='dark'>Table Open</span>: " + item.Table_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Crown_Open))
                        {
                            description += ",<span class='dark'>Crown Open</span>: " + item.Crown_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Pav_Open))
                        {
                            description += ",<span class='dark'>Pav Open</span>: " + item.Pav_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Girdle_Open))
                        {
                            description += ",<span class='dark'>Girdle Open</span>: " + item.Girdle_Open;
                        }
                        if (!string.IsNullOrEmpty(item.STONE_REF_NO))
                        {
                            description += ",<span class='dark'>Ref No / Certi No</span>: " + item.STONE_REF_NO;
                        }

                        /*if (!string.IsNullOrEmpty(item.FROM_RAP_AMOUNT)) { }
                        if (!string.IsNullOrEmpty(item.TO_RAP_AMOUNT)) { }
                        if (!string.IsNullOrEmpty(item.FormName)) { }*/

                        if (!string.IsNullOrEmpty(description))
                        {
                            description = description.StartsWith(",") ? description.Remove(0, 1) : description;
                        }
                        else
                        {
                            description = "All Criteria";
                        }
                        if (!string.IsNullOrEmpty(item.ActivityType) && item.ActivityType == "Excel")
                        {
                            description = "<i class='fa fa-file-excel-o' aria-hidden='true' style='font-size: 15px; margin-top: -7px; margin-right: 4px; margin-bottom: -1px;'></i>" + description;
                        }

                        item.Description = description;
                    }

                    return Ok(new ServiceResponse<RecentSearchResponse>
                    {
                        Data = recentSearchResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<RecentSearchResponse>
                    {
                        Data = recentSearchResponses,
                        Message = "No Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<RecentSearchResponse>
                {
                    Data = new List<RecentSearchResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetPairSearch([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();

            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsRequest>
                {
                    Data = new List<SearchDiamondsRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                DataTable dtSumm = new DataTable();
                dtSumm.Columns.Add("TOT_PAGE", typeof(Int32));
                dtSumm.Columns.Add("PAGE_SIZE", typeof(Int32));
                dtSumm.Columns.Add("TOT_PCS", typeof(Int32));
                dtSumm.Columns.Add("TOT_CTS", typeof(Decimal));
                dtSumm.Columns.Add("TOT_RAP_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("TOT_NET_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("AVG_PRICE_PER_CTS", typeof(Decimal));
                dtSumm.Columns.Add("AVG_SALES_DISC_PER", typeof(Decimal));

                DataTable dtData = PairSearchInner(searchDiamondsRequest, userID, "N");
                DataRow[] dra = dtData.Select("P_SEQ_NO IS NULL");
                SearchSummary searchSummary = new SearchSummary();

                if (dra.Length > 0)
                {
                    searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                    searchSummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["STONE_REF_NO"]);
                    searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["CTS"]);
                    searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble((dra[0]["RAP_AMOUNT"].ToString() != "" && dra[0]["RAP_AMOUNT"].ToString() != null ? dra[0]["RAP_AMOUNT"] : "0"));
                    searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["NET_AMOUNT"]);
                    searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(dra[0]["PRICE_PER_CTS"]);
                    searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0"));
                }

                dtData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();
                dtData.Columns.Add("PairLastColumn", typeof(System.Boolean));
                int n = 0;
                foreach (DataRow row in dtData.Rows)
                {
                    if (n > 0)
                    {
                        if (Convert.ToInt32(row["pair_no"]) != n)
                        {
                            n = Convert.ToInt32(row["pair_no"]);
                            row["PairLastColumn"] = true;
                        }
                        else
                        {
                            row["PairLastColumn"] = false;
                        }
                    }
                    else
                    {
                        n = Convert.ToInt32(row["pair_no"]);
                        row["PairLastColumn"] = false;
                    }
                    //need to set value to NewColumn column

                }
                PairSearchDiamondsResponse searchDiamondsResponse = new PairSearchDiamondsResponse();

                List<PairSearchStone> listSearchStone = new List<PairSearchStone>();
                listSearchStone = DataTableExtension.ToList<PairSearchStone>(dtData);
                List<PairSearchDiamondsResponse> searchDiamondsResponses = new List<PairSearchDiamondsResponse>();

                if (listSearchStone.Count > 0)
                {
                    searchDiamondsResponses.Add(new PairSearchDiamondsResponse()
                    {
                        DataList = listSearchStone,
                        DataSummary = searchSummary
                    });

                    return Ok(new ServiceResponse<PairSearchDiamondsResponse>
                    {
                        Data = searchDiamondsResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<PairSearchDiamondsResponse>
                    {
                        Data = searchDiamondsResponses,
                        Message = "No Data Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<PairSearchDiamondsResponse>
                {
                    Data = new List<PairSearchDiamondsResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetUserSearch()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (userID > 0)
                    para.Add(db.CreateParam("iuserId", DbType.String, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("iuserId", DbType.String, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("UserSearchDet_Select_userid", para.ToArray(), false);

                List<UserSearchResponse> userSearchResponses = new List<UserSearchResponse>();
                userSearchResponses = DataTableExtension.ToList<UserSearchResponse>(dt);

                if (userSearchResponses.Count > 0)
                {
                    string description = string.Empty;
                    foreach (var item in userSearchResponses)
                    {
                        description = string.Empty;
                        if (!string.IsNullOrEmpty(item.sShape))
                        {
                            description += ",Shape: " + item.sShape;
                        }
                        if (!string.IsNullOrEmpty(item.sPointer))
                        {
                            description += ",Carat Weight: " + item.sPointer;
                        }
                        if (item.dFromCts > 0.0 && item.dToCts > 0.0)
                        {
                            description += ",Carat Weight: " + item.dFromCts.ToString() + "-" + item.dToCts.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.Color_Description))
                        {
                            description += ",Color: " + item.Color_Description;
                        }
                        if (!string.IsNullOrEmpty(item.sClarity))
                        {
                            description += ",Clarity: " + item.sClarity;
                        }
                        if (!string.IsNullOrEmpty(item.sCut))
                        {
                            description += ",Cut: " + item.sCut;
                        }
                        if (!string.IsNullOrEmpty(item.sPolish))
                        {
                            description += ",Polish: " + item.sPolish;
                        }
                        if (!string.IsNullOrEmpty(item.sSymm))
                        {
                            description += ",Sym: " + item.sSymm;
                        }
                        if (!string.IsNullOrEmpty(item.sFls))
                        {
                            description += ",Fls: " + item.sFls;
                        }
                        if (!string.IsNullOrEmpty(item.sShade))
                        {
                            description += ",BGM: " + item.sShade;
                        }
                        if (!string.IsNullOrEmpty(item.location1))
                        {
                            description += ",Location: " + item.location1;
                        }
                        if (!string.IsNullOrEmpty(item.sNatts))
                        {
                            description += ",Table Natts: " + item.sNatts;
                        }
                        if (!string.IsNullOrEmpty(item.sCrownNatts))
                        {
                            description += ",Crown Natts: " + item.sCrownNatts;
                        }
                        if (!string.IsNullOrEmpty(item.sLab))
                        {
                            description += ",Lab: " + item.sLab;
                        }
                        if (!string.IsNullOrEmpty(item.sInclusion))
                        {
                            description += ",Table Inclusion: " + item.sInclusion;
                        }
                        if (!string.IsNullOrEmpty(item.sCrownInclusion))
                        {
                            description += ",Crown Inclusion: " + item.sCrownInclusion;
                        }
                        /*if (!string.IsNullOrEmpty(item.LUSTER)) { }*/
                        if (!string.IsNullOrEmpty(item.SkeyToSymbol))
                        {
                            description += ",Key To Symbol: " + item.SkeyToSymbol;
                        }
                        if (item.dFromDisc > 0.0 && item.dToDisc > 0.0)
                        {
                            description += ",Discount: " + item.dFromDisc.ToString() + "-" + item.dToDisc.ToString();
                        }
                        if (item.dFromPriceCts > 0.0 && item.dToPriceCts > 0.0)
                        {
                            description += ",Price/Cts USD: " + item.dFromPriceCts.ToString() + "-" + item.dToPriceCts.ToString();
                        }
                        if (item.dFromNetPrice > 0.0 && item.dToNetPrice > 0.0)
                        {
                            description += ",Total Amount: " + item.dFromNetPrice.ToString() + "-" + item.dToNetPrice.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.bImage.ToString()) || !string.IsNullOrEmpty(item.bHDMovie.ToString()))
                        {
                            description += ",Media: " + (!string.IsNullOrEmpty(item.bImage.ToString()) ? "Image-" + item.bImage.ToString() : "")
                                + (!string.IsNullOrEmpty(item.bHDMovie.ToString()) ? "Video-" + item.bHDMovie.ToString() : "");
                        }
                        if (item.dFromLength > 0.0 && item.dToLength > 0.0)
                        {
                            description += ",Length: " + item.dFromLength.ToString() + "-" + item.dToLength.ToString();
                        }
                        if (item.dFromWidth > 0.0 && item.dToWidth > 0.0)
                        {
                            description += ",Width: " + item.dFromWidth.ToString() + "-" + item.dToWidth.ToString();
                        }
                        /* if (!string.IsNullOrEmpty(item.FROMDEPTH)) { }
                         if (!string.IsNullOrEmpty(item.TODEPTH)) { }*/
                        if (item.dFromDepthPer > 0.0 && item.dToDepthPer > 0.0)
                        {
                            description += ",Depth%: " + item.dFromDepthPer.ToString() + "-" + item.dToDepthPer.ToString();
                        }
                        if (item.dFromTablePer > 0.0 && item.dToTablePer > 0.0)
                        {
                            description += ",Table%: " + item.dFromTablePer.ToString() + "-" + item.dToTablePer.ToString();
                        }

                        /*  if (!string.IsNullOrEmpty(item.PROMOTION)) { }
                          if (!string.IsNullOrEmpty(item.SHAPECOLORPURITY)) { } */
                        if (item.dFromCrAng > 0.0 && item.dToCrAng > 0.0)
                        {
                            description += ",Cr Ang: " + item.dFromCrAng.ToString() + "-" + item.dToCrAng.ToString();
                        }
                        if (item.dFromCrHt > 0.0 && item.dToCrHt > 0.0)
                        {
                            description += ",Cr Ht: " + item.dFromCrHt.ToString() + "-" + item.dToCrHt.ToString();
                        }
                        if (item.dFromPavAng > 0.0 && item.dToPavAng > 0.0)
                        {
                            description += ",Pav Ang: " + item.dFromPavAng.ToString() + "-" + item.dToPavAng.ToString();
                        }
                        if (item.dFromPavHt > 0.0 && item.dToPavHt > 0.0)
                        {
                            description += ",Pav Ht: " + item.dFromPavHt.ToString() + "-" + item.dToPavHt.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.Table_Open))
                        {
                            description += ",Table Open: " + item.Table_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Crown_Open))
                        {
                            description += ",Crown Open: " + item.Crown_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Pav_Open))
                        {
                            description += ",Pav Open: " + item.Pav_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Girdle_Open))
                        {
                            description += ",Girdle Open: " + item.Girdle_Open;
                        }
                        /*if (!string.IsNullOrEmpty(item.FROMRAPAMOUNT)) { }
                        if (!string.IsNullOrEmpty(item.TORAPAMOUNT)) { }
                        if (!string.IsNullOrEmpty(item.FormName)) { }*/

                        if (!string.IsNullOrEmpty(description))
                        {
                            description = description.StartsWith(",") ? description.Remove(0, 1) : description;
                        }
                        else
                        {
                            description = "All Criteria";
                        }
                        item.sDescription = description;
                    }

                    return Ok(new ServiceResponse<UserSearchResponse>
                    {
                        Data = userSearchResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<UserSearchResponse>
                    {
                        Data = userSearchResponses,
                        Message = "No Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserSearchResponse>
                {
                    Data = new List<UserSearchResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Dashboard_GetUserSearch()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (userID > 0)
                    para.Add(db.CreateParam("iuserId", DbType.String, ParameterDirection.Input, userID));
                else
                    para.Add(db.CreateParam("iuserId", DbType.String, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("UserSearchDet_Select_userid_Dashboard", para.ToArray(), false);

                List<UserSearchResponse> userSearchResponses = new List<UserSearchResponse>();
                userSearchResponses = DataTableExtension.ToList<UserSearchResponse>(dt);

                if (userSearchResponses.Count > 0)
                {
                    string description = string.Empty;
                    foreach (var item in userSearchResponses)
                    {
                        description = string.Empty;
                        if (!string.IsNullOrEmpty(item.sShape))
                        {
                            description += ",<span class='dark'>Shape:</span> " + item.sShape;
                        }
                        if (!string.IsNullOrEmpty(item.sPointer))
                        {
                            description += ",<span class='dark'>Carat Weight:</span> " + item.sPointer;
                        }
                        if (item.dFromCts > 0.0 && item.dToCts > 0.0)
                        {
                            description += ",<span class='dark'>Carat Weight:</span> " + item.dFromCts.ToString() + "-" + item.dToCts.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.Color_Description))
                        {
                            description += ",<span class='dark'>Color:</span> " + item.Color_Description;
                        }
                        if (!string.IsNullOrEmpty(item.sClarity))
                        {
                            description += ",<span class='dark'>Clarity:</span> " + item.sClarity;
                        }
                        if (!string.IsNullOrEmpty(item.sCut))
                        {
                            description += ",<span class='dark'>Cut:</span> " + item.sCut;
                        }
                        if (!string.IsNullOrEmpty(item.sPolish))
                        {
                            description += ",<span class='dark'>Polish:</span> " + item.sPolish;
                        }
                        if (!string.IsNullOrEmpty(item.sSymm))
                        {
                            description += ",<span class='dark'>Sym:</span> " + item.sSymm;
                        }
                        if (!string.IsNullOrEmpty(item.sFls))
                        {
                            description += ",<span class='dark'>Fls:</span> " + item.sFls;
                        }
                        if (!string.IsNullOrEmpty(item.sShade))
                        {
                            description += ",<span class='dark'>BGM:</span> " + item.sShade;
                        }
                        if (!string.IsNullOrEmpty(item.location1))
                        {
                            description += ",<span class='dark'>Location:</span> " + item.location1;
                        }
                        if (!string.IsNullOrEmpty(item.sNatts))
                        {
                            description += ",<span class='dark'>Table Natts:</span> " + item.sNatts;
                        }
                        if (!string.IsNullOrEmpty(item.sCrownNatts))
                        {
                            description += ",<span class='dark'>Crown Natts:</span> " + item.sCrownNatts;
                        }
                        if (!string.IsNullOrEmpty(item.sLab))
                        {
                            description += ",<span class='dark'>Lab:</span> " + item.sLab;
                        }
                        if (!string.IsNullOrEmpty(item.sInclusion))
                        {
                            description += ",<span class='dark'>Table Inclusion:</span> " + item.sInclusion;
                        }
                        if (!string.IsNullOrEmpty(item.sCrownInclusion))
                        {
                            description += ",<span class='dark'>Crown Inclusion:</span> " + item.sCrownInclusion;
                        }
                        /*if (!string.IsNullOrEmpty(item.LUSTER)) { }*/
                        if (!string.IsNullOrEmpty(item.SkeyToSymbol))
                        {
                            description += ",<span class='dark'>Key To Symbol:</span> " + item.SkeyToSymbol;
                        }
                        if (item.dFromDisc > 0.0 && item.dToDisc > 0.0)
                        {
                            description += ",<span class='dark'>Discount:</span> " + item.dFromDisc.ToString() + "-" + item.dToDisc.ToString();
                        }
                        if (item.dFromPriceCts > 0.0 && item.dToPriceCts > 0.0)
                        {
                            description += ",<span class='dark'>Price/Cts USD:</span> " + item.dFromPriceCts.ToString() + "-" + item.dToPriceCts.ToString();
                        }
                        if (item.dFromNetPrice > 0.0 && item.dToNetPrice > 0.0)
                        {
                            description += ",<span class='dark'>Total Amount:</span> " + item.dFromNetPrice.ToString() + "-" + item.dToNetPrice.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.bImage.ToString()) || !string.IsNullOrEmpty(item.bHDMovie.ToString()))
                        {
                            description += ",<span class='dark'>Media:</span> " + (!string.IsNullOrEmpty(item.bImage.ToString()) ? "Image-" + item.bImage.ToString() : "")
                                + (!string.IsNullOrEmpty(item.bHDMovie.ToString()) ? "Video-" + item.bHDMovie.ToString() : "");
                        }
                        if (item.dFromLength > 0.0 && item.dToLength > 0.0)
                        {
                            description += ",<span class='dark'>Length:</span> " + item.dFromLength.ToString() + "-" + item.dToLength.ToString();
                        }
                        if (item.dFromWidth > 0.0 && item.dToWidth > 0.0)
                        {
                            description += ",<span class='dark'>Width:</span> " + item.dFromWidth.ToString() + "-" + item.dToWidth.ToString();
                        }
                        /* if (!string.IsNullOrEmpty(item.FROMDEPTH)) { }
                         if (!string.IsNullOrEmpty(item.TODEPTH)) { }*/
                        if (item.dFromDepthPer > 0.0 && item.dToDepthPer > 0.0)
                        {
                            description += ",<span class='dark'>Depth%</span>: " + item.dFromDepthPer.ToString() + "-" + item.dToDepthPer.ToString();
                        }
                        if (item.dFromTablePer > 0.0 && item.dToTablePer > 0.0)
                        {
                            description += ",<span class='dark'>Table%</span>: " + item.dFromTablePer.ToString() + "-" + item.dToTablePer.ToString();
                        }

                        /*  if (!string.IsNullOrEmpty(item.PROMOTION)) { }
                          if (!string.IsNullOrEmpty(item.SHAPECOLORPURITY)) { } */
                        if (item.dFromCrAng > 0.0 && item.dToCrAng > 0.0)
                        {
                            description += ",<span class='dark'>Cr Ang</span>: " + item.dFromCrAng.ToString() + "-" + item.dToCrAng.ToString();
                        }
                        if (item.dFromCrHt > 0.0 && item.dToCrHt > 0.0)
                        {
                            description += ",<span class='dark'>Cr Ht</span>: " + item.dFromCrHt.ToString() + "-" + item.dToCrHt.ToString();
                        }
                        if (item.dFromPavAng > 0.0 && item.dToPavAng > 0.0)
                        {
                            description += ",<span class='dark'>Pav Ang</span>: " + item.dFromPavAng.ToString() + "-" + item.dToPavAng.ToString();
                        }
                        if (item.dFromPavHt > 0.0 && item.dToPavHt > 0.0)
                        {
                            description += ",<span class='dark'>Pav Ht</span>: " + item.dFromPavHt.ToString() + "-" + item.dToPavHt.ToString();
                        }
                        if (!string.IsNullOrEmpty(item.Table_Open))
                        {
                            description += ",<span class='dark'>Table Open</span>: " + item.Table_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Crown_Open))
                        {
                            description += ",<span class='dark'>Crown Open</span>: " + item.Crown_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Pav_Open))
                        {
                            description += ",<span class='dark'>Pav Open</span>: " + item.Pav_Open;
                        }
                        if (!string.IsNullOrEmpty(item.Girdle_Open))
                        {
                            description += ",<span class='dark'>Girdle Open</span>: " + item.Girdle_Open;
                        }
                        /*if (!string.IsNullOrEmpty(item.FROMRAPAMOUNT)) { }
                        if (!string.IsNullOrEmpty(item.TORAPAMOUNT)) { }
                        if (!string.IsNullOrEmpty(item.FormName)) { }*/

                        if (!string.IsNullOrEmpty(description))
                        {
                            description = description.StartsWith(",") ? description.Remove(0, 1) : description;
                        }
                        else
                        {
                            description = "All Criteria";
                        }
                        item.sDescription = description;
                    }

                    return Ok(new ServiceResponse<UserSearchResponse>
                    {
                        Data = userSearchResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<UserSearchResponse>
                    {
                        Data = userSearchResponses,
                        Message = "No Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserSearchResponse>
                {
                    Data = new List<UserSearchResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult UserSaveSearch([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();

            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
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

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                searchDiamondsRequest.UserID = userID;

                if (searchDiamondsRequest.SearchID == "" || searchDiamondsRequest.SearchID == null || searchDiamondsRequest.SearchID.ToString() == "0")
                {
                    DAL.Stock objstock = new DAL.Stock();
                    DataTable UniqName = objstock.UserSearchDet_UniqueName(searchDiamondsRequest.SearchName, Convert.ToInt32(searchDiamondsRequest.UserID));
                    if (Convert.ToInt32(UniqName.Rows[0]["cnt"]) != 0)
                    {
                        return Ok(new ServiceResponse<CommonResponse>
                        {
                            Data = new List<CommonResponse>(),
                            Message = "Name already Exist",
                            Status = "0"
                        });
                    }
                }

                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(searchDiamondsRequest.SearchID))
                    para.Add(db.CreateParam("iSearchId", DbType.String, ParameterDirection.Input, searchDiamondsRequest.SearchID));
                else
                    para.Add(db.CreateParam("iSearchId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.SearchName))
                    para.Add(db.CreateParam("sSearchName", DbType.String, ParameterDirection.Input, searchDiamondsRequest.SearchName));
                else
                    para.Add(db.CreateParam("sSearchName", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("iUserId", DbType.String, ParameterDirection.Input, searchDiamondsRequest.UserID));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Shape))
                    para.Add(db.CreateParam("sShape", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Shape));
                else
                    para.Add(db.CreateParam("sShape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Lab))
                    para.Add(db.CreateParam("sLab", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Lab));
                else
                    para.Add(db.CreateParam("sLab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Color))
                    para.Add(db.CreateParam("sColor", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Color));
                else
                    para.Add(db.CreateParam("sColor", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Clarity))
                    para.Add(db.CreateParam("sClarity", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Clarity));
                else
                    para.Add(db.CreateParam("sClarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Cut))
                    para.Add(db.CreateParam("sCut", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Cut));
                else
                    para.Add(db.CreateParam("sCut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Polish))
                    para.Add(db.CreateParam("sPolish", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Polish));
                else
                    para.Add(db.CreateParam("sPolish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Symm))
                    para.Add(db.CreateParam("sSymm", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Symm));
                else
                    para.Add(db.CreateParam("sSymm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Fls))
                    para.Add(db.CreateParam("sFls", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Fls));
                else
                    para.Add(db.CreateParam("sFls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Pointer))
                    para.Add(db.CreateParam("sPointer", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Pointer));
                else
                    para.Add(db.CreateParam("sPointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Shade))
                    para.Add(db.CreateParam("sShade", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Shade));
                else
                    para.Add(db.CreateParam("sShade", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Natts))
                    para.Add(db.CreateParam("sNatts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Natts));
                else
                    para.Add(db.CreateParam("sNatts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Inclusion))
                    para.Add(db.CreateParam("sInclusion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Inclusion));
                else
                    para.Add(db.CreateParam("sInclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CertiNo))
                    para.Add(db.CreateParam("sCertiNo", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CertiNo));
                else
                    para.Add(db.CreateParam("sCertiNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.StoneID))
                    para.Add(db.CreateParam("sRefNo", DbType.String, ParameterDirection.Input, searchDiamondsRequest.StoneID));
                else
                    para.Add(db.CreateParam("sRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCts))
                    para.Add(db.CreateParam("dFromCts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FromCts));
                else
                    para.Add(db.CreateParam("dFromCts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCts))
                    para.Add(db.CreateParam("dToCts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToCts));
                else
                    para.Add(db.CreateParam("dToCts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDisc))
                    para.Add(db.CreateParam("dFromDisc", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormDisc));
                else
                    para.Add(db.CreateParam("dFromDisc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDisc))
                    para.Add(db.CreateParam("dToDisc", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToDisc));
                else
                    para.Add(db.CreateParam("dToDisc", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("dFromRapAmount", DbType.String, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dToRapAmount", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormNetAmt))
                    para.Add(db.CreateParam("dFromNetPrice", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormNetAmt));
                else
                    para.Add(db.CreateParam("dFromNetPrice", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToNetAmt))
                    para.Add(db.CreateParam("dToNetPrice", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToNetAmt));
                else
                    para.Add(db.CreateParam("dToNetPrice", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormLength))
                    para.Add(db.CreateParam("dFromLength", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormLength));
                else
                    para.Add(db.CreateParam("dFromLength", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToLength))
                    para.Add(db.CreateParam("dToLength", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToLength));
                else
                    para.Add(db.CreateParam("dToLength", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormWidth))
                    para.Add(db.CreateParam("dFromWidth", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormWidth));
                else
                    para.Add(db.CreateParam("dFromWidth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToWidth))
                    para.Add(db.CreateParam("dToWidth", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToWidth));
                else
                    para.Add(db.CreateParam("dToWidth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDepth))
                    para.Add(db.CreateParam("dFromDepth", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormDepth));
                else
                    para.Add(db.CreateParam("dFromDepth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDepth))
                    para.Add(db.CreateParam("dToDepth", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToDepth));
                else
                    para.Add(db.CreateParam("dToDepth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDepthPer))
                    para.Add(db.CreateParam("dFromDepthPer", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormDepthPer));
                else
                    para.Add(db.CreateParam("dFromDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDepthPer))
                    para.Add(db.CreateParam("dToDepthPer", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToDepthPer));
                else
                    para.Add(db.CreateParam("dToDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormTablePer))
                    para.Add(db.CreateParam("dFromTablePer", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormTablePer));
                else
                    para.Add(db.CreateParam("dFromTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToTablePer))
                    para.Add(db.CreateParam("dToTablePer", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToTablePer));
                else
                    para.Add(db.CreateParam("dToTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCrownAngle))
                    para.Add(db.CreateParam("dFromCrAng", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FromCrownAngle));
                else
                    para.Add(db.CreateParam("dFromCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCrownAngle))
                    para.Add(db.CreateParam("dToCrAng", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToCrownAngle));
                else
                    para.Add(db.CreateParam("dToCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCrownHeight))
                    para.Add(db.CreateParam("dFromCrHt", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FromCrownHeight));
                else
                    para.Add(db.CreateParam("dFromCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCrownHeight))
                    para.Add(db.CreateParam("dToCrHt", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToCrownHeight));
                else
                    para.Add(db.CreateParam("dToCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromPavAngle))
                    para.Add(db.CreateParam("dFromPavAng", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FromPavAngle));
                else
                    para.Add(db.CreateParam("dFromPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPavAngle))
                    para.Add(db.CreateParam("dToPavAng", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToPavAngle));
                else
                    para.Add(db.CreateParam("dToPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromPavHeight))
                    para.Add(db.CreateParam("dFromPavHt", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FromPavHeight));
                else
                    para.Add(db.CreateParam("dFromPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPavHeight))
                    para.Add(db.CreateParam("dToPavHt", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToPavHeight));
                else
                    para.Add(db.CreateParam("dToPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.PageNo))
                    para.Add(db.CreateParam("iPages", DbType.String, ParameterDirection.Input, searchDiamondsRequest.PageNo));
                else
                    para.Add(db.CreateParam("iPages", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormPricePerCts))
                    para.Add(db.CreateParam("dFromPriceCts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormPricePerCts));
                else
                    para.Add(db.CreateParam("dFromPriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPricePerCts))
                    para.Add(db.CreateParam("dToPriceCts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ToPricePerCts));
                else
                    para.Add(db.CreateParam("dToPriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CrownNatts))
                    para.Add(db.CreateParam("sCrownNatts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CrownNatts));
                else
                    para.Add(db.CreateParam("sCrownNatts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CrownInclusion))
                    para.Add(db.CreateParam("sCrownInclusion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CrownInclusion));
                else
                    para.Add(db.CreateParam("sCrownInclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Luster))
                    para.Add(db.CreateParam("sLuster", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Luster));
                else
                    para.Add(db.CreateParam("sLuster", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Location))
                    para.Add(db.CreateParam("sSupplLocation", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Location));
                else
                    para.Add(db.CreateParam("sSupplLocation", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Black))
                    para.Add(db.CreateParam("iBlack", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Black));
                else
                    para.Add(db.CreateParam("iBlack", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.BGM))
                    para.Add(db.CreateParam("iBgm", DbType.String, ParameterDirection.Input, searchDiamondsRequest.BGM));
                else
                    para.Add(db.CreateParam("iBgm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.KeyToSymbol))
                    para.Add(db.CreateParam("keytosymbol", DbType.String, ParameterDirection.Input, searchDiamondsRequest.KeyToSymbol));
                else
                    para.Add(db.CreateParam("keytosymbol", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.HasImage))
                    para.Add(db.CreateParam("bImage", DbType.String, ParameterDirection.Input, searchDiamondsRequest.HasImage));
                else
                    para.Add(db.CreateParam("bImage", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.HasHDMovie))
                    para.Add(db.CreateParam("bHDMovie", DbType.String, ParameterDirection.Input, searchDiamondsRequest.HasHDMovie));
                else
                    para.Add(db.CreateParam("bHDMovie", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ColorType))
                    para.Add(db.CreateParam("ColorType", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ColorType));
                else
                    para.Add(db.CreateParam("ColorType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Intensity))
                    para.Add(db.CreateParam("Intensity", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Intensity));
                else
                    para.Add(db.CreateParam("Intensity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Overtone))
                    para.Add(db.CreateParam("Overtone", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Overtone));
                else
                    para.Add(db.CreateParam("Overtone", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Fancy_Color))
                    para.Add(db.CreateParam("Fancy_Color", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Fancy_Color));
                else
                    para.Add(db.CreateParam("Fancy_Color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Table_Open))
                    para.Add(db.CreateParam("Table_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Table_Open));
                else
                    para.Add(db.CreateParam("Table_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Crown_Open))
                    para.Add(db.CreateParam("Crown_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Crown_Open));
                else
                    para.Add(db.CreateParam("Crown_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Pav_Open))
                    para.Add(db.CreateParam("Pav_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Pav_Open));
                else
                    para.Add(db.CreateParam("Pav_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Girdle_Open))
                    para.Add(db.CreateParam("Girdle_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Girdle_Open));
                else
                    para.Add(db.CreateParam("Girdle_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dt = db.ExecuteSP("UserSearchDet_InsertNew", para.ToArray(), false);

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
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult UserSearchDelete([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();

            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsRequest>
                {
                    Data = new List<SearchDiamondsRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            CommonResponse resp = new CommonResponse();
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (searchDiamondsRequest.SearchID != null)
                    para.Add(db.CreateParam("iSearchId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(searchDiamondsRequest.SearchID)));
                else
                    para.Add(db.CreateParam("iSearchId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                db.ExecuteSP("UserSearchDet_Delete", para.ToArray(), false);

                resp.Status = "SUCCESS";
                resp.Message = "User Search Delete Sucessfully";
                resp.Error = "";
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
        public IHttpActionResult EmailStones([FromBody] JObject data)
        {
            EmailStonesRequest emailStonesRequest = new EmailStonesRequest();

            try
            {
                emailStonesRequest = JsonConvert.DeserializeObject<EmailStonesRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsRequest>
                {
                    Data = new List<SearchDiamondsRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userID)));
                DataTable _dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);
                string iUserType = "";
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    iUserType = _dt.Rows[0]["iUserType"].ToString();
                }

                MailMessage xloMail = new MailMessage();
                SmtpClient xloSmtp = new SmtpClient();
                try
                {

                    xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Bhavyam Diam");
                    xloMail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["BCCEmail"]));
                    if (emailStonesRequest.ToAddress.EndsWith(","))
                        emailStonesRequest.ToAddress = emailStonesRequest.ToAddress.Remove(emailStonesRequest.ToAddress.Length - 1);
                    xloMail.To.Add(emailStonesRequest.ToAddress);
                    xloMail.Subject = "Stone Selection";
                    xloMail.IsBodyHtml = false;
                    AlternateView av = AlternateView.CreateAlternateViewFromString(emailStonesRequest.Comments, null, "");
                    xloMail.AlternateViews.Add(av);

                    SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();
                    searchDiamondsRequest.StoneID = emailStonesRequest.StoneID;
                    searchDiamondsRequest.UserID = userID;
                    searchDiamondsRequest.PageNo = "0";
                    searchDiamondsRequest.FormName = emailStonesRequest.FormName;
                    searchDiamondsRequest.ActivityType = emailStonesRequest.ActivityType;
                    DataTable dt = SearchStockInner(searchDiamondsRequest, false);
                    //DataTable dt = SearchDiamondsRequest(emailStonesRequest.StoneID, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "0", userID.ToString(), "");
                    dt.DefaultView.RowFilter = "Sr IS NOT NULL";
                    dt = dt.DefaultView.ToTable();

                    string fileName = "";
                    string realPath = "";
                    if (dt.Rows.Count > 0)
                    {
                        if (ConfigurationManager.AppSettings["Location"] == "M")
                        {
                            if (ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
                                fileName = ExportToExcelEpPlus(dt);
                            else
                                fileName = ExportToExcelOpenXml(dt);
                        }
                        else
                        {
                            if (emailStonesRequest.FormName == "New Arrival")
                                fileName = "NewArrival " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                            else if (emailStonesRequest.FormName == "Revised Price")
                                fileName = "RevisedPrice " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                            else if (emailStonesRequest.FormName == "My Wishlist")
                                fileName = "WishList " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                            else if (emailStonesRequest.IsAll == 1)
                                fileName = "Bhavyam Diam Inventory " + Lib.Models.Common.GetHKTime().ToString("dd.MM.yy");
                            else if (emailStonesRequest.IsAll == 0)
                                fileName = "Bhavyam Diam Selection " + Lib.Models.Common.GetHKTime().ToString("dd.MM.yy");

                            string _path = ConfigurationManager.AppSettings["data"];
                            realPath = HostingEnvironment.MapPath("~/ExcelFile/");
                            string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                            EpExcelExport.CreateExcel(dt, realPath, realPath + fileName + ".xlsx", _livepath, emailStonesRequest.ColorType, iUserType);
                        }

                        ContentType contentType = new ContentType();
                        contentType.MediaType = MediaTypeNames.Application.Octet;
                        contentType.Name = fileName + ".xlsx";
                        Attachment attachFile = new Attachment(realPath + fileName + ".xlsx", contentType);
                        xloMail.Attachments.Add(attachFile);
                    }

                    xloSmtp.Send(xloMail);

                    xloMail.Attachments.Dispose();
                    xloMail.AlternateViews.Dispose();
                    xloMail.Dispose();

                    if (fileName.Length > 0)
                        if (System.IO.File.Exists(fileName))
                            System.IO.File.Delete(fileName);

                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    resp.Status = "0";
                    resp.Message = ex.ToString();
                    resp.Error = ex.Message;

                    return Ok(resp);
                }

                resp.Status = "1";
                resp.Message = "Mail sent successfully.";
                resp.Error = "";

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
        public IHttpActionResult MyWishlistEmailStones([FromBody]JObject data)
        {
            ViewWishListRequest viewWishListRequest = new ViewWishListRequest();
            try
            {
                viewWishListRequest = JsonConvert.DeserializeObject<ViewWishListRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ViewCartResponse>
                {
                    Data = new List<ViewCartResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                viewWishListRequest.UserID = userID;
                viewWishListRequest.Comments = (viewWishListRequest.Comments == null ? "" : viewWishListRequest.Comments);

                MailMessage xloMail = new MailMessage();
                SmtpClient xloSmtp = new SmtpClient();
                try
                {

                    if (ConfigurationManager.AppSettings["Location"] == "H")
                        xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                    else
                        xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Shairu Gems");

                    xloMail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["BCCEmail"]));
                    if (viewWishListRequest.ToAddress.EndsWith(","))
                        viewWishListRequest.ToAddress = viewWishListRequest.ToAddress.Remove(viewWishListRequest.ToAddress.Length - 1);
                    xloMail.To.Add(viewWishListRequest.ToAddress);
                    xloMail.Subject = "Stone Selection";
                    xloMail.IsBodyHtml = false;
                    AlternateView av = AlternateView.CreateAlternateViewFromString(viewWishListRequest.Comments, null, "");
                    xloMail.AlternateViews.Add(av);


                    DataTable dtData = ViewWishListInner(viewWishListRequest);
                    dtData.DefaultView.RowFilter = "stone_ref_no IS NOT NULL";
                    dtData = dtData.DefaultView.ToTable();

                    SearchSummary searchSummary = new SearchSummary();
                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        DataRow[] dra = dtData.Select("VSR IS NULL");
                        searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["stone_ref_no"]);
                        searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["PCS"]);
                        searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["Cts"]);
                        searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble((dra[0]["RAP_AMOUNT"].ToString() != "" && dra[0]["RAP_AMOUNT"].ToString() != null ? dra[0]["RAP_AMOUNT"] : "0"));
                        searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["NET_AMOUNT"]);
                        searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(dra[0]["PRICE_PER_CTS"]);
                        searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0"));
                    }
                    dtData.DefaultView.RowFilter = "VSR IS NOT NULL";
                    dtData = dtData.DefaultView.ToTable();

                    string fileName = "";
                    string realPath = "";
                    if (dtData.Rows.Count > 0)
                    {
                        if (ConfigurationManager.AppSettings["Location"] == "M")
                        {
                            if (ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
                                fileName = ExportToExcelEpPlus(dtData);
                            else
                                fileName = ExportToExcelOpenXml(dtData);
                        }
                        else
                        {
                            fileName = "WishList " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");

                            string _path = ConfigurationManager.AppSettings["data"];
                            realPath = HostingEnvironment.MapPath("~/ExcelFile/");
                            string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                            EpExcelExport.CreateWishListExcel(dtData, realPath, realPath + fileName + ".xlsx", _livepath, viewWishListRequest.IsAssistBy);
                        }

                        ContentType contentType = new ContentType();
                        contentType.MediaType = MediaTypeNames.Application.Octet;
                        contentType.Name = fileName + ".xlsx";
                        Attachment attachFile = new Attachment(realPath + fileName + ".xlsx", contentType);
                        xloMail.Attachments.Add(attachFile);
                    }

                    xloSmtp.Send(xloMail);

                    xloMail.Attachments.Dispose();
                    xloMail.AlternateViews.Dispose();
                    xloMail.Dispose();

                    if (fileName.Length > 0)
                        if (System.IO.File.Exists(fileName))
                            System.IO.File.Delete(fileName);

                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    resp.Status = "0";
                    resp.Message = ex.ToString();
                    resp.Error = ex.Message;

                    return Ok(resp);
                }

                resp.Status = "1";
                resp.Message = "Mail sent successfully.";
                resp.Error = "";

                return Ok(resp);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Something Went wrong.\nPlease try again later");
            }
        }
        [NonAction]
        private DataTable ViewWishListInner(ViewWishListRequest ViewWishList)
        {
            try
            {
                Database db = new Database();
                List<System.Data.IDbDataParameter> para;
                para = new List<System.Data.IDbDataParameter>();

                if (string.IsNullOrEmpty(ViewWishList.RefNoCerti))
                    para.Add(db.CreateParam("RefNoCerti", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("RefNoCerti", DbType.String, ParameterDirection.Input, ViewWishList.RefNoCerti));

                if (ViewWishList.UserID <= 0)
                    para.Add(db.CreateParam("iUserid", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("iUserid", DbType.String, ParameterDirection.Input, ViewWishList.UserID));

                //if (string.IsNullOrEmpty(ViewWishList.TUserID))
                //    para.Add(db.CreateParam("@TUserid", DbType.String, ParameterDirection.Input, DBNull.Value));
                //else
                //    para.Add(db.CreateParam("@TUserid", DbType.String, ParameterDirection.Input, ViewWishList.TUserID));

                if (string.IsNullOrEmpty(ViewWishList.RefNo))
                    para.Add(db.CreateParam("refno", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("refno", DbType.String, ParameterDirection.Input, ViewWishList.RefNo));

                if (string.IsNullOrEmpty(ViewWishList.CompName))
                    para.Add(db.CreateParam("scompName", DbType.String, ParameterDirection.Input, DBNull.Value));
                else
                    para.Add(db.CreateParam("scompName", DbType.String, ParameterDirection.Input, ViewWishList.CompName));

                if (!string.IsNullOrEmpty(ViewWishList.Shape))
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, ViewWishList.Shape.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Pointer))
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, ViewWishList.Pointer.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Color))
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, ViewWishList.Color.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Clarity))
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, ViewWishList.Clarity.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Cut))
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, ViewWishList.Cut.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Polish))
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, ViewWishList.Polish.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Symm))
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, ViewWishList.Symm.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Fls))
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, ViewWishList.Fls.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Lab))
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, ViewWishList.Lab.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Location))
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, ViewWishList.Location.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.ToDate))
                    para.Add(db.CreateParam("p_for_ToDate", DbType.String, ParameterDirection.Input, ViewWishList.ToDate));
                else
                    para.Add(db.CreateParam("p_for_ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.FromDate))
                    para.Add(db.CreateParam("p_for_FromDate", DbType.String, ParameterDirection.Input, ViewWishList.FromDate));
                else
                    para.Add(db.CreateParam("p_for_FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.Status))
                    para.Add(db.CreateParam("p_for_status", DbType.String, ParameterDirection.Input, ViewWishList.Status));
                else
                    para.Add(db.CreateParam("p_for_status", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.PageSize))
                    para.Add(db.CreateParam("p_for_pageSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(ViewWishList.PageSize)));
                else
                    para.Add(db.CreateParam("p_for_pageSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ViewWishList.iUserid_certi_no))
                    para.Add(db.CreateParam("p_for_iUserid_certi_no", DbType.String, ParameterDirection.Input, ViewWishList.iUserid_certi_no));
                else
                    para.Add(db.CreateParam("p_for_iUserid_certi_no", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("p_for_page", DbType.String, ParameterDirection.Input, ViewWishList.PageNo));
                para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, ViewWishList.OrderBy));
                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, ViewWishList.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, ViewWishList.ActivityType));
                para.Add(db.CreateParam("SubUser", DbType.String, ParameterDirection.Input, ViewWishList.SubUser));

                DataTable dt = db.ExecuteSP("IPD_get_wishlist_Sunrise", para.ToArray(), false);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[HttpPost]
        //public IHttpActionResult CartEmailStones([FromBody]JObject data)
        //{
        //    EmailStonesRequest emailStonesRequest = new EmailStonesRequest();

        //    try
        //    {
        //        emailStonesRequest = JsonConvert.DeserializeObject<EmailStonesRequest>(data.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        DAL.Common.InsertErrorLog(ex, null, Request);
        //        return Ok("Input Parameters are not in the proper format");
        //    }

        //    try
        //    {
        //        CommonResponse resp = new CommonResponse();
        //        int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

        //        MailMessage xloMail = new MailMessage();
        //        SmtpClient xloSmtp = new SmtpClient();
        //        try
        //        {

        //            if (ConfigurationManager.AppSettings["Location"] == "H")
        //                xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
        //            else
        //                xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Shairu Gems");
        //            //xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
        //            xloMail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["BCCEmail"]));
        //            if (emailStonesRequest.ToAddress.EndsWith(","))
        //                emailStonesRequest.ToAddress = emailStonesRequest.ToAddress.Remove(emailStonesRequest.ToAddress.Length - 1);
        //            xloMail.To.Add(emailStonesRequest.ToAddress);
        //            xloMail.Subject = "Stone Selection";
        //            xloMail.IsBodyHtml = false;
        //            AlternateView av = AlternateView.CreateAlternateViewFromString(emailStonesRequest.Comments, null, "");
        //            xloMail.AlternateViews.Add(av);

        //            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();
        //            searchDiamondsRequest.StoneID = emailStonesRequest.StoneID;
        //            searchDiamondsRequest.UserID = userID;
        //            searchDiamondsRequest.PageNo = "0";
        //            DataTable dt = SearchStockInner(searchDiamondsRequest, false);
        //            //DataTable dt = SearchDiamondsRequest(emailStonesRequest.StoneID, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "0", userID.ToString(), "");
        //            dt.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
        //            dt = dt.DefaultView.ToTable();

        //            string fileName = "";
        //            string realPath = "";
        //            if (dt.Rows.Count > 0)
        //            {
        //                if (ConfigurationManager.AppSettings["Location"] == "M")
        //                {
        //                    if (ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
        //                        fileName = ExportToExcelEpPlus(dt);
        //                    else
        //                        fileName = ExportToExcelOpenXml(dt);
        //                }
        //                else
        //                {
        //                    fileName = "Stone_Selection_" + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
        //                    string _path = ConfigurationManager.AppSettings["data"];
        //                    realPath = HostingEnvironment.MapPath("~/ExcelFile/");
        //                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

        //                    EpExcelExport.CreateCartExcel(dt, realPath, realPath + fileName + ".xlsx", _livepath, emailStonesRequest.isAdmin, emailStonesRequest.isEmp);
        //                }

        //                ContentType contentType = new ContentType();
        //                contentType.MediaType = MediaTypeNames.Application.Octet;
        //                contentType.Name = fileName + ".xlsx";
        //                Attachment attachFile = new Attachment(realPath + fileName + ".xlsx", contentType);
        //                xloMail.Attachments.Add(attachFile);
        //            }

        //            xloSmtp.Send(xloMail);

        //            xloMail.Attachments.Dispose();
        //            xloMail.AlternateViews.Dispose();
        //            xloMail.Dispose();

        //            if (fileName.Length > 0)
        //                if (System.IO.File.Exists(fileName))
        //                    System.IO.File.Delete(fileName);

        //        }
        //        catch (Exception ex)
        //        {
        //            DAL.Common.InsertErrorLog(ex, null, Request);
        //            resp.Status = "0";
        //            resp.Message = ex.ToString();
        //            resp.Error = ex.Message;

        //            return Ok(resp);
        //        }

        //        resp.Status = "1";
        //        resp.Message = "Mail sent successfully.";
        //        resp.Error = "";

        //        return Ok(resp);
        //    }
        //    catch (Exception ex)
        //    {
        //        DAL.Common.InsertErrorLog(ex, null, Request);
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public IHttpActionResult CartEmailStones([FromBody] JObject data)
        {
            ViewCartRequest viewCartRequest = new ViewCartRequest();
            try
            {
                viewCartRequest = JsonConvert.DeserializeObject<ViewCartRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ViewCartResponse>
                {
                    Data = new List<ViewCartResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                int transID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == ServiceConstants.SessionTransID).FirstOrDefault().Value);

                MailMessage xloMail = new MailMessage();
                SmtpClient xloSmtp = new SmtpClient();
                try
                {
                    if (viewCartRequest.Comments == null)
                        viewCartRequest.Comments = "";
                    xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Bhavyam Diam"); 
                    xloMail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["BCCEmail"]));
                    if (viewCartRequest.ToAddress.EndsWith(","))
                        viewCartRequest.ToAddress = viewCartRequest.ToAddress.Remove(viewCartRequest.ToAddress.Length - 1);
                    xloMail.To.Add(viewCartRequest.ToAddress);
                    xloMail.Subject = "Stone Selection";
                    xloMail.IsBodyHtml = false;
                    AlternateView av = AlternateView.CreateAlternateViewFromString(viewCartRequest.Comments, null, "");
                    xloMail.AlternateViews.Add(av);

                    DataTable dt = ViewCartInner(viewCartRequest, userID, transID);

                    dt.DefaultView.RowFilter = "sr IS NOT NULL";
                    dt = dt.DefaultView.ToTable();

                    string fileName = "";
                    string realPath = "";
                    if (dt.Rows.Count > 0)
                    {
                        if (ConfigurationManager.AppSettings["Location"] == "M")
                        {
                            if (ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
                                fileName = ExportToExcelEpPlus(dt);
                            else
                                fileName = ExportToExcelOpenXml(dt);
                        }
                        else
                        {
                            fileName = "MyCart " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                            string _path = ConfigurationManager.AppSettings["data"];
                            realPath = HostingEnvironment.MapPath("~/ExcelFile/");
                            string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                            EpExcelExport.CreateCartExcel(dt, realPath, realPath + fileName + ".xlsx", _livepath, viewCartRequest.isAdmin, viewCartRequest.isEmp);
                        }

                        ContentType contentType = new ContentType();
                        contentType.MediaType = MediaTypeNames.Application.Octet;
                        contentType.Name = fileName + ".xlsx";
                        Attachment attachFile = new Attachment(realPath + fileName + ".xlsx", contentType);
                        xloMail.Attachments.Add(attachFile);
                    }

                    xloSmtp.Send(xloMail);

                    xloMail.Attachments.Dispose();
                    xloMail.AlternateViews.Dispose();
                    xloMail.Dispose();

                    if (fileName.Length > 0)
                        if (System.IO.File.Exists(fileName))
                            System.IO.File.Delete(fileName);

                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    resp.Status = "0";
                    resp.Message = ex.ToString();
                    resp.Error = ex.Message;

                    return Ok(resp);
                }

                resp.Status = "1";
                resp.Message = "Mail sent successfully.";
                resp.Error = "";

                return Ok(resp);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult PairEmailStones([FromBody] JObject data)
        {
            EmailAllStonesRequest pairRequest = new EmailAllStonesRequest();
            try
            {
                pairRequest = JsonConvert.DeserializeObject<EmailAllStonesRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0",
                    Error = ex.StackTrace
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userID)));
                DataTable _dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);
                string iUserType = "";
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    iUserType = _dt.Rows[0]["iUserType"].ToString();
                }

                MailMessage xloMail = new MailMessage();
                SmtpClient xloSmtp = new SmtpClient();
                try
                {
                    if (ConfigurationManager.AppSettings["Location"] == "H")
                        xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                    else
                        xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Shairu Gems");
                    xloMail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["BCCEmail"]));
                    if (pairRequest.ToAddress.EndsWith(","))
                        pairRequest.ToAddress = pairRequest.ToAddress.Remove(pairRequest.ToAddress.Length - 1);
                    xloMail.To.Add(pairRequest.ToAddress);
                    xloMail.Subject = "Stone Selection";
                    xloMail.IsBodyHtml = false;
                    AlternateView av = AlternateView.CreateAlternateViewFromString(pairRequest.Comments, null, "");
                    xloMail.AlternateViews.Add(av);

                    DataTable dt = PairSearchInner(pairRequest.SearchCriteria, userID, "Y");
                    dt.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                    dt = dt.DefaultView.ToTable();

                    dt.DefaultView.RowFilter = "iSr IS NOT NULL";
                    dt = dt.DefaultView.ToTable();

                    string fileName = "";
                    string realPath = "";
                    if (dt.Rows.Count > 0)
                    {
                        if (ConfigurationManager.AppSettings["Location"] == "M")
                        {
                            if (ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
                                fileName = ExportToExcelEpPlus(dt);
                            else
                                fileName = ExportToExcelOpenXml(dt);
                        }
                        else
                        {
                            fileName = "PairSearch " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                            string _path = ConfigurationManager.AppSettings["data"];
                            realPath = HostingEnvironment.MapPath("~/ExcelFile/");
                            string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                            EpExcelExport.CreateExcel(dt, realPath, realPath + fileName + ".xlsx", _livepath, "", iUserType);
                        }

                        ContentType contentType = new ContentType();
                        contentType.MediaType = MediaTypeNames.Application.Octet;
                        contentType.Name = fileName + ".xlsx";
                        Attachment attachFile = new Attachment(realPath + fileName + ".xlsx", contentType);
                        xloMail.Attachments.Add(attachFile);
                    }

                    xloSmtp.Send(xloMail);

                    xloMail.Attachments.Dispose();
                    xloMail.AlternateViews.Dispose();
                    xloMail.Dispose();

                    if (fileName.Length > 0)
                        if (System.IO.File.Exists(fileName))
                            System.IO.File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    resp.Status = "0";
                    resp.Message = ex.ToString();
                    resp.Error = ex.Message;

                    return Ok(resp);
                }

                resp.Status = "1";
                resp.Message = "Mail sent successfully.";
                resp.Error = "";

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
        public IHttpActionResult EmailAllStones([FromBody] JObject data)
        {
            EmailAllStonesRequest emailAllStonesRequest = new EmailAllStonesRequest();

            try
            {
                emailAllStonesRequest = JsonConvert.DeserializeObject<EmailAllStonesRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userID)));
                DataTable _dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);
                string iUserType = "";
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    iUserType = _dt.Rows[0]["iUserType"].ToString();
                }

                MailMessage xloMail = new MailMessage();
                SmtpClient xloSmtp = new SmtpClient();
                try
                {

                    xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Bhavyam Diam");
                    //xloMail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["BCCEmail"]));
                    if (emailAllStonesRequest.ToAddress.EndsWith(","))
                        emailAllStonesRequest.ToAddress = emailAllStonesRequest.ToAddress.Remove(emailAllStonesRequest.ToAddress.Length - 1);

                    xloMail.To.Add(emailAllStonesRequest.ToAddress);
                    xloMail.Subject = "Stone Selection";
                    xloMail.IsBodyHtml = false;
                    AlternateView av = AlternateView.CreateAlternateViewFromString(emailAllStonesRequest.Comments, null, "");
                    xloMail.AlternateViews.Add(av);

                    DataTable dt = new DataTable();
                    emailAllStonesRequest.SearchCriteria.UserID = userID;
                    emailAllStonesRequest.SearchCriteria.PageNo = "0";

                    if (emailAllStonesRequest.IsRevised != null && emailAllStonesRequest.IsRevised == true)
                    {
                        emailAllStonesRequest.SearchCriteria.ReviseStockFlag = "1";
                    }
                    dt = SearchStockInner(emailAllStonesRequest.SearchCriteria, false);
                    //DataTable dt = SearchDiamondsInner(emailAllStonesRequest.SearchCriteria.StoneID, emailAllStonesRequest.SearchCriteria.CertiNo, emailAllStonesRequest.SearchCriteria.Shape, emailAllStonesRequest.SearchCriteria.Pointer, emailAllStonesRequest.SearchCriteria.Color,
                    //    emailAllStonesRequest.SearchCriteria.Clarity, emailAllStonesRequest.SearchCriteria.Cut, emailAllStonesRequest.SearchCriteria.Polish, emailAllStonesRequest.SearchCriteria.Symm, emailAllStonesRequest.SearchCriteria.Fls, emailAllStonesRequest.SearchCriteria.Lab, emailAllStonesRequest.SearchCriteria.Luster, emailAllStonesRequest.SearchCriteria.Location, emailAllStonesRequest.SearchCriteria.Inclusion, emailAllStonesRequest.SearchCriteria.Natts, 
                    //    emailAllStonesRequest.SearchCriteria.Shade, emailAllStonesRequest.SearchCriteria.FromCts,
                    //    emailAllStonesRequest.SearchCriteria.ToCts, emailAllStonesRequest.SearchCriteria.FormDisc, emailAllStonesRequest.SearchCriteria.ToDisc, emailAllStonesRequest.SearchCriteria.FormPricePerCts, emailAllStonesRequest.SearchCriteria.ToPricePerCts, emailAllStonesRequest.SearchCriteria.FormNetAmt, emailAllStonesRequest.SearchCriteria.ToNetAmt, emailAllStonesRequest.SearchCriteria.FormDepth, emailAllStonesRequest.SearchCriteria.ToDepth,
                    //    emailAllStonesRequest.SearchCriteria.FormLength, emailAllStonesRequest.SearchCriteria.ToLength, emailAllStonesRequest.SearchCriteria.FormWidth, emailAllStonesRequest.SearchCriteria.ToWidth, emailAllStonesRequest.SearchCriteria.FormDepthPer, emailAllStonesRequest.SearchCriteria.ToDepthPer, emailAllStonesRequest.SearchCriteria.FormTablePer, emailAllStonesRequest.SearchCriteria.ToTablePer,
                    //    emailAllStonesRequest.SearchCriteria.HasImage, emailAllStonesRequest.SearchCriteria.HasHDMovie, emailAllStonesRequest.SearchCriteria.IsPromotion, "", "", emailAllStonesRequest.SearchCriteria.CrownInclusion, emailAllStonesRequest.SearchCriteria.CrownNatts, "0", emailAllStonesRequest.SearchCriteria.UserID.ToString(),"");
                    dt.DefaultView.RowFilter = "Sr IS NOT NULL";
                    dt = dt.DefaultView.ToTable();

                    string fileName = "";
                    string realPath = "";
                    if (dt.Rows.Count > 0)
                    {
                        if (ConfigurationManager.AppSettings["Location"] == "M")
                        {
                            if (ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
                                fileName = ExportToExcelEpPlus(dt);
                            else
                                fileName = ExportToExcelOpenXml(dt);
                        }
                        else
                        {

                            if (emailAllStonesRequest.SearchCriteria.FormName == "New Arrival")
                                fileName = "NewArrival " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                            else if (emailAllStonesRequest.SearchCriteria.FormName == "Revised Price")
                                fileName = "RevisedPrice " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                            else if (emailAllStonesRequest.SearchCriteria.FormName == "My Wishlist")
                                fileName = "WishList " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                            else if (emailAllStonesRequest.IsAll == 1)
                                fileName = "Bhavyam Diam Inventory " + Lib.Models.Common.GetHKTime().ToString("dd.MM.yy");
                            else if (emailAllStonesRequest.IsAll == 0)
                                fileName = "Bhavyam Diam Selection " + Lib.Models.Common.GetHKTime().ToString("dd.MM.yy");

                            string _path = ConfigurationManager.AppSettings["data"];
                            realPath = System.Web.Hosting.HostingEnvironment.MapPath("~/ExcelFile/");
                            string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                            EpExcelExport.CreateExcel(dt, realPath, realPath + fileName + ".xlsx", _livepath, emailAllStonesRequest.SearchCriteria.ColorType, iUserType);
                        }

                        ContentType contentType = new System.Net.Mime.ContentType();
                        contentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Octet;
                        contentType.Name = fileName + ".xlsx";
                        Attachment attachFile = new Attachment(realPath + fileName + ".xlsx", contentType);
                        xloMail.Attachments.Add(attachFile);
                    }

                    xloSmtp.Timeout = 500000;
                    xloSmtp.Send(xloMail);

                    xloMail.Attachments.Dispose();
                    xloMail.AlternateViews.Dispose();
                    xloMail.Dispose();

                    if (fileName.Length > 0)
                        if (System.IO.File.Exists(fileName))
                            System.IO.File.Delete(fileName);

                }
                catch (Exception ex)
                {
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    resp.Status = "0";
                    resp.Message = ex.ToString();
                    resp.Error = ex.Message;

                    return Ok(resp);
                }

                resp.Status = "1";
                resp.Message = "Mail sent successfully.";
                resp.Error = "";

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
        public IHttpActionResult DownloadStockMedia([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();

            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsRequest>
                {
                    Data = new List<SearchDiamondsRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                searchDiamondsRequest.UserID = userID;

                DataTable dtData = new DataTable();
                if (searchDiamondsRequest.DownloadMedia.ToLower() != "pdf")
                {
                    dtData = SearchStockInner(searchDiamondsRequest);
                }
                else
                {
                    dtData = SearchStockInner(searchDiamondsRequest, true, true);
                }

                string subFolderZipName = searchDiamondsRequest.DownloadMedia.ToLower() == "image" ? "StoneImages" :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "video" ? "StoneVideos" :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "certificate" ? "StoneCertificates" : "";
                subFolderZipName += DAL.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                string _path = ConfigurationManager.AppSettings["data"];
                string _realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _downloadURL = searchDiamondsRequest.DownloadMedia.ToLower() == "image" ? ConfigurationManager.AppSettings["Img"] :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "video" ? ConfigurationManager.AppSettings["HDVIDEO"] :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "certificate" ? "" : "";



                if (CreateMediaZip(dtData, searchDiamondsRequest.DownloadMedia.ToLower(), _realpath, _realpath + subFolderZipName, subFolderZipName, _downloadURL))
                {
                    string _str = string.Empty;
                    if (searchDiamondsRequest.DownloadMedia.ToLower() != "pdf")
                        _str = _path + subFolderZipName + ".zip";
                    else
                        _str = _path + subFolderZipName + ".pdf";
                    return Ok(_str);
                }
                else
                {
                    if (searchDiamondsRequest.DownloadMedia.ToLower() == "video")
                    {
                        return Ok("Error to download video, video is not MP4 !");
                    }
                    else if (searchDiamondsRequest.DownloadMedia.ToLower() == "image")
                    {
                        return Ok("Image is not available in this stone !");
                    }
                    else if (searchDiamondsRequest.DownloadMedia.ToLower() == "certificate")
                    {
                        return Ok("Certificate is not available in this stone !");
                    }
                    else
                    {
                        return Ok("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [HttpPost]
        public IHttpActionResult OrderHistoryMediaDownload([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();
            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsRequest>
                {
                    Data = new List<SearchDiamondsRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable dt = new DataTable();
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(searchDiamondsRequest.SearchName))
                    para.Add(db.CreateParam("iOrderId_sRefNo", DbType.String, ParameterDirection.Input, searchDiamondsRequest.SearchName));
                else
                    para.Add(db.CreateParam("iOrderId_sRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                dt = db.ExecuteSP("IPD_Get_Order_History_Download", para.ToArray(), false);

                string subFolderZipName = searchDiamondsRequest.DownloadMedia.ToLower() == "image" ? "StoneImages" :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "video" ? "StoneVideos" :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "certificate" ? "StoneCertificates" : "";
                subFolderZipName += DAL.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                string _path = ConfigurationManager.AppSettings["data"];
                string _realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _downloadURL = searchDiamondsRequest.DownloadMedia.ToLower() == "image" ? ConfigurationManager.AppSettings["Img"] :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "video" ? ConfigurationManager.AppSettings["HDVIDEO"] :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "certificate" ? "" : "";
                string All_subFolderZipName = "Stone Images, Videos, Certificates";
                All_subFolderZipName += DAL.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");

                if (searchDiamondsRequest.DownloadMedia.ToLower() == "all")
                {
                    //if (All_CreateMediaZip(dt, "All", _realpath, _realpath + All_subFolderZipName, All_subFolderZipName, ConfigurationManager.AppSettings["Img"], ConfigurationManager.AppSettings["S_HDVIDEO"]))
                    //{
                    //    string _str = string.Empty;
                    //    _str = _path + All_subFolderZipName + ".zip";
                    //    return Ok(_str);
                    //}
                    //else
                    //{
                    //    return Ok("Images, Videos, Certificates is not available in this stone !");
                    //}
                    return Ok("");
                }
                else
                {
                    if (CreateMediaZip(dt, searchDiamondsRequest.DownloadMedia.ToLower(), _realpath, _realpath + subFolderZipName, subFolderZipName, _downloadURL))
                    {
                        string _str = string.Empty;
                        if (searchDiamondsRequest.DownloadMedia.ToLower() != "pdf")
                            _str = _path + subFolderZipName + ".zip";
                        else
                            _str = _path + subFolderZipName + ".pdf";
                        return Ok(_str);
                    }
                    else
                    {
                        if (searchDiamondsRequest.DownloadMedia.ToLower() == "video")
                        {
                            bool IsOverseas = false, IsVideo = false;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Convert.ToBoolean(row["IsOverseas"]) == true && row["sVdoLink"].ToString() != "")
                                {
                                    IsOverseas = Convert.ToBoolean(row["IsOverseas"]);
                                }
                                if (Convert.ToBoolean(row["IsOverseas"]) == false && row["sVdoLink"].ToString() == "")
                                {
                                    IsVideo = true;
                                }
                            }
                            if (IsVideo == true)
                            {
                                return Ok("Video is not available in this stone !");
                            }
                            else if (IsOverseas == true)
                            {
                                return Ok("Video download is not allow for this stone !");
                            }
                            else
                            {
                                return Ok("Error to download video, video is not MP4 !");
                            }
                        }
                        else if (searchDiamondsRequest.DownloadMedia.ToLower() == "image")
                        {
                            return Ok("Image is not available in this stone !");
                        }
                        else if (searchDiamondsRequest.DownloadMedia.ToLower() == "certificate")
                        {
                            return Ok("Certificate is not available in this stone !");
                        }
                        else
                        {
                            return Ok("Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult DownloadStockMediaWithoutToken([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();

            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsRequest>
                {
                    Data = new List<SearchDiamondsRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                //int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                //searchDiamondsRequest.UserID = userID;

                DataTable dtData = new DataTable();
                if (searchDiamondsRequest.DownloadMedia.ToLower() != "pdf")
                {
                    dtData = SearchStockInner(searchDiamondsRequest);
                }
                else
                {
                    dtData = SearchStockInner(searchDiamondsRequest, true, true);
                }

                string subFolderZipName = searchDiamondsRequest.DownloadMedia.ToLower() == "image" ? "StoneImages" :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "video" ? "StoneVideos" :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "certificate" ? "StoneCertificates" : "";
                subFolderZipName += DAL.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                string _path = ConfigurationManager.AppSettings["data"];
                string _realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _downloadURL = searchDiamondsRequest.DownloadMedia.ToLower() == "image" ? ConfigurationManager.AppSettings["Img"] :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "video" ? ConfigurationManager.AppSettings["HDVIDEO"] :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "certificate" ? "" : "";



                if (CreateMediaZip(dtData, searchDiamondsRequest.DownloadMedia.ToLower(), _realpath, _realpath + subFolderZipName, subFolderZipName, _downloadURL))
                {
                    string _str = string.Empty;
                    if (searchDiamondsRequest.DownloadMedia.ToLower() != "pdf")
                        _str = _path + subFolderZipName + ".zip";
                    else
                        _str = _path + subFolderZipName + ".pdf";
                    return Ok(_str);
                }
                else
                {
                    if (searchDiamondsRequest.DownloadMedia.ToLower() == "video")
                    {
                        return Ok("Error to download video, video is not MP4..!");
                    }
                    else if (searchDiamondsRequest.DownloadMedia.ToLower() == "image")
                    {
                        return Ok("Image is not available in this stone !");
                    }
                    else
                    {
                        return Ok("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult OverseasDNAMediaDownloadByURL([FromBody] JObject data)
        {
            DNAOverSeasDownload dnaoverseasdownload = new DNAOverSeasDownload();

            try
            {
                dnaoverseasdownload = JsonConvert.DeserializeObject<DNAOverSeasDownload>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<DNAOverSeasDownload>
                {
                    Data = new List<DNAOverSeasDownload>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                string subFolderZipName = dnaoverseasdownload.Type.ToLower() == "image" ? "StoneImages_" :
                                      dnaoverseasdownload.Type.ToLower() == "video" ? "StoneVideos_" : "";
                subFolderZipName += dnaoverseasdownload.StoneId + "_";
                subFolderZipName += DAL.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                string _realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _path = ConfigurationManager.AppSettings["data"];

                if (CreateMediaZipFromURL(_realpath, _realpath + subFolderZipName, dnaoverseasdownload.Type.ToLower(), dnaoverseasdownload.URL, dnaoverseasdownload.StoneId))
                {
                    string _str = _path + subFolderZipName + ".zip";
                    return Ok(_str);
                }
                else
                {
                    return Ok("Error");
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [HttpPost]
        public IHttpActionResult GetRevisedStock([FromBody] JObject data)
        {
            RevisedStockRequest revisedStockRequest = new RevisedStockRequest();

            try
            {
                revisedStockRequest = JsonConvert.DeserializeObject<RevisedStockRequest>(data.ToString());
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

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();
                searchDiamondsRequest.StoneID = revisedStockRequest.CertiNo;
                searchDiamondsRequest.UserID = userID;
                searchDiamondsRequest.PageNo = revisedStockRequest.PageNo;
                searchDiamondsRequest.ReviseStockFlag = "1";
                searchDiamondsRequest.Shape = revisedStockRequest.Shape;
                searchDiamondsRequest.Color = revisedStockRequest.Color;
                searchDiamondsRequest.Polish = revisedStockRequest.Polish;
                searchDiamondsRequest.Pointer = revisedStockRequest.Pointer;
                searchDiamondsRequest.Lab = revisedStockRequest.Lab;
                searchDiamondsRequest.Fls = revisedStockRequest.Fls;
                searchDiamondsRequest.Clarity = revisedStockRequest.Clarity;
                searchDiamondsRequest.Cut = revisedStockRequest.Cut;
                searchDiamondsRequest.Symm = revisedStockRequest.Symm;
                searchDiamondsRequest.Location = revisedStockRequest.Location;

                DataTable dtData = SearchStockInner(searchDiamondsRequest);

                DataTable dtSumm = new DataTable();
                dtSumm.Columns.Add("TOT_PAGE", typeof(Int32));
                dtSumm.Columns.Add("PAGE_SIZE", typeof(Int32));
                dtSumm.Columns.Add("TOT_PCS", typeof(Int32));
                dtSumm.Columns.Add("TOT_CTS", typeof(Decimal));
                dtSumm.Columns.Add("TOT_RAP_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("TOT_NET_AMOUNT", typeof(Decimal));
                dtSumm.Columns.Add("AVG_PRICE_PER_CTS", typeof(Decimal));
                dtSumm.Columns.Add("AVG_SALES_DISC_PER", typeof(Decimal));

                DataRow[] dra = dtData.Select("P_SEQ_NO IS NULL");
                SearchSummary searchSummary = new SearchSummary();
                if (dra.Length > 0)
                {
                    searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                    searchSummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["STONE_REF_NO"]);
                    searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["CTS"]);
                    searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble((dra[0]["rap_amount"].ToString() != "" && dra[0]["rap_amount"].ToString() != null ? dra[0]["rap_amount"] : "0"));
                    searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["NET_AMOUNT"]);
                    searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(dra[0]["PRICE_PER_CTS"]);
                    searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0"));
                }

                dtData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                SearchDiamondsResponse searchDiamondsResponse = new SearchDiamondsResponse();

                List<SearchStone> listSearchStone = new List<SearchStone>();
                listSearchStone = DataTableExtension.ToList<SearchStone>(dtData);
                List<SearchDiamondsResponse> searchDiamondsResponses = new List<SearchDiamondsResponse>();

                if (listSearchStone.Count > 0)
                {
                    searchDiamondsResponses.Add(new SearchDiamondsResponse()
                    {
                        DataList = listSearchStone,
                        DataSummary = searchSummary
                    });

                    return Ok(new ServiceResponse<SearchDiamondsResponse>
                    {
                        Data = searchDiamondsResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<SearchDiamondsResponse>
                    {
                        Data = searchDiamondsResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsResponse>
                {
                    Data = new List<SearchDiamondsResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult DownloadPairSearchExcel([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();

            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsRequest>
                {
                    Data = new List<SearchDiamondsRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(userID)));
                DataTable _dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);
                string iUserType = "";
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    iUserType = _dt.Rows[0]["iUserType"].ToString();
                }

                DataTable dtData = PairSearchInner(searchDiamondsRequest, userID, "Y");

                dtData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                dtData.DefaultView.RowFilter = "iSr IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    string filename = "PairSearch " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    EpExcelExport.CreateExcel(dtData, realpath, realpath + filename + ".xlsx", _livepath, "", iUserType);

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                return Ok("Bad - No record found");
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult DownloadPairSearchMedia([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();

            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsRequest>
                {
                    Data = new List<SearchDiamondsRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                searchDiamondsRequest.UserID = userID;

                DataTable dtData = new DataTable();
                dtData = PairSearchInner(searchDiamondsRequest, userID, "Y");

                string subFolderZipName = searchDiamondsRequest.DownloadMedia.ToLower() == "image" ? "StoneImages" :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "video" ? "StoneVideos" :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "certificate" ? "StoneCertificates" : "";
                subFolderZipName += DAL.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                string _path = ConfigurationManager.AppSettings["data"];
                string _realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _downloadURL = searchDiamondsRequest.DownloadMedia.ToLower() == "image" ? ConfigurationManager.AppSettings["Img"] :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "video" ? ConfigurationManager.AppSettings["HDVIDEO"] :
                                      searchDiamondsRequest.DownloadMedia.ToLower() == "certificate" ? "" : "";


                string _str = string.Empty;
                if (searchDiamondsRequest.DownloadMedia.ToLower() == "pdf")
                {
                    //dtData.DefaultView.RowFilter = "[Sr.] IS NOT NULL";
                    //dtData = dtData.DefaultView.ToTable();
                    Models.PdfTemplate.ExportToPdf(dtData, _realpath + subFolderZipName + ".pdf");
                    _str = _path + subFolderZipName + ".pdf";
                    return Ok(_str);
                }
                else if (CreateMediaZip(dtData, searchDiamondsRequest.DownloadMedia.ToLower(), _realpath, _realpath + subFolderZipName, subFolderZipName, _downloadURL))
                {
                    _str = _path + subFolderZipName + ".zip";
                    return Ok(_str);
                }
                else
                {
                    if (searchDiamondsRequest.DownloadMedia.ToLower() == "video")
                    {
                        return Ok("Error to download video, video is not MP4..!");
                    }
                    else if (searchDiamondsRequest.DownloadMedia.ToLower() == "image")
                    {
                        return Ok("Image is not available in this stone !");
                    }
                    else
                    {
                        return Ok("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
            //return Ok("Error");
        }

        #region NonAction Methods

        [NonAction]
        private DataTable SearchDiamondsFullStock(string Full, string StoneID, string CertiNo, string Shape, string Pointer, string Color, string Clarity, string Cut, string Polish, string Symm, string Fls, string Lab, string Luster, string Location, string Inclusion, string Natts, string Shade, string FromCts, string ToCts, string FormDisc, string ToDisc, string FormPricePerCts, string ToPricePerCts, string FormNetAmt, string ToNetAmt, string FormDepth, string ToDepth, string FormLength, string ToLength, string FormWidth, string ToWidth, string FormDepthPer, string ToDepthPer, string FormTablePer, string ToTablePer, string HasImage, string HasHDMovie, string IsPromotion, string ShapeColorPurity, string ReviseStockFlag, string CrownInclusion, string CrownNatts, string PageNo, string UserID, string TokenNo, string StoneStatus, string FromCrownAngle = "", string ToCrownAngle = "", string FromCrownHeight = "", string ToCrownHeight = "", string FromPavAngle = "", string ToPavAngle = "", string FromPavHeight = "", string ToPavHeight = "", string BGM = "", string Black = "", string SmartSearch = "", string keytosymbol = "", string PgSize = "", string OrderBy = "", string FormName = "", string ActivityType = "", string ColorType = "", string Intensity = "", string Overtone = "", string Fancy_Color = "", string Table_Open = "", string Crown_Open = "", string Pav_Open = "", string Girdle_Open = "", string UsedFor = "", string NewArrival = "")
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(Shape))
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, Shape.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Pointer))
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, Pointer.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Color))
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, Color.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Clarity))
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, Clarity.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Cut))
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, Cut.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Polish))
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, Polish.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Symm))
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, Symm.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Fls))
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, Fls.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Lab))
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, Lab.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Luster))
                    para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, Luster.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Location))
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, Location.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Inclusion))
                    para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, Inclusion.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Natts))
                    para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, Natts.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Shade))
                    para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, Shade.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(FromCts))
                {
                    para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FromCts)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(ToCts))
                {
                    para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToCts)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                if (!string.IsNullOrEmpty(FormDisc))
                {
                    para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormDisc)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(ToDisc))
                {
                    para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToDisc)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(FormPricePerCts))
                {
                    para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormPricePerCts)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(ToPricePerCts))
                {
                    para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToPricePerCts)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(FormNetAmt))
                {
                    para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormNetAmt)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(ToNetAmt))
                {
                    para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToNetAmt)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(FormDepth))
                {
                    para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormDepth)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(ToDepth))
                {
                    para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToDepth)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(FormLength))
                {
                    para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormLength)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(ToLength))
                {
                    para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToLength)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(FormWidth))
                {
                    para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormWidth)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(ToWidth))
                {
                    para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToWidth)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(FormDepthPer))
                {
                    para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormDepthPer)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(ToDepthPer))
                {
                    para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToDepthPer)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(FormTablePer))
                {
                    para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormTablePer)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(ToTablePer))
                {
                    para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToTablePer)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(HasImage))
                    para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, HasImage));
                else
                    para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(HasHDMovie))
                    para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, HasHDMovie));
                else
                    para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(IsPromotion))
                    para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, IsPromotion));
                else
                    para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, DBNull.Value));

                ////////////////////

                if (!string.IsNullOrEmpty(CertiNo))
                    para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, CertiNo.ToUpper()));
                else
                    para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(StoneID))
                    para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, StoneID.ToUpper()));
                else
                    para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ShapeColorPurity))
                    para.Add(db.CreateParam("p_shape_color_purity", DbType.String, ParameterDirection.Input, ShapeColorPurity.ToUpper()));
                else
                    para.Add(db.CreateParam("p_shape_color_purity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ReviseStockFlag))
                    para.Add(db.CreateParam("p_for_revise_stock", DbType.String, ParameterDirection.Input, ReviseStockFlag));
                else
                    para.Add(db.CreateParam("p_for_revise_stock", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(CrownInclusion))
                    para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, CrownInclusion.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(CrownNatts))
                    para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, CrownNatts.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(FromCrownAngle))
                    para.Add(db.CreateParam("p_from_crown_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FromCrownAngle)));
                else
                    para.Add(db.CreateParam("p_from_crown_angle", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ToCrownAngle))
                    para.Add(db.CreateParam("p_to_crown_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToCrownAngle)));
                else
                    para.Add(db.CreateParam("p_to_crown_angle", DbType.String, ParameterDirection.Input, DBNull.Value));



                if (!string.IsNullOrEmpty(FromCrownHeight))
                    para.Add(db.CreateParam("p_from_crown_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FromCrownHeight)));
                else
                    para.Add(db.CreateParam("p_from_crown_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ToCrownHeight))
                    para.Add(db.CreateParam("p_to_crown_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToCrownHeight)));
                else
                    para.Add(db.CreateParam("p_to_crown_Height", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(FromPavAngle))
                    para.Add(db.CreateParam("p_from_Pav_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FromPavAngle)));
                else
                    para.Add(db.CreateParam("p_from_Pav_angle", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ToPavAngle))
                    para.Add(db.CreateParam("p_to_Pav_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToPavAngle)));
                else
                    para.Add(db.CreateParam("p_to_Pav_angle", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(FromPavHeight))
                    para.Add(db.CreateParam("p_from_Pav_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FromPavHeight)));
                else
                    para.Add(db.CreateParam("p_from_Pav_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ToPavHeight))
                    para.Add(db.CreateParam("p_to_Pav_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToPavHeight)));
                else
                    para.Add(db.CreateParam("p_to_Pav_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(BGM))
                    para.Add(db.CreateParam("p_for_BGM", DbType.String, ParameterDirection.Input, BGM.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_BGM", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Black))
                    para.Add(db.CreateParam("p_for_Black", DbType.String, ParameterDirection.Input, Black.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Black", DbType.String, ParameterDirection.Input, DBNull.Value));

                //para.Add(db.CreateParam("p_for_token", DbType.String, ParameterDirection.Input, TokenNo));
                if (!string.IsNullOrEmpty(UserID))
                    para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, UserID));
                else
                    para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("p_for_page", DbType.String, ParameterDirection.Input, PageNo));

                if (!string.IsNullOrEmpty(StoneStatus))
                    para.Add(db.CreateParam("p_for_Status", DbType.String, ParameterDirection.Input, StoneStatus));
                else
                    para.Add(db.CreateParam("p_for_Status", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(SmartSearch))
                    para.Add(db.CreateParam("p_for_smartSearch", DbType.String, ParameterDirection.Input, SmartSearch));
                else
                    para.Add(db.CreateParam("p_for_smartSearch", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(keytosymbol))
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, keytosymbol));
                else
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Full) && Full == "N")
                {
                    if (!string.IsNullOrEmpty(PgSize))
                        para.Add(db.CreateParam("iPgSize", DbType.String, ParameterDirection.Input, PgSize));
                    else
                        para.Add(db.CreateParam("iPgSize", DbType.String, ParameterDirection.Input, DBNull.Value));

                    if (!string.IsNullOrEmpty(OrderBy))
                        para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, OrderBy));
                    else
                        para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, ActivityType));

                if (!string.IsNullOrEmpty(ColorType))
                    para.Add(db.CreateParam("p_for_color_type", DbType.String, ParameterDirection.Input, ColorType));
                else
                    para.Add(db.CreateParam("p_for_color_type", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Intensity))
                    para.Add(db.CreateParam("p_for_color_intensity", DbType.String, ParameterDirection.Input, Intensity));
                else
                    para.Add(db.CreateParam("p_for_color_intensity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Overtone))
                    para.Add(db.CreateParam("p_for_color_overtone", DbType.String, ParameterDirection.Input, Overtone));
                else
                    para.Add(db.CreateParam("p_for_color_overtone", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Fancy_Color))
                    para.Add(db.CreateParam("p_for_color_fancy_color", DbType.String, ParameterDirection.Input, Fancy_Color));
                else
                    para.Add(db.CreateParam("p_for_color_fancy_color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Table_Open))
                    para.Add(db.CreateParam("p_for_Table_Open", DbType.String, ParameterDirection.Input, Table_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Table_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Crown_Open))
                    para.Add(db.CreateParam("p_for_Crown_Open", DbType.String, ParameterDirection.Input, Crown_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Crown_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Pav_Open))
                    para.Add(db.CreateParam("p_for_Pav_Open", DbType.String, ParameterDirection.Input, Pav_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Pav_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Girdle_Open))
                    para.Add(db.CreateParam("p_for_Girdle_Open", DbType.String, ParameterDirection.Input, Girdle_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Girdle_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(UsedFor))
                    para.Add(db.CreateParam("UsedFor", DbType.String, ParameterDirection.Input, UsedFor));
                else
                    para.Add(db.CreateParam("UsedFor", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(NewArrival))
                    para.Add(db.CreateParam("NewArrival", DbType.String, ParameterDirection.Input, NewArrival));
                else
                    para.Add(db.CreateParam("NewArrival", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(Full) && Full == "N")
                {
                    dt = db.ExecuteSP("IPD_Search_Diamonds_Full_stock", para.ToArray(), false);
                }
                else
                {
                    dt = db.ExecuteSP("IPD_Search_Diamonds_Full_stock", para.ToArray(), false);
                }


                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }

        [NonAction]
        public DataTable SearchStockInner(SearchDiamondsRequest searchDiamondsRequest, bool withPageSize = true, bool isForPDF = false)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                #region Pass Search parameter in SP
                if (!string.IsNullOrEmpty(searchDiamondsRequest.Shape))
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Shape.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Pointer))
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Pointer.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Color))
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Color.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Clarity))
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Clarity.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Cut))
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Cut.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Polish))
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Polish.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Symm))
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Symm.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Fls))
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Fls.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Lab))
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Lab.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Luster))
                    para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Luster.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Location))
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Location.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Inclusion))
                    para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Inclusion.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Natts))
                    para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Natts.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Shade))
                    para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Shade.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCts))
                {
                    para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromCts)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCts))
                {
                    para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToCts)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDisc))
                {
                    para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormDisc)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDisc))
                {
                    para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToDisc)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormPricePerCts))
                {
                    para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormPricePerCts)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPricePerCts))
                {
                    para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToPricePerCts)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormNetAmt))
                {
                    para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormNetAmt)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToNetAmt))
                {
                    para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToNetAmt)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDepth))
                {
                    para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormDepth)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDepth))
                {
                    para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToDepth)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormLength))
                {
                    para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormLength)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToLength))
                {
                    para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToLength)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormWidth))
                {
                    para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormWidth)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToWidth))
                {
                    para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToWidth)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDepthPer))
                {
                    para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormDepthPer)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDepthPer))
                {
                    para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToDepthPer)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                ////////////////////


                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormTablePer))
                {
                    para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormTablePer)));
                }
                else
                {
                    para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToTablePer))
                {
                    para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToTablePer)));
                }
                else
                {
                    para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));
                }


                if (!string.IsNullOrEmpty(searchDiamondsRequest.HasImage))
                    para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, searchDiamondsRequest.HasImage));
                else
                    para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(searchDiamondsRequest.HasHDMovie))
                    para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, searchDiamondsRequest.HasHDMovie));
                else
                    para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.IsPromotion))
                    para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.IsPromotion));
                else
                    para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, DBNull.Value));

                ////////////////////

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CertiNo))
                    para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CertiNo.ToUpper()));
                else
                    para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.StoneID))
                    para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, searchDiamondsRequest.StoneID.ToUpper()));
                else
                    para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ShapeColorPurity))
                    para.Add(db.CreateParam("p_shape_color_purity", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ShapeColorPurity.ToUpper()));
                else
                    para.Add(db.CreateParam("p_shape_color_purity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ReviseStockFlag))
                    para.Add(db.CreateParam("p_for_revise_stock", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ReviseStockFlag));
                else
                    para.Add(db.CreateParam("p_for_revise_stock", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CrownInclusion))
                    para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CrownInclusion.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CrownNatts))
                    para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CrownNatts.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCrownAngle))
                    para.Add(db.CreateParam("p_from_crown_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromCrownAngle)));
                else
                    para.Add(db.CreateParam("p_from_crown_angle", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCrownAngle))
                    para.Add(db.CreateParam("p_to_crown_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToCrownAngle)));
                else
                    para.Add(db.CreateParam("p_to_crown_angle", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCrownHeight))
                    para.Add(db.CreateParam("p_from_crown_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromCrownHeight)));
                else
                    para.Add(db.CreateParam("p_from_crown_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCrownHeight))
                    para.Add(db.CreateParam("p_to_crown_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToCrownHeight)));
                else
                    para.Add(db.CreateParam("p_to_crown_Height", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromPavAngle))
                    para.Add(db.CreateParam("p_from_Pav_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromPavAngle)));
                else
                    para.Add(db.CreateParam("p_from_Pav_angle", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPavAngle))
                    para.Add(db.CreateParam("p_to_Pav_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToPavAngle)));
                else
                    para.Add(db.CreateParam("p_to_Pav_angle", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromPavHeight))
                    para.Add(db.CreateParam("p_from_Pav_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromPavHeight)));
                else
                    para.Add(db.CreateParam("p_from_Pav_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPavHeight))
                    para.Add(db.CreateParam("p_to_Pav_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToPavHeight)));
                else
                    para.Add(db.CreateParam("p_to_Pav_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.BGM))
                    para.Add(db.CreateParam("p_for_BGM", DbType.String, ParameterDirection.Input, searchDiamondsRequest.BGM.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_BGM", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Black))
                    para.Add(db.CreateParam("p_for_Black", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Black.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Black", DbType.String, ParameterDirection.Input, DBNull.Value));

                #endregion

                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, searchDiamondsRequest.UserID));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.PageNo))
                    para.Add(db.CreateParam("p_for_page", DbType.String, ParameterDirection.Input, searchDiamondsRequest.PageNo));
                else
                    para.Add(db.CreateParam("p_for_page", DbType.String, ParameterDirection.Input, 0));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.StoneStatus))
                    para.Add(db.CreateParam("p_for_Status", DbType.String, ParameterDirection.Input, searchDiamondsRequest.StoneStatus));
                else
                    para.Add(db.CreateParam("p_for_Status", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(searchDiamondsRequest.SmartSearch))
                    para.Add(db.CreateParam("p_for_smartSearch", DbType.String, ParameterDirection.Input, searchDiamondsRequest.SmartSearch));
                else
                    para.Add(db.CreateParam("p_for_smartSearch", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.KeyToSymbol))
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, searchDiamondsRequest.KeyToSymbol));
                else
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!withPageSize)
                {
                    para.Add(db.CreateParam("iPgSize", DbType.Int16, ParameterDirection.Input, DBNull.Value));
                }
                else
                {
                    if (searchDiamondsRequest.PgSize > 0)
                        para.Add(db.CreateParam("iPgSize", DbType.Int16, ParameterDirection.Input, searchDiamondsRequest.PgSize));
                    else
                        para.Add(db.CreateParam("iPgSize", DbType.Int16, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(searchDiamondsRequest.OrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, searchDiamondsRequest.OrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ActivityType));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CurrencyAmt))
                    para.Add(db.CreateParam("p_for_CurrencyAmt", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CurrencyAmt));
                else
                    para.Add(db.CreateParam("p_for_CurrencyAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ColorType))
                    para.Add(db.CreateParam("p_for_color_type", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ColorType));
                else
                    para.Add(db.CreateParam("p_for_color_type", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Intensity))
                    para.Add(db.CreateParam("p_for_color_intensity", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Intensity));
                else
                    para.Add(db.CreateParam("p_for_color_intensity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Overtone))
                    para.Add(db.CreateParam("p_for_color_overtone", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Overtone));
                else
                    para.Add(db.CreateParam("p_for_color_overtone", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Fancy_Color))
                    para.Add(db.CreateParam("p_for_color_fancy_color", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Fancy_Color));
                else
                    para.Add(db.CreateParam("p_for_color_fancy_color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Table_Open))
                    para.Add(db.CreateParam("p_for_Table_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Table_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Table_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Crown_Open))
                    para.Add(db.CreateParam("p_for_Crown_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Crown_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Crown_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Pav_Open))
                    para.Add(db.CreateParam("p_for_Pav_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Pav_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Pav_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Girdle_Open))
                    para.Add(db.CreateParam("p_for_Girdle_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Girdle_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Girdle_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.UsedFor))
                    para.Add(db.CreateParam("UsedFor", DbType.String, ParameterDirection.Input, searchDiamondsRequest.UsedFor));
                else
                    para.Add(db.CreateParam("UsedFor", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.NewArrival))
                    para.Add(db.CreateParam("NewArrival", DbType.String, ParameterDirection.Input, searchDiamondsRequest.NewArrival));
                else
                    para.Add(db.CreateParam("NewArrival", DbType.String, ParameterDirection.Input, DBNull.Value)); 

                DataTable dt = new DataTable();

                if (!isForPDF)
                    dt = db.ExecuteSP("ipd_search_diamonds_sunrise", para.ToArray(), false);
                else
                    dt = db.ExecuteSP("ipd_search_diamonds_PDF_sunrise", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult SaveNoFoundSearchStock(SearchDiamondsRequest searchDiamondsRequest)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                #region Pass Search parameter in SP
                int UserID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("p_for_iUserId", DbType.String, ParameterDirection.Input, UserID));
                para.Add(db.CreateParam("p_for_Action", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Action));
                para.Add(db.CreateParam("p_for_dTransDate", DbType.String, ParameterDirection.Input, searchDiamondsRequest.dTransDate));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Shape))
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Shape.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Pointer))
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Pointer.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Color))
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Color.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Clarity))
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Clarity.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Cut))
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Cut.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Polish))
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Polish.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Symm))
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Symm.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Fls))
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Fls.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Lab))
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Lab.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCts))
                    para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromCts)));
                else
                    para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCts))
                    para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToCts)));
                else
                    para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDisc))
                    para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormDisc)));
                else
                    para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDisc))
                    para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToDisc)));
                else
                    para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormPricePerCts))
                    para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormPricePerCts)));
                else
                    para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPricePerCts))
                    para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToPricePerCts)));
                else
                    para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormNetAmt))
                    para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormNetAmt)));
                else
                    para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToNetAmt))
                    para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToNetAmt)));
                else
                    para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDepth))
                    para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormDepth)));
                else
                    para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDepth))
                    para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToDepth)));
                else
                    para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormLength))
                    para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormLength)));
                else
                    para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToLength))
                    para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToLength)));
                else
                    para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormWidth))
                    para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormWidth)));
                else
                    para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToWidth))
                    para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToWidth)));
                else
                    para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CertiNo))
                    para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CertiNo.ToUpper()));
                else
                    para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.StoneID))
                    para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, searchDiamondsRequest.StoneID.ToUpper()));
                else
                    para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Inclusion))
                    para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Inclusion.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Natts))
                    para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Natts.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Shade))
                    para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Shade.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormTablePer))
                    para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormTablePer)));
                else
                    para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToTablePer))
                    para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToTablePer)));
                else
                    para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDepthPer))
                    para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormDepthPer)));
                else
                    para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDepthPer))
                    para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToDepthPer)));
                else
                    para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.HasImage))
                    para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, searchDiamondsRequest.HasImage));
                else
                    para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.HasHDMovie))
                    para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, searchDiamondsRequest.HasHDMovie));
                else
                    para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.IsPromotion))
                    para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.IsPromotion));
                else
                    para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ShapeColorPurity))
                    para.Add(db.CreateParam("p_shape_color_purity", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ShapeColorPurity.ToUpper()));
                else
                    para.Add(db.CreateParam("p_shape_color_purity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ReviseStockFlag))
                    para.Add(db.CreateParam("p_for_revise_stock", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ReviseStockFlag));
                else
                    para.Add(db.CreateParam("p_for_revise_stock", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CrownNatts))
                    para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CrownNatts.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CrownInclusion))
                    para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CrownInclusion.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Luster))
                    para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Luster.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Location))
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Location.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCrownAngle))
                    para.Add(db.CreateParam("p_from_crown_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromCrownAngle)));
                else
                    para.Add(db.CreateParam("p_from_crown_angle", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCrownAngle))
                    para.Add(db.CreateParam("p_to_crown_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToCrownAngle)));
                else
                    para.Add(db.CreateParam("p_to_crown_angle", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCrownHeight))
                    para.Add(db.CreateParam("p_from_crown_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromCrownHeight)));
                else
                    para.Add(db.CreateParam("p_from_crown_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCrownHeight))
                    para.Add(db.CreateParam("p_to_crown_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToCrownHeight)));
                else
                    para.Add(db.CreateParam("p_to_crown_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromPavAngle))
                    para.Add(db.CreateParam("p_from_Pav_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromPavAngle)));
                else
                    para.Add(db.CreateParam("p_from_Pav_angle", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPavAngle))
                    para.Add(db.CreateParam("p_to_Pav_angle", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToPavAngle)));
                else
                    para.Add(db.CreateParam("p_to_Pav_angle", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromPavHeight))
                    para.Add(db.CreateParam("p_from_Pav_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromPavHeight)));
                else
                    para.Add(db.CreateParam("p_from_Pav_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPavHeight))
                    para.Add(db.CreateParam("p_to_Pav_Height", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToPavHeight)));
                else
                    para.Add(db.CreateParam("p_to_Pav_Height", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.BGM))
                    para.Add(db.CreateParam("p_for_bgm", DbType.String, ParameterDirection.Input, searchDiamondsRequest.BGM.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_bgm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Black))
                    para.Add(db.CreateParam("p_for_black", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Black.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_black", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.StoneStatus))
                    para.Add(db.CreateParam("p_for_status", DbType.String, ParameterDirection.Input, searchDiamondsRequest.StoneStatus));
                else
                    para.Add(db.CreateParam("p_for_status", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.KeyToSymbol))
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, searchDiamondsRequest.KeyToSymbol));
                else
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ColorType))
                    para.Add(db.CreateParam("p_for_color_type", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ColorType));
                else
                    para.Add(db.CreateParam("p_for_color_type", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Intensity))
                    para.Add(db.CreateParam("p_for_color_intensity", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Intensity));
                else
                    para.Add(db.CreateParam("p_for_color_intensity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Overtone))
                    para.Add(db.CreateParam("p_for_color_overtone", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Overtone));
                else
                    para.Add(db.CreateParam("p_for_color_overtone", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Fancy_Color))
                    para.Add(db.CreateParam("p_for_color_fancy_color", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Fancy_Color));
                else
                    para.Add(db.CreateParam("p_for_color_fancy_color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Table_Open))
                    para.Add(db.CreateParam("Table_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Table_Open));
                else
                    para.Add(db.CreateParam("Table_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Crown_Open))
                    para.Add(db.CreateParam("Crown_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Crown_Open));
                else
                    para.Add(db.CreateParam("Crown_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Pav_Open))
                    para.Add(db.CreateParam("Pav_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Pav_Open));
                else
                    para.Add(db.CreateParam("Pav_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Girdle_Open))
                    para.Add(db.CreateParam("Girdle_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Girdle_Open));
                else
                    para.Add(db.CreateParam("Girdle_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.NewArrival))
                    para.Add(db.CreateParam("NewArrival", DbType.String, ParameterDirection.Input, searchDiamondsRequest.NewArrival));
                else
                    para.Add(db.CreateParam("NewArrival", DbType.String, ParameterDirection.Input, DBNull.Value));

                #endregion Pass Search parameter in SP

                DataTable dt = new DataTable();

                dt = db.ExecuteSP("SearchStock_NoFoundDet_Insert", para.ToArray(), false);

                int Id = ((dt != null && dt.Rows.Count > 0) ? (dt.Rows[0]["Status"].ToString() == "1" ? Convert.ToInt32(dt.Rows[0]["Id"]) : 0) : 0);

                string ToEmail = null;
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DEMAND_NOTIFICATION_EMAILID"]))
                    ToEmail = ConfigurationManager.AppSettings["DEMAND_NOTIFICATION_EMAILID"];

                if (ToEmail != "" && Id != 0)
                {
                    try
                    {
                        IPadCommon.EmailNewDemandNotification(UserID, Id, ToEmail);
                    }
                    catch (Exception ex)
                    {
                        DAL.Common.InsertErrorLog(ex, "EMAIL ERROR", Request);
                    }
                }


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
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        
        [NonAction]
        private DataTable QuickSearchInner(QuickSearchRequest quickSearchRequest)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(quickSearchRequest.Cut))
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, quickSearchRequest.Cut.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(quickSearchRequest.Fls))
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, quickSearchRequest.Fls.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("flag", DbType.String, ParameterDirection.Input, 1));

                para.Add(db.CreateParam("iUserId", DbType.String, ParameterDirection.Input, quickSearchRequest.iUserId));
                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, quickSearchRequest.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, quickSearchRequest.ActivityType));

                DataTable dt = db.ExecuteSP("IPD_Quick_Search_Sunrise", para.ToArray(), false);
                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }

        [NonAction]
        private DataTable GetSubQuickSearchInner(SubQuickSearchRequest subQuickSearchRequest)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (!string.IsNullOrEmpty(subQuickSearchRequest.Cut))
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, subQuickSearchRequest.Cut.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(subQuickSearchRequest.Fls))
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, subQuickSearchRequest.Fls.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, subQuickSearchRequest.Pointer));
                para.Add(db.CreateParam("p_for_col_grp", DbType.String, ParameterDirection.Input, subQuickSearchRequest.ColorGroup));
                para.Add(db.CreateParam("p_for_pur_grp", DbType.String, ParameterDirection.Input, subQuickSearchRequest.PurityGroup));
                int iUserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                para.Add(db.CreateParam("iUserId", DbType.String, ParameterDirection.Input, iUserId));
                para.Add(db.CreateParam("flag", DbType.String, ParameterDirection.Input, 2));

                DataTable dt = db.ExecuteSP("IPD_Quick_Search_Sunrise", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }

        [NonAction]
        private DataTable PairSearchInner(SearchDiamondsRequest searchDiamondsRequest, int userID, string Full = "N")
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Shape))
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Shape.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Pointer))
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Pointer.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Color))
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Color.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Clarity))
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Clarity.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Cut))
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Cut.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Polish))
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Polish.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Symm))
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Symm.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Fls))
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Fls.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Lab))
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Lab.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Luster))
                    para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Luster.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Location))
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Location.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Inclusion))
                    para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Inclusion.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Natts))
                    para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Natts.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Shade))
                    para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Shade.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FromCts))
                    para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FromCts)));
                else
                    para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToCts))
                    para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToCts)));
                else
                    para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDisc))
                    para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormDisc)));
                else
                    para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDisc))
                    para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToDisc)));
                else
                    para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, DBNull.Value));

                //////////////////////////////////////////
                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormPricePerCts))
                    para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormPricePerCts)));
                else
                    para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToPricePerCts))
                    para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToPricePerCts)));
                else
                    para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormNetAmt))
                    para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormNetAmt)));
                else
                    para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToNetAmt))
                    para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToNetAmt)));
                else
                    para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDepth))
                    para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormDepth)));
                else
                    para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDepth))
                    para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToDepth)));
                else
                    para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormLength))
                    para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormLength)));
                else
                    para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToLength))
                    para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToLength)));
                else
                    para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormWidth))
                    para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormWidth)));
                else
                    para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToWidth))
                    para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToWidth)));
                else
                    para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormDepthPer))
                    para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormDepthPer)));
                else
                    para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToDepthPer))
                    para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToDepthPer)));
                else
                    para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.FormTablePer))
                    para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.FormTablePer)));
                else
                    para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.ToTablePer))
                    para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(searchDiamondsRequest.ToTablePer)));
                else
                    para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.HasImage))
                    para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, searchDiamondsRequest.HasImage));
                else
                    para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, DBNull.Value));


                if (!string.IsNullOrEmpty(searchDiamondsRequest.HasHDMovie))
                    para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, searchDiamondsRequest.HasHDMovie));
                else
                    para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.IsPromotion))
                    para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.IsPromotion));
                else
                    para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CertiNo))
                    para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CertiNo.ToUpper()));
                else
                    para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.StoneID))
                    para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, searchDiamondsRequest.StoneID.ToUpper()));
                else
                    para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.BGM))
                    para.Add(db.CreateParam("p_for_BGM", DbType.String, ParameterDirection.Input, searchDiamondsRequest.BGM.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_BGM", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.KeyToSymbol))
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, searchDiamondsRequest.KeyToSymbol));
                else
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, DBNull.Value));

                //para.Add(db.CreateParam("p_for_token", DbType.String, ParameterDirection.Input, TokenNo));
                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, userID));
                para.Add(db.CreateParam("p_for_page", DbType.String, ParameterDirection.Input, searchDiamondsRequest.PageNo));

                if (searchDiamondsRequest.StoneStatus == null)
                    searchDiamondsRequest.StoneStatus = "";

                if (searchDiamondsRequest.StoneStatus != "")
                    para.Add(db.CreateParam("p_for_sStatus", DbType.String, ParameterDirection.Input, searchDiamondsRequest.StoneStatus));
                else
                    para.Add(db.CreateParam("p_for_sStatus", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CrownInclusion))
                    para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CrownInclusion.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.CrownNatts))
                    para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, searchDiamondsRequest.CrownNatts.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ActivityType));

                if (!string.IsNullOrEmpty(Full) && Full == "N")
                {
                    if (!string.IsNullOrEmpty(searchDiamondsRequest.PgSize.ToString()))
                        para.Add(db.CreateParam("iPgSize", DbType.String, ParameterDirection.Input, searchDiamondsRequest.PgSize));
                    else
                        para.Add(db.CreateParam("iPgSize", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Table_Open))
                    para.Add(db.CreateParam("p_for_Table_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Table_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Table_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Crown_Open))
                    para.Add(db.CreateParam("p_for_Crown_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Crown_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Crown_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Pav_Open))
                    para.Add(db.CreateParam("p_for_Pav_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Pav_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Pav_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.Girdle_Open))
                    para.Add(db.CreateParam("p_for_Girdle_Open", DbType.String, ParameterDirection.Input, searchDiamondsRequest.Girdle_Open.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_Girdle_Open", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(searchDiamondsRequest.DownloadMedia) && searchDiamondsRequest.DownloadMedia.ToLower() == "pdf")
                {
                    dt = db.ExecuteSP("IPD_GET_PAIR_STONE_PDF_Sunrise", para.ToArray(), false);
                }
                else
                {
                    dt = db.ExecuteSP("IPD_GET_PAIR_STONE_Sunrise", para.ToArray(), false);
                }

                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }

        // [NonAction]
        // private DataTable SearchDiamondsInner(string StoneID, string CertiNo, string Shape, string Pointer, string Color, string Clarity, string Cut, string Polish, string Symm, string Fls, string Lab, string Luster, string Location, string Inclusion, string Natts, string Shade, string FromCts, string ToCts, string FormDisc, string ToDisc,
        //string FormPricePerCts, string ToPricePerCts,
        //string FormNetAmt, string ToNetAmt,
        //string FormDepth, string ToDepth,
        //string FormLength, string ToLength,
        //string FormWidth, string ToWidth, string FormDepthPer, string ToDepthPer, string FormTablePer, string ToTablePer, string HasImage, string HasHDMovie, string IsPromotion, string ShapeColorPurity, string ReviseStockFlag, string CrownInclusion, string CrownNatts, string PageNo, string UserID, string TokenNo)
        // {
        //     try
        //     {
        //         Database db = new Database();
        //         List<IDbDataParameter> para = new List<IDbDataParameter>();

        //         if (Shape != "")
        //             para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, Shape.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_shape", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Pointer != "")
        //             para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, Pointer.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_pointer", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Color != "")
        //             para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, Color.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_color", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Clarity != "")
        //             para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, Clarity.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_clarity", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Cut != "")
        //             para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, Cut.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_cut", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Polish != "")
        //             para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, Polish.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_polish", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Symm != "")
        //             para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, Symm.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_symm", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Fls != "")
        //             para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, Fls.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_fls", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Lab != "")
        //             para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, Lab.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_lab", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Luster != "")
        //             para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, Luster.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_luster", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Location != "")
        //             para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, Location.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_location", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Inclusion != "")
        //             para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, Inclusion.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Natts != "")
        //             para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, Natts.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_natts", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (Shade != "")
        //             para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, Shade.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_shade", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (FromCts != "")
        //             para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FromCts)));
        //         else
        //             para.Add(db.CreateParam("p_from_cts", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ToCts != "")
        //             para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToCts)));
        //         else
        //             para.Add(db.CreateParam("p_to_cts", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (FormDisc != "")
        //             para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormDisc)));
        //         else
        //             para.Add(db.CreateParam("p_from_disc", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ToDisc != "")
        //             para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToDisc)));
        //         else
        //             para.Add(db.CreateParam("p_to_disc", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (FormPricePerCts != "")
        //             para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormPricePerCts)));
        //         else
        //             para.Add(db.CreateParam("p_from_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ToPricePerCts != "")
        //             para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToPricePerCts)));
        //         else
        //             para.Add(db.CreateParam("p_to_PriceCts", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (FormNetAmt != "")
        //             para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormNetAmt)));
        //         else
        //             para.Add(db.CreateParam("p_from_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ToNetAmt != "")
        //             para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToNetAmt)));
        //         else
        //             para.Add(db.CreateParam("p_to_netAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (FormDepth != "")
        //             para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormDepth)));
        //         else
        //             para.Add(db.CreateParam("p_from_depth", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ToDepth != "")
        //             para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToDepth)));
        //         else
        //             para.Add(db.CreateParam("p_to_depth", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (FormLength != "")
        //             para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormLength)));
        //         else
        //             para.Add(db.CreateParam("p_from_length", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ToLength != "")
        //             para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToLength)));
        //         else
        //             para.Add(db.CreateParam("p_to_length", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (FormWidth != "")
        //             para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormWidth)));
        //         else
        //             para.Add(db.CreateParam("p_from_width", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ToWidth != "")
        //             para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToWidth)));
        //         else
        //             para.Add(db.CreateParam("p_to_width", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (FormDepthPer != "")
        //             para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormDepthPer)));
        //         else
        //             para.Add(db.CreateParam("p_from_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ToDepthPer != "")
        //             para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToDepthPer)));
        //         else
        //             para.Add(db.CreateParam("p_to_depth_per", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (FormTablePer != "")
        //             para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(FormTablePer)));
        //         else
        //             para.Add(db.CreateParam("p_from_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ToTablePer != "")
        //             para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, Convert.ToDecimal(ToTablePer)));
        //         else
        //             para.Add(db.CreateParam("p_to_table_per", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (HasImage != "")
        //             para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, HasImage));
        //         else
        //             para.Add(db.CreateParam("p_for_image", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (HasHDMovie != "")
        //             para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, HasHDMovie));
        //         else
        //             para.Add(db.CreateParam("p_for_movie", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (IsPromotion != "")
        //             para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, IsPromotion));
        //         else
        //             para.Add(db.CreateParam("p_for_promotion", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (CertiNo != "")
        //             para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, CertiNo.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_certino", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (StoneID != "")
        //             para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, StoneID.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_refno", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ShapeColorPurity != "")
        //             para.Add(db.CreateParam("p_shape_color_purity", DbType.String, ParameterDirection.Input, ShapeColorPurity.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_shape_color_purity", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (ReviseStockFlag != "")
        //             para.Add(db.CreateParam("p_for_revise_stock", DbType.String, ParameterDirection.Input, ReviseStockFlag));
        //         else
        //             para.Add(db.CreateParam("p_for_revise_stock", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (CrownInclusion != "")
        //             para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, CrownInclusion.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_crown_inclusion", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         if (CrownNatts != "")
        //             para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, CrownNatts.ToUpper()));
        //         else
        //             para.Add(db.CreateParam("p_for_crown_natts", DbType.String, ParameterDirection.Input, DBNull.Value));

        //         para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, UserID));
        //         para.Add(db.CreateParam("p_for_page", DbType.String, ParameterDirection.Input, PageNo));

        //         DataTable dt = db.ExecuteSP("ipd_search_diamonds_sunrise", para.ToArray(), false);

        //         return dt;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }

        [NonAction]
        private String ExportToExcelOpenXml(DataTable ExportTable)
        {
            DataTableExcelExport ge;
            DataTableEpExcelExport ep_ge;
            if (ExportTable.Rows.Count == 0) return null;

            ge = new DataTableExcelExport(ExportTable, "StoneSelection", "StoneSelection");

            ge.BeforeCreateColumnEvent += BeforeCreateColumnEventHandler;
            ge.AfterCreateCellEvent += AfterCreateCellEventHandler;
            ge.FillingWorksheetEvent += this.FillingWorksheetEventHandler;
            ge.AddHeaderEvent += this.AddHeaderEventHandler;

            string filename = "Stone_Selection_" + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
            string parentpath = System.Web.Hosting.HostingEnvironment.MapPath("~/ExcelFile/");
            //filename = @"c:\" + filename + ".xlsx";
            //filename = filename + ".xlsx";
            if (ConfigurationManager.AppSettings["ConnMode"] == "Oracle")
                parentpath = @"C:\inetpub\wwwroot\Temp\";

            filename = parentpath + filename + ".xlsx";

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ge.CreateExcel(ms);
            System.IO.File.WriteAllBytes(filename, ms.ToArray());

            return filename;

            //return ms;
        }

        [NonAction]
        private String ExportToExcelEpPlus(DataTable ExportTable)
        {
            if (ExportTable.Rows.Count == 0) return null;

            ep_ge = new DataTableEpExcelExport(ExportTable, "StoneSelection", "StoneSelection");

            ep_ge.BeforeCreateColumnEvent += Ep_BeforeCreateColumnEventHandler;
            ep_ge.AfterCreateCellEvent += Ep_AfterCreateCellEventHandler;
            ep_ge.FillingWorksheetEvent += Ep_FillingWorksheetEventHandler;
            ep_ge.AddHeaderEvent += Ep_AddHeaderEventHandler;

            string filename = "Stone_Selection_" + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
            string parentpath = System.Web.Hosting.HostingEnvironment.MapPath("~/ExcelFile/");
            if (System.Configuration.ConfigurationManager.AppSettings["ConnMode"] == "Oracle")
                parentpath = @"C:\inetpub\wwwroot\Temp\";

            filename = parentpath + filename + ".xlsx";

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ep_ge.CreateExcel(ms, parentpath);
            System.IO.File.WriteAllBytes(filename, ms.ToArray());

            return filename;
        }

        [NonAction]
        private void Ep_AddHeaderEventHandler(object sender, ref EpExcelExportLib.EpExcelExport.AddHeaderEventArgs e)
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

            //change by Hitesh on [31-03-2016] as per [Doc No 201]
            EpExcelExportLib.EpExcelExport.ExcelCellFormat Ec = new EpExcelExportLib.EpExcelExport.ExcelCellFormat();
            Ec.isbold = true;
            Ec.fontsize = 12;
            Ec.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(0, 112, 192).ToArgb());
            UInt32 ecInd = ee.AddStyle(Ec);

            if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
            {
                ee.SetCellValue("G1", "SHAIRUGEMS DIAMONDS INVENTORY FOR THE DATE  " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy"), xInd);
            }
            else
            {
                ee.SetCellValue("G1", "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy"), xInd);
                ee.SetCellValue("G2", "UNIT 1, 14/F, PENINSULA SQUARE, EAST WING, 18 SUNG ON STREET, HUNG HOM, KOWLOON, HONG KONG TEL : +852 - 27235100    FAX : +852 - 2314 9100", ecInd);
                ee.SetCellValue("G3", "Email Id : sales@sunrisediam.com    Web : www.sunrisediamonds.com.hk . Download Apps on Android, IOS and Windows", ecInd);
                ee.SetCellValue("B2", "Abbreviation ", statusind);

                ee.SetCellValue("C2", "Buss. Proc", 1);
                ee.SetCellValue("D2", "B", statusind);

                ee.SetCellValue("C3", "Promotion", 1);
                ee.SetCellValue("D3", "P", statusind);

                ee.SetCellValue("B4", "Table & Crown Inclusion = White Inclusion", 1);
                ee.SetCellValue("B5", "Table & Crown Natts = Black Inclusion", 1);

                ee.AddNewRow("A6");

            }
            // End by Hitesh on [31-03-2016] as per [Doc No 201]
        }

        [NonAction]
        private void Ep_BeforeCreateColumnEventHandler(object sender, ref EpExcelExport.ExcelHeader e)
        {
            switch (e.ColName.ToUpper())
            {
                case "VIEW_DNA":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 1;
                    e.HyperlinkColName = "VIEW_DNA";
                    e.Caption = "DNA";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "LOCATION":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 2;
                    e.Caption = "Location";
                    e.ColDataType = eDataTypes.String;
                    e.Width = 14;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "STATUS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 1;
                    else
                        e.ColInd = 3;
                    e.Caption = "Status";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "STONE_REF_NO":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 2;
                    else
                        e.ColInd = 4;
                    e.Caption = "Ref No.";
                    e.ColDataType = eDataTypes.String;
                    e.Width = 13;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Count;
                    break;
                case "SHAPE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 3;
                    else
                        e.ColInd = 5;
                    e.Caption = "Shape";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "POINTER":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 4;
                    else
                        e.ColInd = 6;
                    e.Caption = "Pointer";
                    e.ColDataType = eDataTypes.String;
                    e.Width = 12.15;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "LAB":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 5;
                    else
                        e.ColInd = 7;
                    e.HyperlinkColName = "VERIFY_CERTI_URL";
                    e.Caption = "Lab";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "CERTI_NO":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 6;
                    else
                        e.ColInd = 8;
                    e.Caption = "Certi No.";
                    e.ColDataType = eDataTypes.Number;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.Width = 13.30;
                    break;
                case "SHADE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 9;
                    e.Caption = "Shade";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "COLOR":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 7;
                    else
                        e.ColInd = 10;
                    e.Caption = "Color";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "CLARITY":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 8;
                    else
                        e.ColInd = 11;
                    e.Caption = "Clarity";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "CTS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 9;
                    else
                        e.ColInd = 12;
                    e.Caption = "Cts";
                    e.ColDataType = eDataTypes.Number;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.NumFormat = "#,##0.00";
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                    break;
                case "CUR_RAP_RATE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 10;
                    else
                        e.ColInd = 13;
                    e.Caption = "Rap Price($)";
                    e.ColDataType = eDataTypes.Number;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.NumFormat = "#,##0.00";
                    e.Width = 15;
                    break;
                case "RAP_AMOUNT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 11;
                    else
                        e.ColInd = 14;
                    e.Caption = "Rap Amt($)";
                    e.ColDataType = eDataTypes.Number;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                    e.NumFormat = "#,##0.00";
                    e.Width = 15;
                    break;
                case "SALES_DISC_PER":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 12;
                    else
                        e.ColInd = 15;
                    e.Caption = "Disc %";
                    e.ColDataType = eDataTypes.Number;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                    e.SummFormula = "(1- (" + ep_ge.GetSummFormula("Net Amt($)", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                     "/" + ep_ge.GetSummFormula("Rap Amt($)", EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*-100";
                    e.NumFormat = "#,##0.00";
                    break;
                case "NET_AMOUNT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 13;
                    else
                        e.ColInd = 16;
                    e.Caption = "Net Amt($)";
                    e.ColDataType = eDataTypes.Number;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                    e.NumFormat = "#,##0.00";
                    e.Width = 15;
                    break;
                case "CUT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 14;
                    else
                        e.ColInd = 17;
                    e.Caption = "Cut";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "POLISH":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 15;
                    else
                        e.ColInd = 18;
                    e.Caption = "Polish";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "SYMM":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 16;
                    else
                        e.ColInd = 19;
                    e.Caption = "Symm";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "FLS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 17;
                    else
                        e.ColInd = 20;
                    e.Caption = "Fls";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "LENGTH":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 18;
                    else
                        e.ColInd = 21;
                    e.Caption = "Length";
                    e.ColDataType = eDataTypes.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "WIDTH":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 19;
                    else
                        e.ColInd = 22;
                    e.Caption = "Width";
                    e.ColDataType = eDataTypes.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "DEPTH":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 20;
                    else
                        e.ColInd = 23;
                    e.Caption = "Depth";
                    e.ColDataType = eDataTypes.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "DEPTH_PER":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 21;
                    else
                        e.ColInd = 24;
                    e.Caption = "Depth(%)";
                    e.ColDataType = eDataTypes.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "TABLE_PER":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 22;
                    else
                        e.ColInd = 25;
                    e.Caption = "Table(%)";
                    e.ColDataType = eDataTypes.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "SYMBOL":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 26;
                    e.Caption = "Key To Symbol";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.Width = 15;
                    break;
                case "LUSTER":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 27;
                    e.Caption = "Luster/Milky";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.Width = 15;
                    break;
                case "INCLUSION":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 28;
                    e.Caption = "Table Incl.";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.Width = 15;
                    break;
                case "CROWN_INCLUSION":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 29;
                    e.Caption = "Crown Incl.";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.Width = 15;
                    break;
                case "TABLE_NATTS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 30;
                    e.Caption = "Table Natts";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.Width = 15;
                    break;
                case "CROWN_NATTS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 31;
                    e.Caption = "Crown Natts";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    e.Width = 15;
                    break;
                case "CROWN_ANGLE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 23;
                    else
                        e.ColInd = 32;
                    e.Caption = "Cr Ang";
                    e.ColDataType = eDataTypes.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "CROWN_HEIGHT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 24;
                    else
                        e.ColInd = 33;
                    e.Caption = "Cr Ht";
                    e.ColDataType = eDataTypes.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "PAV_ANGLE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 25;
                    else
                        e.ColInd = 34;
                    e.Caption = "Pav Ang";
                    e.ColDataType = eDataTypes.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "PAV_HEIGHT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 26;
                    else
                        e.ColInd = 35;
                    e.Caption = "Pav Ht";
                    e.ColDataType = eDataTypes.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "GIRDLE_TYPE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 36;

                    e.Caption = "Gridle Type";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "SINSCRIPTION":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 37;
                    e.Caption = "Laser Insc";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "IMAGE_URL":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 27;
                    else
                        e.ColInd = 38;
                    e.HyperlinkColName = "IMAGE_URL";
                    e.Caption = "Image";
                    e.ColDataType = eDataTypes.String;
                    e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    break;
                case "MOVIE_URL":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                    {
                        e.ColInd = 28;
                        e.HyperlinkColName = "MOVIE_URL";
                        e.Caption = "HD Movie";
                        e.ColDataType = eDataTypes.String;
                        e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                    else
                    {
                        e.ColInd = 39;
                        e.HyperlinkColName = "MOVIE_URL";
                        e.Caption = "HD Movie";
                        e.ColDataType = eDataTypes.String;
                        e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                    break;
                default:
                    e.visible = false;
                    break;
            }
        }

        [NonAction]
        private void Ep_FillingWorksheetEventHandler(object sender, ref EpExcelExport.FillingWorksheetEventArgs e)
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

            //change by Hitesh on [31-03-2016] as per [Doc No 201]
            format = new EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Blue.ToArgb());
            format.isbold = true;
            InscStyleindex = ee.AddStyle(format);
            //End by Hitesh on [31-03-2016] as per [Doc No 201]
        }

        [NonAction]
        private void Ep_AfterCreateCellEventHandler(object sender, ref EpExcelExport.ExcelCellFormat e)
        {
            if (e.tableArea == EpExcelExport.TableArea.Header)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.isbold = true;
            }
            else if (e.tableArea == EpExcelExport.TableArea.Detail)
            {
                switch (e.ColumnName)
                {
                    case "DNA":
                        if (e.url.Length > 0)
                        {
                            e.Text = "DNA";
                            e.Formula = @"=HYPERLINK(""" + e.url + @""",""" + e.Text + @""")";

                        }
                        break;
                    case "Lab":

                        if (e.url.Length > 0)
                        {
                            e.Formula = @"=HYPERLINK(""" + e.url + @""",""" + e.Text + @""")";
                        }
                        break;
                    case "Image":
                        if (e.url.Length > 0)
                        {
                            // Change By Hitesh on [28-03-2017] as per [Doc No 701]
                            if (e.url.Contains("ViewImgVideo.aspx"))
                            {
                                e.Formula = @"=HYPERLINK(""" + e.url.Replace("ViewImgVideo.aspx", "ViewImage.aspx") + @""",""Image"")";
                            }
                            else
                                e.Formula = @"=HYPERLINK(""" + e.url + @""",""Image"")";
                            e.Text = "Image";
                        }
                        break;
                    case "HD Movie":
                        if (e.url.Length > 0)
                        {
                            // Change By Hitesh on [28-03-2017] as per [Doc No 701]
                            if (e.url.Contains("ViewHdImageVideo.aspx"))
                            {
                                e.Formula = @"=HYPERLINK(""" + e.url.Replace("ViewHdImageVideo.aspx", "ViewHDImage.aspx") + @""",""HD Movie"")";
                            }
                            else
                                e.Formula = @"=HYPERLINK(""" + e.url + @""",""HD Movie"")";
                            e.Text = "HD Movie";
                        }
                        break;
                    case "Disc %":
                        e.StyleInd = DiscNormalStyleindex;
                        break;
                    case "Cut":
                        if (e.Text == "3EX")
                            e.StyleInd = CutNormalStyleindex;
                        //e.StyleInd = CutNormalStyleindex;
                        break;
                    case "Net Amt($)":
                        //change by Hitesh on 18-03-2016] as per [Doc No 201]
                        e.StyleInd = DiscNormalStyleindex;
                        break;
                    case "Laser Insc":
                        e.StyleInd = InscStyleindex;
                        break;
                    default:
                        break;
                }
            }
            else if (e.tableArea == EpExcelExport.TableArea.Footer)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.isbold = true;

                switch (e.ColumnName)
                {
                    case "Disc %":
                        break;
                    case "Image":
                        break;
                    case "HD Movie":
                        break;
                    default:
                        break;
                }

            }

        }

        [NonAction]
        private void AddHeaderEventHandler(object sender, ref AddHeaderEventArgs e)
        {
            ExcelExport ee = (ExcelExport)sender;

            ExcelCellFormat f = new ExcelCellFormat();
            f.isbold = true;
            f.fontsize = 11;
            UInt32 statusind = ee.AddStyle(f);

            ExcelCellFormat c = new ExcelCellFormat();
            c.isbold = true;
            c.fontsize = 24;
            c.forColorArgb = ExcelExport.GetHexValue(System.Drawing.Color.FromArgb(0, 112, 192).ToArgb());

            if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
            {
                ee.SetCellValue("C1", "SHAIRUGEMS DIAMONDS INVENTORY FOR THE DATE  " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy"), ee.AddStyle(c));
            }
            else
            {
                ee.SetCellValue("C1", "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + Lib.Models.Common.GetHKTime().Date.ToString("dd-MMM-yyyy"), ee.AddStyle(c));
                ee.SetCellValue("C2", "Note :", statusind);
                ee.SetCellValue("D2", "Promotion Stones have fix Cash Selling Price", 1);
                ee.SetCellValue("C3", "Status :", statusind);
                ee.SetCellValue("D3", "Promotion", 1);
                ee.SetCellValue("E3", "P", statusind);
                ee.SetCellValue("F3", "Buss. Proc", 1);
                ee.SetCellValue("G3", "B", statusind);

                ee.AddNewRow("A4");
                ee.AddNewRow("A5");
            }
        }

        [NonAction]
        private void BeforeCreateColumnEventHandler(object sender, ref ExcelHeader e)
        {
            switch (e.ColName.ToUpper())
            {
                case "SR":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 1;
                    else
                        e.ColInd = 1;
                    e.Caption = "Sr";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "VIEW_DNA":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 2;
                    e.HyperlinkColName = "VIEW_DNA";
                    e.Caption = "DNA";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "IMAGE_URL":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 2;
                    else
                        e.ColInd = 3;
                    e.HyperlinkColName = "IMAGE_URL";
                    e.Caption = "Image";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    e.SummText = "Total:";
                    e.SummFunction = DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Custom;
                    break;
                case "MOVIE_URL":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                    {
                        e.ColInd = 3;
                        e.HyperlinkColName = "MOVIE_URL";
                        e.Caption = "HD Movie";
                        e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    }
                    else
                    {
                        e.ColInd = 4;
                        e.HyperlinkColName = "MOVIE_URL";
                        e.Caption = "HD Movie";
                        e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    }
                    break;
                case "STONE_REF_NO":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 4;
                    else
                        e.ColInd = 5;
                    e.Caption = "Ref No.";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    e.SummFunction = DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Count;
                    e.NumFormat = "#,##0";
                    break;
                case "LAB":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 5;
                    else
                        e.ColInd = 7;
                    e.HyperlinkColName = "VERIFY_CERTI_URL";
                    e.Caption = "Lab";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "SHAPE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 6;
                    else
                        e.ColInd = 8;
                    e.Caption = "Shape";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "POINTER":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 7;
                    else
                        e.ColInd = 9;
                    e.Caption = "Pointer";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "CERTI_NO":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 8;
                    else
                        e.ColInd = 10;
                    e.Caption = "Certi No.";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    e.Width = 13.30;
                    break;
                case "SHADE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 11;
                    e.Caption = "Shade";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "COLOR":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 9;
                    else
                        e.ColInd = 12;
                    e.Caption = "Color";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "CLARITY":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 10;
                    else
                        e.ColInd = 13;
                    e.Caption = "Clarity";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "CTS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 11;
                    else
                        e.ColInd = 14;
                    e.Caption = "Cts";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right;
                    e.SummFunction = DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Sum;
                    break;
                case "CUR_RAP_RATE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 12;
                    else
                        e.ColInd = 15;
                    e.Caption = "Rap Price($)";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right;
                    e.NumFormat = "#,##0.00";
                    e.Width = 15;
                    break;
                case "RAP_AMOUNT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 13;
                    else
                        e.ColInd = 16;
                    e.Caption = "Rap Amt($)";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right;
                    e.SummFunction = DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Sum;
                    e.NumFormat = "#,##0.00";
                    e.Width = 15;
                    break;
                case "SALES_DISC_PER":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 14;
                    else
                        e.ColInd = 17;
                    e.Caption = "Disc %";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right;
                    e.SummFunction = DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Custom;
                    e.SummFormula = "(1- (" + ge.GetSummFormula("Net Amt($)", DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Sum) +
                                     "/" + ge.GetSummFormula("Rap Amt($)", DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Sum) + " ))*-100";
                    e.NumFormat = "#,##0.00";
                    break;
                case "NET_AMOUNT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 15;
                    else
                        e.ColInd = 18;
                    e.Caption = "Net Amt($)";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right;
                    e.SummFunction = DocumentFormat.OpenXml.Spreadsheet.TotalsRowFunctionValues.Sum;
                    e.NumFormat = "#,##0.00";
                    e.Width = 15;
                    break;
                case "CUT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 16;
                    else
                        e.ColInd = 19;
                    e.Caption = "Cut";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "POLISH":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 17;
                    else
                        e.ColInd = 20;
                    e.Caption = "Polish";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "SYMM":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 18;
                    else
                        e.ColInd = 21;
                    e.Caption = "Symm";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "FLS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 19;
                    else
                        e.ColInd = 22;
                    e.Caption = "Fls";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "MEASUREMENT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.visible = false;
                    e.Caption = "Measurement";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "LENGTH":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 20;
                    else
                        e.ColInd = 23;
                    e.Caption = "Length";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "WIDTH":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 21;
                    else
                        e.ColInd = 24;
                    e.Caption = "Width";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "DEPTH":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 22;
                    else
                        e.ColInd = 25;
                    e.Caption = "Depth";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "DEPTH_PER":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 23;
                    else
                        e.ColInd = 26;
                    e.Caption = "Depth(%)";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "TABLE_PER":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 24;
                    else
                        e.ColInd = 27;
                    e.Caption = "Table(%)";
                    e.NumFormat = "#,##0.00";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "USER_COMMENTS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 26;
                    else
                        e.visible = false;
                    e.Caption = "User Comments";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "SYMBOL":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 28;
                    e.Caption = "Key To Symbol";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    e.Width = 15;
                    break;
                case "INCLUSION":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 29;
                    e.Caption = "Table Incl.";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    e.Width = 15;
                    break;
                case "TABLE_NATTS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 30;
                    e.Caption = "Table Natts";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    e.Width = 15;
                    break;
                case "CROWN_ANGLE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 27;
                    else
                        e.ColInd = 31;
                    e.Caption = "Cr Ang";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;

                    break;
                case "CROWN_HEIGHT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 28;
                    else
                        e.ColInd = 32;
                    e.Caption = "Cr Ht";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "PAV_ANGLE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 29;
                    else
                        e.ColInd = 33;
                    e.Caption = "Pav Ang";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "PAV_HEIGHT":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 30;
                    else
                        e.ColInd = 34;
                    e.Caption = "Pav Ht";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                    e.NumFormat = "#,##0.00";
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "GIRDLE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 31;
                    else
                        e.visible = false;
                    e.Caption = "Gridle";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "GIRDLE_TYPE":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.visible = false;
                    else
                        e.ColInd = 35;
                    e.Caption = "Gridle Type";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                case "STATUS":
                    if (System.Configuration.ConfigurationManager.AppSettings["Location"] == "M")
                        e.ColInd = 25;
                    else
                        e.ColInd = 6;
                    e.Caption = "Status";
                    e.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                    break;
                default:
                    e.visible = false;
                    break;
            }
        }

        [NonAction]
        private void FillingWorksheetEventHandler(object sender, ref FillingWorksheetEventArgs e)
        {
            //return;
            ExcelExport ee = (ExcelExport)sender;

            ExcelFormat format = new ExcelFormat();

            //format.backgroundArgb = "FCE53D";
            //promotionStyleindex = ee.AddStyle(format);

            //format = new ExcelFormat();
            //format.backgroundArgb = ExcelExport.GetHexValue(System.Drawing.Color.LightPink.ToArgb());
            //NewStyleionindex = ee.AddStyle(format);

            //format = new ExcelFormat();
            //format.backgroundArgb = ExcelExport.GetHexValue(System.Drawing.Color.Aquamarine.ToArgb());
            //businessStyleionindex = ee.AddStyle(format);


            format = new ExcelFormat();
            format.forColorArgb = ExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            format.isbold = true;
            DiscNormalStyleindex = ee.AddStyle(format);

            format = new ExcelFormat();
            format.isbold = true;
            CutNormalStyleindex = ee.AddStyle(format);

            //format = new ExcelFormat();
            //format.backgroundArgb = "FCE53D";
            //format.forColorArgb = ExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            //DiscpromotionStyleindex = ee.AddStyle(format);


            //format = new ExcelFormat();
            //format.backgroundArgb = ExcelExport.GetHexValue(System.Drawing.Color.LightPink.ToArgb());
            //format.forColorArgb = ExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            //DiscNewStyleionindex = ee.AddStyle(format);

            //format = new ExcelFormat();
            //format.backgroundArgb = ExcelExport.GetHexValue(System.Drawing.Color.Aquamarine.ToArgb());
            //format.forColorArgb = ExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            //DiscbusinessStyleionindex = ee.AddStyle(format);

        }

        [NonAction]
        private void AfterCreateCellEventHandler(object sender, ref ExcelCellFormat e)
        {
            //return;
            if (e.tableArea == TableArea.Header)
            {
                e.backgroundArgb = ExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                e.isbold = true;

            }
            else if (e.tableArea == TableArea.Detail)
            {
                switch (e.ColumnName)
                {
                    case "DNA":
                        if (e.url.Length > 0)
                        {
                            e.Text = "DNA";
                        }
                        break;
                    case "Image":
                        if (e.url.Length > 0)
                        {
                            // comment bcoz this is not work in open Xml
                            //// Change By Hitesh on [28-03-2017] as per [Doc No 701]
                            //if (e.url.Contains("ViewImgVideo.aspx"))
                            //{
                            //    e.Formula = @"=HYPERLINK(""" + e.url.Replace("ViewImgVideo.aspx", "ViewImage.aspx") + @""",""Image"")";
                            //    //e.Formula = @"=HYPERLINK(""" + e.url + @""",""Image"")";
                            //}
                            //else
                            //    e.Formula = @"=HYPERLINK(""" + e.url + @""",""Image"")";
                            e.Text = "Image";
                        }
                        break;
                    case "HD Movie":
                        if (e.url.Length > 0)
                        {
                            // comment bcoz this is not work in open Xml
                            //// Change By Hitesh on [28-03-2017] as per [Doc No 701]
                            //if (e.url.Contains("ViewHdImageVideo.aspx"))
                            //{
                            //    e.Formula = @"=HYPERLINK(""" + e.url.Replace("ViewHdImageVideo.aspx", "ViewHDImage.aspx") + @""",""HD Movie"")";
                            //}
                            //else
                            //    e.Formula = @"=HYPERLINK(""" + e.url + @""",""HD Movie"")";
                            e.Text = "HD Movie";
                        }
                        break;
                    case "Disc %":
                        e.StyleInd = DiscNormalStyleindex;
                        break;
                    case "Net Amt($)":
                        //e.Formula = "@[Rap Amt($)]-(@[Rap Amt($)]*(@[Disc %]/100))";
                        //e.Formula = "StoneSelection[[#This Row],[Rap Amt($)]] + (StoneSelection[[#This Row],[Rap Amt($)]] * (StoneSelection[[#This Row],[Disc %]] / 100))";
                        break;
                    case "Cut":
                        if (e.Text == "3EX")
                            e.StyleInd = CutNormalStyleindex;
                        break;
                    default:
                        break;
                }
            }

            else if (e.tableArea == TableArea.Footer)
            {
                e.backgroundArgb = ExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.isbold = true;
                e.ul = DocumentFormat.OpenXml.Spreadsheet.UnderlineValues.None;

                switch (e.ColumnName)
                {
                    case "Disc %":
                        e.forColorArgb = ExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
                        break;
                    default:
                        break;
                }

            }

        }

        [NonAction]
        private bool CreateMediaZip(DataTable DataTableData, string MediaType, string FolderPath, string SubFolderPath, string ZipFileName, string DownloadURL)
        {
            bool flag = false;

            try
            {
                if (MediaType.ToLower() == "image")
                {
                    DataRow[] dra = DataTableData.Select("P_SEQ_NO IS NULL");

                    DataTableData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                    DataTableData = DataTableData.DefaultView.ToTable();

                    Directory.CreateDirectory(SubFolderPath);
                    bool isJson = false;
                    foreach (DataRow drItem in DataTableData.Rows)
                    {
                        string url = string.Empty;
                        url = Convert.ToString(drItem["Overseas_Image_Download_Link"]);
                        if (url != "")
                        {
                            if (CheckExists(url))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\PR.jpg");
                                }
                                isJson = true;
                            }
                        }
                        url = Convert.ToString(drItem["Overseas_Image_Download_Link1"]);
                        if (url != "")
                        {
                            if (CheckExists(url))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\HB.jpg");
                                }
                                isJson = true;
                            }
                        }
                        url = Convert.ToString(drItem["Overseas_Image_Download_Link2"]);
                        if (url != "")
                        {
                            if (CheckExists(url))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\HT.jpg");
                                }
                                isJson = true;
                            }
                        }
                        url = Convert.ToString(drItem["Overseas_Image_Download_Link3"]);
                        if (url != "")
                        {
                            if (CheckExists(url))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\AS.jpg");
                                }
                                isJson = true;
                            }
                        }
                    }

                    if (!isJson)
                    {
                        flag = false;
                    }
                    else
                    {
                        if (Directory.GetDirectories(FolderPath + ZipFileName).Length > 0)
                        {
                            ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
                            flag = true;
                        }
                    }
                    //Directory.Delete(SubFolderPath, true);
                }
                else if (MediaType.ToLower() == "video")
                {
                    DataRow[] dra = DataTableData.Select("P_SEQ_NO IS NULL");

                    DataTableData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                    DataTableData = DataTableData.DefaultView.ToTable();

                    Directory.CreateDirectory(SubFolderPath);
                    bool isJson = false;
                    foreach (DataRow drItem in DataTableData.Rows)
                    {
                        string url = string.Empty;
                        if (Convert.ToBoolean(drItem["IsOverseas"]))
                        {
                            isJson = false;
                        }
                        else
                        {
                            if (Convert.ToBoolean(drItem["bMP4"]) == true || Convert.ToBoolean(drItem["bJson"]) == true)
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                if (Convert.ToBoolean(drItem["bMP4"]) == true)
                                {
                                    url = DownloadURL + "imaged/" + Convert.ToString(drItem["p_seq_no"]) + "/video.mp4";
                                    if (CheckExists(url))
                                    {
                                        using (WebClient client = new WebClient())
                                        {
                                            client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + ".mp4");
                                        }
                                        isJson = isJson ? isJson : false;
                                    }
                                }
                                else if (Convert.ToBoolean(drItem["bJson"]) == true)
                                {
                                    //isJson = true;
                                    Directory.Delete(subStoneFolder, true);
                                }
                            }
                        }
                    }

                    if (isJson)
                    {
                        flag = false;
                    }
                    else
                    {
                        if (Directory.GetDirectories(FolderPath + ZipFileName).Length > 0)
                        {
                            ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
                            flag = true;
                        }
                    }
                    Directory.Delete(SubFolderPath, true);
                }
                else if (MediaType.ToLower() == "certificate")
                {

                    DataRow[] dra = DataTableData.Select("P_SEQ_NO IS NULL");
                    bool isJson = false;
                    DataTableData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                    DataTableData = DataTableData.DefaultView.ToTable();
                    Directory.CreateDirectory(SubFolderPath);

                    foreach (DataRow drItem in DataTableData.Rows)
                    {
                        string url = string.Empty;
                        if (Convert.ToBoolean(drItem["IsOverseas"]))
                        {
                            url = Convert.ToString(drItem["Overseas_Certi_Download_Link"]);
                            if (url != "")
                            {
                                if (CheckExists(url))
                                {
                                    string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                    Directory.CreateDirectory(subStoneFolder);
                                    string extension = System.IO.Path.GetExtension(url.Split('?')[0]);
                                    if (extension.ToLower() != ".pdf")
                                    {
                                        extension = ".jpg";
                                    }
                                    using (WebClient client = new WebClient())
                                    {
                                        client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + extension);
                                    }
                                    isJson = true;
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(drItem["view_certi_url"])))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                url = Convert.ToString(drItem["view_certi_url"]);
                                if (CheckExists(url))
                                {
                                    using (WebClient client = new WebClient())
                                    {
                                        try
                                        {
                                            string extension = System.IO.Path.GetExtension(url.Split('?')[0]);
                                            if (extension != ".pdf")
                                            {
                                                extension = ".jpg";
                                            }
                                            client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + extension);
                                            isJson = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            Directory.Delete(subStoneFolder);
                                            isJson = false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!isJson)
                    {
                        flag = false;
                    }
                    else
                    {
                        if (Directory.GetDirectories(FolderPath + ZipFileName).Length > 0)
                        {
                            ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
                            flag = true;
                        }
                    }
                    Directory.Delete(SubFolderPath, true);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
            return flag;
        }

        //[NonAction]
        //private bool CreateMediaZip(DataTable DataTableData, string MediaType, string FolderPath, string SubFolderPath, string ZipFileName, string DownloadURL)
        //{
        //    bool flag = false;

        //    try
        //    {
        //        if (MediaType.ToLower() == "image")
        //        {
        //            DataRow[] dra = DataTableData.Select("P_SEQ_NO IS NULL");

        //            DataTableData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
        //            DataTableData = DataTableData.DefaultView.ToTable();

        //            Directory.CreateDirectory(SubFolderPath);
        //            bool isJson = false;

        //            foreach (DataRow drItem in DataTableData.Rows)
        //            {
        //                string url = string.Empty;
        //                if (!Convert.ToBoolean(drItem["IsOverseas"]))
        //                {

        //                    if (Convert.ToBoolean(drItem["bPRimg"]) == true
        //                    || Convert.ToBoolean(drItem["bASimg"]) == true
        //                    || Convert.ToBoolean(drItem["bHTimg"]) == true
        //                    || Convert.ToBoolean(drItem["bHBimg"]) == true)
        //                    {
        //                        string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
        //                        Directory.CreateDirectory(subStoneFolder);
        //                        if (Convert.ToBoolean(drItem["bPRimg"]) == true)
        //                        {
        //                            url = DownloadURL + Convert.ToString(drItem["certi_no"]) + "/PR.jpg";
        //                            if (CheckExists(url))
        //                            {
        //                                using (WebClient client = new WebClient())
        //                                {
        //                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\PR.jpg");
        //                                }
        //                            }
        //                        }
        //                        if (Convert.ToBoolean(drItem["bASimg"]) == true)
        //                        {
        //                            url = DownloadURL + Convert.ToString(drItem["certi_no"]) + "/AS.jpg";
        //                            if (CheckExists(url))
        //                            {
        //                                using (WebClient client = new WebClient())
        //                                {
        //                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\AS.jpg");
        //                                }
        //                            }
        //                        }
        //                        if (Convert.ToBoolean(drItem["bHTimg"]) == true)
        //                        {
        //                            url = DownloadURL + Convert.ToString(drItem["certi_no"]) + "/HT.jpg";
        //                            if (CheckExists(url))
        //                            {
        //                                using (WebClient client = new WebClient())
        //                                {
        //                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\HT.jpg");
        //                                }
        //                            }
        //                        }
        //                        if (Convert.ToBoolean(drItem["bHBimg"]) == true)
        //                        {
        //                            url = DownloadURL + Convert.ToString(drItem["certi_no"]) + "/HB.jpg";
        //                            if (CheckExists(url))
        //                            {
        //                                using (WebClient client = new WebClient())
        //                                {
        //                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\HB.jpg");
        //                                }
        //                            }
        //                        }
        //                        isJson = true;
        //                    }
        //                }
        //            }

        //            if (!isJson)
        //            {
        //                flag = false;
        //            }
        //            else
        //            {
        //                if (Directory.GetDirectories(FolderPath + ZipFileName).Length > 0)
        //                {
        //                    ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
        //                    flag = true;
        //                }
        //            }
        //            //Directory.Delete(SubFolderPath, true);
        //        }
        //        else if (MediaType.ToLower() == "video")
        //        {
        //            DataRow[] dra = DataTableData.Select("P_SEQ_NO IS NULL");

        //            DataTableData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
        //            DataTableData = DataTableData.DefaultView.ToTable();

        //            Directory.CreateDirectory(SubFolderPath);
        //            bool isJson = false;
        //            foreach (DataRow drItem in DataTableData.Rows)
        //            {
        //                string url = string.Empty;
        //                if (!Convert.ToBoolean(drItem["IsOverseas"]))
        //                {
        //                    if (Convert.ToBoolean(drItem["bMP4"]) == true || Convert.ToBoolean(drItem["bJson"]) == true)
        //                    {
        //                        string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
        //                        Directory.CreateDirectory(subStoneFolder);
        //                        if (Convert.ToBoolean(drItem["bMP4"]) == true)
        //                        {
        //                            url = DownloadURL + "imaged/" + Convert.ToString(drItem["p_seq_no"]) + "/video.mp4";
        //                            using (WebClient client = new WebClient())
        //                            {
        //                                client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + ".mp4");
        //                            }
        //                            isJson = isJson ? isJson : false;
        //                        }
        //                        else if (Convert.ToBoolean(drItem["bJson"]) == true)
        //                        {
        //                            isJson = true;
        //                        }
        //                    }
        //                }
        //            }

        //            if (isJson)
        //            {
        //                flag = false;
        //            }
        //            else
        //            {
        //                if (Directory.GetDirectories(FolderPath + ZipFileName).Length > 0)
        //                {
        //                    ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
        //                    flag = true;
        //                }
        //            }
        //            Directory.Delete(SubFolderPath, true);
        //        }
        //        else if (MediaType.ToLower() == "certificate")
        //        {

        //            DataRow[] dra = DataTableData.Select("P_SEQ_NO IS NULL");
        //            bool isJson = false;
        //            DataTableData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
        //            DataTableData = DataTableData.DefaultView.ToTable();
        //            Directory.CreateDirectory(SubFolderPath);

        //            foreach (DataRow drItem in DataTableData.Rows)
        //            {
        //                string url = string.Empty;
        //                if (!Convert.ToBoolean(drItem["IsOverseas"]))
        //                {
        //                    if (!string.IsNullOrEmpty(Convert.ToString(drItem["view_certi_url"])))
        //                    {
        //                        string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
        //                        Directory.CreateDirectory(subStoneFolder);
        //                        url = Convert.ToString(drItem["view_certi_url"]);
        //                        using (WebClient client = new WebClient())
        //                        {
        //                            try
        //                            {
        //                                string extension = System.IO.Path.GetExtension(url.Split('?')[0]);
        //                                if (extension != ".pdf")
        //                                {
        //                                    extension = ".jpg";
        //                                }
        //                                client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + extension);
        //                                isJson = true;
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                Directory.Delete(subStoneFolder);
        //                                isJson = false;
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (!isJson)
        //            {
        //                flag = false;
        //            }
        //            else
        //            {
        //                if (Directory.GetDirectories(FolderPath + ZipFileName).Length > 0)
        //                {
        //                    ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
        //                    flag = true;
        //                }
        //            }
        //            Directory.Delete(SubFolderPath, true);
        //        }
        //        else if (MediaType.ToLower() == "pdf")
        //        {
        //            //List<SearchStone> stones = DataTableExtension.ToList<SearchStone>(DataTableData);
        //            //string template = Models.PdfTemplate.GetHTMLString(stones, dataRows[0], Request.RequestUri.GetLeftPart(UriPartial.Authority));
        //            //HtmlToPdf renderer = new HtmlToPdf();
        //            ////   pdf = renderer.Document;
        //            //renderer.Options.DisplayHeader = true;
        //            //renderer.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
        //            //renderer.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
        //            //renderer.Options.MarginRight = 10;
        //            //renderer.Options.MarginTop = 10;
        //            //renderer.Options.PdfPageSize = PdfPageSize.A4;

        //            //PdfTextSection text = new PdfTextSection(0, 10, "Page: {page_number} of {total_pages}  ", new System.Drawing.Font("Verdana", 8));
        //            //text.HorizontalAlign = PdfTextHorizontalAlign.Right;
        //            //renderer.Header.Add(text);

        //            //var pdfDoc = renderer.ConvertHtmlString(template, Request.RequestUri.GetLeftPart(UriPartial.Authority));
        //            //pdfDoc.Save(FolderPath + ZipFileName + ".pdf");
        //            Models.PdfTemplate.ExportToPdf(DataTableData, FolderPath + ZipFileName + ".pdf");

        //            flag = true;
        //        }
        //        else
        //        {
        //            return flag;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        flag = false;
        //        throw ex;
        //    }
        //    return flag;
        //}

        [NonAction]
        private bool CreateMediaZipFromURL(string _realpath, string ZipFileName, string MediaType, string MediaURL, string StoneId)
        {
            bool flag = false;

            try
            {
                if (MediaType.ToLower() == "image")
                {
                    string subStoneFolder = ZipFileName + "//" + StoneId;
                    string url = string.Empty;
                    Directory.CreateDirectory(subStoneFolder);
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri(MediaURL), subStoneFolder + "/" + StoneId + ".jpg");
                    }

                    if (Directory.GetDirectories(ZipFileName).Length > 0)
                    {
                        ZipFile.CreateFromDirectory(subStoneFolder, ZipFileName + ".zip");
                        flag = true;
                    }
                }
                else if (MediaType.ToLower() == "video")
                {
                    string subStoneFolder = ZipFileName + "//" + StoneId;
                    string url = string.Empty;
                    Directory.CreateDirectory(subStoneFolder);
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri(MediaURL), subStoneFolder + "/" + StoneId + ".mp4");
                    }

                    if (Directory.GetDirectories(ZipFileName).Length > 0)
                    {
                        ZipFile.CreateFromDirectory(subStoneFolder, ZipFileName + ".zip");
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
            return flag;
        }

        [NonAction]
        private bool CreateMediaZipCart(DataTable DataTableData, string MediaType, string FolderPath, string SubFolderPath, string ZipFileName, string DownloadURL)
        {
            bool flag = false;

            try
            {
                if (MediaType.ToLower() == "image")
                {
                    DataRow[] dra = DataTableData.Select("sr IS NULL");

                    DataTableData.DefaultView.RowFilter = "sr IS NOT NULL";
                    DataTableData = DataTableData.DefaultView.ToTable();

                    Directory.CreateDirectory(SubFolderPath);
                    bool isJson = false;
                    foreach (DataRow drItem in DataTableData.Rows)
                    {
                        string url = string.Empty;
                        url = Convert.ToString(drItem["Overseas_Image_Download_Link"]);
                        if (url != "")
                        {
                            if (CheckExists(url))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\PR.jpg");
                                }
                                isJson = true;
                            }
                        }
                        url = Convert.ToString(drItem["Overseas_Image_Download_Link1"]);
                        if (url != "")
                        {
                            if (CheckExists(url))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\HB.jpg");
                                }
                                isJson = true;
                            }
                        }
                        url = Convert.ToString(drItem["Overseas_Image_Download_Link2"]);
                        if (url != "")
                        {
                            if (CheckExists(url))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\HT.jpg");
                                }
                                isJson = true;
                            }
                        }
                        url = Convert.ToString(drItem["Overseas_Image_Download_Link3"]);
                        if (url != "")
                        {
                            if (CheckExists(url))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\AS.jpg");
                                }
                                isJson = true;
                            }
                        }
                    }

                    if (!isJson)
                    {
                        flag = false;
                    }
                    else
                    {
                        if (Directory.GetDirectories(FolderPath + ZipFileName).Length > 0)
                        {
                            ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
                            flag = true;
                        }
                    }
                    //Directory.Delete(SubFolderPath, true);
                }
                else if (MediaType.ToLower() == "video")
                {
                    DataRow[] dra = DataTableData.Select("sr IS NULL");

                    DataTableData.DefaultView.RowFilter = "sr IS NOT NULL";
                    DataTableData = DataTableData.DefaultView.ToTable();

                    Directory.CreateDirectory(SubFolderPath);
                    bool isJson = false;
                    foreach (DataRow drItem in DataTableData.Rows)
                    {
                        string url = string.Empty;
                        if (Convert.ToBoolean(drItem["IsOverseas"]))
                        {
                            isJson = false;
                        }
                        else
                        {
                            if (Convert.ToBoolean(drItem["bMP4"]) == true || Convert.ToBoolean(drItem["bJson"]) == true)
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                if (Convert.ToBoolean(drItem["bMP4"]) == true)
                                {
                                    url = DownloadURL + "imaged/" + Convert.ToString(drItem["p_seq_no"]) + "/video.mp4";
                                    if (CheckExists(url))
                                    {
                                        using (WebClient client = new WebClient())
                                        {
                                            client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + ".mp4");
                                        }
                                        isJson = isJson ? isJson : false;
                                    }
                                }
                                else if (Convert.ToBoolean(drItem["bJson"]) == true)
                                {
                                    //isJson = true;
                                    Directory.Delete(subStoneFolder, true);
                                }
                            }
                        }
                    }

                    if (isJson)
                    {
                        flag = false;
                    }
                    else
                    {
                        if (Directory.GetDirectories(FolderPath + ZipFileName).Length > 0)
                        {
                            ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
                            flag = true;
                        }
                    }
                    Directory.Delete(SubFolderPath, true);
                }
                else if (MediaType.ToLower() == "certificate")
                {

                    DataRow[] dra = DataTableData.Select("sr IS NULL");
                    bool isJson = false;
                    DataTableData.DefaultView.RowFilter = "sr IS NOT NULL";
                    DataTableData = DataTableData.DefaultView.ToTable();
                    Directory.CreateDirectory(SubFolderPath);

                    foreach (DataRow drItem in DataTableData.Rows)
                    {
                        string url = string.Empty;
                        if (Convert.ToBoolean(drItem["IsOverseas"]))
                        {
                            url = Convert.ToString(drItem["Overseas_Certi_Download_Link"]);
                            if (url != "")
                            {
                                if (CheckExists(url))
                                {
                                    string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                    Directory.CreateDirectory(subStoneFolder);
                                    string extension = System.IO.Path.GetExtension(url.Split('?')[0]);
                                    if (extension.ToLower() != ".pdf")
                                    {
                                        extension = ".jpg";
                                    }

                                    using (WebClient client = new WebClient())
                                    {
                                        client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + extension);
                                    }
                                    isJson = true;
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(drItem["view_certi_url"])))
                            {
                                string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                                Directory.CreateDirectory(subStoneFolder);
                                url = Convert.ToString(drItem["view_certi_url"]);
                                if (CheckExists(url))
                                {
                                    using (WebClient client = new WebClient())
                                    {
                                        try
                                        {
                                            string extension = System.IO.Path.GetExtension(url.Split('?')[0]);
                                            if (extension != ".pdf")
                                            {
                                                extension = ".jpg";
                                            }
                                            client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + extension);
                                            isJson = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            Directory.Delete(subStoneFolder);
                                            isJson = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!isJson)
                    {
                        flag = false;
                    }
                    else
                    {
                        if (Directory.GetDirectories(FolderPath + ZipFileName).Length > 0)
                        {
                            ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
                            flag = true;
                        }
                    }
                    Directory.Delete(SubFolderPath, true);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
            return flag;
        }

        #endregion

        [HttpPost]
        public IHttpActionResult ExpireAvailStock([FromBody] JObject data)
        {
            SearchDiamondsRequest searchdiamondsrequest = new SearchDiamondsRequest();

            try
            {
                searchdiamondsrequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db1 = new Database();
                Database db2 = new Database();

                List<IDbDataParameter> para3 = new List<IDbDataParameter>();
                para3.Add(db1.CreateParam("p_for_iId", DbType.String, ParameterDirection.Input, searchdiamondsrequest.iId));
                DataTable dt2 = db1.ExecuteSP("SearchStock_NoFoundDet_Update", para3.ToArray(), false);

                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse
                {
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetStockAvail()
        {
            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("p_for_Action", DbType.String, ParameterDirection.Input, "Notify Me"));
                para.Add(db.CreateParam("p_for_iUserId", DbType.String, ParameterDirection.Input, userID));

                DataTable dt = db.ExecuteSP("NoFoundSearchStock_Get", para.ToArray(), false);


                foreach (DataRow dr in dt.Rows)
                {
                    List<SearchDiamondsResponse> searchDiamondsResponses = new List<SearchDiamondsResponse>();

                    SearchDiamondsRequest searchdiamondsrequest = new SearchDiamondsRequest();
                    searchdiamondsrequest = DataTableExtension.ToObject<SearchDiamondsRequest>(dr);

                    //StockDownloadRequest stockDownloadRequest = new StockDownloadRequest();
                    //stockDownloadRequest = DataTableExtension.ToObject<StockDownloadRequest>(dr);

                    DataTable dtData = SearchStockInner(searchdiamondsrequest, true, false);

                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        int Total_Stones = dtData.Rows[0]["stone_ref_no"].ToString() != "" ? Int32.Parse(dtData.Rows[0]["stone_ref_no"].ToString()) : 0;
                        if (Total_Stones > 0)
                        {
                            DataTable dt2 = new DataTable();

                            dt2.Columns.Add("SearchID", typeof(string));
                            dt2.Columns.Add("SearchName", typeof(string));
                            dt2.Columns.Add("StoneID", typeof(string));
                            dt2.Columns.Add("CertiNo", typeof(string));
                            dt2.Columns.Add("Shape", typeof(string));
                            dt2.Columns.Add("Pointer", typeof(string));
                            dt2.Columns.Add("Color", typeof(string));
                            dt2.Columns.Add("Clarity", typeof(string));
                            dt2.Columns.Add("Cut", typeof(string));
                            dt2.Columns.Add("Polish", typeof(string));
                            dt2.Columns.Add("Symm", typeof(string));
                            dt2.Columns.Add("Fls", typeof(string));
                            dt2.Columns.Add("Lab", typeof(string));
                            dt2.Columns.Add("Inclusion", typeof(string));
                            dt2.Columns.Add("Natts", typeof(string));
                            dt2.Columns.Add("Shade", typeof(string));
                            dt2.Columns.Add("FromCts", typeof(string));
                            dt2.Columns.Add("ToCts", typeof(string));
                            dt2.Columns.Add("FormDisc", typeof(string));
                            dt2.Columns.Add("ToDisc", typeof(string));
                            dt2.Columns.Add("FormPricePerCts", typeof(string));
                            dt2.Columns.Add("ToPricePerCts", typeof(string));
                            dt2.Columns.Add("FormNetAmt", typeof(string));
                            dt2.Columns.Add("ToNetAmt", typeof(string));
                            dt2.Columns.Add("FormDepth", typeof(string));
                            dt2.Columns.Add("ToDepth", typeof(string));
                            dt2.Columns.Add("FormLength", typeof(string));
                            dt2.Columns.Add("ToLength", typeof(string));
                            dt2.Columns.Add("FormWidth", typeof(string));
                            dt2.Columns.Add("ToWidth", typeof(string));
                            dt2.Columns.Add("FormDepthPer", typeof(string));
                            dt2.Columns.Add("ToDepthPer", typeof(string));
                            dt2.Columns.Add("FormTablePer", typeof(string));
                            dt2.Columns.Add("ToTablePer", typeof(string));
                            dt2.Columns.Add("HasImage", typeof(string));
                            dt2.Columns.Add("HasHDMovie", typeof(string));
                            dt2.Columns.Add("IsPromotion", typeof(string));
                            dt2.Columns.Add("CrownInclusion", typeof(string));
                            dt2.Columns.Add("ShapeColorPurity", typeof(string));
                            dt2.Columns.Add("ReviseStockFlag", typeof(string));
                            dt2.Columns.Add("CrownNatts", typeof(string));
                            dt2.Columns.Add("Luster", typeof(string));
                            dt2.Columns.Add("Location", typeof(string));
                            dt2.Columns.Add("PageNo", typeof(string));
                            dt2.Columns.Add("StoneStatus", typeof(string));
                            dt2.Columns.Add("FromCrownAngle", typeof(string));
                            dt2.Columns.Add("ToCrownAngle", typeof(string));
                            dt2.Columns.Add("FromCrownHeight", typeof(string));
                            dt2.Columns.Add("ToCrownHeight", typeof(string));
                            dt2.Columns.Add("FromPavAngle", typeof(string));
                            dt2.Columns.Add("ToPavAngle", typeof(string));
                            dt2.Columns.Add("FromPavHeight", typeof(string));
                            dt2.Columns.Add("ToPavHeight", typeof(string));
                            dt2.Columns.Add("BGM", typeof(string));
                            dt2.Columns.Add("Black", typeof(string));
                            dt2.Columns.Add("UserID", typeof(Int32));
                            dt2.Columns.Add("iId", typeof(Int32));
                            dt2.Columns.Add("SmartSearch", typeof(string));
                            dt2.Columns.Add("KeyToSymbol", typeof(string));
                            dt2.Columns.Add("ColorType", typeof(string));
                            dt2.Columns.Add("Intensity", typeof(string));
                            dt2.Columns.Add("Overtone", typeof(string));
                            dt2.Columns.Add("Fancy_Color", typeof(string));
                            dt2.Columns.Add("Table_Open", typeof(string));
                            dt2.Columns.Add("Crown_Open", typeof(string));
                            dt2.Columns.Add("Pav_Open", typeof(string));
                            dt2.Columns.Add("Girdle_Open", typeof(string));

                            DataRow[] dra = dt.Select("iId = " + searchdiamondsrequest.iId);
                            if (dra.Length > 0)
                            {
                                DataRow dr1 = dt2.NewRow();

                                dr1["Shape"] = dra[0]["Shape"];
                                dr1["Pointer"] = dra[0]["Pointer"];
                                dr1["Color"] = dra[0]["Color"];
                                dr1["Clarity"] = dra[0]["Clarity"];
                                dr1["Cut"] = dra[0]["Cut"];
                                dr1["Polish"] = dra[0]["Polish"];
                                dr1["Symm"] = dra[0]["Symm"];
                                dr1["Fls"] = dra[0]["Fls"];
                                dr1["Lab"] = dra[0]["Lab"];
                                dr1["Inclusion"] = dra[0]["Inclusion"];
                                dr1["Natts"] = dra[0]["Natts"];
                                dr1["Shade"] = dra[0]["Shade"];
                                dr1["FromCts"] = dra[0]["FromCts"];
                                dr1["ToCts"] = dra[0]["ToCts"];
                                dr1["FormDisc"] = dra[0]["FormDisc"];
                                dr1["ToDisc"] = dra[0]["ToDisc"];
                                dr1["FormPricePerCts"] = dra[0]["FormPricePerCts"];
                                dr1["ToPricePerCts"] = dra[0]["ToPricePerCts"];
                                dr1["FormNetAmt"] = dra[0]["FormNetAmt"];
                                dr1["ToNetAmt"] = dra[0]["ToNetAmt"];
                                dr1["FormDepth"] = dra[0]["FormDepth"];
                                dr1["ToDepth"] = dra[0]["ToDepth"];
                                dr1["FormLength"] = dra[0]["FormLength"];
                                dr1["ToLength"] = dra[0]["ToLength"];
                                dr1["FormWidth"] = dra[0]["FormWidth"];
                                dr1["ToWidth"] = dra[0]["ToWidth"];
                                dr1["FormDepthPer"] = dra[0]["FormDepthPer"];
                                dr1["ToDepthPer"] = dra[0]["ToDepthPer"];
                                dr1["FormTablePer"] = dra[0]["FormTablePer"];
                                dr1["ToTablePer"] = dra[0]["ToTablePer"];
                                dr1["HasImage"] = dra[0]["HasImage"];
                                dr1["HasHDMovie"] = dra[0]["HasHDMovie"];
                                dr1["IsPromotion"] = dra[0]["IsPromotion"];
                                dr1["CrownInclusion"] = dra[0]["CrownInclusion"];
                                dr1["ShapeColorPurity"] = dra[0]["ShapeColorPurity"];
                                dr1["ReviseStockFlag"] = dra[0]["ReviseStockFlag"];
                                dr1["CrownNatts"] = dra[0]["CrownNatts"];
                                dr1["Luster"] = dra[0]["Luster"];
                                dr1["Location"] = dra[0]["Location"];
                                dr1["StoneStatus"] = dra[0]["StoneStatus"];
                                dr1["FromCrownAngle"] = dra[0]["FromCrownAngle"];
                                dr1["ToCrownAngle"] = dra[0]["ToCrownAngle"];
                                dr1["FromCrownHeight"] = dra[0]["FromCrownHeight"];
                                dr1["ToCrownHeight"] = dra[0]["ToCrownHeight"];
                                dr1["FromPavAngle"] = dra[0]["FromPavAngle"];
                                dr1["ToPavAngle"] = dra[0]["ToPavAngle"];
                                dr1["FromPavHeight"] = dra[0]["FromPavHeight"];
                                dr1["ToPavHeight"] = dra[0]["ToPavHeight"];
                                dr1["BGM"] = dra[0]["BGM"];
                                dr1["Black"] = dra[0]["Black"];
                                dr1["UserID"] = dra[0]["UserID"];
                                dr1["iId"] = dra[0]["iId"];
                                dr1["KeyToSymbol"] = dra[0]["KeyToSymbol"];
                                dr1["ColorType"] = dra[0]["ColorType"];
                                dr1["Intensity"] = dra[0]["Intensity"];
                                dr1["Overtone"] = dra[0]["Overtone"];
                                dr1["Fancy_Color"] = dra[0]["Fancy_Color"];
                                dr1["Table_Open"] = dra[0]["Table_Open"];
                                dr1["Crown_Open"] = dra[0]["Crown_Open"];
                                dr1["Pav_Open"] = dra[0]["Pav_Open"];
                                dr1["Girdle_Open"] = dra[0]["Girdle_Open"];

                                dt2.Rows.Add(dr1);
                            }

                            List<SearchDiamondsRequest> listsearchdiamondsrequest = new List<SearchDiamondsRequest>();
                            listsearchdiamondsrequest = DataTableExtension.ToList<SearchDiamondsRequest>(dt2);

                            if (listsearchdiamondsrequest.Count > 0)
                            {
                                return Ok(new ServiceResponse<SearchDiamondsRequest>
                                {
                                    Data = listsearchdiamondsrequest,
                                    Message = "SUCCESS",
                                    Status = "1"
                                });
                            }
                            else
                            {
                                return Ok();
                            }
                        }
                    }
                }

                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse
                {
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Currency_Country_List([FromBody] JObject data)
        {
            CurrencyCountryListDetail CurrencyCountryListRequest = new CurrencyCountryListDetail();
            try
            {
                CurrencyCountryListRequest = JsonConvert.DeserializeObject<CurrencyCountryListDetail>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                DataTable dt = new DataTable();

                if (!string.IsNullOrEmpty(CurrencyCountryListRequest.iCountryId))
                    para.Add(db.CreateParam("p_for_iCountryId", DbType.String, ParameterDirection.Input, CurrencyCountryListRequest.iCountryId));
                else
                    para.Add(db.CreateParam("p_for_iCountryId", DbType.String, ParameterDirection.Input, DBNull.Value));

                dt = db.ExecuteSP("Currency_Country_List", para.ToArray(), false);

                List<CurrencyCountryListDetail> currencycountrylistdetail = new List<CurrencyCountryListDetail>();
                currencycountrylistdetail = DataTableExtension.ToList<CurrencyCountryListDetail>(dt);

                if (currencycountrylistdetail.Count > 0)
                {
                    return Ok(new ServiceResponse<CurrencyCountryListDetail>
                    {
                        Data = currencycountrylistdetail,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
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
                return Ok(new ServiceResponse<UserListResponse>
                {
                    Data = new List<UserListResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult GET_Scheme_Disc()
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = new DataTable();
                dt = db.ExecuteSP("GET_Scheme_Disc", para.ToArray(), false);

                List<Scheme_Disc> Scheme_Disc = new List<Scheme_Disc>();
                Scheme_Disc = DataTableExtension.ToList<Scheme_Disc>(dt);

                if (Scheme_Disc.Count > 0)
                {
                    return Ok(new ServiceResponse<Scheme_Disc>
                    {
                        Data = Scheme_Disc,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
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
                return Ok(new ServiceResponse<Scheme_Disc>
                {
                    Data = new List<Scheme_Disc>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult FancyColor_Image_Status_Get()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = new DataTable();

                para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, userID));
                dt = db.ExecuteSP("FancyColor_UserDetail_Select", para.ToArray(), false);

                List<FancyColor_Image> FancyColor_Image = new List<FancyColor_Image>();
                FancyColor_Image = DataTableExtension.ToList<FancyColor_Image>(dt);

                if (FancyColor_Image.Count > 0)
                {
                    return Ok(new ServiceResponse<FancyColor_Image>
                    {
                        Data = FancyColor_Image,
                        Message = "Found",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<FancyColor_Image>
                    {
                        Data = FancyColor_Image,
                        Message = "No Found",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<FancyColor_Image>
                {
                    Data = new List<FancyColor_Image>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult FancyColor_Image_Status_Set()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = new DataTable();

                para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, userID));
                dt = db.ExecuteSP("FancyColor_UserDetail_Insert", para.ToArray(), false);

                List<FancyColor_Image> FancyColor_Image = new List<FancyColor_Image>();
                FancyColor_Image = DataTableExtension.ToList<FancyColor_Image>(dt);

                if (FancyColor_Image.Count > 0)
                {
                    return Ok(new ServiceResponse<FancyColor_Image>
                    {
                        Data = FancyColor_Image,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<FancyColor_Image>
                    {
                        Data = FancyColor_Image,
                        Message = "ERROR",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<FancyColor_Image>
                {
                    Data = new List<FancyColor_Image>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult OrderHistory_Video_Status_Get()
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = new DataTable();

                para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, userID));
                dt = db.ExecuteSP("OrderHistory_UserDetail_Select", para.ToArray(), false);

                List<OrderHistory_Video> OrderHistory_Video = new List<OrderHistory_Video>();
                OrderHistory_Video = DataTableExtension.ToList<OrderHistory_Video>(dt);

                if (OrderHistory_Video.Count > 0)
                {
                    return Ok(new ServiceResponse<OrderHistory_Video>
                    {
                        Data = OrderHistory_Video,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<OrderHistory_Video>
                    {
                        Data = OrderHistory_Video,
                        Message = "ERROR",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<FancyColor_Image>
                {
                    Data = new List<FancyColor_Image>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        #region Task Scheduler : WS_GetStockAvail

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult WS_GetStockAvail()
        {
            string path = HttpContext.Current.Server.MapPath("~/Stock_Available_Email_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();
            try
            {
                string _EmailList = string.Empty;
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                para.Add(db.CreateParam("p_for_Action", DbType.String, ParameterDirection.Input, "Email Me"));
                para.Add(db.CreateParam("p_for_iUserId", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("NoFoundSearchStock_Get", para.ToArray(), false);

                foreach (DataRow dr in dt.Rows)
                {
                    db = new Database();
                    para = new List<IDbDataParameter>();
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(dt.Rows[0]["UserID"])));
                    DataTable _dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);
                    string iUserType = "";
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        iUserType = _dt.Rows[0]["iUserType"].ToString();
                    }

                    SearchDiamondsRequest searchdiamondsrequest = new SearchDiamondsRequest();
                    searchdiamondsrequest = DataTableExtension.ToObject<SearchDiamondsRequest>(dr);

                    StockDownloadRequest stockDownloadRequest = new StockDownloadRequest();
                    stockDownloadRequest = DataTableExtension.ToObject<StockDownloadRequest>(dr);

                    DataTable dtData1 = SearchDiamondsFullStock("Y", stockDownloadRequest.StoneID, stockDownloadRequest.CertiNo, 
                        stockDownloadRequest.Shape, stockDownloadRequest.Pointer, stockDownloadRequest.Color, stockDownloadRequest.Clarity, 
                        stockDownloadRequest.Cut, stockDownloadRequest.Polish, stockDownloadRequest.Symm, stockDownloadRequest.Fls, 
                        stockDownloadRequest.Lab, stockDownloadRequest.Luster, stockDownloadRequest.Location, stockDownloadRequest.Inclusion, 
                        stockDownloadRequest.Natts, stockDownloadRequest.Shade, stockDownloadRequest.FromCts, stockDownloadRequest.ToCts, 
                        stockDownloadRequest.FormDisc, stockDownloadRequest.ToDisc, stockDownloadRequest.FormPricePerCts, 
                        stockDownloadRequest.ToPricePerCts, stockDownloadRequest.FormNetAmt, stockDownloadRequest.ToNetAmt, 
                        stockDownloadRequest.FormDepth, stockDownloadRequest.ToDepth, stockDownloadRequest.FormLength, 
                        stockDownloadRequest.ToLength, stockDownloadRequest.FormWidth, stockDownloadRequest.ToWidth, 
                        stockDownloadRequest.FormDepthPer, stockDownloadRequest.ToDepthPer, stockDownloadRequest.FormTablePer, 
                        stockDownloadRequest.ToTablePer, stockDownloadRequest.HasImage, stockDownloadRequest.HasHDMovie, 
                        stockDownloadRequest.IsPromotion, "", stockDownloadRequest.Reviseflg, stockDownloadRequest.CrownInclusion, 
                        stockDownloadRequest.CrownNatts, stockDownloadRequest.PageNo, "", stockDownloadRequest.TokenNo, 
                        stockDownloadRequest.StoneStatus, stockDownloadRequest.FromCrownAngle, stockDownloadRequest.ToCrownAngle, 
                        stockDownloadRequest.FromCrownHeight, stockDownloadRequest.ToCrownHeight, stockDownloadRequest.FromPavAngle, 
                        stockDownloadRequest.ToPavAngle, stockDownloadRequest.FromPavHeight, stockDownloadRequest.ToPavHeight, 
                        stockDownloadRequest.BGM, stockDownloadRequest.Black, stockDownloadRequest.SmartSearch, stockDownloadRequest.keytosymbol, 
                        "", "", stockDownloadRequest.FormName, stockDownloadRequest.ActivityType, stockDownloadRequest.ColorType, 
                        stockDownloadRequest.Intensity, stockDownloadRequest.Overtone, stockDownloadRequest.Fancy_Color, 
                        stockDownloadRequest.Table_Open, stockDownloadRequest.Crown_Open, stockDownloadRequest.Pav_Open, 
                        stockDownloadRequest.Girdle_Open, "View", stockDownloadRequest.NewArrival);

                    if (dtData1 != null && dtData1.Rows.Count > 0)
                    {
                        int Total_Stones = dtData1.Rows[0]["stone_ref_no"].ToString() != "" ? Int32.Parse(dtData1.Rows[0]["stone_ref_no"].ToString()) : 0;
                        if (Total_Stones > 0)
                        {
                            dtData1.DefaultView.RowFilter = "Sr IS NOT NULL";
                            dtData1 = dtData1.DefaultView.ToTable();

                            MailMessage xloMail = new MailMessage();
                            SmtpClient xloSmtp = new SmtpClient();
                            try
                            {
                                string Emaillist = string.Empty, fileName = string.Empty, _path = string.Empty, realPath = string.Empty, _livepath = string.Empty;

                                xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Bhavyam Diam");

                                Database db2 = new Database();
                                Database db3 = new Database();
                                Database db4 = new Database();
                                Database db5 = new Database();

                                List<IDbDataParameter> para2 = new List<IDbDataParameter>();
                                para2.Add(db2.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(searchdiamondsrequest.UserID)));
                                DataTable dt3 = db2.ExecuteSP("UserMas_SelectOne", para2.ToArray(), false);
                                if (dt3 != null && dt3.Rows.Count > 0)
                                {
                                    _EmailList += dt3.Rows[0]["sCompEmail"].ToString() + ", ";

                                    xloMail.To.Add(dt3.Rows[0]["sCompEmail"].ToString());
                                    xloMail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["BCCEmail"]));

                                    string _Body = string.Empty;
                                    _Body = "Dear " + dt3.Rows[0]["sFirstName"].ToString() + " " + dt3.Rows[0]["sLastName"].ToString() + (dt3.Rows[0]["sCompName"] != null && dt3.Rows[0]["sCompName"].ToString() != "" ? " (" + dt3.Rows[0]["sCompName"].ToString() + ")" : "");
                                    _Body += "<br /><br />We have stones in our inventory as per your search in out inventory.<br /> Please find attached sheet for available stones.";

                                    xloMail.Subject = "Stones Available as per your search";
                                    xloMail.IsBodyHtml = true;
                                    xloMail.Body = _Body;
                                }

                                fileName = "SearchStock " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                                _path = ConfigurationManager.AppSettings["data"];
                                realPath = System.Web.Hosting.HostingEnvironment.MapPath("~/ExcelFile/");
                                _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                                EpExcelExport.CreateExcel(dtData1.DefaultView.ToTable(), realPath, realPath + fileName + ".xlsx", _livepath, "", iUserType);

                                ContentType contentType = new System.Net.Mime.ContentType();
                                contentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Octet;
                                contentType.Name = fileName + ".xlsx";
                                Attachment attachFile = new Attachment(realPath + fileName + ".xlsx", contentType);
                                xloMail.Attachments.Add(attachFile);

                                xloSmtp.Timeout = 500000;
                                xloSmtp.Send(xloMail);

                                xloMail.Attachments.Dispose();
                                xloMail.AlternateViews.Dispose();
                                xloMail.Dispose();

                                if (fileName.Length > 0)
                                    if (System.IO.File.Exists(fileName))
                                        System.IO.File.Delete(fileName);


                                List<IDbDataParameter> para3 = new List<IDbDataParameter>();
                                para3.Add(db5.CreateParam("p_for_iId", DbType.String, ParameterDirection.Input, searchdiamondsrequest.iId));
                                DataTable dt4 = db5.ExecuteSP("SearchStock_NoFoundDet_Update", para3.ToArray(), false);
                                if (dt4 != null && dt4.Rows.Count > 0)
                                {
                                    //return Ok(dt4.Rows[0]["Message"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                return Ok(ex.Message);
                            }
                        }
                    }
                }
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append((_EmailList != "" && _EmailList != null ? "Email Send Successfully to " + _EmailList.Remove(_EmailList.Length - 2) : "SUCCESS") + " " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();

                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = (_EmailList != "" && _EmailList != null ? "Email Send Successfully to " + _EmailList.Remove(_EmailList.Length - 2) : "SUCCESS"),
                    Status = "1"
                }); ;
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

        #endregion

        #region Task Scheduler : WS_GetGraphDetail_Ora

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult WS_GetGraphDetail_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/GraphDetail_Get_Ora.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();
            try
            {
                DateTime date = DateTime.Now;
                string FromDate = "01-" + string.Format("{0:MMM-yyyy}", date);
                string ToDate = string.Format("{0:dd-MMM-yyyy}", date);

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("p_for_comp", OracleDbType.Int32);
                param1.Value = 1;
                paramList.Add(param1);

                OracleParameter param2 = new OracleParameter("p_from_date", OracleDbType.Date);
                param2.Value = FromDate;
                paramList.Add(param2);

                OracleParameter param3 = new OracleParameter("p_to_date", OracleDbType.Date);
                param3.Value = ToDate;
                paramList.Add(param3);

                OracleParameter param4 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param4.Direction = ParameterDirection.Output;
                paramList.Add(param4);

                System.Data.DataTable dt = oracleDbAccess.CallSP("get_trans_det_graph_new", paramList);
                string xml = string.Empty;
                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;
                    xml += "<ARRAY_OF_GRAPH_DETAIL>";
                    foreach (DataRow drItem in dt.Rows)
                    {
                        xml += "<GRAPH>";
                        xml += "<VZONE>" + drItem["VZONE"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</VZONE>";
                        xml += "<ZONE_NUMBER>" + drItem["ZONE_NUMBER"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</ZONE_NUMBER>";
                        xml += "<SALE_SOURCE_PARTY_NAME>" + drItem["SALE_SOURCE_PARTY_NAME"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</SALE_SOURCE_PARTY_NAME>";
                        xml += "<PCS>" + drItem["PCS"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</PCS>";
                        xml += "<CUR_CTS>" + drItem["CUR_CTS"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</CUR_CTS>";
                        xml += "<SALE_VALUE>" + drItem["SALE_VALUE"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</SALE_VALUE>";
                        xml += "<PUR_VALUE>" + drItem["PUR_VALUE"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</PUR_VALUE>";
                        xml += "<PROFIT>" + drItem["PROFIT"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</PROFIT>";
                        xml += "<BUYER>" + drItem["BUYER"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</BUYER>";
                        xml += "<PROCESS>" + drItem["PROCESS"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</PROCESS>";
                        xml += "<SUPPLIER>" + drItem["SUPPLIER"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</SUPPLIER>";
                        xml += "<ALIAS_PARTY_NAME>" + drItem["ALIAS_PARTY_NAME"].ToString().Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;") + "</ALIAS_PARTY_NAME>";
                        xml += "<PRE_SOLD>" + (drItem["PRE_SOLD"].ToString() != "" && drItem["PRE_SOLD"] != null && drItem["PRE_SOLD"].ToString() == "Y" ? true : false) + "</PRE_SOLD>";
                        xml += "</GRAPH>";
                    }
                    xml += "</ARRAY_OF_GRAPH_DETAIL>";

                    Database db = new Database(Request);
                    List<IDbDataParameter> para = new List<IDbDataParameter>();
                    dt = new DataTable();

                    para.Add(db.CreateParam("Month", DbType.String, ParameterDirection.Input, string.Format("{0:MMM-yyyy}", date).ToUpper()));
                    para.Add(db.CreateParam("columnsDefs", DbType.String, ParameterDirection.Input, xml));

                    dt = db.ExecuteSP("GraphDetail_Ora_Insert", para.ToArray(), false);

                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append(("Success " + Count + " Data Found from " + FromDate + " to " + ToDate) + ", Log Time " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();

                    return Ok(new CommonResponse
                    {
                        Message = "Success " + Count + " Data Found from " + FromDate + " to " + ToDate + ", Log Time " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"),
                        Status = "1",
                        Error = ""
                    });
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append(("No Data Found from " + FromDate + " to " + ToDate) + ", Log Time " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();

                    return Ok(new CommonResponse
                    {
                        Message = ("No Data Found from " + FromDate + " to " + ToDate) + ", Log Time " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"),
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }

        #endregion

        #region Task Scheduler : WS_StockUpload_Ora

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult WS_StockUpload_Ora()
        {
            Database db3 = new Database(Request);
            System.Collections.Generic.List<System.Data.IDbDataParameter> para3;
            para3 = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            DataTable dt3 = new DataTable();
            dt3 = db3.ExecuteSP("StockUploadTiming", para3.ToArray(), false);

            string path = HttpContext.Current.Server.MapPath("~/Stock_Upload_From_API_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            if (dt3.Rows[0]["STATUS"].ToString() == "1")
            {
                try
                {
                    Ora_Stock_Get_Log("Insert");

                    DateTime date = DateTime.Now;
                    DataTable dt = new DataTable();

                    string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                    //string _API = "http://203.187.204.211:801/?APIKEY=tejas";
                    //string _API = "http://183.182.89.105:801/?APIKEY=tejas";
                    //string _API = "http://103.246.86.23:801/?APIKEY=tejas";
                    string _API = "http://115.124.106.150:801/?apikey=NATURAL";
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
                    JArray jsonArray = JArray.Parse(json);
                    var r = jsonArray.ToString();

                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(r);
                    json = JsonConvert.SerializeObject(json_1);

                    ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                    dt = jDt.JsonStringToDataTable(json);

                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            if (row[i].ToString() == "" || row[i].ToString().ToUpper() == "NULL")
                            {
                                row[i] = DBNull.Value;
                            }
                        }
                    }

                    int Count = 0;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Count = dt.Rows.Count;

                        Database db = new Database(Request);
                        DataTable dt1 = new DataTable();
                        List<SqlParameter> para = new List<SqlParameter>();

                        SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                        param.Value = dt;
                        para.Add(param);

                        dt1 = db.ExecuteSP("StockDetail_Ora_Insert", para.ToArray(), false);
                        Ora_Stock_Get_Log("Delete");

                        string Message = string.Empty;
                        if (dt1 != null)
                        {
                            Message = dt1.Rows[0]["Message"].ToString();
                        }
                        string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                        if (Message == "SUCCESS")
                        {
                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                            sb.Append(Message + " " + Count + " Stock Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                            sb.AppendLine("");
                            File.AppendAllText(path, sb.ToString());
                            sb.Clear();

                            //WS_LiveDiscUpload_Ora();

                            return Ok(new CommonResponse
                            {
                                Message = Message + " " + Count + " Stock Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                                Status = "1",
                                Error = ""
                            });
                        }
                        else
                        {

                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                            sb.Append("Stock upload in issue" + (Message != "" && Message != null ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                            sb.AppendLine("");
                            File.AppendAllText(path, sb.ToString());
                            sb.Clear();
                            return Ok(new CommonResponse
                            {
                                Message = "Stock upload in issue" + (Message != "" && Message != null ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                                Status = "1",
                                Error = ""
                            });
                        }
                    }
                    else
                    {
                        Ora_Stock_Get_Log("Delete");
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("No Stock Found, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = "No Stock Found",
                            Status = "1",
                            Error = ""
                        });
                    }

                }
                catch (Exception ex)
                {
                    Ora_Stock_Get_Log("Delete");
                    DAL.Common.InsertErrorLog(ex, null, Request);
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append(ex.Message + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = ex.Message,
                        Status = "0",
                        Error = ex.StackTrace
                    });
                }
            }
            else
            {
                if (dt3.Rows[0]["MESSAGE"].ToString() != "")
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append(dt3.Rows[0]["MESSAGE"].ToString() + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                }

                return Ok(new CommonResponse
                {
                    Message = dt3.Rows[0]["MESSAGE"].ToString(),
                    Status = "1",
                    Error = ""
                });
            }
        }
        public void Ora_Stock_Get_Log(string Types)
        {
            Database db3 = new Database(Request);
            System.Collections.Generic.List<System.Data.IDbDataParameter> para3;
            para3 = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            DataTable dt3 = new DataTable();
            para3.Add(db3.CreateParam("Type", DbType.String, ParameterDirection.Input, Types));
            dt3 = db3.ExecuteSP("Ora_Stock_Get_Start_CRUD", para3.ToArray(), false);
        }
        public void WS_LiveDiscUpload_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/Live_Disc_Upload_From_Oracle_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                DateTime date = DateTime.Now;
                string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                Database db = new Database(Request);
                System.Collections.Generic.List<System.Data.IDbDataParameter> para1;
                para1 = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("p_for_comp", OracleDbType.Int32);
                param1.Value = 1;
                paramList.Add(param1);

                OracleParameter param2 = new OracleParameter("P_for_type", OracleDbType.NVarchar2);
                param2.Value = "S";
                paramList.Add(param2);

                OracleParameter param3 = new OracleParameter("p_for_Date", OracleDbType.Date);
                param3.Value = string.Format("{0:dd-MMM-yyyy}", date);
                paramList.Add(param3);

                OracleParameter param4 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param4.Direction = ParameterDirection.Output;
                paramList.Add(param4);

                string get_live_disc_data = ConfigurationManager.AppSettings["get_live_disc_data"];
                System.Data.DataTable dt = oracleDbAccess.CallSP(get_live_disc_data, paramList);

                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;

                    DataTable dt1 = new DataTable();
                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dt1 = db.ExecuteSP("LiveDiscDetail_Ora_Insert", para.ToArray(), false);

                    string Message = string.Empty;
                    if (dt1 != null)
                    {
                        Message = dt1.Rows[0]["Message"].ToString();
                    }
                    string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                    if (Message == "SUCCESS")
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(Message + " " + Count + " Live Disc Detail Found, process time " + fromtime + " to " + totime + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                    }
                    else
                    {

                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("Live Disc upload in issue" + (Message != "" && Message != null ? " " + Message : "") + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                    }
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("No Live Disc Found, Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
            }
        }

        #endregion

        #region Task Scheduler : WS_SalesDataUpdate_Ora

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult WS_SalesDataUpdate_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/SalesDataUpdate_Ora.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();
            try
            {
                DateTime date = DateTime.Now;
                string FromDate = string.Format("{0:dd-MMM-yyyy}", date.AddDays(-30));
                string ToDate = string.Format("{0:dd-MMM-yyyy}", date);

                Database db = new Database(Request);
                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param2 = new OracleParameter("p_from_date", OracleDbType.Date);
                param2.Value = FromDate;
                paramList.Add(param2);

                OracleParameter param3 = new OracleParameter("p_to_date", OracleDbType.Date);
                param3.Value = ToDate;
                paramList.Add(param3);

                OracleParameter param4 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param4.Direction = ParameterDirection.Output;
                paramList.Add(param4);

                System.Data.DataTable dt = oracleDbAccess.CallSP("get_live_data_sales", paramList);

                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;

                    DataTable dt1 = new DataTable();
                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dt1 = db.ExecuteSP("OrderHistory_SalesData_Update", para.ToArray(), false);

                    if (dt1.Rows[0]["Status"].ToString() == "1")
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(dt1.Rows[0]["Message"].ToString() + " from " + FromDate + " to " + ToDate + " date, Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();

                        return Ok(new CommonResponse
                        {
                            Message = dt1.Rows[0]["Message"].ToString() + " from " + FromDate + " to " + ToDate + " date, Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                    else
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(dt1.Rows[0]["Message"].ToString() + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();

                        return Ok(new CommonResponse
                        {
                            Message = dt1.Rows[0]["Message"].ToString() + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("No Data Found from " + FromDate + " to " + ToDate + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();

                    return Ok(new CommonResponse
                    {
                        Message = "No Data Found from " + FromDate + " to " + ToDate + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"),
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();

                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }

        #endregion

        #region Task Scheduler : WS_PacketTrace_Upload

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult WS_PacketTrace_Upload()
        {
            string path = HttpContext.Current.Server.MapPath("~/PacketTrace_Upload.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = new DataTable();
                dt = db.ExecuteSP("Stock_PacketTrace_Upload", para.ToArray(), false);
                if (dt.Rows[0]["Status"].ToString() == "1")
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append(dt.Rows[0]["Message"].ToString() + ", Log Time " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = dt.Rows[0]["Message"].ToString() + ", Log Time " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"),
                        Status = "1",
                        Error = ""
                    });
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append(dt.Rows[0]["Message"].ToString() + ", Log Time " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = dt.Rows[0]["Message"].ToString() + ", Log Time " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"),
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }

        #endregion

        #region Task Scheduler : WS_PairStockUpload_Ora

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult WS_PairStockUpload_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/Pair_Stock_Upload_From_Oracle_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                DateTime date = DateTime.Now;
                string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                Database db = new Database(Request);
                System.Collections.Generic.List<System.Data.IDbDataParameter> para1;
                para1 = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("p_for_comp", OracleDbType.Int32);
                param1.Value = 1;
                paramList.Add(param1);

                OracleParameter param2 = new OracleParameter("p_for_exclude", OracleDbType.NVarchar2);
                param2.Value = "N";
                paramList.Add(param2);

                OracleParameter param4 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param4.Direction = ParameterDirection.Output;
                paramList.Add(param4);

                string get_live_Pairdata = ConfigurationManager.AppSettings["get_live_Pairdata"];
                System.Data.DataTable dt = oracleDbAccess.CallSP(get_live_Pairdata, paramList);

                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;

                    DataTable dt1 = new DataTable();
                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dt1 = db.ExecuteSP("PairStockDetail_Ora_Insert", para.ToArray(), false);

                    string Message = string.Empty;
                    if (dt1 != null)
                    {
                        Message = dt1.Rows[0]["Message"].ToString();
                    }
                    string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                    if (Message == "SUCCESS")
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(Message + " " + Count + " Pair Stock Found, process time " + fromtime + " to " + totime + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = Message + " " + Count + " Pair Stock Found, process time " + fromtime + " to " + totime + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                    else
                    {

                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("Pair Stock upload in issue" + (Message != "" && Message != null ? " " + Message : "") + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = "Pair Stock upload in issue" + (Message != "" && Message != null ? " " + Message : "") + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("No Pair Stock Found, Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = "No Pair Stock Found",
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DAL.Common.GetHKTime().ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }

        }

        #endregion

        #region Task Scheduler : Auto_Suspend_User_Make

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Auto_Suspend_User_Make()
        {
            string path = HttpContext.Current.Server.MapPath("~/Auto_Suspend_User_Make.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();
            try
            {
                DateTime date = DateTime.Now;

                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);
                DataTable dt = db.ExecuteSP("Auto_Suspend_User_Make", para.ToArray(), false);
                string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                string subject = "Bhavyam Diam – Account Suspend – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");

                if (dt != null)
                {
                    string TotalUserFound = dt.Rows.Count.ToString();
                    string BccEmailAdd = "";

                    StringBuilder Sb1 = new StringBuilder();
                    StringBuilder Sb2 = new StringBuilder();

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string ToEmailAdd = "", CompName = "", Username = "";
                        int iUserid;

                        Username = dt.Rows[j]["sUsername"].ToString();
                        CompName = dt.Rows[j]["sCompName"].ToString();
                        ToEmailAdd = dt.Rows[j]["sCompEmail"].ToString();
                        iUserid = Convert.ToInt32(dt.Rows[j]["iUserid"].ToString());

                        if (string.IsNullOrEmpty(ToEmailAdd))
                        {
                            ToEmailAdd = ConfigurationManager.AppSettings["BCCEmail"].ToString();
                        }

                        Lib.Models.Common.EmailOfSuspendedUser(subject, ToEmailAdd, "", Username, CompName);

                        Sb1.Append(@"<tr>");
                        Sb1.Append(@"<td style='font-size:11px;font-family:Tahoma;text-align:center;white-space:nowrap'>" + Username + "</td>");
                        Sb1.Append(@"<td style='font-size:11px;font-family:Tahoma;text-align:center;white-space:nowrap'>" + CompName + "</td>");
                        Sb1.Append(@"</tr>");
                    }

                    if (Sb1.ToString() != "")
                    {
                        Sb2.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear Admin,</p>");
                        Sb2.Append(@"<p><b>As per our company policy his/her account is suspended.</b></p>");
                        Sb2.Append("<br/>");
                        Sb2.Append(@"<table border='1' style='overflow-x:scroll!important;width: 350px!important;'>");
                        Sb2.Append(@"<tbody><tr>");
                        Sb2.Append(@"<th style='font-size:12px;font-family:Tahoma;text-align:center;background-color:#83caff'>User Name</th>");
                        Sb2.Append(@"<th style='font-size:12px;font-family:Tahoma;text-align:center;background-color:#83caff'>Company Name</th>");
                        Sb2.Append(@"</tr>");
                        
                        Sb2.Append(Sb1);
                    
                        Sb2.Append("</tbody></table>");
                        Sb2.Append("<br/>");

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
                                    BccEmailAdd += DT_AdminLst.Rows[I]["sCompEmail"].ToString() + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(BccEmailAdd))
                            {
                                BccEmailAdd = BccEmailAdd.TrimEnd(',');
                            }
                        }
                        else
                        {
                            BccEmailAdd = ConfigurationManager.AppSettings["BCCEmail"].ToString();
                        }

                        Lib.Models.Common.EmailOfAdminSuspendedUser(subject, BccEmailAdd, Sb2.ToString());
                    }

                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("SUCCESS Total " + TotalUserFound + " User(s) Suspend, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();

                    return Ok(new CommonResponse
                    {
                        Message = "SUCCESS Total " + TotalUserFound + " User(s) Suspend, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                        Status = "1",
                        Error = ""
                    });
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("Error, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = "Error, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                        Status = "0",
                        Error = ""
                    });
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
                return Ok(new CommonResponse
                {
                    Message = ex.Message + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                    Status = "0",
                    Error = ""
                });
            }
        }

        #endregion

        public static bool CheckExists(string url)
        {
            Uri uri = new Uri(url);

            if (uri.IsFile) // File is local
                return System.IO.Directory.Exists(uri.LocalPath);

            try
            {
                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
                request.Method = "HEAD"; // No need to download the whole thing
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                return (response.StatusCode == HttpStatusCode.OK); // Return true if the file exists
            }
            catch
            {
                return false; // URL does not exist
            }
        }

        [HttpPost]
        public IHttpActionResult LoginCheck()
        {
            try
            {
                return Ok(new CommonResponse
                {
                    Message = "OK",
                    Status = "1",
                    Error = ""
                });
            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse
                {
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Ora_Stock_History([FromBody] JObject data)
        {
            NotificationGetRequest NotificationGetRequest = new NotificationGetRequest();
            try
            {
                NotificationGetRequest = JsonConvert.DeserializeObject<NotificationGetRequest>(data.ToString());
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
                DataTable dt = new DataTable();

                if (!string.IsNullOrEmpty(NotificationGetRequest.FromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, NotificationGetRequest.FromDate));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(NotificationGetRequest.ToDate))
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, NotificationGetRequest.ToDate));
                else
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(NotificationGetRequest.PageNo))
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(NotificationGetRequest.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(NotificationGetRequest.PageSize))
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(NotificationGetRequest.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                dt = db.ExecuteSP("Ora_Stock_History_GET", para.ToArray(), false);

                List<Ora_Stock_History> Ora_Stock_History = new List<Ora_Stock_History>();
                Ora_Stock_History = DataTableExtension.ToList<Ora_Stock_History>(dt);

                if (Ora_Stock_History.Count > 0)
                {
                    return Ok(new ServiceResponse<Ora_Stock_History>
                    {
                        Data = Ora_Stock_History,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
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
                return Ok(new ServiceResponse<Ora_Stock_History>
                {
                    Data = new List<Ora_Stock_History>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetPacketDet([FromBody] JObject data)
        {
            PacketDet PacketDet = new PacketDet();
            PacketDet_Request PacketDet_Request = new PacketDet_Request();
            try
            {
                PacketDet_Request = JsonConvert.DeserializeObject<PacketDet_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                DataTable dt = PacketDet.PacketDet_SelectOne(PacketDet_Request.StockId, 0);
                List<PacketDet_Response> listPacketDet_Response = new List<PacketDet_Response>();
                listPacketDet_Response = DataTableExtension.ToList<PacketDet_Response>(dt);

                if (listPacketDet_Response.Count > 0)
                {
                    int len = listPacketDet_Response.Count() - 1;

                    string _strfilepath = ConfigurationManager.AppSettings["Img"] + listPacketDet_Response[len].sCertiNo.ToString().Replace("*", "");

                    if (CheckExists(_strfilepath + "//PR_s.jpg"))
                    {
                        listPacketDet_Response[len].imgPr = _strfilepath + "//PR_s.jpg";
                        listPacketDet_Response[len].imgPr_L = _strfilepath + "/PR.jpg";
                        if (string.IsNullOrEmpty(listPacketDet_Response[len].lsFirstImg))
                        {
                            listPacketDet_Response[len].lsFirstImg = _strfilepath + "/PR.jpg";
                            listPacketDet_Response[len].litImageName = "Photo Real";
                        }
                    }
                    if (CheckExists(_strfilepath + "//PRB_s.jpg"))
                    {
                        listPacketDet_Response[len].imgPrb = _strfilepath + "//PRB_s.jpg";
                        listPacketDet_Response[len].imgPrb_L = _strfilepath + "/PRB.jpg";
                        if (string.IsNullOrEmpty(listPacketDet_Response[len].lsFirstImg))
                        {
                            listPacketDet_Response[len].lsFirstImg = _strfilepath + "/PRB.jpg";
                            listPacketDet_Response[len].litImageName = "Photo Real Bottom";
                        }
                    }
                    if (CheckExists(_strfilepath + "//AS_s.jpg"))
                    {
                        listPacketDet_Response[len].imgAs = _strfilepath + "//AS_s.jpg";
                        listPacketDet_Response[len].imgAs_L = _strfilepath + "/AS.jpg";
                        if (string.IsNullOrEmpty(listPacketDet_Response[len].lsFirstImg))
                        {
                            listPacketDet_Response[len].lsFirstImg = _strfilepath + "/AS.jpg";
                            listPacketDet_Response[len].litImageName = "Aset Scope";
                        }
                    }
                    if (CheckExists(_strfilepath + "//HT_s.jpg"))
                    {
                        listPacketDet_Response[len].imgHt = _strfilepath + "//HT_s.jpg";
                        listPacketDet_Response[len].imgHt_L = _strfilepath + "/HT.jpg";
                        if (string.IsNullOrEmpty(listPacketDet_Response[len].lsFirstImg))
                        {
                            listPacketDet_Response[len].lsFirstImg = _strfilepath + "/HT.jpg";
                            listPacketDet_Response[len].litImageName = "H & A Top";
                        }
                    }
                    if (CheckExists(_strfilepath + "//HB_s.jpg"))
                    {
                        listPacketDet_Response[len].imgHb = _strfilepath + "//HB_s.jpg";
                        listPacketDet_Response[len].imgHb_L = _strfilepath + "/HB.jpg";
                        if (string.IsNullOrEmpty(listPacketDet_Response[len].lsFirstImg))
                        {
                            listPacketDet_Response[len].lsFirstImg = _strfilepath + "/HB.jpg";
                            listPacketDet_Response[len].litImageName = "H & A Bottom";
                        }
                    }

                    return Ok(new ServiceResponse<PacketDet_Response>
                    {
                        Data = listPacketDet_Response,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CommonResponse>
                    {
                        Data = new List<CommonResponse>(),
                        Message = "SUCCESS",
                        Status = "1"
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

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetHDVideoDetail([FromBody] JObject data)
        {
            ViewHDImageResponse VideoDetail = new ViewHDImageResponse();
            ViewHDImageRequest HDImage_Request = new ViewHDImageRequest();
            try
            {
                HDImage_Request = JsonConvert.DeserializeObject<ViewHDImageRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }
            try
            {
                string Refno = HDImage_Request.RefNo;
                List<ViewHDImageResponse> listVideoDetail_Response = new List<ViewHDImageResponse>();

                string _strMp4Vdo = ConfigurationManager.AppSettings["ImagePathMain"] + "imaged/" + Refno + "/video.mp4";
                string _strJsonVdo = ConfigurationManager.AppSettings["ImagePathMain"] + "imaged/" + Refno + "/0.json";
                bool Mp4Vdo = CheckExists(_strMp4Vdo);
                bool JsonVdo = CheckExists(_strJsonVdo);


                if (JsonVdo == true)
                {

                    VideoDetail.VideoPath = @"<iframe style=""border: none;margin-top: 17px;""   height =""485px"" width = ""446px"" scrolling=""auto"" src=""https://4e0s0i2r4n0u1s0.com/Vision360.html?d=" +
                                           Refno + "&s=0&surl=" + ConfigurationManager.AppSettings["ImagePathMain"] + "&i=HDInfo.aspx?SeqNo=" + Refno + "\"></iframe>";

                }
                else
                {
                    VideoDetail.VideoPath = @"<iframe style=""border: none;margin-top: 17px;"" height =""485px"" width = ""446px"" scrolling=""auto"" src=""https://4e0s0i2r4n0u1s0.com/ViewVideoMp4.aspx?seqno=" + Refno + "\"></iframe>";

                }
                if (VideoDetail != null)
                {
                    listVideoDetail_Response.Add(VideoDetail);
                }

                return Ok(new ServiceResponse<ViewHDImageResponse>
                {
                    Data = listVideoDetail_Response,
                    Message = "SUCCESS",
                    Status = "1"
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
        public IHttpActionResult Hold_Stone_Avail_Customers([FromBody] JObject data)
        {
            SearchDiamondsRequest searchDiamondsRequest = new SearchDiamondsRequest();
            try
            {
                searchDiamondsRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
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
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, searchDiamondsRequest.UserID));
                para.Add(db.CreateParam("p_for_refno", DbType.String, ParameterDirection.Input, searchDiamondsRequest.StoneID));

                System.Data.DataTable dt = db.ExecuteSP("Hold_Stone_Avail_Customers", para.ToArray(), false);

                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = dt.Rows[0]["Message"].ToString(),
                    Status = dt.Rows[0]["Status"].ToString()
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

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetIVC_From_Fortune([FromBody] JObject data)
        {
            SearchByStoneIDRequest searchByStoneIDRequest = new SearchByStoneIDRequest();
            try
            {
                searchByStoneIDRequest = JsonConvert.DeserializeObject<SearchByStoneIDRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("p_for_comp", OracleDbType.Int32);
                param1.Value = 1;
                paramList.Add(param1);

                OracleParameter param2 = new OracleParameter("p_for_refno", OracleDbType.NVarchar2);
                param2.Value = searchByStoneIDRequest.StoneID;
                paramList.Add(param2);

                OracleParameter param3 = new OracleParameter("p_for_certi", OracleDbType.NVarchar2);
                param3.Value = "";
                paramList.Add(param3);

                OracleParameter param4 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param4.Direction = ParameterDirection.Output;
                paramList.Add(param4);

                System.Data.DataTable Ora_dt = oracleDbAccess.CallSP("get_pkt_det", paramList);

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("stone_ref_no", typeof(string));
                dt1.Columns.Add("certi_no", typeof(string));
                dt1.Columns.Add("color", typeof(string));
                dt1.Columns.Add("clarity", typeof(string));
                dt1.Columns.Add("cts", typeof(string));
                dt1.Columns.Add("cut", typeof(string));
                dt1.Columns.Add("polish", typeof(string));
                dt1.Columns.Add("symm", typeof(string));
                dt1.Columns.Add("fls", typeof(string));
                dt1.Columns.Add("image_url", typeof(string));
                dt1.Columns.Add("image_url1", typeof(string));
                dt1.Columns.Add("image_url2", typeof(string));
                dt1.Columns.Add("image_url3", typeof(string));
                dt1.Columns.Add("sVdoLink", typeof(string));
                dt1.Columns.Add("bMP4", typeof(string));
                dt1.Columns.Add("p_seq_no", typeof(string));
                dt1.Columns.Add("Location", typeof(string));
                dt1.Columns.Add("view_certi_url", typeof(string));
                dt1.Columns.Add("IsOverseas", typeof(string));
                dt1.Columns.Add("Supplier", typeof(string));
                dt1.Columns.Add("Supplier_Stone_No", typeof(string));

                DataRow dr = dt1.NewRow();

                dr["stone_ref_no"] = Ora_dt.Rows[0]["REFNUMBER"].ToString();
                dr["certi_no"] = Ora_dt.Rows[0]["CERTIFICATEID"].ToString();
                dr["color"] = Ora_dt.Rows[0]["COLOR"].ToString();
                dr["clarity"] = Ora_dt.Rows[0]["CLARITY"].ToString();
                dr["cts"] = Ora_dt.Rows[0]["CTS"].ToString();
                dr["cut"] = Ora_dt.Rows[0]["CUT"].ToString();
                dr["polish"] = Ora_dt.Rows[0]["POLISH"].ToString();
                dr["symm"] = Ora_dt.Rows[0]["SYMM"].ToString();
                dr["fls"] = Ora_dt.Rows[0]["FLS"].ToString();
                dr["image_url"] = Ora_dt.Rows[0]["IMG_LINK"].ToString();
                dr["image_url1"] = Ora_dt.Rows[0]["IMG_LINK1"].ToString();
                dr["image_url2"] = Ora_dt.Rows[0]["IMG_LINK2"].ToString();
                dr["image_url3"] = Ora_dt.Rows[0]["IMG_LINK3"].ToString();
                dr["sVdoLink"] = Ora_dt.Rows[0]["VIDEO_LINK"].ToString();
                dr["bMP4"] = Ora_dt.Rows[0]["MP4_STATUS"].ToString();
                dr["p_seq_no"] = Ora_dt.Rows[0]["PSEQNO"].ToString();
                dr["Location"] = Ora_dt.Rows[0]["LOCATION"].ToString();
                dr["view_certi_url"] = Ora_dt.Rows[0]["CERTI_PATH"].ToString();
                dr["IsOverseas"] = Ora_dt.Rows[0]["OVERSEAS_FLAG"].ToString();
                dr["Supplier"] = Ora_dt.Rows[0]["SUPPLIER"].ToString();
                dr["Supplier_Stone_No"] = Ora_dt.Rows[0]["SUPPLIER_STONE_NO"].ToString();

                dt1.Rows.Add(dr);

                Database db = new Database(Request);
                List<SqlParameter> para = new List<SqlParameter>();

                SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                param.Value = dt1;
                para.Add(param);

                System.Data.DataTable Sql_dt = db.ExecuteSP("Get_Stone_Det_Wise_IVC_Det", para.ToArray(), false);

                List<SearchStone> listSearchStone = new List<SearchStone>();
                listSearchStone = DataTableExtension.ToList<SearchStone>(Sql_dt);

                if (listSearchStone.Count > 0)
                {
                    return Ok(listSearchStone.FirstOrDefault());
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
    }
}
