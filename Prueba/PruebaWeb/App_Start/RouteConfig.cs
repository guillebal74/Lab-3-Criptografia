﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PruebaWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name:"PruebaController",
                url:"DesencriptarZigZag/{clave1}",
                new { controller = "Prueba", action = "DesencriptarZigZag", clave1 = UrlParameter.Optional}
                );
            routes.MapRoute(
                name:"PruebaController1",
                url:"ZigZagEncriptar/{clave}",
                new { controller = "Prueba", action = "ZigZagEncriptar", clave = UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Prueba", action = "CaesarEncriptar", id = UrlParameter.Optional }
            );
        }
    }
}
