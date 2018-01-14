using Autofac;
using Net.SDS.ServiceDiscovery.DataAccess;
using Net.SDS.ServiceDiscovery.Abstractions.Repositories;
using Net.SDS.ServiceDiscovery.Abstractions.Services;

namespace Net.SDS.ServiceDiscovery.Registry
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ServiceInstanceRepositoryInMemoryStub>()
                   .As<IServiceInstanceRepository>()
                   .SingleInstance();
        }
    }
}
