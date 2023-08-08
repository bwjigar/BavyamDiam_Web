using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class Notification
    {
        public DataTable CustomerDemandDet_Select_Stock_For_Notification(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("CustomerDemandDet_Select_Stock_For_Notification", para.ToArray(), false);
            return dt;
        }
        public DataTable UserSearchDet_Select_Stock_For_Notification(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("UserSearchDet_Select_Stock_For_Notification", para.ToArray(), false);
            return dt;
        }
        public DataTable NotificationDet_SelectByPara(Int64? iTransID, Int64? iUSerID, bool? bIsActive, String sDisplayType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUSerID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUSerID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            para.Add(db.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsActive));
            if (sDisplayType != null)
                para.Add(db.CreateParam("sDisplayType", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, sDisplayType));
            else
                para.Add(db.CreateParam("sDisplayType", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("NotificationDet_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public void CustomerDemandDet_Update_LSeqNo_For_Notification(Int64? iDemandID, Int64? iLastSeqNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iDemandID != null)
                para.Add(db.CreateParam("iDemandId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iDemandID));
            else
                para.Add(db.CreateParam("iDemandId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iLastSeqNo != null)
                para.Add(db.CreateParam("iLSeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iLastSeqNo));
            else
                para.Add(db.CreateParam("iLSeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("CustomerDemandDet_Update_LSeqNo_For_Notification", para.ToArray(), false);
        }

        public void UserSearchDet_Update_LSeqNo_For_Notification(Int64? iSearchID, Int64? iLastSeqNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iSearchID != null)
                para.Add(db.CreateParam("iSearchId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iSearchID));
            else
                para.Add(db.CreateParam("iSearchId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iLastSeqNo != null)
                para.Add(db.CreateParam("iLSeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iLastSeqNo));
            else
                para.Add(db.CreateParam("iLSeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("UserSearchDet_Update_LSeqNo_For_Notification", para.ToArray(), false);
        }

        public void NotificationDet_Delete(Int64? iTransID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("NotificationDet_Delete", para.ToArray(), false);
        }

        public void NotificationMas_Delete(Int64? iTransID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("NotificationMas_Delete", para.ToArray(), false);
        }


        public DataTable NotificationMas_SelectByPara(Int64? iTransID, String sNotificationName, DateTime? dtFromDate, DateTime? dtToDate, bool? bIsActive, Int32? iPageNo, Int32? iPageSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sNotificationName != null)
                para.Add(db.CreateParam("sNotificationName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sNotificationName));
            else
                para.Add(db.CreateParam("sNotificationName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

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

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            DataTable dt = db.ExecuteSP("NotificationMas_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public void NotificationMas_Update(Int64? iTransID, String sName, String sMessage, Int32? iValidDays, String sDisplayType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sName != null)
                para.Add(db.CreateParam("sName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sName));
            else
                para.Add(db.CreateParam("sName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sMessage != null)
                para.Add(db.CreateParam("sMessage", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMessage));
            else
                para.Add(db.CreateParam("sMessage", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iValidDays != null)
                para.Add(db.CreateParam("iValidDays", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iValidDays));
            else
                para.Add(db.CreateParam("iValidDays", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sDisplayType != null)
                para.Add(db.CreateParam("sDisplayType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDisplayType));
            else
                para.Add(db.CreateParam("sDisplayType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("NotificationMas_Update", para.ToArray(), false);

        }

        public void NotificationDet_Insert(Int64? iTransID, Int64? iUSerID, String sType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUSerID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUSerID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sType != null)
                para.Add(db.CreateParam("sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sType));
            else
                para.Add(db.CreateParam("sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("NotificationDet_Insert", para.ToArray(), false);

        }

        public Int64? NotificationMas_Insert(String sName, String sMessage, Int32? iValidDays, String sDisplayType, ref Int64? iTransID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sName != null)
                para.Add(db.CreateParam("sName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sName));
            else
                para.Add(db.CreateParam("sName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sMessage != null)
                para.Add(db.CreateParam("sMessage", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMessage));
            else
                para.Add(db.CreateParam("sMessage", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iValidDays != null)
                para.Add(db.CreateParam("iValidDays", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iValidDays));
            else
                para.Add(db.CreateParam("iValidDays", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sDisplayType != null)
                para.Add(db.CreateParam("sDisplayType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDisplayType));
            else
                para.Add(db.CreateParam("sDisplayType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            IDbDataParameter pr = db.CreateParam("iTransId", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, DBNull.Value);
            para.Add(pr);

            db.ExecuteSP("NotificationMas_Insert", para.ToArray(), false);
            iTransID = Convert.ToInt32(pr.Value);
            return iTransID;
        }

        public DataTable NotificationDet_SelectForPublish(Int64? iTransID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransID != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransID));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            DataTable dt = db.ExecuteSP("NotificationDet_SelectForPublish", para.ToArray(), false);
            return dt;
        }

    }
}
