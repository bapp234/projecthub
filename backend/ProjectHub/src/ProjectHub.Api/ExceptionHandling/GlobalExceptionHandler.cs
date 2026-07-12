using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProjectHub.Api.ExceptionHandling;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IProblemDetailsService _problemDetailsService;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            """
            Unhandled exception occurred.
            TraceId: {TraceId}
            RequestPath: {RequestPath}
            """,
            httpContext.TraceIdentifier,
            httpContext.Request.Path);

        var mapping = ExceptionMappings.GetMapping(exception);

        httpContext.Response.StatusCode = mapping.StatusCode;

        var problemDetails = ProblemDetailsFactory.Create(
            httpContext,
            exception,
            mapping);

        await _problemDetailsService.WriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = problemDetails
            });

        return true;
    }
}