using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repository
{
    public class UserRepository : IUserRepository
    {
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

        public string[] GetRoles(string userName)
        {
            string[] userRoles = null;
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("GetRoles", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("userName", userName);
                    using (var dataReader = sqlCommand.ExecuteReader())
                    {
                        userRoles = new string[dataReader.FieldCount + 1];
                        for (int i = 0; dataReader.Read(); ++i)
                        {
                            userRoles[i] = dataReader["RoleName"].ToString();
                        }

                    }
                }
            }
            return userRoles;
        }

        public UserDto GetUserByName(string userName)
        {
            UserDto userDto = null;
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("GetUserByName", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("userName", userName);
                    using (var dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            userDto = new UserDto()
                            {
                                UserName = dataReader["UserName"].ToString(),
                                Email = dataReader["Email"].ToString()
                            };
                        }
                    }
                }
            }
            return userDto;
        }

        public UserDto[] GetUsersForPagedList(int skip, int rowSize,out int totalCountUsers)
        {
            var usersDto = default(UserDto[]);
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("GetUsersForPagedList", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    var param = new SqlParameter[]
                     {
                        new SqlParameter(@"skip",skip),
                        new SqlParameter(@"rowSize",rowSize),
                     };

                    var returnVal = sqlCommand.Parameters.Add("@return_value", SqlDbType.Int);
                    returnVal.Direction = ParameterDirection.ReturnValue;

                    sqlCommand.Parameters.AddRange(param);

                    using (var dataReader = sqlCommand.ExecuteReader())
                    {
                        usersDto = new UserDto[dataReader.FieldCount];
                        for (int i = 0;dataReader.Read(); i++)
                        {
                            usersDto[i] = new UserDto()
                            {
                                UserName = dataReader["UserName"].ToString(),
                                Email = dataReader["Email"].ToString()
                            };
                        }
                        dataReader.NextResult();
                        totalCountUsers = (int)returnVal.Value;
                    }
                }
            }
            return usersDto;
        }

        public bool IsValidUser(string userName, string password)
        {
            bool result = false;
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("SELECT dbo.IsValidUser(@name,@password)", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@name", userName);
                    sqlCommand.Parameters.AddWithValue("@password", password);

                    result = (bool)sqlCommand.ExecuteScalar();
                }
            }
            return result;
        }
    }
}
