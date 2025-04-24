using Common.ExceptionBase;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.ExceptionHandlers;

public class DataNotFoundExceptionHandler(ILogger<DataNotFoundExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DataNotFoundException dataNotFoundException)
        {
            return false;
        }

        logger.LogError(
            dataNotFoundException,
            "Exception occurred: {Message}",
            dataNotFoundException.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Not Found",
            Detail = dataNotFoundException.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}