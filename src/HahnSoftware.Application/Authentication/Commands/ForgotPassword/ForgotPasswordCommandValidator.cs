using FluentValidation;

using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage(ValidationImmutable.Required("Email"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Email"))
            .EmailAddress().WithMessage(ValidationImmutable.InvalidFormat("Email"));
    }
}
