using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Data.Services
{
    public interface IUserDataService
    {
        User GetUserByName(string userName);
        bool IsValidLogin(string userName, string password);
    }
}
