using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAL
{
   public class Fair
    {
        public DataTable FairDate_det_select(DateTime dtDate)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (dtDate!=null)
            para.Add(db.CreateParam("@dtDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtDate));
            else
                para.Add(db.CreateParam("@dtDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("FairDate_det_select", para.ToArray(), false);
            return dt;
        }

    }
}
