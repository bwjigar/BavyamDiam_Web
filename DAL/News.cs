using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAL
{
    public class News
    {
        public DataTable NewsMas_Select(Int64? iId, Int64? IsActive, Int64? iPgNo, Int64? iPgSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iId != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iId));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (IsActive != null)
                para.Add(db.CreateParam("IsActive", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, IsActive));
            else
                para.Add(db.CreateParam("IsActive", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPgNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iPgNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPgSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iPgSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("NewsMas_Select", para.ToArray(), false);
            return dt;
        }
        public void NewsMas_Insert(String sDescription, DateTime? dtStartDate, DateTime? dtEndDate, String sColor, String sColorRgb)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sDescription != null)
                para.Add(db.CreateParam("sDescription", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDescription));
            else
                para.Add(db.CreateParam("sDescription", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtStartDate != null)
                para.Add(db.CreateParam("dtStartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtStartDate));
            else
                para.Add(db.CreateParam("dtStartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtEndDate != null)
                para.Add(db.CreateParam("dtEndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtEndDate));
            else
                para.Add(db.CreateParam("dtEndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColorRgb != null)
                para.Add(db.CreateParam("sColorRgb", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColorRgb));
            else
                para.Add(db.CreateParam("sColorRgb", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("NewsMas_Insert", para.ToArray(), false);

        }

        public void Newsmas_Update(Int32? iID, String sDescription, DateTime? dtStartDate, DateTime? dtEndDate, String sColor, String sColorRgb)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iID != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sDescription != null)
                para.Add(db.CreateParam("sDescription", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDescription));
            else
                para.Add(db.CreateParam("sDescription", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtStartDate != null)
                para.Add(db.CreateParam("dtStartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtStartDate));
            else
                para.Add(db.CreateParam("dtStartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtEndDate != null)
                para.Add(db.CreateParam("dtEndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtEndDate));
            else
                para.Add(db.CreateParam("dtEndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sColorRgb != null)
                para.Add(db.CreateParam("sColorRgb", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColorRgb));
            else
                para.Add(db.CreateParam("sColorRgb", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("Newsmas_Update", para.ToArray(), false);

        }

        public void Newsmas_Delete(Int32? iId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iId != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iId));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("Newsmas_Delete", para.ToArray(), false);

        }
        public DataTable News_Select_By_Id()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            DataTable dt = db.ExecuteSP("IPD_NewsMas_Select", para.ToArray(), false);
            return dt;
        }
        public DataTable News_SelectAllByType(bool? bIsType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (bIsType != null)
                para.Add(db.CreateParam("bType", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsType));
            else
                para.Add(db.CreateParam("bType", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            DataTable dt = db.ExecuteSP("News_SelectAllByType", para.ToArray(), false);
            return dt;
        }
    }
}
