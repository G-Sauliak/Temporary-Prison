using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    [ServiceContract]
    public interface IPrisonerService
    {
        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        PrisonerDto[] GetPrisonersForPagedList(int skip, int rowSize, out int totalCount);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        PrisonerDto GetPrisonerById(int Id);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        bool AddPrisoner(PrisonerDto prisoner, out int newId);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        PrisonerDto[] FindPrisonersByName(string search);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void EditPrisoner(PrisonerDto prisoner);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void DeletePrisoner(int id);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void RegisterDetention(RegistrationOfDetentionDto registrationOfDetention);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void ReleaseOfPrisoner(ReleaseOfPrisonerDto release);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        DetentionPagedListDto[] GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        DetentionDto GetDetentionById(int id);
    }
}
