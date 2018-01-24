using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Service.A
{
    public static class HealthCheckExtensions
    {
        public static void AddHeathCheckApi(this IServiceCollection services, RegistrationInfoBase serviceInfo)
        {
            services.AddSingleton((IServiceProvider serviceProvider) =>
            {
                var counfig = (IConfiguration)serviceProvider.GetService(typeof(IConfiguration));
                var connString = counfig.GetConnectionString("NetSDS");
                new RegistrationHandler(connString, serviceInfo).Register();
                return (IHealthInfoAccessor)serviceProvider.GetRequiredService(typeof(HealthInfoAccessor));
            });
        }
    }
}
