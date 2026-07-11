using MediatR;
using ProjectHub.Application.Features.Users.DTOs;
using ProjectHub.Application.Interfaces;

namespace ProjectHub.Application.Features.Users.Queries.GetUserById;

public sealed class GetUserByIdHandler
    : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _repository;

    public GetUserByIdHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto?> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (user is null)
            return null;

        return new UserDto(
            user.Id,
            user.FullName,
            user.Email,
            user.AvatarUrl);
    }
}