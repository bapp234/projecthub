using MediatR;
using ProjectHub.Application.Features.Users.DTOs;

namespace ProjectHub.Application.Features.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string FullName,
    string Email,
    string Password,
    string? AvatarUrl
) : IRequest<UserDto>;