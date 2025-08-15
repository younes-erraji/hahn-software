using FluentValidation;

using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage(ValidationImmutable.Required("Email"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Email"))
            .EmailAddress().WithMessage(ValidationImmutable.InvalidFormat("Email"));

        RuleFor(x => x.Password)
            .NotNull().WithMessage(ValidationImmutable.Required("Password"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Password"));
    }
}
