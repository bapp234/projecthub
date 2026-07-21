namespace ProjectHub.Application.Features.Authentication.Commands.Login;

public sealed record LoginResult(
    Guid UserId,
    string FullName,
    string Email,
    string AccessToken);