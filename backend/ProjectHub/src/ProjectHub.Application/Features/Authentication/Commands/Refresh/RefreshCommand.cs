using MediatR;

namespace ProjectHub.Application.Features.Authentication.Commands.Refresh;

public sealed record RefreshCommand(
    string RefreshToken
) : IRequest<RefreshResult>;