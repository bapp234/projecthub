using MediatR;
using ProjectHub.Application.Features.Users.DTOs;
using ProjectHub.Application.Interfaces;
using ProjectHub.Domain.Entities;
using ProjectHub.Domain.Exceptions;

namespace ProjectHub.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserHandler
    : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        if (existingUser is not null)
            throw new DuplicateEmailException(request.Email);

        var user = User.Create(
            request.FullName,
            request.Email,
            request.AvatarUrl);

        await _repository.AddAsync(user, cancellationToken);

        await _repository.SaveChangesAsync(cancellationToken);

        return new UserDto(
            user.Id,
            user.FullName,
            user.Email,
            user.AvatarUrl);
    }
}