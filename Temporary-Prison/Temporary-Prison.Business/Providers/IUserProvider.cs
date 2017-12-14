using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business.Providers
{
    public interface IUserProvider
    {
        bool IsValidUser(string userName, string password);
        User GetUserByName(string userName);
        IReadOnlyList<User> GetUsers();
    }
}
