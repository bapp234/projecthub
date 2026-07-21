using MediatR;
using ProjectHub.Application.Interfaces;
using ProjectHub.Domain.Exceptions;
using ProjectHub.Domain.Interfaces;
using ProjectHub.Domain.ValueObjects;

namespace ProjectHub.Application.Features.Authentication.Commands.Login;

public sealed class LoginHandler
    : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<LoginResult> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var user = await _userRepository.GetByEmailAsync(
            email,
            cancellationToken);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }
        var isValidPassword = _passwordHasher.Verify(
            request.Password,
            user.PasswordHash!);
        if (!isValidPassword)
        {
            throw new InvalidCredentialsException();
        }
        var accessToken = _jwtProvider.GenerateAccessToken(user);
        return new LoginResult(
            user.Id,
            user.FullName,
            user.Email.Value,
            accessToken);
    }
}