using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class DemandListData
    {
        public DataTable CustomerDemandDet_Select(Int32? iUserCode, Int32? iPageNo, Int32? iPageSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserCode != null)
                para.Add(db.CreateParam("p_for_usercode", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserCode));
            else
                para.Add(db.CreateParam("p_for_usercode", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));


            System.Data.DataTable dt = db.ExecuteSP("CustomerDemandDet_Select", para.ToArray(), false);
            return dt;
        }

        public DataTable CustomerDemandDet_Insert(Int32? iUserID, String sShape, String sColor, String sClarity, String sCut, String sPolish, String sSymm, String sFls, String sLab, float? dFromCts, float? dToCts, float? dFromDisc, float? dToDisc,
             String sPointer, float? dFromNetPrice, float? dToNetPrice, float? dFromLength, float? dToLength, float? dFromWidth, float? dToWidth, float? dFromDepth, float? dToDepth, float? dFromDepthPer, float? dToDepthPer, float? dFromTablePer, float? dToTablePer,
            float? dFromCrAng, float? dToCrAng, float? dFromCrHt, float? dToCrHt, float? dFromPavAng, float? dToPavAng, float? dFromPavHt, float? dToPavHt, String sShade, Int32? iValidDays, Int32? iQty, String sRemarks, String sDescription, String sDescriptionQuery)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

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

            if (sShade != null)
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShade));
            else
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            if (iValidDays != null)
                para.Add(db.CreateParam("iValidDays", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iValidDays));
            else
                para.Add(db.CreateParam("iValidDays", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iQty != null)
                para.Add(db.CreateParam("iQty", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iQty));
            else
                para.Add(db.CreateParam("iQty", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRemarks != null)
                para.Add(db.CreateParam("sRemarks", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRemarks));
            else
                para.Add(db.CreateParam("sRemarks", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sDescription != null)
                para.Add(db.CreateParam("sDescription", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDescription));
            else
                para.Add(db.CreateParam("sDescription", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sDescriptionQuery != null)
                para.Add(db.CreateParam("sDescriptionQry", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDescriptionQuery));
            else
                para.Add(db.CreateParam("sDescriptionQry", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("CustomerDemandDet_Insert", para.ToArray(), false);
            return dt;
        }

        public void CustomerDemandDet_Delete(Int32? iDemandID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iDemandID != null)
                para.Add(db.CreateParam("iDemandid", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iDemandID));
            else
                para.Add(db.CreateParam("iDemandid", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));


            db.ExecuteSP("CustomerDemandDet_Delete", para.ToArray(), false);

        }

    }
}
