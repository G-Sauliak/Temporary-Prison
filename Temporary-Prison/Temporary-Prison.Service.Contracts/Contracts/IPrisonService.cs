using System.Collections.Generic;
using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    [ServiceContract]
    public interface IPrisonService
    {
        [OperationContract]
        List<PrisonerDto> GetPrisoners();

        [OperationContract]
        List<PrisonerDto> GetPrisonerById();

    }
}
