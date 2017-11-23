using StructureMap.Configuration.DSL;

namespace Temporary_Prison.Business.Container
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
     
            For<IPrisonProvider>().Use<PrisonProvider>();
        }
    }

}
