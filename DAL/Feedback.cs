using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class Feedback
    {
        public DataTable feedback_userinfo_select(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("p_for_userid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("p_for_userid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("feedback_userinfo_select", para.ToArray(), false);
            return dt;
        }

        public DataTable TitleMas_Select()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            System.Data.DataTable dt = db.ExecuteSP("TitleMas_Select", para.ToArray(), false);
            return dt;
        }

        public DataTable Feedbak_Insert(String sCustomerName, String sCompName, String sEmailID, String sTitle, String sFeedback, DateTime dtTransDate, bool? bIpdFlag ,ref Int32? iID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sCustomerName != "")
                para.Add(db.CreateParam("sCustName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCustomerName));
            else
                para.Add(db.CreateParam("sCustName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCompName != "")
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompName));
            else
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sEmailID != "")
                para.Add(db.CreateParam("sEmailId", System.Data.DbType.String, System.Data.ParameterDirection.Input, sEmailID));
            else
                para.Add(db.CreateParam("sEmailId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sTitle != "")
                para.Add(db.CreateParam("sTitle", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTitle));
            else
                para.Add(db.CreateParam("sTitle", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFeedback != "")
                para.Add(db.CreateParam("sfeedback", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFeedback));
            else
                para.Add(db.CreateParam("sfeedback", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            para.Add(db.CreateParam("dtTransDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtTransDate));
            para.Add(db.CreateParam("IpdFlag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, 0));            
            IDbDataParameter pr = db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, DBNull.Value);
            para.Add(pr);

            System.Data.DataTable dt = db.ExecuteSP("Feedbak_Insert", para.ToArray(), false);
            return dt;
        }

        public DataTable Feedback_SelectAll(Int32? iPageNo, Int32? iPageSize, String sOrderBy)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            
            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOrderBy != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBy));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Feedback_SelectAll", para.ToArray(), false);
            return dt;
        }
    }
}
