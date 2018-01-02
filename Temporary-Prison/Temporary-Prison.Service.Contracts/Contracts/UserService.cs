using log4net;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class UserService : DataAccessService, IUserService
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public void AddUser(UserDto user)
        {
            if (user != null)
            {
                ExecNonQuery("insertUser", user);

                var parametrs = new SqlParameter[]
                {
                new SqlParameter("@RoleName",user.Roles.First()),
                new SqlParameter("@UserName",user.UserName)
                };
                ExecNonQuery("AddToRole", parametrs);
            }
        }

        public string[] GetAllRoles()
        {
            return GetArrayByColumn<string>("GetAllRoles", "RoleName");
        }

        public UserDto GetUserByName(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var user = GetModel<UserDto>("GetUserByName", new SqlParameter("@userName", userName));
                user.Roles = GetArrayByColumn<string>("GetRolesByUserName", "RoleName", new SqlParameter("@userName", userName));
                return user;
            }
            return default(UserDto);
        }

        public UserDto[] GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers)
        {
            if (rowSize > 0)
            {
                var param = new SqlParameter[]
                     {
                        new SqlParameter(@"skip",skip),
                        new SqlParameter(@"rowSize",rowSize),
                     };

                return GetModels<UserDto, int>("GetUsersForPagedList", "TotalCount", out totalCountUsers, param);
            }
            totalCountUsers = default(int);
            return default(UserDto[]);
        }

        public bool IsExistsByLogin(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return ExecScalarValued<bool>("SELECT dbo.IsExistsLogin(@userName)",
                    new SqlParameter("@userName", userName));
            }
            return default(bool);
        }

        public bool IsExistsByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return ExecScalarValued<bool>("SELECT dbo.IsExistsLogin(@email)",
              new SqlParameter("@email", email));
            }
            return default(bool);
        }

        public bool IsValidLogin(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userName))
            {
                var parametrs = new SqlParameter[]
                 {
                     new SqlParameter("@name",userName),
                     new SqlParameter("@password",password)
                 };
                return ExecScalarValued<bool>("SELECT dbo.IsValidUser(@name,@password)", parametrs);
            }
            return default(bool);
        }

        public void EditUser(UserDto user)
        {
            if (user != null)
            {
                ExecNonQuery("EditUser", user);
            }
        }

        public void DeleteUser(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                ExecNonQuery("DeleteUser", new SqlParameter("@userName", userName));
            }
        }

        public void RemoveFromRoles(string userName, string roleName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(roleName))
            {
                var parametrs = new SqlParameter[]
                {
                   new SqlParameter("@userName",userName),
                   new SqlParameter("@roleName",roleName)
                };
                ExecNonQuery("DeleteFromRoles", parametrs);
            }
        }

        public void AddToRole(string userName, string roleName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(roleName))
            {
                var param = new SqlParameter[]
                {
                   new SqlParameter("@userName",userName),
                   new SqlParameter("@roleName",roleName)
                };
                ExecNonQuery("AddToRole", param);
            }
        }
    }
}
