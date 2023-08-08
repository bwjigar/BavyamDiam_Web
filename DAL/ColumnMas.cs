using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class ColumnMas
    {
        public DataTable ColumnConfDet_Select(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("UserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("ColumnConfDet_Select", para.ToArray(), false);
            return dt;
        }

        public DataTable ColumnMas_Select()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("ColumnMas_Select", para.ToArray(), false);
            return dt;
        }

        public Int64? ColumnConfMas_Insert(String iParaType, String iParaValue, bool? bIsDefault, ref Int64? iTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iParaType != null)
                para.Add(db.CreateParam("iParaType", System.Data.DbType.String, System.Data.ParameterDirection.Input, iParaType));
            else
                para.Add(db.CreateParam("iParaType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iParaValue != null)
                para.Add(db.CreateParam("iParaValue", System.Data.DbType.String, System.Data.ParameterDirection.Input, iParaValue));
            else
                para.Add(db.CreateParam("iParaValue", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsDefault != null)
                para.Add(db.CreateParam("isDefault", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsDefault));
            else
                para.Add(db.CreateParam("isDefault", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            IDbDataParameter pr = db.CreateParam("iTransId", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, DBNull.Value);
            para.Add(pr);

            System.Data.DataTable dt = db.ExecuteSP("ColumnConfMas_Insert", para.ToArray(), false);
            iTransId = Convert.ToInt32(pr.Value);
            return iTransId;

        }

        public void ColumnConfDet_Insert(Int64? iTransId, Int32? iColumnId, Int32? iPriority)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iColumnId != null)
                para.Add(db.CreateParam("iColumnId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iColumnId));
            else
                para.Add(db.CreateParam("iColumnId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPriority != null)
                para.Add(db.CreateParam("iPriority", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPriority));
            else
                para.Add(db.CreateParam("iPriority", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("ColumnConfDet_Insert", para.ToArray(), false);



        }

        public DataTable ColumnConfDet_Select_ByPara(String sParaType, String sParaValue, bool? bIsDefault)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (sParaType != null)
                para.Add(db.CreateParam("ParaType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sParaType));
            else
                para.Add(db.CreateParam("ParaType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sParaValue != null)
                para.Add(db.CreateParam("ParaValue", System.Data.DbType.String, System.Data.ParameterDirection.Input, sParaValue));
            else
                para.Add(db.CreateParam("ParaValue", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsDefault != null)
                para.Add(db.CreateParam("isDefault", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsDefault));
            else
                para.Add(db.CreateParam("isDefault", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));


            System.Data.DataTable dt = db.ExecuteSP("ColumnConfDet_Select_ByPara", para.ToArray(), false);
            
            return dt;
        }
    }
}
