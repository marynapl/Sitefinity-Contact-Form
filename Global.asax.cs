using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Newtonsoft.Json;
using SitefinityWebApp.Extensions;
using Telerik.Sitefinity.Abstractions;

namespace SitefinityWebApp
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Initialized += (o, args) =>
           {
               if (args.CommandName == "Bootstrapped")
               {                  
                   GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
                   GlobalConfiguration.Configuration.EnsureInitialized();

                   RegisterRoutes(RouteTable.Routes);                  

                   // DI for MVC controllers
                   NinjectControllerFactory.RegisterControllerFactory();
               }               
           };
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;
            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/",
                new { id = RouteParameter.Optional }
            );
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        private void Application_EndRequest(object source, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }
        
        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}