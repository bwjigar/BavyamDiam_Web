using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class HoldStone
    {
        public DataTable HoldVerifyStone(String sRefNo, Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("HoldVerifyStone", para.ToArray(), false);
            return dt;
        }
        public DataTable HoldPartyList()    
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("HoldPartyList", para.ToArray(), false);
            return dt;
        }
        public DataTable HoldStock(String sHoldBy)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sHoldBy != null)
                para.Add(db.CreateParam("vHoldBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sHoldBy));
            else
                para.Add(db.CreateParam("vHoldBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("HoldStock", para.ToArray(), false);
            return dt;
        }
    }
}
