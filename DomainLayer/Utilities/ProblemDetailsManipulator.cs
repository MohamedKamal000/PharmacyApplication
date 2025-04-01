using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Utilities
{
    public static class ProblemDetailsManipulator
    {

        public static ProblemDetails CreateProblemDetailWithBadRequest(string details)
        {
            return new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Input Error",
                Type = "Input Error",
                Detail = details
            };
        }

    }
}
