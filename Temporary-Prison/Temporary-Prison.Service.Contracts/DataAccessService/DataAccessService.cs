using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;
using Temporary_Prison.Service.Contracts.Extensions;
using Temporary_Prison.Service.Contracts.Helpers;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class DataAccessService
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public DataAccessService()
        {

        }

        private string GetConnectionString
        {
            get
            {
                var ConStrSettings = ConfigurationManager.ConnectionStrings["PrisonDataBase"];
                if (ConStrSettings == null)
                {
                    throw new NullReferenceException("Connection string is null");
                }
                return ConStrSettings.ConnectionString;
            }
        }

        private DataTable ConvertToDataTable<TInput>(TInput[] objectModels)
        {
            var dataTable = new DataTable();
            var temp = typeof(TInput);
            var properties = temp.GetProperties();
            if (properties != null)
            {
                foreach (var prop in temp.GetProperties())
                {
                    if (IsValidPropertyType(prop))
                    {
                        var column = new DataColumn
                        {
                            DataType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType,
                            ColumnName = prop.Name,
                            AllowDBNull = true
                        };
                        dataTable.Columns.Add(column);
                    }
                }
                foreach (var model in objectModels)
                {
                    var row = dataTable.NewRow();
                    foreach (var prop in temp.GetProperties())
                    {
                        if (IsValidPropertyType(prop))
                        {
                            row[prop.Name] = prop.GetValue(model, null) ?? DBNull.Value;
                        }
                    }
                    dataTable.Rows.Add(row);
                }
            }
            else
            {
                new ArgumentException($"{typeof(TInput)} no properties");
            }
            return dataTable;
        }

        private bool IsValidPropertyType(PropertyInfo prop)
        {
            if (prop.PropertyType.IsPrimitive
                 || prop.PropertyType == typeof(String)
                 || prop.PropertyType == typeof(DateTime)
                 || prop.PropertyType == typeof(decimal)
                 || prop.PropertyType == typeof(DateTime?))
            {
                return true;
            }
            return false;
        }

        private DataTable ConvertToDataTable<TInput>(TInput objectModel)
        {
            var dataTable = new DataTable();
            var temp = typeof(TInput);
            var properties = temp.GetProperties();
            if (properties != null)
            {
                foreach (var prop in temp.GetProperties())
                {
                    if (IsValidPropertyType(prop))
                    {
                        var column = new DataColumn
                        {
                            DataType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType,
                            ColumnName = prop.Name,
                            AllowDBNull = true
                        };
                        dataTable.Columns.Add(column);
                    }
                }
                var row = dataTable.NewRow();
                foreach (var prop in temp.GetProperties())
                {
                    if (IsValidPropertyType(prop))
                    {
                        row[prop.Name] = prop.GetValue(objectModel, null) ?? DBNull.Value;
                    }
                }
                dataTable.Rows.Add(row);
            }
            else
            {
                new ArgumentException($"{typeof(TInput)} no properties");
            }
            return dataTable;
        }

        public void ExecNonQuery<TypeModel, TOut>(string sqlCommandString, TypeModel objectmodel, string outPutName,
            out TOut outPutValue) where TypeModel : class
        {
            try
            {
                var dataTable = ConvertToDataTable(objectmodel);

                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        var returnVal = sqlCommand.Parameters.Add($"@{outPutName}", SqlDbTypeHelper.GetDbType(typeof(TOut)));
                        returnVal.Direction = ParameterDirection.Output;

                        var param = sqlCommand.Parameters.AddWithValue("@dt", dataTable);
                        param.SqlDbType = SqlDbType.Structured;

                        sqlCommand.ExecuteNonQuery();

                        outPutValue = (TOut)returnVal.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }

        public void ExecNonQuery(string sqlCommandString, params SqlParameter[] parametrs)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddRange(parametrs);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }

        public void ExecNonQuery<TOut>(string sqlCommandString, string outPutName, out TOut outPutValue, params SqlParameter[] parametrs)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        var outPutParametr = sqlCommand.Parameters.Add($"@{outPutName}", SqlDbTypeHelper.GetDbType(typeof(TOut)));
                        outPutParametr.Direction = ParameterDirection.Output;

                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddRange(parametrs);
                        sqlCommand.ExecuteNonQuery();
                        outPutValue = (TOut)outPutParametr.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }



        public void ExecNonQuery<TypeModel>(string sqlCommandString, TypeModel[] objectModels)
        {
            try
            {
                var dataTable = ConvertToDataTable(objectModels);

                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        var param = sqlCommand.Parameters.AddWithValue("@dt", dataTable);
                        param.SqlDbType = SqlDbType.Structured;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }

        public void ExecNonQuery<TypeModel>(string sqlCommandString, TypeModel objectmodel, params SqlParameter[] parametrs)
        {
            try
            {
                var dataTable = ConvertToDataTable(objectmodel);
                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        var param = sqlCommand.Parameters.AddWithValue("@dt", dataTable);
                        if (parametrs != null)
                        {
                            sqlCommand.Parameters.AddRange(parametrs);
                        }
                        param.SqlDbType = SqlDbType.Structured;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }

        public TypeModel[] ExecProcGetModels<TypeModel, TOut>(string sqlCommandString, string outPutName, out TOut outPutValue,
            params SqlParameter[] inputParametrs) where TypeModel : class, new()
        {
            var result = default(TypeModel[]);
            try
            {
                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        var outPutParametr = sqlCommand.Parameters.Add($"@{outPutName}", SqlDbTypeHelper.GetDbType(typeof(TOut)));
                        outPutParametr.Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.AddRange(inputParametrs);

                        using (var dataTable = new DataTable())
                        {
                            dataTable.Load(sqlCommand.ExecuteReader());
                            result = dataTable.ConvertToArrayOfModels<TypeModel>();
                            outPutValue = (TOut)outPutParametr.Value;
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }

        public TypeModel[] ExecProcGetModels<TypeModel>(string sqlCommandString, params SqlParameter[] inputParametrs)
            where TypeModel : class, new()
        {
            var result = default(TypeModel[]);
            try
            {
                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddRange(inputParametrs);
                        using (var dataTable = new DataTable())
                        {
                            dataTable.Load(sqlCommand.ExecuteReader());
                            result = dataTable.ConvertToArrayOfModels<TypeModel>();
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }


        public TypeModel ExecProcGetModel<TypeModel>(string sqlCommandString, params SqlParameter[] inputParametrs)
            where TypeModel : class, new()
        {
            var result = default(TypeModel);
            try
            {
                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddRange(inputParametrs);
                        using (var dataTable = new DataTable())
                        {
                            dataTable.Load(sqlCommand.ExecuteReader());
                            result = dataTable.ConvertToModel<TypeModel>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }

        public TypeArray[] GetArrayByColumn<TypeArray>(string sqlCommandString, string columnName, params SqlParameter[] inputParametrs)
        {
            var result = default(TypeArray[]);
            try
            {
                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        if (inputParametrs != null)
                        {
                            sqlCommand.Parameters.AddRange(inputParametrs);
                        }
                        using (var dataTable = new DataTable())
                        {
                            dataTable.Load(sqlCommand.ExecuteReader());
                            result = dataTable.ConvertToArrayByColumn<TypeArray>(columnName);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }


        }
        public TResult ExecScalarValued<TResult>(string sqlCommandString, params SqlParameter[] inputParametrs)
        {
            try
            {
                var result = default(TResult);
                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlCommandString, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        if (inputParametrs != null)
                        {
                            sqlCommand.Parameters.AddRange(inputParametrs);
                        }
                        result = (TResult)sqlCommand.ExecuteScalar();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = ex.Message;
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }
    }
}

