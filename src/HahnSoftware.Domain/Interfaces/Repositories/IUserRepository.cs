using HahnSoftware.Domain.Entities;

namespace HahnSoftware.Domain.Interfaces.Repositories;

public interface IUserRepository : IEntityRepository<User>
{
    Task<User> GetUser(string username);
    Task<User> GetUser(Guid userId);
    bool MailExists(string mail);
    Task<User?> GetActiveRefreshTokenAsync(string refreshToken);
}
