using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var result = userRepository.GetUsersForPagedList(1,2,out total);
            Assert.IsNotNull(result);
            Assert.IsTrue(total ==2);
        }
    }
}
