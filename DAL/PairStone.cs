using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class PairStone
    {
        public DataTable PairStone_Select(String sShape, String sColor, String sClarity, String sCut, String sFls, float? dFromCts, float? dToCts, float? dFromLenght, float? dToLength, float? dFromWidth, float? dToWidth,
            float? dFromDepth, float? dToDepth, float? dFromTablePer, float? dToTablePer, String sPointer, String sStatus,  bool? bimage, bool? bHD, String sPolish, String sSymm, String sLab, decimal? dExRate, Int32? iPageNo, Int32? iPageSize, Int64? iUserID)
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

            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromCts != null)
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCts));
            else
                para.Add(db.CreateParam("dFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromLenght != null)
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromLenght));
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

            if (dFromTablePer != null)
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromTablePer));
            else
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dToTablePer != null)
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToTablePer));
            else
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sStatus != null)
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStatus));
            else
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

          
            if (bimage != null)
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bimage));
            else
                para.Add(db.CreateParam("Bimage", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bHD != null)
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bHD));
            else
                para.Add(db.CreateParam("BHd", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dExRate != null)
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, dExRate));
            else
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));


            
            System.Data.DataTable dt = db.ExecuteSP("PairStone_Select", para.ToArray(), false);
            return dt;
        }
    }
}
