using log4net;
using System;
using System.Collections.Generic;
using Temporary_Prison.Business.CacheManager;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;

namespace Temporary_Prison.Business.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserDataService userDataService;
        private readonly ICacheService cacheService;
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        public UserProvider(){}

        public UserProvider(IUserDataService userDataService, ICacheService cacheService)
        {
            this.cacheService = cacheService;
            this.userDataService = userDataService;
        }

        public void AddUser(User user)
        {
            userDataService.AddUser(user);
        }

        public IReadOnlyList<string> GetAllRoles()
        {
            var roles = default(IReadOnlyList<string>);
            var cacheKeyForPageList = "AllRolers";
            try
            {
                roles = cacheService.GetOrSet(cacheKeyForPageList,
                        () => userDataService.GetAllRoles());
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
            return roles;
        }

        public User GetUserByName(string userName)
        {
            return userDataService.GetUserByName(userName);
        }

        public IReadOnlyList<User> GetUsersForPagedList(int skip, int rowSize, ref int totalCount)
        {
            var cacheKeyForPageList = $"usersForPagelist_s_{skip}_r_{rowSize}_t_{totalCount}";
            var outTotalCount = default(int);
            var users = default(IReadOnlyList<User>);
            try
            {
                users = cacheService.GetOrSet(cacheKeyForPageList,
                    () => userDataService.GetUsersForPagedList(skip, rowSize, out outTotalCount));

                if (totalCount == default(int))
                {
                    cacheService.Remove(cacheKeyForPageList);
                }
                totalCount = outTotalCount;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
            return users;
        }

        public bool IsExistsByEmail(string email)
        {
            return userDataService.IsExistsByEmail(email);
        }

        public bool IsExistsByLogin(string userName)
        {
            return userDataService.IsExistLogin(userName);
        }

        public bool IsValidUser(string userName, string password)
        {
            return userDataService.IsValidLogin(userName, password);
        }
    }
}
