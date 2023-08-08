using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;


namespace Lib.Models
{
    public class ConvertJsonStringToDataTable
    {
        public DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            if (jsonString != "[]" && jsonString != "undefined")
            {
                string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
                List<string> ColumnsName = new List<string>();

                foreach (string jSA in jsonStringArray)
                {
                    var regex = new Regex("\\\"(.*?)\\\"");
                    var output = regex.Replace(jSA, m => m.Value.Replace(",", "_@_"));

                    string[] jsonStringData = Regex.Split(output.Replace("{", "").Replace("}", ""), ",");
                    foreach (string ColumnsNameData in jsonStringData)
                    {
                        if (ColumnsNameData != "")
                        {
                            try
                            {
                                int idx = ColumnsNameData.IndexOf(":");
                                string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                                if (!ColumnsName.Contains(ColumnsNameString))
                                {
                                    ColumnsName.Add(ColumnsNameString);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                            }
                        }
                    }
                    break;
                }

                foreach (string AddColumnName in ColumnsName)
                {
                    dt.Columns.Add(AddColumnName);
                }

                foreach (string jSA in jsonStringArray)
                {
                    var regex = new Regex("\\\"(.*?)\\\"");
                    var output = regex.Replace(jSA, m => m.Value.Replace(",", "_@_"));

                    string[] RowData = Regex.Split(output.Replace("{", "").Replace("}", ""), ",");
                    DataRow nr = dt.NewRow();
                    foreach (string rowData in RowData)
                    {
                        try
                        {
                            int idx = rowData.IndexOf(":");
                            string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                            string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                            RowDataString = RowDataString.Replace("_@_", ",");

                            nr[RowColumns] = RowDataString;
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    dt.Rows.Add(nr);
                }
                return dt;
            }
            return dt;
        }
    }
}