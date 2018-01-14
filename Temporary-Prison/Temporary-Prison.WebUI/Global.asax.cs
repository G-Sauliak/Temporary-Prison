using System.Web.Mvc;
using System.Web.Routing;
using Temporary_Prison.Dependencies.MapperRegistry;
using Temporary_Prison.WebMapperProfile;

namespace Temporary_Prison
{
    public partial class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Dependencies.MapperRegistry.MapperProfiles.Configuration.AddProfile(new WebMapper());
            Dependencies.MapperRegistry.MapperProfiles.InitialiseMappers();
        }
      
    }
}
