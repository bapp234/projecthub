using ProjectHub.Domain.Entities;
namespace ProjectHub.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    System.Threading.Tasks.Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default);

    System.Threading.Tasks.Task<RefreshToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default);

    System.Threading.Tasks.Task UpdateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default);
}