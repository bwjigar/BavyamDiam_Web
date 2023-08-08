using DAL;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http;

namespace Sunrise.Services
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string token = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                string username = decodedToken.Split(':')[0];
                string password = decodedToken.Split(':')[1];
                if (Login(username, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username),null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }

        private bool Login(string username, string password)
        {
            try
            {
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                CommonResponse resp = new CommonResponse();
                DataTable _dt = ApiUploadMethod_viewAll(1, 1000);
                DataView _dv = new DataView(_dt);

                _dv.RowFilter = "WebAPIUserName = '" + username.Trim() + "' AND WebAPIPassword = '" + password.Trim() +"'";

                //if (apiuploadmethod.iTransId > 0 && apiuploadmethod.iTransId != null)
                //    _dv.RowFilter += " AND iTransId <> " + apiuploadmethod.iTransId;

                _dt = _dv.ToTable();
                if (_dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [NonAction]
        private DataTable ApiUploadMethod_viewAll(int iPgNo, int iPgSize)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtToDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_select", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}