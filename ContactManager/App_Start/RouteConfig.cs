using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContactManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                   name: "EDIT",
                   url: "{controller}/{action}/{id}",
                   defaults: new { controller = "Contact", action = "Edit", id = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "Delete",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Contact", action = "Delete", id = UrlParameter.Optional }
           );
        }
    }
}
