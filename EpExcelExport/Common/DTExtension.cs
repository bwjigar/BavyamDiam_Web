using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EpExcelExportLib
{
    public static class DTExtension
    {
        private static Dictionary<Type, List<PropertyInfo>> typeDictionary = new Dictionary<Type, List<PropertyInfo>>();
        public static List<PropertyInfo> GetPropertiesForType<T>()
        {
            var type = typeof(T);
            if (!typeDictionary.ContainsKey(typeof(T)))
            {
                typeDictionary.Add(type, type.GetProperties().ToList());
            }
            return typeDictionary[type];
        }

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<PropertyInfo> properties = GetPropertiesForType<T>();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            try
            {
                foreach (var property in properties)
                {
                    if (row.Table.Columns.Contains(property.Name))
                    {
                        if (row[property.Name] == DBNull.Value)
                        {
                            property.SetValue(item, null, null);
                        }
                        else
                        {
                            property.SetValue(item, row[property.Name], null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

    }
}
