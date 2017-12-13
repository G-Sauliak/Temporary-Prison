using System.Collections.Generic;
using System.ServiceModel;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    [ServiceContract]
    public interface IPrisonerService
    {
        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        List<PrisonerDto> GetPrisoners();

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        PrisonerDto GetPrisonerById(int Id);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        bool TryAddPrisoner(PrisonerDto prisoner, out int newId);
    }
}
