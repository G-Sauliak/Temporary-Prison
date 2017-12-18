using System.Collections.Generic;
using AutoMapper;
using log4net;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.UserService;

namespace Temporary_Prison.Data.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        private readonly IUserClient userClient;
        public UserDataService() { }

        public UserDataService(IUserClient userClient)
        {
            this.userClient = userClient;
        }

        public void AddUser(User user)
        {
            var userDto = default(UserDto);
            try
            {
                userDto = Mapper.Map<User, UserDto>(user);
            }
            catch (AutoMapperMappingException me)
            {
                log.Error(me.Message);
            }
            new UserServiceClient().Execute(client => client.AddUser(userDto));
        }

        public void DeleteUser(string userName)
        {
            new UserServiceClient().Execute(client => client.DeleteUser(userName));
        }

        public void EditUser(User user)
        {
            if (user != null)
            {
                var userDto = Mapper.Map<User, UserDto>(user);
                new UserServiceClient().Execute(client => client.EditUser(userDto));
            }
        }

        public IReadOnlyList<string> GetAllRoles()
        {
            var roles = new UserServiceClient().Execute(client => client.GetAllRoles());
            if (roles != null)
            {
                return roles;
            }
            log.Error("Get All Roles Is Null");
            return default(string[]);
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

        public bool IsExistLogin(string userName)
        {
            return new UserServiceClient().Execute(client => client.IsExistsByLogin(userName));
        }

        public bool IsExistsByEmail(string email)
        {
            return new UserServiceClient().Execute(client => client.IsExistsByEmail(email));
        }

        public bool IsValidLogin(string userName, string password)
        {
            return userClient.IsValidLogin(userName, password);
        }
    }
}
