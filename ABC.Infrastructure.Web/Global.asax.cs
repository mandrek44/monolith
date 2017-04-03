﻿using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ABC.Infrastructure.Contracts;
using ABC.Infrastructure.Root;
using ABC.Infrastructure.Web.Defaults;
using ABC.Sales;
using ABC.Support;
using Autofac;
using Autofac.Integration.Mvc;

namespace ABC.Infrastructure.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            BundleTable.Bundles.Add(new Bundle("~/bundle.css"));
            BundleTable.Bundles.Add(new Bundle("~/bundle.js"));

            var builder = CreateContainerBuilder();

            var subApps = new IApp[] { new RootApp(), new SupportApp(), new SalesApp() };
            foreach (var app in subApps)
            {
                StartApplication(app, builder);
            }

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private static void StartApplication(IApp app, ContainerBuilder container)
        {
            container.RegisterControllers(app.GetType().Assembly);
            ControllerBuilder.Current.DefaultNamespaces.Add(app.GetType().Namespace);
            DefaultRoute.CreateAppRoute(app.GetType());

            app.OnApplicationStart(container);
        }

        private static ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(Global).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(Global).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
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
                    defaults: new {controller = "Home", action = "Index", area = RootApp.Area, id = UrlParameter.Optional},
                    namespaces: new[] {typeof(RootApp).Namespace})
                .WithDefaultSettings()
                .WithArea(RootApp.Area);
        }
    }
}