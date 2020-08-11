using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp
{
    public class RequestInfoMiddleware
    {
        RequestDelegate _next;

        public RequestInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DebugLogger debugLogger)
        {
            var host = context.Request.Host.Value;
            var path = context.Request.Path;
            var query = context.Request.QueryString.Value;
            var secret = context.Request.Query["secret"];

            if (path.ToString().IndexOf("requestinfo", StringComparison.OrdinalIgnoreCase) > 0 && secret.Equals("qwerty"))
            {
                debugLogger.logger.LogInformation("Request Info was requested");
                
                await context.Response.WriteAsync($"<h3>Host: {host}</h3>" +
                                                  $"<h3>Path: {path}</h3>" +
                                                  $"<h3>Query: {query}</h3>");
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class ApplicationBuilderRequestInfoExtension
    {
        public static IApplicationBuilder UseRequestInfo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestInfoMiddleware>();
        }
    }
}
