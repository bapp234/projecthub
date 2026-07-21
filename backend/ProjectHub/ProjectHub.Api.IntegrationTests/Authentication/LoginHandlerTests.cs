using Moq;
using ProjectHub.Api.IntegrationTests.Helpers;
using ProjectHub.Application.Features.Authentication.Commands.Login;
using ProjectHub.Application.Interfaces;
using ProjectHub.Domain.Entities;
using ProjectHub.Domain.Exceptions;
using ProjectHub.Domain.Interfaces;
using ProjectHub.Domain.ValueObjects;
using Task = System.Threading.Tasks.Task;
namespace ProjectHub.Api.IntegrationTests.Authentication;

public sealed class LoginHandlerTests
{
    private readonly Mock<IUserRepository> _repository = new();
    private readonly Mock<IPasswordHasher> _passwordHasher = new();
    private readonly Mock<IJwtProvider> _jwtProvider = new();

    private readonly LoginHandler _handler;

    public LoginHandlerTests()
    {
        _handler = new LoginHandler(
            _repository.Object,
            _passwordHasher.Object,
            _jwtProvider.Object);
    }
    [Fact]
    public async Task Handle_Should_ReturnLoginResult_WhenCredentialsAreValid()
    {
        // Arrange
        var command = new LoginCommand(
            "john@example.com",
            "Password@123");

        var user = TestUserFactory.Create();

        _repository
            .Setup(x => x.GetByEmailAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasher
            .Setup(x => x.Verify(
                command.Password,
                user.PasswordHash!))
            .Returns(true);

        _jwtProvider
            .Setup(x => x.GenerateAccessToken(user))
            .Returns("jwt-token");

        // Act
        var result = await _handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.Equal(user.Id, result.UserId);
        Assert.Equal(user.FullName, result.FullName);
        Assert.Equal(user.Email.Value, result.Email);
        Assert.Equal("jwt-token", result.AccessToken);

        _repository.Verify(
            x => x.GetByEmailAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _passwordHasher.Verify(
            x => x.Verify(
                command.Password,
                user.PasswordHash!),
            Times.Once);

        _jwtProvider.Verify(
            x => x.GenerateAccessToken(user),
            Times.Once);
    }
    [Fact]
    public async Task Handle_Should_ThrowInvalidCredentialsException_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new LoginCommand(
            "notfound@example.com",
            "Password@123");

        _repository
            .Setup(x => x.GetByEmailAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidCredentialsException>(
            () => _handler.Handle(
                command,
                CancellationToken.None));

        _passwordHasher.Verify(
            x => x.Verify(
                It.IsAny<string>(),
                It.IsAny<PasswordHash>()),
            Times.Never);

        _jwtProvider.Verify(
            x => x.GenerateAccessToken(It.IsAny<User>()),
            Times.Never);
    }
    [Fact]
    public async System.Threading.Tasks.Task Handle_Should_ThrowInvalidCredentialsException_WhenPasswordIsIncorrect()
    {
        // Arrange
        var command = new LoginCommand(
            "john@example.com",
            "WrongPassword");

        var user = TestUserFactory.Create();

        _repository
            .Setup(x => x.GetByEmailAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasher
            .Setup(x => x.Verify(
                command.Password,
                user.PasswordHash!))
            .Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidCredentialsException>(
            () => _handler.Handle(
                command,
                CancellationToken.None));

        _repository.Verify(
            x => x.GetByEmailAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _passwordHasher.Verify(
            x => x.Verify(
                command.Password,
                user.PasswordHash!),
            Times.Once);

        _jwtProvider.Verify(
            x => x.GenerateAccessToken(It.IsAny<User>()),
            Times.Never);
    }
    [Fact]
    public async System.Threading.Tasks.Task Handle_Should_GenerateJwt_ForAuthenticatedUser()
    {
        // Arrange
        var command = new LoginCommand(
            "john@example.com",
            "Password@123");

        var user = TestUserFactory.Create();

        _repository
            .Setup(x => x.GetByEmailAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasher
            .Setup(x => x.Verify(
                command.Password,
                user.PasswordHash!))
            .Returns(true);

        _jwtProvider
            .Setup(x => x.GenerateAccessToken(user))
            .Returns("jwt-token");

        // Act
        await _handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        _jwtProvider.Verify(
            x => x.GenerateAccessToken(user),
            Times.Once);
    }
}