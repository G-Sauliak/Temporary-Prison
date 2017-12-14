using log4net;
using Temporary_Prison.Data.UserService;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public class UserClient : IUserClient
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public UserDto GetUserByName(string userName)
        {
            return new UserServiceClient().GetResult(client => client.GetUserByName(userName));
        }

        public bool IsValidLogin(string userName, string password)
        {
            return new UserServiceClient().GetResult(clinet => clinet.IsValidLogin(userName, password));
        }
    }
}
