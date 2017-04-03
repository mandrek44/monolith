using ABC.Infrastructure.Contracts;
using ABC.Infrastructure.Web.Defaults;
using Autofac;

namespace ABC.Support
{
    [AppName(Area)]
    public class SupportApp : IApp, IMenuItem, IDashboardWidget
    {
        public const string Area = "Support";

        public void OnApplicationStart(ContainerBuilder container)
        {
            container.RegisterType<CallsRepository>().AsSelf();
            container.RegisterType<SupportPerformanceMonitor>().AsSelf();

            DefaultRazorEngine.Initialize(GetType(), container);
            DefaultBundle.Css.IncludeDirectory($"~/Content/ABC.{Area}/", "*.css");
        }

        public ActionLink MenuLink => new ActionLink(
            area: Area,
            controller: nameof(CustomerCallController),
            action: nameof(CustomerCallController.Index),
            title: "Support Calls");

        public ActionLink WidgetLink => new ActionLink(
            area: SupportApp.Area,
            controller: nameof(CustomerCallWidgetController),
            action: nameof(CustomerCallWidgetController.Index),
            title: "Calls Report");
    }
}