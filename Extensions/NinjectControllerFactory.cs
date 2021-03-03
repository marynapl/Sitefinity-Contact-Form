using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Extensions
{
    public class NinjectControllerFactory : FrontendControllerFactory
    {
        private readonly IKernel kernel = Telerik.Sitefinity.Frontend.FrontendModule.Current.DependencyResolver;

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            var controller = kernel.Get(controllerType);

            return controller as IController;
        }

        private static void RegisterDependencies(IKernel dependencyResolver)
        {  
            //dependencyResolver.Bind<IDbService>().To<DbService>();
            dependencyResolver.Bind<IMailService>().To<MailService>();
        }

        public static void RegisterControllerFactory()
        {
            RegisterDependencies(Telerik.Sitefinity.Frontend.FrontendModule.Current.DependencyResolver);
            ObjectFactory.Container.RegisterType<ISitefinityControllerFactory, NinjectControllerFactory>(new ContainerControlledLifetimeManager());
            var factory = ObjectFactory.Resolve<ISitefinityControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
    }

    // To inject dependency in Widget Controllers we would need to inherit from FrontendControllerFactory 
    // This class is needed to configure NInject DI
    // After the DI has been injected in Startup, our controllers will be created by the factory presented above.
    // Dependency for Widget controller is done through Ninject DI container, whereas dependency for API controllers is managed by Unity DI container
}