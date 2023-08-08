using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace Lib.Models
{
    public class ConvertStringArrayToDatatable
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

        public DataTable StringArrayToDataTable(string[] columns, string[] rows)
        {
            DataTable dt = new DataTable();
            string[] col = columns[0].Split(',');

            for (int i = 0; i < col.Length; i++)
            {
                dt.Columns.Add(col[i]);
            }

            for (int j = 0; j < rows.Length; j++)
            {
                string[] res = SplitString(rows[j]);
                DataRow row = dt.NewRow();
                for (int i = 0; i < res.Length; i++)
                {
                    row[i] = res[i];
                }
                dt.Rows.Add(row);

            }
            return dt;
        }
    }
}
