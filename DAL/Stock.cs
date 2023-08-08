using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class Stock
    {
        public DataTable UserStock_SelectStockByUserId(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("UserStock_SelectStockByUserId", para.ToArray(), false);
            return dt;
        }

        public DataTable Offer_DetailUpdate(Int64? iID, string srefno, float? ddisc, int? ddays, int? iuserid)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iID != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (srefno != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, srefno));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ddisc != null)
                para.Add(db.CreateParam("SOfferPer", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, ddisc));
            else
                para.Add(db.CreateParam("SOfferPer", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ddays != null)
                para.Add(db.CreateParam("SOffer_Validity", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, ddays));
            else
                para.Add(db.CreateParam("SOffer_Validity", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iuserid != null)
                para.Add(db.CreateParam("iUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iuserid));
            else
                para.Add(db.CreateParam("iUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iuserid != null)
                para.Add(db.CreateParam("iOfferId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iuserid));
            else
                para.Add(db.CreateParam("iOfferId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Offer_DetailUpdate", para.ToArray(), false);
            return dt;
        }


        // Added By jubin Shah 
        public DataTable Stock_SelectAllByPara_jubin(String sShape, String sColor, String sClarity, String sCut, String sPolish, String sSymm, String sFls, String sLab, float? dFromCts, float? dToCts, float? dFromDisc, float? dToDisc,
            String sPointer, String sStatus, float? dFromRapAmt, float? dToRapAmt, float? dFromNetPrice, float? dToNetPrice, float? dFromLength, float? dToLength, float? dFromWidth, float? dToWidth,
            float? dFromDepth, float? dToDepth, float? dFromDepthPer, float? dToDepthPer, float? dFromTablePer, float? dToTablePer, float? dFromCrAng, float? dToCrAng, float? dFromCrHt, float? dToCrHt,
            float? dFromPavAng, float? dToPavAng, float? dFromPavHt, float? dToPavHt, String sCertiNo, String sRefNo, Int32? iPageNo, Int32? iPageSize, String sOrderBy, bool? bAdvSearch, String sColorClarity,
            bool? bLocFlag, String sShade, String sInclusion, String sTableNatts, bool? bImage, bool? bHd, bool? bIsReviseStock, Decimal? iExcRate, Int32? iUserID, bool? bIsNotification, String sCrownNatts, String sCrownInclusion, Int32? iSupplyId, String sMilky, String sLocation)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCts != null)
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCts));
            else
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDisc != null)
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDisc));
            else
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDisc != null)
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDisc));
            else
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sStatus != null)
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStatus));
            else
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromRapAmt != null)
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromRapAmt));
            else
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToRapAmt != null)
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToRapAmt));
            else
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromNetPrice != null)
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromNetPrice));
            else
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToNetPrice != null)
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToNetPrice));
            else
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromLength != null)
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromLength));
            else
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToLength != null)
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToLength));
            else
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromWidth != null)
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromWidth));
            else
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToWidth != null)
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToWidth));
            else
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepth != null)
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepth));
            else
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepth != null)
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepth));
            else
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepthPer != null)
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepthPer));
            else
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepthPer != null)
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepthPer));
            else
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromTablePer != null)
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromTablePer));
            else
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToTablePer != null)
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToTablePer));
            else
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrAng != null)
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrAng));
            else
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrAng != null)
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrAng));
            else
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrHt != null)
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrHt));
            else
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrHt != null)
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrHt));
            else
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavAng != null)
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavAng));
            else
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavAng != null)
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavAng));
            else
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavHt != null)
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavHt));
            else
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavHt != null)
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavHt));
            else
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCertiNo != null)
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOrderBy != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBy));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bAdvSearch != null)
                para.Add(db.CreateParam("bAdvSearch", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bAdvSearch));
            else
                para.Add(db.CreateParam("bAdvSearch", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColorClarity != null)
                para.Add(db.CreateParam("p_for_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColorClarity));
            else
                para.Add(db.CreateParam("p_for_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bLocFlag != null)
                para.Add(db.CreateParam("loc_flag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bLocFlag));
            else
                para.Add(db.CreateParam("loc_flag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShade != null)
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShade));
            else
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sInclusion != null)
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sInclusion));
            else
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sTableNatts != null)
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTableNatts));
            else
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bImage != null)
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bImage));
            else
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bHd != null)
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bHd));
            else
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsReviseStock != null)
                para.Add(db.CreateParam("bReviseStock", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsReviseStock));
            else
                para.Add(db.CreateParam("bReviseStock", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iExcRate != null)
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, iExcRate));
            else
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsNotification != null)
                para.Add(db.CreateParam("bIsNotification", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsNotification));
            else
                para.Add(db.CreateParam("bIsNotification", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownNatts != null)
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownNatts));
            else
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownInclusion != null)
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownInclusion));
            else
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iSupplyId != null)
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iSupplyId));
            else
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sMilky != null)
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMilky));
            else
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLocation != null)
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLocation));
            else
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Stock_SelectAllByPara_jubin", para.ToArray(), false);
            return dt;
        }

        public DataTable Stock_SelectAllByPara_Offer(String sShape, String sColor, String sClarity, String sCut, String sPolish, String sSymm, String sFls, String sLab, float? dFromCts, float? dToCts, float? dFromDisc, float? dToDisc,
            String sPointer, String sStatus, float? dFromRapAmt, float? dToRapAmt, float? dFromNetPrice, float? dToNetPrice, float? dFromLength, float? dToLength, float? dFromWidth, float? dToWidth,
            float? dFromDepth, float? dToDepth, float? dFromDepthPer, float? dToDepthPer, float? dFromTablePer, float? dToTablePer, float? dFromCrAng, float? dToCrAng, float? dFromCrHt, float? dToCrHt,
            float? dFromPavAng, float? dToPavAng, float? dFromPavHt, float? dToPavHt, String sCertiNo, String sRefNo, Int32? iPageNo, Int32? iPageSize, String sOrderBy, bool? bAdvSearch, String sColorClarity,
            bool? bLocFlag, String sShade, String sInclusion, String sTableNatts, bool? bImage, bool? bHd, bool? bIsReviseStock, Decimal? iExcRate, Int32? iUserID, bool? bIsNotification, String sCrownNatts, String sCrownInclusion, Int32? iSupplyId, String sMilky, String sLocation)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCts != null)
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCts));
            else
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDisc != null)
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDisc));
            else
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDisc != null)
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDisc));
            else
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sStatus != null)
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStatus));
            else
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromRapAmt != null)
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromRapAmt));
            else
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToRapAmt != null)
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToRapAmt));
            else
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromNetPrice != null)
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromNetPrice));
            else
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToNetPrice != null)
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToNetPrice));
            else
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromLength != null)
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromLength));
            else
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToLength != null)
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToLength));
            else
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromWidth != null)
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromWidth));
            else
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToWidth != null)
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToWidth));
            else
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepth != null)
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepth));
            else
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepth != null)
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepth));
            else
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepthPer != null)
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepthPer));
            else
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepthPer != null)
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepthPer));
            else
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromTablePer != null)
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromTablePer));
            else
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToTablePer != null)
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToTablePer));
            else
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrAng != null)
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrAng));
            else
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrAng != null)
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrAng));
            else
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrHt != null)
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrHt));
            else
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrHt != null)
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrHt));
            else
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavAng != null)
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavAng));
            else
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavAng != null)
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavAng));
            else
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavHt != null)
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavHt));
            else
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavHt != null)
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavHt));
            else
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCertiNo != null)
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOrderBy != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBy));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bAdvSearch != null)
                para.Add(db.CreateParam("bAdvSearch", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bAdvSearch));
            else
                para.Add(db.CreateParam("bAdvSearch", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColorClarity != null)
                para.Add(db.CreateParam("p_for_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColorClarity));
            else
                para.Add(db.CreateParam("p_for_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bLocFlag != null)
                para.Add(db.CreateParam("loc_flag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bLocFlag));
            else
                para.Add(db.CreateParam("loc_flag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShade != null)
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShade));
            else
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sInclusion != null)
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sInclusion));
            else
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sTableNatts != null)
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTableNatts));
            else
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bImage != null)
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bImage));
            else
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bHd != null)
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bHd));
            else
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsReviseStock != null)
                para.Add(db.CreateParam("bReviseStock", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsReviseStock));
            else
                para.Add(db.CreateParam("bReviseStock", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iExcRate != null)
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, iExcRate));
            else
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsNotification != null)
                para.Add(db.CreateParam("bIsNotification", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsNotification));
            else
                para.Add(db.CreateParam("bIsNotification", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownNatts != null)
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownNatts));
            else
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownInclusion != null)
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownInclusion));
            else
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iSupplyId != null)
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iSupplyId));
            else
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sMilky != null)
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMilky));
            else
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLocation != null)
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLocation));
            else
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Stock_SelectAllByPara_Offer", para.ToArray(), false);
            return dt;
        }

        public DataTable Stock_SelectAllByPara_Kaushal(String sShape, String sColor, String sClarity, String sCut, String sPolish, String sSymm, String sFls, String sLab, float? dFromCts, float? dToCts, float? dFromDisc, float? dToDisc,
           String sPointer, String sStatus, float? dFromRapAmt, float? dToRapAmt, float? dFromNetPrice, float? dToNetPrice, float? dFromLength, float? dToLength, float? dFromWidth, float? dToWidth,
           float? dFromDepth, float? dToDepth, float? dFromDepthPer, float? dToDepthPer, float? dFromTablePer, float? dToTablePer, float? dFromCrAng, float? dToCrAng, float? dFromCrHt, float? dToCrHt,
           float? dFromPavAng, float? dToPavAng, float? dFromPavHt, float? dToPavHt, String sCertiNo, String sRefNo, Int32? iPageNo, Int32? iPageSize, String sOrderBy, bool? bAdvSearch, String sColorClarity,
           bool? bLocFlag, String sShade, String sInclusion, String sTableNatts, bool? bImage, bool? bHd, bool? bIsReviseStock, Decimal? iExcRate, Int32? iUserID, bool? bIsNotification, String sCrownNatts, String sCrownInclusion, Int32? iSupplyId, String sMilky, String sLocation)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCts != null)
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCts));
            else
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDisc != null)
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDisc));
            else
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDisc != null)
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDisc));
            else
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sStatus != null)
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStatus));
            else
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromRapAmt != null)
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromRapAmt));
            else
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToRapAmt != null)
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToRapAmt));
            else
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromNetPrice != null)
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromNetPrice));
            else
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToNetPrice != null)
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToNetPrice));
            else
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromLength != null)
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromLength));
            else
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToLength != null)
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToLength));
            else
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromWidth != null)
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromWidth));
            else
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToWidth != null)
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToWidth));
            else
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepth != null)
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepth));
            else
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepth != null)
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepth));
            else
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepthPer != null)
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepthPer));
            else
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepthPer != null)
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepthPer));
            else
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromTablePer != null)
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromTablePer));
            else
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToTablePer != null)
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToTablePer));
            else
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrAng != null)
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrAng));
            else
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrAng != null)
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrAng));
            else
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrHt != null)
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrHt));
            else
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrHt != null)
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrHt));
            else
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavAng != null)
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavAng));
            else
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavAng != null)
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavAng));
            else
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavHt != null)
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavHt));
            else
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavHt != null)
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavHt));
            else
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCertiNo != null)
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOrderBy != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBy));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bAdvSearch != null)
                para.Add(db.CreateParam("bAdvSearch", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bAdvSearch));
            else
                para.Add(db.CreateParam("bAdvSearch", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColorClarity != null)
                para.Add(db.CreateParam("p_for_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColorClarity));
            else
                para.Add(db.CreateParam("p_for_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bLocFlag != null)
                para.Add(db.CreateParam("loc_flag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bLocFlag));
            else
                para.Add(db.CreateParam("loc_flag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShade != null)
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShade));
            else
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sInclusion != null)
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sInclusion));
            else
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sTableNatts != null)
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTableNatts));
            else
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bImage != null)
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bImage));
            else
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bHd != null)
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bHd));
            else
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsReviseStock != null)
                para.Add(db.CreateParam("bReviseStock", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsReviseStock));
            else
                para.Add(db.CreateParam("bReviseStock", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iExcRate != null)
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, iExcRate));
            else
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsNotification != null)
                para.Add(db.CreateParam("bIsNotification", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsNotification));
            else
                para.Add(db.CreateParam("bIsNotification", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownNatts != null)
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownNatts));
            else
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownInclusion != null)
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownInclusion));
            else
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iSupplyId != null)
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iSupplyId));
            else
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sMilky != null)
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMilky));
            else
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLocation != null)
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLocation));
            else
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Stock_SelectAllByPara_Kaushal", para.ToArray(), false);
            return dt;
        }

        public DataTable Stock_SelectAllByPara(String sShape, String sColor, String sClarity, String sCut, String sPolish, String sSymm, String sFls, String sLab, float? dFromCts, float? dToCts, float? dFromDisc, float? dToDisc,
            String sPointer, String sStatus, float? dFromRapAmt, float? dToRapAmt, float? dFromNetPrice, float? dToNetPrice, float? dFromLength, float? dToLength, float? dFromWidth, float? dToWidth,
            float? dFromDepth, float? dToDepth, float? dFromDepthPer, float? dToDepthPer, float? dFromTablePer, float? dToTablePer, float? dFromCrAng, float? dToCrAng, float? dFromCrHt, float? dToCrHt,
            float? dFromPavAng, float? dToPavAng, float? dFromPavHt, float? dToPavHt, String sCertiNo, String sRefNo, Int32? iPageNo, Int32? iPageSize, String sOrderBy, bool? bAdvSearch, String sColorClarity,
            bool? bLocFlag, String sShade, String sInclusion, String sTableNatts, bool? bImage, bool? bHd, bool? bIsReviseStock, Decimal? iExcRate, Int32? iUserID, bool? bIsNotification, String sCrownNatts, String sCrownInclusion, Int32? iSupplyId, String sMilky, String sLocation)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCts != null)
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCts));
            else
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDisc != null)
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDisc));
            else
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDisc != null)
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDisc));
            else
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sStatus != null)
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStatus));
            else
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromRapAmt != null)
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromRapAmt));
            else
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToRapAmt != null)
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToRapAmt));
            else
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromNetPrice != null)
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromNetPrice));
            else
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToNetPrice != null)
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToNetPrice));
            else
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromLength != null)
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromLength));
            else
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToLength != null)
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToLength));
            else
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromWidth != null)
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromWidth));
            else
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToWidth != null)
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToWidth));
            else
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepth != null)
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepth));
            else
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepth != null)
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepth));
            else
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepthPer != null)
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepthPer));
            else
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepthPer != null)
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepthPer));
            else
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromTablePer != null)
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromTablePer));
            else
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToTablePer != null)
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToTablePer));
            else
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrAng != null)
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrAng));
            else
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrAng != null)
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrAng));
            else
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrHt != null)
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrHt));
            else
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrHt != null)
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrHt));
            else
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavAng != null)
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavAng));
            else
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavAng != null)
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavAng));
            else
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavHt != null)
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavHt));
            else
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavHt != null)
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavHt));
            else
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCertiNo != null)
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOrderBy != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBy));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bAdvSearch != null)
                para.Add(db.CreateParam("bAdvSearch", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bAdvSearch));
            else
                para.Add(db.CreateParam("bAdvSearch", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColorClarity != null)
                para.Add(db.CreateParam("p_for_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColorClarity));
            else
                para.Add(db.CreateParam("p_for_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bLocFlag != null)
                para.Add(db.CreateParam("loc_flag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bLocFlag));
            else
                para.Add(db.CreateParam("loc_flag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShade != null)
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShade));
            else
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sInclusion != null)
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sInclusion));
            else
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sTableNatts != null)
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTableNatts));
            else
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bImage != null)
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bImage));
            else
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bHd != null)
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bHd));
            else
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsReviseStock != null)
                para.Add(db.CreateParam("bReviseStock", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsReviseStock));
            else
                para.Add(db.CreateParam("bReviseStock", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iExcRate != null)
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, iExcRate));
            else
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsNotification != null)
                para.Add(db.CreateParam("bIsNotification", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsNotification));
            else
                para.Add(db.CreateParam("bIsNotification", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownNatts != null)
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownNatts));
            else
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownInclusion != null)
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownInclusion));
            else
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iSupplyId != null)
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iSupplyId));
            else
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sMilky != null)
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMilky));
            else
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLocation != null)
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLocation));
            else
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Stock_SelectAllByPara", para.ToArray(), false);
            return dt;
        }

        public DataTable Offer_SelectAllByPara(String sShape, String sColor, String sClarity, String sCut, String sPolish, String sSymm, String sFls, String sLab, float? dFromCts,
            float? dToCts, float? dFromDisc, float? dToDisc, String sPointer,String sStatus, String sCertiNo, String sRefNo, Int32? iPageNo, Int32? iPageSize, String sOrderBy,
            Int32? iUserID, float? SOffer, Int32? SOffer_Validity, DateTime? OfferDate)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCts != null)
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCts));
            else
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDisc != null)
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDisc));
            else
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDisc != null)
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDisc));
            else
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            if (sStatus != null)
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStatus));
            else
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));           

            if (sCertiNo != null)
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOrderBy != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBy));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
        
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (SOffer != null)
                para.Add(db.CreateParam("SOffer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, SOffer));
            else
                para.Add(db.CreateParam("SOffer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (OfferDate != null)
                para.Add(db.CreateParam("OfferDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, OfferDate));
            else
                para.Add(db.CreateParam("OfferDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, DBNull.Value));

            if (SOffer_Validity != null)
                para.Add(db.CreateParam("SOffer_Validity", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, SOffer_Validity));
            else
                para.Add(db.CreateParam("SOffer_Validity", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Offer_SelectAllByPara", para.ToArray(), false);
            return dt;
        }

        public DataTable Offer_Insert(Int32 iOfferID, String sRefNo, String sShape, float? dCts, String sColor, String sClarity, float? dRepPrice, String sCut, String sPolish,
            String sSymm, String sFls, float? dLength, float? dWidth,float? dDepth,float? dDepthPer,float? dTablePer,float? dCrAng,float? dCrHt, float? dPavAng,
            float? dPavHt,   String sCertiNo, float? dDisc, String sLab, String sStatus, float? SOffer, Int32? SOffer_Validity, String sPointer,bool? bImage,
            bool? bHDMovie, String sLuster,String sInclusion, String sTableNatts,String sGirdleType,String Location, String sShade,String sSymbol,int iuserid, String sCrownNatts, String sCrownInclusion)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iOfferID != null)
                para.Add(db.CreateParam("iOfferId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iOfferID));
            else
                para.Add(db.CreateParam("iOfferId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dCts != null)
                para.Add(db.CreateParam("dCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dCts));
            else
                para.Add(db.CreateParam("dCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
       
            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dRepPrice != null)
                para.Add(db.CreateParam("dRepPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dRepPrice));
            else
                para.Add(db.CreateParam("dRepPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dLength != null)
                para.Add(db.CreateParam("dLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dLength));
            else
                para.Add(db.CreateParam("dLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dWidth != null)
                para.Add(db.CreateParam("dWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dWidth));
            else
                para.Add(db.CreateParam("dWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));


            if (dDepth != null)
                para.Add(db.CreateParam("dDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dDepth));
            else
                para.Add(db.CreateParam("dDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dDepthPer != null)
                para.Add(db.CreateParam("dDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dDepthPer));
            else
                para.Add(db.CreateParam("dDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dTablePer != null)
                para.Add(db.CreateParam("dTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dTablePer));
            else
                para.Add(db.CreateParam("dTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dCrAng != null)
                para.Add(db.CreateParam("dCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dCrAng));
            else
                para.Add(db.CreateParam("dCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dCrHt != null)
                para.Add(db.CreateParam("dCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dCrHt));
            else
                para.Add(db.CreateParam("dCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dPavAng != null)
                para.Add(db.CreateParam("dPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dPavAng));
            else
                para.Add(db.CreateParam("dPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dPavHt != null)
                para.Add(db.CreateParam("dPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dPavHt));
            else
                para.Add(db.CreateParam("dPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCertiNo != null)
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dDisc != null)
                para.Add(db.CreateParam("dDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dDisc));
            else
                para.Add(db.CreateParam("dDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));


            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sStatus != null)
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStatus));
            else
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            //para.Add(db.CreateParam("bImage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bImage));
            //para.Add(db.CreateParam("bHDMovie", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bImage));

            if (bImage != null)
                para.Add(db.CreateParam("bImage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bImage));
            else
                para.Add(db.CreateParam("bImage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));


            if (sLuster != null)
                para.Add(db.CreateParam("sLuster", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLuster));
            else
                para.Add(db.CreateParam("sLuster", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sTableNatts != null)
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTableNatts));
            else
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sGirdleType != null)
                para.Add(db.CreateParam("sGirdleType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sGirdleType));
            else
                para.Add(db.CreateParam("sGirdleType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sInclusion != null)
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sInclusion));
            else
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (Location != null)
                para.Add(db.CreateParam("Location", System.Data.DbType.String, System.Data.ParameterDirection.Input, Location));
            else
                para.Add(db.CreateParam("Location", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShade != null)
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShade));
            else
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymbol != null)
                para.Add(db.CreateParam("sSymbol", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymbol));
            else
                para.Add(db.CreateParam("sSymbol", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (SOffer != null)
                para.Add(db.CreateParam("@SOfferPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, SOffer));
            else
                para.Add(db.CreateParam("@SOfferPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (SOffer_Validity != null)
                para.Add(db.CreateParam("SOffer_Validity", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, SOffer_Validity));
            else
                para.Add(db.CreateParam("SOffer_Validity", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            para.Add(db.CreateParam("iUserid", System.Data.DbType.String, System.Data.ParameterDirection.Input, iuserid));
            para.Add(db.CreateParam("iId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownNatts != null)
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownNatts));
            else
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownInclusion != null)
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownInclusion));
            else
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Offer_Insert", para.ToArray(), false);
            return dt;
        }



        public DataTable Offer_Excel(Int32? iUserID, Int32? iOfferId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iOfferId != null)
                para.Add(db.CreateParam("iOfferId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iOfferId));
            else
                para.Add(db.CreateParam("iOfferId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Offer_Excel", para.ToArray(), false);
            return dt;
        }

        public Int32 GetNewOfferID()
        {
            Int32 RetVal = 0;
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            RetVal = Convert.ToInt32(db.ExecuteScaler("SELECT NEXT VALUE FOR SUNRISE.GETOFFERID", para.ToArray(), true).ToString());

            //System.Data.DataTable dt = db.ExecuteSP("Offer_SelectAllByPara", para.ToArray(), false);
            return RetVal;
        }

        public DataTable Offer_SelectAllByPara_Excel(String sShape, String sColor, String sClarity, String sCut, String sPolish, String sSymm, String sFls, String sLab, float? dFromCts,
           float? dToCts, float? dFromDisc, float? dToDisc, String sPointer, String sStatus, String sCertiNo, String sRefNo, Int32? iPageNo, Int32? iPageSize, String sOrderBy,
           Int32? iUserID, Int32? iSupplyId, float? SOffer, Int32? SOffer_Validity)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCts != null)
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCts));
            else
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDisc != null)
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDisc));
            else
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDisc != null)
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDisc));
            else
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            if (sStatus != null)
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStatus));
            else
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCertiNo != null)
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOrderBy != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBy));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iSupplyId != null)
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iSupplyId));
            else
                para.Add(db.CreateParam("iSupplId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (SOffer != null)
                para.Add(db.CreateParam("SOffer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, SOffer));
            else
                para.Add(db.CreateParam("SOffer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (SOffer_Validity != null)
                para.Add(db.CreateParam("SOffer_Validity", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, SOffer_Validity));
            else
                para.Add(db.CreateParam("SOffer_Validity", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Offer_SelectAllByPara_Excel", para.ToArray(), false);
            return dt;
        }

        public DataTable ParaMas_SelectByType(Int32? iType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iType != null)
                para.Add(db.CreateParam("iType", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iType));
            else
                para.Add(db.CreateParam("iType", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("ParaMas_SelectByType", para.ToArray(), false);
            return dt;
        }

        public DataTable UserSearchDet_UniqueName(String sUserName, Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sUserName != null)
                para.Add(db.CreateParam("iUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserName));
            else
                para.Add(db.CreateParam("iUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("UserSearchDet_UniqueName", para.ToArray(), false);
            return dt;
        }

        public DataTable UserSearchDet_Insert(Int32? iSearchID, String sSearchName, Int32? iUserId, String sShape, String sLab, String sColor, String sClarity, String sCut, String sPolish,
            String sSymm, String sFls, String sPointer, String sShade, String sNatts, String sInclusion, String sCertiNo, String sRefNo, float? dFromCts, float? dToCts, float? dFromDisc, float? dToDisc, float? dFromRapAmount,
            float? dToRapAmount, float? dFromNetPrice, float? dToNetPrice, float? dFromLength, float? dToLength, float? dFromWidth, float? dToWidth, float? dFromDepth, float? dToDepth, float? dFromDepthPer,
            float? dToDepthPer, float? dFromTablePer, float? dToTablePer, float? dFromCrAng, float? dToCrAng, float? dFromCrHt, float? dToCrHt, float? dFromPavAng, float? dToPavAng, float? dFromPavHt,
            float? dToPavHt, Int32? iPages, float? dFromPriceCts, float? dToPriceCts, String sCrownNatts, String sCrownInclusion, String sLuster, Int32? iSupplLocation)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iSearchID != null)
                para.Add(db.CreateParam("iSearchId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iSearchID));
            else
                para.Add(db.CreateParam("iSearchId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSearchName != null)
                para.Add(db.CreateParam("sSearchName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSearchName));
            else
                para.Add(db.CreateParam("sSearchName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserId != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserId));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShade != null)
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShade));
            else
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sNatts != null)
                para.Add(db.CreateParam("sNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sNatts));
            else
                para.Add(db.CreateParam("sNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sInclusion != null)
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sInclusion));
            else
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCertiNo != null)
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCts != null)
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCts));
            else
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDisc != null)
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDisc));
            else
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDisc != null)
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDisc));
            else
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromRapAmount != null)
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromRapAmount));
            else
                para.Add(db.CreateParam("dFromRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToRapAmount != null)
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToRapAmount));
            else
                para.Add(db.CreateParam("dToRapAmount", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromNetPrice != null)
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromNetPrice));
            else
                para.Add(db.CreateParam("dFromNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToNetPrice != null)
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToNetPrice));
            else
                para.Add(db.CreateParam("dToNetPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromLength != null)
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromLength));
            else
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToLength != null)
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToLength));
            else
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromWidth != null)
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromWidth));
            else
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToWidth != null)
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToWidth));
            else
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepth != null)
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepth));
            else
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepth != null)
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepth));
            else
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromDepthPer != null)
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepthPer));
            else
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToDepthPer != null)
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepthPer));
            else
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromTablePer != null)
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromTablePer));
            else
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToTablePer != null)
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToTablePer));
            else
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrAng != null)
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrAng));
            else
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrAng != null)
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrAng));
            else
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCrHt != null)
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrHt));
            else
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCrHt != null)
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrHt));
            else
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavAng != null)
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavAng));
            else
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavAng != null)
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavAng));
            else
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavHt != null)
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavHt));
            else
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPavHt != null)
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavHt));
            else
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPages != null)
                para.Add(db.CreateParam("iPages", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPages));
            else
                para.Add(db.CreateParam("iPages", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPriceCts != null)
                para.Add(db.CreateParam("dFromPriceCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPriceCts));
            else
                para.Add(db.CreateParam("dFromPriceCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPriceCts != null)
                para.Add(db.CreateParam("dToPriceCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPriceCts));
            else
                para.Add(db.CreateParam("dToPriceCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownNatts != null)
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, sCrownNatts));
            else
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownInclusion != null)
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.Single, System.Data.ParameterDirection.Input, sCrownInclusion));
            else
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLuster != null)
                para.Add(db.CreateParam("sLuster", System.Data.DbType.Single, System.Data.ParameterDirection.Input, sLuster));
            else
                para.Add(db.CreateParam("sLuster", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iSupplLocation != null)
                para.Add(db.CreateParam("iSupplLocation", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iSupplLocation));
            else
                para.Add(db.CreateParam("iSupplLocation", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("UserSearchDet_Insert", para.ToArray(), false);
            return dt;
        }

        public DataTable UserSearchDet_Select(Int64? iSearchID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iSearchID != null)
                para.Add(db.CreateParam("iSearchId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iSearchID));
            else
                para.Add(db.CreateParam("iSearchId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("UserSearchDet_Select", para.ToArray(), false);
            return dt;
        }

        public DataTable UserSearchDet_ListSelect(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("UserSearchDet_ListSelect", para.ToArray(), false);
            return dt;
        }

        public void UserSearchDet_Delete(Int64? iSearchID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iSearchID != null)
                para.Add(db.CreateParam("iSearchId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iSearchID));
            else
                para.Add(db.CreateParam("iSearchId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("UserSearchDet_Delete", para.ToArray(), false);
        }

        public DataTable offer_detail(DateTime? FrmDate, DateTime? toDate, string comp_name, Int64? iUserID, Int64? iOfferID, String pActive, Int32? iPageNo, Int32? iPageSize, String sRefNo, float? fOfferDisc, string sOfferStatus)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (FrmDate != null)
                para.Add(db.CreateParam("frdate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, FrmDate));
            else
                para.Add(db.CreateParam("frdate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (toDate != null)
                para.Add(db.CreateParam("todate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, toDate));
            else
                para.Add(db.CreateParam("todate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            
            if (iUserID != null)
                para.Add(db.CreateParam("iuserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iuserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iOfferID != null)
                para.Add(db.CreateParam("iOfferid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOfferID));
            else
                para.Add(db.CreateParam("iOfferid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (comp_name != null)
                para.Add(db.CreateParam("comp_name", System.Data.DbType.String, System.Data.ParameterDirection.Input, comp_name));
            else
                para.Add(db.CreateParam("comp_name", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            
            
            if (pActive != null)
                para.Add(db.CreateParam("ActiveFlag", System.Data.DbType.String, System.Data.ParameterDirection.Input, pActive));
            else
                para.Add(db.CreateParam("ActiveFlag", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (fOfferDisc != null)
                para.Add(db.CreateParam("fOfferDisc", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, fOfferDisc));
            else
                para.Add(db.CreateParam("fOfferDisc", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOfferStatus != null)
                para.Add(db.CreateParam("OfferStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOfferStatus));
            else
                para.Add(db.CreateParam("OfferStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            
            System.Data.DataTable dt = db.ExecuteSP("Offer_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public DataTable Stock_SelectOne(String sRefNo, Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sRefNo != null)
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Stock_SelectOne", para.ToArray(), false);
            return dt;
        }

        //public DataTable Offer_SelectOne(int iOfferID)
        //{
        //    Database db = new Database();
        //    System.Collections.Generic.List<System.Data.IDbDataParameter> para;
        //    para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
        //    if (iOfferID != null)
        //        para.Add(db.CreateParam("iOfferId", System.Data.DbType.String, System.Data.ParameterDirection.Input, iOfferID));
        //    else
        //        para.Add(db.CreateParam("iOfferId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

        //    System.Data.DataTable dt = db.ExecuteSP("Offer_Select", para.ToArray(), false);
        //    return dt;
        //}

        public DataTable Stock_SelectLuster()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("Stock_SelectLuster", para.ToArray(), false);
            return dt;
        }

        public DataTable Stock_SelectCountry()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("Stock_SelectCountry", para.ToArray(), false);
            return dt;
        }

        public DataTable IPD_Recent_Search(Int32? iUserCode)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserCode != null)
                para.Add(db.CreateParam("p_for_usercode", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserCode));
            else
                para.Add(db.CreateParam("p_for_usercode", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("IPD_Recent_Search", para.ToArray(), false);
            return dt;
        }

        public DataTable Stock_SelectOneBySeqNo(Int64? ISeqNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (ISeqNo != null)
                para.Add(db.CreateParam("ISeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, ISeqNo));
            else
                para.Add(db.CreateParam("ISeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("Stock_SelectOneBySeqNo", para.ToArray(), false);
            return dt;
        }

        public DataTable PointerMas_SelectAll()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("PointerMas_SelectAll", para.ToArray(), false);
            return dt;
        }

        public DataTable ParaMas_SelectByPara(Int64? iType, Int64? iFromSr, Int64? iToSr)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iType != null)
                para.Add(db.CreateParam("iType", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iType));
            else
                para.Add(db.CreateParam("iType", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iFromSr != null)
                para.Add(db.CreateParam("iFromSr", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iFromSr));
            else
                para.Add(db.CreateParam("iFromSr", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iToSr != null)
                para.Add(db.CreateParam("iToSr", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iToSr));
            else
                para.Add(db.CreateParam("iToSr", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("ParaMas_SelectByPara", para.ToArray(), false);
            return dt;

        }

        public DataTable IPD_Pointer_List()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("IPD_Pointer_List", para.ToArray(), false);
            return dt;

        }

        public void Offer_Insert(string p, string p_2, float p_3, string p_4, string p_5, float p_6, string p_7, string p_8, string p_9, string p_10, string p_11, float p_12, string p_13, string p_14, float p_15, int p_16, string p_17)
        {
            throw new NotImplementedException();
        }

        public DataTable Offer_SelectByOfferId(Int32? iuserid)
        {

            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iuserid != null)
                para.Add(db.CreateParam("iuserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iuserid));
            else
                para.Add(db.CreateParam("iuserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            
            System.Data.DataTable dt = db.ExecuteSP("Offer_SelectByOfferId", para.ToArray(), false);
            return dt;

        }

        public void Offer_UpdateOfferStatus(Int32 iOfferID, String sRefNo, String sOfferStatus)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iOfferID != null)
                para.Add(db.CreateParam("iOfferID", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOfferID));
            else
                para.Add(db.CreateParam("iOfferID", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOfferStatus != null)
                para.Add(db.CreateParam("sOfferStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOfferStatus));
            else
                para.Add(db.CreateParam("sOfferStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("Offer_UpdateOfferStatus", para.ToArray(), false);

        }
    }
}
