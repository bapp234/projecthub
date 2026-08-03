using ProjectHub.Domain.Entities;

namespace ProjectHub.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();

    DateTime GetAccessTokenExpiration();

    DateTime GetRefreshTokenExpiration();
}