using MediatR;
using ProjectHub.Application.Interfaces;
using ProjectHub.Domain.Entities;
using ProjectHub.Domain.Exceptions;
using ProjectHub.Domain.Interfaces;
using ProjectHub.Domain.ValueObjects;

namespace ProjectHub.Application.Features.Authentication.Commands.Register;

public sealed class RegisterHandler
    : IRequestHandler<RegisterCommand, RegisterResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResult> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var existingUser= await _userRepository.GetByEmailAsync(email, cancellationToken);
        if(existingUser is not null)
        {
            throw new DuplicateEmailException(email.Value);
        }
        var hashedPassword = _passwordHasher.Hash(request.Password);
        var user = User.Create(request.FullName, email, hashedPassword, request.AvatarUrl);
        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
        return new RegisterResult(
            user.Id,
            user.FullName,
            user.Email.Value,
            user.AvatarUrl);
    }
}