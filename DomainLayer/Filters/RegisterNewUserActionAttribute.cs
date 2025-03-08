using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PresentationLayer.Filters
{
    public class RegisterNewUserActionAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!CheckKeysExist(context.ActionArguments.ToDictionary()))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestResult();
                context.HttpContext.Response.ContentType = "application/json"; 
                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize("Invalid Parameters"));
                return; 
            }

            if (!ValidateValues(context.ActionArguments.ToDictionary(), out string responseResult))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestResult();
                context.HttpContext.Response.ContentType = "application/json"; 
                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(responseResult));
                return; 
            }

            
            await next();
        }


        private bool CheckKeysExist(Dictionary<string, object?> requestInputDirectory)
        {
            return requestInputDirectory.ContainsKey("phoneNumber") &&
                   requestInputDirectory.ContainsKey("password") &&
                   requestInputDirectory.ContainsKey("userName");
        }

        private bool ValidateValues(Dictionary<string, object?> requestInputDictionary,out string responseError)
        {
            string phoneNumber = (string) requestInputDictionary["phoneNumber"];
            string password = (string) requestInputDictionary["password"];
            string userName = (string) requestInputDictionary["userName"];

            bool phoneNumberConstraint = phoneNumber.Length == 11;

            if (!phoneNumberConstraint)
            {
                responseError = "phoneNumber is not accepted";
                return false;
            }

            if (password.Length is > 200 or < 10)
            {
                responseError = "password length is not acceptable";
                return false;
            }

            if (userName.Length is > 40 or < 10)
            {
                responseError = "userName length is not acceptable";
                return false;
            }

            responseError = "";
            return true;
        }
    }
}
