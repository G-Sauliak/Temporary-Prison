using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Data.Services
{
    public interface IUserDataService
    {
        User GetUserByName(string userName);
        bool IsValidLogin(string userName, string password);
        IReadOnlyList<User> GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers);
        IReadOnlyList<string> GetAllRoles();
        bool IsExistLogin(string userName);
        bool IsExistsByEmail(string email);
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(string userName);
        void RemoveFromRoles(string userName, string roleName);
        void AddToRole(string userName, string roleName);
    }
}
