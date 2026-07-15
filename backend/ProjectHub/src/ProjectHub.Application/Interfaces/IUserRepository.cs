using ProjectHub.Domain.Entities;
using ProjectHub.Domain.ValueObjects;
using Task = System.Threading.Tasks.Task;
namespace ProjectHub.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<User?> GetByEmailAsync(
        Email email,
        CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}