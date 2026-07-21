using MediatR;

namespace ProjectHub.Application.Features.Authentication.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password)
    : IRequest<LoginResult>;