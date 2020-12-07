using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using rulesencyclopediabackend;


namespace rulesencyclopediabackend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //routing traffic to the correct controller. routeparameters are optional.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
