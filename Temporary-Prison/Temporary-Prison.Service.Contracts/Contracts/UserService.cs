using log4net;
using System.Data.SqlClient;
using System.Linq;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class UserService : IUserService
    {
        private readonly DataAccessService context = new DataAccessService();
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public void AddUser(UserDto user)
        {
            if (user != null)
            {
                context.ExecNonQuery("insertUser", user);

                var parametrs = new SqlParameter[]
                {
                new SqlParameter("@RoleName",user.Roles.First()),
                new SqlParameter("@UserName",user.UserName)
                };
                context.ExecNonQuery("AddToRole", parametrs);
            }
        }

        public string[] GetAllRoles()
        {
            return context.GetArrayByColumn<string>("GetAllRoles", "RoleName");
        }

        public UserDto GetUserByName(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var user = context.ExecProcGetModel<UserDto>("GetUserByName", new SqlParameter("@userName", userName));
                user.Roles = context.GetArrayByColumn<string>("GetRolesByUserName", "RoleName", new SqlParameter("@userName", userName));
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

                return context.ExecProcGetModels<UserDto, int>("GetUsersForPagedList", "TotalCount", out totalCountUsers, param);
            }
            totalCountUsers = default(int);
            return default(UserDto[]);
        }

        public bool IsExistsByLogin(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return context.ExecScalarValued<bool>("SELECT dbo.IsExistsLogin(@userName)",
                    new SqlParameter("@userName", userName));
            }
            return default(bool);
        }

        public bool IsExistsByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return context.ExecScalarValued<bool>("SELECT dbo.IsExistsLogin(@email)",
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
                return context.ExecScalarValued<bool>("SELECT dbo.IsValidUser(@name,@password)", parametrs);
            }
            return default(bool);
        }

        public void EditUser(UserDto user)
        {
            if (user != null)
            {
                context.ExecNonQuery("EditUser", user);
            }
        }

        public void DeleteUser(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                context.ExecNonQuery("DeleteUser", new SqlParameter("@userName", userName));
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
                context.ExecNonQuery("DeleteFromRoles", parametrs);
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
                context.ExecNonQuery("AddToRole", param);
            }
        }
    }
}
