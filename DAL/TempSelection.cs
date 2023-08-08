using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class TempSelection
    {
        public void TempSelection_DeleteTempData(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("TempSelection_DeleteTempData", para.ToArray(), false);
        }

        public DataTable TempSelectionForCart_Select(Int64? iUserID, String sSessionID, bool? bIsAlert, bool? bIsCart)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSessionID != null)
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSessionID));
            else
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsAlert != null)
                para.Add(db.CreateParam("bIsAlert", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsAlert));
            else
                para.Add(db.CreateParam("bIsAlert", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsCart != null)
                para.Add(db.CreateParam("bIsCart", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsCart));
            else
                para.Add(db.CreateParam("bIsCart", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("TempSelectionForCart_Select", para.ToArray(), false);
            return dt;
        }

        public DataTable TemporderDet_InsertById(String sRefNO, Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sRefNO != null)
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNO));
            else
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("TemporderDet_InsertById", para.ToArray(), false);
            return dt;
        }

        public DataTable TempOrderDet_SelectAllByUserid(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iUserID != null)
                para.Add(db.CreateParam("iUserid", System.Data.DbType.String, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserid", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("TempOrderDet_SelectAllByUserid", para.ToArray(), false);
            return dt;
        }

        public DataTable TempSelection_Select(Int64? iUserID, String sSessionID, Int32? iPageNo, String sModuleName, bool? bIsAlert, bool? bIsCart)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSessionID != null)
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSessionID));
            else
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPageNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPageNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sModuleName != null)
                para.Add(db.CreateParam("sModuleName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sModuleName));
            else
                para.Add(db.CreateParam("sModuleName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsAlert != null)
                para.Add(db.CreateParam("bIsAlert", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsAlert));
            else
                para.Add(db.CreateParam("bIsAlert", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsCart != null)
                para.Add(db.CreateParam("bIsCart", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsCart));
            else
                para.Add(db.CreateParam("bIsCart", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("TempSelection_Select", para.ToArray(), false);
            return dt;
        }

        public void TempSelection_Delete(Int64? iUserID, String sSessionID, String sModuleName, String sRefNo, bool? bIsAlert, bool? bIsCart)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sSessionID != null)
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSessionID));
            else
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            if (sModuleName != null)
                para.Add(db.CreateParam("sModuleName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sModuleName));
            else
                para.Add(db.CreateParam("sModuleName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            if (bIsAlert != null)
                para.Add(db.CreateParam("bIsAlert", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsAlert));
            else
                para.Add(db.CreateParam("bIsAlert", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsCart != null)
                para.Add(db.CreateParam("bIsCart", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsCart));
            else
                para.Add(db.CreateParam("bIsCart", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("TempSelection_Delete", para.ToArray(), false);
        }
        public void TempSelection_Insert(Int64? iUserID, String sRefNo, String sSessionID, Int32? iPageNo, String sModuleName, bool? bIsAlert, bool? bIsCart, float? dAlertRapPrice,
            float? dalertOrgDisc, float? dAlertExpDisc, float? dAlertOrgNetValue, float? dAlertExpNetValue, DateTime? dtAlertExpiryDate)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sRefNo != null)
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            if (sSessionID != null)
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSessionID));
            else
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPageNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPageNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));


            if (sModuleName != null)
                para.Add(db.CreateParam("sModuleName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sModuleName));
            else
                para.Add(db.CreateParam("sModuleName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsAlert != null)
                para.Add(db.CreateParam("bIsAlert", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsAlert));
            else
                para.Add(db.CreateParam("bIsAlert", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsCart != null)
                para.Add(db.CreateParam("bIsCart", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsCart));
            else
                para.Add(db.CreateParam("bIsCart", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dAlertRapPrice != null)
                para.Add(db.CreateParam("Alert_dRapPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dAlertRapPrice));
            else
                para.Add(db.CreateParam("Alert_dRapPrice", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dalertOrgDisc != null)
                para.Add(db.CreateParam("Alert_dOrgDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dalertOrgDisc));
            else
                para.Add(db.CreateParam("Alert_dOrgDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dAlertExpDisc != null)
                para.Add(db.CreateParam("Alert_dExpDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dAlertExpDisc));
            else
                para.Add(db.CreateParam("Alert_dExpDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dAlertOrgNetValue != null)
                para.Add(db.CreateParam("Alert_dOrgNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dAlertOrgNetValue));
            else
                para.Add(db.CreateParam("Alert_dOrgNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dAlertExpNetValue != null)
                para.Add(db.CreateParam("Alert_dExpNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dAlertExpNetValue));
            else
                para.Add(db.CreateParam("Alert_dExpNetValue", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtAlertExpiryDate != null)
                para.Add(db.CreateParam("Alert_dtExpiryDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtAlertExpiryDate));
            else
                para.Add(db.CreateParam("Alert_dtExpiryDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("TempSelection_Insert", para.ToArray(), false);
        }
        public void TempOrderDet_Delete(Int64? iUserID, String sRefNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iiUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iiUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sRefNo != null)
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("TempOrderDet_Delete", para.ToArray(), false);

        }
    }
}
