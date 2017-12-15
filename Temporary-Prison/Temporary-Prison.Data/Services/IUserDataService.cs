using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Data.Services
{
    public interface IUserDataService
    {
        User GetUserByName(string userName);
        bool IsValidLogin(string userName, string password);
        IReadOnlyList<User> GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers);
    }
}
