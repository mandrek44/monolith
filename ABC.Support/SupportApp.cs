using System.Web.Optimization;
using ABC.Infrastructure.Contracts;
using ABC.Infrastructure.Web.Defaults;
using Autofac;

namespace ABC.Support
{
    [AppRoute(Area)]
    public class SupportApp : IApp, IMenuItem, IDashboardWidget
    {
        public const string Area = "Support";

        public void OnApplicationStart(ContainerBuilder container)
        {
            container.RegisterInstance(this).AsImplementedInterfaces();

            DefaultRazorEngine.Initialize(GetType(), container);

            BundleTable.Bundles.GetBundleFor("~/bundle.css").IncludeDirectory("~/Content/ABC.Support/", "*.css");
        }

        public ActionLink MenuLink => new ActionLink(
            area: Area,
            controller: nameof(CustomerCallController),
            action: nameof(CustomerCallController.Index),
            title: "Support Calls");

        public ActionLink WidgetLink => new ActionLink(SupportApp.Area,
           nameof(CustomerCallReportController),
           nameof(CustomerCallReportController.Index),
           "Calls Report");
    }   
}