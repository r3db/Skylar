using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Skylar
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            RouteTable.Routes.MapMvcAttributeRoutes();
            GlobalConfiguration.Configuration.EnsureInitialized(); 
        }
    }
}