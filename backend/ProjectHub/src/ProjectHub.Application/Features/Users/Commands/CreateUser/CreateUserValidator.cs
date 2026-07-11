using FluentValidation;
using ProjectHub.Domain.Constants;

namespace ProjectHub.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserValidator
    : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(ValidationConstants.FullNameMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.AvatarUrl)
            .MaximumLength(ValidationConstants.FullNameMaxLength);
    }
}