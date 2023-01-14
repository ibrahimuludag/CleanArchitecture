using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case ApplicationException:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Detail = exception.Message;
                problemDetails.Title = "Application Error";
                break;
            case KeyNotFoundException:
            case NotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                problemDetails.Detail = exception.Message;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                problemDetails.Title = "Not Found";
                break;
            case ValidationException ex:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails = new ValidationProblemDetails(ex.Errors);
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                problemDetails.Detail = ex.Message;
                problemDetails.Extensions.Add("invalidParams", ex.Errors);
                problemDetails.Title = "Validation Error";
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Detail = _env.IsDevelopment() ? $"{exception.Message} - {exception.StackTrace}" : "Internal Server error. Check Logs!";
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
                problemDetails.Title = "Server error";
                break;
        }
        _logger.LogError(exception, "Exception in request");
        var result = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(result);
    }
}