using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business.UserManagers
{
    public interface IUserManager
    {
        void CreateUser(User user);
        void EditUser(User user);
        void EditUserRoles(string userName, string[] roles);
        void DeleteUser(string userName);
    }
}
