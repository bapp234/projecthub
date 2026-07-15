using FluentValidation;
using ProjectHub.Domain.Constants;

namespace ProjectHub.Application.Features.Authentication.Commands.Register;

public sealed class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(ValidationConstants.FullNameMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(ValidationConstants.EmailMaxLength);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(PasswordConstants.MinimumLength)
            .MaximumLength(PasswordConstants.MaximumLength);

        RuleFor(x => x.AvatarUrl)
            .MaximumLength(ValidationConstants.AvatarUrlMaxLength);
    }
}