using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace DAL
{
    public abstract class DBObjects
    {
        public abstract IDbConnection GetConnection();
        public abstract IDbCommand GetCommand();
        public abstract IDbDataAdapter GetAdapter();
        public abstract IDbDataParameter GetParameter();
        public abstract IDbTransaction BeginTransaction(IDbConnection conn);
        public abstract void AddDefExtrapara(IDbCommand cmd);
        public abstract void ConvertParamInClob(IDbDataParameter cmd);
        public abstract String GetProcedureNamePrefix();
    }


    public class SqlObjects : DBObjects
    {

        private string _ConnStr;
        public SqlObjects(String ConnStr)
        {

            this._ConnStr = ConnStr;

        }

        public override IDbConnection GetConnection()
        {
            return new System.Data.SqlClient.SqlConnection(this._ConnStr);
        }

        public override IDbCommand GetCommand()
        {
            return new SqlCommand();
        }

        public override IDbDataAdapter GetAdapter()
        {
            SqlDataAdapter adept;
            adept = new SqlDataAdapter
            {
                SelectCommand = (SqlCommand)this.GetCommand()
            };
            return adept;
        }

        public override IDbDataParameter GetParameter()
        {
            return new SqlParameter();
        }

        public override IDbTransaction BeginTransaction(IDbConnection conn)
        {
            return conn.BeginTransaction();
        }

        public override void AddDefExtrapara(IDbCommand cmd)
        {

            //OracleParameter p = new OracleParameter("VREC", OracleType.Cursor);
            //p.Direction = ParameterDirection.Output;
            //cmd.Parameters.Add(p);

        }

        public override void ConvertParamInClob(IDbDataParameter param)
        {
            ///((OracleParameter)param).OracleType = OracleType.Clob;
        }

        public override String GetProcedureNamePrefix()
        {
            ///return "sunrise.";
            return System.Configuration.ConfigurationManager.AppSettings["DatabasePrefix"].ToString();
        }

    }
    public class Database
    {
        //}
        DBObjects db = ApplicationData.GetDataObjects();
        IDbConnection conn;
        IDbTransaction trans;
        HttpRequestMessage CurrRequest = new HttpRequestMessage();

        public Database()
        {
            conn = db.GetConnection();
            CurrRequest = new HttpRequestMessage();
        }

        public Database(HttpRequestMessage httpRequest)
        {
            conn = db.GetConnection();
            CurrRequest = httpRequest;
        }
        public void OpenConnection(bool withTrans)
        {
            conn.Open();

            if (trans != null)
            {
                trans = conn.BeginTransaction();
            }
            if (withTrans)
            {
                trans = conn.BeginTransaction();
            }
        }

        public void CommitTrans()
        {
            if (trans != null)
            {
                trans.Commit();
            }
        }

        public void CloseConnection()
        {
            if (conn != null && conn.State != ConnectionState.Closed)
                conn.Close();

            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }

        public DataTable ExecuteSP(String SpName, IDbDataParameter[] paramarr, bool withTrans)
        {

            try
            {
                IDbConnection localconn;
                IDbDataAdapter adept = db.GetAdapter();
                DataSet ds = new DataSet();
                adept.SelectCommand.CommandText = db.GetProcedureNamePrefix() + SpName;
                adept.SelectCommand.CommandType = CommandType.StoredProcedure;

                adept.SelectCommand.CommandTimeout = 150;

                if (withTrans == true)
                {
                    if (trans != null)
                    {
                        localconn = this.conn;
                    }
                    else
                    {
                        this.OpenConnection(true);
                        localconn = this.conn;
                    }

                    adept.SelectCommand.Transaction = trans;

                }
                else
                {
                    if (trans != null)
                    {
                        localconn = db.GetConnection();
                    }
                    else
                    {
                        localconn = conn;

                    }
                }

                foreach (IDbDataParameter para in paramarr)
                {
                    adept.SelectCommand.Parameters.Add(para);
                }

                db.AddDefExtrapara(adept.SelectCommand);

                adept.SelectCommand.Connection = localconn;
                adept.Fill(ds);

                this.CloseConnection();

                if (localconn != null && localconn.State != ConnectionState.Closed)
                {
                    localconn.Close();
                }
                if (localconn != null)
                {
                    localconn.Dispose();
                    localconn = null;
                }

                GC.Collect();

                if (ds.Tables.Count > 0)
                    return ds.Tables[ds.Tables.Count - 1];
                else
                    return null;

            }
            catch (Exception ex)
            {
                string path = HttpContext.Current.Server.MapPath("~/DatabaseErrorLog.txt");
                try
                {
                    if (!File.Exists(@"" + path + ""))
                    {
                        File.Create(@"" + path + "", 1024, FileOptions.Asynchronous);
                    }
                    StringBuilder sbt = new StringBuilder();
                    sbt.AppendLine("= = = = = = = = = = = = = = = = = = = ");
                    sbt.Append("Error in Sp :" + Common.GetHKTime() + "," + SpName + " User ID :" + (CurrRequest.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);
                    sbt.AppendLine("");
                    sbt.AppendLine();

                    using (StreamWriter outfile = new StreamWriter(@"" + path + "", true))
                    {
                        outfile.Write(sbt.ToString());
                    }
                }

                catch (Exception ex1) { }
                ErrorLog objerror = new ErrorLog();
                objerror.ErrorLog_Insert(Common.GetHKTime(), null, null, ex.StackTrace.ToString(), ex.Message.ToString(), null, null);
                // HttpContext.Current.Response.Redirect("~/Error.aspx", false);
            }
            return null;

        }

        public object ExecuteScaler(String Query, IDbDataParameter[] paramarr, bool withTrans)
        {

            IDbConnection localconn;
            IDbCommand cmd = db.GetCommand();
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.Text;

            if (withTrans == true)
            {
                if (trans != null)
                {
                    localconn = this.conn;
                }
                else
                {
                    this.OpenConnection(true);
                    localconn = this.conn;
                }
                cmd.Transaction = trans;

            }
            else
            {
                if (trans != null)
                {
                    localconn = db.GetConnection();
                }
                else
                {
                    localconn = conn;

                }
            }

            cmd.Connection = localconn;

            foreach (IDbDataParameter para in paramarr)
            {
                cmd.Parameters.Add(para);
            }
            return cmd.ExecuteScalar();

        }

        public Int64 ExecuteNonQuery(String Query, IDbDataParameter[] paramarr, bool withTrans)
        {

            IDbConnection localconn;
            IDbCommand cmd = db.GetCommand();
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.Text;

            if (withTrans == true)
            {
                if (trans != null)
                {
                    localconn = this.conn;
                }
                else
                {
                    this.OpenConnection(true);
                    localconn = this.conn;
                }
                cmd.Transaction = trans;

            }
            else
            {
                if (trans != null)
                {
                    localconn = db.GetConnection();
                }
                else
                {
                    localconn = conn;

                }
            }
            foreach (IDbDataParameter para in paramarr)
            {
                cmd.Parameters.Add(para);
            }
            if (localconn.State == ConnectionState.Closed)
                localconn.Open();
            cmd.Connection = localconn;
            int res = cmd.ExecuteNonQuery();

            cmd.Dispose();
            this.CloseConnection();

            if (localconn != null && localconn.State != ConnectionState.Closed)
            {
                localconn.Close();
            }
            if (localconn != null)
            {
                localconn.Dispose();
                localconn = null;
            }

            GC.Collect();
            return res;
        }

        public IDbDataParameter CreateParam()
        {
            return db.GetParameter();
        }

        public IDbDataParameter CreateParam(String ParamName, System.Data.DbType type, System.Data.ParameterDirection Direction, object val)
        {
            return CreateParam(ParamName, type, Direction, val, false);
        }

        public IDbDataParameter CreateParam(String ParamName, System.Data.DbType type, System.Data.ParameterDirection Direction, object val, Boolean ConvertInClob)
        {
            IDbDataParameter para = db.GetParameter();

            para.DbType = type;
            para.Direction = Direction;
            para.ParameterName = ParamName;
            para.Value = val;

            if (ConvertInClob)
                db.ConvertParamInClob(para);

            return para;
        }
    }
    public static class ApplicationData
    {

        public static DBObjects GetDataObjects()
        {
            return new SqlObjects(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);

        }
    }
}
