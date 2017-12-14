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
            HttpCookie authoCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authoCookies != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authoCookies.Value);
                var user = JsonConvert.DeserializeObject<User>(ticket.UserData);
                var UserIdentity = new UserIdentity(user);
                var UserPrincipal = new UserPrincipal(UserIdentity);
                HttpContext.Current.User = UserPrincipal;
            }
        }
    }
}