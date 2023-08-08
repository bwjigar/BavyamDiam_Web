using System.Collections.Generic;

namespace Lib.Models
{
    public class TransValue
    {
        public int TransId { get; set; }
        public string OfferName { get; set; }
    }

    public class TransValueResponse
    {
        public List<TransValue> Data { get; set; }
        public string Msg { get; set; }
    }

    public class TransValueRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class LabStockDownloadRequest
    {
        public string RefNo { get; set; }
        public string Pointer { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public string Lab { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal FromCarat { get; set; }
        public decimal ToCarat { get; set; }
        public string TransId { get; set; }
        public short IsCustomer { get; set; }

        public double FromLength { get; set; }
        public double ToLength { get; set; }
        public double FromWidth { get; set; }
        public double ToWidth { get; set; }
        public double FromDepth { get; set; }
        public double ToDepth { get; set; }
        public double FromDisc { get; set; }
        public double ToDisc { get; set; }

        public double FromPrice { get; set; }
        public double ToPrice { get; set; }
        public double FromAmount { get; set; }
        public double ToAmount { get; set; }
        public double FromDepthper { get; set; }
        public double ToDepthper { get; set; }
        public double FromTableper { get; set; }
        public double ToTableper { get; set; }
        public string Tableblack { get; set; }
        public string Tablewhite { get; set; }
        public string Crownblack { get; set; }

        public string Crownwhite { get; set; }
        public double FromCrownangle { get; set; }
        public double ToCrownangle { get; set; }
        public double FromCrownheight { get; set; }
        public double ToCrownheight { get; set; }
        public double FromPavangle { get; set; }
        public double ToPavangle { get; set; }
        public double FromPavheight { get; set; }
        public double ToPavheight { get; set; }
    }

    public class LabStockDownloadResponse
    {
        public string RefNo { get; set; }
        public string Shape { get; set; }
        public string Pointer { get; set; }
        public string Color { get; set; }
        public string Purity { get; set; }
        public decimal CTS { get; set; }
        public decimal Sales_disc_per { get; set; }
        public decimal Sales_disc_value { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string FLS { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
        public decimal DepthPer { get; set; }
        public decimal TablePer { get; set; }
        public string Symbol { get; set; }
        public decimal GirdlePer { get; set; }
        public decimal CrownAngle { get; set; }
        public decimal CrownHeight { get; set; }
        public decimal PavAngle { get; set; }
        public decimal PavHeight { get; set; }
        public string TableNatts { get; set; }
        public string CrownNatts { get; set; }
        public string TableInclusion { get; set; }
        public string CrownInclusion { get; set; }
        public string Culet { get; set; }
        public string Comments { get; set; }
        public string Lab { get; set; }
        public decimal RapPrice { get; set; }
        public decimal RapValue { get; set; }
    }
    public class LabSearchStockDownloadRequest
    {
        public int PgNo { get; set; }
        public float PgSize { get; set; }
        public string OrderBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RefNo { get; set; }
        public string iVendor { get; set; }
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

        public string dFromDisc { get; set; }
        public string dToDisc { get; set; }
        public string dFromTotAmt { get; set; }
        public string dToTotAmt { get; set; }
        public string dFromLength { get; set; }
        public string dToLength { get; set; }
        public string dFromWidth { get; set; }
        public string dToWidth { get; set; }
        public string dFromDepth { get; set; }
        public string dToDepth { get; set; }
        public string dFromDepthPer { get; set; }
        public string dToDepthPer { get; set; }
        public string dFromTablePer { get; set; }
        public string dToTablePer { get; set; }
        public string dFromCrAng { get; set; }
        public string dToCrAng { get; set; }
        public string dFromCrHt { get; set; }
        public string dToCrHt { get; set; }
        public string dFromPavAng { get; set; }
        public string dToPavAng { get; set; }
        public string dFromPavHt { get; set; }
        public string dToPavHt { get; set; }

        public string dKeytosymbol { get; set; }
        public string dCheckKTS { get; set; }
        public string dUNCheckKTS { get; set; }
        public string sBGM { get; set; }
        public string sCrownBlack { get; set; }
        public string sTableBlack { get; set; }
        public string sCrownWhite { get; set; }
        public string sTableWhite { get; set; }
        public string sTableOpen { get; set; }
        public string sCrownOpen { get; set; }
        public string sPavOpen { get; set; }
        public string sGirdleOpen { get; set; }
        public string Img { get; set; }
        public string Vdo { get; set; }
        public string PriceMethod { get; set; }
        public float PricePer { get; set; }
        public int ExcelType { get; set; }

        public string KTSBlank { get; set; }
        public string LengthBlank { get; set; }
        public string WidthBlank { get; set; }
        public string DepthBlank { get; set; }
        public string DepthPerBlank { get; set; }
        public string TablePerBlank { get; set; }
        public string CrAngBlank { get; set; }
        public string CrHtBlank { get; set; }
        public string PavAngBlank { get; set; }
        public string PavHtBlank { get; set; }
    }

    public class LabSearchStockGridResponseInner
    {
        public long ID { get; set; }
        public int PAGE_SIZE { get; set; }
        public float TOTAL_PAGE { get; set; }
        public string TRANS_DATE { get; set; }
        public string TRANS_TIME { get; set; }
        public long TRANS_ID { get; set; }
        public string PARTY_STONE_NO { get; set; }
        public string REF_NO { get; set; }
        public string LAB { get; set; }
        public string SHAPE { get; set; }
        public long SHAPE_CODE { get; set; }
        public string VSHAPE { get; set; }
        public string PCS { get; set; }
        public float CTS { get; set; }
        public string COLOR { get; set; }
        public string PURITY { get; set; }
        public string CUT { get; set; }
        public string POLISH { get; set; }
        public string SYMM { get; set; }
        public string FLS { get; set; }
        public float SUPP_OFFER_PER { get; set; }
        public string CERTI_NO { get; set; }
        public float NET_VALUE { get; set; }
        public float LENGTH { get; set; }
        public float WIDTH { get; set; }
        public float HEIGHT { get; set; }
        public float DEPTH { get; set; }
        public float DEPTH_PER { get; set; }
        public float CROWN_ANGLE { get; set; }
        public float CROWN_HEIGHT { get; set; }
        public float PAV_ANGLE { get; set; }
        public string DAYS { get; set; }
        public float PAV_HEIGHT { get; set; }
        public string GIRDLE_TYPE { get; set; }
        public string CULET { get; set; }
        public string COMMENTS { get; set; }
        public string SYMBOL { get; set; }
        public float TABLE_PER { get; set; }
        public string TABLE_DEPTH { get; set; }
        public float CUR_RAP_PRICE { get; set; }
        public string SUBPOINTER { get; set; }
        public string PARTY_NAME { get; set; }
        public string POINTER { get; set; }
        public long SHADE { get; set; }
        public string SHADE_NAME { get; set; }
        public string LUSTER { get; set; }
        public string MILKY { get; set; }
        public string STONE_CLARITY { get; set; }
        public string CROWN_NATTS { get; set; }
        public string CROWN_INCLUSION { get; set; }
        public string TABLE_NATTS { get; set; }
        public string TABLE_INCLUSION { get; set; }
        public float RAP_PRICE { get; set; }
        public decimal RAP_VALUE { get; set; }
        public float SALES_DISC_PER { get; set; }
        public decimal SALES_DISC_VALUE { get; set; }
        public decimal OFFER_DISC_PER { get; set; }
        public decimal OFFER_DISC_VALUE { get; set; }
        public decimal SUPP_BASE_VALUE { get; set; }
        public long SUGS_NO { get; set; }
        public decimal SUPP_BASE_OFFER_PER { get; set; }
        public string OFFER_POINTER { get; set; }
        public string IMG_PATH { get; set; }
        public string VDO_PATH { get; set; }
        public string DNA_PATH { get; set; }
        public string TABLE_OPEN { get; set; }
        public string CROWN_OPEN { get; set; }
        public string PAV_OPEN { get; set; }
        public string GIRDLE_OPEN { get; set; }
        public string BUYER_NAME { get; set; }
        public string STATUS { get; set; }
        public float GIRDLE_PER { get; set; }
        public string SEQ_NO { get; set; }
        public string OFFER_NAME { get; set; }
        public string CER_PATH { get; set; }
        public string TYPE2A { get; set; }
        public string LOCATION { get; set; }
        public string HEART_IMAGE { get; set; }
        public string ARROW_IMAGE { get; set; }
        public string ASSET_IMAGE { get; set; }
        public string BGM { get; set; }
        public float MAX_SLAB_DISC { get; set; }
        public float MAX_SLAB_VALUE { get; set; }
        public string SUPPLIER { get; set; }
        public int ORD_BY { get; set; }
        public string COLOR_TYPE { get; set; }
        public string FANCY_AMOUNT { get; set; }
        public float RAPNET_DISC { get; set; }
        public string RAPNET_DAYS { get; set; }
        public float ANALIS_SALES_DISCOUNT { get; set; }
        public float ANALIS_STOCK_DISCOUNT { get; set; }
        public float ANALIS_SALES_PCS { get; set; }
        public float ANALIS_PUR_PCS { get; set; }
        public float ANALIS_PUR_DISCOUNT { get; set; }
        public float ANALIS_STOCK_PCS { get; set; }
        public float RN_SGST_DISC { get; set; }
        public string CMT_GRD { get; set; }
        public string PARAM_GRD { get; set; }
        public string BLT_GRD { get; set; }
        public string LUS_GRD { get; set; }
        public float RECO_DISC_PER { get; set; }
        public string PRE_SOLD_PARTY_NAME { get; set; }
        public string PRE_SOLD_FINAL_DISC { get; set; }
        public string VZONE { get; set; }
        public float PRICE_DISC_PER { get; set; }
        public string LAB_CMT_GRD_VALUE { get; set; }
        public float FMS { get; set; }
        public string FMS_GRADE { get; set; }
        public float ANALIS_SALES_DAYS { get; set; }
        public string SUPP_COMMENT { get; set; }
        public float BG { get; set; }
        public string RATIO { get; set; }
        public float SUPP_FINAL_DISC_PER { get; set; }
        public float SUPP_FINAL_DIS_WITH_MAX_SLAB { get; set; }
        public string ByRequestStatus { get; set; }
        public string ByRequestUsername { get; set; }
        public string ByRequestEntryDate { get; set; }
        public int Userid { get; set; }
        public string SupplierStatus { get; set; }
        public decimal CostDiscPer { get; set; }
        public decimal CostValue { get; set; }
        public decimal CostPriceCts { get; set; }
    }

    public class LabSearchStockGridResponse
    {
        private List<LabSearchStockGridResponseInner> dataList;
        public List<LabSearchStockGridResponseInner> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }
        private LabSummary dataSummary;
        public LabSummary DataSummary
        {
            get { return dataSummary; }
            set { dataSummary = value; }
        }
        public LabSearchStockGridResponse()
        {
            DataList = new List<LabSearchStockGridResponseInner>();
            dataSummary = new LabSummary();
        }
    }
    public class LabSummary
    {
        public int TOT_PAGE { get; set; }
        public int PAGE_SIZE { get; set; }
        public int TOT_PCS { get; set; }
        public double TOT_CTS { get; set; }
        public double TOT_RAP_AMOUNT { get; set; }
        public double TOT_NET_AMOUNT { get; set; }
        public double AVG_OFFER_DISC_PER { get; set; }
        public double TOT_OFFER_DISC_VALUE { get; set; }
        public double AVG_SUPP_BASE_OFFER_PER { get; set; }
        public double TOT_SUPP_BASE_VALUE { get; set; }
        public double AVG_SALES_DISC_PER { get; set; }
    }
    public class ByRequestReq
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string REF_NO { get; set; }
        public string EntryBy { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public int PgNo { get; set; }
        public float PgSize { get; set; }
        public string OrderBy { get; set; }
        public bool Pending { get; set; }
        public bool Approve { get; set; }
        public bool Reject { get; set; }
        public bool Supp_Status { get; set; }

        public List<CostCharges> CostCharges { get; set; }
        public ByRequestReq()
        {
            CostCharges = new List<CostCharges>();
        }
    }
    public class CostCharges
    {
        public string SupplierStatus { get; set; }
        public string CostDisc { get; set; }
        public string CostValue { get; set; }
        public string CostPriceCts { get; set; }
        public string REF_NO_Userid { get; set; }
        public string REF_NO { get; set; }
    }
}