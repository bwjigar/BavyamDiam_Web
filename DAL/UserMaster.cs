using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class UserMaster
    {
        public Int64? UserActivityDet_Insert(Int32? iTransID, Int64? iUserID, DateTime dtDate, String sShape, String sColor, String sClarity, String sCut, String sPolish, String sSymm, String sFls, String sLab,
            float? dFromcts, float? dToCts, float? dFromDisc, float? dToDisc, String sPointer, String sStatus, float? dFromRapAmt, float? dToRapAmt, float? dFromNetPrice, float? dToNetPrice,
            float? dFromLength, float? dToLength, float? dFromWidth, float? dToWidth, float? dFromDepth, float? dTodepth, float? dFromDepthPer, float? dToDepthPer,
            float? dFromTablePer, float? dToTablePer, float? dFromCrAng, float? dToCrAng, float? dFromCrHt, float? dToCrHt, float? dFromPavAng, float? dToPavAng,
            float? dFromPavHt, float? dToPavHt, String sShade, String sInclusion, String sTableNatts, String sFormName, String sActivityType, float? dFromPriceCts, float? dToPriceCts,
            String sCertiNo, String sRefNo, bool? bImage, bool? bHD, String sPromotion, String sShapeColorPurity, String sCrownNatts, String sCrownInclusion, String sMilky, String sLocation, ref int? iOutTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtDate != null)
                para.Add(db.CreateParam("dTransDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtDate));
            else
                para.Add(db.CreateParam("dTransDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

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

            if (dFromcts != null)
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromcts));
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

            if (dTodepth != null)
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dTodepth));
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

            if (sFormName != null)
                para.Add(db.CreateParam("sFormName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFormName));
            else
                para.Add(db.CreateParam("sFormName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sActivityType != null)
                para.Add(db.CreateParam("sActivityType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sActivityType));
            else
                para.Add(db.CreateParam("sActivityType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPriceCts != null)
                para.Add(db.CreateParam("p_from_PriceCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPriceCts));
            else
                para.Add(db.CreateParam("p_from_PriceCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToPriceCts != null)
                para.Add(db.CreateParam("p_to_PriceCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPriceCts));
            else
                para.Add(db.CreateParam("p_to_PriceCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCertiNo != null)
                para.Add(db.CreateParam("p_certino", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("p_certino", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("p_refno", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("p_refno", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bImage != null)
                para.Add(db.CreateParam("p_for_image", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bImage));
            else
                para.Add(db.CreateParam("p_for_image", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bHD != null)
                para.Add(db.CreateParam("p_for_movie", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bHD));
            else
                para.Add(db.CreateParam("p_for_movie", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPromotion != null)
                para.Add(db.CreateParam("p_for_promotion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPromotion));
            else
                para.Add(db.CreateParam("p_for_promotion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShapeColorPurity != null)
                para.Add(db.CreateParam("p_shape_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShapeColorPurity));
            else
                para.Add(db.CreateParam("p_shape_color_purity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownNatts != null)
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownNatts));
            else
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCrownInclusion != null)
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownInclusion));
            else
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sMilky != null)
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMilky));
            else
                para.Add(db.CreateParam("sMilky", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLocation != null)
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLocation));
            else
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            //if (iOutTransId != null)
            //    para.Add(db.CreateParam("iOutTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Output, iOutTransId));
            //else
            //    para.Add(db.CreateParam("iOutTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Output, DBNull.Value));

            IDbDataParameter pr = db.CreateParam("iOutTransId", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, DBNull.Value);
            para.Add(pr);

            System.Data.DataTable dt = db.ExecuteSP("UserActivityDet_Insert", para.ToArray(), false);
            iOutTransId = Convert.ToInt32(pr.Value);
            return iOutTransId;
        }
        public DataTable get_assist_by_emp(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iUserID != null)
                para.Add(db.CreateParam("p_for_userid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("p_for_userid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("get_assist_by_emp", para.ToArray(), false);
            return dt;
        }

        public void link_cnt(Int64? iUserId, String sType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iUserId != null)
                para.Add(db.CreateParam("iuserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            else
                para.Add(db.CreateParam("iuserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sType != null)
                para.Add(db.CreateParam("type", System.Data.DbType.String, System.Data.ParameterDirection.Input, sType));
            else
                para.Add(db.CreateParam("type", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("link_cnt", para.ToArray(), false);

        }

        public DataTable UserActivityDet_Select(DateTime? dtFromDate, DateTime? dtToDate, Int64? iUserID, Int64? iEmpID, String sFullName, String sUserName, String sCompanyName, String sCountryName, Int32? iPageNo, Int32? iPageSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (dtFromDate != null)
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtFromDate));
            else
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtToDate != null)
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtToDate));
            else
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iEmpID != null)
                para.Add(db.CreateParam("iEmpId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iEmpID));
            else
                para.Add(db.CreateParam("iEmpId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFullName != null)
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFullName));
            else
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sUserName != null)
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserName));
            else
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCompanyName != null)
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompanyName));
            else
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCountryName != null)
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryName));
            else
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("UserActivityDet_Select", para.ToArray(), false);
            return dt;
        }
    }
}
