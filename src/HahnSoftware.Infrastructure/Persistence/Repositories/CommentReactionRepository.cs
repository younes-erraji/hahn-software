using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;

namespace HahnSoftware.Infrastructure.Persistence.Repositories;

public class CommentReactionRepository : EntityRepository<CommentReaction>, ICommentReactionRepository
{
    public CommentReactionRepository(HahnSoftwareDbContext context) : base(context)
    {

    }
}
