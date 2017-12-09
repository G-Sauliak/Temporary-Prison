using System.Linq;
using System.Security.Principal;

namespace Temporary_Prison.Business.SecurityPrincipal
{
    public class UserPrincipal : IPrincipal
    {
        private readonly UserIdentity userIdentity;

        public UserPrincipal(UserIdentity userIdentity)
        {
            this.userIdentity = userIdentity;
        }
        public IIdentity Identity
        {
            get { return userIdentity; }
        }

        public bool IsInRole(string role)
        {
            return userIdentity.User.Roles.Contains(role);  
        }
    }
}
