namespace ProjectHub.Application.Features.Authentication.Commands.Register;

public sealed record RegisterResult(
    Guid UserId,
    string FullName,
    string Email,
    string? AvatarUrl
);