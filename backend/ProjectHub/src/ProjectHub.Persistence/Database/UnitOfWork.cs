using ProjectHub.Application.Interfaces;

namespace ProjectHub.Persistence.Database;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ProjectHubDbContext _context;

    public UnitOfWork(ProjectHubDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}