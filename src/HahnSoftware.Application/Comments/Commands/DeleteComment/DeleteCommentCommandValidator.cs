using FluentValidation;

using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Immutable;

namespace HahnSoftware.Application.Comments.Commands.DeletePost;

public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.CommentId)
            .NotNull().WithMessage(ValidationImmutable.Required("Comment Id"));
    }
}
