using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class OfferCriteria
    {
        public float OfferPer { get; set; }
    }

    public class OfferTransactionsRequest
    {
        public int UserID { get; set; }
        public string StoneID { get; set; }
        public string OfferDiscPer { get; set; }
        public string OfferValidity { get; set; }
        public string Comments { get; set; }
    }

    public class OfferHistoryRequest
    {
        public string OfferId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string Active { get; set; }
        public string PageNo { get; set; }
        public string PageSize { get; set; }
    }
    public class OfferHisRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PageNo { get; set; }
        public string RefNo { get; set; }
        public string Status { get; set; }
        //public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string isAdmin { get; set; }
        public string isEmp { get; set; }
        public string PageSize { get; set; }
        public string Shape { get; set; }
        public string Pointer { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public string Lab { get; set; }
        public string OrderBy { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
    }
    public class OfferSummary
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
    public class OfferHisResponse
    {
        private List<OfferHistoryResponse> dataList;

        public List<OfferHistoryResponse> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        private OfferSummary dataSummary;

        public OfferSummary DataSummary
        {
            get { return dataSummary; }
            set { dataSummary = value; }
        }

        public OfferHisResponse()
        {
            DataList = new List<OfferHistoryResponse>();
            dataSummary = new OfferSummary();
        }
    }
    public class OfferHistoryResponse
    {
        public long iTotalRec { get; set; }
        public int iOfferId { get; set; }
        public string OfferDate { get; set; }
        public string sRefNo { get; set; }
        public int? Stock_Avail { get; set; }
        public string sUsername { get; set; }
        public string sCompName { get; set; }
        public string sLab { get; set; }
        public string sCertiNo { get; set; }
        public decimal Cts { get; set; }
        public string sShape { get; set; }
        public string sColor { get; set; }
        public string sClarity { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public decimal Disc { get; set; }
        public string sPointer { get; set; }
        public decimal RapAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
        public decimal DepthPer { get; set; }
        public decimal TablePer { get; set; }
        public decimal CrAng { get; set; }
        public decimal CrHt { get; set; }
        public decimal PavAng { get; set; }
        public decimal PavHt { get; set; }
        public string sShade { get; set; }
        public string sInclusion { get; set; }
        public string sTableNatts { get; set; }
        public string sCrownInclusion { get; set; }
        public string sCrownNatts { get; set; }
        public string sLuster { get; set; }
        public string sLocation { get; set; }
        public string sStatus { get; set; }
        public string StoneStatus { get; set; }
        public string BGM { get; set; }
        public DateTime Offexpiry_date { get; set; }
        public string is_Active { get; set; }
        public string view_certi_url { get; set; }
        public int SOffer_Validity { get; set; }
        public double SOfferPer { get; set; }
        public double cur_rap_rate { get; set; }
        public string ImagesLink { get; set; }
        public string image_url { get; set; }
        public string movie_url { get; set; }
        public string view_dna { get; set; }
        public string image_url_link { get; set; }
        public string movie_url_link { get; set; }
    }
}

