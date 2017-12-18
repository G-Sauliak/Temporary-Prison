﻿using System.Collections.Generic;
using Temporary_Prison.Data.UserService;

namespace Temporary_Prison.Data.Clients
{
    public interface IUserClient 
    {
        UserDto GetUserByName(string userName);
        bool IsValidLogin(string userName, string password);
        IReadOnlyList<UserDto> GetUsersForPagedList(int skip, int rowSize, out int totalCount);
    }
}
