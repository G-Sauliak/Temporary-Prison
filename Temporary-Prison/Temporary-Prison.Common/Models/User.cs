using System.Collections.Generic;

namespace Temporary_Prison.Common.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public string Password { get; set; }
    }
    public class UserAndRoles
    {
        public string UserName { get; set; }
        public IReadOnlyList<string> Roles { get; set; }
    }
}
