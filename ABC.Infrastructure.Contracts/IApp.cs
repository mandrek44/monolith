using Autofac;

namespace ABC.Infrastructure.Contracts
{
    public interface IApp
    {
        void OnApplicationStart(ContainerBuilder container);
    }
}