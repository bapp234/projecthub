using FluentValidation;
using ProjectHub.Domain.Messages;

namespace ProjectHub.Application.Features.Authentication.Commands.Refresh;

public sealed class RefreshCommandValidator
    : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage(ValidationMessages.RefreshTokenRequired);
    }
}