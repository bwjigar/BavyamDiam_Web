using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class PartyInfo
    {
        public DataTable PartyInfo_SelectByPara(String sPartyName, String sContactPerson, String sPartyPrifix, Int32? iCountryID, Int32? iPageNO, Int32? iPageSize, String sOrderBy)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sPartyName != null)
                para.Add(db.CreateParam("sPartyName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPartyName));
            else
                para.Add(db.CreateParam("sPartyName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sContactPerson != null)
                para.Add(db.CreateParam("sContactPerson", System.Data.DbType.String, System.Data.ParameterDirection.Input, sContactPerson));
            else
                para.Add(db.CreateParam("sContactPerson", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPartyPrifix != null)
                para.Add(db.CreateParam("sPartyPrefix", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPartyPrifix));
            else
                para.Add(db.CreateParam("sPartyPrefix", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iCountryID != null)
                para.Add(db.CreateParam("sCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iCountryID));
            else
                para.Add(db.CreateParam("sCountryId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNO != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNO));
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

            System.Data.DataTable dt = db.ExecuteSP("PartyInfo_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public void ParttyInfo_Delete(Int32? iiD)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iiD != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iiD));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("ParttyInfo_Delete", para.ToArray(), false);

        }

        public DataTable PartyInfo_SelectOne(Int32? iiD)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iiD != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iiD));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            DataTable dt = db.ExecuteSP("PartyInfo_SelectOne", para.ToArray(), false);
            return dt;
        }

        public Int32? PartyInfo_Insert(String sPartyName, String sPertyPrifix, String sEmail, String sContactNo, String sContactPerson, DateTime? dtCreatedDate, Int32? iLocation, String sPath, ref Int32? iID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sPartyName != null)
                para.Add(db.CreateParam("sPartyName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPartyName));
            else
                para.Add(db.CreateParam("sPartyName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPertyPrifix != null)
                para.Add(db.CreateParam("sPartyPrefix", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPertyPrifix));
            else
                para.Add(db.CreateParam("sPartyPrefix", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sEmail != null)
                para.Add(db.CreateParam("sEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, sEmail));
            else
                para.Add(db.CreateParam("sEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sContactNo != null)
                para.Add(db.CreateParam("sContactNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sContactNo));
            else
                para.Add(db.CreateParam("sContactNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sContactPerson != null)
                para.Add(db.CreateParam("sContactPerson", System.Data.DbType.String, System.Data.ParameterDirection.Input, sContactPerson));
            else
                para.Add(db.CreateParam("sContactPerson", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            if (dtCreatedDate != null)
                para.Add(db.CreateParam("dCreatedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtCreatedDate));
            else
                para.Add(db.CreateParam("dCreatedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iLocation != null)
                para.Add(db.CreateParam("sLocation", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iLocation));
            else
                para.Add(db.CreateParam("sLocation", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPath != null)
                para.Add(db.CreateParam("sPath", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPath));
            else
                para.Add(db.CreateParam("sPath", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            IDbDataParameter pr = db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, DBNull.Value);
            para.Add(pr);
            db.ExecuteSP("PartyInfo_Insert", para.ToArray(), false);
            iID = Convert.ToInt32(pr.Value);
            return iID;
        }

        public void PartyInfo_Update(Int32? iID, String sPartyName, String sPertyPrifix, String sContactPerson, String sEmail, String iLocation, String sPath, String sContactNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iID != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPartyName != null)
                para.Add(db.CreateParam("sPartyName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPartyName));
            else
                para.Add(db.CreateParam("sPartyName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPertyPrifix != null)
                para.Add(db.CreateParam("sPartyPrefix", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPertyPrifix));
            else
                para.Add(db.CreateParam("sPartyPrefix", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sContactPerson != null)
                para.Add(db.CreateParam("sContactPerson", System.Data.DbType.String, System.Data.ParameterDirection.Input, sContactPerson));
            else
                para.Add(db.CreateParam("sContactPerson", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
          
            if (sEmail != null)
                para.Add(db.CreateParam("sEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, sEmail));
            else
                para.Add(db.CreateParam("sEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iLocation != null)
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, iLocation));
            else
                para.Add(db.CreateParam("sLocation", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
          
            if (sPath != null)
                para.Add(db.CreateParam("sPath", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPath));
            else
                para.Add(db.CreateParam("sPath", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sContactNo != null)
                para.Add(db.CreateParam("sContactNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sContactNo));
            else
                para.Add(db.CreateParam("sContactNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            db.ExecuteSP("PartyInfo_Update", para.ToArray(), false);
           
           
        }
    }
}
