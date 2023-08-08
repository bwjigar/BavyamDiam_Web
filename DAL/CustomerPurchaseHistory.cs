using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class CustomerPurchaseHistory
    {
        public DataTable CustPurchaseHistoryDet_SelectByPara(Int32? iTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.String, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("CustPurchaseHistoryDet_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public DataTable CustPurchaseHistoryMas_SelectByPara(Int32? iMonth, Int32? iYear, Int64? iTransID, Int64? iUserID, String sFullName, String sUserName, String sCompanyName,
            String sCountryName, Int32? iAssistByID, Int32? iPageNo, Int32? iPageSize, Int32? iUserType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iMonth != null)
                para.Add(db.CreateParam("dtMonth", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iMonth));
            else
                para.Add(db.CreateParam("dtMonth", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iYear != null)
                para.Add(db.CreateParam("dtYear", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iYear));
            else
                para.Add(db.CreateParam("dtYear", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

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

            if (iAssistByID != null)
                para.Add(db.CreateParam("AssistById", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iAssistByID));
            else
                para.Add(db.CreateParam("AssistById", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserType != null)
                para.Add(db.CreateParam("iUserType", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserType));
            else
                para.Add(db.CreateParam("iUserType", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("CustPurchaseHistoryMas_SelectByPara", para.ToArray(), false);
            return dt;
        }
    }
}
