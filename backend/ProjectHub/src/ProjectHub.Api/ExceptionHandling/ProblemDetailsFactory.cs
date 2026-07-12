using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ProjectHub.Api.ExceptionHandling;

public static class ProblemDetailsFactory
{
    public static ProblemDetails Create(
        HttpContext httpContext,
        Exception exception,
        ExceptionMapping mapping)
    {
        if (exception is ValidationException validationException)
        {
            var validationProblemDetails = new ValidationProblemDetails(
                validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()))
            {
                Status = mapping.StatusCode,
                Title = mapping.Title,
                Type = mapping.Type,
                Detail = "One or more validation errors occurred.",
                Instance = httpContext.Request.Path
            };

            validationProblemDetails.Extensions["traceId"] =
                httpContext.TraceIdentifier;

            return validationProblemDetails;
        }

        var problemDetails = new ProblemDetails
        {
            Status = mapping.StatusCode,
            Title = mapping.Title,
            Type = mapping.Type,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["traceId"] =
            httpContext.TraceIdentifier;

        return problemDetails;
    }
}