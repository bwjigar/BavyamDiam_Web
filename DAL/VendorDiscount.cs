using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class VendorDiscount
    {
        public DataTable VendorDiscMas_SelectByPara(DateTime? dtFromDate, DateTime? dtToDate, Int32? iVendorID, Int64? iTransID, Int32? iPageNo, Int32? iPageSize, String sOrderBY)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (dtFromDate != null)
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtFromDate));
            else
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtToDate != null)
                para.Add(db.CreateParam("dtTodate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtToDate));
            else
                para.Add(db.CreateParam("dtTodate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iVendorID != null)
                para.Add(db.CreateParam("iVendorId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iVendorID));
            else
                para.Add(db.CreateParam("iVendorId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOrderBY != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBY));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("VendorDiscMas_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public void VendorDiscMasAndDet_Delete(Int64? iTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("VendorDiscMasAndDet_Delete", para.ToArray(), false);

        }

        public DataTable VendorDisc_SelectOne(Int64? iTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            DataTable dt = db.ExecuteSP("VendorDisc_SelectOne", para.ToArray(), false);
            return dt;
        }

        public Int64? VendorDiscMas_Insert(Int64? ivendorID, ref Int64? iTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (ivendorID != null)
                para.Add(db.CreateParam("sVendorId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, ivendorID));
            else
                para.Add(db.CreateParam("sVendorId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            IDbDataParameter pr = db.CreateParam("iTransId", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, DBNull.Value);
            para.Add(pr);

            db.ExecuteSP("VendorDiscMas_Insert", para.ToArray(), false);
            iTransId = Convert.ToInt32(pr.Value);
            return iTransId;
        }

        public void VendorDisc_DeleteByTransID(Int64? iTransID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("VendorDisc_DeleteByTransID", para.ToArray(), false);

        }

        public Int64? VendorDisc_Insert(String lsShape, String lsLab, String FromPointer, String lsToPointer, float? ldFromCts, float? ldToCts, String lsCutFrom, String lsToCut,
                                    String lsFromColor, String lsToColor, String lsFromClarity, String lsToClarity, String lsFromFls, String lsToFls, String sFromShade,
                                    String sToShade, float? dDisc, Int64? iTransId, Int64? iTrackUser, String sTrackIP, ref Int64? iDetTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (lsShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (FromPointer != null)
                para.Add(db.CreateParam("sFromPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, FromPointer));
            else
                para.Add(db.CreateParam("sFromPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsToPointer != null)
                para.Add(db.CreateParam("sToPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsToPointer));
            else
                para.Add(db.CreateParam("sToPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ldFromCts != null)
                para.Add(db.CreateParam("sFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, ldFromCts));
            else
                para.Add(db.CreateParam("sFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ldToCts != null)
                para.Add(db.CreateParam("sToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, ldToCts));
            else
                para.Add(db.CreateParam("sToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsCutFrom != null)
                para.Add(db.CreateParam("sFromCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsCutFrom));
            else
                para.Add(db.CreateParam("sFromCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsToCut != null)
                para.Add(db.CreateParam("sToCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsToCut));
            else
                para.Add(db.CreateParam("sToCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsFromColor != null)
                para.Add(db.CreateParam("sFromColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsFromColor));
            else
                para.Add(db.CreateParam("sFromColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsToColor != null)
                para.Add(db.CreateParam("sToColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsToColor));
            else
                para.Add(db.CreateParam("sToColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsFromClarity != null)
                para.Add(db.CreateParam("sFromClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsFromClarity));
            else
                para.Add(db.CreateParam("sFromClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsToClarity != null)
                para.Add(db.CreateParam("sToClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsToClarity));
            else
                para.Add(db.CreateParam("sToClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsFromFls != null)
                para.Add(db.CreateParam("sFromFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsFromFls));
            else
                para.Add(db.CreateParam("sFromFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (lsToFls != null)
                para.Add(db.CreateParam("sToFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, lsToFls));
            else
                para.Add(db.CreateParam("sToFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFromShade != null)
                para.Add(db.CreateParam("sFromShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFromShade));
            else
                para.Add(db.CreateParam("sFromShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sToShade != null)
                para.Add(db.CreateParam("sToShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sToShade));
            else
                para.Add(db.CreateParam("sToShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dDisc != null)
                para.Add(db.CreateParam("sDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dDisc));
            else
                para.Add(db.CreateParam("sDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iTrackUser != null)
                para.Add(db.CreateParam("iTrackUser", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTrackUser));
            else
                para.Add(db.CreateParam("iTrackUser", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sTrackIP != null)
                para.Add(db.CreateParam("sTrackIp", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTrackIP));
            else
                para.Add(db.CreateParam("sTrackIp", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            
            IDbDataParameter pr = db.CreateParam("iDetTransId", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, DBNull.Value);
            para.Add(pr);

            db.ExecuteSP("VendorDisc_Insert", para.ToArray(), false);
            iTransId = Convert.ToInt32(pr.Value);
            return iTransId;
        }
    }
}
