using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Net.SDS.ServiceDiscovery.Abstractions;
using Newtonsoft.Json;

namespace Net.SDS.ServiceDiscovery.API
{
    public class DomainErrorsHandlerMiddlware
    {
        private readonly RequestDelegate _next;

        public DomainErrorsHandlerMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            switch (exception)
            {
                case ServiceInstanceAlreadyExistsException _:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

    public static class DomainErrorsHandlerMiddlwareExtensions
    {
        public static IApplicationBuilder UseDomainErrorsHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DomainErrorsHandlerMiddlware>();
        }
    }
}
