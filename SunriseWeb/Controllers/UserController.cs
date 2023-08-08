using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Filter;
using SunriseWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class UserController : BaseController
    {
        API _api = new API();
        
        // GET: User
        public ActionResult Manage()
        {
            return View();
        }
        public ActionResult ErrorLog()
        {
            return View();
        }
        public ActionResult PacketTrace()
        {
            return View();
        }
        public ActionResult Notify()
        {
            return View();
        }
        public ActionResult UserMgt()
        {
            return View();
        }
        public JsonResult PacketTraceGetList(string StockId, string CertiNo)
        {
            var input = new
            {
                StockId = StockId,
                CertiNo = CertiNo
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.PacketTraceGetList, inputJson);
            ServiceResponse<PacketTrace_Response> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PacketTrace_Response>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NotifyList(string SearchList, string NotifyId, string PageNo, string PageSize, string OrderBy)
        {
            var input = new
            {
                SearchList = SearchList,
                NotifyId = NotifyId,
                PageNo = PageNo,
                PageSize = PageSize,
                OrderBy = OrderBy
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.NotifyList, inputJson);
            ServiceResponse<NotifyResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<NotifyResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveNotifyList(NotifySaveRequest notifysaverequest)
        {
            if (notifysaverequest != null)
            {
                CommonResponse _data = new CommonResponse();
                string inputJson = (new JavaScriptSerializer()).Serialize(notifysaverequest);
                string _response = _api.CallAPI(Constants.NotifySave, inputJson);
                _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
                return Json(_data); 
            }
            return Json("");
        }
        public ActionResult NotifyDet()
        {
            return View();
        }
        public JsonResult NotifyDetList(string FromDate, string ToDate, string PageNo, string PageSize, string OrderBy)
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                PageNo = PageNo,
                PageSize = PageSize,
                OrderBy = OrderBy
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.NotifyDetList, inputJson);
            ServiceResponse<NotifyDetResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<NotifyDetResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NotifyGet_User(string iUserid)
        {
            var input = new
            {
                iUserid = iUserid
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.NotifyGet_User, inputJson);
            ServiceResponse<NotifyGet_UserResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<NotifyGet_UserResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult News()
        {
            return View();
        }
        public JsonResult NewsMaster(string FromDate, string ToDate, string Description, string FontColor, int iID, string Flag)
        {
            
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                Description = Description,
                FontColor = FontColor,
                iID = iID,
                Flag = Flag
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.NewsMst, inputJson);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            ServiceResponse<NewsMst> _data = serializer.Deserialize<ServiceResponse<NewsMst>>(_response);
            return new JsonResult()
            {
                ContentEncoding = Encoding.Default,
                ContentType = "application/json",
                Data = _data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        public JsonResult ErrorLogMst(string FromDate, string ToDate, string MSearch, int PageNo, int PageSize, string OrderBy)
        {
            var input = new
            {
                FromDate = FromDate,
                ToDate = ToDate,
                MSearch = MSearch,
                PageNo = PageNo,
                PageSize = PageSize,
                OrderBy = OrderBy
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.ErrorLogMst, inputJson);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            ServiceResponse<ErrorLgResponse> _data = serializer.Deserialize<ServiceResponse<ErrorLgResponse>>(_response);
            return new JsonResult()
            {
                ContentEncoding = Encoding.Default,
                ContentType = "application/json",
                Data = _data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        public JsonResult GetUsers(string CompanyName, string CountryName, string UserName, string UserFullName, string UserType, string UserStatus, string PageNo, string IsEmployee = "0", string SortColumn = "", string SortDirection = "", bool PrimaryUser = false, string UserID = "", string FilterType = "", string FromDate = "", string ToDate = "", string FortunePartyCode = "")
        {
            var input = new
            {
                CompanyName = CompanyName,
                CountryName = CountryName,
                UserName = UserName,
                UserFullName = UserFullName,
                UserType = UserType,
                UserStatus = UserStatus,
                PageNo= PageNo,
                IsEmployee= IsEmployee,
                SortColumn= SortColumn,
                SortDirection= SortDirection,
                PrimaryUser = PrimaryUser,
                UserID = UserID,
                FilterType = FilterType,
                FromDate = FromDate,
                ToDate = ToDate,
                FortunePartyCode = FortunePartyCode
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetUserList, inputJson);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            ServiceResponse<UserListResponse> _data = serializer.Deserialize<ServiceResponse<UserListResponse>>(_response);
            //ServiceResponse<UserListResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserListResponse>>(_response);

            //return Json(_data, JsonRequestBehavior.AllowGet);
            return new JsonResult()
            {
                //ContentEncoding = Encoding.Default,
                //ContentType = "application/json",
                Data = _data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }

        public JsonResult Delete(int UserID)
        {
            var input = new
            {
                UserID = UserID
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DeleteUser, inputJson);
            ServiceResponse<UserListResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserListResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DownloadUser(string CompanyName, string CountryName, string UserName, string UserFullName, string UserType, string UserStatus, string FormName, string ActivityType, string IsEmployee = "0", bool PrimaryUser = false, string FilterType = "", string FromDate = "", string ToDate = "", string FortunePartyCode = "")
        {
            var input = new
            {
                CompanyName = CompanyName,
                CountryName = CountryName,
                UserName = UserName,
                UserFullName = UserFullName,
                UserType = UserType,
                UserStatus = UserStatus,
                FormName = FormName,
                ActivityType = ActivityType,
                IsEmployee = IsEmployee,
                PrimaryUser = PrimaryUser,
                FilterType = FilterType,
                FromDate = FromDate,
                ToDate = ToDate,
                FortunePartyCode = FortunePartyCode
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.DownloadUser, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string UserType, int UserID, string UserName)
        {
            var input = new
            {
                UserID = UserID,
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPI(Constants.GetUserList, inputJson);
            ServiceResponse<UserListResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserListResponse>>(_response);
            UserListResponse userResponse = _data.Data.FirstOrDefault();

            UserDetailsModel user = new UserDetailsModel
            {
                UserName = userResponse.sUsername,
                Password = userResponse.sPassword,
                FirstName = userResponse.sFirstName,
                LastName = userResponse.sLastName,
                OtherName = userResponse.sOtherName,
                IsActive = userResponse.bIsActive,
                Suspended = userResponse.Suspended,
                IsCompanyUser = userResponse.bIsCompUser,
                EmpID1 = userResponse.iEmpId,
                EmpID2 = userResponse.iEmpId2,
                StockType = userResponse.sStockCategory,
                PassportId = userResponse.sPassportId,
                HkId = userResponse.sHkId,
                CompanyName = userResponse.sCompName,
                CompanyAddress = userResponse.sCompAddress,
                CompanyAddress2 = userResponse.sCompAddress2,
                CompCity = userResponse.sCompCity,
                CompZipCode = userResponse.sCompZipcode,
                CompCountry = userResponse.sCompCountry,
                CompMobile = userResponse.sCompMobile,
                CompMobile2 = userResponse.sCompMobile2,
                CompPhone = userResponse.sCompPhone,
                CompPhone2 = userResponse.sCompPhone2,
                CompFaxNo = userResponse.sCompFaxNo,
                CompEmail = userResponse.sCompEmail,
                CompEmail2 = userResponse.scompemail2,
                RapnetID = userResponse.sRapNetId,
                CompRegNo = userResponse.sCompRegNo,
                WeChatId = userResponse.sWeChatId,
                SkypeId = userResponse.sSkypeId,
                Website = userResponse.sWebsite,
                UserType = userResponse.sUserType,
                iCompCityId = userResponse.iCompCityId,
                iCompCountryId = userResponse.iCompCountryId,
                UserID = userResponse.iUserid,
                FortunePartyCode = userResponse.FortunePartyCode,
                DBA = userResponse.DBA,
                Remark = userResponse.Remark,
                IsPrimary = userResponse.IsPrimary,
                SupplierIdLst = userResponse.SupplierId,
                MessageId = userResponse.MessageId,
                OrderApproved = userResponse.OrderApproved
            };

            var input1 = new
            {
                SortColumn = "sFullName",
                SortDirection = "asc",
                UserType = 2//UserType.ToLower()=="admin"?1:UserType.ToLower() == "employee"?2: UserType.ToLower() == "employee"?3:0,
            };


            string inputJson1 = (new JavaScriptSerializer()).Serialize(input1);
            string _response1 = _api.CallAPI(Constants.GetUserList, inputJson1);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            ServiceResponse<UserListResponse> _data1 = serializer.Deserialize<ServiceResponse<UserListResponse>>(_response1);

            if (user.EmpID1 > 0)
            {
                user.EmpList1 = new SelectList(_data1.Data, "iUserid", "sFullName", user.EmpID1);
            }
            else
            {
                user.EmpList1 = new SelectList(_data1.Data, "iUserid", "sFullName");
            }

            if (user.EmpID2 > 0)
            {
                user.EmpList2 = new SelectList(_data1.Data, "iUserid", "sFullName", user.EmpID2);
            }
            else
            {
                user.EmpList2 = new SelectList(_data1.Data, "iUserid", "sFullName");
            }

            var _input2 = new
            {
                iPgNo = "1",
                iPgSize = "1000",
                IsActive = true
            };
            string _inputJson2 = (new JavaScriptSerializer()).Serialize(_input2);
            string _response2 = _api.CallAPI(Constants.Get_MessageMst, _inputJson2);
            ServiceResponse<MessageMstSelect_Response> _data2 = (new JavaScriptSerializer()).Deserialize<ServiceResponse<MessageMstSelect_Response>>(_response2);

            if (user.MessageId > 0)
            {
                user.MessageList = new SelectList(_data2.Data, "Id", "MessageName", user.MessageId);
            }
            else
            {
                user.MessageList = new SelectList(_data2.Data, "Id", "MessageName");
            }

            var StockTypelst = new[]
            {
                    new{ Value = "T" , Text = "Total Stock" },
                    new{ Value = "O" , Text = "Office Stock" },
                    new{ Value = "OC", Text = "Office Stock with Consignment" },
                    new{ Value = "OH", Text = "Office Stock with Hold" }
            };
            var input_V = new
            {
                PartyName = "",
                ContactPerson = "",
                PartyPrefix = "",
                CountryId = 0,
                PageNo = 0,
                PageSize = 0,
                OrderBy = 0
            };
            string inputJson_V = (new JavaScriptSerializer()).Serialize(input_V);

            user.StockTypeList = new SelectList(StockTypelst, "Value", "Text", user.StockType);

            string _response_V = _api.CallAPI(Constants.PartyInfo, inputJson_V);
            ServiceResponse<PartyInfoResponse> _data_V = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PartyInfoResponse>>(_response_V);
            List<SelectListItem> columns_V = new List<SelectListItem>();
            foreach (var item in _data_V.Data)
            {
                columns_V.Add(new SelectListItem
                {
                    Text = item.sPartyName,
                    Value = item.sPartyName
                });
            }
            if (!string.IsNullOrEmpty(user.SupplierId))
            {
                user.SupplierList = new SelectList(_data_V.Data, "id", "sPartyName", user.SupplierId);
            }
            else
            {
                user.SupplierList = new SelectList(_data_V.Data, "id", "sPartyName");
            }

            return View(user);
        }

        public ActionResult Add()
        {
            UserDetailsModel user = new UserDetailsModel();
            var input_V = new
            {
                PartyName = "",
                ContactPerson = "",
                PartyPrefix = "",
                CountryId = 0,
                PageNo = 0,
                PageSize = 0,
                OrderBy = 0
            };
            string inputJson_V = (new JavaScriptSerializer()).Serialize(input_V);
            string _response_V = _api.CallAPI(Constants.PartyInfo, inputJson_V);
            ServiceResponse<PartyInfoResponse> _data_V = (new JavaScriptSerializer()).Deserialize<ServiceResponse<PartyInfoResponse>>(_response_V);
            List<SelectListItem> columns_V = new List<SelectListItem>();
            foreach (var item in _data_V.Data)
            {
                columns_V.Add(new SelectListItem
                {
                    Text = item.sPartyName,
                    Value = item.sPartyName
                });
            }
            if (!string.IsNullOrEmpty(user.SupplierId))
            {
                user.SupplierList = new SelectList(_data_V.Data, "id", "sPartyName", user.SupplierId);
            }
            else
            {
                user.SupplierList = new SelectList(_data_V.Data, "id", "sPartyName");
            }

            return View(user);
        }

        public JsonResult SaveUserData(UserDetails user)
        {
            if (HttpContext.Request.Cookies["language"] != null)
            {
                user.Lang = HttpContext.Request.Cookies["language"].Value;
            }
            string inputJson = (new JavaScriptSerializer()).Serialize(user);
            string _response = _api.CallAPI(Constants.AddUser, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUserData(UserDetails user)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(user);
            string _response = _api.CallAPI(Constants.UpdateUser, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUserList(UserListSearchRequest _obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(_obj);
            string _response = _api.CallAPI(Constants.GetUserListSearch, inputJson);
            ServiceResponse<UserListSearchResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserListSearchResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DownloadUserList(UserListSearchRequest _obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(_obj);
            string _response = _api.CallAPI(Constants.DownloadUserList, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrderDisc()
        {
            return View();
        }
        [HttpPost]
        public JsonResult OrderDisc_InsUpd(OrderDisc_InsUpd _obj)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(_obj);
            string _response = _api.CallAPI(Constants.OrderDisc_InsUpd, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OrderDisc_Select()
        {
            string _response = _api.CallAPI(Constants.OrderDisc_Select, "");
            ServiceResponse<OrderDisc_Select> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<OrderDisc_Select>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_UserMgt(UserMgtRequest usermgtrequest)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(usermgtrequest);
            string _response = _api.CallAPI(Constants.Get_UserMgt, inputJson);
            ServiceResponse<UserMgtResponse> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserMgtResponse>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_UserMgt(UserMgtRequest usermgtrequest)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(usermgtrequest);
            string _response = _api.CallAPI(Constants.Excel_UserMgt, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save_UserMgt(UserMgtSave_Request UserMgt)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(UserMgt);
            string _response = _api.CallAPI(Constants.Save_UserMgt, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FortunePartyCode_Exist(FortunePartyCode_Exist_Request fortunepartycode_exist_request)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(fortunepartycode_exist_request);
            string _response = _api.CallAPI(Constants.FortunePartyCode_Exist, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanyForUserMgt(GetCompanyForUserMgt_Request request)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(request); 
            string _response = _api.CallAPI(Constants.GetCompanyForUserMgt, inputJson);
            ServiceResponse<GetCompanyForUserMgt_Response> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<GetCompanyForUserMgt_Response>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanyForHoldStonePlaceOrder(GetCompanyForUserMgt_Request request)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(request);
            string _response = _api.CallAPI(Constants.GetCompanyForHoldStonePlaceOrder, inputJson);
            ServiceResponse<GetCompanyForUserMgt_Response> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<GetCompanyForUserMgt_Response>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Set_ManageUser_To_UserMgt(UserMgtRequest usermgtrequest)
        {
            Session["ManageUser_To_UserMgt"] = usermgtrequest;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_ManageUser_To_UserMgt()
        {
            UserMgtRequest obj = (UserMgtRequest)Session["ManageUser_To_UserMgt"];
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MessageMst()
        {
            return View();
        }
        public JsonResult Get_MessageMst(MessageMstSelect_Request req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.Get_MessageMst, inputJson);
            ServiceResponse<MessageMstSelect_Response> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<MessageMstSelect_Response>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MessageMst_Save(MessageMstSave_Request req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string _response = _api.CallAPI(Constants.MessageMst_Save, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserStatusReport()
        {
            return View();
        }
        public JsonResult Get_UserStatusReport(GetUserStatusReport_Request userstatusreportreq)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(userstatusreportreq);
            string _response = _api.CallAPI(Constants.Get_UserStatusReport, inputJson);
            ServiceResponse<GetUserStatusReport_Response> _data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<GetUserStatusReport_Response>>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_UserStatusReport(GetUserStatusReport_Request userstatusreportreq)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(userstatusreportreq);
            string _response = _api.CallAPI(Constants.Excel_UserStatusReport, inputJson);
            string _data = (new JavaScriptSerializer()).Deserialize<string>(_response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
    }
}