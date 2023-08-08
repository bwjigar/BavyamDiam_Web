using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class OfferCriteria
    {
        public DataTable OfferCriteria_Select()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("OfferCriteria_Select", para.ToArray(), false);
            return dt;
        }

        public DataTable OfferCriteria_UpdateOffer(decimal OfferPer)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (OfferPer != null)
                para.Add(db.CreateParam("OfferPer", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, OfferPer));
            else
                para.Add(db.CreateParam("OfferPer", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("OfferCriteria_UpdateOffer", para.ToArray(), false);
            return dt;
        }
    }
}
