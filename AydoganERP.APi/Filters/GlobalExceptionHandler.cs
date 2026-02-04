using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Domain.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AydoganERP.Api.Filters;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IDictionary<Type, Action<HttpContext, Exception>> _exceptionHandlers;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
        _exceptionHandlers = new Dictionary<Type, Action<HttpContext, Exception>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            { typeof(BusinessRuleValidationException), HandleBusinessRuleException },
            { typeof(DomainException), HandleCustomException },
        };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

        HandleException(httpContext, exception);

        return true;
    }

    private void HandleException(HttpContext httpContext,
        Exception exception)
    {
        Type type = exception.GetType();
        
        if(type.BaseType == typeof(DomainException))
            type = type.BaseType;
        
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(httpContext, exception);
            return;
        }

        HandleUnknownException(httpContext, exception);
    }

    private void HandleValidationException(HttpContext httpContext,
        Exception exception)
    {
        var validatationException = exception as ValidationException;

        var details = new ValidationProblemDetails(validatationException.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = JsonConvert.SerializeObject(validatationException.Errors)
        };

        WriteProblemDetails(httpContext, details, StatusCodes.Status400BadRequest);
    }

    private void HandleNotFoundException(HttpContext httpContext,
        Exception exception)
    {
        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };

        WriteProblemDetails(httpContext, details, StatusCodes.Status404NotFound);
    }

    private void HandleUnauthorizedAccessException(HttpContext httpContext,
        Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        httpContext.Response.WriteAsJsonAsync(new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        });
    }

    private void HandleForbiddenAccessException(HttpContext httpContext,
        Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        httpContext.Response.WriteAsJsonAsync(new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        });
    }

    private void HandleBusinessRuleException(HttpContext httpContext,
        Exception exception)
    {
        var businessRoleException = exception as BusinessRuleValidationException;

        var details = new ValidationProblemDetails(businessRoleException.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = JsonConvert.SerializeObject(businessRoleException.Errors)
        };

        WriteProblemDetails(httpContext, details, StatusCodes.Status422UnprocessableEntity);
    }

    private void HandleUnknownException(HttpContext httpContext,
        Exception exception)
    {
        var details = new ProblemDetails()
        {
            Title = "An unexpected error occurred.",
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        WriteProblemDetails(httpContext, details, StatusCodes.Status500InternalServerError);
    }

    private void HandleCustomException(HttpContext httpContext,
        Exception exception)
    {
        var details = new ProblemDetails()
        {
            Title = "An unexpected error occurred.",
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        WriteProblemDetails(httpContext, details, StatusCodes.Status400BadRequest);
    }

    private async void WriteProblemDetails(HttpContext httpContext, ProblemDetails details, int statusCode)
    {
        details.Status = statusCode;
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(details);
    }
}
