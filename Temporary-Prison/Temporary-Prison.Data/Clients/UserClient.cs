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

            var users = new UserServiceClient().Execute(client => client.GetUsersForPagedList(skip, rowSize, out totalCountUsers));
            totalCount = totalCountUsers;

            return users;
        }

        public UserDto GetUserByName(string userName)
        {
            return new UserServiceClient().Execute(client => client.GetUserByName(userName));
        }

        public bool IsValidLogin(string userName, string password)
        {
            return new UserServiceClient().Execute(clinet => clinet.IsValidLogin(userName, password));
        }
    }
}
