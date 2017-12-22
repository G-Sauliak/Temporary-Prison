using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Temporary_Prison.Service.Contracts.Dto;
using Temporary_Prison.Service.Contracts.Extensions;

namespace Temporary_Prison.Service.Contracts.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

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

        public void AddToRole(string userName, string roleName)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("AddToRole", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue(@"userName", userName);
                    sqlCommand.Parameters.AddWithValue(@"roleName", roleName);
                    sqlCommand.ExecuteNonQuery();
                }
                log.Info($"RoleName {roleName} added to user: {userName}");
            }
        }

        public void AddUser(UserDto user)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("AddUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter[]
                    {
                        new SqlParameter(@"UserName",user.UserName),
                        new SqlParameter(@"Email",user.Email),
                        new SqlParameter(@"Password",user.Password),
                     };

                    var returnVal = sqlCommand.Parameters.Add("@return_value", SqlDbType.Int);
                    returnVal.Direction = ParameterDirection.ReturnValue;
                    sqlCommand.Parameters.AddRange(param);

                    var result = sqlCommand.ExecuteNonQuery();

                    int newId = (int)returnVal.Value;

                    sqlCommand.CommandText = "AddToRoles";
                    foreach (var number in user.Roles)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("RoleName", number);
                        sqlCommand.Parameters.AddWithValue("UserId", newId);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DeleteUser(string userName)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("DeleteUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue(@"userName", userName);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void EditUser(UserDto user)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("UpdateUser", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    var param = new SqlParameter[]
                     {
                        new SqlParameter(@"Email",user.Email),
                        new SqlParameter(@"userName",user.UserName),
                        new SqlParameter(@"Password",user.Password),
                     };
                    sqlCommand.Parameters.AddRange(param);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public string[] GetAllRoles()
        {
            string[] allRoles = null;
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("GetAllRoles", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    using (var dataTable = new DataTable())
                    {
                        dataTable.Load(sqlCommand.ExecuteReader());
                        allRoles = dataTable.ConvertToArrayOfStrings("RoleName");
                    }
                }
            }
            return allRoles;
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
                    var tableModel = new DataTable();
                    var tableArray = new DataTable();
                    try
                    {
                        tableModel.Load(sqlCommand.ExecuteReader());
                        userDto = tableModel.ConvertToModel<UserDto>();

                        sqlCommand.CommandText = "GetRolesByUserName";

                        tableArray.Load(sqlCommand.ExecuteReader());
                        userDto.Roles = tableArray.ConvertToArrayOfStrings("RoleName");
                    }
                    finally
                    {
                        tableModel.Dispose();
                        tableArray.Dispose();
                    }
                }
            }
            return userDto;
        }

        public UserDto[] GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers)
        {
            UserDto[] usersDto = null;
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("GetUsersForPagedList", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    var returnVal = sqlCommand.Parameters.Add("@return_value", SqlDbType.Int);
                    returnVal.Direction = ParameterDirection.ReturnValue;

                    var param = new SqlParameter[]
                     {
                        new SqlParameter(@"skip",skip),
                        new SqlParameter(@"rowSize",rowSize),
                     };
                    sqlCommand.Parameters.AddRange(param);

                    using (var dataTable = new DataTable())
                    {
                        dataTable.Load(sqlCommand.ExecuteReader());
                        usersDto = dataTable.ConvertToArrayOfModels<UserDto>();
                        totalCountUsers = (int)returnVal.Value;
                    }
                }
            }
            return usersDto;
        }

        public bool IsExistsByEmail(string email)
        {
            bool result = false;
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("SELECT dbo.IsExistsEmail(@email)", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@email", email);

                    result = (bool)sqlCommand.ExecuteScalar();
                }
            }
            return result;
        }

        public bool IsExistsByLogin(string userName)
        {
            bool result = false;
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("SELECT dbo.IsExistsLogin(@userName)", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@userName", userName);

                    result = (bool)sqlCommand.ExecuteScalar();
                }
            }
            return result;
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

        public void RemoveFromRoles(string userName, string roleName)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("DeleteFromRoles", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue(@"userName", userName);
                    sqlCommand.Parameters.AddWithValue(@"roleName", roleName);
                    sqlCommand.ExecuteNonQuery();
                }
                log.Info($"RoleName {roleName} deleted from user: {userName}");
            }
        }
    }
}
