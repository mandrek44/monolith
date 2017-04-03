using System;
using System.Reflection;

namespace ABC.Infrastructure.Contracts
{
    public class AppNameAttribute : Attribute
    {
        public string Route { get; }

        public AppNameAttribute(string route)
        {
            Route = route;
        }

        public static string GetDefaultRoute(Type t) => t.Name;
    }

    public static class AppNameHelper
    {
        public static string GetName(this IApp app) => GetAppName(app.GetType());

        public static string GetAppName(this Type appType) =>
            appType.GetCustomAttribute<AppNameAttribute>()?.Route
            ?? AppNameAttribute.GetDefaultRoute(appType);
    }
}