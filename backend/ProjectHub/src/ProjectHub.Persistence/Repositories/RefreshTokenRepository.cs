using Microsoft.EntityFrameworkCore;
using ProjectHub.Domain.Entities;
using ProjectHub.Domain.Interfaces;
using ProjectHub.Persistence.Database;

namespace ProjectHub.Persistence.Repositories;

public sealed class RefreshTokenRepository
    : IRefreshTokenRepository
{
    private readonly ProjectHubDbContext _context;

    public RefreshTokenRepository(
        ProjectHubDbContext context)
    {
        _context = context;
    }

    public async System.Threading.Tasks.Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default)
    {
        await _context.RefreshTokens.AddAsync(
            refreshToken,
            cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(
                x => x.Token == token,
                cancellationToken);
    }

    public System.Threading.Tasks.Task UpdateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default)
    {
        _context.RefreshTokens.Update(refreshToken);

        return System.Threading.Tasks.Task.CompletedTask;
    }
}