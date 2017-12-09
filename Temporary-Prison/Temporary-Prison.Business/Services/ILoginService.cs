using Temporary_Prison.Business.LogInState;

namespace Temporary_Prison.Business.Services
{
    public interface ILoginService
    {
        LogInStatus PasswordLogIn(string userName, string password);
        void Logout();
    }
}
