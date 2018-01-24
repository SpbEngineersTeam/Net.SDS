using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using Net.SDS.ServiceDiscovery.API;

namespace Net.SDS.ServiceDiscovery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureServices(services => services.AddAutofac())
              //     .ConfigureServices(services => services.AddNetSds())
                   .UseStartup<Startup>()
                   .Build();
    }
}
