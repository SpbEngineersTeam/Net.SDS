using Autofac;

namespace Net.SDS.ServiceDiscovery.DataAccess
{
    public class DataAccessModule : Module
    {
        public bool InMemoryStub { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            if (InMemoryStub)
            {
                builder.ConfigureInMemoryStub();
            }
            else
            {
                builder.ConfigureMongoDB();
            }
        }
    }

}
