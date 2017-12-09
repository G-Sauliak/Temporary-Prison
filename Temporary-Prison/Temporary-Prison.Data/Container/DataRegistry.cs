using StructureMap.Configuration.DSL;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.Services;

namespace Temporary_Prison.Data.Container
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IPrisonerDataService>().Use<PrisonerDataService>();
            For<IPrisonerClient>().Use<PrisonerClient>();
            For<IUserDataService>().Use<UserDataService>();
            For<IUserClient>().Use<UserClient>();
        }
    }
}
