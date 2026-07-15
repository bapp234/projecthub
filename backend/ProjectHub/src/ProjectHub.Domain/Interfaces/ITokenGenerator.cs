namespace ProjectHub.Domain.Interfaces;

public interface ITokenGenerator
{
    string GenerateAccessToken(Guid userId);

    string GenerateRefreshToken();
}