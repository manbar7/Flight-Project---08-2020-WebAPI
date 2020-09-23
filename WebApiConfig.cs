using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlightSystemWebAPI
{
    public class WebApiConfig : ApiController
    {
        
        
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                    new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            // nugget -- Microsoft.AspNet.WebApi.Cors
            //config.EnableCors();
            
        }
    }
}