using System.Collections.Generic;
using Temporary_Prison.Common.Entities;

namespace Temporary_Prison.Business.Providers
{
    public interface IUserProvider
    {
        bool IsValidUser(string userName, string password);
        User GetUserByName(string userName);
        IReadOnlyList<User> GetUsersForPagedList(int skip, int rowSize, ref int totalCount);
        IReadOnlyList<string> GetAllRoles();
        bool IsExistsByLogin(string userName);
        bool IsExistsByEmail(string userName);
    }
}
