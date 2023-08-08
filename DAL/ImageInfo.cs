using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAL
{
    public class ImageInfo
    {
        public DataTable ImageInfo_SelectByPara(Int64? iId, string sTitle, Int64? IsActive, Int64? iPgNo, Int64? iPgSize, string sType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iId != null)
            {
                para.Add(db.CreateParam("@iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iId));
            }
            else
            { para.Add(db.CreateParam("@iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value)); }
            if (sTitle != null)
            {
                para.Add(db.CreateParam("@sTitle", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, sTitle));
            }
            else
            {
                para.Add(db.CreateParam("@sTitle", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            }
            if (IsActive != null)
                para.Add(db.CreateParam("@IsActive", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, IsActive));
            else
                para.Add(db.CreateParam("@IsActive", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPgNo != null)
                para.Add(db.CreateParam("@iPgNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iPgNo));
            else
                para.Add(db.CreateParam("@iPgNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPgSize != null)
                para.Add(db.CreateParam("@iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iPgSize));
            else
                para.Add(db.CreateParam("@iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            // Change By Hitesh on [11-03-2017] as Per [Doc No 691]
            if (sType != null)
                para.Add(db.CreateParam("@sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sType));
            else
                para.Add(db.CreateParam("@sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            // End By Hitesh on [11-03-2017] as Per [Doc No 691]

            System.Data.DataTable dt = db.ExecuteSP("ImageInfo_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public void ImageInfo_Delete(Int32? iID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iID != null)
            {
                para.Add(db.CreateParam("iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iID));
            }
            else
            {
                para.Add(db.CreateParam("iId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            }

            db.ExecuteSP("ImageInfo_Delete", para.ToArray(), false);

        }

        public void ImageInfo_Insert(String sTitle, DateTime? dtStartDate, DateTime? dtEndDate, String sImagePath, string sType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sTitle != null)
                para.Add(db.CreateParam("sTitle", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTitle));
            else
                para.Add(db.CreateParam("sTitle", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtStartDate != null)
                para.Add(db.CreateParam("dtStartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtStartDate));
            else
                para.Add(db.CreateParam("dtStartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtEndDate != null)
                para.Add(db.CreateParam("dtEndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtEndDate));
            else
                para.Add(db.CreateParam("dtEndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sImagePath != null)
                para.Add(db.CreateParam("sImagePath", System.Data.DbType.String, System.Data.ParameterDirection.Input, sImagePath));
            else
                para.Add(db.CreateParam("sImagePath", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            // Change By Hitesh on [10-03-2017] as Per [Doc No 691]
            if (sType != null)
                para.Add(db.CreateParam("@sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sType));
            else
                para.Add(db.CreateParam("@sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            // End By Hitesh on [10-03-2017] as Per [Doc No 691]

            db.ExecuteSP("ImageInfo_Insert", para.ToArray(), false);
        }

        public void ImageInfo_Update(Int32? iID, String sTitle, DateTime? dtStartDate, DateTime? dtEndDate, String sImagePath, string sType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iID != null)
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iID));
            else
                para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sTitle != null)
                para.Add(db.CreateParam("sTitle", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTitle));
            else
                para.Add(db.CreateParam("sTitle", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtStartDate != null)
                para.Add(db.CreateParam("dtStartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtStartDate));
            else
                para.Add(db.CreateParam("dtStartDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtEndDate != null)
                para.Add(db.CreateParam("dtEndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtEndDate));
            else
                para.Add(db.CreateParam("dtEndDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sImagePath != null)
                para.Add(db.CreateParam("sImagePath", System.Data.DbType.String, System.Data.ParameterDirection.Input, sImagePath));
            else
                para.Add(db.CreateParam("sImagePath", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            // Change By Hitesh on [11-03-2017] as Per [Doc No 691]
            if (sType != null)
                para.Add(db.CreateParam("@sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sType));
            else
                para.Add(db.CreateParam("@sType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            // End By Hitesh on [11-03-2017] as Per [Doc No 691]

            db.ExecuteSP("ImageInfo_Update", para.ToArray(), false);
        }
    }
}
