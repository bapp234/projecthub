using MediatR;
using ProjectHub.Application.Features.Users.DTOs;

namespace ProjectHub.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FullName,
    string Email,
    string? AvatarUrl
) : IRequest<UserDto>;