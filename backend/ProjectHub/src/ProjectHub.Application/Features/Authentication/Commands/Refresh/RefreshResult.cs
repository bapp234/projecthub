namespace ProjectHub.Application.Features.Authentication.Commands.Refresh;

public sealed record RefreshResult(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAtUtc
);