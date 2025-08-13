using FluentValidation;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Posts.Commands.UpdatePost;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.CommentId)
            .NotNull().WithMessage(ValidationImmutable.Required("Comment Id"));

        RuleFor(x => x.Content)
            .NotNull().WithMessage(ValidationImmutable.Required("Content"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Content"));
    }
}
