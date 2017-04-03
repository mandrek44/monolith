using System.Web.Optimization;
using ABC.Infrastructure.Contracts;
using ABC.Infrastructure.Web.Defaults;
using Autofac;

namespace ABC.Sales
{
    [AppRoute(Area)]
    public class SalesApp : IApp, IMenuItem, IDashboardWidget
    {
        public const string Area = "Sales";

        public void OnApplicationStart(ContainerBuilder container)
        {
            container.RegisterType<SalesStatsRepoitory>().AsSelf();

            DefaultRazorEngine.Initialize(GetType(), container);
            DefaultBundle.Css.IncludeDirectory($"~/Content/ABC.{Area}/", "*.css");
        }

        public ActionLink MenuLink => new ActionLink(
            area: Area,
            controller: nameof(SalesController),
            action: nameof(SalesController.Index),
            title: "Sales");

        public ActionLink WidgetLink => new ActionLink(
            area: Area,
            controller: nameof(SalesStatsController),
            action: nameof(SalesStatsController.SalesStats),
            title: "Sales Stats");
    }
}