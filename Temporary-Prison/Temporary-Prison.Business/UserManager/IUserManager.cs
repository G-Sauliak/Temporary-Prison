﻿using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business.UserManagers
{
    public interface IUserManager
    {
        void CreateUser(User user);
        void EditUser(User user);
        void DeleteUser(string userName);
        void RemoveFromRoles(string userName, string roleName);
        void AddToRole(string userName, string roleName);
        IReadOnlyList<string> GetMissingRoles(string userName);
        UserAndRoles GetUserAndRoles(string userName);
    }
}
