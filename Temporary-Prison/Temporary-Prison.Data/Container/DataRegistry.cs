using StructureMap.Configuration.DSL;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.Converters;
using Temporary_Prison.Data.Services;

namespace Temporary_Prison.Data.Container
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IConvertPrisoners>().Use<ConvertPrisoners>();
            For<IDataService>().Use<DataService>();
            For<IPrisonClient>().Use<PrisonClient>();
        }
    }
}
