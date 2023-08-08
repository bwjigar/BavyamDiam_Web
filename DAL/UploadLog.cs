using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class UploadLog
    {
        public DataTable GetLastUpdate()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();           
            System.Data.DataTable dt = db.ExecuteSP("GetLastUpdate", para.ToArray(), false);
            return dt;
        }
    }
}
