using FluentValidation;
using ProjectHub.Domain.Constants;
using ProjectHub.Domain.Messages;

namespace ProjectHub.Application.Features.Authentication.Commands.Register;

public sealed class RegisterCommandValidator
    : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
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

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password)
            .WithMessage(ValidationMessages.PasswordsDoNotMatch);

        RuleFor(x => x.AvatarUrl)
            .MaximumLength(ValidationConstants.AvatarUrlMaxLength)
            .When(x => !string.IsNullOrWhiteSpace(x.AvatarUrl));
    }
}