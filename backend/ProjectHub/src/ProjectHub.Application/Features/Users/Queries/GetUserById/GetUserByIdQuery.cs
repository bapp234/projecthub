using MediatR;
using ProjectHub.Application.Features.Users.DTOs;

namespace ProjectHub.Application.Features.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid Id)
    : IRequest<UserDto?>;