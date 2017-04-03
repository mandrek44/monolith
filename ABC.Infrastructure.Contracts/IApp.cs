using System;
using Autofac;

namespace ABC.Infrastructure.Contracts
{
    public interface IApp
    {
        void OnApplicationStart(ContainerBuilder container);
    }

    public class AppRouteAttribute : Attribute
    {
        public string Route { get; }

        public AppRouteAttribute(string route)
        {
            Route = route;
        }

        public static string GetDefaultRoute(Type t) => t.Name;
    }
}