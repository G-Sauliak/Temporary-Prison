using System.Collections.Generic;
using Temporary_Prison.Data.UserService;

namespace Temporary_Prison.Data.Clients
{
    public interface IUserClient 
    {
        UserDto GetUserByName(string userName);
        bool IsValidLogin(string userName, string password);
        IReadOnlyList<UserDto> GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers);
        IReadOnlyList<string> GetAllRoles();
        bool IsExistLogin(string userName);
        bool IsExistsByEmail(string email);
        void AddUser(UserDto user);
        void EditUser(UserDto user);
        void DeleteUser(string userName);
        void RemoveFromRoles(string userName, string roleName);
        void AddToRole(string userName, string roleName);

    }
}
