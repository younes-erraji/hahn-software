using FluentValidation;

using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    /*
    public List<string> Tags { get; set; }
    */

    public CreatePostCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage(ValidationImmutable.Required("Title"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Title"))
            .MaximumLength(556).WithMessage(ValidationImmutable.MaximumLength("Title", 556));

        RuleFor(x => x.Body)
            .NotNull().WithMessage(ValidationImmutable.Required("Body"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Body"));
    }
}
