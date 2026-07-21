using FluentValidation;
using ProjectHub.Domain.Messages;

namespace ProjectHub.Application.Features.Authentication.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
.WithMessage(ValidationMessages.Required)
            .EmailAddress()
.WithMessage(ValidationMessages.InvalidEmail);

        RuleFor(x => x.Password)
            .NotEmpty()
  .WithMessage(ValidationMessages.Required);
    }
}