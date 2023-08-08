using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ErrorLog
    {
        public DataTable ErrorLog_Select(DateTime? dtFromDate, DateTime? dtToDate, Int64? iUserID, String sFullName, String sUserName, String sCompanyName, String sCountryName, String MSearch, Int32? iPageNo, Int32? iPageSize, String OrderBy)
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

            if (sCountryName != null)
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryName));
            else
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            
            if (MSearch != null)
                para.Add(db.CreateParam("MSearch", System.Data.DbType.String, System.Data.ParameterDirection.Input, MSearch));
            else
                para.Add(db.CreateParam("MSearch", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (OrderBy != null)
                para.Add(db.CreateParam("OrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, OrderBy));
            else
                para.Add(db.CreateParam("OrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("ErrorLog_Select", para.ToArray(), false);
            return dt;
        }

        public void ErrorLog_Insert(DateTime? dtErrorDate, Int64? iUserId, string sIPAddress, string sErrorTrace, string sErrorMsg, string sErrorSite, string sErrorPage)
        {
            try
            {
                System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
                connection.Open();

                SqlCommand para = new SqlCommand("sunrise.ErrorLog_Insert");
                para.CommandText = "sunrise.ErrorLog_Insert";

                para.CommandType = CommandType.StoredProcedure;

                if (dtErrorDate != null)
                    para.Parameters.Add("dtErrorDate", SqlDbType.DateTime).Value = dtErrorDate;
                else
                    para.Parameters.Add("dtErrorDate", SqlDbType.DateTime).Value = DBNull.Value;

                if (iUserId != null)
                    para.Parameters.Add("iUserId", SqlDbType.Int).Value = iUserId;
                else
                    para.Parameters.Add("iUserId", SqlDbType.Int).Value = DBNull.Value;

                if (sIPAddress != null)
                    para.Parameters.Add("sIPAddress", SqlDbType.VarChar).Value = sIPAddress;
                else
                    para.Parameters.Add("sIPAddress", SqlDbType.VarChar).Value = DBNull.Value;

                if (sErrorTrace != null)
                    para.Parameters.Add("sErrorTrace", SqlDbType.VarChar).Value = sErrorTrace;
                else
                    para.Parameters.Add("sErrorTrace", SqlDbType.VarChar).Value = DBNull.Value;

                if (sErrorMsg != null)
                    para.Parameters.Add("sErrorMsg", SqlDbType.VarChar).Value = sErrorMsg;
                else
                    para.Parameters.Add("sErrorMsg", SqlDbType.VarChar).Value = DBNull.Value;

                if (sErrorSite != null)
                    para.Parameters.Add("sErrorSite", SqlDbType.VarChar).Value = sErrorSite;
                else
                    para.Parameters.Add("sErrorSite", SqlDbType.VarChar).Value = DBNull.Value;

                if (sErrorPage != null)
                    para.Parameters.Add("sErrorPage", SqlDbType.VarChar).Value = sErrorPage;
                else
                    para.Parameters.Add("sErrorPage", SqlDbType.VarChar).Value = DBNull.Value;

                para.Connection = connection;
                para.ExecuteNonQuery();
            }
            catch (Exception ex) { }

            //Database db = new Database();
            //System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            //para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            //if (dtErrorDate != null)
            //    para.Add(db.CreateParam("dtErrorDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtErrorDate));
            //else
            //    para.Add(db.CreateParam("dtErrorDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            //if (iUserId != null)
            //    para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            //else
            //    para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));


            //if (sIPAddress != null)
            //    para.Add(db.CreateParam("sIPAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, sIPAddress));
            //else
            //    para.Add(db.CreateParam("sIPAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            //if (sErrorTrace != null)
            //    para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, sErrorTrace));
            //else
            //    para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            //if (sErrorMsg != null)
            //    para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, sErrorMsg));
            //else
            //    para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            //if (sErrorSite != null)
            //    para.Add(db.CreateParam("sErrorSite", System.Data.DbType.String, System.Data.ParameterDirection.Input, sErrorSite));
            //else
            //    para.Add(db.CreateParam("sErrorSite", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            //if (sErrorPage != null)
            //    para.Add(db.CreateParam("sErrorPage", System.Data.DbType.String, System.Data.ParameterDirection.Input, sErrorPage));
            //else
            //    para.Add(db.CreateParam("sErrorPage", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            //db.ExecuteSP("ErrorLog_Insert", para.ToArray(), false);

        }
    }
}
