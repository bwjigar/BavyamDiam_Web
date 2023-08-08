using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class AlertData
    {
        public void AlertDet_Insert(Int64? iUserID, String sRefNo, float? dRapPrice, float? dOrgDisc, float? dExpDisc, float? dOrgNetValue, float? dExpNetValue, DateTime? dtExpiryDate)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dRapPrice != null)
                para.Add(db.CreateParam("dRapPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dRapPrice));
            else
                para.Add(db.CreateParam("dRapPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dOrgDisc != null)
                para.Add(db.CreateParam("dOrgDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dOrgDisc));
            else
                para.Add(db.CreateParam("dOrgDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dExpDisc != null)
                para.Add(db.CreateParam("dExpDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dExpDisc));
            else
                para.Add(db.CreateParam("dExpDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dOrgNetValue != null)
                para.Add(db.CreateParam("dOrgNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dOrgNetValue));
            else
                para.Add(db.CreateParam("dOrgNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dExpNetValue != null)
                para.Add(db.CreateParam("dExpNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dExpNetValue));
            else
                para.Add(db.CreateParam("dExpNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtExpiryDate != null)
                para.Add(db.CreateParam("dtExpiryDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtExpiryDate));
            else
                para.Add(db.CreateParam("dtExpiryDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("AlertDet_Insert", para.ToArray(), false);

        }

        public DataTable AlertDet_SelectByPara(Int32? iID, Int64? iUserID, String sRefNo, DateTime? dtFromDate, DateTime? dtToDate, bool? bIsActive, Int32? iEmpId, Int32? iPgNo, Int32? iPgSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iID != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtFromDate != null)
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtFromDate));
            else
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtToDate != null)
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtToDate));
            else
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsActive != null)
                para.Add(db.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsActive));
            else
                para.Add(db.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iEmpId != null)
                para.Add(db.CreateParam("iEmpId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iEmpId));
            else
                para.Add(db.CreateParam("iEmpId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPgNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPgNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPgSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPgSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("AlertDet_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public void AlertDet_Update(Int32? iID, float? dRapPrice, float? dExpDisc, float? dExpNetValue, DateTime? dtExpiryDate)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iID != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dRapPrice != null)
                para.Add(db.CreateParam("dRapPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dRapPrice));
            else
                para.Add(db.CreateParam("dRapPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dExpDisc != null)
                para.Add(db.CreateParam("dExpDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dExpDisc));
            else
                para.Add(db.CreateParam("dExpDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dExpNetValue != null)
                para.Add(db.CreateParam("dExpNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dExpNetValue));
            else
                para.Add(db.CreateParam("dExpNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtExpiryDate != null)
                para.Add(db.CreateParam("dtExpiryDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtExpiryDate));
            else
                para.Add(db.CreateParam("dtExpiryDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("AlertDet_Update", para.ToArray(), false);
        }

        public void AlertDet_Delete(Int64? iID, Int64? iUserID, String sRefNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iID != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("AlertDet_Delete", para.ToArray(), false);

        }
    }
}
