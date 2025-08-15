using FluentValidation;

using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.FirstName)
            .NotNull().WithMessage(ValidationImmutable.Required("First name"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("First name"))
            .MaximumLength(56).WithMessage(ValidationImmutable.MaximumLength("First name", 56));

        RuleFor(x => x.LastName)
            .NotNull().WithMessage(ValidationImmutable.Required("Last name"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Last name"))
            .MaximumLength(56).WithMessage(ValidationImmutable.MaximumLength("Last name", 56));

        RuleFor(x => x.Password)
            .NotNull().WithMessage(ValidationImmutable.Required("Password"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Password"))
            .MinimumLength(7).WithMessage(ValidationImmutable.MinimumLength("Password", 7))
            .MaximumLength(21).WithMessage(ValidationImmutable.MaximumLength("Password", 21))
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one number")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character");

        RuleFor(x => x.Mail)
            .NotNull().WithMessage(ValidationImmutable.Required("Email"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Email"))
            .EmailAddress().WithMessage(ValidationImmutable.InvalidFormat("Email"))
            .Must(x => userRepository.MailExists(x) == false).WithMessage(ValidationImmutable.Exists("Email"));
    }
}
