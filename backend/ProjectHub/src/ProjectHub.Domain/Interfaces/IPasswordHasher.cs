using ProjectHub.Domain.ValueObjects;

namespace ProjectHub.Domain.Interfaces;

public interface IPasswordHasher
{
    PasswordHash Hash(string password);

    bool Verify(
        string password,
        PasswordHash passwordHash);
}