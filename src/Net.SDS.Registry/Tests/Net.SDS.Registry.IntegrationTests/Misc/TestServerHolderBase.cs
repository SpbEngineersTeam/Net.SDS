using System.Net.Http;
using Autofac.Extensions.DependencyInjection;
using AutoFixture;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Net.SDS.ServiceDiscovery.API;

namespace Net.SDS.IntegrationTests
{
    public abstract class TestServerHolderBase
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; }
        protected Fixture Fixture { get; }

        protected TestServerHolderBase()
        {
            Server = new TestServer(
                new WebHostBuilder()
                    .ConfigureServices(services => services.AddAutofac())
                    .UseStartup<Startup>()
            );

            Client = Server.CreateClient();

            Fixture = new Fixture();
        }
    }
}
