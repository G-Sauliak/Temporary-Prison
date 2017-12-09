using StructureMap.Configuration.DSL;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Business.Services;

namespace Temporary_Prison.Business.Container
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<ILoginService>().Use<LoginService>();
            For<IPrisonerProvider>().Use<PrisonerProvider>();
            For<IUserProvider>().Use<UserProvider>();
        }
    }
}
