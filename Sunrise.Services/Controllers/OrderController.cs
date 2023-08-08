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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetOrderSummary([FromBody]JObject data)
        {
            OrderSummaryRequest orderSummaryRequest = new OrderSummaryRequest();
            try
            {
                orderSummaryRequest = JsonConvert.DeserializeObject<OrderSummaryRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<OrderSummaryResponse>
                {
                    Data = new List<OrderSummaryResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                }); ; ; // ;
            }

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
                if (orderSummaryRequest.YearID > 0)
                    para.Add(db.CreateParam("p_for_yearid", DbType.String, ParameterDirection.Input, orderSummaryRequest.YearID));
                else
                    para.Add(db.CreateParam("p_for_yearid", DbType.String, ParameterDirection.Input, DBNull.Value));


                DataTable dt = db.ExecuteSP("GetOrderSummary", para.ToArray(), false);
                //List<OrderSummaryResponse> orderSummaryResponses = DataTableExtension.ToList<OrderSummaryResponse>(dt);
                //return Ok(orderSummaryResponses);

                List<OrderSummaryResponse> orderSummaryResponses = new List<OrderSummaryResponse>();
                orderSummaryResponses = DataTableExtension.ToList<OrderSummaryResponse>(dt);
                if (orderSummaryResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<OrderSummaryResponse>
                    {
                        Data = orderSummaryResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<OrderSummaryResponse>
                    {
                        Data = orderSummaryResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<OrderSummaryResponse>
                {
                    Data = new List<OrderSummaryResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        //[WebMethod(EnableSession = true, Description = "Please maintain cookie for this method request with cookie")]
        //public CommonResultResponse ConfirmPurchase(String StoneID, String Comments, String UserID, String TokenNo)
        //{
        //    CommonResultResponse resp = new CommonResultResponse();

        //    try
        //    {
        //        if (TokenNo != Session.SessionID || Session[SessionUserName] == null)
        //        {
        //            //throw new System.Web.Services.Protocols.SoapException("UnAuthorised", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri);
        //            resp.Status = "";
        //            resp.Message = "";
        //            resp.Error = "UnAuthorised";
        //            return resp;
        //        }

        //        Database dbuser = new Database();
        //        System.Collections.Generic.List<System.Data.IDbDataParameter> parauser;
        //        parauser = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

        //        parauser.Clear();
        //        parauser.Add(dbuser.CreateParam("iiUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(UserID)));
        //        System.Data.DataTable dtUserDetail = dbuser.ExecuteSP("UserMas_SelectOne", parauser.ToArray(), false);


        //        Int32 iOrderId = 0;
        //        String CustomerName = GetPartyNameByUserId(Convert.ToInt32(UserID));
        //        Int32 AssistByID = GetAssistByUserId(Convert.ToInt32(UserID));
        //        string user_name = dtUserDetail.Rows[0]["sUsername"].ToString();
        //        string device_type = dtUserDetail.Rows[0]["device_type"].ToString();
        //        int admin = Convert.ToInt32(dtUserDetail.Rows[0]["isadmin"].ToString());
        //        int emp = Convert.ToInt32(dtUserDetail.Rows[0]["isemp"].ToString());

        //        CommonResultResponse resHold = GetAddToHold(StoneID, UserID, CustomerName, Comments, "Y", ref iOrderId, TokenNo, user_name, device_type, admin, emp, AssistByID);
        //        if (resHold.Status == "SUCCESS")
        //        {
        //            //priyanka on date[14-feb-17] as per doc [589]
        //            resp.Status = "SUCCESS";
        //            if (iOrderId > 0)
        //                resp.Message = "Order placed successfully. " + resHold.Message;
        //            else
        //                resp.Message = "Order placed successfully. ";
        //            resp.Error = "";
        //            if (iOrderId > 0)
        //                SendOrderMail(iOrderId, Comments, true, UserID.ToString());
        //            //////
        //        }
        //        else if (resHold.Status == "FAIL")
        //        {
        //            resp.Status = "FAIL";
        //            //resp.Message = "Order placed successfully. " + resHold.Message;
        //            // Change By Hitesh on [21-02-2017] bcoz when status fail than wrong message display
        //            resp.Message = resHold.Message;
        //            resp.Error = "";
        //        }
        //        else
        //        {
        //            resp.Status = "FAIL";
        //            resp.Message = resHold.Message;
        //            resp.Error = "";
        //        }
        //        return resp;
        //    }

        //    // }
        //    catch (Exception ex)
        //    {
        //        InsertErrorLog(ex, null);
        //        resp.Status = "FAIL";
        //        resp.Message = "Due to technical issue please try again.";
        //        resp.Error = "";
        //        return resp;
        //    }
        //    //}
        //    //else
        //    //{
        //    //    resp.Status = "";
        //    //    resp.Error = "";
        //    //    resp.Message = "This facility is not available from 25th Nov to 30th Nov between 18.00 to 10.00am due to exhibition.Kindly use send request.";
        //    //    return resp;
        //    //    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This facility is not avilable from 15th Sep to 20th Sep between 18.00 to 10.00am due to exhibition.Kindly use send request.')", true);

        //    //}
        //}

        [HttpPost]
        public IHttpActionResult GetAssistPersonDetail([FromBody]JObject data)
        {
            try
            {
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                CommonResponse resp = new CommonResponse();

                Int32 AssistByID = GetAssistByUserId(Convert.ToInt32(userID));
                string AssistDetail = GetAssistDetail(AssistByID);

                resp.Status = "1";
                resp.Message = AssistDetail;
                resp.Error = "";
                return Ok(resp);
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
        [NonAction]
        private String GetPartyNameByUserId(Int32 UserId)
        {
            Database db = new Database();
            List<IDbDataParameter> para = new List<IDbDataParameter>
            {
                db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(UserId))
            };

            DataTable dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["sCompName"].ToString().Length > 0)
                    return dt.Rows[0]["sCompName"].ToString();
                else
                    return dt.Rows[0]["sFirstName"].ToString();
            }
            else
            {
                return "";
            }
        }
        [NonAction]
        private String GetCompanyNameForHold(Int32 UserId)
        {
            Database db = new Database();
            List<IDbDataParameter> para = new List<IDbDataParameter>
            {
                db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(UserId))
            };

            DataTable dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["sCompName"].ToString();
            }
            else
            {
                return "";
            }
        }
        [NonAction]
        private Int32 GetAssistByUserId(Int32 UserId)
        {
            Database db = new Database();
            List<IDbDataParameter> para = new List<IDbDataParameter>
            {
                db.CreateParam("p_for_userid", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(UserId))
            };

            DataTable dt = db.ExecuteSP("get_assist_by_emp", para.ToArray(), false);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["iEmpid"].ToString().Length > 0)
                    return Convert.ToInt32(dt.Rows[0]["iEmpid"]);
            }

            //User Id of Jigneshbhai (JIGIJIGA) // why jigneshbhai only in return question from divya rana 
            //return 10; //jignesh user remove and samit add
            return 5682;
        }

        [HttpPost]
        public IHttpActionResult ViewCart([FromBody]JObject data)
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

                SearchSummary searchSummary = new SearchSummary();
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    //object sumObject;
                    //sumObject = dtData.Compute("Count(pcs)", string.Empty);
                    //searchSummary.TOT_PCS = Convert.ToInt32(sumObject.ToString());
                    //sumObject = dtData.Compute("Sum(cts)", string.Empty);
                    //searchSummary.TOT_CTS = Convert.ToDouble(sumObject.ToString());
                    //sumObject = dtData.Compute("Sum(rap_amount)", string.Empty);
                    //searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble(sumObject.ToString());
                    //sumObject = dtData.Compute("Sum(NET_AMOUNT)", string.Empty);
                    //searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(sumObject.ToString());
                    //sumObject = dtData.Compute("Avg(PRICE_PER_CTS)", string.Empty);
                    //searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(sumObject.ToString());
                    //sumObject = dtData.Compute("Avg(SALES_DISC_PER1)", string.Empty);
                    //searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble(sumObject.ToString());

                    DataRow[] dra = dtData.Select("sr IS NULL");
                    searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["stone_ref_no"]);
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["PCS"]);
                    searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["Cts"]);
                    searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble((dra[0]["rap_amount"].ToString() != "" && dra[0]["rap_amount"].ToString() != null ? dra[0]["rap_amount"] : "0"));
                    searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["NET_AMOUNT"]);
                    searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(dra[0]["PRICE_PER_CTS"]);
                    searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0"));
                }

                dtData.DefaultView.RowFilter = "sr IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                ViewCartResponse viewCartResponse = new ViewCartResponse();

                List<SearchStone> listSearchStone = new List<SearchStone>();
                listSearchStone = DataTableExtension.ToList<SearchStone>(dtData);
                List<ViewCartResponse> viewCartResponses = new List<ViewCartResponse>();

                if (dtData != null)
                {
                    viewCartResponses.Add(new ViewCartResponse()
                    {
                        DataList = listSearchStone,
                        DataSummary = searchSummary
                    });

                    return Ok(new ServiceResponse<ViewCartResponse>
                    {
                        Data = viewCartResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ViewCartResponse>
                    {
                        Data = viewCartResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ViewCartResponse>
                {
                    Data = new List<ViewCartResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult AddRemoveToCart([FromBody]JObject data)
        {
            AddToCartRequest addToCartRequest = new AddToCartRequest();
            try
            {
                addToCartRequest = JsonConvert.DeserializeObject<AddToCartRequest>(data.ToString());
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
                int transID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == Lib.Constants.ServiceConstants.SessionTransID).FirstOrDefault().Value);

                string IsAddToCart = GetAddRemoveToCart(addToCartRequest, userID, transID);
                CommonResponse resp = new CommonResponse();
                if (IsAddToCart == "Y")
                {
                    resp.Status = "1";
                    if (addToCartRequest.TransType == ((char)TransactionType.Add).ToString())
                    {
                        resp.Message = "Stone(s) added in cart successfully";
                    }
                    else if (addToCartRequest.TransType == ((char)TransactionType.Remove).ToString())
                    {
                        resp.Message = "Stone(s) removed from cart successfully";
                    }
                    resp.Error = "";
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = IsAddToCart;
                    resp.Error = "";
                }

                return Ok(resp);
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
        public IHttpActionResult RemoveToCart([FromBody]JObject data)
        {
            RemoveFromCartRequest input = new RemoveFromCartRequest();
            try
            {
                input = JsonConvert.DeserializeObject<RemoveFromCartRequest>(data.ToString());
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
                bool IsRemove = GetRemoveToCart(input.removeToCart);
                CommonResponse resp = new CommonResponse();
                if (IsRemove)
                {
                    resp.Status = "1";
                    resp.Message = "Stone(s) removed from cart successfully";
                    resp.Error = "";

                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Remove from cart failed";
                    resp.Error = "";
                }

                return Ok(resp);
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
        public IHttpActionResult AddOverseasToCart([FromBody]JObject data)
        {
            AddToCartRequest addToCartRequest = new AddToCartRequest();
            try
            {
                addToCartRequest = JsonConvert.DeserializeObject<AddToCartRequest>(data.ToString());
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
                int transID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == Lib.Constants.ServiceConstants.SessionTransID).FirstOrDefault().Value);

                string IsAddToCart = "";

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("p_for_transid", DbType.String, ParameterDirection.Input, transID));
                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, userID));
                para.Add(db.CreateParam("p_for_refno", DbType.String, ParameterDirection.Input, addToCartRequest.StoneID, true));

                if (addToCartRequest.OfferTrans != null && addToCartRequest.OfferTrans != "")
                {
                    para.Add(db.CreateParam("p_for_offer", DbType.String, ParameterDirection.Input, addToCartRequest.OfferTrans));
                }

                DataTable dt = db.ExecuteSP("IPD_Add_Overseas_Cart", para.ToArray(), false);

                IsAddToCart = (dt != null && dt.Rows.Count > 0) ?
                    dt.Rows[0]["STATUS"].ToString() : "Something Went wrong.\nPlease try again later";

                CommonResponse resp = new CommonResponse();
                if (IsAddToCart == "Y")
                {
                    resp.Status = "1";
                    resp.Message = "Stone(s) added in cart successfully";
                    resp.Error = "";
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = IsAddToCart;
                    resp.Error = "";
                }

                return Ok(resp);
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
        public IHttpActionResult ExcludeStoneFromStockInsert([FromBody]JObject data)
        {
            ExcludeStoneFromStockRequest excludeStoneFromStockRequest = new ExcludeStoneFromStockRequest();
            try
            {
                excludeStoneFromStockRequest = JsonConvert.DeserializeObject<ExcludeStoneFromStockRequest>(data.ToString());
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

                var str = excludeStoneFromStockRequest.OrderId.Split(',');

                for (int j = 0; j < str.Length; j++)
                {
                    Database db = new Database();
                    System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                    para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
                    para.Add(db.CreateParam("iOrderDetId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(str[j])));
                    para.Add(db.CreateParam("bIsExcludeStk", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, excludeStoneFromStockRequest.bIsExcludeStk));
                    db.ExecuteSP("OrderDet_UpdateExcludeStk", para.ToArray(), false);
                }

                CommonResponse resp = new CommonResponse();
                resp.Status = "1";
                resp.Message = "Update successfully";
                resp.Error = "";
                return Ok(resp);
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
        public IHttpActionResult ViewWishList([FromBody]JObject data)
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
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                viewWishListRequest.UserID = userID;

                //if (viewWishListRequest.IsAdmin == false)
                //{
                //    viewWishListRequest.UserID = userID;
                //}
                //else if (viewWishListRequest.IsAdmin == true && viewWishListRequest.UserID <= 0)
                //{
                //    viewWishListRequest.UserID = userID;
                //}
                DataTable dtData = ViewWishListInner(viewWishListRequest);

                dtData.DefaultView.RowFilter = "stone_ref_no IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                SearchSummary searchSummary = new SearchSummary();
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    //object sumObject;
                    //sumObject = dtData.Compute("Count(pcs)", string.Empty);
                    //searchSummary.TOT_PCS = Convert.ToInt32(sumObject.ToString());
                    //sumObject = dtData.Compute("Sum(cts)", string.Empty);
                    //searchSummary.TOT_CTS = Convert.ToDouble(sumObject.ToString());
                    //sumObject = dtData.Compute("Sum(rap_amount)", string.Empty);
                    //searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble(sumObject.ToString());
                    //sumObject = dtData.Compute("Sum(NET_AMOUNT)", string.Empty);
                    //searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(sumObject.ToString());
                    //sumObject = dtData.Compute("Avg(PRICE_PER_CTS)", string.Empty);
                    //searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(sumObject.ToString());
                    //sumObject = dtData.Compute("Avg(SALES_DISC_PER1)", string.Empty);
                    //searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble(sumObject.ToString());

                    DataRow[] dra = dtData.Select("VSR IS NULL");
                    searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["stone_ref_no"]);
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["PCS"]);
                    searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["Cts"]);
                    searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble((dra[0]["rap_amount"].ToString() != "" && dra[0]["rap_amount"].ToString() != null ? dra[0]["rap_amount"] : "0"));
                    searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["NET_AMOUNT"]);
                    searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(dra[0]["PRICE_PER_CTS"]);
                    searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((dra[0]["SALES_DISC_PER"].ToString() != "" && dra[0]["SALES_DISC_PER"].ToString() != null ? dra[0]["SALES_DISC_PER"] : "0"));
                }

                dtData.DefaultView.RowFilter = "VSR IS NOT NULL";
                dtData = dtData.DefaultView.ToTable();

                ViewWishListResponse viewWishListResponse = new ViewWishListResponse();

                List<SearchStone> listSearchStone = new List<SearchStone>();
                listSearchStone = DataTableExtension.ToList<SearchStone>(dtData);
                List<ViewWishListResponse> viewWishListResponses = new List<ViewWishListResponse>();

                if (dtData != null)
                {
                    viewWishListResponses.Add(new ViewWishListResponse()
                    {
                        DataList = listSearchStone,
                        DataSummary = searchSummary
                    });

                    return Ok(new ServiceResponse<ViewWishListResponse>
                    {
                        Data = viewWishListResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<ViewWishListResponse>
                    {
                        Data = viewWishListResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<ViewWishListResponse>
                {
                    Data = new List<ViewWishListResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult AddRemoveToWishList([FromBody]JObject data)
        {
            AddToWishListRequest addToWishListRequest = new AddToWishListRequest();
            try
            {
                addToWishListRequest = JsonConvert.DeserializeObject<AddToWishListRequest>(data.ToString());
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
                int transID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == Lib.Constants.ServiceConstants.SessionTransID).FirstOrDefault().Value);

                string IsAddTowishList = GetAddRemoveToWishList(addToWishListRequest, userID, transID);

                CommonResponse resp = new CommonResponse();
                if (IsAddTowishList == "Y")
                {
                    resp.Status = "1";
                    if (addToWishListRequest.TransType == ((char)TransactionType.Add).ToString())
                    {
                        resp.Message = "Stone(s) added in wishList successfully";
                    }
                    else if (addToWishListRequest.TransType == ((char)TransactionType.Remove).ToString())
                    {
                        resp.Message = "Stone(s) removed from wishList successfully";
                    }
                    resp.Error = "";
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = IsAddTowishList;
                    resp.Error = "";
                }

                return Ok(resp);
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
        public IHttpActionResult GetOrderHistory([FromBody]JObject data)
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
        public IHttpActionResult GetConfirmOrderList([FromBody]JObject data)
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
                DataTable dtData = GetConfirmOrderInner(orderHistoryRequest);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    dtData.DefaultView.RowFilter = "iSr IS NOT NULL";
                    dtData = dtData.DefaultView.ToTable();
                    if (dtData != null && dtData.Rows.Count > 0)
                    {

                        List<ConfirmOrderData> listOrder = new List<ConfirmOrderData>();
                        listOrder = DataTableExtension.ToList<ConfirmOrderData>(dtData);
                        List<OrderConfirmResponse> orderHistoryResponse = new List<OrderConfirmResponse>();

                        //orderHistoryResponse = DataTableExtension.ToList<OrderHistoryResponse>(dtData);
                        if (listOrder.Count > 0)
                        { //List<string> lst = dtData.AsDataView().ToTable(true, "CompanyName").ToList();
                          //var a = (from r in dtData.AsEnumerable()
                          //  select r["CompanyName"]).Distinct().ToList();
                            orderHistoryResponse.Add(new OrderConfirmResponse()
                            {
                                DataList = listOrder
                            });

                            return Ok(new ServiceResponse<OrderConfirmResponse>
                            {
                                Data = orderHistoryResponse,
                                Message = "SUCCESS",
                                Status = "1"
                            });
                        }
                        else
                        {
                            return Ok(new ServiceResponse<OrderConfirmResponse>
                            {
                                Data = orderHistoryResponse,
                                Message = "Something Went wrong.",
                                Status = "0"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new ServiceResponse<OrderConfirmResponse>
                        {
                            Data = null,
                            Message = "No data found.",
                            Status = "1"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<OrderConfirmResponse>
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
        public IHttpActionResult ExcelGetConfirmOrder([FromBody]JObject data)
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
                DataTable dtData = GetConfirmOrderInner(orderHistoryRequest);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    string filename = "Confirm Order Master " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    EpExcelExport.ExcelGetConfirmOrder(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath,
                                                    Convert.ToDateTime(orderHistoryRequest.FromDate), Convert.ToDateTime(orderHistoryRequest.ToDate));

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
        public IHttpActionResult GetOrderHistoryFilters([FromBody]JObject data)
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

        [HttpPost]
        public IHttpActionResult DownloadOrderHistory([FromBody]JObject data)
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
                        string filename = "Bhavyam Diam Order History " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                        string _path = ConfigurationManager.AppSettings["data"];
                        string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                        string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                        //EpExcelExport.Excel_Generate(dtData.DefaultView.ToTable(), realpath + "Data" + random + ".xlsx");
                        EpExcelExport.CreateOrderExcel(dtData.DefaultView.ToTable(), realpath, realpath + filename + ".xlsx", _livepath,
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
        public IHttpActionResult DownloadWishList([FromBody]JObject data)
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
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                viewWishListRequest.UserID = userID;

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
                if (dtData.Rows.Count > 0)
                {
                    string filename = "WishList " + Lib.Models.Common.GetHKTime().ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");
                    string _livepath = ConfigurationManager.AppSettings["LiveUrl"];

                    //EpExcelExport.Excel_Generate(dtData.DefaultView.ToTable(), realpath + "Data" + random + ".xlsx");
                    EpExcelExport.CreateWishListExcel(dtData, realpath, realpath + filename + ".xlsx", _livepath, viewWishListRequest.IsAssistBy);

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

                if (!string.IsNullOrEmpty(ViewCart.OrderBy))
                    para.Add(db.CreateParam("p_for_OrderBy", DbType.String, ParameterDirection.Input, ViewCart.OrderBy.ToUpper()));
                else
                    para.Add(db.CreateParam("p_for_OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

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

        [NonAction]
        private string GetAddRemoveToCart(AddToCartRequest AddToCart, int UserID, int TransID)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("p_for_transid", DbType.String, ParameterDirection.Input, TransID));
                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, UserID));
                para.Add(db.CreateParam("p_for_refno", DbType.String, ParameterDirection.Input, AddToCart.StoneID, true));
                para.Add(db.CreateParam("p_for_type", DbType.String, ParameterDirection.Input, AddToCart.TransType));

                if (AddToCart.OfferTrans != null && AddToCart.OfferTrans != "")
                {
                    para.Add(db.CreateParam("p_for_offer", DbType.String, ParameterDirection.Input, AddToCart.OfferTrans));
                }

                DataTable dt = db.ExecuteSP("ipd_add_remove_cart", para.ToArray(), false);

                return (dt != null && dt.Rows.Count > 0) ? dt.Rows[0]["STATUS"].ToString() : "Something Went wrong.\nPlease try again later";
            }
            catch (Exception ex)
            {
                return "Something Went wrong.\nPlease try again later";
            }
        }

        [NonAction]
        private bool GetRemoveToCart(string removeToCart)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("p_for_removeToCart", DbType.String, ParameterDirection.Input, removeToCart));

                db.ExecuteSP("IPD_Remove_Cart", para.ToArray(), false);
                return true;
            }
            catch (Exception ex)
            {
                return false;
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

        [NonAction]
        private string GetAddRemoveToWishList(AddToWishListRequest AddToWishList, int UserID, int TransID)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("p_for_transid", DbType.String, ParameterDirection.Input, TransID));
                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, UserID));
                para.Add(db.CreateParam("p_for_refno", DbType.String, ParameterDirection.Input, AddToWishList.StoneID, true));
                para.Add(db.CreateParam("p_for_type", DbType.String, ParameterDirection.Input, AddToWishList.TransType));

                DataTable dt = db.ExecuteSP("IPD_Add_Remove_WishList", para.ToArray(), false);

                return (dt != null && dt.Rows.Count > 0) ? dt.Rows[0]["STATUS"].ToString() : "Something Went wrong.\nPlease try again later";
            }
            catch (Exception ex)
            {
                return "Something Went wrong.\nPlease try again later";
            }
        }
        [NonAction]
        private System.Data.DataTable GetDiamondDetailInner(String StoneID, String UserID, String TokenNo)
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
        [NonAction]
        private string GetAssistDetail(int UserId)
        {
            string AssistName1 = string.Empty, AssistMobile1 = string.Empty, AssistEmail1 = string.Empty, WeChatId = string.Empty;
            string AssistDetail = string.Empty;

            Database db2 = new Database();
            List<IDbDataParameter> para2 = new List<IDbDataParameter>();
            para2.Add(db2.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(UserId)));
            DataTable dt = db2.ExecuteSP("UserMas_SelectOne", para2.ToArray(), false);

            if (dt != null && dt.Rows.Count > 0)
            {
                AssistName1 = dt.Rows[0]["sFirstName"].ToString() + " " + dt.Rows[0]["sLastName"].ToString();
                AssistMobile1 = dt.Rows[0]["sCompMobile"].ToString();
                AssistEmail1 = dt.Rows[0]["sCompEmail"].ToString();
                WeChatId = dt.Rows[0]["sWeChatId"].ToString();

                AssistDetail = "<table><tbody>";
                AssistDetail += "<tr><td><i class=\"fa fa-user\" style=\"font-size: 20px; color: teal;\"></i></td>";
                AssistDetail += "<td>&nbsp;" + AssistName1 + "</td>";
                AssistDetail += "</tr>";
                if (!string.IsNullOrEmpty(AssistMobile1))
                {
                    AssistDetail += "<tr><td><i class=\"fa fa-mobile\" style=\"font-size: 27px; color: #27c4cc; margin-left: 2px;\"></i></td>";
                    AssistDetail += "<td>&nbsp;" + AssistMobile1 + "</td>";
                    AssistDetail += "</tr>";
                }
                if (!string.IsNullOrEmpty(AssistEmail1))
                {
                    AssistDetail += "<tr><td><i class=\"fa fa-envelope-o\" style=\"font-size: 20px; color: red; margin-left: -1px;\"></i></td>";
                    AssistDetail += "<td>&nbsp;" + AssistEmail1 + "</td>";
                    AssistDetail += "</tr>";
                }
                if (!string.IsNullOrEmpty(WeChatId))
                {
                    AssistDetail += "<tr><td><i class=\"fa fa-weixin\" style=\"font-size: 21px; color: #2dc100; margin-left: -3px;\"></i></td>";
                    AssistDetail += "<td>&nbsp;" + WeChatId + "</td>";
                    AssistDetail += "</tr>";
                }
                AssistDetail += "</tbody></table>";
            }

            return AssistDetail;
        }
        [NonAction]
        private bool Send_HoldRelease_StoneMail(String MailType, String StoneID, String Comments, int userid, int LoginUserid, int HoldCompany)
        {
            try
            {
                string lsToMail = "";

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Clear();
                para.Add(db.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(LoginUserid)));
                DataTable dtUserDetail = db.ExecuteSP("UserMas_SelectOne", para.ToArray(), false);

                Database db1 = new Database(Request);
                para.Clear();
                para.Add(db1.CreateParam("sUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(LoginUserid)));
                DataTable dtToMailList = db1.ExecuteSP("UserMas_SelectEmailByUserId", para.ToArray(), false);

                foreach (DataRow row in dtToMailList.Rows)
                    lsToMail += row["sEmail"].ToString() + ",";

                if (lsToMail.Length > 0)
                    lsToMail = lsToMail.Remove(lsToMail.Length - 1);

                string Mobile = "";

                Mobile = String.IsNullOrEmpty(dtUserDetail.Rows[0]["sCompMobile"].ToString()) ? "" : dtUserDetail.Rows[0]["sCompMobile"].ToString() + ", ";
                Mobile += String.IsNullOrEmpty(dtUserDetail.Rows[0]["sCompMobile2"].ToString()) ? "" : dtUserDetail.Rows[0]["sCompMobile2"].ToString() + ", ";
                if (Mobile.Length > 1)
                    Mobile = Mobile.Remove(Mobile.Length - 2);


                /*****ANDROID AND IPHONE IN ORDER PLACED NOTIFICATION SEND IF CUSTOMER PLACED ORDER*************/
                //if (dtUserDetail.Rows[0]["iUserType"].ToString() == "3")
                //{
                //    Database dbs = new Database(Request);
                //    List<IDbDataParameter> paras;
                //    paras = new List<IDbDataParameter>();

                //    paras.Clear();
                //    paras.Add(dbs.CreateParam("iUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(userid)));
                //    DataTable dtToken = dbs.ExecuteSP("FCMToken_Get", paras.ToArray(), false);

                //    string NotificationMsg = string.Empty;
                //    if (dtUserDetail.Rows[0]["sCompName"] != null && dtUserDetail.Rows[0]["sCompName"].ToString() != "")
                //    {
                //        NotificationMsg = dtUserDetail.Rows[0]["sCompName"].ToString() + " has placed the order with " + iOrderID + ".";
                //    }

                //    if (dtToken != null && dtToken.Rows.Count > 0)
                //    {
                //        for (int I = 0; I <= dtToken.Rows.Count - 1; I++)
                //        {
                //            string Token = Convert.ToString(dtToken.Rows[I]["Token"]);
                //            string DeviceType = Convert.ToString(dtToken.Rows[I]["DeviceType"]);

                //            PublishNotification(DeviceType.ToUpper(), Token, "Order Placed", NotificationMsg, "");
                //        }
                //    }
                //}
                /************************************************************************************************/

                String sNotes = Comments;
                if (dtUserDetail.Rows[0]["sCompEmail"].ToString().Length > 0 || dtUserDetail.Rows[0]["sEmail"].ToString().Length > 0)
                {
                    string custMail = "";

                    if (dtUserDetail.Rows[0]["sCompEmail"].ToString() == "" || dtUserDetail.Rows[0]["sCompEmail"].ToString() == null)
                    {
                        custMail = dtUserDetail.Rows[0]["sEmail"].ToString();
                        if (dtUserDetail.Rows[0]["sEmailPersonal"].ToString() != "" && dtUserDetail.Rows[0]["sEmailPersonal"].ToString() != null)
                            custMail += "," + dtUserDetail.Rows[0]["sEmailPersonal"];
                    }
                    else
                    {
                        custMail = dtUserDetail.Rows[0]["sCompEmail"].ToString();
                        if (dtUserDetail.Rows[0]["sCompEmail2"].ToString() != "" && dtUserDetail.Rows[0]["sCompEmail2"].ToString() != null)
                            custMail += "," + dtUserDetail.Rows[0]["sCompEmail2"];
                    }

                    custMail = custMail.Trim(',');
                    string assistbyemail = null;
                    if (dtUserDetail.Rows[0]["Email_AssistBy1"] != null && dtUserDetail.Rows[0]["Email_AssistBy1"].ToString() != "")
                    {
                        assistbyemail = assistbyemail + dtUserDetail.Rows[0]["Email_AssistBy1"].ToString() + ",";
                    }
                    if (dtUserDetail.Rows[0]["Email_AssistBy2"] != null && dtUserDetail.Rows[0]["Email_AssistBy2"].ToString() != "")
                    {
                        assistbyemail = assistbyemail + dtUserDetail.Rows[0]["Email_AssistBy2"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(assistbyemail))
                    {
                        assistbyemail = assistbyemail.Remove(assistbyemail.Length - 1);
                    }

                    if (dtUserDetail.Rows[0]["iUserType"].ToString() == "3")
                    {
                        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ORDER_ADMIN_EMAILID"]))
                            assistbyemail += ConfigurationManager.AppSettings["ORDER_ADMIN_EMAILID"];
                    }

                    try
                    {
                        string _HoldCompany = "";
                        if (MailType == "HOLD")
                        {
                            _HoldCompany = GetCompanyNameForHold(HoldCompany);
                        }

                        IPadCommon.EmailNewHoldRelease(MailType, userid, LoginUserid, custMail, StoneID, Lib.Models.Common.GetHKTime(), assistbyemail, Comments, _HoldCompany);
                    }
                    catch (Exception ex)
                    {
                        DAL.Common.InsertErrorLog(ex, "HOLD STONE EMAIL ERROR", Request);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
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
                if (!string.IsNullOrEmpty(orderHistoryRequest.Status))
                    para.Add(db.CreateParam("Status", DbType.String, ParameterDirection.Input, (orderHistoryRequest.Status)));
                else
                    para.Add(db.CreateParam("Status", DbType.String, ParameterDirection.Input, DBNull.Value));

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
                if (!string.IsNullOrEmpty(orderHistoryRequest.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, (orderHistoryRequest.UserName)));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));
                if (!string.IsNullOrEmpty(orderHistoryRequest.Location))
                    para.Add(db.CreateParam("Location", DbType.String, ParameterDirection.Input, (orderHistoryRequest.Location)));
                else
                    para.Add(db.CreateParam("Location", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(orderHistoryRequest.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, (orderHistoryRequest.OrderBy)));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("PgSize", DbType.String, ParameterDirection.Input, (orderHistoryRequest.PgSize)));
                para.Add(db.CreateParam("p_for_FormName", DbType.String, ParameterDirection.Input, orderHistoryRequest.FormName));
                para.Add(db.CreateParam("p_for_ActivityType", DbType.String, ParameterDirection.Input, orderHistoryRequest.ActivityType));

                if (!string.IsNullOrEmpty(orderHistoryRequest.iUserid_FullOrderDate))
                    para.Add(db.CreateParam("p_for_iUserid_FullOrderDate", DbType.String, ParameterDirection.Input, orderHistoryRequest.iUserid_FullOrderDate));
                else
                    para.Add(db.CreateParam("p_for_iUserid_FullOrderDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("PickUp", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.PickUp));
                para.Add(db.CreateParam("NotPickUp", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.NotPickUp));
                para.Add(db.CreateParam("Collected", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.Collected));
                para.Add(db.CreateParam("NotCollected", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.NotCollected));
                para.Add(db.CreateParam("DateStatus", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.DateStatus));
                para.Add(db.CreateParam("SubUser", DbType.Boolean, ParameterDirection.Input, orderHistoryRequest.SubUser));

                DataTable dsData = db.ExecuteSP("IPD_Get_Order_History_Sunrise", para.ToArray(), false);

                return dsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private DataTable GetConfirmOrderInner(OrderHistoryRequest orderHistoryRequest)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(orderHistoryRequest.FromDate))
                    para.Add(db.CreateParam("dtFromDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, orderHistoryRequest.FromDate));
                else
                    para.Add(db.CreateParam("dtFromDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(orderHistoryRequest.ToDate))
                    para.Add(db.CreateParam("dtToDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, orderHistoryRequest.ToDate));
                else
                    para.Add(db.CreateParam("dtToDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(orderHistoryRequest.RefNo))
                    para.Add(db.CreateParam("Stock_OrderNo_Certi", System.Data.DbType.String, System.Data.ParameterDirection.Input, orderHistoryRequest.RefNo));
                else
                    para.Add(db.CreateParam("Stock_OrderNo_Certi", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(orderHistoryRequest.CompanyName))
                    para.Add(db.CreateParam("Company_User", System.Data.DbType.String, System.Data.ParameterDirection.Input, orderHistoryRequest.CompanyName));
                else
                    para.Add(db.CreateParam("Company_User", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(orderHistoryRequest.OrderBy))
                    para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, orderHistoryRequest.OrderBy));
                else
                    para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(orderHistoryRequest.PageNo))
                    para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(orderHistoryRequest.PageNo)));
                else
                    para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(orderHistoryRequest.PageSize))
                    para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(orderHistoryRequest.PageSize)));
                else
                    para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, null));

                if (!string.IsNullOrEmpty(orderHistoryRequest.Assist))
                    para.Add(db.CreateParam("Assist", System.Data.DbType.String, System.Data.ParameterDirection.Input, orderHistoryRequest.Assist));
                else
                    para.Add(db.CreateParam("Assist", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

                System.Data.DataTable dt = db.ExecuteSP("Get_ConfirmOrder", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private CommonResponse SendPushMessageInner(String WebsiteName, String UdID, String DeviseType, String NotificationMsg, String IsDevelopment)
        {
            CommonResponse resp = new CommonResponse();
            try
            {
                String RetReslt = "";
                if (IsDevelopment == "")
                { IsDevelopment = "0"; }

                switch (DeviseType.ToString().ToUpper())
                {
                    case "IPAD":
                        RetReslt = PushMessageApple.pushMessage(WebsiteName, UdID, "", "0", IsDevelopment, NotificationMsg);
                        if (RetReslt == "Message sent")
                        {
                            resp.Message = "";
                            resp.Status = "1";
                            resp.Error = "";
                            return resp;
                        }
                        else
                        {
                            resp.Message = RetReslt;
                            resp.Status = "0";
                            resp.Error = "";
                            return resp;
                        }
                    case "IPHONE":
                        RetReslt = PushMessageApple.pushMessage(WebsiteName, UdID, "", "1", IsDevelopment, NotificationMsg);
                        if (RetReslt == "Message sent")
                        {
                            resp.Message = "";
                            resp.Status = "1";
                            resp.Error = "";
                            return resp;
                        }
                        else
                        {
                            resp.Message = RetReslt;
                            resp.Status = "0";
                            resp.Error = "";
                            return resp;
                        }
                    case "ANDROID":

                        RetReslt = PushMessageAndroid.pushMessage(WebsiteName, UdID, NotificationMsg);

                        if (!RetReslt.ToUpper().Contains("ERROR"))
                        {
                            resp.Message = "";
                            resp.Status = "1";
                            resp.Error = "";
                            return resp;
                        }
                        else
                        {
                            resp.Message = RetReslt;
                            resp.Status = "0";
                            resp.Error = "";
                            return resp;
                        }

                    default:
                        resp.Message = "Invalid Devise Type Found. Device type is '" + DeviseType.ToString() + "'";
                        resp.Status = "0";
                        resp.Error = "";
                        return resp;
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                resp.Message = ex.Message.ToString();
                resp.Status = "0";
                resp.Error = ex.ToString();
                return resp;
            }
        }
        public void PublishNotification(string DeviceType, string DeviceIds, string title, string body, string clickaction)
        {
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer()
                {
                    MaxJsonLength = Int32.MaxValue // specify length as per your business requirement
                };
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = new Dictionary<string, object>();

                string[] strDevice = DeviceIds.Split(',');

                string SenderId = "300946530292";
                string ServerKey = "AAAARhHPn_Q:APA91bHC2SDIJd7BMW0rBr-NRXKhmm5pTmw4h2G-HB3ZUmf1KXN7iLUxnWDt1hvUC9obVo2xKLxR718ug4z4uygyG6iATmcuvNobyBeDonUKY5PIbITxBb_l-a3VMp1S0obciL7mf1le";
                var applicationID = ServerKey;
                var senderId = SenderId;
                //string deviceId = DeviceId;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                //string[] deviceIds = new string[] { strDevice };

                Byte[] byteArray;


                if (DeviceType == "ANDROID")
                {
                    var data = new
                    {
                        registration_ids = strDevice,
                        data = new
                        {
                            body = body,
                            message = body,
                            title = title,
                            click_action = clickaction,
                        },
                        content_available = true,
                        priority = "high"

                    };
                    serializer = new JavaScriptSerializer();
                    var json = serializer.Serialize(data);
                    byteArray = Encoding.UTF8.GetBytes(json);
                }
                else
                {
                    var data = new
                    {
                        registration_ids = strDevice,

                        notification = new
                        {
                            body = body,
                            message = body,
                            title = title,
                            click_action = clickaction
                        },
                        content_available = true,
                        priority = "high"

                    };
                    serializer = new JavaScriptSerializer();
                    var json = serializer.Serialize(data);
                    byteArray = Encoding.UTF8.GetBytes(json);
                }

                //var serializer = new JavaScriptSerializer();
                //var json = serializer.Serialize(data);
                //Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                string Response = "";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();

                                Response = sResponseFromServer;
                            }
                        }
                    }
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Notification Send..');", true);
            }
            catch (Exception ex)
            {

            }
        }


        #region PLACE ORDER IN SEARCH STOCK, MY WISHLIST, MY CART, PAIR SEARCH, NEW ARRIVAL

        [HttpPost]
        public IHttpActionResult Final_PlaceOrder([FromBody]JObject data)
        {
            Final_PlaceOrderRequest confirmOrderRequest = new Final_PlaceOrderRequest();
            try
            {
                confirmOrderRequest = JsonConvert.DeserializeObject<Final_PlaceOrderRequest>(data.ToString());
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
                int transID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == Lib.Constants.ServiceConstants.SessionTransID).FirstOrDefault().Value);

                CommonResponse resp = new CommonResponse();

                long iOrderId = Order_Save(confirmOrderRequest, userID);

                if (iOrderId > 0)
                {
                    SendOrderMail(iOrderId, confirmOrderRequest.Comments, userID);

                    resp.Status = "SUCCESS";
                    resp.Message = "Order Placed Successfully";
                    resp.Error = "";
                }
                else
                {
                    resp.Status = "FAIL";
                    resp.Message = "Order Placed Failed";
                    resp.Error = "";
                }

                return Ok(resp);
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

        [NonAction]
        private long Order_Save(Final_PlaceOrderRequest ConfirmOrder, int UserID)
        {
            try
            {
                Database db = new Database(Request);
                List<System.Data.IDbDataParameter> para = new List<System.Data.IDbDataParameter>();

                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, UserID));

                para.Add(db.CreateParam("p_for_refno", DbType.String, ParameterDirection.Input, ConfirmOrder.StoneID, true));

                if (!string.IsNullOrEmpty(ConfirmOrder.Comments))
                    para.Add(db.CreateParam("p_for_comments", DbType.String, ParameterDirection.Input, ConfirmOrder.Comments));
                else
                    para.Add(db.CreateParam("p_for_comments", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("IPD_Confirm_Order_Web", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["STATUS"].ToString() == "Y")
                    {
                        return Convert.ToInt32(dt.Rows[0]["orderid"]);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [NonAction]
        private bool SendOrderMail(long OrderId, String Comments, int userid)
        {
            try
            {
                string lsToMail = "";

                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Clear();
                para.Add(db.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(userid)));
                DataTable dtUserDetail = db.ExecuteSP("UserMas_SelectOne", para.ToArray(), false);

                Database db1 = new Database(Request);
                para.Clear();
                para.Add(db1.CreateParam("sUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(userid)));
                DataTable dtToMailList = db1.ExecuteSP("UserMas_SelectEmailByUserId", para.ToArray(), false);

                foreach (DataRow row in dtToMailList.Rows)
                    lsToMail += row["sEmail"].ToString() + ",";

                if (lsToMail.Length > 0)
                    lsToMail = lsToMail.Remove(lsToMail.Length - 1);

                string Mobile = "";

                Mobile = String.IsNullOrEmpty(dtUserDetail.Rows[0]["sCompMobile"].ToString()) ? "" : dtUserDetail.Rows[0]["sCompMobile"].ToString() + ", ";
                Mobile += String.IsNullOrEmpty(dtUserDetail.Rows[0]["sCompMobile2"].ToString()) ? "" : dtUserDetail.Rows[0]["sCompMobile2"].ToString() + ", ";
                if (Mobile.Length > 1)
                    Mobile = Mobile.Remove(Mobile.Length - 2);

                /*****ANDROID AND IPHONE IN ORDER PLACED NOTIFICATION SEND IF CUSTOMER PLACED ORDER*************/
                if (dtUserDetail.Rows[0]["iUserType"].ToString() == "3")
                {
                    Database dbs = new Database(Request);
                    List<IDbDataParameter> paras;
                    paras = new List<IDbDataParameter>();

                    paras.Clear();
                    paras.Add(dbs.CreateParam("iUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(userid)));
                    DataTable dtToken = dbs.ExecuteSP("FCMToken_Get", paras.ToArray(), false);

                    string NotificationMsg = string.Empty;
                    if (dtUserDetail.Rows[0]["sCompName"] != null && dtUserDetail.Rows[0]["sCompName"].ToString() != "")
                    {
                        NotificationMsg = dtUserDetail.Rows[0]["sCompName"].ToString() + " has placed the order with " + OrderId + ".";
                    }

                    if (dtToken != null && dtToken.Rows.Count > 0)
                    {
                        for (int I = 0; I <= dtToken.Rows.Count - 1; I++)
                        {
                            string Token = Convert.ToString(dtToken.Rows[I]["Token"]);
                            string DeviceType = Convert.ToString(dtToken.Rows[I]["DeviceType"]);

                            PublishNotification(DeviceType.ToUpper(), Token, "Order Placed", NotificationMsg, "");
                        }
                    }
                }
                /************************************************************************************************/

                String sNotes = Comments;

                if (dtUserDetail.Rows[0]["sCompEmail"].ToString().Length > 0 || dtUserDetail.Rows[0]["sEmail"].ToString().Length > 0)
                {
                    string custMail = "";

                    if (dtUserDetail.Rows[0]["sCompEmail"].ToString() == "" || dtUserDetail.Rows[0]["sCompEmail"].ToString() == null)
                    {
                        custMail = dtUserDetail.Rows[0]["sEmail"].ToString();
                        if (dtUserDetail.Rows[0]["sEmailPersonal"].ToString() != "" && dtUserDetail.Rows[0]["sEmailPersonal"].ToString() != null)
                            custMail += "," + dtUserDetail.Rows[0]["sEmailPersonal"];
                    }
                    else
                    {
                        custMail = dtUserDetail.Rows[0]["sCompEmail"].ToString();
                        if (dtUserDetail.Rows[0]["sCompEmail2"].ToString() != "" && dtUserDetail.Rows[0]["sCompEmail2"].ToString() != null)
                            custMail += "," + dtUserDetail.Rows[0]["sCompEmail2"];
                    }

                    custMail = custMail.Trim(',');
                    string Adminemail = null;

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
                                Adminemail += DT_AdminLst.Rows[I]["sCompEmail"].ToString() + ",";
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(Adminemail))
                    {
                        Adminemail = Adminemail.Remove(Adminemail.Length - 1);
                    }

                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ORDER_ADMIN_EMAILID"]))
                        Adminemail += ConfigurationManager.AppSettings["ORDER_ADMIN_EMAILID"];

                    DAL.Common.InsertErrorLog(null, "OrderEmail AssistByMail: " + Convert.ToString(userid) + ", " + custMail + ", " + OrderId.ToString() + ", " + DateTime.Now.ToString() + ", " + dtUserDetail.Rows[0]["sFirstName"].ToString() + " " + dtUserDetail.Rows[0]["sLastName"].ToString() + ", " +
                        dtUserDetail.Rows[0]["sCompAddress"] + ((String.IsNullOrEmpty(dtUserDetail.Rows[0]["sCompAddress2"].ToString()) ? "" : ", ") + dtUserDetail.Rows[0]["sCompAddress2"].ToString()) + ", " + dtUserDetail.Rows[0]["sCity"].ToString() + ", " +
                        dtUserDetail.Rows[0]["sPhone"].ToString() + ", " + dtUserDetail.Rows[0]["sMobile"].ToString() + ", " + dtUserDetail.Rows[0]["sCompEmail"].ToString() + ", " + sNotes + ", " + ConfigurationManager.AppSettings["BCCEmail"] + ", " + Adminemail, Request);

                    try
                    {
                        IPadCommon.EmailNewOrder(Convert.ToInt32(userid), custMail, OrderId, DateTime.Now, 1, dtUserDetail.Rows[0]["sFirstName"] + " " + dtUserDetail.Rows[0]["sLastName"],
                            dtUserDetail.Rows[0]["sCompAddress"] + ((String.IsNullOrEmpty(dtUserDetail.Rows[0]["sCompAddress2"].ToString()) ? "" : ", ") + dtUserDetail.Rows[0]["sCompAddress2"].ToString()) + ", " + dtUserDetail.Rows[0]["sCity"].ToString(),
                            dtUserDetail.Rows[0]["sPhone"].ToString(), dtUserDetail.Rows[0]["sMobile"].ToString(), dtUserDetail.Rows[0]["sCompEmail"].ToString(), sNotes, null, ConfigurationManager.AppSettings["BCCEmail"], Adminemail);
                    }
                    catch (Exception ex)
                    {
                        DAL.Common.InsertErrorLog(ex, "EMAIL ERROR", Request);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        #endregion

        #region HOLD AND RELEASE STONE FOR HOLD MODULE

        [HttpPost]
        public IHttpActionResult HoldStone_1([FromBody]JObject data)
        {
            HoldStoneRequest_1 holdstonerequest = new HoldStoneRequest_1();
            try
            {
                holdstonerequest = JsonConvert.DeserializeObject<HoldStoneRequest_1>(data.ToString());
                holdstonerequest.IsFromAPI = false;
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
                int userID, LoginUser;
                LoginUser = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                if (holdstonerequest.Userid == 0)
                {
                    userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                }
                else
                {
                    userID = holdstonerequest.Userid;
                }
                int transID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == Lib.Constants.ServiceConstants.SessionTransID).FirstOrDefault().Value);

                CommonResponse resp = new CommonResponse();
                DAL.Usermas objUser = new DAL.Usermas();
                DataTable dtUserDetail = objUser.UserMas_SelectOne(Convert.ToInt64(userID));

                long HoldId = 0;
                String CustomerName = GetPartyNameByUserId(Convert.ToInt32(userID));
                Int32 AssistByID = GetAssistByUserId(Convert.ToInt32(userID));
                string user_name = dtUserDetail.Rows[0]["sUsername"].ToString();
                string device_type = dtUserDetail.Rows[0]["device_type"].ToString();
                int admin = Convert.ToInt32(dtUserDetail.Rows[0]["isadmin"].ToString());
                int emp = Convert.ToInt32(dtUserDetail.Rows[0]["isemp"].ToString());
                string FortunePartyCode = dtUserDetail.Rows[0]["FortunePartyCode"].ToString();

                CommonResponse resHold = MakeWeb_Hold_Release_1(LoginUser, holdstonerequest.StoneID, holdstonerequest.Hold_Stone_List, holdstonerequest.UnHold_Stone_List, userID, CustomerName, holdstonerequest.Comments, ref HoldId, transID.ToString(), user_name, device_type, admin, emp, AssistByID, FortunePartyCode, holdstonerequest.IsAdminEmp_Hold, holdstonerequest.IsFromAPI, holdstonerequest.Userid);
                if (resHold.Status == "SUCCESS")
                {
                    Send_HoldRelease_StoneMail("HOLD", holdstonerequest.StoneID, holdstonerequest.Comments, userID, LoginUser, holdstonerequest.Userid);

                    resp.Status = "SUCCESS";
                    resp.Message = resHold.Message;
                    resp.Error = "";
                }
                else
                {
                    resp.Status = "FAIL";
                    resp.Message = resHold.Message;
                    resp.Error = "";
                }
                return Ok(resp);
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
        [NonAction]
        private CommonResponse MakeWeb_Hold_Release_1(int LoginUserid, String StoneID, List<Hold_Stone_List> Hold_StoneID, List<UnHold_Stone_List> UnHold_StoneID, int UserID, String CustomerName, String Comments, ref long HoldId, String TokenNo, String user_name, String Device_Type, int admin, int emp, int AssistByID, string FortunePartyCode, bool IsAdminEmp_Hold, bool IsFromAPI, int HoldCompany)
        {
            try
            {
                int ReleasedSuccessCount = 0;
                string AssistDetail = string.Empty;
                bool HoldStone_Without_Comp = false, UnHoldStone_Without_Comp = false;
                if (Hold_StoneID.Count() > 0 && IsAdminEmp_Hold == false && (admin == 1 || emp == 1))
                {
                    HoldStone_Without_Comp = true;
                }
                else if (UnHold_StoneID.Count() > 0 && IsAdminEmp_Hold == false && (admin == 1 || emp == 1))
                {
                    UnHoldStone_Without_Comp = true;
                }

                CommonResponse resp = new CommonResponse();
                System.Data.DataTable dtHold;
                FortuneService.CommonResultResponse dtHold_StoneID;
                DateTime dDate = DateTime.Now;
                FortuneService.ServiceSoapClient wbService = new FortuneService.ServiceSoapClient();
                HoldStoneRequest_1 obj = new HoldStoneRequest_1();
                obj.StoneID = StoneID;
                obj.Hold_Stone_List = Hold_StoneID;
                obj.UnHold_Stone_List = UnHold_StoneID;
                obj.Comments = Comments;
                obj.Userid = UserID;
                obj.IsAdminEmp_Hold = IsAdminEmp_Hold;
                obj.IsFromAPI = IsFromAPI;
                obj.LoginUserid = LoginUserid;

                DAL.Order objOrder = new DAL.Order();

                if (Hold_StoneID.Count() > 0)
                {
                    for (int i = 0; i < Hold_StoneID.Count(); i++)
                    {
                        string h_stoneno = string.Empty, h_fortunecode = string.Empty, vhold_by = string.Empty, Status = string.Empty;

                        h_stoneno = Hold_StoneID[i].sRefNo.ToString();
                        h_fortunecode = Hold_StoneID[i].Hold_Party_Code.ToString();
                        vhold_by = Hold_StoneID[i].Hold_CompName.ToString();
                        Status = Hold_StoneID[i].Status.ToString();

                        if (Hold_StoneID[i].Hold_Party_Code != "0")
                        {
                            dtHold_StoneID = wbService.MakeWebReleaseTrans(Device_Type.ToString(), h_fortunecode, CustomerName, h_stoneno, Status);
                            if (dtHold_StoneID.Status == "SUCCESS")
                            {
                                ReleasedSuccessCount = ReleasedSuccessCount + 1;
                            }
                        }
                        else if (Hold_StoneID[i].Hold_Party_Code == "0")
                        {
                            dtHold_StoneID = wbService.MakeWebReleaseTrans(Device_Type.ToString(), "", vhold_by, h_stoneno, Status);
                            if (dtHold_StoneID.Status == "SUCCESS")
                            {
                                ReleasedSuccessCount = ReleasedSuccessCount + 1;
                            }
                        }
                    }
                    if (Hold_StoneID.Count() != ReleasedSuccessCount)
                    {
                        resp.Status = "FAIL";
                        resp.Message = "Hold Stone(s) are fail in Release";
                        resp.Error = "";
                        return resp;
                    }
                }

                List<FortuneService.HoldStone> pStoneList = new List<FortuneService.HoldStone>();
                string vds = "N";
                List<String> stoneList = new List<String>(StoneID.Split(','));
                if (stoneList.Count > 0)
                {
                    for (int i = 0; i < stoneList.Count(); i++)
                    {
                        string REF_NO = stoneList[i];

                        System.Data.DataTable dtDmd = GetDiamondDetailInner(REF_NO.ToString(), UserID.ToString(), TokenNo);
                        if (dtDmd.Rows.Count > 0)
                        {
                            DAL.Stock objstock = new DAL.Stock();
                            DataTable result = objstock.Stock_SelectOne(Convert.ToString(REF_NO), Convert.ToInt32(UserID));
                            if (result.Rows.Count > 0)
                            {
                                DataRow loStockSelStne = result.Rows[0];
                                if (loStockSelStne["dDisc"].ToString() != loStockSelStne["OrgDisc"].ToString())
                                    vds = "Y";
                                System.Data.DataRow drDmd = dtDmd.Rows[0];
                                FortuneService.HoldStone hs = new FortuneService.HoldStone();
                                hs.ref_no = drDmd["STONE_REF_NO"].ToString();
                                hs.cur_cts = Convert.ToDecimal(drDmd["CTS"]);

                                string CUR_RAP_RATE = (drDmd["CUR_RAP_RATE"].ToString() != "" && drDmd["CUR_RAP_RATE"].ToString() != null ? drDmd["CUR_RAP_RATE"].ToString() : "0");
                                string RAP_AMOUNT = (drDmd["RAP_AMOUNT"].ToString() != "" && drDmd["RAP_AMOUNT"].ToString() != null ? drDmd["RAP_AMOUNT"].ToString() : "0");
                                string SALES_DISC_PER = (drDmd["SALES_DISC_PER"].ToString() != "" && drDmd["SALES_DISC_PER"].ToString() != null ? drDmd["SALES_DISC_PER"].ToString() : "0");
                                string dDisc = (loStockSelStne["dDisc"].ToString() != "" && loStockSelStne["dDisc"].ToString() != null ? loStockSelStne["dDisc"].ToString() : "0");

                                hs.rap_price = Convert.ToDecimal(CUR_RAP_RATE);
                                hs.rap_value = Convert.ToDecimal(RAP_AMOUNT);
                                hs.sales_disc_per = Convert.ToDecimal(SALES_DISC_PER);
                                hs.disc_per = Convert.ToDecimal(dDisc);
                                pStoneList.Add(hs);
                            }
                        }
                    }
                }
                else
                {
                    resp.Status = "FAIL";
                    resp.Message = "Stone(s) Hold has been Failed";
                    resp.Error = "";
                    return resp;
                }

                if (pStoneList.Count() == stoneList.Count)
                {
                    FortuneService.CommonResultResponse cResult;
                    if (admin.ToString() == "1")
                    {
                        //AssistByID = 10; //jignesh user remove and samit add
                        AssistByID = 5682;
                        CustomerName = Comments;
                    }
                    else if (emp.ToString() == "1")
                    {
                        AssistByID = Convert.ToInt32(LoginUserid);
                        CustomerName = Comments;
                    }

                    try
                    {
                        if (Hold_StoneID.Count() > 0)
                        {
                            cResult = wbService.MakeWebHoldTrans(Device_Type, AssistByID.ToString(), (HoldStone_Without_Comp == true ? Comments : CustomerName), Comments, "Y", pStoneList.ToArray(), (HoldStone_Without_Comp == true ? "" : FortunePartyCode));
                        }
                        else
                        {
                            cResult = wbService.MakeWebHoldTrans(Device_Type, AssistByID.ToString(), (UnHoldStone_Without_Comp == true ? Comments : CustomerName), Comments, "Y", pStoneList.ToArray(), (UnHoldStone_Without_Comp == true ? "" : FortunePartyCode));
                        }
                    }
                    catch (Exception ex)
                    {
                        resp.Status = "FAIL";
                        resp.Message = "Stone(s) Hold has been Failed";
                        resp.Error = "";
                        return resp;
                    }

                    if (cResult.Status == "SUCCESS")
                    {
                        HoldId = HoldDet_Save(obj, Convert.ToInt32(LoginUserid), HoldCompany);
                        if (HoldId <= 0)
                        {
                            resp.Status = "FAIL";
                            resp.Message = "No Record will be Proceed";
                            resp.Error = "";
                            return resp;
                        }
                        else
                        {
                            resp.Status = "SUCCESS";
                            resp.Message = "Stone(s) Hold Successfully";
                            resp.Error = "";
                            return resp;
                        }
                    }
                    else
                    {
                        resp.Status = "FAIL";
                        resp.Message = "Stone(s) Hold has been Failed";
                        resp.Error = "";
                        return resp;
                    }
                }
                else
                {
                    resp.Status = "FAIL";
                    resp.Message = "Stone(s) Hold has been Failed";
                    resp.Error = "";
                    return resp;
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [NonAction]
        private long HoldDet_Save(HoldStoneRequest_1 HolDet, int UserID, int HoldCompany)
        {
            try
            {
                string h_stoneno = string.Empty;
                if (HolDet.Hold_Stone_List.Count() > 0)
                {
                    for (int i = 0; i < HolDet.Hold_Stone_List.Count(); i++)
                    {
                        h_stoneno += HolDet.Hold_Stone_List[i].sRefNo.ToString() + ',';
                    }
                    h_stoneno = h_stoneno.Remove(h_stoneno.Length - 1);
                }

                Database db = new Database(Request);
                List<System.Data.IDbDataParameter> para = new List<System.Data.IDbDataParameter>();

                if (HoldCompany > 0)
                    para.Add(db.CreateParam("p_for_HoldCompany", DbType.Int32, ParameterDirection.Input, HoldCompany));
                else
                    para.Add(db.CreateParam("p_for_HoldCompany", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, UserID));

                para.Add(db.CreateParam("p_for_refno", DbType.String, ParameterDirection.Input, HolDet.StoneID, true));

                if (!string.IsNullOrEmpty(h_stoneno))
                    para.Add(db.CreateParam("p_for_hold_refno", DbType.String, ParameterDirection.Input, h_stoneno));
                else
                    para.Add(db.CreateParam("p_for_hold_refno", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(HolDet.Comments))
                    para.Add(db.CreateParam("p_for_comments", DbType.String, ParameterDirection.Input, HolDet.Comments));
                else
                    para.Add(db.CreateParam("p_for_comments", DbType.String, ParameterDirection.Input, DBNull.Value));

                int AdminEmpHold_userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                if (HolDet.IsAdminEmp_Hold == true)
                    para.Add(db.CreateParam("p_for_AdminEmpHold_UserId", DbType.Int32, ParameterDirection.Input, AdminEmpHold_userID));
                else
                    para.Add(db.CreateParam("p_for_AdminEmpHold_UserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (HolDet.Hold_Stone_List.Count() > 0 || HolDet.UnHold_Stone_List.Count() > 0)
                {
                    if (HolDet.IsAdminEmp_Hold == false)
                        para.Add(db.CreateParam("p_for_Hold_Without_Company", DbType.Boolean, ParameterDirection.Input, true));
                    else
                        para.Add(db.CreateParam("p_for_Hold_Without_Company", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                else
                {
                    para.Add(db.CreateParam("p_for_Hold_Without_Company", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }

                if (HolDet.IsFromAPI == true)
                    para.Add(db.CreateParam("p_for_IsFromAPI", DbType.Boolean, ParameterDirection.Input, true));
                else
                    para.Add(db.CreateParam("p_for_IsFromAPI", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (HolDet.UnHold_Stone_List.Count() > 0)
                    para.Add(db.CreateParam("p_for_UnHold_Stone_List", DbType.Boolean, ParameterDirection.Input, true));
                else
                    para.Add(db.CreateParam("p_for_UnHold_Stone_List", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("HoldDet_Save", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "1" && dt.Rows[0]["Message"].ToString() == "Success")
                {
                    return Convert.ToInt64(dt.Rows[0]["HoldId"] != null || dt.Rows[0]["HoldId"].ToString() != "" ? dt.Rows[0]["HoldId"].ToString() : "0");
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        #endregion

        #region ONLY RELEASE STONE FOR RELEASE MODULE

        [HttpPost]
        public IHttpActionResult ReleaseStone_1([FromBody]JObject data)
        {
            HoldStoneRequest_1 releasestonerequest = new HoldStoneRequest_1();
            try
            {
                releasestonerequest = JsonConvert.DeserializeObject<HoldStoneRequest_1>(data.ToString());
                releasestonerequest.IsFromAPI = false;
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
                long ReleaseId = 0;
                int LoginUser = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                int transID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == Lib.Constants.ServiceConstants.SessionTransID).FirstOrDefault().Value);

                DAL.Usermas objUser = new DAL.Usermas();
                DataTable dtUserDetail = objUser.UserMas_SelectOne(Convert.ToInt64(LoginUser));
                string device_type = dtUserDetail.Rows[0]["device_type"].ToString();
                String CustomerName = GetPartyNameByUserId(Convert.ToInt32(LoginUser));

                CommonResponse resp = new CommonResponse();

                CommonResponse resHold = MakeWeb_Release_1(LoginUser, releasestonerequest.StoneID, releasestonerequest.Hold_Stone_List, device_type, CustomerName, ref ReleaseId);
                if (resHold.Status == "SUCCESS")
                {
                    Send_HoldRelease_StoneMail("RELEASE", releasestonerequest.StoneID, releasestonerequest.Comments, LoginUser, LoginUser, releasestonerequest.Userid);

                    resp.Status = "SUCCESS";
                    resp.Message = resHold.Message;
                    resp.Error = "";
                }
                else
                {
                    resp.Status = "FAIL";
                    resp.Message = resHold.Message;
                    resp.Error = "";
                }
                return Ok(resp);
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
        [NonAction]
        private CommonResponse MakeWeb_Release_1(int LoginUserid, String StoneID, List<Hold_Stone_List> Hold_StoneID, string Device_Type, string CustomerName, ref long ReleaseId)
        {
            try
            {
                int ReleasedSuccessCount = 0;
                CommonResponse resp = new CommonResponse();
                System.Data.DataTable dtHold;
                FortuneService.CommonResultResponse dtHold_StoneID;
                FortuneService.ServiceSoapClient wbService = new FortuneService.ServiceSoapClient();

                HoldStoneRequest_1 obj = new HoldStoneRequest_1();
                obj.StoneID = StoneID;
                obj.Hold_Stone_List = Hold_StoneID;
                obj.LoginUserid = LoginUserid;

                DAL.Order objOrder = new DAL.Order();

                if (Hold_StoneID.Count() > 0)
                {
                    for (int i = 0; i < Hold_StoneID.Count(); i++)
                    {
                        string h_stoneno = string.Empty, h_fortunecode = string.Empty, vhold_by = string.Empty, Status = string.Empty;

                        h_stoneno = Hold_StoneID[i].sRefNo.ToString();
                        h_fortunecode = Hold_StoneID[i].Hold_Party_Code.ToString();
                        vhold_by = Hold_StoneID[i].Hold_CompName.ToString();
                        Status = Hold_StoneID[i].Status.ToString();

                        if (Hold_StoneID[i].Hold_Party_Code != "0")
                        {
                            dtHold_StoneID = wbService.MakeWebReleaseTrans(Device_Type.ToString(), h_fortunecode, CustomerName, h_stoneno, Status);
                            if (dtHold_StoneID.Status == "SUCCESS")
                            {
                                ReleasedSuccessCount = ReleasedSuccessCount + 1;
                            }
                        }
                        else if (Hold_StoneID[i].Hold_Party_Code == "0")
                        {
                            dtHold_StoneID = wbService.MakeWebReleaseTrans(Device_Type.ToString(), "", vhold_by, h_stoneno, Status);
                            if (dtHold_StoneID.Status == "SUCCESS")
                            {
                                ReleasedSuccessCount = ReleasedSuccessCount + 1;
                            }
                        }
                    }
                    if (Hold_StoneID.Count() == ReleasedSuccessCount)
                    {
                        ReleaseId = ReleaseDet_Save(obj, LoginUserid);
                        if (ReleaseId <= 0)
                        {
                            resp.Status = "FAIL";
                            resp.Message = "Hold Stone(s) are fail in Release";
                            resp.Error = "";
                            return resp;
                        }
                        else
                        {
                            resp.Status = "SUCCESS";
                            resp.Message = "Stone(s) Release Successfully";
                            resp.Error = "";
                            return resp;
                        }
                    }
                    else
                    {
                        resp.Status = "FAIL";
                        resp.Message = "Hold Stone(s) are fail in Release";
                        resp.Error = "";
                        return resp;
                    }
                }
                else
                {
                    resp.Status = "FAIL";
                    resp.Message = "Hold Stone(s) are fail in Release";
                    resp.Error = "";
                    return resp;
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [NonAction]
        private long ReleaseDet_Save(HoldStoneRequest_1 RelDet, int UserID)
        {
            try
            {
                string h_stoneno = string.Empty;
                if (RelDet.Hold_Stone_List.Count() > 0)
                {
                    for (int i = 0; i < RelDet.Hold_Stone_List.Count(); i++)
                    {
                        h_stoneno += RelDet.Hold_Stone_List[i].sRefNo.ToString() + ',';
                    }
                    h_stoneno = h_stoneno.Remove(h_stoneno.Length - 1);
                }

                Database db = new Database(Request);
                List<System.Data.IDbDataParameter> para = new List<System.Data.IDbDataParameter>();

                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, UserID));

                para.Add(db.CreateParam("p_for_refno", DbType.String, ParameterDirection.Input, RelDet.StoneID, true));

                if (!string.IsNullOrEmpty(h_stoneno))
                    para.Add(db.CreateParam("p_for_hold_refno", DbType.String, ParameterDirection.Input, h_stoneno));
                else
                    para.Add(db.CreateParam("p_for_hold_refno", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ReleaseDet_Save", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "1" && dt.Rows[0]["Message"].ToString() == "Success")
                {
                    return Convert.ToInt64(dt.Rows[0]["ReleaseId"] != null || dt.Rows[0]["ReleaseId"].ToString() != "" ? dt.Rows[0]["ReleaseId"].ToString() : "0");
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        #endregion

        [HttpPost]
        public IHttpActionResult GetHoldHistory([FromBody]JObject data)
        {
            OrderHistoryRequest holdhistoryrequest = new OrderHistoryRequest();
            try
            {
                holdhistoryrequest = JsonConvert.DeserializeObject<OrderHistoryRequest>(data.ToString());
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

                DataTable dtData = HoldHistoryInner(holdhistoryrequest, userID);

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
        [NonAction]
        private DataTable HoldHistoryInner(OrderHistoryRequest holdhistoryrequest, int userID)
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("p_for_usercode", DbType.String, ParameterDirection.Input, userID));

                if (!string.IsNullOrEmpty(holdhistoryrequest.PageNo))
                    para.Add(db.CreateParam("p_for_page", DbType.Int16, ParameterDirection.Input, Convert.ToInt16(holdhistoryrequest.PageNo)));
                else
                    para.Add(db.CreateParam("p_for_page", DbType.Int16, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(holdhistoryrequest.OrderBy))
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, (holdhistoryrequest.OrderBy)));
                else
                    para.Add(db.CreateParam("sOrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(holdhistoryrequest.RefNo))
                    para.Add(db.CreateParam("RefNoCertiNo", DbType.String, ParameterDirection.Input, (holdhistoryrequest.RefNo)));
                else
                    para.Add(db.CreateParam("RefNoCertiNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(holdhistoryrequest.CommonName))
                    para.Add(db.CreateParam("PartyNameAssistByPartyCode", DbType.String, ParameterDirection.Input, (holdhistoryrequest.CommonName)));
                else
                    para.Add(db.CreateParam("PartyNameAssistByPartyCode", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("iPgSize", DbType.String, ParameterDirection.Input, (holdhistoryrequest.PgSize)));

                DataTable dt = db.ExecuteSP("Hold_Stone_Get", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
