using System.Text.Json;
using InfrastructureLayer.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PresentationLayer.Controllers;

namespace PresentationLayer.Filters
{
    public class DeleteUserAudit : IAsyncActionFilter
    {

        private readonly ILogger<UserController> _logger;
        
        
        public DeleteUserAudit(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            _logger.LogAdminBehaviour<UserController>("Admin Retreived user");
            await next();
        }
    }
}
