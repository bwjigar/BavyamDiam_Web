using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class StockDownloadRequest
    {
        public string UserID;
        public string StoneID { get; set; }
        public string CertiNo { get; set; }
        public string Shape { get; set; }
        public string Pointer { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public string Lab { get; set; }
        public string Inclusion { get; set; }
        public string Natts { get; set; }
        public string Shade { get; set; }
        public string FromCts { get; set; }
        public string ToCts { get; set; }
        public string FormDisc { get; set; }
        public string ToDisc { get; set; }
        public string FormPricePerCts { get; set; }
        public string ToPricePerCts { get; set; }
        public string FormNetAmt { get; set; }
        public string ToNetAmt { get; set; }
        public string FormDepth { get; set; }
        public string ToDepth { get; set; }
        public string FormLength { get; set; }
        public string ToLength { get; set; }
        public string FormWidth { get; set; }
        public string ToWidth { get; set; }
        public string FormDepthPer { get; set; }
        public string ToDepthPer { get; set; }
        public string FormTablePer { get; set; }
        public string ToTablePer { get; set; }
        public string HasImage { get; set; }
        public string HasHDMovie { get; set; }
        public string IsPromotion { get; set; }
        public string CrownInclusion { get; set; }
        public string CrownNatts { get; set; }
        public string Luster { get; set; }
        public string Location { get; set; }
        public string PageNo { get; set; }
        public string TokenNo { get; set; }
        public string StoneStatus { get; set; }
        public string FromCrownAngle { get; set; }
        public string ToCrownAngle { get; set; }
        public string FromCrownHeight { get; set; }
        public string ToCrownHeight { get; set; }
        public string FromPavAngle { get; set; }
        public string ToPavAngle { get; set; }
        public string FromPavHeight { get; set; }
        public string ToPavHeight { get; set; }
        public string BGM { get; set; }
        public string Black { get; set; }
        public string SmartSearch { get; set; }
        public string keytosymbol { get; set; }
        public string Reviseflg { get; set; }
        public string Loginpara { get; set; }
        public bool isAdmin { get; set; }
        public bool isEmp { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
        public string FullDate { get; set; }
        public short IsAll { get; set; }
        public string ColorType { get; set; }
        public string Intensity { get; set; }
        public string Overtone { get; set; }
        public string Fancy_Color { get; set; }
        public string ShapeColorPurity { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Girdle_Open { get; set; }
        public string UsedFor { get; set; }
        public string NewArrival { get; set; }
    }

    public class DashboardCountResponse
    {
        public int sCnt { get; set; }
        public string Type { get; set; }
    }
    public class Chk_StockDisc_AvailResponse
    {
        public bool Status { get; set; }
    }

    public class StockSummaryRequest
    {
        public string parameter { get; set; }
    }

    public class StockSummaryResponse
    {
        public string para { get; set; }
        public double cts { get; set; }
        public int TotalPiece { get; set; }
        public double amount { get; set; }
    }

    public class ListValueRequest
    {
        public string ListValue { get; set; }
    }

    public class ListValueResponse
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string ListType { get; set; }
        public string UrlValue { get; set; }
        public string UrlValueHov { get; set; }
        public bool isActive { get; set; }
        public long SORT_NO { get; set; }
    }

    public class KeyToSymbolResponse
    {
        public string sSymbol { get; set; }
    }


    public class UserSearchResponse
    {
        public int iSearchId { get; set; }
        public string sSearchName { get; set; }
        public string sShape { get; set; }
        public string sLab { get; set; }
        public string sColor { get; set; }
        public string sClarity { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public string sPointer { get; set; }
        public string sShade { get; set; }
        public string sNatts { get; set; }
        public string sInclusion { get; set; }
        public string sCertiNo { get; set; }
        public string sRefNo { get; set; }
        public Single? dFromCts { get; set; }
        public Single? dToCts { get; set; }
        public Single? dFromDisc { get; set; }
        public Single? dToDisc { get; set; }
        public Single? dFromRapAmount { get; set; }
        public Single? dToRapAmount { get; set; }
        public Single? dFromNetPrice { get; set; }
        public Single? dToNetPrice { get; set; }
        public Single? dFromLength { get; set; }
        public Single? dToLength { get; set; }
        public Single? dFromWidth { get; set; }
        public Single? dToWidth { get; set; }
        public Single? dFromDepth { get; set; }
        public Single? dToDepth { get; set; }
        public Single? dFromDepthPer { get; set; }
        public Single? dToDepthPer { get; set; }
        public Single? dFromTablePer { get; set; }
        public Single? dToTablePer { get; set; }
        public Single? dFromCrAng { get; set; }
        public Single? dToCrAng { get; set; }
        public Single? dFromCrHt { get; set; }
        public Single? dToCrHt { get; set; }
        public Single? dFromPavAng { get; set; }
        public Single? dToPavAng { get; set; }
        public Single? dFromPavHt { get; set; }
        public Single? dToPavHt { get; set; }
        public int? iPages { get; set; }
        public Single? dFromPriceCts { get; set; }
        public Single? dToPriceCts { get; set; }
        public string sCrownNatts { get; set; }
        public string sCrownInclusion { get; set; }
        public string location1 { get; set; }
        public int? iSupplLocation { get; set; }
        public string black { get; set; }
        public string bgm { get; set; }
        public string SkeyToSymbol { get; set; }
        public bool? bImage { get; set; }
        public bool? bHDMovie { get; set; }
        public string sDescription { get; set; }
        public string TransDate { get; set; }
        public string Color_Description { get; set; }
        public string ColorType { get; set; }
        public string Intensity { get; set; }
        public string Overtone { get; set; }
        public string Fancy_Color { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Girdle_Open { get; set; }
    }

    public class ColumnsConfigUserWiseResponse
    {
        public string sColumnName { get; set; }
        public string sCaption { get; set; }
        public long iPriority { get; set; }
    }

    public class ColumnsCaptionsResponse
    {
        public string Col_Name { get; set; }
        public string Col_Map_Name { get; set; }
        public string Caption { get; set; }
    }

    public class ColumnsConfigForSearchResponse
    {
        public int iColumnId { get; set; }
        public string ColumnName { get; set; }
        public string Caption { get; set; }
        public int Priority { get; set; }
        public string SPSearchColumn { get; set; }
    }

    public class SearchDiamondsRequest
    {
        public string SearchID { get; set; }
        public string SearchName { get; set; }
        public string StoneID { get; set; }
        public string CertiNo { get; set; }
        public string Shape { get; set; }
        public string Pointer { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public string Lab { get; set; }
        public string Inclusion { get; set; }
        public string Natts { get; set; }
        public string Shade { get; set; }
        public string FromCts { get; set; }
        public string ToCts { get; set; }
        public string FormDisc { get; set; }
        public string ToDisc { get; set; }
        public string FormPricePerCts { get; set; }
        public string ToPricePerCts { get; set; }
        public string FormNetAmt { get; set; }
        public string ToNetAmt { get; set; }
        public string FormDepth { get; set; }
        public string ToDepth { get; set; }
        public string FormLength { get; set; }
        public string ToLength { get; set; }
        public string FormWidth { get; set; }
        public string ToWidth { get; set; }
        public string FormDepthPer { get; set; }
        public string ToDepthPer { get; set; }
        public string FormTablePer { get; set; }
        public string ToTablePer { get; set; }
        public string HasImage { get; set; }
        public string HasHDMovie { get; set; }
        public string IsPromotion { get; set; }
        public string CrownInclusion { get; set; }
        public string ShapeColorPurity { get; set; }
        public string ReviseStockFlag { get; set; }

        public string CrownNatts { get; set; }
        public string Luster { get; set; }
        public string Location { get; set; }
        public string PageNo { get; set; }
        public string StoneStatus { get; set; }
        public string FromCrownAngle { get; set; }
        public string ToCrownAngle { get; set; }
        public string FromCrownHeight { get; set; }
        public string ToCrownHeight { get; set; }
        public string FromPavAngle { get; set; }
        public string ToPavAngle { get; set; }
        public string FromPavHeight { get; set; }
        public string ToPavHeight { get; set; }
        public string BGM { get; set; }
        public string Black { get; set; }
        public int UserID { get; set; }
        public int? iId { get; set; }
        public string SmartSearch { get; set; }
        public string KeyToSymbol { get; set; }
        public short PgSize { get; set; }
        public string OrderBy { get; set; }
        public string CheckKTS { get; set; }
        public string UNCheckKTS { get; set; }
        public string CaratType { get; set; }
        public string DownloadMedia { get; set; } ///Values can be "Image"/"Video"/"Certificate"
        public Boolean IsTripalEx { get; set; }
        public Boolean IsTripalVg { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
        public string Action { get; set; }
        public string dTransDate { get; set; }
        public string CurrencyAmt { get; set; }
        public string ColorType { get; set; }
        public string Intensity { get; set; }
        public string Overtone { get; set; }
        public string Fancy_Color { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Girdle_Open { get; set; }
        public string UsedFor { get; set; }
        public string NewArrival { get; set; }
    }
    public class SearchDiamondsResponse
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

        public SearchDiamondsResponse()
        {
            DataList = new List<SearchStone>();
            dataSummary = new SearchSummary();
        }
    }
    public class DNAOverSeasDownload
    {
        public string URL { get; set; }
        public string Type { get; set; }
        public string StoneId { get; set; }
    }
    public class SearchStone
    {
        public long Sr { get; set; }
        public int iUserid { get; set; }
        public string certi_no { get; set; }
        public string certi_type { get; set; }
        public string stone_ref_no { get; set; }
        public string shape { get; set; }
        public string color { get; set; }
        public string clarity { get; set; }
        public string cut { get; set; }
        public string polish { get; set; }
        public string symm { get; set; }
        public string fls { get; set; }
        public string lab { get; set; }
        public decimal cts { get; set; }
        public string pointer { get; set; }
        public double cur_rap_rate { get; set; }
        public decimal sales_disc_per { get; set; }
        public string status { get; set; }
        public decimal p_seq_no { get; set; }
        public decimal table_per { get; set; }
        public decimal depth_per { get; set; }
        public decimal price_per_cts { get; set; }
        public decimal rap_amount { get; set; }
        public decimal net_amount { get; set; }
        public decimal Final_Disc { get; set; }
        public decimal Final_Value { get; set; }
        public string promotion { get; set; }
        public string new_arrival { get; set; }
        public string status1 { get; set; }
        public string sh_name { get; set; }
        public bool? bImage { get; set; }
        public string image_url { get; set; }
        public string image_url1 { get; set; }
        public string image_url2 { get; set; }
        public string image_url3 { get; set; }
        public string movie_url { get; set; }
        public string view_certi_url { get; set; }
        public string verify_certi_url { get; set; }
        public string view_dna { get; set; }
        public int page_size { get; set; }
        public float total_page { get; set; }
        public string symbol { get; set; }
        public string inclusion { get; set; }
        public string table_natts { get; set; }
        public decimal crown_height { get; set; }
        public decimal crown_angle { get; set; }
        public decimal pav_height { get; set; }
        public decimal pav_angle { get; set; }
        public decimal length { get; set; }
        public decimal width { get; set; }
        public decimal depth { get; set; }
        public string shade { get; set; }
        public string girdle { get; set; }
        public string girdle_type { get; set; }
        public string Stock_Staus { get; set; }
        public string Party_Name { get; set; }
        public string Crown_Natts { get; set; }
        public string Crown_Inclusion { get; set; }
        public string Luster { get; set; }
        public string Location { get; set; }
        public string sInscription { get; set; }
        public int SOffer { get; set; }
        public string BGM { get; set; }
        public string Black { get; set; }
        public string sCulet { get; set; }

        public string dCertiDate { get; set; }
        public decimal dTableDepth { get; set; }
        public decimal girdle_per { get; set; }
        public string sComments { get; set; }
        public string sHNA { get; set; }
        public string sLrHalf { get; set; }
        public string sLrHgt { get; set; }
        public string sOpen { get; set; }
        public string sSideFtr { get; set; }
        public string sSideNatts { get; set; }
        public string sStrLn { get; set; }
        public string sTableFtr { get; set; }
        public string ImagesLink { get; set; }
        public string chk { get; set; }
        public string vw { get; set; }
        public string StoneStatus { get; set; }
        public bool? bPRimg { get; set; }
        public bool? bASimg { get; set; }
        public bool? bHTimg { get; set; }
        public bool? bHBimg { get; set; }
        public bool? bMP4 { get; set; }
        public bool? bjson { get; set; }
        public decimal offerDisc { get; set; }
        public decimal offerAmt { get; set; }
        public int validDays { get; set; }
        public string CompName { get; set; }
        public string UserName { get; set; }
        public int cust_id { get; set; }
        public string Trans_date { get; set; }
        public string cust_name { get; set; }
        public string AssistBy1 { get; set; }
        public string sShort_Name { get; set; }
        public string TempOrderDate { get; set; }
        public int IsOverseas { get; set; }
        public string sImglink { get; set; }
        public string sVdoLink { get; set; }
        public decimal net_amount_CurrencyAmt { get; set; }
        public string Overseas_Image_Download_Link { get; set; }
        public string Overseas_Image_Download_Link1 { get; set; }
        public string Overseas_Image_Download_Link2 { get; set; }
        public string Overseas_Image_Download_Link3 { get; set; }
        public string Overseas_Certi_Download_Link { get; set; }
        public int Hold_Party_Code { get; set; }
        public string Hold_Username { get; set; }
        public string Hold_CompName { get; set; }
        public int ForCust_Hold { get; set; }
        public int ForAssist_Hold { get; set; }
        public int ForAdmin_Hold { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Girdle_Open { get; set; }
        public string Cur_Status { get; set; }
        public string Measurement { get; set; }
        public string Actual_image_url { get; set; }
        public string Actual_movie_url { get; set; }
        public string Actual_certi_url { get; set; }
    }

    public class SearchSummary
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

    public class SearchByStoneIDRequest
    {
        public string StoneID { get; set; }
        public int UserId { get; set; }
    }

    public class QuickSearchRequest
    {
        public int iUserId { get; set; }
        public string Cut { get; set; }
        public string Fls { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
    }

    public class QuickSearchResponse
    {
        public int col_grp_sr { get; set; }
        public string Color { get; set; }
        public int purity_grp_sr { get; set; }
        public string Purity { get; set; }
        public int p_018_022 { get; set; }
        public int p_023_029 { get; set; }
        public int p_030_039 { get; set; }
        public int p_040_049 { get; set; }
        public int p_050_059 { get; set; }
        public int p_060_069 { get; set; }
        public int p_070_079 { get; set; }
        public int p_080_089 { get; set; }
        public int p_090_099 { get; set; }
        public int p_100_119 { get; set; }
        public int p_120_149 { get; set; }
        public int p_150_199 { get; set; }
        public int p_200_299 { get; set; }
        public int p_300_9999 { get; set; }
    }

    public class SubQuickSearchRequest
    {
        public string Cut { get; set; }
        public string Fls { get; set; }
        public string Pointer { get; set; }
        public string ColorGroup { get; set; }
        public string PurityGroup { get; set; }
    }

    public class SubQuickSearchResponse
    {
        public int Is_Header { get; set; }
        public int Is_Fancy { get; set; }
        public int Color_Sr { get; set; }
        public string Shape_Color { get; set; }
        public int FL { get; set; }
        public int I1 { get; set; }
        public int I2 { get; set; }
        public int I3 { get; set; }
        public int IF { get; set; }
        public int VVS1 { get; set; }
        public int VVS2 { get; set; }
        public int VS1 { get; set; }
        public int VS2 { get; set; }
        public int SI1 { get; set; }
        public int SI2 { get; set; }
        public int IS_FL { get; set; }
        public int IS_IF { get; set; }
        public int IS_VVS1 { get; set; }
        public int IS_VVS2 { get; set; }
        public int IS_VS1 { get; set; }
        public int IS_VS2 { get; set; }
        public int IS_SI1 { get; set; }
        public int IS_SI2 { get; set; }
        public int IS_I1 { get; set; }
        public int IS_I2 { get; set; }
        public int IS_I3 { get; set; }
        public int Total { get; set; }
    }
    public class RecentSearchResponse
    {
        public long iSr { get; set; }
        public int iTransId { get; set; }
        public int iUserId { get; set; }
        public int iSeqNo { get; set; }
        public string dTransDate { get; set; }
        public string SHAPE { get; set; }
        public string COLOR { get; set; }
        public string CLARITY { get; set; }
        public string CUT { get; set; }
        public string POLISH { get; set; }
        public string SYMM { get; set; }
        public string FLS { get; set; }
        public string LAB { get; set; }
        public string POINTER { get; set; }
        public double FROM_CTS { get; set; }
        public double TO_CTS { get; set; }
        public double FROM_PRICECTS { get; set; }
        public double TO_PRICECTS { get; set; }
        public double FROM_NETAMT { get; set; }
        public double TO_NETAMT { get; set; }
        public double FROM_DISC { get; set; }
        public double TO_DISC { get; set; }
        public double FROM_LENGTH { get; set; }
        public double TO_LENGTH { get; set; }
        public double FROM_WIDTH { get; set; }
        public double TO_WIDTH { get; set; }
        public double FROM_DEPTH { get; set; }
        public double TO_DEPTH { get; set; }
        public double FROM_DEPTH_PER { get; set; }
        public double TO_DEPTH_PER { get; set; }
        public double FROM_TABLE_PER { get; set; }
        public double TO_TABLE_PER { get; set; }
        public string SHADE { get; set; }
        public string INCLUSION { get; set; }
        public string NATTS { get; set; }
        public string CERTI_NO { get; set; }
        public string STONE_REF_NO { get; set; }
        public bool? IMAGE { get; set; }
        public bool? MOVIE { get; set; }
        public bool? PROMOTION { get; set; }
        public string SHAPE_COLOR_PURITY { get; set; }
        public double FROM_CR_ANG { get; set; }
        public double TO_CR_ANG { get; set; }
        public double FROM_CR_HT { get; set; }
        public double TO_CR_HT { get; set; }
        public double FROM_PAV_ANG { get; set; }
        public double TO_PAV_ANG { get; set; }
        public double FROM_PAV_HT { get; set; }
        public double TO_PAV_HT { get; set; }
        public double FROM_RAP_AMOUNT { get; set; }
        public double TO_RAP_AMOUNT { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
        public string CROWN_NATTS { get; set; }
        public string CROWN_INCLUSION { get; set; }
        public string LUSTER { get; set; }
        public string LOCATION { get; set; }
        public string skeytosymbol { get; set; }
        public int Total_Rec { get; set; }
        public string Description { get; set; }
        public string ColorType { get; set; }
        public string Intensity { get; set; }
        public string Overtone { get; set; }
        public string Fancy_Color { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Girdle_Open { get; set; }
    }

    public class PairSearchDiamondsResponse
    {
        private List<PairSearchStone> dataList;

        public List<PairSearchStone> DataList
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

        public PairSearchDiamondsResponse()
        {
            DataList = new List<PairSearchStone>();
            dataSummary = new SearchSummary();
        }
    }

    public class PairSearchStone
    {
        public long Sr { get; set; }
        public string certi_no { get; set; }
        public Boolean PairLastColumn { get; set; }
        public string stone_ref_no { get; set; }
        public long PAIR_NO { get; set; }
        public string shape { get; set; }
        public string color { get; set; }
        public string clarity { get; set; }
        public string cut { get; set; }
        public string polish { get; set; }
        public string symm { get; set; }
        public string fls { get; set; }
        public string lab { get; set; }
        public double cts { get; set; }
        public string pointer { get; set; }
        public string measurement { get; set; }
        public double cur_rap_rate { get; set; }
        public double sales_disc_per { get; set; }
        public string status { get; set; }
        public int p_seq_no { get; set; }
        public Single table_per { get; set; }
        public Single depth_per { get; set; }
        public double price_per_cts { get; set; }
        public decimal rap_amount { get; set; }
        public decimal net_amount { get; set; }
        public string promotion { get; set; }
        public string new_arrival { get; set; }
        public string status1 { get; set; }
        public bool? bImage { get; set; }
        public string image_url { get; set; }
        public string movie_url { get; set; }
        public string view_certi_url { get; set; }
        public string verify_certi_url { get; set; }
        public string view_dna { get; set; }
        public int page_size { get; set; }
        public float total_page { get; set; }
        public string symbol { get; set; }
        public string inclusion { get; set; }
        public string table_natts { get; set; }
        public Single crown_height { get; set; }
        public Single crown_angle { get; set; }
        public Single pav_height { get; set; }
        public Single pav_angle { get; set; }
        public Single length { get; set; }
        public Single width { get; set; }
        public Single depth { get; set; }
        public string shade { get; set; }
        public string girdle { get; set; }
        public string girdle_type { get; set; }
        public string Stock_Staus { get; set; }
        public string Party_Name { get; set; }
        public string Crown_Natts { get; set; }
        public string Crown_Inclusion { get; set; }
        public string Luster { get; set; }
        public string Location { get; set; }
        public string sInscription { get; set; }
        public int SOffer { get; set; }
        public string BGM { get; set; }
        public string Black { get; set; }
        public string sCulet { get; set; }
        public string ImagesLink { get; set; }
        public string chk { get; set; }
        public string vw { get; set; }
        public string StoneStatus { get; set; }
        public string sh_name { get; set; }
        public bool? bPRimg { get; set; }
        public decimal girdle_per { get; set; }
        public int Hold_Party_Code { get; set; }
        public string Hold_Username { get; set; }
        public string Hold_CompName { get; set; }
        public int ForCust_Hold { get; set; }
        public int ForAssist_Hold { get; set; }
        public int ForAdmin_Hold { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Girdle_Open { get; set; }
    }

    public class EmailStonesRequest
    {
        public string StoneID { get; set; }
        public string ToAddress { get; set; }
        public string Comments { get; set; }
        public string FormName { get; set; }
        public string ActivityType { get; set; }
        public bool isAdmin { get; set; }
        public bool isEmp { get; set; }
        public short IsAll { get; set; }
        public string ColorType { get; set; }
    }

    public class EmailAllStonesRequest
    {
        public bool? IsRevised { get; set; }
        public SearchDiamondsRequest SearchCriteria { get; set; }
        public string ToAddress { get; set; }
        public string Comments { get; set; }
        public short IsAll { get; set; }
    }

    public class RevisedStockRequest
    {
        public string PageNo { get; set; }
        public string CertiNo { get; set; }
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
    }
    public class SmartSearchURL
    {
        public string URL { get; set; }
    }
    public class Scheme_Disc
    {
        public string Discount { get; set; }
        public string Value { get; set; }
    }
    public class Ora_Stock_History
    {
        public long iTotalRec { get; set; }
        public string iId { get; set; }
        public string TotalStock { get; set; }
        public string Message { get; set; }
        public string DateTime { get; set; }
        public string Status { get; set; }
        public string Full_Stock_Excel_Gen_DateTime { get; set; }
        public string Full_Stock_Excel_Gen_URL { get; set; }
    }
    public class FancyColor_Image
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
    public class OrderHistory_Video
    {
        public string Video_Visible { get; set; }
    }
    public class GetLastLoggedinRequest
    {
        public string DeviceType { get; set; }
    }
    public class GetLastLoggedinResponse
    {
        public string LoginDate { get; set; }
    }
}