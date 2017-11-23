using StructureMap.Configuration.DSL;
using Temporary_Prison.Business.Container;
using Temporary_Prison.Data.Container;

namespace Temporary_Prison.Dependencies.Registries
{
    public class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            Scan(scan => {
                scan.Assembly(typeof(BusinessRegistry).Assembly);
                scan.Assembly(typeof(DataRegistry).Assembly);
                scan.WithDefaultConventions();
            });
        }
    }
}
