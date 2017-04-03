using ABC.Infrastructure.Contracts;
using ABC.Support;
using Autofac;

namespace ABC.Sales.Support
{
    [AppRoute(Area)]
    public class SalesSupportAdapterApp : IApp
    {
        public const string Area = "SalesSupport";

        public void OnApplicationStart(ContainerBuilder container)
        {            
            container.RegisterType<SupportPerformanceAdaptaer>().AsImplementedInterfaces().AsSelf();
        }
    }   
}