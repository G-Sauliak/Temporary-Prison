using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repository
{
    public interface IUserRepository
    {
        bool IsValidUser(string userName, string password);
        UserDto GetUserByName(string userName);
        UserDto[] GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers);
        string[] GetAllRoles();
        void AddUser(UserDto user);
        void EditUser(UserDto user);
        void DeleteUser(string userName);
        bool IsExistsByLogin(string userName);
        bool IsExistsByEmail(string email);
        void RemoveFromRoles(string userName, string roleName);
        void AddToRole(string userName, string roleName);
    }
}
