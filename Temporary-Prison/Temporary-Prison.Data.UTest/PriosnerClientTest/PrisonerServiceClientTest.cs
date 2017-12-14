using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temporary_Prison.Data.Clients;

namespace Temporary_Prison.Data.UTest.PriosnerClient
{
    
    [TestClass]
    public class PrisonerServiceClientTest
    {
        private IPrisonerClient prisonerClient;

        [TestInitialize]
        public void TestInitialize()
        {
            prisonerClient = new PrisonerClient();
        }

        [TestMethod]
        public void PrisonerServiceClient_PrisonerForPagelistIsReturned()
        {
            int outResult;
            var result = prisonerClient.GetPrisonersForPagedList(1, 3, out outResult);
            Assert.IsTrue(outResult > 0);
            Assert.IsNotNull(result);
        }
    }
}
