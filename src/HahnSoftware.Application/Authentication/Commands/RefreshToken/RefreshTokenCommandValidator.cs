using FluentValidation;

using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotNull().WithMessage(ValidationImmutable.Required("Token"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Token"));
    }
}
