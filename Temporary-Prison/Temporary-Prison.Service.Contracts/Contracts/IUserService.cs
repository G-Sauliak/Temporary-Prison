using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.Sign)]
    public interface IUserService
    {
        [OperationContract(ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(DataErrorDto), ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        bool IsValidLogin(string userName, string password);

        [OperationContract(ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(DataErrorDto), ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        UserDto GetUserByName(string userName);

        [OperationContract(ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(DataErrorDto))]
        UserDto[] GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        string[] GetAllRoles();

        [OperationContract(ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(DataErrorDto), ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        void AddUser(UserDto user);

        [OperationContract(ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(DataErrorDto), ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
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

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void RemoveFromRoles(string userName, string roleName);
        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void AddToRole(string userName, string roleName);
    }
}
