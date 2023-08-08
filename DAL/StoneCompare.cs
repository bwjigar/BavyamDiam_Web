using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class StoneCompare
    {
        public void StoneComparison_Delete(Int64 iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("StoneComparison_Delete", para.ToArray(), false);

        }

        public void StoneComparison_Insert(Int64? iUserID, String sRefNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("StoneComparison_Insert", para.ToArray(), false);
        }

        public DataTable StoneComparison_SelectBypara(Int32? iUserid, String sRefNo, String sCertiNo, Int64? iSeqNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserid != null)
                para.Add(db.CreateParam("iUserid", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserid));
            else
                para.Add(db.CreateParam("iUserid", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCertiNo != null)
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCertiNo));
            else
                para.Add(db.CreateParam("sCertiNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iSeqNo != null)
                para.Add(db.CreateParam("iSeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iSeqNo));
            else
                para.Add(db.CreateParam("iSeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            DataTable dtStone = db.ExecuteSP("StoneComparison_SelectBypara", para.ToArray(), false);
            return dtStone;
        }
    }
}
