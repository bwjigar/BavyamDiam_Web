using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class Calendar
    {
        public DataTable Cal_Event_insert(string sEventName, DateTime? dtStartDate, DateTime? dtEndtime, string sDesc, string sImage, Int32? iUserID, string sDeviceType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sEventName != null)
                para.Add(db.CreateParam("EventName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sEventName));
            else
                para.Add(db.CreateParam("EventName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtStartDate != null)
                para.Add(db.CreateParam("StartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtStartDate));
            else
                para.Add(db.CreateParam("StartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtEndtime != null)
                para.Add(db.CreateParam("EndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtEndtime));
            else
                para.Add(db.CreateParam("EndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sDesc != null)
                para.Add(db.CreateParam("Description", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDesc));
            else
                para.Add(db.CreateParam("Description", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sImage != null)
                para.Add(db.CreateParam("Image", System.Data.DbType.String, System.Data.ParameterDirection.Input, sImage));
            else
                para.Add(db.CreateParam("Image", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserID != null)
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sDeviceType != null)
                para.Add(db.CreateParam("DeviseType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDeviceType));
            else
                para.Add(db.CreateParam("DeviseType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("Cal_Event_ins", para.ToArray(), false);
            return dt;
        }


        public DataTable Cal_Event_Delete(Int32? iID, Int32? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iID != null)
                para.Add(db.CreateParam("id", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("id", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));


            System.Data.DataTable dt = db.ExecuteSP("Cal_Event_Delete", para.ToArray(), false);
            return dt;
        }

        public DataTable Cal_Event_Update(Int32? iID, string sEventName, DateTime? dtStartDate, DateTime? dtEndtime, string sDesc, string sImage, Int32? iUserID, string sDeviceType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iID != null)
                para.Add(db.CreateParam("id", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("id", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sEventName != null)
                para.Add(db.CreateParam("EventName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sEventName));
            else
                para.Add(db.CreateParam("EventName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtStartDate != null)
                para.Add(db.CreateParam("StartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtStartDate));
            else
                para.Add(db.CreateParam("StartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtEndtime != null)
                para.Add(db.CreateParam("EndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtEndtime));
            else
                para.Add(db.CreateParam("EndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sDesc != null)
                para.Add(db.CreateParam("Description", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDesc));
            else
                para.Add(db.CreateParam("Description", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sImage != null)
                para.Add(db.CreateParam("Image", System.Data.DbType.String, System.Data.ParameterDirection.Input, sImage));
            else
                para.Add(db.CreateParam("Image", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserID != null)
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sDeviceType != null)
                para.Add(db.CreateParam("DeviseType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDeviceType));
            else
                para.Add(db.CreateParam("DeviseType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("Cal_Event_Update", para.ToArray(), false);
            return dt;
        }

        public DataTable Cal_Event_selectByPara(Int32? iID, string sEventName, DateTime? dtStartDate, DateTime? dtEndtime, DateTime? dtDate, Int32? iMonth, Int32? iYear, string sDesc, string sImage, Int32? iUserID, Int32? iPageNo, Int32? iPageSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iID != null)
                para.Add(db.CreateParam("id", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("id", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sEventName != null)
                para.Add(db.CreateParam("EventName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sEventName));
            else
                para.Add(db.CreateParam("EventName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtStartDate != null)
                para.Add(db.CreateParam("StartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtStartDate));
            else
                para.Add(db.CreateParam("StartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtEndtime != null)
                para.Add(db.CreateParam("EndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtEndtime));
            else
                para.Add(db.CreateParam("EndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtDate != null)
                para.Add(db.CreateParam("Date", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtDate));
            else
                para.Add(db.CreateParam("Date", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iMonth != null)
                para.Add(db.CreateParam("month", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iMonth));
            else
                para.Add(db.CreateParam("month", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iYear != null)
                para.Add(db.CreateParam("year", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iYear));
            else
                para.Add(db.CreateParam("year", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));


            if (sDesc != null)
                para.Add(db.CreateParam("Description", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDesc));
            else
                para.Add(db.CreateParam("Description", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sImage != null)
                para.Add(db.CreateParam("Image", System.Data.DbType.String, System.Data.ParameterDirection.Input, sImage));
            else
                para.Add(db.CreateParam("Image", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserID != null)
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Cal_Event_selectByPara", para.ToArray(), false);
            return dt;
        }


    }
}
