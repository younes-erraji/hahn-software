using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;

namespace HahnSoftware.Infrastructure.Persistence.Repositories;

public class CommentRepository : EntityRepository<Comment>, ICommentRepository
{
    public CommentRepository(HahnSoftwareDbContext context) : base(context)
    {
        
    }
}
