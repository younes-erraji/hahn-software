using FluentValidation;
using HahnSoftware.Application.Authentication.DTO;
using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandVaidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandVaidator()
    {
        RuleFor(x => x.Mail)
            .NotNull().WithMessage(ValidationImmutable.Required("Email"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Email"))
            .EmailAddress().WithMessage(ValidationImmutable.InvalidFormat("Email"));

        RuleFor(x => x.Token)
            .NotNull().WithMessage(ValidationImmutable.Required("Token"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Token"));

        RuleFor(x => x.NewPassword)
            .NotNull().WithMessage(ValidationImmutable.Required("New Password"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("New Password"))
            .MinimumLength(6).WithMessage(ValidationImmutable.MinimumLength("New Password", 6))
            .MaximumLength(21).WithMessage(ValidationImmutable.MaximumLength("New Password", 21))
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one number")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character");

        RuleFor(x => x.PasswordConfirmation)
            .NotNull().WithMessage(ValidationImmutable.Required("Password Confirmation"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Password Confirmation"))
            .MinimumLength(6).WithMessage(ValidationImmutable.MinimumLength("Password Confirmation", 6))
            .MaximumLength(21).WithMessage(ValidationImmutable.MaximumLength("Password Confirmation", 21))
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one number")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character")
            .Must((model, password) => model.NewPassword == password).WithMessage("");
    }
}
