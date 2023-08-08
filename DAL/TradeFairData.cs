using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public  class TradeFairData
    {
        public DataTable IPD_Trade_Fair_SelectAll(Int32? iUserCode, Int32? iPageNo, Int32? iPageSize)
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


            System.Data.DataTable dt = db.ExecuteSP("IPD_Trade_Fair_SelectAll", para.ToArray(), false);
            return dt;
        }
    }
}
