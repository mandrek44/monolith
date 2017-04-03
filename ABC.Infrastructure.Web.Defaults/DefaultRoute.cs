using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using ABC.Infrastructure.Contracts;

namespace ABC.Infrastructure.Web.Defaults
{
    public static class DefaultRoute
    {
        public static Route CreateAppRoute(Type appName)
        {
            var routeName = appName.GetCustomAttribute<AppRouteAttribute>()?.Route ?? AppRouteAttribute.GetDefaultRoute(appName).ToLower();

            return RouteTable.Routes.MapRoute(
                    name: routeName,
                    url: routeName + "/{controller}/{action}/{id}",
                    defaults: new {controller = "Root", action = "Index", id = UrlParameter.Optional, area = routeName},
                    namespaces: new[] {appName.Namespace})
                .WithDefaultSettings()
                .WithArea(routeName);
        }

        public static Route WithDefaultSettings(this Route route) => route.WithoutNamespaceFallback();

        public static Route WithoutNamespaceFallback(this Route route)
        {
            route.DataTokens["UseNamespaceFallback"] = false;
            return route;
        }

        public static Route WithArea(this Route route, string area)
        {
            route.DataTokens["area"] = area;
            return route;
        }
    }
}