namespace ProjectHub.Application.Features.Users.DTOs;

public sealed record UserDto(
    Guid Id,
    string FullName,
    string Email,
    string? AvatarUrl);