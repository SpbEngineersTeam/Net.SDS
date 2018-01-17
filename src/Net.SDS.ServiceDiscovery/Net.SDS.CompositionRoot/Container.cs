using Autofac;
using Net.SDS.ServiceDiscovery.DataAccess;
using Net.SDS.ServiceDiscovery.Registry;

namespace Net.SDS.CompositionRoot
{
    public static class Container
    {
        public static void ConfigureContainer(this ContainerBuilder builder)
        {
            builder.RegisterModule<RegistryServiceModule>();
            builder.RegisterModule<DataAccessInMemoryStubModule>();
        }
    }
}
