using Microsoft.AspNetCore.Mvc.Filters;

namespace PresentationLayer.middlewares
{
    public class RateLimiterMiddleWare
    {
        private readonly RequestDelegate _next;
        
        public RateLimiterMiddleWare(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            
            
            await _next(httpContext);
        }
        
    }
}
