using System.Security.Principal;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business.SecurityPrincipal
{
    public class UserIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }
        public User User { get; set; }

        public UserIdentity(User user)
        {
            Identity = new GenericIdentity(user.UserName);
            User = user;
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }
    }
}
