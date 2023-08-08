using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class LoginLog
    {
        public DataTable LoginLog_SelectByUserId(DateTime? dtFromDate, DateTime? dtToDate, Int64? iUserID, String sCountry, String sFullName, String sUserName, String sCompanyName, Int32? iPageNo, Int32? iPageSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (dtFromDate != null)
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtFromDate));
            else
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtToDate != null)
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtToDate));
            else
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCountry != null)
                para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountry));
            else
                para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFullName != null)
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFullName));
            else
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sUserName != null)
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserName));
            else
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCompanyName != null)
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompanyName));
            else
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            DataTable dt = db.ExecuteSP("LoginLog_SelectByUserId", para.ToArray(), false);
            return dt;
        }

        public DataTable LoginLog_Select_UDID(Int64? iUserId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserId != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("LoginLog_Select_UDID", para.ToArray(), false);
            return dt;
        }

        public DataTable LoginLog_SelectLoginType()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            System.Data.DataTable dt = db.ExecuteSP("LoginLog_SelectLoginType", para.ToArray(), false);
            return dt;
        }

        public DataTable LoginLog_ForChart(String sGroupBY, DateTime? dtFromDate, DateTime? dtToDate, String sCountry, String iUserID, bool? bIsTop)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sGroupBY != null)
                para.Add(db.CreateParam("sGroupBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sGroupBY));
            else
                para.Add(db.CreateParam("sGroupBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtFromDate != null)
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtFromDate));
            else
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtToDate != null)
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtToDate));
            else
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            
            if (sCountry != null)
                para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountry));
            else
                para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsTop != null)
                para.Add(db.CreateParam("bIsTop10", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsTop));
            else
                para.Add(db.CreateParam("bIsTop10", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("LoginLog_ForChart", para.ToArray(), false);
            return dt;
        }
    }
}
