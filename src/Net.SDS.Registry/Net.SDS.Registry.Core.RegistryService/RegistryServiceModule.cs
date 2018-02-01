using Autofac;
using Net.SDS.ServiceDiscovery.Abstractions.Services;

namespace Net.SDS.ServiceDiscovery.Registry
{
    public class RegistryServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RegistryService>().As<IRegistryService>()
                   .InstancePerLifetimeScope();
        }
    }
}
