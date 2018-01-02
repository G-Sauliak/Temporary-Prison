using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.Services;
using Temporary_Prison.Service.Contracts.Contracts;
using System.ServiceModel;
using Temporary_Prison.Data.PrisonService;

namespace Temporary_Prison.Data.UTest.PriosnerClient
{
    
    [TestClass]
    public class PrisonerServiceClientTest
    {
        private IPrisonerDataService service;
        private DataAccessService db;

     

        [TestInitialize]
        public void TestInitialize()
        {
            service = new PrisonerDataService();
        }


        [TestMethod]
        public void PrisonerServiceClient_PrisonerForPagelistIsReturned()
        {
            int outResult;
            var result = service.GetPrisonersForPagedList(1, 3, out outResult);
            Assert.IsTrue(outResult > 0);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void registerDetent()
        {
            int id = 0;
            var emp = new Common.Entities.Employee()
            {
                FristName = "sfirstname",
                Position = "sposition",
                LastName = "slasname",
                Surname = "ssurname"
            };
            var regist = new RegistDetention()
            {
                PrisonerID = 19,
                DateOfArrival = DateTime.Now.Date,
                DateOfDetention = DateTime.Now.Date,
                PlaceofDetention = "stest PlaceofDetention",
                DeliveredEmployee = emp,
                DetainedEmployee = emp
            };

            service.RegisterDetention(regist);

            
        }
    }
}
