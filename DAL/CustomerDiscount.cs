using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class CustomerDiscount
    {
        public DataTable CustDisc_View_new(String sParaType, String sParaTypeVal, String vCompList, bool? bAllData, Int16? iPageNo, Int16? iPageSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sParaType != null)
                para.Add(db.CreateParam("sParaType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sParaType));
            else
                para.Add(db.CreateParam("sParaType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sParaTypeVal != null)
                para.Add(db.CreateParam("sParaTypeVal", System.Data.DbType.String, System.Data.ParameterDirection.Input, sParaTypeVal));
            else
                para.Add(db.CreateParam("sParaTypeVal", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (vCompList != null)
                para.Add(db.CreateParam("vCompList", System.Data.DbType.String, System.Data.ParameterDirection.Input, vCompList));
            else
                para.Add(db.CreateParam("vCompList", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bAllData != null)
                para.Add(db.CreateParam("allData", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bAllData));
            else
                para.Add(db.CreateParam("allData", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo == null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, iPageNo));
            if (iPageSize == null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, iPageSize));

            System.Data.DataTable dt = db.ExecuteSP("CustDisc_View_new", para.ToArray(), false);
            return dt;
        }

        public void CustDiscMasAndDet_Delete(Int64? iTransID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("CustDiscMasAndDet_Delete", para.ToArray(), false);
        }

        public DataTable CustDiscDet_SelectOne(Int64? iTransID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            DataTable dt = db.ExecuteSP("CustDiscDet_SelectOne", para.ToArray(), false);
            return dt;
        }

        public Int64? CustDiscMas_Insert(String sType, String sTypeValue, ref Int64? iTransID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sType != null)
                para.Add(db.CreateParam("sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sType));
            else
                para.Add(db.CreateParam("sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sTypeValue != null)
                para.Add(db.CreateParam("sType_val", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTypeValue));
            else
                para.Add(db.CreateParam("sType_val", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            
            IDbDataParameter pr = db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Output, DBNull.Value);
            para.Add(pr);
            DataTable dt = db.ExecuteSP("CustDiscMas_Insert", para.ToArray(), false);
            iTransID = Convert.ToInt64(pr.Value);
            return iTransID;
        }

        public Int64? CustDiscDet_Insert(String FromShape, String ToShape, String Lab, String FromPointer, String ToPointer, float? FromCts,
                                        float? ToCts, String FromCut, String ToCut, String FromColor, String ToColor, String FromClarity,
                                        String ToClarity, String FromFls, String ToFls, String sFromShade, String sToShade, float? Disc,
                                        float? ValDisc, Int32? iVendorId, Int64? iTransId, Int32? iTrackUser, String sTrackIp, ref Int64? iSeq_no)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (FromShape == null)
                para.Add(db.CreateParam("sFromShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sFromShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, FromShape));

            if (ToShape == null)
                para.Add(db.CreateParam("sToShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sToShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, ToShape));

            if (Lab == null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, Lab));

            if (FromPointer == null)
                para.Add(db.CreateParam("sFromPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sFromPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, FromPointer));

            if (ToPointer == null)
                para.Add(db.CreateParam("sToPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sToPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, ToPointer));

            if (FromCts == null)
                para.Add(db.CreateParam("rFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("rFromCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, Convert.ToSingle(FromCts)));

            if (ToCts == null)
                para.Add(db.CreateParam("rToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("rToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, Convert.ToSingle(ToCts)));


            if (FromCut == null)
                para.Add(db.CreateParam("sFromCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sFromCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, FromCut));

            if (ToCut == null)
                para.Add(db.CreateParam("sToCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sToCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, ToCut));

            if (FromColor == null)
                para.Add(db.CreateParam("sFromColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sFromColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, FromColor));

            if (ToColor == null)
                para.Add(db.CreateParam("sToColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sToColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, ToColor));

            if (FromClarity == null)
                para.Add(db.CreateParam("sFromClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sFromClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, FromClarity));

            if (ToClarity == null)
                para.Add(db.CreateParam("sToClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sToClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, ToClarity));

            if (FromFls == null)
                para.Add(db.CreateParam("sFromFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sFromFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, FromFls));

            if (ToFls == null)
                para.Add(db.CreateParam("sToFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sToFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, ToFls));

            if (sFromShade == null)
                para.Add(db.CreateParam("sFromShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sFromShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFromShade));

            if (sToShade == null)
                para.Add(db.CreateParam("sToShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sToShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sToShade));

            if (Disc == null)
                para.Add(db.CreateParam("rDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("rDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, Convert.ToSingle(Disc)));

            if (ValDisc == null)
                para.Add(db.CreateParam("rValDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("rValDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, Convert.ToSingle(ValDisc)));

            if (iVendorId == null)
                para.Add(db.CreateParam("iVendorId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("iVendorId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(iVendorId)));

            if (iTransId == null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));

            if (iTrackUser == null)
                para.Add(db.CreateParam("iTrackUser", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("iTrackUser", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iTrackUser));

            if (sTrackIp == null)
                para.Add(db.CreateParam("sTrackIp", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sTrackIp", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTrackIp));

            IDbDataParameter pr = db.CreateParam("iSeq_no", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, DBNull.Value);
            para.Add(pr);

            db.ExecuteSP("CustDiscDet_Insert", para.ToArray(), false);
            iSeq_no = Convert.ToInt64(pr.Value);
            return iSeq_no;
        }
    }
}
