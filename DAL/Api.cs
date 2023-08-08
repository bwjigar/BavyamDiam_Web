using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAL
{
    public class Api
    {
        public void ApiMas_Delete(Int64? iTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("ApiMas_Delete", para.ToArray(), false);

        }
        public void ApiDetDelete(Int64? iTransId, Int64? iSeqNo)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("Trans_id", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("Trans_id", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iSeqNo != null)
                para.Add(db.CreateParam("iSeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iSeqNo));
            else
                para.Add(db.CreateParam("iSeqNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("ApiDetDelete", para.ToArray(), false);

        }

        public DataTable ApiMas_Select(Int64? iTransId, DateTime? dtFromDate, DateTime? dtToDate, Int64? iUserId, Int64? iEmpId, string sFullName, string sUserName, string sCompName, string sCountryName, Int64? iPgNo, Int64? iPgSize)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dtFromDate != null)
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtFromDate));
            else
                para.Add(db.CreateParam("dtFromDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dtToDate != null)
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, dtToDate));
            else
                para.Add(db.CreateParam("dtToDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserId != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iEmpId != null)
                para.Add(db.CreateParam("iEmpId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iEmpId));
            else
                para.Add(db.CreateParam("iEmpId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sFullName != null)
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFullName));
            else
                para.Add(db.CreateParam("sFullName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sUserName != null)
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserName));
            else
                para.Add(db.CreateParam("sUserName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCompName != null)
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCompName));
            else
                para.Add(db.CreateParam("sCompName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCountryName != null)
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCountryName));
            else
                para.Add(db.CreateParam("sCountryName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPgNo != null)
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iPgNo));
            else
                para.Add(db.CreateParam("iPgNo", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPgSize != null)
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iPgSize));
            else
                para.Add(db.CreateParam("iPgSize", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("ApiMas_Select", para.ToArray(), false);
            return dt;
        }

        public DataTable ApiDet_Select(Int64? iTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("ApiDet_Select", para.ToArray(), false);
            return dt;

        }

        public DataTable GetLastUpdate()
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            System.Data.DataTable dt = db.ExecuteSP("GetLastUpdate", para.ToArray(), false);
            return dt;

        }




        public DataTable Api_TEMP_SelectByPara(Int64? icolumnId, string sColumnName, string sUserCaption, Int64? iOrderId, string sSessionId, string sCustMiseCaption, Int64? iuserId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (icolumnId != null)
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, icolumnId));
            else
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sColumnName != null)
                para.Add(db.CreateParam("sColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColumnName));
            else
                para.Add(db.CreateParam("sColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sUserCaption != null)
                para.Add(db.CreateParam("sUserCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserCaption));
            else
                para.Add(db.CreateParam("sUserCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iOrderId != null)
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderId));
            else
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sSessionId != null)
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSessionId));
            else
                para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCustMiseCaption != null)
                para.Add(db.CreateParam("sCustMiseCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCustMiseCaption));
            else
                para.Add(db.CreateParam("sCustMiseCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iuserId != null)
                para.Add(db.CreateParam("iuserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iuserId));
            else
                para.Add(db.CreateParam("iuserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            System.Data.DataTable dt = db.ExecuteSP("Api_TEMP_SelectByPara", para.ToArray(), false);
            return dt;
        }
        public void Api_TEMP_Delete(Int64? icolumnId, string sColumnName, Int64? iUserId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (icolumnId != null)
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, icolumnId));
            else
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sColumnName != null)
                para.Add(db.CreateParam("sColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColumnName));
            else
                para.Add(db.CreateParam("sColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserId != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("Api_TEMP_Delete", para.ToArray(), false);

        }

        public void api_temp_insert(string sSessionId, Int64? icolumnId, Int64? iOrderId, string sColumnName, string sUserCaption, Int64? iUserId, string sCustMiseCaption, ref Int64? id)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            para.Add(db.CreateParam("sSessionId", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSessionId));
            if (icolumnId != null)
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, icolumnId));
            else
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iOrderId != null)
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iOrderId));
            else
                para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sColumnName != null)
                para.Add(db.CreateParam("sColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColumnName));
            else
                para.Add(db.CreateParam("sColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sUserCaption != null)
                para.Add(db.CreateParam("sUserCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserCaption));
            else
                para.Add(db.CreateParam("sUserCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserId != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sUserCaption != null)
                para.Add(db.CreateParam("sCustMiseCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCustMiseCaption));
            else
                para.Add(db.CreateParam("sCustMiseCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            para.Add(db.CreateParam("id", System.Data.DbType.Int64, System.Data.ParameterDirection.Output, id));
            db.ExecuteSP("api_temp_insert", para.ToArray(), false);

        }


        public void ApiDet_Insert(Int64? iTransId, Int64? icolumnId, Int64? iPriority, string sUser_ColumnName, string sCustMiseCaption)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (icolumnId != null)
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, icolumnId));
            else
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iPriority != null)
                para.Add(db.CreateParam("iPriority", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iPriority));
            else
                para.Add(db.CreateParam("iPriority", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sUser_ColumnName != null)
                para.Add(db.CreateParam("sUser_ColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUser_ColumnName));
            else
                para.Add(db.CreateParam("sUser_ColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCustMiseCaption != null)
                para.Add(db.CreateParam("sCustMiseCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCustMiseCaption));
            else
                para.Add(db.CreateParam("sCustMiseCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            db.ExecuteSP("ApiDet_Insert", para.ToArray(), false);

        }
        public void api_temp_update(Int64? icolumnId, string sColumnName, string sUserCaption, Int64? iUserId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (icolumnId != null)
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, icolumnId));
            else
                para.Add(db.CreateParam("icolumnId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sColumnName != null)
                para.Add(db.CreateParam("sColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColumnName));
            else
                para.Add(db.CreateParam("sColumnName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sUserCaption != null)
                para.Add(db.CreateParam("sUserCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, sUserCaption));
            else
                para.Add(db.CreateParam("sUserCaption", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iUserId != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("api_temp_update", para.ToArray(), false);

        }
        public void ApiMaster_Update(Int64? iTransId, DateTime sTransDate, string sShape, string sColor, string sClarity, string sCut, string sPolish, string sSymm, string sFls,
            string sLab, Single? dFromcts, Single? dToCts, Single? sFromDisc, Single? dToDisc, string sPointer, Single? dFromLength, Single? dToLength, Single? dFromWidth, Single? dToWidth, Single? dFromDepth,
            Single? dToDepth, Single? dFromDepthPer, Single? dToDepthPer, Single? dFromTablePer, Single? dToTablePer, Single? dFromCrAng, Single? dToCrAng, Single? dFromCrHt, Single? dToCrHt, Single? dFromPavAng, Single? dToPavAng,
            Single? dFromPavHt, Single? dToPavHt, string sShade, string sInclusion, string sTableNatts, string sApiName, string sExpType, string sCrownNatts, string sCrownInclusion, string sFtpName,
            string sftpUser, string sftpPass, Int64? iFtpUploadTime, char? sSeprator, char? sRepeat, string sEmail, Int64? iLocation, string sMailUploadTime, string ApiUrl)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            if (iTransId != null)
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iTransId));
            else
                para.Add(db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            //if (iUserId != null)
            //    para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            //else
            //    para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sTransDate != null)
                para.Add(db.CreateParam("dTransDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, sTransDate));
            else
                para.Add(db.CreateParam("dTransDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromcts != null)
                para.Add(db.CreateParam("dFromcts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromcts));
            else
                para.Add(db.CreateParam("dFromcts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sFromDisc != null)
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, sFromDisc));
            else
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToDisc != null)
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDisc));
            else
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromLength != null)
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromLength));
            else
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToLength != null)
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToLength));
            else
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromWidth != null)
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromWidth));
            else
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToWidth != null)
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToWidth));
            else
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromDepth != null)
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepth));
            else
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToDepth != null)
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepth));
            else
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromDepthPer != null)
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepthPer));
            else
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToDepthPer != null)
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepthPer));
            else
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromTablePer != null)
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromTablePer));
            else
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToTablePer != null)
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToTablePer));
            else
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromCrAng != null)
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrAng));
            else
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToCrAng != null)
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrAng));
            else
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromCrHt != null)
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrHt));
            else
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToCrHt != null)
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrHt));
            else
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromPavAng != null)
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavAng));
            else
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToPavAng != null)
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavAng));
            else
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavHt != null)
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavHt));
            else
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToPavHt != null)
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavHt));
            else
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShade != null)
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShade));
            else
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sInclusion != null)
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sInclusion));
            else
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sTableNatts != null)
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTableNatts));
            else
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sApiName != null)
                para.Add(db.CreateParam("sApiName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sApiName));
            else
                para.Add(db.CreateParam("sApiName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sExpType != null)
                para.Add(db.CreateParam("sExpType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sExpType));
            else
                para.Add(db.CreateParam("sExpType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCrownNatts != null)
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownNatts));
            else
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCrownInclusion != null)
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownInclusion));
            else
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sFtpName != null)
                para.Add(db.CreateParam("sFtpName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFtpName));
            else
                para.Add(db.CreateParam("sFtpName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sftpUser != null)
                para.Add(db.CreateParam("sftpUser", System.Data.DbType.String, System.Data.ParameterDirection.Input, sftpUser));
            else
                para.Add(db.CreateParam("sftpUser", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sftpPass != null)
                para.Add(db.CreateParam("sftpPass", System.Data.DbType.String, System.Data.ParameterDirection.Input, sftpPass));
            else
                para.Add(db.CreateParam("sftpPass", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iFtpUploadTime != null)
                para.Add(db.CreateParam("iFtpUploadTime", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iFtpUploadTime));
            else
                para.Add(db.CreateParam("iFtpUploadTime", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sSeprator != null)
                para.Add(db.CreateParam("sSeprator", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSeprator));
            else
                para.Add(db.CreateParam("sSeprator", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sRepeat != null)
                para.Add(db.CreateParam("sRepeat", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRepeat));
            else
                para.Add(db.CreateParam("sRepeat", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sEmail != null)
                para.Add(db.CreateParam("sEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, sEmail));
            else
                para.Add(db.CreateParam("sEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iLocation != null)
                para.Add(db.CreateParam("iLocation", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iLocation));
            else
                para.Add(db.CreateParam("iLocation", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sMailUploadTime != null)
                para.Add(db.CreateParam("sMailUploadTime", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMailUploadTime));
            else
                para.Add(db.CreateParam("sMailUploadTime", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ApiUrl != null)
                para.Add(db.CreateParam("ApiUrl", System.Data.DbType.String, System.Data.ParameterDirection.Input, ApiUrl));
            else
                para.Add(db.CreateParam("ApiUrl", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            db.ExecuteSP("ApiMaster_Update", para.ToArray(), false);
        }
        public Int64? ApiMaster_Insert(Int64? iUserId, DateTime sTransDate, string sShape, string sColor, string sClarity, string sCut, string sPolish, string sSymm, string sFls,
                string sLab, Single? dFromcts, Single? dToCts, Single? sFromDisc, Single? dToDisc, string sPointer, Single? dFromLength, Single? dToLength, Single? dFromWidth, Single? dToWidth, Single? dFromDepth,
                Single? dToDepth, Single? dFromDepthPer, Single? dToDepthPer, Single? dFromTablePer, Single? dToTablePer, Single? dFromCrAng, Single? dToCrAng, Single? dFromCrHt, Single? dToCrHt, Single? dFromPavAng, Single? dToPavAng,
                Single? dFromPavHt, Single? dToPavHt, string sShade, string sInclusion, string sTableNatts, string sApiName, string sExpType, string sCrownNatts, string sCrownInclusion, string sFtpName,
                string sftpUser, string sftpPass, Int64? iFtpUploadTime, char? sSeprator, char? sRepeat, string sEmail, string sMailUploadTime, DateTime? MailLastUploadTime, DateTime? FTP_LAST_UPLOAD_DATE, Int64? iLocation, string ApiUrl, ref Int64? iTransId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            if (iUserId != null)
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iUserId));
            else
                para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sTransDate != null)
                para.Add(db.CreateParam("dTransDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, sTransDate));
            else
                para.Add(db.CreateParam("dTransDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sShape != null)
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShape));
            else
                para.Add(db.CreateParam("sShape", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sColor != null)
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, sColor));
            else
                para.Add(db.CreateParam("sColor", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sClarity != null)
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, sClarity));
            else
                para.Add(db.CreateParam("sClarity", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCut != null)
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCut));
            else
                para.Add(db.CreateParam("sCut", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sPolish != null)
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPolish));
            else
                para.Add(db.CreateParam("sPolish", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sSymm != null)
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSymm));
            else
                para.Add(db.CreateParam("sSymm", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sFls != null)
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFls));
            else
                para.Add(db.CreateParam("sFls", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sLab != null)
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, sLab));
            else
                para.Add(db.CreateParam("sLab", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromcts != null)
                para.Add(db.CreateParam("dFromcts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromcts));
            else
                para.Add(db.CreateParam("dFromcts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToCts != null)
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCts));
            else
                para.Add(db.CreateParam("dToCts", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sFromDisc != null)
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, sFromDisc));
            else
                para.Add(db.CreateParam("dFromDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToDisc != null)
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDisc));
            else
                para.Add(db.CreateParam("dToDisc", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sPointer != null)
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, sPointer));
            else
                para.Add(db.CreateParam("sPointer", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromLength != null)
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromLength));
            else
                para.Add(db.CreateParam("dFromLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToLength != null)
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToLength));
            else
                para.Add(db.CreateParam("dToLength", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromWidth != null)
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromWidth));
            else
                para.Add(db.CreateParam("dFromWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToWidth != null)
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToWidth));
            else
                para.Add(db.CreateParam("dToWidth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromDepth != null)
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepth));
            else
                para.Add(db.CreateParam("dFromDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToDepth != null)
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepth));
            else
                para.Add(db.CreateParam("dToDepth", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromDepthPer != null)
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromDepthPer));
            else
                para.Add(db.CreateParam("dFromDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToDepthPer != null)
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToDepthPer));
            else
                para.Add(db.CreateParam("dToDepthPer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromTablePer != null)
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromTablePer));
            else
                para.Add(db.CreateParam("dFromTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToTablePer != null)
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToTablePer));
            else
                para.Add(db.CreateParam("dToTablePer", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromCrAng != null)
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrAng));
            else
                para.Add(db.CreateParam("dFromCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToCrAng != null)
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrAng));
            else
                para.Add(db.CreateParam("dToCrAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromCrHt != null)
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromCrHt));
            else
                para.Add(db.CreateParam("dFromCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToCrHt != null)
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToCrHt));
            else
                para.Add(db.CreateParam("dToCrHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dFromPavAng != null)
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavAng));
            else
                para.Add(db.CreateParam("dFromPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToPavAng != null)
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavAng));
            else
                para.Add(db.CreateParam("dToPavAng", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (dFromPavHt != null)
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dFromPavHt));
            else
                para.Add(db.CreateParam("dFromPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));
            if (dToPavHt != null)
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, dToPavHt));
            else
                para.Add(db.CreateParam("dToPavHt", System.Data.DbType.Single, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sShade != null)
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, sShade));
            else
                para.Add(db.CreateParam("sShade", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sInclusion != null)
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sInclusion));
            else
                para.Add(db.CreateParam("sInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sTableNatts != null)
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sTableNatts));
            else
                para.Add(db.CreateParam("sTableNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sApiName != null)
                para.Add(db.CreateParam("sApiName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sApiName));
            else
                para.Add(db.CreateParam("sApiName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sExpType != null)
                para.Add(db.CreateParam("sExpType", System.Data.DbType.String, System.Data.ParameterDirection.Input, sExpType));
            else
                para.Add(db.CreateParam("sExpType", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCrownNatts != null)
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownNatts));
            else
                para.Add(db.CreateParam("sCrownNatts", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sCrownInclusion != null)
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, sCrownInclusion));
            else
                para.Add(db.CreateParam("sCrownInclusion", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sFtpName != null)
                para.Add(db.CreateParam("sFtpName", System.Data.DbType.String, System.Data.ParameterDirection.Input, sFtpName));
            else
                para.Add(db.CreateParam("sFtpName", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sftpUser != null)
                para.Add(db.CreateParam("sftpUser", System.Data.DbType.String, System.Data.ParameterDirection.Input, sftpUser));
            else
                para.Add(db.CreateParam("sftpUser", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sftpPass != null)
                para.Add(db.CreateParam("sftpPass", System.Data.DbType.String, System.Data.ParameterDirection.Input, sftpPass));
            else
                para.Add(db.CreateParam("sftpPass", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (iFtpUploadTime != null)
                para.Add(db.CreateParam("iFtpUploadTime", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iFtpUploadTime));
            else
                para.Add(db.CreateParam("iFtpUploadTime", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sSeprator != null)
                para.Add(db.CreateParam("sSeprator", System.Data.DbType.String, System.Data.ParameterDirection.Input, sSeprator));
            else
                para.Add(db.CreateParam("sSeprator", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sRepeat != null)
                para.Add(db.CreateParam("sRepeat", System.Data.DbType.String, System.Data.ParameterDirection.Input, sRepeat));
            else
                para.Add(db.CreateParam("sRepeat", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
            if (sEmail != null)
                para.Add(db.CreateParam("sEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, sEmail));
            else
                para.Add(db.CreateParam("sEmail", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (sMailUploadTime != null)
                para.Add(db.CreateParam("sMailUploadTime", System.Data.DbType.String, System.Data.ParameterDirection.Input, sMailUploadTime));
            else
                para.Add(db.CreateParam("sMailUploadTime", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));

            if (MailLastUploadTime != null)
                para.Add(db.CreateParam("MailLastUploadTime", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, MailLastUploadTime));
            else
                para.Add(db.CreateParam("MailLastUploadTime", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));
            if (FTP_LAST_UPLOAD_DATE != null)
                para.Add(db.CreateParam("FTP_LAST_UPLOAD_DATE", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, FTP_LAST_UPLOAD_DATE));
            else
                para.Add(db.CreateParam("FTP_LAST_UPLOAD_DATE", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, DBNull.Value));

            if (iLocation != null)
                para.Add(db.CreateParam("iLocation", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, iLocation));
            else
                para.Add(db.CreateParam("iLocation", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, DBNull.Value));
            if (ApiUrl != null)
                para.Add(db.CreateParam("ApiUrl", System.Data.DbType.String, System.Data.ParameterDirection.Input, ApiUrl));
            else
                para.Add(db.CreateParam("ApiUrl", System.Data.DbType.String, System.Data.ParameterDirection.Input, DBNull.Value));
                        
            IDbDataParameter pr = db.CreateParam("iTransId", System.Data.DbType.Int64, System.Data.ParameterDirection.Output, iTransId);
            para.Add(pr);
            db.ExecuteSP("ApiMaster_Insert", para.ToArray(), false);
            iTransId = Convert.ToInt64(pr.Value);
            return iTransId;
        }
    }
}
