using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public interface IUserClient 
    {
        UserDto GetUserByName(string userName);
        bool IsValidLogin(string userName, string password);
    }
}
