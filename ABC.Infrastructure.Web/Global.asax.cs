using System;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ABC.Home;
using ABC.Infrastructure.Contracts;
using ABC.Infrastructure.Web.Defaults;
using ABC.Infrastructure.Web.Defaults.VirtualPath;
using ABC.Sales;
using ABC.Sales.Support;
using ABC.Support;
using Autofac;
using Autofac.Integration.Mvc;

namespace ABC.Infrastructure.Web
{
    public class Global : System.Web.HttpApplication
    {
        private readonly IApp[] _subApps = {
            new HomeApp(),
            new SupportApp(),
            new SalesApp(),
            new SalesSupportAdapterApp(),  };

        protected void Application_Start(object sender, EventArgs e)
        {
            BundleTable.Bundles.Add(new Bundle(DefaultBundle.CssName));
            BundleTable.Bundles.Add(new Bundle(DefaultBundle.JsName));

            var builder = CreateContainerBuilder();
            builder.RegisterType<ABCVirtualPathProvider>().AsSelf();

            foreach (var app in _subApps)
            {
                StartApplication(app, builder);
            }

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);            

#if DEBUG
            HostingEnvironment.RegisterVirtualPathProvider(container.Resolve<ABCVirtualPathProvider>());
#endif
        }

        private static void StartApplication(IApp app, ContainerBuilder container)
        {
            container.RegisterControllers(app.GetType().Assembly);
            container.RegisterInstance(app).AsSelf().AsImplementedInterfaces();

            ControllerBuilder.Current.DefaultNamespaces.Add(app.GetType().Namespace);
            DefaultRoute.CreateAppRoute(app.GetType());

            app.OnApplicationStart(container);
        }

        private static ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(Global).Assembly);
            builder.RegisterModelBinders(typeof(Global).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();

            return builder;
        }
    }

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                    name: "Default_withID",
                    url: "{controller}/{action}/{id}",
                    defaults: new {controller = "Home", action = "Index", area = HomeApp.Area, id = UrlParameter.Optional},
                    namespaces: new[] {typeof(HomeApp).Namespace})
                .WithDefaultSettings()
                .WithArea(HomeApp.Area);
        }
    }
}