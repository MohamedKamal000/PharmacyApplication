using System.Text.Json;
using DomainLayer.Interfaces;
using InfrastructureLayer.Logging;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.middlewares
{
    public class GlobalErrorHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlerMiddleWare> _logger;
        
        public GlobalErrorHandlerMiddleWare(RequestDelegate next,
            ILogger<GlobalErrorHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
            
                _logger.LogSystemBehaviour<GlobalErrorHandlerMiddleWare>("System error occurred",
                    LogLevel.Error,e);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                ProblemDetails problem = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "Internal Error",
                    Title = "Internal Error",
                    Detail = "Something wrong happen in the system"
                };

                string error = JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(error);
            }
        }
    }
}
