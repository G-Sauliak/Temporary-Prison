using Newtonsoft.Json;
using System.Web;
using System.Web.Security;
using Temporary_Prison.Business.SecurityPrincipal;
using Temporary_Prison.Common.Entities;


namespace Temporary_Prison.HttpModule
{ 

    public class Auth : IHttpModule
    {
        public void Dispose()
        {
           
        }

        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += Context_PostAuthenticateRequest;
        }

        private void Context_PostAuthenticateRequest(object sender, System.EventArgs e)
        {
            var authCookies = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
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