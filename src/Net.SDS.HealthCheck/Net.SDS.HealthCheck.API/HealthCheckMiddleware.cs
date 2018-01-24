using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Service.A
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    class HealthCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHealthInfoAccessor _healthInfoAccessor;

        public HealthCheckMiddleware(RequestDelegate next, IHealthInfoAccessor healthInfoAccessor)
        {
            _next = next;
            _healthInfoAccessor = healthInfoAccessor;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;

            if (httpContext.Request.Query.ContainsKey("flag"))
            {
                var flagStr = httpContext.Request.Query["flag"];
                if (Enum.TryParse(flagStr, out HealthFlags flag))
                {
                    var healthInfo = _healthInfoAccessor.GetHealthInfo(flag);
                    var healthInfoJson = Newtonsoft.Json.JsonConvert.SerializeObject(healthInfo);
                    await httpContext.Response.WriteAsync(healthInfoJson);
                }
                else
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await httpContext.Response.WriteAsync($"Can't parse {flagStr} to {nameof(HealthFlags)}");
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HealthCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder builder)
        {
            return builder.Map("/api/health", app => app.UseMiddleware<HealthCheckMiddleware>());
        }
    }
}
