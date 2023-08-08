using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAL
{
    public class TempOrder
    {
        public DataTable TempOrderDet_SelectSummByUserid(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID!=null)
            para.Add(db.CreateParam("iUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("TempOrderDet_SelectSummByUserid", para.ToArray(), false);
            return dt;
        }
        public DataTable TempOrderDet_SelectAllByUserid(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("TempOrderDet_SelectAllByUserid", para.ToArray(), false);
            return dt;
        }
        
    }
}
