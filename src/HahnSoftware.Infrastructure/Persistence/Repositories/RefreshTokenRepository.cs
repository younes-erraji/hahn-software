using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;

namespace HahnSoftware.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository : EntityRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(HahnSoftwareDbContext context) : base(context)
    {
        
    }
}
