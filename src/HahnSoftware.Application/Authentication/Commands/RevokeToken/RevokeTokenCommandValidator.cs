using FluentValidation;

using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Authentication.Commands.RevokeToken;

public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotNull().WithMessage(ValidationImmutable.Required("Token"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Token"));
    }
}
