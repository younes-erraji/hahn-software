using FluentValidation;

using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.PostId)
            .NotNull().WithMessage(ValidationImmutable.Required("Post Id"));

        RuleFor(x => x.Content)
            .NotNull().WithMessage(ValidationImmutable.Required("Content"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Content"));
    }
}
