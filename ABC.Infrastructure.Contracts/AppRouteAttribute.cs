using System;
using System.Reflection;

namespace ABC.Infrastructure.Contracts
{
    public class AppRouteAttribute : Attribute
    {
        public string Route { get; }

        public AppRouteAttribute(string route)
        {
            Route = route;
        }

        public static string GetDefaultRoute(Type t) => t.Name;
    }

    public static class AppRouteHelper
    {
        public static string GetRoute(this IApp app) => GetAppRoute(app.GetType());

        public static string GetAppRoute(this Type appType) =>
            appType.GetCustomAttribute<AppRouteAttribute>()?.Route
            ?? AppRouteAttribute.GetDefaultRoute(appType);
    }
}