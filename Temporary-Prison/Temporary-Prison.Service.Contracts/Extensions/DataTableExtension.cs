using System;
using System.Data;

namespace Temporary_Prison.Service.Contracts.Extensions
{
    public static class DataTableExtension
    {
        public static string[] ConvertToArrayOfStrings(this DataTable dataTable, string columName)
        {
            string[] data = new string[dataTable.Rows.Count];
            int i = 0;  
            foreach (DataRow row in dataTable.Rows)
            {
                data[i] = row[columName].ToString();
                i++;
            }
            return data;
        }

        public static TResult ConvertToModel<TResult>(this DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                return default(TResult);
            }
            var obj = Activator.CreateInstance<TResult>();
            var temp = typeof(TResult);
            var row = dataTable.Rows[0];

            foreach (DataColumn colum in row.Table.Columns)
            {
                foreach (var property in temp.GetProperties())
                {
                    if (property.Name != colum.ColumnName)
                    {
                        continue;
                    }
                    property.SetValue(obj, row[colum.ColumnName], null);
                }
            }
            return obj;
        }

        public static TResult[] ConvertToArrayOfModels<TResult>(this DataTable dataTable)
        {
            TResult[] data = new TResult[dataTable.Rows.Count];
            int i = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                TResult item = GetItem<TResult>(row);
                data[i] = item;
                i++;
            }
            return data;
        }

        private static TResult GetItem<TResult>(DataRow row)
        {
            var temp = typeof(TResult);
            var obj = Activator.CreateInstance<TResult>();

            foreach (DataColumn colum in row.Table.Columns)
            {
                foreach (var property in temp.GetProperties())
                {
                    if (property.Name != colum.ColumnName)
                    {
                        continue;
                    }
                    property.SetValue(obj, row[colum.ColumnName], null);
                }
            }
            return obj;
        }


    }
}
