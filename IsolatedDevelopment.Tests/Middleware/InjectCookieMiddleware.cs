using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IsolatedDevelopment.Tests.Middleware
{
    public class InjectCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public InjectCookieMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.Headers.Add("x-foo", "bar");
            await _next(httpContext);
        }
    }
}
