namespace ProjectHub.Api.ExceptionHandling;

public sealed record ExceptionMapping(
    int StatusCode,
    string Title,
    string Type);