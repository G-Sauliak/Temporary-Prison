using System;
using System.Data;

namespace Temporary_Prison.Service.Contracts.Extensions
{
    public static class DataTableExtension
    {
        public static TArray[] ConvertToArrayByColumn<TArray>(this DataTable dataTable, string columName)
        {
            TArray[] data = new TArray[dataTable.Rows.Count];
            int i = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                data[i] = (TArray)row[columName];
                i++;
            }
            return data;
        }

        public static TModel ConvertToModel<TModel>(this DataTable dataTable) where TModel : class, new()
        {
            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                return GetItem<TModel>(row);
            }
            return default(TModel);
        }

        public static TModel[] ConvertToArrayOfModels<TModel>(this DataTable dataTable)
            where TModel : class, new()
        {
            if (dataTable.Rows.Count > 0)
            {
                TModel[] data = new TModel[dataTable.Rows.Count];
                int i = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    TModel item = GetItem<TModel>(row);
                    data[i] = item;
                    i++;
                }
                return data;
            }
            return default(TModel[]);
        }

        private static TModel GetItem<TModel>(DataRow row) where TModel : class, new()
        {
            var temp = typeof(TModel);
            var obj = new TModel();
            foreach (DataColumn colum in row.Table.Columns)
            {
                foreach (var property in temp.GetProperties())
                {
                    if (property.Name == colum.ColumnName)
                    {
                        object value = row[colum.ColumnName];
                        if (value == DBNull.Value)
                        {
                            value = null;
                            property.SetValue(obj, value, null);
                            break;
                        }
                        property.SetValue(obj, row[colum.ColumnName], null);
                        break;
                    }
                }
            }
            return obj;
        }
    }
}
