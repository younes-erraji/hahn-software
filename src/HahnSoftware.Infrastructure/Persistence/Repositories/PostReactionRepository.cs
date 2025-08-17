using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;

namespace HahnSoftware.Infrastructure.Persistence.Repositories;

public class PostReactionRepository : EntityRepository<PostReaction>, IPostReactionRepository
{
    public PostReactionRepository(HahnSoftwareDbContext context) : base(context)
    {

    }
}
