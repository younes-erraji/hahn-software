using FluentValidation;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Comments.Commands.ReplyComment;

public class ReplyCommentCommandValidator : AbstractValidator<ReplyCommentCommand>
{
    public ReplyCommentCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.CommentId)
            .NotNull().WithMessage(ValidationImmutable.Required("Comment Id"));

        RuleFor(x => x.Content)
            .NotNull().WithMessage(ValidationImmutable.Required("Content"))
            .NotEmpty().WithMessage(ValidationImmutable.Required("Content"));
    }
}
