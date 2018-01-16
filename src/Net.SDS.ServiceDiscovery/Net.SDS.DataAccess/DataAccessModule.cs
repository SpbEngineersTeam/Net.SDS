using Autofac;
using Net.SDS.ServiceDiscovery.DataAccess;
using Net.SDS.ServiceDiscovery.Abstractions.Repositories;

namespace Net.SDS.ServiceDiscovery.DataAccess
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
