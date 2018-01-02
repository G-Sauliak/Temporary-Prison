using System.Collections.Generic;
using log4net;
using Temporary_Prison.Data.UserService;


namespace Temporary_Prison.Data.Clients
{
    public class UserClient : IUserClient
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public IReadOnlyList<UserDto> GetUsersForPagedList(int skip, int rowSize, out int totalCount)
        {
            int totalCountUsers = default(int);

            var users = new UserServiceClient().Execute(client =>
            client.GetUsersForPagedList(skip, rowSize, out totalCountUsers));

            totalCount = totalCountUsers;
            return users;
        }

        public UserDto GetUserByName(string userName)
        {
            return new UserServiceClient().Execute(client =>
            client.GetUserByName(userName));
        }

        public IReadOnlyList<string> GetAllRoles()
        {
            return new UserServiceClient().Execute(client =>
            client.GetAllRoles());
        }

        public void AddUser(UserDto userDto)
        {
            new UserServiceClient().Execute(client =>
            client.AddUser(userDto));
        }

        public void EditUser(UserDto userDto)
        {
            new UserServiceClient().Execute(client =>
            client.EditUser(userDto));
        }

        public void DeleteUser(string userName)
        {
            new UserServiceClient().Execute(client =>
            client.DeleteUser(userName));
        }

        public void AddToRole(string userName, string roleName)
        {
            new UserServiceClient().Execute(client =>
            client.AddToRole(userName, roleName));
        }

        public bool IsExistLogin(string userName)
        {
            return new UserServiceClient().Execute(client =>
            client.IsExistsByLogin(userName));
        }

        public bool IsExistsByEmail(string email)
        {
            return new UserServiceClient().Execute(client =>
            client.IsExistsByEmail(email));
        }

        public bool IsValidLogin(string userName, string password)
        {
            return new UserServiceClient().Execute(client =>
            client.IsValidLogin(userName, password));
        }

        public void RemoveFromRoles(string userName, string roleName)
        {
            new UserServiceClient().Execute(client =>
            client.RemoveFromRoles(userName, roleName));
        }
    }
}
