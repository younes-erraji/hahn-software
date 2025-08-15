using FluentValidation;

using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Authentication.Commands.ChangePassword;

public class ChangePasswordCommandHandler : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandHandler()
    {
        RuleFor(x => x.CurrentPassword)
            .NotNull().WithMessage(ValidationImmutable.Required("Current Password"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Current Password"));

        RuleFor(x => x.NewPassword)
            .NotNull().WithMessage(ValidationImmutable.Required("New Password"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("New Password"))
            .MinimumLength(6).WithMessage(ValidationImmutable.MinimumLength("New Password", 6))
            .MaximumLength(21).WithMessage(ValidationImmutable.MaximumLength("New Password", 21))
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one number")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character");

        RuleFor(x => x.NewPasswordConfirmation)
            .NotNull().WithMessage(ValidationImmutable.Required("Password Confirmation"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Password Confirmation"))
            .Must((model, password) => model.NewPassword == password).WithMessage("Password and confirmation must be the same");
    }
}
