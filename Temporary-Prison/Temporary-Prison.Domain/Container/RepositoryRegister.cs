using StructureMap.Configuration.DSL;
using Temporary_Prison.Domain.Repositories;

namespace Temporary_Prison.Domain.Container
{
    class RepositoryRegister : Registry
    {
        public RepositoryRegister()
        {
            For<IPrisonRepository>().Use<PrisonRepository>();
        }
    }
}
