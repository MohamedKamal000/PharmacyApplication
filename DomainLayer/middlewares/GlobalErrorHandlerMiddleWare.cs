using System.Text.Json;
using DataAccess;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DomainLayer.middlewares
{
    public class GlobalErrorHandlerMiddleWare
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
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
