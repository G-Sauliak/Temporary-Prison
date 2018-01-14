using StructureMap.Configuration.DSL;
using Temporary_Prison.Business.CacheManager;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Business.Services;
using Temporary_Prison.Business.SiteConfigService;

namespace Temporary_Prison.Business.Container
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<ILoginService>().Use<LoginService>();
            For<IPrisonerProvider>().Use<PrisonerProvider>();
            For<IUserProvider>().Use<UserProvider>();
            For<ICacheService>().Use<CacheService>();
            For<IConfigService>().Use<ConfigService>();
        }
    }
}
