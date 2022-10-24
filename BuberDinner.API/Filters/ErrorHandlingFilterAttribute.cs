using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BuberDinner.API.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var problemDetails = new ProblemDetails
        {
            Type = "https://httpstatuses.com/500",
            Title = "An error occurred while processing your request",
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = exception.Message
        };
        var errorResult = new { error = "An error occurred while processing your request" };
        
        context.Result = new ObjectResult(problemDetails);

        context.ExceptionHandled = true;
    }
}
