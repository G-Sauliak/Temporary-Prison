using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temporary_Prison.Service.Contracts.Repositories;

namespace Temporary_Prison.Service.Contracts.UTest
{
    [TestClass]
    public class TestPrisonerRepository
    {
        private IPrisonerRepository prisonerRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            prisonerRepository = new PrisonerRepository();
        }

        [TestMethod]
        public void PrisonerRepository_PrisonerForPagelistIsReturned()
        {
            int outResult;
            var result = prisonerRepository.GetPrisonersForPagedList(1, 3, out outResult);
            Assert.IsTrue(outResult > 0);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void PrisonerRepository_GetPrisonerById()
        {
            int outResult;
            var result = prisonerRepository.GetPrisonerById(1);
            Assert.IsNotNull(result);
        }
    }
}
