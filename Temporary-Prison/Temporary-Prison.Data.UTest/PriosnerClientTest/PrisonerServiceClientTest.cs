using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;
using Temporary_Prison.Data.Clients;
using Moq;
using Temporary_Prison.Data.PrisonService;
using System.Collections.Generic;

namespace Temporary_Prison.Data.UTest.PriosnerClient
{

    [TestClass]
    public class PrisonerServiceClientTest
    {
        private IPrisonerDataService prisonerDataService;

        private Mock<IPrisonerClient> prisonerClient;

        [TestInitialize]
        public void TestInitialize()
        {
            prisonerClient = new Mock<IPrisonerClient>();
            prisonerDataService = new PrisonerDataService(prisonerClient.Object);
            prisonerDataService = new PrisonerDataService();
        }


        [TestMethod]
        public void PrisonerServiceClient_PrisonerForPagelist_isredtunerMore0()
        {
            var result = prisonerDataService.SearchFilter(null, null, null);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PrisonerServiceClient_DeteinonsForPagedlist_3isReturned()
        {
            var result = prisonerDataService.GetDetentionsByPrisonerIdForPagedList(19, 0, 4, out int outResult);
            Assert.IsTrue(outResult > 3);
        }

        [TestMethod]
        public void PrisonerServiceClient_GetDetentionById_isNotNull()
        {
            var result = prisonerDataService.GetDetentionById(28);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PrisonerDataService_SerachFilter_dateOfdtentionIsNull_returnMore0()
        {
            prisonerClient
                .Setup(e => e.GetDetentionById(It.IsAny<int>()))
                .Returns((DetentionDto detentionDto) =>
                {
                    if (detentionDto != null)
                    {
                        throw new NullReferenceException();
                    }
                    return detentionDto;
                });
            var actualResult = prisonerDataService.GetDetentionById(1);
            Assert.IsNotNull(actualResult);
     
        }

    }
}
