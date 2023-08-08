using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class Order
    {
        public DataTable OrderMas_Update_CustMail_Status(Int64? iOrderId, bool? bMailFlag)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderId != null)
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderId));
            else
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (bMailFlag != null)
                para.Add(db.CreateParam("bCustMailFlag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bMailFlag));
            else
                para.Add(db.CreateParam("bCustMailFlag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("OrderMas_Update_CustMail_Status", para.ToArray(), false);
            return dt;
        }

        public DataTable OrderMas_Update_AdminMail_Status(Int64? iOrderId, bool? bAdminMailFlag)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderId != null)
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderId));
            else
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (bAdminMailFlag != null)
                para.Add(db.CreateParam("bAdminMailFlag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bAdminMailFlag));
            else
                para.Add(db.CreateParam("bAdminMailFlag", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("OrderMas_Update_AdminMail_Status", para.ToArray(), false);
            return dt;
        }

        public DataTable OrderDet_SelectAllByOrderId(Int64? iOrderID, Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderID != null)
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderID));
            else
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserID != null)
                para.Add(db.CreateParam("iuserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iuserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("OrderDet_SelectAllByOrderId", para.ToArray(), false);
            return dt;
        }

        public DataTable OrderMas_SelectByPara(int? iOrderId, byte? iOrderStatus, int? iUserId, int? iEmployeeId, DateTime? dtFromDate, DateTime? dtToDate, short? iPgNo, short? iPgSize, string sOrderBy, string sFullName, string sUserName, string sCountryName, string sCompName)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderId != null)
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderId));
            else
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iOrderStatus != null)
                para.Add(db.CreateParam("iOrderStatus", System.Data.DbType.Byte, System.Data.ParameterDirection.Input, iOrderStatus));
            else
                para.Add(db.CreateParam("iOrderStatus", System.Data.DbType.Byte, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserId != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iEmployeeId != null)
                para.Add(db.CreateParam("iEmployeeId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iEmployeeId));
            else
                para.Add(db.CreateParam("iEmployeeId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dtFromDate != null)
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtFromDate));
            else
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dtToDate != null)
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtToDate));
            else
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPgNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPgNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPgSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPgSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sOrderBy != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBy));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sFullName != null)
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFullName));
            else
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sUserName != null)
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserName));
            else
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCountryName != null)
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryName));
            else
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCompName != null)
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompName));
            else
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("OrderMas_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public DataTable OrderMas_SelectOne(Int64 iOrderID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderID != null)
                para.Add(db.CreateParam("iiOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderID));
            else
                para.Add(db.CreateParam("iiOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("OrderMas_SelectOne", para.ToArray(), false);
            return dt;
        }

        public void OrderDet_InsertFromTempOrder(Int64? iOrderID, Int64? iUserID, String RefNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderID != null)
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderID));
            else
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (RefNo != "")
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, RefNo));
            else
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("OrderDet_InsertFromTempOrder", para.ToArray(), false);

        }

        public Int64? OrderMas_Insert(Int64? iiUserId, DateTime? dadtOrderDate, byte? byiOrderStatus, string ssCustomerNote, string ssAdminNote, double? fdTotal, double? dOffer, string ssFirstName, string ssLastName, string ssAddress, string ssCity, string ssZipcode, string ssState, string ssCountry, string ssPhone, string ssMobile, string ssEmail, Int64? iiModifiedBy, DateTime? dadtModifiedDate, bool? bbIsDeleted, ref Int64? iiOrderid)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iiUserId != null)
                para.Add(db.CreateParam("iiUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iiUserId));
            else
                para.Add(db.CreateParam("iiUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dadtOrderDate != null)
                para.Add(db.CreateParam("dadtOrderDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dadtOrderDate));
            else
                para.Add(db.CreateParam("dadtOrderDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (byiOrderStatus != null)
                para.Add(db.CreateParam("byiOrderStatus", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, byiOrderStatus));
            else
                para.Add(db.CreateParam("byiOrderStatus", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCustomerNote != null)
                para.Add(db.CreateParam("ssCustomerNote", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCustomerNote));
            else
                para.Add(db.CreateParam("ssCustomerNote", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssAdminNote != null)
                para.Add(db.CreateParam("ssAdminNote", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAdminNote));
            else
                para.Add(db.CreateParam("ssAdminNote", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (fdTotal != null)
                para.Add(db.CreateParam("fdTotal", System.Data.DbType.Double, System.Data.ParameterDirection.Input, fdTotal));
            else
                para.Add(db.CreateParam("fdTotal", System.Data.DbType.Double, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dOffer != null)
                para.Add(db.CreateParam("dOffer", System.Data.DbType.String, System.Data.ParameterDirection.Input, dOffer));
            else
                para.Add(db.CreateParam("dOffer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssFirstName != null)
                para.Add(db.CreateParam("ssFirstName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssFirstName));
            else
                para.Add(db.CreateParam("ssFirstName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssLastName != null)
                para.Add(db.CreateParam("ssLastName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssLastName));
            else
                para.Add(db.CreateParam("ssLastName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssAddress != null)
                para.Add(db.CreateParam("ssAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAddress));
            else
                para.Add(db.CreateParam("ssAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCity != null)
                para.Add(db.CreateParam("ssCity", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCity));
            else
                para.Add(db.CreateParam("ssCity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssZipcode != null)
                para.Add(db.CreateParam("ssZipcode", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssZipcode));
            else
                para.Add(db.CreateParam("ssZipcode", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssState != null)
                para.Add(db.CreateParam("ssState", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssState));
            else
                para.Add(db.CreateParam("ssState", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCountry != null)
                para.Add(db.CreateParam("ssCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCountry));
            else
                para.Add(db.CreateParam("ssCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssPhone != null)
                para.Add(db.CreateParam("ssPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssPhone));
            else
                para.Add(db.CreateParam("ssPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssMobile != null)
                para.Add(db.CreateParam("ssMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssMobile));
            else
                para.Add(db.CreateParam("ssMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssEmail != null)
                para.Add(db.CreateParam("ssEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssEmail));
            else
                para.Add(db.CreateParam("ssEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iiModifiedBy != null)
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iiModifiedBy));
            else
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dadtModifiedDate != null)
                para.Add(db.CreateParam("dadtModifiedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dadtModifiedDate));
            else
                para.Add(db.CreateParam("dadtModifiedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (bbIsDeleted != null)
                para.Add(db.CreateParam("bbIsDeleted", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bbIsDeleted));
            else
                para.Add(db.CreateParam("bbIsDeleted", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            //if (iiOrderid != null)
            //    para.Add(db.CreateParam("iiOrderid", System.Data.DbType.Int64, System.Data.ParameterDirection.Output, iiOrderid));
            //else
            //    para.Add(db.CreateParam("iiOrderid", System.Data.DbType.Int64, System.Data.ParameterDirection.Output, DBNull.Value));
            IDbDataParameter pr = db.CreateParam("iiOrderid", System.Data.DbType.Int64, System.Data.ParameterDirection.Output, iiOrderid);
            para.Add(pr);

            db.ExecuteSP("OrderMas_Insert", para.ToArray(), false);
            iiOrderid = Convert.ToInt64(pr.Value);
            return iiOrderid;

        }

        public DataTable OrderDet_Update_StoneStatus(Int64? iOrderId, string ssRefNo, string sStoneStatus, Boolean bIsExcludeStk)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderId != null)
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderId));
            else
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssRefNo != null)
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssRefNo));
            else
                para.Add(db.CreateParam("ssRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sStoneStatus != null)
                para.Add(db.CreateParam("sStoneStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStoneStatus));
            else
                para.Add(db.CreateParam("sStoneStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (bIsExcludeStk != null)
                para.Add(db.CreateParam("bIsExcludeStk", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsExcludeStk));
            else
                para.Add(db.CreateParam("bIsExcludeStk", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("OrderDet_Update_StoneStatus", para.ToArray(), false);
            return dt;
        }

        public void OrderDet_UpdateExcludeStk(Int64? iOrderID, Int32? bIsExcludedStock)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderID != null)
                para.Add(db.CreateParam("iOrderDetId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderID));
            else
                para.Add(db.CreateParam("iOrderDetId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsExcludedStock != null)
                para.Add(db.CreateParam("bIsExcludeStk", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, bIsExcludedStock));
            else
                para.Add(db.CreateParam("bIsExcludeStk", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("OrderDet_UpdateExcludeStk", para.ToArray(), false);

        }

        public DataTable OrderDet_SelectByPara(DateTime? dtFromDate, DateTime? dtToDate, String sRefNo, Int32? iPgNo, Int32? iPgSize, String sOrderBy, String sStatus,
            String sFullName, String sUserName, String sCountryName, String sCompanyName)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (dtFromDate != null)
            {
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, dtFromDate));
            }
            else
            {
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, null));
            }
            if (dtToDate != null)
            {
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, dtToDate));
            }
            else
            {
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.Date, System.Data.ParameterDirection.Input, null));
            }
            if (sRefNo != null)
            {
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRefNo));
            }
            else
            {
                para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));
            }
            if (iPgNo != null)
            {
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(iPgNo)));
            }
            else
            {
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, null));
            }
            if (iPgSize != null)
            {
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Convert.ToInt32(iPgSize)));
            }
            else
            {
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, null));
            }
            // changes by Hitesh on [15-08-2017] as per [Doc No 884]
            if (sOrderBy != null)
            {
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(sOrderBy)));
            }
            else
            {
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));
            }
            if (sStatus != null)
            {
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(sStatus)));
            }
            else
            {
                para.Add(db.CreateParam("sStatus", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));
            }
            if (sFullName != null)
            {
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(sFullName)));
            }
            else
            {
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));
            }
            if (sUserName != null)
            {
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(sUserName)));
            }
            else
            {
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));
            }
            if (sCountryName != null)
            {
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(sCountryName)));
            }
            else
            {
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));
            }
            if (sCompanyName != null)
            {
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(sCompanyName)));
            }
            else
            {
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));
            }
            // End by Hitesh on [15-08-2017] as per [Doc No 884]
            System.Data.DataTable dt = db.ExecuteSP("OrderDet_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public void OrderMas_UpdateStatus(Int64? iOrderId, Int32? iModifiedBy, Int16? iOrderStatus)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderId != null)
                para.Add(db.CreateParam("iiOrderid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderId));
            else
                para.Add(db.CreateParam("iiOrderid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iModifiedBy != null)
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iModifiedBy));
            else
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iOrderStatus != null)
                para.Add(db.CreateParam("iOrderStatus", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, iOrderStatus));
            else
                para.Add(db.CreateParam("iOrderStatus", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("OrderMas_UpdateStatus", para.ToArray(), false);

        }

        public void OrderMas_UpdateNotes(Int64? iOrderId, Int32? iModifiedBy, String sCustomerNotes, String sAdminNotes)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iOrderId != null)
                para.Add(db.CreateParam("iiOrderid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderId));
            else
                para.Add(db.CreateParam("iiOrderid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iModifiedBy != null)
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iModifiedBy));
            else
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCustomerNotes != null)
                para.Add(db.CreateParam("ssCustomerNote", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCustomerNotes));
            else
                para.Add(db.CreateParam("ssCustomerNote", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sAdminNotes != null)
                para.Add(db.CreateParam("ssAdminNote", System.Data.DbType.String, System.Data.ParameterDirection.Input, sAdminNotes));
            else
                para.Add(db.CreateParam("ssAdminNote", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("OrderMas_UpdateNotes", para.ToArray(), false);

        }
    }
}
