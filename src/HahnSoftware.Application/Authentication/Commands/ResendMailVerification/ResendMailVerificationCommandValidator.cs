using FluentValidation;

using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Authentication.Commands.ResendMailVerification;

public class ResendMailVerificationCommandValidator : AbstractValidator<ResendMailVerificationCommand>
{
    public ResendMailVerificationCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage(ValidationImmutable.Required("Email"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Email"))
            .EmailAddress().WithMessage(ValidationImmutable.InvalidFormat("Email"));
    }
}
