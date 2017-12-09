using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Temporary_Prison.Business.SecurityPrincipal;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Dependencies.MapperRegistry;

namespace Temporary_Prison
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MapperProfiles.InitialiseMappers();
        }

        

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
