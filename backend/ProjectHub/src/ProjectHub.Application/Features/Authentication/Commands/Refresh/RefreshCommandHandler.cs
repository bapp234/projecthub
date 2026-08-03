using MediatR;
using ProjectHub.Application.Interfaces;
using ProjectHub.Domain.Constants;
using ProjectHub.Domain.Entities;
using ProjectHub.Domain.Exceptions;
using ProjectHub.Domain.Interfaces;

namespace ProjectHub.Application.Features.Authentication.Commands.Refresh;

public sealed class RefreshCommandHandler
    : IRequestHandler<RefreshCommand, RefreshResult>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<RefreshResult> Handle(
        RefreshCommand request,
        CancellationToken cancellationToken)
    {
        var refreshToken =
            await _refreshTokenRepository.GetByTokenAsync(
                request.RefreshToken,
                cancellationToken);
        if (refreshToken is null)
        {
            throw new InvalidRefreshTokenException();
        }
        if (refreshToken.IsExpired)
        {
            throw new RefreshTokenExpiredException();
        }
        if (refreshToken.IsRevoked)
        {
            throw new RefreshTokenRevokedException();
        }
        var user = refreshToken.User;
        var accessToken=_jwtProvider.GenerateAccessToken(user);
        var newRefreshToken =   _jwtProvider.GenerateRefreshToken();
        var refreshTokenExpiresAtUtc =
            _jwtProvider.GetRefreshTokenExpiration();
        var refreshTokenEntity= RefreshToken.Create(
            user.Id,
            newRefreshToken,
            refreshTokenExpiresAtUtc);
        refreshToken.Revoke(
            newRefreshToken,
            RefreshTokenMessages.ReplacedByRotation);

        await _refreshTokenRepository.AddAsync(
            refreshTokenEntity,
            cancellationToken);

        await _refreshTokenRepository.UpdateAsync(
            refreshToken,
            cancellationToken);
        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new RefreshResult(
    accessToken,
    newRefreshToken,
    refreshTokenExpiresAtUtc);
    }
}