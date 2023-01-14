using CleanArchitecture.Application.Validation;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToHttpResponse<T>(this Result<T> result) {
        if (result.IsSuccess)
        {
            if (result.ValueOrDefault is Unit) {
                return new NoContentResult();
            }
            return new OkObjectResult(result.Value);
        }
        return HandleError(result);
    }
    public static ActionResult ToCreatedHttpResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new ObjectResult(result.Value) { 
                StatusCode = StatusCodes.Status201Created };
        }
        return HandleError(result);
    }

    private static ObjectResult HandleError<T>(Result<T> result)
    {
        var firstError = result.Errors.FirstOrDefault();

        if (firstError is ValidationError)
        {
            return HandleValidationError(result);
        }
        else if (firstError is NotFoundError)
        {
            return HandleNotFoundError(result);
        }

        return new BadRequestObjectResult(new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Unexpected Error",
            Detail = result.Errors.FirstOrDefault()?.Message ?? string.Empty
        });
    }

    private static ObjectResult HandleValidationError<T>(Result<T> result)
    {
        var details = new ValidationProblemDetails(new Dictionary<string, string[]> {
            { "ValidationError" , result.Errors.Select(c => c.Message).ToArray()}
        })
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Validation Error"
        };

        return new BadRequestObjectResult(details);
    }

    private static ObjectResult HandleNotFoundError<T>(Result<T> result)
    {
        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = result.Errors.FirstOrDefault()?.Message ?? string.Empty
        };

        return new NotFoundObjectResult(details);
    }
}
