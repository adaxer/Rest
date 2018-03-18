using NorthwindRest.CoreWebApi.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSimpleAuthentication(this IApplicationBuilder app)
        {
            app.UseMiddleware<SimpleAuthentication>();
            return app;
        }
    }
}
