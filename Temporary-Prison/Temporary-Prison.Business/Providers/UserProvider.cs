using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;

namespace Temporary_Prison.Business.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserDataService userDataService;

        public UserProvider(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
        }

        public User GetUserByName(string userName)
        {
            return userDataService.GetUserByName(userName);
        }

        public IReadOnlyList<User> GetUsers()
        {
            throw new System.NotImplementedException();
        }

        public bool IsValidUser(string userName, string password)
        {
            return userDataService.IsValidLogin(userName, password);
        }
    }
}
