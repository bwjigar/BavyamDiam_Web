using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class OrderSummaryResponse
    {
        public string OrderMonth { get; set; }
        public decimal Cts { get; set; }
        public decimal NetPrice { get; set; }
    }

    public class OrderSummaryRequest
    {
        public int YearID { get; set; }
    }
    public class ConfirmOrderRequest
    {
        public string StoneID { get; set; }
        public string Hold_StoneID { get; set; }
        public string Comments { get; set; }
    }

    public class CustomerPlaceOrderRequest
    {
        public string StoneID { get; set; }
        public string Comments { get; set; }
    }
    public class ViewCartRequest
    {
        public string PageNo { get; set; }
        public string PageSize { get; set; }
        public string RefNo1 { get; set; }
        public string RefNo { get; set; }
        public string OfferTrans { get; set; }
        public string Shape { get; set; }
        public string Pointer { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public string Lab { get; set; }
        public string Location { get; set; }
        public string OrderBy { get; set; }
        public bool isAdmin { get; set; }
        public bool isEmp { get; set; }
        public string DownloadMedia { get; set; }

        public string ToAddress { get; set; }
        public string Comments { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }

        public string CompanyName { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
        public bool SubUser { get; set; }
    }

    public class AddToCartRequest
    {
        public string StoneID { get; set; }
        public string OfferTrans { get; set; }
        public string TransType { get; set; }
    }

    public class RemoveFromCartRequest
    {
        public string removeToCart { get; set; }
    }

    public class ExcludeStoneFromStockRequest
    {
        public string OrderId { get; set; }
        public bool bIsExcludeStk { get; set; }
    }
    public class ViewWishListRequest
    {
        public bool IsAdmin { get; set; }
        public bool IsAssistBy { get; set; }
        public int UserID { get; set; }
        //public string TUserID { get; set; }
        public string RefNoCerti { get; set; }
        public string RefNo { get; set; }
        public string CompName { get; set; }
        public string Shape { get; set; }
        public string Pointer { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public string Lab { get; set; }
        public string Location { get; set; }
        public string PageNo { get; set; }
        public string OrderBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string PageSize { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
        public string iUserid_certi_no { get; set; }
        public bool SubUser { get; set; }
        public string ToAddress { get; set; }
        public string Comments { get; set; }
    }


    public class AddToWishListRequest
    {
        public string StoneID { get; set; }
        public string TransType { get; set; }
    }

    public class ViewCartResponse
    {
        private List<SearchStone> dataList;

        public List<SearchStone> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        private SearchSummary dataSummary;

        public SearchSummary DataSummary
        {
            get { return dataSummary; }
            set { dataSummary = value; }
        }

        public ViewCartResponse()
        {
            DataList = new List<SearchStone>();
            dataSummary = new SearchSummary();
        }
    }

    public class ViewWishListResponse
    {
        private List<SearchStone> dataList;

        public List<SearchStone> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        private SearchSummary dataSummary;

        public SearchSummary DataSummary
        {
            get { return dataSummary; }
            set { dataSummary = value; }
        }

        public ViewWishListResponse()
        {
            DataList = new List<SearchStone>();
            dataSummary = new SearchSummary();
        }
    }

    public class OrderHistoryRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PageNo { get; set; }
        public string PageSize { get; set; }

        public string RefNo { get; set; }
        public string Status { get; set; }
        public string CommonName { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public bool isAdmin { get; set; }
        public bool isEmp { get; set; }
        public string OrderBy { get; set; }
        public string PgSize { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
        public string iUserid_FullOrderDate { get; set; }
        public string Assist { get; set; }
        public bool PickUp { get; set; }
        public bool NotPickUp { get; set; }
        public bool Collected { get; set; }
        public bool NotCollected { get; set; }
        public bool DateStatus { get; set; }
        public bool SubUser { get; set; }
        public bool ConfirmOrder { get; set; }
        public bool NotConfirmOrder { get; set; }
    }

    public class OrderHistoryResponse
    {
        private List<OrderList> dataList;

        public List<OrderList> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        private OrderSummary dataSummary;

        public OrderSummary DataSummary
        {
            get { return dataSummary; }
            set { dataSummary = value; }
        }

        public OrderHistoryResponse()
        {
            DataList = new List<OrderList>();
            dataSummary = new OrderSummary();
        }
    }

    public class OrderConfirmResponse
    {
        private List<ConfirmOrderData> dataList;

        public List<ConfirmOrderData> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        public OrderConfirmResponse()
        {
            DataList = new List<ConfirmOrderData>();
        }
    }

    public class OrderHistoryFiltersResponse
    {
        private List<string> companies;
        public List<string> Companies
        {
            get { return companies; }
            set { companies = value; }
        }

        private List<string> customers;
        public List<string> Customers
        {
            get { return customers; }
            set { customers = value; }
        }

        private List<string> status;
        public List<string> Status
        {
            get { return status; }
            set { status = value; }
        }

        private List<string> locations;
        public List<string> Locations
        {
            get { return locations; }
            set { locations = value; }
        }

        private List<string> users;
        public List<string> Users
        {
            get { return users; }
            set { users = value; }
        }

        public OrderHistoryFiltersResponse()
        {
            Companies = new List<string>();
            Customers = new List<string>();
            Users = new List<string>();
            Status = new List<string>();
            Locations = new List<string>();
        }
    }
    public class ConfirmOrderData
    {
        public long iSr { get; set; }
        public long iTotalRec { get; set; }
        public string dtOrderDate1 { get; set; }
        public int iOrderDetId { get; set; }
        public int iUserId { get; set; }
        public int iOrderid { get; set; }
        public string sUsername { get; set; }
        public string sFullName { get; set; }
        public string sRefNo { get; set; }
        public bool bIsExcludeStk { get; set; }
        public string sCompName { get; set; }
        public string sStatus { get; set; }
        public string Assist1 { get; set; }
        public string Assist2 { get; set; }
        public string sCertiNo { get; set; }
    }
    public class OrderSummary
    {
        public int TOT_PAGE { get; set; }
        public int PAGE_SIZE { get; set; }
        public int TOT_PCS { get; set; }
        public double TOT_CTS { get; set; }
        public double TOT_RAP_AMOUNT { get; set; }
        public double TOT_NET_AMOUNT { get; set; }
        public double AVG_PRICE_PER_CTS { get; set; }
        public double AVG_SALES_DISC_PER { get; set; }
    }

    public class OrderList
    {
        public long iSr { get; set; }
        public int iOrderid { get; set; }
        public int iUserId { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string AssistBy1 { get; set; }
        public string CompanyName { get; set; }
        public string OrderDate { get; set; }
        public string FullOrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string sCustomerNote { get; set; }
        public string sAdminNote { get; set; }
        public decimal dRapAmount { get; set; }
        public decimal dNetPrice { get; set; }
        public double PRICE_PER_CTS { get; set; }
        public int iOrderDetId { get; set; }
        public string sRefNo { get; set; }
        public string sShape { get; set; }
        public double dCts { get; set; }
        public string sColor { get; set; }
        public string sClarity { get; set; }
        public double dRepPrice { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public double dLength { get; set; }
        public double dWidth { get; set; }
        public double dDepth { get; set; }
        public double dDepthPer { get; set; }
        public double dTablePer { get; set; }
        public double dCrAng { get; set; }
        public double dCrHt { get; set; }
        public double dPavAng { get; set; }
        public double dPavHt { get; set; }
        public string sCertiNo { get; set; }
        public string sGirdle { get; set; }
        public double girdle_per { get; set; }
        public double dDisc { get; set; }
        public string sLab { get; set; }
        public string sPointer { get; set; }
        public string sStatus { get; set; }
        public string sShade { get; set; }
        public decimal iSeqNo { get; set; }
        public string sLuster { get; set; }
        public string sTableNatts { get; set; }
        public string sGirdleType { get; set; }
        public string sCulet { get; set; }
        public double dTableDepth { get; set; }
        public string sSideFtr { get; set; }
        public string sTableFtr { get; set; }
        public string sInclusion { get; set; }
        public string sHNA { get; set; }
        public string sSideNatts { get; set; }
        public string sComments { get; set; }
        public string sSymbol { get; set; }
        public string sStoneStatus { get; set; }
        public string sCrownNatts { get; set; }
        public string sCrownInclusion { get; set; }
        public string Location { get; set; }
        public float sSupplDisc { get; set; }
        public string sInscription { get; set; }
        public string view_certi_url { get; set; }
        public string verify_certi_url { get; set; }
        public bool bPRimg { get; set; }
        public bool bASimg { get; set; }
        public bool bHTimg { get; set; }
        public bool bHBimg { get; set; }
        public string BGM { get; set; }
        public string image_url { get; set; }
        public string movie_url { get; set; }
        public string ImagesLink { get; set; }
        public int page_size { get; set; }
        public int total_page { get; set; }
        public decimal Web_Benefit { get; set; }
        public decimal Final_Disc { get; set; }
        public decimal Net_Value { get; set; }
        public string PickUp_Status { get; set; }
        public string Exp_Del_Date { get; set; }
        public string Delivery_Date { get; set; }
        public string FortunePartyCode { get; set; }
        public bool IsConfirmOrder { get; set; }
        public decimal Profit { get; set; }
        public decimal SUPP_BASE_OFFER_VALUE { get; set; }
        public decimal SupplierPrice { get; set; }
        public string Supplier_Status { get; set; }
        public string SuppOrderTime { get; set; }
        public string API_Status { get; set; }
        public int LoginId { get; set; }
        public string IpAddress { get; set; }
        public string DeviceType { get; set; }
        public string OrderBy { get; set; }
        public string LabEntryResponse { get; set; }
        //public decimal SUPP_BASE_OFFER_PER { get; set; }
        //public decimal SUPP_COST_VALUE { get; set; }
        //public decimal SUPP_COST_PER { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Girdle_Open { get; set; }
        public string Actual_image_url { get; set; }
        public string Actual_movie_url { get; set; }
        public string Actual_certi_url { get; set; }
        public string Measurement { get; set; }
    }
    public class OrderCriteria
    {
        public Decimal OfferPer { get; set; }
    }
    public class SaveCustWiseDisc_Req
    {
        public string DiscPer { get; set; }
        public string UserList { get; set; }
        public string Type { get; set; }
    }
    public class GetCustWiseDisc_Res
    {
        public int iTotalRec { get; set; }
        public long iSr { get; set; }
        public long Id { get; set; }
        public int iUserid { get; set; }
        public string sUsername { get; set; }
        public string FullName { get; set; }
        public string sCompName { get; set; }
        public bool bIsActive { get; set; }
        public double PricePer { get; set; }
        public string CreationDate { get; set; }
    }
    public class PurchaseOrder_Delete_Request
    {
        public string iOrderId_sRefNo { get; set; }
    }
    public class ConfirmOrderRequest_Web
    {
        public string StoneID { get; set; }
        public string Comments { get; set; }
        public int Userid { get; set; }
        public bool IsEmployedHold { get; set; }
        public List<Hold_Stone_List> Hold_Stone_List { get; set; }
        public ConfirmOrderRequest_Web()
        {
            Hold_Stone_List = new List<Hold_Stone_List>();
        }
        public bool IsFromAPI { get; set; }
    }
    public class ConfirmOrderRequest_Web_1
    {
        public string StoneID { get; set; }
        public string Comments { get; set; }
        public int Userid { get; set; }
        public bool IsAdminEmp_Hold { get; set; }
        public List<Hold_Stone_List> Hold_Stone_List { get; set; }
        public List<UnHold_Stone_List> UnHold_Stone_List { get; set; }
        public ConfirmOrderRequest_Web_1()
        {
            Hold_Stone_List = new List<Hold_Stone_List>();
            UnHold_Stone_List = new List<UnHold_Stone_List>();
        }
        public bool IsFromAPI { get; set; }
    }
    public class Hold_Stone_List
    {
        public string sRefNo { get; set; }
        public string Hold_Party_Code { get; set; }
        public string Hold_CompName { get; set; }
        public string Status { get; set; }
    }
    public class UnHold_Stone_List
    {
        public string sRefNo { get; set; }
    }
    public class HoldStoneRequest_1
    {
        public string StoneID { get; set; }
        public string Comments { get; set; }
        public int Userid { get; set; }
        public bool IsAdminEmp_Hold { get; set; }
        public List<Hold_Stone_List> Hold_Stone_List { get; set; }
        public List<UnHold_Stone_List> UnHold_Stone_List { get; set; }
        public HoldStoneRequest_1()
        {
            Hold_Stone_List = new List<Hold_Stone_List>();
            UnHold_Stone_List = new List<UnHold_Stone_List>();
        }
        public bool IsFromAPI { get; set; }
        public int LoginUserid { get; set; }
    }
    public class Final_PlaceOrderRequest
    {
        public string StoneID { get; set; }
        public string Comments { get; set; }
    }
}
