using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public static class DataTableExtension
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

        public static T ToObject<T>(this DataRow row) where T : new()
        {
            List<PropertyInfo> properties = GetPropertiesForType<T>();
            T result = new T();
            result = CreateItemFromRow<T>((DataRow)row, properties);
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



        //Oracle
        public static List<PropertyInfo> GetPropertiesForTypeOracle<T>()
        {
            var type = typeof(T);
            if (!typeDictionary.ContainsKey(typeof(T)))
            {
                typeDictionary.Add(type, type.GetProperties().ToList());
            }
            return typeDictionary[type];
        }

        public static List<T> ToListOracle<T>(this DataTable table) where T : new()
        {
            List<PropertyInfo> properties = GetPropertiesForTypeOracle<T>();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRowOracle<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        public static T ToObjectOracle<T>(this DataRow row) where T : new()
        {
            List<PropertyInfo> properties = GetPropertiesForTypeOracle<T>();
            T result = new T();
            result = CreateItemFromRowOracle<T>((DataRow)row, properties);
            return result;
        }

        private static T CreateItemFromRowOracle<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
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
                            var Type = row[property.Name].GetType().Name;

                            if (Type.ToString().ToUpper() == "SINGLE")
                            {
                                property.SetValue(item, Convert.ToDecimal(row[property.Name]), null);
                            }
                            else if (Type.ToString().ToUpper() == "DATETIME")
                            {
                                property.SetValue(item, row[property.Name].ToString(), null);
                            }
                            else
                            {
                                property.SetValue(item, row[property.Name], null);
                            }



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
