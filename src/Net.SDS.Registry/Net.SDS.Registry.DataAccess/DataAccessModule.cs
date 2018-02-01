using Autofac;
using Net.SDS.ServiceDiscovery.Abstractions.Repositories;

namespace Net.SDS.ServiceDiscovery.DataAccess
{
    public class DataAccessModule : Module
    {
        public bool InMemoryStub { get; set; } //TODO: later unnecessary

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            if (InMemoryStub)
            {
                ConfigureInMemoryStub(builder);
            }
            else
            {
                ConfigureMongoDB(builder);
            }
        }

        internal static void ConfigureMongoDB(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDB.ServiceInstanceRepository>()
                   .As<IServiceInstanceRepository>()
                   .SingleInstance();
        }

        internal static void ConfigureInMemoryStub(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryStub.ServiceInstanceRepository>()
                   .As<IServiceInstanceRepository>()
                   .SingleInstance();
        }
    }
}
