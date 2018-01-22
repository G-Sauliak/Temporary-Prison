using Temporary_Prison.Business.LogInState;

namespace Temporary_Prison.WebUI
{
    public interface ILoginService
    {
        LogInStatus PasswordLogIn(string userName, string password);
        void Logout();
    }
}
