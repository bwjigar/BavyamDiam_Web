using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Usermas : Configuration
    {
        public DataTable UserMas_SelectByUsername(String sUsername, String sPassword)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand om1 = new SqlCommand("sunrise.UserMas_SelectByUsername");
            om1.CommandType = CommandType.StoredProcedure;
            if (!String.IsNullOrEmpty(sUsername))
            {
                om1.Parameters.Add("ssUsername", SqlDbType.VarChar).Value = sUsername;
            }
            else
            {
                om1.Parameters.Add("ssUsername", SqlDbType.VarChar).Value = DBNull.Value;
            }
            if (!String.IsNullOrEmpty(sPassword))
            {
                om1.Parameters.Add("ssPassword", SqlDbType.VarChar).Value = sPassword;
            }
            else
            {
                om1.Parameters.Add("ssPassword", SqlDbType.VarChar).Value = DBNull.Value;
            }
            om1.Connection = connection;
            om1.ExecuteNonQuery();
            SqlDataAdapter da1 = new SqlDataAdapter(om1);
            DataTable dtdata = new DataTable();
            da1.Fill(dtdata);
            connection.Dispose();
            return dtdata;
        }


        //public DataTable Get_SusPended_user(Int32 iUserID)
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    connection.Open();
        //    SqlCommand cmd = new SqlCommand("sunrise.Get_SusPended_user");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("iUserId", SqlDbType.BigInt).Value = iUserID;
        //    cmd.Connection = connection;
        //    cmd.ExecuteNonQuery();
        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        //    DataTable dtdata = new DataTable();
        //    da1.Fill(dtdata);
        //    connection.Dispose();
        //    return dtdata;
        //}

        public DataTable Get_SusPended_user(Int64? iUserID)
        {

            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("Get_SusPended_user", para.ToArray(), false);
            return dt;
        }

        public void UserMas_ActiveInactive(Int64? iUserID, Boolean? iBool)
        {
            Database dbUp = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> paraUp;
            paraUp = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iUserID != null)
                paraUp.Add(dbUp.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                paraUp.Add(dbUp.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iBool != null)
                paraUp.Add(dbUp.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, iBool));
            else
                paraUp.Add(dbUp.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            dbUp.ExecuteSP("UserMas_ActiveInactive", paraUp.ToArray(), false);
        }

        public DataTable UserMas_SelectEmailByUserId(Int64? iUserID)
        {
            Database dbb = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> paras;
            paras = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                paras.Add(dbb.CreateParam("sUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                paras.Add(dbb.CreateParam("sUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            DataTable dtUser = dbb.ExecuteSP("UserMas_SelectEmailByUserId", paras.ToArray(), false);

            return dtUser;
        }

        public void UserMas_Update_Suspended_Date(Int64? iuserID, Boolean? iBool)
        {
            Database dbUp = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> paraUp;
            paraUp = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iuserID != null)
                paraUp.Add(dbUp.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iuserID));
            else
                paraUp.Add(dbUp.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iBool != null)
                paraUp.Add(dbUp.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, iBool));
            else
                paraUp.Add(dbUp.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            dbUp.ExecuteSP("UserMas_Update_Suspended_Date", paraUp.ToArray(), true);
        }

        public DataTable get_assist_by_emp(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("p_for_userid", System.Data.DbType.String, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("p_for_userid", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("get_assist_by_emp", para.ToArray(), false);
            return dt;
        }

        public DataTable LoginLog_Insert(DateTime? dtdate, Int64? iUserID, String sBrowserName, bool? bIsMobile, String sBrowserVersion, String sMobileMenu, String sMobileModel, String sPlatform, String ssrcPixelHt, String ssrcPixelWt, String sIpAddress, String sCountry, String sCity)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (dtdate != null)
                para.Add(db.CreateParam("LoginDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtdate));
            else
                para.Add(db.CreateParam("LoginDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserID != null)
                para.Add(db.CreateParam("UserID", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("UserID", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sBrowserName != "")
                para.Add(db.CreateParam("BrowserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sBrowserName));
            else
                para.Add(db.CreateParam("BrowserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            para.Add(db.CreateParam("IsMobile", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsMobile));

            if (sBrowserVersion != "")
                para.Add(db.CreateParam("BrowserVersion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sBrowserVersion));
            else
                para.Add(db.CreateParam("BrowserVersion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sMobileMenu != "")
                para.Add(db.CreateParam("MobileManu", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMobileMenu));
            else
                para.Add(db.CreateParam("MobileManu", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sMobileModel != "")
                para.Add(db.CreateParam("MobileModel", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMobileModel));
            else
                para.Add(db.CreateParam("MobileModel", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sPlatform != "")
                para.Add(db.CreateParam("Platform", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPlatform));
            else
                para.Add(db.CreateParam("Platform", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssrcPixelHt != "")
                para.Add(db.CreateParam("ScreenPixelsHeight", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssrcPixelHt));
            else
                para.Add(db.CreateParam("ScreenPixelsHeight", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssrcPixelWt != "")
                para.Add(db.CreateParam("ScreenPixelsWidth", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssrcPixelWt));
            else
                para.Add(db.CreateParam("ScreenPixelsWidth", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sIpAddress != "")
                para.Add(db.CreateParam("IPAddr", System.Data.DbType.String, System.Data.ParameterDirection.Input, sIpAddress));
            else
                para.Add(db.CreateParam("IPAddr", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCountry != "")
                para.Add(db.CreateParam("Country", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountry));
            else
                para.Add(db.CreateParam("Country", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCity != "")
                para.Add(db.CreateParam("City", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCity));
            else
                para.Add(db.CreateParam("City", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("LoginLog_Insert", para.ToArray(), false);
            return dt;
        }

        public DataTable UserMas_SelectOne(Int64 iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            System.Data.DataTable dt = db.ExecuteSP("UserMas_SelectOne", para.ToArray(), false);
            return dt;
        }

        public DataTable User_List()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("User_List", para.ToArray(), false);
            return dt;
        }
        public DataTable UserName_List()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            System.Data.DataTable dt = db.ExecuteSP("UserName_List", para.ToArray(), false);
            return dt;
        }

        public DataTable UserMas_SelectByEmpId(Int64? iEmployeeId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iEmployeeId != null)
                para.Add(db.CreateParam("iEmployeeId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iEmployeeId));
            else
                para.Add(db.CreateParam("iEmployeeId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("UserMas_SelectByEmpId", para.ToArray(), false);
            return dt;
        }

        public Int32? UserMas_Insert(string ssUsername, string ssPassword, string ssFirstName, string ssLastName, string ssOtherName, string ssAddress, string ssAddress2, string ssAddress3, string ssCity, string ssZipcode, string ssState, string ssCountry, string ssMobile, string ssPhone, string ssEmail, string ssEmailPersonal, string ssPassportId, string ssHkId, string ssCompName, string ssCompAddress, string ssCompAddress2, string ssCompAddress3, string ssCompCity, string ssCompZipcode, string ssCompState, string ssCompCountry, string ssCompMobile, string ssCompMobile2, string ssCompPhone, string ssCompPhone2, string ssCompFaxNo, string ssCompEmail, string ssRapNetId, string ssCompRegNo, byte? byiUserType, int? iiEmpId, int? iiEmpId2, byte? byiLoginFailed, bool? bbIsActive, bool? bbIsDeleted, DateTime? dadtModifiedDate, int? iiModifiedBy, DateTime? dadtCreatedDate, ref Int32? iiUserid, DateTime? dadtBirthDate, bool? bIsCompUser, string sCompEmail2)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (ssUsername != null)
                para.Add(db.CreateParam("ssUsername", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssUsername));
            else
                para.Add(db.CreateParam("ssUsername", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssPassword != null)
                para.Add(db.CreateParam("ssPassword", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssPassword));
            else
                para.Add(db.CreateParam("ssPassword", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssFirstName != null)
                para.Add(db.CreateParam("ssFirstName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssFirstName));
            else
                para.Add(db.CreateParam("ssFirstName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssLastName != null)
                para.Add(db.CreateParam("ssLastName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssLastName));
            else
                para.Add(db.CreateParam("ssLastName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssOtherName != null)
                para.Add(db.CreateParam("ssOtherName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssOtherName));
            else
                para.Add(db.CreateParam("ssOtherName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssAddress != null)
                para.Add(db.CreateParam("ssAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAddress));
            else
                para.Add(db.CreateParam("ssAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssAddress2 != null)
                para.Add(db.CreateParam("ssAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAddress2));
            else
                para.Add(db.CreateParam("ssAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssAddress3 != null)
                para.Add(db.CreateParam("ssAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAddress3));
            else
                para.Add(db.CreateParam("ssAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
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
            if (ssMobile != null)
                para.Add(db.CreateParam("ssMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssMobile));
            else
                para.Add(db.CreateParam("ssMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssPhone != null)
                para.Add(db.CreateParam("ssPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssPhone));
            else
                para.Add(db.CreateParam("ssPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssEmail != null)
                para.Add(db.CreateParam("ssEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssEmail));
            else
                para.Add(db.CreateParam("ssEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssEmailPersonal != null)
                para.Add(db.CreateParam("ssEmailPersonal", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssEmailPersonal));
            else
                para.Add(db.CreateParam("ssEmailPersonal", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssPassportId != null)
                para.Add(db.CreateParam("ssPassportId", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssPassportId));
            else
                para.Add(db.CreateParam("ssPassportId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssHkId != null)
                para.Add(db.CreateParam("ssHkId", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssHkId));
            else
                para.Add(db.CreateParam("ssHkId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompName != null)
                para.Add(db.CreateParam("ssCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompName));
            else
                para.Add(db.CreateParam("ssCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompAddress != null)
                para.Add(db.CreateParam("ssCompAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompAddress));
            else
                para.Add(db.CreateParam("ssCompAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompAddress2 != null)
                para.Add(db.CreateParam("ssCompAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompAddress2));
            else
                para.Add(db.CreateParam("ssCompAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompAddress3 != null)
                para.Add(db.CreateParam("ssCompAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompAddress3));
            else
                para.Add(db.CreateParam("ssCompAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompCity != null)
                para.Add(db.CreateParam("ssCompCity", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompCity));
            else
                para.Add(db.CreateParam("ssCompCity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompZipcode != null)
                para.Add(db.CreateParam("ssCompZipcode", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompZipcode));
            else
                para.Add(db.CreateParam("ssCompZipcode", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompState != null)
                para.Add(db.CreateParam("ssCompState", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompState));
            else
                para.Add(db.CreateParam("ssCompState", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompCountry != null)
                para.Add(db.CreateParam("ssCompCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompCountry));
            else
                para.Add(db.CreateParam("ssCompCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompMobile != null)
                para.Add(db.CreateParam("ssCompMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompMobile));
            else
                para.Add(db.CreateParam("ssCompMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompMobile2 != null)
                para.Add(db.CreateParam("ssCompMobile2", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompMobile2));
            else
                para.Add(db.CreateParam("ssCompMobile2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompPhone != null)
                para.Add(db.CreateParam("ssCompPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompPhone));
            else
                para.Add(db.CreateParam("ssCompPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompPhone2 != null)
                para.Add(db.CreateParam("ssCompPhone2", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompPhone2));
            else
                para.Add(db.CreateParam("ssCompPhone2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompFaxNo != null)
                para.Add(db.CreateParam("ssCompFaxNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompFaxNo));
            else
                para.Add(db.CreateParam("ssCompFaxNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompEmail != null)
                para.Add(db.CreateParam("ssCompEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompEmail));
            else
                para.Add(db.CreateParam("ssCompEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssRapNetId != null)
                para.Add(db.CreateParam("ssRapNetId", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssRapNetId));
            else
                para.Add(db.CreateParam("ssRapNetId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssCompRegNo != null)
                para.Add(db.CreateParam("ssCompRegNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompRegNo));
            else
                para.Add(db.CreateParam("ssCompRegNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (byiUserType != null)
                para.Add(db.CreateParam("byiUserType", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, byiUserType));
            else
                para.Add(db.CreateParam("byiUserType", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iiEmpId != null)
                para.Add(db.CreateParam("iiEmpId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iiEmpId));
            else
                para.Add(db.CreateParam("iiEmpId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iiEmpId2 != null)
                para.Add(db.CreateParam("iiEmpId2", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iiEmpId2));
            else
                para.Add(db.CreateParam("iiEmpId2", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (byiLoginFailed != null)
                para.Add(db.CreateParam("byiLoginFailed", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, byiLoginFailed));
            else
                para.Add(db.CreateParam("byiLoginFailed", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (bbIsActive != null)
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bbIsActive));
            else
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            if (bbIsDeleted != null)
                para.Add(db.CreateParam("bbIsDeleted", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bbIsDeleted));
            else
                para.Add(db.CreateParam("bbIsDeleted", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dadtModifiedDate != null)
                para.Add(db.CreateParam("dadtModifiedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dadtModifiedDate));
            else
                para.Add(db.CreateParam("dadtModifiedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iiModifiedBy != null)
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iiModifiedBy));
            else
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dadtCreatedDate != null)
                para.Add(db.CreateParam("dadtCreatedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dadtCreatedDate));
            else
                para.Add(db.CreateParam("dadtCreatedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            IDbDataParameter pr = db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Output, iiUserid);
            para.Add(pr);

            if (dadtBirthDate != null)
                para.Add(db.CreateParam("dadtBirthDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dadtBirthDate));
            else
                para.Add(db.CreateParam("dadtBirthDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (bIsCompUser != null)
                para.Add(db.CreateParam("bIsCompUser", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsCompUser));
            else
                para.Add(db.CreateParam("bIsCompUser", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCompEmail2 != null)
                para.Add(db.CreateParam("sCompEmail2", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompEmail2));
            else
                para.Add(db.CreateParam("sCompEmail2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            db.ExecuteSP("UserMas_Insert", para.ToArray(), false);
            iiUserid = Convert.ToInt32(pr.Value);
            return iiUserid;


        }

        public DataTable UserMas_SelectByDate_UserType(DateTime? dtFromDate, DateTime? dtToDate, String sCountry, String sFullName, String sUserName, String sCompanyName, Int32? iUserType, Int32? iPageNo, Int32? iPageSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (dtFromDate != null)
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtFromDate));
            else
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dtToDate != null)
                para.Add(db.CreateParam("dtTodate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtToDate));
            else
                para.Add(db.CreateParam("dtTodate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCountry != null)
                para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountry));
            else
                para.Add(db.CreateParam("sCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFullName != null)
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFullName));
            else
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sUserName != null)
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserName));
            else
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCompanyName != null)
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompanyName));
            else
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserType != null)
                para.Add(db.CreateParam("iUserType", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserType));
            else
                para.Add(db.CreateParam("iUserType", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            DataTable dt = db.ExecuteSP("UserMas_SelectByDate_UserType", para.ToArray(), false);
            return dt;
        }

        public DataTable UserMas_SelectByPara(Int64? iUserID, Int32? iUserType, bool? bIsActive, String sFullName, String sUserName, String sCompanyName, String sCountryName, bool? bSuspendedUser, Int32? iPageNo, Int32? iPageSize, string sOrderBy, Int32? iEmpID1, Int32? iEmpID2, Int32? iAssistBYID, Boolean? bIsDeleted = false)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserType != null)
                para.Add(db.CreateParam("iUserType", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserType));
            else
                para.Add(db.CreateParam("iUserType", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsActive != null)
                para.Add(db.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsActive));
            else
                para.Add(db.CreateParam("bIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sFullName != null)
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFullName));
            else
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sUserName != null)
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserName));
            else
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCompanyName != null)
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompanyName));
            else
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCountryName != null)
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryName));
            else
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bSuspendedUser != null)
                para.Add(db.CreateParam("bSuspendedUser", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bSuspendedUser));
            else
                para.Add(db.CreateParam("bSuspendedUser", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iPageNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iPageNo));
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

            if (iEmpID1 != null)
                para.Add(db.CreateParam("iEmployeeId1", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iEmpID1));
            else
                para.Add(db.CreateParam("iEmployeeId1", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iEmpID2 != null)
                para.Add(db.CreateParam("iEmployeeId2", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iEmpID2));
            else
                para.Add(db.CreateParam("iEmployeeId2", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iAssistBYID != null)
                para.Add(db.CreateParam("AssistById", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iAssistBYID));
            else
                para.Add(db.CreateParam("AssistById", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsDeleted != null)
                para.Add(db.CreateParam("bIsDeleted", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsDeleted));
            else
                para.Add(db.CreateParam("bIsDeleted", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("UserMas_SelectByPara", para.ToArray(), false);
            return dt;
        }

        public void AssistBy_update(Int32? iEmpID1, Int32? iEmpID2, Int32? iUpdEmp1, Int32? iUpdEmp2, String sUserList)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iEmpID1 != null)
                para.Add(db.CreateParam("Emp1", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iEmpID1));
            else
                para.Add(db.CreateParam("Emp1", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iEmpID2 != null)
                para.Add(db.CreateParam("Emp2", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iEmpID2));
            else
                para.Add(db.CreateParam("Emp2", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUpdEmp1 != null)
                para.Add(db.CreateParam("updEmp1", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUpdEmp1));
            else
                para.Add(db.CreateParam("updEmp1", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUpdEmp2 != null)
                para.Add(db.CreateParam("updEmp2", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUpdEmp2));
            else
                para.Add(db.CreateParam("updEmp2", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sUserList != null)
                para.Add(db.CreateParam("UserList", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserList));
            else
                para.Add(db.CreateParam("UserList", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("AssistBy_update", para.ToArray(), false);

        }

        public DataTable UserMas_Update_Emp(Int32? iUserID, string ssFirstName, string ssLastName, string ssOtherName, string ssAddress, string ssAddress2, string ssAddress3, string ssCity, string ssZipcode, string ssState, string ssCountry,
            string ssMobile, string ssPhone, string ssEmail, string ssEmailPersonal, string ssPassportId, string ssHkId, byte? byiUserType, byte? byiLoginFailed, bool? bbIsActive,
            DateTime? dadtModifiedDate, int? iiModifiedBy, DateTime? dadtBirthDate, bool? bIsCompUser, string sStockCategory, string sCompEmail2, string sPassword)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssFirstName != null)
                para.Add(db.CreateParam("ssFirstName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssFirstName));
            else
                para.Add(db.CreateParam("ssFirstName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssLastName != null)
                para.Add(db.CreateParam("ssLastName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssLastName));
            else
                para.Add(db.CreateParam("ssLastName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssOtherName != null)
                para.Add(db.CreateParam("ssOtherName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssOtherName));
            else
                para.Add(db.CreateParam("ssOtherName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssAddress != null)
                para.Add(db.CreateParam("ssAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAddress));
            else
                para.Add(db.CreateParam("ssAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssAddress2 != null)
                para.Add(db.CreateParam("ssAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAddress2));
            else
                para.Add(db.CreateParam("ssAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssAddress3 != null)
                para.Add(db.CreateParam("ssAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAddress3));
            else
                para.Add(db.CreateParam("ssAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

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

            if (ssMobile != null)
                para.Add(db.CreateParam("ssMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssMobile));
            else
                para.Add(db.CreateParam("ssMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssPhone != null)
                para.Add(db.CreateParam("ssPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssPhone));
            else
                para.Add(db.CreateParam("ssPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssEmail != null)
                para.Add(db.CreateParam("ssEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssEmail));
            else
                para.Add(db.CreateParam("ssEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ssEmailPersonal != null)
                para.Add(db.CreateParam("ssEmailPersonal", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssEmailPersonal));
            else
                para.Add(db.CreateParam("ssEmailPersonal", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssPassportId != null)
                para.Add(db.CreateParam("ssPassportId", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssPassportId));
            else
                para.Add(db.CreateParam("ssPassportId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssHkId != null)
                para.Add(db.CreateParam("ssHkId", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssHkId));
            else
                para.Add(db.CreateParam("ssHkId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (byiUserType != null)
                para.Add(db.CreateParam("byiUserType", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, byiUserType));
            else
                para.Add(db.CreateParam("byiUserType", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, DBNull.Value));
            if (byiLoginFailed != null)
                para.Add(db.CreateParam("byiLoginFailed", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, byiLoginFailed));
            else
                para.Add(db.CreateParam("byiLoginFailed", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bbIsActive != null)
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bbIsActive));
            else
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dadtModifiedDate != null)
                para.Add(db.CreateParam("dadtModifiedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dadtModifiedDate));
            else
                para.Add(db.CreateParam("dadtModifiedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iiModifiedBy != null)
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iiModifiedBy));
            else
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dadtBirthDate != null)
                para.Add(db.CreateParam("dadtBirthDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dadtBirthDate));
            else
                para.Add(db.CreateParam("dadtBirthDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (bIsCompUser != null)
                para.Add(db.CreateParam("bIsCompUser", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsCompUser));
            else
                para.Add(db.CreateParam("bIsCompUser", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sStockCategory != null)
                para.Add(db.CreateParam("sStockCategory", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStockCategory));
            else
                para.Add(db.CreateParam("sStockCategory", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCompEmail2 != null)
                para.Add(db.CreateParam("sCompEmail2", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompEmail2));
            else
                para.Add(db.CreateParam("sCompEmail2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            
            // Change By Hitesh on [07-06-2017] as per [Doc No 794]
            if (sPassword != null)
                para.Add(db.CreateParam("ssPassword", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPassword));
            else
                para.Add(db.CreateParam("ssPassword", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("UserMas_Update_Emp", para.ToArray(), false);
            return dt;


        }

        public void UserMas_UpdatePassword(Int64? iUserID, String sPassword)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sPassword != null)
                para.Add(db.CreateParam("ssPassword", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPassword));
            else
                para.Add(db.CreateParam("ssPassword", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("UserMas_UpdatePassword", para.ToArray(), false);

        }

        public DataTable UserMas_Delete(Int64? iUserID)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("UserMas_Delete", para.ToArray(), false);
            return dt;
        }

        public DataTable Get_BdateYear_Det(Int64? P_for_UserId, DateTime P_for_Date)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (P_for_UserId != null)
                para.Add(db.CreateParam("P_for_UserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, P_for_UserId));
            else
                para.Add(db.CreateParam("P_for_UserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (P_for_Date != null)
                para.Add(db.CreateParam("P_for_Date", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, P_for_Date));
            else
                para.Add(db.CreateParam("P_for_Date", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("Get_BdateYear_Det", para.ToArray(), false);
            return dt;
        }
        public DataTable UserMas_SelectPwdByUsername(string ssUsername)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (ssUsername != null)
                para.Add(db.CreateParam("ssUsername", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssUsername));
            else
                para.Add(db.CreateParam("ssUsername", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("UserMas_SelectPwdByUsername", para.ToArray(), false);
            return dt;
        }

        public void UserMas_Update_Cust(Int64? iUserID, string ssFirstName, string ssLastName, string ssAddress, string ssAddress2, string ssAddress3, string ssCity, string ssZipcode, string ssState, string ssCountry,
            string ssMobile, string ssPhone, string ssEmail, string ssEmailPersonal, string ssCompName, string ssCompAddress, string ssCompAddress2, string ssCompAddress3, string ssCompCity, string ssCompZipcode,
            string ssCompState, string ssCompCountry, string ssCompMobile, string ssCompMobile2, string ssCompPhone, string ssCompPhone2, string ssCompFaxNo, string ssCompEmail, string ssRapNetId,
             string ssCompRegNo, int? iiEmpId, int? iiEmpId2, byte? byiLoginFailed, bool? bbIsActive, DateTime? dadtModifiedDate, int? iiModifiedBy, bool? bIsCompUser, string sStockCategory, string sCompEmail2, string sPassword)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iUserID != null)
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iiUserid", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

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

            if (ssAddress2 != null)
                para.Add(db.CreateParam("ssAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAddress2));
            else
                para.Add(db.CreateParam("ssAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssAddress3 != null)
                para.Add(db.CreateParam("ssAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssAddress3));
            else
                para.Add(db.CreateParam("ssAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

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

            if (ssMobile != null)
                para.Add(db.CreateParam("ssMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssMobile));
            else
                para.Add(db.CreateParam("ssMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));


            if (ssPhone != null)
                para.Add(db.CreateParam("ssPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssPhone));
            else
                para.Add(db.CreateParam("ssPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssEmail != null)
                para.Add(db.CreateParam("ssEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssEmail));
            else
                para.Add(db.CreateParam("ssEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssEmailPersonal != null)
                para.Add(db.CreateParam("ssEmailPersonal", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssEmailPersonal));
            else
                para.Add(db.CreateParam("ssEmailPersonal", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompName != null)
                para.Add(db.CreateParam("ssCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompName));
            else
                para.Add(db.CreateParam("ssCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompAddress != null)
                para.Add(db.CreateParam("ssCompAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompAddress));
            else
                para.Add(db.CreateParam("ssCompAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompAddress2 != null)
                para.Add(db.CreateParam("ssCompAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompAddress2));
            else
                para.Add(db.CreateParam("ssCompAddress2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompAddress3 != null)
                para.Add(db.CreateParam("ssCompAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompAddress3));
            else
                para.Add(db.CreateParam("ssCompAddress3", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompCity != null)
                para.Add(db.CreateParam("ssCompCity", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompCity));
            else
                para.Add(db.CreateParam("ssCompCity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompZipcode != null)
                para.Add(db.CreateParam("ssCompZipcode", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompZipcode));
            else
                para.Add(db.CreateParam("ssCompZipcode", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompState != null)
                para.Add(db.CreateParam("ssCompState", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompState));
            else
                para.Add(db.CreateParam("ssCompState", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompCountry != null)
                para.Add(db.CreateParam("ssCompCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompCountry));
            else
                para.Add(db.CreateParam("ssCompCountry", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompMobile != null)
                para.Add(db.CreateParam("ssCompMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompMobile));
            else
                para.Add(db.CreateParam("ssCompMobile", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompMobile2 != null)
                para.Add(db.CreateParam("ssCompMobile2", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompMobile2));
            else
                para.Add(db.CreateParam("ssCompMobile2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompPhone != null)
                para.Add(db.CreateParam("ssCompPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompPhone));
            else
                para.Add(db.CreateParam("ssCompPhone", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompPhone2 != null)
                para.Add(db.CreateParam("ssCompPhone2", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompPhone2));
            else
                para.Add(db.CreateParam("ssCompPhone2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompFaxNo != null)
                para.Add(db.CreateParam("ssCompFaxNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompFaxNo));
            else
                para.Add(db.CreateParam("ssCompFaxNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompEmail != null)
                para.Add(db.CreateParam("ssCompEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompEmail));
            else
                para.Add(db.CreateParam("ssCompEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssRapNetId != null)
                para.Add(db.CreateParam("ssRapNetId", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssRapNetId));
            else
                para.Add(db.CreateParam("ssRapNetId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (ssCompRegNo != null)
                para.Add(db.CreateParam("ssCompRegNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, ssCompRegNo));
            else
                para.Add(db.CreateParam("ssCompRegNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iiEmpId != null)
                para.Add(db.CreateParam("iiEmpId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iiEmpId));
            else
                para.Add(db.CreateParam("iiEmpId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iiEmpId2 != null)
                para.Add(db.CreateParam("iiEmpId2", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, iiEmpId2));
            else
                para.Add(db.CreateParam("iiEmpId2", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, DBNull.Value));

            if (byiLoginFailed != null)
                para.Add(db.CreateParam("byiLoginFailed", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, byiLoginFailed));
            else
                para.Add(db.CreateParam("byiLoginFailed", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bbIsActive != null)
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bbIsActive));
            else
                para.Add(db.CreateParam("bbIsActive", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dadtModifiedDate != null)
                para.Add(db.CreateParam("dadtModifiedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dadtModifiedDate));
            else
                para.Add(db.CreateParam("dadtModifiedDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iiModifiedBy != null)
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iiModifiedBy));
            else
                para.Add(db.CreateParam("iiModifiedBy", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            if (bIsCompUser != null)
                para.Add(db.CreateParam("bIsCompUser", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, bIsCompUser));
            else
                para.Add(db.CreateParam("bIsCompUser", System.Data.DbType.Boolean, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sStockCategory != null)
                para.Add(db.CreateParam("sStockCategory", System.Data.DbType.String, System.Data.ParameterDirection.Input, sStockCategory));
            else
                para.Add(db.CreateParam("sStockCategory", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCompEmail2 != null)
                para.Add(db.CreateParam("sCompEmail2", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompEmail2));
            else
                para.Add(db.CreateParam("sCompEmail2", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            // Change By Hitesh on [07-06-2017] as per [Doc No 794]
            if (sPassword != null)
                para.Add(db.CreateParam("ssPassword", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPassword));
            else
                para.Add(db.CreateParam("ssPassword", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("UserMas_Update_Cust", para.ToArray(), false);



        }

        public DataTable UserMas_SelectByCompOrCountryOrUserId(String CompanyList, String CountryList, String UserList, Int32? PageSize, Int32? PageNo, String sDisplayType)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (CompanyList == null)
                para.Add(db.CreateParam("sCompanyList", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sCompanyList", System.Data.DbType.String, System.Data.ParameterDirection.Input, CompanyList));

            if (CountryList == null)
                para.Add(db.CreateParam("sCountryList", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sCountryList", System.Data.DbType.String, System.Data.ParameterDirection.Input, CountryList));

            if (UserList == null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, UserList));

            if (PageSize == null)
                para.Add(db.CreateParam("iPageSize", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("iPageSize", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, PageSize));

            if (PageNo == null)
                para.Add(db.CreateParam("iPageNo", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("iPageNo", System.Data.DbType.Int16, System.Data.ParameterDirection.Input, PageNo));

            if (sDisplayType == null)
                para.Add(db.CreateParam("sDisplayType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            else
                para.Add(db.CreateParam("sDisplayType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sDisplayType));

            System.Data.DataTable dt = db.ExecuteSP("UserMas_SelectByCompOrCountryOrUserId", para.ToArray(), false);

            return dt;
        }

        public DataTable UserMas_CustDiscPara_Select(String sCompanyList, String sCountryList, String iUserID, String sOrderBY)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (sCompanyList != null)
                para.Add(db.CreateParam("sCompanyList", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompanyList));
            else
                para.Add(db.CreateParam("sCompanyList", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sCountryList != null)
                para.Add(db.CreateParam("sCountryList", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryList));
            else
                para.Add(db.CreateParam("sCountryList", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iUserID != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, iUserID));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sOrderBY != null)
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, sOrderBY));
            else
                para.Add(db.CreateParam("sOrderBy", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            System.Data.DataTable dt = db.ExecuteSP("UserMas_CustDiscPara_Select", para.ToArray(), false);
            return dt;
        }
    }
}
