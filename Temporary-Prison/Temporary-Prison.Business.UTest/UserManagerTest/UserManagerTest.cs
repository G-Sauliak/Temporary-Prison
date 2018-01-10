using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Temporary_Prison.Business.CacheManager;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Business.UserManagers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;

namespace Temporary_Prison.Business.UTest.UserManagerTest
{
    [TestClass]
    public class UserManagerTest
    {
        private Mock<IUserProvider> userProvider;

        private IUserManager usermanager;

        [TestInitialize]
        public void TestInitialize()
        {
            usermanager = new UserManager(new UserDataService(),new UserProvider(new UserDataService(),new CacheService()));
        }

        [TestMethod]
        public void Business_UserManager_CreateUser()
        {
            var user = new User()
            {
                UserName = "Test",
                Email = "test@test.com",
                Roles = new string[] { "Adimn", "Editor" },
                Password = "hhrt112"
            };

            usermanager.CreateUser(user);

        }
    }
}
