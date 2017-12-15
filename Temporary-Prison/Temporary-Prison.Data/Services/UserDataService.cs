using System.Collections.Generic;
using AutoMapper;
using log4net;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        private readonly IUserClient userClient;

        public UserDataService(IUserClient userClient)
        {
            this.userClient = userClient;
        }

        public User GetUserByName(string userName)
        {
            var userDto = userClient.GetUserByName(userName);
            if (userDto != null)
            {
                var user = Mapper.Map<UserDto, User>(userDto);
                return user;
            }
            log.Error("Get user by name is null");

            return default(User);
        }

        public IReadOnlyList<User> GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers)
        {
            var usersDto = userClient.GetUsersForPagedList(skip, rowSize, out totalCountUsers);
            if (usersDto != null)
            {
                var users = Mapper.Map<IReadOnlyList<UserDto>, IReadOnlyList<User>>(usersDto);

                return users;
            }
            log.Error("DataUserService GetUsers is null");

            return default(IReadOnlyList<User>);
        }

        public bool IsValidLogin(string userName, string password)
        {
            return userClient.IsValidLogin(userName,password);
        }
    }
}
