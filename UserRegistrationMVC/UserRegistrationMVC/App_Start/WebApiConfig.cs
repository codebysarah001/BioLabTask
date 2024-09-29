using System.Web.Http;

namespace UserRegistrationMVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable attribute routing
            config.MapHttpAttributeRoutes();

            // Default route (if necessary)
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
