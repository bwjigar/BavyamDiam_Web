using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class CountryMas
    {
        public DataTable CountryMas_SelectAll(bool? bIsActive, String sContryName, Int32? iPgNo, Int32? iPgSize, bool? bIsExRate)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (bIsActive != null)
                para.Add(db.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsActive));
            else
                para.Add(db.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sContryName != null)
                para.Add(db.CreateParam("sContryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sContryName));
            else
                para.Add(db.CreateParam("sContryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPgNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPgNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPgSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPgSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsExRate != null)
                para.Add(db.CreateParam("bIsExRate", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsExRate));
            else
                para.Add(db.CreateParam("bIsExRate", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("CountryMas_SelectAll", para.ToArray(), false);
            return dt;
        }

        public DataTable CountryMas_SelectOne(Int32? iCountryID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iCountryID != null)
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iCountryID));
            else
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("CountryMas_SelectOne", para.ToArray(), false);
            return dt;
        }

        public DataTable Country_List()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("Country_List", para.ToArray(), false);
            return dt;
        }

        public DataTable Company_List()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("Company_List", para.ToArray(), false);
            return dt;
        }

        public DataTable CountryMas_Delete(Int32? iCountryID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iCountryID != null)
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iCountryID));
            else
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("CountryMas_Delete", para.ToArray(), false);
            return dt;
        }

        public DataTable CountryMas_Insert(String sCountryCode, String sCountryName, bool? bIsActive, ref Int32? iCountryID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sCountryCode != null)
                para.Add(db.CreateParam("ssCountryCode", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryCode));
            else
                para.Add(db.CreateParam("ssCountryCode", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCountryName != null)
                para.Add(db.CreateParam("ssCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryName));
            else
                para.Add(db.CreateParam("ssCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsActive != null)
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsActive));
            else
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iCountryID != null)
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iCountryID));
            else
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("CountryMas_Insert", para.ToArray(), false);
            return dt;
        }

        public DataTable CountryMas_Update(Int32? iCountryID, String sCountryCode, String sCountryName, bool? bIsActive)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iCountryID != null)
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iCountryID));
            else
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCountryCode != null)
                para.Add(db.CreateParam("ssCountryCode", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryCode));
            else
                para.Add(db.CreateParam("ssCountryCode", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCountryName != null)
                para.Add(db.CreateParam("ssCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryName));
            else
                para.Add(db.CreateParam("ssCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsActive != null)
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsActive));
            else
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("CountryMas_Update", para.ToArray(), false);
            return dt;
        }

        public DataTable ContryMas_UpdateExRate(Int32? iCountryID, String sSymbol, decimal? iExcRate)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iCountryID != null)
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iCountryID));
            else
                para.Add(db.CreateParam("iiCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSymbol != null)
                para.Add(db.CreateParam("sSymbol", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymbol));
            else
                para.Add(db.CreateParam("sSymbol", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iExcRate != null)
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, iExcRate));
            else
                para.Add(db.CreateParam("dExRate", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("ContryMas_UpdateExRate", para.ToArray(), false);
            return dt;
        }

        public DataTable get_CompanyName_by_user(String sUserName, String sCompanyName, String iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sUserName != null)
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserName));
            else
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCompanyName != null)
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompanyName));
            else
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("get_CompanyName_by_user", para.ToArray(), false);
            return dt;
        }
    }
}
