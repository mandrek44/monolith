using System.Web.Optimization;
using ABC.Infrastructure.Contracts;
using ABC.Infrastructure.Web.Defaults;
using Autofac;

namespace ABC.Home
{
    [AppRoute(Area)]
    public class HomeApp : IApp, IMenuItem
    {
        public const string Area = "Home";

        public void OnApplicationStart(ContainerBuilder container)
        {
            container.RegisterInstance(this).AsImplementedInterfaces();

            DefaultRazorEngine.Initialize(GetType(), container);
            BundleTable.Bundles.GetBundleFor("~/bundle.css").IncludeDirectory($"~/Content/ABC.{Area}/", "*.css");
        }

        public ActionLink MenuLink { get; } = new ActionLink(
            Area, 
            nameof(HomeController), 
            nameof(HomeController.Index),
            "Home");
    }
}
