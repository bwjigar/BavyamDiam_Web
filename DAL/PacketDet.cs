using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public  class PacketDet
    {
        public DataTable PacketDet_SelectOne(string sRefNo, Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sRefNo != null)
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            else
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("PacketDet_SelectOne", para.ToArray(), false);
            return dt;
        }


        public DataTable PacketDet_SelectOneBySeqNo(Int64? iSeqNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iSeqNo != null)
                para.Add(db.CreateParam("@ISeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iSeqNo));
            else
                para.Add(db.CreateParam("@ISeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("PacketDet_SelectOneBySeqNo", para.ToArray(), false);
            return dt;
        }
    }
}
