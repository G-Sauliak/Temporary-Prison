using System;
using Temporary_Prison.Business.Enums;
using Temporary_Prison.Business.Exceptions;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;

namespace Temporary_Prison.Business.UserManagers
{
    public class UserManager : IUserManager
    {
        private readonly IUserDataService userDataService;
        private readonly IUserProvider userProvider;
        public UserManager() { }

        public UserManager(IUserDataService userDataService, IUserProvider userProvider)
        {
            this.userProvider = userProvider;
            this.userDataService = userDataService;
        }

        public void CreateUser(User user)
        {

            if (userProvider.IsExistsByLogin(user.UserName))
            {
                throw new CreateUserException(UserCreateStatus.DuplicateUserName);
            }
            if (userProvider.IsExistsByEmail(user.Email))
            {
                throw new CreateUserException(UserCreateStatus.DuplicateEmail);
            }
            //Encrypt pass
            //user.Password

            userDataService.AddUser(user);

        }

        public void EditUserRoles(string userName, string[] roles)
        {
            throw new NotImplementedException();
        }

        public void EditUser(User updatedUser)
        {
            var currentUser = userProvider.GetUserByName(updatedUser.UserName);

            if (userProvider.IsExistsByEmail(updatedUser.Email) && currentUser.Email != updatedUser.Email)
            {
                throw new CreateUserException(UserCreateStatus.DuplicateEmail);
            }

            userDataService.EditUser(updatedUser);
        }

        public void DeleteUser(string userName)
        {
            if (userName != null)
            {
                userDataService.DeleteUser(userName);
            }
        }
    }
}
