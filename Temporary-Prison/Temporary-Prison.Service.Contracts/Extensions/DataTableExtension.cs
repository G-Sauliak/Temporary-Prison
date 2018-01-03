using System;
using System.Data;

namespace Temporary_Prison.Service.Contracts.Extensions
{
    public static class DataTableExtension
    {
        public static TypeArray[] ConvertToArrayByColumn<TypeArray>(this DataTable dataTable, string columName)
        {
            TypeArray[] data = new TypeArray[dataTable.Rows.Count];
            int i = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                data[i] = (TypeArray)row[columName];
                i++;
            }
            return data;
        }

        public static TypeModel ConvertToModel<TypeModel>(this DataTable dataTable) where TypeModel : class, new()
        {
            if (dataTable.Rows.Count == 0)
            {
                return default(TypeModel);
            }
            var obj = new TypeModel();
            var temp = typeof(TypeModel);
            var row = dataTable.Rows[0];

            foreach (DataColumn colum in row.Table.Columns)
            {
                foreach (var property in temp.GetProperties())
                {
                    if (property.Name == colum.ColumnName)
                    {
                        property.SetValue(obj, row[colum.ColumnName], null);
                    }
                }
            }
            return obj;
        }

        public static TypeModel[] ConvertToArrayOfModels<TypeModel>(this DataTable dataTable)
            where TypeModel : class, new()
        {
            TypeModel[] data = new TypeModel[dataTable.Rows.Count];
            int i = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                TypeModel item = GetItem<TypeModel>(row);
                data[i] = item;
                i++;
            }
            return data;
        }

        private static TypeModel GetItem<TypeModel>(DataRow row) where TypeModel : class, new()
        {
            var temp = typeof(TypeModel);
            var obj = new TypeModel();
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
