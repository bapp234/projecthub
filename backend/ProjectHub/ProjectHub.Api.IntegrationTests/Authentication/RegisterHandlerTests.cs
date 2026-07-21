using Moq;
using ProjectHub.Application.Features.Authentication.Commands.Register;
using ProjectHub.Application.Interfaces;
using ProjectHub.Domain.Exceptions;
using ProjectHub.Domain.Interfaces;
using ProjectHub.Domain.ValueObjects;

namespace ProjectHub.Api.IntegrationTests.Authentication;

public sealed class RegisterHandlerTests
{
    private readonly Mock<IUserRepository> _repository;
    private readonly Mock<IPasswordHasher> _passwordHasher;

    public RegisterHandlerTests()
    {
        _repository = new Mock<IUserRepository>();

        _passwordHasher = new Mock<IPasswordHasher>();
    }

    [Fact]
    public async Task Handle_Should_Create_User_Successfully()
    {
        // Arrange

        var command = new RegisterCommand(
    "John Doe",
    "john@test.com",
    "123456",
    "123456",
    null);

        _repository
            .Setup(x => x.GetByEmailAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProjectHub.Domain.Entities.User?)null);

        _passwordHasher
            .Setup(x => x.Hash(command.Password))
            .Returns(
                PasswordHash.Create("hashed-password"));

        var handler = new RegisterHandler(
            _repository.Object,
            _passwordHasher.Object);

        // Act

        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert

        Assert.NotNull(result);

        Assert.Equal(
            command.FullName,
            result.FullName);

        Assert.Equal(
            command.Email,
            result.Email);

        _repository.Verify(
            x => x.AddAsync(
                It.IsAny<ProjectHub.Domain.Entities.User>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _repository.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
    [Fact]
    public async Task Handle_Should_Throw_When_Email_Already_Exists()
    {
        // Arrange

        var command = new RegisterCommand(
            "John Doe",
            "john@test.com",
            "123456",
            "123456",
            null);

        var existingUser = Helpers.TestUserFactory.Create(
            fullName: "Existing User",
            email: "john@test.com");

        _repository
            .Setup(x => x.GetByEmailAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        var handler = new RegisterHandler(
            _repository.Object,
            _passwordHasher.Object);

        // Act + Assert

        await Assert.ThrowsAsync<DuplicateEmailException>(
            () => handler.Handle(
                command,
                CancellationToken.None));

        _repository.Verify(
            x => x.AddAsync(
                It.IsAny<ProjectHub.Domain.Entities.User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _repository.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
    [Fact]
    public async Task Handle_Should_Hash_Password_Before_Creating_User()
    {
        // Arrange

        var command = new RegisterCommand(
            "John Doe",
            "john@test.com",
            "123456",
            "123456",
            null);

        var hashedPassword = PasswordHash.Create("hashed-password");

        _repository
            .Setup(x => x.GetByEmailAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProjectHub.Domain.Entities.User?)null);

        _passwordHasher
            .Setup(x => x.Hash(command.Password))
            .Returns(hashedPassword);

        var handler = new RegisterHandler(
            _repository.Object,
            _passwordHasher.Object);

        // Act

        await handler.Handle(
            command,
            CancellationToken.None);

        // Assert

        _passwordHasher.Verify(
            x => x.Hash(command.Password),
            Times.Once);

        _repository.Verify(
            x => x.AddAsync(
                It.Is<ProjectHub.Domain.Entities.User>(
                    u => u.PasswordHash != null &&
                         u.PasswordHash.Equals(hashedPassword)),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}