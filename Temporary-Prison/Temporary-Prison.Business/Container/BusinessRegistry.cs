using StructureMap.Configuration.DSL;
using Temporary_Prison.Business.CacheManager;
using Temporary_Prison.Business.Providers;

namespace Temporary_Prison.Business.Container
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<IPrisonerProvider>().Use<PrisonerProvider>();
            For<IUserProvider>().Use<UserProvider>();
            For<ICacheService>().Use<CacheService>();
        }
    }
}
