using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace Lib.Models
{
    public class ConvertJsonObjectToDataTable
    {
        static Regex exSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
        public static string[] SplitString(string input)
        {

            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in exSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }
        public DataTable StringArrayToDataTable(List<string> keys, List<List<object>> rows)
        {
            DataTable dt = new DataTable();
            string[] col = keys.ToArray();

            for (int i = 0; i < col.Length; i++)
            {
                dt.Columns.Add(col[i]);
            }
            for (int j = 0; j < rows.ToArray().Length; j++)
            {
                DataRow row = dt.NewRow();
                for (int i = 0; i < rows[j].Count; i++)
                {
                    row[i] = rows[j][i];
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
