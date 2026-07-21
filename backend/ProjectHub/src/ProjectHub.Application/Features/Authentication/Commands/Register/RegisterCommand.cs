using MediatR;

namespace ProjectHub.Application.Features.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string FullName,
    string Email,
    string Password,
    string ConfirmPassword,
    string? AvatarUrl
) : IRequest<RegisterResult>;