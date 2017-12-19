using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;
using Temporary_Prison.Service.Contracts.Repository;

namespace Temporary_Prison.Service.Contracts.UTest.TestsADORepositories
{
    [TestClass]
    public class TestUserRepository
    {
        private IUserRepository userRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepository = new UserRepository();
        }


        [TestMethod]
        public void UserRepository_GetUSersForPagedList()
        {
            int total;
            var result = userRepository.GetUsersForPagedList(0, 4, out total);
            Assert.IsNotNull(result);
            Assert.IsTrue(total > 4);
        }

        [TestMethod]
        public void UserRepository_GetAllRoles()
        {
            var result = userRepository.GetAllRoles();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }


        [TestMethod]
        public void UserRepository_GetUserByName()
        {
            var result = userRepository.GetUserByName("gen");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Password == "asd");
        }
        [TestMethod]
        public void UserRepository_AddUser()
        {
            var user = new UserDto()
            {
                Email = "gennfdgadiy082@gmail.com",
                Password = "123324",
                UserName = "fsdfsd",
                Roles = new string[] { "Admin", "Editor" }
            };
            userRepository.AddUser(user);
        }
        [TestMethod]
        public void UserRepository_IsExistLogin()
        {
            var userName = "gen";
            var result = userRepository.IsExistsByLogin(userName);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void UserRepository_IsExistByEmail()
        {
            var email = "gennadiy082@gmail.com";
            var result = userRepository.IsExistsByEmail(email);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void UserRepository_DeleteUser()
        {
            var userName = "fsdfsd";
            userRepository.DeleteUser(userName);
        }
        [TestMethod]
        public void UserRepository_UpdateUser()
        {
            var user = new UserDto()
            {
                Email = "gennfdgadiy082@gmail.com",
                Password = "asd",
                UserName = "gen",
            };
            userRepository.EditUser(user);
        }
    }
}
