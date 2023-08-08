using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Lib.Models
{
    public class ConvertXmlStringToDataTable
    {
        private DataTable XmlStringToDataTable(string xmlFile)
        {
            DataTable dt = new DataTable();
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(xmlFile);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(ds.GetXml());
                XmlNode nodoEstructura = xml.DocumentElement.ChildNodes.Cast<XmlNode>().ToList()[0];
                foreach (XmlNode columna in nodoEstructura.ChildNodes)
                {
                    dt.Columns.Add(columna.Name, typeof(String));
                }
                XmlNode filas = xml.DocumentElement;
                foreach (XmlNode fila in filas.ChildNodes)
                {
                    dt.Rows.Add(fila["CustomerId"].InnerText, fila["Name"].InnerText, fila["Country"].InnerText);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable ConvertXmlNodeListToDataTable(XmlNodeList xnl)
        {
            DataTable dt = new DataTable();
            int TempColumn = 0;
            foreach (XmlNode node in xnl.Item(0).ChildNodes)
            {
                TempColumn++;
                DataColumn dc = new DataColumn(node.Name, System.Type.GetType("System.String"));
                if (dt.Columns.Contains(node.Name))
                {
                    dt.Columns.Add(dc.ColumnName = dc.ColumnName + TempColumn.ToString());
                }
                else
                {
                    dt.Columns.Add(dc);
                }
            }
            int ColumnsCount = dt.Columns.Count;
            for (int i = 0; i < xnl.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < ColumnsCount; j++)
                {
                    dr[j] = xnl.Item(i).ChildNodes[j].InnerText;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
