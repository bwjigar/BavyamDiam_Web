using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunriseWeb.Data
{
    public class Constants
    {
        //Imagecdn link
        public static string ImageCdnLink = "https://4e0s0i2r4n0u1s0.com/img/";

        public static string UserLogin = "/User/Login";
        public static string YearDataForDashboard = "/Master/GetYearForOrderSummary";
        public static string CityListAutocomplete = "/Master/GetCityListAutocomplete";
        public static string CountryListAutoComplete = "/Master/GetCountryListAutoComplete";
        
        public static string CountryList = "/Master/GetCountryList";
        
        public static string GetSearchParameter = "/Stock/GetListValue";
        public static string AddRemoveToCart = "/Order/AddRemoveToCart";
        public static string RemoveToCart = "/Order/RemoveToCart";
        public static string AddOverseasToCart = "/Order/AddOverseasToCart";
        public static string AddRemoveToWishList = "/Order/AddRemoveToWishList";
        public static string GetAssistPersonDetail = "/Order/GetAssistPersonDetail";

        public static string MyWishlistEmailStones = "/Stock/MyWishlistEmailStones";
        public static string SelectedEmailStones = "/Stock/EmailStones";
        public static string CartEmailStones = "/Stock/CartEmailStones";
        
        public static string EmailAllStones = "/Stock/EmailAllStones";
        public static string ViewCart = "/Order/ViewCart";
        public static string GetQuickSearch = "/Stock/GetQuickSearch";
        public static string ViewWishList = "/Order/ViewWishList";
        public static string GetKeyToSymbolList = "/Stock/GetKeyToSymbol";
        public static string DownloadWishList = "/Order/DownloadWishList";

        public static string DownloadPairSearchExcel = "/Stock/DownloadPairSearchExcel";
        public static string DownloadPairSearchMedia = "/Stock/DownloadPairSearchMedia";
        public static string StockDownload = "/Stock/DownloadStockExcel";
        public static string TotalStockDownload = "/Stock/TotalStockDownload";
        public static string DownloadStockExcelWhenStockUpload = "/Stock/DownloadStockExcelWhenStockUpload";
        public static string StockDownloadWithoutToken = "/Stock/DownloadStockExcelWithoutToken";
        public static string CartStockDownload = "/Stock/CartStockDownloadExcel";
        public static string DownloadCartSearchMedia = "/Stock/DownloadCartSearchMedia";
        public static string DownloadStockMedia = "/Stock/DownloadStockMedia";
        public static string OrderHistoryMediaDownload = "/Stock/OrderHistoryMediaDownload";
        public static string DownloadStockMediaWithoutToken = "/Stock/DownloadStockMediaWithoutToken";
        public static string OverseasDNAMediaDownloadByURL = "/Stock/OverseasDNAMediaDownloadByURL";
        public static string StocDashboardCount = "/Stock/GetDashboardCnt";
        public static string Chk_StockDisc_Avail = "/Stock/Chk_StockDisc_Avail";
        public static string GetLastLoggedin = "/Stock/GetLastLoggedin";
        public static string GetRecentSearch = "/Stock/GetRecentSearch";
        public static string Dashboard_GetRecentSearch = "/Stock/Dashboard_GetRecentSearch";
        public static string GetSavedSearch = "/Stock/GetUserSearch";
        public static string Dashboard_GetSavedSearch = "/Stock/Dashboard_GetUserSearch";
        public static string DeleteUserSearch = "/Stock/UserSearchDelete";
        public static string UserSaveSearch = "/Stock/UserSaveSearch";
        public static string RevisedStock = "/Stock/GetRevisedStock";
        public static string GetSubQuickSearch = "/Stock/GetSubQuickSearch";
        public static string DynamicChart = "/Stock/GetStockSummary";
        public static string OrderSummaryChart = "/Order/GetOrderSummary";
        public static string UserOrderHistory = "/Order/GetOrderHistory";
        public static string AdminOrderHistory = "/Order/GetConfirmOrderList";
        public static string GetHoldHistory = "/Order/GetHoldHistory";
        public static string ConfirmOrder_Excel = "/Order/ExcelGetConfirmOrder";
        public static string ExcludeStoneFromStockInsert = "/Order/ExcludeStoneFromStockInsert";
        public static string UserOrderHistoryFilter = "/Order/GetOrderHistoryFilters";
        public static string DownloadOrderHistory = "/Order/DownloadOrderHistory";
        public static string GetColumnsConfigForSearch = "/Stock/GetColumnsConfigForSearch";
        public static string GetColumnsCaptionsList = "/Stock/GetColumnsCaptionsList";
        public static string GetSearchStock = "/Stock/GetSearchStock";
        public static string SaveNoFoundSearchStock = "/Stock/SaveNoFoundSearchStock";
        public static string GetSearchStockByStoneID = "/Stock/GetSearchStockByStoneID";
        public static string GetStockAvail = "/Stock/GetStockAvail";
        public static string ExpireAvailStock = "/Stock/ExpireAvailStock";
        public static string CurrencyCountryList = "/Stock/Currency_Country_List";
        public static string GET_Scheme_Disc = "/Stock/GET_Scheme_Disc";
        public static string FancyColor_Image_Status_Get = "/Stock/FancyColor_Image_Status_Get";
        public static string FancyColor_Image_Status_Set = "/Stock/FancyColor_Image_Status_Set";
        public static string OrderHistory_Video_Status_Get = "/Stock/OrderHistory_Video_Status_Get";
        public static string Hold_Stone_Avail_Customers = "/Stock/Hold_Stone_Avail_Customers";

        public static string GetSearchStockByStoneIDWithoutToken = "/Stock/GetSearchStockByStoneIDWithoutToken";
        public static string GetIVC_From_Fortune = "/Stock/GetIVC_From_Fortune";
        public static string PairSearchStock = "/Stock/GetPairSearch";
        public static string PairEmailStones = "/Stock/PairEmailStones";

        public static string DNADetail = "/Stock/DNADetail";

        public static string GetUserMas = "/Settings/GetUserMas";
        public static string SaveColumnsSettings = "/Settings/SaveColumnsSettings";
        public static string GetOfferCriteria = "/Offer/GetOfferCriteria";
        public static string SaveOfferTransactions = "/Offer/SaveOfferTransactions";
        public static string DownloadOfferExcel = "/Offer/DownloadOfferExcel";
        public static string GetOfferHistory = "/Offer/OfferHistoryDetail";
        public static string DownloadOfferHistory = "/Offer/DownloadOfferHistoryDetail";
        public static string SaveOfferCriteria = "/Offer/SaveOfferCriteria";
        public static string SaveCustWiseDisc = "/Customer/SaveCustWiseDisc";
        public static string GetCustWiseDisc = "/Customer/GetCustWiseDisc";
        
        //Admin User Constants
        public static string GetUserList = "/User/GetFullUserList";
        public static string AddUser = "/User/AddUser";
        public static string UpdateUser = "/User/UpdateUser";
        public static string DeleteUser = "/User/DeleteUser";
        public static string DownloadUser = "/User/DownloadUser";
        public static string GetUserListSearch = "/User/UserListSearch";
        public static string DownloadUserList = "/User/DownloadUserListSearch";
        public static string OrderDisc_InsUpd = "/User/OrderDisc_InsUpd";
        public static string OrderDisc_Select = "/User/OrderDisc_Select";
        
        //Settings Constants
        public static string GetCoumnsSettings = "/Settings/GetColumnsSettings";

        //API Settings
        public static string GetApiColumnsDetails = "/ApiSettings/GetApiColumnsDetails";
        public static string SaveApiFilter = "/ApiSettings/SaveApiFilter";
        public static string GetApiViews = "/ApiSettings/GetApiViews";
        public static string GetApiDetails = "/ApiSettings/GetApiDetails";
        public static string DeleteApi = "/ApiSettings/DeleteApi";
        public static string DownloadApi = "/ApiSettings/ExportApi";
        public static string GetApiCriteria = "/ApiSettings/GetApiCriteria";
        public static string SaveApiUploadMethod = "/ApiSettings/ApiUploadMethod";
        public static string GetApiUploadMethod = "/ApiSettings/GetApiUploadMethod";
        public static string ExcelGetApiUploadMethod = "/ApiSettings/ExcelGetApiUploadMethod";
        public static string ApiUploadMethod_viewAll = "/ApiSettings/ApiUploadMethod_viewAll";
        public static string GetURLApi = "/ApiSettings/GetURLApi";
        public static string CallAPIMethod = "/ApiSettings/GetApiMethodDetails";
        public static string FTPAPIPortalLogList = "/ApiSettings/FTPAPIPortalLogList";
        public static string UserwiseCompany_select = "/ApiSettings/UserwiseCompany_select";
        public static string Get_UploadMethodReport = "/ApiSettings/Get_UploadMethodReport"; 
        public static string Excel_UploadMethodReport = "/ApiSettings/Excel_UploadMethodReport"; 

        //Account Settings
        public static string GetUserDetails = "/User/GetUserDetails";
        public static string UpdateUserProfileDetails = "/User/UpdateUserProfileDetails";
        public static string GetUserProfilePicture = "/User/GetUserProfilePicture";
        public static string UpdateUserProfilePicture = "/User/UpdateUserProfilePicture";
        public static string UserRegister = "/User/RegisterUser";
        public static string ForgotPassword = "/User/ForgotPassword";
        public static string ChangePassword = "/User/UpdatePassword";
        public static string KeyAccountData = "/User/GetKeyAccountData";
        public static string NotifyList = "/User/NotifyList";
        public static string NotifySave = "/User/NotifySave";
        public static string NotifyDetList = "/User/NotifyDetList";
        public static string NotifyGet_User = "/User/NotifyGet_User";
        public static string IP_Wise_Login_Detail = "/User/IP_Wise_Login_Detail";
        public static string UserDetailGet = "/User/UserDetailGet";
        public static string PacketTraceGetList = "/User/PacketTraceGetList";
        public static string Get_UserMgt = "/User/Get_UserMgt";
        public static string Excel_UserMgt = "/User/Excel_UserMgt";
        public static string Save_UserMgt = "/User/Save_UserMgt";
        public static string FortunePartyCode_Exist = "/User/FortunePartyCode_Exist";
        public static string GetCompanyForUserMgt = "/User/GetCompanyForUserMgt";
        public static string GetCompanyForHoldStonePlaceOrder = "/User/GetCompanyForHoldStonePlaceOrder";
        public static string Get_StockDiscMgt = "/Customer/Get_StockDiscMgt";

        public static string Get_MessageMst = "/User/Get_MessageMst";
        public static string MessageMst_Save = "/User/MessageMst_Save";

        public static string Get_UserStatusReport = "/User/Get_UserStatusReport";
        public static string Excel_UserStatusReport = "/User/Excel_UserStatusReport";
        
        public static string NewsMst = "/User/NewsMst";
        public static string ErrorLogMst = "/User/ErrorLogMst";
        //Lab Stock
        public static string GetTransId = "/LabStock/GetTransId";
        public static string Lab_GetTransId = "/LabStock/Lab_GetTransId";
        
        public static string GenerateCustomerExcel = "/LabStock/CustomerExcel";
        public static string LabSearchExcel = "/LabStock/LabSearchExcel";
        public static string LabSearchGrid = "/LabStock/LabSearchGrid";
        public static string LabSearchGridExcel = "/LabStock/LabSearchGridExcel";
        


        //Information
        public static string SaveInfoDetails = "/Information/SaveInformation";
        public static string GetInfoList = "/Information/GetInformations";
        public static string GetFutureInfoList = "/Information/GetFutureInformations";
        public static string SaveEventAction = "/Information/SaveEventAction";
        public static string GetCustInfoList = "/Information/GetCustInformations";
        public static string Login_Information_Get = "/Information/Login_Information_Get";

        //UserActivity
        public static string GetUserActivityDetail = "/UserActivity/UserActivityDetail";
        public static string DownloadActivityUpdate = "/UserActivity/DownloadActivityUpdateList";

        //Notification
        public static string GetNotifications = "/Notification/NotificationList";

        //LoginInfo
        public static string GetLoginInfoDetail = "/LoginInfo/LoginInfoDetail";
        public static string DownloadLoginInfo = "/LoginInfo/DownloadLoginInfo";

        //Overseas
        public static string GetOverseasColumnsConfigForSearch = "/Overseas/GetOverseasColumnsConfigForSearch";
        public static string GetSearchOverseasStock = "/Overseas/GetSearchOverseasStock";
        public static string OverseasStockDownload = "/Overseas/DownloadOverseasStockExcel";
        public static string OverseasStockDownloadDNA = "/Overseas/DownloadOverseasStockExcelDNA";
        public static string SaveOverseasColumnsSettings = "/Overseas/SaveOverseasColumnsSettings";
        public static string EmailAllOverseasStone = "/Overseas/EmailAllOverseasStone";
        public static string OverseasStockMediaDownload = "/Overseas/DownloadOverseasStockMedia";

        //Customer
        public static string GetCustomer = "/Customer/GetCustomer";
        public static string GetCustomerDisc = "/Customer/GetCustomerDisc";
        public static string SaveCustomerDisc = "/Customer/SaveCustomerDisc";
        public static string GetUserDisc = "/Customer/GetUserDisc";
        public static string Get_StockDiscMgtReport = "/Customer/Get_StockDiscMgtReport";
        public static string Excel_StockDiscMgtReport = "/Customer/Excel_StockDiscMgtReport";
        public static string GetUserDisc_Excel = "/Customer/GetUserDisc_Excel";
        public static string SaveUserDisc = "/Customer/SaveUserDisc";
        public static string SaveStockDisc = "/Customer/SaveStockDisc";
        public static string ImportStockDisc = "/Customer/ImportStockDisc";
        public static string PartyInfo = "/Customer/GetPartyInfo";
        public static string GetSupplier = "/Customer/GetSupplier";

        public static string Get_SupplierPrefix = "/Customer/Get_SupplierPrefix";
        public static string Save_SuppPrefix = "/Customer/Save_SuppPrefix";
        public static string Delete_SuppPrefix = "/Customer/Delete_SuppPrefix";
        public static string StockUpload = "/Customer/StockUpload";

        public static string Get_SupplierMaster = "/Customer/Get_SupplierMaster";
        public static string SaveSupplierMaster = "/Customer/SaveSupplierMaster";
        public static string Get_SuppColSettMas = "/Customer/Get_SuppColSettMas";
        public static string Get_Column_Mas_Select = "/Customer/Get_Column_Mas_Select";
        public static string SupplierColSettings_ExistorNot = "/Customer/SupplierColSettings_ExistorNot";
        public static string Get_SuppColSettDet = "/Customer/Get_SuppColSettDet";
        public static string Save_SuppColSettMas = "/Customer/Save_SuppColSettMas";
        public static string SupplierColumnsGetFromAPI = "/Customer/SupplierColumnsGetFromAPI";
        public static string Get_Supplier_PriceList = "/Customer/Get_Supplier_PriceList";
        public static string Supplier_PriceList_Delete = "/Customer/Supplier_PriceList_Delete";
        
        public static string SupplierGetFrom_PriceList = "/Customer/SupplierGetFrom_PriceList";
        public static string Get_API_StockFilter = "/Customer/Get_API_StockFilter";
        public static string Save_SuppStockValue = "/Customer/Save_SuppStockValue";
        public static string Save_SuppCostValue = "/Customer/Save_SuppCostValue";
        public static string Get_SuppStockValue = "/Customer/Get_SuppStockValue";
        public static string Get_SuppCostValue = "/Customer/Get_SuppCostValue";
        

        public static string GetPacketDet = "/Stock/GetPacketDet";
        public static string GetViewHDImage = "/Stock/GetHDVideoDetail";

        public static string LogoutWithoutToken = "/Stock/LogoutWithoutToken";

        public static string PlaceConfirmOrderUsingApi_1 = "/ConfirmOrder/PlaceConfirmOrderUsingApi_1";

        public static string PlaceConfirmOrderUsingApi_Web_1 = "/ConfirmOrder/PlaceConfirmOrderUsingApi_Web_1";
        public static string PlaceConfirmOrderUsingApi_Web_2 = "/ConfirmOrder/PlaceConfirmOrderUsingApi_Web_2";

        public static string UserConfirmOrderHistory = "/ConfirmOrder/GetOrderHistory";
        public static string UserConfirmOrderHistoryFilter = "/ConfirmOrder/GetOrderHistoryFilters";
        public static string DownloadConfirmOrderHistory = "/ConfirmOrder/DownloadOrderHistory";
        public static string GetSupplierOrderLog = "/ConfirmOrder/GetSupplierOrderLogData";
        public static string DownloadSuppOrderLog = "/ConfirmOrder/DownloadSupplierOrderLog";
        public static string PurchaseOrder_Delete = "/ConfirmOrder/PurchaseOrder_Delete";
        public static string AUTO_PlaceConfirmOrderUsingApi_Web = "/ConfirmOrder/AUTO_PlaceConfirmOrderUsingApi_Web";

        //PLACE ORDER
        public static string ConfirmOrder = "/Order/PurchaseConfirmOrder";
        public static string PurchaseConfirmOrder_Web = "/Order/PurchaseConfirmOrder_Web";
        public static string PurchaseConfirmOrder_Web_1 = "/Order/PurchaseConfirmOrder_Web_1";
        public static string Final_PlaceOrder = "/Order/Final_PlaceOrder";
        //PLACE ORDER

        //HOLD STONE
        public static string HoldStone_1 = "/Order/HoldStone_1";
        public static string ReleaseStone_1 = "/Order/ReleaseStone_1";
        //HOLD STONE

        public static string ByRequest_CRUD = "/LabStock/ByRequest_CRUD";
        
        public static string ByRequest_Cart = "/LabStock/ByRequest_Cart";
        public static string LabByRequestCartGet = "/LabStock/LabByRequestCartGet"; 
        
        public static string LabByRequestGet = "/LabStock/LabByRequestGet";
        public static string ByRequest_ApproveReject = "/LabStock/ByRequest_ApproveReject";
        public static string ByRequest_Apply_Disc = "/LabStock/ByRequest_Apply_Disc";
        

    }
}