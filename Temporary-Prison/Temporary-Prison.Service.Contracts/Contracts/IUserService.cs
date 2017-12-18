using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        bool IsValidLogin(string userName, string password);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        UserDto GetUserByName(string userName);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        UserDto[] GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        string[] GetAllRoles();

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void AddUser(UserDto user);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void EditUser(UserDto user);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void DeleteUser(string userName);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        bool IsExistsByLogin(string userName);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        bool IsExistsByEmail(string email);
    }
}
