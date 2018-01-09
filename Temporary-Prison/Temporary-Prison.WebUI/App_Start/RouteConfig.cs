using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Temporary_Prison
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "ListOfPrisoners",
               url: "{Prisoner}/{ListOfPrisoner}/{page}",
               defaults: new { controller = "Prisoner", action = "ListOfPrisoner", page = UrlParameter.Optional }
           );
        }
    }
}
