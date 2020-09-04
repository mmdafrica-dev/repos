using Microsoft.AspNetCore.Builder;

namespace Service.API1.API.Middleware
    {
    public static class HttpContextMiddlewareExtensions
        {
        public static IApplicationBuilder UseHttpContextMiddleware(this IApplicationBuilder builder)
            {
            return builder.UseMiddleware<HttpContextMiddleware>();
            }
        }
    }




