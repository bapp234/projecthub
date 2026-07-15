using MediatR;
using ProjectHub.Application.Features.Users.DTOs;
using ProjectHub.Application.Interfaces;
using ProjectHub.Domain.Entities;
using ProjectHub.Domain.Exceptions;
using ProjectHub.Domain.ValueObjects;

namespace ProjectHub.Application.Features.Authentication.Commands.Register;

public sealed class RegisterHandler
    : IRequestHandler<RegisterCommand, UserDto>
{
    private readonly IUserRepository _repository;

    public RegisterHandler(
        IUserRepository repository)
    {
        _repository = repository;
    }


    public async Task<UserDto> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);


        var existingUser = await _repository.GetByEmailAsync(
            email,
            cancellationToken);


        if (existingUser is not null)
            throw new DuplicateEmailException(request.Email);



        // Temporary until Authentication Infrastructure
        // This will be replaced by PasswordHasher service
        var passwordHash = PasswordHash.Create(
            request.Password);



        var user = User.Create(
            request.FullName,
            email,
            request.AvatarUrl);



        await _repository.AddAsync(
            user,
            cancellationToken);


        await _repository.SaveChangesAsync(
            cancellationToken);



        return new UserDto(
            user.Id,
            user.FullName,
            user.Email.Value,
            user.AvatarUrl);
    }
}