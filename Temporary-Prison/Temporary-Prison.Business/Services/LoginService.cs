using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Temporary_Prison.Business.LogInState;
using Temporary_Prison.Business.Providers;

namespace Temporary_Prison.Business.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserProvider userProvider;

        public LoginService(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public LogInStatus PasswordLogIn(string userName, string password)
        {
            if (userProvider.IsValidUser(userName, password))
            {
                var user = userProvider.GetUserByName(userName);
                var userData = JsonConvert.SerializeObject(user);
                var ticket = new FormsAuthenticationTicket(2, userName, DateTime.Now, DateTime.Now.AddHours(1), false, userData);
                var encTicket = FormsAuthentication.Encrypt(ticket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                HttpContext.Current.Response.Cookies.Add(authCookie);

                return LogInStatus.Success;
            }
            else
            {
                return LogInStatus.Failure;
            }
        }
    }
}
