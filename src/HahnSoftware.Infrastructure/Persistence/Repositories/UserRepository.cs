using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Exceptions;
using HahnSoftware.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HahnSoftware.Infrastructure.Persistence.Repositories;

public class UserRepository : EntityRepository<User>, IUserRepository
{
    public UserRepository(HahnSoftwareDbContext context) : base(context)
    {
    }

    public async Task<User> GetUser(string username)
    {
        User? user = await Query(x => x.Mail == username)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            throw new NotFoundException("User");
        }

        return user;
    }

    public async Task<User> GetUser(Guid userId)
    {
        User? user = await Query(x => x.Id == userId)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            throw new NotFoundException("User");
        }

        return user;
    }

    public bool MailExists(string mail)
    {
        return Query(x => x.Mail == mail).Any();
    }

    public async Task<User?> GetActiveRefreshTokenAsync(string refreshToken)
    {
        return await Query(x => x.RefreshTokens.Any(z => z.Token == refreshToken && z.Active))
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync();
    }
}
