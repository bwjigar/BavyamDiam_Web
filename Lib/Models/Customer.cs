using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class CustomerReq
    {
        public string SearchText { get; set; }
    }
    public class Customer
    {
        public int iUserid { get; set; }
        public string sFullName { get; set; }
    }
    public class CustomerDiscReq
    {
        public string CustId { get; set; }
        public int TransId { get; set; }
        public string Oper { get; set; }
        public string xmlStr { get; set; }
        public string UserName { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    public class CustomerDisc
    {
        public long RowNo { get; set; }
        public int CustDiscId { get; set; }
        public int iTransId { get; set; }
        public string sCompName { get; set; }
        public string Username { get; set; }
        public string AssistBy { get; set; }
        public bool IsActive { get; set; }
        public string EntryDate { get; set; }
        public string sShape { get; set; }
        public string sFromShape { get; set; }
        public string sToShape { get; set; }
        public string sLab { get; set; }
        public string sFromPointer { get; set; }
        public string sToPointer { get; set; }
        public double rFromCts { get; set; }
        public double rToCts { get; set; }
        public string sFromColor { get; set; }
        public string sToColor { get; set; }
        public string sFromClarity { get; set; }
        public string sToClarity { get; set; }
        public string sFromCut { get; set; }
        public string sToCut { get; set; }
        public string sFromPolish { get; set; }
        public string sToPolish { get; set; }
        public string sFromSymm { get; set; }
        public string sToSymm { get; set; }
        public string sFromFls { get; set; }
        public string sToFls { get; set; }
        public string sFromShade { get; set; }
        public string sToShade { get; set; }
        public double rDisc { get; set; }
        public double rValDisc { get; set; }
        public int iSeqNo { get; set; }
        public int iVendorId { get; set; }
        public int iTrackUser { get; set; }
        public DateTime dtTrackDate { get; set; }
        public string sTrackIp { get; set; }

        public string DiscType { get; set; }

        public string sSign { get; set; }
        public string sPartyName { get; set; }
        public string Stock { get; set; }
        public string Location { get; set; }
        public string FullName { get; set; }
        public int CustId { get; set; }
        public string Supplier { get; set; }
    }

    public class CustomerDiscResponse
    {
        private List<CustomerDisc> dataList;

        public List<CustomerDisc> DataList
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

        public CustomerDiscResponse()
        {
            DataList = new List<CustomerDisc>();
            dataSummary = new SearchSummary();
        }
    }

    public class PartyInfoReq
    {
        public string PartyName { get; set; }
        public string ContactPerson { get; set; }
        public string PartyPrefix { get; set; }
        public int CountryId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
    }

    public class PartyInfoResponse
    {
        public int Id { get; set; }
        public string sPartyName { get; set; }
        public string sPartyPrefix { get; set; }
        public string sEmail { get; set; }
        public string sContactNo { get; set; }
        public string sContactPerson { get; set; }
        public DateTime dtCreatedDate { get; set; }
        public int sLocation { get; set; }
        public string sPath { get; set; }
    }
    public class SaveStockDiscReq
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string UserIdList { get; set; }
        public List<SaveStockDisc_Filters> Filters { get; set; }
        public SaveStockDiscReq()
        {
            Filters = new List<SaveStockDisc_Filters>();
        }
    }
    public class SaveStockDisc_Filters
    {
        public long? Id { get; set; }
        public string Sr { get; set; }
        public string iSupplier { get; set; }
        public string iLocation { get; set; }
        public string sShape { get; set; }
        public string sPointer { get; set; }
        public string sColorType { get; set; }
        public string sColor { get; set; }
        public string sINTENSITY { get; set; }
        public string sOVERTONE { get; set; }
        public string sFANCY_COLOR { get; set; }
        public string sClarity { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public string sLab { get; set; }
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
        public string dKeyToSymbol { get; set; }
        public string dCheckKTS { get; set; }
        public string dUNCheckKTS { get; set; }
        public string sBGM { get; set; }
        public string sCrownBlack { get; set; }
        public string sTableBlack { get; set; }
        public string sCrownWhite { get; set; }
        public string sTableWhite { get; set; }
        public bool View { get; set; }
        public bool Download { get; set; }
        public string Img { get; set; }
        public string Vdo { get; set; }
        public string PriceMethod { get; set; }
        public double? PricePer { get; set; }
    }
    public class GetStockDiscRes
    {
        public long RowNo { get; set; }
        public long Id { get; set; }
        public long iTransId { get; set; }
        public string iVendor { get; set; }
        public string iLocation { get; set; }
        public string sShape { get; set; }
        public string sPointer { get; set; }
        public string sColorType { get; set; }
        public string sColor { get; set; }
        public string sINTENSITY { get; set; }
        public string sOVERTONE { get; set; }
        public string sFANCY_COLOR { get; set; }
        public string sClarity { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public string sLab { get; set; }
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
        public string dKeyToSymbol { get; set; }
        public string dCheckKTS { get; set; }
        public string dUNCheckKTS { get; set; }
        public string sBGM { get; set; }
        public string sCrownBlack { get; set; }
        public string sTableBlack { get; set; }
        public string sCrownWhite { get; set; }
        public string sTableWhite { get; set; }
        public bool View { get; set; }
        public bool Download { get; set; }
        public string Img { get; set; }
        public string Vdo { get; set; }
        public string PriceMethod { get; set; }
        public double? PricePer { get; set; }
        public string CreationDate { get; set; }
        public string CustName { get; set; }
        public string sCompCountry { get; set; }
        public string sCompName { get; set; }
        public string Username { get; set; }
        public string AssistBy { get; set; }
        public bool IsActive { get; set; }
    }
    public class StockImport
    {
        public string UserName { get; set; }
        public string Supplier { get; set; }
        public string Download { get; set; }
        public string View { get; set; }
        public string PriceMethod { get; set; }
        public string PricePer { get; set; }
    }
    public class StockImportList
    {
        public List<StockImport> StockImport { get; set; }
        public StockImportList()
        {
            StockImport = new List<StockImport>();
        }
    }
    public class Save_SuppPrefix_Request
    {
        public List<SuppPrefix_Request> SuppPre { get; set; }
        public Save_SuppPrefix_Request()
        {
            SuppPre = new List<SuppPrefix_Request>();
        }
    }
    public class SuppPrefix_Request
    {
        public long SupplierPriceList_Id { get; set; }
        public int Supplier_Id { get; set; }
        public int Pointer_Id { get; set; }
        public int Location_Id { get; set; }
        public string Prefix { get; set; }
    }
    public class SuppPrefix_Response
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public long Id { get; set; }
        public int Supplier_Id { get; set; }
        public int Location_Id { get; set; }
        public int Pointer_Id { get; set; }
        public string Prefix { get; set; }
        public string CreateDate { get; set; }
    }
    public class Get_APIMst_Request
    {
        public long Id { get; set; }
        public long Supplier_Mas_Id { get; set; }
        public long SupplierPriceList_Id { get; set; }
        public string Search { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int iPgNo { get; set; }
        public int iPgSize { get; set; }
        public string OrderBy { get; set; }
        public bool For_RefNo_Pricing { get; set; }
    }
    public class Get_APIMst_Response
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public long Id { get; set; }
        public string SupplierURL { get; set; }
        public string SupplierName { get; set; }
        public string SupplierHitUrl { get; set; }
        public string SupplierResponseFormat { get; set; }
        public string FileLocation { get; set; }
        public string LocationExportType { get; set; }
        public string RepeateveryType { get; set; }
        public string Repeatevery { get; set; }
        public string SupplierAPIMethod { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string FileName { get; set; }
        public bool DiscInverse { get; set; }
        public bool NewRefNoGen { get; set; }
        public bool NewDiscGen { get; set; }
    }
    public class Save_APIMst_Request
    {
        public long Id { get; set; }
        public string SupplierURL { get; set; }
        public string SupplierName { get; set; }
        public string SupplierHitUrl { get; set; }
        public string SupplierResponseFormat { get; set; }
        public string SupplierAPIMethod { get; set; }
        public string FileLocation { get; set; }
        public string LocationExportType { get; set; }
        public string RepeateveryType { get; set; }
        public string Repeatevery { get; set; }
        public bool Active { get; set; }
        public bool DiscInverse { get; set; }
        public bool NewRefNoGen { get; set; }
        public bool NewDiscGen { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }
    }
    public class Get_SuppColSettMas_Response
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public long Id { get; set; }
        public string SupplierName { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
    }
    public class Get_Column_Mas_Response
    {
        public int SEQ_NO { get; set; }
        public string COLUMN_NAME { get; set; }
        public string DISPLAY_NAME { get; set; }
        public string PARA_SYNONYM { get; set; }
        public decimal SORT_NO { get; set; }
        public string DATA_FORMAT { get; set; }
    }
    public class Get_SuppColSettDet_Response
    {
        public long Id { get; set; }
        public int Supplier_Mas_Id { get; set; }
        public int Column_Mas_Id { get; set; }
        public string SupplierColumnName { get; set; }
        public int DisplayOrder { get; set; }
        public string DateFormatType { get; set; }
        public string DATA_FORMAT { get; set; }
    }
    public class Save_SuppColSettMas_Request
    {
        public List<ObjSuppColSettMasLst> SuppColSett { get; set; }
        public Save_SuppColSettMas_Request()
        {
            SuppColSett = new List<ObjSuppColSettMasLst>();
        }
    }
    public class ObjSuppColSettMasLst
    {
        public int Supplier_Mas_Id { get; set; }
        public string SupplierColumnName { get; set; }
        public int Column_Mas_Id { get; set; }
        public int DisplayOrder { get; set; }
        public string DateFormatColumn { get; set; }
    }
    public class Get_SupplierColumnsFromAPI_Response
    {
        public int Id { get; set; }
        public string SupplierColumn { get; set; }
    }
    public class AnkitGems
    {
        public string code { get; set; }
        public bool flag { get; set; }
        public string message { get; set; }
        public string ref_id { get; set; }
        public AnkitGems_Inner_1 data { get; set; }
    }
    public class AnkitGems_Inner_1
    {
        public AnkitGems_Inner_2 user { get; set; }
        public string accessToken { get; set; }
    }
    public class AnkitGems_Inner_2
    {
        public string name { get; set; }
        public string day_terms { get; set; }
        public string account_name { get; set; }
        public string account_short_code { get; set; }
        public string business_type { get; set; }
        public string registration_date { get; set; }
        public string is_active { get; set; }
    }

    //By Dhruv Patel-01-12-2021
    public class Dharam
    {
        public int uniqID { get; set; }
        public string company { get; set; }
        public string actCode { get; set; }
        public string selectAll { get; set; }
        public int StartIndex { get; set; }
        public int count { get; set; }
        public string columns { get; set; }
        public string finder { get; set; }
        public string sort { get; set; }

    }

    //By Dhruv Patel-02-12-2021
    public class JOY
    {
        public List<string> keys { get; set; }
        public List<List<object>> rows { get; set; }

    }

    //By Dhruv Patel-15-12-2021
    public class SGLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class SGLoginResponse
    {
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string TokenId { get; set; }
    }

    public class SGStockRequest
    {
        public string UserId { get; set; }
        public string TokenId { get; set; }
    }

    public class SGStockResponse
    {
        public List<data> Data { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
    }
    public class data
    {
        public string Stock_ID { get; set; }
        public string Shape { get; set; }
        public double Cts { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public double Rep_Price { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Depth_Per { get; set; }
        public double Table_Per { get; set; }
        public double Cr_Ang { get; set; }
        public double Cr_Ht { get; set; }
        public double Pav_Ang { get; set; }
        public double Pav_Ht { get; set; }
        public string Certi_No { get; set; }
        public string Girdle { get; set; }
        public double Disc { get; set; }
        public string Lab { get; set; }
        public string Pointer { get; set; }
        public string Status { get; set; }
        public string Shade { get; set; }
        public string Luster { get; set; }
        public string Table_Natts { get; set; }
        public string Girdle_Type { get; set; }
        public string Culet { get; set; }
        public object Table_Depth { get; set; }
        public string Inclusion { get; set; }
        public string HNA { get; set; }
        public string Side_Natts { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Comments { get; set; }
        public string Key_To_Symbol { get; set; }
        public double Disc_By_Date { get; set; }
        public string Inscription { get; set; }
        public double Girdle_Per { get; set; }
        public object Revise_Disc_Flag { get; set; }
        public string Crown_Natts { get; set; }
        public string Crown_Inclusion { get; set; }
        public string Certi_Date { get; set; }
        public string BGM { get; set; }
        public string UserComments { get; set; }
        public double Group_Disc { get; set; }
        public double Rap_Amount { get; set; }
        public double Net_Price { get; set; }
        public string Table_White { get; set; }
        public string Side_White { get; set; }
        public string Milky_Grade { get; set; }
        public string Source { get; set; }
        public string Location { get; set; }
        public string Fls_Color { get; set; }
        public DateTime? Lab_Date { get; set; }
        public double Price_Per_Cts { get; set; }
        public string View_Image { get; set; }
        public string View_Video { get; set; }
        public string View_Certi { get; set; }
        public string TableOpen { get; set; }
        public string CrownOpen { get; set; }
        public string PavillionOpen { get; set; }
        public string GirdleOpen { get; set; }
    }

    public class DiamartResponse
    {
        public string Loat_NO { get; set; }
        public string Status { get; set; }
        public string Shape { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Depth { get; set; }
        public double TotalDepth { get; set; }
        public double Table { get; set; }
        public double Discount { get; set; }
        public double Rap { get; set; }
        public string Lab { get; set; }
        public string CertiNo { get; set; }
        public string Inscription { get; set; }
        public double CrownAngle { get; set; }
        public double CrownHeight { get; set; }
        public double PavAngle { get; set; }
        public double PavDepth { get; set; }
        public string KeytoSymbols { get; set; }
        public string Natts { get; set; }
        public string Comment { get; set; }
        public string HNA { get; set; }
        public string EyeClean { get; set; }
        public string Girdle { get; set; }
        public double GirdlePerc { get; set; }
        public string Culet { get; set; }
        public string GirdleCondition { get; set; }
        public string Location { get; set; }
        public string IMG_URL { get; set; }
        public string VID_URL { get; set; }
        public string CERTI_URL { get; set; }
        public string Shade { get; set; }
        public string Milky { get; set; }
        public string Brown { get; set; }
        public string Green { get; set; }
        public string CenterBlack { get; set; }
        public string SideBlack { get; set; }
        public string OpenTable { get; set; }
        public string OpenCrown { get; set; }
        public string OpenGirdle { get; set; }
        public string OpenPavilion { get; set; }
        public string NaturalOnCrown { get; set; }
        public string NaturalOnGirdle { get; set; }
        public string NaturalOnPavillion { get; set; }
        public string EFOC { get; set; }
        public string EFOP { get; set; }
        public string Bowtie { get; set; }
        public string StarLength { get; set; }
        public string LowerHalf { get; set; }
        public double NetDollar { get; set; }
        public double Dcaret { get; set; }
    }
    public class Get_Supplier_PriceList_Response
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public long Id { get; set; }
        public long Supplier_Mas_Id { get; set; }
        public string SupplierName { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string DeleteDate { get; set; }
    }
    public class Get_API_StockFilter_Response
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public int SORT_NO { get; set; }
        public string Type { get; set; }
        public bool isActive { get; set; }
    }
    public class Stock_Upload
    {
        public string stock_num { get; set; }
        public string availability { get; set; }
        public string shape { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string fancy_color { get; set; }
        public string fancy_color_intensity { get; set; }
        public string fancy_color_overtone { get; set; }
        public string clarity { get; set; }
        public string cut { get; set; }
        public string polish { get; set; }
        public string symmetry { get; set; }
        public string fluor_intensity { get; set; }
        public string fluor_color { get; set; }
        public string lab { get; set; }
        public string measurement { get; set; }
        public string cert_num { get; set; }
        public string depth_percent { get; set; }
        public string table_percent { get; set; }
        public string Rap_price { get; set; }
        public string discount_percent { get; set; }
        public string price_per_cara { get; set; }
        public string total_sales_price { get; set; }
        public string video_url { get; set; }
        public string cert_url { get; set; }
        public string image_url { get; set; }
        public string comments { get; set; }
        public string cert_comment { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string crown_angle { get; set; }
        public string crown_height { get; set; }
        public string pavilion_depth { get; set; }
        public string pavilion_angle { get; set; }
        public string girdle_percent { get; set; }
        public string girdle_min { get; set; }
        public string girdle_max { get; set; }
        public string girdle_condition { get; set; }
        public string ratio { get; set; }
        public string DiamondType { get; set; }
        public string bgm { get; set; }
        public string culet_size { get; set; }
        public string culet_condition { get; set; }
        public string inclusion_center { get; set; }
        public string milky { get; set; }
        public string set_matched_pair_separable { get; set; }
        public string treatment { get; set; }
        public string trade_show { get; set; }
        public string shade { get; set; }
        public string laser_inscription { get; set; }
        public string camera_ref { get; set; }
        public string cash_discount { get; set; }
        public string cash_price { get; set; }
        public string pair_stock_num { get; set; }
        public string parcel_stones { get; set; }
        public string eye_clean { get; set; }
        public string star_length { get; set; }
        public string inclusion_black { get; set; }
        public string inclusion_open { get; set; }
        public string camera_type { get; set; }
        public string fancy_color_secondary_color { get; set; }
    }
    public class Save_SuppStockValue_Request
    {
        public List<SuppStockValue> Filters { get; set; }
        public Save_SuppStockValue_Request()
        {
            Filters = new List<SuppStockValue>();
        }
    }
    public class SuppStockValue
    {
        public int UserId { get; set; }
        public string BLANK { get; set; }
        public int SupplierId { get; set; }
        public string Supplier { get; set; }
        public string Location { get; set; }
        public string GoodsType { get; set; }
        public string Shape { get; set; }
        public string Pointer { get; set; }
        public string ColorType { get; set; }
        public string Color { get; set; }
        public string INTENSITY { get; set; }
        public string OVERTONE { get; set; }
        public string FANCY_COLOR { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public string Lab { get; set; }
        public Single? FromLength { get; set; }
        public Single? ToLength { get; set; }
        public Single? FromWidth { get; set; }
        public Single? ToWidth { get; set; }
        public Single? FromDepth { get; set; }
        public Single? ToDepth { get; set; }
        public Single? FromDepthPer { get; set; }
        public Single? ToDepthPer { get; set; }
        public Single? FromTablePer { get; set; }
        public Single? ToTablePer { get; set; }
        public Single? FromCrAng { get; set; }
        public Single? ToCrAng { get; set; }
        public Single? FromCrHt { get; set; }
        public Single? ToCrHt { get; set; }
        public Single? FromPavAng { get; set; }
        public Single? ToPavAng { get; set; }
        public Single? FromPavHt { get; set; }
        public Single? ToPavHt { get; set; }
        public string KeyToSymbol { get; set; }
        public string CheckKTS { get; set; }
        public string UNCheckKTS { get; set; }
        public string BGM { get; set; }
        public string CrownBlack { get; set; }
        public string TableBlack { get; set; }
        public string CrownWhite { get; set; }
        public string TableWhite { get; set; }
        public string Img { get; set; }
        public string Vdo { get; set; }
        public string PriceMethod { get; set; }
        public double? PricePer { get; set; }
    }
}