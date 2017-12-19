using Newtonsoft.Json;
using System.Web;
using System.Web.Security;
using Temporary_Prison.Business.SecurityPrincipal;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison
{
    public partial class MvcApplication 
    {
        protected void Application_PostAuthenticateRequest()
        {
            var authCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookies != null)
            {
                var ticket = FormsAuthentication.Decrypt(authCookies.Value);
                var user = JsonConvert.DeserializeObject<User>(ticket.UserData);
                var UserIdentity = new UserIdentity(user);
                var UserPrincipal = new UserPrincipal(UserIdentity);
                HttpContext.Current.User = UserPrincipal;
            }
        }
    }
}