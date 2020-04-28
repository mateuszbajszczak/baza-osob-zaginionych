using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RejestrOsobZaginionych
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "OsobyZaginione", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "OsobyZaginione",
                url: "{controller}/{action}/{name}/{id}"
            );
        }
    }
}
