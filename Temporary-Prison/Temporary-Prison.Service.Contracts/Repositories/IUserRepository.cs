using System.Collections;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repository
{
    public interface IUserRepository
    {
        bool IsValidUser(string userName, string password);
        UserDto GetUserByName(string userName);
        string[] GetRoles(string userName);
    }
}
