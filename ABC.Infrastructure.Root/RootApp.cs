using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;
using ABC.Infrastructure.Contracts;
using ABC.Infrastructure.Web.Defaults;
using Autofac;

namespace ABC.Infrastructure.Root
{
    [AppRoute(Area)]
    public class RootApp : IApp, IMenuItem
    {
        public const string Area = "Root";

        public void OnApplicationStart(ContainerBuilder container)
        {
            container.RegisterInstance(this).AsImplementedInterfaces();

            DefaultRazorEngine.Initialize(GetType());

            BundleTable.Bundles.GetBundleFor("~/bundle.css").IncludeDirectory("~/Content/ABC.Infrastructure.Root/", "*.css");
        }

        public ActionLink MenuLink { get; } = new ActionLink(
            Area, 
            nameof(HomeController), 
            nameof(HomeController.Index),
            "Home");
    }
}
