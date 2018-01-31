using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Service.A
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHeathCheck(option =>
            {
                //todo: to Attribute of startup class
                option.ServiceId = Guid.Parse("85e3c269-28a5-494d-a90e-b2ba062648f4");
                //todo: to config
                option.ServiceRegistryUrl = "net.sds.registry.api:80";
                //todo: to config
                option.ServiceUrl = "sample.service.a:80";
                option.ServiceVersion = "0.0.1.0";
                option.ServiceName = GetType().Assembly.GetName().Name;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthCheckApi();
            app.UseMvc();
        }
    }
}
