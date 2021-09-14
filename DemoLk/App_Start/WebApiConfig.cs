using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoLk
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            //routes to  Cmk.Wex.Controllers.Integro namespace (the order 'before internalapi' is imporytant?)
            config.Routes.MapHttpRoute("externalapi", "api/demo/{controller}/{action}");
            //routes to  Cmk.Wex.Controllers namespace
            config.Routes.MapHttpRoute("internalapi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });

        }
    }
}
