using ABC.Infrastructure.Contracts;
using ABC.Infrastructure.Web.Defaults;
using Autofac;

namespace ABC.Sales
{
    [AppRoute(Area)]
    public class SalesApp : IApp, IMenuItem
    {
        public const string Area = "sales";

        public void OnApplicationStart(ContainerBuilder container)
        {
            container.RegisterInstance(this).AsImplementedInterfaces();

            DefaultRazorEngine.Initialize(GetType());
        }

        public ActionLink MenuLink => new ActionLink(
            area: Area,
            controller: nameof(SalesController),
            action: nameof(SalesController.Index),
            title: "Sales");
    }
}