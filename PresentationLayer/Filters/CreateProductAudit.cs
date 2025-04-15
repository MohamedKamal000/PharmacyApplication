using System.Security.Claims;
using ApplicationLayer.Dtos.Product_DTOS;
using InfrastructureLayer.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PresentationLayer.Controllers;

namespace PresentationLayer.Filters;

public class CreateProductAudit : IAsyncActionFilter
{
    private readonly ILogger<ProductsController> _logger;
    
    public CreateProductAudit(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }
    
    // will add session id when i add the authen and authori
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionResult = await next();

        if (actionResult.Result is not  OkObjectResult)
        {
            return;
        }
        
        string args = "";
        
        if (context.ActionArguments.Values.First() is CreateProductDto)
        {
            var casted = context.ActionArguments.Values.First() as CreateProductDto;

            args = casted!.ToString();
        }
        else
        {
            args = "Couldn't Gather the arguments";
        }

        string? adminName = actionResult.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        string? phoneNumber = actionResult.HttpContext.User.FindFirst("PhoneNumber")?.Value;
        
        
        string message = $"Admin (Name: {adminName}, Phone: {phoneNumber})," +
                         $" Created Product with the following parameters: " + args;
        _logger.LogAdminBehaviour<CreateProductAudit>(message);
    }
}