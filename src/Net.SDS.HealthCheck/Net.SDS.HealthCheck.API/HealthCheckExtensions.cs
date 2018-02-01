using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Net.SDS.HealthCheck.API
{
    public static class HealthCheckExtensions
    {
        public static void AddHeathCheck(this IServiceCollection services, Action<HealthCheckOptions> optionsBuilder)
        {
            var options = new HealthCheckOptions();
            optionsBuilder(options);
            var serviceRegistryUrl = new Uri(options.ServiceRegistryUrl);
            var serviceUrl = new Uri(options.ServiceUrl);
            var registrationHandler = new HeartBeat(
                serviceRegistryUrl,
                options.ServiceId,
                options.ServiceVersion,
                serviceUrl);
            
            services.AddSingleton(registrationHandler);

            registrationHandler.RunRegistrationLoop();
        }

        public static IApplicationBuilder UseHealthCheckApi(this IApplicationBuilder builder)
        {
            return builder.Map("/api/health-check", app => app.Run(_ => Task.CompletedTask));
        }
    }
}
