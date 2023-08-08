using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class ColumnsRequest
    {
        public int? UserId { get; set; }
    }
    public class ColumnsUserResponse
    {
        public long iSr { get; set; }
        public long iUserid { get; set; }
        public string sFullName { get; set; }
        public string sCompName { get; set; }
        public string sUsername { get; set; }
    }
    public class ColumnsSettingsResponse
    {
        public int iColumnId { get; set; }
        public string sColumnName { get; set; }
        public string sCaption { get; set; }
        public int iPriority { get; set; }
        public int tempPriority { get; set; }
        public bool IsActive { get; set; }
    }

    public class ColumnsSettingsRequest
    {
        public long? Userid { get; set; }
        public List<ColumnsSettings> ColumnsSettings { get; set; }
        public ColumnsSettingsRequest()
        {
            ColumnsSettings = new List<ColumnsSettings>();
        }
    }

    public class ColumnsSettings
    {
        public int iColumnId { get; set; }
        public string sColumnName { get; set; }
        public string sCaption { get; set; }
        public int iPriority { get; set; }
        public bool IsActive { get; set; }
    }
    public class StockUploadRequest
    {
        public string Supplier { get; set; }
        public string connString { get; set; }
    }
    public class StockUpload
    {
        public string REFNUMBER { get; set; }
        public string SHAPE { get; set; }
        public string CERTIFICATEID { get; set; }
        public string POINTERS { get; set; }
        public string COLOR { get; set; }
        public string CLARITY { get; set; }
        public string CTS { get; set; }
        public string RAP_PRICE { get; set; }
        public string DISC_PER { get; set; }
        public string CUT { get; set; }
        public string POLISH { get; set; }
        public string SYMM { get; set; }
        public string FLS { get; set; }
        public string LENGTH { get; set; }
        public string WIDTH { get; set; }
        public string DEPTH { get; set; }
        public string DEPTH_PER { get; set; }
        public string TABLE_PER { get; set; }
        public string STATUS { get; set; }
        public string LAB { get; set; }
        public string CROWN_ANGLE { get; set; }
        public string CROWN_HEIGHT { get; set; }
        public string PAV_ANGLE { get; set; }
        public string PAV_HEIGHT { get; set; }
        public string GIRDLE { get; set; }
        public string SHADE { get; set; }
        public string PSEQNO { get; set; }
        public string INCLUSION { get; set; }
        public string TABLE_NATTS { get; set; }
        public string SIDE_NATTS { get; set; }
        public string CULET { get; set; }
        public string TABLE_DEPTH { get; set; }
        public string HNA { get; set; }
        public string SIDE_FTR { get; set; }
        public string TABLE_FTR { get; set; }
        public string COMMENTS { get; set; }
        public string KEYTOSYMBOL { get; set; }
        public string DISCPERBYDATE { get; set; }
        public string STONE_CLARITY { get; set; }
        public string LUSTER { get; set; }
        public string INSCRIPTION { get; set; }
        public string STR_LN { get; set; }
        public string LR_HALF { get; set; }
        public string GIRDLE_PER { get; set; }
        public string GIRDLE_TYPE { get; set; }
        public string OPEN { get; set; }
        public string REVISEDISCFLAG { get; set; }
        public string PARTY_NAME { get; set; }
        public string CROWN_INCLUSION { get; set; }
        public string CROWN_NATTS { get; set; }
        public string CERTI_DATE { get; set; }
        public string UPCOMING_FLAG { get; set; }
        public string IMG_LINK { get; set; }
        public string VIDEO_LINK { get; set; }
        public string SEGOMA_IMG { get; set; }
        public string SEGOMA_VDO { get; set; }
        public string OFFER { get; set; }
        public string OVERSEAS_FLAG { get; set; }
        public string SUPPLIER_STONE_NO { get; set; }
        public string SUPPLIER { get; set; }
        public string LOCATION { get; set; }
        public string CERTI_PATH { get; set; }
        public string MP4_STATUS { get; set; }
        public string BGM { get; set; }
        public string PR_STATUS { get; set; }
        public string HT_STATUS { get; set; }
        public string HB_STATUS { get; set; }
        public string AS_STATUS { get; set; }
        public string FANCY_AMOUNT { get; set; }
        public string HOLD_PARTY_CODE { get; set; }
        public string IMG_LINK1 { get; set; }
        public string IMG_LINK2 { get; set; }
        public string IMG_LINK3 { get; set; }
        public string ASSIST_BY { get; set; }
        public string HOLD_BY { get; set; }
        public string TABLE_OPEN { get; set; }
        public string CROWN_OPEN { get; set; }
        public string PAV_OPEN { get; set; }
        public string GIRDLE_OPEN { get; set; }
        public string CUR_STATUS { get; set; }
        public string HOLDDATETIME { get; set; }
    }
    public static class Utility
    {
        public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {
                oledbConn.Open();
                string sheetname = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" }).Rows[0]["TABLE_NAME"].ToString();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheetname + "]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            catch(Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
            }
            finally
            {
                oledbConn.Close();
            }
            return dt;
        }
    }
}
