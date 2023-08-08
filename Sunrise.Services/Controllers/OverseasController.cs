using DAL;
using EpExcelExportLib;
using ExcelExportLib;
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
using System.Web.Hosting;
using System.Web.Http;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Overseas")]
    public class OverseasController : ApiController
    {
        DataTableExcelExport ge;
        DataTableEpExcelExport ep_ge;
        UInt32 DiscNormalStyleindex;
        UInt32 CutNormalStyleindex;
        UInt32 InscStyleindex;

        [HttpPost]
        public IHttpActionResult GetOverseasColumnsConfigForSearch()
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

                DataTable dt = db.ExecuteSP("OverseasColumnConfDet_SearchResult", para.ToArray(), false);

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
        public IHttpActionResult GetSearchOverseasStock([FromBody]JObject data)
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
                DataTable dtData = SearchOverseasStockInner(searchDiamondsRequest);

                DataRow[] dra = dtData.Select("Sr IS NULL");
                SearchSummary searchSummary = new SearchSummary();
                if (dra.Length > 0)
                {
                    searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                    searchSummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["STONE_REF_NO"]);
                    searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["CTS"]);
                    searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble(dra[0]["RAP_AMOUNT"]);
                    searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["NET_AMOUNT"]);
                    searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(dra[0]["PRICE_PER_CTS"]);
                    searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble(dra[0]["SALES_DISC_PER"]);
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
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult DownloadOverseasStockExcelDNA([FromBody]JObject data)
        {
            SearchDiamondsRequest stockDownloadRequest = new SearchDiamondsRequest();

            try
            {
                stockDownloadRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                DataTable dtData = SearchOverseasStockInner(stockDownloadRequest);

                dtData.DefaultView.RowFilter = "Sr IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                string filename = "OverseasSearch " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");

                string _path = ConfigurationManager.AppSettings["data"];
                string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                EpExcelExport.CreateOverseasExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);

                string _strxml = _path + filename + ".xlsx";
                return Ok(_strxml);
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult DownloadOverseasStockExcel([FromBody]JObject data)
        {
            SearchDiamondsRequest stockDownloadRequest = new SearchDiamondsRequest();

            try
            {
                stockDownloadRequest = JsonConvert.DeserializeObject<SearchDiamondsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                stockDownloadRequest.UserID = userID;
                DataTable dtData = SearchOverseasStockInner(stockDownloadRequest);
                
                dtData.DefaultView.RowFilter = "Sr IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                string filename = "OverseasSearch " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");

                string _path = ConfigurationManager.AppSettings["data"];
                string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                EpExcelExport.CreateOverseasExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath);

                string _strxml = _path + filename + ".xlsx";
                return Ok(_strxml);
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

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult DownloadOverseasStockMedia([FromBody]JObject data)
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

                DataTable dtData = SearchOverseasStockInner(searchDiamondsRequest);
                dtData.DefaultView.RowFilter = "Sr IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

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

        [NonAction]
        public DataTable SearchOverseasStockInner(SearchDiamondsRequest searchDiamondsRequest)
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

                if (!string.IsNullOrEmpty(searchDiamondsRequest.KeyToSymbol))
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, searchDiamondsRequest.KeyToSymbol));
                else
                    para.Add(db.CreateParam("p_for_symbol", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (searchDiamondsRequest.PgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int16, ParameterDirection.Input, searchDiamondsRequest.PgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int16, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(searchDiamondsRequest.OrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, searchDiamondsRequest.OrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, searchDiamondsRequest.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, searchDiamondsRequest.ActivityType));

                DataTable dt = new DataTable();
                
                dt = db.ExecuteSP("IPD_Search_OverseasStock_Sunrise", para.ToArray(), false);
                
                return dt;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult SaveOverseasColumnsSettings([FromBody]JObject data)
        {
            ColumnsSettingsRequest columnsSettingsRequest = new ColumnsSettingsRequest();

            try
            {
                columnsSettingsRequest = JsonConvert.DeserializeObject<ColumnsSettingsRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                string columnsSettingsValue = IPadCommon.ToXML<List<ColumnsSettings>>(columnsSettingsRequest.ColumnsSettings);
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iParaType", System.Data.DbType.String, System.Data.ParameterDirection.Input, "User"));

                if (userID > 0)
                    para.Add(db.CreateParam("iParaValue", DbType.String, ParameterDirection.Input, Convert.ToInt64(userID)));
                else
                    para.Add(db.CreateParam("iParaValue", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("isDefault", DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("columnsDefs", DbType.String, ParameterDirection.Input, columnsSettingsValue));


                DataTable dt = db.ExecuteSP("ColumnsSettings_Overseas_Sunrise", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
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
        public IHttpActionResult EmailAllOverseasStone([FromBody]JObject data)
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

                MailMessage xloMail = new MailMessage();
                SmtpClient xloSmtp = new SmtpClient();
                try
                {

                    if (ConfigurationManager.AppSettings["Location"] == "H")
                        xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                    else
                        xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Shairu Gems");

                    xloMail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["BCCEmail"]));
                    if (emailAllStonesRequest.ToAddress.EndsWith(","))
                        emailAllStonesRequest.ToAddress = emailAllStonesRequest.ToAddress.Remove(emailAllStonesRequest.ToAddress.Length - 1);

                    xloMail.To.Add(emailAllStonesRequest.ToAddress);
                    xloMail.Subject = "Stone Selection";
                    xloMail.IsBodyHtml = false;
                    AlternateView av = AlternateView.CreateAlternateViewFromString(emailAllStonesRequest.Comments, null, "");
                    xloMail.AlternateViews.Add(av);

                    emailAllStonesRequest.SearchCriteria.UserID = userID;
                    emailAllStonesRequest.SearchCriteria.PageNo = "0";

                    DataTable dt = SearchOverseasStockInner(emailAllStonesRequest.SearchCriteria);
                    dt.DefaultView.RowFilter = "Sr IS NOT NULL";
                    dt = dt.DefaultView.ToTable();

                    string fileName = "";
                    string realPath = "";
                    if (dt != null && dt.Rows.Count > 0)
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
                            fileName = "OverseasSearch " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                            string _path = ConfigurationManager.AppSettings["data"];
                            realPath = System.Web.Hosting.HostingEnvironment.MapPath("~/ExcelFile/");
                            string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                            EpExcelExport.CreateOverseasExcel(dt, realPath, realPath + fileName + ".xlsx", _livepath);
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

        [NonAction]
        private bool CreateMediaZip(DataTable DataTableData, string MediaType, string FolderPath, string SubFolderPath, string ZipFileName, string DownloadURL)
        {
            bool flag = false;

            try
            {
                if (MediaType.ToLower() == "image")
                {
                    DataRow[] dra = DataTableData.Select("P_SEQ_NO IS NULL");

                    //DataTableData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                    //DataTableData = DataTableData.DefaultView.ToTable();

                    Directory.CreateDirectory(SubFolderPath);
                    bool isJson = false;

                    foreach (DataRow drItem in DataTableData.Rows)
                    {
                        string url = string.Empty;
                        if (Convert.ToBoolean(drItem["bPRimg"]) == true
                            || Convert.ToBoolean(drItem["bASimg"]) == true
                            || Convert.ToBoolean(drItem["bHTimg"]) == true
                            || Convert.ToBoolean(drItem["bHBimg"]) == true
                            || Convert.ToBoolean(drItem["bHBimg"]) == true
                            || (drItem["image_url"] != null && drItem["image_url"].ToString() != "")
                            )
                        {
                            string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                            Directory.CreateDirectory(subStoneFolder);
                            if (Convert.ToBoolean(drItem["bPRimg"]) == true)
                            {
                                url = DownloadURL + Convert.ToString(drItem["certi_no"]) + "/PR.jpg";
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\PR.jpg");
                                }
                            }
                            if (Convert.ToBoolean(drItem["bASimg"]) == true)
                            {
                                url = DownloadURL + Convert.ToString(drItem["certi_no"]) + "/AS.jpg";
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\AS.jpg");
                                }
                            }
                            if (Convert.ToBoolean(drItem["bHTimg"]) == true)
                            {
                                url = DownloadURL + Convert.ToString(drItem["certi_no"]) + "/HT.jpg";
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\HT.jpg");
                                }
                            }
                            if (Convert.ToBoolean(drItem["bHBimg"]) == true)
                            {
                                url = DownloadURL + Convert.ToString(drItem["certi_no"]) + "/HB.jpg";
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\HB.jpg");
                                }
                            }
                            //path.substring(path.indexOf("&s=")+3, path.indexOf("&c="))
                            if (drItem["image_url"] != null && drItem["image_url"].ToString() != "")
                            {
                                url = "https://dna.stonehdfile.com/stoneimages/" + drItem["image_url"].ToString().Substring(drItem["image_url"].ToString().IndexOf("&s=") + 3, drItem["image_url"].ToString().IndexOf("&c=") - drItem["image_url"].ToString().IndexOf("&s=") - 3) + ".jpg";

                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\Image.jpg");
                                }
                            }
                            isJson = true;
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
                        if (Convert.ToBoolean(drItem["bMP4"]) == true || Convert.ToBoolean(drItem["bJson"]) == true)
                        {
                            string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                            Directory.CreateDirectory(subStoneFolder);
                            if (Convert.ToBoolean(drItem["bMP4"]) == true)
                            {
                                url = DownloadURL + "imaged/" + Convert.ToString(drItem["p_seq_no"]) + "/video.mp4";
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + ".mp4");
                                }
                                isJson = isJson ? isJson : false;
                            }
                            else if (Convert.ToBoolean(drItem["bJson"]) == true)
                            {
                                isJson = true;
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

                    DataTableData.DefaultView.RowFilter = "P_SEQ_NO IS NOT NULL";
                    DataTableData = DataTableData.DefaultView.ToTable();
                    Directory.CreateDirectory(SubFolderPath);

                    foreach (DataRow drItem in DataTableData.Rows)
                    {
                        string url = string.Empty;
                        if (!string.IsNullOrEmpty(Convert.ToString(drItem["view_certi_url"])))
                        {
                            string subStoneFolder = SubFolderPath + "//" + Convert.ToString(drItem["stone_ref_no"]);
                            Directory.CreateDirectory(subStoneFolder);
                            url = Convert.ToString(drItem["view_certi_url"]);
                            using (WebClient client = new WebClient())
                            {
                                try
                                {
                                    client.DownloadFile(new Uri(url), subStoneFolder + @"\" + Convert.ToString(drItem["stone_ref_no"]) + ".pdf");
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                    ZipFile.CreateFromDirectory(SubFolderPath, FolderPath + ZipFileName + ".zip");
                    Directory.Delete(SubFolderPath, true);
                    flag = true;
                }
                else if (MediaType.ToLower() == "pdf")
                {
                    Models.PdfTemplate.ExportToPdf(DataTableData, FolderPath + ZipFileName + ".pdf");

                    flag = true;
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
            
            if (ConfigurationManager.AppSettings["ConnMode"] == "Oracle")
                parentpath = @"C:\inetpub\wwwroot\Temp\";

            filename = parentpath + filename + ".xlsx";

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ge.CreateExcel(ms);
            System.IO.File.WriteAllBytes(filename, ms.ToArray());

            return filename;
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
    }
}
