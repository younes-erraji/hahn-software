using FluentValidation;

using HahnSoftware.Application.Authentication.Commands.MailVerification;
using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Authentication.Commands.Login;

public class MailVerificationCommandValidator : AbstractValidator<MailVerificationCommand>
{
    public MailVerificationCommandValidator()
    {
        RuleFor(x => x.Mail)
            .NotNull().WithMessage(ValidationImmutable.Required("Username"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Username"))
            .EmailAddress().WithMessage(ValidationImmutable.InvalidFormat("Username"));

        RuleFor(x => x.Token)
            .NotNull().WithMessage(ValidationImmutable.Required("Token"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Token"));
    }
}
