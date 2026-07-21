using ProjectHub.Domain.Entities;
using ProjectHub.Domain.ValueObjects;

namespace ProjectHub.Api.IntegrationTests.Helpers;

public static class TestUserFactory
{
    public static User Create(
        string? fullName = null,
        string? email = null,
        string? password = null,
        string? avatarUrl = null)
    {
        var emailVo = Email.Create(
            email ?? "john.doe@test.com");

        var passwordHash = PasswordHash.Create(
            password ?? "$2a$11$abcdefghijklmnopqrstuv");

        return User.Create(
            fullName ?? "John Doe",
            emailVo,
            passwordHash,
            avatarUrl);
    }
}