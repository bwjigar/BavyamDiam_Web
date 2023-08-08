using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Collections.Generic;
using System.Data;

namespace Sunrise.Services.Models
{
    public class Oracle_DBAccess
    {
        public Oracle_DBAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        OracleConnection connection;
        public string GetConnectionString()
        {
            // Connection For Live / Dummy
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["Oraweb"].ToString();
            return connString;

            // Connection For Local
            //string connString = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.10)(PORT = 1530)) (CONNECT_DATA = (SERVICE_NAME = DEVDB)) );Persist Security Info = True ;Pooling = true ;User ID = acfortune ;Password = ac4tune";
            //return connString;
        }

        public OracleCommand CreateCommand(string sql, CommandType type, params OracleParameter[] parameters)
        {
            connection = new OracleConnection();
            connection.ConnectionString = GetConnectionString();
            connection.Open();


            OracleCommand command = new OracleCommand(sql, connection);
            command.CommandType = type;
            if (parameters != null && parameters.Length > 0)
            {
                OracleParameterCollection cmdParams = command.Parameters;
                for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
            }
            return command;
        }

        public OracleDataReader GetDataReader(string storedProcedure, params OracleParameter[] parameters)
        {
            return CreateCommand(storedProcedure, CommandType.StoredProcedure, parameters).ExecuteReader();
        }

        public static OracleParameter CreateCursorParameter(string name)
        {
            OracleParameter prm = new OracleParameter(name, OracleDbType.RefCursor);
            prm.Direction = ParameterDirection.Output;
            return prm;
        }

        public static OracleParameter addCustParameter(object name)
        {
            OracleParameter prm = new OracleParameter("vhold_by", name);
            prm.Direction = ParameterDirection.Input;
            return prm;
        }
        public static OracleParameter addTransType(object sType)
        {
            OracleParameter prm = new OracleParameter("vtrans_type", sType);
            prm.Direction = ParameterDirection.Input;
            return prm;
        }
        public static OracleParameter addUserCustPara(decimal Usercode)
        {
            OracleParameter prm = new OracleParameter("vuser_code", Usercode);
            prm.Direction = ParameterDirection.Input;
            return prm;
        }
        public static OracleParameter addOrderType(object sOrderType)
        {
            OracleParameter prm = new OracleParameter("vOrder_type", sOrderType);
            prm.Direction = ParameterDirection.Input;
            return prm;
        }
        public static OracleParameter addEntryType(object sEntryType)
        {
            OracleParameter prm = new OracleParameter("vEntry_type", sEntryType);
            prm.Direction = ParameterDirection.Input;
            return prm;
        }
        //--By Aniket on [05-08-15]
        public static OracleParameter addRemarks(object sRemarks)
        {
            OracleParameter prm = new OracleParameter("vremarks", sRemarks);
            prm.Direction = ParameterDirection.Input;
            return prm;
        }
        //--Over [05-08-15]

        public static OracleParameter CreateCustomTypeArrayInputParameter<t>(string name, string oracleUDTName, t value) where t : IOracleCustomType, INullable
        {
            OracleParameter parameter = new OracleParameter();
            parameter.ParameterName = name;
            parameter.OracleDbType = OracleDbType.Array;
            parameter.Direction = ParameterDirection.Input;
            parameter.UdtTypeName = oracleUDTName;
            parameter.Value = value;
            return parameter;
        }

        public DataTable CallSP(string SP, List<OracleParameter> paramList)
        {
            OracleConnection connection = new OracleConnection();
            Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();

            connection.ConnectionString = oracleDbAccess.GetConnectionString();
            
            cmd.Connection = connection;
            cmd.CommandText = SP;
            cmd.CommandType = CommandType.StoredProcedure;
            if (paramList != null)
            {
                int totP = paramList.Count, i = 0;
                for (; i < totP; i++)
                {
                    cmd.Parameters.Add(paramList[i]);
                }
            }
            connection.Open();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmd.Dispose();
            connection.Close();
            return dt;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (connection != null)
                connection = null;
        }

        #endregion
    }
}