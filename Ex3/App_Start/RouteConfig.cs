using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("display", "display/{param1}/{param2}/{param3}",
            defaults: new { controller = "Map", action = "display", param3 = UrlParameter.Optional });

            routes.MapRoute("save", "save/{ip}/{port}/{timesPerSec}/{time}/{fileName}",
            defaults: new { controller = "Map", action = "save" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Map", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
