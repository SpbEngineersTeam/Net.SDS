using Autofac;
using Net.SDS.ServiceDiscovery.Abstractions.Repositories;

namespace Net.SDS.ServiceDiscovery.DataAccess
{
    internal static class DataAccessCompositionRoot
    {
        internal static void ConfigureMongoDB(this ContainerBuilder builder)
        {
            builder.RegisterType<MongoDB.ServiceInstanceRepository>()
                   .As<IServiceInstanceRepository>()
                   .SingleInstance();
        }

        internal static void ConfigureInMemoryStub(this ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryStub.ServiceInstanceRepository>()
                   .As<IServiceInstanceRepository>()
                   .SingleInstance();
        }
    }

}
