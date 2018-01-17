using Autofac;
using Net.SDS.ServiceDiscovery.Abstractions.Repositories;

namespace Net.SDS.ServiceDiscovery.DataAccess
{
    public class DataAccessMongoDbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ServiceInstanceRepository>()
                   .As<IServiceInstanceRepository>()
                   .SingleInstance();
        }
    }
}
